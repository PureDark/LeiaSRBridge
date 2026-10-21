using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class dt<a>
{
	// Token: 0x06000616 RID: 1558 RVA: 0x0001B9A4 File Offset: 0x00019BA4
	public void bib(a a)
	{
		this.crm[this.crk] = a;
		this.crk++;
		if (this.crk == this.crm.Length)
		{
			this.crk = 0;
		}
		this.crl = Mathf.Min(this.crm.Length, this.crl + 1);
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0001BA04 File Offset: 0x00019C04
	public void jmz()
	{
		for (int i = 0; i < this.crm.Length; i++)
		{
			this.crm[i] = default(a);
		}
		this.crk = 0;
	}

	// Token: 0x17000081 RID: 129
	public a this[int position]
	{
		get
		{
			return this.crm[this.bie(position)];
		}
		set
		{
			this.crm[this.bie(position)] = value;
		}
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0001BA55 File Offset: 0x00019C55
	public dt(int a)
	{
		this.crm = new a[a];
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0001BA69 File Offset: 0x00019C69
	private int bie(int a)
	{
		if (a >= this.crm.Length)
		{
			a -= this.crm.Length;
		}
		return a;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0001BA88 File Offset: 0x00019C88
	public a bic()
	{
		if (this.crl > 0)
		{
			a result = this.crm[this.crk];
			this.crk--;
			if (this.crk == -1)
			{
				this.crk = this.crm.Length - 1;
			}
			this.crl--;
			return result;
		}
		return default(a);
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0001BB04 File Offset: 0x00019D04
	public void bid()
	{
		for (int i = 0; i < this.crm.Length; i++)
		{
			this.crm[i] = default(a);
		}
		this.crk = 0;
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001BB40 File Offset: 0x00019D40
	public int dcr
	{
		get
		{
			return this.crl;
		}
	}

	// Token: 0x04000553 RID: 1363
	private int crk;

	// Token: 0x04000554 RID: 1364
	private int crl;

	// Token: 0x04000555 RID: 1365
	private readonly a[] crm;
}
