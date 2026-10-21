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
	// Token: 0x0200000E RID: 14
	public class ConversionDialog : BaseDialog, IComponentConnector
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003420 File Offset: 0x00001620
		public ConversionDialog()
		{
			this.InitializeComponent();
			\u2014.\u00A0(new Action(this.RefreshLanguage));
			this.RefreshLanguage();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003450 File Offset: 0x00001650
		[NullableContext(1)]
		private void Conv2DBtnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				base.DialogResult = new bool?(true);
				base.Result = Odyssey3DHubDialogResult.Converion2D;
				if (this.IsDialogCheckBoxChecked())
				{
					this.SerializeSettingsVM(base.Result);
				}
				base.Close();
			}
			catch (Exception ex)
			{
				\u2025.\u1680(this.TAG, ex.ToString(), "Conv2DBtnClick");
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034B8 File Offset: 0x000016B8
		[NullableContext(1)]
		private void ConvSBSBtnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				base.DialogResult = new bool?(true);
				base.Result = Odyssey3DHubDialogResult.ConversionSBS;
				if (this.IsDialogCheckBoxChecked())
				{
					this.SerializeSettingsVM(base.Result);
				}
				base.Close();
			}
			catch (Exception ex)
			{
				\u2025.\u1680(this.TAG, ex.ToString(), "ConvSBSBtnClick");
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003520 File Offset: 0x00001720
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

		// Token: 0x0600005A RID: 90 RVA: 0x0000354C File Offset: 0x0000174C
		private bool IsDialogCheckBoxChecked()
		{
			return this.DialogCheckBox.IsChecked.GetValueOrDefault();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000356C File Offset: 0x0000176C
		private void SerializeSettingsVM(Odyssey3DHubDialogResult selectedMode)
		{
			\u2003.Instance.SettingsVM.IsAutoConvert = 1;
			\u2003.Instance.SettingsVM.IsStandardVideo = ((selectedMode == Odyssey3DHubDialogResult.Converion2D) ? 1 : 0);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003594 File Offset: 0x00001794
		public void RefreshLanguage()
		{
			this.tb_message.Text = \u2014.\u1680("TV_SID_REALHUB_CFJUN_WHAT_TYPE_OF_VIDEO_ARE_YOU_WATCHING_SELECT_THE_RIGHT_OPTION_FOR_3D_CONVERSION");
			this.tb2D.Text = \u2014.\u1680("TV_SID_REALHUB_CFJUN_STANDARD_VIDEO");
			this.tbSBS.Text = \u2014.\u1680("TV_SID_REALHUB_CFJUN_SPLIT_VIDEO");
			this.cb_message.Text = \u2014.\u1680("TV_SID_REALHUB_CFJUN_SAVE_VIDEO_TYPE_AND_DON_T_SHOW_AGAIN");
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000035F8 File Offset: 0x000017F8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/conversiondialog.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F98 File Offset: 0x00001198
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003628 File Offset: 0x00001828
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
				this.bd2D = (Border)target;
				return;
			case 3:
				this.Conv2DBtn = (Button)target;
				this.Conv2DBtn.Click += new RoutedEventHandler(this.Conv2DBtnClick);
				return;
			case 4:
				this.tb2D = (TextBlock)target;
				return;
			case 5:
				this.bdSBS = (Border)target;
				return;
			case 6:
				this.ConvSBSBtn = (Button)target;
				this.ConvSBSBtn.Click += new RoutedEventHandler(this.ConvSBSBtnClick);
				return;
			case 7:
				this.tbSBS = (TextBlock)target;
				return;
			case 8:
				this.HelpBtn = (Button)target;
				this.HelpBtn.Click += new RoutedEventHandler(this.HelpBtn_Click);
				return;
			case 9:
				this.CloseBtn = (Button)target;
				return;
			case 10:
				this.DialogCheckBox = (CheckBox)target;
				return;
			case 11:
				this.cb_message = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400002F RID: 47
		[Nullable(1)]
		private readonly string TAG = "Conversion Dialog";

		// Token: 0x04000030 RID: 48
		internal TextBlock tb_message;

		// Token: 0x04000031 RID: 49
		internal Border bd2D;

		// Token: 0x04000032 RID: 50
		internal Button Conv2DBtn;

		// Token: 0x04000033 RID: 51
		internal TextBlock tb2D;

		// Token: 0x04000034 RID: 52
		internal Border bdSBS;

		// Token: 0x04000035 RID: 53
		internal Button ConvSBSBtn;

		// Token: 0x04000036 RID: 54
		internal TextBlock tbSBS;

		// Token: 0x04000037 RID: 55
		internal Button HelpBtn;

		// Token: 0x04000038 RID: 56
		internal Button CloseBtn;

		// Token: 0x04000039 RID: 57
		internal CheckBox DialogCheckBox;

		// Token: 0x0400003A RID: 58
		internal TextBlock cb_message;

		// Token: 0x0400003B RID: 59
		private bool _contentLoaded;
	}
}
