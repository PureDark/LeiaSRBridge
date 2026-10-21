using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200002B RID: 43
public static class PatchNoteHelper
{
	// Token: 0x060000DB RID: 219 RVA: 0x00006588 File Offset: 0x00004788
	public static string il(bool a = false)
	{
		string result = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		string text2 = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		text = string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ky(), PatchNoteHelper.bzc);
		if (a)
		{
			PatchNoteInfo newPatchNoteInfo = PatchNoteHelper.GetNewPatchNoteInfo();
			if (!string.IsNullOrWhiteSpace(newPatchNoteInfo.GameAssets) && !PatchNoteHelper.iq())
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ky() + newPatchNoteInfo.GameAssets;
			}
			text2 = PatchNoteHelper.io(newPatchNoteInfo);
		}
		text = PatchNoteHelper.ip(text);
		if (!string.IsNullOrWhiteSpace(text))
		{
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kz();
		}
		if (!string.IsNullOrWhiteSpace(text2))
		{
			text2 = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kz() + text2;
		}
		if (LanguageHelper.hv() == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp())
		{
			result = string.Concat(new string[]
			{
				text,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.la(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lb(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lc(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ld(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.le(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lf(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lg(),
				text2
			});
		}
		else
		{
			result = string.Concat(new string[]
			{
				text,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lh(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.li(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lj(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lk(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ll(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lm(),
				PatchNoteHelper.bzb,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ln(),
				text2
			});
		}
		return result;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000673C File Offset: 0x0000493C
	public static string im()
	{
		PatchNoteInfo newPatchNoteInfo = PatchNoteHelper.GetNewPatchNoteInfo();
		string text = PatchNoteHelper.iq() ? string.Empty : PatchNoteHelper.ip(newPatchNoteInfo.GameAssets);
		string text2 = PatchNoteHelper.io(newPatchNoteInfo);
		if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(text2))
		{
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kz();
		}
		return text + text2;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00006793 File Offset: 0x00004993
	public static bool @in()
	{
		return File.Exists(PatchNoteHelper.bzd);
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000067A4 File Offset: 0x000049A4
	public static void CleanUpPatchNoteJSON()
	{
		try
		{
			if (File.Exists(PatchNoteHelper.bzd))
			{
				File.Delete(PatchNoteHelper.bzd);
			}
		}
		catch (Exception ex)
		{
			bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lo(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lp() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lq());
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x000067FC File Offset: 0x000049FC
	public static PatchNoteInfo GetNewPatchNoteInfo()
	{
		try
		{
			if (File.Exists(PatchNoteHelper.bzd))
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(PatchNoteHelper.bzd));
				if (dictionary.ContainsKey(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lr()))
				{
					return JsonConvert.DeserializeObject<PatchNoteInfo>(JsonConvert.SerializeObject(dictionary[<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lr()]));
				}
			}
		}
		catch (Exception ex)
		{
			bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lo(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ls() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lt());
		}
		return null;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00006884 File Offset: 0x00004A84
	private static string io(PatchNoteInfo a)
	{
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		foreach (string text2 in new string[]
		{
			a.LeiaSR,
			a.Odyssey3DPlayer,
			a.ConversionModel,
			a.GameLauncher
		})
		{
			if (!string.IsNullOrWhiteSpace(text2))
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lu() + text2;
			}
		}
		if (!string.IsNullOrWhiteSpace(text))
		{
			text = ((LanguageHelper.hv() == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp()) ? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lw() : <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lv()) + text;
		}
		return text;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00006918 File Offset: 0x00004B18
	private static string ip(string a)
	{
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (string text2 in a.Split(new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ky()
		}, StringSplitOptions.RemoveEmptyEntries))
		{
			string text3 = text2;
			string item = text3.Substring(3, text3.Length - 3);
			if (text2.StartsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lx()))
			{
				if (!list2.Remove(item) && !list.Contains(item))
				{
					list.Add(item);
				}
			}
			else if (text2.StartsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ly()) && !list.Remove(item) && !list2.Contains(item))
			{
				list2.Add(item);
			}
		}
		if (list.Count > 0 || list2.Count > 0)
		{
			text += ((LanguageHelper.hv() == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp()) ? (<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mb() + bn.cyp.cyq.Version + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ma()) : (<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lz() + bn.cyp.cyq.Version + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ma()));
		}
		if (list.Count > 0)
		{
			text += ((LanguageHelper.hv() == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp()) ? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.md() : <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mc());
			text += string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ky(), list.Select(new Func<string, string>(PatchNoteHelper.<>c.<>9.ij)));
		}
		if (list2.Count > 0)
		{
			if (list.Count > 0)
			{
				text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.me();
			}
			text += ((LanguageHelper.hv() == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp()) ? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mg() : <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mf());
			text += string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ky(), list2.Select(new Func<string, string>(PatchNoteHelper.<>c.<>9.ik)));
		}
		return text;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00006B1C File Offset: 0x00004D1C
	private static bool iq()
	{
		return new Version(string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bu(), bn.cyp.cyq.Version.Split('.', StringSplitOptions.None).Take(2).Concat(new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dz()
		}))) < new Version(string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bu(), Application.version.Split('.', StringSplitOptions.None).Take(2).Concat(new string[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dz()
		})));
	}

	// Token: 0x040000D7 RID: 215
	private static readonly string bza = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lo();

	// Token: 0x040000D8 RID: 216
	private static readonly string bzb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mh();

	// Token: 0x040000D9 RID: 217
	private static List<string> bzc = new List<string>();

	// Token: 0x040000DA RID: 218
	private static readonly string bzd = Path.Combine(bm.cao, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mi());
}
