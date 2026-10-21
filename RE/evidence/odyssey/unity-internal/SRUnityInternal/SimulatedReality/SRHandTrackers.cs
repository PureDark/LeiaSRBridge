using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x0200000E RID: 14
	public static class SRHandTrackers
	{
		// Token: 0x06000019 RID: 25
		[DllImport("SimulatedRealityHandTrackers")]
		public static extern IntPtr createHandTracker(IntPtr context);

		// Token: 0x0600001A RID: 26
		[DllImport("SimulatedRealityHandTrackers")]
		public static extern IntPtr createHandPoseListener(IntPtr handTracker, SRHandTrackers.acceptHandPoseCallback callback);

		// Token: 0x0600001B RID: 27
		[DllImport("SimulatedRealityHandTrackers")]
		public static extern void deleteHandPoseListener(IntPtr handPoseListener);

		// Token: 0x0600001C RID: 28
		[DllImport("SimulatedRealityHandTrackers")]
		public static extern IntPtr createHandEventListener(IntPtr handTracker, SRHandTrackers.acceptHandEventCallback callback);

		// Token: 0x0600001D RID: 29
		[DllImport("SimulatedRealityHandTrackers")]
		public static extern void deleteHandEventListener(IntPtr handEventListener);

		// Token: 0x04000032 RID: 50
		private const string dllPostFix = "";

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x0600003E RID: 62
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void acceptHandPoseCallback(SR_handPose handPose);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x06000042 RID: 66
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void acceptHandEventCallback(SR_handEvent handEvent);
	}
}
