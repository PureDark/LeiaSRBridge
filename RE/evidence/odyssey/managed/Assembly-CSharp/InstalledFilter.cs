using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000D0 RID: 208
[CreateAssetMenu(fileName = "Installed_Filter", menuName = "GameFilter/Filter/Installed", order = 1)]
public class InstalledFilter : BaseFilter
{
	// Token: 0x0600056D RID: 1389 RVA: 0x00019E98 File Offset: 0x00018098
	public override Game[] bdy(Game[] a)
	{
		if (a == null)
		{
			return new Game[0];
		}
		return GamesVM.lb(a, this.orderByLastPlayed).Where(new Func<Game, bool>(InstalledFilter.<>c.<>9.bea)).ToArray<Game>();
	}

	// Token: 0x04000510 RID: 1296
	[Tooltip("Sort by last played time")]
	public bool orderByLastPlayed = true;
}
