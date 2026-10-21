using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x0200000B RID: 11
	public static class SREyeTrackers
	{
		// Token: 0x06000016 RID: 22
		[DllImport("SimulatedRealityFaceTrackers")]
		public static extern IntPtr createEyeTracker(IntPtr context);

		// Token: 0x06000017 RID: 23
		[DllImport("SimulatedRealityFaceTrackers")]
		public static extern IntPtr createEyePairListener(IntPtr eyeTracker, SREyeTrackers.AcceptEyePairCallback callback);

		// Token: 0x06000018 RID: 24
		[DllImport("SimulatedRealityFaceTrackers")]
		public static extern void deleteEyePairListener(IntPtr eyePairListener);

		// Token: 0x04000026 RID: 38
		private const string dllPostFix = "";

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x0600003A RID: 58
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void AcceptEyePairCallback(SR_eyePair eyePair);
	}
}
