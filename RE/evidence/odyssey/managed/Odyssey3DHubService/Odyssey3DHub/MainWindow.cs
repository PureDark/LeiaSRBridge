using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using \u00A0;

namespace Odyssey3DHub
{
	// Token: 0x02000004 RID: 4
	public class MainWindow : Window, IComponentConnector
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002268 File Offset: 0x00000468
		public MainWindow()
		{
			\u2002.\u2000();
			if (!\u2002.\u00A0())
			{
				Process.GetCurrentProcess().Kill();
			}
			if (!\u202A.\u00A0())
			{
				if (Application.Current != null)
				{
					Application.Current.Shutdown();
				}
				return;
			}
			this.InitializeComponent();
			\u2014.\u00A0();
			\u2025.\u00A0("Service.log");
			\u2005.\u2002();
			\u2004.\u00A0();
			\u2013.\u1680();
			\u2035.Instance.\u2008();
			\u204A.Instance.\u00A0();
			\u202E.\u00A0();
			\u1680.\u00A0();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022EC File Offset: 0x000004EC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/mainwindow.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000231C File Offset: 0x0000051C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000004 RID: 4
		private bool _contentLoaded;
	}
}
