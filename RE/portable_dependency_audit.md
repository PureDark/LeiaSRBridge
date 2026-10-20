# 便携眼追依赖审计

日期：2026-10-20。仅读取本地 PE、已保存的无头 IDA 证据、安装目录文件；未运行 FaceTracking、MCU、摄像头或原厂应用，未修改安装文件。

## 结论

现有静态证据支持把原厂眼追生产者和 MCU SDK 随转接器打包，并去掉 DepthTransformerPlus 主应用与机器 COM 注册依赖。关键约束是 broker 和 FaceTracking.exe 必须激活**同一磁盘副本**的 FaceTrackingManagerService.dll：其 eye_info 使用 PE 可写共享节，两个不同文件不能依赖同一份位置数据。

可以给私有 FaceTracking.exe 副本嵌入注册无关 COM manifest，并由 broker 的活动 activation context 指向同一 DLL。此方案未发现固定原安装路径的静态阻碍；必须再用无设备的 COM 激活与跨进程 SetPosition/GetPosition 检查验证。仅证明 manifest 能被解析不等于实际子进程使用了正确副本。

## 打包闭包

以下以 vendor/ 为 MCU DLL 目录，vendor/eyetracking/ 为眼追程序目录：

| 路径 | 用途 |
|---|---|
| MCU_library.dll | MCU 串口、光学校准和摄像头校准读取、3D 开关 |
| eyetracking/FaceTracking.exe | Media Foundation 摄像头采集、眼追生产、写 COM 共享位置 |
| eyetracking/FaceTrackingManagerService.dll | COM 接口、启动同目录眼追子进程、共享 eye_info |
| eyetracking/BOE_3DET_SDK.dll | 原生眼追算法 |
| eyetracking/opencv_world4110.dll | SDK 与生产者静态导入 |
| eyetracking/onnxruntime.dll | SDK 静态导入 |
| eyetracking/BoeLandmark.dll | SDK 静态导入 |
| eyetracking/models/face_det.onnx | SDK 中有精确模型文件名 |
| eyetracking/models/data21_v2.mdata | SDK 中有精确模型文件名 |
| eyetracking/faceTrackingConfig.ini | 分辨率 1280×480、90 fps、预测 0.065、show_img=0 |
| eyetracking/boe_mcu_mat.ini | 启动前由**当前设备**读取的 9/9/10 校准生成；不得复制本机标定给另一台屏 |

建议第一版额外保留 models/landmark_det.onnx（2,433,524 字节）：主 SDK 未发现这个文件名字，但尚未完整排除 BoeLandmark 内部路径，保留成本低。不需要 DI/ 图像、net8.0-windows/ 测试程序、安装/卸载 bat。Runtime_BOE_18NBSDK_ETSP.dll 与 boe_ET_Predict.dll 均不在上述静态导入闭包，FaceTracking.exe 未导入 LoadLibrary/GetProcAddress；不把它们认作必要依赖。

VC 运行库闭包需 MSVCP140.dll、MSVCP140_1.dll、VCRUNTIME140.dll、VCRUNTIME140_1.dll、CONCRT140.dll，必须可从**子 EXE**的搜索环境解析。Windows API set、MF/MFPlat/MFReadWrite、SetupAPI、OLE/COM 等由系统提供。原厂 FaceTracking.exe 是 native x64，CLR directory=(0,0)；其同目录 .exe.config 的 .NET Framework 4.8 配置不构成 managed 启动依赖。

完整 PE 导入、延迟导入、SHA256、CLR/security 目录记录于 `evidence/depth/portable_pe_dependency_audit.json`。

## 路径与 COM 证据

- `tracking_com2_sub_180008B80.json`：Open helper 调 GetModuleFileNameW(自身 DLL)，拼接同目录 FaceTracking.exe，CreateProcessW 的 lpCurrentDirectory 设为 DLL 目录。不存在固定 Program Files 路径；原函数命令行没有给 EXE 路径加引号，并使用 MAX_PATH 缓冲，便携运行目录应尽量简短，避免不可靠的路径形态。
- 同 helper 在 Open 时清空共享五个位置值。不要通过额外 Open 调用做无副作用探针；它会启动生产者。
- `tracking_exe_sub_14000F920.json`：算法初始化模型路径 `./models`，日志 `../logs`。应确保 vendor/logs/ 存在且可写。
- `tracking_exe_calibration.json`：读取 boe_mcu_mat.ini 的 E1/E2/E3，解析长度必须为 9/9/10。
- COM CLSID `{D98A272F-DE1C-4E73-A1D0-A93A8F068B4B}`，ThreadingModel Neutral；type library `{E136F17E-F67B-457F-9020-DA10237D7C82}`。FaceTracking.exe 有 CoCreateInstance 导入，其共享节生产路线已由 `tracking_exe_doEye_asm.json` 验证。
- FaceTracking.exe 现有嵌入 manifest 含 asInvoker 和 SegmentHeap，应保留并增加 COM 重定向。仅放外部 `.manifest` 不能作为可靠覆盖现有嵌入 manifest 的办法。
- EXE 的 security directory=(336384,10792)，是带 Authenticode 数据的文件。修改**私有打包副本**资源会使原签名失效，应移除陈旧签名记录并保存原始/修改后哈希。原厂安装文件保持不变。COM DLL security directory=(0,0)。
- BOE_3DET_SDK.dll 自带 manifest 提及 onnxruntime.dll/opencv_world4110.dll；随包保持同目录，未发现需要机器级 SxS 安装的 assembly dependency。

## 驱动和静态证据边界

MCU SDK 通过 SetupAPI 查找 VID_28E9/PID_018A 串口，并独占 CreateFile(COM)。摄像头通过 Media Foundation 枚举与采样。因此系统必须能枚举实际 USB 串口和摄像头，且 Windows 媒体组件可用。安装目录未找到 .sys/.inf/.cat 驱动包；不能从该事实断言别的电脑卸载主程序后仍保有所有设备驱动。

本审计未执行任意算法、摄像头、串口操作，不能证明硬件采样成功。BoeLandmark/OpenCV 中含可选 OpenCL、插件等字符串；主闭包静态导入没有厂商 GPU 驱动 DLL，不意味着所有机器上的可选后端行为已验证。
