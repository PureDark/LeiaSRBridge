# AOC 配套软件与 LeiaSRBridge 兼容性检查

日期：2026-10-25。分析对象来自用户本次重装的软件；AOC 品牌归属来自用户说明，未提供具体显示器型号。本次只做静态分析，没有运行厂商程序、访问摄像头、发送 MCU 命令或修改标定。

## 结论

**这套 AOC 软件仍采用现有桥接支持的 BOE 设备控制、眼追算法和光学交织方案，可直接使用 LeiaSRBridge 0.9.1 进行实机测试，无需新增显示后端。**

这不是“整个安装包完全相同”：新版眼追采集程序新增可选的摄像头人脸区域反馈（BOE_ROI.dll），主程序和两个包装 DLL 也有变化。已用无头 IDA、汇编、托管反编译及完整哈希分别核实。当前桥接没有集成新增 ROI 功能；软件兼容证据不等于已验证 AOC 实体屏的枚举、追踪质量及裸眼效果。

## 实际安装及样本身份

| 安装目录 | 主程序版本 |
|---|---|
| C:\Program Files (x86)\3D Spatial Gateway | 1.8.1.117 |
| C:\Program Files (x86)\DepthTransformerPlus | 1.1.30.184 |

[identity-and-presence.json](evidence/aoc/identity-and-presence.json) 保存主程序身份、五份分析副本的 SHA256 和版本信息。所有副本与安装源文件一致；分析结束前重新核对组件哈希，未发现安装内容中途变化。

本次与雷神记录中的 21 项基准逐项比较：14 项相同、4 项变化、3 项缺失，另发现新增 BOE_ROI.dll。完整结果见 [component-comparison.json](evidence/aoc/component-comparison.json)。

| 关键组件 | 比较结果 | 对桥接的意义 |
|---|---|---|
| MCU_library.dll | SHA256 完全相同 | 设备控制与校准读取接口一致 |
| interlaceSDK.dll | SHA256 完全相同 | 沿用已分析的原生光学交织及 shader |
| eyetracking/BOE_3DET_SDK.dll | SHA256 完全相同 | 桥接同帧双眼及旋转浮点数据读取的版本门槛满足 |
| eyetracking/FaceTrackingManagerService.dll | SHA256 完全相同 | COM 共享眼位接口和便携激活方案一致 |
| BoeLandmark.dll、opencv_world4110.dll、onnxruntime.dll | SHA256 完全相同 | 核心眼追依赖一致 |
| faceTrackingConfig.ini、face_det.onnx、data21_v2.mdata | SHA256 完全相同 | 摄像头配置及所用模型一致 |
| FaceTracking.exe | 已变化 | 新增 ROI 调用及显示/日志相关代码，详见后文 |
| BOEInterleaving.dll、BOERenderWapper.dll | 已变化 | 已核对主要托管接口，仍使用相同的原生输出路径 |
| DepthTransformerPlus.exe | 已变化 | 新版应用仍通过 MCU + BOE interlaceSDK 工作 |
| LF3DRenderCore.dll、LF3DRenderCoreForCSharp.dll | SHA256 完全相同 | 不能仅因存在 LF3D 文件，就归类为 ViewX 的新版路径 |
| StereoscopicDisplayList.json | SHA256 完全相同 | 没有发现本次特有的显示列表变化 |

关键 SHA256：

- MCU_library.dll：0a2e51c617b585e53e0733135ccede7d5c3d3f30d58bea23313026641e050858
- interlaceSDK.dll：380a9194a5c2ab42cf1f70a2b3c383f9b340b3b292ac72177350aa0dae56cc6f
- BOE_3DET_SDK.dll：752cb438a6d98848f45870c2502ea4ffc886c8daf14c1ee411af77518cd328b5
- 新 FaceTracking.exe：b11b153445658424809ee196aac532a6e5a9091e42eecd9b585ae15c0e532cc9
- 新 BOE_ROI.dll：76269befa8b1e98218dfd837ab4a67e48b8ba4d2eafca9a5b60fe5b6ca285b58

安装包缺少 landmark_det.onnx、Runtime_BOE_18NBSDK_ETSP.dll 和 boe_ET_Predict.dll。后两者本来不在现有便携采集程序的必要导入闭包中；桥接自己的 vendor 目录仍保留 landmark_det.onnx，因此这些安装包裁剪不要求删除或替换桥接依赖。参见 [portable_dependency_audit.md](portable_dependency_audit.md)。

## 主程序与包装层的实际调用链

相关反编译保存在 [managed/](evidence/aoc/managed/)，PE 导入表在 [native-imports.json](evidence/aoc/native-imports.json)。

- CaptureRenderBase.InitializeBoeRenderer：Token 0x060003EE，RVA 0x0001BFA4。先调用 BOE_MCU_openMCU，读取 6/6/8 个 double 的设备、wholeMat 和折射参数，再调用 Interleaving.InitSDK，并设置相同参数。光学标定仍来自当前设备。
- CaptureRenderBase 与 BasicCaptureDXGIAsync 的实际输出调用仍是 Interleaving.DoInterleave，输入眼位、SBS SRV、输出 RTV、视图尺寸与 RLInter 模式。
- FrameConvert.DrawFrame：Token 0x06000419，RVA 0x0001D31C。InterWave_SBS 分支只调用 FrameCopy.DrawFrame；AI Render 位于另一分支。仍可以直接使用游戏已有的左右眼图像。
- BOEInterleaving.Interleaving 保留相同 InitSDK / DoInterleave 参数布局，PE 导入直接指向 interlaceSDK.dll。与旧反编译文本的差异主要是局部变量名，不能把文本差异当成 ABI 改变。
- BOERenderWapper.RenderWapper 去除 Token/RVA 注释后，与旧包装类的反编译文本完全相同。该结论只覆盖已比较的包装类，不声称整个混合 DLL 逐字节等同。

桥接本身不加载这些托管包装 DLL，直接使用已核验的设备组件与自己的交织实现。

## 无头 IDA：新版眼追与旧采集程序

使用 IDA 9.4 的独立 force_headless 会话 aoc_tracker、aoc_reference、aoc_roi。每个会话先检查 server_health，确认分析副本路径、模块、自动分析完成与 Hex-Rays 可用；没有沿用之前安装路径对应的活动数据库。

| 行为 | AOC 新版地址 | 旧版地址 |
|---|---|---|
| 摄像头选择 | 0x14000C190 | 0x14000BD20 |
| 每帧眼追处理 | 0x14000D9A0 | 0x14000D4D0 |
| doEyeTrack_3D 调用 | 0x14000DB59 | 0x14000D699 |
| COM 眼位发布调用 | 0x14000DBB3 | 0x14000D6FC |

两版均用 Media Foundation 枚举摄像头，根据配置的宽、高和帧率匹配媒体模式，并将 NV12 图像交给同一个 doEyeTrack_3D。新版主函数 0x140011140 读取 camera_info，默认 1280×480 / 90 fps；配置文件也与旧版完全相同。新版继续激活同一套 COM 共享组件，发布中心眼位。真实双眼和旋转仍可由桥接现有代理从相同 BOE 算法结果中取得。

没有在所检查的摄像头选择分支发现 AOC 品牌/型号白名单。此结论不扩展为所有摄像头或固件均能采集成功。

完整反编译及交叉引用见 tracker-main-full.json、tracker-camera-full.json、tracker-frame-full.json、reference-camera.json、reference-frame.json 和 tracker-import-xrefs.json。

## 新增 BOE_ROI.dll 的意义及边界

[tracker-import-diff.json](evidence/aoc/tracker-import-diff.json) 确认新版新增 BOE_ROI_init、BOE_ROI_release、BOE_ROI_setFaceROI 导入。

- BOE_ROI_init 通过摄像头 ActivateObject / QueryInterface 获取 IKsControl；失败返回 false。
- BOE_ROI_setFaceROI 接收人脸矩形坐标，先尝试 Sunplus 协议，失败再尝试 Realtek；成功后缓存协议选择。无有效接口或协议不支持时返回 false。
- 采集初始化在 0x14000E7BA 调用 BOE_ROI_init。成功与失败仅走不同日志分支，随后都到 0x14000E941 继续正常采集初始化；并非初始化失败就禁止眼追。
- 每帧在 0x14000DB59 调用算法，0x14000DBB3 先发布眼位，随后每三帧在 0x14000DC0B 反馈人脸区域；汇编显示该调用的返回值没有用于阻止位置发布。
- 当前桥接的旧采集程序不导入 BOE_ROI.dll。已有 BOE 算法、COM、媒体模式匹配与校准链路并未因此失效。

因此，ROI 是可选摄像头区域控制增强，不是新的眼追算法或显示器交织后端。没有实机测量，不能声称它在 AOC 上对曝光、采集质量或追踪延迟完全无影响；如以后补齐这一增强，需要单独迁移并验证新版采集程序的便携 COM 清单和新增依赖，而不是直接覆盖旧 EXE。

反编译：roi-init.json、roi-set.json、roi-release.json、tracker-capture-start.json。汇编：tracker-roi-init-asm.json、tracker-eye-output-asm.json。

## 数据库整理

仅修改 RE/inputs/aoc 下新建的分析数据库，原始程序字节不变：

- 0x14000C190 → Aoc_FindCameraByConfiguredMediaMode
- 0x14000D9A0 → Aoc_TrackFrame_PublishCenter_OptionalROI
- 0x14000E350 → Aoc_StartCapture_WithOptionalROI
- 在 ROI 初始化分支、COM 眼位发布及 ROI 更新调用处补充已核实的行为注释。

重命名、注释结果保存在 tracker-renames.json、tracker-comments.json、roi-comments.json。未改动旧分析数据库或桥接源码。

## 实际使用建议与未验证事项

先使用现有 LeiaSRBridge 0.9.1 完整包；关闭 DepthTransformerPlus 及其守护进程，连接屏幕视频和 USB，将目标裸眼屏设为主显示器。使用包内配套组件，不覆盖为此次安装的新 EXE 或 DLL。

仍需 AOC 实体屏验证 USB 枚举、设备校准读取、1280×480 / 90 fps 摄像头模式、实际双眼/头姿追踪及裸眼输出。+8 mm / 40 ms 为现有默认补偿，不代表所有 AOC 型号的最佳值。本次未新增兼容代码、未改 MCU 标定、未编译新发行包。
