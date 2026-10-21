using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Coffee.UIEffects;
using NonObfuscatable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200009C RID: 156
public class ItemRecent : BaseGameItem
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060003BD RID: 957 RVA: 0x0001273F File Offset: 0x0001093F
	// (set) Token: 0x060003BE RID: 958 RVA: 0x00012747 File Offset: 0x00010947
	public bool day
	{
		get
		{
			return this.cka;
		}
		set
		{
			UnityAction<int> unityAction = this.ckd;
			if (unityAction != null)
			{
				unityAction.Invoke(this.ciu);
			}
			this.cka = !this.cka;
			this.wp(this.cka);
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0001277B File Offset: 0x0001097B
	public void wm(int a, UnityAction<int, PointerEventData> b, UnityAction<int> c)
	{
		base.ui(a, b);
		this.ckd = c;
		this.wn();
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00012794 File Offset: 0x00010994
	private void wn()
	{
		if (base.dao != null)
		{
			base.dao.OnEventStartHover.RemoveListener(new UnityAction(this.wq));
			base.dao.OnEventStartHover.AddListener(new UnityAction(this.wq));
			base.dao.OnEventEndHover.RemoveListener(new UnityAction(this.wr));
			base.dao.OnEventEndHover.AddListener(new UnityAction(this.wr));
		}
		this.wr();
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00012828 File Offset: 0x00010A28
	public void wo(Game a)
	{
		ItemRecent.cv cv = new ItemRecent.cv();
		cv.cjw = a;
		GameItemReference gameItemReference = this.reference;
		gameItemReference.transform.name = cv.cjw.GameInfo.Title;
		cv.cjx = gameItemReference.daf;
		cv.cjy = gameItemReference.dag;
		cv.cjz = gameItemReference.dah;
		Image image = gameItemReference.dai;
		Image image2 = gameItemReference.daj;
		Component component = this.reference.dak;
		Image image3 = this.reference.dal;
		string backgroundImage = cv.cjw.R_Thumbnail.BackgroundImage;
		string charactorImage = cv.cjw.R_Thumbnail.CharactorImage;
		string logoImage = cv.cjw.R_Thumbnail.LogoImage;
		string allInOneImage = cv.cjw.R_Thumbnail.AllInOneImage;
		this.cka = cv.cjw.GameInfo.FavoritedByUser;
		if (cv.cjx != null && this.Default_Thumbnail != null)
		{
			cv.cjx.sprite = this.Default_Thumbnail;
		}
		if (image3 != null)
		{
			image3.sprite = ((cv.cjw.GameInfo.cyn > DateTime.MinValue) ? this.ItemSprites.playedSprite : this.ItemSprites.installedSprite);
		}
		CustomButton customButton = gameItemReference.dam;
		bn.cyp.cyq.GetGameSprite(allInOneImage, new Action<Sprite>(cv.wg), null);
		if (image != null)
		{
			if (cv.cjw.GameInfo.Tier == GameTier.Ultimate.ToString())
			{
				cv.cjy.gameObject.SetActive(true);
				cv.cjz.gameObject.SetActive(true);
				image.gameObject.SetActive(true);
				customButton.popOutDistance3 = this.ckc;
				customButton.popOutDistance4 = this.ckc;
				bn.cyp.cyq.GetGameSprite(charactorImage, new Action<Sprite>(cv.wh), null);
				bn.cyp.cyq.GetGameSprite(logoImage, new Action<Sprite>(cv.wi), null);
				bn.cyp.cyq.GetGameSprite(backgroundImage, new Action<Sprite>(cv.wj), null);
			}
			else
			{
				cv.cjy.gameObject.SetActive(false);
				cv.cjz.gameObject.SetActive(false);
				image.gameObject.SetActive(false);
				customButton.useLayerEffect = false;
				customButton.popOutDistance3 = this.ckb;
				customButton.popOutDistance4 = this.ckb;
			}
		}
		bool active = !cv.cjw.GameInfo.DiscoveredByUser && cv.cjw.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rf();
		if (image2)
		{
			image2.gameObject.SetActive(active);
		}
		component.gameObject.SetActive(this.cka);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00012B38 File Offset: 0x00010D38
	public void wp(bool a)
	{
		this.cka = a;
		this.reference.dak.gameObject.SetActive(a);
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00012B57 File Offset: 0x00010D57
	private void wq()
	{
		if (this.UiEffect == null || base.dao.viewStatus != CustomButton.ck.cgt || !base.dao.isEnabled)
		{
			return;
		}
		this.UiEffect.enabled = true;
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00012B8E File Offset: 0x00010D8E
	private void wr()
	{
		if (this.UiEffect == null)
		{
			return;
		}
		this.UiEffect.enabled = false;
	}

	// Token: 0x040003C8 RID: 968
	[SerializeField]
	private GameItemReference reference;

	// Token: 0x040003C9 RID: 969
	[SerializeField]
	private ItemRecent.ItemRecentSprite ItemSprites;

	// Token: 0x040003CA RID: 970
	[SerializeField]
	private UIEffect UiEffect;

	// Token: 0x040003CB RID: 971
	[SerializeField]
	private Sprite Default_Thumbnail;

	// Token: 0x040003CC RID: 972
	private bool cka;

	// Token: 0x040003CD RID: 973
	private float ckb = -70f;

	// Token: 0x040003CE RID: 974
	private float ckc = -170f;

	// Token: 0x040003CF RID: 975
	protected UnityAction<int> ckd;

	// Token: 0x0200009D RID: 157
	[Serializable]
	public class ItemRecentSprite
	{
		// Token: 0x040003D0 RID: 976
		[Header("Status Sprite")]
		public Sprite installedSprite;

		// Token: 0x040003D1 RID: 977
		public Sprite playedSprite;

		// Token: 0x040003D2 RID: 978
		public Sprite upcomingSprite;
	}

	// Token: 0x0200009E RID: 158
	[CompilerGenerated]
	private sealed class cv
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00012BCC File Offset: 0x00010DCC
		internal void wg(Sprite a)
		{
			string allInOneImage = this.cjw.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
			this.cjx.sprite = a;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012C08 File Offset: 0x00010E08
		internal void wh(Sprite a)
		{
			this.cjy.sprite = a;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012C16 File Offset: 0x00010E16
		internal void wi(Sprite a)
		{
			this.cjz.sprite = a;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00012C24 File Offset: 0x00010E24
		internal void mgg(Sprite a)
		{
			this.cjx.sprite = a;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012C24 File Offset: 0x00010E24
		internal void wj(Sprite a)
		{
			this.cjx.sprite = a;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012C16 File Offset: 0x00010E16
		internal void ims(Sprite a)
		{
			this.cjz.sprite = a;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012C24 File Offset: 0x00010E24
		internal void kid(Sprite a)
		{
			this.cjx.sprite = a;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012C16 File Offset: 0x00010E16
		internal void nzm(Sprite a)
		{
			this.cjz.sprite = a;
		}

		// Token: 0x040003D3 RID: 979
		public Game cjw;

		// Token: 0x040003D4 RID: 980
		public Image cjx;

		// Token: 0x040003D5 RID: 981
		public Image cjy;

		// Token: 0x040003D6 RID: 982
		public Image cjz;
	}
}
