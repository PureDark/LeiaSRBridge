using System;
using AstraDepth3D.EventBus;

namespace AstraDepth3D
{
	// Token: 0x02000007 RID: 7
	internal class GlobalStatusManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002193 File Offset: 0x00000393
		public static GlobalStatusManager instance
		{
			get
			{
				return GlobalStatusManager._lazyInstance.Value;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000219F File Offset: 0x0000039F
		private GlobalStatusManager()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021BF File Offset: 0x000003BF
		public static GlobalStatusManager Instance
		{
			get
			{
				return GlobalStatusManager.instance;
			}
		}

		// Token: 0x04000007 RID: 7
		private static readonly Lazy<GlobalStatusManager> _lazyInstance = new Lazy<GlobalStatusManager>(() => new GlobalStatusManager());

		// Token: 0x04000008 RID: 8
		public volatile LocalEnumeration.InterWaveType _iInterWaveType;

		// Token: 0x04000009 RID: 9
		public volatile LocalEnumeration.InterWaveStatus _iInterWaveStatus;

		// Token: 0x0400000A RID: 10
		public volatile LocalEnumeration.EyeStatus _iEyeStatus;

		// Token: 0x0400000B RID: 11
		public volatile LocalEnumeration.RasterStatus _iRasterStatus;

		// Token: 0x0400000C RID: 12
		public volatile float _calibration_a = 1f;

		// Token: 0x0400000D RID: 13
		public volatile float _calibration_b;

		// Token: 0x0400000E RID: 14
		public volatile bool _bInterWaveRet;

		// Token: 0x0400000F RID: 15
		public volatile bool _bEyeCrackrRet;

		// Token: 0x04000010 RID: 16
		public volatile bool _bRasterRet;

		// Token: 0x04000011 RID: 17
		public string _uuid = "";
	}
}
