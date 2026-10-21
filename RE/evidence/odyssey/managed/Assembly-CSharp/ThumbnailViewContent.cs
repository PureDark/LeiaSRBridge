using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AC RID: 172
public class ThumbnailViewContent : MonoBehaviour
{
	// Token: 0x0600045B RID: 1115 RVA: 0x00014411 File Offset: 0x00012611
	private void Start()
	{
		this.outOfViewY = ((this.displayType == global::a.bry) ? 180f : ((this.displayType == global::a.bsa) ? 186f : 0f));
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00014440 File Offset: 0x00012640
	private void Update()
	{
		bool flag = false;
		if (this.totalContent != null)
		{
			float y = this.totalContent.anchoredPosition.y;
			flag = ((this.displayType == global::a.bry) ? (y > this.outOfViewY) : (this.displayType == global::a.bsa && y < this.outOfViewY));
		}
		if (flag != this.isContentOutOfView)
		{
			this.ze(base.gameObject, !flag);
			this.isContentOutOfView = flag;
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000144B8 File Offset: 0x000126B8
	private void ze(GameObject a, bool b)
	{
		if (a != null)
		{
			foreach (object obj in a.transform)
			{
				Transform transform = ((Transform)obj).Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uj());
				if (transform != null)
				{
					Image component = transform.GetComponent<Image>();
					if (component != null)
					{
						component.raycastTarget = b;
					}
				}
			}
		}
	}

	// Token: 0x04000427 RID: 1063
	public RectTransform totalContent;

	// Token: 0x04000428 RID: 1064
	public bool isContentOutOfView;

	// Token: 0x04000429 RID: 1065
	public float outOfViewY;

	// Token: 0x0400042A RID: 1066
	public global::a displayType;
}
