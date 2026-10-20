using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AstraDepth3D
{
	// Token: 0x02000005 RID: 5
	public class AiStereoEngineLoading : Window, IComponentConnector
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000209F File Offset: 0x0000029F
		public AiStereoEngineLoading()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020B0 File Offset: 0x000002B0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/aistereoengineloading.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020E0 File Offset: 0x000002E0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.progressBar1 = (ProgressBar)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000002 RID: 2
		internal ProgressBar progressBar1;

		// Token: 0x04000003 RID: 3
		private bool _contentLoaded;
	}
}
