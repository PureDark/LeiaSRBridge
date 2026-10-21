using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003E RID: 62
public class ThumbnailViewLanguageScript : MonoBehaviour
{
	// Token: 0x06000154 RID: 340 RVA: 0x00007E8F File Offset: 0x0000608F
	private void Start()
	{
		LanguageHelper.byo += this.jv;
		this.jv();
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00007EA8 File Offset: 0x000060A8
	private void jv()
	{
		this.tv_RegTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mx());
		this.tv_RegTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.my());
		this.tv_SupTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mz());
		this.tv_SupTooltip.text = string.Concat(new string[]
		{
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.na()),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(),
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nc()),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(),
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nd())
		});
		this.tv_UpcTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ne());
		this.tv_UpcTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nf());
		this.tv_NoGamesReg.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ng());
		this.tv_NoSupGames.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nh());
	}

	// Token: 0x0400014F RID: 335
	public Text tv_RegTitle;

	// Token: 0x04000150 RID: 336
	public Text tv_SupTitle;

	// Token: 0x04000151 RID: 337
	public Text tv_UpcTitle;

	// Token: 0x04000152 RID: 338
	public Text tv_RegTooltip;

	// Token: 0x04000153 RID: 339
	public Text tv_SupTooltip;

	// Token: 0x04000154 RID: 340
	public Text tv_UpcTooltip;

	// Token: 0x04000155 RID: 341
	public Text tv_NoGamesReg;

	// Token: 0x04000156 RID: 342
	public Text tv_NoSupGames;
}
