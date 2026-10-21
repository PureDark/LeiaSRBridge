using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000038 RID: 56
public class PopupInformationLanguageScript : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x0000788A File Offset: 0x00005A8A
	private void Start()
	{
		LanguageHelper.byo += this.jp;
		this.jp();
	}

	// Token: 0x06000143 RID: 323 RVA: 0x000078A4 File Offset: 0x00005AA4
	private void jp()
	{
		this.pathTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nx());
		this.btnChangePathText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ny());
		this.gameInfoTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nz());
		this.platformTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oa());
		this.genreTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ob());
		this.ageRatingTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oc());
		this.developerTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.od());
		this.publisherTitle.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oe());
		this.btnCloseText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nw());
		this.btnUnregisterText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.of());
		this.btnSaveText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.og());
		this.btnPlayNow.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oh());
		this.pathGuideTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oi());
	}

	// Token: 0x04000119 RID: 281
	public Text pathTitleText;

	// Token: 0x0400011A RID: 282
	public Text btnChangePathText;

	// Token: 0x0400011B RID: 283
	public Text pathGuideTooltip;

	// Token: 0x0400011C RID: 284
	public Text gameInfoTitle;

	// Token: 0x0400011D RID: 285
	public Text platformTitle;

	// Token: 0x0400011E RID: 286
	public Text genreTitle;

	// Token: 0x0400011F RID: 287
	public Text ageRatingTitle;

	// Token: 0x04000120 RID: 288
	public Text developerTitle;

	// Token: 0x04000121 RID: 289
	public Text publisherTitle;

	// Token: 0x04000122 RID: 290
	public Text btnCloseText;

	// Token: 0x04000123 RID: 291
	public Text btnUnregisterText;

	// Token: 0x04000124 RID: 292
	public Text btnSaveText;

	// Token: 0x04000125 RID: 293
	public Text btnPlayNow;
}
