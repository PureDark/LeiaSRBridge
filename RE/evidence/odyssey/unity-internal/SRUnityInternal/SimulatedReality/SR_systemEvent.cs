using System;

namespace SimulatedReality
{
	// Token: 0x02000003 RID: 3
	public struct SR_systemEvent
	{
		// Token: 0x04000010 RID: 16
		public ulong time;

		// Token: 0x04000011 RID: 17
		public SR_eventType eventType;

		// Token: 0x04000012 RID: 18
		public ulong messageLength;

		// Token: 0x04000013 RID: 19
		public IntPtr message;
	}
}
