#pragma once
#include <Windows.h>
#include <cstdint>
#include <type_traits>

namespace titan {
inline constexpr wchar_t kMappingName[] = L"Local\\TitanLeiaBridge.State.v3";
inline constexpr wchar_t kBrokerMutexName[] = L"Local\\TitanLeiaBridge.Broker.v1";
inline constexpr std::uint32_t kProtocolMagic = 0x544C4231;
inline constexpr std::uint32_t kProtocolVersion = 3;
inline constexpr std::uint32_t kMaxClients = 32;
inline constexpr std::uint64_t kLeaseMs = 2000;
// All nonzero values remain usable for optical weaving.
inline constexpr std::uint32_t kEyeTracked = 1;
inline constexpr std::uint32_t kEyeDefault = 2;
inline constexpr std::uint32_t kEyeHeld = 3;

enum class ProviderStatus : std::uint32_t {
    Stopped, Initializing, Ready, NoDevice, Busy, Error, Mock
};

// Only broker writes Snapshot; sequence brackets memcpy with MemoryBarrier.
// observationTick is a broker observation, NEVER a sensor frame timestamp.
struct Snapshot {
    std::uint32_t brokerPid{};
    ProviderStatus status{ProviderStatus::Stopped};
    std::uint64_t heartbeatTick{};
    std::uint64_t observationTick{};
    std::uint32_t calibrationValid{};
    std::uint32_t eyePositionValid{}; // 0 unavailable, 1 tracked (or mock), 2 default, 3 held
    std::uint32_t sensorFreshnessKnown{};
    std::uint32_t lensEnabled{};
    double deviceParams[6]{};
    double wholeMat[6]{};
    double refractParams[8]{};
    float eye[3]{};
    std::uint32_t stereoMode{}; // 0 LR, 1 RL; bridge default is real L|R SBS.
    wchar_t message[256]{};
    // One complete vendor result, observed at the SDK boundary. Never sensor time.
    std::uint32_t eyePairValid{};
    std::uint64_t eyePairObservationTick{};
    std::uint64_t eyePairSequence{};
    float eyePairCenter[3]{};
    float eyePair[2][3]{}; // left/right in display X order, still camera-space mm
    float rawEyePair[2][3]{}; // same vendor-filtered sample, no bridge prediction
    std::uint32_t headPoseValid{};
    std::uint64_t headPoseObservationTick{};
    std::uint64_t headPoseSequence{};
    double headRotation[9]{}; // row-major display-space rotation, independent validity
};

// One process owns each slot. 64-bit fields are aligned for Interlocked*64.
struct alignas(8) ClientSlot {
    volatile LONG processId{}; // claim with InterlockedCompareExchange(0 -> pid)
    volatile LONG requestLens{};
    volatile LONG64 processCreationTime{};
    volatile LONG64 heartbeatTick{};
    volatile LONG64 lastWeaveTick{};
    volatile LONG64 windowHandle{};
};

struct alignas(8) SharedState {
    std::uint32_t magic{};
    std::uint32_t version{};
    std::uint32_t size{};
    volatile LONG sequence{};
    Snapshot snapshot{};
    ClientSlot clients[kMaxClients]{};
};
static_assert(std::is_trivially_copyable_v<Snapshot>);

inline bool ReadSnapshot(const SharedState* state, Snapshot& out) noexcept {
    if (!state || state->magic != kProtocolMagic ||
        state->version != kProtocolVersion || state->size != sizeof(SharedState)) return false;
    for (int retry = 0; retry != 8; ++retry) {
        const LONG before = state->sequence;
        if (before & 1) { YieldProcessor(); continue; }
        MemoryBarrier();
        memcpy(&out, &state->snapshot, sizeof(out));
        MemoryBarrier();
        if (before == state->sequence) return true;
    }
    return false;
}
inline void WriteSnapshot(SharedState* state, const Snapshot& snapshot) noexcept {
    InterlockedIncrement(&state->sequence);
    MemoryBarrier();
    memcpy(&state->snapshot, &snapshot, sizeof(snapshot));
    MemoryBarrier();
    InterlockedIncrement(&state->sequence);
}
inline std::uint64_t ProcessCreationTime(HANDLE process) noexcept {
    FILETIME creation{}, exit{}, kernel{}, user{};
    if (!GetProcessTimes(process, &creation, &exit, &kernel, &user)) return 0;
    ULARGE_INTEGER value{};
    value.LowPart = creation.dwLowDateTime; value.HighPart = creation.dwHighDateTime;
    return value.QuadPart;
}
} // namespace titan
