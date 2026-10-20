# 原厂 LeiaSR 实时跟踪只读探针（2026-10-21）

## 结论

当前运行的原厂 LeiaSR 服务确实发布 `Head`、`HeadPose` 和 `EyePair`，包含变化的头部位置、姿态和两眼位置。**双耳字段不是独立测量的双耳：它们与“头中心 + 固定左右各 70 mm 的旋转头模”数值一致。**

更重要的是，当前原厂 DLL 的 `Head.orientation` 在耳模型中精确表现为 **Rodrigues 旋转向量（轴角）**。不能直接把这三个数按逐轴 Euler pitch/yaw/roll 相乘。SDK 1.34.10 头文件称其为 pitch/yaw/roll，当前运行实现与这种常见的 Euler 解释并不一致。

这次观察能证明接口实际有数据和耳模型的生成关系；**不能证明两眼数据都是独立、准确的物理三角测量，也不能证明姿态估计精度或任何相机的可移植性**。

## 边界与身份

- SDK 编译头文件及导入库：`F:/GithubMods/UU3D/dependencies/submodules/SR-lib/SR-SDK-1.34.10/x64`。
- 实际加载的 `SimulatedRealityCore.dll`、`SimulatedRealityFaceTrackers.dll` 均在 `C:/Program Files/LeiaSR/Platform/bin`；进程内枚举核实，没有加载 Titan 桥。
- 当前 FaceTrackers 原厂版本：`57c9f88a7d v1.36.4.35453 2026-06-08T09:19:10Z`。**头文件版本与实际运行平台版本不同，结果仅代表这个安装版本。**
- `SimulatedRealityCore.dll` SHA256：`8ed554af444f8b98284358b060108465b611250fa35ff6b5fe73d4ee6c19254b`。
- `SimulatedRealityFaceTrackers.dll` SHA256：`f4c86e5b519985abeaa8c3a4d2a86a6520ea8f44b378356346b6f21bf13448ce`。
- 使用 `SRContext::create(false, NonBlockingClientMode)`，仅订阅已运行服务。没有构造 Display、Lens、Weaver 或 Camera；没有打开相机、接管相机、操作镜片、改服务或配置、运行游戏。
- 结束后 `SREyeTracker` PID 43736 和 `SRService` PID 19276 仍在运行。订阅清理时原厂库打印一次被取消的 socket receive 日志；客户端正常退出 0，订阅和上下文已销毁。
- 不保存相机图像或逐帧坐标，仅保存聚合数值。首轮初始化及第二轮预热观察也保留为有限证据。

## 最终 10 秒观察

先丢弃 2 秒订阅预热，再记录 10 秒。没有人为规定用户动作，不能把该样本作为姿态精度测试。

| 项目 | Head | HeadPose | EyePair |
|---|---:|---:|---:|
| 回调数 | 2393 | 2393 | 2401 |
| frameId 前进次数 | 2392 | 2392 | 2400 |
| time 前进次数 | 531 | 531 | 531 |

Head 内部 headPose / eyes / ears 的 `frameId`、`time` 与顶层完全一致，2393/2393。Head 和 HeadPose 聚合对应一致。

不能把约 240 Hz 回调率称作相机帧率：大量回调重复时间戳。最终观察有 531 次时间前进，正向差值最短约 16.47 ms，最大 565.7 ms；前两轮还出现较长陈旧时间戳跃迁。SDK 注释称 time 是 epoch 微秒，但本机实际约 `205383299814`，不对应当前 Unix epoch。因此这里未将它认定为相机曝光时间或可靠的端到端延迟依据。

### 有效性与筛选

- 最终 2393 个 Head 的位置、姿态、两眼、两耳相关聚合均为 finite，无 NaN/Inf。
- 没有按“生理合理”位置/姿态范围删掉样本，也没有去重时间戳。所有模型拟合使用这 2393 个收到的 Head，包括异常位置和重复内容。
- 最终样本头部 z 为 -439.73～1836.71 mm，眼距 25.29～110.93 mm；前一轮有 288/2386 个双眼重合的帧。这说明 **finite 不等于跟踪有效或准确**。
- 因而本记录不将 2393 次回调当成 2393 次独立传感器观测，也不对两眼/头位输出的准确性作保证。

## 双耳模型与姿态语义

最终 2393 帧的耳距为 139.9998805～140.0001198 mm。耳中点与 head.position 的最大距离为 0.00006082 mm。

设 `r = headPose.orientation`，`p = headPose.position`。标准 Rodrigues 旋转矩阵满足：

```
R(r) = I*cos(theta) + (1-cos(theta))*u*u^T + sin(theta)*[u]x
theta = length(r), u = r/theta

ear_left  ≈ p + R(r) * (-70, 0, 0)
ear_right ≈ p + R(r) * (+70, 0, 0)
```

探针逆变换采用 `R(-r) * (ear - p)`。实测结果：

| 数值 | 左耳 | 右耳 |
|---|---:|---:|
| 局部坐标均值 x（mm） | -70.0000006161 | 70.0000009493 |
| 局部坐标均值 y（mm） | 0.00000002254 | -0.0000004664 |
| 局部坐标均值 z（mm） | -0.0000004692 | 0.00000008003 |
| 每帧最大 L∞ 残差（mm） | 0.0000599363 | 0.0000599784 |
| 每帧 L2 残差的保守上界（mm） | 0.0000991515 | 0.0000999250 |

L∞ 最大值直接由各轴已保存的 min/max 计算，是这批帧的最大坐标残差。L2 只给保守上界，因为不同坐标的最大值未必属于同一帧；未保存逐帧数据，不能把这个上界写成实际最大 L2 残差。

双耳 6 个局部坐标的标准差为约 7.5e-6～2.2e-5 mm，合并方差 1.357e-9 mm²，符合浮点计算/传输量化误差。耳距和相对头位关系在此样本中是固定头模，不能称为独立追踪真实耳部。

先前枚举常规 Euler 的 6 个轴顺序及 8 种符号组合不能得到这一固定局部坐标。再将 orientation 作为旋转向量使用 Rodrigues 公式，误差降至上述量级，区分非常明确。

数学实现另做离线检查：3 个轴各正 90° 的已知结果、零向量恒等、独立显式矩阵对照、旋转再逆转及长度保持，共 **232 项**，最大误差 **5.47e-14**。这些检查不连接跟踪服务。

双眼在相同逆变换后并不固定在头模某两个点（各轴标准差约 98～142 mm，含异常/不同步/估计误差的可能性）。这排除了“本样本双眼只是同一个固定头模旋转后的点”的简单解释；**仍不足以证明原厂以何种算法独立测量两眼**，也不排除各流内部滤波时序差异。

## 文件与复现

- 源码：`leia-probe/probe.cpp`
- 离线数学检查：`leia-probe/math_check.cpp`
- 构建：`leia-probe/CMakeLists.txt`
- 最终聚合：`leia-probe/live-aggregate.json`
- 模块路径与生命周期：`leia-probe/modules-and-status.log`
- DLL/探针身份：`leia-probe/artifact-identity.json`
- 最大残差范围：`leia-probe/ear-residual-bounds.json`
- 数学检查结果：`leia-probe/math-check.log`
- 前两次有限聚合：`leia-probe/initial-aggregate.json`、`leia-probe/warmup-aggregate.json` 及对应模块日志。

使用 Visual Studio 18 x64 Release 构建。运行时进程 PATH 前置原厂 `C:/Program Files/LeiaSR/Platform/bin`，探针在创建上下文之前枚举加载模块，若发现 Titan 模块或不在该原厂目录的 SimulatedReality 模块则退出。

没有修改 TitanLeiaBridge 产品代码，也没有把本机原厂跟踪服务移植或切换到泰坦军团相机。
