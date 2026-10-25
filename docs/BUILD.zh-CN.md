# 构建与目录约定

项目名为 LeiaSRBridge，仓库根目录为 `F:\开发项目\LeiaSRBridge`，源码、研究证据、设计稿和 Agent 记录均在此目录。CMake 只允许源码外构建，默认输出到 `build/`。当前版本为 0.9.2，包含 Opus 5.5 桌面界面和 Odyssey Hub 的进程内兼容入口。

## 项目目录与局域网共享

- 本机目录：F:\开发项目\LeiaSRBridge。
- 局域网共享：\\PUREDARK-GAMING\LeiaSRBridge；也可通过本机当前局域网地址 \\192.168.3.42\LeiaSRBridge 访问。
- 共享沿用原 PureDark、TitanBridgeShare 账户及读写权限，账户密码未改动。另一台电脑上的项目目录、映射盘目标和 Agent 工作目录需改为新共享地址。
- 旧目录 F:\开发项目\TitanLeiaBridge 和旧共享已移除，不保留别名。
- 2026-10-25 目录迁移时，将原 build 归档到 artifacts/build-before-directory-rename-20261025，并在新路径重新配置和构建。旧发行包和历史研究记录保留原证据路径，不作为当前构建配置使用。

## 配置、编译、测试

需要 CMake 3.25+（当前预设使用 Visual Studio 2026，需要能识别该生成器的 CMake 版本）、Visual Studio 2026 C++ x64 工具链、Windows SDK 和 Python 3。其他 MSVC 版本可在不入库的 `CMakeUserPresets.json` 中继承预设并更换生成器。

在项目根目录运行：

```powershell
cmake --preset windows-x64
cmake --build --preset release --parallel
ctest --preset release
```

也可运行 `./build.ps1` 完成以上三个步骤。`-BuildDirectory` 支持单独输出目录，`-Preset` 支持本机预设。测试总日志保存在 `build/full-tracking-tests.log`，详细日志在 `build/Testing/Temporary/LastTest.log`。可执行文件在 `build/package/`，PDB 在 `build/symbols/`。

CMake 自动查找 Python 和 Windows SDK 的 `mt.exe`，也可通过缓存变量 `Python3_EXECUTABLE`、`PORTABLE_PYTHON`、`PORTABLE_MT` 显式设置路径。

## 界面构建

正式界面模板在 `src/app/ui/index.html.in`，CMake 填入版本后通过 Windows 资源嵌入 EXE。`WebUi.cpp` 静态链接微软 WebView2 loader，使用系统安装的 WebView2 Runtime。用户不需要 Node.js、Python 或 HTTP 服务；UI 缓存放在 `%LOCALAPPDATA%/TitanLeiaBridge/WebView2`（保留旧缓存目录以兼容升级），不写入项目和发布包。

首次克隆运行 `./tools/setup_webview2.ps1`，下载并校验官方 SDK 1.0.4258.31，保存在忽略入库的 `dependencies/WebView2/sdk`。本机已有该依赖。可通过 `WEBVIEW2_SDK` 指定解压位置。nlohmann/json 3.12.0 单头文件及 MIT 许可证随源码保存。

`tools/generate_app_icon.py` 根据设计稿双色圆形几何生成透明 SVG/PNG/ICO，使用 Python 标准库。图标作为源资源入库，不属于编译产物。

`UiDesign` 检查原稿 CSS 逐字保留与图标 alpha；`WebUi` 在隔离目录用 `--mock --ui-test` 验证实际 WebView2 DOM、真实 IPC 调参和保存，截图位于 `build/verification-ui`。它不打开 MCU、摄像头或游戏。`AppControls` 验证全局快捷键及跨进程设置恢复。模拟模式的本机测试命令不会在正常设备模式启用。

动画边界可另外运行 `node tools/test_viz_motion.cjs` 验证：直接执行正式页面中的动画逻辑，覆盖 147 组极端位置、显示范围及旋转跨 ±180° 的插值。Node.js 只用于该开发检查，程序本身不需要。

启动功能：`Startup.cpp` 管理当前用户 Run 项；`StartupSmoke` 在隔离 HKCU 路径验证 Unicode/空格路径、跨进程保留、版本迁移、取消及无关项保留。依据 [Windows Run 文档](https://learn.microsoft.com/en-us/windows/win32/setupapi/run-and-runonce-registry-keys)，命令长度限制为 260 个字符，超长路径会明确报错。测试不写真实 Run 项。

`BrokerStartupSmoke` 将真实 broker 与测试专用 provider 链接，验证首次 USB 未就绪、释放资源、延迟成功及退出打断等待，不加载厂商驱动。`WebUi` 检查自启动开关和模拟模式不写系统设置，`AppControls` 检查重复后台启动退出。

## 本地依赖

迁移时已将必需依赖保存到项目内；此电脑重新构建不需要旧 ModexLite 目录，也不需要安装 DepthTransformerPlus。

- `dependencies/LeiaSDK/`：未修改的 SDK 1.34.10 x64（包含 OpenCV 头文件和导入库）。
- `dependencies/opencv_world343.dll`：与 SDK 相配的运行库。
- `dependencies/vendor/`：已准备好便携 COM 清单的官方眼追运行库、模型和 CRT；不包含桥接生成的 `BOE_3DET_SDK.dll`，由 CMake 重新编译。
- `RE/inputs/depth/tracking/BOE_3DET_SDK.dll`、`RE/inputs/depth/interlaceSDK.dll`：原始分析输入，CMake 校验其 SHA-256 后复制。

这些第三方依赖和原始二进制只在本地保留，不入 Git。Git 克隆后须另外复制上述依赖；`docs/migration/dependencies.json` 记录当前依赖的 SHA-256。CMake 可用 `LEIA_SDK_ROOT`、`OPENCV_RUNTIME`、`VENDOR_RUNTIME_ROOT` 指定其他依赖位置。

若需重新从厂商安装目录准备便携组件，显式指定工具路径，输出到独立的 `dependencies/vendor/`：

```powershell
python -B tools/prepare_vendor.py '<厂商目录>' dependencies/vendor --mt '<Windows SDK mt.exe>' --crt '<x64 CRT 目录>'
```

## 打包

`package.ps1` 统一调用 `tools/package_release.py`，版本从 CMakeLists.txt 读取，要求本次构建已通过测试；现有同版本发行包不会被覆盖。

```powershell
./package.ps1 -CrtDirectory '<x64 CRT 目录>' -Clients @('<FallenFlower3D DLL>', '<YakuzaVRMod DLL>')
```

`dist/` 保留历次发行包，`artifacts/build-before-move/` 保留迁移前的完整构建目录（其中 CMakeCache 包含历史路径，只作归档）。`installers/` 保留相关安装包。以上均被 Git 忽略。

## 开发记录与设计

- `design/titan-bridge-opus5.5.html`：本次采用的界面设计稿；原文件和 PNG 均保留。`titan-bridge-concept.html` 是早期方案。
- `RE/`：原有逆向报告、证据、工具和探针源码；历史记录中的旧路径不改写。
- `docs/agent-history/`：主会话与 13 个子 Agent 的 JSONL 快照、文本版及附件，纳入 Git。
- `docs/migration/inventory.json`：迁移前的 5,012 个文件及哈希，已在目标目录逐个核验。随后旧 build 移到归档目录，少量构建脚本已按新布局修改。

Git 忽略构建目录、发行包、依赖、安装包、分析二进制和 IDA 数据库，但保留源码中使用的原厂 shader 字节码 `.dxbc`、研究日志和设计截图。


## 原生 LeiaSR 预览验证

NativePreview.cpp 使用已安装官方 C API 的 Head/Eye listener，不链接桥接导入库，不创建 Broker。原厂路径从 64 位注册表读取；验证所有必需导出后才创建不请求镜片的 context。退出时先解除回调，再删除 listener/context 和卸载模块。接口出错时留在预览错误状态，不自动转入 BOE 模式。

tests/NativePreviewSmoke.js 是需要真实设备的只读 UI 检查，不属于无硬件 CTest。显式执行 LeiaSRBridge.exe --native-preview-test --duration-ms 10000，生成 logs/native-preview-test.json 和 logs/native-preview.png；检查原生模式提示、桥接控件隐藏、pose 包更新、镜像变换及原厂推荐视图尺寸。0.8.2 在本机原厂服务实测返回 1920 × 1080 px；主屏首选模式为 3840 × 2160 px。0.8.3 的 Bridge API 将原生宽高各除以 2，推荐单眼尺寸为 1920 × 1080 px；均基于读取结果而非硬编码。原厂 Displays 模块和 context 一同保留，context 销毁后再卸载；查询缺失或返回无效值只显示暂不可用，不中断追踪。测试入口不保存运行模式，也不调节设备或调用游戏操作。不要将有限次实机通过解释为任意 LeiaSR 版本兼容。

## 0.9.1 名称与兼容边界

- CMake 项目和主目标：LeiaSRBridge；独立测试目标：LeiaSRBridgeTest。
- 不构建或打包旧名启动器。正式入口仅 LeiaSRBridge.exe 和 LeiaSRBridgeTest.exe；打包脚本排除旧构建目录中残留的旧名 EXE。
- settings.ini / games.txt 的位置与字段不变。主日志为 logs/LeiaSRBridge.log。
- 自启动读取三代 Run 条目；同目录旧项迁移到 LeiaSRBridge，并复制 StartupApproved 状态后移除旧 Run 项。已有新名称登记优先；不会自动迁移另一安装目录或打开原本未登记的自启动。
- 免列表接入保持原 HKLM 所有权键、事务恢复字段和 runtime 目录布局。TitanLeiaRuntime DLL 名、转发导出、IPC、窗口类、厂商组件私有名称属于兼容接口，不全局替换。
- RenameUpgrade 在临时目录内验证新 EXE 读取已有配置、中文空格路径、后台参数、单实例、已有补偿/预测/Hub/游戏列表。Startup 与 GlobalRuntime 在隔离 HKCU 键内验证旧登记，不修改真实系统配置。
- 历史 RE、设计原稿和 docs/agent-history 不回写名称，以保持证据原样。当前使用和开发说明采用新产品名。

## 更新器

Update.cpp 将资源中内嵌的 update.ps1 写入当前用户的独立临时目录，通过系统 Windows PowerShell 5.1 隐藏执行；主线程只轮询状态，不阻塞追踪。使用 GitHub latest Release API，接受 vMAJOR.MINOR.PATCH 正式版本与匹配的 LeiaSRBridge-VERSION-win64.zip，要求 Release asset 的 sha256 digest。参考：[GitHub Release API](https://docs.github.com/en/rest/releases/releases)。

更新包须为 package_release.py 生成的根目录 ZIP，包含 manifest.json；不接受源码压缩包、预览版、降级、未知下载地址或缺少完整性元数据的包。所有 ZIP 路径、文件数量和大小、清单及每个文件哈希均验证。Apply 等待主程序退出（PID + 创建时间），重新验证 ZIP，在替换前检查目标文件占用。原目录临时备份用于逐文件原子替换及失败回滚。失败会提示并保留诊断；若回滚不完整，不自动重启。正常成功会清理自己的临时下载目录。

Updater 测试在隔离目录验证版本比较、Release 资产约束、ZIP 遍历及哈希拒绝、配置保留、文件锁和中途覆盖失败回滚，不接触真实安装、MCU、摄像头或游戏。WebUi 同时检查入口与模拟模式禁止联网/安装。
