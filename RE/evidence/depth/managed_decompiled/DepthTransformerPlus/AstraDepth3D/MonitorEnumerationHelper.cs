using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using Windows.Foundation;

namespace AstraDepth3D
{
	// Token: 0x02000016 RID: 22
	internal static class MonitorEnumerationHelper
	{
		// Token: 0x06000122 RID: 290
		[DllImport("user32.dll")]
		private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumerationHelper.EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

		// Token: 0x06000123 RID: 291
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorEnumerationHelper.MonitorInfoEx lpmi);

		// Token: 0x06000124 RID: 292 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public static IEnumerable<MonitorInfo> GetMonitors()
		{
			List<MonitorInfo> result = new List<MonitorInfo>();
			MonitorEnumerationHelper.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref MonitorEnumerationHelper.RECT lprcMonitor, IntPtr dwData)
			{
				MonitorEnumerationHelper.MonitorInfoEx mi = default(MonitorEnumerationHelper.MonitorInfoEx);
				mi.Size = Marshal.SizeOf<MonitorEnumerationHelper.MonitorInfoEx>(mi);
				if (MonitorEnumerationHelper.GetMonitorInfo(hMonitor, ref mi))
				{
					MonitorInfo info = new MonitorInfo
					{
						ScreenSize = new Vector2((float)(mi.Monitor.right - mi.Monitor.left), (float)(mi.Monitor.bottom - mi.Monitor.top)),
						MonitorArea = new Rect((double)mi.Monitor.left, (double)mi.Monitor.top, (double)(mi.Monitor.right - mi.Monitor.left), (double)(mi.Monitor.bottom - mi.Monitor.top)),
						WorkArea = new Rect((double)mi.WorkArea.left, (double)mi.WorkArea.top, (double)(mi.WorkArea.right - mi.WorkArea.left), (double)(mi.WorkArea.bottom - mi.WorkArea.top)),
						IsPrimary = (mi.Flags > 0U),
						Hmon = hMonitor,
						DeviceName = mi.DeviceName
					};
					result.Add(info);
				}
				return true;
			}, IntPtr.Zero);
			return result;
		}

		// Token: 0x0400008B RID: 139
		private const int CCHDEVICENAME = 32;

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000409 RID: 1033
		private delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref MonitorEnumerationHelper.RECT lprcMonitor, IntPtr dwData);

		// Token: 0x02000085 RID: 133
		public struct RECT
		{
			// Token: 0x040003DA RID: 986
			public int left;

			// Token: 0x040003DB RID: 987
			public int top;

			// Token: 0x040003DC RID: 988
			public int right;

			// Token: 0x040003DD RID: 989
			public int bottom;
		}

		// Token: 0x02000086 RID: 134
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct MonitorInfoEx
		{
			// Token: 0x040003DE RID: 990
			public int Size;

			// Token: 0x040003DF RID: 991
			public MonitorEnumerationHelper.RECT Monitor;

			// Token: 0x040003E0 RID: 992
			public MonitorEnumerationHelper.RECT WorkArea;

			// Token: 0x040003E1 RID: 993
			public uint Flags;

			// Token: 0x040003E2 RID: 994
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DeviceName;
		}
	}
}
