using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003B RID: 59
public class PopupUnregisterGameLanguageScript : MonoBehaviour
{
	// Token: 0x0600014B RID: 331 RVA: 0x00007C30 File Offset: 0x00005E30
	private void Start()
	{
		LanguageHelper.byo += this.js;
		this.js();
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00007C4C File Offset: 0x00005E4C
	private void js()
	{
		this.message.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ph()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ky() + LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pi());
		this.okButtonText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pj());
		this.cancelButtonText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj());
	}

	// Token: 0x0400013D RID: 317
	public Text message;

	// Token: 0x0400013E RID: 318
	public Text okButtonText;

	// Token: 0x0400013F RID: 319
	public Text cancelButtonText;
}
