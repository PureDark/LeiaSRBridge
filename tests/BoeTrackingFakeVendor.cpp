#include <cstdint>
#include <cstring>
#include <limits>

// Deliberately independent of the production protocol decoder. No camera or
// actual vendor SDK is loaded by this fixture.
extern "C" __declspec(dllexport) int __cdecl doEyeTrack_3D(
    const void* image, int width, int height, int format, void* output) {
    if (image != reinterpret_cast<const void*>(std::uintptr_t(0x123456789ABC)) ||
        height != 480 || format != 7 || !output) return -9;
    if (width == 1001) return 0; // Failure which leaves the result untouched.
    const float points[9]{12, -3, 600, -20, -5, 590, 44, -1, 610};
    std::memcpy(output, points, sizeof(points));
    if (width == 1002) {
        const float invalid = std::numeric_limits<float>::quiet_NaN();
        std::memcpy(static_cast<char*>(output) + 12, &invalid, sizeof(invalid));
    }
    if (width == 1003) {
        const float invalid = -1;
        std::memcpy(static_cast<char*>(output) + 8, &invalid, sizeof(invalid));
    }
    if (width == 1004) std::memcpy(static_cast<char*>(output) + 24,
        static_cast<char*>(output) + 12, 12); // Degenerate pair.
    auto* bytes = static_cast<std::uint8_t*>(output);
    for (unsigned i = 36; i < 168; ++i) bytes[i] = std::uint8_t(i);
    return 1;
}
// No guessed ABI is involved in production: these six names are PE forwarders.
extern "C" __declspec(dllexport) void initBoeEyeTrack_3D() {}
extern "C" __declspec(dllexport) void releaseBoeEyeTrack_3D() {}
extern "C" __declspec(dllexport) void setCameraMat_3D() {}
extern "C" __declspec(dllexport) void setEnableTrackArea_3D() {}
extern "C" __declspec(dllexport) void setFaceAreaTime_3D() {}
extern "C" __declspec(dllexport) void setHighConArea_3D() {}
