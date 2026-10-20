using System;
using System.Runtime.InteropServices;

namespace AstraDepth3D.BOEStereoRender
{
	// Token: 0x02000056 RID: 86
	// (Invoke) Token: 0x06000388 RID: 904
	public delegate int StatusCallback(int type, int status, IntPtr points, [MarshalAs(UnmanagedType.LPStr)] string msg);
}
