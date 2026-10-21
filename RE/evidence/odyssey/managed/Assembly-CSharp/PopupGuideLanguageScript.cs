using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000037 RID: 55
public class PopupGuideLanguageScript : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x00007752 File Offset: 0x00005952
	private void Start()
	{
		LanguageHelper.byo += this.jo;
		this.jo();
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000776C File Offset: 0x0000596C
	private void jo()
	{
		this._1Title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nk());
		this._1Body.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nl());
		this._2Title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nm());
		this._2Body.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nn());
		this._3Title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.no());
		this._3Body.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.np());
		this._4Title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nq());
		this._4Body.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nr());
		this._5Title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ns());
		this._5Body.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nt());
		this._6Title.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nu());
		this._6Body.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nv());
		this.btnText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nw());
	}

	// Token: 0x0400010C RID: 268
	public Text _1Title;

	// Token: 0x0400010D RID: 269
	public Text _1Body;

	// Token: 0x0400010E RID: 270
	public Text _2Title;

	// Token: 0x0400010F RID: 271
	public Text _2Body;

	// Token: 0x04000110 RID: 272
	public Text _3Title;

	// Token: 0x04000111 RID: 273
	public Text _3Body;

	// Token: 0x04000112 RID: 274
	public Text _4Title;

	// Token: 0x04000113 RID: 275
	public Text _4Body;

	// Token: 0x04000114 RID: 276
	public Text _5Title;

	// Token: 0x04000115 RID: 277
	public Text _5Body;

	// Token: 0x04000116 RID: 278
	public Text _6Title;

	// Token: 0x04000117 RID: 279
	public Text _6Body;

	// Token: 0x04000118 RID: 280
	public Text btnText;
}
