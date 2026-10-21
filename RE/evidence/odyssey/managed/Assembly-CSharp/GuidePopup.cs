using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000BC RID: 188
public class GuidePopup : MonoBehaviour
{
	// Token: 0x060004DF RID: 1247 RVA: 0x00017060 File Offset: 0x00015260
	private void Start()
	{
		bd.ih(GuidePopup.cnr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		this.prevButton.onClick.AddListener(new UnityAction(this.OnPrevButtonClicked));
		this.nextButton.onClick.AddListener(new UnityAction(this.OnNextButtonClicked));
		this.closeButton.onClick.AddListener(new UnityAction(this.bbq));
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x000170D0 File Offset: 0x000152D0
	private void Update()
	{
		if (Input.GetKeyDown(276) && this.cnu != 0)
		{
			this.OnPrevButtonClicked();
		}
		else if (Input.GetKeyDown(275) && this.cnu != this.textItems.Count - 1)
		{
			this.OnNextButtonClicked();
		}
		if (this.cnu == 0)
		{
			this.prevButton.gameObject.SetActive(false);
		}
		else
		{
			this.prevButton.gameObject.SetActive(true);
		}
		if (this.cnu == this.textItems.Count - 1)
		{
			this.nextButton.gameObject.SetActive(false);
			return;
		}
		this.nextButton.gameObject.SetActive(true);
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00017184 File Offset: 0x00015384
	private void OnDisable()
	{
		bd.ih(GuidePopup.cnr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xg());
		this.Transition(0, this.cnv * -1f, 0f);
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x000171B0 File Offset: 0x000153B0
	private void OnPrevButtonClicked()
	{
		bd.ih(GuidePopup.cnr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xj());
		int nextIndex = (this.cnu - 1 + this.textItems.Count) % this.textItems.Count;
		this.cnv += this.cnt;
		this.Transition(nextIndex, -this.cnt, this.defaultDuration);
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00017214 File Offset: 0x00015414
	private void OnNextButtonClicked()
	{
		bd.ih(GuidePopup.cnr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xk());
		int nextIndex = (this.cnu + 1) % this.textItems.Count;
		this.cnv += this.cnt;
		this.Transition(nextIndex, this.cnt, this.defaultDuration);
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00016D0A File Offset: 0x00014F0A
	private void bbq()
	{
		PopupManager.ClosePopup();
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0001726C File Offset: 0x0001546C
	private void Transition(int nextIndex, float moveDistance, float duration)
	{
		GuidePopup.dg dg = new GuidePopup.dg();
		dg.cnq = this;
		bd.ih(GuidePopup.cnr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sp());
		dg.cnp = this.cnu;
		RectTransform component = this.textItems[dg.cnp].GetComponent<RectTransform>();
		CanvasGroup component2 = this.textItems[dg.cnp].GetComponent<CanvasGroup>();
		RectTransform component3 = this.imageItems[dg.cnp].GetComponent<RectTransform>();
		CanvasGroup component4 = this.imageItems[dg.cnp].GetComponent<CanvasGroup>();
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(component, 0f, duration, false), 1);
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(component2, 0f, duration), 1), new TweenCallback(dg.bbo));
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(component3, -moveDistance, duration, false), 1);
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(component4, 0f, duration), 1), new TweenCallback(dg.bbp));
		this.cnu = nextIndex;
		this.textItems[this.cnu].SetActive(true);
		RectTransform component5 = this.textItems[this.cnu].GetComponent<RectTransform>();
		CanvasGroup component6 = this.textItems[this.cnu].GetComponent<CanvasGroup>();
		component5.anchoredPosition = new Vector2(0f, component5.anchoredPosition.y);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(component5, 0f, duration, false), 1);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(component6, 1f, duration), 1);
		this.imageItems[this.cnu].SetActive(true);
		RectTransform component7 = this.imageItems[this.cnu].GetComponent<RectTransform>();
		CanvasGroup component8 = this.imageItems[this.cnu].GetComponent<CanvasGroup>();
		component7.anchoredPosition = new Vector2(moveDistance, component7.anchoredPosition.y);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(component7, 0f, duration, false), 1);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(component8, 1f, duration), 1);
		float x = this.indicator.sizeDelta.x;
		float num = (float)this.cnu * (x + this.cns);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.indicator, num, duration, false), 9);
	}

	// Token: 0x0400049D RID: 1181
	private static string cnr = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xl();

	// Token: 0x0400049E RID: 1182
	public List<GameObject> textItems;

	// Token: 0x0400049F RID: 1183
	public List<GameObject> imageItems;

	// Token: 0x040004A0 RID: 1184
	public CustomButton prevButton;

	// Token: 0x040004A1 RID: 1185
	public CustomButton nextButton;

	// Token: 0x040004A2 RID: 1186
	public CustomButton closeButton;

	// Token: 0x040004A3 RID: 1187
	public RectTransform indicator;

	// Token: 0x040004A4 RID: 1188
	public float defaultDuration = 0.3f;

	// Token: 0x040004A5 RID: 1189
	private float cns = 16f;

	// Token: 0x040004A6 RID: 1190
	private float cnt = 200f;

	// Token: 0x040004A7 RID: 1191
	private int cnu;

	// Token: 0x040004A8 RID: 1192
	private float cnv;

	// Token: 0x020000BD RID: 189
	[CompilerGenerated]
	private sealed class dg
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x000174EE File Offset: 0x000156EE
		internal void bbo()
		{
			if (this.cnp != this.cnq.cnu)
			{
				this.cnq.textItems[this.cnp].SetActive(false);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000174EE File Offset: 0x000156EE
		internal void dwg()
		{
			if (this.cnp != this.cnq.cnu)
			{
				this.cnq.textItems[this.cnp].SetActive(false);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000174EE File Offset: 0x000156EE
		internal void ftl()
		{
			if (this.cnp != this.cnq.cnu)
			{
				this.cnq.textItems[this.cnp].SetActive(false);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000174EE File Offset: 0x000156EE
		internal void ncm()
		{
			if (this.cnp != this.cnq.cnu)
			{
				this.cnq.textItems[this.cnp].SetActive(false);
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001751F File Offset: 0x0001571F
		internal void bbp()
		{
			if (this.cnp != this.cnq.cnu)
			{
				this.cnq.imageItems[this.cnp].SetActive(false);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001751F File Offset: 0x0001571F
		internal void ero()
		{
			if (this.cnp != this.cnq.cnu)
			{
				this.cnq.imageItems[this.cnp].SetActive(false);
			}
		}

		// Token: 0x040004A9 RID: 1193
		public int cnp;

		// Token: 0x040004AA RID: 1194
		public GuidePopup cnq;
	}
}
