using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};

// Token: 0x02000047 RID: 71
[Serializable]
public class MultilingualText
{
	// Token: 0x06000169 RID: 361 RVA: 0x00008320 File Offset: 0x00006520
	public void ke()
	{
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.je(), this.en_US);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp(), this.ko_KR);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ji(), this.fr_FR);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jc(), this.de_DE);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jk(), this.hu_HU);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jm(), this.it_IT);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jr(), this.pl_PL);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jt(), this.pt_PT);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jv(), this.ru_RU);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jg(), this.es_ES);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jx(), this.sv_SE);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jz(), this.tr_TR);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kb(), this.zh_CN);
		this.Texts.TryAdd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ix(), this.ja_JP);
	}

	// Token: 0x04000191 RID: 401
	public string en_US;

	// Token: 0x04000192 RID: 402
	public string ko_KR;

	// Token: 0x04000193 RID: 403
	public string fr_FR;

	// Token: 0x04000194 RID: 404
	public string de_DE;

	// Token: 0x04000195 RID: 405
	public string hu_HU;

	// Token: 0x04000196 RID: 406
	public string it_IT;

	// Token: 0x04000197 RID: 407
	public string pl_PL;

	// Token: 0x04000198 RID: 408
	public string pt_PT;

	// Token: 0x04000199 RID: 409
	public string ru_RU;

	// Token: 0x0400019A RID: 410
	public string es_ES;

	// Token: 0x0400019B RID: 411
	public string sv_SE;

	// Token: 0x0400019C RID: 412
	public string tr_TR;

	// Token: 0x0400019D RID: 413
	public string zh_CN;

	// Token: 0x0400019E RID: 414
	public string ja_JP;

	// Token: 0x0400019F RID: 415
	[NonSerialized]
	public Dictionary<string, string> Texts = new Dictionary<string, string>();
}
