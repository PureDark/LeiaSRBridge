# LF3DRenderCore 静态逆向结果

分析日期：2026-10-20。仅对 `RE/inputs/depth` 中的副本进行无头 IDA、C++/CLI 元数据及 DXBC 静态分析；未运行目标 DLL，未操作显示器，未修改安装目录。

## 结论和定位

`LF3DRenderCore.dll` 确实包含裸眼显示所需的逐 RGB 子像素织图，能直接接收已经生成的 `2 × 1` SBS 图像，不需要 AI 深度生成。可将它作为 LeiaSR 转接层的备用 DX11 输出后端。但同一安装包主程序实际走的是 `BOEInterleaving.InitSDK / DoInterleave`，不是这个 LF3D wrapper；主路径以 [depth_managed_analysis.md](F:/目标软件/ModexLite/RE/depth_managed_analysis.md) 和 interlaceSDK 的独立分析为准。

本 DLL 已确认的 API 后端为 D3D11、OpenGL/OpenGLES。这里没有确认原生 DX12 接口；不要把其他 DLL 的 DX12 能力归到 LF3D。LF3D 的织图能力也不等于它能开启面板 3D 模式或取得眼追数据：本库的已确认接口消费外部眼位和面板参数，硬件控制、实际追踪采集仍需外围提供。

输入身份：

| 文件 | SHA256 |
| --- | --- |
| LF3DRenderCore.dll，600064 bytes，x64 native | `3ef8d67f55ffb9e071c9a5e40d022a799413b642c5eab5dc7150b5b4d9ac27f1` |
| LF3DRenderCoreForCSharp.dll，67584 bytes，C++/CLI wrapper | `6024f4b38f86b48a592579ce1060082721634ad6a49a08d31d657de5ff6e1b10` |

以下 VA 均按 native image base `0x180000000`。地址、原始函数名和伪代码见 `RE/evidence/depth/lf3d*.json`。

## 导出及可复原接口

PE 有 9 个导出，不能将 IDA 的 DLL 入口点计入导出 API：

| 导出 | VA | 已确认职责 |
| --- | --- | --- |
| CreateDX11Render | `0x180021BE0` | 输入调用方 `ID3D11Device*`，返回带虚表的 renderer |
| CreateGLCoreRender | `0x180022500` | GL core renderer 工厂 |
| CreateGLESRender | `0x180022590` | GLES renderer 工厂 |
| CreateSimpleRender | `0x180023600` | 按图形 API 枚举创建另一层包装；枚举 2 选择 D3D11 |
| DestorySimpleRenderInstance | `0x180022B50` | SimpleRender 销毁入口，拼写如此 |
| GetLensInfo | `0x180022B80` | 配置/镜片信息读取；本次未恢复完整输出结构 ABI |
| LoadLicenseFromBuffer | `0x1800228D0` | 输入 buffer 与 length，建立进程内许可证状态 |
| LoadLicenseFromFile | `0x180022620` | 从路径加载许可证 |
| VersionNumber | `0x180022610` | 版本查询 |

`LF3DDX11Render` 虚表位于 `0x180084C68`。C++/CLI 的 `LF3DRenderCoreManager.cs` 明确给出了参数和虚表 byte offset；native 函数体、汇编用于交叉确认。以下可作为后续写头文件的依据，但没有编写或执行调用测试。

| 虚表 byte offset | wrapper 方法/参数 | native VA | 约束 |
| --- | --- | --- | --- |
| 8 | `SharpInitialize()` | `0x180021530` | 初始化 DX11 shader/缓冲对象 |
| 16 | `SharpSetTexture(IntPtr texture,int w,int h)` | `0x1800215B0` | texture 是 `ID3D11Texture2D*`，不是 SRV；此版本以 GetDesc 为准，w/h 未用于读取尺寸 |
| 24 | `SharpSetCompTex(IntPtr texture,int defectType,int w,int h)` | `0x180021660` | 可选面板补偿纹理/模式 |
| 32 | `SharpSetFormat(int h,int v)` | `0x1800218A0` | h/v 写入 atlas columns/rows；SBS 为 `(2,1)` |
| 40 | `SharpLoadDeviceConfigFromFile(string path,bool viewpointOffset,bool colorInversion)` | `0x1800218D0` | native char* 路径，两个 bool 为 1 byte |
| 48 | `SharpSetScreenSize(int width,int height)` | `0x180021940` → `0x180005820` | 物理输出屏幕像素尺寸，参与相位计算 |
| 56 | `SharpSetWindowPos(int x,int y,int width,int height)` | `0x180021950` | 设置面板坐标下的输出矩形，非 D3D viewport API |
| 64 | `SharpUpdateEyeInfo(float x,float y,float z,bool enabled,float z_l,float z_r)` | `0x1800219B0` | 眼位更新和视图索引缩放；此版本未读取 z_r |
| 72 | `SharpUpdateOffsetXb(float v)` | `0x1800219E0` | 相位/横向校准偏移 |
| 128 | `SharpSetDepth(float fDpt,float dD3)` | `0x180021A10` | 2D+depth 路径参数，已有 SBS 的标准映射不依赖它 |
| 136 | `SharpDraw()` | `0x180021A30` | 使用保存的 immediate context 绘制 |
| 144 | `SharpRelease()` | `0x180021B40` | 释放 renderer 资源；对象析构另有 slot 0 |

wrapper 源码证据：[LF3DRenderCoreManager.cs](F:/目标软件/ModexLite/RE/evidence/depth/managed_decompiled/LF3DRenderCoreForCSharp/ManageLF3DRenderCore/LF3DRenderCoreManager.cs)。不要用这个 legacy renderer 的析构约定替代 `DestorySimpleRenderInstance` 对其对应对象的约定。

## SBS → 裸眼织图的证据

`0x1800218A0` 把两个整数转 float 写入参数块 `+52/+56`，恰为 shader 的 `columns/rows`。`0x180021A30` 检查 `rows > 0` 后直接使用正常映射 renderer；只有另一分支才懒建 `RenderCore_D3D11_2dz`。因此 `(2,1)` 走现成双眼 atlas 的正常路径。

`0x1800063C0` 调用 `ID3D11Device::CreatePixelShader`，输入 DXBC 为 `0x18005B610`、长度 27824。静态提取后调用系统 `D3DDisassemble`，确认它是 `ps_4_0`：输入 `Texture2D t0`、sampler s0、constant buffer b0，输出 SV_TARGET0。证据：[lf3d_mapping_pixel_shader.asm](F:/目标软件/ModexLite/RE/evidence/depth/lf3d_mapping_pixel_shader.asm)。提取脚本只读取 PE 字节，未加载目标 DLL。

shader 对每个 RGB 子像素 i 分别算相位，大意如下（省略方向翻转、边界处理）：

```text
actual = texUV * (widthProp,heightProp) + (startXProp,startYProp)
phase_i = frac((actual.x + shift_Norm + i*(PixelOffset+subp)
                + actual.y*tilt) * pitch + center)
view_i = clamp(floor(phase_i * columns * rows),0,columns*rows-1) * z_l
atlasUV = ((view_i % columns + texUV.x)/columns,
           (floor(view_i/columns) + texUV.y)/rows)
out = (sample_i[ri].r, sample_i[1].g, sample_i[bi].b, 1)
```

这证明它不是简单 SBS 复制/缩放：左右视图的选择随面板像素位置、RGB 子像素、lens pitch/tilt、眼位 shift 变化。双眼分别在两张纹理中时，转接层需先将其拼成一个可采样的 2×1 atlas；此接口没有接收两张独立左右眼纹理的参数。

输入纹理经 `0x1800215B0` 的 `GetDesc`（texture 虚表 +80）和 `CreateShaderResourceView`（device 虚表 +56）创建内部 SRV；SRV 为 Texture2D 维度、首 mip、单 mip。因此原型应先保证输入为同 device 的普通单采样、可建 SRV 的 Texture2D，特殊资源类型或 typeless 格式需由转接层整理成兼容纹理。

**必须初始化 z_l。** 参数块构造器 `0x180005460` 把含 `+60` 的区域清零；标准 shader 又将 view 索引乘 `z_l`。`0x1800219B0` 将输入 `z_l` 写入此字段，汇编与修正类型后的反编译都证明未读取 `z_r`。普通 SBS 需要 `z_l=1`；即使先使用固定眼位、`enabled=false`，也应调用该接口设置它，否则所有子像素可能一直读取 view 0。证据：`lf3d_config_draw_decompile.json`、`lf3d_eye_abi_typed.json`、DXBC 反汇编。

## 校准与眼位契约

配置加载链为 `0x1800218D0 → 0x180005600 → 0x18000F8A0 → 0x180010680`。文件并非直接裸 JSON：加载器先进行字节解码，再取 `Lens_param`、`cam_param`、`fit_curve` 等外层字段。本次只确认它有专用配置格式及实际使用的字段，不生成许可证或替代配置。

`0x180010680` 实际读取的 lens 参数包括：

- 像素/光栅：`ScreenWidth`、`ScreenHight`（原拼写）、`lens_Size`、`DPI`、`slope`、`pitch`、`subp`、`center`、`offsetXb`。
- 眼位/几何：`headcenter_x/y/z`、`pixel_size`、`gap`、`vd`、`dpitch`、`deltax/y/z`、`sign_x/y`。
- 方向/补偿：`cycle_s`、`ri`、`bi`、`photo_offsetx`、`orientation`、`rectification`、`fixedCols/Rows/Scale`、`gain`、`mean`。

`viewpointOffset` 为真时，`0x180005600` 把 `photo_offsetx` 加入 `offsetXb`；`colorInversion` 为真时交换 ri/bi。`0x180005900` 将校准写入 shader 常量；`0x1800059F0` 消费外部提供的 x/y/z，在 enabled 分支中按几何关系更新 `shift_Norm`（CB +24）、`pitch`（CB +32），还按补偿模式写其他常量。false 分支使用固定校准的基准值。

wrapper 的 x/y/z 类型已确认是 float，native 汇编在 XMM1/XMM2/XMM3 读取。**它们与 LeiaSR 的坐标原点、单位、符号和相机外参的对应关系尚未通过实际设备输入验证**，不能直接假设传入 LeiaSR 原始坐标即可。这个 DLL 不负责生成这三个追踪值；转接层需要和实际追踪服务/摄像头路径对接。

`SetScreenSize/SetWindowPos` 均影响织图相位，必须使用最终显示器的实际像素坐标。窗口缩放、DPI、显示器偏移及最终后处理若不匹配，会导致裸眼视图位置错误；这些是该公式的直接约束，尚未进行面板视觉验证。

## RTV、viewport、多 pass 和 DX12

工厂 `0x180021BE0` 的汇编 `0x180021C32..0x180021C55` 明确将 immediate context 存入对象 `+16`：调用 device vtable `+0x140`，输出地址是对象 `+0x10`。它不是接收调用方任意 deferred context 的接口。

完整 draw `0x180006280` 及标准子调用 `0x180006400/0x180006430`：设置 IA vertex/index buffer、topology、input layout、VS、PS、sampler、PS SRV0、PS CB0，然后 `DrawIndexed`。**这个路径没有绑定 RTV、没有设置 viewport、没有 Present，也没有保存/恢复宿主状态。** `SetWindowPos` 只写 shader 常量，不替代 `RSSetViewports`。

因此可以由宿主预先绑定自己的输出 RTV 和 viewport，在该 target 上织图，再由宿主 Present。反复修改输入/校准并绘制到不同 target 的多 pass 在接口层面可组织；这不是已验证的线程安全、多 context 或并发能力。转接层还需显式设置适当的 rasterizer/depth/blend 状态并恢复被覆盖的图形状态，避免继承游戏的 draw 状态。

LF3D 的已确认 DX11 工厂、Texture2D/SRV 和 immediate-context 路径不能直接吃 `ID3D12Resource*`。若单独选用 LF3D 后端，DX12 模组还需要额外 interop 或自实现织图。本安装包的实际 BOE/interlaceSDK 主路径另有 DX12 实现，应优先依据主路径分析，不必为 LF3D 先引入这层转换。

## 许可证和可行性边界

`CreateDX11Render` 在 `0x180021BE6..0x180021C15` 检查进程全局 `0x18036D660`；未成功加载许可证时弹出错误并返回 null。`LoadLicenseFromFile/Buffer` 都会解析许可证内容，经过有效期/长期许可分支后设置该状态（分别 `0x18002283B`、`0x180022AB0`）；二者在 wrapper 中均是 void，没有可直接依赖的成功返回码。

原型需使用配套且被本库接受的有效许可证，并检查 factory 是否返回 null。此次没有调用它来验证当前安装许可证状态，也未修改或绕过校验。能找到文件/导出不能证明在第三方进程中已满足该条件。

从静态证据看，LF3D 提供了 `已有双眼 atlas → 配套校准/眼位 → 自有 RTV 的裸眼织图` 这条完整可调用渲染路径。它尚不单独覆盖：面板模式开关、追踪采集、LeiaSR ABI 兼容、所有模组版本的 feature surface、许可证实际可用性、该台面板的最终光学效果。它适合备用后端或算法参照；本设备的第一实现目标应跟随已安装应用实际使用的 BOEInterleaving/interlaceSDK 路径。

## 证据与数据库

- 入口/身份：`lf3d_health.json`、`lf3d_survey.json`、`lf3d_pe_metadata.json`。
- 工厂/虚表接口：`lf3d_create_dx11.json`、`lf3d_factory_asm.json`、`lf3d_interface_decompile.json`、`lf3d_csharp_metadata.json`。
- 纹理与完整绘制：`lf3d_set_texture_asm.json`、`lf3d_draw.json`、`lf3d_draw_asm.json`、`lf3d_mapping_pixel_shader.asm`。
- 校准/眼位：`lf3d_config_draw_decompile.json`、`lf3d_config_parser.json`、`lf3d_lens_fields.json`、`lf3d_eye_math.json`、`lf3d_eye_math_asm.json`、`lf3d_eye_abi_typed.json`。
- 许可证：`lf3d_license_file.json`、`lf3d_license_buffer.json`。
- IDA 中已按证据重命名 16 个函数，补充 7 处注释并修正两个眼位函数浮点原型，记录见 `lf3d_rename.json`、`lf3d_comments.json`、`lf3d_types.json`。

数据库副本路径：`F:\目标软件\ModexLite\RE\inputs\depth\LF3DRenderCore.dll.i64`。保存与关闭结果见 `lf3d_save.json`、`lf3d_close.json`。
