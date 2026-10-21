using System;
using System.Runtime.InteropServices;

namespace SimulatedReality
{
	// Token: 0x02000005 RID: 5
	public static class SRDisplays
	{
		// Token: 0x06000007 RID: 7
		[DllImport("SimulatedRealityDisplays")]
		public static extern IntPtr createScreen(IntPtr context);

		// Token: 0x06000008 RID: 8
		[DllImport("SimulatedRealityDisplays")]
		public static extern int getResolutionHeight(IntPtr screen);

		// Token: 0x06000009 RID: 9
		[DllImport("SimulatedRealityDisplays")]
		public static extern int getResolutionWidth(IntPtr screen);

		// Token: 0x0600000A RID: 10
		[DllImport("SimulatedRealityDisplays")]
		public static extern int getPhysicalResolutionHeight(IntPtr screen);

		// Token: 0x0600000B RID: 11
		[DllImport("SimulatedRealityDisplays")]
		public static extern int getPhysicalResolutionWidth(IntPtr screen);

		// Token: 0x0600000C RID: 12
		[DllImport("SimulatedRealityDisplays")]
		public static extern float getPhysicalSizeHeight(IntPtr screen);

		// Token: 0x0600000D RID: 13
		[DllImport("SimulatedRealityDisplays")]
		public static extern float getPhysicalSizeWidth(IntPtr screen);

		// Token: 0x0600000E RID: 14
		[DllImport("SimulatedRealityDisplays")]
		public static extern float getDotPitch(IntPtr screen);

		// Token: 0x0600000F RID: 15
		[DllImport("SimulatedRealityDisplays")]
		public static extern IntPtr createSwitchableLensHint(IntPtr context);

		// Token: 0x06000010 RID: 16
		[DllImport("SimulatedRealityDisplays")]
		public static extern void lensEnableHint(IntPtr lensHint);

		// Token: 0x06000011 RID: 17
		[DllImport("SimulatedRealityDisplays")]
		public static extern void lensDisableHint(IntPtr lensHint);

		// Token: 0x06000012 RID: 18
		[DllImport("SimulatedRealityDisplays")]
		public static extern void deleteSwitchableLensHint(IntPtr lensHint);

		// Token: 0x04000015 RID: 21
		private const string dllPostFix = "";
	}
}
