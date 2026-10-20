# ModexLite 启动器原生桥分析

## 结论

本次三个原生模块中，`game_monitor_plugin.dll` 负责进程状态监控，`modex_lite.exe` 主要是 Flutter runner；没有找到它们直接向游戏注入 Stereo SDK 或切换显示器 3D 的实现。`media_transpose2to3_plugin.dll` 确实调用 `AiSetereoKit.dll`，并提供 **D3D11 device/context + 输入 SRV + 输出 RTV** 的原生 GPU 图像处理入口。这说明私有引擎可以由本机原生代码驱动，但此处观察到的是单图 2D→3D 媒体转换，不能据此宣称其接口已经能接受 LeiaSR mod 的左右眼图像并完成面板编织。

此报告只说明已分析模块的静态行为；实际游戏启动链仍可能位于 `data/app.so` 中。未启动软件、游戏或设备，未调用目标 DLL。

## 分析对象与方法

- `RE/inputs/game_monitor_plugin.dll`，IDA imagebase `0x180000000`。
- `RE/inputs/media_transpose2to3_plugin.dll`，IDA imagebase `0x180000000`。
- `RE/inputs/modex_lite.exe`，IDA imagebase `0x140000000`。

使用本机 IDA 9.4 MCP 的 `force_headless` session `launcher`，每次打开后用 `server_health` 确认模块、autoanalysis 和 Hex-Rays 状态。三个数据库均已保存并关闭。下述地址为各模块 VA，不可跨模块混用。

原文件与分析副本的 SHA-256 三组全部相同，记录在 `evidence/launcher_input_hashes.json`。

## game_monitor_plugin.dll

Flutter channel 为 `game_monitor` / `game_monitor/event`。方法分发函数 `sub_180008440` 支持：

| 方法 | 实际行为证据 |
|---|---|
| `startGameMonitor` | `0x180008710` 调用 `sub_18000AC00` 启动监控；方法名在优化后的汇编中以两个立即数比较。 |
| `stopGameMonitor` | 清理被监控游戏表、下发停止标志并 join 监控线程；分支从 `0x18000875B` 开始。 |
| `addGame` | `0x1800088DF` 调用 `sub_180006600` 添加监控目标。 |
| `removeGame` / `removeAllGames` | 从容器移除目标；为空时停止监控。 |
| `getAllStatus` | `0x180008BCA` 调用 `sub_180007D20` 查询状态并返回 Flutter map。 |

`sub_1800079F0` 在 `0x180007A19` 调用 `CreateToolhelp32Snapshot(2,0)`，随后 `Process32FirstW` / `Process32NextW` 遍历，`_wcsicmp(pe.szExeFile, requestedName)` 匹配后返回 PID。导入表未出现 `AiSetereoKit` / `StereoCore`、`CreateProcess`、`VirtualAllocEx`、`WriteProcessMemory`、`CreateRemoteThread` 或动态加载 API。

证据：`evidence/launcher_game_dispatch.json`（含汇编）、`launcher_game_findpid.json`、`launcher_game_imports.json`。

## media_transpose2to3_plugin.dll

静态导入 `AiSetereoKit` 的 `LoadStereoEngine`、`CreateStereoRenderer`、`UnloadStereoEngine`。自身导出 `LoadMediaTransposeEngine`、`CreateMediaTranspose`、`UnloadMediaTransposeEngine`，以及 Flutter 注册函数。

### 引擎与对象生命周期

- `LoadMediaTransposeEngine` 位于 `0x180006D90`，参数是 `const char* cacheDir`。`0x180006E22` 将该参数原样传给 `LoadStereoEngine(cacheDir)`，之后设置本地已加载标志。包装层没有将被调函数返回值作为加载成功条件，这里的 ready 标志不能证明底层引擎实际就绪。
- `CreateMediaTranspose` 位于 `0x180006B70`，等待已加载标志，成功路径在 `0x180006D33` 调用 `CreateStereoRenderer()`。返回的私有 renderer 指针保存到包装对象 `+8`；包装对象 `+0` 是 `gear::MediaTranspose` vtable。
- 包装 vtable 位于 `0x18001AA18`。析构函数本身不代替独立的释放函数；调用方显式先释放 renderer 再析构包装对象。

### 已观察到的 renderer ABI

| 包装对象 vtable 槽位 | 包装函数 | 私有 renderer vtable 偏移 | 从调用点可确定的参数/行为 |
|---|---|---|---|
| `+0` | `0x1800069C0` | 无 | 包装对象 deleting destructor。 |
| `+8` | `0x1800069F0` | `+8` | `(this, ID3D11Device*, ID3D11DeviceContext*)`。 |
| `+16` | `0x180006A30` | `+16` | `(this, uint32 width, uint32 height)`。 |
| `+24` | `0x180006A70` | `+32` | `(this, ID3D11ShaderResourceView* input, ID3D11RenderTargetView* output, 1)`；第四参数由包装函数固定设置。 |
| `+32` | `0x180006A50` | `+40` | 转发，当前调用链未确定语义。 |
| `+40` | `0x180006A10` | `+48` | 清零包装对象中的 renderer 指针，调用底层释放方法。 |

这些是对实际调用的 ABI 还原，不是厂商头文件声明，返回类型尚未完整确定。特别注意：Hex-Rays 对 forwarding thunk 会只显示 `this`，不能据此认为其没有额外参数。`0x1800069F0` 汇编只有读取 `this+8`、读 vtable、`jmp [rax+8]`，因此 `RDX`/`R8` 保持调用方输入。`0x180006A70` 明确执行 `mov r9d,1; jmp [rax+20h]`。

`sub_18000CE20` 是清晰的实际使用点：

1. 创建输入 `ID3D11Texture2D`、输入 `ID3D11ShaderResourceView`、输出 `ID3D11RenderTargetView`。
2. `0x18000CFCF` 向包装方法 `+8` 传入对象保存的 device 和 immediate context。
3. `0x18000CFE3` 向包装方法 `+16` 传入宽高。
4. 设置输出 RTV 和 viewport。
5. `0x18000D055` 向包装方法 `+24` 传入输入 SRV 与输出 RTV；它再调用底层 renderer `+32`，固定模式参数为 `1`。
6. `0x18000D05F` 调用 context `Flush`，随后 `0x18000D06B` 释放 renderer，`0x18000D079` 析构包装对象。

另一个调用点 `sub_18000B940` 执行同样的 device/context、宽高、单输入 SRV 与输出 RTV 调用，再将输出复制到 staging texture、Map 并写入图像文件。该分支支持“媒体 2D→3D 转换”的判断，而非显示器控制或游戏注入。

证据：`evidence/launcher_media_load.json`、`launcher_media_create.json`、`launcher_media_0x18000ce20.json`、`launcher_media_0x18000b940.json`、`launcher_media_render_asm.json`、`launcher_media_render_forward_asm.json`、`launcher_media_forward_asm.json`、`launcher_media_release.json`。

### 对 LeiaSR 转接的意义

可复用的是引擎加载与 D3D11 renderer 的接入方式，尤其 native device/context 和 GPU view 参数。尚须在 `AiSetereoKit`/实际载荷中确认：是否有接收左右眼两张纹理或 SBS 图像的原生输出模式、面板校准/编织、追踪坐标、3D 开关等接口。不能把此处固定的 `mode=1` 直接猜成 SBS、weave 或显示器 3D 模式。

## modex_lite.exe

EXE 包含 `\flutter_assets`、`\icudtl.dat`、`\app.so` 路径，静态导入多个 Flutter 插件注册入口。没有 `AiSetereoKit` / `StereoCore` 直接导入，也没有 `CreateProcess` 或上述远程进程注入 API 导入。

所有 `LoadLibraryA`、`LoadLibraryExA`、`GetProcAddress` 的交叉引用均已追踪：

| 函数 | 已确认的动态加载用途 |
|---|---|
| `0x1400027A0` | `client_extension.dll` → `CreateBuglyAgent`，初始化 Bugly crash reporter；配置中版本为 `1.6.1.76`。 |
| `0x140001020` | `User32.dll` → `GetDpiForWindow` / `GetSystemMetricsForDpi`，并安装当前线程 window hook。 |
| `0x140007780` | `User32.dll` → `EnableNonClientDpiScaling`，窗口创建/DPI 处理。 |

因此不能从该 EXE 的动态加载 API 推断存在 SDK 注入；这些使用点已有其他明确用途。业务启动与私有显示接口仍应在 Dart AOT `data/app.so`、其 FFI 绑定或其他私有模块寻找。

证据：`evidence/launcher_exe_imports.json`、`launcher_exe_load_xrefs.json`、`launcher_exe_main_init.json`、`launcher_exe_0x140001020.json`、`launcher_exe_0x140007780.json`。
