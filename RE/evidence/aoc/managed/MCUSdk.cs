using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AstraDepth3D.BOEStereoRender
{
	// Token: 0x02000062 RID: 98
	internal class MCUSdk
	{
		// Token: 0x0600043D RID: 1085
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_initSDK([MarshalAs(UnmanagedType.LPStr)] string path = "./", int loglevel = 2);

		// Token: 0x0600043E RID: 1086
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void BOE_MCU_releaseSDK();

		// Token: 0x0600043F RID: 1087
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void BOE_MCU_setCallback(StatusCallback callback);

		// Token: 0x06000440 RID: 1088
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_openMCU();

		// Token: 0x06000441 RID: 1089
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void BOE_MCU_closeMCU();

		// Token: 0x06000442 RID: 1090
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_set3DMode([MarshalAs(UnmanagedType.Bool)] bool is3D);

		// Token: 0x06000443 RID: 1091
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BOE_MCU_get3DMode();

		// Token: 0x06000444 RID: 1092
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getUSBState();

		// Token: 0x06000445 RID: 1093
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getDeviceParam([Out] double[] @params);

		// Token: 0x06000446 RID: 1094
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getRefractionDeviceParam([Out] double[] @params);

		// Token: 0x06000447 RID: 1095
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getMonocularCameraParam([Out] double[] @params);

		// Token: 0x06000448 RID: 1096
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getLeftCameraMatParam([Out] double[] @params);

		// Token: 0x06000449 RID: 1097
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getRightCameraMatParam([Out] double[] @params);

		// Token: 0x0600044A RID: 1098
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getStereoCameraDistParam([Out] double[] @params);

		// Token: 0x0600044B RID: 1099
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getCameraType();

		// Token: 0x0600044C RID: 1100
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getWholeMat([Out] double[] @params);

		// Token: 0x0600044D RID: 1101
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getStringParamByType(int type, StringBuilder outMsgBuf, int bufSize);

		// Token: 0x0600044E RID: 1102
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getSNCode(StringBuilder outBuf, int bufSize);

		// Token: 0x0600044F RID: 1103
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getMCUVersion(StringBuilder outBuf, int bufSize);

		// Token: 0x04000330 RID: 816
		private const string DllName = "MCU_library.dll";
	}
}
