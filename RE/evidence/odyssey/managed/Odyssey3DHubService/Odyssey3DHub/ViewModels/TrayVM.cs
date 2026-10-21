using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Odyssey3DHub.Dialogs;
using \u00A0;

namespace Odyssey3DHub.ViewModels
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class TrayVM : INotifyPropertyChanged
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000046D5 File Offset: 0x000028D5
		public static TrayVM Instance
		{
			get
			{
				return TrayVM.instance.Value;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000046E4 File Offset: 0x000028E4
		private TrayVM()
		{
			this.RunCommand = new RelayCommand(new Action<object>(this.OnAppOpen));
			this.AboutCommand = new RelayCommand(new Action<object>(this.OnAboutOpen));
			this.UpdateCommand = new RelayCommand(new Action<object>(this.OnStartUpdate));
			this.QuitCommand = new RelayCommand(new Action<object>(this.OnAppExit));
			this.ToggleConvertTo3DCommand = new RelayCommand(new Action<object>(this.OnToggleConvertTo3D));
			this.ToggleAutoConvertCommand = new RelayCommand(new Action<object>(this.OnToggleAutoConvert));
			this.SelectStandardVideoCommand = new RelayCommand(new Action<object>(this.OnSelectStandardVideo));
			this.SelectSplitVideoCommand = new RelayCommand(new Action<object>(this.OnSelectSplitVideo));
			this.IsConvertTo3DEnabled = (\u2003.Instance.SettingsVM.IsConvertVideoTo3D == 1);
			this.HasGpuSupport = \u202B.\u2002();
			this.IsAutoConvertEnabled = (\u2003.Instance.SettingsVM.IsAutoConvert == 1);
			this.IsStandardVideo = (\u2003.Instance.SettingsVM.IsStandardVideo == 1);
			\u2014.\u00A0(new Action(this.RefreshLanguage));
			this.RefreshLanguage();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004819 File Offset: 0x00002A19
		public ICommand RunCommand { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004821 File Offset: 0x00002A21
		public ICommand AboutCommand { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004829 File Offset: 0x00002A29
		public ICommand UpdateCommand { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004831 File Offset: 0x00002A31
		public ICommand QuitCommand { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004839 File Offset: 0x00002A39
		public ICommand ToggleConvertTo3DCommand { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004841 File Offset: 0x00002A41
		public ICommand ToggleAutoConvertCommand { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004849 File Offset: 0x00002A49
		public ICommand SelectStandardVideoCommand { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004851 File Offset: 0x00002A51
		public ICommand SelectSplitVideoCommand { get; }

		// Token: 0x060000CA RID: 202 RVA: 0x0000485C File Offset: 0x00002A5C
		public void RefreshLanguage()
		{
			TrayVM.RunText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206E()).Replace(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206F(), \u2006.\u2001);
			TrayVM.AboutText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u3000()).Replace(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206F(), \u2006.\u2001);
			TrayVM.UpdateText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u00A0());
			TrayVM.QuitText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u1680()).Replace(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206F(), \u2006.\u2001);
			TrayVM.ConvertTo3DText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2000());
			TrayVM.AutoConvertText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2001());
			TrayVM.VideoTypeText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2002());
			TrayVM.StandardVideoText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2003());
			TrayVM.SplitVideoText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2004());
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000491D File Offset: 0x00002B1D
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004925 File Offset: 0x00002B25
		public bool HasGpuSupport
		{
			get
			{
				return this.hasGpuSupport;
			}
			private set
			{
				if (this.hasGpuSupport != value)
				{
					this.hasGpuSupport = value;
					this.OnPropertyChanged(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2005());
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004942 File Offset: 0x00002B42
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000494A File Offset: 0x00002B4A
		public bool IsConvertTo3DEnabled
		{
			get
			{
				return this.isConvertTo3DEnabled;
			}
			set
			{
				if (this.isConvertTo3DEnabled != value)
				{
					this.isConvertTo3DEnabled = value;
					this.OnPropertyChanged(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2006());
					\u2003.Instance.SettingsVM.IsConvertVideoTo3D = ((value > false) ? 1 : 0);
					this.OnPropertyChanged(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2007());
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004985 File Offset: 0x00002B85
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004990 File Offset: 0x00002B90
		public bool IsAutoConvertEnabled
		{
			get
			{
				return this.isAutoConvertEnabled;
			}
			set
			{
				if (this.isAutoConvertEnabled != value)
				{
					this.isAutoConvertEnabled = value;
					this.OnPropertyChanged(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2008());
					if (this.isConvertTo3DEnabled)
					{
						\u2003.Instance.SettingsVM.IsAutoConvert = ((value > false) ? 1 : 0);
					}
					this.OnPropertyChanged(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2007());
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000049DE File Offset: 0x00002BDE
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000049E6 File Offset: 0x00002BE6
		public bool IsStandardVideo
		{
			get
			{
				return this.isStandardVideo;
			}
			set
			{
				if (this.isStandardVideo != value)
				{
					this.isStandardVideo = value;
					this.OnPropertyChanged(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2009());
					\u2003.Instance.SettingsVM.IsStandardVideo = ((value > false) ? 1 : 0);
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004A16 File Offset: 0x00002C16
		public bool IsVideoTypeEnabled
		{
			get
			{
				return this.isConvertTo3DEnabled && this.isAutoConvertEnabled;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004A28 File Offset: 0x00002C28
		[NullableContext(2)]
		private void OnAppOpen(object parameter)
		{
			if (Process.GetProcessesByName(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u200A()).Length == 0)
			{
				string text = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u200B();
				if (File.Exists(text))
				{
					Process.Start(text);
				}
			}
			else
			{
				\u2007.\u1680();
			}
			this.CloseNotificationArea();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004A64 File Offset: 0x00002C64
		private void CloseNotificationArea()
		{
			IntPtr intPtr = \u202D.\u00A0(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2010(), null);
			if (intPtr != IntPtr.Zero)
			{
				\u202D.\u00A0(intPtr, \u202D.\u2000);
			}
			IntPtr intPtr2 = \u202D.\u00A0(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2011(), null);
			if (intPtr2 != IntPtr.Zero)
			{
				\u202D.\u00A0(intPtr2, \u202D.\u2000);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004AB1 File Offset: 0x00002CB1
		[NullableContext(2)]
		private void OnAboutOpen(object parameter)
		{
			AboutDialog aboutDialog = new AboutDialog();
			aboutDialog.PositionWindowAtCenter();
			aboutDialog.Topmost = true;
			aboutDialog.ShowDialog();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004ACC File Offset: 0x00002CCC
		[NullableContext(2)]
		private void OnStartUpdate(object parameter)
		{
			\u2025.\u00A0(TrayVM.TAG, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2012());
			string text = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205C();
			if (File.Exists(text))
			{
				using (Process.Start(text, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2013()))
				{
				}
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004B20 File Offset: 0x00002D20
		[NullableContext(2)]
		private void OnAppExit(object parameter)
		{
			\u2025.\u00A0(TrayVM.TAG, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2014());
			\u2025.\u00A0(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2015(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2022(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2014());
			\u2007.\u2000();
			\u2035.\u2009();
			\u1680.\u2002();
			\u204A.Instance.\u1680();
			\u2007.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2024());
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004B73 File Offset: 0x00002D73
		[NullableContext(2)]
		private void OnToggleConvertTo3D(object parameter)
		{
			this.IsConvertTo3DEnabled = !this.IsConvertTo3DEnabled;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004B84 File Offset: 0x00002D84
		[NullableContext(2)]
		private void OnToggleAutoConvert(object parameter)
		{
			if (this.isConvertTo3DEnabled)
			{
				this.IsAutoConvertEnabled = !this.IsAutoConvertEnabled;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004B9D File Offset: 0x00002D9D
		[NullableContext(2)]
		private void OnSelectStandardVideo(object parameter)
		{
			this.IsStandardVideo = true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004BA6 File Offset: 0x00002DA6
		[NullableContext(2)]
		private void OnSelectSplitVideo(object parameter)
		{
			this.IsStandardVideo = false;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000DD RID: 221 RVA: 0x00004BB0 File Offset: 0x00002DB0
		// (remove) Token: 0x060000DE RID: 222 RVA: 0x00004BE8 File Offset: 0x00002DE8
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000DF RID: 223 RVA: 0x00004C1D File Offset: 0x00002E1D
		[NullableContext(2)]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x04000072 RID: 114
		private static readonly string TAG = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2025();

		// Token: 0x04000073 RID: 115
		private static readonly Lazy<TrayVM> instance = new Lazy<TrayVM>(new Func<TrayVM>(TrayVM.\u00A0.\u00A0.\u00A0));

		// Token: 0x04000074 RID: 116
		private bool isConvertTo3DEnabled;

		// Token: 0x04000075 RID: 117
		private bool isAutoConvertEnabled;

		// Token: 0x04000076 RID: 118
		private bool isStandardVideo;

		// Token: 0x04000077 RID: 119
		private bool hasGpuSupport;

		// Token: 0x04000078 RID: 120
		public static string RunText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206E()).Replace(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206F(), \u2006.\u2001);

		// Token: 0x04000079 RID: 121
		public static string AboutText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u3000()).Replace(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206F(), \u2006.\u2001);

		// Token: 0x0400007A RID: 122
		public static string UpdateText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u00A0());

		// Token: 0x0400007B RID: 123
		public static string QuitText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u1680()).Replace(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206F(), \u2006.\u2001);

		// Token: 0x0400007C RID: 124
		public static string ConvertTo3DText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2000());

		// Token: 0x0400007D RID: 125
		public static string AutoConvertText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2001());

		// Token: 0x0400007E RID: 126
		public static string VideoTypeText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2002());

		// Token: 0x0400007F RID: 127
		public static string StandardVideoText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2003());

		// Token: 0x04000080 RID: 128
		public static string SplitVideoText = \u2014.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2004());

		// Token: 0x0200001D RID: 29
		[CompilerGenerated]
		[Serializable]
		private sealed class \u00A0
		{
			// Token: 0x060000E3 RID: 227 RVA: 0x00004D29 File Offset: 0x00002F29
			internal TrayVM \u00A0()
			{
				return new TrayVM();
			}

			// Token: 0x0400008A RID: 138
			[Nullable(0)]
			public static readonly TrayVM.\u00A0 \u00A0 = new TrayVM.\u00A0();
		}
	}
}
