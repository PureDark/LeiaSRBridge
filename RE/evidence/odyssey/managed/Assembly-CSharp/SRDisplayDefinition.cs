using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
[Serializable]
public class SRDisplayDefinition
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001B5DC File Offset: 0x000197DC
	// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0001B5E4 File Offset: 0x000197E4
	public string dcf
	{
		get
		{
			return this.cqy;
		}
		set
		{
			this.cqy = value;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001B5ED File Offset: 0x000197ED
	// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0001B5F5 File Offset: 0x000197F5
	public Vector2 dcg
	{
		get
		{
			return this.cqz;
		}
		set
		{
			this.cqz = value;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001B5FE File Offset: 0x000197FE
	// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001B606 File Offset: 0x00019806
	public float dch
	{
		get
		{
			return this.cra;
		}
		set
		{
			this.cra = value;
		}
	}

	// Token: 0x04000547 RID: 1351
	private string cqy;

	// Token: 0x04000548 RID: 1352
	private Vector2 cqz;

	// Token: 0x04000549 RID: 1353
	private float cra;
}
