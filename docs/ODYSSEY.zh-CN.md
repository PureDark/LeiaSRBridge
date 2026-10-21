# Odyssey 3D Hub 测试版

适用于本机已分析的 Hub 1.5.1。桥接器新增“Odyssey 兼容启动”，同时接入 Hub 的 3D 界面和游戏输出所用的 LeiaSR 接口。

1. 完全退出旧桥接器、Hub 界面及后台 `Odyssey3DHubService.exe`，解压 0.5.0 到新目录。需要保留调节时复制旧 `settings.ini`。
2. 将泰坦军团显示器设为 Windows 主显示器，使用原生分辨率、横屏和 SDR。保持 DepthTransformerPlus 关闭。
3. 启动 `TitanLeiaBridge.exe`，等设备和校准就绪，点击 **Odyssey 兼容启动**。默认读取 `C:\Program Files\Odyssey 3D Hub`。此入口不需要启用系统“免列表接入”。
4. 先检查 Hub 首页的 3D，再从 Hub 启动一个支持的 DX11/DX12 游戏。全局 +8.0 mm / 40 ms 参数及保存功能继续生效；没有人脸时仍可使用默认眼位。

请使用桥接器里的兼容启动按钮。直接打开原厂 Hub 快捷方式或它的自动启动项不会自动安装本版进程 hook。已运行的原厂 Hub 也不会被强行替换。首次使用 Hub 所需的原厂安装、协议确认和游戏配置仍由 Hub 自身处理。

本版拦截显示器识别、三星设备代理查询和 LeiaSR 库路径，作用限于兼容进程；退出 Hub 即结束。没有改系统 EDID、MCU 标定或 Hub 的安装文件。后台服务的 3D 开关请求不直接占用镜片，实际渲染客户端提交画面后才请求 3D。

已在本机用模拟设备验证实际 Hub 通过检测，并持续调用桥接交织；已验证旧版 DX11/DX12 接口与游戏模块加载顺序。尚未在泰坦军团屏上验收 Hub 或实际游戏的最终裸眼 3D。此次不是“所有 Hub 游戏已测试通过”的发布。

如失败，请提供具体游戏名、使用的图形接口，以及包内 `logs/TitanLeiaBridge.log`、`logs/OdysseyHook.log`、对应 `logs/runtime-<PID>.log`。Hub 自身日志通常位于 `%USERPROFILE%\AppData\LocalLow\Samsung\Odyssey3DHubUI\Service.log` 和 `Player.log`。
