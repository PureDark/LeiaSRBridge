using System;

namespace LeiaUnity
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public struct LeiaCameraData
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00025361 File Offset: 0x00023561
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00025369 File Offset: 0x00023569
		public float baseline { readonly get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00025372 File Offset: 0x00023572
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0002537A File Offset: 0x0002357A
		public float screenHalfHeight { readonly get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00025383 File Offset: 0x00023583
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0002538B File Offset: 0x0002358B
		public float screenHalfWidth { readonly get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00025394 File Offset: 0x00023594
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0002539C File Offset: 0x0002359C
		public float baselineScaling { readonly get; set; }
	}
}
