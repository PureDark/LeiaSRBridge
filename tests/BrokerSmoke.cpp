#include "../src/broker/BrokerService.h"
#include "../src/common/TrackingControl.h"
#include <cstdio>
#include <functional>
#include <stdexcept>

using namespace titan;
using namespace titan::broker;

namespace {
void Require(bool condition, const char* message) {
    if (!condition) throw std::runtime_error(message);
}
bool Eventually(const std::function<bool()>& predicate, DWORD timeout = 2000) {
    const auto start = GetTickCount64();
    do {
        if (predicate()) return true;
        Sleep(10);
    } while (GetTickCount64() - start < timeout);
    return predicate();
}
void Claim(ClientSlot& slot, std::uint64_t creation, std::uint64_t heartbeat,
           std::uint64_t weave = 0, LONG request = 0) {
    Require(InterlockedCompareExchange(&slot.processId, -1, 0) == 0, "slot was not empty");
    InterlockedExchange64(&slot.processCreationTime, static_cast<LONG64>(creation));
    InterlockedExchange64(&slot.heartbeatTick, static_cast<LONG64>(heartbeat));
    InterlockedExchange64(&slot.lastWeaveTick, static_cast<LONG64>(weave));
    InterlockedExchange64(&slot.windowHandle, 0);
    InterlockedExchange(&slot.requestLens, request);
    InterlockedExchange(&slot.processId, static_cast<LONG>(GetCurrentProcessId()));
}
bool Released(ClientSlot& slot) {
    return InterlockedCompareExchange(&slot.processId, 0, 0) == 0;
}
struct MappingView {
    HANDLE handle{};
    SharedState* state{};
    ~MappingView() { if (state) UnmapViewOfFile(state); if (handle) CloseHandle(handle); }
};
}

int main() {
    try {
        Require(!GetModuleHandleW(L"MCU_library.dll"), "MCU was already loaded before test");
        Require(!GetModuleHandleW(L"FaceTrackingManagerService.dll"), "eye provider was already loaded before test");
        ProviderOptions options;
        options.mock = true;
        options.horizontalTenths=std::make_shared<std::atomic<int>>(0);
        // Deliberately nonexistent: mock must never inspect or load this path.
        options.vendorDirectory = L"Z:\\ThisVendorDoesNotExist";
        {
            DeviceProvider provider(options, {});
            Require(provider.Initialize(), "mock provider initialization failed");
            Require(provider.State().status == ProviderStatus::Mock && provider.State().calibrationValid,
                    "mock state is not explicitly marked synthetic");
            Require(!provider.State().sensorFreshnessKnown, "mock invented sensor freshness");
            Require(provider.SetLens(true) && provider.State().lensEnabled, "mock ON transition failed");
            Require(provider.SetLens(false) && !provider.State().lensEnabled, "mock OFF transition failed");
            provider.Shutdown();
            Require(!provider.State().calibrationValid && !provider.State().eyePositionValid && !provider.State().lensEnabled,
                    "shutdown left usable hardware state behind");
        }
        BrokerService broker;
        Require(broker.Start(options, {}), "mock broker start failed; another broker may be running");
        Require(Eventually([&] { return broker.ReadStatus().status == ProviderStatus::Mock; }), "mock broker never became ready");
        BrokerService duplicate;
        Require(!duplicate.Start(options, {}), "second broker acquired hardware ownership");
        MappingView mapping;
        mapping.handle = OpenFileMappingW(FILE_MAP_ALL_ACCESS, FALSE, kMappingName);
        Require(mapping.handle != nullptr, "shared mapping missing");
        mapping.state = static_cast<SharedState*>(MapViewOfFile(mapping.handle, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(SharedState)));
        Require(mapping.state != nullptr, "could not map broker state");
        Snapshot snapshot{};
        Require(ReadSnapshot(mapping.state, snapshot), "snapshot protocol validation failed");
        Require(snapshot.brokerPid == GetCurrentProcessId() && snapshot.status == ProviderStatus::Mock,
                "snapshot identity or mode incorrect");
        const float originalX=snapshot.eye[0];
        options.horizontalTenths->store(160);
        Require(Eventually([&]{return ReadSnapshot(mapping.state,snapshot)&&snapshot.eye[0]==originalX+16.0f;}),"Global offset not published");
        Require(snapshot.eyePairValid&&snapshot.eyePairCenter[0]==16&&snapshot.eyePair[0][0]==-16&&
            snapshot.eyePair[1][0]==48&&snapshot.rawEyePair[0][0]==-16,"Global offset did not reach full pair/raw/center");
        Sleep(80);
        Require(ReadSnapshot(mapping.state,snapshot)&&snapshot.eye[0]==originalX+16.0f,"Global offset accumulated into raw eye");
        Require(snapshot.eyePair[0][0]==-16&&snapshot.rawEyePair[1][0]==48,"Pair offset accumulated");
        options.horizontalTenths->store(-50);
        Require(Eventually([&]{return ReadSnapshot(mapping.state,snapshot)&&snapshot.eye[0]==originalX-5.0f;}),"Negative offset failed");
        options.horizontalTenths->store(0);
        Require(Eventually([&]{return ReadSnapshot(mapping.state,snapshot)&&snapshot.eye[0]==originalX;}),"Global reset failed");
        TrackingView tuning;Require(tuning.Open()&&ControlAlive(tuning.data),"Tuning control missing");
        const auto creation = ProcessCreationTime(GetCurrentProcess());
        Require(creation != 0, "current process creation time unavailable");
        // No window is created, focused or activated by this test. A valid client
        // without a weave/lens request cannot turn on 3D, regardless of focus.
        Claim(mapping.state->clients[0], creation, GetTickCount64());
        Sleep(80);
        Require(!broker.ReadStatus().lensEnabled, "idle client enabled lens");
        // PID reuse must not accept a mismatched process creation time.
        Claim(mapping.state->clients[1], creation + 1, GetTickCount64(), GetTickCount64(), 1);
        Require(Eventually([&] { return Released(mapping.state->clients[1]); }), "wrong process generation was accepted");
        // No game process, serial port or camera is involved in lease expiry.
        Claim(mapping.state->clients[2], creation, GetTickCount64() + 10000, GetTickCount64(), 1);
        Require(Eventually([&] { return Released(mapping.state->clients[2]); }), "future/invalid heartbeat was accepted");
        // A process dying while publishing must not permanently strand a slot.
        // A mismatched creation time represents a previous generation of PID.
        auto& reserved = mapping.state->clients[3];
        Require(InterlockedCompareExchange(&reserved.processId, -1, 0) == 0, "reserved test slot occupied");
        InterlockedExchange64(&reserved.processCreationTime, static_cast<LONG64>(creation + 1));
        InterlockedExchange(&reserved.processId, -static_cast<LONG>(GetCurrentProcessId()));
        Require(Eventually([&] { return Released(reserved); }), "dead-generation reserved slot was stranded");
        Require(Eventually([&] { return Released(mapping.state->clients[0]); }, 3000), "expired lease was not reclaimed");
        Require(!broker.ReadStatus().lensEnabled, "invalid clients enabled lens");
        broker.Stop();
        Require(!ControlAlive(tuning.data),"Stop left live tuning request");
        Require(broker.ReadStatus().status == ProviderStatus::Stopped, "broker did not publish stopped state");
        Require(ReadSnapshot(mapping.state, snapshot) && !snapshot.lensEnabled && !snapshot.calibrationValid,
                "shared snapshot remained usable after shutdown");
        Require(!GetModuleHandleW(L"MCU_library.dll") && !GetModuleHandleW(L"FaceTrackingManagerService.dll"),
                "mock unexpectedly loaded vendor hardware modules");
        std::puts("BrokerSmoke PASS: mock lifecycle, singleton, IPC identity, invalid PID generation, lease expiry, clean shutdown; no hardware modules loaded.");
        return 0;
    } catch (const std::exception& error) {
        std::fprintf(stderr, "BrokerSmoke FAIL: %s\n", error.what());
        return 1;
    }
}
