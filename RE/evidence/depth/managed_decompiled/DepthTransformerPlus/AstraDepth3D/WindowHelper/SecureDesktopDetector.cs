using System;
using System.Runtime.InteropServices;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000040 RID: 64
	public class SecureDesktopDetector
	{
		// Token: 0x06000265 RID: 613
		[DllImport("user32.dll")]
		private static extern IntPtr OpenInputDesktop(uint flags, bool inherit, uint desiredAccess);

		// Token: 0x06000266 RID: 614
		[DllImport("user32.dll")]
		private static extern bool CloseDesktop(IntPtr handle);

		// Token: 0x06000267 RID: 615
		[DllImport("user32.dll")]
		private static extern IntPtr GetThreadDesktop(uint threadId);

		// Token: 0x06000268 RID: 616
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		// Token: 0x06000269 RID: 617 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public static bool IsSecureDesktopActive()
		{
			IntPtr hSecureDesktop = SecureDesktopDetector.OpenInputDesktop(0U, false, 1U);
			if (hSecureDesktop != IntPtr.Zero)
			{
				IntPtr hCurrentDesktop = SecureDesktopDetector.GetThreadDesktop(SecureDesktopDetector.GetCurrentThreadId());
				bool isSecure = hSecureDesktop != hCurrentDesktop;
				SecureDesktopDetector.CloseDesktop(hSecureDesktop);
				return isSecure;
			}
			return false;
		}
	}
}
