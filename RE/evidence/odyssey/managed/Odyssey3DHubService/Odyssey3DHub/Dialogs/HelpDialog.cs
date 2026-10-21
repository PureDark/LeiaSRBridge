using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using \u00A0;

namespace Odyssey3DHub.Dialogs
{
	// Token: 0x02000010 RID: 16
	public class HelpDialog : BaseDialog, IComponentConnector
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000038DB File Offset: 0x00001ADB
		public HelpDialog()
		{
			this.InitializeComponent();
			\u2014.\u00A0(new Action(this.RefreshLanguage));
			this.RefreshLanguage();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003900 File Offset: 0x00001B00
		public void RefreshLanguage()
		{
			this.HelpMessage.Text = string.Concat(new string[]
			{
				\u2014.\u1680("TV_SID_REALHUB_CENOV_PRESS_ESC_TO_STOP_3D_PLAYBACK"),
				"\n",
				\u2014.\u1680("TV_SID_IMPORTANT_REMINDERS"),
				"\n",
				\u2014.\u1680("TV_SID_REALHUB_CEDEC_1_DRM_PROTECTED_CONTENT_MAY_NOT_SUPPORT_3D_CONVERSION"),
				"\n",
				\u2014.\u1680("TV_SID_REALHUB_CEDEC_2_SOME_VIDEO_PLAYERS_SUCH_AS_WINDOWS_MEDIA_PLAYER_MAY_NOT_SUPPORT_3D_CONVERSION"),
				"\n",
				\u2014.\u1680("TV_SID_REALHUB_CFFEB_3_3D_CONVERSION_REQUIRES_THE_RESOLUTION_TO_BE_SET_TO_UHD"),
				"\n",
				\u2014.\u1680("TV_SID_REALHUB_CFFEB_4_IN_HDR_MODE_3D_CONVERSION_MAY_NOT_PERFORM_CORRECTLY"),
				"\n",
				\u2014.\u1680("TV_SID_REALHUB_MIX_CFFEB_5_IF_THE_CONTENT_DOES_NOT_GUARANTEE_A_3D_CONVERSION_EFFECT_THROUGH_CONVERSION_MAY_NOT_BE_POSSIB").Replace("<<A>>", \u2006.\u2001)
			});
			this.CancelBtn.Content = \u2014.\u1680("COM_IDS_BTN_CLOSE");
			base.Title = \u2014.\u1680("TV_SID_REALHUB_CEDEC_HELP_LENGTH_LIMIT");
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000039E8 File Offset: 0x00001BE8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/helpdialog.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F98 File Offset: 0x00001198
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003A18 File Offset: 0x00001C18
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.HelpMessage = (TextBlock)target;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.CancelBtn = (Button)target;
			this.CancelBtn.Click += new RoutedEventHandler(base.CancelBtnClick);
		}

		// Token: 0x04000041 RID: 65
		internal TextBlock HelpMessage;

		// Token: 0x04000042 RID: 66
		internal Button CancelBtn;

		// Token: 0x04000043 RID: 67
		private bool _contentLoaded;
	}
}
