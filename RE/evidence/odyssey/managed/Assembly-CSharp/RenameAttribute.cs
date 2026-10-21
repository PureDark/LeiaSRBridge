using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class RenameAttribute : PropertyAttribute
{
	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001B28A File Offset: 0x0001948A
	// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0001B292 File Offset: 0x00019492
	public string dbx
	{
		get
		{
			return this.cqt;
		}
		set
		{
			this.cqt = value;
		}
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0001B29B File Offset: 0x0001949B
	public RenameAttribute(string name)
	{
		this.dbx = name;
	}

	// Token: 0x04000539 RID: 1337
	private string cqt;
}
