using System;
using System.Runtime.InteropServices;

namespace BOEReasterTest
{
	// Token: 0x02000004 RID: 4
	internal class LoadBoeMCUDll
	{
		// Token: 0x06000004 RID: 4
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Init();

		// Token: 0x06000005 RID: 5
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int OpenRaster();

		// Token: 0x06000006 RID: 6
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int UpdateCalidata();

		// Token: 0x06000007 RID: 7
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int UpdateEyeCoordinate(float x, float y);

		// Token: 0x06000008 RID: 8
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern IntPtr GetDeviceId();

		// Token: 0x06000009 RID: 9
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern void FreeBstr(IntPtr bstr);

		// Token: 0x0600000A RID: 10
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int CloseRaster();

		// Token: 0x0600000B RID: 11
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int ResetRasterState();

		// Token: 0x0600000C RID: 12
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int ReadCalidataInMCU(out float cali0, out float cali1, out float cali2, out float cali3);

		// Token: 0x0600000D RID: 13
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int WriteCalidataToMCU(float cali0, float cali1, float cali2, float cali3);

		// Token: 0x0600000E RID: 14
		[DllImport("BoeMCU.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern bool UInit();

		// Token: 0x0600000F RID: 15 RVA: 0x00002078 File Offset: 0x00000278
		public static string getDevUUID()
		{
			IntPtr deviceId = LoadBoeMCUDll.GetDeviceId();
			string str = Marshal.PtrToStringBSTR(deviceId);
			LoadBoeMCUDll.FreeBstr(deviceId);
			return str;
		}
	}
}
