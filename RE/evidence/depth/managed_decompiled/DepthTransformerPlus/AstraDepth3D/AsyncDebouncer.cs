using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AstraDepth3D
{
	// Token: 0x02000006 RID: 6
	internal class AsyncDebouncer : IDisposable
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000020FA File Offset: 0x000002FA
		public AsyncDebouncer(TimeSpan delay, Func<IntPtr, long, long, Task> callback)
		{
			this._delay = delay;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._callback = callback;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000212C File Offset: 0x0000032C
		public void RunAsync(IntPtr hwnd, long idObject, long idChild)
		{
			AsyncDebouncer.<RunAsync>d__4 <RunAsync>d__;
			<RunAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RunAsync>d__.<>4__this = this;
			<RunAsync>d__.hwnd = hwnd;
			<RunAsync>d__.idObject = idObject;
			<RunAsync>d__.idChild = idChild;
			<RunAsync>d__.<>1__state = -1;
			<RunAsync>d__.<>t__builder.Start<AsyncDebouncer.<RunAsync>d__4>(ref <RunAsync>d__);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000217B File Offset: 0x0000037B
		public void Dispose()
		{
			this._cts.Cancel();
			this._cts.Dispose();
		}

		// Token: 0x04000004 RID: 4
		private readonly TimeSpan _delay;

		// Token: 0x04000005 RID: 5
		private readonly Func<IntPtr, long, long, Task> _callback;

		// Token: 0x04000006 RID: 6
		private CancellationTokenSource _cts = new CancellationTokenSource();
	}
}
