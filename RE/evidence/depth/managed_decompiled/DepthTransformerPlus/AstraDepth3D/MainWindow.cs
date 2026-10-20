using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Markup;
using AiStereoKitWrapper;
using AstraDepth3D.BOEStereoRender;
using AstraDepth3D.CaptureSampleCore;
using AstraDepth3D.EventBus;
using AstraDepth3D.Properties;
using AstraDepth3D.Tracker;
using AstraDepth3D.WebSocketModule;
using AstraDepth3D.WindowHelper;
using AstraDepth3D.WinowHelper;
using AstraDepth3D.WMI;
using BOEReasterTest;
using Fleck;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Foundation;
using Windows.UI.Composition;

namespace AstraDepth3D
{
	// Token: 0x02000014 RID: 20
	public class MainWindow : Window, IComponentConnector
	{
		// Token: 0x060000B3 RID: 179
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		// Token: 0x060000B4 RID: 180
		[DllImport("user32")]
		private static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);

		// Token: 0x060000B5 RID: 181
		[DllImport("user32.dll")]
		private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

		// Token: 0x060000B6 RID: 182
		[DllImport("user32.dll")]
		private static extern IntPtr GetParent(IntPtr hWnd);

		// Token: 0x060000B7 RID: 183
		[DllImport("UiaccessTools.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool YLSetCurrentProcessGPUPriority(int priority);

		// Token: 0x060000B8 RID: 184 RVA: 0x00005B4C File Offset: 0x00003D4C
		public MainWindow()
		{
			this.InitializeComponent();
			this.InitLanguage();
			SystemEvents.UserPreferenceChanged += delegate(object sender, UserPreferenceChangedEventArgs e)
			{
				if (Settings.Default.Language == 0)
				{
					CultureInfo local_culture = CultureInfo.CurrentUICulture;
					NLogService.WriteLog("当前语言已变更：" + local_culture.Name);
					SettingsTools.SwitchLanguage(local_culture.Name);
				}
			};
			SystemEvents.DisplaySettingsChanged += this.OnDisplaySettingsChanged;
			SystemEvents.SessionSwitch += this.SystemEvents_SessionSwitch;
			this.OpenWebSocketServer();
			this._throttleTimer.Elapsed += this.ProcessHwndQueue3;
			this._throttleTimer.AutoReset = true;
			this._reorderHandler = new ReorderHandler(new Action(this.PauseCapture));
			this.displayDeviceList = this.GetStereoscopicDisplays();
			this.g_monitor = MonitorHelper.FindMonitor(this.displayDeviceList);
			if (MCUSdk.BOE_MCU_initSDK("./logs/", 2) == 0)
			{
				NLogService.WriteLog("BOE_MCU_init successed");
				this.CheckBOEMCU();
			}
			else
			{
				NLogService.WriteLog("BOE_MCU_init failed");
			}
			Task<JArray> task_game_white_list = Task.Run<JArray>(() => this.InitGameWhiteList());
			Task<bool> task = Task.Run<bool>(() => Task.FromResult<bool>(this.InitMCU()));
			this.weDelegate = new User32.WinEventDelegate(this.WinEventProc3);
			this.m_hhook = User32.SetWinEventHook(3U, 32779U, IntPtr.Zero, this.weDelegate, 0U, 0U, 2U);
			this.gameWhiteList = task_game_white_list.Result;
			GlobalStatusManager.Instance._iInterWaveStatus = LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle;
			Facetracker.Instance.OpenAsync();
			task.Wait(1000);
			if (!task.IsCompleted)
			{
				NLogService.WriteLog("读取uuid失败！");
				Task.Run(delegate()
				{
					this.InitMCU();
				}).LogExceptions();
			}
			NLogService.WriteLog("初始化完成");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005D6C File Offset: 0x00003F6C
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			this.UpdateCenterPosition();
			Screen currentScreen = this.GetCurrentScreen(this);
			this._lastScreenRect = currentScreen.Bounds;
			NLogService.WriteLog(string.Format("SourceInitialized: Screen Rect={0}", this._lastScreenRect));
			this.InitAiStereoEngine();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005DB4 File Offset: 0x00003FB4
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			((HwndSource)PresentationSource.FromVisual(this)).AddHook(new HwndSourceHook(this.MainHwndHook));
			WindowInteropHelper interopWindow = new WindowInteropHelper(this);
			this.mainHwnd = interopWindow.Handle;
			User32.SetWindowDisplayAffinity(this.mainHwnd, 17U);
			NLogService.WriteLog("设置主窗口反截屏");
			this._ScreenStateNotify = PowerSetting.RegisterPowerSettingNotification(this.mainHwnd, ref PowerSetting.GUID_CONSOLE_DISPLAY_STATE, 0);
			if (this._ScreenStateNotify == IntPtr.Zero)
			{
				int err = Marshal.GetLastWin32Error();
				NLogService.WriteLog(string.Format("❌ 注册电源状态通知失败! Win32 Error: {0}", err));
			}
			else
			{
				NLogService.WriteLog(string.Format("✅ 注册电源状态通知成功, Handle: {0}", this._ScreenStateNotify));
			}
			this._hotkey = new LowLevelHotkey();
			this.RegisterHotKey();
			this._hotkey.HotkeyPressed += delegate(object s, HotkeyPressedEventArgs args)
			{
				int wParam = 9527 + args.Id;
				try
				{
					this.SendMessageToSelf((IntPtr)wParam, (IntPtr)0);
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("发送消息失败：" + ex.Message);
				}
			};
			this.InitComposition();
			this.desktopHandle = APIWrapper.GetDesktopWindow();
			this.shellHandle = APIWrapper.GetShellWindow();
			if (MainWindow.YLSetCurrentProcessGPUPriority(1))
			{
				NLogService.WriteLog("Verified GPU Priority");
			}
			else
			{
				NLogService.WriteLog("Failed to set GPU priority with YLSetCurrentProcessGPUPriority. Try running as Administrator.");
			}
			if (this.TrayIcon.IsTaskbarIconCreated)
			{
				NLogService.WriteLog("TaskbarIcon Created!");
			}
			else
			{
				NLogService.WriteLog("TaskbarIcon Create failed!");
				System.Windows.Application.Current.Shutdown();
			}
			this.isReady = true;
			NLogService.WriteLog("Window Loaded!");
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005F08 File Offset: 0x00004108
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (!this._isShuttingDown)
			{
				e.Cancel = true;
			}
			HwndSource hwndSource = (HwndSource)PresentationSource.FromVisual(this);
			if (hwndSource != null)
			{
				hwndSource.RemoveHook(new HwndSourceHook(this.MainHwndHook));
			}
			WMIButtonReceiveEvent wmibuttonReceiveEvent = this.wMIButtonReceiveEvent;
			if (wmibuttonReceiveEvent != null)
			{
				wmibuttonReceiveEvent.Stop();
			}
			NLogService.WriteLog("Window_Closing!");
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005F64 File Offset: 0x00004164
		private void Window_Closed(object sender, EventArgs e)
		{
			BasicSampleApplication sample = this._sample;
			if (sample != null)
			{
				sample.Dispose();
			}
			ContainerVisual rootVisual = this._rootVisual;
			if (rootVisual != null)
			{
				rootVisual.Dispose();
			}
			Compositor compositor = this._compositor;
			if (compositor != null)
			{
				compositor.Dispose();
			}
			if (this.m_hhook != IntPtr.Zero)
			{
				User32.UnhookWinEvent(this.m_hhook);
			}
			IntPtr screenStateNotify = this._ScreenStateNotify;
			PowerSetting.UnregisterPowerSettingNotification(this._ScreenStateNotify);
			MCUSdk.BOE_MCU_closeMCU();
			MCUSdk.BOE_MCU_releaseSDK();
			NLogService.WriteLog("BOE_MCU关闭释放资源");
			this.UnRegisterHotKey();
			System.Timers.Timer throttleTimer = this._throttleTimer;
			if (throttleTimer != null)
			{
				throttleTimer.Close();
			}
			ReorderHandler reorderHandler = this._reorderHandler;
			if (reorderHandler != null)
			{
				reorderHandler.Dispose();
			}
			WebSocketFleck webSocketFleck = this.server;
			if (webSocketFleck != null)
			{
				webSocketFleck.Stop();
			}
			this.server = null;
			NLogService.WriteLog("server release!");
			Facetracker.Instance.Dispose();
			SystemEvents.SessionSwitch -= this.SystemEvents_SessionSwitch;
			SystemEvents.DisplaySettingsChanged -= this.OnDisplaySettingsChanged;
			YLStereoRenderCSharp.UnloadStereoEngineCSharp();
			NLogService.WriteLog("Window_Closed!");
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006070 File Offset: 0x00004270
		private IntPtr MainHwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 124 && (long)wParam == -20L)
			{
				MainWindow.STYLESTRUCT styleStruct = (MainWindow.STYLESTRUCT)Marshal.PtrToStructure(lParam, typeof(MainWindow.STYLESTRUCT));
				styleStruct.StyleNew |= 134742048;
				Marshal.StructureToPtr<MainWindow.STYLESTRUCT>(styleStruct, lParam, false);
				handled = true;
			}
			else if (msg == 536)
			{
				NLogService.WriteLog(string.Format("\ud83d\udce8 收到 WM_POWERBROADCAST, wParam={0:X}, lParam={1}", wParam.ToInt32(), lParam));
				if (wParam.ToInt32() == 32787)
				{
					PowerSetting.POWERBROADCAST_SETTING s = (PowerSetting.POWERBROADCAST_SETTING)Marshal.PtrToStructure(lParam, typeof(PowerSetting.POWERBROADCAST_SETTING));
					if (s.PowerSetting == PowerSetting.GUID_CONSOLE_DISPLAY_STATE)
					{
						NLogService.WriteLog("显示器事件: " + ((s.Data == 0) ? "关闭" : "开启"));
						if (s.Data == 0)
						{
							this.isScreenOn = false;
							if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
							{
								NLogService.WriteLog("显示器关闭，开始退出交织！");
								this.StopInterwave();
								this.InitFullscreenWindow();
							}
							MCUSdk.BOE_MCU_closeMCU();
						}
						else
						{
							this.isScreenOn = true;
							Facetracker.Instance.OpenAsync();
							Task.Run(delegate()
							{
								this.TryCloseRaster();
							}).LogExceptions();
						}
					}
				}
			}
			else if (msg == 537)
			{
				int eventType = wParam.ToInt32();
				if ((eventType == 32772 || eventType == 32768) && Marshal.PtrToStructure<MainWindow.DEV_BROADCAST_HDR>(lParam).dbch_devicetype == 3U)
				{
					bool isOn = eventType == 32768;
					NLogService.WriteLog("\ud83d\udda5️ 显示器" + (isOn ? "连接" : "断开") + " (WM_DEVICECHANGE)");
					if (!isOn)
					{
						if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
						{
							NLogService.WriteLog("\ud83d\udda5️显示器断开，开始退出交织!");
							this.StopInterwave();
							this.InitFullscreenWindow();
						}
						MCUSdk.BOE_MCU_closeMCU();
					}
				}
			}
			else if (msg != this.uCallBackMsg && (msg == 786 || (long)msg == 32769L))
			{
				if (wParam.ToInt32() == 9628)
				{
					NLogService.WriteLog("按下快捷键，准备开启或关闭交织");
					if (!this.isReady || !this.isAiStereoEngineEnable)
					{
						NLogService.WriteLog("MainWindow|Button Event：初始化未完成!\n");
						return IntPtr.Zero;
					}
					if (!this.isScreenOn)
					{
						NLogService.WriteLog("MainWindow|Button Event：屏幕未开启\n");
						return IntPtr.Zero;
					}
					if (!this.isSessionUnlock)
					{
						NLogService.WriteLog("MainWindow|Button Event：锁屏状态\n");
						return IntPtr.Zero;
					}
					base.Dispatcher.InvokeAsync(delegate()
					{
						try
						{
							if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Starting && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Started)
							{
								if (this.CheckBOEMCU())
								{
									this.StartInterwave();
								}
							}
							else
							{
								NLogService.WriteLog("测试快捷键，关闭交织\n");
								this.StopInterwave();
								this.InitFullscreenWindow();
							}
						}
						catch (Exception ex)
						{
							NLogService.WriteLog("HotKey Toggle Interwave Error: " + ex.Message);
						}
					});
				}
				else if (wParam.ToInt32() == 9629)
				{
					NLogService.WriteLog("切换格式快捷键：Ctrl+T");
					if (GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Started)
					{
						ScreenCaptureWindow screenCaptureWindow = this.captureRender;
						if (screenCaptureWindow != null)
						{
							screenCaptureWindow.FlipInterWaveType();
						}
					}
				}
				else if (wParam.ToInt32() == 9630)
				{
					NLogService.WriteLog("关闭进程快捷键：Ctrl+SHift+Alt+Q;");
					this._isShuttingDown = true;
					System.Windows.Application.Current.Shutdown();
				}
				else if (wParam.ToInt32() == 9631)
				{
					this.ButtonEventArrived();
				}
				else if (wParam.ToInt32() == 9632)
				{
					ScreenCaptureWindow screenCaptureWindow2 = this.captureRender;
					if (screenCaptureWindow2 != null)
					{
						screenCaptureWindow2.SetParax(0.05f);
					}
					Settings settingsDialog = this.SettingsDialog;
					if (settingsDialog != null)
					{
						settingsDialog.ResetSpatialDepthBar();
					}
				}
				else if (wParam.ToInt32() == 9633)
				{
					ScreenCaptureWindow screenCaptureWindow3 = this.captureRender;
					if (screenCaptureWindow3 != null)
					{
						screenCaptureWindow3.SetParax(-0.05f);
					}
					Settings settingsDialog2 = this.SettingsDialog;
					if (settingsDialog2 != null)
					{
						settingsDialog2.ResetSpatialDepthBar();
					}
				}
				else if (wParam.ToInt32() == 9634)
				{
					ScreenCaptureWindow screenCaptureWindow4 = this.captureRender;
					if (screenCaptureWindow4 != null)
					{
						screenCaptureWindow4.SetZeroPlane(0.05f);
					}
					Settings settingsDialog3 = this.SettingsDialog;
					if (settingsDialog3 != null)
					{
						settingsDialog3.ResetPopOutOfTheScreenBar();
					}
				}
				else if (wParam.ToInt32() == 9635)
				{
					ScreenCaptureWindow screenCaptureWindow5 = this.captureRender;
					if (screenCaptureWindow5 != null)
					{
						screenCaptureWindow5.SetZeroPlane(-0.05f);
					}
					Settings settingsDialog4 = this.SettingsDialog;
					if (settingsDialog4 != null)
					{
						settingsDialog4.ResetPopOutOfTheScreenBar();
					}
				}
				else if (wParam.ToInt32() == 100)
				{
					base.Dispatcher.InvokeAsync(delegate()
					{
						if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
						{
							NLogService.WriteLog("光栅发生错误，准备退出交织");
							this.StopInterwave();
							this.InitFullscreenWindow();
						}
					});
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000064BC File Offset: 0x000046BC
		private void InitComposition()
		{
			if (this._compositor == null || this._sample == null || this._rootVisual == null)
			{
				try
				{
					this._compositor = new Compositor();
					this._sample = new BasicSampleApplication(this._compositor);
					this._rootVisual = this._compositor.CreateContainerVisual();
					this._rootVisual.put_RelativeSizeAdjustment(Vector2.One);
					this._rootVisual.put_Size(new Vector2(0f, 0f));
					this._rootVisual.put_Offset(new Vector3(0f, 0f, 0f));
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("Compositor 初始化失败: " + ex.Message);
					System.Windows.Application.Current.Shutdown();
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006590 File Offset: 0x00004790
		private void RegisterStaticHotKey()
		{
			LowLevelHotkey hotkey = this._hotkey;
			if (hotkey != null && hotkey.Register(101, 70, 7))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 101 success!");
			}
			LowLevelHotkey hotkey2 = this._hotkey;
			if (hotkey2 != null && hotkey2.Register(102, 84, 7))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 102 success!");
			}
			LowLevelHotkey hotkey3 = this._hotkey;
			if (hotkey3 != null && hotkey3.Register(103, 81, 7))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 103 success!");
			}
			int StereoShortcutParaxAddModifier = 2;
			int StereoShortcutParaxAdd = 39;
			LowLevelHotkey hotkey4 = this._hotkey;
			if (hotkey4 != null && hotkey4.Register(105, StereoShortcutParaxAdd, StereoShortcutParaxAddModifier))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 105 success!");
			}
			int StereoShortcutParaxSubtractModifier = 2;
			int StereoShortcutParaxSubtract = 37;
			LowLevelHotkey hotkey5 = this._hotkey;
			if (hotkey5 != null && hotkey5.Register(106, StereoShortcutParaxSubtract, StereoShortcutParaxSubtractModifier))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 106 success!");
			}
			int StereoShortcutZPAddModifier = 1;
			int StereoShortcutZPAdd = 39;
			LowLevelHotkey hotkey6 = this._hotkey;
			if (hotkey6 != null && hotkey6.Register(107, StereoShortcutZPAdd, StereoShortcutZPAddModifier))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 107 success!");
			}
			int StereoShortcutZPSubtractModifier = 1;
			int StereoShortcutZPSubtract = 37;
			LowLevelHotkey hotkey7 = this._hotkey;
			if (hotkey7 != null && hotkey7.Register(108, StereoShortcutZPSubtract, StereoShortcutZPSubtractModifier))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 108 success!");
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000066AC File Offset: 0x000048AC
		private void RegisterHotKey()
		{
			int modifier = Settings.Default.StereoModifier;
			int mainKey = Settings.Default.StereoShortcutKey;
			LowLevelHotkey hotkey = this._hotkey;
			if (hotkey != null && hotkey.Register(104, mainKey, modifier))
			{
				NLogService.WriteLog("MainWindow|RegisterHotKey 100 success!");
			}
			this.RegisterStaticHotKey();
			int vk_modifier = SettingsTools.GetVirtualKeyFromModifierKeys(modifier);
			WebSocketFleck webSocketFleck = this.server;
			if (webSocketFleck == null)
			{
				return;
			}
			webSocketFleck.BroadcastMsg(TencentProtocolHandler.response_Query3DHotkey_msg(1, vk_modifier, mainKey));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006718 File Offset: 0x00004918
		private void UnRegisterHotKey()
		{
			LowLevelHotkey hotkey = this._hotkey;
			if (hotkey != null)
			{
				hotkey.Unregister(101);
			}
			LowLevelHotkey hotkey2 = this._hotkey;
			if (hotkey2 != null)
			{
				hotkey2.Unregister(102);
			}
			LowLevelHotkey hotkey3 = this._hotkey;
			if (hotkey3 != null)
			{
				hotkey3.Unregister(103);
			}
			LowLevelHotkey hotkey4 = this._hotkey;
			if (hotkey4 != null)
			{
				hotkey4.Unregister(104);
			}
			LowLevelHotkey hotkey5 = this._hotkey;
			if (hotkey5 != null)
			{
				hotkey5.Unregister(105);
			}
			LowLevelHotkey hotkey6 = this._hotkey;
			if (hotkey6 != null)
			{
				hotkey6.Unregister(106);
			}
			LowLevelHotkey hotkey7 = this._hotkey;
			if (hotkey7 != null)
			{
				hotkey7.Unregister(107);
			}
			LowLevelHotkey hotkey8 = this._hotkey;
			if (hotkey8 == null)
			{
				return;
			}
			hotkey8.Unregister(108);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000067BC File Offset: 0x000049BC
		private void ResetRegisterHotKey(int what)
		{
			if (what == 0)
			{
				this.UnRegisterHotKey();
				return;
			}
			this.RegisterHotKey();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000067D0 File Offset: 0x000049D0
		public void SetTransparentHitThrough()
		{
			User32.SetWindowLongPtr(this.mainHwnd, -20, (IntPtr)((int)((long)User32.GetWindowLongPtr(this.mainHwnd, -20) | 32L | 134217728L)));
			User32.SetWindowDisplayAffinity(this.mainHwnd, 17U);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000681C File Offset: 0x00004A1C
		private Task TryCloseRaster()
		{
			MainWindow.<TryCloseRaster>d__63 <TryCloseRaster>d__;
			<TryCloseRaster>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<TryCloseRaster>d__.<>1__state = -1;
			<TryCloseRaster>d__.<>t__builder.Start<MainWindow.<TryCloseRaster>d__63>(ref <TryCloseRaster>d__);
			return <TryCloseRaster>d__.<>t__builder.Task;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006858 File Offset: 0x00004A58
		private bool CheckBOEMCU()
		{
			int usbState = -1;
			if (MCUSdk.BOE_MCU_openMCU() == 0)
			{
				usbState = MCUSdk.BOE_MCU_getUSBState();
				if (!this.isMCUInitSuccess)
				{
					int hr = this.UpdateMCUMat();
					this.isMCUInitSuccess = (hr == 0);
					NLogService.WriteLog(string.Format("BOE_MCU_updateMat {0}", this.isMCUInitSuccess));
				}
			}
			MCUSdk.BOE_MCU_closeMCU();
			NLogService.WriteLog(string.Format("BOE_MCU_getUSBState: {0}", usbState));
			bool isUSBConnected = usbState == 1 || usbState == 2;
			if (isUSBConnected)
			{
				return true;
			}
			if (this.g_monitor != null)
			{
				MonitorGuide.ShowMessage("设备离线", this.g_monitor.ModelName, "请检查显示器的USB是否是否连接到电脑？", null);
				return isUSBConnected;
			}
			NLogService.WriteLog("未找到匹配的显示器，跳过BOE MCU检查");
			return true;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006910 File Offset: 0x00004B10
		private int UpdateMCUMat()
		{
			IniFile boeMcuMatIniFile = new IniFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "eyetracking/boe_mcu_mat.ini"));
			double[] leftCameraMat = new double[9];
			int hr = MCUSdk.BOE_MCU_getLeftCameraMatParam(leftCameraMat);
			if (hr != 0)
			{
				NLogService.WriteLog("BOE_MCU_getLeftCameraMatParam failed");
				return hr;
			}
			try
			{
				boeMcuMatIniFile.Write("calibration_data", "E1", string.Join<double>(",", leftCameraMat));
				NLogService.WriteLog("update leftCameraMat values successed.");
			}
			catch
			{
			}
			double[] rightCameraMat = new double[9];
			hr = MCUSdk.BOE_MCU_getRightCameraMatParam(rightCameraMat);
			if (hr != 0)
			{
				NLogService.WriteLog("BOE_MCU_getRightCameraMatParam failed");
				return hr;
			}
			try
			{
				boeMcuMatIniFile.Write("calibration_data", "E2", string.Join<double>(",", rightCameraMat));
				NLogService.WriteLog("update rightCameraMat values successed.");
			}
			catch
			{
			}
			double[] stereoCameraDist = new double[10];
			hr = MCUSdk.BOE_MCU_getStereoCameraDistParam(stereoCameraDist);
			if (hr != 0)
			{
				NLogService.WriteLog("BOE_MCU_getStereoCameraDistParam failed");
				return hr;
			}
			try
			{
				boeMcuMatIniFile.Write("calibration_data", "E3", string.Join<double>(",", stereoCameraDist));
				NLogService.WriteLog("update stereoCameraDist values successed.");
			}
			catch
			{
			}
			return 0;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006A44 File Offset: 0x00004C44
		private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
		{
			SessionSwitchReason reason = e.Reason;
			if (reason != SessionSwitchReason.SessionLock)
			{
				if (reason == SessionSwitchReason.SessionUnlock)
				{
					this.isSessionUnlock = true;
					return;
				}
			}
			else
			{
				this.isSessionUnlock = false;
				if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
				{
					NLogService.WriteLog("MainWindow|用户锁屏，开始退出交织");
					this.StopInterwave();
					this.InitFullscreenWindow();
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006AA4 File Offset: 0x00004CA4
		private void OnDisplaySettingsChanged(object sender, EventArgs e)
		{
			MonitorFullInfo monitor = MonitorHelper.FindMonitor(this.displayDeviceList);
			if (this.g_monitor == null || this.g_monitor != monitor)
			{
				this.g_monitor = monitor;
				string str = "当前使用的显示器: ";
				MonitorFullInfo monitorFullInfo = this.g_monitor;
				NLogService.WriteLog(str + (((monitorFullInfo != null) ? monitorFullInfo.ModelName : null) ?? "主显示器"));
			}
			base.Dispatcher.InvokeAsync(delegate()
			{
				this.UpdateCenterPosition();
				Rectangle screenRect = this.GetCurrentScreen(this).Bounds;
				NLogService.WriteLog(string.Format("屏幕分辨率为：{0}*{1}", screenRect.Width, screenRect.Height));
				ScreenRotationHelper.DisplayOrientation newOrientation = ScreenRotationHelper.GetScreenOrientation();
				NLogService.WriteLog(string.Format("屏幕方向为：{0}", newOrientation));
				if (newOrientation != ScreenRotationHelper.DisplayOrientation.Default)
				{
					NLogService.WriteLine(string.Format("屏幕方向已更改为：{0}", newOrientation));
					if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						NLogService.WriteLog("屏幕方向改变，开始退出交织！");
						this.StopInterwave();
						this.InitFullscreenWindow();
					}
				}
				if (this._lastScreenRect != screenRect)
				{
					NLogService.WriteLine(string.Format("屏幕分辨率已更改为：{0}*{1}=>{2}*{3}", new object[]
					{
						this._lastScreenRect.Width,
						this._lastScreenRect.Height,
						screenRect.Width,
						screenRect.Height
					}));
					this._lastScreenRect = screenRect;
					if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						NLogService.WriteLog("屏幕分辨率改变，开始退出交织！");
						this.StopInterwave();
						this.InitFullscreenWindow();
					}
				}
			});
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006B22 File Offset: 0x00004D22
		protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
		{
			base.OnDpiChanged(oldDpi, newDpi);
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (!base.IsLoaded)
				{
					return;
				}
				this.UpdateCenterPosition();
			}), Array.Empty<object>());
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006B49 File Offset: 0x00004D49
		private void ResetInferDevice()
		{
			this.InitAiStereoEngine();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006B54 File Offset: 0x00004D54
		private void InitAiStereoEngine()
		{
			this.isAiStereoEngineEnable = false;
			NLogService.WriteLog("Initializing AI Stereo Engine,StopInterwave");
			this.StopInterwave();
			this.InitFullscreenWindow();
			string device = Settings.Default.InferenceDevice;
			Task.Factory.StartNew(delegate()
			{
				object engineLock = MainWindow._engineLock;
				lock (engineLock)
				{
					try
					{
						YLStereoRenderCSharp.UnloadStereoEngineCSharp();
						NLogService.WriteLog("load ai model: " + device);
						YLStereoRenderCSharp.LoadStereoEngineCSharp(null, device);
						this.isAiStereoEngineEnable = true;
					}
					catch (Exception ex)
					{
						NLogService.WriteLog("InitAiStereoEngine Error: " + ex.Message);
						this.isAiStereoEngineEnable = false;
					}
				}
			}, TaskCreationOptions.LongRunning);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006BB8 File Offset: 0x00004DB8
		private bool InitMCU()
		{
			string uuid = RegistrySingleFactory.ReadRegistry(RegistrySingleFactory.valueName);
			NLogService.WriteLog("注册表UUID：" + uuid);
			if (MCUSdk.BOE_MCU_openMCU() != 0)
			{
				NLogService.WriteLog("BOE_MCU_openMCU failed");
			}
			StringBuilder buffer = new StringBuilder(256);
			if (MCUSdk.BOE_MCU_getMCUVersion(buffer, 256) == 0)
			{
				NLogService.WriteLog("MCUVersion:" + buffer.ToString());
			}
			else
			{
				NLogService.WriteLog("BOE_MCU_getMCUVersion failed");
			}
			if (MCUSdk.BOE_MCU_getSNCode(buffer, 256) == 0)
			{
				string id = buffer.ToString();
				if ((string.IsNullOrEmpty(uuid) || uuid != id) && id != "000000000000000000000000" && !string.IsNullOrEmpty(id))
				{
					RegistrySingleFactory.WriteRegistry(RegistrySingleFactory.valueName, id);
					GlobalStatusManager.instance._uuid = id;
					NLogService.WriteLog("写入注册表新设备UUID：" + id);
				}
				else
				{
					GlobalStatusManager.instance._uuid = uuid;
					NLogService.WriteLog("注册表获取设备UUID:" + uuid);
				}
			}
			else
			{
				NLogService.WriteLog("获取设备UUID失败,重试");
			}
			MCUSdk.BOE_MCU_closeMCU();
			return true;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006CB9 File Offset: 0x00004EB9
		private void Timer_Tick(object sender, EventArgs e)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006CBC File Offset: 0x00004EBC
		public void StartInterwave()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.InvokeAsync(delegate()
				{
					this.StartInterwave();
				});
				return;
			}
			if (!this.checkLicense())
			{
				NLogService.WriteLog("License Check Failed,退出交织");
				this.StopInterwave();
				MessageWindow.ShowMessage("Illegal License", base.FindResource("error_illegal_license").ToString(), null);
				return;
			}
			this.InitComposition();
			object @lock = this._lock;
			lock (@lock)
			{
				if (GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Starting || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Started)
				{
					return;
				}
				ReorderHandler reorderHandler = this._reorderHandler;
				if (reorderHandler != null)
				{
					reorderHandler.SetFullScreenHwndHandle(this.fullScreenHwndHandle, this._lastScreenRect);
				}
				GlobalStatusManager.Instance._iInterWaveStatus = LocalEnumeration.InterWaveStatus.InterWaveStatus_Starting;
			}
			try
			{
				Facetracker.Instance.StartCamera();
				GlobalStatusManager.Instance._bInterWaveRet = true;
				WebSocketFleck webSocketFleck = this.server;
				if (webSocketFleck != null)
				{
					webSocketFleck.broadcast_GlobalOpen_status(1);
				}
				this.BeginCapture();
				@lock = this._lock;
				lock (@lock)
				{
					GlobalStatusManager.Instance._iInterWaveStatus = LocalEnumeration.InterWaveStatus.InterWaveStatus_Started;
				}
				IntPtr prdWin = this.PrdWin;
				if (this.PrdWin != IntPtr.Zero)
				{
					if (User32.IsWindow(this.PrdWin))
					{
						User32.RECT prdRect = default(User32.RECT);
						bool hr = User32.GetWindowRect(this.PrdWin, ref prdRect);
						if (hr)
						{
							if (prdRect.Right > 2 && prdRect.Bottom > 2)
							{
								NLogService.WriteLog(string.Format("状态跟踪:设置Prd{0}\n", hr));
							}
							else
							{
								this.PrdWin = IntPtr.Zero;
								User32.SetWindowPos(this.mainHwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
							}
						}
						else
						{
							this.PrdWin = IntPtr.Zero;
							User32.SetWindowPos(this.mainHwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						}
					}
				}
				else
				{
					User32.SetWindowPos(this.mainHwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
				}
			}
			catch (Exception ex)
			{
				@lock = this._lock;
				lock (@lock)
				{
					GlobalStatusManager.Instance._iInterWaveStatus = LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle;
				}
				NLogService.WriteLog("StartInterwave failed: " + ex.Message);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006F68 File Offset: 0x00005168
		public void StopInterwave()
		{
			NLogService.WriteLog("状态跟踪:StopInterwave called\n");
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.InvokeAsync(delegate()
				{
					this.StopInterwave();
				});
				return;
			}
			NLogService.WriteLog("状态跟踪:StopInterwave\n");
			object @lock = this._lock;
			lock (@lock)
			{
				if (GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
				{
					return;
				}
				ReorderHandler reorderHandler = this._reorderHandler;
				if (reorderHandler != null)
				{
					reorderHandler.Reset();
				}
				GlobalStatusManager.Instance._iInterWaveStatus = LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping;
			}
			try
			{
				Facetracker.Instance.StopCamera();
				this.StopCapture();
				GlobalStatusManager.Instance._iInterWaveType = LocalEnumeration.InterWaveType.InterWave_None;
				GlobalStatusManager.Instance._bInterWaveRet = false;
				WebSocketFleck webSocketFleck = this.server;
				if (webSocketFleck != null)
				{
					webSocketFleck.broadcast_GlobalOpen_status(0);
				}
			}
			finally
			{
				@lock = this._lock;
				lock (@lock)
				{
					GlobalStatusManager.Instance._iInterWaveStatus = LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle;
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000070A0 File Offset: 0x000052A0
		public void BeginCapture()
		{
			try
			{
				NLogService.WriteLog("状态跟踪:BeginCapture\n");
				this.SetTransparentHitThrough();
				if (this.captureRender != null)
				{
					this.captureRender.CloseCaptureWindow();
					NLogService.WriteLog("关闭已存在的CaptureWindow!\n");
				}
				this.captureRender = new ScreenCaptureWindow(this._compositor, this._rootVisual, this._sample);
				this.captureRender.Show();
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("BeginCapture失败: " + ex.Message + "\n");
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007134 File Offset: 0x00005334
		public void StopCapture()
		{
			NLogService.WriteLog("状态跟踪:开始停止捕获\n");
			if (this.captureRender != null)
			{
				this.captureRender.CloseCaptureWindow();
				this.captureRender = null;
				return;
			}
			NLogService.WriteLog("状态跟踪:没有正在捕获的窗口\n");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007168 File Offset: 0x00005368
		public void ForceOpen()
		{
			if (!this.isReady || !this.isAiStereoEngineEnable)
			{
				NLogService.WriteLog("MainWindow|Button Event：初始化未完成!\n");
				return;
			}
			if (!this.isScreenOn)
			{
				NLogService.WriteLog("MainWindow|Button Event：屏幕未开启\n");
				return;
			}
			if (!this.isSessionUnlock)
			{
				NLogService.WriteLog("MainWindow|Button Event：锁屏状态\n");
				return;
			}
			base.Dispatcher.InvokeAsync(delegate()
			{
				if (GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Starting || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Started)
				{
					NLogService.WriteLog("状态跟踪:ForceOpen-Close\n");
					this.StopInterwave();
					this.InitFullscreenWindow();
					return;
				}
				if (!this.CheckBOEMCU())
				{
					return;
				}
				this.StartInterwave();
			});
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000071D0 File Offset: 0x000053D0
		private void OpenWMIButtonServer()
		{
			if (this.wMIButtonReceiveEvent == null)
			{
				this.wMIButtonReceiveEvent = new WMIButtonReceiveEvent();
				bool ret = this.wMIButtonReceiveEvent.Start(new EventArrivedEventHandler(this.ButtonEventArrived));
				NLogService.WriteLog(string.Format("MainWindow|launcher WMI reciever:{0}", ret));
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007220 File Offset: 0x00005420
		public void ButtonEventArrived(object sender, EventArrivedEventArgs e)
		{
			NLogService.WriteLog("MainWindow|Button Event:用户拨动开关\n");
			if (!this.isReady)
			{
				NLogService.WriteLog("MainWindow|Button Event：初始化未完成\n");
				return;
			}
			if (!this.isScreenOn)
			{
				NLogService.WriteLog("MainWindow|Button Event：屏幕未开启\n");
				return;
			}
			if (!this.isSessionUnlock)
			{
				NLogService.WriteLog("MainWindow|Button Event：锁屏状态\n");
				return;
			}
			NLogService.WriteLog(string.Format("MainWindow|Button Event|交织状态：{0}", GlobalStatusManager.Instance._iInterWaveStatus));
			this.InvokeWithTimeoutAsync(delegate
			{
				if (GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Starting || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Started)
				{
					NLogService.WriteLog("状态跟踪:交织处于开启状态，开始关闭交织\n");
					this.StopInterwave();
					this.InitFullscreenWindow();
					return;
				}
				if (!this.CheckBOEMCU())
				{
					return;
				}
				NLogService.WriteLog("状态跟踪:交织处于关闭状态，开始打开交织\n");
				this.CheckForegroundExeAndOpen();
			}, TimeSpan.FromMilliseconds(2000.0));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000072B4 File Offset: 0x000054B4
		public void ButtonEventArrived()
		{
			NLogService.WriteLog("MainWindow|Button Event:用户按下2D转3D快捷键");
			if (!this.isReady || !this.isAiStereoEngineEnable)
			{
				NLogService.WriteLog("MainWindow|Button Event：初始化未完成\n");
				return;
			}
			if (!this.isScreenOn)
			{
				NLogService.WriteLog("MainWindow|Button Event：屏幕未开启\n");
				return;
			}
			if (!this.isSessionUnlock)
			{
				NLogService.WriteLog("MainWindow|Button Event：锁屏状态\n");
				return;
			}
			NLogService.WriteLog(string.Format("MainWindow|Button Event|交织状态：{0}", GlobalStatusManager.Instance._iInterWaveStatus));
			this.InvokeWithTimeoutAsync(delegate
			{
				if (GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Starting || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Started)
				{
					NLogService.WriteLog("状态跟踪:交织处于开启状态，开始关闭交织\n");
					this.StopInterwave();
					this.InitFullscreenWindow();
					return;
				}
				if (!this.CheckBOEMCU())
				{
					return;
				}
				NLogService.WriteLog("状态跟踪:交织处于关闭状态，开始打开交织\n");
				this.CheckForegroundExeAndOpen();
			}, TimeSpan.FromMilliseconds(2000.0));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007350 File Offset: 0x00005550
		private void OpenWebSocketServer()
		{
			if (this.serverTask == null)
			{
				NLogService.WriteLog("WebSocketFleck|服务开始启动");
				this.server = new WebSocketFleck();
				this.server.set_GlobalOpen_Notify_callback(new WebSocketFleck.GlobalOpen_Notify_Delegate(this.GlobalOpen_Notify_callback));
				this.server.set_Set3DHotkey_Notify_Delegate_callback(new WebSocketFleck.Set3DHotkey_Notify_Delegate(this.Set3DHotkey));
				this.server.set_Query3DHotkey_Notify_Delegate_callback(new WebSocketFleck.Query3DHotkey_Notify_Delegate(this.Query3DHotkey));
				this.server.set_YLClientMsg_Delegate_callback(new WebSocketFleck.YLClientMsg_Delegate(this.YLClientMsg_callback));
				this.serverTask = this.server.Start();
				NLogService.WriteLog("MainWindow|launcher WebSocketServer success");
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000073F4 File Offset: 0x000055F4
		private void YLClientMsg_callback(int cmd_type, float value)
		{
			if (!this.isReady)
			{
				NLogService.WriteLog("MainWindow|YLClientMsg_callback：初始化未完成\n");
				return;
			}
			switch (cmd_type)
			{
			case 1101:
				base.Dispatcher.Invoke(delegate()
				{
					if (value == 1f)
					{
						GlobalStatusManager.Instance._calibration_a = 1f;
						GlobalStatusManager.Instance._calibration_b = 0f;
						if (GlobalStatusManager.instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle || this.captureRender == null)
						{
							this.StartInterwave();
							ScreenCaptureWindow screenCaptureWindow = this.captureRender;
							if (screenCaptureWindow != null)
							{
								screenCaptureWindow.ResetCurrentInterwaveType(LocalEnumeration.InterWaveType.InterWave_SBS);
							}
							NLogService.WriteLine("MainWindow|YLClientMsg_callback|1101");
							return;
						}
					}
					else
					{
						NLogService.WriteLog("MainWindow|YLClientMsg_callback close");
						this.StopInterwave();
						this.InitFullscreenWindow();
					}
				});
				return;
			case 1102:
				GlobalStatusManager.Instance._calibration_a = value;
				NLogService.WriteLine(string.Format("MainWindow|YLClientMsg_callback|1102|offsetX:{0}", value));
				return;
			case 1103:
				break;
			case 1104:
				GlobalStatusManager.Instance._calibration_b = value;
				NLogService.WriteLine(string.Format("MainWindow|YLClientMsg_callback|1102|offsetX:{0}", value));
				break;
			default:
				return;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000074B8 File Offset: 0x000056B8
		private int YLMcuVlue_callback(float value0, float value1, float value2, float value3)
		{
			int result = LoadBoeMCUDll.WriteCalidataToMCU(value0, value1, value2, value3);
			if (result != 0)
			{
				NLogService.WriteLog(string.Format(" write failed:MCUValue = {0}, {1}, {2}, {3}", new object[]
				{
					value0,
					value1,
					value2,
					value3
				}));
			}
			else
			{
				NLogService.WriteLog(string.Format(" write successed:MCUValue = {0}, {1}, {2}, {3}", new object[]
				{
					value0,
					value1,
					value2,
					value3
				}));
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000754C File Offset: 0x0000574C
		private void GlobalOpen_Notify_callback(int nGlobalOpen)
		{
			NLogService.WriteLog(string.Format("MainWindow|Receive GlobalOpen cmd:{0}\n", nGlobalOpen));
			if (!this.isReady)
			{
				NLogService.WriteLog("MainWindow|GlobalOpen_Notify_callback：初始化未完成\n");
				return;
			}
			if (!this.isScreenOn)
			{
				NLogService.WriteLog("MainWindow|GlobalOpen_Notify_callback：屏幕未开启\n");
				return;
			}
			if (!this.isSessionUnlock)
			{
				NLogService.WriteLog("MainWindow|GlobalOpen_Notify_callback：锁屏状态\n");
				return;
			}
			this.InvokeWithTimeoutAsync(delegate
			{
				if (nGlobalOpen == 0)
				{
					NLogService.WriteLog("MainWindow|Receive GlobalOpen 关闭交织\n");
					this.StopInterwave();
					this.InitFullscreenWindow();
					return;
				}
				if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Started && !this.CheckBOEMCU())
				{
					return;
				}
				this.CheckForegroundExeAndOpen2();
			}, TimeSpan.FromMilliseconds(2000.0));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000075E4 File Offset: 0x000057E4
		private void Set3DHotkey(IWebSocketConnection sock, int KeyId, int vk_Modifier, int Key, string Desc)
		{
			if (KeyId != 1)
			{
				NLogService.WriteLog(string.Format("MainWindow|Set3DHotkey: Invalid hotkey parameters. KeyId: {0}", KeyId));
				return;
			}
			int modifier = SettingsTools.GetModifierKeysFromVirtualKey(vk_Modifier);
			if ((modifier != 1 && modifier != 2 && modifier != 9) || Key == 0)
			{
				WebSocketFleck webSocketFleck = this.server;
				if (webSocketFleck != null)
				{
					webSocketFleck.Response(sock, TencentProtocolHandler.response_Set3DHotkey_msg(201, "Invalid hotkey"));
				}
				NLogService.WriteLog(string.Format("MainWindow|Set3DHotkey: Invalid hotkey parameters. KeyId: {0}, Modifier: {1}, Key: {2}", KeyId, modifier, Key));
				return;
			}
			if (!this.isLowLevelHotkeyCanbeRegister(modifier, Key) || !SettingsTools.isShortcutCanbeRegister(modifier, Key))
			{
				WebSocketFleck webSocketFleck2 = this.server;
				if (webSocketFleck2 != null)
				{
					webSocketFleck2.Response(sock, TencentProtocolHandler.response_Set3DHotkey_msg(201, "This hotkey has been registered"));
				}
				NLogService.WriteLog(string.Format("MainWindow|Set3DHotkey: This hotkey has been registered. KeyId: {0}, Modifier: {1}, Key: {2}", KeyId, modifier, Key));
				return;
			}
			switch (KeyId)
			{
			case 1:
				Settings.Default.StereoModifier = modifier;
				Settings.Default.StereoShortcutKey = Key;
				break;
			case 2:
				Settings.Default.StereoShortcutParaxAddModifier = modifier;
				Settings.Default.StereoShortcutParaxAdd = Key;
				break;
			case 3:
				Settings.Default.StereoShortcutParaxSubtractModifier = modifier;
				Settings.Default.StereoShortcutParaxSubtract = Key;
				break;
			case 4:
				Settings.Default.StereoShortcutZPAddModifier = modifier;
				Settings.Default.StereoShortcutZPAdd = Key;
				break;
			case 5:
				Settings.Default.StereoShortcutZPSubtractModifier = modifier;
				Settings.Default.StereoShortcutZPSubtract = Key;
				break;
			default:
				return;
			}
			Settings.Default.Save();
			this.ResetRegisterHotKey(1);
			base.Dispatcher.InvokeAsync(delegate()
			{
				Settings settingsDialog = this.SettingsDialog;
				if (settingsDialog == null)
				{
					return;
				}
				settingsDialog.RestShortcut();
			});
			string msg = TencentProtocolHandler.response_Set3DHotkey_msg(200, "ok");
			NLogService.WriteLog("Set3DHotkey response:" + msg);
			WebSocketFleck webSocketFleck3 = this.server;
			if (webSocketFleck3 == null)
			{
				return;
			}
			webSocketFleck3.Response(sock, msg);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000077B0 File Offset: 0x000059B0
		private void Query3DHotkey(IWebSocketConnection sock, int KeyId)
		{
			if (KeyId != 1)
			{
				NLogService.WriteLog(string.Format("MainWindow|Set3DHotkey: Invalid hotkey parameters. KeyId: {0}", KeyId));
				return;
			}
			int stereoModifier = Settings.Default.StereoModifier;
			int mainKey = Settings.Default.StereoShortcutKey;
			int vk_modifier = SettingsTools.GetVirtualKeyFromModifierKeys(stereoModifier);
			WebSocketFleck webSocketFleck = this.server;
			if (webSocketFleck == null)
			{
				return;
			}
			webSocketFleck.Response(sock, TencentProtocolHandler.response_Query3DHotkey_msg(KeyId, vk_modifier, mainKey));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000780C File Offset: 0x00005A0C
		private void CheckForegroundExeAndOpen()
		{
			MainWindow.<CheckForegroundExeAndOpen>d__87 <CheckForegroundExeAndOpen>d__;
			<CheckForegroundExeAndOpen>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckForegroundExeAndOpen>d__.<>4__this = this;
			<CheckForegroundExeAndOpen>d__.<>1__state = -1;
			<CheckForegroundExeAndOpen>d__.<>t__builder.Start<MainWindow.<CheckForegroundExeAndOpen>d__87>(ref <CheckForegroundExeAndOpen>d__);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007844 File Offset: 0x00005A44
		private void CheckForegroundExeAndOpen2()
		{
			MainWindow.<CheckForegroundExeAndOpen2>d__88 <CheckForegroundExeAndOpen2>d__;
			<CheckForegroundExeAndOpen2>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckForegroundExeAndOpen2>d__.<>4__this = this;
			<CheckForegroundExeAndOpen2>d__.<>1__state = -1;
			<CheckForegroundExeAndOpen2>d__.<>t__builder.Start<MainWindow.<CheckForegroundExeAndOpen2>d__88>(ref <CheckForegroundExeAndOpen2>d__);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000787C File Offset: 0x00005A7C
		private JArray InitGameWhiteList()
		{
			JArray result;
			try
			{
				string jsonPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\gamelist.json";
				NLogService.WriteLine("MainWindow|InitGameWhiteList path:" + jsonPath);
				if (!File.Exists(jsonPath))
				{
					NLogService.WriteLog("MainWindow|InitGameWhiteList 文件不存在:" + jsonPath);
					result = null;
				}
				else
				{
					string content = JsonTools.GetJsonFile(jsonPath);
					if (string.IsNullOrEmpty(content))
					{
						NLogService.WriteLog("MainWindow|InitGameWhiteList JSON文件内容为空:" + jsonPath);
						result = null;
					}
					else
					{
						JArray _gameWhiteList = JObject.Parse(content)["games"] as JArray;
						if (_gameWhiteList == null)
						{
							NLogService.WriteLog("MainWindow|InitGameWhiteList 未找到games节点或节点不是数组格式");
							result = null;
						}
						else
						{
							foreach (JToken jtoken in _gameWhiteList)
							{
								JObject jobject = (JObject)jtoken;
								JToken jtoken2 = jobject["title"];
								string gameName = ((jtoken2 != null) ? jtoken2.ToString() : null) ?? "";
								JToken jtoken3 = jobject["path"];
								if (jtoken3 != null)
								{
									jtoken3.ToString();
								}
								NLogService.WriteLog("MainWindow|InitGameWhiteList:" + gameName);
							}
							result = _gameWhiteList;
						}
					}
				}
			}
			catch (JsonException ex)
			{
				NLogService.WriteLog("MainWindow|InitGameWhiteList JSON解析失败: " + ex.Message);
				result = null;
			}
			catch (Exception ex2)
			{
				NLogService.WriteLog("MainWindow|InitGameWhiteList 发生异常: " + ex2.Message + "\n" + ex2.StackTrace);
				result = null;
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007A34 File Offset: 0x00005C34
		private JArray GetStereoscopicDisplays()
		{
			JArray result;
			try
			{
				string jsonPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\StereoscopicDisplayList.json";
				if (!File.Exists(jsonPath))
				{
					NLogService.WriteLog("MainWindow|GetStereoscopicDisplays 文件不存在:" + jsonPath);
					result = null;
				}
				else
				{
					string content = JsonTools.GetJsonFile(jsonPath);
					if (string.IsNullOrEmpty(content))
					{
						NLogService.WriteLog("MainWindow|GetStereoscopicDisplays JSON文件内容为空:" + jsonPath);
						result = null;
					}
					else
					{
						JArray deviceList = JObject.Parse(content)["devices"] as JArray;
						if (deviceList == null)
						{
							NLogService.WriteLog("MainWindow|GetStereoscopicDisplays 未找到devices节点");
							result = null;
						}
						else
						{
							foreach (JToken jtoken in deviceList)
							{
								JObject jobject = (JObject)jtoken;
								JToken jtoken2 = jobject["Manufacturer"];
								string manufacturer = ((jtoken2 != null) ? jtoken2.ToString() : null) ?? "";
								JToken jtoken3 = jobject["ModelName"];
								string modelName = ((jtoken3 != null) ? jtoken3.ToString() : null) ?? "";
								JToken jtoken4 = jobject["Lens"];
								string lens = ((jtoken4 != null) ? jtoken4.ToString() : null) ?? "";
								NLogService.WriteLog(string.Concat(new string[]
								{
									"MainWindow|GetStereoscopicDisplays: Manufacturer=",
									manufacturer,
									", ModelName=",
									modelName,
									", Lens=",
									lens
								}));
							}
							result = deviceList;
						}
					}
				}
			}
			catch (JsonException ex)
			{
				NLogService.WriteLog("MainWindow|GetStereoscopicDisplays JSON解析失败: " + ex.Message);
				result = null;
			}
			catch (Exception ex2)
			{
				NLogService.WriteLog("MainWindow|GetStereoscopicDisplays 发生异常: " + ex2.Message + "\n" + ex2.StackTrace);
				result = null;
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007C34 File Offset: 0x00005E34
		private int GetGameType(string exeName, string processPath)
		{
			if (this.gameWhiteList != null && !string.IsNullOrEmpty(exeName) && !string.IsNullOrEmpty(processPath))
			{
				int lastIndex = processPath.LastIndexOf("\\");
				int lastIndex2 = processPath.LastIndexOf("\\", lastIndex - 1);
				if (lastIndex == -1 || lastIndex2 == -1)
				{
					return 0;
				}
				string game_path = processPath.Substring(lastIndex2 + 1);
				NLogService.WriteLine("MainWindow|GetGameType|game:" + game_path + "，_windowTarget:" + exeName);
				JToken _jobject = (from t in this.gameWhiteList
				where t["title"].ToString() == exeName
				select t).FirstOrDefault<JToken>();
				if (_jobject != null)
				{
					NLogService.WriteLine(string.Format("MainWindow|GetGameType|find game:{0}", _jobject["path"]));
					if (game_path == _jobject["path"].ToString())
					{
						if (_jobject["type"].ToString() == "2")
						{
							NLogService.WriteLog(string.Format("MainWindow|GetGameType|{0},TYPE:{1} is in whitelist,set stereomode to ai", _jobject["path"], _jobject["type"].ToString()));
							return 2;
						}
						NLogService.WriteLog(string.Format("MainWindow|GetGameType|{0} is in whitelist,set stereomode to sbs", _jobject["path"]));
						return 1;
					}
				}
			}
			string fullName = Directory.GetParent(processPath).FullName;
			string dxgi_path = Path.Combine(fullName, "dxgi.dll");
			string inlab_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "inlab\\game.lic");
			string game_lic = Path.Combine(fullName, "game.lic");
			if (File.Exists(dxgi_path) && !dxgi_path.Equals("C:\\Windows\\System32\\dxgi.dll") && (File.Exists(game_lic) || File.Exists(inlab_file)))
			{
				NLogService.WriteLog("MainWindow|GetGameType|路径下存在dxgi.dll,game.lic");
				return 1;
			}
			return 0;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007DF0 File Offset: 0x00005FF0
		private ValueTuple<IntPtr, IntPtr> GetForegroundWindow()
		{
			this.desktopHandle = APIWrapper.GetDesktopWindow();
			this.shellHandle = APIWrapper.GetShellWindow();
			User32.FindWindow("Shell_TrayWnd", null);
			Rectangle screenRect = this._lastScreenRect;
			IntPtr _foregHwnd = APIWrapper.GetForegroundWindow();
			User32.RECT foregRect = default(User32.RECT);
			User32.GetWindowRect(_foregHwnd, ref foregRect);
			bool isPrd_fore = this.isPrd(_foregHwnd);
			User32.GetTopWindow(_foregHwnd, screenRect);
			IntPtr _onForgHwnd = User32.WindowFromPoint(new System.Drawing.Point(foregRect.Left + (foregRect.Right - foregRect.Left) / 2, foregRect.Top + (foregRect.Bottom - foregRect.Top) / 2));
			User32.RECT _onForgHwndRect = default(User32.RECT);
			User32.GetWindowRect(_onForgHwnd, ref _onForgHwndRect);
			bool isPrd_centerOfForeg = this.isPrd(_onForgHwnd);
			System.Drawing.Point pt = new System.Drawing.Point(screenRect.Width / 2 + screenRect.Left - 5, screenRect.Bottom - 5);
			try
			{
				RECT taskbarRect = APIWrapper.GetTaskbarRect().rc;
				pt = new System.Drawing.Point(taskbarRect.left + (taskbarRect.right - taskbarRect.left) / 2, taskbarRect.top + (taskbarRect.bottom - taskbarRect.top) / 2);
			}
			catch
			{
				NLogService.WriteLog("MainWindow|Error：获取任务栏区域失败");
			}
			IntPtr _onTaskbarHwnd = User32.WindowFromPoint(pt);
			User32.RECT _onTaskbarHwndRect = default(User32.RECT);
			User32.GetWindowRect(_onTaskbarHwnd, ref _onTaskbarHwndRect);
			bool isPrd_bottom = this.isPrd(_onTaskbarHwnd);
			if (isPrd_bottom)
			{
				this.PrdWin = _onTaskbarHwnd;
			}
			else if (isPrd_centerOfForeg)
			{
				this.PrdWin = _onForgHwnd;
			}
			else if (isPrd_fore)
			{
				this.PrdWin = _foregHwnd;
			}
			NLogService.WriteLog(string.Format("MainWindow|开始检测前台窗口:{0},{1}，{2}", _foregHwnd, _onForgHwnd, _onTaskbarHwnd));
			if (isPrd_centerOfForeg || isPrd_bottom || isPrd_fore || User32.IsWindowFromCurrentProcess(_foregHwnd))
			{
				if (this.storeFullScreenHwndInfo.hwnd != IntPtr.Zero && User32.IsWindow(this.storeFullScreenHwndInfo.hwnd) && User32.IsFullScreen(this.storeFullScreenHwndInfo.hwnd, screenRect))
				{
					return new ValueTuple<IntPtr, IntPtr>(this.storeFullScreenHwndInfo.hwnd, this.storeFullScreenHwndInfo.associatedHwnd);
				}
				IntPtr _topHwnd = IntPtr.Zero;
				IntPtr _associated = IntPtr.Zero;
				int _topProcessId = 0;
				bool isFindTopHwnd = false;
				IntPtr currentHwnd = _foregHwnd;
				if (_onForgHwnd != IntPtr.Zero)
				{
					currentHwnd = _onForgHwnd;
				}
				if (this.isPrd(currentHwnd))
				{
					currentHwnd = User32.GetWindow(currentHwnd, 0U);
				}
				while (!(currentHwnd == IntPtr.Zero))
				{
					int exStyle = User32.GetWindowLong(currentHwnd, -20);
					StringBuilder _lptrString2 = new StringBuilder(256);
					User32.GetWindowTextW(currentHwnd, _lptrString2, _lptrString2.Capacity);
					string _title2 = _lptrString2.ToString();
					User32.GetClassName(currentHwnd, _lptrString2, _lptrString2.Capacity);
					string _className2 = _lptrString2.ToString();
					NLogService.WriteLog(string.Format("PRD覆盖：检测全屏:{0}, {1},{2}", currentHwnd, _title2, _className2));
					if (WindowEnumerationHelper.IsWindowValidForCapture(currentHwnd) && (exStyle & 32) == 0 && !string.IsNullOrWhiteSpace(_title2) && _title2 != "InterwaveNotification" && currentHwnd != this.mainHwnd && !this.isPrd(currentHwnd) && !User32.IsWindowFromCurrentProcess(_foregHwnd))
					{
						User32.RECT _rect = default(User32.RECT);
						User32.GetWindowRect(currentHwnd, ref _rect);
						bool isFullSize = _rect.Left <= screenRect.Left + 1 && _rect.Top <= screenRect.Top + 1 && _rect.Right >= screenRect.Right - 1 && _rect.Bottom >= screenRect.Bottom - 1;
						NLogService.WriteLog(string.Format("PRD覆盖：监测全屏窗口句柄: {0},{1},{2}，{3},{4}", new object[]
						{
							currentHwnd,
							_rect.Left,
							_rect.Top,
							_rect.Right,
							_rect.Bottom
						}));
						if (!isFindTopHwnd)
						{
							if (!isFullSize)
							{
								break;
							}
							_topHwnd = currentHwnd;
							isFindTopHwnd = true;
							IntPtr processId;
							User32.GetWindowThreadProcessId(currentHwnd, out processId);
							_topProcessId = (int)processId;
							NLogService.WriteLog(string.Format("PRD覆盖：监测全屏窗口句柄: {0},{1},{2}，{3},{4}", new object[]
							{
								currentHwnd,
								_title2,
								_className2,
								isFullSize,
								_topProcessId
							}));
						}
						else
						{
							if (!isFullSize)
							{
								break;
							}
							IntPtr processId2;
							User32.GetWindowThreadProcessId(currentHwnd, out processId2);
							if (_topProcessId == (int)processId2)
							{
								_associated = currentHwnd;
								NLogService.WriteLog(string.Format("PRD覆盖：associated: {0}", _associated));
								break;
							}
							break;
						}
					}
					currentHwnd = User32.GetWindow(currentHwnd, 2U);
				}
				if (_topHwnd != IntPtr.Zero && _associated != IntPtr.Zero)
				{
					return new ValueTuple<IntPtr, IntPtr>(_associated, _topHwnd);
				}
				if (_topHwnd != IntPtr.Zero && _associated == IntPtr.Zero)
				{
					return new ValueTuple<IntPtr, IntPtr>(_topHwnd, IntPtr.Zero);
				}
			}
			else
			{
				bool flag = User32.checkFullScreen(foregRect, screenRect);
				bool isFullScreen_centerofdetected = User32.checkFullScreen(_onForgHwndRect, screenRect);
				bool isFullScreen_bottomofdetected = User32.checkFullScreen(_onTaskbarHwndRect, screenRect);
				StringBuilder lptrString = new StringBuilder(256);
				User32.GetWindowTextW(_foregHwnd, lptrString, lptrString.Capacity);
				string title = lptrString.ToString();
				NLogService.WriteLog(string.Format("MainWindow|检测前台窗口:{0},{1}", _foregHwnd, title));
				if (flag && title != "Program Manager" && _foregHwnd != IntPtr.Zero)
				{
					IntPtr fore_processId;
					User32.GetWindowThreadProcessId(_onForgHwnd, out fore_processId);
					NLogService.WriteLog(string.Format("MainWindow|检测前台窗口全屏:{0},{1}", _foregHwnd, title));
					IntPtr _onTaskbarOwnerHwnd = MainWindow.GetWindow(_onTaskbarHwnd, 4U);
					if ((_onTaskbarOwnerHwnd == _foregHwnd || _onTaskbarHwnd == _foregHwnd) && _foregHwnd != this.desktopHandle && _foregHwnd != this.shellHandle)
					{
						NLogService.WriteLog(string.Format("MainWindow|检测全屏窗口:{0},{1},{2},{3}", new object[]
						{
							_foregHwnd,
							_onForgHwnd,
							_onTaskbarHwnd,
							_onTaskbarOwnerHwnd
						}));
						if (isFullScreen_centerofdetected)
						{
							if (_foregHwnd == _onForgHwnd)
							{
								return new ValueTuple<IntPtr, IntPtr>(_foregHwnd, IntPtr.Zero);
							}
							IntPtr processId3;
							User32.GetWindowThreadProcessId(_onForgHwnd, out processId3);
							if (fore_processId == processId3)
							{
								return new ValueTuple<IntPtr, IntPtr>(_foregHwnd, _onForgHwnd);
							}
							return new ValueTuple<IntPtr, IntPtr>(_onForgHwnd, IntPtr.Zero);
						}
						else if (APIWrapper.IsOwner(_foregHwnd, _onForgHwnd))
						{
							return new ValueTuple<IntPtr, IntPtr>(_foregHwnd, IntPtr.Zero);
						}
					}
					else
					{
						StringBuilder _lptrString3 = new StringBuilder(256);
						User32.GetWindowTextW(_onForgHwnd, _lptrString3, _lptrString3.Capacity);
						string _title3 = _lptrString3.ToString();
						NLogService.WriteLog(string.Format("MainWindow|检测中心窗口1:{0},{1},{2}", _onForgHwnd, _title3, _onTaskbarOwnerHwnd));
						if (isFullScreen_centerofdetected && _onForgHwnd != IntPtr.Zero && (_onTaskbarOwnerHwnd == _onForgHwnd || _onTaskbarHwnd == _onForgHwnd) && _onForgHwnd != this.desktopHandle && _onForgHwnd != this.shellHandle)
						{
							if (_foregHwnd == _onForgHwnd)
							{
								return new ValueTuple<IntPtr, IntPtr>(_foregHwnd, IntPtr.Zero);
							}
							IntPtr processId4;
							User32.GetWindowThreadProcessId(_onForgHwnd, out processId4);
							if (fore_processId == processId4)
							{
								return new ValueTuple<IntPtr, IntPtr>(_foregHwnd, _onForgHwnd);
							}
							return new ValueTuple<IntPtr, IntPtr>(_onForgHwnd, IntPtr.Zero);
						}
					}
				}
				else if (isFullScreen_centerofdetected && _onForgHwnd != IntPtr.Zero)
				{
					IntPtr _ontaskbarOwnerHwnd = MainWindow.GetWindow(_onTaskbarHwnd, 4U);
					StringBuilder _lptrString4 = new StringBuilder(256);
					User32.GetWindowTextW(_onForgHwnd, _lptrString4, _lptrString4.Capacity);
					string _title4 = _lptrString4.ToString();
					NLogService.WriteLog(string.Format("MainWindow|检测中心窗口2:{0},{1},{2}", _onForgHwnd, _title4, _ontaskbarOwnerHwnd));
					if ((_ontaskbarOwnerHwnd == _onForgHwnd || _onTaskbarHwnd == _onForgHwnd || !WindowEnumerationHelper.IsWindowValid(_onTaskbarHwnd)) && _onForgHwnd != this.desktopHandle && _onForgHwnd != this.shellHandle && _title4 != "Program Manager")
					{
						return new ValueTuple<IntPtr, IntPtr>(_onForgHwnd, IntPtr.Zero);
					}
				}
				else if (isFullScreen_bottomofdetected && _onTaskbarHwnd != IntPtr.Zero)
				{
					IntPtr _onForeOwnerHwnd = MainWindow.GetWindow(_onForgHwnd, 4U);
					StringBuilder _lptrString5 = new StringBuilder(256);
					User32.GetWindowTextW(_onTaskbarHwnd, _lptrString5, _lptrString5.Capacity);
					string _title5 = _lptrString5.ToString();
					NLogService.WriteLog(string.Format("MainWindow|检测中心窗口3:{0},{1},{2}，{3}", new object[]
					{
						_onTaskbarHwnd,
						_title5,
						_onForeOwnerHwnd,
						_onForgHwnd
					}));
					if ((_onForeOwnerHwnd == _onTaskbarHwnd || _onForgHwnd == _onTaskbarHwnd || !WindowEnumerationHelper.IsWindowValid(_onForgHwnd)) && _onTaskbarHwnd != this.desktopHandle && _onTaskbarHwnd != this.shellHandle && _title5 != "Program Manager")
					{
						return new ValueTuple<IntPtr, IntPtr>(_onTaskbarHwnd, IntPtr.Zero);
					}
				}
			}
			NLogService.WriteLog("MainWindow|未发现全屏窗口！");
			return new ValueTuple<IntPtr, IntPtr>(IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000086F0 File Offset: 0x000068F0
		private ValueTuple<IntPtr, IntPtr, IntPtr> GetForegroundWindowEx()
		{
			this.desktopHandle = APIWrapper.GetDesktopWindow();
			this.shellHandle = APIWrapper.GetShellWindow();
			User32.FindWindow("Shell_TrayWnd", null);
			Rectangle screenRect = this._lastScreenRect;
			IntPtr foregroundWindow = APIWrapper.GetForegroundWindow();
			IntPtr forgHwnd = IntPtr.Zero;
			IntPtr onForgHwnd = IntPtr.Zero;
			IntPtr onTaskbarHwnd = IntPtr.Zero;
			ValueTuple<IntPtr, IntPtr, IntPtr> topWindow = User32.GetTopWindow(foregroundWindow, screenRect);
			forgHwnd = topWindow.Item1;
			onForgHwnd = topWindow.Item2;
			onTaskbarHwnd = topWindow.Item3;
			if (forgHwnd != IntPtr.Zero && (forgHwnd == this.desktopHandle || forgHwnd == this.shellHandle))
			{
				forgHwnd = IntPtr.Zero;
				onForgHwnd = IntPtr.Zero;
				onTaskbarHwnd = IntPtr.Zero;
			}
			if (forgHwnd != IntPtr.Zero)
			{
				StringBuilder _lptrString = new StringBuilder(256);
				User32.GetWindowTextW(forgHwnd, _lptrString, _lptrString.Capacity);
				string _title = _lptrString.ToString();
				NLogService.WriteLog(string.Format("MainWindow|检测中心窗口3:{0},{1},{2}，{3}", new object[]
				{
					forgHwnd,
					_title,
					onForgHwnd,
					onTaskbarHwnd
				}));
				if (_title == "Program Manager")
				{
					forgHwnd = IntPtr.Zero;
					onForgHwnd = IntPtr.Zero;
					onTaskbarHwnd = IntPtr.Zero;
				}
			}
			return new ValueTuple<IntPtr, IntPtr, IntPtr>(forgHwnd, onForgHwnd, onTaskbarHwnd);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008824 File Offset: 0x00006A24
		private ValueTuple<IntPtr, IntPtr, IntPtr> GetForegroundWindowOnPrd()
		{
			if (User32.IsPrd(APIWrapper.GetForegroundWindow()) && this.storeFullScreenHwndInfo.hwnd != IntPtr.Zero)
			{
				return new ValueTuple<IntPtr, IntPtr, IntPtr>(this.storeFullScreenHwndInfo.hwnd, this.storeFullScreenHwndInfo.associatedHwnd, this.storeFullScreenHwndInfo.onTaskbarHwnd);
			}
			return this.GetForegroundWindowEx();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008884 File Offset: 0x00006A84
		private bool isPrd(IntPtr hwnd)
		{
			StringBuilder lptrString = new StringBuilder(256);
			User32.GetWindowTextW(hwnd, lptrString, lptrString.Capacity);
			string title = lptrString.ToString();
			NLogService.WriteLine(string.Format("MainWindow|检测全屏窗口2: 句柄:{0},标题:{1}", hwnd, title));
			return title.StartsWith("TX_PRD_");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000088D4 File Offset: 0x00006AD4
		private void GetProcessInfo(int processId, out string processName, out string processPath)
		{
			processName = "";
			processPath = "";
			try
			{
				using (Process process = Process.GetProcessById(processId))
				{
					processName = process.ProcessName;
					ProcessModule mainModule = process.MainModule;
					processPath = ((mainModule != null) ? mainModule.FileName : null);
					NLogService.WriteLog("MainWindow|GetProcessInfo:进程名称: " + processName + ",文件路径: " + processPath);
				}
			}
			catch (ArgumentException)
			{
				NLogService.WriteLog("MainWindow|GetProcessInfo:未找到指定的进程 ID。");
			}
			catch (Win32Exception ex)
			{
				NLogService.WriteLog("MainWindow|GetProcessInfo:" + ex.Message);
				User32.GetProcessInfo(processId, out processName, out processPath);
				NLogService.WriteLog("MainWindow|文件路径: " + processPath);
			}
			catch (Exception ex2)
			{
				NLogService.WriteLog("MainWindow|GetProcessInfo:发生错误: " + ex2.Message);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000089C4 File Offset: 0x00006BC4
		private void InitFullscreenWindow()
		{
			this.fullScreenHwndHandle = IntPtr.Zero;
			this.associatedHwndHandle = IntPtr.Zero;
			this.onTaskbarHwnd = IntPtr.Zero;
			this.fullScreenProcessId = IntPtr.Zero;
			this.fullScreenHwndOldStyle = 0L;
			this.fullScreenHwndRect.Left = 0;
			this.fullScreenHwndRect.Top = 0;
			this.fullScreenHwndRect.Right = 0;
			this.fullScreenHwndRect.Bottom = 0;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008A38 File Offset: 0x00006C38
		private void ProcessHwndQueue3(object sender, ElapsedEventArgs e)
		{
			Dictionary<IntPtr, DateTime> hwndCache = this._hwndCache;
			lock (hwndCache)
			{
				if (this._hwndCache.Count == 0 && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
				{
					this._hwndCache.Clear();
					this._throttleTimer.Stop();
				}
				else
				{
					User32.RECT rect = default(User32.RECT);
					if (!User32.GetWindowRect(this.fullScreenHwndHandle, ref rect) || !User32.checkFullScreen(rect, this.fullScreenHwndRect) || WindowEnumerationHelper.IsClocked(this.fullScreenHwndHandle))
					{
						if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
						{
							NLogService.WriteLog("MainWindow|WinEventProc:窗口尺寸改变：开始退出交织;event:0x800B");
							this.StopInterwave();
							this.InitFullscreenWindow();
						}
						this._hwndCache.Clear();
						this._throttleTimer.Stop();
					}
					else
					{
						bool isCovered = false;
						foreach (IntPtr hwnd in this._hwndCache.Keys.ToList<IntPtr>())
						{
							if (!(hwnd == this.fullScreenHwndHandle) && !this.isPrd(hwnd) && !(hwnd == this.associatedHwndHandle) && !(hwnd == this.onTaskbarHwnd) && !WindowEnumerationHelper.IsClocked(hwnd) && !User32.IsIconic(hwnd))
							{
								StringBuilder content = new StringBuilder(256);
								User32.GetClassName(hwnd, content, content.Capacity);
								string _className = content.ToString();
								if (!(_className == "Chrome_RenderWidgetHostHWND") && !(_className == "TWINCONTROL") && !(_className == "SysShaow"))
								{
									if (_className == "ApplicationFrameWindow")
									{
										if (User32.IsGameBar(hwnd))
										{
											NLogService.WriteLine(string.Format("GameBar：{0}，{1}", hwnd, _className));
											isCovered = true;
											if (isCovered)
											{
												break;
											}
										}
									}
									else if (GlobalStatusManager.Instance._iInterWaveType == LocalEnumeration.InterWaveType.InterWave_SBS)
									{
										isCovered = User32.IsCovered(this.fullScreenHwndHandle, this.associatedHwndHandle, this.onTaskbarHwnd, hwnd, this._lastScreenRect, true);
									}
									else
									{
										isCovered = User32.IsObscuredByFullscreenWindow(this.fullScreenHwndHandle, this.associatedHwndHandle, this.onTaskbarHwnd, hwnd, this._lastScreenRect, true);
									}
									if (isCovered)
									{
										NLogService.WriteLog("MainWindow|WinEventProc:窗口被覆盖：" + _className);
										break;
									}
								}
							}
						}
						if (isCovered)
						{
							if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
							{
								NLogService.WriteLog("MainWindow|WinEventProc:被其他窗口覆盖：开始退出交织;event:0x800B");
								this.StopInterwave();
								this.InitFullscreenWindow();
							}
							this._hwndCache.Clear();
							this._throttleTimer.Stop();
						}
						else
						{
							this._hwndCache.Clear();
						}
					}
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008D5C File Offset: 0x00006F5C
		public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, long idObject, long idChild, ulong dwEventThread, ulong dwmsEventTime)
		{
			MainWindow.<>c__DisplayClass99_0 CS$<>8__locals1 = new MainWindow.<>c__DisplayClass99_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.hwnd = hwnd;
			CS$<>8__locals1.eventType = eventType;
			if (idObject != 0L)
			{
				return;
			}
			uint eventType2 = CS$<>8__locals1.eventType;
			if (eventType2 <= 32U)
			{
				if (eventType2 != 3U)
				{
					if (eventType2 != 32U)
					{
						return;
					}
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						NLogService.WriteLog("MainWindow|WinEventProc:已切换活动桌面，开始退出交织");
						this.StopInterwave();
						this.InitFullscreenWindow();
						return;
					}
				}
				else
				{
					IntPtr foreground = User32.GetForegroundWindow();
					if (this.PrdWin != IntPtr.Zero && this.PrdWin == CS$<>8__locals1.hwnd)
					{
						this.PrdWin = CS$<>8__locals1.hwnd;
						User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						return;
					}
					StringBuilder lptr_s = new StringBuilder(256);
					User32.GetWindowTextW(CS$<>8__locals1.hwnd, lptr_s, lptr_s.Capacity);
					string _winName_s = lptr_s.ToString();
					if (foreground != this.mainHwnd)
					{
						if (_winName_s.StartsWith("TX_PRD_"))
						{
							this.PrdWin = CS$<>8__locals1.hwnd;
							User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
							return;
						}
						IntPtr prdWin = this.PrdWin;
						if (this.PrdWin == IntPtr.Zero)
						{
							User32.SetWindowPos(this.mainHwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						}
					}
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						if (((CS$<>8__locals1.hwnd == this.fullScreenHwndHandle || CS$<>8__locals1.hwnd == this.associatedHwndHandle || CS$<>8__locals1.hwnd == this.onTaskbarHwnd) && CS$<>8__locals1.hwnd != IntPtr.Zero) || !User32.IsWindowVisible(CS$<>8__locals1.hwnd))
						{
							return;
						}
						NLogService.WriteLine("MainWindow|WinEventProc:检测前台窗口变化：" + _winName_s);
						if (_winName_s == "Program Manager" && foreground == CS$<>8__locals1.hwnd)
						{
							NLogService.WriteLog("MainWindow|WinEventProc:Program Manager显示：开始退出交织;event:" + CS$<>8__locals1.eventType.ToString("X8"));
							this.StopInterwave();
							this.InitFullscreenWindow();
							return;
						}
						if (!WindowEnumerationHelper.IsClocked(CS$<>8__locals1.hwnd))
						{
							Task.Run(delegate()
							{
								if ((User32.IsCovered(CS$<>8__locals1.<>4__this.fullScreenHwndHandle, CS$<>8__locals1.<>4__this.associatedHwndHandle, CS$<>8__locals1.<>4__this.onTaskbarHwnd, CS$<>8__locals1.hwnd, CS$<>8__locals1.<>4__this._lastScreenRect, true) || WindowEnumerationHelper.IsClocked(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || User32.IsIconic(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || User32.IsWindowMinimized(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || !User32.IsWindowVisible(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || !User32.IsFullScreen(CS$<>8__locals1.<>4__this.fullScreenHwndHandle, CS$<>8__locals1.<>4__this._lastScreenRect)) && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
								{
									NLogService.WriteLog("MainWindow|WinEventProc:窗口尺寸改变或隐藏：开始退出交织;event:" + CS$<>8__locals1.eventType.ToString("X8"));
									CS$<>8__locals1.<>4__this.StopInterwave();
									CS$<>8__locals1.<>4__this.InitFullscreenWindow();
								}
							}).LogExceptions();
							return;
						}
					}
				}
			}
			else
			{
				switch (eventType2)
				{
				case 32769U:
					break;
				case 32770U:
				{
					StringBuilder lptr_s2 = new StringBuilder(256);
					User32.GetWindowTextW(CS$<>8__locals1.hwnd, lptr_s2, lptr_s2.Capacity);
					string _winName_s2 = lptr_s2.ToString();
					User32.GetClassName(CS$<>8__locals1.hwnd, lptr_s2, lptr_s2.Capacity);
					lptr_s2.ToString();
					if (_winName_s2 == "SysShaow" || _winName_s2 == "TWINCONTROL" || _winName_s2 == "Chrome_RenderWidgetHostHWND")
					{
						return;
					}
					if (_winName_s2.StartsWith("TX_PRD_"))
					{
						NLogService.WriteLine("MainWindow|WinEventProc:PRD弹窗显示：" + _winName_s2);
						this.PrdWin = CS$<>8__locals1.hwnd;
						User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						return;
					}
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping && User32.IsWindowVisible(CS$<>8__locals1.hwnd) && !(CS$<>8__locals1.hwnd == this.fullScreenHwndHandle) && !WindowEnumerationHelper.IsClocked(CS$<>8__locals1.hwnd))
					{
						Task.Run(delegate()
						{
							MainWindow.<>c__DisplayClass99_0.<<WinEventProc>b__1>d <<WinEventProc>b__1>d;
							<<WinEventProc>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<WinEventProc>b__1>d.<>4__this = CS$<>8__locals1;
							<<WinEventProc>b__1>d.<>1__state = -1;
							<<WinEventProc>b__1>d.<>t__builder.Start<MainWindow.<>c__DisplayClass99_0.<<WinEventProc>b__1>d>(ref <<WinEventProc>b__1>d);
							return <<WinEventProc>b__1>d.<>t__builder.Task;
						}).LogExceptions();
						return;
					}
					return;
				}
				case 32771U:
				{
					StringBuilder lptr_h = new StringBuilder(256);
					User32.GetWindowTextW(CS$<>8__locals1.hwnd, lptr_h, lptr_h.Capacity);
					if (lptr_h.ToString().StartsWith("TX_PRD_"))
					{
						this.PrdWin = IntPtr.Zero;
					}
					return;
				}
				default:
					if (eventType2 != 32779U)
					{
						if (eventType2 != 32791U)
						{
							return;
						}
					}
					else
					{
						if (!(this.fullScreenHwndHandle != IntPtr.Zero) || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
						{
							return;
						}
						Dictionary<IntPtr, DateTime> hwndCache = this._hwndCache;
						lock (hwndCache)
						{
							this._hwndCache[CS$<>8__locals1.hwnd] = DateTime.Now;
							if (!this._throttleTimer.Enabled)
							{
								this._throttleTimer.Start();
							}
							return;
						}
					}
					break;
				}
				if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
				{
					IntPtr intPtr = this.fullScreenHwndHandle;
					if (CS$<>8__locals1.hwnd == this.fullScreenHwndHandle && CS$<>8__locals1.hwnd != IntPtr.Zero)
					{
						NLogService.WriteLog(string.Format("MainWindow|WinEventProc:窗口销毁或不可见，开始退出交织：{0}", CS$<>8__locals1.hwnd));
						this.StopInterwave();
						this.InitFullscreenWindow();
						return;
					}
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000092D8 File Offset: 0x000074D8
		public void WinEventProc2(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, long idObject, long idChild, ulong dwEventThread, ulong dwmsEventTime)
		{
			MainWindow.<>c__DisplayClass100_0 CS$<>8__locals1 = new MainWindow.<>c__DisplayClass100_0();
			CS$<>8__locals1.hwnd = hwnd;
			CS$<>8__locals1.eventType = eventType;
			CS$<>8__locals1.<>4__this = this;
			if (idObject != 0L)
			{
				return;
			}
			uint eventType2 = CS$<>8__locals1.eventType;
			if (eventType2 <= 32U)
			{
				if (eventType2 != 3U)
				{
					if (eventType2 != 32U)
					{
						return;
					}
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						NLogService.WriteLog("MainWindow|WinEventProc:已切换活动桌面");
						this.StopInterwave();
						this.InitFullscreenWindow();
						return;
					}
				}
				else
				{
					IntPtr foreground = User32.GetForegroundWindow();
					if (this.PrdWin != IntPtr.Zero && this.PrdWin == CS$<>8__locals1.hwnd)
					{
						this.PrdWin = CS$<>8__locals1.hwnd;
						User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						return;
					}
					StringBuilder lptr_s = new StringBuilder(256);
					User32.GetWindowTextW(CS$<>8__locals1.hwnd, lptr_s, lptr_s.Capacity);
					string _winName_s = lptr_s.ToString();
					if (foreground != this.mainHwnd)
					{
						if (_winName_s.StartsWith("TX_PRD_"))
						{
							this.PrdWin = CS$<>8__locals1.hwnd;
							User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
							return;
						}
						IntPtr prdWin = this.PrdWin;
						if (this.PrdWin == IntPtr.Zero)
						{
							User32.SetWindowPos(this.mainHwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						}
					}
					if (!_winName_s.StartsWith("TX_PRD_") && !_winName_s.StartsWith("yl_3d_") && this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						Task.Run(delegate()
						{
							if (_winName_s == "Program Manager" && foreground == CS$<>8__locals1.hwnd)
							{
								NLogService.WriteLog("MainWindow|WinEventProc:Program Manager显示：开始退出交织;event:" + CS$<>8__locals1.eventType.ToString("X8"));
								CS$<>8__locals1.<>4__this.StopInterwave();
								CS$<>8__locals1.<>4__this.InitFullscreenWindow();
								return;
							}
							if (WindowEnumerationHelper.IsClocked(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || User32.IsIconic(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || User32.IsWindowMinimized(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || !User32.IsWindowVisible(CS$<>8__locals1.<>4__this.fullScreenHwndHandle) || !User32.IsFullScreen(CS$<>8__locals1.<>4__this.fullScreenHwndHandle, CS$<>8__locals1.<>4__this._lastScreenRect))
							{
								if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
								{
									NLogService.WriteLog("MainWindow|WinEventProc:窗口尺寸改变：开始退出交织;event:0x0003");
									CS$<>8__locals1.<>4__this.StopInterwave();
									CS$<>8__locals1.<>4__this.InitFullscreenWindow();
									return;
								}
							}
							else if (User32.IsObscuredByFullscreenWindow(CS$<>8__locals1.<>4__this.fullScreenHwndHandle, CS$<>8__locals1.<>4__this.associatedHwndHandle, CS$<>8__locals1.<>4__this.onTaskbarHwnd, CS$<>8__locals1.hwnd, CS$<>8__locals1.<>4__this._lastScreenRect, true) && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
							{
								NLogService.WriteLog("MainWindow|WinEventProc:全屏窗口切换：开始退出交织;event:0x0003");
								CS$<>8__locals1.<>4__this.StopInterwave();
								CS$<>8__locals1.<>4__this.InitFullscreenWindow();
							}
						}).LogExceptions();
						return;
					}
				}
			}
			else
			{
				switch (eventType2)
				{
				case 32769U:
					break;
				case 32770U:
				{
					StringBuilder lptr_s2 = new StringBuilder(256);
					User32.GetWindowTextW(CS$<>8__locals1.hwnd, lptr_s2, lptr_s2.Capacity);
					string _winName_s2 = lptr_s2.ToString();
					User32.GetClassName(CS$<>8__locals1.hwnd, lptr_s2, lptr_s2.Capacity);
					lptr_s2.ToString();
					if (_winName_s2 == "SysShaow" || _winName_s2 == "TWINCONTROL" || _winName_s2 == "Chrome_RenderWidgetHostHWND")
					{
						return;
					}
					if (_winName_s2.StartsWith("TX_PRD_"))
					{
						NLogService.WriteLog("MainWindow|WinEventProc:PRD弹窗显示：" + _winName_s2);
						this.PrdWin = CS$<>8__locals1.hwnd;
						User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
						return;
					}
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping && User32.IsWindowVisible(CS$<>8__locals1.hwnd) && !(CS$<>8__locals1.hwnd == this.fullScreenHwndHandle) && !WindowEnumerationHelper.IsClocked(CS$<>8__locals1.hwnd))
					{
						Task.Run(delegate()
						{
							MainWindow.<>c__DisplayClass100_0.<<WinEventProc2>b__1>d <<WinEventProc2>b__1>d;
							<<WinEventProc2>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<WinEventProc2>b__1>d.<>4__this = CS$<>8__locals1;
							<<WinEventProc2>b__1>d.<>1__state = -1;
							<<WinEventProc2>b__1>d.<>t__builder.Start<MainWindow.<>c__DisplayClass100_0.<<WinEventProc2>b__1>d>(ref <<WinEventProc2>b__1>d);
							return <<WinEventProc2>b__1>d.<>t__builder.Task;
						}).LogExceptions();
						return;
					}
					return;
				}
				case 32771U:
				{
					StringBuilder lptr_h = new StringBuilder(256);
					User32.GetWindowTextW(CS$<>8__locals1.hwnd, lptr_h, lptr_h.Capacity);
					if (lptr_h.ToString().StartsWith("TX_PRD_"))
					{
						this.PrdWin = IntPtr.Zero;
					}
					return;
				}
				default:
					if (eventType2 != 32779U)
					{
						if (eventType2 != 32791U)
						{
							return;
						}
					}
					else
					{
						if (!(this.fullScreenHwndHandle != IntPtr.Zero) || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
						{
							return;
						}
						Dictionary<IntPtr, DateTime> hwndCache = this._hwndCache;
						lock (hwndCache)
						{
							this._hwndCache[CS$<>8__locals1.hwnd] = DateTime.Now;
							if (!this._throttleTimer.Enabled)
							{
								this._throttleTimer.Start();
							}
							return;
						}
					}
					break;
				}
				if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
				{
					IntPtr intPtr = this.fullScreenHwndHandle;
					if (CS$<>8__locals1.hwnd == this.fullScreenHwndHandle && CS$<>8__locals1.hwnd != IntPtr.Zero)
					{
						NLogService.WriteLog(string.Format("MainWindow|WinEventProc:窗口销毁或不可见，开始退出交织：{0}", CS$<>8__locals1.hwnd));
						this.StopInterwave();
						this.InitFullscreenWindow();
						return;
					}
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000983C File Offset: 0x00007A3C
		public void WinEventProc3(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, long idObject, long idChild, ulong dwEventThread, ulong dwmsEventTime)
		{
			if (idObject != 0L && eventType != 32772U)
			{
				return;
			}
			if (eventType <= 32U)
			{
				if (eventType == 3U)
				{
					this.CheckForegroundEvent(hwnd);
					return;
				}
				if (eventType != 22U)
				{
					if (eventType != 32U)
					{
						return;
					}
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						NLogService.WriteLog("MainWindow|WinEventProc:已切换活动桌面,开始退出交织");
						this.StopInterwave();
						this.InitFullscreenWindow();
						return;
					}
					return;
				}
			}
			else
			{
				switch (eventType)
				{
				case 32768U:
					if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping && GlobalStatusManager.Instance._iInterWaveType == LocalEnumeration.InterWaveType.InterWave_SBS)
					{
						Task.Run(delegate()
						{
							StringBuilder strBid = new StringBuilder(256);
							User32.GetClassName(hwnd, strBid, strBid.Capacity);
							string _winClsName_ = strBid.ToString();
							if (_winClsName_ == "GameBarFT_HKM" || _winClsName_ == "Shell_LightDismissOverlay")
							{
								NLogService.WriteLog("MainWindow|WinEventProc:" + _winClsName_);
								this.StopInterwave();
								this.InitFullscreenWindow();
							}
						});
						return;
					}
					return;
				case 32769U:
					break;
				case 32770U:
					this.CheckObjectShowEvent(hwnd);
					return;
				case 32771U:
				{
					StringBuilder lptr_h = new StringBuilder(256);
					User32.GetWindowTextW(hwnd, lptr_h, lptr_h.Capacity);
					if (lptr_h.ToString().StartsWith("TX_PRD_"))
					{
						this.PrdWin = IntPtr.Zero;
					}
					return;
				}
				case 32772U:
				{
					if (!(hwnd != IntPtr.Zero) || !(hwnd == this.desktopHandle) || !(this.fullScreenHwndHandle != IntPtr.Zero) || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						return;
					}
					ReorderHandler reorderHandler = this._reorderHandler;
					if (reorderHandler != null)
					{
						reorderHandler.EnableReorderCheck();
					}
					ReorderHandler reorderHandler2 = this._reorderHandler;
					if (reorderHandler2 == null)
					{
						return;
					}
					reorderHandler2.Start();
					return;
				}
				default:
					if (eventType != 32779U)
					{
						if (eventType != 32791U)
						{
							return;
						}
					}
					else
					{
						if (!(this.fullScreenHwndHandle != IntPtr.Zero) || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle || GlobalStatusManager.Instance._iInterWaveStatus == LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
						{
							return;
						}
						Dictionary<IntPtr, DateTime> hwndCache = this._hwndCache;
						lock (hwndCache)
						{
							this._hwndCache[hwnd] = DateTime.Now;
							if (!this._throttleTimer.Enabled)
							{
								this._throttleTimer.Start();
							}
							return;
						}
					}
					break;
				}
			}
			if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
			{
				IntPtr intPtr = this.fullScreenHwndHandle;
				if (hwnd == this.fullScreenHwndHandle && hwnd != IntPtr.Zero)
				{
					NLogService.WriteLog(string.Format("MainWindow|WinEventProc:窗口销毁|隐藏|最小化,开始退出交织：{0}", hwnd));
					this.StopInterwave();
					this.InitFullscreenWindow();
					return;
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00009B68 File Offset: 0x00007D68
		private void CheckForegroundEvent(IntPtr hwnd)
		{
			IntPtr foreground = User32.GetForegroundWindow();
			if (this.PrdWin != IntPtr.Zero && this.PrdWin == hwnd)
			{
				this.PrdWin = hwnd;
				User32.SetWindowPos(hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
				return;
			}
			StringBuilder lptr_s = new StringBuilder(256);
			User32.GetWindowTextW(hwnd, lptr_s, lptr_s.Capacity);
			string _winName_s = lptr_s.ToString();
			NLogService.WriteLine("MainWindow|WinEventProc:前台窗口变化：" + _winName_s);
			if (foreground != this.mainHwnd)
			{
				if (_winName_s.StartsWith("TX_PRD_"))
				{
					this.PrdWin = hwnd;
					User32.SetWindowPos(hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
					return;
				}
				IntPtr prdWin = this.PrdWin;
				if (this.PrdWin == IntPtr.Zero)
				{
					User32.SetWindowPos(this.mainHwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
				}
			}
			if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
			{
				Task.Run(delegate()
				{
					bool _requireStopCapture = false;
					string _reason = "";
					if (_winName_s == "Program Manager" && foreground == hwnd)
					{
						_requireStopCapture = true;
						_reason = "Program Manager显示";
					}
					else if (GlobalStatusManager.Instance._iInterWaveType == LocalEnumeration.InterWaveType.InterWave_SBS)
					{
						if ((hwnd == this.fullScreenHwndHandle || hwnd == this.associatedHwndHandle || hwnd == this.onTaskbarHwnd) && hwnd != IntPtr.Zero)
						{
							return;
						}
						if (User32.IsCovered(this.fullScreenHwndHandle, this.associatedHwndHandle, this.onTaskbarHwnd, hwnd, this._lastScreenRect, true) || WindowEnumerationHelper.IsClocked(this.fullScreenHwndHandle) || User32.IsIconic(this.fullScreenHwndHandle) || User32.IsWindowMinimized(this.fullScreenHwndHandle) || !User32.IsWindowVisible(this.fullScreenHwndHandle) || !User32.IsFullScreen(this.fullScreenHwndHandle, this._lastScreenRect))
						{
							_requireStopCapture = true;
							_reason = "窗口尺寸改变或隐藏";
						}
						else
						{
							StringBuilder strBid_ = new StringBuilder(256);
							User32.GetClassName(hwnd, strBid_, strBid_.Capacity);
							if (strBid_.ToString() == "XamlExplorerHostIslandWindow" && !User32.IsOutScreent(hwnd, this._lastScreenRect) && !string.IsNullOrWhiteSpace(User32.GetWinTitle(hwnd)))
							{
								_requireStopCapture = true;
								_reason = "任务切换";
							}
						}
					}
					else if (WindowEnumerationHelper.IsClocked(this.fullScreenHwndHandle) || User32.IsIconic(this.fullScreenHwndHandle) || User32.IsWindowMinimized(this.fullScreenHwndHandle) || !User32.IsWindowVisible(this.fullScreenHwndHandle) || !User32.IsFullScreen(this.fullScreenHwndHandle, this._lastScreenRect))
					{
						_requireStopCapture = true;
						_reason = "窗口尺寸改变";
					}
					else if (User32.IsObscuredByFullscreenWindow(this.fullScreenHwndHandle, this.associatedHwndHandle, this.onTaskbarHwnd, hwnd, this._lastScreenRect, true))
					{
						_requireStopCapture = true;
						_reason = "全屏窗口切换";
					}
					if (_requireStopCapture && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
					{
						NLogService.WriteLog("MainWindow|WinEventProc:" + _reason + "：开始退出交织;event:0x0003");
						this.StopInterwave();
						this.InitFullscreenWindow();
					}
				}).LogExceptions();
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009CE0 File Offset: 0x00007EE0
		private void CheckObjectShowEvent(IntPtr hwnd)
		{
			MainWindow.<>c__DisplayClass103_0 CS$<>8__locals1 = new MainWindow.<>c__DisplayClass103_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.hwnd = hwnd;
			StringBuilder _sb = new StringBuilder(256);
			User32.GetWindowTextW(CS$<>8__locals1.hwnd, _sb, _sb.Capacity);
			string _winName = _sb.ToString();
			User32.GetClassName(CS$<>8__locals1.hwnd, _sb, _sb.Capacity);
			CS$<>8__locals1._className = _sb.ToString();
			if (_winName == "SysShaow" || _winName == "TWINCONTROL" || _winName == "Chrome_RenderWidgetHostHWND")
			{
				return;
			}
			if (_winName.StartsWith("TX_PRD_"))
			{
				NLogService.WriteLine("MainWindow|WinEventProc:PRD弹窗显示：" + _winName);
				this.PrdWin = CS$<>8__locals1.hwnd;
				User32.SetWindowPos(CS$<>8__locals1.hwnd, (IntPtr)(-1), 0, 0, 0, 0, 3U);
				return;
			}
			if (this.fullScreenHwndHandle != IntPtr.Zero && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
			{
				if (!User32.IsWindowVisible(CS$<>8__locals1.hwnd) || CS$<>8__locals1.hwnd == this.fullScreenHwndHandle || WindowEnumerationHelper.IsClocked(CS$<>8__locals1.hwnd))
				{
					return;
				}
				Task.Run(delegate()
				{
					MainWindow.<>c__DisplayClass103_0.<<CheckObjectShowEvent>b__0>d <<CheckObjectShowEvent>b__0>d;
					<<CheckObjectShowEvent>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<CheckObjectShowEvent>b__0>d.<>4__this = CS$<>8__locals1;
					<<CheckObjectShowEvent>b__0>d.<>1__state = -1;
					<<CheckObjectShowEvent>b__0>d.<>t__builder.Start<MainWindow.<>c__DisplayClass103_0.<<CheckObjectShowEvent>b__0>d>(ref <<CheckObjectShowEvent>b__0>d);
					return <<CheckObjectShowEvent>b__0>d.<>t__builder.Task;
				}).LogExceptions();
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009E27 File Offset: 0x00008027
		public void PauseCapture()
		{
			if (GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Idle && GlobalStatusManager.Instance._iInterWaveStatus != LocalEnumeration.InterWaveStatus.InterWaveStatus_Stopping)
			{
				NLogService.WriteLog("MainWindow|PauseCapture：暂停交织");
				this.StopInterwave();
				this.InitFullscreenWindow();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009E5C File Offset: 0x0000805C
		private void OpenGuardProcess()
		{
			if (this.IsProcessRunning("WatchProcessService"))
			{
				NLogService.WriteLog("MainWindow|GuardProcess：守护进程已存在");
				return;
			}
			try
			{
				Process.Start(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\ProcessGuard" + "\\WatchProcessService.exe");
				NLogService.WriteLog("MainWindow|GuardProcess：守护进程打开成功");
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("MainWindow|Error：" + ex.Message);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009EE4 File Offset: 0x000080E4
		private bool IsProcessRunning(string processName)
		{
			return Process.GetProcessesByName(processName).Length != 0;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009EF0 File Offset: 0x000080F0
		public Task InvokeWithTimeoutAsync(Action action, TimeSpan timeout)
		{
			MainWindow.<InvokeWithTimeoutAsync>d__107 <InvokeWithTimeoutAsync>d__;
			<InvokeWithTimeoutAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InvokeWithTimeoutAsync>d__.<>4__this = this;
			<InvokeWithTimeoutAsync>d__.action = action;
			<InvokeWithTimeoutAsync>d__.timeout = timeout;
			<InvokeWithTimeoutAsync>d__.<>1__state = -1;
			<InvokeWithTimeoutAsync>d__.<>t__builder.Start<MainWindow.<InvokeWithTimeoutAsync>d__107>(ref <InvokeWithTimeoutAsync>d__);
			return <InvokeWithTimeoutAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009F44 File Offset: 0x00008144
		private void StoreFullScreenInfo()
		{
			IntPtr foregHwnd = APIWrapper.GetForegroundWindow();
			if (this.isPrd(foregHwnd))
			{
				if (this.storeFullScreenHwndInfo.hwnd != IntPtr.Zero)
				{
					if (!User32.IsWindow(this.storeFullScreenHwndInfo.hwnd))
					{
						this.storeFullScreenHwndInfo.hwnd = IntPtr.Zero;
						this.storeFullScreenHwndInfo.associatedHwnd = IntPtr.Zero;
						this.storeFullScreenHwndInfo.onTaskbarHwnd = IntPtr.Zero;
						NLogService.WriteLog(string.Format("清除全屏窗口：{0},{1}", this.storeFullScreenHwndInfo.hwnd, this.storeFullScreenHwndInfo.associatedHwnd));
						return;
					}
					Rectangle screenRect = this._lastScreenRect;
					if (!User32.IsFullScreen(this.storeFullScreenHwndInfo.hwnd, screenRect))
					{
						this.storeFullScreenHwndInfo.hwnd = IntPtr.Zero;
						this.storeFullScreenHwndInfo.associatedHwnd = IntPtr.Zero;
						this.storeFullScreenHwndInfo.onTaskbarHwnd = IntPtr.Zero;
						NLogService.WriteLog(string.Format("清除全屏窗口：{0},{1}", this.storeFullScreenHwndInfo.hwnd, this.storeFullScreenHwndInfo.associatedHwnd));
					}
				}
				return;
			}
			ref IntPtr ptr = ref this.storeFullScreenHwndInfo.hwnd;
			ref IntPtr ptr2 = ref this.storeFullScreenHwndInfo.associatedHwnd;
			ValueTuple<IntPtr, IntPtr, IntPtr> foregroundWindowEx = this.GetForegroundWindowEx();
			ptr = foregroundWindowEx.Item1;
			ptr2 = foregroundWindowEx.Item2;
			this.storeFullScreenHwndInfo.onTaskbarHwnd = foregroundWindowEx.Item3;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A0B0 File Offset: 0x000082B0
		private void ClearStoreFullScreenInfo()
		{
			if (this.storeFullScreenHwndInfo.hwnd != IntPtr.Zero)
			{
				if (!User32.IsWindow(this.storeFullScreenHwndInfo.hwnd))
				{
					this.storeFullScreenHwndInfo.hwnd = IntPtr.Zero;
					this.storeFullScreenHwndInfo.associatedHwnd = IntPtr.Zero;
					this.storeFullScreenHwndInfo.onTaskbarHwnd = IntPtr.Zero;
					NLogService.WriteLog(string.Format("清除全屏窗口：{0},{1}", this.storeFullScreenHwndInfo.hwnd, this.storeFullScreenHwndInfo.associatedHwnd));
					return;
				}
				Rectangle screenRect = this._lastScreenRect;
				if (!User32.IsFullScreen(this.storeFullScreenHwndInfo.hwnd, screenRect) || APIWrapper.GetForegroundWindow() != this.storeFullScreenHwndInfo.hwnd)
				{
					this.storeFullScreenHwndInfo.hwnd = IntPtr.Zero;
					this.storeFullScreenHwndInfo.associatedHwnd = IntPtr.Zero;
					this.storeFullScreenHwndInfo.onTaskbarHwnd = IntPtr.Zero;
					NLogService.WriteLog(string.Format("清除全屏窗口：{0},{1}", this.storeFullScreenHwndInfo.hwnd, this.storeFullScreenHwndInfo.associatedHwnd));
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A1DC File Offset: 0x000083DC
		private void RegisterAppBar(bool registered)
		{
			APPBARDATA abd = default(APPBARDATA);
			abd.cbSize = Marshal.SizeOf<APPBARDATA>(abd);
			abd.hWnd = this.mainHwnd;
			if (!registered)
			{
				this.uCallBackMsg = APIWrapper.RegisterWindowMessage("APPBARMSG_CSDN_HELPER");
				abd.uCallbackMessage = this.uCallBackMsg;
				uint ret = APIWrapper.SHAppBarMessage(0, ref abd);
				NLogService.WriteLog(string.Format("MainWindow|RegisterAppBar:注册AppBar:{0}", ret));
				return;
			}
			APIWrapper.SHAppBarMessage(1, ref abd);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A253 File Offset: 0x00008453
		private void InitLanguage()
		{
			SettingsTools.ResetLanguage(Settings.Default.Language);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A264 File Offset: 0x00008464
		private bool checkLicense()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool valid = YLStereoRenderCSharp.IsValidLicenseCSharp(stringBuilder);
			stringBuilder.Clear();
			return valid;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A284 File Offset: 0x00008484
		private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
		{
			base.Dispatcher.InvokeAsync(delegate()
			{
				Settings.Instance.ShortcutSettingsChanged = new MessageHelp.ShortcutSettingsChanged(this.ResetRegisterHotKey);
				Settings.Instance.InferDeviceChanged = new MessageHelp.InferDeviceChanged(this.ResetInferDevice);
				Settings.Instance.Closed += this.Settings_Closed;
				Settings.Instance.Show();
				Settings.Instance.Activate();
				this.SettingsDialog = Settings.Instance;
				NLogService.WriteLog("settings?.Show()");
			});
			e.Handled = true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A2A5 File Offset: 0x000084A5
		private void Settings_Closed(object sender, EventArgs e)
		{
			this.SettingsDialog = null;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A2AE File Offset: 0x000084AE
		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			this._isShuttingDown = true;
			System.Windows.Application.Current.Shutdown();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A2C4 File Offset: 0x000084C4
		private bool isLowLevelHotkeyCanbeRegister(int modifier, int key)
		{
			LowLevelHotkey hotkey = this._hotkey;
			bool? flag = (hotkey != null) ? new bool?(hotkey.IsHotkeyRegistered(key, modifier)) : null;
			bool flag2 = false;
			return flag.GetValueOrDefault() == flag2 & flag != null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A308 File Offset: 0x00008508
		private void UpdateCenterPosition()
		{
			IEnumerable<MonitorInfo> monitors = MonitorEnumerationHelper.GetMonitors();
			MonitorInfo monitorInfo;
			if ((monitorInfo = monitors.FirstOrDefault(delegate(MonitorInfo m)
			{
				string deviceName = m.DeviceName;
				MonitorFullInfo monitorFullInfo = this.g_monitor;
				return deviceName == ((monitorFullInfo != null) ? monitorFullInfo.GdiName : null);
			})) == null)
			{
				monitorInfo = monitors.FirstOrDefault((MonitorInfo m) => m.IsPrimary);
			}
			MonitorInfo primaryMonitor = monitorInfo;
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
			int posX = (int)(monitorArea.X + (monitorArea.Width - 800.0) / 2.0);
			int posY = (int)(monitorArea.Y + (monitorArea.Height - 450.0) / 2.0);
			User32.SetWindowPos(hwnd, User32.HWND_TOPMOST, posX, posY, 800, 450, 48U);
			NLogService.WriteLog("MainWindow|UpdateCenterPosition: " + string.Format("Monitor=({0},{1},{2}×{3}), ", new object[]
			{
				monitorArea.X,
				monitorArea.Y,
				monitorArea.Width,
				monitorArea.Height
			}) + string.Format("Pos=({0},{1}), Size=800×450", posX, posY));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A46B File Offset: 0x0000866B
		public Screen GetCurrentScreen(Window window)
		{
			return Screen.FromHandle(new WindowInteropHelper(window).Handle);
		}

		// Token: 0x060000FC RID: 252
		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060000FD RID: 253 RVA: 0x0000A480 File Offset: 0x00008680
		public void SendMessageToSelf(IntPtr wParam, IntPtr lParam)
		{
			if (this.mainHwnd == IntPtr.Zero)
			{
				throw new InvalidOperationException("窗口句柄尚未初始化");
			}
			if (!MainWindow.PostMessage(this.mainHwnd, 32769U, wParam, lParam))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), "PostMessage 失败");
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A4D0 File Offset: 0x000086D0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/mainwindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A500 File Offset: 0x00008700
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A50C File Offset: 0x0000870C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MainWindow)target).SourceInitialized += this.Window_SourceInitialized;
				((MainWindow)target).Loaded += this.Window_Loaded;
				((MainWindow)target).Closed += this.Window_Closed;
				((MainWindow)target).Closing += this.Window_Closing;
				return;
			case 2:
				this.TrayIcon = (TaskbarIcon)target;
				this.TrayIcon.TrayLeftMouseDown += this.TaskbarIcon_TrayLeftMouseDown;
				return;
			case 3:
				((System.Windows.Controls.MenuItem)target).Click += this.MenuItem_Click;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000053 RID: 83
		private const uint GW_HWNDNEXT = 2U;

		// Token: 0x04000054 RID: 84
		private const uint MONITOR_DEFAULTTONEAREST = 2U;

		// Token: 0x04000055 RID: 85
		private const int WM_DEVICECHANGE = 537;

		// Token: 0x04000056 RID: 86
		private const int DBT_DEVICEREMOVECOMPLETE = 32772;

		// Token: 0x04000057 RID: 87
		private const int DBT_DEVICEARRIVAL = 32768;

		// Token: 0x04000058 RID: 88
		private const int DBT_DEVTYP_MONITOR = 3;

		// Token: 0x04000059 RID: 89
		private IntPtr mainHwnd;

		// Token: 0x0400005A RID: 90
		private Task serverTask;

		// Token: 0x0400005B RID: 91
		private WebSocketFleck server;

		// Token: 0x0400005C RID: 92
		private WMIButtonReceiveEvent wMIButtonReceiveEvent;

		// Token: 0x0400005D RID: 93
		private IntPtr desktopHandle;

		// Token: 0x0400005E RID: 94
		private IntPtr shellHandle;

		// Token: 0x0400005F RID: 95
		private IntPtr m_hhook = IntPtr.Zero;

		// Token: 0x04000060 RID: 96
		private User32.WinEventDelegate weDelegate;

		// Token: 0x04000061 RID: 97
		private volatile IntPtr fullScreenHwndHandle = IntPtr.Zero;

		// Token: 0x04000062 RID: 98
		private IntPtr associatedHwndHandle = IntPtr.Zero;

		// Token: 0x04000063 RID: 99
		private IntPtr onTaskbarHwnd = IntPtr.Zero;

		// Token: 0x04000064 RID: 100
		private IntPtr fullScreenProcessId = IntPtr.Zero;

		// Token: 0x04000065 RID: 101
		private long fullScreenHwndOldStyle;

		// Token: 0x04000066 RID: 102
		private User32.RECT fullScreenHwndRect;

		// Token: 0x04000067 RID: 103
		private MainWindow.HWNDINFO storeFullScreenHwndInfo;

		// Token: 0x04000068 RID: 104
		private JArray gameWhiteList;

		// Token: 0x04000069 RID: 105
		private JArray displayDeviceList;

		// Token: 0x0400006A RID: 106
		private System.Timers.Timer _throttleTimer = new System.Timers.Timer(150.0);

		// Token: 0x0400006B RID: 107
		private Dictionary<IntPtr, DateTime> _hwndCache = new Dictionary<IntPtr, DateTime>();

		// Token: 0x0400006C RID: 108
		private ReorderHandler _reorderHandler;

		// Token: 0x0400006D RID: 109
		private Rectangle _lastScreenRect;

		// Token: 0x0400006E RID: 110
		private MonitorFullInfo g_monitor;

		// Token: 0x0400006F RID: 111
		private IntPtr PrdWin = IntPtr.Zero;

		// Token: 0x04000070 RID: 112
		private IntPtr _ScreenStateNotify;

		// Token: 0x04000071 RID: 113
		private readonly object _lock = new object();

		// Token: 0x04000072 RID: 114
		private bool isReady;

		// Token: 0x04000073 RID: 115
		private bool isScreenOn = true;

		// Token: 0x04000074 RID: 116
		private bool isSessionUnlock = true;

		// Token: 0x04000075 RID: 117
		private bool isMCUInitSuccess;

		// Token: 0x04000076 RID: 118
		private volatile bool isAiStereoEngineEnable;

		// Token: 0x04000077 RID: 119
		private ScreenCaptureWindow captureRender;

		// Token: 0x04000078 RID: 120
		private BasicSampleApplication _sample;

		// Token: 0x04000079 RID: 121
		private Compositor _compositor;

		// Token: 0x0400007A RID: 122
		private ContainerVisual _rootVisual;

		// Token: 0x0400007B RID: 123
		private static readonly object _engineLock = new object();

		// Token: 0x0400007C RID: 124
		public LowLevelHotkey _hotkey;

		// Token: 0x0400007D RID: 125
		private volatile bool _isShuttingDown;

		// Token: 0x0400007E RID: 126
		private int uCallBackMsg;

		// Token: 0x0400007F RID: 127
		private Settings SettingsDialog;

		// Token: 0x04000080 RID: 128
		private const uint WM_APP = 32768U;

		// Token: 0x04000081 RID: 129
		private const uint WM_MY_CUSTOM_MESSAGE = 32769U;

		// Token: 0x04000082 RID: 130
		private const int RASTER_ERROR = 100;

		// Token: 0x04000083 RID: 131
		internal TaskbarIcon TrayIcon;

		// Token: 0x04000084 RID: 132
		private bool _contentLoaded;

		// Token: 0x02000072 RID: 114
		private struct STYLESTRUCT
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001DBCE File Offset: 0x0001BDCE
			// (set) Token: 0x060003E4 RID: 996 RVA: 0x0001DBD6 File Offset: 0x0001BDD6
			public int StyleOld { readonly get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001DBDF File Offset: 0x0001BDDF
			// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001DBE7 File Offset: 0x0001BDE7
			public int StyleNew { readonly get; set; }
		}

		// Token: 0x02000073 RID: 115
		private struct HWNDINFO
		{
			// Token: 0x0400039F RID: 927
			public IntPtr hwnd;

			// Token: 0x040003A0 RID: 928
			public IntPtr associatedHwnd;

			// Token: 0x040003A1 RID: 929
			public IntPtr onTaskbarHwnd;

			// Token: 0x040003A2 RID: 930
			public string clsName;

			// Token: 0x040003A3 RID: 931
			public string title;

			// Token: 0x040003A4 RID: 932
			public IntPtr owner;

			// Token: 0x040003A5 RID: 933
			public IntPtr processId;

			// Token: 0x040003A6 RID: 934
			public long style;

			// Token: 0x040003A7 RID: 935
			public User32.RECT rect;
		}

		// Token: 0x02000074 RID: 116
		private struct DEV_BROADCAST_HDR
		{
			// Token: 0x040003A8 RID: 936
			public uint dbch_size;

			// Token: 0x040003A9 RID: 937
			public uint dbch_devicetype;

			// Token: 0x040003AA RID: 938
			public uint dbch_reserved;
		}
	}
}
