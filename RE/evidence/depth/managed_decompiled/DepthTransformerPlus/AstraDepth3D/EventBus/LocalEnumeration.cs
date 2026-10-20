using System;

namespace AstraDepth3D.EventBus
{
	// Token: 0x0200002C RID: 44
	public static class LocalEnumeration
	{
		// Token: 0x020000A5 RID: 165
		public enum InterWaveType
		{
			// Token: 0x0400041B RID: 1051
			InterWave_None,
			// Token: 0x0400041C RID: 1052
			InterWave_SBS,
			// Token: 0x0400041D RID: 1053
			InterWave_AI
		}

		// Token: 0x020000A6 RID: 166
		public enum InterWaveStatus
		{
			// Token: 0x0400041F RID: 1055
			InterWaveStatus_Idle,
			// Token: 0x04000420 RID: 1056
			InterWaveStatus_Starting,
			// Token: 0x04000421 RID: 1057
			InterWaveStatus_Started,
			// Token: 0x04000422 RID: 1058
			InterWaveStatus_Stopping
		}

		// Token: 0x020000A7 RID: 167
		public enum EyeStatus
		{
			// Token: 0x04000424 RID: 1060
			EyeStatus_OFF,
			// Token: 0x04000425 RID: 1061
			EyeStatus_ON,
			// Token: 0x04000426 RID: 1062
			EyeStatus_ERROR
		}

		// Token: 0x020000A8 RID: 168
		public enum RasterStatus
		{
			// Token: 0x04000428 RID: 1064
			RasterStatus_OFF,
			// Token: 0x04000429 RID: 1065
			RasterStatus_ON,
			// Token: 0x0400042A RID: 1066
			RasterStatus_ERROR
		}

		// Token: 0x020000A9 RID: 169
		public enum StereoModeStatus
		{
			// Token: 0x0400042C RID: 1068
			StereoMode_None = -1,
			// Token: 0x0400042D RID: 1069
			StereoMode_2D,
			// Token: 0x0400042E RID: 1070
			StereoMode_SBS,
			// Token: 0x0400042F RID: 1071
			StereoMode_Detection
		}
	}
}
