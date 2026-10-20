# ModexLite → 泰坦军团 M27E6V-3D：LeiaSR 转接可行性

日期：2026-10-20。目标：让既有 LeiaSR mod 的真实左右眼输出驱动这块非 LeiaSR 显示器。当前阶段为静态逆向与接口可行性调查，没有实现、部署或实机验证转接 DLL。

## 结论

**可以做 LeiaSR 转接层，独立接收真实左右眼的原厂交织路径已经找到。用户补充的 `C:\Program Files (x86)\DepthTransformerPlus` 包含原生 `interlaceSDK.dll`、MCU 校准/控制和眼追组件；它们可以组成无需 AI 估深度的显示后端。** 这是代码与接口层面的可行性结论，当前尚未实现转接 DLL 或完成显示器实测，不能宣称所有 LeiaSR mod 已兼容。

已经能排除一条错误路线：`AiSetereoKit.dll` 并不是这块显示器的织图 SDK，`StereoCore.dll` 也不是可加载的显示驱动 DLL。前者是单目图像深度推理与 SBS 生成模块，后者在已确认的调用链中是推理模型权重。

DepthTransformerPlus 同时封装了 AI 转换、原生 SBS 捕获和光学交织；不能仅按它的 2D 转 3D 功能判断整个程序只做估深度。`FrameConvert.DrawFrame` 的 SBS 分支只复制图像，IL 确认跳过 AI Render，随后调用独立交织 SDK。新的转接层可以直接传 mod 的 GPU 双眼图，不必使用它的 GUI、捕获或 AI 初始化。

推荐链路是：**LeiaSR 兼容入口 → 已有左右眼/SBS → 原厂光学交织后端 → 宿主当前输出 RT**；MCU 提供屏幕校准和 3D 开关，原厂跟踪提供眼位。最小版本锁定本机已经核验的 Windows x64、SR-SDK 1.34.10、FallenFlower3D/YakuzaVRMod，再扩展其他 ABI/API。

主要实现难点已经收敛为 **Leia 的 C++ ABI、DX12 命令/资源契约、跟踪有效性和设备所有权**。原厂实际调用 DX11，native DLL 的 DX12 实现还存在固定资源状态、单一常量缓冲等限制。已提取光学 compute shader 和常量布局，因此可以实现适合宿主命令列表的 DX12 后端，不必从零猜织图算法。

## 新增证据：DepthTransformerPlus 的直接 SBS 后端

| 层 | 已证实能力 | 关键证据 |
|---|---|---|
| SBS 输入 | 跳过 AI，复制真实双眼图后织图 | `FrameConvert.DrawFrame` Token `0x06000359`；IL `IL_0029/002E` |
| 光学交织 | 输入 SBS SRV、输出 RTV、眼位；没有深度输入 | `interlaceSDK!BOE_Interleaving_doInterleave @ 0x180060580` |
| 图形实现 | 当前主程序 DX11；DLL 内同时有 DX12 | init 根据 11/12 分支；DX12 init `0x18005D6B0` |
| 面板校准 | 从设备读取 6/6/8 个 double 参数组 | `BOE_MCU_getDeviceParam/getWholeMat/getRefractionDeviceParam` |
| 3D 开关 | MCU 串口工作线程持续发送开启命令 | `BOE_MCU_set3DMode`、`SendThreadProc` |
| 跟踪 | 原厂 COM 提供 xyz；实际注册使用 eyetracking 子目录版本 | `FaceTrackingManagerService`、注册表与 native 分析 |

已提取 shader 2.1 是 `cs_5_0`：一张 packed stereo texture、一个 UAV 和 80 字节常量，按面板光学参数与眼位逐 RGB 子像素选取左右眼。由此可以确定 AI 深度模型不在目标转接链上。

当前 COM 的五个 float 不含传感器时间戳、frameId 或有效标记，后两项在当前生产者中写固定值；不能直接伪装成完整 Leia HeadPose。光学交织眼位与向 mod 提供可验证的新鲜头部跟随样本，应分别实现。底层 BOE 跟踪 SDK 暴露更丰富结果，但本轮未完整恢复其结构。

MCU 串口采用独占打开，原厂程序和新转接不能各自无协调占用设备；兼容层宜由一个设备 owner/broker 管理校准、开关和跟踪，再供多个客户端使用。API 返回值或 getter 可能只反映软件状态，不能当作硬件已成功切换的证明。

对应详细报告：

- [原生 interlaceSDK 与 DX12 限制](depth_interlace_analysis.md)，含 [恢复的 ABI 声明](recovered_boe_interlace_api.h)。
- [托管 SBS 分支与实际调用链](depth_managed_analysis.md)。
- [MCU、眼追与控制](depth_tracking_control.md)。
- [随包 LF3D 备用路径](depth_lf3d_analysis.md)：也有直接 SBS 光学交织，但不是当前主调用链，带独立校准/许可证前提。

## 实际分析范围与身份

- 软件：`modex_lite.exe`，文件版本 `1.6.1.76`。
- 显示器：用户确认的 **泰坦军团 M27E6V-3D**。
- 使用本机 IDA 9.4 的 idalib/MCP，所有目标以 `force_headless` 打开；每个原生/AOT 数据库打开后检查 `server_health`、模块名、SHA-256、自动分析和 Hex-Rays 状态。
- 对 `modex_lite.exe`、`game_monitor_plugin.dll`、`media_transpose2to3_plugin.dll`、`AiSetereoKit.dll` 做目标相关反编译与关键汇编检查；对 `data/app.so` 做 ELF/AOT 结构、字符串与 xref 检查。
- 后续根据用户提供的安装路径，分析 `interlaceSDK.dll`、`LF3DRenderCore.dll`、`MCU_library.dll`、两版 `FaceTrackingManagerService.dll` 和眼追生产者；CLR 主程序和包装器使用本机 dnSpy 静态反编译并核对 IL/PE 导入。
- `StereoCore.dll` 最初只能作为 raw data 加载；确认其消费者后停止把它当作 PE 代码分析。
- 原文件未改写，IDA 数据库和输入副本存于 `RE/inputs`；原始响应存于 `RE/evidence`。精确哈希见 `RE/input_hashes.json`。没有运行目标 DLL、启动器、游戏或切换显示器。
- 新输入清单和哈希见 `RE/evidence/depth/input_hashes.json` 及各子报告。显示器列表含 `TTR / 27E6V-PRO-3D / BOE`；尚未以实机 EDID 核实与用户营销型号的对应，不能把一个字符串当成硬件验收。

| 原文件 | SHA-256 |
|---|---|
| modex_lite.exe | `270E7766F5D3F75EDF9B8A620287AB15BC530D9F1E7A95FE086CC8DD9F505E37` |
| AiSetereoKit.dll | `C3CD9FD7085A076A4FF4CD2F80816027C096785CD8395C191B3738DDD528D639` |
| StereoCore.dll | `F8B25622E4392FFEBEBA0A9555028F6F114A2C252E038DBB0B9F67B5869EFC37` |
| data/app.so | `0CF08F3F4C535E5466C2FEEB489D0229431B6FA001B926087423EC574CEBF458` |

## 已证实的组件职责

### 1. 原生启动器与监控层

`modex_lite.exe` 是 Flutter runner。已追踪的动态加载是 Bugly crash reporter 和 User32 DPI 支持，不是 3D 驱动注入：`0x1400027A0`、`0x140001020`、`0x140007780`。业务代码在 `data/app.so`，不能因 runner 没有相关 API 就推断整个程序没有游戏启动行为。

`game_monitor_plugin.dll` 的 `0x180008440` 分发 start/stop/add/remove/status 方法；`0x1800079F0` 用 Toolhelp 进程快照按 EXE 名查询 PID。已分析路径负责监控，不负责显示交织。

详见 [启动器原生桥报告](launcher_analysis.md)。地址均为对应模块的 VA，不能跨 DLL 复用。

### 2. AiSetereoKit 是 2D → SBS 引擎

`media_transpose2to3_plugin.dll!LoadMediaTransposeEngine` 位于 `0x180006D90`，把 cacheDir 传给 `AiSetereoKit!LoadStereoEngine`。实际 GPU 使用点 `0x18000CE20` 向 renderer 传入 D3D11 device/context、宽高、**单个输入 SRV** 和输出 RTV；调用点 `0x18000D055` 经 `0x180006A70` 转发，固定第四参数为 `1`。该数值本身不能作为显示模式语义的证明。

`AiSetereoKit.dll` 中：

- `LoadStereoEngine @ 0x18015A770` 创建深度引擎。
- `0x18013ECC0` 的渲染路径依次执行预处理、GPU readback、OpenVINO 深度推理、深度上传、膨胀、StereoProcess2、补洞，再写输出 RTV。
- `StereoProcess2::CreateShader @ 0x1801405F0` 使用 `0x18057D450` 的像素 shader。其 DXBC 反汇编按 `u >= 0.5` 区分左右半屏，使用 `2*u` / `2*u-1` 映射并应用方向相反的视差；输入是颜色与深度，常量是景深、立体强度、左右排列、Y 翻转和尺寸倒数。
- 这条路径证实普通 SBS 生成，不包含面板光学参数或眼位输入。把 mod 已有双眼图像交给它重新估深度，会丢失真实双眼信息，不是目标转接方案。

`StereoCore.dll` 的文件头不是 `MZ`。`0x180117990` 读取模块目录下模型资源并执行重复 XOR；关键循环为 `0x180118330..0x180118360`，已核对汇编。`0x180137000` 随后用结果构造 `ov::Tensor` 并复制权重。此结论来自消费者代码，不只是文件名或字符串猜测。

详见 [AI 转换引擎报告](aikit_analysis.md)，以及 `evidence/aikit_core_path.json`、`aikit_model_xor_asm.json`、`aikit_stereo_pixel_shader.asm`。

### 3. Dart 中的显示控制线索

`data/app.so` 是 x64 ELF Dart AOT，非另一个普通 Windows DLL。IDA 得到 15200 个函数，但普通自动分析未还原 Dart snapshot/object pool 对字符串的间接关系。

| IDA 地址 | 已存在的字面量 | 证据等级 |
|---|---|---|
| `0x314BD0` | `ws://localhost:20650` | 本机 WebSocket 通道线索 |
| `0x3EEC00` | `\\.\pipe\BOE3DAssistantNamedPipe2` | 命名管道通道线索 |
| `0x352260` / `0x425BC0` | `open3d` / `close3d` | 控制字符串线索 |
| `0x3CAFE0` | `{"Query3DHotkey": 1}` | 请求格式线索 |
| `0x358CB0` / `0x4821D0` | Win3DControl 初始化 namedPipe / websocket 日志 | 至少存在两个实现分支的线索 |
| `0x2A5880` | `C:\Program Files\Depth Transformer\Depth Transformer.exe` | 外部组件路径线索 |

另有完整 `C:\Program Files (x86)\DepthTransformerPlus\DepthTransformerPlus.exe` 路径（原始文件字符串），以及 `ipcOpened3d` / `ipcClosed3d` 等状态字符串。

第一阶段只分析 ModexLite 时，这些仅是字符串线索，IDA xref 查询为空的结果保存在 `evidence/dart_control_xrefs.json`。补充安装目录后，已在 DepthTransformerPlus 中还原 `ws://127.0.0.1:20650` 的 JSON 控制处理：`GlobalOpen`、`Query3DStatus`、`YLClibrationInterWaveOpen` 等。它控制状态/模式，没有接收 GPU texture/shared handle 的通道；详见托管报告。BOE 命名管道分支与所有具体客户端选择仍未全部覆盖。

此前未找到该组件的结论已由用户随后提供的实际目录更新。本次没有向这些端点发试探消息，也没有启动它们。

## 官方资料背景

官网该型号产品页、支持页和随官网下载教程见 [厂商资料记录](monitor_vendor_sources.md)。本地保存的教程为 `titanarmy_3d_software_manual.pdf`，其中：

- 第 2 页：DepthTransformer 为必装交织程序；ModexLite 是依赖主启动器与交织程序的可选扩展。
- 第 6 页：播放器可处理原生 SBS 视频。这证明官方软件链能够消费真实双眼内容，但不证明开放 GPU API 或任意游戏自动接管。
- 第 7 页：3D 游戏和视频需要 Depth Transformer 后台运行；Plus 另有 AI 2D 转 3D 能力。

第 2、7 页已渲染复核。教程同时存在正常设备校验流程；转接方案应复用已正常启用的原厂软件链，本次未修改授权或设备校验行为。

## LeiaSR 兼容层需要实现什么

通过当前本机 FallenFlower3D、YakuzaVRMod 源码和实际构建产物导入表核验，它们共享静态 `SR-lib`，引用 `SR-SDK-1.34.10`。不是替换一个名叫 LeiaSR.dll 的文件。

| 兼容模块 | 需要接住的行为 |
|---|---|
| `SimulatedRealityCore.dll` | context 创建、初始化、销毁 |
| `SimulatedRealityDirectX.dll` | DX12 weaver 工厂及对象虚表、输入纹理、当前 command list、RT/viewport/scissor、weave |
| `SimulatedRealityDisplays.dll` | SwitchableLensHint 生命周期与 enable/disable 请求 |
| `SimulatedRealityFaceTrackers.dll` | tracker 工厂、流生命周期、可选的真实 head/head-pose 样本 |

这些是 MSVC C++ decorated exports 与对象 ABI，包含虚继承和 `std::shared_ptr`。必须按具体 SDK 头文件/客户端构建验证，不能用几个同名 C 函数替代。

**重要细节：**

1. 两个已核验 mod 的实际 Leia 输出均走 DX12。`weave()` 记录到宿主 command list，并写入当前 RT；它不等同于 Present。
2. FallenFlower3D 同一帧会先织离屏遮罩，再织最终颜色。转接不能只在任意一次 weave 把图像提交到独立窗口；SBS 原厂服务路线也必须确保遮罩、颜色、后续 UI 合成使用一致布局。
3. 原厂为光学交织使用眼追踪，与将真实头/眼位置暴露给 mod 进行视差投影，是两项不同能力。后者缺失时可明确禁用 head-follow；当前 SR-lib 对 tracker 创建失败有降级路径，不应伪造有效样本。不可简单返回空 tracker 让宿主解引用。
4. 若映射追踪，需转换毫米坐标、面板中心与方向、时间戳和 frameId。两个客户端都对停止推进的样本有约 250 ms 失效处理。
5. 所有权、失焦退出、多人进程争用、分辨率、窗口位置、HDR/色彩、同步和资源 lifetime 均需在具体原厂接口上确认。它们不是本次已经通过的验收项。

精确导出名、头文件和源码行号见 [LeiaSR 客户端契约](leiasr_contract.md)。

## 可执行的实现方案

必需的原厂实现已经取得并静态分析。下一阶段可开始编写限定范围的兼容层，无需继续寻找 AI 转换入口。

1. 按 SR-SDK 1.34.10 的真实 MSVC x64 头文件实现 Core、DirectX、Displays、FaceTrackers 入口，核对 decorated exports、虚继承和 stream lifetime。
2. 保持 mod 原有 SBS 输入，在宿主 DX12 command list 记录光学交织，支持当前绑定 RT、viewport/scissor、离屏遮罩和最终颜色的多次 weave。常量、描述符和资源按 GPU lifetime 管理。
3. 由一个 owner 管理 MCU 的读取与 3D 启停，处理失焦/退出/多进程。复用正常安装的原厂跟踪；先明确支持位置样本的范围，再对更完整的底层追踪接口做补充恢复。
4. 先用原生左右眼测试图核对眼序、RGB 子像素、校准、眼位方向与原生分辨率，再验证上述两个真实 mod。构建/离线测试与显示器验收分别记录。

最小兼容版本建议锁定 **Windows x64 + SDK 1.34.10 + 当前两个已核验 mod**，先做真实双眼输出再补 head-follow。随后逐一扩展其他 ABI、DX11/DX9/OpenGL/Vulkan、旧 weaver/C API、32 位与其他 SDK 版本。现有证据不能支持一次性保证全部 LeiaSR mod。

如果只要求自有 mod，可以在共同的 SR-lib 处增加原生后端并重编译；如果要求已有第三方 mod 无需修改，则应实现上述 runtime ABI 兼容层。两种部署目标应区别验收。

## 证据边界与产物

本轮交付为报告、IDA 数据库、原始工具响应和 shader 反汇编。没有转接层二进制，没有硬件/游戏内验证。不存在编译通过即可证明该显示器正常裸眼输出的结论。

收尾复核：28 个已记录输入的当前 SHA-256 与分析前记录全部一致，见 `evidence/depth/final_source_hash_verification.json`；12 个 `.i64` 保留在 `inputs`。所有无头会话已保存关闭，本任务启动的 supervisor 已停止，临时进程记录与截断的 shader 响应已清理。

数据库可从 `RE/inputs` 继续；重新打开前后应再次核对模块与哈希。`RE/tools/ida_rpc.py` 是只用于本地 MCP 证据获取的工具，不会调用被分析的目标 DLL。
