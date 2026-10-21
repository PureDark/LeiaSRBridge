using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x0200007E RID: 126
[RequireComponent(typeof(RectTransform))]
public class BaseCustomButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IDropHandler
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002DA RID: 730 RVA: 0x0000E69B File Offset: 0x0000C89B
	// (set) Token: 0x060002DB RID: 731 RVA: 0x0000E6A3 File Offset: 0x0000C8A3
	public BaseCustomButton.cj czx
	{
		get
		{
			return this.cgn;
		}
		private set
		{
			if (this.cgn != value)
			{
				this.cgn = value;
				UnityEvent<BaseCustomButton.cj> unityEvent = this.cgo;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke(this.cgn);
			}
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002DC RID: 732 RVA: 0x0000E6CC File Offset: 0x0000C8CC
	public RectTransform czy
	{
		get
		{
			RectTransform result;
			if ((result = this.cgp) == null)
			{
				result = (this.cgp = base.GetComponent<RectTransform>());
			}
			return result;
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000E6F2 File Offset: 0x0000C8F2
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		UnityEvent<PointerEventData> onEventClick = this.OnEventClick;
		if (onEventClick != null)
		{
			onEventClick.Invoke(eventData);
		}
		this.czx = BaseCustomButton.cj.cgl;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000E70D File Offset: 0x0000C90D
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		UnityEvent<PointerEventData> onEventStartClick = this.OnEventStartClick;
		if (onEventStartClick != null)
		{
			onEventStartClick.Invoke(eventData);
		}
		this.czx = BaseCustomButton.cj.cgm;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000E728 File Offset: 0x0000C928
	public virtual void OnPointerUp(PointerEventData eventData)
	{
		UnityEvent<PointerEventData> onEventEndClick = this.OnEventEndClick;
		if (onEventEndClick == null)
		{
			return;
		}
		onEventEndClick.Invoke(eventData);
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000E73B File Offset: 0x0000C93B
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		this.czx = BaseCustomButton.cj.cgl;
		UnityEvent onEventStartHover = this.OnEventStartHover;
		if (onEventStartHover == null)
		{
			return;
		}
		onEventStartHover.Invoke();
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000E754 File Offset: 0x0000C954
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		this.czx = BaseCustomButton.cj.cgk;
		UnityEvent onEventEndHover = this.OnEventEndHover;
		if (onEventEndHover == null)
		{
			return;
		}
		onEventEndHover.Invoke();
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000E76D File Offset: 0x0000C96D
	public virtual void OnPointerMove(PointerEventData eventData)
	{
		if (this.czx != BaseCustomButton.cj.cgl)
		{
			return;
		}
		UnityEvent onEventHover = this.OnEventHover;
		if (onEventHover == null)
		{
			return;
		}
		onEventHover.Invoke();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000238E File Offset: 0x0000058E
	public virtual void OnDrop(PointerEventData eventData)
	{
	}

	// Token: 0x040002E3 RID: 739
	[Header("Events")]
	[HideInInspector]
	public UnityEvent<PointerEventData> OnEventClick;

	// Token: 0x040002E4 RID: 740
	[HideInInspector]
	public UnityEvent<PointerEventData> OnEventStartClick;

	// Token: 0x040002E5 RID: 741
	[HideInInspector]
	public UnityEvent<PointerEventData> OnEventEndClick;

	// Token: 0x040002E6 RID: 742
	[HideInInspector]
	public UnityEvent OnEventStartHover;

	// Token: 0x040002E7 RID: 743
	[HideInInspector]
	public UnityEvent OnEventEndHover;

	// Token: 0x040002E8 RID: 744
	[HideInInspector]
	public UnityEvent OnEventHover;

	// Token: 0x040002E9 RID: 745
	private BaseCustomButton.cj cgn;

	// Token: 0x040002EA RID: 746
	protected UnityEvent<BaseCustomButton.cj> cgo;

	// Token: 0x040002EB RID: 747
	private RectTransform cgp;

	// Token: 0x0200007F RID: 127
	public enum cj
	{
		// Token: 0x040002ED RID: 749
		cgk,
		// Token: 0x040002EE RID: 750
		cgl,
		// Token: 0x040002EF RID: 751
		cgm
	}
}
