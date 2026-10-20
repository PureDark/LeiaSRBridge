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
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D
{
	// Token: 0x0200000A RID: 10
	public class MyMessageBox : Window, IComponentConnector
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000277C File Offset: 0x0000097C
		public MyMessageBox()
		{
			this.InitializeComponent();
			base.Owner = System.Windows.Application.Current.MainWindow;
			DoubleAnimation opacityAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(1.5));
			opacityAnimation.Completed += this.OpacityAnimation_Completed;
			this.TIPS_BOX.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027F3 File Offset: 0x000009F3
		private void OpacityAnimation_Completed(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027FB File Offset: 0x000009FB
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdatePosition();
			User32.SetWindowDisplayAffinity(new WindowInteropHelper(this).Handle, 17U);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002818 File Offset: 0x00000A18
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

		// Token: 0x0600002D RID: 45 RVA: 0x00002920 File Offset: 0x00000B20
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/mymessagebox.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002950 File Offset: 0x00000B50
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MyMessageBox)target).SourceInitialized += this.Window_SourceInitialized;
				return;
			case 2:
				this.TIPS_BOX = (Grid)target;
				return;
			case 3:
				this.TIPS_TEXT = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000019 RID: 25
		internal Grid TIPS_BOX;

		// Token: 0x0400001A RID: 26
		internal TextBlock TIPS_TEXT;

		// Token: 0x0400001B RID: 27
		private bool _contentLoaded;
	}
}
