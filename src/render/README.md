# DX12 renderer implementation notes

`Renderer::Record` records one fullscreen triangle into the render target already
bound by the caller. It does not call `OMSetRenderTargets`, issue barriers, submit,
or present. The SBS texture must already be in `PIXEL_SHADER_RESOURCE`; the host
retains ownership of that texture through GPU completion. Each call records its
own 80-byte optical constants as root constants, so multiple calls in one command
list do not overwrite a shared upload buffer.

The optical PS is a mechanical translation of the original BOE shader 2.1 CS,
extracted from the hash-checked `interlaceSDK.dll` analysis copy. The translation
uses exact hexadecimal DXBC literals and preserves signed integer modifiers,
register masks, layouts, loops and texture Loads. The stage changes are viewport
local `SV_Position` in place of `SV_DispatchThreadID`, and `SV_Target` in place of
the final UAV store. There is no depth estimation, capture, or stereo synthesis.

`OpticalConstants.h` reproduces the native float conversion, Euler matrix and
translation at `0x18005EBD0` / `0x18005F160`, and constant mapping at
`0x18005F530`. The first three `wholeMat` doubles are degrees; the last three are
translations. The misleading IDA `log` thunks were separately resolved to `cosf`
and `sinf`. `RE/evidence/bridge-renderer` contains those checks.

Descriptor heaps, root signatures and PSOs remain in a bounded process cache to
survive `Delete()` before GPU completion. Each new input texture obtains a GUID
private-data identity; pointer reuse cannot alias an old descriptor. Descriptors
are never overwritten. The cache retains no historical input resources. Limits
are 4096 immutable SRV entries per device and eight devices per process; reaching
a limit fails the weave explicitly and asks for a game restart. The runtime DLL
must remain pinned. This avoids frame-count guesses and queue interception.

Both target clients explicitly disable SRGB shader conversion. This first build
rejects explicit `readSrgb` / `writeSrgb` conversion requests instead of silently
ignoring them. Typed sRGB SRV/RTV formats otherwise retain their DXGI semantics.
The output is single-sample and one render target; unsupported view formats,
cross-device inputs and malformed dimensions are rejected before recording.

`RendererSmoke` uses a D3D12 WARP device with the debug layer. It compares five
optical passes against the original CS (LR/RL, distinct eye positions and the
near-eye branch), plus a separate left-eye fallback, viewport origin and clipped
scissor. All six passes are recorded together, and the renderer is destroyed
before submission to check retained GPU objects. Result: 84,480 compared channel
bytes, zero mismatches, zero debug errors/warnings. A CPU fixture checks the
wholeMat rotation and translation. This is GPU/software validation, not monitor
or game validation.

Regenerate after intentional shader changes:

1. Run `shaders/translate_boe_shader.py` with Windows Python and the analysis copy
   available. Only the known source DLL SHA-256 is accepted.
2. Compile `BoeInterlacePS.hlsl` and `LeftEyePS.hlsl` with `fxc /O3 /T ps_5_0 /E main`,
   and `FullscreenVS.hlsl` with `fxc /O3 /T vs_5_0 /E main`, to their `.dxbc` files.
3. Run `shaders/embed_shaders.py`, rebuild and run `RendererSmoke` with the absolute
   path of `shaders/BoeOriginalCS.dxbc`.

The deployed runtime uses embedded bytecode and does not load the vendor DLL or
the shader compiler for rendering.
