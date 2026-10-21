using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Odyssey3DHub.Dialogs
{
	// Token: 0x02000012 RID: 18
	public class NineSliceImage : UserControl, IComponentConnector
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public NineSliceImage()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003BD0 File Offset: 0x00001DD0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/ninesliceimage.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C00 File Offset: 0x00001E00
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000048 RID: 72
		private bool _contentLoaded;
	}
}
