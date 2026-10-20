using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using AstraDepth3D.Composition.WindowsRuntimeHelpers;
using AstraDepth3D.Properties;
using AstraDepth3D.WindowHelper;
using Windows.System;

namespace AstraDepth3D
{
	// Token: 0x02000013 RID: 19
	public class App : Application
	{
		// Token: 0x060000A8 RID: 168
		[DllImport("UiaccessTools.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void YLPrepareForUIAccess();

		// Token: 0x060000A9 RID: 169 RVA: 0x00005798 File Offset: 0x00003998
		public App()
		{
			this._controller = CoreMessagingHelper.CreateDispatcherQueueControllerForCurrentThread();
			base.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
			TaskScheduler.UnobservedTaskException += this.TaskScheduler_UnobservedTaskException;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000057F0 File Offset: 0x000039F0
		protected override void OnStartup(StartupEventArgs e)
		{
			App.YLPrepareForUIAccess();
			bool isNewInstance;
			this.mutex = new Mutex(true, "com.yinglun.AstraDepth3D", ref isNewInstance);
			if (!isNewInstance)
			{
				NLogService.WriteLog("W:交织程序已存在！");
				Application.Current.Shutdown();
				return;
			}
			if (!Environment.UserInteractive)
			{
				NLogService.WriteLog("W:没有交互式用户！");
				Application.Current.Shutdown();
				return;
			}
			NLogService.WriteLog("交织程序开始启动:版本：" + this.GetVersionInfo());
			if (!ComRegistrar.IsComRegisteredByClsidWithPath("{5495b4d1-b099-4d8e-9371-cb573827ce59}"))
			{
				NLogService.WriteLog("检测到COM未注册：注册COM组件");
				ComRegistrar.RegisterComDll(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\eyetracking\\FaceTrackingManagerService.dll");
			}
			NLogService.WriteLog("begin to launch！");
			RenderOptions.ProcessRenderMode = RenderMode.Default;
			base.OnStartup(e);
			App.EnsureWerConfigured();
			if (Settings.Default.UpgradeRequired)
			{
				Settings.Default.Upgrade();
				Settings.Default.UpgradeRequired = false;
				Settings.Default.Save();
			}
			try
			{
				new MainWindow().Show();
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("创建主窗口时发生异常:" + ex.Message);
			}
			NLogService.WriteLog("交织程序已启动！");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005920 File Offset: 0x00003B20
		protected override void OnExit(ExitEventArgs e)
		{
			NLogService.WriteLog("交织程序退出！");
			base.OnExit(e);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005933 File Offset: 0x00003B33
		private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			NLogService.WriteLog("UnobservedTaskException：" + e.Exception.Message);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005950 File Offset: 0x00003B50
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = e.ExceptionObject as Exception;
				if (ex != null)
				{
					NLogService.WriteLog(string.Format("UnhandledException：{0}", ex));
					if (string.IsNullOrEmpty(ex.StackTrace))
					{
						StackTrace supplemental = new StackTrace(true);
						NLogService.WriteLog(string.Format("⚠️ Original StackTrace is empty. Supplemental trace:\n{0}", supplemental));
					}
				}
				else
				{
					string format = "UnhandledException (Non-Exception)：Type={0}, Value={1}";
					object exceptionObject = e.ExceptionObject;
					NLogService.WriteLog(string.Format(format, (exceptionObject != null) ? exceptionObject.GetType().FullName : null, e.ExceptionObject));
				}
				if (e.IsTerminating)
				{
					NLogService.WriteLog("❌ 程序即将因未处理异常而终止！");
				}
			}
			catch (Exception logEx)
			{
				Trace.TraceError(string.Format("Failed to log unhandled exception: {0}", logEx));
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005A08 File Offset: 0x00003C08
		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			NLogService.WriteLog(string.Format("DispatcherUnhandledException：{0}", e.Exception));
			e.Handled = true;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005A28 File Offset: 0x00003C28
		private string GetVersionInfo()
		{
			Version assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
			AssemblyInformationalVersionAttribute customAttribute = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
			string result;
			if ((result = ((customAttribute != null) ? customAttribute.InformationalVersion : null)) == null)
			{
				result = string.Format("v{0}.{1}.{2}.{3}", new object[]
				{
					assemblyVersion.Major,
					assemblyVersion.Minor,
					assemblyVersion.Build,
					assemblyVersion.Revision
				});
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005AA8 File Offset: 0x00003CA8
		private static void EnsureWerConfigured()
		{
			string ExeName = Process.GetCurrentProcess().ProcessName + ".exe";
			try
			{
				if (!WerDumpConfigurator.IsConfigured(ExeName))
				{
					WerDumpConfigurator.ConfigureLocalDumps(ExeName, null, 5, 2);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("W:无法配置WER本地转储: " + ex.Message);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005B08 File Offset: 0x00003D08
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/app.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005B38 File Offset: 0x00003D38
		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}

		// Token: 0x04000050 RID: 80
		private Mutex mutex;

		// Token: 0x04000051 RID: 81
		private DispatcherQueueController _controller;

		// Token: 0x04000052 RID: 82
		private bool _contentLoaded;
	}
}
