using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000D4 RID: 212
[CreateAssetMenu(fileName = "Upcoming_Filter", menuName = "GameFilter/Filter/Upcoming", order = 2)]
public class UpcomingFilter : BaseFilter
{
	// Token: 0x06000578 RID: 1400 RVA: 0x00019F8C File Offset: 0x0001818C
	public override Game[] bdy(Game[] a)
	{
		if (a == null)
		{
			return new Game[0];
		}
		return GamesVM.lb(a.Where(new Func<Game, bool>(UpcomingFilter.<>c.<>9.bed)).Where(new Func<Game, bool>(UpcomingFilter.<>c.<>9.bee)), false).ToArray<Game>();
	}
}
