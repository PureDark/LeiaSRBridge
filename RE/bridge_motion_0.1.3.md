# 0.1.3 方向相关闪动排查

2026-10-20。用户在另一台电脑测试泰坦军团 M27E6V-3D：向左移动不闪，向右移动闪动。没有在开发机控制游戏或显示器。本轮没有修改程序二进制，也没有制作新的修复包。

## 用户日志

输入附件：`C:/Users/PureDark/.codex/attachments/2024531a-da78-4b2e-80f0-5d153a0b31b9/已粘贴的文本.txt`。

- 02:37:53–02:38:21 未获得眼位；后续确实发生过较长跟踪丢失，包括 02:39:03–02:39:22。
- 02:39:28–02:40:13 的连续统计窗口均 held=0、default=0；tracked 每 5 秒约 1148–1160 次，changed 约 369–453 次（观察坐标变化约 74–91 Hz，不是已验证的传感器帧率）。此段没有 lens OFF，02:40:15 才关闭。
- 稳定跟踪段 max_step_mm 为 1.44–6.35 mm。此前 306 mm 的最大跳变跨越了长时间丢失，不能当作每帧抖动或运动速度。
- 不能将闪动归因于所有时间段都在回退，也不能仅凭五秒汇总值判断实际端到端延迟、屏幕扫描或与用户所述左右方向逐帧对应。
- runtime 的 context 创建行说明 head pose streams/late latching 未实现，本身不是本次错误报告。

## 无头 IDA 复核：预测配置返回值被丢弃

模块 FaceTracking.exe，与此前已哈希原始输入一致；自有会话 `motion-exe`。模块与数据库验证见 `evidence/motion-exe-health.json`。

- `sub_140002060` 读取 `kl_param/predict_time`，返回 float；函数内部只做配置查找和字符串转数值，没有将值写入眼追全局或 SDK。
- 唯一代码调用在 `0x1400108F3`，调用后 `0x1400108F8` 立即准备 `show_img` 的默认值；没有消费预测返回值。之后按顺序读取 show_img、width、height、fps。
- 证据：`evidence/depth/tracking_exe_sub_140002060.json`、`evidence/motion-predict-config-asm.json`、`evidence/motion-predict-reader-xrefs.json`。
- 因此配置文件内 0.065 不能证明 65 ms 预测已启用。此前回复及 README 的相关暗示已纠正。此结论不等同于证明 SDK 内部完全没有历史滤波或预测。

SDK 会话 `motion-sdk` 验证见 `evidence/motion-sdk-health.json`。持续跟踪、几何处理完整输出存入 `evidence/motion-tracking-update-full.json`、`evidence/motion-triangulation-full.json`。后者存在历史窗口和 0.8/0.2 加权处理；未在本轮完整恢复其时间语义，不能直接当作用户方向相关闪动的根因。不得仅因字符串中没有 predict 就断言不存在预测。

## 下一步需要的区分

先区分分眼串扰/重影、整幅明暗闪烁或游戏帧撕裂，并确认头部停住后的表现。动态延迟与固定光学偏差可能产生方向不对称，但当前没有证据决定应调预测、翻转坐标或修改 MCU 标定。不要直接为一个方向加补偿，也不要继续盲目提高轮询频率。
