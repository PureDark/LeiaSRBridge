using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	// Token: 0x0200014F RID: 335
	public class SoftMaskToggler : MonoBehaviour
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00027074 File Offset: 0x00025274
		public void Toggle(bool enabled)
		{
			if (this.mask)
			{
				this.mask.GetComponent<SoftMask>().enabled = enabled;
				this.mask.GetComponent<Mask>().enabled = !enabled;
				if (!this.doNotTouchImage)
				{
					this.mask.GetComponent<Image>().enabled = !enabled;
				}
			}
		}

		// Token: 0x0400071C RID: 1820
		public GameObject mask;

		// Token: 0x0400071D RID: 1821
		public bool doNotTouchImage;
	}
}
