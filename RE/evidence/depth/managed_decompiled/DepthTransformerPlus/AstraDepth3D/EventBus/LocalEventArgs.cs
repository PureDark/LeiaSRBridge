using System;

namespace AstraDepth3D.EventBus
{
	// Token: 0x0200002A RID: 42
	public class LocalEventArgs : EventArgs
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000D328 File Offset: 0x0000B528
		public LocalEventArgs(string key, int value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x040000DC RID: 220
		public string key;

		// Token: 0x040000DD RID: 221
		public int value;
	}
}
