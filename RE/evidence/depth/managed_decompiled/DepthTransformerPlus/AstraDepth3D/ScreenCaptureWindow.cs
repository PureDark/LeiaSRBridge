using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Markup;
using AstraDepth3D.BOEStereoRender;
using AstraDepth3D.CaptureSampleCore;
using AstraDepth3D.Composition.WindowsRuntimeHelpers;
using AstraDepth3D.EventBus;
using AstraDepth3D.Properties;
using AstraDepth3D.Tracker;
using AstraDepth3D.WindowHelper;
using Microsoft.Win32;
using Windows.Foundation;
using Windows.Graphics.Capture;
using Windows.UI.Composition;

namespace AstraDepth3D
{
	// Token: 0x0200000F RID: 15
	public class ScreenCaptureWindow : Window, IComponentConnector
	{
		// Token: 0x06000052 RID: 82
		[DllImport("user32.dll")]
		private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

		// Token: 0x06000053 RID: 83 RVA: 0x0000342C File Offset: 0x0000162C
		public ScreenCaptureWindow(Compositor compositor, ContainerVisual rootVisual, BasicSampleApplication sample)
		{
			this.InitializeComponent();
			Window mainWindow = System.Windows.Application.Current.MainWindow;
			if (mainWindow != null)
			{
				WindowInteropHelper helper = new WindowInteropHelper(mainWindow);
				if (helper.Handle != IntPtr.Zero)
				{
					this._mainHwnd = helper.Handle;
					base.Owner = mainWindow;
				}
			}
			this._localPublisher = new LocalEventPublisher();
			this._localPublisher.MsgEventHandler += this.LocalCustomEventProc;
			this._compositor = compositor;
			this._sample = sample;
			this._rootVisual = rootVisual;
			NLogService.WriteLog("ScreenCaptureWindow:" + base.Title);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000034D8 File Offset: 0x000016D8
		public ScreenCaptureWindow(Compositor compositor, ContainerVisual rootVisual, BasicSampleApplication sample, IntPtr hwnd)
		{
			this.InitializeComponent();
			Window mainWindow = System.Windows.Application.Current.MainWindow;
			if (mainWindow != null)
			{
				WindowInteropHelper helper = new WindowInteropHelper(mainWindow);
				if (helper.Handle != IntPtr.Zero)
				{
					this._mainHwnd = helper.Handle;
					base.Owner = mainWindow;
				}
			}
			this._localPublisher = new LocalEventPublisher();
			this._localPublisher.MsgEventHandler += this.LocalCustomEventProc;
			this._compositor = compositor;
			this._sample = sample;
			this._rootVisual = rootVisual;
			this._fullscreenHwnd = hwnd;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003575 File Offset: 0x00001775
		public void SetTracker(Facetracker facetracker)
		{
			this._faceTracking = facetracker;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000357E File Offset: 0x0000177E
		public void FlipInterWaveType()
		{
			BasicSampleApplication sample = this._sample;
			if (sample == null)
			{
				return;
			}
			sample.FlipInterWaveType();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003590 File Offset: 0x00001790
		public void ResetCurrentInterwaveType(LocalEnumeration.InterWaveType type)
		{
			GlobalStatusManager.instance._iInterWaveType = type;
			BasicSampleApplication sample = this._sample;
			if (sample == null)
			{
				return;
			}
			sample.ResetCurrentInterwaveStatus(type);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035B0 File Offset: 0x000017B0
		public void CloseCaptureWindow()
		{
			if (this._isClosing)
			{
				return;
			}
			this._isClosing = true;
			if (!this._isInitialized)
			{
				this._deferredClose = true;
				return;
			}
			this.ExecuteClose();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035E0 File Offset: 0x000017E0
		private void OnLoadedClose(object sender, RoutedEventArgs e)
		{
			base.Loaded -= this.OnLoadedClose;
			this._isInitialized = true;
			if (this._deferredClose)
			{
				this.ExecuteClose();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000360D File Offset: 0x0000180D
		private void ExecuteClose()
		{
			this.StopCapture();
			if (this._localPublisher != null)
			{
				this._localPublisher.MsgEventHandler -= this.LocalCustomEventProc;
				this._localPublisher = null;
			}
			base.Close();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003644 File Offset: 0x00001844
		public void SetParax(float value)
		{
			float parax = Settings.Default.stereo_parax;
			parax += value;
			parax = (float)Math.Round((double)(parax / 0.05f), MidpointRounding.AwayFromZero) * 0.05f;
			parax = Math.Max(Math.Min(parax, 1f), 0.05f);
			parax = (float)Math.Round((double)(parax * 20f)) / 20f;
			Settings.Default.stereo_parax = parax;
			Settings.Default.Save();
			float zero_plane = Settings.Default.zero_panel;
			zero_plane = (float)Math.Round((double)(zero_plane / 0.05f), MidpointRounding.AwayFromZero) * 0.05f;
			zero_plane = Math.Max(Math.Min(zero_plane, 1f), 0f);
			zero_plane = (float)Math.Round((double)(zero_plane * 20f)) / 20f;
			BasicSampleApplication sample = this._sample;
			if (sample != null)
			{
				sample.SetParax(parax, zero_plane);
			}
			base.Dispatcher.InvokeAsync(delegate()
			{
				int progress = (int)(parax * 100f);
				int progress2 = (int)(zero_plane * 100f);
				PrallaxWindow.Instance.UpdateProgress(this, progress, progress2);
			});
			NLogService.WriteLog(string.Format("parax:{0}", parax));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000037B4 File Offset: 0x000019B4
		public void SetZeroPlane(float value)
		{
			float zero_plane = Settings.Default.zero_panel;
			zero_plane += value;
			zero_plane = (float)Math.Round((double)(zero_plane / 0.05f), MidpointRounding.AwayFromZero) * 0.05f;
			zero_plane = Math.Max(Math.Min(zero_plane, 1f), 0f);
			zero_plane = (float)Math.Round((double)(zero_plane * 20f)) / 20f;
			Settings.Default.zero_panel = zero_plane;
			Settings.Default.Save();
			float parax = Settings.Default.stereo_parax;
			parax = (float)Math.Round((double)(parax / 0.05f), MidpointRounding.AwayFromZero) * 0.05f;
			parax = Math.Max(Math.Min(parax, 1f), 0.05f);
			parax = (float)Math.Round((double)(parax * 20f)) / 20f;
			BasicSampleApplication sample = this._sample;
			if (sample != null)
			{
				sample.SetParax(parax, zero_plane);
			}
			base.Dispatcher.InvokeAsync(delegate()
			{
				int progress = (int)(parax * 100f);
				int progress2 = (int)(zero_plane * 100f);
				PrallaxWindow.Instance.UpdateProgress(this, progress, progress2);
			});
			NLogService.WriteLog(string.Format("zero_plane:{0}", zero_plane));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003924 File Offset: 0x00001B24
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			IntPtr handle = new WindowInteropHelper(this).Handle;
			int exStyle = User32.GetWindowLong(handle, -20);
			exStyle |= 134217888;
			User32.SetWindowLong(handle, -20, (long)exStyle);
			this.UpdateFullScreenPosition();
			User32.SetWindowPos(handle, User32.HWND_TOPMOST, 0, 0, 0, 0, 115U);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003970 File Offset: 0x00001B70
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			((HwndSource)PresentationSource.FromVisual(this)).AddHook(new HwndSourceHook(this.ChildHwndHook));
			WindowInteropHelper interopWindow = new WindowInteropHelper(this);
			this._captureHwnd = interopWindow.Handle;
			User32.SetWindowDisplayAffinity(this._captureHwnd, 17U);
			this.InitComposition();
			this.CreateFaceTrackingNotification();
			if (this._fullscreenHwnd != IntPtr.Zero)
			{
				this.StartCapture(this._fullscreenHwnd);
			}
			else
			{
				this.StartCapture();
			}
			this._isInitialized = true;
			NLogService.WriteLog("ScreenCaptureWindow loaded");
			if (this._deferredClose)
			{
				this.ExecuteClose();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A10 File Offset: 0x00001C10
		private IntPtr ChildHwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 124 && (long)wParam == -20L)
			{
				ScreenCaptureWindow.STYLESTRUCT styleStruct = (ScreenCaptureWindow.STYLESTRUCT)Marshal.PtrToStructure(lParam, typeof(ScreenCaptureWindow.STYLESTRUCT));
				styleStruct.StyleNew |= 134742176;
				Marshal.StructureToPtr<ScreenCaptureWindow.STYLESTRUCT>(styleStruct, lParam, false);
				handled = true;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003A6C File Offset: 0x00001C6C
		private void InitComposition()
		{
			this._windowTarget = this._compositor.CreateDesktopWindowTarget(this._captureHwnd, true);
			this._windowTarget.put_Root(this._rootVisual);
			this._sample.SetEventBus(this._localPublisher);
			this._rootVisual.Children.InsertAtTop(this._sample.Visual);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003AD0 File Offset: 0x00001CD0
		private void StartHwndCapture(IntPtr hwnd)
		{
			GraphicsCaptureItem item = CaptureHelper.CreateItemForWindow(hwnd);
			if (item != null)
			{
				GraphicsCaptureItem @object = item;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<GraphicsCaptureItem, object>>(new Func<TypedEventHandler<GraphicsCaptureItem, object>, EventRegistrationToken>(@object.add_Closed), new Action<EventRegistrationToken>(@object.remove_Closed), delegate(GraphicsCaptureItem s, object a)
				{
					this.StopCapture();
				});
				BasicSampleApplication sample = this._sample;
				if (sample != null)
				{
					sample.SetInterwaveStatus(GlobalStatusManager.instance._iInterWaveType);
				}
				BasicSampleApplication sample2 = this._sample;
				if (sample2 == null)
				{
					return;
				}
				sample2.StartCaptureFromItem(item);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B44 File Offset: 0x00001D44
		private void StartHmonCapture(IntPtr hmon)
		{
			try
			{
				GraphicsCaptureItem item = CaptureHelper.CreateItemForMonitor(hmon);
				if (item != null)
				{
					GraphicsCaptureItem @object = item;
					WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<GraphicsCaptureItem, object>>(new Func<TypedEventHandler<GraphicsCaptureItem, object>, EventRegistrationToken>(@object.add_Closed), new Action<EventRegistrationToken>(@object.remove_Closed), delegate(GraphicsCaptureItem s, object a)
					{
						NLogService.WriteLog("Hmon item closed!");
						this.StopCapture();
					});
					NLogService.WriteLog(string.Format("===monitor info[name:{0},size:{1}*{2}]===", item.DisplayName, item.Size.Width, item.Size.Height));
					BasicSampleApplication sample = this._sample;
					if (sample != null)
					{
						sample.SetInterwaveStatus(GlobalStatusManager.instance._iInterWaveType);
					}
					BasicSampleApplication sample2 = this._sample;
					if (sample2 != null)
					{
						sample2.StartCaptureFromItem(item);
					}
				}
			}
			catch (COMException ex)
			{
				NLogService.WriteLog("StartHwndCapture|COM异常，回退DXGI: " + ex.Message);
				BasicSampleApplication sample3 = this._sample;
				if (sample3 != null)
				{
					sample3.SetInterwaveStatus(GlobalStatusManager.Instance._iInterWaveType);
				}
				BasicSampleApplication sample4 = this._sample;
				if (sample4 != null)
				{
					sample4.StartCaptureFromDXGI();
				}
			}
			catch (Exception ex2)
			{
				NLogService.WriteLog("StartHwndCapture|异常: " + ex2.Message);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C6C File Offset: 0x00001E6C
		private void StartPrimaryMonitorCapture()
		{
			MonitorInfo monitor = this.GetMonitorFromWindow();
			this.StartHmonCapture(monitor.Hmon);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C8C File Offset: 0x00001E8C
		private void StartCapture()
		{
			if (ScreenCaptureWindow.IsSupportWGCAndSupportBorderRequired())
			{
				this.StartPrimaryMonitorCapture();
				NLogService.WriteLog("ScreenCaptureWindow Started");
				return;
			}
			BasicSampleApplication sample = this._sample;
			if (sample != null)
			{
				sample.SetInterwaveStatus(GlobalStatusManager.instance._iInterWaveType);
			}
			BasicSampleApplication sample2 = this._sample;
			if (sample2 == null)
			{
				return;
			}
			sample2.StartCaptureFromDXGI();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003CDE File Offset: 0x00001EDE
		private void StartCapture(IntPtr hwnd)
		{
			this.StartHwndCapture(hwnd);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003CE7 File Offset: 0x00001EE7
		private void StopCapture()
		{
			BasicSampleApplication sample = this._sample;
			if (sample == null)
			{
				return;
			}
			sample.StopCapture();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003CFC File Offset: 0x00001EFC
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			NLogService.WriteLog("ScreenCaptureWindow Closing...");
			((HwndSource)PresentationSource.FromVisual(this)).RemoveHook(new HwndSourceHook(this.ChildHwndHook));
			FaceTrackingNotification faceTrackingNotify = this._faceTrackingNotify;
			if (faceTrackingNotify != null)
			{
				faceTrackingNotify.Close();
			}
			this._faceTrackingNotify = null;
			BasicSampleApplication sample = this._sample;
			if (sample != null)
			{
				sample.StopCapture();
			}
			BasicSampleApplication sample2 = this._sample;
			if (sample2 != null)
			{
				sample2.SetEventBus(null);
			}
			if (this._windowTarget != null)
			{
				this._windowTarget.put_Root(null);
				CompositionTarget windowTarget = this._windowTarget;
				if (windowTarget != null)
				{
					windowTarget.Dispose();
				}
				this._windowTarget = null;
			}
			ContainerVisual rootVisual = this._rootVisual;
			if (rootVisual != null)
			{
				rootVisual.Children.RemoveAll();
			}
			this._rootVisual = null;
			this._compositor = null;
			this._sample = null;
			NLogService.WriteLog("ScreenCaptureWindow Closing");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003DCC File Offset: 0x00001FCC
		private void Window_Closed(object sender, EventArgs e)
		{
			this._faceTracking = null;
			if (this._localPublisher != null)
			{
				this._localPublisher.MsgEventHandler -= this.LocalCustomEventProc;
				this._localPublisher = null;
			}
			try
			{
				if (MCUSdk.BOE_MCU_get3DMode() && MCUSdk.BOE_MCU_set3DMode(false) == 0)
				{
					NLogService.WriteLog("状态跟踪:透镜已经关闭\n");
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_OFF;
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("状态跟踪:关闭透镜失败，异常信息:" + ex.Message + "\n");
			}
			InterwaveNotification interwaveNotification = new InterwaveNotification(0);
			Window mainWindow = System.Windows.Application.Current.MainWindow;
			if (mainWindow != null && new WindowInteropHelper(mainWindow).Handle != IntPtr.Zero)
			{
				interwaveNotification.Owner = mainWindow;
			}
			interwaveNotification.Show();
			NLogService.WriteLog("ScreenCaptureWindow Closed");
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003EA0 File Offset: 0x000020A0
		private void CreateFaceTrackingNotification()
		{
			if (this._faceTrackingNotify == null)
			{
				this._faceTrackingNotify = new FaceTrackingNotification();
				this._faceTrackingNotify.Owner = this;
				this._faceTrackingNotify.Show();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003ECC File Offset: 0x000020CC
		private void ShowInterwaveNotify(int interwaveState)
		{
			new InterwaveNotification(interwaveState)
			{
				Owner = this
			}.Show();
			NLogService.WriteLog(string.Format("交织类型改变:{0}；1为sbs,2为ai", interwaveState));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003EF8 File Offset: 0x000020F8
		private void LocalCustomEventProc(object sender, LocalEventArgs customEventArgs)
		{
			base.Dispatcher.InvokeAsync(delegate()
			{
				if (customEventArgs.key == "InterWaveStatusChanged")
				{
					if (GlobalStatusManager.Instance._iInterWaveType != (LocalEnumeration.InterWaveType)customEventArgs.value)
					{
						GlobalStatusManager.Instance._iInterWaveType = (LocalEnumeration.InterWaveType)customEventArgs.value;
					}
					this.ShowInterwaveNotify(customEventArgs.value);
					return;
				}
				if (customEventArgs.key == "FaceTrackingStatusChanged")
				{
					GlobalStatusManager.instance._bEyeCrackrRet = (customEventArgs.value >= 0);
					FaceTrackingNotification faceTrackingNotify = this._faceTrackingNotify;
					if (faceTrackingNotify == null)
					{
						return;
					}
					faceTrackingNotify.UpdateNotifucation(customEventArgs.value);
					return;
				}
				else
				{
					if (customEventArgs.key == "RasterStatusChanged")
					{
						if (customEventArgs.value == 2)
						{
							MainWindow mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
							if (mainWindow != null)
							{
								mainWindow.SendMessageToSelf((IntPtr)100, (IntPtr)0);
							}
						}
						GlobalStatusManager.instance._bRasterRet = (customEventArgs.value == 1);
						NLogService.WriteLog(string.Format("MainWindow|RasterStatus:{0}", customEventArgs.value == 1));
						return;
					}
					if (customEventArgs.key == "StartInterWave_error")
					{
						NLogService.WriteLog("MainWindow|InterWaveType:交织开启失败，准备重试");
						if (this.IsLoaded)
						{
							BasicSampleApplication sample = this._sample;
							if (sample != null)
							{
								sample.ResetDevice();
							}
							this.StartCapture();
						}
					}
					return;
				}
			});
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003F34 File Offset: 0x00002134
		public static bool IsSupportWGCAndSupportBorderRequired()
		{
			try
			{
				RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
				if (registry != null)
				{
					string currentBuild = (string)registry.GetValue("CurrentBuild");
					NLogService.WriteLog("System version: " + currentBuild);
					if (currentBuild != null && int.Parse(currentBuild) >= 20348)
					{
						return true;
					}
				}
			}
			catch
			{
				NLogService.WriteLog("读取CurrentBuild失败!");
			}
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003FAC File Offset: 0x000021AC
		private void UpdateFullScreenPosition2()
		{
			MonitorInfo primaryMonitor = MonitorEnumerationHelper.GetMonitors().FirstOrDefault((MonitorInfo m) => m.IsPrimary);
			if (primaryMonitor.MonitorArea == default(Rect))
			{
				return;
			}
			IntPtr hwnd = new WindowInteropHelper(this).Handle;
			if (hwnd == IntPtr.Zero)
			{
				return;
			}
			Rect monitorArea = primaryMonitor.MonitorArea;
			User32.SetWindowPos(hwnd, User32.HWND_TOPMOST, (int)monitorArea.X, (int)monitorArea.Y, (int)monitorArea.Width, (int)monitorArea.Height, 48U);
			NLogService.WriteLog("ScreenCaptureWindow|UpdateFullScreenPosition: " + string.Format("MonitorArea=({0},{1},{2},{3})", new object[]
			{
				monitorArea.X,
				monitorArea.Y,
				monitorArea.Width,
				monitorArea.Height
			}));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000040A4 File Offset: 0x000022A4
		private void UpdateFullScreenPosition()
		{
			IntPtr hwnd = new WindowInteropHelper(this).Handle;
			if (hwnd == IntPtr.Zero)
			{
				return;
			}
			if (this._mainHwnd == IntPtr.Zero)
			{
				return;
			}
			Screen currentScreen = Screen.FromHandle(this._mainHwnd);
			if (currentScreen == null)
			{
				return;
			}
			Rectangle bounds = currentScreen.Bounds;
			User32.SetWindowPos(hwnd, User32.HWND_TOPMOST, bounds.X, bounds.Y, bounds.Width, bounds.Height, 48U);
			NLogService.WriteLog("ScreenCaptureWindow|UpdateFullScreenPosition: " + string.Format("MonitorArea=({0},{1},{2},{3})", new object[]
			{
				bounds.X,
				bounds.Y,
				bounds.Width,
				bounds.Height
			}));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000417C File Offset: 0x0000237C
		private MonitorInfo GetMonitorFromWindow()
		{
			if (this._mainHwnd == IntPtr.Zero)
			{
				return null;
			}
			IntPtr hMonitor = ScreenCaptureWindow.MonitorFromWindow(this._mainHwnd, 2U);
			return MonitorEnumerationHelper.GetMonitors().FirstOrDefault((MonitorInfo m) => m.Hmon == hMonitor);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000041CC File Offset: 0x000023CC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/screencapturewindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000041FC File Offset: 0x000023FC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((ScreenCaptureWindow)target).SourceInitialized += this.Window_SourceInitialized;
				((ScreenCaptureWindow)target).Loaded += this.Window_Loaded;
				((ScreenCaptureWindow)target).Closing += this.Window_Closing;
				((ScreenCaptureWindow)target).Closed += this.Window_Closed;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400002C RID: 44
		private const uint MONITOR_DEFAULTTONEAREST = 2U;

		// Token: 0x0400002D RID: 45
		public IntPtr _captureHwnd;

		// Token: 0x0400002E RID: 46
		private Compositor _compositor;

		// Token: 0x0400002F RID: 47
		private CompositionTarget _windowTarget;

		// Token: 0x04000030 RID: 48
		private ContainerVisual _rootVisual;

		// Token: 0x04000031 RID: 49
		private BasicSampleApplication _sample;

		// Token: 0x04000032 RID: 50
		private Facetracker _faceTracking;

		// Token: 0x04000033 RID: 51
		private LocalEventPublisher _localPublisher;

		// Token: 0x04000034 RID: 52
		private FaceTrackingNotification _faceTrackingNotify;

		// Token: 0x04000035 RID: 53
		private LocalEventPublisher _eventPublisher;

		// Token: 0x04000036 RID: 54
		private IntPtr _fullscreenHwnd;

		// Token: 0x04000037 RID: 55
		private IntPtr _mainHwnd = IntPtr.Zero;

		// Token: 0x04000038 RID: 56
		private volatile bool _isInitialized;

		// Token: 0x04000039 RID: 57
		private volatile bool _isClosing;

		// Token: 0x0400003A RID: 58
		private volatile bool _deferredClose;

		// Token: 0x0400003B RID: 59
		private bool _contentLoaded;

		// Token: 0x02000064 RID: 100
		private struct STYLESTRUCT
		{
			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001D399 File Offset: 0x0001B599
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001D3A1 File Offset: 0x0001B5A1
			public int StyleOld { readonly get; set; }

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060003CA RID: 970 RVA: 0x0001D3AA File Offset: 0x0001B5AA
			// (set) Token: 0x060003CB RID: 971 RVA: 0x0001D3B2 File Offset: 0x0001B5B2
			public int StyleNew { readonly get; set; }
		}
	}
}
