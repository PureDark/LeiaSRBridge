# TitanLeiaBridge 方向性串扰：debug-mode 证据记录

2026-10-20，诊断构建 0.1.6。没有在开发机启动游戏、MCU 或摄像头。

## 预期与复现

预期：同一位置附近，以近似速度向左右移动头部，交织都能跟随眼位，静止后分眼稳定。

实际（用户反馈）：FallenFlower3D 开启 DLSS、AFW、LeiaSR；Stereo3D 已停用插帧。向右移动触发串扰/重影/立体感跳变，停住减轻，向左正常。0.1.4 的 20 ms 水平预测开/关没有消除异常。

FallenFlower3D/native/GpuBackend.cpp 的执行顺序已只读核对：AFW evaluate → composeSbs → LeiaSR Weave。排查围绕提交后的眼位、交织和显示路径，不把 AFW 或已停用的插帧当成已确认原因。

## 现有证据

- 0.1.3 用户日志 02:39:28–02:40:13 多个区间 held/default 为 0；眼位仍变化。它不支持以持续失脸/默认回退解释整个异常区间，但未逐帧对齐用户的视觉事件。
- 同区间未记录 lens OFF。软件请求没有开关不等于硬件镜片状态已得到确认。
- COM 没有曝光时间或可靠传感器帧号。observationTick 是后台读取时间，不能当作采集/显示延迟。
- 0.1.5 原厂完整 DX11 + 原厂 CPU 参数映射，与转接层常量映射 + 原始 CS 的合成样例比较：196,608 通道字节相同。原始 CS 与转接 PS 的另一个离线样例比较：84,480 通道字节相同。不能据此证明真实屏幕校准、游戏提交、呈现时序正确。
- 本机现有新增日志均为开发离线测试。没有收到 0.1.5 原厂窗口在用户屏幕上的结果；新附件头部属于 SkyrimUpscaler，排除于本任务证据。

## 可证伪假设与最小观察

| 假设 | 当前判断 | 下一项区分证据 |
|---|---|---|
| H_TRACK：共同眼位来源/校准/面板路径有方向异常 | 未验证；常规状态日志不能检查真实跟随误差 | 原厂独立 DX11 窗口也复现相同方向异常，会将优先级移至共同路径；正常则削弱此解释，但两条路径的时序仍不同 |
| H_CACHE：真实游戏的 Reset/包装层使录制快照跨帧保留 | 未验证；原生 WARP Reset 与同录制多 pass 测试正常，不代表游戏包装对象同样正常 | 消费端 max_cache_lag_ms 是否持续增长，live/selected 是否出现持续差异；一次录制内有限差异属于预期，不能单凭非零值判错 |
| H_GEOMETRY：交织像素与显示物理像素不对应 | 未验证；仅现象不能确认 | viewport、client、monitor 的尺寸/原点；不匹配支持进一步检查，匹配不能排除交织之后的缩放 |
| H_MATH：转接常量映射或 shader 的普遍方向性错误 | 已被现有合成样例削弱，未穷尽 | 保留原厂静态输出比对；不重复相同样例，也不把数值一致当动态验收 |

## 0.1.6 最小埋点

标识 `DEBUG-MOTION H_CACHE H_GEOMETRY`，仅当启动客户端时存在包内 `logs/motion-debug.enabled` 才启用。每进程独立 `motion-<PID>-<启动tick>.log`，不覆盖历史文件。适配另一台电脑的便携包，使用程序现有可写 logs 目录，不写开发机绝对路径或创建网络端点。

渲染线程只更新固定大小计数；IpcClient 原有工作线程每秒写出。统计实际 weave 选中快照与本次后台读取快照的距离/发布时间差，以及物理像素几何。没有改动眼位选择、预测参数、shader、游戏或 mod。

没有记录 GPU 执行或 Present 时间，不能用这份记录宣称测得端到端延迟。默认眼位 observationTick=0 不参与“观察年龄”最大值，日志同时保留 eye_state。

## 本轮离线实验

先运行 NativeInterlace、ABI、MotionDiagnostic，3 项通过。后续仅补跑 EyePrediction、EyeFallback、PortableTracking、Loader、Broker、Renderer，6 项通过；未重复前三项。

MotionDiagnostic 复用真实 WARP/ABI 测试，故意使同一录制的 live X 从 10 变成 20，而 selected 保持 10；确认日志记录 10 mm 差异、无效回退及未知窗口几何，IPC 工作线程退出前完成落盘。该实验确认记录机制，不确认真实故障。

证据随包：

- verification/motion-diagnostic-test.log：前三项及消费端记录内容。
- verification/test.log、test-details.log：其余六项。
- verification/StereoTest-offline.log：原厂数值对照。
- verification/artifact-verification.json：当前产物和 PDB 配对。

保留已有日志；运行新的原厂离线测试前把前次 StereoTest-offline.log 改名归档。诊断验证结束后只移除本轮 `DEBUG-MOTION` 埋点。暂不移除，因为用户真实异常尚未验证解决。

## 待用户最小反馈

优先确认 0.1.5/0.1.6 的原厂测试窗口是否也“向右闪、向左正常”，无需为升级重复已完成的原厂对照。

若原厂正常，再用 0.1.6 的可选游戏消费端日志；保持预测 0、游戏场景、DLSS/AFW、分辨率和显示模式不变，只增加记录。静止、向右并停住、向左并停住各约 5 秒，退出游戏后提供日志和对应视觉阶段。没有屏幕反馈前不把候选原因升级为根因，也不通过增加预测或翻转坐标试错。

## 新证据：20:51–20:52 原厂实测与 0.1.7

用户提供 `TitanLeiaBridge/dist/StereoTest.log`、`TitanLeiaBridge/dist/TitanLeiaBridge.log`，原文件保留。用户观感：“左右移动其实都感觉有点追踪跟不上的感觉”。原厂窗口也有动态滞后，不能用游戏独有的 DX12 缓存解释全部现象；但尚不能认定它和游戏“向右闪”完全属于同一故障，H_CACHE 仍可能是附加问题。

实测为原厂 DX11 3840×2160，测试期间预测 0 ms，主要区间约 120 Present 调用/秒；每秒汇总没有呈现吞吐下降的明显证据，不能排除亚秒停顿或呈现排队。20:51:58 请求 ON，20:52:51 测试结束时 OFF。20:52:32–35 有保持/默认眼位，再恢复；其余主要移动区间持续跟踪。

旧 `observation_age_ms` 在 Present 返回后由 GetTickCount64 计算，含 CPU 绘制/Present 等待，且没有曝光时间。持续 16 不等于测到摄像头延迟 16 ms。默认眼位时间戳 0 被错误相减，683828/684828 是开机 tick，不是残留十多分钟的传感器眼位。0.1.7 改为无观察记 -1，并分别在读取和 Present 返回后报告。

新增 H_QUEUE：呈现排队使使用的眼位在显示时已落后。与当前证据相容，未确认。

0.1.7 原厂窗口 A/B 只改变帧调度：同一个 waitable swapchain，F5=A（最多 3 帧、不主动在读取前等待），F6=B（最多 1 帧、帧就绪后才读取眼位），再 F5=A。两模式保持 VSync、纹理、眼追、标定、光学 shader 与预测不变。A 是新构建的对照，不冒充旧版非 waitable 路径。队列限制不等于真实在途帧数。

实现参考 Microsoft 官方文档 https://learn.microsoft.com/en-us/windows/uwp/gaming/reduce-latency-with-dxgi-1-3-swap-chains ，使用每交换链 SetMaximumFrameLatency，并在首帧及随后每帧取眼位前等待 GetFrameLatencyWaitableObject。QPC 记录 CPU wait/draw/Present 平均及 Present 最大值，不报告虚构的 GPU/扫描时间。

本轮只构建独立测试 EXE。NativeInterlace、NativePacing 两项通过：原厂数值输出仍一致；隐藏 WARP 窗口确认限制 3→1→3、句柄及首帧等待，无 Present/IPC/眼追/MCU。其他组件未改，保留九项旧检查并校对包内二进制身份，不重复无关回归。

待反馈：预测 0、图案 1、同位置近似速度，A 左右移动 10 秒，B 重复，A 再复核；切换后等 1 秒。B 若明显改善则支持排队影响；近似则降低它的优先级，再检查摄像头处理/过滤时序和面板路径。当前没有足够证据选择摄像头或面板中的某一项为根因。

## 第二次原厂实测及更精确描述

用户后续更新相同路径日志，并增加 `motion-10600-925484.log`。第二次测试启动 tick=928187，停止 tick=986609，约 58 秒；所有一秒采样均为 tracked/lens=1，约 120 次 Present/秒。后台 20:57:08 有短暂丢失（本 5 秒区间 held=14），不是持续默认回退。秒级记录仍不足以描述每帧轨迹。

用户进一步明确：向右移动马上看到圆右边残影，随后向左移动则该残影立即消失，停住又出现少量残影；缓慢向左移动时最正常。原厂路径也出现相同方向性，因此游戏独有 DX12 缓存不再能作为全部异常的必要解释。

新增 H_BIAS：存在静止分眼偏差，运动中的眼位滞后在一个方向暂时抵消偏差，在另一方向加剧。此现象与假设相容，但日志没有真实眼位、曝光时间或分眼光学测量，不能由此指定偏移符号/大小，也不能确认必须修改 wholeMat 或 refractParam。先做已经完成的 H_QUEUE A/B，分别记录运动与静止时残影，不同时加入偏移调整。

`motion-10600-925484.log` 只有 131 字节说明头，属于预期：启用标记让 IpcClient 创建该文件，但 H_CACHE/H_GEOMETRY 的 ObserveWeave 仅位于游戏 DX12 weaver；原厂 DX11 窗口不走它。不能把该空记录当作游戏无缓存错误的证据，本次以 StereoTest.log 为准。
