using System;

namespace AstraDepth3D.WinowHelper
{
	// Token: 0x02000021 RID: 33
	public struct APPBARDATA
	{
		// Token: 0x040000A2 RID: 162
		public int cbSize;

		// Token: 0x040000A3 RID: 163
		public IntPtr hWnd;

		// Token: 0x040000A4 RID: 164
		public int uCallbackMessage;

		// Token: 0x040000A5 RID: 165
		public int uEdge;

		// Token: 0x040000A6 RID: 166
		public RECT rc;

		// Token: 0x040000A7 RID: 167
		public IntPtr lParam;
	}
}
