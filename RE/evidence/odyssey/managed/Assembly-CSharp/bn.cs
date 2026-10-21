using System;

// Token: 0x0200004D RID: 77
internal class bn
{
	// Token: 0x06000176 RID: 374 RVA: 0x00008678 File Offset: 0x00006878
	private bn()
	{
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000177 RID: 375 RVA: 0x00008699 File Offset: 0x00006899
	internal static bn cyp
	{
		get
		{
			if (bn.cau == null)
			{
				bn.cau = new bn();
			}
			return bn.cau;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000178 RID: 376 RVA: 0x000086B1 File Offset: 0x000068B1
	// (set) Token: 0x06000179 RID: 377 RVA: 0x000086B9 File Offset: 0x000068B9
	public GamesVM cyq
	{
		get
		{
			return this.cav;
		}
		set
		{
			this.cav = value;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600017A RID: 378 RVA: 0x000086C2 File Offset: 0x000068C2
	// (set) Token: 0x0600017B RID: 379 RVA: 0x000086CA File Offset: 0x000068CA
	public SettingsVM cyr
	{
		get
		{
			return this.caw;
		}
		set
		{
			this.caw = value;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600017C RID: 380 RVA: 0x000086D3 File Offset: 0x000068D3
	// (set) Token: 0x0600017D RID: 381 RVA: 0x000086DB File Offset: 0x000068DB
	public bool cys
	{
		get
		{
			return this.cax;
		}
		set
		{
			this.cax = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600017E RID: 382 RVA: 0x000086E4 File Offset: 0x000068E4
	// (set) Token: 0x0600017F RID: 383 RVA: 0x000086EC File Offset: 0x000068EC
	public bool cyt
	{
		get
		{
			return this.cay;
		}
		set
		{
			this.cay = value;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000180 RID: 384 RVA: 0x000086F5 File Offset: 0x000068F5
	// (set) Token: 0x06000181 RID: 385 RVA: 0x000086FD File Offset: 0x000068FD
	public VersionsVM cyu
	{
		get
		{
			return this.caz;
		}
		set
		{
			this.caz = value;
		}
	}

	// Token: 0x040001C0 RID: 448
	public string cat = Environment.CurrentDirectory;

	// Token: 0x040001C1 RID: 449
	private static bn cau;

	// Token: 0x040001C2 RID: 450
	private GamesVM cav;

	// Token: 0x040001C3 RID: 451
	private SettingsVM caw;

	// Token: 0x040001C4 RID: 452
	private bool cax = true;

	// Token: 0x040001C5 RID: 453
	private bool cay = true;

	// Token: 0x040001C6 RID: 454
	private VersionsVM caz;
}
