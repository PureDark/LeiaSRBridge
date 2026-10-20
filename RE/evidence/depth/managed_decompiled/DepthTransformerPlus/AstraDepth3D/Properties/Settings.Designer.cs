using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AstraDepth3D.Properties
{
	// Token: 0x02000018 RID: 24
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "18.5.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000ABBB File Offset: 0x00008DBB
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000ABCD File Offset: 0x00008DCD
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0.35")]
		public float stereo_parax
		{
			get
			{
				return (float)this["stereo_parax"];
			}
			set
			{
				this["stereo_parax"] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0.5")]
		public float zero_panel
		{
			get
			{
				return (float)this["zero_panel"];
			}
			set
			{
				this["zero_panel"] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000AC05 File Offset: 0x00008E05
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000AC17 File Offset: 0x00008E17
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		public int Language
		{
			get
			{
				return (int)this["Language"];
			}
			set
			{
				this["Language"] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000AC2A File Offset: 0x00008E2A
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000AC3C File Offset: 0x00008E3C
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool AutoStart
		{
			get
			{
				return (bool)this["AutoStart"];
			}
			set
			{
				this["AutoStart"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000AC4F File Offset: 0x00008E4F
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000AC61 File Offset: 0x00008E61
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("113")]
		public int StereoShortcutKey
		{
			get
			{
				return (int)this["StereoShortcutKey"];
			}
			set
			{
				this["StereoShortcutKey"] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000AC74 File Offset: 0x00008E74
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000AC86 File Offset: 0x00008E86
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("2")]
		public int StereoModifier
		{
			get
			{
				return (int)this["StereoModifier"];
			}
			set
			{
				this["StereoModifier"] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000AC99 File Offset: 0x00008E99
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000ACAB File Offset: 0x00008EAB
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("38")]
		public int StereoShortcutParaxAdd
		{
			get
			{
				return (int)this["StereoShortcutParaxAdd"];
			}
			set
			{
				this["StereoShortcutParaxAdd"] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000ACBE File Offset: 0x00008EBE
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("40")]
		public int StereoShortcutParaxSubtract
		{
			get
			{
				return (int)this["StereoShortcutParaxSubtract"];
			}
			set
			{
				this["StereoShortcutParaxSubtract"] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000ACE3 File Offset: 0x00008EE3
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000ACF5 File Offset: 0x00008EF5
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("2")]
		public int StereoShortcutParaxAddModifier
		{
			get
			{
				return (int)this["StereoShortcutParaxAddModifier"];
			}
			set
			{
				this["StereoShortcutParaxAddModifier"] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000AD08 File Offset: 0x00008F08
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000AD1A File Offset: 0x00008F1A
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("2")]
		public int StereoShortcutParaxSubtractModifier
		{
			get
			{
				return (int)this["StereoShortcutParaxSubtractModifier"];
			}
			set
			{
				this["StereoShortcutParaxSubtractModifier"] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000AD2D File Offset: 0x00008F2D
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000AD3F File Offset: 0x00008F3F
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("38")]
		public int StereoShortcutZPAdd
		{
			get
			{
				return (int)this["StereoShortcutZPAdd"];
			}
			set
			{
				this["StereoShortcutZPAdd"] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000AD52 File Offset: 0x00008F52
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000AD64 File Offset: 0x00008F64
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("40")]
		public int StereoShortcutZPSubtract
		{
			get
			{
				return (int)this["StereoShortcutZPSubtract"];
			}
			set
			{
				this["StereoShortcutZPSubtract"] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000AD77 File Offset: 0x00008F77
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000AD89 File Offset: 0x00008F89
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("1")]
		public int StereoShortcutZPAddModifier
		{
			get
			{
				return (int)this["StereoShortcutZPAddModifier"];
			}
			set
			{
				this["StereoShortcutZPAddModifier"] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000AD9C File Offset: 0x00008F9C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000ADAE File Offset: 0x00008FAE
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("1")]
		public int StereoShortcutZPSubtractModifier
		{
			get
			{
				return (int)this["StereoShortcutZPSubtractModifier"];
			}
			set
			{
				this["StereoShortcutZPSubtractModifier"] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000ADC1 File Offset: 0x00008FC1
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000ADD3 File Offset: 0x00008FD3
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("auto")]
		public string InferenceDevice
		{
			get
			{
				return (string)this["InferenceDevice"];
			}
			set
			{
				this["InferenceDevice"] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000ADE1 File Offset: 0x00008FE1
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000ADF3 File Offset: 0x00008FF3
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool UpgradeRequired
		{
			get
			{
				return (bool)this["UpgradeRequired"];
			}
			set
			{
				this["UpgradeRequired"] = value;
			}
		}

		// Token: 0x0400008E RID: 142
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
