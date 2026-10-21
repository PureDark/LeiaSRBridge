using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003C RID: 60
public class PopupVersionInfoLanguageScript : MonoBehaviour
{
	// Token: 0x0600014E RID: 334 RVA: 0x00007CAC File Offset: 0x00005EAC
	private void Start()
	{
		LanguageHelper.byo += this.jt;
		this.jt();
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007CC8 File Offset: 0x00005EC8
	private void jt()
	{
		this.title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pk());
		this.version.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pl()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pm() + bn.cyp.cyu.cze;
		this.gameData.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pn() + bn.cyp.cyu.czi;
		this.gameLauncher.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.po() + bn.cyp.cyu.czh;
		this.odyssey3DPlayer.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pp() + bn.cyp.cyu.czf;
		this.leiaSR.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pq() + bn.cyp.cyu.czg;
		this.description.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pr());
		this.btnCloseText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nw());
		if (this.popup != null)
		{
			this.popup.bdx();
		}
	}

	// Token: 0x04000140 RID: 320
	public Text title;

	// Token: 0x04000141 RID: 321
	public Text version;

	// Token: 0x04000142 RID: 322
	public Text gameData;

	// Token: 0x04000143 RID: 323
	public Text gameLauncher;

	// Token: 0x04000144 RID: 324
	public Text odyssey3DPlayer;

	// Token: 0x04000145 RID: 325
	public Text leiaSR;

	// Token: 0x04000146 RID: 326
	public Text description;

	// Token: 0x04000147 RID: 327
	public Text btnCloseText;

	// Token: 0x04000148 RID: 328
	[SerializeField]
	private VersionInfoPopup popup;
}
