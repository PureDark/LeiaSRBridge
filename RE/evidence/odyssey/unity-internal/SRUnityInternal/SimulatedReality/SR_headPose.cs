using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x02000008 RID: 8
	public struct SR_headPose
	{
		// Token: 0x0400001C RID: 28
		public ulong frameId;

		// Token: 0x0400001D RID: 29
		public ulong time;

		// Token: 0x0400001E RID: 30
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public double[] position;

		// Token: 0x0400001F RID: 31
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public double[] orientation;
	}
}
