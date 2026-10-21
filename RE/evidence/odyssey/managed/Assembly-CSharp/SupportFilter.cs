using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000D2 RID: 210
[CreateAssetMenu(fileName = "Support_Filter", menuName = "GameFilter/Filter/Support", order = 2)]
public class SupportFilter : BaseFilter
{
	// Token: 0x06000572 RID: 1394 RVA: 0x00019F14 File Offset: 0x00018114
	public override Game[] bdy(Game[] a)
	{
		if (a == null)
		{
			return new Game[0];
		}
		return GamesVM.lb(a.Where(new Func<Game, bool>(SupportFilter.<>c.<>9.beb)).Where(new Func<Game, bool>(SupportFilter.<>c.<>9.bec)), false).ToArray<Game>();
	}
}
