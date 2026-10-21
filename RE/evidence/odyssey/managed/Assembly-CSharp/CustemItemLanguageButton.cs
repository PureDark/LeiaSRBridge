using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000080 RID: 128
public class CustemItemLanguageButton : BaseCustomButton
{
	// Token: 0x060002E5 RID: 741 RVA: 0x0000E789 File Offset: 0x0000C989
	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		this.rg();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0000E798 File Offset: 0x0000C998
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		this.rj();
		this.rh();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000E7AD File Offset: 0x0000C9AD
	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		if (eventData.button != null)
		{
			return;
		}
		this.ri();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000E7C5 File Offset: 0x0000C9C5
	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		if (eventData.button != null)
		{
			return;
		}
		this.rj();
		if (this.cgq)
		{
			this.rg();
			return;
		}
		this.rh();
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
	private void rg()
	{
		if (this.cgq)
		{
			return;
		}
		this.cgq = true;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.hoverScaleValue, this.hoverDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.popoutDistance, this.hoverDuration, false), this.hoverEase);
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000E858 File Offset: 0x0000CA58
	private void rh()
	{
		if (!this.cgq)
		{
			return;
		}
		this.cgq = false;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.originalScale, this.hoverDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.originalPos.z, this.hoverDuration, false), this.hoverEase);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
	private void ri()
	{
		if (this.cgr)
		{
			return;
		}
		this.cgr = true;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.hoverScaleValue * this.pressScaleValue, this.pressDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.popoutDistance + this.popInDistance, this.pressDuration, false), this.hoverEase);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0000E93C File Offset: 0x0000CB3C
	private void rj()
	{
		if (!this.cgr)
		{
			return;
		}
		this.cgr = false;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.originalScale, this.pressDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.originalPos.z, this.pressDuration, false), this.hoverEase);
	}

	// Token: 0x040002F0 RID: 752
	[SerializeField]
	private RectTransform Target;

	// Token: 0x040002F1 RID: 753
	[Header("Hover Setting")]
	[SerializeField]
	private Vector3 hoverScaleValue;

	// Token: 0x040002F2 RID: 754
	[SerializeField]
	private float popoutDistance;

	// Token: 0x040002F3 RID: 755
	[SerializeField]
	private Vector3 originalScale;

	// Token: 0x040002F4 RID: 756
	[SerializeField]
	private Vector3 originalPos;

	// Token: 0x040002F5 RID: 757
	[SerializeField]
	private float hoverDuration;

	// Token: 0x040002F6 RID: 758
	[SerializeField]
	private Ease hoverEase;

	// Token: 0x040002F7 RID: 759
	[Header("Press Settings")]
	public float pressDuration;

	// Token: 0x040002F8 RID: 760
	public float pressScaleValue = 1f;

	// Token: 0x040002F9 RID: 761
	public float popInDistance;

	// Token: 0x040002FA RID: 762
	private bool cgq;

	// Token: 0x040002FB RID: 763
	private bool cgr;

	// Token: 0x040002FC RID: 764
	private Toggle cgs;
}
