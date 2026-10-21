using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using \u00A0;

namespace Odyssey3DHub.Dialogs
{
	// Token: 0x02000013 RID: 19
	public class OkDialog : BaseDialog, IComponentConnector
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003C09 File Offset: 0x00001E09
		private OkDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003C17 File Offset: 0x00001E17
		[NullableContext(1)]
		public OkDialog(string message)
		{
			this.InitializeComponent();
			this.tb_message.Text = message;
			\u2014.\u00A0(new Action(this.RefreshLanguage));
			this.RefreshLanguage();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030BA File Offset: 0x000012BA
		[NullableContext(1)]
		private new void OKBtnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C48 File Offset: 0x00001E48
		public void RefreshLanguage()
		{
			this.OKBtn.Content = \u2014.\u1680("COM_IDS_BTN_CLOSE");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C60 File Offset: 0x00001E60
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/okdialog.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F98 File Offset: 0x00001198
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003C90 File Offset: 0x00001E90
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.tb_message = (TextBlock)target;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.OKBtn = (Button)target;
			this.OKBtn.Click += new RoutedEventHandler(this.OKBtnClick);
		}

		// Token: 0x04000049 RID: 73
		internal TextBlock tb_message;

		// Token: 0x0400004A RID: 74
		internal Button OKBtn;

		// Token: 0x0400004B RID: 75
		private bool _contentLoaded;
	}
}
