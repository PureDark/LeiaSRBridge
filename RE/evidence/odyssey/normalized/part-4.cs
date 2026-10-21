// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\‬.cs
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace obf_12
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(0)]
	public class obf_25
	{
		// Token: 0x0600018B RID: 395
		[DllImport("txBridge.dll", EntryPoint = "txBridge_connect")]
		public static extern int obf_12();

		// Token: 0x0600018C RID: 396
		[DllImport("txBridge.dll", EntryPoint = "txBridge_disconnect")]
		public static extern int obf_14();

		// Token: 0x0600018D RID: 397
		[DllImport("txBridge.dll", EntryPoint = "txBridge_destroyOldProcess_ifNeeded")]
		public static extern int obf_18();

		// Token: 0x0600018E RID: 398
		[DllImport("txBridge.dll", EntryPoint = "txBridge_spawnProxy")]
		public static extern int obf_12(string);

		// Token: 0x0600018F RID: 399
		[DllImport("txBridge.dll", EntryPoint = "txBridge_spawnProxyConsoleWindow")]
		public static extern int obf_14(string);

		// Token: 0x06000190 RID: 400
		[DllImport("txBridge.dll", EntryPoint = "txBridge_killProxy")]
		public static extern int obf_22();

		// Token: 0x06000191 RID: 401
		[DllImport("txBridge.dll", EntryPoint = "txBridge_sendDeviceQuery")]
		public static extern int obf_24();

		// Token: 0x06000192 RID: 402
		[DllImport("txBridge.dll", EntryPoint = "txBridge_sendDevicProxyQuery")]
		public static extern int obf_20();

		// Token: 0x06000193 RID: 403
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getDeviceRespondingFlag")]
		public static extern int obf_21();

		// Token: 0x06000194 RID: 404
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getDeviceProxyRespondingFlag")]
		public static extern int obf_26();

		// Token: 0x06000195 RID: 405
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getVideoCablePluggedFlag")]
		public static extern int obf_19();

		// Token: 0x06000196 RID: 406
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getPivotOnFlag")]
		public static extern int obf_17();

		// Token: 0x06000197 RID: 407
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getPipOnFlag")]
		public static extern int obf_27();

		// Token: 0x06000198 RID: 408
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getPowerStateFlag")]
		public static extern int obf_28();

		// Token: 0x06000199 RID: 409
		[DllImport("txBridge.dll", EntryPoint = "txBridge_get3dFlag")]
		public static extern int obf_29();

		// Token: 0x0600019A RID: 410
		[DllImport("txBridge.dll", EntryPoint = "txBridge_set3dFlag")]
		public static extern int obf_12(int, int, int);

		// Token: 0x0600019B RID: 411
		[DllImport("txBridge.dll", EntryPoint = "txBridge_getFirmwareUpgradePendingState")]
		public static extern int obf_30();

		// Token: 0x0600019C RID: 412
		[DllImport("txBridge.dll", EntryPoint = "txBridge_sendText")]
		public static extern int obf_18(string);

		// Token: 0x0600019D RID: 413
		[DllImport("txBridge.dll", EntryPoint = "txBridge_waitDeviceProxyReadyNotification")]
		public static extern int obf_31();

		// Token: 0x0600019E RID: 414
		[DllImport("txBridge.dll", EntryPoint = "txBridge_waitDeviceReadyNotification")]
		public static extern int obf_32();

		// Token: 0x0600019F RID: 415
		[DllImport("txBridge.dll", EntryPoint = "txBridge_waitCableConnectionChangeNotification")]
		public static extern int obf_33();

		// Token: 0x060001A0 RID: 416
		[DllImport("txBridge.dll", EntryPoint = "txBridge_waitPowerStateChangeNotification")]
		public static extern int obf_34();

		// Token: 0x060001A1 RID: 417
		[DllImport("txBridge.dll", EntryPoint = "txBridge_wait3dModeChangeNotification")]
		public static extern int obf_35();
	}
}
