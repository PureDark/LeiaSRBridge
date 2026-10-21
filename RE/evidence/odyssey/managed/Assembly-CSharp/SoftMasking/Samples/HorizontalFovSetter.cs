using System;
using UnityEngine;

namespace SoftMasking.Samples
{
	// Token: 0x02000144 RID: 324
	[RequireComponent(typeof(Camera))]
	public class HorizontalFovSetter : MonoBehaviour
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x00026572 File Offset: 0x00024772
		public void Awake()
		{
			this.ctn = base.GetComponent<Camera>();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00026580 File Offset: 0x00024780
		public void Update()
		{
			this.ctn.fieldOfView = this.horizontalFov / this.ctn.aspect;
		}

		// Token: 0x040006E5 RID: 1765
		public float horizontalFov;

		// Token: 0x040006E6 RID: 1766
		private Camera ctn;
	}
}
