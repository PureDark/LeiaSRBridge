using System;
using System.IO;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2025
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00005FA0 File Offset: 0x000041A0
		public static void \u00A0(string A_0)
		{
			if (!Directory.Exists(\u2006.\u00A0))
			{
				Directory.CreateDirectory(\u2006.\u00A0);
			}
			\u2025.\u00A0 = Path.Combine(\u2006.\u00A0, A_0);
			if (File.Exists(\u2025.\u00A0))
			{
				File.Delete(\u2025.\u00A0);
			}
			\u2025.\u00A0(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2025(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2027() + \u2025.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2028());
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006008 File Offset: 0x00004208
		public static void \u00A0(string A_0, string A_1, [CallerMemberName] string callerFunctionName = "")
		{
			if (A_0 != null && A_1 != null)
			{
				\u2025.\u2000(A_0, A_1, callerFunctionName);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006018 File Offset: 0x00004218
		public static void \u1680(string A_0, string A_1, [CallerMemberName] string callerFunctionName = "")
		{
			\u2025.\u2000(A_0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2029() + A_1, callerFunctionName);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000602C File Offset: 0x0000422C
		public static void \u00A0(string A_0, [CallerMemberName] string callerFunctionName = "")
		{
			\u2025.\u2000(A_0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202A(), callerFunctionName);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000603A File Offset: 0x0000423A
		public static void \u1680(string A_0, [CallerMemberName] string callerFunctionName = "")
		{
			\u2025.\u2000(A_0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202B(), callerFunctionName);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006048 File Offset: 0x00004248
		public static void \u2000(string A_0, string A_1, string A_2)
		{
			string text = DateTime.Now.ToString(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202C());
			object u00A = \u2025.\u00A0;
			lock (u00A)
			{
				try
				{
					string u00A2 = \u2025.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 4);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202D());
					defaultInterpolatedStringHandler.AppendFormatted(text);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202E());
					defaultInterpolatedStringHandler.AppendFormatted(A_0);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202F());
					defaultInterpolatedStringHandler.AppendFormatted(A_2);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202F());
					defaultInterpolatedStringHandler.AppendFormatted(A_1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2032());
					File.AppendAllText(u00A2, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400011D RID: 285
		private static readonly object \u00A0 = new object();

		// Token: 0x0400011E RID: 286
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();
	}
}
