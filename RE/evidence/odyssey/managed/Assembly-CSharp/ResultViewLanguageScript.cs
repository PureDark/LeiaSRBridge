using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003D RID: 61
public class ResultViewLanguageScript : MonoBehaviour
{
	// Token: 0x06000151 RID: 337 RVA: 0x00007DEB File Offset: 0x00005FEB
	private void Start()
	{
		LanguageHelper.byo += this.ju;
		this.ju();
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00007E04 File Offset: 0x00006004
	private void ju()
	{
		this.regTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mx());
		this.supTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mz());
		this.upcTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ne());
		this.regNoResultsFound.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ps());
		this.supNoResultsFound.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ps());
		this.upcNoResultsFound.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ps());
	}

	// Token: 0x04000149 RID: 329
	public Text regTitleText;

	// Token: 0x0400014A RID: 330
	public Text supTitleText;

	// Token: 0x0400014B RID: 331
	public Text upcTitleText;

	// Token: 0x0400014C RID: 332
	public Text regNoResultsFound;

	// Token: 0x0400014D RID: 333
	public Text supNoResultsFound;

	// Token: 0x0400014E RID: 334
	public Text upcNoResultsFound;
}
