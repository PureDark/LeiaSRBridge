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

namespace AstraDepth3D
{
	// Token: 0x02000008 RID: 8
	public class MessageWindow : Window, IComponentConnector
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000021E2 File Offset: 0x000003E2
		public MessageWindow(string title, string message)
		{
			this.InitializeComponent();
			base.Title = title;
			this.MessageText.Text = message;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002203 File Offset: 0x00000403
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdatePosition();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000220B File Offset: 0x0000040B
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
			base.Close();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002220 File Offset: 0x00000420
		public static bool? ShowMessage(string title, string message, Window owner = null)
		{
			bool? dialogResult = null;
			if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null || System.Windows.Application.Current.Dispatcher.CheckAccess())
			{
				MessageWindow win = new MessageWindow(title, message);
				if (owner != null)
				{
					win.Owner = owner;
				}
				dialogResult = win.ShowDialog();
			}
			else
			{
				System.Windows.Application.Current.Dispatcher.Invoke(delegate()
				{
					MessageWindow win2 = new MessageWindow(title, message);
					if (owner != null)
					{
						win2.Owner = owner;
					}
					dialogResult = win2.ShowDialog();
				});
			}
			return dialogResult;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022D0 File Offset: 0x000004D0
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
			double physicalHeight = base.Height / physicalToLogical;
			double physicalLeft = (double)bounds.X + ((double)bounds.Width - physicalWidth) / 2.0;
			double physicalTop = (double)bounds.Y + ((double)bounds.Height - physicalHeight) / 2.0;
			base.Left = physicalLeft * physicalToLogical;
			base.Top = physicalTop * physicalToLogical;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023F8 File Offset: 0x000005F8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/messagewindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002428 File Offset: 0x00000628
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MessageWindow)target).SourceInitialized += this.Window_SourceInitialized;
				return;
			case 2:
				this.MessageText = (TextBlock)target;
				return;
			case 3:
				this.OkButton = (System.Windows.Controls.Button)target;
				this.OkButton.Click += this.OkButton_Click;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000012 RID: 18
		internal TextBlock MessageText;

		// Token: 0x04000013 RID: 19
		internal System.Windows.Controls.Button OkButton;

		// Token: 0x04000014 RID: 20
		private bool _contentLoaded;
	}
}
