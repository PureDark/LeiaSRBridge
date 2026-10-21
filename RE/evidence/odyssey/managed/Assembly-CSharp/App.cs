using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Newtonsoft.Json;
using Odyssey3DHubUI.Helper;
using UnityEngine;

// Token: 0x02000005 RID: 5
internal class App : MonoBehaviour
{
	// Token: 0x06000004 RID: 4 RVA: 0x0000207F File Offset: 0x0000027F
	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.a(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.b());
			LeiaPluginHelper.DestroyLeiaModules();
			return;
		}
		bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.c(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.b());
		LeiaPluginHelper.StartLeiaModules();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020B8 File Offset: 0x000002B8
	private void OnApplicationQuit()
	{
		App.stopwatch.Stop();
		SharedMemoryManager.ft();
		bb.hk(z.bxg, ba.bxv, ((long)App.stopwatch.Elapsed.TotalSeconds).ToString());
		CancellationTokenSource cancellationTokenSource = App.bsd;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		App.fl();
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000210B File Offset: 0x0000030B
	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			if (!Application.runInBackground)
			{
				Application.runInBackground = true;
			}
			if (!SharedMemoryManager.cyk)
			{
				bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.d(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.e());
				SharedMemoryManager.SendData(SharedMemoryManager.j.bun, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.f());
			}
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
	public static void MoveToPrimaryMonitor()
	{
		List<DisplayInfo> list = new List<DisplayInfo>();
		Screen.GetDisplayLayout(list);
		if (list != null && list.Count > 0)
		{
			int num = 0;
			foreach (DisplayInfo displayInfo in list)
			{
				bd.id(App.bsc, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.g(), num, list[num].name), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.h());
				num++;
			}
			DisplayInfo displayInfo2 = list[0];
			Screen.MoveMainWindowTo(ref displayInfo2, new Vector2Int(list[0].width / 2, list[0].height / 2));
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000220C File Offset: 0x0000040C
	[RuntimeInitializeOnLoadMethod(3)]
	private static void OnBeforeSplashScreen()
	{
		bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.i(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.j());
		if (!RegistryHelper.CheckEULAConfirmation())
		{
			Process.GetCurrentProcess().Kill();
		}
		App.StartService();
		int num = 0;
		while (!Odyssey3DHubIPCManager.IsClientConnected())
		{
			if (num++ > 20)
			{
				bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.k(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.j());
				App.fl();
			}
			bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.l(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.j());
			Thread.Sleep(500);
		}
		if (!SingleInstanceHelper.EnsureSingleInstance())
		{
			Odyssey3DHubIPCManager.SendMessage(Odyssey3DHubIPCManager.c.bsh, Odyssey3DHubIPCManager.d.bsq, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
			while (Odyssey3DHubIPCManager.buf)
			{
				Thread.Sleep(100);
				if (num++ > 10)
				{
					bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.n(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.j());
					break;
				}
			}
			bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.o(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.j());
			Odyssey3DHubIPCManager.fo();
			App.fl();
			return;
		}
		App.stopwatch.Start();
		bb.hk(z.bxc, ba.bxr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.p());
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		LanguageHelper.Init();
		App.MoveToPrimaryMonitor();
		bl.jf();
		App.deviceStatus = Odyssey3DHubIPCManager.e.bte;
		Odyssey3DHubIPCManager.SendMessage(Odyssey3DHubIPCManager.c.bsi, Odyssey3DHubIPCManager.d.bsu, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		num = 0;
		while (App.deviceStatus == Odyssey3DHubIPCManager.e.bte)
		{
			if (num++ > 20)
			{
				bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.q(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.j());
				break;
			}
			Thread.Sleep(500);
		}
		if (App.deviceStatus != Odyssey3DHubIPCManager.e.btf && App.deviceStatus != Odyssey3DHubIPCManager.e.btp)
		{
			App.fl();
		}
		Odyssey3DHubIPCManager.SendMessage(Odyssey3DHubIPCManager.c.bsh, Odyssey3DHubIPCManager.d.bsr, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		App.LoadSettingsInfo();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000238E File Offset: 0x0000058E
	[RuntimeInitializeOnLoadMethod(1)]
	private static void OnBeforeSceneLoad()
	{
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002390 File Offset: 0x00000590
	[RuntimeInitializeOnLoadMethod(0)]
	private static void OnAfterSceneLoad()
	{
		App.LoadGameInfo(false);
		try
		{
			App.bsd = new CancellationTokenSource();
			Task.Run(new Action(App.<>c.<>9.fj));
		}
		catch (OperationCanceledException ex)
		{
			bd.ie(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.r() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.s());
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002408 File Offset: 0x00000608
	private static void StartService()
	{
		try
		{
			if (Process.GetProcessesByName(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.t()).Length == 0)
			{
				string text = Path.Combine(Directory.GetCurrentDirectory(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.u());
				if (!File.Exists(text))
				{
					goto IL_68;
				}
				using (Process process = Process.Start(text))
				{
					if (process != null && !process.HasExited)
					{
						bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.v(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.w());
						Odyssey3DHubIPCManager.StartClient();
					}
					else
					{
						App.fl();
					}
					goto IL_68;
				}
			}
			Odyssey3DHubIPCManager.StartClient();
			IL_68:;
		}
		catch (Exception ex)
		{
			bd.ie(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.x() + ex.Message, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.w());
			App.fl();
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000024C4 File Offset: 0x000006C4
	private static void LoadGameInfo(bool isUpdated = false)
	{
		bd.ih(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.y());
		string text = App.TryReadFromFile(Path.Combine(Application.streamingAssetsPath, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.z()));
		if (string.IsNullOrEmpty(text))
		{
			bd.ie(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ba(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.y());
			App.fl();
			return;
		}
		GamesVM gamesVM = JsonUtility.FromJson<GamesVM>(text);
		if (gamesVM != null && gamesVM.Games != null)
		{
			string path = Path.Combine(Application.persistentDataPath, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bb(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bc());
			if (File.Exists(path))
			{
				UserGameInfoList userGameInfoList = JsonConvert.DeserializeObject<UserGameInfoList>(File.ReadAllText(path));
				if (userGameInfoList != null && userGameInfoList.Infos.Count > 0)
				{
					using (List<UserGameInfo>.Enumerator enumerator = userGameInfoList.Infos.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							App.b b = new App.b();
							b.bsb = enumerator.Current;
							Game game = gamesVM.Games.FirstOrDefault(new Func<Game, bool>(b.fk));
							if (game != null && game.GameInfo != null)
							{
								game.GameInfo.ExePath = b.bsb.ExePath;
								game.GameInfo.DiscoveredByUser = b.bsb.DiscoveredByUser;
								game.GameInfo.UnregisteredByUser = b.bsb.UnregisteredByUser;
								game.GameInfo.LastPlayedTimeUTC = b.bsb.LastPlayedTimeUTC;
								game.GameInfo.FavoritedByUser = b.bsb.FavoritedByUser;
								game.GameInfo.LastRegisteredTimeUTC = b.bsb.LastRegisteredTimeUTC;
							}
						}
					}
				}
			}
		}
		bn.cyp.cyq = gamesVM;
		bn.cyp.cyq.lg();
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000026A8 File Offset: 0x000008A8
	private static string TryReadFromFile(string filePath)
	{
		string result;
		try
		{
			result = (File.Exists(filePath) ? File.ReadAllText(filePath) : null);
		}
		catch (Exception ex)
		{
			bd.ie(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bd() + filePath + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.be() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bf());
			result = null;
		}
		return result;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002704 File Offset: 0x00000904
	private static string GetGameInfoFromResource()
	{
		bd.id(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bh());
		TextAsset textAsset = Resources.Load<TextAsset>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bi());
		if (!(textAsset != null))
		{
			return null;
		}
		return textAsset.text;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002744 File Offset: 0x00000944
	private static void LoadSettingsInfo()
	{
		bd.ih(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bj());
		SettingsVM settingsVM = null;
		VersionsVM versionsVM = null;
		Directory.CreateDirectory(bm.cao);
		if (File.Exists(bm.cap))
		{
			settingsVM = JsonUtility.FromJson<SettingsVM>(File.ReadAllText(bm.cap));
		}
		if (settingsVM == null)
		{
			bd.ie(App.bsc, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bk(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bj());
			File.Delete(bm.cap);
			TextAsset textAsset = Resources.Load<TextAsset>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bl());
			settingsVM = JsonUtility.FromJson<SettingsVM>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bm() + textAsset.text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bn());
		}
		if (File.Exists(bm.cas))
		{
			versionsVM = JsonUtility.FromJson<VersionsVM>(File.ReadAllText(bm.cas));
		}
		bn.cyp.cyr = settingsVM;
		bn.cyp.cyr.ly();
		bn.cyp.cyr.SerializeData();
		if (versionsVM != null)
		{
			bn.cyp.cyu = versionsVM;
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000238E File Offset: 0x0000058E
	[RuntimeInitializeOnLoadMethod]
	private static void OnRuntimeInitialized()
	{
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002825 File Offset: 0x00000A25
	private static void fl()
	{
		Process.GetCurrentProcess().Kill();
	}

	// Token: 0x04000006 RID: 6
	private static string bsc = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bo();

	// Token: 0x04000007 RID: 7
	public static bool isAlreadyRunning = false;

	// Token: 0x04000008 RID: 8
	public static Odyssey3DHubIPCManager.e deviceStatus = Odyssey3DHubIPCManager.e.bte;

	// Token: 0x04000009 RID: 9
	public static Stopwatch stopwatch = new Stopwatch();

	// Token: 0x0400000A RID: 10
	private static CancellationTokenSource bsd;

	// Token: 0x02000007 RID: 7
	[CompilerGenerated]
	private sealed class b
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002880 File Offset: 0x00000A80
		internal bool fk(Game a)
		{
			return a.Id == this.bsb.Id;
		}

		// Token: 0x0400000D RID: 13
		public UserGameInfo bsb;
	}
}
