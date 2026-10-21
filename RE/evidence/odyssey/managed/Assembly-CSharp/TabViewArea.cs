using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000B1 RID: 177
public class TabViewArea : ViewArea, cs, cr, cq, IScrollHandler, IEventSystemHandler
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600047D RID: 1149 RVA: 0x00014F90 File Offset: 0x00013190
	public int dbj
	{
		get
		{
			if (base.ciy)
			{
				if (this.cmd == null)
				{
					return this.cmf = 0;
				}
				if (this.cmf != this.cmd.Length)
				{
					this.cmf = this.cmd.Length;
				}
				base.us();
			}
			return this.cmf;
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00014FE4 File Offset: 0x000131E4
	protected override void InstantiateArea()
	{
		this.scrollRect.onValueChanged.RemoveAllListeners();
		if (this.cmd != null && this.cmd.Length != 0)
		{
			this.zv(true);
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.bae));
			this.zz();
			if (this.cmc == null)
			{
				this.cmc = new Dictionary<int, ItemTab>();
			}
			float width = this.itemPrefab.dap.rect.width;
			float height = this.itemPrefab.dap.rect.height;
			int num = this.cmd.Length;
			int num2 = Mathf.CeilToInt((float)num / (float)this.tabViewAreaSetting.ITEMS_PER_ROW);
			RectTransform rectTransform = this.content;
			Vector2 sizeDelta = rectTransform.sizeDelta;
			sizeDelta.y = this.tabViewAreaSetting.VERTICAL_MARGIN * 2f + (height + this.tabViewAreaSetting.PADDING) * (float)num2;
			rectTransform.sizeDelta = sizeDelta;
			int num3 = Mathf.Min(num, this.tabViewAreaSetting.dbi);
			this.cmg.x = -rectTransform.rect.width / 2f + (width + this.tabViewAreaSetting.HORIZONTAL_MARGIN * 2f);
			this.cmg.y = rectTransform.rect.height / 2f - (height / 2f + this.tabViewAreaSetting.VERTICAL_MARGIN);
			int num4 = 0;
			for (int i = 0; i < num3; i++)
			{
				this.InitItem(num4, i);
				num4++;
			}
			return;
		}
		this.zv(false);
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0001519C File Offset: 0x0001339C
	private void InitItem(int offset, int i)
	{
		try
		{
			Vector3 a = this.baa(this.itemPrefab.dap, this.tabViewAreaSetting.PADDING, (float)offset);
			ItemTab itemTab = this.un(a, this.content) as ItemTab;
			if (itemTab != null)
			{
				this.bab(itemTab, this.cmd[offset], offset);
				this.cmc.Add(offset, itemTab);
			}
		}
		catch (Exception arg)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wg(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wh(), i, arg), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wi());
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00015238 File Offset: 0x00013438
	private void zv(bool a)
	{
		this.itemContent.gameObject.SetActive(a);
		this.emptyContent.gameObject.SetActive(!a);
		CanvasGroup canvasGroup = a ? this.itemContent.GetComponent<CanvasGroup>() : this.emptyContent.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0f;
		DOTweenModuleUI.DOFade(canvasGroup, 1f, this.contentFadeDuration);
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x000152A4 File Offset: 0x000134A4
	public override void ur(Game[] a, Action<Game, PointerEventData> b)
	{
		base.ur(a, b);
		if (this.filterBar != null)
		{
			this.filterBar.cip -= this.zy;
			this.filterBar.cip += this.zy;
		}
		this.filterBar.uc();
		ViewAreaController.cmk -= this.bac;
		ViewAreaController.cmk += this.bac;
		this.contentView.cji -= this.bad;
		this.contentView.cji += this.bad;
		this.zw();
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00015358 File Offset: 0x00013558
	private void zw()
	{
		foreach (BaseFilter baseFilter in this.filterBar.Filters)
		{
			if (!(baseFilter is AllFilter))
			{
				foreach (Game a in baseFilter.bdy(this.cja).Take(30).ToArray<Game>())
				{
					this.zx(a);
				}
			}
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x000153C4 File Offset: 0x000135C4
	private void zx(Game a)
	{
		string backgroundImage = a.S_Thumbnail.BackgroundImage;
		string charactorImage = a.S_Thumbnail.CharactorImage;
		string logoImage = a.S_Thumbnail.LogoImage;
		string allInOneImage = a.S_Thumbnail.AllInOneImage;
		if (!string.IsNullOrEmpty(allInOneImage))
		{
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
		}
		if (!string.IsNullOrEmpty(charactorImage))
		{
			bn.cyp.cyq.GetGameSprite(charactorImage, null, null);
		}
		if (!string.IsNullOrEmpty(logoImage))
		{
			bn.cyp.cyq.GetGameSprite(logoImage, null, null);
		}
		if (!string.IsNullOrEmpty(backgroundImage))
		{
			bn.cyp.cyq.GetGameSprite(backgroundImage, null, null);
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001546C File Offset: 0x0001366C
	public void ul(ScrollRect a)
	{
		if (a == null || this.cmd == null)
		{
			return;
		}
		int num = Mathf.CeilToInt((float)this.cmd.Length / (float)this.tabViewAreaSetting.ITEMS_PER_ROW);
		if (num > 0)
		{
			int num2 = Mathf.RoundToInt((1f - a.verticalNormalizedPosition) * (float)(num - 1));
			num2 = Mathf.Clamp(num2, 0, num - 1);
			if (num2 != this.cmh)
			{
				this.cmh = num2;
				this.um();
				return;
			}
		}
		else
		{
			this.cmh = 0;
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x000154EC File Offset: 0x000136EC
	public void um()
	{
		if (this.cmd == null || this.cmd.Length == 0)
		{
			this.zz();
			return;
		}
		if (this.content == null || this.itemPrefab == null)
		{
			return;
		}
		int num = Mathf.CeilToInt((float)this.cmd.Length / (float)this.tabViewAreaSetting.ITEMS_PER_ROW);
		int num2 = Mathf.Max(0, this.cmh - this.tabViewAreaSetting.ROWS_BUFFER);
		int num3 = Mathf.Min(num - 1, this.cmh + this.tabViewAreaSetting.ROWS_BUFFER);
		int num4 = num2 * this.tabViewAreaSetting.ITEMS_PER_ROW;
		int num5 = Mathf.Min((num3 + 1) * this.tabViewAreaSetting.ITEMS_PER_ROW - 1, this.cmd.Length - 1);
		num4 = Mathf.Max(0, num4);
		num5 = Mathf.Min(num5, this.cmd.Length - 1);
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, ItemTab> keyValuePair in this.cmc)
		{
			if (keyValuePair.Key < num4 || keyValuePair.Key > num5)
			{
				if (keyValuePair.Value != null && this.cme != null)
				{
					ItemTab value = keyValuePair.Value;
					UnityEvent onEventEndHover = value.dao.OnEventEndHover;
					if (onEventEndHover != null)
					{
						onEventEndHover.Invoke();
					}
					value.dao.ry(CustomButton.ck.cgt);
					value.xf();
					this.cme.cms.Release(value.gameObject);
				}
				list.Add(keyValuePair.Key);
			}
		}
		foreach (int key in list)
		{
			this.cmc.Remove(key);
		}
		for (int i = num4; i <= num5; i++)
		{
			if (!this.cmc.ContainsKey(i))
			{
				int offset = i;
				this.InitItem(offset, i);
			}
		}
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00015708 File Offset: 0x00013908
	public BaseGameItem un(Vector3 a, Transform b)
	{
		if (this.cme == null)
		{
			this.cme = new de(this.itemPrefab.gameObject, b, true);
		}
		ItemTab itemTab = this.cme.bar<ItemTab>();
		itemTab.dap.anchoredPosition = a;
		return itemTab;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00015746 File Offset: 0x00013946
	private void zy(BaseFilter a)
	{
		if (a == null)
		{
			return;
		}
		this.cmi = a;
		this.cmd = a.bdy(this.cja);
		base.ut();
		this.InstantiateArea();
		base.us();
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00015780 File Offset: 0x00013980
	private void zz()
	{
		if (this.cmc == null || this.cme == null)
		{
			return;
		}
		foreach (KeyValuePair<int, ItemTab> keyValuePair in this.cmc)
		{
			if (!(keyValuePair.Value == null))
			{
				keyValuePair.Value.xf();
				this.cme.cms.Release(keyValuePair.Value.gameObject);
			}
		}
		this.cme.cms.Clear();
		this.cmc.Clear();
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00015830 File Offset: 0x00013A30
	private Vector3 baa(RectTransform a, float b, float c)
	{
		int num = Mathf.FloorToInt(c / (float)this.tabViewAreaSetting.ITEMS_PER_ROW);
		int num2 = Mathf.FloorToInt(c % (float)this.tabViewAreaSetting.ITEMS_PER_ROW);
		float width = a.rect.width;
		float height = a.rect.height;
		this.cmj.x = this.cmg.x + (width + b) * (float)num2;
		this.cmj.y = this.cmg.y - (height + b) * (float)num;
		this.cmj.z = 0f;
		return this.cmj;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x000158D5 File Offset: 0x00013AD5
	private void bab(ItemTab a, Game b, int c)
	{
		a.xb(c, new UnityAction<int, PointerEventData>(this.OnItemClick), new UnityAction<int>(this.OnFavoriteClick));
		a.xa(b);
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00015900 File Offset: 0x00013B00
	protected override void OnItemClick(int index, PointerEventData eventData)
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
		if (this.cmd == null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
			return;
		}
		int num = this.cmd.Length;
		if (index > num - 1)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vl(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
			return;
		}
		if (this.cmd[index] == null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vk(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
			return;
		}
		Action<Game, PointerEventData> cjb = this.cjb;
		if (cjb == null)
		{
			return;
		}
		cjb(this.cmd[index], eventData);
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x000159AB File Offset: 0x00013BAB
	public void OnScroll(PointerEventData eventData)
	{
		if (this.scrollRect != null)
		{
			this.scrollRect.OnScroll(eventData);
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x000159C8 File Offset: 0x00013BC8
	private void OnFavoriteClick(int index)
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
		if (this.cmd == null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
			return;
		}
		int num = this.cmd.Length;
		if (index > num - 1)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vl(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
			return;
		}
		if (this.cmd[index] != null)
		{
			bool flag = !this.cmd[index].GameInfo.FavoritedByUser;
			this.cmd[index].GameInfo.FavoritedByUser = flag;
			bn.cyp.cyq.SerializeData();
			base.ut();
			ViewAreaController.bah(this.cmd[index], flag);
			return;
		}
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vk(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wd());
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00015AA8 File Offset: 0x00013CA8
	private void bac(Game a, bool b)
	{
		if (this.cmi is FavoriteFilter)
		{
			this.zy(this.cmi);
		}
		if (base.ciy)
		{
			base.us();
			return;
		}
		if (this.cmd == null || this.cmd.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.cmd.Length; i++)
		{
			Game game = this.cmd[i];
			if (game != null && game == a)
			{
				if (this.cmc.ContainsKey(i))
				{
					this.cmc[i].wz(b);
				}
				return;
			}
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00015B34 File Offset: 0x00013D34
	private void bad(Vector2 a)
	{
		if (this.cmc == null || this.cmc.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, ItemTab> keyValuePair in this.cmc)
		{
			ItemTab value = keyValuePair.Value;
			value.dao.rv(this.contentView.vf(value.dap) ? CustomButton.ck.cgt : CustomButton.ck.cgv);
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00015BE2 File Offset: 0x00013DE2
	[CompilerGenerated]
	private void bae(Vector2 a)
	{
		this.ul(this.scrollRect);
	}

	// Token: 0x04000442 RID: 1090
	private const string cmb = "TabViewArea";

	// Token: 0x04000443 RID: 1091
	[SerializeField]
	private RectTransform content;

	// Token: 0x04000444 RID: 1092
	[SerializeField]
	private RectTransform itemContent;

	// Token: 0x04000445 RID: 1093
	[SerializeField]
	private RectTransform emptyContent;

	// Token: 0x04000446 RID: 1094
	[SerializeField]
	private FilterBar filterBar;

	// Token: 0x04000447 RID: 1095
	[SerializeField]
	private ScrollRect scrollRect;

	// Token: 0x04000448 RID: 1096
	[SerializeField]
	private ViewContent contentView;

	// Token: 0x04000449 RID: 1097
	[SerializeField]
	private ItemTab itemPrefab;

	// Token: 0x0400044A RID: 1098
	[SerializeField]
	private TabViewArea.TabViewAreaSetting tabViewAreaSetting = new TabViewArea.TabViewAreaSetting();

	// Token: 0x0400044B RID: 1099
	[SerializeField]
	private float contentFadeDuration = 0.3f;

	// Token: 0x0400044C RID: 1100
	private Dictionary<int, ItemTab> cmc;

	// Token: 0x0400044D RID: 1101
	private Game[] cmd;

	// Token: 0x0400044E RID: 1102
	private de cme;

	// Token: 0x0400044F RID: 1103
	private int cmf;

	// Token: 0x04000450 RID: 1104
	private Vector2 cmg;

	// Token: 0x04000451 RID: 1105
	private int cmh;

	// Token: 0x04000452 RID: 1106
	private BaseFilter cmi;

	// Token: 0x04000453 RID: 1107
	private Vector3 cmj;

	// Token: 0x020000B2 RID: 178
	[Serializable]
	public class TabViewAreaSetting
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00015BF0 File Offset: 0x00013DF0
		public int dbi
		{
			get
			{
				return this.ITEMS_PER_ROW * this.MAX_ROWS;
			}
		}

		// Token: 0x04000454 RID: 1108
		[Header("Grid Settings")]
		[Tooltip("Setting Max Item Per Row, must be >= 1")]
		[Min(1f)]
		public int ITEMS_PER_ROW = 6;

		// Token: 0x04000455 RID: 1109
		[Tooltip("Setting Max Row Show At Begin, must be >= 3")]
		[Min(3f)]
		public int MAX_ROWS = 5;

		// Token: 0x04000456 RID: 1110
		[Header("Layout Settings")]
		public float HORIZONTAL_MARGIN = 40f;

		// Token: 0x04000457 RID: 1111
		public float VERTICAL_MARGIN = 40f;

		// Token: 0x04000458 RID: 1112
		public float PADDING = 40f;

		// Token: 0x04000459 RID: 1113
		[Header("Advanced Settings")]
		[Tooltip("Setting Row buffer being waiting at out of bound view, must be >= 1")]
		[Min(1f)]
		public int ROWS_BUFFER = 3;
	}
}
