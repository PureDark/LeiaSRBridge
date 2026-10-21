using System;
using UnityEngine;

namespace SoftMasking.Samples
{
	// Token: 0x02000148 RID: 328
	public class Minimap : MonoBehaviour
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x000268C7 File Offset: 0x00024AC7
		public void LateUpdate()
		{
			this.map.anchoredPosition = -this.marker.anchoredPosition * this.ctq;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000268EF File Offset: 0x00024AEF
		public void bkr()
		{
			this.ctq = this.bkt(this.ctq + this.zoomStep);
			this.map.localScale = Vector3.one * this.ctq;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00026925 File Offset: 0x00024B25
		public void bks()
		{
			this.ctq = this.bkt(this.ctq - this.zoomStep);
			this.map.localScale = Vector3.one * this.ctq;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002695B File Offset: 0x00024B5B
		private float bkt(float a)
		{
			return Mathf.Clamp(a, this.minZoom, this.maxZoom);
		}

		// Token: 0x040006F5 RID: 1781
		public RectTransform map;

		// Token: 0x040006F6 RID: 1782
		public RectTransform marker;

		// Token: 0x040006F7 RID: 1783
		[Space]
		public float minZoom = 0.8f;

		// Token: 0x040006F8 RID: 1784
		public float maxZoom = 1.4f;

		// Token: 0x040006F9 RID: 1785
		public float zoomStep = 0.2f;

		// Token: 0x040006FA RID: 1786
		private float ctq = 1f;
	}
}
