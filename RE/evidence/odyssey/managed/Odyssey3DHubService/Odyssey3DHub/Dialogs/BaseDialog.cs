using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using \u00A0;

namespace Odyssey3DHub.Dialogs
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class BaseDialog : Window
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003045 File Offset: 0x00001245
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000304D File Offset: 0x0000124D
		public Odyssey3DHubDialogResult Result { get; set; } = Odyssey3DHubDialogResult.Cancel;

		// Token: 0x06000048 RID: 72 RVA: 0x00003058 File Offset: 0x00001258
		public BaseDialog()
		{
			this.showAnimation = new DoubleAnimation();
			base.Topmost = true;
			base.Loaded += new RoutedEventHandler(this.BaseWindow_Loaded);
			SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEvents_DisplaySettingsChanged);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030BA File Offset: 0x000012BA
		private void SystemEvents_DisplaySettingsChanged([Nullable(2)] object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000030C4 File Offset: 0x000012C4
		private void AdjustWindowSizeAndUI()
		{
			double num = \u2027.\u2001();
			base.Width /= num;
			base.Height /= num;
			Border border = base.Content as Border;
			if (border != null)
			{
				border.LayoutTransform = new ScaleTransform(1.0 / num, 1.0 / num);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003124 File Offset: 0x00001324
		private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.AdjustWindowSizeAndUI();
			if (this.showAnimation != null)
			{
				Storyboard storyboard = new Storyboard();
				Storyboard.SetTarget(this.showAnimation, this);
				Storyboard.SetTargetProperty(this.showAnimation, new PropertyPath(Window.TopProperty));
				storyboard.Children.Add(this.showAnimation);
				storyboard.Begin();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000317B File Offset: 0x0000137B
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			SystemEvents.DisplaySettingsChanged -= new EventHandler(this.SystemEvents_DisplaySettingsChanged);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003198 File Offset: 0x00001398
		public void PositionWindowAtBottomRight()
		{
			if (this.primaryScreen == null)
			{
				\u2025.\u00A0(BaseDialog.TAG, "primaryScreen is null", "PositionWindowAtBottomRight");
				return;
			}
			using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
			{
				double num = (double)graphics.DpiX / 96.0;
				base.Left = ((double)this.primaryScreen.Bounds.Width - base.Width) / num;
				base.Top = (double)this.primaryScreen.Bounds.Height / num;
				this.showAnimation = new DoubleAnimation
				{
					From = new double?(base.Top),
					To = new double?(base.Top - base.Height / num),
					Duration = TimeSpan.FromSeconds(0.4),
					EasingFunction = new ElasticEase
					{
						EasingMode = 1,
						Oscillations = 3,
						Springiness = 12.0
					}
				};
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032B4 File Offset: 0x000014B4
		public void PositionWindowAtCenter()
		{
			if (this.primaryScreen == null)
			{
				\u2025.\u00A0(BaseDialog.TAG, "primaryScreen is null", "PositionWindowAtCenter");
				return;
			}
			using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
			{
				double num = (double)graphics.DpiX / 96.0;
				base.Left = ((double)this.primaryScreen.Bounds.Width - base.Width) / 2.0 / num;
				base.Top = ((double)this.primaryScreen.Bounds.Height - base.Height) / 2.0 / num;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003374 File Offset: 0x00001574
		public void SetTimeout(int timeoutSec)
		{
			this.timeOut = timeoutSec;
			Task.Run(delegate()
			{
				for (;;)
				{
					int num = this.timeOut;
					this.timeOut = num - 1;
					if (num <= 0)
					{
						break;
					}
					Thread.Sleep(1000);
				}
				base.Dispatcher.Invoke(delegate()
				{
					base.Close();
				});
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000338F File Offset: 0x0000158F
		public void ChangeTimeOut(int timeoutSec)
		{
			this.timeOut = timeoutSec;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003398 File Offset: 0x00001598
		public void OKBtnClick(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
			this.Result = Odyssey3DHubDialogResult.OK;
			base.Close();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033B3 File Offset: 0x000015B3
		public void CancelBtnClick(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(false);
			this.Result = Odyssey3DHubDialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0400002A RID: 42
		private static string TAG = "BaseDialog";

		// Token: 0x0400002B RID: 43
		[Nullable(2)]
		private Screen primaryScreen = Screen.PrimaryScreen;

		// Token: 0x0400002C RID: 44
		private DoubleAnimation showAnimation;

		// Token: 0x0400002E RID: 46
		private int timeOut = 10;
	}
}
