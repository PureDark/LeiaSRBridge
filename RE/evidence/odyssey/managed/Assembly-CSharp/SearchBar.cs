using System;
using System.Collections;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000A6 RID: 166
public class SearchBar : MonoBehaviour
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013969 File Offset: 0x00011B69
	// (set) Token: 0x06000418 RID: 1048 RVA: 0x00013970 File Offset: 0x00011B70
	public static bool cln { get; set; }

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000419 RID: 1049 RVA: 0x00013978 File Offset: 0x00011B78
	// (set) Token: 0x0600041A RID: 1050 RVA: 0x0001397F File Offset: 0x00011B7F
	public static bool clo { get; private set; }

	// Token: 0x0600041B RID: 1051 RVA: 0x00013988 File Offset: 0x00011B88
	private void Start()
	{
		bd.ih(SearchBar.clm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		Object.FindAnyObjectByType<BackgroundBlur>();
		Component component = Object.FindAnyObjectByType<BackgroundBackButton>();
		this.clp = this.panel.GetComponent<RectTransform>().sizeDelta;
		this.panelSearchButton.onClick.AddListener(new UnityAction(this.yw));
		this.inputField.onValueChanged.RemoveListener(new UnityAction<string>(this.yp));
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.yp));
		Button component2 = component.GetComponent<Button>();
		if (component2 != null)
		{
			component2.onClick.AddListener(new UnityAction(this.yx));
		}
		this.inputField.onEndEdit.RemoveAllListeners();
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.yy));
		this.resultBackButton.onClick.AddListener(new UnityAction(this.yz));
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00013A88 File Offset: 0x00011C88
	private void yp(string a)
	{
		this.panelSearchText.text = a;
		if (!string.IsNullOrEmpty(a.Trim()) && a.Length >= 1 && !SearchBar.clo)
		{
			CanvasGroup component = this.gameScanButton.GetComponent<CanvasGroup>();
			component.blocksRaycasts = false;
			DOTweenModuleUI.DOFade(component, 0f, 0.15f);
			this.SwitchToResultView();
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00013AE6 File Offset: 0x00011CE6
	private void Update()
	{
		if (Input.GetKeyDown(27) && !PopupManager.bvl)
		{
			Debug.Log(SearchBar.clm + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vv());
			this.inputField.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
			this.SearchModeOff();
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00013B24 File Offset: 0x00011D24
	public void SearchModeOn()
	{
		bd.ih(SearchBar.clm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vw());
		if (!SearchBar.cln)
		{
			base.StopAllCoroutines();
			if (!this.resultView.activeSelf)
			{
				this.clq = this.skinnedMeshRenderer.GetBlendShapeWeight(0);
				this.clr = this.skinnedMeshRenderer.GetBlendShapeWeight(1);
				this.cls = this.panel.GetComponent<RectTransform>().sizeDelta;
			}
			base.StartCoroutine(this.yq());
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00013BA1 File Offset: 0x00011DA1
	public void SearchModeOff()
	{
		bd.ih(SearchBar.clm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vx());
		if (SearchBar.cln)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.yr());
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00013BCC File Offset: 0x00011DCC
	private void SwitchToResultView()
	{
		bd.ih(SearchBar.clm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vy());
		if (!SearchBar.clo)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.ys());
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00013BF7 File Offset: 0x00011DF7
	public IEnumerator yq()
	{
		this.panelSearchText.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		CanvasGroup component = this.searchPopup.GetComponent<CanvasGroup>();
		component.alpha = 1f;
		this.inputField.placeholder.GetComponent<CanvasGroup>().alpha = 0f;
		this.searchButton.gameObject.SetActive(true);
		this.inputField.Select();
		this.inputField.ActivateInputField();
		component.blocksRaycasts = true;
		SearchBar.cln = true;
		yield return null;
		yield break;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00013C06 File Offset: 0x00011E06
	public IEnumerator yr()
	{
		this.backgroundBackButton.gameObject.SetActive(false);
		if (string.IsNullOrWhiteSpace(this.inputField.text))
		{
			this.panelSearchText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pt());
			this.yt(0f, 0f);
			this.yu(0f, this.transitionDuration);
			DOTweenModuleUI.DOFade(this.inputField.placeholder.GetComponent<CanvasGroup>(), 1f, this.transitionDuration);
		}
		else
		{
			this.panelSearchText.text = this.inputField.text;
			this.yu(1f, this.transitionDuration);
			this.yt(110f, 0f);
		}
		this.active.GetComponent<CanvasGroup>().blocksRaycasts = true;
		DOTweenModuleUI.DOFade(this.active.GetComponent<CanvasGroup>(), 1f, this.transitionDuration);
		this.resultView.GetComponent<CanvasGroup>().blocksRaycasts = false;
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.resultView.GetComponent<CanvasGroup>(), 0f, this.transitionDuration), new TweenCallback(this.za));
		this.mainPanelCanvas.xq(cn.chy);
		if (SearchBar.clo)
		{
			this.panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.panel.GetComponent<RectTransform>(), this.clp, this.transitionDuration, false), 9), new TweenCallback(this.zb));
			SearchBar.clo = false;
		}
		SearchPopup component = this.searchPopup.GetComponent<SearchPopup>();
		if (component != null)
		{
			component.bcf(SearchBar.clo);
		}
		CanvasGroup component2 = this.gameScanButton.GetComponent<CanvasGroup>();
		DOTweenModuleUI.DOFade(component2, SearchBar.clo ? 0f : 1f, 0.15f);
		component2.blocksRaycasts = !SearchBar.clo;
		this.searchButton.gameObject.SetActive(false);
		DOTweenModuleUI.DOFade(this.searchPopup.GetComponent<CanvasGroup>(), 0f, 0.15f);
		yield return new WaitForSeconds(0.15f);
		SearchBar.cln = false;
		yield break;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00013C15 File Offset: 0x00011E15
	private IEnumerator ys()
	{
		SearchBar.clo = true;
		this.backgroundBackButton.gameObject.SetActive(false);
		this.yu(1f, this.transitionDuration);
		this.yt(110f, 0f);
		this.resultView.SetActive(true);
		this.resultView.GetComponent<CanvasGroup>().blocksRaycasts = true;
		DOTweenModuleUI.DOFade(this.resultView.GetComponent<CanvasGroup>(), 1f, this.transitionDuration);
		this.active.GetComponent<CanvasGroup>().blocksRaycasts = false;
		DOTweenModuleUI.DOFade(this.active.GetComponent<CanvasGroup>(), 0f, this.transitionDuration);
		this.yv();
		SearchPopup component = this.searchPopup.GetComponent<SearchPopup>();
		if (component != null)
		{
			component.bcf(SearchBar.clo);
		}
		yield return new WaitForSeconds(this.transitionDuration);
		yield break;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00013C24 File Offset: 0x00011E24
	private void yt(float a, float b)
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.panelSearchButton.GetComponent<RectTransform>(), a, b, false), 9);
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x00013C44 File Offset: 0x00011E44
	private void yu(float a, float b)
	{
		if (a == 1f)
		{
			this.panelBackButton.gameObject.SetActive(true);
			this.panelBackButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
			DOTweenModuleUI.DOFade(this.panelBackButton.GetComponent<CanvasGroup>(), a, b);
			return;
		}
		if (a == 0f)
		{
			this.panelBackButton.ry(CustomButton.ck.cgt);
			this.panelBackButton.gameObject.SetActive(false);
			TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.panelBackButton.GetComponent<CanvasGroup>(), a, b), new TweenCallback(this.zc));
		}
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00013CD8 File Offset: 0x00011ED8
	public void yv()
	{
		SearchBar.cy cy = new SearchBar.cy();
		cy.clb = this;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOSizeDelta(this.panel.GetComponent<RectTransform>(), MainPanelCanvas.ListViewPanelSize, this.transitionDuration, false), 9);
		cy.cla = this.skinnedMeshRenderer.GetBlendShapeWeight(0);
		cy.clc = this.skinnedMeshRenderer.GetBlendShapeWeight(1);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnUpdate<TweenerCore<float, float, FloatOptions>>(DOTween.To(new DOGetter<float>(cy.xt), new DOSetter<float>(cy.xu), MainPanelCanvas.ListViewBlendShapeSize.x, 0.3f), new TweenCallback(cy.xv)), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnUpdate<TweenerCore<float, float, FloatOptions>>(DOTween.To(new DOGetter<float>(cy.xw), new DOSetter<float>(cy.xx), MainPanelCanvas.ListViewBlendShapeSize.y, 0.3f), new TweenCallback(cy.xy)), 9);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00013DFD File Offset: 0x00011FFD
	[CompilerGenerated]
	private void yw()
	{
		this.SearchModeOn();
		this.resultView.SetActive(true);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00013E11 File Offset: 0x00012011
	[CompilerGenerated]
	private void yx()
	{
		this.SearchModeOff();
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00013E19 File Offset: 0x00012019
	[CompilerGenerated]
	private void yy(string a)
	{
		if (!SearchBar.clo && string.IsNullOrWhiteSpace(this.inputField.text))
		{
			this.inputField.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
			this.SearchModeOff();
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00013E4A File Offset: 0x0001204A
	[CompilerGenerated]
	private void yz()
	{
		this.inputField.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		this.SearchModeOff();
		SearchPopup component = this.searchPopup.GetComponent<SearchPopup>();
		if (component == null)
		{
			return;
		}
		component.bcf(SearchBar.clo);
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00013E7C File Offset: 0x0001207C
	[CompilerGenerated]
	private void za()
	{
		this.resultView.SetActive(false);
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00013E8A File Offset: 0x0001208A
	[CompilerGenerated]
	private void zb()
	{
		this.panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00013E9D File Offset: 0x0001209D
	[CompilerGenerated]
	private void zc()
	{
		this.panelBackButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	// Token: 0x040003FE RID: 1022
	public BackgroundBlur backgroundBlur;

	// Token: 0x040003FF RID: 1023
	public BackgroundBackButton backgroundBackButton;

	// Token: 0x04000400 RID: 1024
	private static string clm = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vz();

	// Token: 0x04000401 RID: 1025
	[Header("Panel")]
	public Transform panel;

	// Token: 0x04000402 RID: 1026
	public SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x04000403 RID: 1027
	public GameObject active;

	// Token: 0x04000404 RID: 1028
	public GameObject resultView;

	// Token: 0x04000405 RID: 1029
	public float panelDistance;

	// Token: 0x04000406 RID: 1030
	public CustomButton panelBackButton;

	// Token: 0x04000407 RID: 1031
	public CustomButton panelSearchButton;

	// Token: 0x04000408 RID: 1032
	public Text panelSearchText;

	// Token: 0x04000409 RID: 1033
	public CustomButton resultBackButton;

	// Token: 0x0400040A RID: 1034
	public MainPanelCanvas mainPanelCanvas;

	// Token: 0x0400040B RID: 1035
	public CustomButton gameScanButton;

	// Token: 0x0400040C RID: 1036
	[Header("Popup")]
	public RectTransform searchPopup;

	// Token: 0x0400040D RID: 1037
	public CustomButton searchButton;

	// Token: 0x0400040E RID: 1038
	public InputField inputField;

	// Token: 0x0400040F RID: 1039
	public Vector3 expandedScale = new Vector3(0.674f, 0.674f, 0.674f);

	// Token: 0x04000410 RID: 1040
	public float transitionDuration = 0.6f;

	// Token: 0x04000413 RID: 1043
	private Vector2 clp;

	// Token: 0x04000414 RID: 1044
	private float clq;

	// Token: 0x04000415 RID: 1045
	private float clr;

	// Token: 0x04000416 RID: 1046
	private Vector2 cls;

	// Token: 0x020000A7 RID: 167
	[CompilerGenerated]
	private sealed class cy
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00013EB0 File Offset: 0x000120B0
		internal void xu(float a)
		{
			this.cla = a;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00013EB0 File Offset: 0x000120B0
		internal void krp(float a)
		{
			this.cla = a;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013EB9 File Offset: 0x000120B9
		internal float dfg()
		{
			return this.clc;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013EC1 File Offset: 0x000120C1
		internal void xv()
		{
			this.clb.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cla);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013EDA File Offset: 0x000120DA
		internal void fpp(float a)
		{
			this.clc = a;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013EE3 File Offset: 0x000120E3
		internal float xt()
		{
			return this.cla;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013EB0 File Offset: 0x000120B0
		internal void mea(float a)
		{
			this.cla = a;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013EE3 File Offset: 0x000120E3
		internal float nzv()
		{
			return this.cla;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013EEB File Offset: 0x000120EB
		internal void xy()
		{
			this.clb.skinnedMeshRenderer.SetBlendShapeWeight(1, this.clc);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013EB9 File Offset: 0x000120B9
		internal float hwn()
		{
			return this.clc;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013EE3 File Offset: 0x000120E3
		internal float bat()
		{
			return this.cla;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013EB9 File Offset: 0x000120B9
		internal float xw()
		{
			return this.clc;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013EDA File Offset: 0x000120DA
		internal void xx(float a)
		{
			this.clc = a;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013EEB File Offset: 0x000120EB
		internal void bmo()
		{
			this.clb.skinnedMeshRenderer.SetBlendShapeWeight(1, this.clc);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013EEB File Offset: 0x000120EB
		internal void hap()
		{
			this.clb.skinnedMeshRenderer.SetBlendShapeWeight(1, this.clc);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013EDA File Offset: 0x000120DA
		internal void efi(float a)
		{
			this.clc = a;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013EB9 File Offset: 0x000120B9
		internal float ofc()
		{
			return this.clc;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013EEB File Offset: 0x000120EB
		internal void iie()
		{
			this.clb.skinnedMeshRenderer.SetBlendShapeWeight(1, this.clc);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013EB0 File Offset: 0x000120B0
		internal void ksl(float a)
		{
			this.cla = a;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013EEB File Offset: 0x000120EB
		internal void msb()
		{
			this.clb.skinnedMeshRenderer.SetBlendShapeWeight(1, this.clc);
		}

		// Token: 0x04000417 RID: 1047
		public float cla;

		// Token: 0x04000418 RID: 1048
		public SearchBar clb;

		// Token: 0x04000419 RID: 1049
		public float clc;
	}
}
