using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace \u00A0
{
	// Token: 0x0200007E RID: 126
	public class \u203E : ILoggerProvider, IDisposable
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00009A14 File Offset: 0x00007C14
		[NullableContext(1)]
		public \u203E(string A_1)
		{
			this.\u00A0 = A_1;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009A23 File Offset: 0x00007C23
		[NullableContext(1)]
		public ILogger CreateLogger(string A_1)
		{
			return new \u2047(this.\u00A0);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000528A File Offset: 0x0000348A
		public void Dispose()
		{
		}

		// Token: 0x0400028D RID: 653
		[Nullable(1)]
		private readonly string \u00A0;
	}
}
