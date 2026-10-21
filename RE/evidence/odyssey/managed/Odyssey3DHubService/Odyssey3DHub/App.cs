using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using \u00A0;

namespace Odyssey3DHub
{
	// Token: 0x02000003 RID: 3
	[NullableContext(1)]
	[Nullable(0)]
	public class App : Application
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002149 File Offset: 0x00000349
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (e != null && e.ExceptionObject != null)
			{
				Exception ex = (Exception)e.ExceptionObject;
				if (ex != null)
				{
					\u2025.\u00A0(this.TAG, ex.ToString(), "CurrentDomain_UnhandledException");
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021AD File Offset: 0x000003AD
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			if (e != null && e.Exception != null)
			{
				\u2025.\u00A0(this.TAG, e.Exception.ToString(), "Application_DispatcherUnhandledException");
				e.Handled = true;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021DC File Offset: 0x000003DC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			base.Startup += new StartupEventHandler(this.Application_Startup);
			base.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(this.Application_DispatcherUnhandledException);
			base.StartupUri = new Uri("Src/MainWindow.xaml", 2);
			Uri uri = new Uri("/Odyssey3DHubService;component/app.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002241 File Offset: 0x00000441
		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}

		// Token: 0x04000002 RID: 2
		private readonly string TAG = "App";

		// Token: 0x04000003 RID: 3
		private bool _contentLoaded;
	}
}
