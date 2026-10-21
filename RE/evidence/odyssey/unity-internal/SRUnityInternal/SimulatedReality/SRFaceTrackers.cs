using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x0200000A RID: 10
	public static class SRFaceTrackers
	{
		// Token: 0x06000013 RID: 19
		[DllImport("SimulatedRealityFaceTrackers")]
		public static extern IntPtr createHeadTracker(IntPtr context);

		// Token: 0x06000014 RID: 20
		[DllImport("SimulatedRealityFaceTrackers")]
		public static extern IntPtr createHeadListener(IntPtr eyeTracker, SRFaceTrackers.acceptHeadCallback callback);

		// Token: 0x06000015 RID: 21
		[DllImport("SimulatedRealityFaceTrackers")]
		public static extern IntPtr deleteHeadListener(IntPtr headListener);

		// Token: 0x04000025 RID: 37
		private const string dllPostFix = "";

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x06000036 RID: 54
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void acceptHeadCallback(SR_head eyePair);
	}
}
