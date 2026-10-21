using System;

namespace SimulatedReality
{
	// Token: 0x02000009 RID: 9
	public struct SR_head
	{
		// Token: 0x04000020 RID: 32
		public ulong frameId;

		// Token: 0x04000021 RID: 33
		public ulong time;

		// Token: 0x04000022 RID: 34
		public SR_headPose headPose;

		// Token: 0x04000023 RID: 35
		public SR_eyePair eyes;

		// Token: 0x04000024 RID: 36
		public SR_earPair ears;
	}
}
