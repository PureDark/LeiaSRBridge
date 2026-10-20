using System;
using System.Threading.Tasks;

namespace AstraDepth3D.EventBus
{
	// Token: 0x0200002B RID: 43
	public class LocalEventPublisher
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001B8 RID: 440 RVA: 0x0000D340 File Offset: 0x0000B540
		// (remove) Token: 0x060001B9 RID: 441 RVA: 0x0000D378 File Offset: 0x0000B578
		public event EventHandler<LocalEventArgs> MsgEventHandler;

		// Token: 0x060001BA RID: 442 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		protected virtual void OnMyEventAsync(LocalEventArgs e)
		{
			EventHandler<LocalEventArgs> handler = this.MsgEventHandler;
			if (handler != null)
			{
				Task.Run(delegate()
				{
					handler(this, e);
				});
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D3F7 File Offset: 0x0000B5F7
		protected virtual void OnMyEventSync(LocalEventArgs e)
		{
			EventHandler<LocalEventArgs> msgEventHandler = this.MsgEventHandler;
			if (msgEventHandler == null)
			{
				return;
			}
			msgEventHandler(this, e);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D40B File Offset: 0x0000B60B
		public void DoSomethingAsync(string key, int value)
		{
			this.OnMyEventAsync(new LocalEventArgs(key, value));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D41A File Offset: 0x0000B61A
		public void DoSomethingSync(string key, int value)
		{
			this.OnMyEventSync(new LocalEventArgs(key, value));
		}
	}
}
