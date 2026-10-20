using System;
using System.Runtime.InteropServices;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200003C RID: 60
	internal class PowerSetting
	{
		// Token: 0x06000252 RID: 594
		[DllImport("User32", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, int Flags);

		// Token: 0x06000253 RID: 595
		[DllImport("User32", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern bool UnregisterPowerSettingNotification(IntPtr handle);

		// Token: 0x0400018E RID: 398
		public static Guid GUID_CONSOLE_DISPLAY_STATE = new Guid(1877382486, 28746, 18336, 143, 36, 194, 141, 147, 111, 218, 71);

		// Token: 0x0400018F RID: 399
		public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0;

		// Token: 0x04000190 RID: 400
		public const int WM_POWERBROADCAST = 536;

		// Token: 0x04000191 RID: 401
		public const int PBT_POWERSETTINGCHANGE = 32787;

		// Token: 0x020000B3 RID: 179
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct POWERBROADCAST_SETTING
		{
			// Token: 0x04000456 RID: 1110
			public Guid PowerSetting;

			// Token: 0x04000457 RID: 1111
			public uint DataLength;

			// Token: 0x04000458 RID: 1112
			public byte Data;
		}
	}
}
