using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using FuzzySharp;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

// Token: 0x02000019 RID: 25
public class GameScanHelper
{
	// Token: 0x0600005A RID: 90 RVA: 0x000043D8 File Offset: 0x000025D8
	public static Task<bool> ValidateGamesPath(Game[] games, bool validateUnregisteredGames, bool reScan, CancellationToken cancellationToken = default(CancellationToken))
	{
		GameScanHelper.y y;
		y.bwq = AsyncTaskMethodBuilder<bool>.Create();
		y.bwr = games;
		y.bws = validateUnregisteredGames;
		y.bwt = reScan;
		y.bwu = cancellationToken;
		y.bwp = -1;
		y.bwq.Start<GameScanHelper.y>(ref y);
		return y.bwq.Task;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004434 File Offset: 0x00002634
	public static string hc(GameInfo a, CancellationToken b)
	{
		b.ThrowIfCancellationRequested();
		string[] array = a.ExeName.Split(',', StringSplitOptions.RemoveEmptyEntries);
		string path = a.RegistryInfo.Path;
		if (!string.IsNullOrEmpty(path))
		{
			string registryInstallLocation = GameScanHelper.GetRegistryInstallLocation(path, a.RegistryInfo.Key);
			if (!string.IsNullOrEmpty(registryInstallLocation))
			{
				foreach (string path2 in array)
				{
					string path3 = Path.Combine(registryInstallLocation, path2);
					if (File.Exists(path3))
					{
						return Path.GetFullPath(path3);
					}
				}
			}
		}
		foreach (string text in a.Platform.Split(',', StringSplitOptions.RemoveEmptyEntries))
		{
			string text2;
			if (text.Contains(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.he()))
			{
				text2 = GameScanHelper.hf(array, a.Title);
			}
			else if (text.Contains(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hf()))
			{
				text2 = GameScanHelper.hi(array, a.Title);
			}
			else
			{
				text2 = null;
			}
			string text3 = text2;
			if (!string.IsNullOrWhiteSpace(text3))
			{
				return Path.GetFullPath(text3);
			}
		}
		return <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000453C File Offset: 0x0000273C
	public static Task FindGamesInSteam(CancellationToken cancellationToken)
	{
		GameScanHelper.x x;
		x.bwm = AsyncTaskMethodBuilder.Create();
		x.bwn = cancellationToken;
		x.bwl = -1;
		x.bwm.Start<GameScanHelper.x>(ref x);
		return x.bwm.Task;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004580 File Offset: 0x00002780
	private static void ExtractSteamGameManifest(string libraryPath, CancellationToken cancellationToken)
	{
		string text = Path.Combine(libraryPath, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hg());
		if (Directory.Exists(text))
		{
			try
			{
				foreach (GameInfo gameInfo in GameScanHelper.bxa.Keys.ToList<GameInfo>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					string value;
					if (!GameScanHelper.bxa.TryGetValue(gameInfo, out value) || string.IsNullOrEmpty(value))
					{
						string path = Path.Combine(text, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hh() + gameInfo.SteamAppID.Official + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hi());
						if (!File.Exists(path))
						{
							path = Path.Combine(text, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hh() + gameInfo.SteamAppID.Demo + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hi());
						}
						if (File.Exists(path))
						{
							string[] array = File.ReadAllLines(path);
							string text2 = string.Empty;
							foreach (string input in array)
							{
								Match match = GameScanHelper.bxb.Match(input);
								if (match.Success)
								{
									text2 = match.Groups[1].Value;
									break;
								}
							}
							if (!string.IsNullOrEmpty(text2))
							{
								string[] array3 = gameInfo.ExeName.Split(',', StringSplitOptions.RemoveEmptyEntries);
								foreach (string path2 in array3)
								{
									string text3 = Path.Combine(text, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hj(), text2);
									string path3 = Path.Combine(text3, path2);
									if (File.Exists(path3))
									{
										GameScanHelper.bxa[gameInfo] = Path.GetFullPath(path3);
										break;
									}
									if (Directory.Exists(text3))
									{
										string text4 = GameScanHelper.SearchExeInDirectory(text3, array3, 0);
										if (!string.IsNullOrWhiteSpace(text4))
										{
											GameScanHelper.bxa[gameInfo] = Path.GetFullPath(text4);
											break;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				bd.id(GameScanHelper.bwx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hk() + text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.be() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hl());
			}
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000047BC File Offset: 0x000029BC
	private static string hd()
	{
		foreach (string name in new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hm(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hn()
		})
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					string text = registryKey.GetValue(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ho()) as string;
					if (!string.IsNullOrWhiteSpace(text) && Directory.Exists(text))
					{
						return text;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000484C File Offset: 0x00002A4C
	private static List<string> he(string a)
	{
		List<string> list = new List<string>();
		string path = Path.Combine(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hp());
		if (!File.Exists(path))
		{
			list.Add(a);
			return list;
		}
		string[] array = File.ReadAllLines(path);
		Regex regex = new Regex(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hq());
		foreach (string input in array)
		{
			Match match = regex.Match(input);
			if (match.Success)
			{
				string value = match.Groups[1].Value;
				if (Directory.Exists(value))
				{
					list.Add(value);
				}
			}
		}
		return list;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000048E4 File Offset: 0x00002AE4
	private static string hf(string[] a, string b)
	{
		GameScanHelper.n n = new GameScanHelper.n();
		n.bvw = a;
		string text = GameScanHelper.hg();
		if (string.IsNullOrWhiteSpace(text))
		{
			return null;
		}
		string path = Path.Combine(text, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hr());
		if (!Directory.Exists(path))
		{
			return null;
		}
		n.bvv = GameScanHelper.hj(b);
		foreach (m m in Directory.EnumerateFiles(path, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hs(), SearchOption.TopDirectoryOnly).Where(new Func<string, bool>(GameScanHelper.<>c.<>9.gg)).Select(new Func<string, m>(n.gn)).Where(new Func<m, bool>(n.go)).OrderByDescending(new Func<m, bool>(n.gp)).ThenByDescending(new Func<m, bool>(n.gq)).ThenByDescending(new Func<m, bool>(n.gr)).ThenByDescending(new Func<m, bool>(n.gs)).ThenBy(new Func<m, bool>(GameScanHelper.<>c.<>9.gh)).ThenByDescending(new Func<m, int>(GameScanHelper.<>c.<>9.gi)).ToList<m>())
		{
			string text2 = Path.Combine(m.bvs, m.bvp);
			if (File.Exists(text2))
			{
				return text2;
			}
			string text3 = GameScanHelper.SearchExeInDirectory(m.bvs, n.bvw, 0);
			if (!string.IsNullOrWhiteSpace(text3))
			{
				return text3;
			}
		}
		return null;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004A9C File Offset: 0x00002C9C
	private static string hg()
	{
		foreach (string name in new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ht(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hu()
		})
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					string text = registryKey.GetValue(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hv()) as string;
					if (string.IsNullOrWhiteSpace(text))
					{
						text = (registryKey.GetValue(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gw()) as string);
					}
					if (!string.IsNullOrWhiteSpace(text) && Directory.Exists(text))
					{
						return text;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004B4C File Offset: 0x00002D4C
	private static string GetEpicManifestInfo(string manifestFile, string infoField)
	{
		try
		{
			if (!File.Exists(manifestFile))
			{
				bd.id(GameScanHelper.bwx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hw() + manifestFile, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hx());
				return null;
			}
			JObject jobject = JObject.Parse(File.ReadAllText(manifestFile));
			if (jobject[infoField] != null)
			{
				string text = jobject[infoField].Value<string>();
				if (!string.IsNullOrWhiteSpace(text))
				{
					return text;
				}
			}
		}
		catch (Exception ex)
		{
			bd.id(GameScanHelper.bwx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hy() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hx());
		}
		return null;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00004BE4 File Offset: 0x00002DE4
	private static bool hh(string a)
	{
		return new FileInfo(a).Length != 0L && !string.IsNullOrEmpty(GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gw())) && !string.IsNullOrEmpty(GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gx()));
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004C20 File Offset: 0x00002E20
	private static string hi(string[] a, string b)
	{
		GameScanHelper.o o = new GameScanHelper.o();
		o.bvy = a;
		string[] array = new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hz(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ia()
		};
		o.bvx = new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ib(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ic(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.id()
		};
		o.bvz = GameScanHelper.hj(b);
		foreach (string name in array)
		{
			GameScanHelper.p p = new GameScanHelper.p();
			p.bwf = o;
			p.bwe = Registry.LocalMachine.OpenSubKey(name);
			try
			{
				if (p.bwe != null)
				{
					IEnumerable<m> source = p.bwe.GetSubKeyNames().Select(new Func<string, m>(p.gx)).Where(new Func<m, bool>(GameScanHelper.<>c.<>9.gk));
					Func<m, bool> keySelector;
					if ((keySelector = p.bwf.bwa) == null)
					{
						keySelector = (p.bwf.bwa = new Func<m, bool>(p.bwf.gt));
					}
					IOrderedEnumerable<m> source2 = source.OrderByDescending(keySelector);
					Func<m, bool> keySelector2;
					if ((keySelector2 = p.bwf.bwb) == null)
					{
						keySelector2 = (p.bwf.bwb = new Func<m, bool>(p.bwf.gu));
					}
					IOrderedEnumerable<m> source3 = source2.ThenByDescending(keySelector2);
					Func<m, bool> keySelector3;
					if ((keySelector3 = p.bwf.bwc) == null)
					{
						keySelector3 = (p.bwf.bwc = new Func<m, bool>(p.bwf.gv));
					}
					IOrderedEnumerable<m> source4 = source3.ThenByDescending(keySelector3);
					Func<m, bool> keySelector4;
					if ((keySelector4 = p.bwf.bwd) == null)
					{
						keySelector4 = (p.bwf.bwd = new Func<m, bool>(p.bwf.gw));
					}
					foreach (m m in source4.ThenByDescending(keySelector4).ThenBy(new Func<m, bool>(GameScanHelper.<>c.<>9.gl)).ThenByDescending(new Func<m, int>(GameScanHelper.<>c.<>9.gm)).ToList<m>())
					{
						string fullPath = Path.GetFullPath(m.bvq);
						if (File.Exists(fullPath))
						{
							return fullPath;
						}
						string text = GameScanHelper.SearchExeInDirectory(m.bvs, p.bwf.bvy, 0);
						if (!string.IsNullOrWhiteSpace(text))
						{
							return text;
						}
					}
				}
			}
			finally
			{
				if (p.bwe != null)
				{
					((IDisposable)p.bwe).Dispose();
				}
			}
		}
		return null;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004EFC File Offset: 0x000030FC
	private static string SearchExeInDirectory(string path, string[] fileNames, int currentDepth = 0)
	{
		GameScanHelper.r r = new GameScanHelper.r();
		r.bwh = path;
		if (currentDepth > GameScanHelper.bwz)
		{
			return null;
		}
		if (GameScanHelper.bwy.Any(new Func<string, bool>(r.gz)))
		{
			return null;
		}
		try
		{
			foreach (string path2 in fileNames)
			{
				string text = Path.Combine(r.bwh, path2);
				if (File.Exists(text))
				{
					return text;
				}
			}
			foreach (string path3 in Directory.EnumerateDirectories(r.bwh))
			{
				string text2 = GameScanHelper.SearchExeInDirectory(path3, fileNames, currentDepth + 1);
				if (!string.IsNullOrWhiteSpace(text2))
				{
					return text2;
				}
			}
		}
		catch (Exception ex)
		{
			bd.id(GameScanHelper.bwx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ie() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@if());
		}
		return null;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00005004 File Offset: 0x00003204
	private static string hj(string a)
	{
		return Regex.Replace(a.ToLowerInvariant(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ig(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000501C File Offset: 0x0000321C
	private static string GetRegistryInstallLocation(string registryPath, string keyValue)
	{
		string result;
		try
		{
			if (string.IsNullOrEmpty(registryPath))
			{
				result = null;
			}
			else
			{
				string name = registryPath;
				RegistryHive hKey = RegistryHive.LocalMachine;
				if (registryPath.StartsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ih(), StringComparison.OrdinalIgnoreCase))
				{
					hKey = RegistryHive.LocalMachine;
					name = registryPath.Substring(registryPath.IndexOf('\\') + 1);
				}
				else if (registryPath.StartsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ii(), StringComparison.OrdinalIgnoreCase))
				{
					hKey = RegistryHive.CurrentUser;
					name = registryPath.Substring(registryPath.IndexOf('\\') + 1);
				}
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(hKey, RegistryView.Registry64))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
					{
						string text;
						if (registryKey2 == null)
						{
							text = null;
						}
						else
						{
							object value = registryKey2.GetValue(keyValue);
							text = ((value != null) ? value.ToString() : null);
						}
						result = text;
					}
				}
			}
		}
		catch (Exception ex)
		{
			bd.ie(GameScanHelper.bwx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ij() + ex.Message, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ik());
			result = null;
		}
		return result;
	}

	// Token: 0x0400007A RID: 122
	private static string bwx = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.il();

	// Token: 0x0400007B RID: 123
	private static readonly string[] bwy = new string[]
	{
		Environment.GetFolderPath(Environment.SpecialFolder.Windows)
	};

	// Token: 0x0400007C RID: 124
	private static readonly int bwz = 4;

	// Token: 0x0400007D RID: 125
	private static ConcurrentDictionary<GameInfo, string> bxa = new ConcurrentDictionary<GameInfo, string>();

	// Token: 0x0400007E RID: 126
	private static readonly Regex bxb = new Regex(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.im(), RegexOptions.Compiled);

	// Token: 0x0200001B RID: 27
	[CompilerGenerated]
	private sealed class n
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000051AF File Offset: 0x000033AF
		internal bool npt(m a)
		{
			return this.bvw.Contains(a.bvp);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000051C2 File Offset: 0x000033C2
		internal bool ipg(m a)
		{
			return a.bvr.EndsWith(this.bvv);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000051D5 File Offset: 0x000033D5
		internal bool gq(m a)
		{
			return a.bvr.StartsWith(this.bvv);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000051E8 File Offset: 0x000033E8
		internal bool gp(m a)
		{
			return a.bvr.Equals(this.bvv);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000051AF File Offset: 0x000033AF
		internal bool go(m a)
		{
			return this.bvw.Contains(a.bvp);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000051AF File Offset: 0x000033AF
		internal bool nic(m a)
		{
			return this.bvw.Contains(a.bvp);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000051FC File Offset: 0x000033FC
		internal m dtc(string a)
		{
			string epicManifestInfo = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gs());
			string epicManifestInfo2 = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gt());
			string text = GameScanHelper.hj(Path.GetFileName(epicManifestInfo));
			return new m
			{
				bvs = epicManifestInfo,
				bvp = epicManifestInfo2,
				bvr = text,
				bvt = Fuzz.TokenSortRatio(text, this.bvv)
			};
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005259 File Offset: 0x00003459
		internal bool gs(m a)
		{
			return a.bvr.Contains(this.bvv);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000051E8 File Offset: 0x000033E8
		internal bool fuj(m a)
		{
			return a.bvr.Equals(this.bvv);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000526C File Offset: 0x0000346C
		internal m eyp(string a)
		{
			string epicManifestInfo = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gu());
			string epicManifestInfo2 = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gv());
			string text = GameScanHelper.hj(Path.GetFileName(epicManifestInfo));
			return new m
			{
				bvs = epicManifestInfo,
				bvp = epicManifestInfo2,
				bvr = text,
				bvt = Fuzz.TokenSortRatio(text, this.bvv)
			};
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000052CC File Offset: 0x000034CC
		internal m gn(string a)
		{
			string epicManifestInfo = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gw());
			string epicManifestInfo2 = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gx());
			string text = GameScanHelper.hj(Path.GetFileName(epicManifestInfo));
			return new m
			{
				bvs = epicManifestInfo,
				bvp = epicManifestInfo2,
				bvr = text,
				bvt = Fuzz.TokenSortRatio(text, this.bvv)
			};
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000051E8 File Offset: 0x000033E8
		internal bool fyq(m a)
		{
			return a.bvr.Equals(this.bvv);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000051E8 File Offset: 0x000033E8
		internal bool oia(m a)
		{
			return a.bvr.Equals(this.bvv);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000051D5 File Offset: 0x000033D5
		internal bool hyw(m a)
		{
			return a.bvr.StartsWith(this.bvv);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000051C2 File Offset: 0x000033C2
		internal bool gr(m a)
		{
			return a.bvr.EndsWith(this.bvv);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000051D5 File Offset: 0x000033D5
		internal bool gqr(m a)
		{
			return a.bvr.StartsWith(this.bvv);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000051E8 File Offset: 0x000033E8
		internal bool nvc(m a)
		{
			return a.bvr.Equals(this.bvv);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000532C File Offset: 0x0000352C
		internal m ioz(string a)
		{
			string epicManifestInfo = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gy());
			string epicManifestInfo2 = GameScanHelper.GetEpicManifestInfo(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gz());
			string text = GameScanHelper.hj(Path.GetFileName(epicManifestInfo));
			return new m
			{
				bvs = epicManifestInfo,
				bvp = epicManifestInfo2,
				bvr = text,
				bvt = Fuzz.TokenSortRatio(text, this.bvv)
			};
		}

		// Token: 0x04000087 RID: 135
		public string bvv;

		// Token: 0x04000088 RID: 136
		public string[] bvw;
	}

	// Token: 0x0200001C RID: 28
	[CompilerGenerated]
	private sealed class o
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00005389 File Offset: 0x00003589
		internal bool gw(m a)
		{
			return a.bvr.Contains(this.bvz);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000539C File Offset: 0x0000359C
		internal bool gu(m a)
		{
			return a.bvr.StartsWith(this.bvz);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000053AF File Offset: 0x000035AF
		internal bool gv(m a)
		{
			return a.bvr.EndsWith(this.bvz);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000539C File Offset: 0x0000359C
		internal bool dfg(m a)
		{
			return a.bvr.StartsWith(this.bvz);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000053C2 File Offset: 0x000035C2
		internal bool guf(m a)
		{
			return a.bvr.Equals(this.bvz);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000053AF File Offset: 0x000035AF
		internal bool dbr(m a)
		{
			return a.bvr.EndsWith(this.bvz);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000053C2 File Offset: 0x000035C2
		internal bool dht(m a)
		{
			return a.bvr.Equals(this.bvz);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000053C2 File Offset: 0x000035C2
		internal bool mws(m a)
		{
			return a.bvr.Equals(this.bvz);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000053AF File Offset: 0x000035AF
		internal bool nej(m a)
		{
			return a.bvr.EndsWith(this.bvz);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000053C2 File Offset: 0x000035C2
		internal bool nfa(m a)
		{
			return a.bvr.Equals(this.bvz);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000053C2 File Offset: 0x000035C2
		internal bool gt(m a)
		{
			return a.bvr.Equals(this.bvz);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000539C File Offset: 0x0000359C
		internal bool mko(m a)
		{
			return a.bvr.StartsWith(this.bvz);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000539C File Offset: 0x0000359C
		internal bool mae(m a)
		{
			return a.bvr.StartsWith(this.bvz);
		}

		// Token: 0x04000089 RID: 137
		public string[] bvx;

		// Token: 0x0400008A RID: 138
		public string[] bvy;

		// Token: 0x0400008B RID: 139
		public string bvz;

		// Token: 0x0400008C RID: 140
		public Func<m, bool> bwa;

		// Token: 0x0400008D RID: 141
		public Func<m, bool> bwb;

		// Token: 0x0400008E RID: 142
		public Func<m, bool> bwc;

		// Token: 0x0400008F RID: 143
		public Func<m, bool> bwd;
	}

	// Token: 0x0200001D RID: 29
	[CompilerGenerated]
	private sealed class p
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000053D8 File Offset: 0x000035D8
		internal m gx(string a)
		{
			GameScanHelper.q q = new GameScanHelper.q();
			q.bwg = this.bwe.OpenSubKey(a);
			if (q.bwg == null)
			{
				return null;
			}
			string text = this.bwf.bvx.Select(new Func<string, string>(q.gy)).FirstOrDefault(new Func<string, bool>(GameScanHelper.<>c.<>9.gj));
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			string text2 = q.bwg.GetValue(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ha()) as string;
			if (string.IsNullOrWhiteSpace(text2))
			{
				return null;
			}
			string text3 = q.bwg.GetValue(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.hb()) as string;
			if (string.IsNullOrWhiteSpace(text3) || !this.bwf.bvy.Contains(Path.GetFileName(text3)))
			{
				return null;
			}
			string text4 = GameScanHelper.hj(Path.GetFileName(text));
			return new m
			{
				bvs = text,
				bvp = text3,
				bvq = text2,
				bvr = text4,
				bvt = Fuzz.TokenSortRatio(text4, this.bwf.bvz)
			};
		}

		// Token: 0x04000090 RID: 144
		public RegistryKey bwe;

		// Token: 0x04000091 RID: 145
		public GameScanHelper.o bwf;
	}

	// Token: 0x0200001E RID: 30
	[CompilerGenerated]
	private sealed class q
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000054F1 File Offset: 0x000036F1
		internal string hon(string a)
		{
			return this.bwg.GetValue(a) as string;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000054F1 File Offset: 0x000036F1
		internal string gy(string a)
		{
			return this.bwg.GetValue(a) as string;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000054F1 File Offset: 0x000036F1
		internal string mqs(string a)
		{
			return this.bwg.GetValue(a) as string;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000054F1 File Offset: 0x000036F1
		internal string grf(string a)
		{
			return this.bwg.GetValue(a) as string;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000054F1 File Offset: 0x000036F1
		internal string dor(string a)
		{
			return this.bwg.GetValue(a) as string;
		}

		// Token: 0x04000092 RID: 146
		public RegistryKey bwg;
	}

	// Token: 0x0200001F RID: 31
	[CompilerGenerated]
	private sealed class r
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00005504 File Offset: 0x00003704
		internal bool gz(string a)
		{
			return this.bwh.Contains(a, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005504 File Offset: 0x00003704
		internal bool end(string a)
		{
			return this.bwh.Contains(a, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000093 RID: 147
		public string bwh;
	}

	// Token: 0x02000020 RID: 32
	[CompilerGenerated]
	private sealed class u
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00005513 File Offset: 0x00003713
		internal Task ha(string a)
		{
			return Task.Run(new Action(new GameScanHelper.w
			{
				bwk = this,
				bwj = a
			}.hb));
		}

		// Token: 0x04000094 RID: 148
		public CancellationToken bwi;
	}

	// Token: 0x02000021 RID: 33
	[CompilerGenerated]
	private sealed class w
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00005538 File Offset: 0x00003738
		internal void ghz()
		{
			GameScanHelper.ExtractSteamGameManifest(this.bwj, this.bwk.bwi);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005538 File Offset: 0x00003738
		internal void hb()
		{
			GameScanHelper.ExtractSteamGameManifest(this.bwj, this.bwk.bwi);
		}

		// Token: 0x04000095 RID: 149
		public string bwj;

		// Token: 0x04000096 RID: 150
		public GameScanHelper.u bwk;
	}
}
