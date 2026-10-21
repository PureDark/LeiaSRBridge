using System;
using LeiaUnity;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class LeiaDebugGUI : Singleton<LeiaDebugGUI>
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001BFAF File Offset: 0x0001A1AF
	// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001BFB7 File Offset: 0x0001A1B7
	public float dcu
	{
		get
		{
			return this.csc;
		}
		set
		{
			this.csc = value;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
	// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
	public float dcv
	{
		get
		{
			return this.csd;
		}
		set
		{
			this.csd = value;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001BFD1 File Offset: 0x0001A1D1
	// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001BFD9 File Offset: 0x0001A1D9
	public bool dcw
	{
		get
		{
			return this.cse;
		}
		set
		{
			this.cse = value;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001BFE2 File Offset: 0x0001A1E2
	// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001BFEA File Offset: 0x0001A1EA
	public Vector2Int dcx
	{
		get
		{
			return this.csf;
		}
		set
		{
			this.csf = value;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001BFF3 File Offset: 0x0001A1F3
	// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001BFFB File Offset: 0x0001A1FB
	public Vector2Int dcy
	{
		get
		{
			return this.csg;
		}
		set
		{
			this.csg = value;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001C004 File Offset: 0x0001A204
	// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001C00C File Offset: 0x0001A20C
	public bool dcz
	{
		get
		{
			return this.csh;
		}
		set
		{
			this.csh = value;
		}
	}

	// Token: 0x0400056D RID: 1389
	private float csc = 0.1f;

	// Token: 0x0400056E RID: 1390
	private float csd = 0.1f;

	// Token: 0x0400056F RID: 1391
	private bool cse = true;

	// Token: 0x04000570 RID: 1392
	private Vector2Int csf;

	// Token: 0x04000571 RID: 1393
	private Vector2Int csg;

	// Token: 0x04000572 RID: 1394
	private bool csh = true;

	// Token: 0x04000573 RID: 1395
	private static LeiaDisplay csi;
}
