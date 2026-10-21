using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class SRProjectSettings : ScriptableObject, dq
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001B493 File Offset: 0x00019693
	// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0001B49B File Offset: 0x0001969B
	public float dby
	{
		get
		{
			return this.unityUnitsPerRealMeter;
		}
		set
		{
			this.unityUnitsPerRealMeter = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001B4A4 File Offset: 0x000196A4
	// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001B4AC File Offset: 0x000196AC
	public dn dbz
	{
		get
		{
			return this.scaleType;
		}
		set
		{
			this.scaleType = value;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001B4B5 File Offset: 0x000196B5
	// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0001B4BD File Offset: 0x000196BD
	public Vector2 dca
	{
		get
		{
			return this.intendedDisplaySize;
		}
		set
		{
			this.intendedDisplaySize = value;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001B4C6 File Offset: 0x000196C6
	// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0001B4CE File Offset: 0x000196CE
	public bool dcb
	{
		get
		{
			return this.allowStartWithoutSimulatedRealityRuntime;
		}
		set
		{
			this.allowStartWithoutSimulatedRealityRuntime = value;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001B4D7 File Offset: 0x000196D7
	// (set) Token: 0x060005EB RID: 1515 RVA: 0x0001B4DF File Offset: 0x000196DF
	public RenderTextureFormat dcc
	{
		get
		{
			return this.frameBufferFormat;
		}
		set
		{
			this.frameBufferFormat = value;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001B4E8 File Offset: 0x000196E8
	// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001B4F0 File Offset: 0x000196F0
	public float dcd
	{
		get
		{
			return this.renderResolution;
		}
		set
		{
			this.renderResolution = Mathf.Max(0.01f, value);
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0000238E File Offset: 0x0000058E
	public void bgy()
	{
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001B504 File Offset: 0x00019704
	public static SRProjectSettings dce
	{
		get
		{
			if (SRProjectSettings.instance_ == null)
			{
				Object[] array = Resources.LoadAll(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), typeof(SRProjectSettings));
				if (array.Length > 1)
				{
					LogUtil.Log(LogLevel.Warning, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bai(), Array.Empty<object>());
				}
				if (array.Length != 0)
				{
					SRProjectSettings.instance_ = (SRProjectSettings)array[0];
				}
			}
			if (SRProjectSettings.instance_ == null)
			{
				SRProjectSettings.instance_ = ScriptableObject.CreateInstance<SRProjectSettings>();
				LogUtil.Log(LogLevel.Warning, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baj(), Array.Empty<object>());
			}
			return SRProjectSettings.instance_;
		}
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0001B586 File Offset: 0x00019786
	public @do bfs()
	{
		if (this.settingsContainer == null)
		{
			this.settingsContainer = new dp(this);
		}
		return this.settingsContainer;
	}

	// Token: 0x0400053F RID: 1343
	[Min(0.01f)]
	private float unityUnitsPerRealMeter = 100f;

	// Token: 0x04000540 RID: 1344
	private dn scaleType;

	// Token: 0x04000541 RID: 1345
	private Vector2 intendedDisplaySize = new Vector2(69f, 39f);

	// Token: 0x04000542 RID: 1346
	private bool allowStartWithoutSimulatedRealityRuntime;

	// Token: 0x04000543 RID: 1347
	[SerializeField]
	private RenderTextureFormat frameBufferFormat = 7;

	// Token: 0x04000544 RID: 1348
	[Min(0.01f)]
	[SerializeField]
	private float renderResolution = 1f;

	// Token: 0x04000545 RID: 1349
	private static SRProjectSettings instance_;

	// Token: 0x04000546 RID: 1350
	private dp settingsContainer;
}
