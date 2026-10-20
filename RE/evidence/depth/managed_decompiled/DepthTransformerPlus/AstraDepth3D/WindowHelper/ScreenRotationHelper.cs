using System;
using System.Runtime.InteropServices;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200003F RID: 63
	public class ScreenRotationHelper
	{
		// Token: 0x06000262 RID: 610
		[DllImport("user32.dll")]
		public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref ScreenRotationHelper.DEVMODE devMode);

		// Token: 0x06000263 RID: 611 RVA: 0x0000FD90 File Offset: 0x0000DF90
		public static ScreenRotationHelper.DisplayOrientation GetScreenOrientation()
		{
			ScreenRotationHelper.DEVMODE devMode = default(ScreenRotationHelper.DEVMODE);
			devMode.dmSize = (short)Marshal.SizeOf<ScreenRotationHelper.DEVMODE>(devMode);
			ScreenRotationHelper.EnumDisplaySettings(null, -1, ref devMode);
			return (ScreenRotationHelper.DisplayOrientation)devMode.dmDisplayOrientation;
		}

		// Token: 0x020000B4 RID: 180
		public struct DEVMODE
		{
			// Token: 0x04000459 RID: 1113
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x0400045A RID: 1114
			public short dmSpecVersion;

			// Token: 0x0400045B RID: 1115
			public short dmDriverVersion;

			// Token: 0x0400045C RID: 1116
			public short dmSize;

			// Token: 0x0400045D RID: 1117
			public short dmDriverExtra;

			// Token: 0x0400045E RID: 1118
			public int dmFields;

			// Token: 0x0400045F RID: 1119
			public int dmPositionX;

			// Token: 0x04000460 RID: 1120
			public int dmPositionY;

			// Token: 0x04000461 RID: 1121
			public int dmDisplayOrientation;

			// Token: 0x04000462 RID: 1122
			public int dmDisplayFixedOutput;
		}

		// Token: 0x020000B5 RID: 181
		public enum DisplayOrientation
		{
			// Token: 0x04000464 RID: 1124
			Default,
			// Token: 0x04000465 RID: 1125
			Rotate90,
			// Token: 0x04000466 RID: 1126
			Rotate180,
			// Token: 0x04000467 RID: 1127
			Rotate270
		}
	}
}
