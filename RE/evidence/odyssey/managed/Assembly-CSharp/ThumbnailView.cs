using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000AB RID: 171
public class ThumbnailView : MonoBehaviour
{
	// Token: 0x06000457 RID: 1111 RVA: 0x00014367 File Offset: 0x00012567
	private void Start()
	{
		this.totalScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.zd));
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00014388 File Offset: 0x00012588
	private void Update()
	{
		if (this.totalContent != null)
		{
			float y = this.totalContent.anchoredPosition.y;
			this.scrollButtons.anchoredPosition = new Vector2(this.scrollButtons.anchoredPosition.x, y);
		}
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x000143D8 File Offset: 0x000125D8
	[CompilerGenerated]
	private void zd(Vector2 a)
	{
		float verticalNormalizedPosition = this.totalScrollRect.verticalNormalizedPosition;
		if (Mathf.Abs(verticalNormalizedPosition - this.clt) > 1E-45f)
		{
			NestedScrollRect.pk();
			this.clt = verticalNormalizedPosition;
		}
	}

	// Token: 0x04000423 RID: 1059
	public RectTransform scrollButtons;

	// Token: 0x04000424 RID: 1060
	public RectTransform totalContent;

	// Token: 0x04000425 RID: 1061
	public ScrollRect totalScrollRect;

	// Token: 0x04000426 RID: 1062
	private float clt;
}
