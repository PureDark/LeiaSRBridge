using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000079 RID: 121
public class RayReceiver : MonoBehaviour
{
	// Token: 0x060002AC RID: 684 RVA: 0x0000D710 File Offset: 0x0000B910
	private void Start()
	{
		Canvas componentInParent = base.GetComponentInParent<Canvas>();
		if (componentInParent != null)
		{
			this.cfn = componentInParent.GetComponent<GraphicRaycaster>();
		}
		this.cfo = Object.FindAnyObjectByType<EventSystem>();
		this.cfp = new PointerEventData(this.cfo);
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000D758 File Offset: 0x0000B958
	private void Update()
	{
		this.cfp.position = Input.mousePosition;
		this.cfq.Clear();
		this.cfn.Raycast(this.cfp, this.cfq);
		this.isHit = false;
		this.isFirstHit = false;
		if (this.cfq.Count > 0)
		{
			if (this.cfq[0].gameObject == base.gameObject)
			{
				UnityEvent onFirstHitByRay = this.OnFirstHitByRay;
				if (onFirstHitByRay != null)
				{
					onFirstHitByRay.Invoke();
				}
				this.isHit = true;
				this.isFirstHit = true;
			}
			foreach (RaycastResult raycastResult in this.cfq)
			{
				if (raycastResult.gameObject == base.gameObject)
				{
					UnityEvent onHitByRay = this.OnHitByRay;
					if (onHitByRay != null)
					{
						onHitByRay.Invoke();
					}
					this.isHit = true;
					break;
				}
			}
			if (!this.isFirstHit)
			{
				UnityEvent onNotFirstHitByRay = this.OnNotFirstHitByRay;
				if (onNotFirstHitByRay != null)
				{
					onNotFirstHitByRay.Invoke();
				}
			}
		}
		if (!this.isHit)
		{
			UnityEvent onNotHitByRay = this.OnNotHitByRay;
			if (onNotHitByRay == null)
			{
				return;
			}
			onNotHitByRay.Invoke();
		}
	}

	// Token: 0x040002C2 RID: 706
	[HideInInspector]
	public UnityEvent OnFirstHitByRay;

	// Token: 0x040002C3 RID: 707
	[HideInInspector]
	public UnityEvent OnNotFirstHitByRay;

	// Token: 0x040002C4 RID: 708
	[HideInInspector]
	public UnityEvent OnHitByRay;

	// Token: 0x040002C5 RID: 709
	[HideInInspector]
	public UnityEvent OnNotHitByRay;

	// Token: 0x040002C6 RID: 710
	public bool isFirstHit;

	// Token: 0x040002C7 RID: 711
	public bool isHit;

	// Token: 0x040002C8 RID: 712
	private GraphicRaycaster cfn;

	// Token: 0x040002C9 RID: 713
	private EventSystem cfo;

	// Token: 0x040002CA RID: 714
	private PointerEventData cfp;

	// Token: 0x040002CB RID: 715
	private List<RaycastResult> cfq = new List<RaycastResult>();
}
