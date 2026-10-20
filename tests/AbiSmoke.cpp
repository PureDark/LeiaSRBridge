// Client-side ABI check: these are the unmodified SDK headers, with no
// COMPILING_DLL macros and no link to a Leia implementation/import library.
#include <sr/management/srcontext.h>
#include <sr/sense/display/switchablehint.h>
#include <sr/sense/headtracker/headtracker.h>
#include <sr/sense/headtracker/headposetracker.h>
#include <sr/sense/eyetracker/eyetracker.h>
#include <sr/weaver/dx12weaver.h>
#include <dxgi1_6.h>
#include <wrl/client.h>
#include "common/SharedProtocol.h"
#include <atomic>
#include <cmath>
#include <cstdio>
#include <filesystem>
#include <mutex>
#include <stdexcept>
#include <string>
#include <thread>

using Microsoft::WRL::ComPtr;
namespace {
void Require(bool condition, const char* message) { if (!condition) throw std::runtime_error(message); }
template<class T> T Symbol(HMODULE module, const char* name) {
    const auto symbol = GetProcAddress(module, name);
    if (!symbol) throw std::runtime_error(std::string("Missing export: ") + name);
    return reinterpret_cast<T>(symbol);
}
struct MockBroker {
    HANDLE mutex{}, mapping{};
    titan::SharedState* state{};
    std::atomic<bool> stopping{}, freezeObservation{}, freezePair{},freezePose{};
    std::thread heartbeat;
    std::mutex snapshotMutex;
    MockBroker() {
        mutex = CreateMutexW(nullptr, TRUE, titan::kBrokerMutexName);
        Require(mutex && GetLastError() != ERROR_ALREADY_EXISTS, "Close the real bridge before running AbiSmoke");
        mapping = CreateFileMappingW(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0, sizeof(titan::SharedState), titan::kMappingName);
        Require(mapping && GetLastError() != ERROR_ALREADY_EXISTS, "An existing bridge mapping is active");
        state = static_cast<titan::SharedState*>(MapViewOfFile(mapping, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(titan::SharedState)));
        Require(state != nullptr, "Cannot map mock broker state");
        *state = {};
        state->magic = titan::kProtocolMagic; state->version = titan::kProtocolVersion; state->size = sizeof(*state);
        titan::Snapshot snapshot{};
        snapshot.status = titan::ProviderStatus::Mock; snapshot.brokerPid = GetCurrentProcessId();
        snapshot.calibrationValid = snapshot.eyePositionValid = 1;
        snapshot.eye[2] = 600; snapshot.refractParams[0] = 1; snapshot.refractParams[1] = 1; snapshot.refractParams[2] = 1;
        snapshot.heartbeatTick = GetTickCount64();
        snapshot.observationTick=snapshot.heartbeatTick;
        titan::WriteSnapshot(state, snapshot);
        heartbeat = std::thread([this] {
            while (!stopping) {
                {
                    std::lock_guard lock(snapshotMutex);
                    titan::Snapshot snapshot{};
                    if (titan::ReadSnapshot(state, snapshot)) {
                        snapshot.heartbeatTick = GetTickCount64();
                        if(!freezeObservation)snapshot.observationTick=snapshot.heartbeatTick;
                        if(snapshot.eyePairValid&&!freezePair){snapshot.eyePairObservationTick=snapshot.heartbeatTick;++snapshot.eyePairSequence;}
                        if(snapshot.headPoseValid&&!freezePose){snapshot.headPoseObservationTick=snapshot.heartbeatTick;++snapshot.headPoseSequence;}
                        titan::WriteSnapshot(state, snapshot);
                    }
                }
                // Keep this test's mock snapshot a single-writer protocol.
                // A short lock here avoids racing SetEye with the heartbeat.
                Sleep(10);
            }
        });
    }
    ~MockBroker() {
        stopping = true;
        if (heartbeat.joinable()) heartbeat.join();
        if (state) UnmapViewOfFile(state);
        if (mapping) CloseHandle(mapping);
        if (mutex) { ReleaseMutex(mutex); CloseHandle(mutex); }
    }
    unsigned ClientCount() const {
        unsigned count = 0;
        for (const auto& client : state->clients) if (client.processId != 0) ++count;
        return count;
    }
    void Tracking(unsigned valid,float x=12,float y=4,float z=600,bool transform=false) {
        std::lock_guard lock(snapshotMutex);
        titan::Snapshot s{};Require(titan::ReadSnapshot(state,s),"Mock tracking read failed");
        s.eyePositionValid=valid;s.eye[0]=x;s.eye[1]=y;s.eye[2]=z;
        std::fill_n(s.wholeMat,6,0.0);
        if(transform){s.wholeMat[2]=90;s.wholeMat[3]=20;s.wholeMat[4]=-10;s.wholeMat[5]=5;}
        s.observationTick=GetTickCount64();s.heartbeatTick=s.observationTick;titan::WriteSnapshot(state,s);
    }
    void Pair(bool valid,float prediction=10,bool transform=true) {
        std::lock_guard lock(snapshotMutex);
        titan::Snapshot s{};Require(titan::ReadSnapshot(state,s),"Mock eye pair read failed");
        s.eyePairValid=valid?1:0;s.eyePairObservationTick=GetTickCount64();++s.eyePairSequence;
        s.eyePositionValid=titan::kEyeTracked;
        // Unequal depth plus a 90 degree wholeMat rotation verifies both full
        // 3D conversion and estimated ears following the measured eye axis.
        const float raw[2][3]={{-18,4,590},{42,4,610}};
        for(unsigned eye=0;eye<2;++eye)for(unsigned axis=0;axis<3;++axis){
            s.rawEyePair[eye][axis]=raw[eye][axis];
            s.eyePair[eye][axis]=raw[eye][axis]+(axis==0?prediction:0);
        }
        s.eyePairCenter[0]=12+prediction;s.eyePairCenter[1]=4;s.eyePairCenter[2]=600;
        std::fill_n(s.wholeMat,6,0.0);
        if(transform){s.wholeMat[2]=90;s.wholeMat[3]=20;s.wholeMat[4]=-10;s.wholeMat[5]=5;}
        s.heartbeatTick=GetTickCount64();titan::WriteSnapshot(state,s);
    }
    void SetEye(float x, bool calibrationValid = true) {
        std::lock_guard lock(snapshotMutex);
        titan::Snapshot snapshot{};
        Require(titan::ReadSnapshot(state, snapshot), "Cannot read mock snapshot");
        snapshot.eye[0] = x;
        snapshot.calibrationValid = calibrationValid ? 1 : 0;
        snapshot.heartbeatTick = GetTickCount64();
        titan::WriteSnapshot(state, snapshot);
    }
    void Pose(bool valid,bool malformed=false){
        std::lock_guard lock(snapshotMutex);
        titan::Snapshot s{};Require(titan::ReadSnapshot(state,s),"Mock pose read failed");
        s.headPoseValid=valid;s.headPoseObservationTick=GetTickCount64();++s.headPoseSequence;
        // Known +90 degree Y rotation; the runtime must not apply wholeMat twice.
        const double rotation[]{0,0,1,0,1,0,-1,0,0};
        std::copy_n(rotation,9,s.headRotation);
        if(malformed)s.headRotation[4]=2;
        titan::WriteSnapshot(state,s);
    }
};
template<class Frame,class Base> struct TrackingListener final : Base {
    std::mutex mutex;
    Frame latest{};
    unsigned count{};
    void accept(const Frame& frame) override{std::lock_guard lock(mutex);latest=frame;++count;}
    Frame Read(){std::lock_guard lock(mutex);return latest;}
    unsigned Count(){std::lock_guard lock(mutex);return count;}
};
template<class Predicate> void WaitFor(Predicate predicate,const char* failure){
    const auto end=GetTickCount64()+1500;
    while(!predicate()&&GetTickCount64()<end)Sleep(5);
    Require(predicate(),failure);
}

}

int wmain(int argc, wchar_t** argv) {
    try {
        const auto runtimeDirectory = argc > 1 ? std::filesystem::absolute(argv[1]) : std::filesystem::current_path();
        SetDefaultDllDirectories(LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
        Require(AddDllDirectory(runtimeDirectory.c_str()) != nullptr, "Cannot add adapter DLL directory");
        MockBroker broker;
        const auto load = [&runtimeDirectory](const wchar_t* name) {
            return LoadLibraryExW((runtimeDirectory / name).c_str(), nullptr,
                LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
        };
        const auto core = load(L"SimulatedRealityCore.dll");
        const auto directx = load(L"SimulatedRealityDirectX.dll");
        const auto displays = load(L"SimulatedRealityDisplays.dll");
        const auto faces = load(L"SimulatedRealityFaceTrackers.dll");
        Require(core && directx && displays && faces, "Cannot load adapter forwarding DLLs/dependencies");
        const auto create = Symbol<SR::SRContext*(*)(SR::SRContext::NetworkMode)>(core, "?create@SRContext@SR@@SAPEAV12@W4NetworkMode@12@@Z");
        const auto initialize = Symbol<void(*)(SR::SRContext*)>(core, "?initialize@SRContext@SR@@QEAAXXZ");
        const auto destroyContext = Symbol<void(*)(SR::SRContext*)>(core, "?deleteSRContext@SRContext@SR@@SAXPEAV12@@Z");
        const auto createLens = Symbol<decltype(&SR::SwitchableLensHint::create)>(displays, "?create@SwitchableLensHint@SR@@SAPEAV12@AEAVSRContext@2@@Z");
        const auto createWeaver = Symbol<decltype(&SR::CreateDX12Weaver)>(directx, "?CreateDX12Weaver@SR@@YA?AW4WeaverErrorCode@@PEAVSRContext@1@PEAUID3D12Device@@PEAUHWND__@@PEAPEAVIDX12Weaver1@1@@Z");
        const auto headFactory = Symbol<decltype(&SR::HeadTracker::create)>(faces, "?create@HeadTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
        const auto poseFactory = Symbol<decltype(&SR::HeadPoseTracker::create)>(faces, "?create@HeadPoseTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
        const auto eyeFactory = Symbol<decltype(&SR::EyeTracker::create)>(faces, "?create@EyeTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
        const auto rawEyeFactory=Symbol<decltype(&SR::EyeTracker::createRaw)>(faces, "?createRaw@EyeTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
        Symbol<void(*)()>(faces, "?stopListening@HeadStream@SR@@QEAAXXZ");
        Symbol<void(*)()>(faces, "?stopListening@HeadPoseStream@SR@@QEAAXXZ");
        const auto stopEyes=Symbol<void(*)(SR::EyePairStream*)>(faces, "?stopListening@EyePairStream@SR@@QEAAXXZ");
        const auto closeEyes=Symbol<void(*)(SR::EyePairStream*)>(faces, "?close@EyePairStream@SR@@QEAAXXZ");
        Symbol<void(*)()>(faces,"??0EyePairStream@SR@@QEAA@PEAVEyeTracker@1@PEAVEyePairListener@1@@Z");
        Symbol<void(*)()>(faces,"??1EyePairStream@SR@@QEAA@XZ");
        Symbol<void(*)()>(faces,"?update@EyePairStream@SR@@QEAAXUSR_eyePair@@@Z");

        auto* context = create(SR::SRContext::NonBlockingClientMode);
        auto* secondContext = create(SR::SRContext::StandaloneMode);
        Require(context && context->mode == SR::SRContext::NonBlockingClientMode, "SRContext header/layout mismatch");
        Require(secondContext->mode == SR::SRContext::StandaloneMode && broker.ClientCount() == 1, "Contexts do not share one runtime lease");
        // Simulate the broker expiring a descheduled client. The live runtime
        // must recover its lease without stealing another process's slot.
        bool reaped = false;
        const LONG pid = static_cast<LONG>(GetCurrentProcessId());
        for (auto& client : broker.state->clients) {
            if (InterlockedCompareExchange(&client.processId, -pid, pid) != pid) continue;
            InterlockedExchange(&client.requestLens, 0);
            InterlockedExchange64(&client.processCreationTime, 0);
            InterlockedExchange64(&client.heartbeatTick, 0);
            InterlockedExchange64(&client.lastWeaveTick, 0);
            InterlockedExchange64(&client.windowHandle, 0);
            InterlockedExchange(&client.processId, 0);
            reaped = true;
            break;
        }
        Require(reaped, "Could not simulate lease expiry");
        const auto deadline = GetTickCount64() + 1000;
        while (broker.ClientCount() != 1 && GetTickCount64() < deadline) Sleep(10);
        Require(broker.ClientCount() == 1, "Runtime did not recover an expired client lease");
        initialize(context);
        auto* lens = createLens(*context);
        Require(lens && createLens(*context) == lens, "Lens factory/lifetime mismatch");
        Require(!lens->getName().empty(), "Sense virtual dispatch failed");
        lens->enable(); lens->enable();
        Require(lens->isEnabledByPreference(), "Lens request virtual dispatch failed");
        Require(!lens->isEnabled(), "Mock should not claim hardware enabled");
        lens->disable(); lens->disable();
        Require(!lens->isEnabledByPreference(), "Duplicate lens toggles are not idempotent");
        const auto stopHead=Symbol<void(*)(SR::HeadStream*)>(faces,"?stopListening@HeadStream@SR@@QEAAXXZ");
        const auto stopPose=Symbol<void(*)(SR::HeadPoseStream*)>(faces,"?stopListening@HeadPoseStream@SR@@QEAAXXZ");
        auto* headTracker=headFactory(*secondContext);
        auto* poseTracker=poseFactory(*secondContext);
        auto* eyeTracker=eyeFactory(*secondContext);auto* rawEyeTracker=rawEyeFactory(*secondContext);
        Require(eyeTracker!=rawEyeTracker&&eyeFactory(*secondContext)==eyeTracker&&rawEyeFactory(*secondContext)==rawEyeTracker,
            "Predicted/raw tracker factories must be distinct and idempotent");
        Require(headFactory(*secondContext)==headTracker&&poseFactory(*secondContext)==poseTracker,"Tracker factories must be idempotent");
        TrackingListener<SR_head,SR::HeadListener> headListener,otherListener;
        TrackingListener<SR_headPose,SR::HeadPoseListener> poseListener;
        auto headStream=headTracker->openHeadStream(&headListener);
        auto otherStream=headTracker->openHeadStream(&otherListener);
        auto poseStream=poseTracker->openHeadPoseStream(&poseListener);
        TrackingListener<SR_eyePair,SR::EyePairListener> eyeListener,rawEyeListener,closedEyeListener;
        auto eyeStream=eyeTracker->openEyePairStream(&eyeListener);
        auto rawEyeStream=rawEyeTracker->openEyePairStream(&rawEyeListener);
        auto closedEyeStream=eyeTracker->openEyePairStream(&closedEyeListener);
        Sleep(20);Require(!headListener.Count()&&!poseListener.Count()&&!eyeListener.Count()&&!rawEyeListener.Count(),"Tracking started before initialize");
        broker.Tracking(titan::kEyeTracked,12,4,600,true);
        initialize(secondContext);
        WaitFor([&]{return headListener.Count()>1&&poseListener.Count()>1&&otherListener.Count()>1;},"Head streams did not publish");
        auto h=headListener.Read();auto p=poseListener.Read();
        Require(std::abs(h.headPose.position.x-16)<.001&&std::abs(h.headPose.position.y-2)<.001&&
            std::abs(h.headPose.position.z-605)<.001,"Head did not use monitor rotation/translation in millimetres");
        Require(std::abs(p.position.x-16)<.001&&p.time>1700000000000000ULL&&p.frameId>0,"Pose coordinate/time ABI mismatch");
        Require(std::isnan(p.orientation.x)&&std::isnan(h.eyes.left.x)&&std::isnan(h.ears.right.z),"Unsupported pose components were fabricated");
        broker.Tracking(titan::kEyeDefault);
        WaitFor([&]{return std::isnan(headListener.Read().headPose.position.x)&&std::isnan(poseListener.Read().position.x);},"Default optical eye was published as a tracked head");
        broker.Tracking(titan::kEyeHeld);Sleep(25);
        Require(std::isnan(headListener.Read().headPose.position.x),"Held optical eye was published as a tracked head");
        broker.Tracking(titan::kEyeTracked,-24,8,620);
        WaitFor([&]{return headListener.Read().headPose.position.x==-24&&poseListener.Read().position.z==620;},"Reacquisition did not update all axes");
        broker.freezeObservation=true;Sleep(350);
        Require(std::isnan(headListener.Read().headPose.position.x),"Broker heartbeat renewed a stale observation");
        broker.freezeObservation=false;
        WaitFor([&]{return headListener.Read().headPose.position.x==-24;},"Observation recovery failed");
        broker.Pair(true);
        WaitFor([&]{return std::isfinite(eyeListener.Read().left.x)&&std::isfinite(rawEyeListener.Read().left.x)&&
            std::isfinite(headListener.Read().eyes.left.x)&&std::abs(poseListener.Read().position.y-12)<.001;},"Measured pairs did not reach all SDK interfaces");
        h=headListener.Read();const auto eyes=eyeListener.Read(),rawEyes=rawEyeListener.Read();
        Require(std::abs(h.headPose.position.x-16)<.001&&std::abs(h.headPose.position.y-12)<.001&&
            std::abs(h.headPose.position.z-605)<.001,"Head must use the richer pair center");
        Require(std::abs(eyes.left.x-16)<.001&&std::abs(eyes.left.y+18)<.001&&std::abs(eyes.left.z-595)<.001&&
            std::abs(eyes.right.y-42)<.001&&std::abs(eyes.right.z-615)<.001,"Independent measured eye coordinates were lost/transformed incorrectly");
        Require(std::abs(rawEyes.left.y+28)<.001&&std::abs(rawEyes.right.y-32)<.001,"createRaw did not remove only bridge prediction");
        Require(std::abs(h.eyes.left.y-eyes.left.y)<.001&&std::abs(h.eyes.right.z-eyes.right.z)<.001&&
            std::isnan(h.headPose.orientation.x),"Head pair disagrees with EyeTracker or fabricated full orientation");
        const auto distance=[](const SR_point3d& a,const SR_point3d& b){const double x=a.x-b.x,y=a.y-b.y,z=a.z-b.z;return std::sqrt(x*x+y*y+z*z);};
        Require(std::abs(distance(h.ears.left,h.ears.right)-140)<.001&&
            std::abs(distance(h.ears.left,h.headPose.position)-70)<.001&&std::abs(distance(h.ears.right,h.headPose.position)-70)<.001,
            "Estimated ear model must be symmetric and 140 mm wide");
        Require(std::abs(h.ears.left.x-16)<.001&&h.ears.right.y>h.ears.left.y&&
            std::abs((h.ears.right.z-h.ears.left.z)/(h.ears.right.y-h.ears.left.y)-1.0/3)<.001,
            "Estimated ears did not follow the transformed measured eye axis");
        Require(eyes.time>1700000000000000ULL&&eyes.frameId>0&&h.eyes.frameId==h.frameId&&h.eyes.time==h.time,
            "Measured pair timestamp/frame ABI mismatch");
        broker.Pose(true);
        WaitFor([&]{return std::isfinite(headListener.Read().headPose.orientation.x)&&
            std::isfinite(poseListener.Read().orientation.x);},"Rotation did not reach Head and HeadPose");
        h=headListener.Read();p=poseListener.Read();
        Require(std::abs(p.orientation.x)<1e-6&&std::abs(p.orientation.y-1.5707963267948966)<1e-6&&
            std::abs(p.orientation.z)<1e-6&&std::abs(h.headPose.orientation.y-p.orientation.y)<1e-6,
            "Rotation vector ABI/sign/radian conversion wrong");
        Require(std::abs(h.ears.left.z-h.headPose.position.z-70)<1e-6&&
            std::abs(h.ears.right.z-h.headPose.position.z+70)<1e-6&&
            std::abs(h.eyes.left.y-eyes.left.y)<.001,"Pose ear model or measured eyes changed incorrectly");
        broker.freezePose=true;Sleep(350);
        Require(std::isnan(poseListener.Read().orientation.y)&&std::isfinite(eyeListener.Read().left.x),
            "Fresh eyes/heartbeat renewed stale rotation");
        broker.freezePose=false;broker.Pose(true,true);
        WaitFor([&]{return std::isnan(headListener.Read().headPose.orientation.y);},"Non-rotation matrix accepted");
        broker.Pose(true);
        WaitFor([&]{return std::isfinite(poseListener.Read().orientation.y);},"Rotation reacquisition failed");
        for(auto invalid:{titan::kEyeHeld,titan::kEyeDefault,0u}){
            broker.Tracking(invalid);
            WaitFor([&]{return std::isnan(poseListener.Read().orientation.x);},"Fallback eye retained rotation");
        }
        broker.Pair(true);broker.Pose(false);
        broker.freezePair=true;Sleep(350);
        Require(std::isnan(eyeListener.Read().left.x)&&std::isnan(rawEyeListener.Read().right.z)&&
            std::isnan(headListener.Read().eyes.left.x)&&std::isnan(headListener.Read().ears.left.x)&&
            std::isfinite(headListener.Read().headPose.position.x),"Fresh COM fallback incorrectly renewed stale pair/ears");
        broker.freezePair=false;
        WaitFor([&]{return std::isfinite(eyeListener.Read().left.x);},"Pair recovery failed");
        for(const auto invalid:{titan::kEyeDefault,titan::kEyeHeld,0u}){
            broker.Tracking(invalid);
            WaitFor([&]{return std::isnan(eyeListener.Read().left.x)&&std::isnan(rawEyeListener.Read().left.x)&&
                std::isnan(headListener.Read().eyes.left.x)&&std::isnan(headListener.Read().ears.left.x);},"Default/held/invalid eye was exposed as measured pair");
        }
        broker.Pair(false);broker.Tracking(titan::kEyeTracked);
        WaitFor([&]{return std::isnan(eyeListener.Read().left.x);},"Invalid pair did not clear SDK eyes");
        broker.Pair(true);
        WaitFor([&]{return std::isfinite(eyeListener.Read().left.x)&&std::isfinite(closedEyeListener.Read().left.x);},"Pair reacquisition failed");
        stopEyes(eyeStream.get());closeEyes(closedEyeStream.get());
        const auto eyeStopped=eyeListener.Count(),closedEyeCount=closedEyeListener.Count();Sleep(30);
        Require(eyeListener.Count()==eyeStopped&&closedEyeListener.Count()==closedEyeCount,"Eye stream close/stop did not fence callbacks");
        broker.Pair(false);broker.Tracking(titan::kEyeTracked);
        stopHead(otherStream.get());const auto stoppedCount=otherListener.Count();Sleep(30);
        Require(otherListener.Count()==stoppedCount,"Stopped listener received callbacks");
        // Stop/start via the actual SDK Sense vtable, retaining open streams.
        headTracker->stop();const auto pausedCount=headListener.Count();Sleep(30);
        Require(headListener.Count()==pausedCount,"Sense stop did not join its worker");
        headTracker->start();WaitFor([&]{return headListener.Count()>pausedCount;},"Sense restart failed");
        stopHead(headStream.get());stopPose(poseStream.get());
        const auto headStopped=headListener.Count(),poseStopped=poseListener.Count();
        Sleep(30);Require(headListener.Count()==headStopped&&poseListener.Count()==poseStopped,"stopListening did not fence callbacks");


        ComPtr<IDXGIFactory4> factory;
        ComPtr<IDXGIAdapter> adapter;
        ComPtr<ID3D12Device> device;
        Require(SUCCEEDED(CreateDXGIFactory2(0, IID_PPV_ARGS(&factory))), "Cannot create DXGI factory");
        Require(SUCCEEDED(factory->EnumWarpAdapter(IID_PPV_ARGS(&adapter))), "Cannot find WARP adapter");
        Require(SUCCEEDED(D3D12CreateDevice(adapter.Get(), D3D_FEATURE_LEVEL_11_0, IID_PPV_ARGS(&device))), "Cannot create WARP DX12 device");
        SR::IDX12Weaver1* weaver = reinterpret_cast<SR::IDX12Weaver1*>(1);
        Require(createWeaver(context, nullptr, nullptr, &weaver) != WeaverSuccess && !weaver, "Invalid factory input was not rejected");
        Require(createWeaver(context, device.Get(), nullptr, &weaver) == WeaverSuccess && weaver, "DX12 factory failed");
        Require(weaver->query<SR::IDX12Weaver1>() == weaver, "IDX12Weaver1 RTTI query ABI mismatch");
        auto* base = weaver->query<SR::IWeaverBase1>();
        auto* destroyable = weaver->query<SR::IDestroyable>();
        Require(base && destroyable, "Virtual base pointer adjustment failed");
        base->setLatency(123456);
        Require(weaver->getLatency() == 123456, "Base-to-derived virtual dispatch failed");
        weaver->setViewport({2, 3, 800, 600, 0, 1});
        weaver->setScissorRect({2, 3, 802, 603});
        weaver->setOutputFormat(DXGI_FORMAT_R8G8B8A8_UNORM);
        weaver->setShaderSRGBConversion(false, false);
        weaver->enableLateLatching(true);
        Require(!weaver->isLateLatchingEnabled(), "Unsupported late latching must not be advertised");
        float left[3]{}, right[3]{};
        weaver->getPredictedEyePositions(left, right);
        Require(std::isnan(left[0]) && std::isnan(right[2]), "Unavailable predicted eyes must not appear valid");
        broker.Pair(true);
        weaver->getPredictedEyePositions(left,right);
        Require(std::abs(left[1]+18)<.001&&std::abs(right[2]-615)<.001,"DX12 did not expose broker-predicted measured eyes");
        broker.freezePair=true;Sleep(350);
        weaver->getPredictedEyePositions(left,right);
        Require(std::isnan(left[0])&&std::isnan(right[2]),"DX12 published a stale measured pair");
        broker.freezePair=false;broker.Pair(false);
        weaver->getPredictedEyePositions(left,right);
        Require(std::isnan(left[0])&&std::isnan(right[2]),"DX12 retained an invalid measured pair");
        // The production wrappers do not catch weave exceptions. Missing state
        // must instead revoke the lens lease and return without GPU recording.
        weaver->weave();
        for (const auto& client : broker.state->clients)
            Require(client.requestLens == 0, "Incomplete DX12 state kept the lens lease");

        // Exercise the real runtime path with a WARP command list. Commands are
        // recorded only: no undefined texture contents are submitted to a GPU.
        const auto runtime = GetModuleHandleW(L"TitanLeiaRuntime.dll");
        const auto inspect = Symbol<BOOL(*)(ID3D12GraphicsCommandList*, titan::Snapshot*, std::uint64_t*)>(runtime,
            "TitanLeiaInspectRecordingSnapshot");
        ComPtr<ID3D12CommandAllocator> allocator, wrongAllocator;
        ComPtr<ID3D12GraphicsCommandList> commands, unrelated;
        Require(SUCCEEDED(device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, IID_PPV_ARGS(&allocator))), "Cannot create test allocator");
        Require(SUCCEEDED(device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_COPY, IID_PPV_ARGS(&wrongAllocator))), "Cannot create mismatched test allocator");
        Require(SUCCEEDED(device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, allocator.Get(), nullptr, IID_PPV_ARGS(&commands))), "Cannot create test command list");
        D3D12_HEAP_PROPERTIES heap{}; heap.Type = D3D12_HEAP_TYPE_DEFAULT;
        D3D12_RESOURCE_DESC desc{}; desc.Dimension = D3D12_RESOURCE_DIMENSION_TEXTURE2D;
        desc.Width = 8; desc.Height = 4; desc.DepthOrArraySize = 1; desc.MipLevels = 1;
        desc.Format = DXGI_FORMAT_R8G8B8A8_UNORM; desc.SampleDesc.Count = 1;
        ComPtr<ID3D12Resource> input, output;
        Require(SUCCEEDED(device->CreateCommittedResource(&heap, D3D12_HEAP_FLAG_NONE, &desc,
            D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE, nullptr, IID_PPV_ARGS(&input))), "Cannot create test SBS");
        desc.Width = 4; desc.Flags = D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET;
        Require(SUCCEEDED(device->CreateCommittedResource(&heap, D3D12_HEAP_FLAG_NONE, &desc,
            D3D12_RESOURCE_STATE_RENDER_TARGET, nullptr, IID_PPV_ARGS(&output))), "Cannot create test RTV texture");
        ComPtr<ID3D12DescriptorHeap> rtvs;
        D3D12_DESCRIPTOR_HEAP_DESC rtvDesc{}; rtvDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE_RTV; rtvDesc.NumDescriptors = 1;
        Require(SUCCEEDED(device->CreateDescriptorHeap(&rtvDesc, IID_PPV_ARGS(&rtvs))), "Cannot create test RTV heap");
        const auto rtv = rtvs->GetCPUDescriptorHandleForHeapStart();
        device->CreateRenderTargetView(output.Get(), nullptr, rtv);
        commands->OMSetRenderTargets(1, &rtv, FALSE, nullptr);
        weaver->setCommandList(commands.Get());
        weaver->setInputViewTexture(input.Get(), 8, 4, desc.Format);
        weaver->setViewport({0, 0, 4, 4, 0, 1});
        weaver->setScissorRect({0, 0, 4, 4});
        lens->enable();
        broker.Pair(true,10);
        broker.SetEye(10);
        weaver->weave();
        titan::Snapshot selected{};
        std::uint64_t generation{};
        Require(inspect(commands.Get(), &selected, &generation) && selected.eye[0] == 10, "First weave did not snapshot the broker eye");
        const auto firstGeneration = generation;
        weaver->getPredictedEyePositions(left,right);
        Require(std::abs(left[1]+18)<.001&&std::abs(right[1]-42)<.001,"DX12 selected pair differs from recording snapshot");
        broker.Pair(true,20);
        broker.SetEye(20);
        weaver->weave();
        Require(inspect(commands.Get(), &selected, &generation) && generation == firstGeneration && selected.eye[0] == 10,
            "Two passes in one command-list recording used different eyes");
        weaver->getPredictedEyePositions(left,right);
        Require(std::abs(left[1]+18)<.001&&std::abs(right[1]-42)<.001,"DX12 eye query changed within one command-list recording");
        Require(SUCCEEDED(commands->Close()), "Cannot close test command list");
        Require(FAILED(commands->Reset(wrongAllocator.Get(), nullptr)), "Mismatched allocator should reject Reset");
        Require(inspect(commands.Get(), &selected, &generation) && generation == firstGeneration && selected.eye[0] == 10,
            "Failed Reset invalidated the recording snapshot");
        Require(SUCCEEDED(commands->Reset(allocator.Get(), nullptr)), "Real WARP Reset failed");
        Require(!inspect(commands.Get(), &selected, &generation) && generation == firstGeneration + 1,
            "Successful Reset did not invalidate the recording snapshot");
        commands->OMSetRenderTargets(1, &rtv, FALSE, nullptr);
        weaver->weave();
        Require(inspect(commands.Get(), &selected, &generation) && generation == firstGeneration + 1 && selected.eye[0] == 20,
            "New recording reused the previous frame's optical snapshot");
        weaver->getPredictedEyePositions(left,right);
        Require(std::abs(left[1]+8)<.001&&std::abs(right[1]-52)<.001,"DX12 eye query retained the previous recording's pair");
        broker.SetEye(30, false);
        weaver->weave();
        for (const auto& client : broker.state->clients)
            Require(client.requestLens == 0, "Invalid live calibration did not immediately revoke the cached-frame lens lease");
        lens->disable();
        Require(SUCCEEDED(commands->Close()), "Cannot close final test recording");
        // Other lists share the patched implementation vtable but are not
        // tracked; their Reset must still forward directly and successfully.
        Require(SUCCEEDED(device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, allocator.Get(), nullptr, IID_PPV_ARGS(&unrelated))), "Cannot create unrelated test command list");
        Require(SUCCEEDED(unrelated->Close()) && SUCCEEDED(unrelated->Reset(allocator.Get(), nullptr)), "Untracked command list Reset forwarding failed");
        Require(!inspect(unrelated.Get(), &selected, &generation), "An unrelated list inherited another object's token");
        Require(SUCCEEDED(unrelated->Close()), "Cannot close unrelated command list");
        destroyable->destroy();
        destroyContext(context);
        Require(broker.ClientCount() == 1, "Deleting one context released a shared active lease");
        destroyContext(secondContext);
        const auto afterDestroy=rawEyeListener.Count();Sleep(30);
        Require(rawEyeListener.Count()==afterDestroy,"Context destruction did not stop an open eye stream");
        stopEyes(rawEyeStream.get());closeEyes(eyeStream.get());
        rawEyeStream.reset();eyeStream.reset();closedEyeStream.reset();
        stopHead(headStream.get());stopPose(poseStream.get());
        headStream.reset();otherStream.reset();poseStream.reset();
        Require(broker.ClientCount() == 0, "Final context deletion leaked IPC lease/thread");
        std::puts("PASS: SDK ABI, WARP weave, Reset generations, coherent multi-pass eye pairs, invalid-data fallback, measured eyes/raw eyes, estimated ears and tracking/lease lifetime");
        return 0;
    } catch (const std::exception& error) {
        std::fprintf(stderr, "FAIL: %s (Win32=%lu)\n", error.what(), GetLastError());
        return 1;
    }
}
