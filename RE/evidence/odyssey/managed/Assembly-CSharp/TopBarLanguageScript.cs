using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003F RID: 63
public class TopBarLanguageScript : MonoBehaviour
{
	// Token: 0x06000157 RID: 343 RVA: 0x00007F95 File Offset: 0x00006195
	private void Start()
	{
		LanguageHelper.byo += this.jw;
		this.jw();
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00007FB0 File Offset: 0x000061B0
	private void jw()
	{
		this.searchText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pt());
		this.gameScanText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pu());
		this.gameScanTooltip.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pv());
	}

	// Token: 0x04000157 RID: 343
	public Text searchText;

	// Token: 0x04000158 RID: 344
	public Text gameScanText;

	// Token: 0x04000159 RID: 345
	public Text gameScanTooltip;
}
