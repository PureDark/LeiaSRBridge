#pragma once
// Static-analysis record for the x64 interlaceSDK.dll shipped with
// C:\Program Files (x86)\DepthTransformerPlus (2026-10-20 analysis).
// SHA256: 380A9194A5C2AB42CF1F70A2B3C383F9B340B3B292AC72177350AA0DAE56CC6F
// These declarations have NOT been validated by executing the vendor SDK.
// See depth_interlace_analysis.md for DX12 state/format/lifetime restrictions.
#include <cstdint>

namespace recovered_boe {
enum class D3DVersion : int { DX11 = 11, DX12 = 12 };
enum class InterMode : int { LR = 0, RL = 1, TB = 2, BT = 3 };

using InitSDK = int (__cdecl *)(
    void* device, void* inputResource,
    const double* deviceParams6, const double* wholeMat6,
    const double* refractParams8, int dxVersion,
    int viewportLeft, int viewportTop, int viewportWidth, int viewportHeight,
    int outputDxgiFormat, const char* logPath);

// DX11: context=ID3D11DeviceContext*, output=ID3D11RenderTargetView*,
//       inputOrRtv=ID3D11ShaderResourceView* (packed stereo).
// DX12: context=ID3D12GraphicsCommandList*, output=ID3D12Resource*,
//       inputOrRtv=D3D12_CPU_DESCRIPTOR_HANDLE* for OUTPUT.
//       The INPUT resource was supplied to InitSDK and is reused.
// This export always returns 1 after forwarding, even if implementation fails.
using DoInterleave = int (__cdecl *)(
    float x, float y, float z, void* context, void* output, void* inputOrRtv,
    int inputWidth, int inputHeight, int inputDxgiFormat,
    int interMode, int sRGBFix);

using SetWholeParam = void (__cdecl *)(const double* params6);
using SetDeviceParam = void (__cdecl *)(const double* params6);
using SetRefractDeviceParam = void (__cdecl *)(const double* params8);
using ReleaseSDK = void (__cdecl *)(); // Observed implementation only logs.

// Reflected byte layout of shader 2.1 (not a vendor public structure).
struct Shader21Constants {
    float Pixel, PlaceHeight, Pitch, Angle;
    float Offset, Zadd, Ng, Beta;
    float Cr;
    float Position[3];
    std::uint32_t NLayoutMode, TexLayoutType, Width, Height;
    std::uint32_t LeftTextureWidth, LeftTextureHeight;
    std::uint32_t RightTextureWidth, RightTextureHeight; // unused in this DXBC
};
static_assert(sizeof(Shader21Constants) == 80);
} // namespace recovered_boe
