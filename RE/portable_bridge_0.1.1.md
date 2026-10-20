# TitanLeiaBridge 0.1.1：去除原厂主程序和 COM 注册依赖

2026-10-20。故障输入来自用户另一台电脑：0.1.0 检测到 DepthTransformerPlus.exe 后停止初始化；卸载主程序后，原眼追 COM 注册失效。此次仅修改本机工作区的转接器及私有打包副本，没有修改系统注册或原厂安装，没有启动真实眼追/摄像头/MCU。

## 实现

- DeviceProvider 不再查询 HKCR，也不再固定到 Program Files。主程序明确传入自身目录的 vendor。
- vendor/MCU_library.dll 读取当前屏的光学 6/6/8 和摄像头 9/9/10 参数。后者用 invariant locale、max_digits10 写入便携 eyetracking/boe_mcu_mat.ini；校验有限值、正焦距，使用同目录临时文件原子替换。任何读取/写入失败均停止初始化，不用安装包中的旧标定。
- 原眼追 EXE 的**私有副本**保留 asInvoker/SegmentHeap，嵌入 COM class/type library 重定向。因资源变化导致原签名不再有效，构建脚本移除该副本陈旧的 Authenticode certificate directory；不伪造签名。原始及打包后 SHA256 记录在 vendor/provenance.json。
- broker 从这个 EXE 的嵌入资源创建 activation context，在同一 worker 线程内保持到 COM 对象释放。CoCreateInstance 后核对实际 vtable 所属模块的文件身份，拒绝退回已安装的另一份 DLL。
- 子 EXE 使用其默认嵌入清单。父子映射同一物理 DLL，保留 eye_info PE 共享节连通。没有 regsvr32、注册表写入或原厂自启组件。
- 便携包包含依赖 DLL、三个眼追模型、摄像头配置与子进程 VC runtime；不包含其他屏的 boe_mcu_mat.ini、深度推理主应用、DI 图片、.NET 测试程序、安装/卸载脚本。
- 启动失败时主窗口日志直接输出 provider 的实际错误，替代原来笼统的“校准/眼位尚未就绪”。“后台已启动”文字也明确区分进程启动与设备就绪。

## 静态证据

原组件身份与此前分析一致。此次无头 IDA 的 portable_com / portable_tracker sessions 均经 server_health 确认模块、自动分析和反编译状态。

- COM Open `0x180006180` → helper `0x180008B80`：用自身模块路径寻找同目录 FaceTracking.exe，设同目录 CWD。证据 portable-com-open-method.json、portable-com-launch.json。
- FaceTracking 主函数 `0x1400100A0` 的 `0x140010B87` 附近标准 CoCreateInstance；之前 CoInitializeEx 使用 STA + DISABLE_OLE1DDE。证据 portable-tracker-main.json、portable-tracker-com-xrefs.json。
- 类型库实际五参数写入方法名是 **SetEyeInfo2，DISPID 4**，读方法 GetPosition，DISPID 6。此前报告使用 SetPosition 描述其语义；它不是可向 IDispatch 查询的真实名字。类型库枚举证据 portable-typelib.txt。
- 依赖列表见 portable_dependency_audit.md 和 evidence/depth/portable_pe_dependency_audit.json。

## 当前验证

PortableTrackingSmoke 已通过。测试进程用 RegOverridePredefKey 将 HKCR 临时映射到无 COM 类的现有只读键；只改变测试进程的注册表视图，不创建/删除/修改任何系统注册项。

父进程 MTA 使用实际 FaceTracking.exe 的嵌入清单；子测试进程具有相同嵌入清单，使用 STA、原接口 IID、默认加载器激活。两端确认实际加载同一私有 DLL，子写入合成 xyz，父 GetPosition 读回一致。未调用 Open、StartCamera、MCU 或真实 FaceTracking.exe。摄像头校准序列化保留精度，并拒绝 NaN。

静态核对随包 DLL 间的 1,012 个导入符号，均有匹配导出；包不含设备标定。7 组本项目 EXE/DLL/PDB GUID/age 匹配。7 个 Leia runtime DLL 与 0.1.0 完全同 hash，两个 mod 二进制的导入及 hash 仍匹配。

本机旧转接器仍在运行，因此没有重新执行会争用全局 broker mapping 的旧 ABI/Broker tests，也没有停止用户进程。保留之前四项 baseline 测试证据；本轮新增 portable test 日志独立保存，不冒充重新完成全部硬件或游戏验收。

## 尚需实机确认

用户另一台电脑的摄像头、串口、校准 getter、眼追采样和裸眼效果。Windows 必须仍能枚举屏幕 USB 串口及摄像头；便携应用不安装操作系统驱动。头部跟随限制与 0.1.0 相同。原厂 helper 的路径处理仍有 MAX_PATH 限制，因此推荐短、可写的解压目录。
