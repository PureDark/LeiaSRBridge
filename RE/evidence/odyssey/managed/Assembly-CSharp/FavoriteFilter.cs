using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000CE RID: 206
[CreateAssetMenu(fileName = "Favorite_Filter", menuName = "GameFilter/Filter/Favorite", order = 3)]
public class FavoriteFilter : BaseFilter
{
	// Token: 0x06000568 RID: 1384 RVA: 0x00019E2C File Offset: 0x0001802C
	public override Game[] bdy(Game[] a)
	{
		if (a == null)
		{
			return new Game[0];
		}
		return GamesVM.lb(a, false).ToArray<Game>().Where(new Func<Game, bool>(FavoriteFilter.<>c.<>9.bdz)).ToArray<Game>();
	}
}
