using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	// Token: 0x02000149 RID: 329
	[RequireComponent(typeof(RectTransform))]
	public class PaintedMask : UIBehaviour
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x000269A4 File Offset: 0x00024BA4
		protected override void Start()
		{
			base.Start();
			this.ctr = new RenderTexture((int)this.ddg.x, (int)this.ddg.y, 0, 0);
			this.ctr.Create();
			this.renderCamera.targetTexture = this.ctr;
			this.targetMask.boy(this.ctr);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00026A0C File Offset: 0x00024C0C
		private Vector2 ddg
		{
			get
			{
				return ((RectTransform)this.targetMask.transform).rect.size;
			}
		}

		// Token: 0x040006FB RID: 1787
		public Camera renderCamera;

		// Token: 0x040006FC RID: 1788
		public SoftMask targetMask;

		// Token: 0x040006FD RID: 1789
		private RenderTexture ctr;
	}
}
