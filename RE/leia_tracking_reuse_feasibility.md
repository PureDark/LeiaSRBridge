# Titan 眼追扩展与原厂 Leia 路径对照

日期：2026-10-21。结论来自官方二进制的无头 IDA 分析、当前 Leia 服务的有限只读采样，以及 0.2.0 组件来源记录。本次没有改动发行包、MCU、相机配置或运行中的官方服务。

## 结论

0.2.0 的 `vendor/eyetracking` 主要来自官方 DepthTransformerPlus：FaceTracking.exe 负责采集，BOE_3DET_SDK.dll / BoeLandmark.dll 和模型负责检测。FaceTracking.exe 私有副本嵌入了免注册 COM manifest；原厂算法 DLL 在 0.2.0 中保持原始字节。桥接、设备管理、数据转发与 Leia 兼容接口是本项目代码。

可以自行扩展数据发布与姿态求解，无须从零重写检测模型。BOE 原厂内部已有分别计算的双眼和面部关键点；当前 COM 只发布中心位置等少量值，丢弃了更丰富的结果。旋转有明确实施入口，但原厂内部模板、坐标和有效性尚待验证，不能直接将三个角作为可靠的 Leia HeadPose 发布。

整套 SREyeTracker.exe 不能仅凭拷贝就认定可用。其底层 blink 接受图像及相机参数，说明编写相机适配器在架构上存在入口；还没有完成 Titan 相机接入、标定适配、模型初始化和效果验证。

## 本机原厂 Leia 的实际路径

当前显示器是 Samsung G90XF，摄像头为 `3D Stereo WebCam`，设备 ID 含 `VID_04E8&PID_20D7`。运行中的官方 SREyeTracker.exe 使用 ET 产品配置，通过 Media Foundation 采集同一设备的左右图像，进入 BlinkEyeTracker。

```
Media Foundation 相机输入 + 每机相机标定
    → Blink FaceDetector
    → HeadPoseDetector / Landmarks3dDetector
    → 头位、姿态、两眼
    → 预测/稳定处理与 SR 服务发布
    → Head / HeadPose / EyePair
```

配置包含不同用途的预测时间：普通眼位 15.6 ms、场景 20 ms、头姿 40 ms，以及眼位稳定参数。不能用这些默认值直接取代 Titan 上已经实测选出的 +8 mm / 40 ms。

相关文件：

- `C:/Program Files/LeiaSR/Tracker/products/ET/ft_user.ini`
- 同目录 `calibration.yml`：双相机模式、640×480 单路尺寸、相机设备匹配条件。
- `C:/ProgramData/Simulated Reality/Devices/VN022519ET0014/` 的 intrinsics.yml、extrinsics.yml、Tracker2DisplayTransform.ini：当前这台设备的内外参及相机到屏幕变换。不能用于另一台屏。
- `C:/Program Files/LeiaSR/Tracker/model/`：检测、眼位、面部模型及初始化依赖。

证据副本与源路径/哈希在 `evidence/leia-tracking/config-sources.json`；启动选择证据在 `active-tracker-startup.txt`。启动日志也加载旧 OpenFace 资源，但实际选择为 BLINKEYE，不能仅据旧模型文件名认定活动算法。

## 静态证据

| 文件与地址 | 核实的数据路径 |
|---|---|
| SREyeTracker.exe `0x14010A3A0` | 创建 BlinkContext、FaceDetector、HeadPoseDetector、Landmarks3dDetector |
| SREyeTracker.exe `0x140108910` | 读取检测数据中的眼点和 68 个面部关键点 |
| SREyeTracker.exe `0x140109130` | 调用 `HeadPoseDetectorData::get_head_pose` 并转换输出坐标 |
| SREyeTracker.exe `0x140108E20` | 调用 `Landmarks3dDetectorData::get_3d_eye_centers`，分别读取两个 XYZ；本函数没有用固定 IPD 合成双眼 |
| SimulatedRealityFaceTrackers.dll `0x18001DA00` | 调用 HeadTracker::create，并创建 HeadPose 包装；两者共享上游数据 |
| blink.dll `0x18139E3C0` | BlinkContext 初始化涉及模型路径、license 路径检查和激活流程 |

双眼读取已另核对汇编：读取 vector 的第 0/1 项，各取三个 double 转 float，并执行 X 符号转换。详细输出在 `evidence/leia-tracking/0x140108e20-asm.json`。

blink 的导出图像接口接收像素、尺寸、步长、时间和 BlIntrinsics / BlExtrinsics；相机采集位于上层。这个接口事实支持编写自有图像输入适配器的可能性，不等于已证明任意相机兼容。当前分析没有证明许可证绑定具体显示器，也没有绕过任何许可证检查。

身份：

- SREyeTracker.exe 1.11.0.35453，SHA256 `91b02c13592167842023151804c0fe50515bf1609791fa247d2f1385ea97f261`
- blink.dll 3.50.4.3，SHA256 `041cd672193405adfa865846f6026dcc29b61988b340b597cc41b846c8f67469`
- SimulatedRealityFaceTrackers.dll 1.36.4.35453，SHA256 `f4c86e5b519985abeaa8c3a4d2a86a6520ea8f44b378356346b6f21bf13448ce`

## 双眼、旋转、双耳分别能做什么

| 数据 | BOE 当前可取得的信息 | 扩展方式与限制 |
|---|---|---|
| 独立双眼 XYZ | 168 字节结果偏移 12、24 已分别给出双目计算结果 | 截获完整结果并发布；定位点为眼睑轮廓均值，非瞳孔/视线；需实机核对左右、镜像及深度 |
| 头部旋转 | 468 个二维关键点，内部 9 点 solvePnP 和旋转矩阵 | 截获内部点/矩阵；核对模板、重投影误差及坐标变换，必要时自行求解 |
| Leia 兼容耳位 | 没有找到独立耳部检测 | 在有效头姿上构造耳位模型；属于估计值 |
| 真实双耳测量 | 当前模型路径没有对应输出 | 需要耳部定位模型、可见性处理及双目重建 |
| 虹膜/注视方向 | 当前路径没有对应输出 | 不能从眼睑中心或头朝向直接冒充视线 |

原厂 Leia 实时样本中的双耳符合 `head.position + Rodrigues(orientation) × (±70,0,0)`，最大坐标残差不到 0.000060 mm，支持固定 140 mm 头模而非独立耳部测量。此安装版本的 orientation 与 Rodrigues 旋转向量语义吻合，不能简单按三轴 Euler 顺序解释。实测版本是 1.36.4，客户端使用 1.34.10 头文件，结论不泛化为所有 SDK 版本。

实时输出存在重复时间戳和异常位置，约 240 Hz 回调不能称为相机帧率。完整采样条件、限制和数学核对见 [原厂实时探针报告](leia_live_tracking_probe.md)。

## 推荐实施顺序

1. 保留已经工作的官方采集与滤波，私有 SDK 代理转发原调用，并截取完整 168 字节结果，通过独立 IPC 发布同一帧的中心与双眼。该代理只扩展读取，不恢复已删除的滤波旁路。
2. 截取内部关键点或姿态矩阵，实现经过验证的相机到显示器姿态转换。原厂内部 PnP 尚有模板/点序疑点，优先取得点集并检查重投影误差；不要不加检查地导出角度。
3. 同一份有效姿态同时发布 Head 和 HeadPose；Head 补充独立眼位及明确为模型估计的耳位。继续保留无脸默认眼位的显示回退，但回退不能被标成新的真实测量。

这条路径不需要写 MCU 标定。原始图像或姿态没有在 Titan 实机上验证前，不能宣告完整旋转功能已经可用。本次完成的是可实施入口与原厂行为核查，没有生成新的测试发行版。

BOE 的确切结构、调用地址与失败语义见 [BOE 完整跟踪报告](boe_full_tracking_feasibility.md)。研究 IDB 已增加对应语义注释并保存；原厂安装目录及 0.2.0 发行包未改动。
