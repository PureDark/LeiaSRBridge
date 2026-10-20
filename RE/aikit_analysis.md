# AiSetereoKit.dll 静态逆向结论

分析日期：2026-10-20。目标为 `RE/inputs/AiSetereoKit.dll` 副本，无头 IDA session `aikit`，imagebase `0x180000000`。SHA-256：`c3cd9fd7085a076a4ff4cd2f80816027c096785cd8395c191b3738ddd528d639`。完成 `server_health` 身份核对与自动分析后使用 Hex-Rays、关键 x64 汇编和 DXBC 反汇编交叉验证。未加载/执行目标 DLL，未改变生产文件或设备状态。

## 结论与转接层影响

**AiSetereoKit 是单目图像经 AI 深度估计生成普通水平 SBS 双目图像的 D3D11 库，不是泰坦军团 M27E6V-3D 的面板织图/眼追踪驱动。** 它提供可调用的 C 导出与内部 C++ 虚表，但已检查的渲染路径不接收真实双眼图像，不读取眼位或显示屏柱镜校准。LeiaSR mod 已经生成双眼，因此转接层应直接保留双眼并对接另行确认的原厂输出/模式控制链；调用这里的 AI 单目重建流程既无必要，也不能代替开启面板 3D。Dart 中出现 BOE 管道名称，但该型号具体 backend 仍待核实。

这份结论限于本 DLL 的已验证主渲染路径、导出与相关动态加载点；没有用静态字符串缺失单独推断整套软件不存在眼追踪。整套软件的 IPC 与其他组件由主报告另行分析。

## 导出与 ABI 形态

公开导出共 13 个，初始 `survey_binary` 只返回前 10 个，完整列表由名称查询与导出入口汇编补全。以下地址均为 VA。

| API | 地址 | 已证实用途 |
|---|---|---|
| `LoadStereoEngine` | `0x18015A770` | 构造/初始化全局 `DepthEngineVino`，输入可选窄字符串 |
| `CreateStereoRenderer` | `0x18015AB40` | 旧版对象创建接口 |
| `UnloadStereoEngine` | `0x18015ADD0` | 旧版全局卸载 |
| `IsValidLicense` | `0x18015B0C0` | 授权检测入口 |
| `YL_GlobalInitStereoEngine` | `0x18015B170` | 另一组全局深度引擎初始化 |
| `YL_GlobalUninitStereoEngine` | `0x18015B540` | 全局清理 |
| `YL_CreateStereoRendererEx` | `0x18015B830` | 验证授权后创建 `StereoGraphicsEngine` |
| `YL_InitGraphicsDevice` | `0x18015B9E0` | `(handle, ID3D11Device*, ID3D11DeviceContext*)` |
| `YL_SetFrameSize` | `0x18015BA00` | `(handle, width, height)`，设置输出 viewport 和纹理 |
| `YL_Render` | `0x18015BB50` | `(handle, monoSRV, outputRTV, arrangement)`，默认 `flipY=0` |
| `YL_Render2` | `0x18015BCA0` | `(handle, monoSRV, outputRTV, arrangement, flipY)` |
| `YL_SetParax` | `0x18015BE10` | `(handle, float fDpt, float dD3)`，两值都钳制到 `[0,1]` |
| `YL_Release` | `0x18015BF60` | 清理 renderer |

参数类型属于静态恢复结果，尚非运行验证后的正式头文件。Hex-Rays 对未知虚函数尾跳丢失部分参数，不能照抄其 `YL_Render(handle)` 原型：`YL_Render2` 在 `0x18015BDF0..0x18015BDFE` 直接尾跳到 vtable `+24`，完整保留寄存器/栈参数；默认 thunk `0x18013ECA0` 写入第五参数 `0` 后调用同一实现。渲染函数 `0x18013ECC0` 最终把输入送入 `PSSetShaderResources`，输出送入 `OMSetRenderTargets`，由此恢复 SRV/RTV 类型。`YL_InitGraphicsDevice` 的实现 `0x18013D990` 在 `+32/+40` 保存 device/context，随后调用符合 D3D11 vtable 的 CreateVertexShader/CreatePixelShader/Map 等方法。

renderer 虚表位于 `0x180585938`：析构 `0x18013D6A0`、初始化 `0x18013D990`、尺寸 `0x18013EBE0`、Render2 `0x18013ECC0`、Render 默认 thunk `0x18013ECA0`、Parax `0x18013F360`、Release `0x18013F460`。创建例程分配 `0x158` 字节对象，并持有全局深度引擎。`YL_CreateStereoRendererEx` 在 `0x18015B83B` 检查授权失败时抛出 `Illegal License!`；本次未修改或绕过授权。

证据：`evidence/aikit_export_names.json`、`aikit_yl_create.json`、`aikit_yl_initgfx.json`、`aikit_initgfx_impl.json`、`aikit_yl_render2_asm.json`、`aikit_render_default_asm.json`、`aikit_vtable_bytes.json`、`aikit_set_parax_impl.json`。

## 渲染数据流：单目 → 深度 → SBS

`StereoGraphicsEngine::Render`（`0x18013ECC0`）执行：

1. `0x18013EF24`：Preprocess 将单目颜色 SRV 渲染到低分辨率输入。
2. `0x18013EF38..0x18013EFC9`：`CopyResource`、`Map`、构造 `cv::Mat`，从 GPU 读回给 CPU 推理使用。
3. `0x18013F004`：调用 `DepthEngineVino` 虚表 `+16` 进行深度推理；失败返回 false。
4. `0x18013F1EE..0x18013F271`：将深度结果写回 D3D11 纹理。
5. `0x18013F2B1`：`DepthDilate`。
6. `0x18013F2EF`：`StereoProcess2`，输入单目颜色与深度，参数为 arrangement / flipY。
7. `0x18013F331`：`Inpating`/FXAA，将结果写至调用方 RTV。

`StereoProcess2::CreateShader`（`0x1801405F0`）明确绑定顶点 DXBC `0x18057D180`，长度 716；像素 DXBC `0x18057D450`，长度 18440。`RE/tools/inspect_aikit_shader.py` 只读取 PE 原始文件并调用 Windows 的 `D3DDisassemble`，没有加载目标 DLL。得到 `evidence/aikit_stereo_pixel_shader.asm`：

```text
cbuffer: fDpt, dD3, arrangement, flipY, rcp_width, rcp_height
t0: myShaderTexture
t1: myDepthTexture
ge r0.y, v1.x, l(0.500000)
movc r0.yz, r0.yyyy, l(0,-1.000000,1.000000,0), l(0,-0.000000,-1.000000,0)
mad r0.y, v1.x, l(2.000000), r0.y
```

这是输出 UV 在 `x=0.5` 处分左右，分别还原为 `2*x` / `2*x-1`，然后按相反方向从单目颜色与深度构造左右图。`arrangement` 控制左右方向，`flipY` 控制垂直翻转。没有 pitch、slant、center、子像素排列、眼位或视区校准参数，输出并非面板柱镜/光栅的逐子像素织图。

证据：`aikit_render_impl.json`、`aikit_stereo_process.json`、`aikit_inpaint.json`、`aikit_stereo_shader_create.json` 与 shader asm。

## StereoCore.dll 实际是模型权重容器

`StereoCore.dll` 虽以 DLL 为扩展名，但唯一目标字符串引用位于 `0x180137000`，函数日志为 `AiStereo::Core::DepthEngineVino::GetWeightTensor`。它通过 `0x180117990`（日志名 `AiStereo::Core::getModel`）读取 AiSetereoKit 所在目录下文件，并在 `0x180118330..0x180118360` 做逐字节重复 XOR：

```text
decoded[i] = file[i] ^ "YL1102"[i % 6]
```

密钥字符串地址 `0x180582E78`，`0x18011834B..0x180118353` 的加载、xor、写回指令验证了算法。`0x1801372D8` 创建 OpenVINO u8 Tensor，`0x180137310` 将解码后的文件数据复制到 tensor。它不是被解密后 `LoadLibrary` 的 PE 显示核心，因此无需围绕它寻找硬件织图代码。

另一个 AES 解密函数 `0x18014CFB0` 与此 XOR 模型流程不同，不能将其误认作 StereoCore 的解码器。

证据：`aikit_core_path.json`、`aikit_resource_load.json`、`aikit_model_key.json`、`aikit_model_xor_asm.json`。

## 硬件、IPC 与依赖边界

直接依赖包含 OpenVINO、ONNX Runtime、OpenCV 4.6、Win32/VC runtime；D3D11 device/context 由调用方提供。完整导入中没有 `DeviceIoControl`、HID、named pipe 或专用 BOE 显示 SDK。`EnumDisplayDevicesW` 以及 `SOFTWARE\\BOE` 字符串归于 EDID/序列号/授权相关代码：`0x1801434B0` 日志名 `AiStereo::EDID::Edid::GetEdidSNs`。这些不能作为面板开启或织图 API 的证据。

所有直接 `LoadLibraryA/W` 与 `GetProcAddress` 交叉引用已枚举；相关函数的可识别字符串是 OpenSSL DSO (`crypto\\dso\\dso_win32.c`)、`OPENSSL_Applink`、`_OPENSSL_isservice` 和 Winsock DNS 兼容加载，没有发现动态隐藏的厂商显示 SDK 入口。

这支持将显示模式切换、眼追踪和最终织图归到其他组件继续分析。仅靠本 DLL，可以实现单目转 SBS；无法由已见导出直接完成 LeiaSR → 原生裸眼 3D 输出，也无法据此承诺所有 LeiaSR mod 兼容。

证据：`aikit_imports.json`、`aikit_display_strings.json`、`aikit_boe_registry.json`、`aikit_loader_xrefs.json`、`aikit_loader_strings.json`。

## 保存的分析资产

IDA 数据库：`RE/inputs/AiSetereoKit.dll.i64`，已保存 9 个有证据的函数名和关键说明（`aikit_renames.json`、`aikit_comments.json`、`aikit_save.json`）。原始 MCP 响应均在 `RE/evidence/aikit*.json`。分析数据库与 shader asm 是静态证据，不代表 M27E6V-3D 实机、眼追踪、画面或游戏兼容性已经验收。
