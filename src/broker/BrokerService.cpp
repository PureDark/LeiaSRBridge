#include "BrokerService.h"
#include "../common/TrackingControl.h"
#include <algorithm>
#include <array>
#include <atomic>
#include <mutex>
#include <thread>

namespace titan::broker {
namespace {
class PollTimer {
public:
    PollTimer() : timer_(CreateWaitableTimerExW(nullptr, nullptr,
        CREATE_WAITABLE_TIMER_HIGH_RESOLUTION, TIMER_MODIFY_STATE | SYNCHRONIZE)) {}
    ~PollTimer() { if (timer_) CloseHandle(timer_); }
    bool HighResolution() const { return timer_ != nullptr; }
    bool Wait(HANDLE stop) {
        if (timer_) {
            LARGE_INTEGER due{};
            due.QuadPart = -40000; // Four milliseconds, in 100 ns units.
            if (SetWaitableTimer(timer_, &due, 0, nullptr, nullptr, FALSE)) {
                const HANDLE handles[] = {stop, timer_};
                return WaitForMultipleObjects(2, handles, FALSE, INFINITE) == WAIT_OBJECT_0 + 1;
            }
        }
        return WaitForSingleObject(stop, 4) == WAIT_TIMEOUT;
    }
private:
    HANDLE timer_{};
};
std::uint64_t ReadAtomic64(volatile LONG64* value) {
    return static_cast<std::uint64_t>(InterlockedCompareExchange64(value, 0, 0));
}
bool Recent(std::uint64_t tick, std::uint64_t now) {
    return tick && tick <= now && now - tick <= kLeaseMs;
}
void ReleaseSlot(ClientSlot& slot, LONG pid) {
    // Identify the process actually doing the reset. If the broker itself
    // crashes mid-reset, its replacement can prove this writer is dead.
    const LONG reserved = -static_cast<LONG>(GetCurrentProcessId());
    if (InterlockedCompareExchange(&slot.processId, reserved, pid) != pid) return;
    InterlockedExchange(&slot.requestLens, 0);
    InterlockedExchange64(&slot.processCreationTime, 0);
    InterlockedExchange64(&slot.heartbeatTick, 0);
    InterlockedExchange64(&slot.lastWeaveTick, 0);
    InterlockedExchange64(&slot.windowHandle, 0);
    InterlockedExchange(&slot.processId, 0);
}
void ReleaseDeadReservedSlot(ClientSlot& slot, LONG reserved) {
    // Only called after proving the negative PID's owning process is dead or
    // has been replaced by a different process generation. No writer remains.
    const LONG resetting = -static_cast<LONG>(GetCurrentProcessId());
    if (InterlockedCompareExchange(&slot.processId, resetting, reserved) != reserved) return;
    InterlockedExchange(&slot.requestLens, 0);
    InterlockedExchange64(&slot.processCreationTime, 0);
    InterlockedExchange64(&slot.heartbeatTick, 0);
    InterlockedExchange64(&slot.lastWeaveTick, 0);
    InterlockedExchange64(&slot.windowHandle, 0);
    InterlockedExchange(&slot.processId, 0);
}
} // namespace

struct BrokerService::Impl {
    struct ClientProcess {
        LONG pid{};
        std::uint64_t creation{};
        std::uint64_t firstSeen{};
        HANDLE handle{};
        void Reset() {
            if (handle) CloseHandle(handle);
            *this = {};
        }
    };
    ProviderOptions options;
    LogFunction logger;
    HANDLE singleton{}, mapping{}, stopEvent{};
    SharedState* shared{};
    HANDLE tuningMapping{};
    TrackingControl* tuning{};
    std::thread worker;
    mutable std::mutex statusMutex;
    Snapshot localStatus{};
    std::array<ClientProcess, kMaxClients> processes{};

    void Log(const std::wstring& text) { if (logger) logger(text); }
    void Publish(Snapshot snapshot) {
        // Apply once to the publication copy, after provider prediction/hold.
        // Never feed a fixed offset back into the raw tracking history.
        const int offset=options.horizontalTenths?std::clamp(options.horizontalTenths->load(),-300,300):0;
        if(snapshot.eyePositionValid)snapshot.eye[0]+=offset*0.1f;
        if(snapshot.eyePairValid){
            snapshot.eyePairCenter[0]+=offset*0.1f;
            for(unsigned i=0;i<2;++i){
                snapshot.eyePair[i][0]+=offset*0.1f;
                snapshot.rawEyePair[i][0]+=offset*0.1f;
            }
        }
        if(tuning){
            InterlockedExchange(&tuning->offsetTenths,offset);
            InterlockedExchange(&tuning->predictionMs,options.predictionMs?options.predictionMs->load():0);
            InterlockedExchange64(&tuning->heartbeat,GetTickCount64());
        }
        snapshot.brokerPid = GetCurrentProcessId();
        snapshot.heartbeatTick = GetTickCount64();
        if (shared) WriteSnapshot(shared, snapshot);
        std::lock_guard lock(statusMutex);
        localStatus = snapshot;
    }
    bool AnyEligibleLensClient() {
        const auto now = GetTickCount64();
        const HWND foreground = GetForegroundWindow();
        DWORD foregroundPid{};
        if (foreground) GetWindowThreadProcessId(foreground, &foregroundPid);
        const HWND foregroundRoot = foreground ? GetAncestor(foreground, GA_ROOT) : nullptr;
        bool requested = false;
        for (std::size_t i = 0; i != processes.size(); ++i) {
            auto& slot = shared->clients[i];
            auto& process = processes[i];
            const LONG pid = InterlockedCompareExchange(&slot.processId, 0, 0);
            if (!pid) { process.Reset(); continue; }
            if (pid < 0) {
                // Writers reserve a slot as -pid. Never expire a live writer
                // based on time: its thread may simply have been descheduled.
                if (pid == -1 || pid == LONG_MIN) continue;
                const LONG owner = -pid;
                const auto reservedCreation = ReadAtomic64(&slot.processCreationTime);
                if (process.pid != owner || (process.creation && reservedCreation && reservedCreation != process.creation)) {
                    process.Reset();
                    process.pid = owner;
                    process.firstSeen = now;
                }
                if (!process.handle) {
                    process.handle = OpenProcess(SYNCHRONIZE | PROCESS_QUERY_LIMITED_INFORMATION,
                                                 FALSE, static_cast<DWORD>(owner));
                    if (!process.handle && GetLastError() == ERROR_INVALID_PARAMETER) {
                        ReleaseDeadReservedSlot(slot, pid);
                        process.Reset();
                        continue;
                    }
                    if (process.handle) process.creation = ProcessCreationTime(process.handle);
                }
                if (process.handle && (WaitForSingleObject(process.handle, 0) != WAIT_TIMEOUT ||
                    (reservedCreation && ProcessCreationTime(process.handle) != reservedCreation))) {
                    ReleaseDeadReservedSlot(slot, pid);
                    process.Reset();
                }
                continue;
            }
            const auto creation = ReadAtomic64(&slot.processCreationTime);
            if (process.pid != pid || (process.creation && creation != process.creation)) {
                process.Reset();
                process.pid = pid;
                process.firstSeen = now;
            }
            const auto heartbeat = ReadAtomic64(&slot.heartbeatTick);
            // A client claims its PID before it can fill the other fields.
            if (!creation || !heartbeat) {
                if (now - process.firstSeen > kLeaseMs) { ReleaseSlot(slot, pid); process.Reset(); }
                continue;
            }
            if (!Recent(heartbeat, now)) { ReleaseSlot(slot, pid); process.Reset(); continue; }
            if (!process.handle) {
                process.handle = OpenProcess(SYNCHRONIZE | PROCESS_QUERY_LIMITED_INFORMATION,
                                             FALSE, static_cast<DWORD>(pid));
                if (!process.handle || ProcessCreationTime(process.handle) != creation) {
                    ReleaseSlot(slot, pid); process.Reset(); continue;
                }
                process.creation = creation;
            }
            if (WaitForSingleObject(process.handle, 0) != WAIT_TIMEOUT) {
                ReleaseSlot(slot, pid); process.Reset(); continue;
            }
            if (!InterlockedCompareExchange(&slot.requestLens, 0, 0) ||
                !Recent(ReadAtomic64(&slot.lastWeaveTick), now) ||
                foregroundPid != static_cast<DWORD>(pid)) continue;
            const HWND window = reinterpret_cast<HWND>(ReadAtomic64(&slot.windowHandle));
            if (window && (!IsWindow(window) || GetAncestor(window, GA_ROOT) != foregroundRoot)) continue;
            // Check that the client did not release/reclaim the slot mid-read.
            if (InterlockedCompareExchange(&slot.processId, 0, 0) == pid &&
                ReadAtomic64(&slot.processCreationTime) == creation) requested = true;
        }
        return requested;
    }
    void Run() {
        try {
            DeviceProvider provider(options, logger);
            Snapshot initial{};
            initial.status = ProviderStatus::Initializing;
            wcsncpy_s(initial.message, L"Initializing the monitor provider.", _TRUNCATE);
            Publish(initial);
            bool initialized = provider.Initialize();
            Publish(provider.State());
            // USB enumeration may finish after Windows has launched login apps.
            // Retry only transient absence/ownership failures, after releasing all handles.
            while(!initialized&&!options.mock &&
                (provider.State().status==ProviderStatus::NoDevice||provider.State().status==ProviderStatus::Busy)){
                Snapshot waiting=provider.State();provider.Shutdown();
                waiting.calibrationValid=waiting.eyePositionValid=waiting.eyePairValid=waiting.headPoseValid=waiting.lensEnabled=0;
                bool stop=false;
                for(int i=0;i<20;++i){
                    Publish(waiting);AnyEligibleLensClient();
                    if(WaitForSingleObject(stopEvent,250)!=WAIT_TIMEOUT){stop=true;break;}
                }
                if(stop)break;
                initialized=provider.Initialize();Publish(provider.State());
            }
            if (initialized) {
                PollTimer pollTimer;
                Log(pollTimer.HighResolution() ? L"Eye polling: 4 ms high-resolution wait (not sensor frame rate)." :
                    L"Eye polling: 4 ms requested; high-resolution timer unavailable, system timer precision applies.");
                while (pollTimer.Wait(stopEvent)) {
                    provider.Poll();
                    const auto& state = provider.State();
                    const bool deviceReady = state.status == ProviderStatus::Ready || state.status == ProviderStatus::Mock;
                    if (!deviceReady) {
                        const Snapshot failed = state;
                        Publish(failed);
                        provider.Shutdown();
                        while (WaitForSingleObject(stopEvent, 250) == WAIT_TIMEOUT) {
                            AnyEligibleLensClient();
                            Publish(failed);
                        }
                        break;
                    }
                    const bool requested = AnyEligibleLensClient();
                    provider.SetLens(deviceReady && state.calibrationValid && state.eyePositionValid && requested);
                    Publish(provider.State());
                }
            } else {
                // Keep the error visible and release any partially opened hardware.
                const Snapshot failed = provider.State();
                provider.Shutdown();
                while (WaitForSingleObject(stopEvent, 250) == WAIT_TIMEOUT) {
                    AnyEligibleLensClient();
                    Publish(failed);
                }
            }
            provider.Shutdown();
            Snapshot stopped = provider.State();
            stopped.status = ProviderStatus::Stopped;
            wcsncpy_s(stopped.message, L"Bridge stopped; lens off was requested and device ownership released.", _TRUNCATE);
            Publish(stopped);
        } catch (const std::exception& exception) {
            const std::string text(exception.what());
            Snapshot failed{};
            failed.status = ProviderStatus::Error;
            const std::wstring message = L"Broker worker exception: " + std::wstring(text.begin(), text.end());
            wcsncpy_s(failed.message, message.c_str(), _TRUNCATE);
            Publish(failed);
            Log(message);
        } catch (...) {
            Snapshot failed{};
            failed.status = ProviderStatus::Error;
            wcsncpy_s(failed.message, L"Broker worker stopped after an unexpected exception.", _TRUNCATE);
            Publish(failed);
            Log(failed.message);
        }
        for (auto& process : processes) process.Reset();
    }
    bool Start(const ProviderOptions& settings, LogFunction log) {
        if (worker.joinable()) return false;
        options = settings;
        logger = std::move(log);
        singleton = CreateMutexW(nullptr, FALSE, kBrokerMutexName);
        if (!singleton || GetLastError() == ERROR_ALREADY_EXISTS) {
            if (singleton) { CloseHandle(singleton); singleton = nullptr; }
            Log(L"Another Stereo3DBridge broker is already running.");
            return false;
        }
        mapping = CreateFileMappingW(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0,
                                     sizeof(SharedState), kMappingName);
        const bool existing = GetLastError() == ERROR_ALREADY_EXISTS;
        if (!mapping) { Stop(); return false; }
        shared = static_cast<SharedState*>(MapViewOfFile(mapping, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(SharedState)));
        if (!shared) { Stop(); return false; }
        if (!existing || shared->magic != kProtocolMagic || shared->version != kProtocolVersion ||
            shared->size != sizeof(SharedState)) {
            ZeroMemory(shared, sizeof(SharedState));
            shared->version = kProtocolVersion;
            shared->size = sizeof(SharedState);
            MemoryBarrier();
            shared->magic = kProtocolMagic;
        } else {
            // A previous broker may have crashed during a seqlock write.
            // No other broker can exist while this process owns singleton.
            InterlockedExchange(&shared->sequence, 0);
        }
        tuningMapping=CreateFileMappingW(INVALID_HANDLE_VALUE,nullptr,PAGE_READWRITE,0,sizeof(TrackingControl),kTrackingControlName);
        if(tuningMapping)tuning=static_cast<TrackingControl*>(MapViewOfFile(tuningMapping,FILE_MAP_ALL_ACCESS,0,0,sizeof(TrackingControl)));
        if(!tuning){Stop();return false;}
        ZeroMemory(tuning,sizeof(*tuning));tuning->brokerPid=GetCurrentProcessId();
        MemoryBarrier();tuning->magic=kTrackingControlMagic;
        stopEvent = CreateEventW(nullptr, TRUE, FALSE, nullptr);
        if (!stopEvent) { Stop(); return false; }
        try { worker = std::thread([this] { Run(); }); }
        catch (...) { Stop(); return false; }
        return true;
    }
    void Stop() {
        if (stopEvent) SetEvent(stopEvent);
        if (worker.joinable()) worker.join();
        if(tuning){
            InterlockedExchange64(&tuning->heartbeat,0);
            UnmapViewOfFile(tuning);tuning=nullptr;
        }
        if(tuningMapping){CloseHandle(tuningMapping);tuningMapping=nullptr;}
        if (stopEvent) { CloseHandle(stopEvent); stopEvent = nullptr; }
        if (shared) { UnmapViewOfFile(shared); shared = nullptr; }
        if (mapping) { CloseHandle(mapping); mapping = nullptr; }
        if (singleton) { CloseHandle(singleton); singleton = nullptr; }
    }
};

BrokerService::BrokerService() : impl_(std::make_unique<Impl>()) {}
BrokerService::~BrokerService() { Stop(); }
bool BrokerService::Start(const ProviderOptions& options, LogFunction logger) {
    return impl_->Start(options, std::move(logger));
}
void BrokerService::Stop() { impl_->Stop(); }
Snapshot BrokerService::ReadStatus() const {
    std::lock_guard lock(impl_->statusMutex);
    return impl_->localStatus;
}
} // namespace titan::broker
