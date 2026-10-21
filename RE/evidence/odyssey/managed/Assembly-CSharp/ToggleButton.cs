using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000089 RID: 137
public class ToggleButton : MonoBehaviour
{
	// Token: 0x06000330 RID: 816 RVA: 0x00010AC7 File Offset: 0x0000ECC7
	private void Start()
	{
		this.thumbnailViewButton.onClick.AddListener(new UnityAction(this.tc));
		this.listViewButton.onClick.AddListener(new UnityAction(this.td));
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00010B04 File Offset: 0x0000ED04
	private void tc()
	{
		if (this.viewState != cn.chy)
		{
			this.viewState = cn.chy;
			this.listlView.blocksRaycasts = false;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.indicator, this.cid.x, this.duration, false), 9);
			this.thumbnailViewIcon.SetActive(true);
			this.listViewIcon.SetActive(false);
			this.thumbnailView.gameObject.SetActive(true);
			ShortcutExtensions.DOKill(this.thumbnailView, false);
			DOTweenModuleUI.DOFade(this.thumbnailView, 1f, this.duration / 2f);
			this.gameScanToolTip.anchoredPosition = new Vector3(this.gameScanToolTip.anchoredPosition.x, this.gameScanToolTip.anchoredPosition.y + 25f, 0f);
			ShortcutExtensions.DOKill(this.listlView, false);
			TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.listlView, 0f, this.duration / 2f), new TweenCallback(this.tg));
			this.te(cn.chy);
			this.tf(cn.chy);
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00010C30 File Offset: 0x0000EE30
	private void td()
	{
		if (this.viewState != cn.chz)
		{
			this.viewState = cn.chz;
			this.thumbnailView.blocksRaycasts = false;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.indicator, this.cie.x, this.duration, false), 9);
			this.thumbnailViewIcon.SetActive(false);
			this.listViewIcon.SetActive(true);
			this.listlView.gameObject.SetActive(true);
			ShortcutExtensions.DOKill(this.listlView, false);
			DOTweenModuleUI.DOFade(this.listlView, 1f, this.duration / 2f);
			this.gameScanToolTip.anchoredPosition = new Vector3(this.gameScanToolTip.anchoredPosition.x, this.gameScanToolTip.anchoredPosition.y - 25f, 0f);
			ShortcutExtensions.DOKill(this.thumbnailView, false);
			TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.thumbnailView, 0f, this.duration / 2f), new TweenCallback(this.th));
			this.te(cn.chz);
			this.tf(cn.chz);
		}
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00010D60 File Offset: 0x0000EF60
	public void te(cn a)
	{
		Vector2 vector = (a == cn.chy) ? MainPanelCanvas.ThumbnailViewPanelSize : MainPanelCanvas.ListViewPanelSize;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.panel, vector, this.duration, false), 9);
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00010D98 File Offset: 0x0000EF98
	public void tf(cn a)
	{
		ToggleButton.co co = new ToggleButton.co();
		co.cib = this;
		Vector2 vector = (a == cn.chy) ? MainPanelCanvas.ThumbnailViewBlendShapeSize : MainPanelCanvas.ListViewBlendShapeSize;
		co.cia = this.skinnedMeshRenderer.GetBlendShapeWeight(0);
		co.cic = this.skinnedMeshRenderer.GetBlendShapeWeight(1);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnUpdate<TweenerCore<float, float, FloatOptions>>(DOTween.To(new DOGetter<float>(co.sw), new DOSetter<float>(co.sx), vector.x, 0.3f), new TweenCallback(co.sy)), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnUpdate<TweenerCore<float, float, FloatOptions>>(DOTween.To(new DOGetter<float>(co.sz), new DOSetter<float>(co.ta), vector.y, 0.3f), new TweenCallback(co.tb)), 9);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00010E9A File Offset: 0x0000F09A
	[CompilerGenerated]
	private void tg()
	{
		this.listlView.gameObject.SetActive(false);
		this.thumbnailView.blocksRaycasts = true;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00010EB9 File Offset: 0x0000F0B9
	[CompilerGenerated]
	private void th()
	{
		this.thumbnailView.gameObject.SetActive(false);
		this.listlView.blocksRaycasts = true;
	}

	// Token: 0x0400035F RID: 863
	public cn viewState;

	// Token: 0x04000360 RID: 864
	public Button thumbnailViewButton;

	// Token: 0x04000361 RID: 865
	public Button listViewButton;

	// Token: 0x04000362 RID: 866
	public RectTransform indicator;

	// Token: 0x04000363 RID: 867
	public GameObject thumbnailViewIcon;

	// Token: 0x04000364 RID: 868
	public GameObject listViewIcon;

	// Token: 0x04000365 RID: 869
	public float duration;

	// Token: 0x04000366 RID: 870
	public CanvasGroup thumbnailView;

	// Token: 0x04000367 RID: 871
	public CanvasGroup listlView;

	// Token: 0x04000368 RID: 872
	public RectTransform panel;

	// Token: 0x04000369 RID: 873
	public RectTransform gameScanToolTip;

	// Token: 0x0400036A RID: 874
	private Vector2 cid = new Vector2(-46f, 0f);

	// Token: 0x0400036B RID: 875
	private Vector2 cie = new Vector2(46f, 0f);

	// Token: 0x0400036C RID: 876
	public SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x0200008A RID: 138
	[CompilerGenerated]
	private sealed class co
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		internal void mvj(float a)
		{
			this.cia = a;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010EE1 File Offset: 0x0000F0E1
		internal void sy()
		{
			this.cib.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cia);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010EFA File Offset: 0x0000F0FA
		internal float sw()
		{
			return this.cia;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		internal void sx(float a)
		{
			this.cia = a;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010F02 File Offset: 0x0000F102
		internal void nou()
		{
			this.cib.skinnedMeshRenderer.SetBlendShapeWeight(1, this.cic);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010EE1 File Offset: 0x0000F0E1
		internal void gvu()
		{
			this.cib.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cia);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010F1B File Offset: 0x0000F11B
		internal void nxk(float a)
		{
			this.cic = a;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010F24 File Offset: 0x0000F124
		internal float sz()
		{
			return this.cic;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010F24 File Offset: 0x0000F124
		internal float mau()
		{
			return this.cic;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010F1B File Offset: 0x0000F11B
		internal void ta(float a)
		{
			this.cic = a;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010F02 File Offset: 0x0000F102
		internal void tb()
		{
			this.cib.skinnedMeshRenderer.SetBlendShapeWeight(1, this.cic);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010F1B File Offset: 0x0000F11B
		internal void kdl(float a)
		{
			this.cic = a;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010F02 File Offset: 0x0000F102
		internal void msf()
		{
			this.cib.skinnedMeshRenderer.SetBlendShapeWeight(1, this.cic);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010F24 File Offset: 0x0000F124
		internal float ian()
		{
			return this.cic;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010EE1 File Offset: 0x0000F0E1
		internal void cwz()
		{
			this.cib.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cia);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		internal void ws(float a)
		{
			this.cia = a;
		}

		// Token: 0x0400036D RID: 877
		public float cia;

		// Token: 0x0400036E RID: 878
		public ToggleButton cib;

		// Token: 0x0400036F RID: 879
		public float cic;
	}
}
