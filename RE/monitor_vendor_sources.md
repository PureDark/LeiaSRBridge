# 泰坦军团 M27E6V-3D 官方资料核验

调查日期：2026-10-20（Asia/Hong_Kong）。范围只限用户确认的 **M27E6V-3D**，不套用 M27E6V-PRO、M27E2V 等近似型号。未下载任何软件安装包，未安装、运行软件或控制显示器；只下载了官方使用教程 PDF 作为研究证据。

## 核心结论

官方教程明确把软件分成 **3DSpatialGateway 主启动器、DepthTransformer 交织程序、可选 modex_lite 第三方游戏扩展库**。因此当前目录中的 ModexLite 并不是完整的显示器运行时；下一步应获取官方配套安装包并静态分析 DepthTransformer 的真实交织及设备接口。这一判断来自教程的组件职责说明，不是由显示器产品参数推导出的接口结论。

官网确认该型号具备眼动追踪和 2D/3D 自动切换，且官方播放器接受原生 SBS 3D 视频。但这不证明 HDMI/DP 能直接接受未经处理的 SBS 并由屏内自动交织，也不证明已有开放的 texture API。显卡纹理提交接口、追踪数据接口、实际交织 shader 与校准参数仍未知。

## 官方来源与可核查事实

### 1. 当前产品页

[M27E6V-3D 产品页](https://www.titanarmy.cn/product/media-m27e6v-3d/)

通过当前官网 HTTP 页面及其中 Product JSON-LD 实读，确认：27 英寸、16:9、3840×2160、QD-Mini LED / FAST IPS、2304 分区；刷新率原文为 **“超频185Hz；原生180Hz”**。眼追部分原文标题为 **“实时眼动追踪”**，正文说明眼追摄像头配合红外补光匹配观看位置；页面同时列明 2D/3D 自动切换、安装 3D 软件后从软件启动适配游戏、AI 实时视频转换。

产品页面列出的 180/185Hz 是显示器规格，**没有明确写成该模式下每眼 3D 刷新率**。旧新闻中的 190Hz 不作为本轮当前规格。HDR1400/XDR2000 产品能力也不能推导出 DepthTransformer 的 HDR 兼容性。

### 2. 官方 3D 技术页

[裸眼 3D 技术页](https://www.titanarmy.cn/technology-3d/)

该页明确说明所列配置对应 M27E6V-3D，列出裸眼 3D 屏（3D/2D）、实时眼动追踪摄像头、4K 185Hz、2304 分区等。该页面是产品功能说明，没有公开软件 ABI、像素交织方式、相机协议或控制命令。

### 3. 官方支持与下载入口

[官方资源下载 3D 专区](https://www.titanarmy.cn/support/#3d)

当前页面分别提供：

| 官方标题 | 官方指向的链接 | 本轮状态 |
|---|---|---|
| M27E6V-3D裸眼3D程序 | [标准版 Synology 分享](https://gofile.me/7pikH/yxYPnYCjH) | 分享入口 HTTP 200；页面需 QuickConnect 动态连接，尚未取得文件大小、清单和直接下载 URL |
| M27E6V-3D裸眼3D程序(无盘版-网吧专用) | [无盘版 NAS 分享](https://nas.titanarmy.cn:5001/sharing/BKfjlgZcC) | 分享页可读；公开 Session 元数据给出文件名 `M27E6V-3D.zip`、`is_folder=false`、`status=0`；没有取得包大小及包内目录 |
| 3D软件使用教程 | [当前官网 PDF](https://www.titanarmy.cn/wp-content/themes/titan-website/assets/downloads/resources/202607021136434173.pdf) | 已下载并提取文本，渲染核对第 2、7 页 |

官网已迁移为新的路径；搜索引擎仍能返回 `/index.php/portal/list/index/id/50.html` 与旧 `/upload/portal/...pdf`。本机实时读取旧列表路径得到 404，故以上引用以当前 `/support/` 和新 PDF 路径为准。

NAS 的 TLS 校验返回 `NotTimeValid`；本轮只对公开、无凭据元数据读取临时跳过证书检查，没有下载或执行其程序。官方分享页面所用 `SYNO.Core.Sharing.Initdata` 查询返回 `error.code=101`，因此没有把未知大小说成已核验，也未尝试登录或枚举其他分享。标准版 QuickConnect 返回服务器 connected，只能说明分享服务在线，不能替代包内文件清单。

### 4. 官方《腾讯 Windows 3D 软件使用指南》V1.0

来源为上述官网直接提供的 PDF，内容注明面向合作伙伴。证据本地保留于 `F:\目标软件\ModexLite\RE\titanarmy_3d_software_manual.pdf`，3,764,998 字节，SHA-256：

```text
BD1877270BB71AD97193447E7F3BA3BB473395CF3ADFBF19708FA791381B6359
```

| 页码 | 官方说明（概述） | 对本任务的含义 |
|---|---|---|
| 2 | 3DSpatialGateway 是主启动器；DepthTransformer 是必装的交织与转换程序；modex_lite 是依赖前两者的可选第三方游戏扩展库 | 当前工作目录不能代表全部原生显示栈 |
| 2–3 | 腾讯软件按显示器 EDID 做白名单设备校验，未通过不能使用相应 3D 功能；未认证设备按厂商流程登记 | 应保留真实设备校验/授权流程，不能把客户端兼容层误作可替代设备支持的证明 |
| 3 | 推荐硬件配置以 4K 60Hz 体验表述 | 这是推荐主机性能目标，不是本型号面板刷新率上限 |
| 4 | 使用前运行 DepthTransformer，保证设备认证、网络和 3840×2160 桌面分辨率；避开面部遮挡和直射光 | 原生交织/追踪对运行环境有具体要求 |
| 5–6 | 单人观看、避免突发大幅头动；播放器支持原生 SBS 3D 视频，也可转换 2D 视频 | 已证明应用层支持 SBS，尚未证明可直接把 SBS 扫描输出到屏幕 |
| 6 | 眼追异常可重置 USB 摄像头或重新启动 DepthTransformer | 追踪摄像头经 USB/Windows 摄像头设备路径参与工作；精确设备型号及传感器 API 未公开 |
| 7 | DepthTransformer 随 Launcher 安装，3D 游戏/视频期间需后台运行；Plus 版本增加 AI 实时转换；HDR 开启可能导致其无法工作 | 首个原型需核对实际运行时版本和 SDR 路径，不把面板 HDR 规格当运行时兼容保证 |

保留三段用于定位原文的短摘录：第 2 页 DepthTransformer 的功能栏写 **“3D 交织程序 + 2D 转 3D 工具”**；同页 modex_lite 备注含 **“需要DepthTransformer进行3D交织”**；第 6 页播放器说明为 **“导入原生 SBS 3D 视频，可直接播放。”**

## BOE / 圣美归属的证据边界

本轮取得的当前官方产品页、官方技术页、官方支持页及教程，足以确认 **DepthTransformer / Depth Transformer Plus** 的职责，但没有找到其中明确把该型号运行时署名为 **BOE** 或 **圣美** 的文字。搜索返回的媒体报道即使提到 BOE 面板，也没有用来证明驱动供应商或软件接口归属。

主代理在本地 `app.so` 发现 `\\.\pipe\BOE3DAssistantNamedPipe2`、`ws://localhost:20650`、DepthTransformer 安装路径，这是独立的二进制线索，应由本地逆向报告记录。管道名称含 BOE 与官方教程确定的 DepthTransformer 职责相互吻合，但目前仍不能据此认定完整 SDK 厂商、公开接口或某个“圣美驱动”版本。

## 仍需真实运行时/包内容回答的问题

- 标准版官方包大小、包内文件清单、DepthTransformer/Plus 的确切版本、架构、数字签名和依赖 DLL。
- 是否支持外部 D3D11/D3D12 SBS texture、共享句柄或进程内 weave；是否仅通过注入/捕获窗口工作。
- 左右眼输入布局、交换次序、色彩空间、窗口定位、每眼有效分辨率与实际 3D 可用刷新率。
- 交织算法及每屏校准数据归属、眼追输出与时间戳、3D 模式控制及多进程所有权。

这些问题必须由原生运行时静态分析或后续授权的实机测试证明；产品宣传参数和本教程均不等价于 ABI 文档。
