using System;

// Token: 0x02000053 RID: 83
[Serializable]
public class VersionsVM
{
	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000934B File Offset: 0x0000754B
	public string cze
	{
		get
		{
			return this.Versions.Odyssey3DHub;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009358 File Offset: 0x00007558
	public string czf
	{
		get
		{
			return this.Versions.Odyssey3DPlayer;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009365 File Offset: 0x00007565
	public string czg
	{
		get
		{
			return this.Versions.LeiaSR;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009372 File Offset: 0x00007572
	public string czh
	{
		get
		{
			return this.Versions.GameLauncher;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060001BA RID: 442 RVA: 0x0000937F File Offset: 0x0000757F
	public string czi
	{
		get
		{
			return this.Versions.GameAssets;
		}
	}

	// Token: 0x040001E9 RID: 489
	public Versions Versions;
}
