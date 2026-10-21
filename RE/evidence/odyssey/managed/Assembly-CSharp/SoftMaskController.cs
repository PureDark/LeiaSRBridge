using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class SoftMaskController : MonoBehaviour
{
	// Token: 0x0600024F RID: 591 RVA: 0x0000BC7D File Offset: 0x00009E7D
	private void Start()
	{
		this.cea = this.softMaskRect.sizeDelta.x;
		this.ceb = this.softMaskRect.anchoredPosition;
		this.cec = this.scrollView.anchoredPosition;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000BCB8 File Offset: 0x00009EB8
	private void Update()
	{
		if (this.panelRotation.enabled)
		{
			float num = this.panelRotation.panel.transform.localRotation.eulerAngles.y;
			if (num > 180f)
			{
				num -= 360f;
			}
			float num2 = Mathf.Clamp01(Mathf.Abs(num) / this.panelRotation.maxRotation);
			if (this.useTween)
			{
				this.ok(num, num2);
				return;
			}
			this.ol(num2, num);
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000BD38 File Offset: 0x00009F38
	private void ok(float a, float b)
	{
		if (Mathf.Abs(a) >= this.panelRotation.maxRotation / 2f)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.softMaskRect, new Vector2(this.cea + this.sizeIncreaseAmount, this.softMaskRect.sizeDelta.y), this.transitionDuration, false), 9);
			float num = this.sizeIncreaseAmount / 2f * Mathf.Sign(a);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.softMaskRect, this.ceb + new Vector2(num, 0f), this.transitionDuration, false), 9);
			return;
		}
		if (Mathf.Abs(a) < this.panelRotation.maxRotation - 1f && Mathf.Abs(a) >= this.panelRotation.maxRotation / 4f)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.softMaskRect, new Vector2(this.cea + this.sizeIncreaseAmount / 2f, this.softMaskRect.sizeDelta.y), this.transitionDuration, false), 9);
			float num2 = this.sizeIncreaseAmount / 2f * Mathf.Sign(a) / 2f;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.softMaskRect, this.ceb + new Vector2(num2, 0f), this.transitionDuration, false), 9);
			return;
		}
		if (Mathf.Abs(a) > this.panelRotation.minRotation + 1f && Mathf.Abs(a) <= this.panelRotation.minRotation / 4f)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.softMaskRect, new Vector2(this.cea + this.sizeIncreaseAmount / 2f, this.softMaskRect.sizeDelta.y), this.transitionDuration, false), 9);
			float num3 = this.sizeIncreaseAmount / 2f * Mathf.Sign(a) / 2f;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.softMaskRect, this.ceb + new Vector2(num3, 0f), this.transitionDuration, false), 9);
			return;
		}
		if (Mathf.Abs(a) <= this.panelRotation.minRotation + 1f)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.softMaskRect, new Vector2(this.cea + this.sizeIncreaseAmount, this.softMaskRect.sizeDelta.y), this.transitionDuration, false), 9);
			float num4 = this.sizeIncreaseAmount / 2f * Mathf.Sign(a) * -1f;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.softMaskRect, this.ceb + new Vector2(num4, 0f), this.transitionDuration, false), 9);
			return;
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.softMaskRect, new Vector2(this.cea, this.softMaskRect.sizeDelta.y), this.transitionDuration, false), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.softMaskRect, this.ceb, this.transitionDuration, false), 9);
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000C064 File Offset: 0x0000A264
	private void ol(float a, float b)
	{
		float num = Mathf.Lerp(this.cea, this.cea + this.sizeIncreaseAmount, a);
		this.softMaskRect.sizeDelta = new Vector2(num, this.softMaskRect.sizeDelta.y);
		float num2 = Mathf.Lerp(0f, this.sizeIncreaseAmount / 2f, a) * Mathf.Sign(b);
		this.softMaskRect.anchoredPosition = this.ceb + new Vector2(num2, 0f);
	}

	// Token: 0x0400026C RID: 620
	public RectTransform softMaskRect;

	// Token: 0x0400026D RID: 621
	public PanelRotation panelRotation;

	// Token: 0x0400026E RID: 622
	public RectTransform scrollView;

	// Token: 0x0400026F RID: 623
	public float sizeIncreaseAmount = 500f;

	// Token: 0x04000270 RID: 624
	public float transitionDuration = 0.3f;

	// Token: 0x04000271 RID: 625
	public bool useTween = true;

	// Token: 0x04000272 RID: 626
	private float cea;

	// Token: 0x04000273 RID: 627
	private Vector2 ceb;

	// Token: 0x04000274 RID: 628
	private Vector2 cec;

	// Token: 0x04000275 RID: 629
	private float ced;
}
