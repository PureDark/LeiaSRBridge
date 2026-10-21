using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x02000007 RID: 7
	public struct SR_earPair
	{
		// Token: 0x04000019 RID: 25
		public ulong frameId;

		// Token: 0x0400001A RID: 26
		public ulong time;

		// Token: 0x0400001B RID: 27
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public double[] ears;
	}
}
