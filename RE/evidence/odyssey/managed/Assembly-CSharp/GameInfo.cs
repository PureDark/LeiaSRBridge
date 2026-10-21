using System;
using System.Globalization;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};

// Token: 0x02000042 RID: 66
[Serializable]
public class GameInfo
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600015F RID: 351 RVA: 0x000081A4 File Offset: 0x000063A4
	// (set) Token: 0x06000160 RID: 352 RVA: 0x00008230 File Offset: 0x00006430
	public DateTime? cyn
	{
		get
		{
			if (string.IsNullOrEmpty(this.LastPlayedTimeUTC))
			{
				this.LastPlayedTimeUTC = DateTime.MinValue.ToUniversalTime().ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qh());
				return new DateTime?(DateTime.MinValue);
			}
			DateTime value;
			if (DateTime.TryParse(this.LastPlayedTimeUTC, null, DateTimeStyles.RoundtripKind, out value))
			{
				return new DateTime?(value);
			}
			this.LastPlayedTimeUTC = DateTime.MinValue.ToUniversalTime().ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qh());
			return new DateTime?(DateTime.MinValue);
		}
		set
		{
			this.LastPlayedTimeUTC = value.Value.ToUniversalTime().ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qh());
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000161 RID: 353 RVA: 0x00008260 File Offset: 0x00006460
	// (set) Token: 0x06000162 RID: 354 RVA: 0x000082AC File Offset: 0x000064AC
	public DateTime? cyo
	{
		get
		{
			if (string.IsNullOrEmpty(this.LastRegisteredTimeUTC))
			{
				return null;
			}
			DateTime value;
			if (DateTime.TryParse(this.LastRegisteredTimeUTC, null, DateTimeStyles.RoundtripKind, out value))
			{
				return new DateTime?(value);
			}
			return null;
		}
		set
		{
			this.LastRegisteredTimeUTC = value.Value.ToUniversalTime().ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qh());
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000082DC File Offset: 0x000064DC
	public string kd(MultilingualText a)
	{
		string key = LanguageHelper.hv();
		if (a.Texts.Count == 0)
		{
			a.ke();
		}
		string text = a.Texts[key];
		if (!string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return a.en_US;
	}

	// Token: 0x04000171 RID: 369
	public string Title;

	// Token: 0x04000172 RID: 370
	public string Platform;

	// Token: 0x04000173 RID: 371
	public MultilingualText Genre;

	// Token: 0x04000174 RID: 372
	public string ReleaseDate;

	// Token: 0x04000175 RID: 373
	public string Developer;

	// Token: 0x04000176 RID: 374
	public string Publisher;

	// Token: 0x04000177 RID: 375
	public string Tier;

	// Token: 0x04000178 RID: 376
	public string AgeRating;

	// Token: 0x04000179 RID: 377
	public string ExeName;

	// Token: 0x0400017A RID: 378
	public string ExePath;

	// Token: 0x0400017B RID: 379
	public SteamAppID SteamAppID;

	// Token: 0x0400017C RID: 380
	public RegistryInfo RegistryInfo;

	// Token: 0x0400017D RID: 381
	public MultilingualText Introduction;

	// Token: 0x0400017E RID: 382
	[NonSerialized]
	public bool DiscoveredByUser;

	// Token: 0x0400017F RID: 383
	[NonSerialized]
	public bool UnregisteredByUser;

	// Token: 0x04000180 RID: 384
	[NonSerialized]
	public string LastPlayedTimeUTC;

	// Token: 0x04000181 RID: 385
	[NonSerialized]
	public bool IsRegistered;

	// Token: 0x04000182 RID: 386
	[NonSerialized]
	public string ExePathCache;

	// Token: 0x04000183 RID: 387
	[NonSerialized]
	public bool FavoritedByUser;

	// Token: 0x04000184 RID: 388
	[NonSerialized]
	public string LastRegisteredTimeUTC;
}
