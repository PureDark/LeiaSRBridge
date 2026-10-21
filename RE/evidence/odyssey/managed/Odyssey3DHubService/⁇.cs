using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Microsoft.Extensions.Logging;

namespace \u00A0
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2047 : ILogger
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00009A30 File Offset: 0x00007C30
		public \u2047(string A_1)
		{
			this.\u00A0 = A_1;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009A3F File Offset: 0x00007C3F
		[return: Nullable(2)]
		public IDisposable BeginScope<\u00A0>(\u00A0 A_1)
		{
			return null;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009A42 File Offset: 0x00007C42
		public bool IsEnabled(LogLevel A_1)
		{
			return true;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009A48 File Offset: 0x00007C48
		[NullableContext(2)]
		public void Log<\u00A0>(LogLevel A_1, EventId A_2, [Nullable(1)] \u00A0 state, Exception A_4, [Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<\u00A0, Exception, string> formatter)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(4, 3);
			defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2051());
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2052());
			defaultInterpolatedStringHandler.AppendFormatted<LogLevel>(A_1);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u202E());
			defaultInterpolatedStringHandler.AppendFormatted(formatter.Invoke(state, A_4));
			string text = defaultInterpolatedStringHandler.ToStringAndClear();
			\u2025.\u00A0(this.\u00A0, text, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2053());
		}

		// Token: 0x0400028E RID: 654
		private readonly string \u00A0;
	}
}
