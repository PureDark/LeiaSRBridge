using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Coffee.UIEffects;
using NonObfuscatable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200009F RID: 159
public class ItemTab : BaseGameItem
{
	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060003D0 RID: 976 RVA: 0x00012C32 File Offset: 0x00010E32
	public GameItemReference daz
	{
		get
		{
			return this.reference;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x00012C3A File Offset: 0x00010E3A
	// (set) Token: 0x060003D2 RID: 978 RVA: 0x00012C42 File Offset: 0x00010E42
	public bool dba
	{
		get
		{
			return this.ckk;
		}
		set
		{
			UnityAction<int> unityAction = this.ckn;
			if (unityAction != null)
			{
				unityAction.Invoke(this.ciu);
			}
			this.ckk = !this.ckk;
			this.wz(this.ckk);
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00012C76 File Offset: 0x00010E76
	public void wz(bool a)
	{
		this.ckk = a;
		this.reference.dak.gameObject.SetActive(a);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00012C98 File Offset: 0x00010E98
	public void xa(Game a)
	{
		ItemTab.cw cw = new ItemTab.cw();
		cw.cke = a;
		cw.ckg = this;
		this.xe();
		this.cko++;
		cw.ckh = this.cko;
		this.xg();
		this.daz.transform.name = cw.cke.GameInfo.Title;
		cw.ckf = this.daz.daf;
		cw.cki = this.daz.dag;
		cw.ckj = this.daz.dah;
		Image image = this.daz.dai;
		Image image2 = this.daz.daj;
		Component component = this.reference.dak;
		Image image3 = this.reference.dal;
		string backgroundImage = cw.cke.S_Thumbnail.BackgroundImage;
		string charactorImage = cw.cke.S_Thumbnail.CharactorImage;
		string logoImage = cw.cke.S_Thumbnail.LogoImage;
		string allInOneImage = cw.cke.S_Thumbnail.AllInOneImage;
		this.ckk = cw.cke.GameInfo.FavoritedByUser;
		if (cw.ckf != null && this.Default_Thumbnail != null)
		{
			cw.ckf.sprite = this.Default_Thumbnail;
		}
		CustomButton customButton = this.daz.dam;
		bn.cyp.cyq.GetGameSprite(allInOneImage, new Action<Sprite>(cw.ws), null);
		if (image3 != null)
		{
			image3.gameObject.SetActive(cw.cke.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rg());
		}
		if (image != null)
		{
			if (cw.cke.GameInfo.Tier == GameTier.Ultimate.ToString())
			{
				cw.cki.gameObject.SetActive(true);
				cw.ckj.gameObject.SetActive(true);
				image.gameObject.SetActive(true);
				customButton.popOutDistance3 = this.ckm;
				customButton.popOutDistance4 = this.ckm;
				bn.cyp.cyq.GetGameSprite(charactorImage, new Action<Sprite>(cw.wt), null);
				bn.cyp.cyq.GetGameSprite(logoImage, new Action<Sprite>(cw.wu), null);
				bn.cyp.cyq.GetGameSprite(backgroundImage, new Action<Sprite>(cw.wv), null);
			}
			else
			{
				cw.cki.gameObject.SetActive(false);
				cw.ckj.gameObject.SetActive(false);
				image.gameObject.SetActive(false);
				customButton.useLayerEffect = false;
				customButton.popOutDistance3 = this.ckl;
				customButton.popOutDistance4 = this.ckl;
			}
		}
		bool active = !cw.cke.GameInfo.DiscoveredByUser && cw.cke.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rf();
		if (image2)
		{
			image2.gameObject.SetActive(active);
		}
		component.gameObject.SetActive(this.ckk);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00012FBB File Offset: 0x000111BB
	public void xb(int a, UnityAction<int, PointerEventData> b, UnityAction<int> c)
	{
		base.ui(a, b);
		this.ckn = c;
		this.xc();
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00012FD4 File Offset: 0x000111D4
	private void xc()
	{
		if (base.dao != null)
		{
			base.dao.OnEventStartHover.RemoveListener(new UnityAction(this.xd));
			base.dao.OnEventStartHover.AddListener(new UnityAction(this.xd));
			base.dao.OnEventEndHover.RemoveListener(new UnityAction(this.xe));
			base.dao.OnEventEndHover.AddListener(new UnityAction(this.xe));
		}
		this.xe();
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00013068 File Offset: 0x00011268
	protected override void uj()
	{
		base.uj();
		Image image = this.daz.daj;
		if (image)
		{
			image.gameObject.SetActive(false);
		}
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001309B File Offset: 0x0001129B
	private void xd()
	{
		if (this.UiEffect == null || base.dao.viewStatus != CustomButton.ck.cgt || !base.dao.isEnabled)
		{
			return;
		}
		this.UiEffect.enabled = true;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x000130D2 File Offset: 0x000112D2
	private void xe()
	{
		if (this.UiEffect == null)
		{
			return;
		}
		this.UiEffect.enabled = false;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x000130EF File Offset: 0x000112EF
	public void xf()
	{
		this.cko++;
		this.xg();
		this.wz(false);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0001310C File Offset: 0x0001130C
	private void xg()
	{
		if (this.daz.daf)
		{
			this.daz.daf.sprite = null;
		}
		if (this.daz.dag)
		{
			this.daz.dag.sprite = null;
		}
		if (this.daz.dah)
		{
			this.daz.dah.sprite = null;
		}
	}

	// Token: 0x040003D7 RID: 983
	[SerializeField]
	private GameItemReference reference;

	// Token: 0x040003D8 RID: 984
	[SerializeField]
	private UIEffect UiEffect;

	// Token: 0x040003D9 RID: 985
	[SerializeField]
	private Sprite Default_Thumbnail;

	// Token: 0x040003DA RID: 986
	private bool ckk;

	// Token: 0x040003DB RID: 987
	private float ckl = -70f;

	// Token: 0x040003DC RID: 988
	private float ckm = -170f;

	// Token: 0x040003DD RID: 989
	protected UnityAction<int> ckn;

	// Token: 0x040003DE RID: 990
	private int cko;

	// Token: 0x020000A0 RID: 160
	[CompilerGenerated]
	private sealed class cw
	{
		// Token: 0x060003DD RID: 989 RVA: 0x000131A0 File Offset: 0x000113A0
		internal void wv(Sprite a)
		{
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000131CF File Offset: 0x000113CF
		internal void wt(Sprite a)
		{
			if (this.cki != null && this.ckg.cko == this.ckh)
			{
				this.cki.sprite = a;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000131CF File Offset: 0x000113CF
		internal void dxl(Sprite a)
		{
			if (this.cki != null && this.ckg.cko == this.ckh)
			{
				this.cki.sprite = a;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013200 File Offset: 0x00011400
		internal void ibd(Sprite a)
		{
			string allInOneImage = this.cke.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000131CF File Offset: 0x000113CF
		internal void knq(Sprite a)
		{
			if (this.cki != null && this.ckg.cko == this.ckh)
			{
				this.cki.sprite = a;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013260 File Offset: 0x00011460
		internal void ws(Sprite a)
		{
			string allInOneImage = this.cke.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000131A0 File Offset: 0x000113A0
		internal void fug(Sprite a)
		{
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000131CF File Offset: 0x000113CF
		internal void dfj(Sprite a)
		{
			if (this.cki != null && this.ckg.cko == this.ckh)
			{
				this.cki.sprite = a;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000132C0 File Offset: 0x000114C0
		internal void fts(Sprite a)
		{
			string allInOneImage = this.cke.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000131A0 File Offset: 0x000113A0
		internal void ngd(Sprite a)
		{
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013320 File Offset: 0x00011520
		internal void exi(Sprite a)
		{
			string allInOneImage = this.cke.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000131CF File Offset: 0x000113CF
		internal void eog(Sprite a)
		{
			if (this.cki != null && this.ckg.cko == this.ckh)
			{
				this.cki.sprite = a;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001337D File Offset: 0x0001157D
		internal void csa(Sprite a)
		{
			if (this.ckj != null && this.ckg.cko == this.ckh)
			{
				this.ckj.sprite = a;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001337D File Offset: 0x0001157D
		internal void wu(Sprite a)
		{
			if (this.ckj != null && this.ckg.cko == this.ckh)
			{
				this.ckj.sprite = a;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000133AC File Offset: 0x000115AC
		internal void byw(Sprite a)
		{
			string allInOneImage = this.cke.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, null, null);
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000131A0 File Offset: 0x000113A0
		internal void jh(Sprite a)
		{
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000131A0 File Offset: 0x000113A0
		internal void nez(Sprite a)
		{
			if (this.ckf != null && this.ckg.cko == this.ckh)
			{
				this.ckf.sprite = a;
			}
		}

		// Token: 0x040003DF RID: 991
		public Game cke;

		// Token: 0x040003E0 RID: 992
		public Image ckf;

		// Token: 0x040003E1 RID: 993
		public ItemTab ckg;

		// Token: 0x040003E2 RID: 994
		public int ckh;

		// Token: 0x040003E3 RID: 995
		public Image cki;

		// Token: 0x040003E4 RID: 996
		public Image ckj;
	}
}
