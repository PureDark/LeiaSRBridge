using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000039 RID: 57
public class PopupLoadingLanguageScript : MonoBehaviour
{
	// Token: 0x06000145 RID: 325 RVA: 0x000079C2 File Offset: 0x00005BC2
	private void Start()
	{
		LanguageHelper.byo += this.jq;
		this.jq();
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000079DB File Offset: 0x00005BDB
	private void jq()
	{
		this.title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oj());
		this.btnText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj());
	}

	// Token: 0x04000126 RID: 294
	public Text title;

	// Token: 0x04000127 RID: 295
	public Text btnText;
}
