# LeiaSR 客户端兼容契约（2026-10-20，只读调查）

## 结论

对本机 `FallenFlower3D` 与 `YakuzaVRMod` 当前代码，做兼容层有清楚的客户端切入点：在现有 mod 生成左右眼 SBS 纹理之后，替代 Leia 的 weave、追踪、透镜控制及 context 生命周期。它们共用静态链接的 SR-lib 和 `SR-SDK-1.34.10`，不是通过一个统一 `LeiaSR.dll` 调用。仅替换 `SimulatedRealityDirectX.dll` 不足以在没有 Leia runtime/service 的机器上工作；完整离开 Leia 服务至少需要 Core、DirectX、Displays、FaceTrackers 四个兼容入口模块，背后可共用一套桥接实现。

“所有 LeiaSR mod”目前不能承诺：这两份代码只验证了 SDK 1.34.10 Windows x64 的一个 C++ ABI 子集。SDK 同时存在新旧 C++ weaver、DX9/DX11/DX12/OpenGL、C API、32 位 DLL 名称与可能不同版本的类布局。应先定义一个可验证的版本/架构/API 兼容范围，再扩展。

本笔记只确认客户端接口条件；私有显示器的原生织图、校准、追踪与 3D 切换是否可复用，必须由启动器逆向证据回答。本轮未修改两个 mod 项目，未启动游戏或控制显示器。

## 实际代码与产物证据

- `F:\GithubMods\FallenFlower3D\CMakeLists.txt:5-22`：依赖 `F:\GithubMods\UU3D\dependencies\submodules\SR-lib`，链接 `SRLib::SR` 并调用 `srlib_apply_delayload`。
- `F:\GithubMods\Yakuza\YakuzaVRMod\cmake\CmakeList.cmake:35-42`：使用同一目录、同样静态 wrapper 与 delay-load 设置。
- `F:\GithubMods\UU3D\dependencies\submodules\SR-lib\CMakeLists.txt:151-152`：`SRLib_SR` 是 `STATIC`。`SRInterfaceDX12`、`SRGetHead` 等 wrapper 编入宿主，不是可以直接换掉的独立 SR-lib DLL。
- 下文 `SDK/...` 均指 `F:\GithubMods\UU3D\dependencies\submodules\SR-lib\SR-SDK-1.34.10\x64\include\...`；`SR.cpp` 均指上述 SR-lib 目录的 `SR.cpp`。

通过 `C:\Program Files\LLVM\bin\llvm-readobj.exe --coff-imports` 只读检查当前构建产物（不是游戏部署目录）：

| 产物 | SHA-256 | 实际 Leia delay-import |
|---|---|---|
| `F:\GithubMods\FallenFlower3D\build\Release\FallenFlower3D.Native.dll` | `FFCB4959B045CBB9F40BF9857E62B33A427FBF1C905D34585EEAD360346F3556` | Core 3、FaceTrackers 7、Displays 1、DirectX 3、OpenGL 1，另有 OpenCV 1 |
| `F:\GithubMods\Yakuza\YakuzaVRMod\build\Release\YakuzaVRMod.asi` | `AA6CF59527F4A4729360A66C7DEC03B062A832C495D671EA57C4CAC5EF19A370` | Core 3、FaceTrackers 7、Displays 1、DirectX 1 |

导入表范围大于实际路径：FallenFlower 的 wrapper 保留了 DX9/DX11/GL 导入，但当前生产调用路径创建的是 DX12 weaver。delay-import 不代表未调用的模块一定会被加载。不能把它的 DX11 Unity 输入误认为 DX11 Leia weaver；`native\D3D11Bridge.cpp:40-63` 有 D3D12 device/queue 和 D3D11/D3D12 shared textures，最终 `native\GpuBackend.cpp:289-292` 调用 `CreateSRInterfaceDX12`。

## 最小导出集合与对象 ABI

两个当前 mod 实际 DX12、追踪及透镜路径需要以下四个模块。列出的 decorated names 已在两个构建产物中核对；括号数字为导入 hint，不应当成按 ordinal 导入。

### SimulatedRealityCore.dll

```text
?create@SRContext@SR@@SAPEAV12@W4NetworkMode@12@@Z
?initialize@SRContext@SR@@QEAAXXZ
?deleteSRContext@SRContext@SR@@SAXPEAV12@@Z
```

`SR.cpp:395-437` 先 `LoadLibraryW` backend 和 Core，再 `SR::SRContext::create()`。`SR.cpp:597-625` 在创建 weaver、配置延迟、注册追踪之后才 `initialize()`；最后一个 weaver 退出时，`SR.cpp:301-320` 先停止追踪再 `deleteSRContext()`。兼容层必须独立完成这些生命周期，不能把空 context 传给原始 Leia DLL 混用。

### SimulatedRealityDirectX.dll

DX12 实际必需：

```text
?CreateDX12Weaver@SR@@YA?AW4WeaverErrorCode@@PEAVSRContext@1@PEAUID3D12Device@@PEAUHWND__@@PEAPEAVIDX12Weaver1@1@@Z
```

FallenFlower 产物还保留下列 delay imports，扩展 DX9/DX11 覆盖时需实现：

```text
?CreateDX9Weaver@SR@@YA?AW4WeaverErrorCode@@PEAVSRContext@1@PEAUIDirect3DDevice9@@PEAUHWND__@@PEAPEAVIDX9Weaver1@1@@Z
?CreateDX11Weaver@SR@@YA?AW4WeaverErrorCode@@PEAVSRContext@1@PEAUID3D11DeviceContext@@PEAUHWND__@@PEAPEAVIDX11Weaver1@1@@Z
```

factory 返回的是 C++ interface 对象，不是函数表地址或任意 handle。`SDK/sr/weaver/dx12weaver.h:29-86` 中 `IDX12Weaver1` 虚继承 `IDestroyable`、`IQueryInterface`、`IWeaverBase1`。必须保持 MSVC x64 虚表、虚基偏移、析构及 this 调整；推荐依照该版本原始头文件实现派生类，并对客户端实际机器码/调用核验。

- `SDK/sr/utility/commoninterfaces.h:14-31`：`destroy()`、`queryInterface(std::type_index)`。
- `SDK/sr/weaver/IWeaverBase.h:25-115`：`setWindowHandle`、`weave`、`enableLateLatching`、状态查询、`setShaderSRGBConversion`、`setLatencyInFrames`、`setLatency`、`getLatency`、`getPredictedEyePositions` 及虚析构。
- `SDK/sr/weaver/dx12weaver.h:43-63`：`setInputViewTexture(ID3D12Resource*, int, int, DXGI_FORMAT)`、`setOutputFormat`、`setCommandList`、**按值** `setViewport(D3D12_VIEWPORT)`、**按值** `setScissorRect(D3D12_RECT)`。
- `SDK/sr/weaver/dx11weaver.h:29-68`：相同虚继承结构；factory 使用 `ID3D11DeviceContext*`，输入为 `ID3D11ShaderResourceView*`，另有 `setContext`。不能拿旧 SDK 的 device/context 签名套用。

### SimulatedRealityFaceTrackers.dll

```text
?create@HeadTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z
?create@HeadPoseTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z
?create@EyeTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z
?createRaw@EyeTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z
?stopListening@HeadStream@SR@@QEAAXXZ
?stopListening@HeadPoseStream@SR@@QEAAXXZ
?stopListening@EyePairStream@SR@@QEAAXXZ
```

实际两个 mod 分别请求 Head 和 HeadPose；EyeTracker 系列是当前 wrapper 的额外导入，可在第一阶段保留明确的 unsupported 行为，不能伪造有效追踪。

`SR.cpp:208-268` 创建 tracker，再经虚函数 `openHeadStream/openHeadPoseStream/openEyePairStream` 取得 `std::shared_ptr<Stream>`。`SR.cpp:152-176` listener 收到 `accept(const SR_head& / SR_headPose& / SR_eyePair&)` 后复制缓存。`SDK/sr/sense/core/inputstream.h:27-52` 的宿主内联析构会先调用非虚 `stopListening()`，然后释放 shared_ptr；因此导出存在还不够，shared_ptr 控制块、对象 lifetime、回调退出同步与 MSVC STL ABI 都需要兼容。Sense 本身也有基类虚表：`SDK/sr/sense/core/sense.h:37-64`。

### SimulatedRealityDisplays.dll

```text
?create@SwitchableLensHint@SR@@SAPEAV12@AEAVSRContext@2@@Z
```

返回 `SwitchableLensHint : Sense`，虚函数为 `enable()`、`disable()`、`isEnabled()`、`isEnabledByPreference()`；其 lifetime 属于 context，宿主不应直接 delete。原契约是所有应用的偏好仲裁，不是单进程无条件关屏：`SDK/sr/sense/display/switchablehint.h:22-36,42-73`。`SR.cpp:830-865` 对重复 enable/disable 去重。

### 额外导入与扩展面

FallenFlower 还导入 `SimulatedRealityOpenGL.dll` 的 `?CreateGLWeaver@SR@@YA?AW4WeaverErrorCode@@AEAVSRContext@1@PEAUHWND__@@PEAPEAVIGLWeaver1@1@@Z`，以及 `opencv_world343.dll` 的 `?deallocate@String@cv@@AEAAXXZ`。当前 DX12 路径不据此要求重做整个 OpenCV；应该确认实际调用及装载依赖后，提供所需原版依赖或对应兼容实现。

SDK 还提供 `SDK/sr/core_c.h:42-73` 的 `newSRContext/initializeSRContext/deleteSRContext` 等 C API、`SDK/sr/displays_c.h:67-203` 的 display/lens C API、以及 `dx11weaver.h:72+`、`dx12weaver.h:90+` 的 deprecated concrete classes。这些不在当前两个 mod 的最小导入集合里，但足以说明“一套新 factory 导出”不能等同全部 LeiaSR 客户端兼容。SR-lib 的 32 位 DLL 名称见 `SR.cpp:359-365` 和 `CMakeLists.txt:191-201`。

## 渲染、追踪与显示契约

### 渲染

1. 输入为完整左右并排 SBS GPU 纹理。FallenFlower 分配 `2 * eyeWidth` × `eyeHeight` 的 `R8G8B8A8_UNORM` 纹理，保持 shader-resource state（`native\GpuBackend.cpp:230-242,378-381`）。Yakuza 在 `src\stereo\StereoRuntime.cpp:225-226` 传入其 SBS resource 与真实目标 format。不是把两个独立眼纹理指针简单转发给驱动。
2. DX12 weave **记录到宿主给出的 command list，写入当前绑定 render target**，宿主负责执行/Present/同步。`SR.cpp:649-694`；Yakuza 在 `StereoRuntime.cpp:231-238` 显式设置 RT、viewport、scissor 和 target barrier。转接层不能把 weave 当成 Present，也不能自行关闭/提交宿主 command list。
3. FallenFlower 开启 UI 遮挡时同一帧先 weave 深度遮罩到 `uiMaskRtv`，再 weave 颜色到最终 RTV（`native\GpuBackend.cpp:383-404`）。兼容层必须支持离屏 RT 和每帧多次 weave，并保证遮罩与颜色使用一致的双眼/光学映射。只在 Present 强制 SBS，或者仅输出到私有 overlay，不能覆盖这个路径。
4. 两个 mod 都设置 `SetShaderSRGBConversion(false,false)`。目标色彩空间与 DXGI format 必须保持；SDK 1.34.10 的 late-latching 文档还注明每帧调用一次 weave，见 `SDK/sr/weaver/IWeaverBase.h:43-48`。当前 FallenFlower 多 pass 情形因此是兼容层需要单独定义同帧快照的地方，不能盲目复制“一次 weave 推进一帧”的计数方式。
5. viewport/backbuffer 与窗口物理 client size、所在显示器、窗口屏幕坐标要匹配；SDK 契约见 `IWeaverBase.h:29-40`。原生显示器若只支持原生分辨率/全屏，第一阶段可明确限制，但不能声称支持所有窗口模式。

### 追踪

- FallenFlower：`native\GpuBackend.cpp:289` 请求 `SR_TRACK_HEAD_POSE`；`native\HeadTracking.cpp:14-36` 读 `SRGetHeadPose`，要求有限毫米坐标、Z 在 100–2500 mm，且 frameId/time 真正推进才刷新 250 ms 有效期。
- Yakuza：`src\stereo\StereoRuntime.cpp:50-58,215` 请求 `SR_TRACK_HEAD`，读取头及双眼，转换成米后用于 head-follow/自动投影；`src\stereo\HeadFollow.h:13-21` 同样检查 250 ms 新鲜度。
- 原始结构 `SDK/sr/sense/headtracker/head.h:11-44`：frameId 为 uint64，time 为 epoch microseconds，位置为毫米，旋转为 radians；`SR_head` 内包含带自身 metadata 的 headPose、eyePair、earPair。不能按六个 float 的简单结构替代。
- 真实追踪丢失应让样本停止推进/失效，不能每个渲染帧更新 frameId 掩盖传感器停顿。若原生 SDK 只暴露眼睛位置，可推导头中心并明确缺失项，但不得宣称有真实头姿旋转/耳坐标。

### 显示器、透镜与所有权

- mod 在聚焦且 Leia 输出有效时 enable lens；失焦、切 SBS/单眼、Suspend 和 Shutdown 时 disable（Yakuza `StereoRuntime.cpp:195,227-241`；FallenFlower `GpuBackend.cpp:395-428`）。兼容层需在原生设备控制上维持同样可恢复的 owner/session 生命周期，并处理多个进程。
- Yakuza 自动投影的物理屏幕尺寸来自实际窗口对应显示器的 EDID，而不是 `SR::Display`：`src\stereo\ScreenMonitor.h:10-37,46-71`。所以伪造 Leia display 查询不会改变此路径的物理尺寸；原生追踪坐标必须校准到该物理面板中心/方向。
- SDK lens hint、weave、tracking 是三条不同通路。原生启动器能把屏幕切到 3D，只证明控制通路存在；仍需原生正确 weaving/校准数据与追踪时间线，才能把 Leia mod 的 SBS 变成实际裸眼 3D。

## 建议的第一阶段兼容范围

固定 `SR-SDK-1.34.10 + Windows x64 + 当前两份客户端导入 ABI`，实现四个 DLL 入口与共享桥接核心：DX12 weaver（含多 pass/offscreen）、Head 与 HeadPose 流、lens owner、context create/init/delete。保持透明 SBS 到原生输出的 GPU 路径，并明确 source texture、descriptor、command list 与 fence 的 ownership。之后再加 DX11、DX9、OpenGL、32 位、旧 weaver 与 C API；每个新增族需要真实客户端导入表和调用验证。

如果允许重编译自有 mod，可在共同静态 SR-lib wrapper 处接一个新后端，绕开仿造 C++ DLL ABI，工程量更小；但这只覆盖重新构建的 mod，不满足第三方现有二进制无需修改的目标。要支持后者，仍应做 runtime ABI 兼容层。
