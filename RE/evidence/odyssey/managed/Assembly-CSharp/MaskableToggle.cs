using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000075 RID: 117
public class MaskableToggle : MonoBehaviour
{
	// Token: 0x06000283 RID: 643 RVA: 0x0000CEE2 File Offset: 0x0000B0E2
	private void Start()
	{
		bd.ih(MaskableToggle.cfe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		this.cfd = base.GetComponent<Text>();
		if (this.cfd == null)
		{
			bd.@if(MaskableToggle.cfe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ub(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000CF24 File Offset: 0x0000B124
	private void Update()
	{
		if (this.targetRect != null && this.cfd != null)
		{
			if (this.targetRect.anchoredPosition.y < this.threshold)
			{
				this.cfd.maskable = false;
				return;
			}
			this.cfd.maskable = true;
		}
	}

	// Token: 0x040002B0 RID: 688
	public RectTransform targetRect;

	// Token: 0x040002B1 RID: 689
	public float threshold;

	// Token: 0x040002B2 RID: 690
	private Text cfd;

	// Token: 0x040002B3 RID: 691
	private static string cfe = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uc();
}
