using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// Token: 0x0200004E RID: 78
[Serializable]
internal class GamesVM
{
	// Token: 0x06000182 RID: 386 RVA: 0x00008708 File Offset: 0x00006908
	public static IOrderedEnumerable<Game> lb(IEnumerable<Game> a, bool b = false)
	{
		Func<Game, int> keySelector = new Func<Game, int>(GamesVM.<>c.<>9.kr);
		IOrderedEnumerable<Game> source;
		if (!b)
		{
			source = a.OrderByDescending(keySelector);
		}
		else
		{
			source = a.OrderByDescending(new Func<Game, DateTime?>(GamesVM.<>c.<>9.ks)).ThenByDescending(keySelector);
		}
		return source.ThenBy(new Func<Game, string>(GamesVM.<>c.<>9.kt));
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00008790 File Offset: 0x00006990
	public List<Game> lc()
	{
		if (this.Games != null)
		{
			return GamesVM.lb(this.Games.Where(new Func<Game, bool>(GamesVM.<>c.<>9.ku)), true).ToList<Game>();
		}
		return null;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x000087DC File Offset: 0x000069DC
	public List<Game> ld()
	{
		if (this.Games != null)
		{
			return GamesVM.lb(this.Games.Where(new Func<Game, bool>(GamesVM.<>c.<>9.kv)).Where(new Func<Game, bool>(GamesVM.<>c.<>9.kw)), false).ToList<Game>();
		}
		return null;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000884C File Offset: 0x00006A4C
	public List<Game> le()
	{
		if (this.Games != null)
		{
			return GamesVM.lb(this.Games.Where(new Func<Game, bool>(GamesVM.<>c.<>9.kx)).Where(new Func<Game, bool>(GamesVM.<>c.<>9.ky)), false).ToList<Game>();
		}
		return null;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x000088BC File Offset: 0x00006ABC
	public List<Game> lf(string a)
	{
		GamesVM.bp bp = new GamesVM.bp();
		bp.cbd = a;
		List<Game> result;
		try
		{
			result = this.Games.Where(new Func<Game, bool>(bp.la)).ToList<Game>();
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000890C File Offset: 0x00006B0C
	public void LaunchGame(Game game)
	{
		bd.ih(GamesVM.cbe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rh());
		game.GameInfo.cyn = new DateTime?(DateTime.UtcNow);
		bn.cyp.cyq.SerializeData();
		Odyssey3DHubIPCManager.SendMessage(Odyssey3DHubIPCManager.c.bsg, Odyssey3DHubIPCManager.d.bsn, string.Concat(new string[]
		{
			game.GameInfo.Title,
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			game.GameInfo.ExePath,
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			game.GameInfo.Tier,
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			game.jy()
		}));
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000089AC File Offset: 0x00006BAC
	public void UnregisterGame(Game game)
	{
		bd.ih(GamesVM.cbe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ri());
		game.GameInfo.ExePath = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		game.GameInfo.IsRegistered = false;
		game.GameInfo.UnregisteredByUser = true;
		game.GameInfo.cyn = new DateTime?(DateTime.MinValue);
		game.GameInfo.LastRegisteredTimeUTC = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00008A15 File Offset: 0x00006C15
	public void ResetDetails(Game game)
	{
		bd.ih(GamesVM.cbe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rj());
		game.GameInfo.ExePath = game.GameInfo.ExePathCache;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00008A3C File Offset: 0x00006C3C
	public void SerializeData()
	{
		bd.ih(GamesVM.cbe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rk());
		try
		{
			UserGameInfoList userGameInfoList = new UserGameInfoList
			{
				Infos = new List<UserGameInfo>()
			};
			foreach (Game game in bn.cyp.cyq.Games)
			{
				UserGameInfo item = new UserGameInfo
				{
					Id = game.Id,
					ExePath = game.GameInfo.ExePath,
					DiscoveredByUser = game.GameInfo.DiscoveredByUser,
					UnregisteredByUser = game.GameInfo.UnregisteredByUser,
					LastPlayedTimeUTC = game.GameInfo.LastPlayedTimeUTC,
					FavoritedByUser = game.GameInfo.FavoritedByUser,
					LastRegisteredTimeUTC = game.GameInfo.LastRegisteredTimeUTC
				};
				userGameInfoList.Infos.Add(item);
			}
			string contents = JsonUtility.ToJson(userGameInfoList, true);
			if (!Directory.Exists(bm.caq))
			{
				Directory.CreateDirectory(bm.caq);
			}
			File.WriteAllText(bm.car, contents);
		}
		catch (Exception ex)
		{
			bd.id(GamesVM.cbe, ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rk());
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00008B78 File Offset: 0x00006D78
	public void lg()
	{
		this.gameResources.Clear();
		this.lh();
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00008B8C File Offset: 0x00006D8C
	public void GetGameSprite(string name, Action<Sprite> OnGetSpriteSuccess = null, Action OnGetSpriteFail = null)
	{
		GamesVM.bo bo = new GamesVM.bo();
		bo.cba = this;
		bo.cbb = OnGetSpriteSuccess;
		bo.cbc = OnGetSpriteFail;
		bd.ih(GamesVM.cbe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rl());
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(name);
		Sprite sprite;
		if (!this.gameResources.TryGetValue(fileNameWithoutExtension, out sprite) || !(sprite != null))
		{
			Action<AsyncOperationHandle<Sprite>> action = new Action<AsyncOperationHandle<Sprite>>(bo.kz);
			try
			{
				AsyncOperationHandle<Sprite> obj;
				if (!this.cbf.TryGetValue(fileNameWithoutExtension, out obj))
				{
					AsyncOperationHandle<Sprite> value = Addressables.LoadAssetAsync<Sprite>(fileNameWithoutExtension);
					value.Completed += action;
					this.cbf[fileNameWithoutExtension] = value;
				}
				else if (!obj.IsDone)
				{
					obj.Completed += action;
				}
				else
				{
					action(obj);
				}
			}
			catch (Exception ex)
			{
				bd.ie(GamesVM.cbe, ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rl());
			}
			return;
		}
		Action<Sprite> cbb = bo.cbb;
		if (cbb == null)
		{
			return;
		}
		cbb(sprite);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00008C7C File Offset: 0x00006E7C
	private void CacheSprite(Sprite sprite)
	{
		if (sprite == null)
		{
			return;
		}
		if (!this.gameResources.ContainsKey(sprite.name))
		{
			this.gameResources[sprite.name] = sprite;
			return;
		}
		bd.id(GamesVM.cbe, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rm() + sprite.name, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rn());
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00008CD8 File Offset: 0x00006ED8
	private void lh()
	{
		Dictionary<string, AsyncOperationHandle<Sprite>> dictionary = this.cbf;
		if (dictionary != null && dictionary.Count > 0)
		{
			foreach (AsyncOperationHandle<Sprite> asyncOperationHandle in this.cbf.Values)
			{
				if (asyncOperationHandle.IsValid())
				{
					Addressables.Release<Sprite>(asyncOperationHandle);
				}
			}
		}
		this.cbf.Clear();
	}

	// Token: 0x040001C7 RID: 455
	private static string cbe = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ro();

	// Token: 0x040001C8 RID: 456
	public string Version;

	// Token: 0x040001C9 RID: 457
	public Game[] Games;

	// Token: 0x040001CA RID: 458
	public Dictionary<string, Sprite> gameResources = new Dictionary<string, Sprite>();

	// Token: 0x040001CB RID: 459
	private readonly Dictionary<string, AsyncOperationHandle<Sprite>> cbf = new Dictionary<string, AsyncOperationHandle<Sprite>>();

	// Token: 0x02000050 RID: 80
	[CompilerGenerated]
	private sealed class bo
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00008E24 File Offset: 0x00007024
		internal void kz(AsyncOperationHandle<Sprite> a)
		{
			if (a.Status == 1)
			{
				this.cba.CacheSprite(a.Result);
				Action<Sprite> action = this.cbb;
				if (action == null)
				{
					return;
				}
				action(a.Result);
				return;
			}
			else
			{
				Action action2 = this.cbc;
				if (action2 == null)
				{
					return;
				}
				action2();
				return;
			}
		}

		// Token: 0x040001D5 RID: 469
		public GamesVM cba;

		// Token: 0x040001D6 RID: 470
		public Action<Sprite> cbb;

		// Token: 0x040001D7 RID: 471
		public Action cbc;
	}

	// Token: 0x02000051 RID: 81
	[CompilerGenerated]
	private sealed class bp
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00008E75 File Offset: 0x00007075
		internal bool la(Game a)
		{
			return a.GameInfo.Title.Contains(this.cbd, StringComparison.OrdinalIgnoreCase) || a.GameInfo.Platform.Contains(this.cbd, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040001D8 RID: 472
		public string cbd;
	}
}
