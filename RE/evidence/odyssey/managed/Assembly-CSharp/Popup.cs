using System;
using System.Collections;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class Popup : MonoBehaviour
{
	// Token: 0x060004FB RID: 1275 RVA: 0x000176A4 File Offset: 0x000158A4
	private void Awake()
	{
		this.cof = base.GetComponent<CanvasGroup>();
		this.cog = base.GetComponent<RectTransform>();
		this.coh = base.GetComponentInChildren<SkinnedMeshRenderer>();
		if (this.coh != null && this.coh.materials.Length != 0)
		{
			this.coi = this.coh.materials[0];
		}
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00017704 File Offset: 0x00015904
	private void Start()
	{
		this.backgroundBlur = Object.FindAnyObjectByType<BackgroundBlur>();
		this.mainPanelCanvas = Object.FindAnyObjectByType<MainPanelCanvas>();
		foreach (object obj in GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xn()).transform)
		{
			Transform transform = (Transform)obj;
			if (transform.transform.name.Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xo()))
			{
				this.backgroundBackButton = transform.GetComponent<BackgroundBackButton>();
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x000177A4 File Offset: 0x000159A4
	private void Update()
	{
		if (Input.GetKeyDown(27) && this.isOpened)
		{
			PopupManager.ClosePopup();
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x000177BC File Offset: 0x000159BC
	public void Open()
	{
		this.isOpened = true;
		this.backgroundBlur.BlurOn();
		this.backgroundBackButton.gameObject.SetActive(true);
		this.mainPanelCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.mainPanelCanvas.transform, 1.6f, this.duration, false), 9);
		base.gameObject.SetActive(true);
		this.cof.alpha = 0f;
		this.cof.blocksRaycasts = false;
		this.cog.localScale = new Vector3(this.scaleDownFactor, this.scaleDownFactor, this.scaleDownFactor);
		this.cof.blocksRaycasts = true;
		DOTweenModuleUI.DOFade(this.cof, 1f, this.duration);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.cog, 1f, this.duration), 9);
		if (this.coi != null)
		{
			ShortcutExtensions.DOFloat(this.coi, 1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sl(), this.duration);
		}
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000178D8 File Offset: 0x00015AD8
	public void bby()
	{
		this.isOpened = false;
		this.backgroundBlur.BlurOff();
		this.backgroundBackButton.gameObject.SetActive(false);
		this.mainPanelCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveZ(this.mainPanelCanvas.transform, 0f, this.duration, false), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.cog, this.scaleDownFactor, this.duration), 9);
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.cof, 0f, this.duration), new TweenCallback(this.bcb));
		if (this.coi != null)
		{
			ShortcutExtensions.DOFloat(this.coi, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sl(), this.duration);
		}
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000179AE File Offset: 0x00015BAE
	public void bbz(float a)
	{
		base.StartCoroutine(this.bca(a));
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000179BE File Offset: 0x00015BBE
	private IEnumerator bca(float a)
	{
		yield return new WaitForSeconds(a);
		this.bby();
		yield break;
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000179F2 File Offset: 0x00015BF2
	[CompilerGenerated]
	private void bcb()
	{
		base.gameObject.SetActive(false);
		if (this.destroyAfterClose)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040004BB RID: 1211
	public float duration = 0.5f;

	// Token: 0x040004BC RID: 1212
	public float scaleDownFactor = 0.8f;

	// Token: 0x040004BD RID: 1213
	public bool isOpened;

	// Token: 0x040004BE RID: 1214
	public bool destroyAfterClose;

	// Token: 0x040004BF RID: 1215
	private CanvasGroup cof;

	// Token: 0x040004C0 RID: 1216
	private RectTransform cog;

	// Token: 0x040004C1 RID: 1217
	private SkinnedMeshRenderer coh;

	// Token: 0x040004C2 RID: 1218
	private Material coi;

	// Token: 0x040004C3 RID: 1219
	public BackgroundBackButton backgroundBackButton;

	// Token: 0x040004C4 RID: 1220
	public BackgroundBlur backgroundBlur;

	// Token: 0x040004C5 RID: 1221
	public MainPanelCanvas mainPanelCanvas;
}
