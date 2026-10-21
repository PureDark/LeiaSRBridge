using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using GameLauncher.Lib;
using GameLauncher.Lib.Structs;
using \u00A0;

namespace \u1680
{
	// Token: 0x02000080 RID: 128
	[NullableContext(1)]
	[Nullable(0)]
	public class \u204A : \u2049, IDisposable
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public Task \u00A0(string A_1, string A_2, \u2036 A_3, string A_4)
		{
			\u204A.\u1680 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			u.\u1680 = A_2;
			u.\u00A0 = A_3;
			u.\u2000 = A_4;
			u.\u00A0 = -1;
			u.\u00A0.Start<\u204A.\u1680>(ref u);
			return u.\u00A0.Task;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009B3C File Offset: 0x00007D3C
		public Task \u1680(string A_1, string A_2, \u2036 A_3, string A_4)
		{
			\u204A.\u2000 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			u.\u1680 = A_2;
			u.\u00A0 = A_3;
			u.\u2000 = A_4;
			u.\u00A0 = -1;
			u.\u00A0.Start<\u204A.\u2000>(ref u);
			return u.\u00A0.Task;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009BA0 File Offset: 0x00007DA0
		public void \u00A0()
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2059());
			CancellationTokenSource u00A = this.\u00A0;
			if (u00A != null)
			{
				u00A.Cancel();
			}
			CancellationTokenSource u00A2 = this.\u00A0;
			if (u00A2 == null)
			{
				return;
			}
			u00A2.Dispose();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009BD4 File Offset: 0x00007DD4
		public void \u00A0(DeviceEvent A_1)
		{
			foreach (IGame game in this.\u00A0)
			{
				\u204A.\u00A0(game, A_1);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009C28 File Offset: 0x00007E28
		public List<IGame> \u1680()
		{
			return this.\u00A0;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009C30 File Offset: 0x00007E30
		private static void \u00A0(IGame A_0, DeviceEvent A_1)
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206D());
			if (A_0 != null)
			{
				string u00A = \u204A.\u00A0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(34, 2);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206E());
				defaultInterpolatedStringHandler.AppendFormatted<int?>(A_0.ProcessId);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206F());
				defaultInterpolatedStringHandler.AppendFormatted(A_1.ToString());
				\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u206D());
				A_0.HandleDeviceEvent(A_1);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009CAF File Offset: 0x00007EAF
		private void \u00A0(IntPtr A_1, IGame A_2)
		{
			while (\u202D.\u2004(A_1))
			{
				Thread.Sleep(1000);
			}
			if (\u2035.Instance.\u200A() == \u202F.\u00A0)
			{
				this.\u00A0.Remove(A_2);
				\u204A.Instance.\u1680(true);
				\u2007.\u1680();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009CEE File Offset: 0x00007EEE
		public void Dispose()
		{
			this.\u00A0.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400028F RID: 655
		private static readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u3000();

		// Token: 0x04000290 RID: 656
		private readonly List<IGame> \u00A0 = new List<IGame>();

		// Token: 0x04000291 RID: 657
		private readonly GameLauncher \u00A0 = new GameLauncher();

		// Token: 0x04000292 RID: 658
		[Nullable(2)]
		private CancellationTokenSource \u00A0;

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		private sealed class \u00A0
		{
			// Token: 0x06000230 RID: 560 RVA: 0x00009D0D File Offset: 0x00007F0D
			internal void \u00A0()
			{
				this.\u00A0.\u00A0(this.\u00A0, this.\u00A0);
			}

			// Token: 0x04000293 RID: 659
			[Nullable(0)]
			public \u204A \u00A0;

			// Token: 0x04000294 RID: 660
			[Nullable(0)]
			public IGame \u00A0;

			// Token: 0x04000295 RID: 661
			public IntPtr \u00A0;
		}
	}
}
