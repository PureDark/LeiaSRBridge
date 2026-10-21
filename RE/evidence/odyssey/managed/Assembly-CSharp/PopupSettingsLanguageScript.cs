using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003A RID: 58
public class PopupSettingsLanguageScript : MonoBehaviour
{
	// Token: 0x06000148 RID: 328 RVA: 0x00007A07 File Offset: 0x00005C07
	private void Start()
	{
		LanguageHelper.byo += this.jr;
		this.jr();
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00007A20 File Offset: 0x00005C20
	private void jr()
	{
		this.cursorTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ok());
		this.cursorBodyText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ol());
		this.shortcutTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.om());
		this.shortcutBodyText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.on());
		this._3dModeFieldText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oo());
		this._OverlayFieldText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.op());
		this._3dDepthNegFieldText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oq());
		this._3dDepthPosFieldText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.or());
		this._3dPopoutNegFieldText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.os());
		this._3dPopoutPosFieldText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ot());
		this.autoRunText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ou());
		this.convertVideoto3DText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ov());
		this.manualConvertText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ow());
		this.autoConvStandardText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ox()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oy() + LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oz()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pa();
		this.autoConvSplitText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ox()) + (bn.cyp.cyt ? (<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.oy() + LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pb()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pa()) : <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		this.autoGameScanText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pc());
		this.btnCloseText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nw());
		this.btnResetText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pd());
		this.btnApplyText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pe());
		this.copyrightText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pf());
		this.languageTitleText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pg());
	}

	// Token: 0x04000128 RID: 296
	public Text cursorTitleText;

	// Token: 0x04000129 RID: 297
	public Text cursorBodyText;

	// Token: 0x0400012A RID: 298
	public Text shortcutTitleText;

	// Token: 0x0400012B RID: 299
	public Text shortcutBodyText;

	// Token: 0x0400012C RID: 300
	public Text _3dModeFieldText;

	// Token: 0x0400012D RID: 301
	public Text _OverlayFieldText;

	// Token: 0x0400012E RID: 302
	public Text _3dDepthNegFieldText;

	// Token: 0x0400012F RID: 303
	public Text _3dDepthPosFieldText;

	// Token: 0x04000130 RID: 304
	public Text _3dPopoutNegFieldText;

	// Token: 0x04000131 RID: 305
	public Text _3dPopoutPosFieldText;

	// Token: 0x04000132 RID: 306
	public Text convertVideoto3DText;

	// Token: 0x04000133 RID: 307
	public Text manualConvertText;

	// Token: 0x04000134 RID: 308
	public Text autoConvStandardText;

	// Token: 0x04000135 RID: 309
	public Text autoConvSplitText;

	// Token: 0x04000136 RID: 310
	public Text autoRunText;

	// Token: 0x04000137 RID: 311
	public Text autoGameScanText;

	// Token: 0x04000138 RID: 312
	public Text btnCloseText;

	// Token: 0x04000139 RID: 313
	public Text btnResetText;

	// Token: 0x0400013A RID: 314
	public Text btnApplyText;

	// Token: 0x0400013B RID: 315
	public Text copyrightText;

	// Token: 0x0400013C RID: 316
	public Text languageTitleText;
}
