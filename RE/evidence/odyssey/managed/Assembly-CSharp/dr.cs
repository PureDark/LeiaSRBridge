using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class dr
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0001B60F File Offset: 0x0001980F
	// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001B617 File Offset: 0x00019817
	public bool dci
	{
		get
		{
			return this.crb;
		}
		set
		{
			this.crb = value;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001B620 File Offset: 0x00019820
	// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001B74C File Offset: 0x0001994C
	public bool dcl
	{
		get
		{
			return this.cre;
		}
		set
		{
			this.cre = value;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001B628 File Offset: 0x00019828
	// (set) Token: 0x060005FF RID: 1535 RVA: 0x0001B641 File Offset: 0x00019841
	public List<SRDisplayDefinition> dcm
	{
		get
		{
			return this.crf;
		}
		set
		{
			this.crf = value;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001B639 File Offset: 0x00019839
	// (set) Token: 0x060005FD RID: 1533 RVA: 0x0001B630 File Offset: 0x00019830
	public bool dcj
	{
		get
		{
			return this.crc;
		}
		set
		{
			this.crc = value;
		}
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0000238E File Offset: 0x0000058E
	public void bhq()
	{
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001B653 File Offset: 0x00019853
	// (set) Token: 0x06000601 RID: 1537 RVA: 0x0001B64A File Offset: 0x0001984A
	public bool dck
	{
		get
		{
			return this.crd;
		}
		set
		{
			this.crd = value;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001B72A File Offset: 0x0001992A
	public static dr dcn
	{
		get
		{
			if (dr.crg == null)
			{
				dr.crg = new dr();
				dr.crg.bhq();
			}
			return dr.crg;
		}
	}

	// Token: 0x0400054A RID: 1354
	private bool crb;

	// Token: 0x0400054B RID: 1355
	private bool crc = true;

	// Token: 0x0400054C RID: 1356
	private bool crd;

	// Token: 0x0400054D RID: 1357
	private bool cre = true;

	// Token: 0x0400054E RID: 1358
	private List<SRDisplayDefinition> crf = new List<SRDisplayDefinition>
	{
		new SRDisplayDefinition
		{
			dcf = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bak(),
			dcg = new Vector2(69f, 39f),
			dch = 70f
		},
		new SRDisplayDefinition
		{
			dcf = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bal(),
			dcg = new Vector2(35.6f, 20f),
			dch = 45f
		},
		new SRDisplayDefinition
		{
			dcf = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bam(),
			dcg = new Vector2(144f, 81f),
			dch = 150f
		}
	};

	// Token: 0x0400054F RID: 1359
	private static dr crg;
}
