using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000027 RID: 39
public class LanguageHelper
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005A81 File Offset: 0x00003C81
	// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005A88 File Offset: 0x00003C88
	public static string cyl
	{
		get
		{
			return LanguageHelper.byj;
		}
		set
		{
			if (LanguageHelper.byj == value)
			{
				return;
			}
			LanguageHelper.byj = value;
			LanguageHelper.bc bc = LanguageHelper.byo;
			if (bc != null)
			{
				bc();
			}
			LanguageHelper.bc bc2 = LanguageHelper.byp;
			if (bc2 == null)
			{
				return;
			}
			bc2();
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060000B2 RID: 178 RVA: 0x00005AC0 File Offset: 0x00003CC0
	// (remove) Token: 0x060000B3 RID: 179 RVA: 0x00005AF4 File Offset: 0x00003CF4
	public static event LanguageHelper.bc byo;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060000B4 RID: 180 RVA: 0x00005B28 File Offset: 0x00003D28
	// (remove) Token: 0x060000B5 RID: 181 RVA: 0x00005B5C File Offset: 0x00003D5C
	public static event LanguageHelper.bc byp;

	// Token: 0x060000B6 RID: 182 RVA: 0x00005B90 File Offset: 0x00003D90
	public static void Init()
	{
		bd.ih(LanguageHelper.byh, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.it());
		try
		{
			LanguageHelper.byo = null;
			LanguageHelper.byp = null;
			LanguageHelper.hu();
			if (LanguageHelper.byi.Contains(CultureInfo.CurrentCulture.Name))
			{
				bd.id(LanguageHelper.byh, CultureInfo.CurrentCulture.Name + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.iu(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.it());
				LanguageHelper.byj = CultureInfo.CurrentCulture.Name;
				LanguageHelper.byl = CultureInfo.CurrentCulture.Name;
			}
			else
			{
				bd.id(LanguageHelper.byh, CultureInfo.CurrentCulture.Name + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.iv(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.it());
			}
			LanguageHelper.byk = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(Resources.Load<TextAsset>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.iw()).text);
			if (LanguageHelper.byj == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ix())
			{
				LanguageHelper.ht();
			}
			LanguageHelper.bym = LanguageHelper.hw().IndexOf(LanguageHelper.cyl);
		}
		catch (Exception ex)
		{
			bd.ie(LanguageHelper.byh, ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.it());
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005CA8 File Offset: 0x00003EA8
	public static bool hs()
	{
		bool flag = LanguageHelper.byi.Contains(CultureInfo.CurrentCulture.Name);
		bool flag2 = LanguageHelper.byi.Contains(LanguageHelper.cyl);
		if (flag)
		{
			LanguageHelper.byl = CultureInfo.CurrentCulture.Name;
		}
		return (flag && CultureInfo.CurrentCulture.Name != LanguageHelper.cyl) || !flag2;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005D0C File Offset: 0x00003F0C
	private static void ht()
	{
		Font font = Resources.Load<Font>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.iy());
		Font font2 = Resources.Load<Font>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.iz());
		if (font == null || font2 == null)
		{
			Debug.LogError(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ja());
			return;
		}
		foreach (Text text in Object.FindObjectsByType<Text>(0))
		{
			if (text.font.name.Contains(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jb()))
			{
				text.font = font2;
			}
			else
			{
				text.font = font;
			}
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00005D90 File Offset: 0x00003F90
	private static void hu()
	{
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jc(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jd());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.je(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jf());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jh());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ji(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jj());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jk(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jl());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jm(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jn());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ix(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jo());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jp(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jq());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jr(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.js());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jt(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ju());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jv(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jw());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jy());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.jz(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ka());
		LanguageHelper.byi.Add(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kb(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kc());
		LanguageHelper.byn = LanguageHelper.byi.Count;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005A81 File Offset: 0x00003C81
	public static string hv()
	{
		return LanguageHelper.byj;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00005EC4 File Offset: 0x000040C4
	public static List<string> hw()
	{
		List<string> list = new List<string>();
		foreach (object obj in LanguageHelper.byi)
		{
			list.Add((string)((DictionaryEntry)obj).Key);
		}
		return list;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00005F30 File Offset: 0x00004130
	public static List<string> hx()
	{
		List<string> list = new List<string>();
		foreach (object obj in LanguageHelper.byi)
		{
			list.Add((string)((DictionaryEntry)obj).Value);
		}
		return list;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00005F9C File Offset: 0x0000419C
	public static string GetString(string key)
	{
		try
		{
			Dictionary<string, string> dictionary = LanguageHelper.byk[key];
			return string.IsNullOrEmpty(dictionary[LanguageHelper.byj]) ? dictionary[LanguageHelper.byl] : dictionary[LanguageHelper.byj];
		}
		catch (Exception ex)
		{
			bd.ie(LanguageHelper.byh, ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kd());
		}
		return null;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00006010 File Offset: 0x00004210
	public static string GetLanguage(int index)
	{
		string result;
		try
		{
			List<string> list = LanguageHelper.hw();
			if (index >= 0 && index < list.Count)
			{
				result = list[index];
			}
			else
			{
				result = LanguageHelper.byl;
			}
		}
		catch (Exception)
		{
			bd.ie(LanguageHelper.byh, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ke(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kf());
			result = LanguageHelper.byl;
		}
		return result;
	}

	// Token: 0x040000C4 RID: 196
	private static string byh = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kg();

	// Token: 0x040000C5 RID: 197
	private static OrderedDictionary byi = new OrderedDictionary();

	// Token: 0x040000C6 RID: 198
	private static string byj = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.je();

	// Token: 0x040000C7 RID: 199
	private static Dictionary<string, Dictionary<string, string>> byk = new Dictionary<string, Dictionary<string, string>>();

	// Token: 0x040000C8 RID: 200
	public static string byl = CultureInfo.CurrentCulture.Name;

	// Token: 0x040000C9 RID: 201
	private static int bym;

	// Token: 0x040000CA RID: 202
	private static int byn;

	// Token: 0x02000028 RID: 40
	// (Invoke) Token: 0x060000C2 RID: 194
	public delegate void bc();
}
