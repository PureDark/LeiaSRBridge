# 构建与目录约定

项目根目录现为 `F:\开发项目\TitanLeiaBridge`，源码、研究证据、设计稿和 Agent 记录均在此目录。CMake 只允许源码外构建，默认输出到 `build/`。运行时行为和版本仍为 0.4.0。

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

- `design/titan-bridge-concept.html`：独立 HTML 交互设计稿，可直接用浏览器打开；PNG 是设计预览。
- `RE/`：原有逆向报告、证据、工具和探针源码；历史记录中的旧路径不改写。
- `docs/agent-history/`：主会话与 13 个子 Agent 的 JSONL 快照、文本版及附件，纳入 Git。
- `docs/migration/inventory.json`：迁移前的 5,012 个文件及哈希，已在目标目录逐个核验。随后旧 build 移到归档目录，少量构建脚本已按新布局修改。

Git 忽略构建目录、发行包、依赖、安装包、分析二进制和 IDA 数据库，但保留源码中使用的原厂 shader 字节码 `.dxbc`、研究日志和设计截图。
