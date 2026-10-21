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
	// Token: 0x02000011 RID: 17
	public class CheckBoxDialog : BaseDialog, IComponentConnector
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003A67 File Offset: 0x00001C67
		private CheckBoxDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A75 File Offset: 0x00001C75
		[NullableContext(1)]
		public CheckBoxDialog(string message)
		{
			this.InitializeComponent();
			this.tb_message.Text = message;
			this.RefreshLanguage();
			\u2014.\u00A0(new Action(this.RefreshLanguage));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003AA8 File Offset: 0x00001CA8
		[NullableContext(1)]
		private new void OKBtnClick(object sender, RoutedEventArgs e)
		{
			if (this.DialogCheckBox.IsChecked.GetValueOrDefault())
			{
				\u00A0.Default.MultipleDevicePopupShow = false;
				\u00A0.Default.Save();
			}
			\u2035.Instance.\u2007 = true;
			Process.Start("Odyssey3DHubUI.exe");
			base.Close();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AFB File Offset: 0x00001CFB
		public void RefreshLanguage()
		{
			this.OKBtn.Content = \u2014.\u1680("COM_IDS_BTN_CLOSE");
			this.DialogCheckBox.Content = \u2014.\u1680("COM_TV_DO_NOT_SHOW_AGAIN");
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B28 File Offset: 0x00001D28
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/multipledevicedialog.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F98 File Offset: 0x00001198
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B58 File Offset: 0x00001D58
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.tb_message = (TextBlock)target;
				return;
			case 2:
				this.OKBtn = (Button)target;
				this.OKBtn.Click += new RoutedEventHandler(this.OKBtnClick);
				return;
			case 3:
				this.DialogCheckBox = (CheckBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000044 RID: 68
		internal TextBlock tb_message;

		// Token: 0x04000045 RID: 69
		internal Button OKBtn;

		// Token: 0x04000046 RID: 70
		internal CheckBox DialogCheckBox;

		// Token: 0x04000047 RID: 71
		private bool _contentLoaded;
	}
}
