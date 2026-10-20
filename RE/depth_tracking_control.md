# DepthTransformerPlus 眼追与 3D 控制静态接口

分析日期：2026-10-20。仅复制、哈希、无头 IDA、PE 检查及读取 COM 注册项；没有运行原厂程序、调用 DLL 导出、发送串口或 IPC 命令。使用 `http://127.0.0.1:18475/mcp`，独占自有 session `tracking`，每次切换前保存关闭；最终已关闭。

## 结论

已有足够接口实现“原厂校准 + 眼追 xyz + 原厂 weave + 3D 模式生命周期”的适配原型。`MCU_library.dll` 可读本机校准并控制显示状态；已注册的眼追 COM 能提供原厂生产者的 xyz。但 COM 快照没有时间戳、帧号、真实两眼位置或旋转，因此这条最低成本路线不能声称完整实现 Leia `Head/HeadPose`。直接调用 `BOE_3DET_SDK.dll` 能保留更多输出和时间字段，需要继续恢复坐标语义及建立自己的摄像头采集。不能据此承诺所有 LeiaSR mod 通用兼容。

关键版本差异：当前 `HKCR\CLSID\{D98A272F-DE1C-4E73-A1D0-A93A8F068B4B}\InprocServer32` 指向 `C:\Program Files (x86)\DepthTransformerPlus\eyetracking\FaceTrackingManagerService.dll`，`ThreadingModel=Neutral`。根目录同名 DLL 是不同旧版，不能混用。记录：`evidence/depth/tracking_registered_com.json`。

## 样本身份

SHA256 完整记录：`evidence/depth/tracking_input_hashes.json`、`tracking_root_hashes.json`。

| 样本 | 字节 | SHA256 |
|---|---:|---|
| MCU_library.dll | 443392 | 0A2E51C617B585E53E0733135CCEDE7D5C3D3F30D58BEA23313026641E050858 |
| 根 FaceTrackingManagerService.dll | 79360 | 24D677EA070FCA322B025FA91823EA1EAA040729DB9EFED5DE2F0FE7855A6118 |
| eyetracking/FaceTrackingManagerService.dll | 98304 | 3132DD6FE285772B327B4AAB99DE5A05C5CFE4376432E9DEFDC2680C465E4604 |
| eyetracking/FaceTracking.exe | 347176 | 85254AD5838FC6D61C68A9EEA1B216D578103239D9432CEF0BE19E0A1CBC37C4 |
| eyetracking/BOE_3DET_SDK.dll | 1343488 | 752CB438A6D98848F45870C2502EA4FFC886C8DAF14C1EE411AF77518CD328B5 |

## MCU：初始化、校准、模式与所有权

导出 ABI 由原生导出与主程序 P/Invoke 双重支持；托管源在 `evidence/depth/managed_decompiled/DepthTransformerPlus/AstraDepth3D/BOEStereoRender/MCUSdk.cs:11` 起，全部 Cdecl。SDK 是内部 singleton，无调用者传入 handle：

```cpp
int BOE_MCU_initSDK(const char* logPath, int logLevel); // 主程序 ./logs/, 2
int BOE_MCU_openMCU();
int BOE_MCU_set3DMode(bool enabled);
bool BOE_MCU_get3DMode();
void BOE_MCU_closeMCU();
void BOE_MCU_releaseSDK();
```

这是签名摘要，非已验证可编译头文件；Windows x64 的 bool 参数原生使用低字节，主程序以 MarshalAs(UnmanagedType.Bool) 传入。具体导出 `0x180035680`，内部 `0x1800151F0`；汇编见 `tracking_mcu_setmode_asm.json`。

1. `BOE_MCU_initSDK` (`0x180035430`) -> `sub_180011C50` 建立读写工作线程，寻找 `VID_28E9 PID_018A` 对应 COM 口。即使没有发现设备也返回 0，不能将 init 成功当硬件可用。
2. `BOE_MCU_openMCU` (`0x1800355E0`) -> `sub_180012010` -> `sub_180033650`。用 `CreateFileA("\\\\.\\"+port, access=0xC0000000, share=0, OPEN_EXISTING)` 打开，115200、8 data bits、no parity、1 stop bit。**独占串口**是明确的资源所有权约束，不能让原厂主程序与新转接层各自打开同一口。汇编/反编译：`tracking_mcu_serialopen_asm.json`、`tracking_mcu_sub_180033650.json`。
3. open 建立读写状态、查询 MCUVersion，按 `Version2=` 选择封包协议。不能绕过这一协商随意硬编码字节包。未找到串口/打开失败有非零返回。
4. `set3DMode(true)` 只写 manager+16 期望标志；`SendThreadProc` (`sub_180014B30`) 的 `0x180014F7F` 起在标志为真时循环发送内部 command 3，`sub_180016740` case 3 经 `sub_180017210` 包装后发串口。它并非只发一个“开关”包。false 清标志并排队 command 1（日志 `send close3D cmd`）。`tracking_mcu_sub_180014B30.json`、`tracking_mcu_sub_180016740.json`、`tracking_mcu_sub_180017210.json`。
5. `get3DMode` (`0x1800356E0`) 经 `0x180035290` 只读 manager+16。**这是软件期望状态，不是屏幕已打开 3D 的设备回执**。IDA 对后者误识别为 CRT 析构名字，已按 caller 汇编和实际函数体验证。见 `tracking_mcu_getmode_asm.json`、`tracking_mcu_0x180035290.json`。
6. `closeMCU` (`0x180035630` -> `0x1800126D0`) 清队列、关串口、禁工作线程；没有看到它先发送关3D。因此适配生命周期应遵循主程序的 false -> close -> release，而不是只卸载 DLL。false 本身异步排队，适配实现还需设计排队完成/关闭顺序。`sub_180033AF0` 实际 CloseHandle。

校准读取均要求 open 状态，通过队列请求后等待对应 event，超时不应继续用未初始化数组。各 getter 所复制的 double 个数由 native loop 和 managed 数组相互验证：

| 导出 | double 个数 | 内部函数 | 意义证据 |
|---|---:|---|---|
| getDeviceParam | 6 | 0x180015480 | 主程序传给 weave，细分字段参见主分析 |
| getWholeMat | 6 | 0x180015E40 | 同上；不是完整两摄像头内参 |
| getRefractionDeviceParam | 8 | 0x180015620 | 折射/显示参数，主程序传给 weave |
| getLeftCameraMatParam | 9 | 0x180015960 | E1，左摄像头内参 |
| getRightCameraMatParam | 9 | 0x180015B00 | E2，右摄像头内参 |
| getStereoCameraDistParam | 10 | 0x180015CA0 | E3，摄像头间变换 |

证据文件 `tracking_mcu_sub_<address>.json`。主程序 `MainWindow.cs:644` 的 `UpdateMCUMat` 将9/9/10数组写入 `eyetracking/boe_mcu_mat.ini` 的 E1/E2/E3（649、664、679行）；这是从硬件读取当前标定值的明确通路，安装包里已有 ini 不能直接当每台设备通用标定。

## 当前注册眼追 COM

接口 IID `{E26EE08A-BCF1-49F8-B01F-0044D2897F28}`；CLSID 见前文。托管契约：`FaceTrackingManagerServiceLib/ISimpleFaceTracking.cs:12`，`Tracker/Facetracker.cs:29` 创建 COM。

```cpp
// COM HRESULT/IDispatch 方法摘要，保留正式 type library 中的 vtable 顺序
Open();                 // DispId 1
Close();                // DispId 5
GetPosition(float* x, float* y, float* z, float* z_l, float* z_r); // DispId 6
StartCamera();          // DispId 11
StopCamera();           // DispId 12
```

新版 vtable 位于 `0x18000F098`，已保存方法地址 `tracking_com2_vtable_methods.json`。Open `0x180006180` 调 `0x180008B80`，运行 DLL 同目录的 FaceTracking.exe，并明确设置工作目录。StartCamera `0x180006BC0` 调 helper 启动 `FaceTracking.exe 1` 后向 `\\.\pipe\YLFaceTrackingPipe` 发 `"1"`；StopCamera `0x180006C40` 发 `"0"`。管道函数 `0x180009450`。这里只分析代码，未调用。

GetPosition `0x1800067A0` 顺次复制 `eye_info` 段中的五个32位值，返回0；它不做采样、不等待新帧、不附时间戳，也不检查数据是否已经过期。SetPosition `0x1800062F0` 更新相同五值。`eye_info` 为 PE `IMAGE_SCN_MEM_SHARED|READ|WRITE` 节，`tracking_com_sections.txt` 明确显示；不是具名共享内存 API。不同磁盘 DLL 副本/不同版本可能形成不同共享 backing，应使用当前注册文件并维持一致身份，不能随意复制为代理 DLL 期待互通。五字段也未发现整包锁/sequence counter，读写并发时不能假设是原子快照。汇编 `tracking_com2_getposition_asm.json`。

根目录旧 DLL 的 Open `0x180006270` 使用 `Global\FaceTrackingExitEvent`、job object，并从自己的目录启动 FaceTracking.exe；Close `0x1800066F0` 先置 event、等待后可 TerminateProcess。它的 vtable 未提供与当前托管所调用相同的有效 StartCamera/StopCamera 实现。不能分析根目录旧版后把结果当成当前注册提供者。

## 眼追生产者与原生 SDK

`FaceTracking.exe` 导入 `BOE_3DET_SDK.dll` 的 initBoeEyeTrack_3D、setCameraMat_3D、doEyeTrack_3D、releaseBoeEyeTrack_3D 等，证据 `tracking_exe_imports.json`。生产线程 `0x14000F920`，函数日志名 CaptureAndDisplayFrameThread：

- `initBoeEyeTrack_3D("./models", "../logs", 3)`；返回0为失败，成功为1。
- `ReadBOEMcuMat` (`0x14000E9A0`) 解析 ini 的 E1/E2/E3 为 double[9]/[9]/[10]。
- `setCameraMat_3D(E1,E2,E3,0)`；调用 setFaceAreaTime / setEnableTrackArea / setHighConArea 后循环采集与 doEyeTrack。
- 正常退出时 releaseBoeEyeTrack_3D。SDK 自带 singleton 与互斥，init 还改进程优先级和 UnhandledExceptionFilter，release 也改优先级；将它直接塞入游戏进程会影响 host，独立 provider 进程更容易控制影响范围。

`0x14000D4D0` 通过 Media Foundation sample/buffer 获取 NV12 帧，用 Y 平面构造 OpenCV Mat，`doEyeTrack_3D(data, cols, rows, channels, result)`，再调用 COM vtable+80 的 SetPosition。反编译将该间接调用参数丢失，**以 `tracking_exe_doEye_asm.json` 的 `0x14000D6C6`—`0x14000D6FC` 为准**：x/y/z 来自 result 首三个 float，另外两个栈参数装相同常数，位模式为 `0x3f800000`（`tracking_exe_zlr_constant.json`）。因此 z_l/z_r 在此版本中不是两眼测得的深度，不能按字段名字推断 IPD 或左右眼坐标。

原始 SDK 比 COM 保留更多信息：`doEyeTrack_3D` (`0x18002A600`) 锁 singleton 后调用 `0x1800261B0`。输出至少168字节，在失败路径 memset 168，成功路径写：

| byte offset | 静态识别 |
|---:|---|
| 0,4,8 | COM 消费的 x/y/z float |
| 12..32 | 另两组三分量 float；来源为 `sub_1800272D0` 的a4/a5，精确眼别/坐标系未完全恢复 |
| 36..64 | 两相机检测矩形 int32 坐标 |
| 72 | uint64 时间，`sub_18002F010` 返回 `Xtime_get_ticks()/10000`；不能直接当 Leia 的单调 time/frameId |
| 80,81 | 两个跟踪/检测状态字节；具体枚举语义未恢复 |
| 84..120 与124..160 | 两组10个 int32 landmark 坐标 |

证据：`tracking_sdk_doEye_internal.json`、`tracking_sdk_result_asm.json`、`tracking_sdk_assign_result.json`。有效检测返回1；多种检测失败路径清零结果并写时间/状态，但未初始化/图像输入失败等路径不保证全量清零，所以客户端要自己清结果缓冲并检查返回值。

**单位不可混用旧代码**：当前 BasicCaptureAsync.GetPose (`.../CaptureSampleCore/BasicCaptureAsync.cs:619`) 原样取 COM xyz；同文件1025/1028行 near=400、far=1000，542行原样送 DoInterleave。其他 Safe/DXGIAsync 同为400/1000，强烈支持当前输出是毫米尺度。旧 BasicCaptureDXGI 则 near=0.3、far=0.6，而且其他遗留函数还会乘1000。这是并存旧路径，不能拿它证明当前输出为米。最终坐标轴正向、原点和屏幕平面偏移仍需与 weave 的数学公式及实机标定对齐；本报告不把推断写成已完成硬件验证。

安装配置 camera_info width=1280,height=480,fps=90，predict_time=0.065。这里只证明静态配置值，未验证实际采样率/延迟；当前 COM 路径也不能知道 frameId 或每次是否为新样本。boe_ET_Predict/Runtime_BOE_18NBSDK_ETSP 已保存哈希但未扩散分析，不能仅凭文件存在认定当前路径使用它们。

## Leia 映射建议与边界

| Leia 契约 | 已有可映射能力 | 尚需解决 |
|---|---|---|
| Head/HeadPose 位置 | COM xyz，或原始SDK前三float | 坐标系/显示平面偏移；不能沿用旧路径额外乘1000 |
| 左右眼/旋转 | 原始SDK还有两组三分量可进一步恢复 | COM 不提供；不能把 z_l/z_r 当眼位，也不能声称完整6DoF |
| 时间/frameId/丢失 | SDK有时间值与返回成功/失败 | COM会保留旧值且无时间；需有生产者采样时钟、seq、新鲜度/丢失策略 |
| lens hint 开关/焦点 | MCU set3DMode + 长期worker | 全局独占所有权、排队关闭、焦点切换；get3DMode不是硬件确认 |
| 每台屏的光学校准 | MCU 6/6/8 -> 原厂weave | 不可用包内默认值硬套别的屏 |

最小可行原型宜由一个 provider 拥有摄像头和 MCU，读取本机校准，用原厂 weave 处理 mod 双眼图像，并把位置提供给 Leia 兼容接口；先限定已有 mod 实际使用的 API。若复用 COM，只能诚实声明位置跟踪子集；若追求 HeadPose/Head 完整语义，应扩展原生 SDK provider，并在静态恢复后做受控实机校验。该报告未制作或运行转接层。
