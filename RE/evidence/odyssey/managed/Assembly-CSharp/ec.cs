using System;

// Token: 0x020000F6 RID: 246
public struct ec
{
	// Token: 0x06000672 RID: 1650 RVA: 0x0001CA7E File Offset: 0x0001AC7E
	public ec(string a, params string[] filterExtensions)
	{
		this.cti = a;
		this.ctj = filterExtensions;
	}

	// Token: 0x04000592 RID: 1426
	public string cti;

	// Token: 0x04000593 RID: 1427
	public string[] ctj;
}
