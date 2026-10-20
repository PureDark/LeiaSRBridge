using System;
using System.Numerics;
using Windows.Foundation;

namespace AstraDepth3D
{
	// Token: 0x02000015 RID: 21
	internal class MonitorInfo
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000AABC File Offset: 0x00008CBC
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public bool IsPrimary { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000AACD File Offset: 0x00008CCD
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000AAD5 File Offset: 0x00008CD5
		public Vector2 ScreenSize { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000AADE File Offset: 0x00008CDE
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		public Rect MonitorArea { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000AAEF File Offset: 0x00008CEF
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000AAF7 File Offset: 0x00008CF7
		public Rect WorkArea { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000AB00 File Offset: 0x00008D00
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000AB08 File Offset: 0x00008D08
		public string DeviceName { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000AB11 File Offset: 0x00008D11
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000AB19 File Offset: 0x00008D19
		public IntPtr Hmon { get; set; }
	}
}
