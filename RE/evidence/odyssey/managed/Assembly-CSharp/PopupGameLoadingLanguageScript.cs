using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000036 RID: 54
public class PopupGameLoadingLanguageScript : MonoBehaviour
{
	// Token: 0x0600013C RID: 316 RVA: 0x0000770D File Offset: 0x0000590D
	private void Start()
	{
		LanguageHelper.byo += this.jn;
		this.jn();
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00007726 File Offset: 0x00005926
	private void jn()
	{
		this.message.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ni());
		this.btnText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj());
	}

	// Token: 0x0400010A RID: 266
	public Text message;

	// Token: 0x0400010B RID: 267
	public Text btnText;
}
