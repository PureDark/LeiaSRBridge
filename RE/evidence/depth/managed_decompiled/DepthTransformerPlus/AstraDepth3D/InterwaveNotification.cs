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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D
{
	// Token: 0x0200000C RID: 12
	public class InterwaveNotification : Window, IComponentConnector
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002D0C File Offset: 0x00000F0C
		public InterwaveNotification(int InterWaveStatus)
		{
			this.InitializeComponent();
			DoubleAnimation opacityAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(1.5));
			opacityAnimation.Completed += this.OpacityAnimation_Completed;
			this.NOTIFICATION_TIPS_BOX.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
			if (InterWaveStatus == 1)
			{
				this.NOTIFICATION_IMAGE.Source = new BitmapImage(new Uri("pack://application:,,,/Themes/images/ic_3D.png"));
				this.NOTIFICATION_TEXT.Text = base.FindResource("tips_3d_mode").ToString();
				return;
			}
			if (InterWaveStatus == 2)
			{
				this.NOTIFICATION_IMAGE.Source = new BitmapImage(new Uri("pack://application:,,,/Themes/images/ic_AI.png"));
				this.NOTIFICATION_TEXT.Text = base.FindResource("tips_ai_mode").ToString();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DE6 File Offset: 0x00000FE6
		private void OpacityAnimation_Completed(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DEE File Offset: 0x00000FEE
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			User32.SetWindowDisplayAffinity(new WindowInteropHelper(this).Handle, 17U);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E04 File Offset: 0x00001004
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdatePosition();
			IntPtr handle = new WindowInteropHelper(this).Handle;
			int exStyle = User32.GetWindowLong(handle, -20);
			exStyle |= 134217888;
			User32.SetWindowLong(handle, -20, (long)exStyle);
			User32.SetWindowPos(handle, User32.HWND_TOPMOST, 0, 0, 0, 0, 115U);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E50 File Offset: 0x00001050
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
			int physicalTop = bounds.Y + 50 + 20;
			base.Left = physicalLeft * physicalToLogical;
			base.Top = (double)physicalTop * physicalToLogical;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F5C File Offset: 0x0000115C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/interwavenotification.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F8C File Offset: 0x0000118C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((InterwaveNotification)target).SourceInitialized += this.Window_SourceInitialized;
				((InterwaveNotification)target).Loaded += this.Window_Loaded;
				return;
			case 2:
				this.NOTIFICATION_TIPS_BOX = (Grid)target;
				return;
			case 3:
				this.NOTIFICATION_IMAGE = (System.Windows.Controls.Image)target;
				return;
			case 4:
				this.NOTIFICATION_TEXT = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000020 RID: 32
		internal Grid NOTIFICATION_TIPS_BOX;

		// Token: 0x04000021 RID: 33
		internal System.Windows.Controls.Image NOTIFICATION_IMAGE;

		// Token: 0x04000022 RID: 34
		internal TextBlock NOTIFICATION_TEXT;

		// Token: 0x04000023 RID: 35
		private bool _contentLoaded;

		// Token: 0x02000063 RID: 99
		private struct STYLESTRUCT
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001D377 File Offset: 0x0001B577
			// (set) Token: 0x060003C5 RID: 965 RVA: 0x0001D37F File Offset: 0x0001B57F
			public int StyleOld { readonly get; set; }

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001D388 File Offset: 0x0001B588
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x0001D390 File Offset: 0x0001B590
			public int StyleNew { readonly get; set; }
		}
	}
}
