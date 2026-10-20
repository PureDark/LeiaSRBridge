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
	// Token: 0x02000009 RID: 9
	public class MonitorGuide : Window, IComponentConnector
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000249B File Offset: 0x0000069B
		public MonitorGuide(string title, string modelName, string message)
		{
			this.InitializeComponent();
			base.Title = title;
			this.MessageText.Text = message;
			this.ModelName.Text = modelName;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024C8 File Offset: 0x000006C8
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdatePosition();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D0 File Offset: 0x000006D0
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
			base.Close();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E4 File Offset: 0x000006E4
		public static bool? ShowMessage(string title, string modelName, string message, Window owner = null)
		{
			bool? dialogResult = null;
			if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null || System.Windows.Application.Current.Dispatcher.CheckAccess())
			{
				MonitorGuide win = new MonitorGuide(title, modelName, message);
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
					MonitorGuide win2 = new MonitorGuide(title, modelName, message);
					if (owner != null)
					{
						win2.Owner = owner;
					}
					dialogResult = win2.ShowDialog();
				});
			}
			return dialogResult;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025A0 File Offset: 0x000007A0
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

		// Token: 0x06000027 RID: 39 RVA: 0x000026C8 File Offset: 0x000008C8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/monitorguide.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026F8 File Offset: 0x000008F8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MonitorGuide)target).SourceInitialized += this.Window_SourceInitialized;
				return;
			case 2:
				this.ModelName = (TextBlock)target;
				return;
			case 3:
				this.MessageText = (TextBlock)target;
				return;
			case 4:
				this.OkButton = (System.Windows.Controls.Button)target;
				this.OkButton.Click += this.OkButton_Click;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000015 RID: 21
		internal TextBlock ModelName;

		// Token: 0x04000016 RID: 22
		internal TextBlock MessageText;

		// Token: 0x04000017 RID: 23
		internal System.Windows.Controls.Button OkButton;

		// Token: 0x04000018 RID: 24
		private bool _contentLoaded;
	}
}
