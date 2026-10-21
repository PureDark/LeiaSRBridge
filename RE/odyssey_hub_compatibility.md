# Odyssey 3D Hub compatibility — 0.5.0

## Scope and evidence

Target: installed Odyssey 3D Hub service 1.5.1, Unity UI 2023.2.20, x64. The Titan device remains the BOE backend. Hardware acceptance requires the M27E6V-3D; the development-machine Hub run used a synthetic broker and never opened the MCU or eye camera.

The original install and game modules are not patched on disk. There are two separate paths: the Unity Hub UI (`SRUnityInternal` C ABI plus `SRUnityNative` legacy DX11/DX12), and games (`Odyssey3DSDK` / `UEVRBackend` C++ ABI). Existing FallenFlower3D/YakuzaVRMod modern DX12 contracts remain present.

## Analysis record

- Headless IDA supervisor, health checked against `Odyssey3DSDK.dll`; evidence in `evidence/odyssey/sdk-health.json`, `sdk-factory.json`, `sdk-functions.json`, `sdk-imports-ida.json`. Factory at `0x1800010f0` constructs the Leia mapping factory, with vtable `0x1800065a0`. This establishes the actual Leia route rather than inferring it from DLL strings.
- Managed binaries were decompiled offline with dnSpy. No application assembly was executed to decode strings. String data was reconstructed from the initialized byte array using `(byte ^ index ^ 170) & 255`.
- Service `DeviceStatusChecker`, method token `0x060001E6`, RVA `0x81B4`: requires proxy responding, device responding and power state all equal to 1. Method `0x060001F3`, RVA `0x89AC`: also checks firmware state, PIP, pivot, cable, multiple supported monitors, primary output and native resolution.
- Monitor helper `0x06000150`, RVA `0x6132`: friendly name must contain `odyssey` and its penultimate character must be `x`. Display names ultimately come from `DisplayConfigGetDeviceInfo` (managed `Odyssey3DHubService.14.cs`). The primary output's friendly name is mapped to `Odyssey G90XF` inside compatibility processes. Actual display modes and EDID are preserved.
- `SRInternalPlatform.GetSimulatedRealityRuntimePath` reads the default value under `HKLM\SOFTWARE\Dimenco\Simulated Reality`. UI initialization checks the `\bin` subdirectory, verified from Assembly-CSharp data at file offset `0x3278a`, decoded string offset `19852`, length `4`.
- GameLauncher.Lib `F919EB25`, P/Invoke token `0x0600003D`, calls `CreateRemoteThread`, alongside `WriteProcessMemory`, `GetProcAddress` and `LoadLibrary`. This motivated a separate external-game interception point; parent-child launch interception alone does not cover Steam-owned game processes.

## Implementation

`TitanOdysseyHook.dll` installs hooks only after explicit initialization outside DllMain. `LdrLoadDll` routes the five SR modules and `txBridge.dll` to the selected bridge directory, including absolute-path loads. Only the known Leia registry key's default value is virtualized; no system registry write is performed. `runtime/bin/Titan-runtime-path.txt` keeps the directory present in ZIP distributions.

`runtime/odyssey/txBridge.dll` answers device readiness from the live shared broker snapshot. Missing, stale or stopped broker state is not reported as ready. Power/cable notifications block until a state change. The Samsung USB/firmware proxy is never spawned by this replacement. PIP is treated as off; actual primary-output pivot is queried. Users must keep the Titan panel primary, native resolution, landscape and SDR. MCU calibration is not written.

The existing startup-breakpoint injector is reused for the Hub service and UI. The UI is launched with `-force-d3d11`. x64 child launch hooks preserve the caller's suspended state. System helper executables and Steam are left to their ordinary launch path. Before the Hub remotely loads exactly `Odyssey3DSDK.dll` or `UEVRBackend.dll`, the target receives the bridge runtime and hooks first, even if its parent is Steam. Unknown loader names/manual mapping are outside this hook's contract. An already loaded foreign SR runtime is rejected instead of mixing implementations.

Added ABI: C context/system/display/head/eye APIs; SystemSense events; Display physical dimensions in centimeters; legacy `PredictingDX11Weaver` / `PredictingDX12Weaver` using the original 1.34.10 class declarations. DX11 preserves immediate-context state. DX12 follows the SDK header's UAV input / bound render-target contract, restoring input state and never submitting the host queue. The existing immutable GPU cache preserves resources through submission. Optional sRGB decode/encode now works in both renderers; all default optical arithmetic remains unchanged.

Late latching and Leia-specific anti-crosstalk algorithms are not supplied. Legacy contrast setters retain their requested value but do not apply a separate Leia contrast model. Modern `CreateDX11Weaver`, DX9, OpenGL and Vulkan remain outside the implemented renderer scope. Hub conversion-video AI is not part of this change.

## Validation and limits

- Full CTest run: 20/20 passed. It covers the existing suite plus DX11 pixel comparison with original BOE compute shader, all sRGB flag combinations on fallback, legacy DX12 original-CS pixel comparison, UAV state restoration and destruction-before-submit with debug-layer validation, C ABI readiness/disconnect/head/eye callbacks and context-first deletion.
- Startup tests cover absolute DLL redirection, virtual registry query sizing, child suspended-state preservation and an independent external game process receiving hooks before a harmless test module named UEVRBackend.dll.
- Static export validation covers all SR imports of FallenFlower3D (15), YakuzaVRMod (12), Odyssey3DSDK (29), UEVRBackend (22), SRUnityNative (32). Thin DLLs expose 100 forwarders. Final package verification checks 11 binary/PDB identities and ZIP content hashes.
- Actual installed Hub service and UI were started with a synthetic broker. Service log reported `Device Status : Normal`; UI's lease reported `requestLens=1`, nonzero HWND and a live `lastWeaveTick`. At the first sampled observation, UI PID 52624 had heartbeat and last-weave both 215660781. Service PID 19068 had no weave lease, as expected. Evidence is `hub-live-mock-device.log`, runtime logs and `hub-live-mock.json`. Later loss of focus paused UI weaving, so the final snapshot is not a frame-rate measurement.
- No actual game was started. Real Hub/game 3D, eye following, stereo direction and optical quality on the Titan monitor remain unverified. A passed import audit or synthetic GPU test does not establish whole-game compatibility.
