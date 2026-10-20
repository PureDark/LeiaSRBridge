# interlaceSDK.dll：原生双眼交织接口

分析日期：2026-10-20。输入来自用户指定的 `C:\Program Files (x86)\DepthTransformerPlus`；目标副本在 `RE/inputs/depth`。没有加载执行目标 DLL，没有操作显示器。

## 结论

**独立的 SBS → 面板光学交织 GPU 接口已经证实存在，输入不需要深度图。** 主程序实际使用 DX11；DLL 内还包含 DX12 实现。可以在 LeiaSR ABI 适配层下接原厂光学路径，完全跳过 AI 估深度、桌面捕获和启动器。

但原厂 DX12 实现的资源契约与 LeiaSR 客户端不同，需要适配，不能原样转发几个函数就宣称兼容。针对已核验客户端，更稳妥的方向是根据已提取的原厂 shader/常量布局实现受宿主管理的 DX12 交织后端，复用 MCU 校准与原厂跟踪。直接使用 DLL 可用于受控的原生 SBS 验证，或在限制范围内做桥接。

## 身份和方法

- 模块 SHA-256：`380A9194A5C2AB42CF1F70A2B3C383F9B340B3B292AC72177350AA0DAE56CC6F`。
- x64 PE，image base `0x180000000`。IDA 9.4 idalib，`force_headless`，`server_health` 确认目标、自动分析和 Hex-Rays 可用。
- 关键输出：`evidence/depth/interlace_*.json`；带命名/类型/注释的 IDB：`inputs/depth/interlaceSDK.dll.i64`。
- 原生浮点 ABI 与释放行为核对了汇编；SBS 分支另有托管 IL 交叉证据，见 [managed 分析](depth_managed_analysis.md)。
- DXBC 通过系统 `D3DDisassemble` 静态反汇编，没有创建设备或执行 shader。原始 shader 地址、长度来自原生 PSO 创建代码；见 `interlace_constants.json`、`interlace_shader21.asm`、`interlace_conversions.json`。

## 导出 ABI

声明整理在 [recovered_boe_interlace_api.h](recovered_boe_interlace_api.h)。这是静态恢复的接口记录，不是已测试的开发包。地址为本模块 VA。

| 导出 | VA | 已核验行为 |
|---|---|---|
| `BOE_Interleaving_initSDK` | `0x1800602A0` | 接收 GPU device、可选/必需的输入资源、3 组校准、图形 API、viewport 和格式 |
| `BOE_Interleaving_doInterleave` | `0x180060580` | 接收眼位、绘制上下文、输出与输入相关句柄、尺寸、排列 |
| `BOE_Interleaving_releaseSDK` | `0x1800604A0` | 本版本函数主体仅日志，未见全局对象重置/释放 |
| `BOE_Interleaving_setWholeParam` | `0x1800604F0` | 6 个 double，更新追踪到显示坐标的变换 |
| `BOE_Interleaving_setDeviceParam` | `0x180060520` | 6 个 double，基础面板参数 |
| `BOE_Interleaving_setRefractDeviceParam` | `0x180060550` | 8 个 double，折射参数，选择 shader 2.1 |

包装 DLL 的 IL 指定 `cdecl`；目标为 Windows x64，浮点参数在 XMM 寄存器中。IDA 初始自动原型把一些 float 推断成整数，已依据包装器和汇编纠正，不能直接照抄初始反编译原型。

初始化根据 `dxVersion=11/12` 建立对应对象，存入进程全局 shared_ptr `0x1800BA758`。接口没有独立 instance handle。重复初始化替换同一全局对象，不能假设多个 weaver 各自独立。

`doInterleave` 汇编在调用虚函数后固定返回 `1`，不传播内部失败值；该返回不能作为绘制成功证明。`releaseSDK` 的完整 15 条指令仅走日志帮助函数，不应据名称假设它会立即释放 GPU 资源；DLL 卸载/全局析构是不同生命周期。

## 实际 DX11 路径

`DepthTransformerPlus` 从 MCU 读取 `deviceParams[6]`、`wholeMat[6]`、`refractParams[8]`，以 `dxVersion=11` 初始化；每帧先生成/保留 SBS，再直接调用 SDK。SBS 时只复制原图，跳过 AI Render。

- `BOE_DX11_Init @ 0x180058D60`：保存 device、viewport、输出格式并创建资源。
- `BOE_DX11_DoInterleave @ 0x180058EB0`：接收 `ID3D11DeviceContext*`、输出 `ID3D11RenderTargetView*`、SBS `ID3D11ShaderResourceView*`。
- `0x180059040`：更新常量缓冲；`0x180059270`：光学交织 compute；随后绑定调用方输出 RTV，`0x1800596C0` 绘制输出。
- 原厂托管调用使用 `InterMode.RLInter=1`。模式还包括 `LRInter=0`、`TBInter=2`、`BTInter=3`；左右眼排列必须和 mod 输入匹配，不应把全部输入固定按 RL 解释。

这证明第三方适配器可以提供真实双眼 SRV，而不必截取桌面或调用原厂 AI。

## DX12 不是 DX11 参数的简单替换

| 参数/行为 | DX11 | 本版本 DX12 |
|---|---|---|
| init 的第二参数 | 本机托管调用为 null | 必须是输入 `ID3D12Resource*`，被保存在对象里 |
| 每帧 context 参数 | device context | `ID3D12GraphicsCommandList*` |
| 每帧 output 参数 | RTV 对象 | 输出 `ID3D12Resource*` |
| 每帧 input/handle 参数 | 输入 SRV 对象 | 指向 `D3D12_CPU_DESCRIPTOR_HANDLE` 的指针，用于输出 RTV |
| 输入纹理 | 每次可提供 SRV | 从 init 保存的 resource 读取 |
| 提交/Present | 调用路径负责织图 | 已分析 draw 路径只记录命令，没有执行队列或 Present |

DX12 初始化 `0x18005D6B0` 要求两个非空指针。托管 `InitSDK` 虽有 `inputResource` 参数，实际固定传 null，因此不能直接用这个包装器启用 DX12；原生导出与托管包装必须区别处理。

关键限制：

1. `BOE_DX12_DispatchInterlace @ 0x18005DB00` 自行设置 root signature、PSO、descriptor heap，并把输入变为 compute SRV，之后变回 COMMON。内部结果纹理用于随后绘制。
2. `BOE_DX12_DrawOutput @ 0x18005E380` 明确记录输出 `COMMON → RENDER_TARGET → COMMON`，设置 init 时的 viewport 和从原点开始的 scissor，再 `DrawInstanced(3,1,0,0)`。汇编已核对两处 barrier 参数。Leia 客户端交给 weaver 时，输入/输出已有自己的状态，不能套用这个假设。
3. `0x18005CC20` 创建单一 upload buffer；`BOE_DX12_UploadConstants @ 0x18005D900` 每次映射同一 buffer 并复制 80 字节。本路径没有每次 weave 的独立常量分配。不同参数的多次织图以及 GPU 在途帧必须处理常量 lifetime。
4. DX12 资源与 PSO 创建含固定格式值 `28`（RGBA8 UNORM）。输出格式参数被保存，不意味着所有资源/PSO 都实际按它创建；一般 HDR/其他格式路径不能据签名承诺。
5. 接口要求输出 resource 与 CPU RTV handle；Leia 的 weave 契约主要依赖宿主已经绑定的 RT。直接调用原 DLL 还需获得这些对象，或采用只记录自有交织/输出命令的实现。

这些限制决定了实现方式，没有推翻 SBS 交织本身的可行性。对当前 FallenFlower3D 的遮罩/颜色两次 weave，必须保留各自结果、格式与命令顺序，不能把任意一次 weave 当成 Present。

## 已提取的真正光学 shader

`BOE_SetRefractParams8_SelectShader21 @ 0x18005FF20` 选择 shader 2.1。DX12 PSO 创建 `0x18005C8C0` 采用 `0x180089840` 的 23656 字节 DXBC。反射和指令证据见 [interlace_shader21.asm](evidence/depth/interlace_shader21.asm)。

该 shader 为 `cs_5_0`，线程组 `8×8×1`，输入仅 `Texture2D LeftTexture(t0)`，输出 `RWTexture2D Result(u0)`，加一个 80 字节常量缓冲。输入名叫 LeftTexture，但指令会按布局在这张纹理的两半中选择不同眼，并非只接受左眼。没有深度纹理输入。

常量包括 `Pixel / PlaceHeight / Pitch / Angle / Offset / Zadd / Ng / Beta / Cr / Position / NLayoutMode / TexLayoutType / Width / Height / LeftTextureWidth / LeftTextureHeight`。RGB 子像素分别参与光学映射，再从 SBS/TB 对应眼位置取色。

`BOE_MapShaderConstants @ 0x18005F530` 在 shader 2.1 分支把折射数组映射为：Pitch=参数0、Pixel=参数1、PlaceHeight=参数2、Angle=参数3、Offset=参数4、Zadd=参数5；Position 来自 wholeMat 变换后的眼位。Ng/Beta/Cr 在此版本代码中赋固定 bit pattern，不能凭参数名假设八项都直接传给 shader。

`BOE_MapStereoLayout @ 0x18005F3D0`：

| mode | 包装枚举 | NLayoutMode | TexLayoutType |
|---|---|---|---|
| 0 | LRInter | 1 | 2 |
| 1 | RLInter | 0 | 2 |
| 2 | TBInter | 0 | 4 |
| 3 | BTInter | 1 | 4 |

因此即使原生 DX12 封装难以直接嵌入，也已经有可复现的 shader 与参数来源，不必从零猜面板交织规则。shader 恢复和接口分析尚不等于实机裸眼显示验收。

## 推荐实现边界

先锁定 Windows x64、现有 SR-SDK 1.34.10 ABI 和已核验的两个客户端。把左右眼打包成 SDK 需要的布局；跟踪/MCU 在一处拥有生命周期；图形后端在宿主 command list 记录交织，自己管理描述符、状态和逐次常量。

原厂实际 DX11 调用链可以作为比对基准。是否直接复用其 DLL，还是以已恢复 shader 实现 DX12 后端，属于实现阶段选择；目前还没有编译、部署或设备上验证任何 LeiaSR 转接 DLL。
