using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000077 RID: 119
public class NestedScrollRect : ScrollRect, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x0600028B RID: 651 RVA: 0x0000D028 File Offset: 0x0000B228
	public override void OnInitializePotentialDrag(PointerEventData eventData)
	{
		base.OnInitializePotentialDrag(eventData);
		ScrollRect scrollRect = this.pi();
		if (scrollRect != null)
		{
			scrollRect.OnInitializePotentialDrag(eventData);
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000D054 File Offset: 0x0000B254
	public override void OnBeginDrag(PointerEventData eventData)
	{
		if (Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
		{
			this.cfh = true;
			ScrollRect scrollRect = this.pi();
			if (scrollRect != null)
			{
				scrollRect.OnBeginDrag(eventData);
				return;
			}
		}
		else
		{
			this.pj();
			base.OnBeginDrag(eventData);
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
	public override void OnDrag(PointerEventData eventData)
	{
		if (this.cfh)
		{
			ScrollRect scrollRect = this.pi();
			if (scrollRect != null)
			{
				scrollRect.OnDrag(eventData);
				return;
			}
		}
		else
		{
			base.OnDrag(eventData);
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
	public override void OnEndDrag(PointerEventData eventData)
	{
		if (this.cfh)
		{
			ScrollRect scrollRect = this.pi();
			if (scrollRect != null)
			{
				scrollRect.OnEndDrag(eventData);
			}
			this.cfh = false;
			return;
		}
		base.OnEndDrag(eventData);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000D120 File Offset: 0x0000B320
	public override void OnScroll(PointerEventData eventData)
	{
		ScrollRect scrollRect = this.pi();
		if (scrollRect != null && !this.pl() && !Input.GetKey(304) && !Input.GetKey(303))
		{
			scrollRect.OnScroll(eventData);
			return;
		}
		base.OnScroll(eventData);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000D16C File Offset: 0x0000B36C
	private ScrollRect pi()
	{
		return base.transform.parent.GetComponentInParent<ScrollRect>();
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000D17E File Offset: 0x0000B37E
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.pl())
		{
			NestedScrollRect.pk();
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000D18D File Offset: 0x0000B38D
	public void pj()
	{
		NestedScrollRect.focusedScrollRect = this;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000D195 File Offset: 0x0000B395
	public static void pk()
	{
		NestedScrollRect.focusedScrollRect = null;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000D19D File Offset: 0x0000B39D
	public bool pl()
	{
		return NestedScrollRect.focusedScrollRect != null && NestedScrollRect.focusedScrollRect == this;
	}

	// Token: 0x040002B7 RID: 695
	private bool cfh;

	// Token: 0x040002B8 RID: 696
	public static ScrollRect focusedScrollRect;
}
