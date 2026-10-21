using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x020000AD RID: 173
public class RecentViewArea : ViewArea, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600045F RID: 1119 RVA: 0x00014540 File Offset: 0x00012740
	protected override void InstantiateArea()
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wa(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wb());
		bool flag = this.cja != null && this.cja.Length != 0;
		if (this.ContentView != null)
		{
			GameObject gameObject = this.ContentView;
			if (gameObject != null)
			{
				gameObject.SetActive(flag);
			}
		}
		if (this.EmptyView != null)
		{
			GameObject emptyView = this.EmptyView;
			if (emptyView != null)
			{
				emptyView.SetActive(!flag);
			}
		}
		if (flag)
		{
			if (this.gameItems == null)
			{
				bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wa(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wc(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wb());
				return;
			}
			if (this.gameItems.Length <= 0)
			{
				return;
			}
			this.zj();
			this.zl();
		}
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x000145F0 File Offset: 0x000127F0
	public override void ur(Game[] a, Action<Game, PointerEventData> b)
	{
		base.ur(a, b);
		this.InstantiateArea();
		base.us();
		ViewAreaController.cmk -= this.zi;
		ViewAreaController.cmk += this.zi;
		this.contentView.cjj -= this.zm;
		this.contentView.cjj += this.zm;
		this.scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.zq));
		this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.zq));
		ScrollButtonGroup trackingScrollButtonGroup = this.TrackingScrollButtonGroup;
		trackingScrollButtonGroup.OnFocusChangeCallback = (UnityAction<bool>)Delegate.Remove(trackingScrollButtonGroup.OnFocusChangeCallback, new UnityAction<bool>(this.zn));
		ScrollButtonGroup trackingScrollButtonGroup2 = this.TrackingScrollButtonGroup;
		trackingScrollButtonGroup2.OnFocusChangeCallback = (UnityAction<bool>)Delegate.Combine(trackingScrollButtonGroup2.OnFocusChangeCallback, new UnityAction<bool>(this.zn));
		NestedVerticalScrollRect nestedVerticalScrollRect = this.scrollRect;
		nestedVerticalScrollRect.OnFocusChangeCallback = (UnityAction<bool>)Delegate.Remove(nestedVerticalScrollRect.OnFocusChangeCallback, new UnityAction<bool>(this.zn));
		NestedVerticalScrollRect nestedVerticalScrollRect2 = this.scrollRect;
		nestedVerticalScrollRect2.OnFocusChangeCallback = (UnityAction<bool>)Delegate.Combine(nestedVerticalScrollRect2.OnFocusChangeCallback, new UnityAction<bool>(this.zn));
		this.recentButtonPrevious.onClick.RemoveAllListeners();
		this.recentButtonPrevious.onClick.AddListener(new UnityAction(this.zr));
		this.recentButtonNext.onClick.RemoveAllListeners();
		this.recentButtonNext.onClick.AddListener(new UnityAction(this.zs));
		this.recentButtonNext.gameObject.SetActive(false);
		this.recentButtonPrevious.gameObject.SetActive(false);
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x000147AF File Offset: 0x000129AF
	private void zh(ItemRecent a, Game b, int c)
	{
		a.gameObject.SetActive(true);
		a.wm(c, new UnityAction<int, PointerEventData>(this.OnItemClick), new UnityAction<int>(this.OnFavoriteClick));
		a.wo(b);
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x000147E4 File Offset: 0x000129E4
	private void OnFavoriteClick(int index)
	{
		if (this.cja == null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
			return;
		}
		int num = this.cja.Length;
		if (index > num - 1)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vl(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
			return;
		}
		if (this.cja[index] != null)
		{
			bool flag = !this.cja[index].GameInfo.FavoritedByUser;
			this.cja[index].GameInfo.FavoritedByUser = flag;
			bn.cyp.cyq.SerializeData();
			base.ut();
			ViewAreaController.bah(this.cja[index], flag);
			return;
		}
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vk(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000148B4 File Offset: 0x00012AB4
	private void zi(Game a, bool b)
	{
		if (base.ciy)
		{
			base.us();
			return;
		}
		if (this.cja == null || this.cja.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.cja.Length; i++)
		{
			if (this.cja[i] != null && a == this.cja[i])
			{
				this.gameItems[i].wp(b);
				return;
			}
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0001491C File Offset: 0x00012B1C
	private void zj()
	{
		if (this.contentRectTransform == null || this.cja == null)
		{
			return;
		}
		int num = Mathf.Min(this.cja.Length, this.gameItems.Length);
		if (num <= 0)
		{
			return;
		}
		float default_WIDTH_ITEM = this.recentViewAreaSetting.DEFAULT_WIDTH_ITEM;
		float num2 = default_WIDTH_ITEM * (float)num + this.recentViewAreaSetting.SPACING * (float)(num - 1) + this.recentViewAreaSetting.PADDING * 2f;
		Vector2 sizeDelta = this.contentRectTransform.sizeDelta;
		sizeDelta.x = Mathf.Max(num2, this.recentViewAreaSetting.DEFAULT_WIDTH_CONTENT);
		this.contentRectTransform.sizeDelta = sizeDelta;
		this.clz.x = (-sizeDelta.x + default_WIDTH_ITEM) / 2f + this.recentViewAreaSetting.PADDING;
		this.clz.y = 0f;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x000149F8 File Offset: 0x00012BF8
	private void zk(int a, out Vector2 b)
	{
		float default_WIDTH_ITEM = this.recentViewAreaSetting.DEFAULT_WIDTH_ITEM;
		b.x = this.clz.x + (float)a * (default_WIDTH_ITEM + this.recentViewAreaSetting.SPACING);
		b.y = this.clz.y;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00014A44 File Offset: 0x00012C44
	private void zl()
	{
		if (this.cja == null || this.gameItems == null)
		{
			return;
		}
		int num = Mathf.Min(this.cja.Length, this.gameItems.Length);
		for (int i = 0; i < this.gameItems.Length; i++)
		{
			ItemRecent itemRecent = this.gameItems[i];
			if (!(itemRecent == null))
			{
				if (i < num)
				{
					Game b = this.cja[i];
					Vector2 anchoredPosition;
					this.zk(i, out anchoredPosition);
					itemRecent.dap.anchoredPosition = anchoredPosition;
					this.zh(itemRecent, b, i);
				}
				else
				{
					itemRecent.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00014AD8 File Offset: 0x00012CD8
	private void zm(ViewContent.ct a)
	{
		if (this.gameItems == null || this.gameItems.Length == 0)
		{
			return;
		}
		foreach (ItemRecent itemRecent in this.gameItems)
		{
			bool flag = this.contentView.vf(itemRecent.dap);
			itemRecent.dao.rv(flag ? CustomButton.ck.cgt : CustomButton.ck.cgv);
		}
		this.zq(Vector2.zero);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00014B40 File Offset: 0x00012D40
	private void zn(bool a)
	{
		if (this.scrollRect.czu || this.TrackingScrollButtonGroup.dad || this.cma)
		{
			this.zq(Vector2.zero);
			return;
		}
		this.recentButtonNext.gameObject.SetActive(false);
		this.recentButtonPrevious.gameObject.SetActive(false);
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00014BA0 File Offset: 0x00012DA0
	private void OnPrevButtonClick()
	{
		RecentViewArea.dc dc = new RecentViewArea.dc();
		dc.clu = this;
		if (this.scrollRect != null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wa(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.we() + this.scrollRect.transform.name, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wf());
			float num = this.zp();
			dc.clv = Math.Max(this.scrollRect.horizontalNormalizedPosition - num, 0f);
			TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(DOTweenModuleUI.DOHorizontalNormalizedPos(this.scrollRect, dc.clv, 0.3f, false), 9), new TweenCallback(dc.zf));
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00014C48 File Offset: 0x00012E48
	private void zo()
	{
		RecentViewArea.dd dd = new RecentViewArea.dd();
		dd.clw = this;
		if (this.scrollRect != null)
		{
			float num = this.zp();
			dd.clx = Math.Max(this.scrollRect.horizontalNormalizedPosition + num, 0f);
			TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(DOTweenModuleUI.DOHorizontalNormalizedPos(this.scrollRect, dd.clx, 0.3f, false), 9), new TweenCallback(dd.zg));
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00014CC4 File Offset: 0x00012EC4
	private float zp()
	{
		int num = this.cja.Length;
		if (num <= 5)
		{
			return 1f;
		}
		return (50f - (float)num) / (10f * (float)num);
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00014CF8 File Offset: 0x00012EF8
	private void zq(Vector2 a)
	{
		this.scrollButtonGroup.SetParent(this.butttonGroup.transform);
		this.scrollButtonGroup.anchoredPosition = Vector2.zero;
		bool flag = this.contentView.dav > ViewContent.ct.cjc;
		bool flag2 = this.scrollRect.czu || this.TrackingScrollButtonGroup.dad || this.cma;
		if (flag || !flag2 || SearchBar.clo)
		{
			this.recentButtonNext.gameObject.SetActive(false);
			this.recentButtonPrevious.gameObject.SetActive(false);
			return;
		}
		bool flag3 = true;
		bool flag4;
		if (this.scrollRect.horizontalNormalizedPosition > 0.001f && this.scrollRect.horizontalNormalizedPosition < 0.999f)
		{
			flag3 = true;
			flag4 = true;
		}
		else if (this.scrollRect.horizontalNormalizedPosition <= 0.001f)
		{
			flag3 = false;
			flag4 = (this.contentRectTransform.rect.width > this.scrollRect.czv.rect.width);
		}
		else
		{
			if (this.contentRectTransform.rect.width <= this.scrollRect.czv.rect.width)
			{
				flag3 = false;
			}
			flag4 = false;
		}
		this.recentButtonPrevious.gameObject.SetActive(flag3);
		this.recentButtonNext.gameObject.SetActive(flag4);
		if (flag3 || flag4)
		{
			this.scrollButtonGroup.SetParent(this.TrackingScrollButtonGroup.transform);
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00014E6C File Offset: 0x0001306C
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.cma = true;
		this.zn(this.cma);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00014E81 File Offset: 0x00013081
	public void OnPointerExit(PointerEventData eventData)
	{
		this.cma = false;
		this.zn(this.cma);
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00014E98 File Offset: 0x00013098
	private void OnDisable()
	{
		this.scrollButtonGroup.SetParent(this.butttonGroup.transform);
		this.scrollButtonGroup.anchoredPosition = Vector2.zero;
		this.recentButtonNext.gameObject.SetActive(false);
		this.recentButtonPrevious.gameObject.SetActive(false);
		this.TrackingScrollButtonGroup.dad = false;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00014F01 File Offset: 0x00013101
	[CompilerGenerated]
	private void zr()
	{
		this.scrollRect.czu = true;
		this.OnPrevButtonClick();
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00014F15 File Offset: 0x00013115
	[CompilerGenerated]
	private void zs()
	{
		this.scrollRect.czu = true;
		this.zo();
	}

	// Token: 0x0400042B RID: 1067
	private const string cly = "RecentViewArea";

	// Token: 0x0400042C RID: 1068
	[SerializeField]
	private GameObject ContentView;

	// Token: 0x0400042D RID: 1069
	[SerializeField]
	private GameObject EmptyView;

	// Token: 0x0400042E RID: 1070
	[SerializeField]
	private ItemRecent[] gameItems;

	// Token: 0x0400042F RID: 1071
	[SerializeField]
	private RectTransform contentRectTransform;

	// Token: 0x04000430 RID: 1072
	[SerializeField]
	private ViewContent contentView;

	// Token: 0x04000431 RID: 1073
	[SerializeField]
	private RecentViewArea.RecentViewAreaSetting recentViewAreaSetting;

	// Token: 0x04000432 RID: 1074
	[SerializeField]
	private NestedVerticalScrollRect scrollRect;

	// Token: 0x04000433 RID: 1075
	[SerializeField]
	private RectTransform butttonGroup;

	// Token: 0x04000434 RID: 1076
	[SerializeField]
	private RectTransform scrollButtonGroup;

	// Token: 0x04000435 RID: 1077
	[SerializeField]
	private ScrollButtonGroup TrackingScrollButtonGroup;

	// Token: 0x04000436 RID: 1078
	[SerializeField]
	private CustomButton recentButtonNext;

	// Token: 0x04000437 RID: 1079
	[SerializeField]
	private CustomButton recentButtonPrevious;

	// Token: 0x04000438 RID: 1080
	private Vector2 clz;

	// Token: 0x04000439 RID: 1081
	private bool cma;

	// Token: 0x020000AE RID: 174
	[Serializable]
	public class RecentViewAreaSetting
	{
		// Token: 0x0400043A RID: 1082
		[Header("Default Settings")]
		public float DEFAULT_WIDTH_CONTENT = 2200f;

		// Token: 0x0400043B RID: 1083
		public float DEFAULT_WIDTH_ITEM = 344f;

		// Token: 0x0400043C RID: 1084
		[Header("Layout Settings")]
		public float SPACING = 32f;

		// Token: 0x0400043D RID: 1085
		public float PADDING = 138f;
	}

	// Token: 0x020000AF RID: 175
	[CompilerGenerated]
	private sealed class dc
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00014F5D File Offset: 0x0001315D
		internal void zf()
		{
			this.clu.scrollRect.horizontalNormalizedPosition = this.clv;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014F5D File Offset: 0x0001315D
		internal void exg()
		{
			this.clu.scrollRect.horizontalNormalizedPosition = this.clv;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014F5D File Offset: 0x0001315D
		internal void hsd()
		{
			this.clu.scrollRect.horizontalNormalizedPosition = this.clv;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014F5D File Offset: 0x0001315D
		internal void njt()
		{
			this.clu.scrollRect.horizontalNormalizedPosition = this.clv;
		}

		// Token: 0x0400043E RID: 1086
		public RecentViewArea clu;

		// Token: 0x0400043F RID: 1087
		public float clv;
	}

	// Token: 0x020000B0 RID: 176
	[CompilerGenerated]
	private sealed class dd
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00014F75 File Offset: 0x00013175
		internal void zg()
		{
			this.clw.scrollRect.horizontalNormalizedPosition = this.clx;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014F75 File Offset: 0x00013175
		internal void dqp()
		{
			this.clw.scrollRect.horizontalNormalizedPosition = this.clx;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00014F75 File Offset: 0x00013175
		internal void isy()
		{
			this.clw.scrollRect.horizontalNormalizedPosition = this.clx;
		}

		// Token: 0x04000440 RID: 1088
		public RecentViewArea clw;

		// Token: 0x04000441 RID: 1089
		public float clx;
	}
}
