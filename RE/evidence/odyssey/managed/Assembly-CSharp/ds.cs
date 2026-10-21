using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class ds
{
	// Token: 0x06000606 RID: 1542 RVA: 0x0001B758 File Offset: 0x00019958
	public void bhy(float a)
	{
		float num = (float)this.crj.dcr;
		int num2 = 0;
		while ((float)num2 < num)
		{
			dt<float> dt = this.crj;
			int position = num2;
			dt[position] += a;
			num2++;
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0001B798 File Offset: 0x00019998
	public void dtd(float a)
	{
		float num = (float)this.crj.dcr;
		int num2 = 0;
		while ((float)num2 < num)
		{
			dt<float> dt = this.crj;
			int position = num2;
			dt[position] += a;
			num2++;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001B8DA File Offset: 0x00019ADA
	// (set) Token: 0x06000608 RID: 1544 RVA: 0x0001B7D8 File Offset: 0x000199D8
	public int dcp
	{
		get
		{
			return this.cri;
		}
		private set
		{
			this.cri = Mathf.Max(value, 1);
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0001B7E8 File Offset: 0x000199E8
	private float bhx()
	{
		float num = (float)this.crj.dcr;
		float num2 = 0f;
		int num3 = 0;
		while ((float)num3 < num)
		{
			num2 += this.crj[num3];
			num3++;
		}
		return num2 / num;
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001B827 File Offset: 0x00019A27
	public int dcq
	{
		get
		{
			return this.crj.dcr;
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0001B834 File Offset: 0x00019A34
	private float krf()
	{
		float num = (float)this.crj.dcr;
		float num2 = -5561f;
		int num3 = 0;
		while ((float)num3 < num)
		{
			num2 += this.crj[num3];
			num3++;
		}
		return num2 / num;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0001B873 File Offset: 0x00019A73
	public void esn()
	{
		this.crj.bid();
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0001B880 File Offset: 0x00019A80
	private float hhh()
	{
		float num = (float)this.crj.dcr;
		float num2 = 1978f;
		int num3 = 0;
		while ((float)num3 < num)
		{
			num2 += this.crj[num3];
			num3++;
		}
		return num2 / num;
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0001B873 File Offset: 0x00019A73
	public void epv()
	{
		this.crj.bid();
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0001B8BF File Offset: 0x00019ABF
	public ds(int a)
	{
		this.dcp = a;
		this.crj = new dt<float>(a);
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0001B873 File Offset: 0x00019A73
	public void bhz()
	{
		this.crj.bid();
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0001B8E4 File Offset: 0x00019AE4
	private float jle()
	{
		float num = (float)this.crj.dcr;
		float num2 = -6178f;
		int num3 = 0;
		while ((float)num3 < num)
		{
			num2 += this.crj[num3];
			num3++;
		}
		return num2 / num;
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001B923 File Offset: 0x00019B23
	public float dco
	{
		get
		{
			return this.crh;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0001B92B File Offset: 0x00019B2B
	public void bhw(float a)
	{
		this.crj.bib(a);
		if (this.crj.dcr > this.dcp)
		{
			this.crj.bic();
		}
		this.crh = this.bhx();
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0001B964 File Offset: 0x00019B64
	private float iuu()
	{
		float num = (float)this.crj.dcr;
		float num2 = 5449f;
		int num3 = 0;
		while ((float)num3 < num)
		{
			num2 += this.crj[num3];
			num3++;
		}
		return num2 / num;
	}

	// Token: 0x04000550 RID: 1360
	private float crh;

	// Token: 0x04000551 RID: 1361
	private int cri;

	// Token: 0x04000552 RID: 1362
	private readonly dt<float> crj;
}
