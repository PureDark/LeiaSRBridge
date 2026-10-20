using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D
{
	// Token: 0x0200000B RID: 11
	public class FaceTrackingNotification : Window, IComponentConnector
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000029AC File Offset: 0x00000BAC
		public FaceTrackingNotification()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029C4 File Offset: 0x00000BC4
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdatePosition();
			IntPtr handle = new WindowInteropHelper(this).Handle;
			int exStyle = User32.GetWindowLong(handle, -20);
			exStyle |= 134217888;
			User32.SetWindowLong(handle, -20, (long)exStyle);
			User32.SetWindowPos(handle, User32.HWND_TOPMOST, 0, 0, 0, 0, 115U);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A0E File Offset: 0x00000C0E
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			User32.SetWindowDisplayAffinity(new WindowInteropHelper(this).Handle, 17U);
			this.UpdateNotifucation(-1);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A2C File Offset: 0x00000C2C
		public void UpdateNotifucation(int current_state)
		{
			if (this.faceTrackingState != current_state)
			{
				switch (current_state)
				{
				case -2:
					this.Prompt_Mark.Background = System.Windows.Media.Brushes.Orange;
					this.Prompt.Text = string.Empty;
					break;
				case -1:
					this.Prompt_Mark.Background = System.Windows.Media.Brushes.Orange;
					this.Prompt.Text = base.FindResource("tips_launch_eye_tracking").ToString();
					break;
				case 0:
					this.Prompt_Mark.Background = System.Windows.Media.Brushes.Orange;
					this.Prompt.Text = string.Empty;
					break;
				case 1:
					this.Prompt_Mark.Background = System.Windows.Media.Brushes.Orange;
					this.Prompt.Text = base.FindResource("msg_too_close_warning").ToString();
					break;
				case 2:
					this.Prompt_Mark.Background = System.Windows.Media.Brushes.Orange;
					this.Prompt.Text = base.FindResource("msg_too_far_warning").ToString();
					break;
				default:
					this.Prompt_Mark.Background = System.Windows.Media.Brushes.Green;
					this.Prompt.Text = string.Empty;
					break;
				}
				this.faceTrackingState = current_state;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B60 File Offset: 0x00000D60
		private void UpdatePosition()
		{
			IntPtr _mainHwnd = IntPtr.Zero;
			Window mainWindow = System.Windows.Application.Current.MainWindow;
			if (mainWindow != null)
			{
				WindowInteropHelper helper = new WindowInteropHelper(mainWindow);
				if (helper.Handle != IntPtr.Zero)
				{
					_mainHwnd = helper.Handle;
				}
			}
			if (_mainHwnd == IntPtr.Zero)
			{
				return;
			}
			Screen currentScreen = Screen.FromHandle(_mainHwnd);
			if (currentScreen == null)
			{
				return;
			}
			Rectangle bounds = currentScreen.Bounds;
			double physicalToLogical = 1.0;
			IntPtr hwnd = new WindowInteropHelper(this).Handle;
			if (hwnd != IntPtr.Zero)
			{
				HwndSource source = HwndSource.FromHwnd(hwnd);
				if (((source != null) ? source.CompositionTarget : null) != null)
				{
					physicalToLogical = source.CompositionTarget.TransformFromDevice.M11;
				}
			}
			double physicalWidth = base.Width / physicalToLogical;
			double physicalLeft = (double)bounds.X + ((double)bounds.Width - physicalWidth) / 2.0;
			int physicalTop = bounds.Y + 5;
			base.Left = physicalLeft * physicalToLogical;
			base.Top = (double)physicalTop * physicalToLogical;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C68 File Offset: 0x00000E68
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/facetrackingnotification.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C98 File Offset: 0x00000E98
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((FaceTrackingNotification)target).SourceInitialized += this.Window_SourceInitialized;
				((FaceTrackingNotification)target).Loaded += this.Window_Loaded;
				return;
			case 2:
				this.Prompt_Mark = (Border)target;
				return;
			case 3:
				this.Prompt = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400001C RID: 28
		private int faceTrackingState = -99;

		// Token: 0x0400001D RID: 29
		internal Border Prompt_Mark;

		// Token: 0x0400001E RID: 30
		internal TextBlock Prompt;

		// Token: 0x0400001F RID: 31
		private bool _contentLoaded;

		// Token: 0x02000062 RID: 98
		private struct STYLESTRUCT
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001D355 File Offset: 0x0001B555
			// (set) Token: 0x060003C1 RID: 961 RVA: 0x0001D35D File Offset: 0x0001B55D
			public int StyleOld { readonly get; set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001D366 File Offset: 0x0001B566
			// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001D36E File Offset: 0x0001B56E
			public int StyleNew { readonly get; set; }
		}
	}
}
