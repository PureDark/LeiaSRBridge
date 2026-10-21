using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000CC RID: 204
[CreateAssetMenu(fileName = "All_Filter", menuName = "GameFilter/Filter/All", order = 0)]
public class AllFilter : BaseFilter
{
	// Token: 0x06000564 RID: 1380 RVA: 0x00019E0C File Offset: 0x0001800C
	public override Game[] bdy(Game[] a)
	{
		return GamesVM.lb(a, false).ToArray<Game>();
	}
}
