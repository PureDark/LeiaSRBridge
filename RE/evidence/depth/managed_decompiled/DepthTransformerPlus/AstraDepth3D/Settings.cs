using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using AstraDepth3D.Properties;
using AstraDepth3D.WindowHelper;
using Microsoft.Win32.TaskScheduler;

namespace AstraDepth3D
{
	// Token: 0x02000010 RID: 16
	public class Settings : Window, IComponentConnector
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000428B File Offset: 0x0000248B
		public static Settings Instance
		{
			get
			{
				if (Settings._SettingsDialog == null)
				{
					Settings._SettingsDialog = new Settings();
				}
				return Settings._SettingsDialog;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000042A3 File Offset: 0x000024A3
		public Settings()
		{
			this.InitializeComponent();
			base.PreviewMouseDown += this.MainWindow_PreviewMouseDown;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000042D0 File Offset: 0x000024D0
		public void ResetSpatialDepthBar()
		{
			int current_spatial_depth_value = (int)(Math.Round((double)(Math.Max(Math.Min((float)Math.Round((double)(Settings.Default.stereo_parax / 0.05f), MidpointRounding.AwayFromZero) * 0.05f, 1f), 0.05f) * 20f)) / 20.0 * 100.0);
			this.slider_spatial_depth.Value = (double)current_spatial_depth_value;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004340 File Offset: 0x00002540
		public void ResetPopOutOfTheScreenBar()
		{
			int pop_out_of_the_screen_value = (int)(Math.Round((double)(Math.Max(Math.Min((float)Math.Round((double)(Settings.Default.zero_panel / 0.05f), MidpointRounding.AwayFromZero) * 0.05f, 1f), 0f) * 20f)) / 20.0 * 100.0);
			this.slider_pop_out_of_the_screen.Value = (double)pop_out_of_the_screen_value;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000043B0 File Offset: 0x000025B0
		private void InitializeShortcuts(bool subscribeEvents = true)
		{
			int StereoModifier = Settings.Default.StereoModifier;
			int StereoShortcutKey = Settings.Default.StereoShortcutKey;
			string mainKey = this.GetKeyDisplayString(StereoShortcutKey);
			this.Shortcut.Text = (mainKey ?? "");
			this.SetHelperText(this.Shortcut_help, Visibility.Hidden);
			int stereoModifier_index = this.GetModifierIndex(StereoModifier);
			this.Shortcut_Modifier.SelectedIndex = stereoModifier_index;
			if (subscribeEvents)
			{
				this.Shortcut_Modifier.SelectionChanged += this.Shortcut_Modifier_SelectionChanged;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000442B File Offset: 0x0000262B
		public void RestShortcut()
		{
			this.Shortcut_Modifier.SelectionChanged -= this.Shortcut_Modifier_SelectionChanged;
			this.InitializeShortcuts(true);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000444C File Offset: 0x0000264C
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			Version assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
			this.VersionInfo.Text = string.Format("{0}.{1}.{2}.{3:D3}", new object[]
			{
				assemblyVersion.Major,
				assemblyVersion.Minor,
				assemblyVersion.Build,
				assemblyVersion.Revision
			});
			int langIndex = Settings.Default.Language;
			NLogService.WriteLog(string.Format("langIndex:{0}", langIndex));
			this.Language.SelectedIndex = ((langIndex >= this.Language.Items.Count) ? 0 : langIndex);
			this.Language.SelectionChanged += this.Language_SelectionChanged;
			this.InitializeShortcuts(true);
			int current_spatial_depth_value = (int)(Math.Round((double)(Math.Max(Math.Min((float)Math.Round((double)(Settings.Default.stereo_parax / 0.05f), MidpointRounding.AwayFromZero) * 0.05f, 1f), 0.05f) * 20f)) / 20.0 * 100.0);
			this.slider_spatial_depth.Value = (double)current_spatial_depth_value;
			this.spatial_depth_percent.Text = string.Format("{0}%", current_spatial_depth_value);
			this.slider_spatial_depth.ValueChanged += this.Slider_spatial_depth_ValueChanged;
			int pop_out_of_the_screen_value = (int)(Math.Round((double)(Math.Max(Math.Min((float)Math.Round((double)(Settings.Default.zero_panel / 0.05f), MidpointRounding.AwayFromZero) * 0.05f, 1f), 0f) * 20f)) / 20.0 * 100.0);
			this.slider_pop_out_of_the_screen.Value = (double)pop_out_of_the_screen_value;
			this.pop_out_of_the_screen_percent.Text = string.Format("{0}%", pop_out_of_the_screen_value);
			this.slider_pop_out_of_the_screen.ValueChanged += this.Slider_pop_out_of_the_screen_ValueChanged;
			string device = Settings.Default.InferenceDevice.ToLower();
			if (device == "npu")
			{
				this.infer_device.SelectedIndex = 1;
			}
			else if (device == "gpu")
			{
				this.infer_device.SelectedIndex = 2;
			}
			else
			{
				this.infer_device.SelectedIndex = 0;
			}
			this.infer_device.SelectionChanged += this.infer_device_SelectionChanged;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000046B0 File Offset: 0x000028B0
		private void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			IInputElement element = FocusManager.GetFocusedElement(this);
			if (element != null && !element.IsMouseOver)
			{
				FocusManager.SetFocusedElement(this, this);
			}
			base.OnPreviewMouseDown(e);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000046DD File Offset: 0x000028DD
		private void Window_Deactivated(object sender, EventArgs e)
		{
			FocusManager.SetFocusedElement(this, null);
			Keyboard.ClearFocus();
			NLogService.WriteLine("窗口失去焦点!");
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000046F5 File Offset: 0x000028F5
		private void HotkeyBox_2d3dPreviewKeyDown(object sender, KeyEventArgs e)
		{
			this.ProcessShortcutKey(sender as TextBox, this.Shortcut_Modifier, this.Shortcut_help, delegate(int mod, int key)
			{
				Settings.Default.StereoModifier = mod;
				Settings.Default.StereoShortcutKey = key;
				Settings.Default.Save();
			}, e);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004730 File Offset: 0x00002930
		private void ResetStereoShortcutButton_Click(object sender, RoutedEventArgs e)
		{
			Settings.Default.StereoModifier = 2;
			Settings.Default.StereoShortcutKey = KeyInterop.VirtualKeyFromKey(Key.F2);
			Settings.Default.Save();
			this.RestShortcut();
			if (this.ShortcutSettingsChanged != null)
			{
				this.ShortcutSettingsChanged(1);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004780 File Offset: 0x00002980
		private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = (sender as ComboBox).SelectedIndex;
			NLogService.WriteLog(string.Format("Language_SelectionChanged:{0}", index));
			if (index == Settings.Default.Language)
			{
				return;
			}
			Settings.Default.Language = index;
			Settings.Default.Save();
			SettingsTools.ResetLanguage(index);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000047D8 File Offset: 0x000029D8
		private void AutoStart_Click(object sender, RoutedEventArgs e)
		{
			Settings.<AutoStart_Click>d__18 <AutoStart_Click>d__;
			<AutoStart_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AutoStart_Click>d__.<>4__this = this;
			<AutoStart_Click>d__.sender = sender;
			<AutoStart_Click>d__.<>1__state = -1;
			<AutoStart_Click>d__.<>t__builder.Start<Settings.<AutoStart_Click>d__18>(ref <AutoStart_Click>d__);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004817 File Offset: 0x00002A17
		private void Shortcut_GotFocus(object sender, RoutedEventArgs e)
		{
			(sender as TextBox).Text = "";
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000482C File Offset: 0x00002A2C
		private void Shortcut_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			this.GetModifierString(Settings.Default.StereoModifier);
			string mainKey = this.GetKeyDisplayString(Settings.Default.StereoShortcutKey);
			textBox.Text = (mainKey ?? "");
			this.SetHelperText(this.Shortcut_help, Visibility.Hidden);
			this.Shortcut_Modifier.SelectedIndex = this.GetModifierIndex(Settings.Default.StereoModifier);
			if (this.ShortcutSettingsChanged != null)
			{
				this.ShortcutSettingsChanged(1);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000048AC File Offset: 0x00002AAC
		private void Shortcut_Modifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.Shortcut.Focus();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000048BC File Offset: 0x00002ABC
		private void Slider_spatial_depth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			int currentValue = (int)e.NewValue;
			Slider slider = sender as Slider;
			if (slider.Value < 5.0)
			{
				slider.Value = 5.0;
				currentValue = 5;
			}
			this.spatial_depth_percent.Text = string.Format("{0}%", currentValue);
			float currentValue_float = (float)currentValue / 100f;
			if (Settings.Default.stereo_parax != currentValue_float)
			{
				Settings.Default.stereo_parax = (float)currentValue / 100f;
				Settings.Default.Save();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004948 File Offset: 0x00002B48
		private void Slider_pop_out_of_the_screen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			int currentValue = (int)e.NewValue;
			this.pop_out_of_the_screen_percent.Text = string.Format("{0}%", currentValue);
			float currentValue_float = (float)currentValue / 100f;
			if (Settings.Default.zero_panel != currentValue_float)
			{
				Settings.Default.zero_panel = (float)currentValue / 100f;
				Settings.Default.Save();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000049AC File Offset: 0x00002BAC
		public static void CreateAndRunTask(string appPath)
		{
			try
			{
				using (TaskService ts = new TaskService())
				{
					Task launcherTask = ts.GetTask("DepthTransformerLauncherTask");
					if (launcherTask != null)
					{
						launcherTask.Run(Array.Empty<string>());
					}
					else
					{
						string currentUser = Settings.GetCurrentUser();
						NLogService.WriteLog("当前用户: " + currentUser);
						TaskDefinition td = ts.NewTask();
						td.RegistrationInfo.Description = "DepthTransformer启动服务";
						td.Principal.RunLevel = 1;
						LogonTrigger logonTrigger = new LogonTrigger();
						td.Triggers.Add<LogonTrigger>(logonTrigger);
						td.Actions.Add<ExecAction>(new ExecAction(appPath, null, null));
						td.Principal.UserId = currentUser;
						td.Principal.LogonType = 3;
						td.Settings.AllowDemandStart = true;
						td.Settings.Enabled = true;
						td.Settings.Hidden = false;
						td.Settings.StartWhenAvailable = true;
						td.Settings.DisallowStartIfOnBatteries = false;
						td.Settings.StopIfGoingOnBatteries = false;
						td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
						ts.RootFolder.RegisterTaskDefinition("DepthTransformerLauncherTask", td, 6, null, null, 3, null);
						ts.GetTask("DepthTransformerLauncherTask").Run(Array.Empty<string>());
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("运行任务失败：" + ex.ToString());
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004B3C File Offset: 0x00002D3C
		public static bool DeleteTask()
		{
			bool result;
			try
			{
				using (TaskService ts = new TaskService())
				{
					ts.RootFolder.DeleteTask("DepthTransformerLauncherTask", true);
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004B90 File Offset: 0x00002D90
		public static bool IsTaskRunning()
		{
			bool result;
			using (TaskService ts = new TaskService())
			{
				Task task = ts.GetTask("DepthTransformerLauncherTask");
				result = (task != null && task.State == 4);
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004BDC File Offset: 0x00002DDC
		public static string GetCurrentUser()
		{
			string result;
			try
			{
				string sessionUser = Settings.GetInteractiveSessionUser();
				if (!string.IsNullOrEmpty(sessionUser))
				{
					result = sessionUser;
				}
				else
				{
					result = WindowsIdentity.GetCurrent().Name;
				}
			}
			catch
			{
				result = Environment.UserName;
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004C24 File Offset: 0x00002E24
		private static string GetInteractiveSessionUser()
		{
			string result;
			try
			{
				ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
				ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE Name = 'explorer.exe'");
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
				{
					foreach (ManagementBaseObject managementBaseObject in searcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						string[] ownerInfo = new string[2];
						string methodName = "GetOwner";
						object[] args = ownerInfo;
						managementObject.InvokeMethod(methodName, args);
						if (!string.IsNullOrEmpty(ownerInfo[0]))
						{
							return Environment.MachineName + "\\" + ownerInfo[0];
						}
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004CF4 File Offset: 0x00002EF4
		private void Window_Closed(object sender, EventArgs e)
		{
			Settings._SettingsDialog = null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004CFC File Offset: 0x00002EFC
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (FocusManager.GetFocusedElement(this) is TextBox)
			{
				FocusManager.SetFocusedElement(this, this);
				Keyboard.ClearFocus();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004D17 File Offset: 0x00002F17
		private void SetHelperText(TextBlock textBox, Visibility visibility)
		{
			textBox.Visibility = visibility;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D20 File Offset: 0x00002F20
		private void ProcessShortcutKey(TextBox textBox, ComboBox modifierComboBox, TextBlock helpTextBlock, Action<int, int> saveSettingsCallback, KeyEventArgs e)
		{
			if (textBox == null)
			{
				return;
			}
			if (e.IsRepeat)
			{
				return;
			}
			Key key = (e.Key == Key.System) ? e.SystemKey : e.Key;
			if (key == Key.LeftCtrl || key == Key.RightCtrl || key == Key.LeftAlt || key == Key.RightAlt || key == Key.LeftShift || key == Key.RightShift || key == Key.LWin || key == Key.RWin || key == Key.Tab || key == Key.NumLock)
			{
				return;
			}
			int modifierKeys;
			switch (modifierComboBox.SelectedIndex)
			{
			case 0:
				modifierKeys = 2;
				break;
			case 1:
				modifierKeys = 1;
				break;
			case 2:
				modifierKeys = 9;
				break;
			default:
				return;
			}
			string mainKey = this.GetKeyDisplayString(key);
			textBox.Text = mainKey;
			int virtualKey = KeyInterop.VirtualKeyFromKey(key);
			if (!this.isShortcutCanbeRegister(modifierKeys, virtualKey))
			{
				NLogService.WriteLog(string.Format("快捷键 {0}+{1} 已被占用", modifierKeys, key));
				this.SetHelperText(helpTextBlock, Visibility.Visible);
				e.Handled = true;
				return;
			}
			if (!this.isLowLevelHotkeyCanbeRegister(modifierKeys, virtualKey))
			{
				NLogService.WriteLog(string.Format("LowLevelHotkey:{0}+{1} 已被占用", modifierKeys, key));
				this.SetHelperText(helpTextBlock, Visibility.Visible);
				e.Handled = true;
				return;
			}
			this.SetHelperText(helpTextBlock, Visibility.Hidden);
			if (saveSettingsCallback != null)
			{
				saveSettingsCallback(modifierKeys, virtualKey);
			}
			e.Handled = true;
			FocusManager.SetFocusedElement(this, null);
			Keyboard.ClearFocus();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004E62 File Offset: 0x00003062
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004E64 File Offset: 0x00003064
		private bool isShortcutRepeat(int modifier, int key)
		{
			int stereoModifier = Settings.Default.StereoModifier;
			int stereoShortcutKey = Settings.Default.StereoShortcutKey;
			if (stereoModifier == modifier && stereoShortcutKey == modifier)
			{
				return true;
			}
			int stereoParaxAddModifier = Settings.Default.StereoShortcutParaxAddModifier;
			int stereoShortcutParaxAdd = Settings.Default.StereoShortcutParaxAdd;
			if (stereoParaxAddModifier == modifier && stereoShortcutParaxAdd == modifier)
			{
				return true;
			}
			int stereoShortcutParaxSubtractModifier = Settings.Default.StereoShortcutParaxSubtractModifier;
			int stereoShortcutParaxSubtract = Settings.Default.StereoShortcutParaxSubtract;
			return stereoParaxAddModifier == modifier && stereoShortcutParaxAdd == modifier;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004ED0 File Offset: 0x000030D0
		private bool isShortcutCanbeRegister(int modifier, int key)
		{
			bool result;
			try
			{
				if (modifier == 9 && key > 0)
				{
					result = true;
				}
				else if (User32.RegisterHotKey(IntPtr.Zero, 9999, (uint)modifier, (uint)key))
				{
					User32.UnregisterHotKey(IntPtr.Zero, 9999);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004F2C File Offset: 0x0000312C
		private bool isLowLevelHotkeyCanbeRegister(int modifier, int key)
		{
			LowLevelHotkey hotkey = (Application.Current.MainWindow as MainWindow)._hotkey;
			bool? flag = (hotkey != null) ? new bool?(hotkey.IsHotkeyRegistered(key, modifier)) : null;
			bool flag2 = false;
			return flag.GetValueOrDefault() == flag2 & flag != null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F80 File Offset: 0x00003180
		private string GetKeyDisplayString(int virtualKey)
		{
			Key key = KeyInterop.KeyFromVirtualKey(virtualKey);
			return this.GetKeyDisplayString(key);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004F9C File Offset: 0x0000319C
		private string GetKeyDisplayString(Key key)
		{
			if (key <= Key.Add)
			{
				switch (key)
				{
				case Key.Back:
					return "Backspace";
				case Key.Tab:
					return "Tab";
				case Key.LineFeed:
				case Key.Clear:
				case Key.Pause:
				case Key.KanaMode:
				case Key.JunjaMode:
				case Key.FinalMode:
				case Key.HanjaMode:
				case Key.ImeConvert:
				case Key.ImeNonConvert:
				case Key.ImeAccept:
				case Key.ImeModeChange:
				case Key.Select:
				case Key.Print:
				case Key.Execute:
				case Key.Snapshot:
				case Key.Help:
					break;
				case Key.Return:
					return "Enter";
				case Key.Capital:
					return "CapsLock";
				case Key.Escape:
					return "Esc";
				case Key.Space:
					return "Space";
				case Key.Prior:
					return "PageUp";
				case Key.Next:
					return "PageDown";
				case Key.End:
					return "End";
				case Key.Home:
					return "Home";
				case Key.Left:
					return "←";
				case Key.Up:
					return "↑";
				case Key.Right:
					return "→";
				case Key.Down:
					return "↓";
				case Key.Insert:
					return "Insert";
				case Key.Delete:
					return "Delete";
				case Key.D0:
					return "0";
				case Key.D1:
					return "1";
				case Key.D2:
					return "2";
				case Key.D3:
					return "3";
				case Key.D4:
					return "4";
				case Key.D5:
					return "5";
				case Key.D6:
					return "6";
				case Key.D7:
					return "7";
				case Key.D8:
					return "8";
				case Key.D9:
					return "9";
				default:
					if (key == Key.Add)
					{
						return "+";
					}
					break;
				}
			}
			else
			{
				if (key == Key.Subtract)
				{
					return "-";
				}
				switch (key)
				{
				case Key.NumLock:
					return "NumLock";
				case Key.Scroll:
					return "ScrollLock";
				case Key.BrowserBack:
					return "BrowserBack";
				case Key.BrowserForward:
					return "BrowserForward";
				case Key.BrowserRefresh:
					return "BrowserRefresh";
				case Key.BrowserStop:
					return "BrowserStop";
				case Key.BrowserSearch:
					return "BrowserSearch";
				case Key.BrowserFavorites:
					return "BrowserFavorites";
				case Key.BrowserHome:
					return "BrowserHome";
				case Key.VolumeMute:
					return "VolumeMute";
				case Key.VolumeDown:
					return "VolumeDown";
				case Key.VolumeUp:
					return "VolumeUp";
				case Key.MediaNextTrack:
					return "NextTrack";
				case Key.MediaPreviousTrack:
					return "PrevTrack";
				case Key.MediaStop:
					return "MediaStop";
				case Key.MediaPlayPause:
					return "PlayPause";
				case Key.LaunchMail:
					return "Mail";
				case Key.SelectMedia:
					return "SelectMedia";
				case Key.LaunchApplication1:
					return "App1";
				case Key.LaunchApplication2:
					return "App2";
				case Key.Oem1:
					return ";";
				case Key.OemPlus:
					return "+";
				case Key.OemComma:
					return ",";
				case Key.OemMinus:
					return "-";
				case Key.OemPeriod:
					return ".";
				case Key.Oem2:
					return "?";
				case Key.Oem3:
					return "~";
				case Key.Oem4:
					return "[";
				case Key.Oem5:
					return "|";
				case Key.Oem6:
					return "]";
				case Key.Oem7:
					return "'";
				case Key.Oem102:
					return "\\";
				}
			}
			if (key >= Key.A && key <= Key.Z)
			{
				return key.ToString();
			}
			if (key >= Key.F1)
			{
				return key.ToString();
			}
			return key.ToString();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000052CD File Offset: 0x000034CD
		private string GetModifierString(int keyValue)
		{
			if (keyValue == 1)
			{
				return "Alt";
			}
			if (keyValue == 4)
			{
				return "Shift";
			}
			if (keyValue == 2)
			{
				return "Ctrl";
			}
			if (keyValue == 8)
			{
				return "Windows";
			}
			if (keyValue == 9)
			{
				return "Tab";
			}
			return "Ctrl";
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005307 File Offset: 0x00003507
		private int GetModifierIndex(int modifierKeys)
		{
			if (modifierKeys == 2)
			{
				return 0;
			}
			if (modifierKeys == 1)
			{
				return 1;
			}
			if (modifierKeys == 9)
			{
				return 2;
			}
			return -1;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005320 File Offset: 0x00003520
		private void infer_device_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = (sender as ComboBox).SelectedIndex;
			string deviceName;
			if (index == 1)
			{
				deviceName = "npu";
			}
			else if (index == 2)
			{
				deviceName = "gpu";
			}
			else
			{
				deviceName = "auto";
			}
			if (deviceName != Settings.Default.InferenceDevice)
			{
				Settings.Default.InferenceDevice = deviceName;
				Settings.Default.Save();
				if (this.InferDeviceChanged != null)
				{
					this.InferDeviceChanged();
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005394 File Offset: 0x00003594
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocater = new Uri("/DepthTransformerPlus;component/settings.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocater);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000053C4 File Offset: 0x000035C4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((Settings)target).Closed += this.Window_Closed;
				((Settings)target).Deactivated += this.Window_Deactivated;
				((Settings)target).Loaded += this.Window_Loaded;
				((Settings)target).SourceInitialized += this.Window_SourceInitialized;
				return;
			case 2:
				this.Shortcut_Modifier = (ComboBox)target;
				return;
			case 3:
				this.Shortcut = (TextBox)target;
				this.Shortcut.GotFocus += this.Shortcut_GotFocus;
				this.Shortcut.LostFocus += this.Shortcut_LostFocus;
				this.Shortcut.PreviewKeyDown += this.HotkeyBox_2d3dPreviewKeyDown;
				return;
			case 4:
				this.Shortcut_help = (TextBlock)target;
				return;
			case 5:
				this.Shortcut_parax_add_help = (TextBlock)target;
				return;
			case 6:
				this.Shortcut_parax_subtract_help = (TextBlock)target;
				return;
			case 7:
				this.Shortcut_zp_add_help = (TextBlock)target;
				return;
			case 8:
				this.Shortcut_zp_subtract_help = (TextBlock)target;
				return;
			case 9:
				((Button)target).Click += this.ResetStereoShortcutButton_Click;
				return;
			case 10:
				this.infer_device = (ComboBox)target;
				return;
			case 11:
				this.spatial_depth_percent = (TextBlock)target;
				return;
			case 12:
				this.slider_spatial_depth = (Slider)target;
				return;
			case 13:
				this.pop_out_of_the_screen_percent = (TextBlock)target;
				return;
			case 14:
				this.slider_pop_out_of_the_screen = (Slider)target;
				return;
			case 15:
				this.Language = (ComboBox)target;
				return;
			case 16:
				this.VersionInfo = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400003C RID: 60
		private static Settings _SettingsDialog;

		// Token: 0x0400003D RID: 61
		public MessageHelp.ShortcutSettingsChanged ShortcutSettingsChanged;

		// Token: 0x0400003E RID: 62
		public MessageHelp.InferDeviceChanged InferDeviceChanged;

		// Token: 0x0400003F RID: 63
		private readonly SemaphoreSlim _autoStartSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000040 RID: 64
		private CancellationTokenSource _cancellationTokenSource;

		// Token: 0x04000041 RID: 65
		internal ComboBox Shortcut_Modifier;

		// Token: 0x04000042 RID: 66
		internal TextBox Shortcut;

		// Token: 0x04000043 RID: 67
		internal TextBlock Shortcut_help;

		// Token: 0x04000044 RID: 68
		internal TextBlock Shortcut_parax_add_help;

		// Token: 0x04000045 RID: 69
		internal TextBlock Shortcut_parax_subtract_help;

		// Token: 0x04000046 RID: 70
		internal TextBlock Shortcut_zp_add_help;

		// Token: 0x04000047 RID: 71
		internal TextBlock Shortcut_zp_subtract_help;

		// Token: 0x04000048 RID: 72
		internal ComboBox infer_device;

		// Token: 0x04000049 RID: 73
		internal TextBlock spatial_depth_percent;

		// Token: 0x0400004A RID: 74
		internal Slider slider_spatial_depth;

		// Token: 0x0400004B RID: 75
		internal TextBlock pop_out_of_the_screen_percent;

		// Token: 0x0400004C RID: 76
		internal Slider slider_pop_out_of_the_screen;

		// Token: 0x0400004D RID: 77
		internal new ComboBox Language;

		// Token: 0x0400004E RID: 78
		internal TextBlock VersionInfo;

		// Token: 0x0400004F RID: 79
		private bool _contentLoaded;
	}
}
