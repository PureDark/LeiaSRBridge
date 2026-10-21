using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public struct LeiaBoundsData
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000253A5 File Offset: 0x000235A5
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x000253AD File Offset: 0x000235AD
		public Vector3[] screen { readonly get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000253B6 File Offset: 0x000235B6
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000253BE File Offset: 0x000235BE
		public Vector3[] north { readonly get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000253C7 File Offset: 0x000235C7
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000253CF File Offset: 0x000235CF
		public Vector3[] south { readonly get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000253D8 File Offset: 0x000235D8
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000253E0 File Offset: 0x000235E0
		public Vector3[] top { readonly get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000253E9 File Offset: 0x000235E9
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x000253F1 File Offset: 0x000235F1
		public Vector3[] bottom { readonly get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x000253FA File Offset: 0x000235FA
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00025402 File Offset: 0x00023602
		public Vector3[] east { readonly get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0002540B File Offset: 0x0002360B
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00025413 File Offset: 0x00023613
		public Vector3[] west { readonly get; set; }
	}
}
