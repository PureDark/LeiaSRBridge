// Original: RE\evidence\odyssey\managed\Odyssey3DHubLauncher\⁫‫‪⁭⁯‍⁯‌‮‍⁫‫‏⁪‍⁭⁯‌‮⁫‭⁫‌⁪‌⁫‫‌⁮‌‍‪‮⁯‏⁪⁮‪‍‬‮.cs
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000088 RID: 136
public class obf_1
{
	// Token: 0x060004FD RID: 1277
	[DllImport("txBridge.dll", EntryPoint = "txBridge_connect")]
	public static extern int obf_2();

	// Token: 0x060004FE RID: 1278
	[DllImport("txBridge.dll", EntryPoint = "txBridge_disconnect")]
	public static extern int obf_3();

	// Token: 0x060004FF RID: 1279
	[NullableContext(1)]
	[DllImport("txBridge.dll", EntryPoint = "txBridge_spawnProxy")]
	public static extern int obf_4(string);

	// Token: 0x06000500 RID: 1280
	[DllImport("txBridge.dll", EntryPoint = "txBridge_killProxy")]
	public static extern int obf_5();

	// Token: 0x06000501 RID: 1281
	[DllImport("txBridge.dll", EntryPoint = "txBridge_sendDeviceQuery")]
	public static extern int obf_6();

	// Token: 0x06000502 RID: 1282
	[DllImport("txBridge.dll", EntryPoint = "txBridge_sendDevicProxyQuery")]
	public static extern int obf_7();

	// Token: 0x06000503 RID: 1283
	[DllImport("txBridge.dll", EntryPoint = "txBridge_getDeviceRespondingFlag")]
	public static extern int obf_8();

	// Token: 0x06000504 RID: 1284
	[DllImport("txBridge.dll", EntryPoint = "txBridge_getDeviceProxyRespondingFlag")]
	public static extern int obf_9();

	// Token: 0x06000505 RID: 1285
	[DllImport("txBridge.dll", EntryPoint = "txBridge_getPowerStateFlag")]
	public static extern int obf_10();

	// Token: 0x06000506 RID: 1286
	[DllImport("txBridge.dll", EntryPoint = "txBridge_getVideoCablePluggedFlag")]
	public static extern int obf_11();
}
