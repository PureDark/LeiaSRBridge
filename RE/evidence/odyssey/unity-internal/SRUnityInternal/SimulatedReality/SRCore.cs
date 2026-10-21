using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x02000004 RID: 4
	public static class SRCore
	{
		// Token: 0x06000001 RID: 1
		[DllImport("SimulatedRealityCore")]
		public static extern IntPtr newSRContext();

		// Token: 0x06000002 RID: 2
		[DllImport("SimulatedRealityCore")]
		public static extern void initializeSRContext(IntPtr context);

		// Token: 0x06000003 RID: 3
		[DllImport("SimulatedRealityCore")]
		public static extern void deleteSRContext(IntPtr context);

		// Token: 0x06000004 RID: 4
		[DllImport("SimulatedRealityCore")]
		public static extern IntPtr createSystemSense(IntPtr context);

		// Token: 0x06000005 RID: 5
		[DllImport("SimulatedRealityCore")]
		public static extern IntPtr createSystemEventListener(IntPtr SystemSense, SRCore.acceptSystemSenseCallback callback);

		// Token: 0x06000006 RID: 6
		[DllImport("SimulatedRealityCore")]
		public static extern IntPtr deleteSystemEventListener(IntPtr SystemEventListener);

		// Token: 0x04000014 RID: 20
		private const string dllPostFix = "";

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x06000032 RID: 50
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void acceptSystemSenseCallback(SR_systemEvent eyePair);
	}
}
