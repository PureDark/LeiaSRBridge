using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000078 RID: 120
public class NestedVerticalScrollRect : ScrollRect, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000296 RID: 662 RVA: 0x0000D1C1 File Offset: 0x0000B3C1
	// (set) Token: 0x06000297 RID: 663 RVA: 0x0000D1C9 File Offset: 0x0000B3C9
	public bool cfj { get; private set; }

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000298 RID: 664 RVA: 0x0000D1D2 File Offset: 0x0000B3D2
	// (set) Token: 0x06000299 RID: 665 RVA: 0x0000D1DA File Offset: 0x0000B3DA
	public bool cfk { get; private set; }

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600029A RID: 666 RVA: 0x0000D1E3 File Offset: 0x0000B3E3
	// (set) Token: 0x0600029B RID: 667 RVA: 0x0000D1EB File Offset: 0x0000B3EB
	public bool czu
	{
		get
		{
			return this.cfl;
		}
		set
		{
			if (this.cfl == value)
			{
				return;
			}
			this.cfl = value;
			UnityAction<bool> onFocusChangeCallback = this.OnFocusChangeCallback;
			if (onFocusChangeCallback == null)
			{
				return;
			}
			onFocusChangeCallback.Invoke(this.cfl);
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x0600029C RID: 668 RVA: 0x0000D214 File Offset: 0x0000B414
	public RectTransform czv
	{
		get
		{
			RectTransform result;
			if ((result = this.cfm) == null)
			{
				result = (this.cfm = base.GetComponent<RectTransform>());
			}
			return result;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x0600029D RID: 669 RVA: 0x0000D23A File Offset: 0x0000B43A
	public RectTransform czw
	{
		get
		{
			return this.m_Parent;
		}
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000D242 File Offset: 0x0000B442
	public void pu(RootNestedVerticalScrollRect a)
	{
		this.cfi = a;
		this.pv(false);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000D252 File Offset: 0x0000B452
	public void pv(bool a)
	{
		if (this.cfj == a)
		{
			return;
		}
		this.cfj = a;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000D268 File Offset: 0x0000B468
	public override void OnScroll(PointerEventData data)
	{
		if (base.horizontal)
		{
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.x = scrollDelta.y * -1f;
			scrollDelta.y *= (this.czu ? 0f : 1f);
			data.scrollDelta = scrollDelta;
		}
		if (this.pw(data.scrollDelta))
		{
			data.scrollDelta *= (base.horizontal ? -1f : 1f);
			base.OnScroll(data);
			UnityAction onScrollCallback = this.OnScrollCallback;
			if (onScrollCallback != null)
			{
				onScrollCallback.Invoke();
			}
		}
		else if (this.cfi != null)
		{
			this.cfi.OnScroll(data);
		}
		else
		{
			base.OnScroll(data);
			UnityAction onScrollCallback2 = this.OnScrollCallback;
			if (onScrollCallback2 != null)
			{
				onScrollCallback2.Invoke();
			}
		}
		this.IsScrolling = data.IsScrolling();
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000D34C File Offset: 0x0000B54C
	public override void OnBeginDrag(PointerEventData eventData)
	{
		if (base.horizontal)
		{
			this.czu = true;
		}
		if (this.pw(eventData.delta * -1f) && !this.qb(eventData))
		{
			base.OnBeginDrag(eventData);
			this.cfk = true;
			return;
		}
		if (this.cfi != null)
		{
			this.cfi.OnBeginDrag(eventData);
			return;
		}
		base.OnBeginDrag(eventData);
		this.cfk = true;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
	public override void OnDrag(PointerEventData eventData)
	{
		if (this.pw(eventData.delta * -1f) && !this.qb(eventData))
		{
			if (!this.cfk)
			{
				base.OnBeginDrag(eventData);
				this.cfk = true;
			}
			base.OnDrag(eventData);
			return;
		}
		if (this.cfi != null)
		{
			this.cfi.OnDrag(eventData);
			return;
		}
		if (!this.cfk)
		{
			base.OnBeginDrag(eventData);
			this.cfk = true;
		}
		base.OnDrag(eventData);
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000D448 File Offset: 0x0000B648
	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		this.cfk = false;
		if (this.cfi != null && this.cfi.cgc)
		{
			this.cfi.OnEndDrag(eventData);
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000D480 File Offset: 0x0000B680
	public bool pw(Vector2 a)
	{
		bool flag = this.px(a) && this.qa();
		this.pv(flag);
		return flag;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
	private bool px(Vector2 a)
	{
		if (base.content == null)
		{
			return false;
		}
		if (base.vertical)
		{
			if (base.content.sizeDelta.y <= base.viewport.rect.height)
			{
				return false;
			}
			float verticalNormalizedPosition = base.verticalNormalizedPosition;
			if (a.y > 0f)
			{
				return verticalNormalizedPosition < 1f;
			}
			if (a.y < 0f)
			{
				return verticalNormalizedPosition > 0f;
			}
		}
		if (base.horizontal)
		{
			if (base.content.sizeDelta.x <= base.viewport.rect.width)
			{
				return false;
			}
			if (Mathf.Abs(a.x) <= Mathf.Abs(a.y))
			{
				return false;
			}
			float horizontalNormalizedPosition = base.horizontalNormalizedPosition;
			if (a.x > 0f)
			{
				return horizontalNormalizedPosition < 1f;
			}
			if (a.x < 0f)
			{
				return horizontalNormalizedPosition > 0f;
			}
		}
		return false;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000D5A9 File Offset: 0x0000B7A9
	public void py()
	{
		if (base.content != null)
		{
			if (base.vertical)
			{
				base.verticalNormalizedPosition = 1f;
			}
			if (base.horizontal)
			{
				base.horizontalNormalizedPosition = 0f;
			}
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000D5DF File Offset: 0x0000B7DF
	public void pz()
	{
		if (base.content != null)
		{
			if (base.vertical)
			{
				base.verticalNormalizedPosition = 0f;
			}
			if (base.horizontal)
			{
				base.horizontalNormalizedPosition = 1f;
			}
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000D618 File Offset: 0x0000B818
	private bool qa()
	{
		if (this.cfi == null || base.content == null || this.cfi.viewport == null)
		{
			return false;
		}
		if (base.vertical)
		{
			float y = this.m_Parent.anchoredPosition.y;
			return Mathf.Abs(this.cfi.content.anchoredPosition.y - Mathf.Abs(y)) < 5f;
		}
		if (base.horizontal)
		{
			bool flag = Input.GetKey(304) || Input.GetKey(303);
			if (flag)
			{
				this.czu = true;
			}
			return flag || this.czu;
		}
		return false;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000D6CF File Offset: 0x0000B8CF
	private bool qb(PointerEventData a)
	{
		return base.horizontal && Mathf.Abs(a.delta.x) < Mathf.Abs(a.delta.y);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000D6FD File Offset: 0x0000B8FD
	public void OnPointerExit(PointerEventData eventData)
	{
		this.czu = false;
		this.pv(false);
	}

	// Token: 0x040002B9 RID: 697
	private RootNestedVerticalScrollRect cfi;

	// Token: 0x040002BA RID: 698
	[SerializeField]
	private RectTransform m_Parent;

	// Token: 0x040002BD RID: 701
	public bool IsScrolling;

	// Token: 0x040002BE RID: 702
	public UnityAction OnScrollCallback;

	// Token: 0x040002BF RID: 703
	public UnityAction<bool> OnFocusChangeCallback;

	// Token: 0x040002C0 RID: 704
	private bool cfl;

	// Token: 0x040002C1 RID: 705
	private RectTransform cfm;
}
