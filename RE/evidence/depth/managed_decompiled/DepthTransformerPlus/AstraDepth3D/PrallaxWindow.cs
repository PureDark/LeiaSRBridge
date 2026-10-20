using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D
{
	// Token: 0x0200000E RID: 14
	public class PrallaxWindow : Window, IComponentConnector
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003038 File Offset: 0x00001238
		public static PrallaxWindow Instance
		{
			get
			{
				if (PrallaxWindow._PrallaxWindowDialog == null)
				{
					PrallaxWindow._PrallaxWindowDialog = new PrallaxWindow();
				}
				return PrallaxWindow._PrallaxWindowDialog;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003050 File Offset: 0x00001250
		public PrallaxWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000306C File Offset: 0x0000126C
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdatePosition();
			IntPtr handle = new WindowInteropHelper(this).Handle;
			int exStyle = User32.GetWindowLong(handle, -20);
			exStyle |= 134217888;
			User32.SetWindowLong(handle, -20, (long)exStyle);
			User32.SetWindowPos(handle, User32.HWND_TOPMOST, 0, 0, 0, 0, 115U);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030B8 File Offset: 0x000012B8
		public void UpdateProgress(Window window, int value, int value2)
		{
			this.ShowMessage(window);
			this.Left_parax_intensity.Value = (double)value;
			this.Left_parax_value.Text = value.ToString();
			this.Left_zero_plane_intensity.Value = (double)value2;
			this.Left_zero_plane_value.Text = value2.ToString();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000310A File Offset: 0x0000130A
		private void OpacityAnimation_Completed(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003114 File Offset: 0x00001314
		private void ShowMessage(Window window)
		{
			this.CancelPreviousOperation();
			this.Container.Opacity = 1.0;
			base.Owner = window;
			base.Show();
			Task.Delay(3000, this._cancellationTokenSource.Token).ContinueWith(delegate(Task t)
			{
				if (!t.IsCanceled)
				{
					base.Dispatcher.BeginInvoke(new Action(delegate()
					{
						((Storyboard)base.Resources["CloseAnimation"]).Begin(this.Container);
					}), Array.Empty<object>());
				}
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000316F File Offset: 0x0000136F
		private void CancelPreviousOperation()
		{
			this._cancellationTokenSource.Cancel();
			this._cancellationTokenSource = new CancellationTokenSource();
			((Storyboard)base.Resources["CloseAnimation"]).Stop(this.Container);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031A7 File Offset: 0x000013A7
		private void CloseAnimation_Completed(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031AF File Offset: 0x000013AF
		private void Window_Closed(object sender, EventArgs e)
		{
			PrallaxWindow._PrallaxWindowDialog = null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031B7 File Offset: 0x000013B7
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			User32.SetWindowDisplayAffinity(new WindowInteropHelper(this).Handle, 17U);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031CC File Offset: 0x000013CC
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
			int physicalTop = bounds.Y + 20;
			base.Left = physicalLeft * physicalToLogical;
			base.Top = (double)physicalTop * physicalToLogical;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032D4 File Offset: 0x000014D4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/prallaxwindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003304 File Offset: 0x00001504
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((PrallaxWindow)target).Closed += this.Window_Closed;
				((PrallaxWindow)target).Loaded += this.Window_Loaded;
				((PrallaxWindow)target).SourceInitialized += this.Window_SourceInitialized;
				return;
			case 2:
				((Storyboard)target).Completed += this.CloseAnimation_Completed;
				return;
			case 3:
				this.Container = (Grid)target;
				return;
			case 4:
				this.Left_parax_intensity = (System.Windows.Controls.ProgressBar)target;
				return;
			case 5:
				this.Left_parax_value = (TextBlock)target;
				return;
			case 6:
				this.Left_zero_plane_intensity = (System.Windows.Controls.ProgressBar)target;
				return;
			case 7:
				this.Left_zero_plane_value = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000024 RID: 36
		private static PrallaxWindow _PrallaxWindowDialog;

		// Token: 0x04000025 RID: 37
		private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		// Token: 0x04000026 RID: 38
		internal Grid Container;

		// Token: 0x04000027 RID: 39
		internal System.Windows.Controls.ProgressBar Left_parax_intensity;

		// Token: 0x04000028 RID: 40
		internal TextBlock Left_parax_value;

		// Token: 0x04000029 RID: 41
		internal System.Windows.Controls.ProgressBar Left_zero_plane_intensity;

		// Token: 0x0400002A RID: 42
		internal TextBlock Left_zero_plane_value;

		// Token: 0x0400002B RID: 43
		private bool _contentLoaded;
	}
}
