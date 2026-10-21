// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\‥.cs
using System;
using System.IO;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace obf_1
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public class obf_11
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00005FA0 File Offset: 0x000041A0
		public static void obf_1(string A_0)
		{
			if (!Directory.Exists(obf_12.obf_1))
			{
				Directory.CreateDirectory(obf_12.obf_1);
			}
			obf_11.obf_1 = Path.Combine(obf_12.obf_1, A_0);
			if (File.Exists(obf_11.obf_1))
			{
				File.Delete(obf_11.obf_1);
			}
			obf_11.obf_1("LogHelper", "Logger created successfully. File path: " + obf_11.obf_1, "CreateLogger");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006008 File Offset: 0x00004208
		public static void obf_1(string A_0, string A_1, [CallerMemberName] string callerFunctionName = "")
		{
			if (A_0 != null && A_1 != null)
			{
				obf_11.obf_4(A_0, A_1, callerFunctionName);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006018 File Offset: 0x00004218
		public static void obf_3(string A_0, string A_1, [CallerMemberName] string callerFunctionName = "")
		{
			obf_11.obf_4(A_0, "ERROR : " + A_1, callerFunctionName);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000602C File Offset: 0x0000422C
		public static void obf_1(string A_0, [CallerMemberName] string callerFunctionName = "")
		{
			obf_11.obf_4(A_0, "Method Entry", callerFunctionName);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000603A File Offset: 0x0000423A
		public static void obf_3(string A_0, [CallerMemberName] string callerFunctionName = "")
		{
			obf_11.obf_4(A_0, "Method Exit", callerFunctionName);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006048 File Offset: 0x00004248
		public static void obf_4(string A_0, string A_1, string A_2)
		{
			string text = DateTime.Now.ToString("HH:mm:ss");
			object u00A = obf_11.obf_1;
			lock (u00A)
			{
				try
				{
					string u00A2 = obf_11.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 4);
					defaultInterpolatedStringHandler.AppendLiteral("[");
					defaultInterpolatedStringHandler.AppendFormatted(text);
					defaultInterpolatedStringHandler.AppendLiteral("] ");
					defaultInterpolatedStringHandler.AppendFormatted(A_0);
					defaultInterpolatedStringHandler.AppendLiteral(" - ");
					defaultInterpolatedStringHandler.AppendFormatted(A_2);
					defaultInterpolatedStringHandler.AppendLiteral(" - ");
					defaultInterpolatedStringHandler.AppendFormatted(A_1);
					defaultInterpolatedStringHandler.AppendLiteral("\n");
					File.AppendAllText(u00A2, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400011D RID: 285
		private static readonly object obf_1 = new object();

		// Token: 0x0400011E RID: 286
		private static string obf_1 = "";
	}
}
