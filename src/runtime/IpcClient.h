#pragma once

#include "common/SharedProtocol.h"
#include <atomic>
#include <memory>
#include <mutex>
#include <thread>

namespace titan {
// A process owns one lease; all contexts in this runtime share it.
class IpcClient final {
public:
    static std::shared_ptr<IpcClient> Connect();
    ~IpcClient();
    bool SnapshotForWeave(Snapshot& result) const noexcept;
    void AddLensRequest() noexcept;
    void RemoveLensRequest() noexcept;
    void MarkWeave(HWND window, bool usable) noexcept;
    bool IsLensEnabled() const noexcept;

private:
    IpcClient();
    void Run() noexcept;
    void PublishLease() noexcept;
    bool BeginSlotWrite() noexcept; // caller holds slotMutex_
    void EndSlotWrite() noexcept;
    HANDLE mapping_{};
    HANDLE stop_{};
    SharedState* state_{};
    ClientSlot* slot_{};
    SRWLOCK slotMutex_ = SRWLOCK_INIT;
    std::uint64_t creationTime_{};
    std::uint64_t lastWeaveTick_{};
    HWND window_{};
    std::atomic<long> lensRequests_{};
    std::atomic<bool> weaveUsable_{};
    std::thread worker_;
};

void RuntimeLog(const char* message) noexcept;
} // namespace titan
