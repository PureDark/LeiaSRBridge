using System;
using System.IO;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000052 RID: 82
[Serializable]
public class SettingsVM
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600019F RID: 415 RVA: 0x00008EA9 File Offset: 0x000070A9
	// (set) Token: 0x060001A0 RID: 416 RVA: 0x00008EB6 File Offset: 0x000070B6
	public int cyv
	{
		get
		{
			return this.Settings.CursorType;
		}
		set
		{
			this.Settings.CursorType = value;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008EC4 File Offset: 0x000070C4
	public string[] cyw
	{
		get
		{
			return this.cbh;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008ECC File Offset: 0x000070CC
	public string[] cyx
	{
		get
		{
			return this.cbi;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008ED4 File Offset: 0x000070D4
	// (set) Token: 0x060001A4 RID: 420 RVA: 0x00008EE1 File Offset: 0x000070E1
	public int cyy
	{
		get
		{
			return this.Settings.IsConvertVideoTo3D;
		}
		set
		{
			this.Settings.IsConvertVideoTo3D = value;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008EEF File Offset: 0x000070EF
	// (set) Token: 0x060001A6 RID: 422 RVA: 0x00008EFC File Offset: 0x000070FC
	public int cyz
	{
		get
		{
			return this.Settings.IsAutoGameScan;
		}
		set
		{
			this.Settings.IsAutoGameScan = value;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008F0A File Offset: 0x0000710A
	// (set) Token: 0x060001A8 RID: 424 RVA: 0x00008F17 File Offset: 0x00007117
	public int cza
	{
		get
		{
			return this.Settings.IsAutoRun;
		}
		set
		{
			this.Settings.IsAutoRun = value;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008F25 File Offset: 0x00007125
	// (set) Token: 0x060001AA RID: 426 RVA: 0x00008F32 File Offset: 0x00007132
	public int czb
	{
		get
		{
			return this.Settings.IsAutoConvert;
		}
		set
		{
			this.Settings.IsAutoConvert = value;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060001AB RID: 427 RVA: 0x00008F40 File Offset: 0x00007140
	// (set) Token: 0x060001AC RID: 428 RVA: 0x00008F4D File Offset: 0x0000714D
	public int czc
	{
		get
		{
			return this.Settings.IsStandardVideo;
		}
		set
		{
			this.Settings.IsStandardVideo = value;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060001AD RID: 429 RVA: 0x00008F5B File Offset: 0x0000715B
	// (set) Token: 0x060001AE RID: 430 RVA: 0x00008F80 File Offset: 0x00007180
	public string czd
	{
		get
		{
			if (!string.IsNullOrEmpty(this.Settings.Language))
			{
				return this.Settings.Language;
			}
			return LanguageHelper.byl;
		}
		set
		{
			if (this.Settings.Language == value || string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Settings.Language = value;
			LanguageHelper.cyl = this.Settings.Language;
		}
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00008FBC File Offset: 0x000071BC
	public void ly()
	{
		this.cbh[0] = this.Settings.CurrOverlayKey;
		this.cbh[1] = this.Settings.Curr3DModeKey;
		this.cbh[2] = this.Settings.Curr3DDepthNegKey;
		this.cbh[3] = this.Settings.Curr3DDepthPosKey;
		this.cbh[4] = this.Settings.Curr3DPopoutNegKey;
		this.cbh[5] = this.Settings.Curr3DPopoutPosKey;
		this.cbi[0] = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rp();
		this.cbi[1] = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rq();
		this.cbi[2] = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rr();
		this.cbi[3] = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rs();
		this.cbi[4] = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rt();
		this.cbi[5] = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ru();
		LanguageHelper.cyl = this.czd;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00009094 File Offset: 0x00007294
	public void UpdateData(string[] shortcutKeys, int cursorType, int convertVideoTo3D, int autoGameScan, int autoRun, int autoConvert, int isStandardVideo, string newLanguage)
	{
		bd.ih(SettingsVM.cbg, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rv());
		this.cbh = shortcutKeys;
		this.cyv = cursorType;
		this.cyy = convertVideoTo3D;
		this.cyz = autoGameScan;
		this.cza = autoRun;
		this.czb = autoConvert;
		this.czc = isStandardVideo;
		this.Settings.CurrOverlayKey = shortcutKeys[0];
		this.Settings.Curr3DModeKey = shortcutKeys[1];
		this.Settings.Curr3DDepthNegKey = shortcutKeys[2];
		this.Settings.Curr3DDepthPosKey = shortcutKeys[3];
		this.Settings.Curr3DPopoutNegKey = shortcutKeys[4];
		this.Settings.Curr3DPopoutPosKey = shortcutKeys[5];
		this.czd = newLanguage;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00009144 File Offset: 0x00007344
	public void ResetData()
	{
		bd.ih(SettingsVM.cbg, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rw());
		this.UpdateData(this.cbi, 0, 1, 1, 1, 0, 1, LanguageHelper.byl);
		MouseCursor.ChangeCursor(this.cyv);
		this.SerializeData();
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00009188 File Offset: 0x00007388
	public void SerializeData()
	{
		try
		{
			string text = JsonUtility.ToJson(bn.cyp.cyr);
			string contents = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(text), Formatting.Indented);
			bd.id(SettingsVM.cbg, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rx() + text, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rk());
			File.WriteAllText(Path.Combine(Application.persistentDataPath, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qo()), contents);
			this.UpdateOHService();
		}
		catch (Exception ex)
		{
			bd.id(SettingsVM.cbg, ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rk());
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00009218 File Offset: 0x00007418
	public void UpdateOHService()
	{
		bd.ih(SettingsVM.cbg, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ry());
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		for (int i = 0; i < this.cbh.Length; i++)
		{
			text = text + this.cbh[i] + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx();
		}
		text = string.Concat(new string[]
		{
			text,
			this.czd,
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			this.cyv.ToString(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			this.cyy.ToString(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			this.cyz.ToString(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			this.cza.ToString(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			this.czb.ToString(),
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(),
			this.czc.ToString()
		});
		Odyssey3DHubIPCManager.SendMessage(Odyssey3DHubIPCManager.c.bsj, Odyssey3DHubIPCManager.d.bsx, text);
	}

	// Token: 0x040001D9 RID: 473
	private static string cbg = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rz();

	// Token: 0x040001DA RID: 474
	public const int defaultCursorType = 0;

	// Token: 0x040001DB RID: 475
	public const int defaultIsConvertVideoTo3D = 1;

	// Token: 0x040001DC RID: 476
	public const int defaultIsAutoGameScan = 1;

	// Token: 0x040001DD RID: 477
	public const int defaultIsAutoRun = 1;

	// Token: 0x040001DE RID: 478
	public const int defaultIsAutoConvert = 0;

	// Token: 0x040001DF RID: 479
	public const int defaultIsStandardVideo = 1;

	// Token: 0x040001E0 RID: 480
	public const string defaultOverlayKey = "Ctrl+Shift+1";

	// Token: 0x040001E1 RID: 481
	public const string default3DModeKey = "Ctrl+Shift+2";

	// Token: 0x040001E2 RID: 482
	public const string default3DDepthNegKey = "Ctrl+Shift+3";

	// Token: 0x040001E3 RID: 483
	public const string default3DDepthPosKey = "Ctrl+Shift+4";

	// Token: 0x040001E4 RID: 484
	public const string default3DPopoutNegKey = "Ctrl+Shift+5";

	// Token: 0x040001E5 RID: 485
	public const string default3DPopoutPosKey = "Ctrl+Shift+6";

	// Token: 0x040001E6 RID: 486
	public Settings Settings;

	// Token: 0x040001E7 RID: 487
	private string[] cbh = new string[6];

	// Token: 0x040001E8 RID: 488
	private string[] cbi = new string[6];
}
