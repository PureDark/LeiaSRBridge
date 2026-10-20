using System;
using System.Runtime.InteropServices;
using System.Text;
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D.WinowHelper
{
	// Token: 0x0200001F RID: 31
	internal class APIWrapper
	{
		// Token: 0x0600016C RID: 364
		[DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
		public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

		// Token: 0x0600016D RID: 365
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterWindowMessage(string msg);

		// Token: 0x0600016E RID: 366
		[DllImport("user32.dll")]
		public static extern IntPtr GetShellWindow();

		// Token: 0x0600016F RID: 367
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x06000170 RID: 368
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x06000171 RID: 369
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000172 RID: 370
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x06000173 RID: 371
		[DllImport("User32.dll")]
		private static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06000174 RID: 372
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

		// Token: 0x06000175 RID: 373 RVA: 0x0000B7CC File Offset: 0x000099CC
		public static IntPtr GetActiveProcessId(IntPtr hwnd)
		{
			IntPtr processId;
			User32.GetWindowThreadProcessId(hwnd, out processId);
			return processId;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public static APPBARDATA GetTaskbarRect()
		{
			IntPtr hTaskbar = User32.FindWindow("Shell_TrayWnd", null);
			if (hTaskbar == IntPtr.Zero)
			{
				throw new InvalidOperationException("Taskbar window not found");
			}
			APPBARDATA appBarData = new APPBARDATA
			{
				cbSize = Marshal.SizeOf(typeof(APPBARDATA)),
				hWnd = hTaskbar
			};
			if (APIWrapper.SHAppBarMessage(5, ref appBarData) == 0U)
			{
				throw new InvalidOperationException("Failed to retrieve taskbar position");
			}
			return appBarData;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000B854 File Offset: 0x00009A54
		public static bool IsTaskbarVisible()
		{
			IntPtr taskbarHandle = APIWrapper.FindWindow("Shell_TrayWnd", null);
			return taskbarHandle != IntPtr.Zero && User32.IsWindowVisible(taskbarHandle);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B882 File Offset: 0x00009A82
		public static bool IsOwner(IntPtr hWndA, IntPtr hWndB)
		{
			return User32.GetWindow(hWndB, 4U) == hWndA;
		}

		// Token: 0x0400009D RID: 157
		private const int ABM_GETTASKBARPOS = 5;
	}
}
