using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000068 RID: 104
public class FadeOutWithContentPosition : MonoBehaviour
{
	// Token: 0x06000236 RID: 566 RVA: 0x0000B4D0 File Offset: 0x000096D0
	private void Start()
	{
		bd.ih(FadeOutWithContentPosition.cdh, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		this.cdf = base.GetComponent<CanvasGroup>();
		this.cdg = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000B4FC File Offset: 0x000096FC
	private void Update()
	{
		float y = this.targetRect.anchoredPosition.y;
		float num = -this.cdg.anchoredPosition.y;
		float num2 = num - this.cdg.rect.height;
		if (y >= num)
		{
			this.cdf.alpha = 0f;
			return;
		}
		if (y <= num2)
		{
			this.cdf.alpha = 1f;
			return;
		}
		float alpha = Mathf.InverseLerp(num, num2, y);
		this.cdf.alpha = alpha;
	}

	// Token: 0x04000243 RID: 579
	public RectTransform targetRect;

	// Token: 0x04000244 RID: 580
	private CanvasGroup cdf;

	// Token: 0x04000245 RID: 581
	private RectTransform cdg;

	// Token: 0x04000246 RID: 582
	private static string cdh = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.su();
}
