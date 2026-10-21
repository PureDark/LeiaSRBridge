using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using \u00A0;

namespace \u1680
{
	// Token: 0x0200007C RID: 124
	public class \u2049 : \u2048
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000988C File Offset: 0x00007A8C
		public void \u00A0(IntPtr A_1, bool A_2)
		{
			\u2025.\u00A0(\u2049.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2056());
			if (A_1 != IntPtr.Zero)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(2, 3);
				defaultInterpolatedStringHandler.AppendFormatted(A_1.ToString());
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2053());
				defaultInterpolatedStringHandler.AppendFormatted(A_2.ToString().ToLower());
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2053());
				defaultInterpolatedStringHandler.AppendFormatted<int>(\u202B.\u2003());
				string arguments = defaultInterpolatedStringHandler.ToStringAndClear();
				using (Process process = Process.Start(new ProcessStartInfo
				{
					FileName = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2063(),
					Arguments = arguments
				}))
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					\u2009 u = A_2 ? \u2009.EXECUTION_3DCONV_SBS : \u2009.EXECUTION_3DCONV_2D;
					\u200B u200B = A_2 ? \u200B.execution_3dconv_sbs : \u200B.execution_3dconv_2d;
					\u2013.\u00A0(u, u200B, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u203E());
					string u00A = \u2049.\u00A0;
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(82, 3);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2064());
					defaultInterpolatedStringHandler.AppendFormatted<IntPtr>(A_1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206A());
					defaultInterpolatedStringHandler.AppendFormatted(A_2.ToString().ToLower());
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206B());
					defaultInterpolatedStringHandler.AppendFormatted<int>(\u202B.\u2003());
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2056());
					process.WaitForExit();
					stopwatch.Stop();
					long num = (long)stopwatch.Elapsed.TotalSeconds;
					\u2009 u2 = A_2 ? \u2009.TIME_3DCONV_SBS : \u2009.TIME_3DCONV_2D;
					\u200B u200B2 = A_2 ? \u200B.time_3dconv_sbs : \u200B.time_3dconv_2d;
					\u2013.\u00A0(u2, u200B2, num.ToString());
				}
			}
		}

		// Token: 0x04000289 RID: 649
		[Nullable(1)]
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206C();
	}
}
