using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2007
	{
		// Token: 0x060000F0 RID: 240
		[DllImport("kernel32.dll", EntryPoint = "SetEnvironmentVariable", SetLastError = true)]
		private static extern bool \u00A0(string, string);

		// Token: 0x060000F1 RID: 241
		[DllImport("kernel32.dll", CharSet = 4, EntryPoint = "GetEnvironmentVariable", SetLastError = true)]
		private static extern uint \u00A0(string, StringBuilder, uint);

		// Token: 0x060000F2 RID: 242 RVA: 0x00004EFD File Offset: 0x000030FD
		public static string \u00A0()
		{
			return Environment.GetEnvironmentVariable(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2035()) ?? Environment.GetFolderPath(40);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004F14 File Offset: 0x00003114
		public static bool \u00A0(string A_0)
		{
			bool result = false;
			using (Ping ping = new Ping())
			{
				IPStatus ipstatus = -1;
				try
				{
					ipstatus = ping.Send(A_0, 1000).Status;
				}
				catch (Exception ex)
				{
					ipstatus = -1;
					\u2025.\u1680(\u2007.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2033());
				}
				if (ipstatus == null)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004F8C File Offset: 0x0000318C
		public static void \u1680()
		{
			\u2025.\u00A0(\u2007.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2036());
			\u202F u202F = \u2035.Instance.\u200A();
			if (u202F == \u202F.\u00A0 || u202F == \u202F.\u2008)
			{
				\u202E.\u00A0(\u202E.\u00A0.\u2000, \u202E.\u1680.\u2004, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032());
				return;
			}
			\u2035.Instance.\u00A0(u202F, false);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004FD4 File Offset: 0x000031D4
		public static void \u2000()
		{
			\u2025.\u00A0(\u2007.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u203E());
			Application.Current.Dispatcher.Invoke(new Action(\u2007.\u00A0.\u00A0.\u00A0));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005014 File Offset: 0x00003214
		public static void \u1680(string A_0)
		{
			try
			{
				foreach (Process process in Process.GetProcessesByName(A_0))
				{
					\u2025.\u00A0(\u2007.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2047() + process.ProcessName, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2048());
					process.Kill();
					process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2007.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2049() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2048());
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000509C File Offset: 0x0000329C
		public static void \u2000(string A_0 = "")
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205C(),
				Arguments = A_0
			});
		}

		// Token: 0x04000093 RID: 147
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u204A();

		// Token: 0x02000024 RID: 36
		[CompilerGenerated]
		[Serializable]
		private sealed class \u00A0
		{
			// Token: 0x060000FC RID: 252 RVA: 0x000050D3 File Offset: 0x000032D3
			internal void \u00A0()
			{
				\u2007.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u200A());
				\u2007.\u1680(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204C());
			}

			// Token: 0x04000094 RID: 148
			[Nullable(0)]
			public static readonly \u2007.\u00A0 \u00A0 = new \u2007.\u00A0();

			// Token: 0x04000095 RID: 149
			[Nullable(0)]
			public static Action \u00A0;
		}
	}
}
