using System;
using SRUnity;
using UnityEngine;

// Token: 0x020000DD RID: 221
public abstract class @do
{
	// Token: 0x060005C5 RID: 1477 RVA: 0x0001B2AC File Offset: 0x000194AC
	public static @do opa(SimulatedRealityCamera a)
	{
		if (a != null)
		{
			SimulatedRealityCameraSettings componentInParent = a.GetComponentInParent<SimulatedRealityCameraSettings>();
			if (componentInParent != null)
			{
				return componentInParent.bfs();
			}
		}
		return SRProjectSettings.dce.bfs();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0001B2E3 File Offset: 0x000194E3
	public float ua()
	{
		return 7715f / this.bgi(SimulatedRealityModule<SRCore>.Instance.getPhysicalSize());
	}

	// Token: 0x060005C7 RID: 1479
	public abstract dn bfe();

	// Token: 0x060005C8 RID: 1480
	public abstract Vector2 bff();

	// Token: 0x060005C9 RID: 1481 RVA: 0x0001B2FC File Offset: 0x000194FC
	public static @do bgf(SimulatedRealityCamera a)
	{
		if (a != null)
		{
			SimulatedRealityCameraSettings componentInParent = a.GetComponentInParent<SimulatedRealityCameraSettings>();
			if (componentInParent != null)
			{
				return componentInParent.bfs();
			}
		}
		return SRProjectSettings.dce.bfs();
	}

	// Token: 0x060005CA RID: 1482
	public abstract float bfd();

	// Token: 0x060005CB RID: 1483 RVA: 0x0001B333 File Offset: 0x00019533
	public float hyc(Vector2 a)
	{
		return this.bfd() * this.bgh(a);
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0001B343 File Offset: 0x00019543
	public float bgj()
	{
		return this.bgi(SimulatedRealityModule<SRCore>.Instance.getPhysicalSize()) * this.cqw;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0001B35C File Offset: 0x0001955C
	public static float msy(Vector2 a, Vector2 b)
	{
		return Math.Min(b.x / a.x, b.y / a.y);
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001B37D File Offset: 0x0001957D
	public float htx(Vector2 a)
	{
		return this.bgi(a) * this.cqw;
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001B3A0 File Offset: 0x000195A0
	public static @do bim(SimulatedRealityCamera a)
	{
		if (a != null)
		{
			SimulatedRealityCameraSettings componentInParent = a.GetComponentInParent<SimulatedRealityCameraSettings>();
			if (componentInParent != null)
			{
				return componentInParent.bfs();
			}
		}
		return SRProjectSettings.dce.bfs();
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001B3D7 File Offset: 0x000195D7
	public float nlf(Vector2 a)
	{
		if (this.bfe() == dn.cqu)
		{
			return -3169f;
		}
		if (this.bfe() == dn.cqv)
		{
			return @do.bgg(a, this.bff());
		}
		return -6949f;
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001B333 File Offset: 0x00019533
	public float jog(Vector2 a)
	{
		return this.bfd() * this.bgh(a);
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001B402 File Offset: 0x00019602
	public float bgl()
	{
		return 1f / this.bgi(SimulatedRealityModule<SRCore>.Instance.getPhysicalSize());
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001B333 File Offset: 0x00019533
	public float bgi(Vector2 a)
	{
		return this.bfd() * this.bgh(a);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001B41A File Offset: 0x0001961A
	public float bgh(Vector2 a)
	{
		if (this.bfe() == dn.cqu)
		{
			return 1f;
		}
		if (this.bfe() == dn.cqv)
		{
			return @do.bgg(a, this.bff());
		}
		return 1f;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0001B35C File Offset: 0x0001955C
	public static float bgg(Vector2 a, Vector2 b)
	{
		return Math.Min(b.x / a.x, b.y / a.y);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0001B445 File Offset: 0x00019645
	public float xa()
	{
		return 6201f / this.bgi(SimulatedRealityModule<SRCore>.Instance.getPhysicalSize());
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0001B333 File Offset: 0x00019533
	public float ize(Vector2 a)
	{
		return this.bfd() * this.bgh(a);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0001B35C File Offset: 0x0001955C
	public static float dry(Vector2 a, Vector2 b)
	{
		return Math.Min(b.x / a.x, b.y / a.y);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0001B333 File Offset: 0x00019533
	public float nrw(Vector2 a)
	{
		return this.bfd() * this.bgh(a);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0001B37D File Offset: 0x0001957D
	public float bgk(Vector2 a)
	{
		return this.bgi(a) * this.cqw;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0001B35C File Offset: 0x0001955C
	public static float fhn(Vector2 a, Vector2 b)
	{
		return Math.Min(b.x / a.x, b.y / a.y);
	}

	// Token: 0x0400053D RID: 1341
	private readonly float cqw = 0.01f;
}
