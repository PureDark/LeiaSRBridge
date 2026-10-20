# 当前原厂组件的校准入口核查

2026-10-20。只读检查本机安装文件、PE 导出及此前保存的反编译证据；未启动原厂软件、未发送协议请求、未修改 MCU 或标定值。

## 结论

存在装载和调整标定参数的接口，但当前安装的 DepthTransformerPlus / 3D Spatial Gateway 中，尚未确认可运行的个人眼位校准向导。找到的旧版“校准入口”依赖未随本机这些安装目录提供的 BoeMCU.dll，不应当作当前 MCU_library.dll 的有效接口。

## 当前 DLL

本轮从实际 PE 重新枚举导出：

- `eyetracking/BOE_3DET_SDK.dll`：7 个导出，其中 `setCameraMat_3D` 接收 E1/E2/E3。原厂生产者从当前 MCU 的 9/9/10 数组生成 boe_mcu_mat.ini，再加载给 SDK。已有原生反编译 `tracking_sdk_calibration_internal.json` 的 `sub_180038B50` 显示它分别设置左右摄像头内参和双目变换。没有独立 start/finish/personal calibration 导出；这不排除内部算法存在未公开的能力。
- `interlaceSDK.dll`：6 个导出。`BOE_Interleaving_setWholeParam` 设置摄像头眼位到显示坐标的六参数变换；`BOE_Interleaving_setRefractDeviceParam` 设置折射/交织参数；`BOE_Interleaving_setDeviceParam` 设置基础面板参数。这些是设置已有参数，不是从用户交互自动求解校准的接口。现有原厂数值回读对照已覆盖非零 wholeMat 旋转和平移。
- `MCU_library.dll`：包含 getWholeMat、getDeviceParam、getRefractionDeviceParam、getLeftCameraMatParam、getRightCameraMatParam、getStereoCameraDistParam 等 getter；另外有 C++ 导出 `?BOE_MCU_WriteData@@YAHHPEBDH@Z`。没有名为 WriteCalidataToMCU 或 StartCalibration 的公开导出。通用 WriteData 的存在不能证明已掌握标定写回协议或持久化语义。

## 旧校准协议及限制

保存的 DepthTransformerPlus 托管反编译：

- `AstraDepth3D/WebSocketModule/YLProtocolHandler.cs:12`：YLClibrationInterWaveOpen。
- 同文件 45、69 行：offsetX、offsetZ，分别分派 1102、1104。
- 同文件 93 行：MCUValue 四浮点参数，调用另一个 MCU callback。
- `AstraDepth3D/MainWindow.cs:1176` 起：校准开关重置 `_calibration_a/_calibration_b`；offsetX 写 a，offsetZ 写 b。不能根据消息名字直接断言它们分别补偿当前眼位 X/Z。
- `MainWindow.cs:1230` 起：YLMcuVlue_callback 调 `LoadBoeMCUDll.WriteCalidataToMCU`。
- `BOEReasterTest/LoadBoeMCUDll.cs`：ReadCalidataInMCU / WriteCalidataToMCU / UpdateCalidata / UpdateEyeCoordinate 都 P/Invoke **BoeMCU.dll**，并非当前 MCU_library.dll。本次在 DepthTransformerPlus、3D Spatial Gateway、工作目录相关范围未找到该 DLL 文件。
- `CaptureSampleCore/BasicCaptureAsync.cs:566` 的 UpdateFaceTrackingState 有 `_calibration_a/_b`，把算出的 X 和 z 乘 1000 后传旧 UpdateEyeCoordinate；在该文件中未找到此方法调用点。
- 实际绘制调用（同文件约 536–542）从 GetPose 取 x/y/z 后直接送 DoInterleave；GetPose（约 619）没有使用 a/b。不能据旧协议认定当前转接层漏掉了生效中的个人校准补偿。

## 程序范围

DepthTransformerPlus 目录的可执行文件为主程序、Launcher、ProcessGuardianService、FaceTracking、COM_FaceTracking_Test、VC redist、卸载器。没有发现单独的校准 EXE。

3D Spatial Gateway 是 Flutter 程序；本机 data/app.so 与当前 ModexLite 的 data/app.so 文件大小相同但 SHA256 不同，没有将两者当作同一版本。对文件名、资产及 app.so 的相关校准词检索没有定位到可确认的校准界面；字符串阴性结果不证明功能绝对不存在。

## 对当前串扰问题的意义

用户描述的“静止有少量串扰、缓慢向左移动时消失、向右加重”使静态光学/坐标偏差值得验证，但还不是已确认根因。

后续可先在独立测试窗口对应用层坐标变换做临时、可归零的水平补偿，验证静止时能否同时改善两只眼的分离；无需先写 MCU。此实验不能证明双目摄像头内参需要重标定，也不应同时改变预测、排队和光学相位参数。此轮未实施补偿或更改现有发行包。
