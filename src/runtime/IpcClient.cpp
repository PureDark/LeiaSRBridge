#include "IpcClient.h"
#include <cmath>
#include <filesystem>
#include <cstdio>
#include <stdexcept>
#include <string>
#include <fstream>
#include <algorithm>

namespace titan {
namespace {
std::mutex connectionMutex;
std::weak_ptr<IpcClient> connection;
bool Recent(std::uint64_t then, std::uint64_t now) noexcept {
    return then != 0 && then <= now && now - then <= kLeaseMs;
}
struct SlotLock {
    explicit SlotLock(SRWLOCK& lock) : lock(lock) { AcquireSRWLockExclusive(&lock); }
    ~SlotLock() { ReleaseSRWLockExclusive(&lock); }
    SRWLOCK& lock;
};

}

void RuntimeLog(const char* message) noexcept {
    OutputDebugStringA("[TitanLeiaBridge] ");
    OutputDebugStringA(message);
    OutputDebugStringA("\n");
    try {
        static std::mutex logMutex;
        std::lock_guard lock(logMutex);
        HMODULE module{};
        if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&RuntimeLog), &module)) return;
        wchar_t path[32768]{};
        const DWORD length = GetModuleFileNameW(module, path, static_cast<DWORD>(std::size(path)));
        if (!length || length == std::size(path)) return;
        const auto logDirectory = std::filesystem::path(path).parent_path().parent_path() / L"logs";
        std::error_code ignored;
        std::filesystem::create_directories(logDirectory, ignored);
        const auto logPath = logDirectory / (L"runtime-" + std::to_wstring(GetCurrentProcessId()) + L".log");
        FILE* file{};
        if (_wfopen_s(&file, logPath.c_str(), L"ab") == 0 && file) {
            SYSTEMTIME time{}; GetLocalTime(&time);
            std::fprintf(file, "%04u-%02u-%02u %02u:%02u:%02u.%03u %s\r\n", time.wYear, time.wMonth,
                time.wDay, time.wHour, time.wMinute, time.wSecond, time.wMilliseconds, message);
            std::fflush(file);
            std::fclose(file);
        }
    } catch (...) { /* Logging must never cross the SDK boundary. */ }
}

std::shared_ptr<IpcClient> IpcClient::Connect() {
    std::lock_guard lock(connectionMutex);
    if (auto current = connection.lock()) return current;
    auto current = std::shared_ptr<IpcClient>(new IpcClient());
    connection = current;
    return current;
}

IpcClient::IpcClient() {
    mapping_ = OpenFileMappingW(FILE_MAP_ALL_ACCESS, FALSE, kMappingName);
    if (!mapping_) throw std::runtime_error("TitanLeiaBridge must be running before creating a Leia context");
    state_ = static_cast<SharedState*>(MapViewOfFile(mapping_, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(SharedState)));
    Snapshot snapshot{};
    if (!ReadSnapshot(state_, snapshot) || !Recent(snapshot.heartbeatTick, GetTickCount64())) {
        if (state_) UnmapViewOfFile(state_);
        CloseHandle(mapping_);
        state_ = nullptr;
        mapping_ = nullptr;
        throw std::runtime_error("TitanLeiaBridge IPC version or broker heartbeat is invalid");
    }
    creationTime_ = ProcessCreationTime(GetCurrentProcess());
    if (creationTime_ && BeginSlotWrite()) EndSlotWrite();
    stop_ = CreateEventW(nullptr, TRUE, FALSE, nullptr);
    if (!slot_ || !stop_) {
        if (slot_) InterlockedExchange(&slot_->processId, 0);
        if (stop_) CloseHandle(stop_);
        UnmapViewOfFile(state_);
        CloseHandle(mapping_);
        throw std::runtime_error("TitanLeiaBridge cannot allocate an IPC client lease");
    }
    try { worker_ = std::thread(&IpcClient::Run, this); }
    catch (...) {
        InterlockedExchange(&slot_->processId, 0);
        CloseHandle(stop_);
        UnmapViewOfFile(state_);
        CloseHandle(mapping_);
        throw;
    }
}

IpcClient::~IpcClient() {
    SetEvent(stop_);
    if (worker_.joinable()) worker_.join();
    const LONG pid = static_cast<LONG>(GetCurrentProcessId());
    if (slot_ && InterlockedCompareExchange(&slot_->processId, -pid, pid) == pid) {
        InterlockedExchange(&slot_->requestLens, 0);
        InterlockedExchange64(&slot_->heartbeatTick, 0);
        InterlockedExchange64(&slot_->lastWeaveTick, 0);
        InterlockedExchange64(&slot_->windowHandle, 0);
        InterlockedExchange64(&slot_->processCreationTime, 0);
        InterlockedExchange(&slot_->processId, 0);
    }
    CloseHandle(stop_);
    UnmapViewOfFile(state_);
    CloseHandle(mapping_);
}

bool IpcClient::SnapshotForWeave(Snapshot& result) const noexcept {
    if (!ReadSnapshot(state_, result) || !Recent(result.heartbeatTick, GetTickCount64()) ||
        (result.status != ProviderStatus::Ready && result.status != ProviderStatus::Mock) ||
        !result.calibrationValid || !result.eyePositionValid) return false;
    for (double value : result.deviceParams) if (!std::isfinite(value)) return false;
    for (double value : result.wholeMat) if (!std::isfinite(value)) return false;
    for (double value : result.refractParams) if (!std::isfinite(value)) return false;
    for (float value : result.eye) if (!std::isfinite(value)) return false;
    return result.eye[2] > 0.0f;
}

void IpcClient::PublishLease() noexcept {
    Snapshot snapshot{};
    const bool usable = weaveUsable_.load(std::memory_order_relaxed) && SnapshotForWeave(snapshot);
    SlotLock lock(slotMutex_);
    if (!BeginSlotWrite()) return;
    InterlockedExchange(&slot_->requestLens, usable && lensRequests_.load(std::memory_order_relaxed) > 0 ? 1 : 0);
    InterlockedExchange64(&slot_->heartbeatTick, GetTickCount64());
    InterlockedExchange64(&slot_->lastWeaveTick, lastWeaveTick_);
    InterlockedExchange64(&slot_->windowHandle, reinterpret_cast<LONG64>(window_));
    EndSlotWrite();
}

bool IpcClient::BeginSlotWrite() noexcept {
    const LONG pid = static_cast<LONG>(GetCurrentProcessId());
    if (slot_ && InterlockedCompareExchange(&slot_->processId, -pid, pid) == pid) {
        if (static_cast<std::uint64_t>(slot_->processCreationTime) == creationTime_) return true;
        InterlockedExchange(&slot_->processId, pid);
    }
    // A suspended process may have lost its lease. Claim an empty slot again,
    // never overwrite a different owner. A negative PID is a short write lock
    // whose owner can still be recovered by the broker after a process crash.
    slot_ = nullptr;
    for (auto& candidate : state_->clients) {
        if (InterlockedCompareExchange(&candidate.processId, -pid, 0) == 0) {
            slot_ = &candidate;
            InterlockedExchange(&slot_->requestLens, 0);
            InterlockedExchange64(&slot_->processCreationTime, creationTime_);
            InterlockedExchange64(&slot_->lastWeaveTick, 0);
            InterlockedExchange64(&slot_->windowHandle, 0);
            InterlockedExchange64(&slot_->heartbeatTick, GetTickCount64());
            return true;
        }
    }
    return false;
}

void IpcClient::EndSlotWrite() noexcept {
    InterlockedExchange(&slot_->processId, static_cast<LONG>(GetCurrentProcessId()));
}

void IpcClient::Run() noexcept {
    while (WaitForSingleObject(stop_, 100) == WAIT_TIMEOUT) PublishLease();
}

void IpcClient::AddLensRequest() noexcept {
    lensRequests_.fetch_add(1, std::memory_order_relaxed);
    PublishLease();
}
void IpcClient::RemoveLensRequest() noexcept {
    lensRequests_.fetch_sub(1, std::memory_order_relaxed);
    PublishLease();
}
void IpcClient::MarkWeave(HWND window, bool usable) noexcept {
    weaveUsable_.store(usable, std::memory_order_relaxed);
    {
        SlotLock lock(slotMutex_);
        window_ = window;
        lastWeaveTick_ = usable ? GetTickCount64() : 0;
    }
    PublishLease();
}
bool IpcClient::IsLensEnabled() const noexcept {
    Snapshot snapshot{};
    return ReadSnapshot(state_, snapshot) && Recent(snapshot.heartbeatTick, GetTickCount64()) && snapshot.lensEnabled;
}
} // namespace titan
