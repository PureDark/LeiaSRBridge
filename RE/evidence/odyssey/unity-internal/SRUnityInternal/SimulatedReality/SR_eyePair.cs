using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x02000006 RID: 6
	public struct SR_eyePair
	{
		// Token: 0x04000016 RID: 22
		public ulong frameId;

		// Token: 0x04000017 RID: 23
		public ulong time;

		// Token: 0x04000018 RID: 24
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public double[] eyes;
	}
}
