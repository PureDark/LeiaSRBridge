using System;
using System.Collections;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000081 RID: 129
public class CustomButton : BaseCustomButton
{
	// Token: 0x060002EE RID: 750 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
	private void Start()
	{
		this.ro();
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
	private void ro()
	{
		if (this.targetTransform0 == null)
		{
			this.targetTransform0 = base.transform;
		}
		if (this.useLayerEffect)
		{
			if (this.targetTransform1)
			{
				this.targetTransform1.gameObject.SetActive(true);
			}
			if (this.targetTransform2)
			{
				this.targetTransform2.gameObject.SetActive(true);
			}
			if (this.targetTransform3 != null && this.targetTransform3.tag != <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ug())
			{
				this.targetTransform3.gameObject.SetActive(true);
			}
		}
		else
		{
			if (this.allInOneSprite != null)
			{
				Image component = this.targetTransform0.GetComponent<Image>();
				RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
				if (component != null)
				{
					component.sprite = this.allInOneSprite;
				}
				else if (component2 != null)
				{
					component2.texture = this.allInOneSprite.texture;
				}
			}
			if (this.targetTransform1 != null)
			{
				this.targetTransform1.gameObject.SetActive(false);
			}
			if (this.targetTransform2 != null)
			{
				this.targetTransform2.gameObject.SetActive(false);
			}
			if (this.targetTransform3 != null)
			{
				this.targetTransform3.gameObject.SetActive(false);
			}
		}
		if (this.rayReceiver == null)
		{
			this.rayReceiver = base.GetComponent<RayReceiver>();
		}
		if (this.noMaskImages.Count != 0 && this.rayReceiver.GetComponent<Image>() != null)
		{
			this.noMaskImages.Add(this.rayReceiver.GetComponent<Image>());
		}
		if (this.hoverEffectObj != null)
		{
			this.hoverEffectObj.SetActive(false);
		}
		if (this.toolTipCanvasGroup != null)
		{
			this.toolTipCanvasGroup.alpha = 0f;
			this.toolTipCanvasGroup.gameObject.SetActive(false);
		}
		if (this.targetTransform0 != null)
		{
			this.cha = this.targetTransform0.localScale;
			this.chf = this.targetTransform0.localPosition;
		}
		if (this.targetTransform1 != null)
		{
			this.chb = this.targetTransform1.localScale;
			this.chg = this.targetTransform1.localPosition;
		}
		if (this.targetTransform2 != null)
		{
			this.chc = this.targetTransform2.localScale;
			this.chh = this.targetTransform2.localPosition;
		}
		if (this.targetTransform3 != null)
		{
			this.chd = this.targetTransform3.localScale;
			this.chi = this.targetTransform3.localPosition;
		}
		if (this.targetTransform4 != null)
		{
			this.che = this.targetTransform4.localScale;
			this.chj = this.targetTransform4.localPosition;
		}
		if (base.gameObject.tag.Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uh()))
		{
			this.chp = base.gameObject.GetComponentInParent<ScrollRect>();
			if (this.chp != null)
			{
				this.chp.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.si));
				this.chp.onValueChanged.AddListener(new UnityAction<Vector2>(this.si));
			}
		}
		this.chq = base.GetComponent<CustomShade>();
		this.sc();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000ED2C File Offset: 0x0000CF2C
	private void Update()
	{
		if (!this.isEnabled)
		{
			return;
		}
		this.rw();
		this.rp();
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000ED44 File Offset: 0x0000CF44
	private void rp()
	{
		if (this.useToolTip && this.isHovering)
		{
			this.chk += Time.unscaledDeltaTime;
			if (this.chk >= this.toolTipAppearanceTime && this.toolTipCanvasGroup != null)
			{
				this.sd();
				return;
			}
		}
		else if (!this.isHovering && this.toolTipCanvasGroup != null)
		{
			this.se();
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
	private void rq(bool a)
	{
		if (this.useHoverEffect)
		{
			if (a && this.viewStatus == CustomButton.ck.cgt && !CustomButton.cho)
			{
				this.rx();
				return;
			}
			if (this.isHovering && !CustomButton.cho && this.viewStatus == CustomButton.ck.cgu)
			{
				if (!a || this.chp.horizontal)
				{
					this.ry(this.viewStatus);
					return;
				}
			}
			else
			{
				this.ry(this.viewStatus);
			}
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0000EE23 File Offset: 0x0000D023
	private void rr(bool a)
	{
		if (this.usePressEffect)
		{
			this.rt(a);
			this.rs(a);
		}
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000EE3C File Offset: 0x0000D03C
	private void rs(bool a)
	{
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			float num = mousePosition.x - this.chl.x;
			float num2 = mousePosition.y - this.chl.y;
			this.sa();
			if (a && Mathf.Abs(num) < this.chm && Mathf.Abs(num2) < this.chn)
			{
				this.OnClick();
			}
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
	private void rt(bool a)
	{
		if (Input.GetMouseButtonDown(0) && a)
		{
			this.chl = Input.mousePosition;
			this.rz();
		}
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
	private void ru()
	{
		CustomButton.ck a = CustomButton.ck.cgt;
		if (this.chp != null && !this.chp.name.Contains(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ui()))
		{
			a = this.sg(this.chp);
		}
		this.rv(a);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000EF0C File Offset: 0x0000D10C
	public void rv(CustomButton.ck a)
	{
		if (a == this.viewStatus)
		{
			return;
		}
		Transform transform = base.GetComponent<Transform>().Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uj());
		if (transform != null)
		{
			Image component = transform.GetComponent<Image>();
			if (component != null)
			{
				component.raycastTarget = (a != CustomButton.ck.cgv);
			}
			RayReceiver component2 = transform.GetComponent<RayReceiver>();
			if (component2 != null)
			{
				component2.enabled = (a != CustomButton.ck.cgv);
			}
		}
		this.viewStatus = a;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000EF80 File Offset: 0x0000D180
	private bool rw()
	{
		if (this.useChildRay)
		{
			if (this.isDetectedWhenFirstHitOnly)
			{
				return (this.rayReceiver != null && this.rayReceiver.isFirstHit) || (this.targetTransform0 != null && this.targetTransform0.GetComponent<RayReceiver>() != null && this.targetTransform0.GetComponent<RayReceiver>().isFirstHit) || (this.targetTransform1 != null && this.targetTransform1.GetComponent<RayReceiver>() != null && this.targetTransform1.GetComponent<RayReceiver>().isFirstHit) || (this.targetTransform2 != null && this.targetTransform2.GetComponent<RayReceiver>() != null && this.targetTransform2.GetComponent<RayReceiver>().isFirstHit) || (this.targetTransform3 != null && this.targetTransform3.GetComponent<RayReceiver>() != null && this.targetTransform3.GetComponent<RayReceiver>().isFirstHit) || (this.targetTransform4 != null && this.targetTransform4.GetComponent<RayReceiver>() != null && this.targetTransform4.GetComponent<RayReceiver>().isFirstHit);
			}
			return (this.rayReceiver != null && this.rayReceiver.isHit) || (this.targetTransform0 != null && this.targetTransform0.GetComponent<RayReceiver>() != null && this.targetTransform0.GetComponent<RayReceiver>().isHit) || (this.targetTransform1 != null && this.targetTransform1.GetComponent<RayReceiver>() != null && this.targetTransform1.GetComponent<RayReceiver>().isHit) || (this.targetTransform2 != null && this.targetTransform2.GetComponent<RayReceiver>() != null && this.targetTransform2.GetComponent<RayReceiver>().isHit) || (this.targetTransform3 != null && this.targetTransform3.GetComponent<RayReceiver>() != null && this.targetTransform3.GetComponent<RayReceiver>().isHit) || (this.targetTransform4 != null && this.targetTransform4.GetComponent<RayReceiver>() != null && this.targetTransform4.GetComponent<RayReceiver>().isHit);
		}
		else
		{
			if (!this.isDetectedWhenFirstHitOnly)
			{
				return this.rayReceiver.isHit;
			}
			return this.rayReceiver.isFirstHit;
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0000F210 File Offset: 0x0000D410
	public void rx()
	{
		if (!this.useHoverEffect || !this.isEnabled)
		{
			return;
		}
		if (this.isPressing)
		{
			return;
		}
		if (!this.isHovering)
		{
			base.StartCoroutine(this.sf());
		}
		UnityEvent onEventStartHover = this.OnEventStartHover;
		if (onEventStartHover != null)
		{
			onEventStartHover.Invoke();
		}
		this.isHovering = true;
		if (this.chq)
		{
			this.chq.nm();
		}
		if (this.useHierarchySetting)
		{
			base.transform.SetAsLastSibling();
		}
		if (this.img_hov != null)
		{
			Image component = this.targetTransform0.GetComponent<Image>();
			RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
			if (component != null)
			{
				component.sprite = this.img_hov;
			}
			else if (component2 != null)
			{
				component2.texture = this.img_hov.texture;
			}
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform0, this.hoverScaleValue0, this.hoverDuration), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform0, this.popOutDistance0, this.hoverDuration, false), 9);
		if (this.targetTransform1 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform1, this.hoverScaleValue1, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform1, this.popOutDistance1, this.hoverDuration, false), 9);
		}
		if (this.targetTransform2 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform2, this.hoverScaleValue2, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform2, this.popOutDistance2, this.hoverDuration, false), 9);
		}
		if (this.targetTransform3 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform3, this.hoverScaleValue3, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform3, this.popOutDistance3, this.hoverDuration, false), 9);
		}
		if (this.targetTransform4 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform4, this.hoverScaleValue4, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform4, this.popOutDistance4, this.hoverDuration, false), 9);
		}
		if (this.noMaskImages != null)
		{
			for (int i = 0; i < this.noMaskImages.Count; i++)
			{
				if (this.noMaskImages[i] != null)
				{
					this.noMaskImages[i].maskable = this.isMaskableWhenHover;
					bool activeInHierarchy = this.noMaskImages[i].gameObject.activeInHierarchy;
					this.noMaskImages[i].gameObject.SetActive(false);
					this.noMaskImages[i].gameObject.SetActive(activeInHierarchy);
				}
			}
		}
		if (this.noMaskTexts != null)
		{
			for (int j = 0; j < this.noMaskTexts.Count; j++)
			{
				this.noMaskTexts[j].maskable = this.isMaskableWhenHover;
			}
		}
		if (this.fadeInCanvasGroupsWhenHover != null)
		{
			for (int k = 0; k < this.fadeInCanvasGroupsWhenHover.Count; k++)
			{
				DOTweenModuleUI.DOFade(this.fadeInCanvasGroupsWhenHover[k], 1f, this.hoverDuration);
			}
		}
		if (this.fadeOutCanvasGroupsWhenHover != null)
		{
			for (int l = 0; l < this.fadeOutCanvasGroupsWhenHover.Count; l++)
			{
				DOTweenModuleUI.DOFade(this.fadeOutCanvasGroupsWhenHover[l], 0f, this.hoverDuration);
			}
		}
		if (this.disactiveObjectsWhenHover != null)
		{
			for (int m = 0; m < this.disactiveObjectsWhenHover.Count; m++)
			{
				this.disactiveObjectsWhenHover[m].SetActive(false);
			}
		}
		if (this.hoverEffectObj != null)
		{
			this.hoverEffectObj.SetActive(true);
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
	public void ry(CustomButton.ck a)
	{
		if (!this.useHoverEffect || !this.isEnabled)
		{
			return;
		}
		if (this.isPressing && a == CustomButton.ck.cgt)
		{
			return;
		}
		if (this.isHovering)
		{
			base.StopCoroutine(this.sf());
			this.chk = 0f;
		}
		UnityEvent onEventEndHover = this.OnEventEndHover;
		if (onEventEndHover != null)
		{
			onEventEndHover.Invoke();
		}
		this.isHovering = false;
		if (this.chq)
		{
			this.chq.nn();
		}
		if (this.targetTransform0 != null)
		{
			if (this.img_nor != null)
			{
				Image component = this.targetTransform0.GetComponent<Image>();
				RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
				if (component != null)
				{
					component.sprite = this.img_nor;
				}
				else if (component2 != null)
				{
					component2.texture = this.img_nor.texture;
				}
			}
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform0, this.cha, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform0, this.chf.z, this.hoverDuration, false), 9);
		}
		if (this.targetTransform1 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform1, this.chb, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform1, this.chg.z, this.hoverDuration, false), 9);
		}
		if (this.targetTransform2 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform2, this.chc, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform2, this.chh.z, this.hoverDuration, false), 9);
		}
		if (this.targetTransform3 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform3, this.chd, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform3, this.chi.z, this.hoverDuration, false), 9);
		}
		if (this.targetTransform4 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform4, this.che, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform4, this.chj.z, this.hoverDuration, false), 9);
		}
		if (this.noMaskImages != null)
		{
			for (int i = 0; i < this.noMaskImages.Count; i++)
			{
				if (this.noMaskImages[i] != null)
				{
					this.noMaskImages[i].maskable = true;
					bool activeInHierarchy = this.noMaskImages[i].gameObject.activeInHierarchy;
					this.noMaskImages[i].gameObject.SetActive(false);
					this.noMaskImages[i].gameObject.SetActive(activeInHierarchy);
				}
			}
		}
		if (this.noMaskTexts != null)
		{
			for (int j = 0; j < this.noMaskTexts.Count; j++)
			{
				this.noMaskTexts[j].maskable = true;
			}
		}
		if (this.fadeInCanvasGroupsWhenHover != null)
		{
			for (int k = 0; k < this.fadeInCanvasGroupsWhenHover.Count; k++)
			{
				DOTweenModuleUI.DOFade(this.fadeInCanvasGroupsWhenHover[k], 0f, this.hoverDuration);
			}
		}
		if (this.fadeOutCanvasGroupsWhenHover != null)
		{
			for (int l = 0; l < this.fadeOutCanvasGroupsWhenHover.Count; l++)
			{
				DOTweenModuleUI.DOFade(this.fadeOutCanvasGroupsWhenHover[l], 1f, this.hoverDuration);
			}
		}
		if (this.disactiveObjectsWhenHover != null)
		{
			for (int m = 0; m < this.disactiveObjectsWhenHover.Count; m++)
			{
				this.disactiveObjectsWhenHover[m].SetActive(true);
			}
		}
		if (this.hoverEffectObj != null)
		{
			this.hoverEffectObj.SetActive(false);
		}
		if (this.toolTipCanvasGroup != null)
		{
			this.se();
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000FA0C File Offset: 0x0000DC0C
	private void rz()
	{
		if (!this.usePressEffect || !this.isEnabled)
		{
			return;
		}
		if (this.isPressing)
		{
			return;
		}
		this.isPressing = true;
		CustomButton.cho = true;
		this.isClicked = false;
		if (this.viewStatus != CustomButton.ck.cgt)
		{
			return;
		}
		if (this.img_sel != null)
		{
			Image component = this.targetTransform0.GetComponent<Image>();
			RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
			if (component != null)
			{
				component.sprite = this.img_sel;
			}
			else if (component2 != null)
			{
				component2.texture = this.img_sel.texture;
			}
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform0, this.hoverScaleValue0 * this.pressScaleValue, this.pressDuration), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform0, this.popOutDistance0 + this.popInDistance, this.pressDuration, false), 9);
		if (this.targetTransform1 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform1, this.hoverScaleValue1, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform1, this.popOutDistance1, this.hoverDuration, false), 9);
		}
		if (this.targetTransform2 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform2, this.hoverScaleValue2, this.hoverDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform2, this.popOutDistance2, this.hoverDuration, false), 9);
		}
		if (this.targetTransform3 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform3, this.hoverScaleValue3, this.pressDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform3, this.popOutDistance3, this.pressDuration, false), 9);
		}
		if (this.targetTransform4 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform4, this.hoverScaleValue4, this.pressDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform4, this.popOutDistance4, this.pressDuration, false), 9);
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000FC28 File Offset: 0x0000DE28
	private void sa()
	{
		if (!this.usePressEffect || !this.isEnabled)
		{
			return;
		}
		if (!this.isPressing)
		{
			return;
		}
		this.isPressing = false;
		CustomButton.cho = false;
		if (this.img_nor != null)
		{
			Image component = this.targetTransform0.GetComponent<Image>();
			RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
			if (component != null)
			{
				component.sprite = this.img_nor;
			}
			else if (component2 != null)
			{
				component2.texture = this.img_nor.texture;
			}
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform0, this.cha, this.pressDuration), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform0, this.chf.z, this.pressDuration, false), 9);
		if (this.targetTransform1 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform1, this.chb, this.pressDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform1, this.chg.z, this.pressDuration, false), 9);
		}
		if (this.targetTransform2 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform2, this.chc, this.pressDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform2, this.chh.z, this.pressDuration, false), 9);
		}
		if (this.targetTransform3 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform3, this.chd, this.pressDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform3, this.chi.z, this.pressDuration, false), 9);
		}
		if (this.targetTransform4 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform4, this.che, this.pressDuration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform4, this.chj.z, this.pressDuration, false), 9);
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000FE40 File Offset: 0x0000E040
	private void OnDisable()
	{
		if (this.isPressing)
		{
			CustomButton.cho = false;
			this.isPressing = false;
			this.sa();
		}
		if (this.isHovering)
		{
			this.isHovering = false;
			UnityEvent onEventEndHover = this.OnEventEndHover;
			if (onEventEndHover != null)
			{
				onEventEndHover.Invoke();
			}
		}
		if (this.hoverEffectObj != null)
		{
			this.hoverEffectObj.SetActive(false);
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
	private void OnClick()
	{
		bd.id(CustomButton.cgz, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uk() + base.tag, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ul());
		if (!this.isEnabled)
		{
			return;
		}
		if (this.isClicked)
		{
			return;
		}
		if (this.viewStatus != CustomButton.ck.cgt)
		{
			return;
		}
		if (CustomButton.clickBlocked)
		{
			return;
		}
		this.isClicked = true;
		this.onClick.Invoke();
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0000FF08 File Offset: 0x0000E108
	public void sb()
	{
		this.isEnabled = false;
		if (this.img_dis != null)
		{
			Image component = this.targetTransform0.GetComponent<Image>();
			RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
			if (component != null)
			{
				component.sprite = this.img_dis;
			}
			else if (component2 != null)
			{
				component2.texture = this.img_dis.texture;
			}
		}
		if (this.targetTransform0 != null)
		{
			ShortcutExtensions.DOKill(this.targetTransform0, false);
			this.targetTransform0.localScale = this.cha;
			this.targetTransform0.localPosition = this.chf;
		}
		if (this.targetTransform1 != null)
		{
			ShortcutExtensions.DOKill(this.targetTransform1, false);
			this.targetTransform1.localScale = this.chb;
			this.targetTransform1.localPosition = this.chg;
		}
		if (this.targetTransform2 != null)
		{
			ShortcutExtensions.DOKill(this.targetTransform2, false);
			this.targetTransform2.localScale = this.chc;
			this.targetTransform2.localPosition = this.chh;
		}
		if (this.targetTransform3 != null)
		{
			ShortcutExtensions.DOKill(this.targetTransform3, false);
			this.targetTransform3.localScale = this.chd;
			this.targetTransform3.localPosition = this.chi;
		}
		if (this.targetTransform4 != null)
		{
			ShortcutExtensions.DOKill(this.targetTransform4, false);
			this.targetTransform4.localScale = this.che;
			this.targetTransform4.localPosition = this.chj;
		}
		this.ry(CustomButton.ck.cgt);
		this.sa();
	}

	// Token: 0x06000300 RID: 768 RVA: 0x000100B4 File Offset: 0x0000E2B4
	public void sc()
	{
		if (this.targetTransform0 == null)
		{
			return;
		}
		Image component = this.targetTransform0.GetComponent<Image>();
		RawImage component2 = this.targetTransform0.GetComponent<RawImage>();
		if (this.isEnabled)
		{
			if (this.img_nor != null)
			{
				if (component != null)
				{
					component.sprite = this.img_nor;
					return;
				}
				if (component2 != null)
				{
					component2.texture = this.img_nor.texture;
					return;
				}
			}
		}
		else
		{
			ShortcutExtensions.DOKill(this.targetTransform0, false);
			this.targetTransform0.localScale = this.cha;
			this.targetTransform0.localPosition = this.chf;
			if (this.targetTransform1 != null)
			{
				ShortcutExtensions.DOKill(this.targetTransform1, false);
				this.targetTransform1.localScale = this.chb;
				this.targetTransform1.localPosition = this.chg;
			}
			if (this.targetTransform2 != null)
			{
				ShortcutExtensions.DOKill(this.targetTransform2, false);
				this.targetTransform2.localScale = this.chc;
				this.targetTransform2.localPosition = this.chh;
			}
			if (this.targetTransform3 != null)
			{
				ShortcutExtensions.DOKill(this.targetTransform3, false);
				this.targetTransform3.localScale = this.chd;
				this.targetTransform3.localPosition = this.chi;
			}
			if (this.targetTransform4 != null)
			{
				ShortcutExtensions.DOKill(this.targetTransform4, false);
				this.targetTransform4.localScale = this.che;
				this.targetTransform4.localPosition = this.chj;
			}
			if (this.img_dis != null)
			{
				if (component != null)
				{
					component.sprite = this.img_dis;
					return;
				}
				if (component2 != null)
				{
					component2.texture = this.img_dis.texture;
				}
			}
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00010296 File Offset: 0x0000E496
	private void sd()
	{
		this.toolTipCanvasGroup.gameObject.SetActive(true);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.toolTipCanvasGroup, 1f, 0.3f), 9);
	}

	// Token: 0x06000302 RID: 770 RVA: 0x000102C6 File Offset: 0x0000E4C6
	private void se()
	{
		this.toolTipCanvasGroup.gameObject.SetActive(false);
	}

	// Token: 0x06000303 RID: 771 RVA: 0x000102D9 File Offset: 0x0000E4D9
	private IEnumerator sf()
	{
		this.chk = 0f;
		while (this.isHovering && this.chk < this.toolTipAppearanceTime)
		{
			this.chk += Time.unscaledDeltaTime;
			yield return null;
		}
		if (this.chk >= this.toolTipAppearanceTime && this.toolTipCanvasGroup != null)
		{
			this.sd();
		}
		yield break;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x000102E8 File Offset: 0x0000E4E8
	private CustomButton.ck sg(ScrollRect a)
	{
		RectTransform component = a.GetComponent<RectTransform>();
		ThumbnailViewContent component2 = a.content.GetComponent<ThumbnailViewContent>();
		ViewContent componentInParent = a.GetComponentInParent<ViewContent>();
		if (component2 != null && component2.isContentOutOfView)
		{
			return CustomButton.ck.cgv;
		}
		RectTransform component3 = base.gameObject.GetComponent<RectTransform>();
		if (componentInParent != null && !componentInParent.vf(component3))
		{
			return CustomButton.ck.cgv;
		}
		Vector3[] array = new Vector3[4];
		Vector3[] array2 = new Vector3[4];
		component3.GetWorldCorners(array);
		component.GetWorldCorners(array2);
		bool flag = array[0].x < array2[0].x;
		bool flag2 = array[2].x > array2[2].x;
		bool flag3 = array[1].y > array2[1].y;
		bool flag4 = array[3].y < array2[3].y;
		bool flag5 = array[2].x < array2[0].x;
		bool flag6 = array[0].x > array2[2].x;
		bool flag7 = array[3].y > array2[1].y;
		bool flag8 = array[1].y < array2[3].y;
		if (flag5 || flag6 || flag7 || flag8)
		{
			return CustomButton.ck.cgv;
		}
		if (!flag && !flag2 && !flag3 && !flag4)
		{
			return CustomButton.ck.cgt;
		}
		return CustomButton.ck.cgu;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0001046D File Offset: 0x0000E66D
	public override void OnPointerEnter(PointerEventData eventData)
	{
		this.ru();
		base.OnPointerEnter(eventData);
		if (!this.useHoverEffect)
		{
			return;
		}
		if (this.viewStatus == CustomButton.ck.cgt && !CustomButton.cho)
		{
			this.rx();
		}
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0001049A File Offset: 0x0000E69A
	public override void OnPointerExit(PointerEventData eventData)
	{
		this.ru();
		base.OnPointerExit(eventData);
		if (!this.useHoverEffect)
		{
			return;
		}
		this.sa();
		this.ry(this.viewStatus);
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000104C4 File Offset: 0x0000E6C4
	public override void OnPointerMove(PointerEventData eventData)
	{
		this.ru();
		base.OnPointerMove(eventData);
		if (!this.useHoverEffect)
		{
			return;
		}
		this.sh();
	}

	// Token: 0x06000308 RID: 776 RVA: 0x000104E4 File Offset: 0x0000E6E4
	private void sh()
	{
		if (!this.isPressing && this.viewStatus == CustomButton.ck.cgt && !CustomButton.cho)
		{
			this.rx();
			return;
		}
		if (this.isHovering && !CustomButton.cho && this.viewStatus == CustomButton.ck.cgu && this.chp.horizontal)
		{
			this.ry(this.viewStatus);
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00010540 File Offset: 0x0000E740
	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		if (eventData.button != null || !this.usePressEffect)
		{
			return;
		}
		this.chl = Input.mousePosition;
		this.rz();
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0001056B File Offset: 0x0000E76B
	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		if (!this.usePressEffect || eventData.button != null)
		{
			return;
		}
		this.sa();
		this.sh();
		this.OnClick();
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00010597 File Offset: 0x0000E797
	public override void OnDrop(PointerEventData eventData)
	{
		base.OnDrop(eventData);
		if (!this.usePressEffect)
		{
			return;
		}
		this.sa();
		this.sh();
	}

	// Token: 0x0600030C RID: 780 RVA: 0x000105B5 File Offset: 0x0000E7B5
	private void si(Vector2 a)
	{
		this.ru();
		if (this.isHovering && this.viewStatus != CustomButton.ck.cgt)
		{
			this.ry(this.viewStatus);
		}
	}

	// Token: 0x040002FD RID: 765
	private static string cgz = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.um();

	// Token: 0x040002FE RID: 766
	[HideInInspector]
	public bool isHovering;

	// Token: 0x040002FF RID: 767
	[HideInInspector]
	public bool isPressing;

	// Token: 0x04000300 RID: 768
	[HideInInspector]
	public bool isClicked;

	// Token: 0x04000301 RID: 769
	private Vector3 cha;

	// Token: 0x04000302 RID: 770
	private Vector3 chb;

	// Token: 0x04000303 RID: 771
	private Vector3 chc;

	// Token: 0x04000304 RID: 772
	private Vector3 chd;

	// Token: 0x04000305 RID: 773
	private Vector3 che;

	// Token: 0x04000306 RID: 774
	private Vector3 chf;

	// Token: 0x04000307 RID: 775
	private Vector3 chg;

	// Token: 0x04000308 RID: 776
	private Vector3 chh;

	// Token: 0x04000309 RID: 777
	private Vector3 chi;

	// Token: 0x0400030A RID: 778
	private Vector3 chj;

	// Token: 0x0400030B RID: 779
	public RayReceiver rayReceiver;

	// Token: 0x0400030C RID: 780
	public Transform targetTransform0;

	// Token: 0x0400030D RID: 781
	public Transform targetTransform1;

	// Token: 0x0400030E RID: 782
	public Transform targetTransform2;

	// Token: 0x0400030F RID: 783
	public Transform targetTransform3;

	// Token: 0x04000310 RID: 784
	public Transform targetTransform4;

	// Token: 0x04000311 RID: 785
	public bool isDetectedWhenFirstHitOnly;

	// Token: 0x04000312 RID: 786
	public bool useChildRay;

	// Token: 0x04000313 RID: 787
	public bool isEnabled = true;

	// Token: 0x04000314 RID: 788
	[Header("Sprites")]
	public Sprite img_nor;

	// Token: 0x04000315 RID: 789
	public Sprite img_hov;

	// Token: 0x04000316 RID: 790
	public Sprite img_sel;

	// Token: 0x04000317 RID: 791
	public Sprite img_dis;

	// Token: 0x04000318 RID: 792
	[Header("Hover Settings")]
	public bool useHoverEffect = true;

	// Token: 0x04000319 RID: 793
	public bool useLayerEffect;

	// Token: 0x0400031A RID: 794
	public bool useHierarchySetting = true;

	// Token: 0x0400031B RID: 795
	public Sprite allInOneSprite;

	// Token: 0x0400031C RID: 796
	public float hoverDuration;

	// Token: 0x0400031D RID: 797
	public float hoverScaleValue0 = 1f;

	// Token: 0x0400031E RID: 798
	public float hoverScaleValue1 = 1f;

	// Token: 0x0400031F RID: 799
	public float hoverScaleValue2 = 1f;

	// Token: 0x04000320 RID: 800
	public float hoverScaleValue3 = 1f;

	// Token: 0x04000321 RID: 801
	public float hoverScaleValue4 = 1f;

	// Token: 0x04000322 RID: 802
	public float popOutDistance0;

	// Token: 0x04000323 RID: 803
	public float popOutDistance1;

	// Token: 0x04000324 RID: 804
	public float popOutDistance2;

	// Token: 0x04000325 RID: 805
	public float popOutDistance3;

	// Token: 0x04000326 RID: 806
	public float popOutDistance4;

	// Token: 0x04000327 RID: 807
	public GameObject hoverEffectObj;

	// Token: 0x04000328 RID: 808
	public bool isMaskableWhenHover;

	// Token: 0x04000329 RID: 809
	public List<Image> noMaskImages;

	// Token: 0x0400032A RID: 810
	public List<Text> noMaskTexts;

	// Token: 0x0400032B RID: 811
	public List<CanvasGroup> fadeInCanvasGroupsWhenHover;

	// Token: 0x0400032C RID: 812
	public List<CanvasGroup> fadeOutCanvasGroupsWhenHover;

	// Token: 0x0400032D RID: 813
	public List<GameObject> disactiveObjectsWhenHover;

	// Token: 0x0400032E RID: 814
	[Header("Press Settings")]
	public bool usePressEffect = true;

	// Token: 0x0400032F RID: 815
	public float pressDuration;

	// Token: 0x04000330 RID: 816
	public float pressScaleValue = 1f;

	// Token: 0x04000331 RID: 817
	public float popInDistance;

	// Token: 0x04000332 RID: 818
	[Header("Tooltip Settings")]
	public bool useToolTip;

	// Token: 0x04000333 RID: 819
	public float toolTipAppearanceTime = 1f;

	// Token: 0x04000334 RID: 820
	public CanvasGroup toolTipCanvasGroup;

	// Token: 0x04000335 RID: 821
	[Header("Events")]
	public UnityEvent onClick;

	// Token: 0x04000336 RID: 822
	public static bool clickBlocked = false;

	// Token: 0x04000337 RID: 823
	private float chk;

	// Token: 0x04000338 RID: 824
	private Vector3 chl;

	// Token: 0x04000339 RID: 825
	private float chm = 10f;

	// Token: 0x0400033A RID: 826
	private float chn = 5f;

	// Token: 0x0400033B RID: 827
	private static bool cho = false;

	// Token: 0x0400033C RID: 828
	private ScrollRect chp;

	// Token: 0x0400033D RID: 829
	private CustomShade chq;

	// Token: 0x0400033E RID: 830
	public CustomButton.ck viewStatus;

	// Token: 0x02000082 RID: 130
	public enum ck
	{
		// Token: 0x04000340 RID: 832
		cgt,
		// Token: 0x04000341 RID: 833
		cgu,
		// Token: 0x04000342 RID: 834
		cgv
	}
}
