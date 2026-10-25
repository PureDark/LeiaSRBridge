# 雷神配套软件与 Stereo3DBridge 兼容性检查

日期：2026-10-25。检查对象为用户重装后的本机安装文件；“雷神配套”来自用户说明，不以应用名称或通用型号列表单独推断屏幕品牌。没有运行厂商程序、访问摄像头或串口、修改 MCU 或更改桥接实现。

## 结论

**软件依赖与现有 BOE 后端完全匹配，可以直接使用 Stereo3DBridge 0.9.0 进行雷神显示器测试，不需要为这套软件另写适配、替换 DLL 或增加 LF3D 后端。**

本次不是仅比较导出名称：20 项主程序/设备组件/模型/配置与之前分析输入或桥接原始依赖的完整 SHA256 均一致，另两项可选眼追组件也与旧输入一致。软件静态兼容的证据很强；雷神实体屏的 USB 枚举、当前屏校准读取、实际追踪和光学效果仍未验收。

## 当前实际安装

- C:\Program Files (x86)\3D Spatial Gateway：3DSpatialGateway.exe 1.6.1.73。
- C:\Program Files (x86)\DepthTransformerPlus：DepthTransformerPlus.exe 1.1.30.166。
- 本轮检查时 C:\Program Files\Depth Transformer 和 C:\Program Files (x86)\ModexLite 不存在。不能继续沿用上一轮 ViewX 的安装路径和缺失组件结论。

## 与现有桥接的逐文件比较

全部原始结果在 [component-comparison.json](evidence/thunderobot/component-comparison.json)。

| 项目 | 当前安装 SHA256 | 比较结果 |
|---|---|---|
| MCU_library.dll | 0a2e51c617b585e53e0733135ccede7d5c3d3f30d58bea23313026641e050858 | 等于桥接实际 MCU 依赖 |
| interlaceSDK.dll | 380a9194a5c2ab42cf1f70a2b3c383f9b340b3b292ac72177350aa0dae56cc6f | 等于已分析并提取光学 shader 的旧输入 |
| eyetracking/BOE_3DET_SDK.dll | 752cb438a6d98848f45870c2502ea4ffc886c8daf14c1ee411af77518cd328b5 | 等于桥接已核验的算法版本；双眼/旋转私有布局版本门槛一致 |
| eyetracking/FaceTracking.exe | 85254ad5838fc6d61c68a9eea1b216d578103239d9432cef0be19e0a1cbc37c4 | 等于桥接便携化前的原始 EXE |
| eyetracking/FaceTrackingManagerService.dll | 3132dd6fe285772b327b4aab99de5a05c5cfe4376432e9defdc2680c465e4604 | 等于桥接实际 COM DLL |
| DepthTransformerPlus.exe | fd5986a80c410edf0d9d3f5307b4235359413772ba45acaf48c264d43d962c32 | 与泰坦军团时期分析输入完全一致 |

另外一致的项目：

- BoeLandmark.dll、opencv_world4110.dll、onnxruntime.dll。
- face_det.onnx、landmark_det.onnx、data21_v2.mdata 三个模型。
- faceTrackingConfig.ini：1280×480 / 90 fps、predict_time=0.065。
- BOEInterleaving.dll、BOERenderWapper.dll。
- LF3DRenderCore.dll、LF3DRenderCoreForCSharp.dll、根目录 FaceTrackingManagerService.dll。
- StereoscopicDisplayList.json。
- Runtime_BOE_18NBSDK_ETSP.dll、boe_ET_Predict.dll 也与旧分析输入一致，见 [optional-tracking-components.json](evidence/thunderobot/optional-tracking-components.json)。它们并非此次新增加的必需桥接依赖。

注意区分根目录 COM DLL 与 eyetracking 子目录 COM DLL：前者是 24d677ea…，后者是 3132dd6f…。桥接实际使用后者，本次后者确实匹配。

桥接私有 FaceTracking.exe 为注册无关运行修改过嵌入资源，因此应比较原始 source_sha256，而不是要求原厂 EXE 等于打包后的 EXE。没有用原厂文件覆盖桥接的私有清单、代理或转发 DLL。

## IDA 证据复用范围

本轮先做完整哈希对比，关键二进制均未改变，因此没有重复启动无头 IDA，也不声称产生新的反编译结果。相同字节的既有 IDA、托管调用链和 shader 证据直接适用：

- [depth_managed_analysis.md](depth_managed_analysis.md)：主程序的 BOEInterleaving 调用与 SBS 跳过 AI 的分支。
- [depth_interlace_analysis.md](depth_interlace_analysis.md)：BOE 交织 DX11/DX12 ABI、80 字节常量和光学 shader。
- [portable_bridge_0.1.1.md](portable_bridge_0.1.1.md)：FaceTracking 便携运行、COM 共享节和独立目录生命周期。
- [bridge_rotation_0.4.0.md](bridge_rotation_0.4.0.md)：相同 BOE_3DET_SDK 的同帧双眼和浮点关键点数据。
- [portable_dependency_audit.md](portable_dependency_audit.md)：同一 MCU SDK 的 USB 串口筛选和依赖闭包。

与 ViewX 的区别：那次安装的主程序为 Depth Transformer 1.3.1.045，实际使用新的 LF3D 文件标定路径且缺少眼追采集程序；本次为完整且字节匹配的旧 BOE 路径，不能混为一谈。

## 桥接源码核对

- DeviceProvider 使用随包 vendor/MCU_library.dll，调用 BOE_MCU_initSDK/openMCU/getUSBState，读取实际设备的 6/6/8 光学参数及 9/9/10 摄像头参数。
- 本次核对的桥接设备初始化和显示选择路径没有额外限制 TTR、M27E6V、泰坦军团品牌或 EDID 型号白名单；厂商 MCU DLL 自身的 USB 筛选仍有效。
- 桥接根据实际读取的标定交织，不需要复用雷神安装目录里的 boe_mcu_mat.ini，更不应把另一块屏的标定抄到新设备。
- BoeTrackingProxy 的算法 SHA256 门槛与雷神安装的 BOE_3DET_SDK.dll 一致，因此不需要修改其私有数据布局偏移。
- 原厂互斥检查的 DepthTransformerPlus.exe 名称与本次安装一致。
- 现有桥接仍按主显示器确定显示尺寸；测试时目标裸眼屏应为主显示器。

## 使用与验证边界

直接使用已有 Stereo3DBridge 0.9.0 完整便携包；退出原厂 DepthTransformerPlus（包括会重新拉起它的守护程序）后，连接屏幕所需的视频和 USB，按既有桥接流程测试。无需将雷神目录中的 DLL 复制进桥接。

+8 mm / 40 ms 是已有实测默认值，不代表每台雷神屏和每位使用者的最佳补偿；保留界面中的调节与保存功能即可。

本次没有启动真实设备、运行游戏或编译新包；仅将分析记录及文本证据提交 Git。结论是“软件栈与已支持后端完全匹配，可直接测试”，不是“已在雷神实体显示器验收通过”。
