using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AstraDepth3D.BOEStereoRender
{
	// Token: 0x02000057 RID: 87
	internal class MCUSdk
	{
		// Token: 0x0600038B RID: 907
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_initSDK([MarshalAs(UnmanagedType.LPStr)] string path = "./", int loglevel = 2);

		// Token: 0x0600038C RID: 908
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void BOE_MCU_releaseSDK();

		// Token: 0x0600038D RID: 909
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void BOE_MCU_setCallback(StatusCallback callback);

		// Token: 0x0600038E RID: 910
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_openMCU();

		// Token: 0x0600038F RID: 911
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void BOE_MCU_closeMCU();

		// Token: 0x06000390 RID: 912
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_set3DMode([MarshalAs(UnmanagedType.Bool)] bool is3D);

		// Token: 0x06000391 RID: 913
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BOE_MCU_get3DMode();

		// Token: 0x06000392 RID: 914
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getUSBState();

		// Token: 0x06000393 RID: 915
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getDeviceParam([Out] double[] @params);

		// Token: 0x06000394 RID: 916
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getRefractionDeviceParam([Out] double[] @params);

		// Token: 0x06000395 RID: 917
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getMonocularCameraParam([Out] double[] @params);

		// Token: 0x06000396 RID: 918
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getLeftCameraMatParam([Out] double[] @params);

		// Token: 0x06000397 RID: 919
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getRightCameraMatParam([Out] double[] @params);

		// Token: 0x06000398 RID: 920
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getStereoCameraDistParam([Out] double[] @params);

		// Token: 0x06000399 RID: 921
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getCameraType();

		// Token: 0x0600039A RID: 922
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getWholeMat([Out] double[] @params);

		// Token: 0x0600039B RID: 923
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getStringParamByType(int type, StringBuilder outMsgBuf, int bufSize);

		// Token: 0x0600039C RID: 924
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getSNCode(StringBuilder outBuf, int bufSize);

		// Token: 0x0600039D RID: 925
		[DllImport("MCU_library.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BOE_MCU_getMCUVersion(StringBuilder outBuf, int bufSize);

		// Token: 0x04000319 RID: 793
		private const string DllName = "MCU_library.dll";
	}
}
