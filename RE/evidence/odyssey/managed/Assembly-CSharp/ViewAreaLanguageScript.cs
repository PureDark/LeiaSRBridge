using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000040 RID: 64
public class ViewAreaLanguageScript : MonoBehaviour
{
	// Token: 0x0600015A RID: 346 RVA: 0x00007FFC File Offset: 0x000061FC
	private void Start()
	{
		LanguageHelper.byo += this.jx;
		this.jx();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00008018 File Offset: 0x00006218
	private void jx()
	{
		this.allFilterText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pw());
		this.installedFilterText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.px());
		this.favoritesFilterText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.py());
		this.support3DFilterText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pz());
		this.upcomingFilterText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ne());
		this.recentText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qa());
		this.legacyTabText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qb());
		this.legacyRecentText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qc());
		this.installedFilterTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qd());
		this.favoritesFilterTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qe());
		this.support3DFilterTooltip.text = string.Concat(new string[]
		{
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.na()),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(),
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nc()),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(),
			LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nd())
		});
		this.upcomingFilterTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nf());
		this.addFavoriteText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qf());
		this.removeFavoriteText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qg());
	}

	// Token: 0x0400015A RID: 346
	public Text allFilterText;

	// Token: 0x0400015B RID: 347
	public Text installedFilterText;

	// Token: 0x0400015C RID: 348
	public Text favoritesFilterText;

	// Token: 0x0400015D RID: 349
	public Text support3DFilterText;

	// Token: 0x0400015E RID: 350
	public Text upcomingFilterText;

	// Token: 0x0400015F RID: 351
	public Text recentText;

	// Token: 0x04000160 RID: 352
	public Text legacyTabText;

	// Token: 0x04000161 RID: 353
	public Text legacyRecentText;

	// Token: 0x04000162 RID: 354
	public Text installedFilterTooltip;

	// Token: 0x04000163 RID: 355
	public Text favoritesFilterTooltip;

	// Token: 0x04000164 RID: 356
	public Text support3DFilterTooltip;

	// Token: 0x04000165 RID: 357
	public Text upcomingFilterTooltip;

	// Token: 0x04000166 RID: 358
	public Text addFavoriteText;

	// Token: 0x04000167 RID: 359
	public Text removeFavoriteText;
}
