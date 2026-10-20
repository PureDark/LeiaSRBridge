#pragma once
#include <Windows.h>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <type_traits>

namespace titan {
inline constexpr std::uint32_t kBoeTrackingMagic = 0x42544531;
inline constexpr std::uint32_t kBoeTrackingVersion = 2;
inline constexpr std::size_t kBoeResultSize = 168;

// Raw SDK coordinates, before the display calibration or user compensation.
// A/B denote SDK offsets 12/24, not a promise about mirrored anatomical sides.
struct BoeTrackingFrame {
    std::uint32_t magic{kBoeTrackingMagic};
    std::uint32_t version{kBoeTrackingVersion};
    std::uint32_t size{sizeof(BoeTrackingFrame)};
    std::uint32_t processId{};
    std::uint64_t processCreationTime{};
    std::uint64_t sequence{};          // SDK calls, including failures; not sensor frames
    std::uint64_t observationTick{};   // GetTickCount64 after SDK return; not exposure time
    std::int32_t sdkResult{};
    std::uint32_t centerValid{};
    std::uint32_t pairValid{};
    float center[3]{};
    float eyeA[3]{};
    float eyeB[3]{};
    std::uint8_t raw[kBoeResultSize]{};
    std::uint32_t facePointsValid{};
    float facePoints[2][5][2]{}; // same-frame subpixel counterparts of raw[84..163]
};
static_assert(std::is_trivially_copyable_v<BoeTrackingFrame>);
static_assert(sizeof(BoeTrackingFrame) == 344);
struct alignas(8) BoeTrackingState {
    volatile LONG64 committedSequence{};
    BoeTrackingFrame frame{};
};

inline std::uint64_t BoeProcessCreationTime(HANDLE process) noexcept {
    FILETIME creation{}, exit{}, kernel{}, user{};
    if (!GetProcessTimes(process, &creation, &exit, &kernel, &user)) return 0;
    ULARGE_INTEGER value{};
    value.LowPart = creation.dwLowDateTime; value.HighPart = creation.dwHighDateTime;
    return value.QuadPart;
}
inline void BoeTrackingNames(DWORD pid, wchar_t (&mapping)[96], wchar_t (&mutex)[96]) noexcept {
    swprintf_s(mapping, L"Local\\TitanLeiaBridge.BoeTracking.v2.%lu", pid);
    swprintf_s(mutex, L"Local\\TitanLeiaBridge.BoeTracking.Lock.v2.%lu", pid);
}
inline bool BoePointValid(const float (&point)[3]) noexcept {
    return std::isfinite(point[0]) && std::isfinite(point[1]) &&
        std::isfinite(point[2]) && point[2] > 0;
}
inline void DecodeBoeTrackingFrame(BoeTrackingFrame& frame) noexcept {
    std::memcpy(frame.center, frame.raw, sizeof(frame.center));
    std::memcpy(frame.eyeA, frame.raw + 12, sizeof(frame.eyeA));
    std::memcpy(frame.eyeB, frame.raw + 24, sizeof(frame.eyeB));
    frame.centerValid = frame.sdkResult == 1 && BoePointValid(frame.center);
    const double dx = double(frame.eyeA[0]) - frame.eyeB[0];
    const double dy = double(frame.eyeA[1]) - frame.eyeB[1];
    const double dz = double(frame.eyeA[2]) - frame.eyeB[2];
    frame.pairValid = frame.centerValid && BoePointValid(frame.eyeA) &&
        BoePointValid(frame.eyeB) && dx * dx + dy * dy + dz * dz > 0.000001;
}

// The kernel mutex protects every byte, including the header. A zero commit
// sequence additionally rejects interrupted writes after mutex abandonment.
class BoeTrackingReader {
public:
    BoeTrackingReader() = default;
    ~BoeTrackingReader() { Close(); }
    BoeTrackingReader(const BoeTrackingReader&) = delete;
    BoeTrackingReader& operator=(const BoeTrackingReader&) = delete;
    bool IsOpen() const noexcept { return view_ != nullptr; }
    bool IsAlive() const noexcept {
        return process_ && WaitForSingleObject(process_, 0) == WAIT_TIMEOUT;
    }
    void Close() noexcept {
        if (view_) UnmapViewOfFile(view_);
        if (mapping_) CloseHandle(mapping_);
        if (mutex_) CloseHandle(mutex_);
        if (process_) CloseHandle(process_);
        view_ = nullptr; mapping_ = mutex_ = process_ = nullptr;
        processId_ = 0; creationTime_ = 0;
    }
    bool Open(DWORD processId, std::uint64_t creationTime) noexcept {
        Close();
        if (!processId || !creationTime) return false;
        process_ = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION | SYNCHRONIZE, FALSE, processId);
        if (!process_ || BoeProcessCreationTime(process_) != creationTime ||
            WaitForSingleObject(process_, 0) != WAIT_TIMEOUT) { Close(); return false; }
        wchar_t mappingName[96]{}, mutexName[96]{};
        BoeTrackingNames(processId, mappingName, mutexName);
        mutex_ = OpenMutexW(SYNCHRONIZE | MUTEX_MODIFY_STATE, FALSE, mutexName);
        mapping_ = OpenFileMappingW(FILE_MAP_READ, FALSE, mappingName);
        if (mutex_ && mapping_) {
            view_ = static_cast<const BoeTrackingState*>(
                MapViewOfFile(mapping_, FILE_MAP_READ, 0, 0, sizeof(BoeTrackingState)));
        }
        if (!view_) { Close(); return false; }
        processId_ = processId; creationTime_ = creationTime;
        return true;
    }
    bool Read(BoeTrackingFrame& out) const noexcept {
        if (!view_ || !IsAlive()) return false;
        const DWORD wait = WaitForSingleObject(mutex_, 0);
        if (wait == WAIT_ABANDONED) { ReleaseMutex(mutex_); return false; }
        if (wait != WAIT_OBJECT_0) return false;
        BoeTrackingFrame frame{};
        const auto committed = std::uint64_t(view_->committedSequence);
        std::memcpy(&frame, &view_->frame, sizeof(frame));
        ReleaseMutex(mutex_);
        if (frame.magic != kBoeTrackingMagic || frame.version != kBoeTrackingVersion ||
            frame.size != sizeof(frame) || frame.processId != processId_ ||
            frame.processCreationTime != creationTime_ || !frame.sequence ||
            committed != frame.sequence) return false;
        out = frame;
        return true;
    }
private:
    HANDLE process_{}, mapping_{}, mutex_{};
    const BoeTrackingState* view_{};
    DWORD processId_{};
    std::uint64_t creationTime_{};
};
} // namespace titan
