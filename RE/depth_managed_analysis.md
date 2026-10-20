# DepthTransformerPlus 托管调用链：已有双眼/SBS，不做 AI 估深度

## 结论

**已找到直接消费现成 SBS 左右图、跳过 AI Render 的实际生产代码分支。** `DepthTransformerPlus.exe` 的 `FrameConvert.DrawFrame` 在 `format == InterWave_SBS (1)` 时只做 GPU 纹理复制；然后 `BOEInterleaving.DoInterleave` 将该 SBS SRV、输出 RTV、眼位与显示器校准交给 `interlaceSDK.dll`。这足以支持“LeiaSR mod 已经生成左右眼 → 打包 SBS → 私有面板编织输出”的转接设计。

必须区分：现有程序即使选 SBS，初始化时仍会建立 `YLStereoRenderCSharp` 对象；但 SBS 每帧分支不调用它的 `Render`。新的原生转接层可以直接调用 `interlaceSDK.dll` 和 MCU/眼追接口，不需要复用这段 AI 对象初始化。是否完全不加载 AI 可由新的调用图保证，而不能把现有程序概括成完全没有加载 AI。

没有运行目标程序/DLL、没有连接 WebSocket 服务或发出设备命令。以下为托管反编译、IL 与 PE 导入表的静态结论。

## 分析产物

工具使用已有 `D:/Coding/dnSpy-net-win64/bin/dnSpy.Console.dll`，通过 `dotnet --roll-forward Major` 执行静态反编译；未安装系统组件。

- 完整反编译：`evidence/depth/managed_decompiled/`。
- SBS 分支 IL：`evidence/depth/managed_frameconvert_draw.il`。
- 原生编织调用 IL：`evidence/depth/managed_dointerleave.il`。
- 含异步/闭包的完整 WebSocket 类：`evidence/depth/managed_websocket_all.cs`。
- PE/CLR 判断：`evidence/depth/managed_pe_headers.json`。
- C++/CLI 包装 DLL 原生导入：`evidence/depth/managed_native_imports.json`，由 `tools/managed_pe_imports.py` 只读解析生成。
- 五个反编译对象的安装目录原文件与分析副本 SHA-256 全部相同，见 `evidence/depth/managed_input_hashes.json`。

`DepthTransformerPlus.exe`、`BOEInterleaving.dll`、`BOERenderWapper.dll`、`AiStereoKitCSharp.dll`、`LF3DRenderCoreForCSharp.dll` 有 CLR header。后四个为混合托管/原生包装，不应把反编译中的 `DllImport("")` 误当成没有外部库：`BOEInterleaving.dll` 的 PE 导入表明确指向 `interlaceSDK.dll`。

## 1. 不经过 AI 的确证

`AstraDepth3D.CaptureSampleCore.FrameConvert.DrawFrame`：Token `0x06000359`，RVA `0x0001B004`。

```csharp
if (format == LocalEnumeration.InterWaveType.InterWave_SBS) {
    frameCopy.DrawFrame(device, shaderResourceView, stereoTargetView, viewportF);
} else {
    stereoRenderCSharp.Render(shaderResourceView.NativePointer,
                            stereoTargetView.NativePointer, 0);
}
```

这不仅是字符串或方法名判断，IL 已确认：

- `IL_000B` 读取 format，`IL_000C` 压入整数 `1`，`IL_000D` 对非 SBS 跳转。
- SBS 路径 `IL_0029` 调用 `FrameCopy.DrawFrame`，`IL_002E` 直接 `leave IL_0083` 返回。
- `YLStereoRenderCSharp.Render` 只位于另一条路径的 `IL_0057`。

`FrameCopy.DrawFrame`（Token `0x060003A8`，RVA `0x0001CDA8`）设置普通 vertex/pixel shader、输入 SRV、输出 RTV，然后 `DrawIndexed` 全屏四边形。它使用 `Shader.g_normalPS`，没有调用 AI 引擎。

随后两条当前捕获实现都会进入相同编织：

- WGC：`BasicCaptureAsync.ProcessFrameRender`，Token `0x06000314`，RVA `0x000175F8`。
- Desktop Duplication：`BasicCaptureDXGIAsync.RenderLoop`，Token `0x060002E2`，RVA `0x0001400C`。

```csharp
frameConvert.DrawFrame(device, capturedSRV, currentInterwaveStatus);
Interleaving.DoInterleave(x, y, z,
    device.ImmediateContext.NativePointer,
    backRenderTargetView.NativePointer,
    frameConvert.GetShaderResource().NativePointer,
    width, height, 87, InterMode.RLInter, 0);
```

格式未指定时（`InterWave_None=0`），才调用 `YLStereoMatcher.ExtractAndMatch` 自动判断，选 SBS=1 或 AI=2。转接已知左右眼的 mod 时应直接选择 SBS，既不需要自动内容检测，也不需要 AI Render。

## 2. 原生编织 ABI 与校准来源

`BOEInterleaving/Interleaving.cs` 保留原始参数名；`managed_dointerleave.il` 确认原生调用为 `CallConvCdecl`。下列是从该包装器还原的 C 接口形状：

```cpp
int __cdecl BOE_Interleaving_initSDK(
    void* device, void* inputResource,
    double* deviceParams,       // 6 doubles
    double* wholeMat,           // 6 doubles
    double* refractParams,      // 8 doubles
    int dxVersion,             // 11 = D3D11, 12 = D3D12
    int viewportTopleftX, int viewportTopleftY,
    int viewportWidth, int viewportHeight,
    int outDXGI_FORMAT,
    const char* logpath);

int __cdecl BOE_Interleaving_doInterleave(
    float x, float y, float z,
    void* context,              // D3D11: ID3D11DeviceContext*
    void* renderTargetView,     // D3D11: ID3D11RenderTargetView*
    void* inShaderResourceView, // D3D11: ID3D11ShaderResourceView* of packed stereo
    int width, int height, int inDXGI_FORMAT,
    int inter_mode, int sRGBFix);

void __cdecl BOE_Interleaving_setDeviceParam(double* params6);
void __cdecl BOE_Interleaving_setWholeParam(double* params6);
void __cdecl BOE_Interleaving_setRefractDeviceParam(double* params8);
void __cdecl BOE_Interleaving_releaseSDK();
```

`InitSDK` 包装方法：Token `0x060000A4`，RVA `0x000019FC`。它检查数组长度为 6、6、8；**虽然公开托管签名含 `inputResource`，实际向原生传入的第二参数固定为 `null`，未使用该托管参数。**

`DoInterleave` 包装方法：Token `0x060000A9`，RVA `0x0000168C`；IL 将上述 11 个参数依次直接转发，没有 AI 调用。

枚举值：

| 参数 | 值 |
|---|---|
| `D3DVersion.dx11` | 11 |
| `D3DVersion.dx12` | 12 |
| `InterMode.LRInter` | 0 |
| `InterMode.RLInter` | 1 |
| `InterMode.TBInter` | 2 |
| `InterMode.BTInter` | 3 |

应用使用 `RLInter=1`；这提供明确的左右图排列选择。实际左右眼顺序仍应与 mod 的打包顺序对齐，不能单凭 SBS 名称默认一种眼序。

初始化真实调用见 `BasicCaptureDXGIAsync.InitializeBoeRenderer`：Token `0x060002DE`，RVA `0x00013A40`：

1. `BOE_MCU_openMCU()`。
2. `BOE_MCU_getDeviceParam(double[6])`。
3. `BOE_MCU_getWholeMat(double[6])`。
4. `BOE_MCU_getRefractionDeviceParam(double[8])`。
5. `InitSDK(device, null, deviceParam, wholeMat, refractionParam, dx11, 0, 0, width, height, 87, "./logs/")`。
6. `SetWholeParam`、`SetDeviceParam`、`SetRefractDeviceParam`。

这些参数来自设备 MCU，没有在托管代码中使用一组固定屏幕参数。托管层只知道数组长度，数组成员的物理语义需依 native SDK/MCU 分析确定。图像格式 `87` 对应 `DXGI_FORMAT_B8G8R8A8_UNORM`；当前实际链证明的是 D3D11，D3D12 枚举的存在本身不证明任意 D3D12 mod 可直接用相同 pointer 语义。

## 3. 屏幕 3D 开关与眼位

`AstraDepth3D.BOEStereoRender.MCUSdk` 的 P/Invoke 明确是 `MCU_library.dll`、`CallingConvention.Cdecl`：

```csharp
int BOE_MCU_initSDK([MarshalAs(UnmanagedType.LPStr)] string path, int loglevel);
int BOE_MCU_openMCU();
void BOE_MCU_closeMCU();
void BOE_MCU_releaseSDK();
int BOE_MCU_set3DMode([MarshalAs(UnmanagedType.Bool)] bool is3D);
[return: MarshalAs(UnmanagedType.Bool)] bool BOE_MCU_get3DMode();
int BOE_MCU_getUSBState();
int BOE_MCU_getDeviceParam([Out] double[] parameters);
int BOE_MCU_getWholeMat([Out] double[] parameters);
int BOE_MCU_getRefractionDeviceParam([Out] double[] parameters);
```

应用入口以 `BOE_MCU_initSDK("./logs/",2)` 初始化。`BasicCaptureDXGIAsync.StartCapture` 调用 `OpenRasterSafe`，后者执行 `BOE_MCU_set3DMode(true)`；结束时 `CloseRasterSafe` 调用 `false` 后关闭 MCU。成功返回判断均为 `0`。因此面板开关与图像编织是两条独立操作，单调用开关不足以显示正确立体画面。

眼位通过 COM 服务：CLSID `D98A272F-DE1C-4E73-A1D0-A93A8F068B4B`，IID `E26EE08A-BCF1-49F8-B01F-0044D2897F28`。托管 interop 的 `GetPosition(out float x, out float y, out float z, out float z_l, out float z_r)` 为 DispId 6；有 `Open`、`Close`、`StartCamera`、`StopCamera`。`Facetracker.GetPose`（Token `0x060001B1`，RVA `0x0000D11C`）将 x/y/z 直接给编织器，丢弃 z_l，把 z_r 留作状态判断。

当前实例化的 `BasicCaptureAsync` 使用 `near=400f`、`far=1000f`（反编译文件 1025/1028 行），`BasicCaptureDXGIAsync` 同为 `400f/1000f`（1050/1053 行）。二者当前 `GetPose` 均将 COM 返回的 x/y/z 原样送入 `DoInterleave`，没有乘 1000；这些数值强支持当前主链以毫米为尺度。旧 `BasicCaptureDXGI` 的 `0.3f/0.6f` 与部分遗留 `UpdateFaceTrackingState` 的 `*1000` 逻辑不能用来判断当前编织输入单位；安装目录 `config.ini` 的 `0.3/0.5` 也不能覆盖已确认的当前类默认值。具体单位、坐标轴方向、原点、刷新/延迟语义仍须与 native/硬件证据合并校验，不能凭常见眼追约定推断。

## 4. StartInterwave / StopInterwave 的完整职责

`MainWindow.StartInterwave`：Token `0x060000CE`，RVA `0x00006CBC`。它在 UI 线程执行现有 `checkLicense`、初始化 composition、设置启动状态，随后 `Facetracker.StartCamera()`、广播全局开启状态、`BeginCapture()`、标记已启动并调整窗口层级。

`StopInterwave`：Token `0x060000CF`，RVA `0x00006F68`。它停止摄像头、停止捕获、将 InterWaveType 复位到 None、广播关闭状态；底层 capture 清理关闭光栅并调用 `ReleaseSDK`。该调用名称不能证明原生 GPU 资源已被释放；native 分析发现本版本导出的 `releaseSDK` 主体仅日志，详见 `depth_interlace_analysis.md`。

`BasicSampleApplication.StartCaptureFromItem` 实例化 `BasicCaptureAsync`（WGC），`StartCaptureFromDXGI` 实例化 `BasicCaptureDXGIAsync`（Desktop Duplication）。后者通过 `Output1.DuplicateOutput` / `TryAcquireNextFrame` 得到 D3D11 texture，复制到本进程 GPU buffer 并创建 SRV。输出是本进程 composition swapchain，再 Present 到覆盖窗口。`ScreenCaptureWindow` 调用 `SetWindowDisplayAffinity(hwnd,17)` 排除自身捕获，防止采集到覆盖层。

当前捕获与 WS 协议没有向客户端接收 D3D texture/shared handle 的字段或路径。使用现有服务意味着先把 SBS 放到被捕获的窗口/桌面；原生 mod 转接可直接传本进程 SRV/RTV，省掉桌面捕获与覆盖窗口。

## 5. WebSocket 20650 协议

`WebSocketFleck` 构造函数：Token `0x06000199`，RVA `0x0000C344`，监听 `ws://127.0.0.1:20650`。可带查询参数 `ClientId=...` 用于连接标识；未提供时生成 GUID。其 `OnMessage` 进入 `parse_recv`，未看到纹理数据传输。

| JSON 消息 | 实际处理 |
|---|---|
| `{"Query3DStatus":1}` | 回复 `Cmd=1001`，`InterWaveRet`、`EyeCrackrRet`、`RasterRet` 各为 0/1。该分支仅检查 key 是否存在。 |
| `{"FunctionStatus":1}` | 旧状态协议，使用 200/201/202/203 状态码。 |
| `{"GlobalOpen":1}` | 调用前台程序检测与选择流程，**不能保证是 SBS 模式**。 |
| `{"GlobalOpen":0}` | 停止交织并重建全屏窗口状态。 |
| `{"YLClibrationInterWaveOpen":1}` | 校准入口，准备好且处于 idle/无 capture 时 `StartInterwave()` 后调用 `ResetCurrentInterwaveType(InterWave_SBS)`，明确强制 SBS。 |
| `{"YLClibrationInterWaveOpen":0}` | 停止交织。 |
| `{"Ret":200,"SbsType":N}` | 对服务发出的前台 EXE SBS 能力查询的回复，触发已有 callback；不是上传图像。 |

强制 SBS 证据：`MainWindow.YLClientMsg_callback`（Token `0x060000D7`，RVA `0x000073F4`），`YLProtocolHandler.response_YLClibrationInterWaveOpen_msg`（Token `0x060001A4`，RVA `0x0000CA68`）。注意已有 capture 正在运行时，这个打开请求不重新强制模式；它的 SBS 设置位于 idle/无 capture 条件内。该入口还设置 `_calibration_a=1`、`_calibration_b=0`，是校准用途。它可证明服务支持强制 SBS，不宜未经处理就当作通用生产接入 API。

回复中的 `Ret=200` 与 `"LR InterWave Open Success"` 在解析阶段构造，不等价于之后硬件已经成功开启；真实状态需看 Query3DStatus/底层返回。此分析没有发送这些消息。

## 6. 最小无 AI 转接调用序列

以下是基于已观察接口的设计，不是已经运行通过的实现：

1. 获取并保持 mod 的 D3D11 device/context，按编织 SDK 需要的眼序把现有左右眼合成一个 SBS texture，并创建 SRV。这里不生成深度图、不调用 `AiStereoKit`。
2. 初始化 MCU，打开设备，读取 `deviceParam[6]`、`wholeMat[6]`、`refractParam[8]`。
3. `BOE_Interleaving_initSDK(device,null,...,11,viewportX,viewportY,outW,outH,format,logpath)`，并遵循实际程序的三个参数 setter。
4. 初始化/启动眼追，启用 `BOE_MCU_set3DMode(true)`。
5. 每帧取得眼位，调用 `BOE_Interleaving_doInterleave(x,y,z,context,finalRTV,sbsSRV,inputW,inputH,format,eyeOrder,0)`，再由 mod/游戏原有链提交显示。
6. 结束时停止追踪、关闭 3D 模式、调用编织 `releaseSDK`、关闭并释放 MCU；编织对象的真正资源回收必须按 native 实现单独处理，不能假定该导出已经完成回收。

该 native 路线可避开现有程序的 AI 初始化和捕获开销；但 LeiaSR front-end ABI、D3D12/OpenGL 输入互操作、窗口/面板位置、左右眼顺序、色彩/格式、GPU 同步与资源生命周期仍需转接实现分别处理，不能由这份静态证据推出所有 LeiaSR mod 已自动兼容。

## 7. BOERenderWapper 与 LF3D 包装的定位

`BOERenderWapper.RenderWapper` 构造原生 `StereoscopicRender`，提供 `Initiallize(ID3D11Device*)` 与 `RenderTexture(ID3D11Device*,ID3D11DeviceContext*,ID3D11ShaderResourceView*,ID3D11RenderTargetView*)`，见 Token `0x060000DA/DB`、RVA `0x000018D0/1908`。它被旧的 `BasicCaptureDXGI` 类引用，但当前 `BasicSampleApplication` 实际构造的是 `BasicCaptureDXGIAsync` 或 `BasicCaptureAsync`，均走上述 `BOEInterleaving` 路径。不能把未看到当前构造入口的旧类当成默认主链。

`LF3DRenderCoreForCSharp.dll` 已一并反编译到证据目录供 native 子任务对照；本次确定的现成 SBS 绕过 AI 路径不依赖它。
