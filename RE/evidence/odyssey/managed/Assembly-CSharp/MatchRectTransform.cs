using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000076 RID: 118
public class MatchRectTransform : MonoBehaviour
{
	// Token: 0x06000287 RID: 647 RVA: 0x0000CF8C File Offset: 0x0000B18C
	private void Start()
	{
		this.cff = base.GetComponent<RectTransform>();
		if (this.targetRect != null)
		{
			this.cff.sizeDelta = this.targetRect.sizeDelta;
			this.cff.anchoredPosition = this.targetRect.anchoredPosition;
			return;
		}
		bd.ie(MatchRectTransform.cfg, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ud(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
	private void Update()
	{
		if (this.targetRect != null)
		{
			this.cff.anchoredPosition = this.targetRect.anchoredPosition;
		}
	}

	// Token: 0x040002B4 RID: 692
	public RectTransform targetRect;

	// Token: 0x040002B5 RID: 693
	private RectTransform cff;

	// Token: 0x040002B6 RID: 694
	private static string cfg = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ue();
}
