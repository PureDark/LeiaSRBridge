using System;
using System.Linq;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class MainArea : MonoBehaviour
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001340C File Offset: 0x0001160C
	public static MainArea dbb
	{
		get
		{
			if (!MainArea.ckr)
			{
				MainArea.ckq = Object.FindFirstObjectByType<MainArea>();
				if (MainArea.ckq == null)
				{
					GameObject gameObject = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vp());
					if (gameObject != null)
					{
						MainArea.ckq = gameObject.GetComponent<MainArea>();
					}
				}
				MainArea.ckr = true;
			}
			return MainArea.ckq;
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00013464 File Offset: 0x00011664
	public void InstantiateScene()
	{
		bd.ih(MainArea.ckp, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vq());
		if (this.cks == null)
		{
			this.cks = base.GetComponent<ViewAreaController>();
		}
		if (this.ckt == null)
		{
			this.ckt = new ViewGameItem();
		}
		this.ckt.TabGameItems = bn.cyp.cyq.Games;
		this.ckt.RecentGameItems = bn.cyp.cyq.Games.Where(new Func<Game, bool>(MainArea.<>c.<>9.xh)).OrderByDescending(new Func<Game, DateTime?>(MainArea.<>c.<>9.xi)).Take(this.totalRecentGame).ToArray<Game>();
		this.cks.baj(this.ckt);
	}

	// Token: 0x040003E5 RID: 997
	private static string ckp = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vp();

	// Token: 0x040003E6 RID: 998
	[SerializeField]
	private int totalRecentGame;

	// Token: 0x040003E7 RID: 999
	private static MainArea ckq;

	// Token: 0x040003E8 RID: 1000
	private static bool ckr = false;

	// Token: 0x040003E9 RID: 1001
	private ViewAreaController cks;

	// Token: 0x040003EA RID: 1002
	private ViewGameItem ckt;
}
