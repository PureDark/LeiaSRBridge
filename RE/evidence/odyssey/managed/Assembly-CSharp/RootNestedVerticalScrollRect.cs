using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200007A RID: 122
public class RootNestedVerticalScrollRect : ScrollRect
{
	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060002AF RID: 687 RVA: 0x0000D8AF File Offset: 0x0000BAAF
	// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000D8B7 File Offset: 0x0000BAB7
	public bool cgc { get; private set; }

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
	// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
	public bool cgd { get; private set; }

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000D8D1 File Offset: 0x0000BAD1
	// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000D8D9 File Offset: 0x0000BAD9
	public bool cgj { get; set; }

	// Token: 0x060002B5 RID: 693 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
	protected override void Start()
	{
		base.Start();
		this.cge = false;
		for (int i = 0; i < this.nestedVerticalScrollRects.Length; i++)
		{
			if (this.nestedVerticalScrollRects[i] != null)
			{
				this.nestedVerticalScrollRects[i].pu(this);
				if (this.nestedVerticalScrollRects[i].vertical)
				{
					NestedVerticalScrollRect nestedVerticalScrollRect = this.nestedVerticalScrollRects[i];
					nestedVerticalScrollRect.OnScrollCallback = (UnityAction)Delegate.Remove(nestedVerticalScrollRect.OnScrollCallback, new UnityAction(this.qj));
					NestedVerticalScrollRect nestedVerticalScrollRect2 = this.nestedVerticalScrollRects[i];
					nestedVerticalScrollRect2.OnScrollCallback = (UnityAction)Delegate.Combine(nestedVerticalScrollRect2.OnScrollCallback, new UnityAction(this.qj));
				}
			}
		}
		this.OnScrollCallback = (UnityAction)Delegate.Remove(this.OnScrollCallback, new UnityAction(this.qj));
		this.OnScrollCallback = (UnityAction)Delegate.Combine(this.OnScrollCallback, new UnityAction(this.qj));
		if (this.VerticalScrollbar)
		{
			this.cgd = false;
			this.VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.qw));
			this.VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.qw));
			this.qi();
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000DA2C File Offset: 0x0000BC2C
	public void qi()
	{
		this.qk(false);
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000DA35 File Offset: 0x0000BC35
	public void qj()
	{
		this.qk(true);
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000DA40 File Offset: 0x0000BC40
	private void qk(bool a)
	{
		if (this.VerticalScrollbar == null)
		{
			return;
		}
		if (this.VerticalScrollbar.gameObject.activeInHierarchy)
		{
			if (this.cgj)
			{
				this.VerticalScrollbar.OnPointerEnter(null);
			}
			else
			{
				this.VerticalScrollbar.OnPointerExit(null);
			}
		}
		CanvasGroup component = this.VerticalScrollbar.GetComponent<CanvasGroup>();
		DOTween.Kill(component, false);
		if (a)
		{
			this.VerticalScrollbar.gameObject.SetActive(true);
			DOTweenModuleUI.DOFade(component, 1f, 0.3f);
			return;
		}
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(component, 0f, 0.3f), new TweenCallback(this.rc));
		this.VerticalScrollbar.OnDeselect(null);
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
	public int ql()
	{
		if (this.nestedVerticalScrollRects == null || this.nestedVerticalScrollRects.Length == 0)
		{
			return -1;
		}
		float y = base.content.anchoredPosition.y;
		for (int i = 0; i < this.nestedVerticalScrollRects.Length; i++)
		{
			NestedVerticalScrollRect nestedVerticalScrollRect = this.nestedVerticalScrollRects[i];
			if (nestedVerticalScrollRect != null && nestedVerticalScrollRect.cfj)
			{
				float y2 = nestedVerticalScrollRect.czw.anchoredPosition.y;
				if (Mathf.Abs(y - Mathf.Abs(y2)) < 5f)
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000DB80 File Offset: 0x0000BD80
	public NestedVerticalScrollRect qm()
	{
		int num = this.ql();
		if (num >= 0 && num < this.nestedVerticalScrollRects.Length && this.nestedVerticalScrollRects[num] != null)
		{
			return this.nestedVerticalScrollRects[num];
		}
		return null;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
	public override void OnScroll(PointerEventData data)
	{
		NestedVerticalScrollRect nestedVerticalScrollRect = this.qm();
		if (nestedVerticalScrollRect != null && nestedVerticalScrollRect.pw(data.scrollDelta))
		{
			nestedVerticalScrollRect.OnScroll(data);
		}
		else if (this.qn(data.scrollDelta.y))
		{
			base.OnScroll(data);
			UnityAction onScrollCallback = this.OnScrollCallback;
			if (onScrollCallback != null)
			{
				onScrollCallback.Invoke();
			}
		}
		this.cge = true;
		this.cgd = data.IsScrolling();
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000DC34 File Offset: 0x0000BE34
	public override void OnBeginDrag(PointerEventData eventData)
	{
		NestedVerticalScrollRect nestedVerticalScrollRect = this.qm();
		if (nestedVerticalScrollRect != null)
		{
			nestedVerticalScrollRect.OnBeginDrag(eventData);
		}
		else
		{
			base.OnBeginDrag(eventData);
			this.cgc = true;
		}
		this.cge = true;
		this.qj();
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000DC78 File Offset: 0x0000BE78
	public override void OnDrag(PointerEventData eventData)
	{
		NestedVerticalScrollRect nestedVerticalScrollRect = this.qm();
		if (nestedVerticalScrollRect != null && nestedVerticalScrollRect.pw(eventData.scrollDelta))
		{
			nestedVerticalScrollRect.OnDrag(eventData);
			return;
		}
		if (!this.cgc)
		{
			base.OnBeginDrag(eventData);
			this.cgc = true;
		}
		base.OnDrag(eventData);
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		this.cgc = false;
		foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.nestedVerticalScrollRects)
		{
			if (nestedVerticalScrollRect != null && nestedVerticalScrollRect.cfk)
			{
				nestedVerticalScrollRect.OnEndDrag(eventData);
			}
		}
		this.qi();
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000DD1C File Offset: 0x0000BF1C
	private bool qn(float a)
	{
		if (base.content == null)
		{
			return false;
		}
		if (base.content.sizeDelta.y <= base.viewport.rect.height)
		{
			return false;
		}
		float verticalNormalizedPosition = base.verticalNormalizedPosition;
		if (a > 0f)
		{
			return verticalNormalizedPosition < 0.99f;
		}
		return a < 0f && verticalNormalizedPosition > 0.01f;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000DD8C File Offset: 0x0000BF8C
	private void Update()
	{
		this.qo();
		if (this.cfw != Vector2.zero)
		{
			if (this.cfx == null)
			{
				this.cfx = new PointerEventData(EventSystem.current);
			}
			this.cfx.scrollDelta = this.cfw;
			this.OnScroll(this.cfx);
			return;
		}
		if (this.cfx != null)
		{
			this.cfx.scrollDelta = Vector2.zero;
		}
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000DE00 File Offset: 0x0000C000
	private void qo()
	{
		KeyCode a;
		if (this.qt(out a))
		{
			this.qp(a);
			return;
		}
		KeyCode a2;
		if (this.qu(out a2))
		{
			this.qr(a2);
			return;
		}
		if (this.cfy == RootNestedVerticalScrollRect.ch.cfs && this.cga <= 0.3f)
		{
			this.cga += 0.1f;
			return;
		}
		this.qs(Vector2.zero);
		this.qv(RootNestedVerticalScrollRect.ch.cfr);
		this.cga = 0f;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000DE78 File Offset: 0x0000C078
	private void qp(KeyCode a)
	{
		if (a <= 274)
		{
			if (a == 273)
			{
				this.qs(Vector2.up * 0.0125f);
				this.qv(RootNestedVerticalScrollRect.ch.cft);
				return;
			}
			if (a != 274)
			{
				return;
			}
			this.qs(Vector2.down * 0.0125f);
			this.qv(RootNestedVerticalScrollRect.ch.cft);
			return;
		}
		else
		{
			if (a == 280)
			{
				this.cga = 0f;
				foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.nestedVerticalScrollRects)
				{
					if (nestedVerticalScrollRect.vertical)
					{
						nestedVerticalScrollRect.pv(true);
					}
				}
				NestedVerticalScrollRect nestedVerticalScrollRect2 = this.qm();
				if (nestedVerticalScrollRect2 != null && nestedVerticalScrollRect2.verticalNormalizedPosition < 0.99f)
				{
					this.qq(nestedVerticalScrollRect2, nestedVerticalScrollRect2.verticalNormalizedPosition + 0.2f);
				}
				else if (base.verticalNormalizedPosition < 0.99f)
				{
					this.qq(this, 1f);
				}
				this.qv(RootNestedVerticalScrollRect.ch.cfs);
				return;
			}
			if (a != 281)
			{
				return;
			}
			this.cga = 0f;
			foreach (NestedVerticalScrollRect nestedVerticalScrollRect3 in this.nestedVerticalScrollRects)
			{
				if (nestedVerticalScrollRect3.vertical)
				{
					nestedVerticalScrollRect3.pv(true);
				}
			}
			NestedVerticalScrollRect nestedVerticalScrollRect4 = this.qm();
			if (base.verticalNormalizedPosition > 0.01f)
			{
				this.qq(this, 0f);
			}
			else if (nestedVerticalScrollRect4 != null && nestedVerticalScrollRect4.verticalNormalizedPosition > 0.01f)
			{
				this.qq(nestedVerticalScrollRect4, nestedVerticalScrollRect4.verticalNormalizedPosition - 0.2f);
			}
			this.qv(RootNestedVerticalScrollRect.ch.cfs);
			return;
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000E000 File Offset: 0x0000C200
	private void qq(ScrollRect a, float b)
	{
		RootNestedVerticalScrollRect.ci ci = new RootNestedVerticalScrollRect.ci();
		ci.cfu = a;
		float verticalNormalizedPosition = ci.cfu.verticalNormalizedPosition;
		float num = Mathf.Clamp01(b);
		DOVirtual.Float(verticalNormalizedPosition, num, 0.3f, new TweenCallback<float>(ci.qc));
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000E044 File Offset: 0x0000C244
	private void qr(KeyCode a)
	{
		if (this.cfy != RootNestedVerticalScrollRect.ch.cft)
		{
			if (this.cfy == RootNestedVerticalScrollRect.ch.cfs)
			{
				if (this.cga >= this.cfz)
				{
					DOTween.Kill(this, false);
					if (a != 280)
					{
						if (a == 281)
						{
							this.qs(Vector2.down * 0.05f);
						}
					}
					else
					{
						this.qs(Vector2.up * 0.05f);
					}
				}
				this.cga += 0.1f;
			}
			return;
		}
		DOTween.Kill(this, false);
		if (a == 273)
		{
			this.qs(Vector2.up * 0.0125f);
			return;
		}
		if (a != 274)
		{
			return;
		}
		this.qs(Vector2.down * 0.0125f);
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000E10C File Offset: 0x0000C30C
	private void qs(Vector2 a)
	{
		if (this.cfw == a)
		{
			return;
		}
		this.cfw = a;
		bool a2 = this.cfw != Vector2.zero && this.cfw.y != 0f;
		foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.nestedVerticalScrollRects)
		{
			if (nestedVerticalScrollRect.vertical)
			{
				nestedVerticalScrollRect.pv(a2);
			}
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000E184 File Offset: 0x0000C384
	private bool qt(out KeyCode a)
	{
		a = 0;
		foreach (KeyCode keyCode in this.cgb)
		{
			if (Input.GetKeyDown(keyCode))
			{
				a = keyCode;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000E1BC File Offset: 0x0000C3BC
	private bool qu(out KeyCode a)
	{
		a = 0;
		foreach (KeyCode keyCode in this.cgb)
		{
			if (Input.GetKey(keyCode))
			{
				a = keyCode;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000E1F3 File Offset: 0x0000C3F3
	private void qv(RootNestedVerticalScrollRect.ch a)
	{
		this.cfy = a;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000E1FC File Offset: 0x0000C3FC
	private void qw(float a)
	{
		this.qx(a, 1);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000E208 File Offset: 0x0000C408
	protected void qx(float a, int b)
	{
		float num = this.qy();
		float num2 = this.m_ContentBounds.size[b] / num;
		if (1f - num2 < a)
		{
			float verticalNormalizedPosition = Mathf.Clamp01((a - (1f - num2)) / num2);
			base.verticalNormalizedPosition = verticalNormalizedPosition;
			if (this.nestedVerticalScrollRects != null)
			{
				foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.nestedVerticalScrollRects)
				{
					if (nestedVerticalScrollRect.vertical && nestedVerticalScrollRect.verticalNormalizedPosition != 1f)
					{
						nestedVerticalScrollRect.verticalNormalizedPosition = 1f;
						break;
					}
				}
			}
		}
		else
		{
			base.verticalNormalizedPosition = 0f;
			if (this.nestedVerticalScrollRects != null)
			{
				float num3 = 1f - num2;
				float verticalNormalizedPosition2 = Mathf.Clamp01(a / num3);
				foreach (NestedVerticalScrollRect nestedVerticalScrollRect2 in this.nestedVerticalScrollRects)
				{
					if (nestedVerticalScrollRect2.vertical)
					{
						nestedVerticalScrollRect2.verticalNormalizedPosition = verticalNormalizedPosition2;
						break;
					}
				}
			}
		}
		if (this.cge)
		{
			this.cgd = true;
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000E31C File Offset: 0x0000C51C
	private float qy()
	{
		float num = 0f;
		num += this.m_ContentBounds.size.y;
		if (this.nestedVerticalScrollRects != null)
		{
			foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.nestedVerticalScrollRects)
			{
				if (nestedVerticalScrollRect.vertical)
				{
					float num2 = nestedVerticalScrollRect.content.rect.height + Mathf.Abs(nestedVerticalScrollRect.czv.anchoredPosition.y);
					if (num2 > num)
					{
						num += num2;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000E3A2 File Offset: 0x0000C5A2
	protected override void LateUpdate()
	{
		base.LateUpdate();
		this.qz();
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
	private void qz()
	{
		float num = this.qy();
		float num2 = this.m_ContentBounds.size[1] / num;
		float num3 = 1f - num2;
		bool flag = !base.velocity[1].Equals(0f) || this.cgj;
		if (this.nestedVerticalScrollRects != null)
		{
			foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.nestedVerticalScrollRects)
			{
				if (nestedVerticalScrollRect.vertical)
				{
					float num4 = Mathf.Clamp01(base.verticalNormalizedPosition * num2 + nestedVerticalScrollRect.verticalNormalizedPosition * num3);
					if (this.VerticalScrollbar.value != num4)
					{
						this.VerticalScrollbar.value = num4;
					}
					if (this.VerticalScrollbar.value != this.cgf)
					{
						this.cgf = this.VerticalScrollbar.value;
					}
					else
					{
						nestedVerticalScrollRect.IsScrolling = false;
						this.cgd = false;
					}
					flag |= (!nestedVerticalScrollRect.velocity[1].Equals(0f) || nestedVerticalScrollRect.IsScrolling || this.cgd);
				}
			}
		}
		if (flag)
		{
			this.cgh = 0f;
			this.qk(flag);
			return;
		}
		if (this.cgh >= 2f)
		{
			this.qk(flag);
			this.cgh = 0f;
			return;
		}
		this.cgh += Time.unscaledDeltaTime;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0000E538 File Offset: 0x0000C738
	public void ra(Vector2 a)
	{
		this.cfv = new Bounds(base.viewRect.rect.center, base.viewRect.rect.size);
		float num = this.qy();
		if (this.m_ContentBounds.size.y > 0f)
		{
			this.VerticalScrollbar.size = Mathf.Clamp01((this.cfv.size.y - Mathf.Abs(a.y)) / num * 1.5f);
		}
		else
		{
			this.VerticalScrollbar.size = 1f;
		}
		this.VerticalScrollbar.value = base.verticalNormalizedPosition;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
	public RootNestedVerticalScrollRect()
	{
		KeyCode[] array = new KeyCode[4];
		RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.9028D2E0A4E3B480DD296390D69DA2192B7A351DBD6FCDC6BD052441F5A6F264).FieldHandle);
		this.cgb = array;
		base..ctor();
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000E620 File Offset: 0x0000C820
	[CompilerGenerated]
	private void rc()
	{
		this.VerticalScrollbar.gameObject.SetActive(false);
		if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == this.VerticalScrollbar)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x040002CC RID: 716
	[SerializeField]
	private NestedVerticalScrollRect[] nestedVerticalScrollRects;

	// Token: 0x040002CD RID: 717
	[SerializeField]
	private Scrollbar VerticalScrollbar;

	// Token: 0x040002CE RID: 718
	private Bounds cfv;

	// Token: 0x040002CF RID: 719
	public UnityAction OnScrollCallback;

	// Token: 0x040002D0 RID: 720
	private Vector2 cfw;

	// Token: 0x040002D1 RID: 721
	private PointerEventData cfx;

	// Token: 0x040002D2 RID: 722
	private RootNestedVerticalScrollRect.ch cfy;

	// Token: 0x040002D3 RID: 723
	private float cfz = 0.8f;

	// Token: 0x040002D4 RID: 724
	private float cga;

	// Token: 0x040002D5 RID: 725
	private KeyCode[] cgb;

	// Token: 0x040002D8 RID: 728
	private bool cge;

	// Token: 0x040002D9 RID: 729
	private float cgf;

	// Token: 0x040002DA RID: 730
	private const float cgg = 2f;

	// Token: 0x040002DB RID: 731
	private float cgh;

	// Token: 0x040002DC RID: 732
	private const float cgi = 0.3f;

	// Token: 0x0200007B RID: 123
	public enum ch
	{
		// Token: 0x040002DF RID: 735
		cfr,
		// Token: 0x040002E0 RID: 736
		cfs,
		// Token: 0x040002E1 RID: 737
		cft
	}

	// Token: 0x0200007C RID: 124
	[CompilerGenerated]
	private sealed class ci
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000E66D File Offset: 0x0000C86D
		internal void jwl(float a)
		{
			this.cfu.verticalNormalizedPosition = a;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E66D File Offset: 0x0000C86D
		internal void cuy(float a)
		{
			this.cfu.verticalNormalizedPosition = a;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E66D File Offset: 0x0000C86D
		internal void eg(float a)
		{
			this.cfu.verticalNormalizedPosition = a;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E66D File Offset: 0x0000C86D
		internal void qc(float a)
		{
			this.cfu.verticalNormalizedPosition = a;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E66D File Offset: 0x0000C86D
		internal void ijs(float a)
		{
			this.cfu.verticalNormalizedPosition = a;
		}

		// Token: 0x040002E2 RID: 738
		public ScrollRect cfu;
	}
}
