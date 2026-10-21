using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class dm : @do
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x0001ACF1 File Offset: 0x00018EF1
	public dm(SimulatedRealityCameraSettings a)
	{
		this.cqf = a;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0001AD00 File Offset: 0x00018F00
	public override float bfd()
	{
		if (this.cqf.dbr)
		{
			return this.cqf.dbu;
		}
		return SRProjectSettings.dce.dby;
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0001AD25 File Offset: 0x00018F25
	public override dn bfe()
	{
		if (this.cqf.dbs)
		{
			return this.cqf.dbv;
		}
		return SRProjectSettings.dce.dbz;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0001AD4A File Offset: 0x00018F4A
	public override Vector2 bff()
	{
		if (this.cqf.dbt)
		{
			return this.cqf.dbw;
		}
		return SRProjectSettings.dce.dca;
	}

	// Token: 0x0400052B RID: 1323
	private readonly SimulatedRealityCameraSettings cqf;
}
