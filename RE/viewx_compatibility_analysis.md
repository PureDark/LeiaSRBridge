# ViewX 配套软件与 Stereo3DBridge 的兼容性分析

日期：2026-10-25。范围：用户指定的两处本机安装目录；用户确认没有 ViewX 实体显示器。本次只分析软件，没有启动厂商程序、加载执行目标 DLL、开启摄像头、发送设备命令或修改 MCU 标定。

## 结论

**同一软件平台的部分组件共用，但新版实际交织路径与 Stereo3DBridge 0.9.0 的 BOE 后端不同，不能直接认定即插即用。软件接口足以支持新增 LF3D 后端，继续接收 LeiaSR 游戏已有的左右眼画面，无需 AI 估深度，也无需修改 mod。**

这里应区分三个问题：

1. 软件是否同源：是。Gateway 的 AiSetereoKit.dll 与旧版输入逐字节相同；Depth Transformer 根目录的眼追 COM DLL 与旧版根目录副本也相同。
2. 是否仍走现有桥接的 MCU + BOE interlaceSDK + BOE_3DET 算法：此次安装内容及实际调用链不能支持这个结论。新版主程序使用 LF3DRenderCore、文件标定和外部命名管道控制。
3. 是否可直接驱动 ViewX 实体屏：目前软件证据不能确认硬件协议、摄像头及屏幕标定兼容。可以确定适配入口和缺失依赖；不能用缺少实体屏代替软件接口分析，也不能把接口分析冒充实机验收。

## 输入身份与方法

- C:\Program Files (x86)\3D Spatial Gateway：3DSpatialGateway.exe 版本 1.7.0.101。
- C:\Program Files\Depth Transformer：Depth Transformer.exe 版本 1.3.1.045。
- 原安装文件只读，分析副本在 RE/inputs/viewx；原文件/副本 SHA256 一致性见 evidence/viewx/identity-and-presence.json。
- IDA 9.4 idalib supervisor，127.0.0.1:18479，force_headless 会话 viewx_lf、viewx_face。逐库 server_health 确认输入路径、模块名、image base 0x180000000、自动分析完成和 Hex-Rays 可用。
- 采用 Hex-Rays、x64 汇编、C++/CLI 虚表调用及托管主程序调用链交叉核对。DXBC 用系统 D3DDisassemble 静态反汇编，不创建 GPU 设备。
- 已保存并关闭两个新数据库；旧分析数据库和桥接源码未改。

| 组件 | 当前 SHA256 | 与旧版的关系 |
|---|---|---|
| Gateway/AiSetereoKit.dll | c3cd9fd7085a076a4ff4cd2f80816027c096785cd8395c191b3738ddd528d639 | 与 RE/inputs/AiSetereoKit.dll 相同；AI/立体内容生成组件，不足以证明光学硬件相同 |
| Depth Transformer/AiSetereoKit.dll | 59de1ebf68790af70db48c702ea47a7f3615a0242e71642bb33ce14ca3751baf | 新版本，不能把 Gateway 那份相同推广到所有组件 |
| 新 LF3DRenderCore.dll | a5d7fcc775fbd64f6ba231407ebc5fb3e2dccc2f5a73fc5e83b79c3314c60474 | 732672 bytes；旧 LF3D 是 600064 bytes、3ef8d67f… |
| 新 LF3DRenderCoreForCSharp.dll | 0760472823bd55192f91c94881e504bb6dedcbd2aba3a5ab2895eb1ed288a991 | 新包装器；主要 DX11 虚表布局仍可对应 |
| 根目录 FaceTrackingManagerService.dll | 24d677ea070fca322b025fa91823ea1eaa040729db9efed5de2f0fe7855a6118 | 与旧 DepthTransformerPlus 根目录副本相同 |
| 当前桥接私有眼追 COM DLL | 3132dd6fe285772b327b4aab99de5a05c5cfe4376432e9defdc2680c465e4604 | 与上项不同，不能混用共享节和启动语义 |

完整导入/导出与文件清单：evidence/viewx/inventory.json；与旧版和桥接组件的本次逐文件比较：component-comparison.json。

## 实际主程序调用链

保存的托管证据位于 evidence/viewx/managed/。

旧 DepthTransformerPlus 主路径是 BOEInterleaving.InitSDK / DoInterleave，使用 MCU 的 6/6/8 光学校准。旧安装也包含备用 LF3D 库，因此 LF3D 并非第一次出现。

**这次新版把 LF3D 变成了实际捕获输出路径：**

- FrameConvert.DrawFrame，Token 0x06000394，RVA 0x19D00：输入为 InterWave_SBS 时只复制已有双眼图；只有其他格式分支才调用 YLStereoRenderCSharp.Render 做转换。
- BasicCapture 初始化：LoadLicense(user.ylks) → CreateDX11Render(device) → SharpInitialize → SharpSetFormat(2,1) → SharpLoadDeviceConfigFromFile → SharpSetTexture。
- GetConfig，Token 0x060003B3，RVA 0x1B308：配置路径为当前用户 AppData/LocalLow/ylkj/deviceConfig.bin。
- UpdateFaceTrackingState，Token 0x060003AF，RVA 0x1B010：从 COM GetPosition 获取中心 x/y/z，传给 SharpUpdateEyeInfo(x,y,z,enabled,1,0)。
- 输出调用 SharpDraw；没有需要给交织器输入深度图的契约。

补偿细节：程序虽调用 GetCompFile 取得 LocalLow/ylkj/Textures/CpMatrix.png 的路径，实际 SharpInitialize 参数却是字面量 "compFile"。不能只看到 CpMatrix 路径字符串便认定该文件已经加载。原生初始化代码会按补偿纹理实际是否加载成功选择标准或补偿渲染分支。

## 原生接口与确证地址

以下地址都属于本次 LF3DRenderCore.dll，不可用于旧版 DLL。

| 接口 | 地址 / 虚表槽 | 已验证行为 |
|---|---|---|
| VersionNumber | 0x180035E00 | 返回 7 |
| CreateDX11Render | 0x180022240 | 接收 ID3D11Device*，创建 DX11 renderer；汇编已核对 |
| CreateSimpleRender | 0x180036E10 | API 枚举 2 走 D3D11；另有 OpenGL/ES 分支；此工厂未找到 DX12 分支 |
| Initialize | 0x180021800 / slot 1 | 选择标准 RenderCore_D3D11 或 Compensation650 路径 |
| SetTexture | 0x180021B60 / slot 2 | 输入纹理句柄及尺寸，包装器签名明确 |
| SetFormat | 0x180021C10 / slot 3 | 主程序调用 2×1，即 SBS atlas |
| LoadDeviceConfig | 0x180021C40 / slot 4 | 文件配置，两个 bool 对应 viewpointOffset / colorInversion |
| UpdateEyeInfo | 0x180021D20 / slot 7 | float x,y,z，bool enabled，float z_l,z_r；保存 z_l，当前 DX11 方法不读取 z_r |
| Draw | 0x180021DB0 / slot 17 | 使用已设置的纹理和光学常量绘制 |
| Release | 0x180021EC0 / slot 18 | 包装器清理入口 |
| GetLensInfo | 0x180035E40 | 解析文件中的 Lens_param，取得 com_type、port_name、baud_rate；不是 BOE_MCU_openMCU 替代函数 |

0x180005340 的 XMM 寄存器及修正原型证实眼位是浮点输入。其计算包含 floor(x*1000/1.3)、floor(y*1000)、floor(z*1000+标定值)。因此适配时应按此软件的米制眼位约定转换，不能把现有桥接毫米值原样传进去。floor 语义已核对 0x18005EE50 的汇编；初始 IDA 自动原型误把浮点参数识别成整数，不能照抄。

z_l / z_r 的名字不代表这里在消费真实完整双眼姿态。主程序传 1/0；原生把 z_l 写入 shader 常量 +60，用于视图选择倍率。这个接口不能据名字推断出左右眼的三维坐标或旋转。

证据：lf-render-exports.json、lf-vtable.json、lf-render-methods.json、lf-optical-core.json、lf-eye-disasm.json、lf-eye-typed.json、lf-math-quantize.json。

## 光学算法不能直接沿用当前 BOE 常量

- 0x180006880 实际创建的标准像素 shader 位于 VA 0x180073000，34648 bytes，SHA256 100ae6db1a8f91871c17579e5f161d07d4fe076d281298499804feee53748638。
- 反汇编为 ps_4_0，输入只有 atlas 纹理 t0、采样器 s0、64 字节常量缓冲。
- 常量包含 PixelOffset、widthProp、heightProp、startXProp、startYProp、center、shift_Norm、tilt、pitch、subp、s、ri、bi、columns、rows、z_l。
- 现有桥接使用 BOE interlaceSDK 2.1 的 cs_5_0 和 80 字节常量；不能把新参数直接塞到原布局。
- 与旧备用 LF3D 的 shader 比较，SHDR 执行指令块也不同：2648 → 3328 bytes，并非只换编译日期/调试信息。
- 0x18000FEC0 解析 deviceConfig.bin，取 Lens_param、cam_param、fit_curve 等字段；0x180004FD0 导入相应几何参数。它与 MCU 6/6/8 的字段映射尚未证明等价。
- 标准分支只需双眼 atlas；补偿分支还可能需要配套补偿纹理。应保持两条分支的差别。

证据：lf-gpu-shader.json、lf3d-default-pixel.asm、shader-identity.json、lf-shader-comparison.json、lf-device-parser.json。原始 DXBC 保存在忽略入 Git 的 RE/inputs/viewx。

## 眼追与硬件模式控制

### COM

CLSID D98A272F-DE1C-4E73-A1D0-A93A8F068B4B；IID E26EE08A-BCF1-49F8-B01F-0044D2897F28；GetPosition 为 DISPID 6。

- 0x180006270：Open 从自身 DLL 路径构造同目录 FaceTracking.exe，调用 CreateProcessW。
- 0x180006880：GetPosition 复制 eye_info 共享节的五个 float：x/y/z/z_l/z_r。
- 0x1800066B0：对应五参数写入；没有在这些 getter/setter 中采集摄像头或运行关键点模型。
- 当前两个用户指定安装树内均未找到 FaceTracking.exe、BOE_3DET_SDK.dll、MCU_library.dll 或 interlaceSDK.dll。
- 当前用户预期的 deviceConfig.bin 和 CpMatrix.png 也不存在。
- 因而不能把 FaceTrackingManagerService.dll 单独当作完整眼追算法。现有桥接的完整双眼/旋转依赖已核验 BOE_3DET_SDK 的结果和私有浮点点集；这个管理 DLL 无法证明新版采集端仍兼容该布局。

证据：face-health.json、face-launch.json、face-shared-abi.json、identity-and-presence.json，托管 ISimpleFaceTracking.cs。

### 3D 开关

新版 NamedPipeClient 连接 BOE3DAssistantNamedPipe，先发送 client_path:程序路径，再用 open3d / close3d 请求模式切换。MainWindow 打开/关闭 3D 时实际调用它。

这是外部管道服务依赖，不能仅凭管道带 BOE 字样证明最终 MCU 命令与旧版相同。当前分析范围未取得该服务端的设备控制实现。也不能把已确认的 LF3D 交织 DLL 当作摄像头/面板开关驱动。

## 对 Stereo3DBridge 的具体影响

可复用：现有 LeiaSR ABI、后台/UI、免列表接入、游戏原始双眼输入、保持/默认眼位策略、用户调参和状态发布框架。

需要新增或核实：

1. 新增 LF3D 设备后端，读取与此屏匹配的 deviceConfig；保留当前 BOE 后端。
2. DX11 可按已恢复接口调用原库。DX12 必须实现相应光学 shader 与参数映射，或验证 D3D11/D3D12 互操作；不能把旧 BOE shader 当成新版本使用。
3. 确定实际眼追采集程序及其坐标约定。COM 可先承载中心位置；未取得真实采集 SDK 前，不应宣称有原生双眼/旋转。
4. 确定管道服务的设备控制路径，或者进一步取得相应 MCU 驱动；不能直接把老 MCU 参数解释当作已证明事实。
5. 按后端识别占用进程。当前 DeviceProvider 的原厂进程检查仅列出 DepthTransformerPlus.exe，尚未覆盖新名称 Depth Transformer.exe。

**实际调用链和 shader 已证明存在可供适配的双眼交织接口；当前 0.9.0 对 ViewX 的直接兼容性未成立。** 是否最终可以直接复用旧便携 BOE 驱动，取决于缺失的实际设备组件/协议证据，不能由这两个应用包单独推导。

本次不改生产源码、默认设备选择、安装配置或 MCU，没有编译新版本，也没有把软件分析记作实体 ViewX 验收。本次仅将分析报告及文本证据提交 Git；厂商二进制、IDA 数据库和构建产物继续排除在仓库外。

## ModexLite 补充检查与后续决定

2026-10-25，按用户要求递归检查 C:/Program Files (x86)/ModexLite：

- 未找到 FaceTracking.exe、BOE_3DET_SDK.dll、MCU_library.dll、interlaceSDK.dll、deviceConfig.bin 或 CpMatrix.png。
- 未发现 .sys / .inf 驱动文件及 .msi / .cab / .zip / .7z 安装包。data 下主要为 Flutter app.so、icudtl.dat、AssetManifest.bin 和 NOTICES.Z。
- 当时逐文件计算 SHA256：AiSetereoKit.dll 与 Gateway 相同；media_transpose2to3_plugin.dll、game_monitor_plugin.dll 和 data/app.so 不同。
- 没有发现能够补齐眼追采集和底层设备控制的独立组件。尚未证明眼追位于显示器固件或独立驱动中；便携屏这一形态本身也不构成无法适配的结论。
- 以上补充结果来自本对话上一轮已完成的目录与哈希检查。整理提交时原安装位置的 AiSetereoKit.dll 已不可读，未将失败的重复检查当作新的文件身份验证。

用户明确决定：**暂不进行 ViewX 兼容开发，只保留并提交此次分析记录。** 上述适配建议仅供未来恢复工作时参考，不代表已安排实现或已支持该设备。
