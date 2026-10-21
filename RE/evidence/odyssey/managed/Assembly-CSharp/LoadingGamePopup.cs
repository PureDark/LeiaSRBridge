using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000BE RID: 190
public class LoadingGamePopup : MonoBehaviour
{
	// Token: 0x060004EF RID: 1263 RVA: 0x00017550 File Offset: 0x00015750
	private void Start()
	{
		LanguageHelper.byo += this.bbr;
		this.bbr();
		this.cancelButton.onClick.AddListener(new UnityAction(this.bbs));
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00017585 File Offset: 0x00015785
	private void bbr()
	{
		this.loadingText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ni());
		this.buttonText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj());
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x000175B4 File Offset: 0x000157B4
	private void Update()
	{
		if (this.cnx)
		{
			return;
		}
		if ((!LoadingGamePopup.gameStarted && this.cny > 90f) || LoadingGamePopup.gameStarted)
		{
			PopupManager.ClosePopup();
			this.cnx = true;
			return;
		}
		this.cny += Time.unscaledDeltaTime;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00017604 File Offset: 0x00015804
	private void OnEnable()
	{
		this.cny = 0f;
		LoadingGamePopup.gameStarted = false;
		this.cnx = false;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001762A File Offset: 0x0001582A
	[CompilerGenerated]
	private void bbs()
	{
		Odyssey3DHubIPCManager.SendMessage(Odyssey3DHubIPCManager.c.bsg, Odyssey3DHubIPCManager.d.bso, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		PopupManager.OpenPopup(this.infomationPopup);
	}

	// Token: 0x040004AB RID: 1195
	public GameObject infomationPopup;

	// Token: 0x040004AC RID: 1196
	private static string cnw = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xm();

	// Token: 0x040004AD RID: 1197
	public Text loadingText;

	// Token: 0x040004AE RID: 1198
	public Text buttonText;

	// Token: 0x040004AF RID: 1199
	public CustomButton cancelButton;

	// Token: 0x040004B0 RID: 1200
	public Game game;

	// Token: 0x040004B1 RID: 1201
	public static bool gameStarted;

	// Token: 0x040004B2 RID: 1202
	private bool cnx;

	// Token: 0x040004B3 RID: 1203
	private float cny;

	// Token: 0x040004B4 RID: 1204
	private const float cnz = 90f;
}
