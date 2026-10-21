using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000084 RID: 132
public class EffectCustomButton : BaseCustomButton
{
	// Token: 0x06000315 RID: 789 RVA: 0x0001073A File Offset: 0x0000E93A
	private void sj(BaseCustomButton.cj a)
	{
		if (a == BaseCustomButton.cj.cgl)
		{
			this.sk();
			return;
		}
		if (a != BaseCustomButton.cj.cgm)
		{
			this.sl();
			return;
		}
		this.sm();
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001075A File Offset: 0x0000E95A
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != null)
		{
			return;
		}
		base.OnPointerDown(eventData);
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001076C File Offset: 0x0000E96C
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button != null)
		{
			return;
		}
		base.OnPointerClick(eventData);
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00010780 File Offset: 0x0000E980
	private void sk()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.hoverScaleValue, this.hoverDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.popoutDistance, this.hoverDuration, false), this.hoverEase);
	}

	// Token: 0x06000319 RID: 793 RVA: 0x000107D4 File Offset: 0x0000E9D4
	private void sl()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.originalScale, this.hoverDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.originalPos.z, this.hoverDuration, false), this.hoverEase);
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00010830 File Offset: 0x0000EA30
	private void sm()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.hoverScaleValue * this.pressScaleValue, this.pressDuration), this.hoverEase);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.Target, this.popoutDistance + this.popInDistance, this.pressDuration, false), this.hoverEase);
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00010898 File Offset: 0x0000EA98
	private void OnEnable()
	{
		UnityEvent<BaseCustomButton.cj> cgo = this.cgo;
		if (cgo != null)
		{
			cgo.RemoveAllListeners();
		}
		if (this.cgo == null)
		{
			this.cgo = new UnityEvent<BaseCustomButton.cj>();
		}
		this.cgo.AddListener(new UnityAction<BaseCustomButton.cj>(this.sj));
		this.sl();
	}

	// Token: 0x04000346 RID: 838
	[SerializeField]
	private RectTransform Target;

	// Token: 0x04000347 RID: 839
	[Header("Hover Setting")]
	[SerializeField]
	private Vector3 hoverScaleValue;

	// Token: 0x04000348 RID: 840
	[SerializeField]
	private float popoutDistance;

	// Token: 0x04000349 RID: 841
	[SerializeField]
	private Vector3 originalScale;

	// Token: 0x0400034A RID: 842
	[SerializeField]
	private Vector3 originalPos;

	// Token: 0x0400034B RID: 843
	[SerializeField]
	private float hoverDuration;

	// Token: 0x0400034C RID: 844
	[SerializeField]
	private Ease hoverEase;

	// Token: 0x0400034D RID: 845
	[Header("Press Settings")]
	public float pressDuration;

	// Token: 0x0400034E RID: 846
	public float pressScaleValue = 1f;

	// Token: 0x0400034F RID: 847
	public float popInDistance;
}
