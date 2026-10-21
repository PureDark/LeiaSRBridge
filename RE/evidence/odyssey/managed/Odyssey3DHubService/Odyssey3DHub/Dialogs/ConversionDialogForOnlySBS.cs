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
	// Token: 0x0200000F RID: 15
	public class ConversionDialogForOnlySBS : BaseDialog, IComponentConnector
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003749 File Offset: 0x00001949
		public ConversionDialogForOnlySBS()
		{
			this.InitializeComponent();
			this.RefreshLanguage();
			\u2014.\u00A0(new Action(this.RefreshLanguage));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000376E File Offset: 0x0000196E
		[NullableContext(1)]
		private new void OKBtnClick(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
			base.Result = Odyssey3DHubDialogResult.ConversionSBS;
			base.Close();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003520 File Offset: 0x00001720
		[NullableContext(1)]
		private void HelpBtn_Click(object sender, RoutedEventArgs e)
		{
			HelpDialog helpDialog = new HelpDialog();
			base.Visibility = 1;
			base.ChangeTimeOut(99999);
			helpDialog.PositionWindowAtBottomRight();
			helpDialog.ShowDialog();
			base.Result = Odyssey3DHubDialogResult.Help;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000378C File Offset: 0x0000198C
		public void RefreshLanguage()
		{
			this.tb_message.Text = \u2014.\u1680("TV_SID_REALHUB_CEAUG_WOULD_YOU_LIKE_TO_CONVERT_THE_CURRENT_VIDEO_TO_3D") + " " + \u2014.\u1680("TV_SID_REALHUB_CEDEC_ONLY_SIDE_BY_SIDE_VIDEO_IS_CONVERTIBLE_BECAUSE_OF_CURRENT_GPU_LIMITATIONS_ABBR_IMG");
			this.OKBtn.Content = \u2014.\u1680("COM_SID_OK");
			this.HelpBtn.Content = \u2014.\u1680("TV_SID_REALHUB_CEDEC_HELP_LENGTH_LIMIT");
			this.CancelBtn.Content = \u2014.\u1680("COM_SID_CANCEL");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003804 File Offset: 0x00001A04
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/conversiondialogforonlysbs.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F98 File Offset: 0x00001198
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003834 File Offset: 0x00001A34
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
				this.HelpBtn = (Button)target;
				this.HelpBtn.Click += new RoutedEventHandler(this.HelpBtn_Click);
				return;
			case 4:
				this.CancelBtn = (Button)target;
				this.CancelBtn.Click += new RoutedEventHandler(base.CancelBtnClick);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400003C RID: 60
		internal TextBlock tb_message;

		// Token: 0x0400003D RID: 61
		internal Button OKBtn;

		// Token: 0x0400003E RID: 62
		internal Button HelpBtn;

		// Token: 0x0400003F RID: 63
		internal Button CancelBtn;

		// Token: 0x04000040 RID: 64
		private bool _contentLoaded;
	}
}
