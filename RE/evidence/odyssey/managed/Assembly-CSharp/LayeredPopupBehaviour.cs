using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000073 RID: 115
public class LayeredPopupBehaviour : MonoBehaviour
{
	// Token: 0x06000279 RID: 633 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
	private void Start()
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
		}
		else if (this.allInOneSprite == null)
		{
			if (this.targetTransform1)
			{
				this.targetTransform1.gameObject.SetActive(true);
			}
			if (this.targetTransform2)
			{
				this.targetTransform2.gameObject.SetActive(true);
			}
		}
		else
		{
			this.targetTransform0.GetComponent<Image>().sprite = this.allInOneSprite;
			if (this.targetTransform1)
			{
				this.targetTransform1.gameObject.SetActive(false);
			}
			if (this.targetTransform2)
			{
				this.targetTransform2.gameObject.SetActive(false);
			}
		}
		if (this.targetTransform0 != null)
		{
			this.ceu = Vector3.one;
			this.cex = Vector3.zero;
		}
		if (this.targetTransform1 != null)
		{
			this.cev = Vector3.one;
			this.cey = Vector3.zero;
		}
		if (this.targetTransform2 != null)
		{
			this.cew = Vector3.one;
			this.cez = Vector3.zero;
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000CA28 File Offset: 0x0000AC28
	public void pe()
	{
		if (!this.useLayerEffect)
		{
			return;
		}
		if (this.targetTransform0 != null)
		{
			this.targetTransform0.localScale = Vector3.one;
			this.targetTransform0.localPosition = Vector3.zero;
		}
		if (this.targetTransform1 != null)
		{
			this.targetTransform1.localScale = Vector3.one;
			this.targetTransform1.localPosition = Vector3.zero;
		}
		if (this.targetTransform2 != null)
		{
			this.targetTransform2.localScale = Vector3.one;
			this.targetTransform2.localPosition = Vector3.zero;
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform0, this.scaleValue0, this.duration), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform0, this.popOutDistance0, this.duration, false), 9);
		if (this.targetTransform1 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform1, this.scaleValue1, this.duration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform1, this.popOutDistance1, this.duration, false), 9);
		}
		if (this.targetTransform2 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform2, this.scaleValue2, this.duration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform2, this.popOutDistance2, this.duration, false), 9);
		}
		if (this.noMaskImages != null)
		{
			for (int i = 0; i < this.noMaskImages.Count; i++)
			{
				this.noMaskImages[i].maskable = false;
			}
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
	public void pf()
	{
		if (!this.useLayerEffect)
		{
			return;
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform0, this.ceu, this.duration), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform0, this.cex.z, this.duration, false), 9);
		if (this.targetTransform1 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform1, this.cev, this.duration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform1, this.cey.z, this.duration, false), 9);
		}
		if (this.targetTransform2 != null)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.targetTransform2, this.cew, this.duration), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.targetTransform2, this.cez.z, this.duration, false), 9);
		}
		if (this.noMaskImages != null)
		{
			for (int i = 0; i < this.noMaskImages.Count; i++)
			{
				this.noMaskImages[i].maskable = true;
			}
		}
	}

	// Token: 0x0400028D RID: 653
	private Vector3 ceu;

	// Token: 0x0400028E RID: 654
	private Vector3 cev;

	// Token: 0x0400028F RID: 655
	private Vector3 cew;

	// Token: 0x04000290 RID: 656
	private Vector3 cex;

	// Token: 0x04000291 RID: 657
	private Vector3 cey;

	// Token: 0x04000292 RID: 658
	private Vector3 cez;

	// Token: 0x04000293 RID: 659
	public Image background;

	// Token: 0x04000294 RID: 660
	public Color backgroundColor;

	// Token: 0x04000295 RID: 661
	[Header("Layer Effect Settings")]
	public bool useLayerEffect;

	// Token: 0x04000296 RID: 662
	public Sprite allInOneSprite;

	// Token: 0x04000297 RID: 663
	public float duration;

	// Token: 0x04000298 RID: 664
	public Transform targetTransform0;

	// Token: 0x04000299 RID: 665
	public Transform targetTransform1;

	// Token: 0x0400029A RID: 666
	public Transform targetTransform2;

	// Token: 0x0400029B RID: 667
	public float scaleValue0 = 1f;

	// Token: 0x0400029C RID: 668
	public float scaleValue1 = 1f;

	// Token: 0x0400029D RID: 669
	public float scaleValue2 = 1f;

	// Token: 0x0400029E RID: 670
	public float popOutDistance0;

	// Token: 0x0400029F RID: 671
	public float popOutDistance1;

	// Token: 0x040002A0 RID: 672
	public float popOutDistance2;

	// Token: 0x040002A1 RID: 673
	public List<Image> noMaskImages;
}
