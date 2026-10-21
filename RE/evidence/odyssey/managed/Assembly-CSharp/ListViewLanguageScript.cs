using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000035 RID: 53
public class ListViewLanguageScript : MonoBehaviour
{
	// Token: 0x06000139 RID: 313 RVA: 0x00007605 File Offset: 0x00005805
	private void Start()
	{
		LanguageHelper.byo += this.jm;
		this.jm();
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007620 File Offset: 0x00005820
	private void jm()
	{
		this.lv_RegTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mx());
		this.lv_RegTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.my());
		this.lv_SupTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mz());
		this.lv_SupTooltip.text = string.Concat(new string[]
		{
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.na()),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(),
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nc()),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(),
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nd())
		});
		this.lv_UpcTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ne());
		this.lv_UpcTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nf());
		this.lv_NoGamesReg.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ng());
		this.lv_NoSupGames.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nh());
	}

	// Token: 0x04000102 RID: 258
	public Text lv_RegTitle;

	// Token: 0x04000103 RID: 259
	public Text lv_SupTitle;

	// Token: 0x04000104 RID: 260
	public Text lv_UpcTitle;

	// Token: 0x04000105 RID: 261
	public Text lv_RegTooltip;

	// Token: 0x04000106 RID: 262
	public Text lv_SupTooltip;

	// Token: 0x04000107 RID: 263
	public Text lv_UpcTooltip;

	// Token: 0x04000108 RID: 264
	public Text lv_NoGamesReg;

	// Token: 0x04000109 RID: 265
	public Text lv_NoSupGames;
}
