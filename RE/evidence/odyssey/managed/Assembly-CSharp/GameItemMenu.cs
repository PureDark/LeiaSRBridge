using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200009A RID: 154
public class GameItemMenu : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001208A File Offset: 0x0001028A
	public bool daw
	{
		get
		{
			return this.cjn;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060003A1 RID: 929 RVA: 0x00012094 File Offset: 0x00010294
	public RectTransform dax
	{
		get
		{
			RectTransform result;
			if ((result = this.cjp) == null)
			{
				result = (this.cjp = base.GetComponent<RectTransform>());
			}
			return result;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060003A2 RID: 930 RVA: 0x000120BA File Offset: 0x000102BA
	// (set) Token: 0x060003A3 RID: 931 RVA: 0x000120C2 File Offset: 0x000102C2
	public bool cjt { get; private set; }

	// Token: 0x060003A4 RID: 932 RVA: 0x000120CB File Offset: 0x000102CB
	protected void Start()
	{
		this.vq();
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x000120D4 File Offset: 0x000102D4
	private void vq()
	{
		this.cjv = false;
		this.cjs = false;
		this.cjr = (Action<Game, bool>)Delegate.Remove(this.cjr, new Action<Game, bool>(ViewAreaController.bah));
		this.cjr = (Action<Game, bool>)Delegate.Combine(this.cjr, new Action<Game, bool>(ViewAreaController.bah));
		this.addFavorite.OnEventClick.RemoveAllListeners();
		this.addFavorite.OnEventClick.AddListener(new UnityAction<PointerEventData>(this.wa));
		this.addFavorite.OnEventStartHover.RemoveAllListeners();
		this.addFavorite.OnEventStartHover.AddListener(new UnityAction(this.wb));
		this.addFavorite.OnEventEndHover.RemoveAllListeners();
		this.addFavorite.OnEventEndHover.AddListener(new UnityAction(this.wc));
		this.removeFavorite.OnEventClick.RemoveAllListeners();
		this.removeFavorite.OnEventClick.AddListener(new UnityAction<PointerEventData>(this.wd));
		this.removeFavorite.OnEventStartHover.RemoveAllListeners();
		this.removeFavorite.OnEventStartHover.AddListener(new UnityAction(this.we));
		this.removeFavorite.OnEventEndHover.RemoveAllListeners();
		this.removeFavorite.OnEventEndHover.AddListener(new UnityAction(this.wf));
		this.Target.localScale = this.originalScale;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001224C File Offset: 0x0001044C
	public void vr(Game a, Vector3 b)
	{
		this.Target.localScale = this.originalScale;
		UnityEvent onEventEndHover = this.removeFavorite.OnEventEndHover;
		if (onEventEndHover != null)
		{
			onEventEndHover.Invoke();
		}
		UnityEvent onEventEndHover2 = this.addFavorite.OnEventEndHover;
		if (onEventEndHover2 != null)
		{
			onEventEndHover2.Invoke();
		}
		b.y += 0.1f;
		base.transform.position = b;
		this.cjq = a;
		this.vs(true);
		EventSystem.current.SetSelectedGameObject(base.gameObject);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000122D0 File Offset: 0x000104D0
	private void vs(bool a)
	{
		if (this.cjn != a)
		{
			this.cjn = a;
		}
		if (this.cjn)
		{
			base.gameObject.SetActive(true);
			this.vu();
		}
		this.vt();
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00012304 File Offset: 0x00010504
	private void vt()
	{
		GameItemMenu.cu cu = new GameItemMenu.cu();
		cu.cjl = this;
		if (this.cjs && !this.cjt)
		{
			return;
		}
		Tweener tweener = this.cjo;
		if (tweener != null)
		{
			TweenExtensions.Kill(tweener, false);
		}
		cu.cjm = (this.cjn ? 1f : 0f);
		float num = 0.3f;
		if (this.group != null)
		{
			this.cjs = (cu.cjm > 0f);
			this.group.alpha = 1f - cu.cjm;
			this.cjo = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTweenModuleUI.DOFade(this.group, cu.cjm, num), 6), new TweenCallback(cu.vl));
			return;
		}
		this.cjs = false;
		base.gameObject.SetActive(this.cjn);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000123E4 File Offset: 0x000105E4
	private void vu()
	{
		if (this.cjq == null)
		{
			return;
		}
		bool favoritedByUser = this.cjq.GameInfo.FavoritedByUser;
		this.addFavorite.gameObject.SetActive(!favoritedByUser);
		this.removeFavorite.gameObject.SetActive(favoritedByUser);
		this.vx();
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00012436 File Offset: 0x00010636
	public void vv(bool a)
	{
		this.cjq.GameInfo.FavoritedByUser = a;
		Action<Game, bool> action = this.cjr;
		if (action != null)
		{
			action(this.cjq, a);
		}
		this.vw();
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00012467 File Offset: 0x00010667
	public void vw()
	{
		this.cjt = false;
		this.cju = true;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00012478 File Offset: 0x00010678
	private void vx()
	{
		RectTransform rectTransform = (!this.cjq.GameInfo.FavoritedByUser) ? this.addFavoriteText : this.removeFavoriteText;
		float preferredWidth = LayoutUtility.GetPreferredWidth(rectTransform);
		float num = preferredWidth + 72f;
		rectTransform.sizeDelta = new Vector2(preferredWidth + 36f, rectTransform.sizeDelta.y);
		RectTransform rectTransform2 = this.button;
		rectTransform2.sizeDelta = new Vector2(num, rectTransform2.sizeDelta.y);
		rectTransform2.anchoredPosition = new Vector2(num / 2f, rectTransform2.anchoredPosition.y);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001250D File Offset: 0x0001070D
	private void LateUpdate()
	{
		if (this.cju && !this.cjt && !this.cjs)
		{
			this.vs(false);
			this.cju = false;
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00012535 File Offset: 0x00010735
	public void OnSelect(BaseEventData eventData)
	{
		this.cjt = true;
		this.cju = true;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00012467 File Offset: 0x00010667
	public void OnDeselect(BaseEventData eventData)
	{
		this.cjt = false;
		this.cju = true;
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00012545 File Offset: 0x00010745
	private void vy()
	{
		if (this.cjv)
		{
			return;
		}
		this.cjv = true;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.hoverScaleValue, this.hoverDuration), this.hoverEase);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001257A File Offset: 0x0001077A
	private void vz()
	{
		if (!this.cjv)
		{
			return;
		}
		this.cjv = false;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Target, this.originalScale, this.hoverDuration), this.hoverEase);
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x000125BE File Offset: 0x000107BE
	[CompilerGenerated]
	private void wa(PointerEventData a)
	{
		if (a.button == null)
		{
			this.vv(true);
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x000125CF File Offset: 0x000107CF
	[CompilerGenerated]
	private void wb()
	{
		this.vy();
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x000125D7 File Offset: 0x000107D7
	[CompilerGenerated]
	private void wc()
	{
		this.vz();
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x000125DF File Offset: 0x000107DF
	[CompilerGenerated]
	private void wd(PointerEventData a)
	{
		if (a.button == null)
		{
			this.vv(false);
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000125CF File Offset: 0x000107CF
	[CompilerGenerated]
	private void we()
	{
		this.vy();
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000125D7 File Offset: 0x000107D7
	[CompilerGenerated]
	private void wf()
	{
		this.vz();
	}

	// Token: 0x040003B2 RID: 946
	[SerializeField]
	private BaseCustomButton addFavorite;

	// Token: 0x040003B3 RID: 947
	[SerializeField]
	private BaseCustomButton removeFavorite;

	// Token: 0x040003B4 RID: 948
	[SerializeField]
	private CanvasGroup group;

	// Token: 0x040003B5 RID: 949
	[SerializeField]
	private RectTransform addFavoriteText;

	// Token: 0x040003B6 RID: 950
	[SerializeField]
	private RectTransform removeFavoriteText;

	// Token: 0x040003B7 RID: 951
	[SerializeField]
	private RectTransform button;

	// Token: 0x040003B8 RID: 952
	[SerializeField]
	private RectTransform Target;

	// Token: 0x040003B9 RID: 953
	[Header("Hover Setting")]
	[SerializeField]
	private Vector3 hoverScaleValue;

	// Token: 0x040003BA RID: 954
	[SerializeField]
	private Vector3 originalScale;

	// Token: 0x040003BB RID: 955
	[SerializeField]
	private float hoverDuration;

	// Token: 0x040003BC RID: 956
	[SerializeField]
	private Ease hoverEase;

	// Token: 0x040003BD RID: 957
	private bool cjn = true;

	// Token: 0x040003BE RID: 958
	private Tweener cjo;

	// Token: 0x040003BF RID: 959
	private RectTransform cjp;

	// Token: 0x040003C0 RID: 960
	private Game cjq;

	// Token: 0x040003C1 RID: 961
	private Action<Game, bool> cjr;

	// Token: 0x040003C2 RID: 962
	private bool cjs;

	// Token: 0x040003C4 RID: 964
	private bool cju;

	// Token: 0x040003C5 RID: 965
	private bool cjv;

	// Token: 0x0200009B RID: 155
	[CompilerGenerated]
	private sealed class cu
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x000125F0 File Offset: 0x000107F0
		internal void vl()
		{
			this.cjl.group.alpha = this.cjm;
			if (!this.cjl.cjn && this.cjl.group.alpha <= 0.01f)
			{
				this.cjl.vu();
				this.cjl.gameObject.SetActive(false);
			}
			this.cjl.cjs = false;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00012660 File Offset: 0x00010860
		internal void lnf()
		{
			this.cjl.group.alpha = this.cjm;
			if (!this.cjl.cjn && this.cjl.group.alpha <= -1509f)
			{
				this.cjl.vu();
				this.cjl.gameObject.SetActive(false);
			}
			this.cjl.cjs = false;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000126D0 File Offset: 0x000108D0
		internal void ofr()
		{
			this.cjl.group.alpha = this.cjm;
			if (!this.cjl.cjn && this.cjl.group.alpha <= -3765f)
			{
				this.cjl.vu();
				this.cjl.gameObject.SetActive(false);
			}
			this.cjl.cjs = false;
		}

		// Token: 0x040003C6 RID: 966
		public GameItemMenu cjl;

		// Token: 0x040003C7 RID: 967
		public float cjm;
	}
}
