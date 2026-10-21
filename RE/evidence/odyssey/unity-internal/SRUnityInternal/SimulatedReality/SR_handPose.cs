using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x0200000C RID: 12
	public struct SR_handPose
	{
		// Token: 0x04000027 RID: 39
		public ulong frameId;

		// Token: 0x04000028 RID: 40
		public ulong time;

		// Token: 0x04000029 RID: 41
		public ulong handId;

		// Token: 0x0400002A RID: 42
		public ulong side;

		// Token: 0x0400002B RID: 43
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 63)]
		public double[] joints;
	}
}
