using System;
using System.IO;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000B8 RID: 184
public class GameInfoPopup : MonoBehaviour
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060004BE RID: 1214 RVA: 0x000164B0 File Offset: 0x000146B0
	// (set) Token: 0x060004BF RID: 1215 RVA: 0x000164B8 File Offset: 0x000146B8
	public Game cmz { get; set; }

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000164C1 File Offset: 0x000146C1
	// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000164CC File Offset: 0x000146CC
	public bool dbl
	{
		get
		{
			return this.cnb;
		}
		set
		{
			this.cnb = !this.cnb;
			this.imgFavorite.sprite = (this.cnb ? this.spriteFavorite : this.spriteNormal);
			this.cmz.GameInfo.FavoritedByUser = this.cnb;
			bn.cyp.cyq.SerializeData();
			ViewAreaController.bah(this.cmz, this.cnb);
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00016540 File Offset: 0x00014740
	private void Start()
	{
		this.changePathButton.onClick.AddListener(new UnityAction(this.OnChangePathButtonClicked));
		this.unregisterButton.onClick.AddListener(new UnityAction(this.bbf));
		this.saveButton.onClick.AddListener(new UnityAction(this.bbe));
		this.playGameButton.onClick.AddListener(new UnityAction(this.OnPlayGameButtonClicked));
		this.closeButton.onClick.AddListener(new UnityAction(this.bbg));
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x000165DC File Offset: 0x000147DC
	private void OnEnable()
	{
		bd.ih(GameInfoPopup.cmy, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ws());
		if (this.cmz == null)
		{
			return;
		}
		this.Initialize();
		this.cna = base.gameObject.GetComponentInParent<LayeredPopupBehaviour>();
		this.cna.pe();
		this.cmz.GameInfo.DiscoveredByUser = true;
		bn.cyp.cyq.SerializeData();
		if (this.cnc == null)
		{
			Transform transform = base.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wt());
			this.cnc = ((transform != null) ? transform.gameObject : null);
		}
		if (this.cnc != null)
		{
			this.cnc.gameObject.SetActive(true);
		}
		this.bbh();
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00016698 File Offset: 0x00014898
	private void Initialize()
	{
		if (this.cmz != null)
		{
			this.cmz.GameInfo.ExePathCache = this.cmz.GameInfo.ExePath;
			this.txtGameInfo.text = this.cmz.GameInfo.kd(this.cmz.GameInfo.Introduction);
			if (this.cmz.GameInfo.IsRegistered)
			{
				this.txtGamePath.text = this.cmz.GameInfo.ExePath;
			}
			else
			{
				this.txtGamePath.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wu());
			}
			this.txtGameTitle.text = this.cmz.GameInfo.Title;
			this.txtPlatform.text = this.cmz.GameInfo.Platform;
			this.txtGenre.text = this.cmz.GameInfo.kd(this.cmz.GameInfo.Genre);
			this.txtAgeRating.text = this.cmz.GameInfo.AgeRating;
			this.txtDeveloper.text = this.cmz.GameInfo.Developer;
			this.txtPublisher.text = this.cmz.GameInfo.Publisher;
			string allInOneImage = this.cmz.I_Image.AllInOneImage;
			bn.cyp.cyq.GetGameSprite(allInOneImage, new Action<Sprite>(this.bbi), null);
			this.imgFavorite.sprite = (this.cmz.GameInfo.FavoritedByUser ? this.spriteFavorite : this.spriteNormal);
			this.goCharacter.SetActive(false);
			this.goLogo.SetActive(false);
			this.saveButton.isEnabled = false;
			this.saveButton.sc();
			this.favoriteButton.OnEventClick.RemoveAllListeners();
			this.favoriteButton.OnEventClick.AddListener(new UnityAction<PointerEventData>(this.bbj));
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0000238E File Offset: 0x0000058E
	public void bbd(GameObject a)
	{
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x000168A8 File Offset: 0x00014AA8
	private void OnChangePathButtonClicked()
	{
		bb.hk(z.bxo, ba.bye, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.p());
		ec[] c = new ec[]
		{
			new ec(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wv(), new string[]
			{
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ha()
			})
		};
		Cursor.visible = true;
		string[] array = ed.bka(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ww(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), c, false);
		Cursor.visible = false;
		bl.jb();
		if (!string.IsNullOrEmpty(array[0]))
		{
			this.txtGamePath.text = array[0];
			this.cmz.GameInfo.ExePath = this.txtGamePath.text;
			this.saveButton.isEnabled = true;
			this.saveButton.sc();
			return;
		}
		bd.id(GameInfoPopup.cmy, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wy());
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0001696C File Offset: 0x00014B6C
	private void bbe()
	{
		this.cmz.GameInfo.ExePath = this.txtGamePath.text;
		if (File.Exists(this.cmz.GameInfo.ExePath))
		{
			this.cmz.GameInfo.IsRegistered = true;
			this.cmz.GameInfo.UnregisteredByUser = false;
			this.cmz.GameInfo.cyo = new DateTime?(DateTime.UtcNow);
		}
		else
		{
			this.cmz.GameInfo.IsRegistered = false;
			this.cmz.GameInfo.LastRegisteredTimeUTC = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		}
		bn.cyp.cyq.SerializeData();
		MainArea.dbb.InstantiateScene();
		PopupManager.ClosePopup();
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00016A2D File Offset: 0x00014C2D
	private void bbf()
	{
		this.unregisterGamePopup.GetComponent<UnregisterGamePopup>().game = this.cmz;
		this.unregisterGamePopup.GetComponent<UnregisterGamePopup>().gameInfoPopup = base.gameObject;
		PopupManager.OpenPopup(this.unregisterGamePopup);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00016A68 File Offset: 0x00014C68
	private void bbg()
	{
		if (this.cnc != null)
		{
			this.cnc.gameObject.SetActive(false);
		}
		if (this.saveButton.isEnabled)
		{
			PopupManager.OpenPopup(PopupManager.CreateTwoButtonPopup(LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wz()), LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pj()), LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj()), new UnityAction(this.bbk), new UnityAction(this.bbl)));
			return;
		}
		PopupManager.ClosePopup();
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00016AE7 File Offset: 0x00014CE7
	private void OnPlayGameButtonClicked()
	{
		bd.ih(GameInfoPopup.cmy, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xa());
		bn.cyp.cyq.LaunchGame(this.cmz);
		PopupManager.OpenPopup(this.loadingGamePopup);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00016B18 File Offset: 0x00014D18
	private void bbh()
	{
		if (this.changePathButton != null)
		{
			this.changePathButton.ry(CustomButton.ck.cgt);
		}
		if (this.unregisterButton != null)
		{
			this.unregisterButton.ry(CustomButton.ck.cgt);
		}
		if (this.saveButton != null)
		{
			this.saveButton.ry(CustomButton.ck.cgt);
		}
		if (this.playGameButton != null)
		{
			this.playGameButton.ry(CustomButton.ck.cgt);
		}
		if (this.closeButton != null)
		{
			this.closeButton.ry(CustomButton.ck.cgt);
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00016BB3 File Offset: 0x00014DB3
	[CompilerGenerated]
	private void bbi(Sprite a)
	{
		this.imgBackgroundImage.sprite = a;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00016BC1 File Offset: 0x00014DC1
	[CompilerGenerated]
	private void bbj(PointerEventData a)
	{
		this.dbl = !this.cnb;
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00016BD2 File Offset: 0x00014DD2
	[CompilerGenerated]
	private void bbk()
	{
		this.bbe();
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00016BDA File Offset: 0x00014DDA
	[CompilerGenerated]
	private void bbl()
	{
		bn.cyp.cyq.ResetDetails(this.cmz);
		PopupManager.ClosePopup();
	}

	// Token: 0x04000472 RID: 1138
	private static readonly string cmy = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xb();

	// Token: 0x04000473 RID: 1139
	public Text txtGameTitle;

	// Token: 0x04000474 RID: 1140
	public Text txtGamePath;

	// Token: 0x04000475 RID: 1141
	public Text txtGameInfo;

	// Token: 0x04000476 RID: 1142
	public Text txtPlatform;

	// Token: 0x04000477 RID: 1143
	public Text txtGenre;

	// Token: 0x04000478 RID: 1144
	public Text txtAgeRating;

	// Token: 0x04000479 RID: 1145
	public Text txtDeveloper;

	// Token: 0x0400047A RID: 1146
	public Text txtPublisher;

	// Token: 0x0400047B RID: 1147
	public Image imgBackgroundImage;

	// Token: 0x0400047C RID: 1148
	public Image imgFavorite;

	// Token: 0x0400047D RID: 1149
	public GameObject goCharacter;

	// Token: 0x0400047E RID: 1150
	public GameObject goLogo;

	// Token: 0x0400047F RID: 1151
	public CustomButton changePathButton;

	// Token: 0x04000480 RID: 1152
	public CustomButton closeButton;

	// Token: 0x04000481 RID: 1153
	public CustomButton unregisterButton;

	// Token: 0x04000482 RID: 1154
	public CustomButton saveButton;

	// Token: 0x04000483 RID: 1155
	public CustomButton playGameButton;

	// Token: 0x04000484 RID: 1156
	public GameObject unregisterGamePopup;

	// Token: 0x04000485 RID: 1157
	public GameObject loadingGamePopup;

	// Token: 0x04000486 RID: 1158
	public Sprite spriteFavorite;

	// Token: 0x04000487 RID: 1159
	public Sprite spriteNormal;

	// Token: 0x04000488 RID: 1160
	public EffectCustomButton favoriteButton;

	// Token: 0x0400048A RID: 1162
	private LayeredPopupBehaviour cna;

	// Token: 0x0400048B RID: 1163
	private bool cnb;

	// Token: 0x0400048C RID: 1164
	private GameObject cnc;
}
