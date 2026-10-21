using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000065 RID: 101
public class CustomShade : MonoBehaviour
{
	// Token: 0x0600021C RID: 540 RVA: 0x0000A9A6 File Offset: 0x00008BA6
	[RuntimeInitializeOnLoadMethod(4)]
	private static void Initialize()
	{
		CustomShade.ccr.Clear();
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000A9B2 File Offset: 0x00008BB2
	private void OnEnable()
	{
		CustomShade.ccr.Add(this);
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000A9BF File Offset: 0x00008BBF
	private void OnDisable()
	{
		CustomShade.ccr.Remove(this);
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000A9D0 File Offset: 0x00008BD0
	private void Start()
	{
		this.xPosDifference = base.GetComponent<RectTransform>().sizeDelta.x + 50f + 1f;
		this.yPosDifference = base.GetComponent<RectTransform>().sizeDelta.y + 50f + 1f;
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000AA30 File Offset: 0x00008C30
	private void nl()
	{
		foreach (CustomShade customShade in new List<CustomShade>(CustomShade.ccr))
		{
			RectTransform component = customShade.GetComponent<RectTransform>();
			if (!(this.rectTransform.parent != component.parent))
			{
				if (this.useHorizontalShades && Mathf.Approximately(this.rectTransform.anchoredPosition.y, component.anchoredPosition.y) && Mathf.Abs(this.rectTransform.anchoredPosition.x - component.anchoredPosition.x) <= this.xPosDifference)
				{
					if (this.rectTransform.anchoredPosition.x < component.anchoredPosition.x)
					{
						this.rightCustomShade = customShade;
					}
					else if (this.rectTransform.anchoredPosition.x > component.anchoredPosition.x)
					{
						this.leftCustomShade = customShade;
					}
				}
				if (this.useVerticalShades && Mathf.Approximately(this.rectTransform.anchoredPosition.x, component.anchoredPosition.x) && Mathf.Abs(this.rectTransform.anchoredPosition.y - component.anchoredPosition.y) <= this.yPosDifference)
				{
					if (this.rectTransform.anchoredPosition.y < component.anchoredPosition.y)
					{
						this.topCustomShade = customShade;
					}
					else if (this.rectTransform.anchoredPosition.y > component.anchoredPosition.y)
					{
						this.bottomCustomShade = customShade;
					}
				}
			}
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000ABF4 File Offset: 0x00008DF4
	public void nm()
	{
		this.nl();
		if (this.rightCustomShade)
		{
			this.rightCustomShade.nq();
		}
		if (this.leftCustomShade)
		{
			this.leftCustomShade.np();
		}
		if (this.topCustomShade)
		{
			this.topCustomShade.ns();
		}
		if (this.bottomCustomShade)
		{
			this.bottomCustomShade.nr();
		}
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000AC68 File Offset: 0x00008E68
	public void nn()
	{
		if (this.rightCustomShade)
		{
			this.rightCustomShade.no();
		}
		if (this.leftCustomShade)
		{
			this.leftCustomShade.no();
		}
		if (this.topCustomShade)
		{
			this.topCustomShade.no();
		}
		if (this.bottomCustomShade)
		{
			this.bottomCustomShade.no();
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000ACD8 File Offset: 0x00008ED8
	public void no()
	{
		this.nt(this.leftShade, 0f);
		this.nt(this.rightShade, 0f);
		this.nt(this.topShade, 0f);
		this.nt(this.bottomShade, 0f);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000AD29 File Offset: 0x00008F29
	public void np()
	{
		this.nt(this.rightShade, 1f);
		this.nt(this.leftShade, 0f);
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000AD4D File Offset: 0x00008F4D
	public void nq()
	{
		this.nt(this.rightShade, 0f);
		this.nt(this.leftShade, 1f);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000AD71 File Offset: 0x00008F71
	public void nr()
	{
		this.nt(this.topShade, 1f);
		this.nt(this.bottomShade, 0f);
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000AD95 File Offset: 0x00008F95
	public void ns()
	{
		this.nt(this.topShade, 0f);
		this.nt(this.bottomShade, 1f);
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000ADBC File Offset: 0x00008FBC
	private void nt(GameObject a, float b)
	{
		if (a == null)
		{
			return;
		}
		CanvasGroup component = a.GetComponent<CanvasGroup>();
		if (component != null)
		{
			component.alpha = b;
		}
	}

	// Token: 0x04000220 RID: 544
	public GameObject leftShade;

	// Token: 0x04000221 RID: 545
	public GameObject rightShade;

	// Token: 0x04000222 RID: 546
	public GameObject topShade;

	// Token: 0x04000223 RID: 547
	public GameObject bottomShade;

	// Token: 0x04000224 RID: 548
	private static string ccq = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ss();

	// Token: 0x04000225 RID: 549
	private static List<CustomShade> ccr = new List<CustomShade>();

	// Token: 0x04000226 RID: 550
	public bool useHorizontalShades;

	// Token: 0x04000227 RID: 551
	public float xPosDifference;

	// Token: 0x04000228 RID: 552
	public bool useVerticalShades;

	// Token: 0x04000229 RID: 553
	public float yPosDifference;

	// Token: 0x0400022A RID: 554
	public RectTransform rectTransform;

	// Token: 0x0400022B RID: 555
	public CustomShade leftCustomShade;

	// Token: 0x0400022C RID: 556
	public CustomShade rightCustomShade;

	// Token: 0x0400022D RID: 557
	public CustomShade topCustomShade;

	// Token: 0x0400022E RID: 558
	public CustomShade bottomCustomShade;
}
