using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
[DisallowMultipleComponent]
[AddComponentMenu("Simulated Reality/Simulated Reality Camera Settings")]
public class SimulatedRealityCameraSettings : MonoBehaviour, dq
{
	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001AD6F File Offset: 0x00018F6F
	// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0001AD77 File Offset: 0x00018F77
	public bool dbr
	{
		get
		{
			return this.cqg;
		}
		set
		{
			this.cqg = value;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001AD80 File Offset: 0x00018F80
	// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0001AD88 File Offset: 0x00018F88
	public bool dbs
	{
		get
		{
			return this.cqh;
		}
		set
		{
			this.cqh = value;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001AD91 File Offset: 0x00018F91
	// (set) Token: 0x060005AA RID: 1450 RVA: 0x0001AD99 File Offset: 0x00018F99
	public bool dbt
	{
		get
		{
			return this.cqi;
		}
		set
		{
			this.cqi = value;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001ADA2 File Offset: 0x00018FA2
	// (set) Token: 0x060005AC RID: 1452 RVA: 0x0001ADAA File Offset: 0x00018FAA
	public float dbu
	{
		get
		{
			return this.cqj;
		}
		set
		{
			this.cqj = value;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001ADB3 File Offset: 0x00018FB3
	// (set) Token: 0x060005AE RID: 1454 RVA: 0x0001ADBB File Offset: 0x00018FBB
	public dn dbv
	{
		get
		{
			return this.cqk;
		}
		set
		{
			this.cqk = value;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001ADC4 File Offset: 0x00018FC4
	// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001ADCC File Offset: 0x00018FCC
	public Vector2 dbw
	{
		get
		{
			return this.cql;
		}
		set
		{
			this.cql = value;
		}
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001ADD5 File Offset: 0x00018FD5
	public @do bfs()
	{
		if (this.cqm == null)
		{
			this.cqm = new dm(this);
		}
		return this.cqm;
	}

	// Token: 0x0400052C RID: 1324
	private bool cqg;

	// Token: 0x0400052D RID: 1325
	private bool cqh;

	// Token: 0x0400052E RID: 1326
	private bool cqi;

	// Token: 0x0400052F RID: 1327
	private float cqj = 100f;

	// Token: 0x04000530 RID: 1328
	private dn cqk;

	// Token: 0x04000531 RID: 1329
	private Vector2 cql = new Vector2(69f, 39f);

	// Token: 0x04000532 RID: 1330
	private dm cqm;
}
