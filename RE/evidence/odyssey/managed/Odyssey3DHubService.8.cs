using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using \u1680;

namespace \u00A0
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	[JsonObject("Settings")]
	public class \u2005
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000402C File Offset: 0x0000222C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004034 File Offset: 0x00002234
		public int CursorType
		{
			get
			{
				return this.\u00A0;
			}
			set
			{
				if (this.\u00A0 != value)
				{
					this.\u00A0 = value;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004046 File Offset: 0x00002246
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004050 File Offset: 0x00002250
		public int IsConvertVideoTo3D
		{
			get
			{
				return this.\u1680;
			}
			set
			{
				if (this.\u1680 != value)
				{
					this.\u1680 = value;
					if (!global::\u00A0.\u2005.\u00A0)
					{
						Application.Current.Dispatcher.Invoke(new Action(global::\u00A0.\u2005.\u00A0.\u00A0.\u00A0));
					}
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000040A2 File Offset: 0x000022A2
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000040AA File Offset: 0x000022AA
		public int IsAutoGameScan
		{
			get
			{
				return this.\u2000;
			}
			set
			{
				if (this.\u2000 != value)
				{
					this.\u2000 = value;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000040BC File Offset: 0x000022BC
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000040C4 File Offset: 0x000022C4
		public int IsAutoRun
		{
			get
			{
				return this.\u2001;
			}
			set
			{
				this.\u2001 = value;
				this.\u00A0();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000040D3 File Offset: 0x000022D3
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000040DC File Offset: 0x000022DC
		public int IsAutoConvert
		{
			get
			{
				return this.\u2002;
			}
			set
			{
				if (this.\u2002 != value)
				{
					this.\u2002 = value;
					if (!global::\u00A0.\u2005.\u00A0)
					{
						Application.Current.Dispatcher.Invoke(new Action(global::\u00A0.\u2005.\u00A0.\u00A0.\u1680));
					}
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000412E File Offset: 0x0000232E
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004138 File Offset: 0x00002338
		public int IsStandardVideo
		{
			get
			{
				return this.\u2003;
			}
			set
			{
				if (this.\u2003 != value)
				{
					this.\u2003 = value;
					if (!global::\u00A0.\u2005.\u00A0)
					{
						Application.Current.Dispatcher.Invoke(new Action(global::\u00A0.\u2005.\u00A0.\u00A0.\u2000));
					}
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000418A File Offset: 0x0000238A
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004192 File Offset: 0x00002392
		public string CurrOverlayKey
		{
			get
			{
				return this.\u2000;
			}
			set
			{
				if (this.\u2000 != value)
				{
					this.\u2000 = value;
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000041A9 File Offset: 0x000023A9
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000041B1 File Offset: 0x000023B1
		public string Curr3DModeKey
		{
			get
			{
				return this.\u2001;
			}
			set
			{
				if (this.\u2001 != value)
				{
					this.\u2001 = value;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000041C8 File Offset: 0x000023C8
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000041D0 File Offset: 0x000023D0
		public string Curr3DDepthNegKey
		{
			get
			{
				return this.\u2002;
			}
			set
			{
				if (this.\u2002 != value)
				{
					this.\u2002 = value;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000041E7 File Offset: 0x000023E7
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000041EF File Offset: 0x000023EF
		public string Curr3DDepthPosKey
		{
			get
			{
				return this.\u2003;
			}
			set
			{
				if (this.\u2003 != value)
				{
					this.\u2003 = value;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004206 File Offset: 0x00002406
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000420E File Offset: 0x0000240E
		public string Curr3DPopoutNegKey
		{
			get
			{
				return this.\u2004;
			}
			set
			{
				if (this.\u2004 != value)
				{
					this.\u2004 = value;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004225 File Offset: 0x00002425
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000422D File Offset: 0x0000242D
		public string Curr3DPopoutPosKey
		{
			get
			{
				return this.\u2005;
			}
			set
			{
				if (this.\u2005 != value)
				{
					this.\u2005 = value;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000424C File Offset: 0x0000244C
		public string Language
		{
			get
			{
				return this.\u2006;
			}
			set
			{
				\u2005.\u1680 u = new \u2005.\u1680();
				u.\u00A0 = this;
				u.\u00A0 = value;
				if (this.\u2006 != u.\u00A0)
				{
					this.\u2006 = u.\u00A0;
					Application.Current.Dispatcher.Invoke(new Action(u.\u00A0));
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000042A8 File Offset: 0x000024A8
		private void \u00A0(string A_1)
		{
			try
			{
				\u2014.\u00A0(A_1);
			}
			catch (Exception ex)
			{
				\u2025.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2033(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2036() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u203E());
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000042F0 File Offset: 0x000024F0
		private void \u00A0()
		{
			if (Environment.GetCommandLineArgs().Length != 0 && Enumerable.Contains<string>(Environment.GetCommandLineArgs(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2047()))
			{
				this.\u2000();
				return;
			}
			this.\u1680();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004318 File Offset: 0x00002518
		private void \u1680()
		{
			\u2025.\u00A0(global::\u00A0.\u2005.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2048());
			string text = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2049();
			string text2 = (this.IsAutoRun == 1) ? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u204B() : 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u204A();
			using (Process process = Process.Start(new ProcessStartInfo
			{
				FileName = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u204C(),
				Arguments = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u204D() + text + 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u204E() + text2,
				WindowStyle = 1,
				UseShellExecute = false,
				CreateNoWindow = true
			}))
			{
				if (process != null)
				{
					process.WaitForExit();
				}
				if (process == null || process.ExitCode != 0)
				{
					\u2025.\u00A0(global::\u00A0.\u2005.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u204F(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2048());
				}
				else
				{
					\u2025.\u00A0(global::\u00A0.\u2005.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2050(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2048());
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000043F4 File Offset: 0x000025F4
		private void \u2000()
		{
			\u2025.\u00A0(global::\u00A0.\u2005.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2051());
			\u2051 u = (\u2051)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2052())));
			string text = Path.Combine(new string[]
			{
				\u2007.\u00A0(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2053(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2054(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2055(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2056(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2057(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2058(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2059(),
				0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205A()
			});
			if (this.IsAutoRun == 1)
			{
				if (global::\u00A0.\u2005.\u2000.\u00A0 == null)
				{
					global::\u00A0.\u2005.\u2000.\u00A0 = CallSite<Func<CallSite, object, \u204D>>.Create(Binder.Convert(16, typeof(\u204D), typeof(\u2005)));
				}
				\u204D u204D = global::\u00A0.\u2005.\u2000.\u00A0.Target.Invoke(global::\u00A0.\u2005.\u2000.\u00A0, u.CreateShortcut(text));
				u204D.WindowStyle = 7;
				u204D.Arguments = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205B();
				u204D.TargetPath = Path.Combine(Environment.CurrentDirectory, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205C());
				u204D.WorkingDirectory = Environment.CurrentDirectory;
				u204D.Save();
				return;
			}
			File.Delete(text);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004510 File Offset: 0x00002710
		public static void \u2001()
		{
			Directory.CreateDirectory(global::\u00A0.\u2006.\u1680);
			if (global::\u00A0.\u2003.Instance.SettingsVM == null)
			{
				\u2025.\u1680(global::\u00A0.\u2005.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205D(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205E());
				return;
			}
			\u2029.\u00A0(global::\u00A0.\u2003.Instance.SettingsVM, global::\u00A0.\u2005.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205F());
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004564 File Offset: 0x00002764
		public static void \u2002()
		{
			try
			{
				if (File.Exists(global::\u00A0.\u2005.\u1680))
				{
					global::\u00A0.\u2005.\u00A0 = true;
					\u2005 u = \u2029.\u00A0<\u2005>(global::\u00A0.\u2005.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205F());
					global::\u00A0.\u2005.\u00A0 = false;
					if (u != null)
					{
						global::\u00A0.\u2003.Instance.SettingsVM = u;
						global::\u00A0.\u1680.\u00A0(global::\u00A0.\u1680.\u00A0.\u00A0);
						global::\u00A0.\u1680.\u00A0(global::\u00A0.\u1680.\u00A0.\u1680);
						global::\u00A0.\u1680.\u00A0(global::\u00A0.\u1680.\u00A0.\u2000);
					}
				}
				else
				{
					global::\u00A0.\u2005.\u2001();
				}
			}
			catch (Exception ex)
			{
				global::\u00A0.\u2005.\u00A0 = false;
				\u2025.\u1680(global::\u00A0.\u2005.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202E() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2060());
			}
		}

		// Token: 0x0400005B RID: 91
		private static readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2033();

		// Token: 0x0400005C RID: 92
		private static readonly string \u1680 = Path.Combine(global::\u00A0.\u2006.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206D());

		// Token: 0x0400005D RID: 93
		private static bool \u00A0 = false;

		// Token: 0x0400005E RID: 94
		private int \u00A0;

		// Token: 0x0400005F RID: 95
		private int \u1680 = 1;

		// Token: 0x04000060 RID: 96
		private int \u2000 = 1;

		// Token: 0x04000061 RID: 97
		private int \u2001 = 1;

		// Token: 0x04000062 RID: 98
		private int \u2002;

		// Token: 0x04000063 RID: 99
		private int \u2003 = 1;

		// Token: 0x04000064 RID: 100
		private string \u2000 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2061();

		// Token: 0x04000065 RID: 101
		private string \u2001 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2062();

		// Token: 0x04000066 RID: 102
		private string \u2002 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2063();

		// Token: 0x04000067 RID: 103
		private string \u2003 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2064();

		// Token: 0x04000068 RID: 104
		private string \u2004 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206A();

		// Token: 0x04000069 RID: 105
		private string \u2005 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206B();

		// Token: 0x0400006A RID: 106
		private string \u2006 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206C();

		// Token: 0x02000019 RID: 25
		[CompilerGenerated]
		[Serializable]
		private sealed class \u00A0
		{
			// Token: 0x060000BB RID: 187 RVA: 0x000046AA File Offset: 0x000028AA
			internal void \u00A0()
			{
				global::\u00A0.\u1680.\u00A0(global::\u00A0.\u1680.\u00A0.\u00A0);
			}

			// Token: 0x060000BC RID: 188 RVA: 0x000046B2 File Offset: 0x000028B2
			internal void \u1680()
			{
				global::\u00A0.\u1680.\u00A0(global::\u00A0.\u1680.\u00A0.\u1680);
			}

			// Token: 0x060000BD RID: 189 RVA: 0x000046BA File Offset: 0x000028BA
			internal void \u2000()
			{
				global::\u00A0.\u1680.\u00A0(global::\u00A0.\u1680.\u00A0.\u2000);
			}

			// Token: 0x0400006B RID: 107
			[Nullable(0)]
			public static readonly \u2005.\u00A0 \u00A0 = new \u2005.\u00A0();

			// Token: 0x0400006C RID: 108
			[Nullable(0)]
			public static Action \u00A0;

			// Token: 0x0400006D RID: 109
			[Nullable(0)]
			public static Action \u1680;

			// Token: 0x0400006E RID: 110
			[Nullable(0)]
			public static Action \u2000;
		}

		// Token: 0x0200001A RID: 26
		[CompilerGenerated]
		private sealed class \u1680
		{
			// Token: 0x060000BF RID: 191 RVA: 0x000046C2 File Offset: 0x000028C2
			internal void \u00A0()
			{
				this.\u00A0.\u00A0(this.\u00A0);
			}

			// Token: 0x0400006F RID: 111
			[Nullable(0)]
			public \u2005 \u00A0;

			// Token: 0x04000070 RID: 112
			[Nullable(0)]
			public string \u00A0;
		}

		// Token: 0x0200001B RID: 27
		[CompilerGenerated]
		private static class \u2000
		{
			// Token: 0x04000071 RID: 113
			[Nullable(0)]
			public static CallSite<Func<CallSite, object, \u204D>> \u00A0;
		}
	}
}
