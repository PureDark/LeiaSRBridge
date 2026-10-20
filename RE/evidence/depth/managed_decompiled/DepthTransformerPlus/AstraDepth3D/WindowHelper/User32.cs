using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AstraDepth3D.WinowHelper;
using Microsoft.Win32;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000042 RID: 66
	internal class User32
	{
		// Token: 0x06000271 RID: 625
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point pt);

		// Token: 0x06000272 RID: 626
		[DllImport("user32.dll")]
		public static extern IntPtr ChildWindowFromPoint(IntPtr hwnd, Point pt);

		// Token: 0x06000273 RID: 627
		[DllImport("user32.dll")]
		public static extern int ShowWindow(int hwnd, int nCmdShow);

		// Token: 0x06000274 RID: 628
		[DllImport("User32.dll")]
		public static extern void SetWindowLong(IntPtr handle, int oldStyle, long newStyle);

		// Token: 0x06000275 RID: 629
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		// Token: 0x06000276 RID: 630
		[DllImport("user32.dll")]
		public static extern int BringWindowToTop(IntPtr hWnd);

		// Token: 0x06000277 RID: 631
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hwnd);

		// Token: 0x06000278 RID: 632
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x06000279 RID: 633
		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

		// Token: 0x0600027A RID: 634
		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		public static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

		// Token: 0x0600027B RID: 635
		[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
		public static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600027C RID: 636
		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLong64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600027D RID: 637 RVA: 0x00010006 File Offset: 0x0000E206
		public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
		{
			if (IntPtr.Size == 8)
			{
				return User32.SetWindowLong64(hWnd, nIndex, dwNewLong);
			}
			return User32.SetWindowLong32(hWnd, nIndex, dwNewLong);
		}

		// Token: 0x0600027E RID: 638
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		// Token: 0x0600027F RID: 639 RVA: 0x00010021 File Offset: 0x0000E221
		public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			if (IntPtr.Size == 8)
			{
				return User32.GetWindowLong64(hWnd, nIndex);
			}
			return User32.GetWindowLong32(hWnd, nIndex);
		}

		// Token: 0x06000280 RID: 640
		[DllImport("user32")]
		public static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

		// Token: 0x06000281 RID: 641
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool EnumWindows(User32.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x06000282 RID: 642
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

		// Token: 0x06000283 RID: 643
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		// Token: 0x06000284 RID: 644
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06000285 RID: 645
		[DllImport("user32.dll")]
		public static extern bool IsIconic(IntPtr hWnd);

		// Token: 0x06000286 RID: 646
		[DllImport("user32.dll")]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref User32.WINDOWPLACEMENT lpwndpl);

		// Token: 0x06000287 RID: 647
		[DllImport("user32.dll")]
		public static extern bool EnumThreadWindows(uint dwThreadId, User32.EnumThreadDelegate lpfn, IntPtr lParam);

		// Token: 0x06000288 RID: 648
		[DllImport("user32.dll")]
		public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

		// Token: 0x06000289 RID: 649
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x0600028A RID: 650 RVA: 0x0001003A File Offset: 0x0000E23A
		public static IntPtr GetProcessHandle(int processId)
		{
			IntPtr processPtr = IntPtr.Zero;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				IntPtr pid;
				User32.GetWindowThreadProcessId(hWnd, out pid);
				if (pid == lParam && User32.IsWindowVisible(hWnd) && User32.GetParentHandle(hWnd) == hWnd)
				{
					processPtr = hWnd;
					return true;
				}
				return true;
			}, new IntPtr(processId));
			return processPtr;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00010069 File Offset: 0x0000E269
		public static IntPtr GetProcessHandle(int processId, string sHwnd)
		{
			IntPtr processPtr = IntPtr.Zero;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				IntPtr pid;
				User32.GetWindowThreadProcessId(hWnd, out pid);
				if (pid == lParam && User32.IsWindowVisible(hWnd) && User32.GetParentHandle(hWnd) == hWnd && sHwnd == hWnd.ToString())
				{
					processPtr = hWnd;
					return true;
				}
				return true;
			}, new IntPtr(processId));
			return processPtr;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001009F File Offset: 0x0000E29F
		public static List<IntPtr> GetProcessHandles(int processId)
		{
			List<IntPtr> processPtrs = new List<IntPtr>();
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				IntPtr pid;
				User32.GetWindowThreadProcessId(hWnd, out pid);
				if (pid == lParam && User32.IsWindowVisible(hWnd) && User32.GetParentHandle(hWnd) == hWnd)
				{
					processPtrs.Add(hWnd);
					return true;
				}
				return true;
			}, new IntPtr(processId));
			return processPtrs;
		}

		// Token: 0x0600028D RID: 653
		[DllImport("user32.dll")]
		public static extern IntPtr GetParent(IntPtr hWnd);

		// Token: 0x0600028E RID: 654 RVA: 0x000100D0 File Offset: 0x0000E2D0
		private static IntPtr GetParentHandle(IntPtr currentAppHandle)
		{
			IntPtr next = User32.GetParent(currentAppHandle);
			IntPtr cur = currentAppHandle;
			while (next != IntPtr.Zero)
			{
				cur = next;
				next = User32.GetParent(cur);
			}
			return cur;
		}

		// Token: 0x0600028F RID: 655
		[DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

		// Token: 0x06000290 RID: 656 RVA: 0x00010100 File Offset: 0x0000E300
		public static IntPtr GetProcessHandle(Process process, string windowTitle)
		{
			IntPtr handle = IntPtr.Zero;
			bool isBreak = false;
			User32.EnumThreadDelegate <>9__0;
			foreach (object obj in process.Threads)
			{
				ProcessThread thread = (ProcessThread)obj;
				if (isBreak)
				{
					break;
				}
				uint id = (uint)thread.Id;
				User32.EnumThreadDelegate lpfn;
				if ((lpfn = <>9__0) == null)
				{
					lpfn = (<>9__0 = delegate(IntPtr hWnd, IntPtr lParam)
					{
						StringBuilder sb = new StringBuilder(256);
						User32.GetWindowTextW(hWnd, sb, sb.Capacity);
						if (sb.ToString() == windowTitle)
						{
							handle = hWnd;
							isBreak = true;
							return false;
						}
						return true;
					});
				}
				User32.EnumThreadWindows(id, lpfn, IntPtr.Zero);
			}
			return handle;
		}

		// Token: 0x06000291 RID: 657
		[DllImport("user32.dll")]
		public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);

		// Token: 0x06000292 RID: 658
		[DllImport("user32.dll")]
		public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, User32.WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

		// Token: 0x06000293 RID: 659
		[DllImport("user32.dll")]
		public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

		// Token: 0x06000294 RID: 660
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindow(IntPtr hWnd);

		// Token: 0x06000295 RID: 661
		[DllImport("User32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000296 RID: 662
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		// Token: 0x06000297 RID: 663
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref User32.RECT lpRect);

		// Token: 0x06000298 RID: 664
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

		// Token: 0x06000299 RID: 665
		[DllImport("psapi.dll")]
		private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [MarshalAs(UnmanagedType.U4)] [In] int nSize);

		// Token: 0x0600029A RID: 666
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x0600029B RID: 667 RVA: 0x000101B4 File Offset: 0x0000E3B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GetProcessInfo(int pid, out string processName, out string processPath)
		{
			processName = "";
			processPath = "";
			IntPtr processHandle = User32.OpenProcess(1040U, false, pid);
			if (processHandle == IntPtr.Zero)
			{
				return;
			}
			StringBuilder sb = new StringBuilder(4000);
			if (User32.GetModuleFileNameEx(processHandle, IntPtr.Zero, sb, 4000) > 0U)
			{
				processName = Path.GetFileNameWithoutExtension(sb.ToString());
				processPath = Path.GetFullPath(sb.ToString());
			}
			User32.CloseHandle(processHandle);
		}

		// Token: 0x0600029C RID: 668
		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		// Token: 0x0600029D RID: 669
		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		// Token: 0x0600029E RID: 670 RVA: 0x0001022C File Offset: 0x0000E42C
		public static string GetWindowsVersionByRegistry()
		{
			RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
			string productName = (string)registry.GetValue("ProductName");
			if (productName != null)
			{
				if (productName.StartsWith("Windows 10"))
				{
					string currentBuild = (string)registry.GetValue("CurrentBuild");
					if (currentBuild != null && int.Parse(currentBuild) >= 22000)
					{
						productName = "Windows11";
					}
					else
					{
						productName = "Windows10";
					}
				}
				else if (productName.StartsWith("Windows 8"))
				{
					productName = "Windows8";
				}
				else if (productName.StartsWith("Windows 7"))
				{
					productName = "Windows7";
				}
			}
			return productName;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000102C4 File Offset: 0x0000E4C4
		public static string GetWindowsVersionByRegistry2()
		{
			RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
			string productName = (string)registry.GetValue("ProductName");
			string currentBuild = "";
			if (productName != null && productName.StartsWith("Windows 10"))
			{
				currentBuild = (string)registry.GetValue("CurrentBuild");
			}
			return currentBuild;
		}

		// Token: 0x060002A0 RID: 672
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetAncestor(IntPtr hwnd, int flags);

		// Token: 0x060002A1 RID: 673
		[DllImport("user32")]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x060002A2 RID: 674 RVA: 0x0001031C File Offset: 0x0000E51C
		public static bool IsFullScreen(IntPtr hwnd, int cx, int cy)
		{
			if (hwnd == IntPtr.Zero)
			{
				return false;
			}
			User32.RECT rect = default(User32.RECT);
			if (User32.GetWindowRect(hwnd, ref rect) && rect.Left <= 1 && rect.Top <= 1 && rect.Right >= cx - 1 && rect.Bottom >= cy - 1)
			{
				NLogService.WriteLog(string.Format("MainWindow|IsFullScreen:窗口尺寸：{0},{1},{2},{3},{4}", new object[]
				{
					hwnd,
					rect.Left,
					rect.Top,
					rect.Right,
					rect.Bottom
				}));
				return true;
			}
			return false;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000103D0 File Offset: 0x0000E5D0
		public static bool IsFullScreen(IntPtr hwnd, Rectangle screenRect)
		{
			if (hwnd == IntPtr.Zero)
			{
				return false;
			}
			User32.RECT rect = default(User32.RECT);
			bool hr = User32.GetWindowRect(hwnd, ref rect);
			NLogService.WriteLog(string.Format("MainWindow|IsFullScreen:窗口尺寸：{0},{1},{2},{3},{4}", new object[]
			{
				hwnd,
				rect.Left,
				rect.Top,
				rect.Right,
				rect.Bottom
			}));
			return hr && rect.Left <= screenRect.Left + 1 && rect.Top <= screenRect.Top + 1 && rect.Right >= screenRect.Right - 1 && rect.Bottom >= screenRect.Bottom - 1;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001049D File Offset: 0x0000E69D
		public static bool checkFullScreen(User32.RECT rect, int cx, int cy)
		{
			return rect.Left <= 1 && rect.Top <= 1 && rect.Right >= cx - 1 && rect.Bottom >= cy - 1;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000104CC File Offset: 0x0000E6CC
		public static bool checkFullScreen(User32.RECT rect, Rectangle screenRect)
		{
			return rect.Left <= screenRect.Left + 1 && rect.Top <= screenRect.Top + 1 && rect.Right >= screenRect.Right - 1 && rect.Bottom >= screenRect.Bottom - 1;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00010520 File Offset: 0x0000E720
		public static bool checkFullScreen(User32.RECT rect, User32.RECT screenRect)
		{
			return rect.Left <= screenRect.Left + 1 && rect.Top <= screenRect.Top + 1 && rect.Right >= screenRect.Right - 1 && rect.Bottom >= screenRect.Bottom - 1;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00010570 File Offset: 0x0000E770
		public static bool IsCovered(IntPtr fullscreenHandle, IntPtr associatedHandle, IntPtr onTaskbar, IntPtr hwnd, int SW, int SH, bool exclusive)
		{
			User32.RECT hwnd_rect = default(User32.RECT);
			if (!User32.GetWindowRect(hwnd, ref hwnd_rect) || hwnd_rect.Left > SW || hwnd_rect.Right < 0 || hwnd_rect.Top > SH || hwnd_rect.Bottom < 0 || (hwnd_rect.Left <= 1 && hwnd_rect.Right <= 1) || (hwnd_rect.Top <= 1 && hwnd_rect.Bottom <= 1) || hwnd_rect.Right - hwnd_rect.Left <= 80 || hwnd_rect.Bottom - hwnd_rect.Top <= 80)
			{
				return false;
			}
			if (WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, hwnd))
			{
				return false;
			}
			User32.RECT _hwnd_rect = User32.IntersectsWith(hwnd_rect, SW, SH);
			IntPtr topHwndFormPoint = User32.WindowFromPoint(new Point(_hwnd_rect.Left + (_hwnd_rect.Right - _hwnd_rect.Left) / 2, _hwnd_rect.Top + (_hwnd_rect.Bottom - _hwnd_rect.Top) / 2));
			bool isPrd_top = false;
			bool isCurrentApp = false;
			string title = "";
			if (exclusive)
			{
				StringBuilder lptrString = new StringBuilder(256);
				User32.GetWindowTextW(topHwndFormPoint, lptrString, lptrString.Capacity);
				title = lptrString.ToString();
				isPrd_top = title.StartsWith("TX_PRD_");
				isCurrentApp = title.StartsWith("yl_3d_");
			}
			if ((topHwndFormPoint == fullscreenHandle || topHwndFormPoint == associatedHandle || topHwndFormPoint == onTaskbar || isPrd_top || isCurrentApp || !WindowEnumerationHelper.IsWindowValid(topHwndFormPoint) || WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, topHwndFormPoint)) && topHwndFormPoint != IntPtr.Zero)
			{
				return false;
			}
			if (topHwndFormPoint == IntPtr.Zero)
			{
				return false;
			}
			NLogService.WriteLog(string.Format("检测到窗口被覆盖：{0},{1},{2}，{3},{4},{5},{6}", new object[]
			{
				hwnd,
				topHwndFormPoint,
				title,
				hwnd_rect.Left,
				hwnd_rect.Top,
				hwnd_rect.Right,
				hwnd_rect.Bottom
			}));
			return true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00010754 File Offset: 0x0000E954
		public static bool IsCovered(IntPtr fullscreenHandle, IntPtr associatedHandle, IntPtr onTaskbar, IntPtr hwnd, Rectangle rectangle, bool exclusive)
		{
			User32.RECT hwnd_rect = default(User32.RECT);
			if (!User32.GetWindowRect(hwnd, ref hwnd_rect) || hwnd_rect.Left > rectangle.Right || hwnd_rect.Right < rectangle.Left || hwnd_rect.Top > rectangle.Bottom || hwnd_rect.Bottom < rectangle.Top || (hwnd_rect.Left <= rectangle.Left + 1 && hwnd_rect.Right <= rectangle.Left + 1) || (hwnd_rect.Top <= rectangle.Top + 1 && hwnd_rect.Bottom <= rectangle.Top + 1) || hwnd_rect.Right - hwnd_rect.Left <= 80 || hwnd_rect.Bottom - hwnd_rect.Top <= 80)
			{
				return false;
			}
			if (WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, hwnd))
			{
				return false;
			}
			User32.RECT _hwnd_rect = User32.IntersectsWith(hwnd_rect, rectangle);
			IntPtr topHwndFormPoint = User32.WindowFromPoint(new Point(_hwnd_rect.Left + (_hwnd_rect.Right - _hwnd_rect.Left) / 2, _hwnd_rect.Top + (_hwnd_rect.Bottom - _hwnd_rect.Top) / 2));
			bool isPrd_top = false;
			bool isCurrentApp = false;
			string title = "";
			if (exclusive)
			{
				StringBuilder lptrString = new StringBuilder(256);
				User32.GetWindowTextW(topHwndFormPoint, lptrString, lptrString.Capacity);
				title = lptrString.ToString();
				isPrd_top = title.StartsWith("TX_PRD_");
				isCurrentApp = title.StartsWith("yl_3d_");
			}
			if ((topHwndFormPoint == fullscreenHandle || topHwndFormPoint == associatedHandle || topHwndFormPoint == onTaskbar || isPrd_top || isCurrentApp || !WindowEnumerationHelper.IsWindowValid(topHwndFormPoint) || WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, topHwndFormPoint)) && topHwndFormPoint != IntPtr.Zero)
			{
				return false;
			}
			if (topHwndFormPoint == IntPtr.Zero)
			{
				return false;
			}
			NLogService.WriteLog(string.Format("检测到窗口被覆盖：{0},{1},{2}，{3},{4},{5},{6}", new object[]
			{
				hwnd,
				topHwndFormPoint,
				title,
				hwnd_rect.Left,
				hwnd_rect.Top,
				hwnd_rect.Right,
				hwnd_rect.Bottom
			}));
			return true;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00010978 File Offset: 0x0000EB78
		public static bool IsCovered2(IntPtr fullscreenHandle, IntPtr associatedHandle, IntPtr onTaskbar, IntPtr hwnd, int SW, int SH, bool exclusive)
		{
			if (!User32.IsFullScreen(hwnd, SW, SH))
			{
				return false;
			}
			if (WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, hwnd))
			{
				return false;
			}
			User32.RECT _hwnd_rect = default(User32.RECT);
			_hwnd_rect.Left = 0;
			_hwnd_rect.Top = 0;
			_hwnd_rect.Right = SW / 2;
			_hwnd_rect.Bottom = SH / 2;
			IntPtr topHwndFormPoint = User32.WindowFromPoint(new Point(_hwnd_rect.Left + (_hwnd_rect.Right - _hwnd_rect.Left) / 2, _hwnd_rect.Top + (_hwnd_rect.Bottom - _hwnd_rect.Top) / 2));
			bool isPrd_top = false;
			bool isCurrentApp = false;
			string title = "";
			if (exclusive)
			{
				StringBuilder lptrString = new StringBuilder(256);
				User32.GetWindowTextW(topHwndFormPoint, lptrString, lptrString.Capacity);
				title = lptrString.ToString();
				isPrd_top = title.StartsWith("TX_PRD_");
				isCurrentApp = title.StartsWith("yl_3d_");
			}
			if ((topHwndFormPoint == fullscreenHandle || topHwndFormPoint == associatedHandle || topHwndFormPoint == onTaskbar || isPrd_top || isCurrentApp || !WindowEnumerationHelper.IsWindowValid(topHwndFormPoint) || WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, topHwndFormPoint)) && topHwndFormPoint != IntPtr.Zero)
			{
				return false;
			}
			if (!User32.IsFullScreen(topHwndFormPoint, SW, SH))
			{
				return false;
			}
			NLogService.WriteLog(string.Format("检测到窗口被覆盖：{0},{1},{2}", hwnd, topHwndFormPoint, title));
			return true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		public static bool IsObscuredByFullscreenWindow(IntPtr fullscreenHandle, IntPtr associatedHandle, IntPtr onTaskbar, IntPtr hwnd, Rectangle rectangle, bool exclusive)
		{
			if (!User32.IsFullScreen(hwnd, rectangle))
			{
				return false;
			}
			if (WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, hwnd))
			{
				return false;
			}
			User32.RECT _hwnd_rect = default(User32.RECT);
			_hwnd_rect.Left = rectangle.Left;
			_hwnd_rect.Top = rectangle.Top;
			_hwnd_rect.Right = rectangle.Width / 2 + rectangle.Left;
			_hwnd_rect.Bottom = rectangle.Height / 2 + rectangle.Top;
			IntPtr topHwndFormPoint = User32.WindowFromPoint(new Point(_hwnd_rect.Left + (_hwnd_rect.Right - _hwnd_rect.Left) / 2, _hwnd_rect.Top + (_hwnd_rect.Bottom - _hwnd_rect.Top) / 2));
			bool isPrd_top = false;
			bool isCurrentApp = false;
			string title = "";
			if (exclusive)
			{
				StringBuilder lptrString = new StringBuilder(256);
				User32.GetWindowTextW(topHwndFormPoint, lptrString, lptrString.Capacity);
				title = lptrString.ToString();
				isPrd_top = title.StartsWith("TX_PRD_");
				isCurrentApp = title.StartsWith("yl_3d_");
			}
			if ((topHwndFormPoint == fullscreenHandle || topHwndFormPoint == associatedHandle || topHwndFormPoint == onTaskbar || isPrd_top || isCurrentApp || !WindowEnumerationHelper.IsWindowValid(topHwndFormPoint) || WindowEnumerationHelper.IsChildOrOwner(fullscreenHandle, topHwndFormPoint)) && topHwndFormPoint != IntPtr.Zero)
			{
				return false;
			}
			if (!User32.IsFullScreen(topHwndFormPoint, rectangle))
			{
				return false;
			}
			NLogService.WriteLog(string.Format("检测到窗口被覆盖：{0},{1},{2}", hwnd, topHwndFormPoint, title));
			return true;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00010C30 File Offset: 0x0000EE30
		public static bool IsSizeChanged(IntPtr fullscreenHandle, User32.RECT fullScreenHwndRect)
		{
			User32.RECT rect = default(User32.RECT);
			if (User32.GetWindowRect(fullscreenHandle, ref rect) && rect.Top <= 1 && rect.Left <= 1 && rect.Bottom >= fullScreenHwndRect.Bottom - 1)
			{
				int right = rect.Right;
				int num = fullScreenHwndRect.Right - 1;
			}
			return false;
		}

		// Token: 0x060002AC RID: 684
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetLayeredWindowAttributes(IntPtr hWnd, out uint crKey, out byte bAlpha, out uint dwFlags);

		// Token: 0x060002AD RID: 685 RVA: 0x00010C84 File Offset: 0x0000EE84
		public static bool IsWindowTransparent(IntPtr hWnd)
		{
			int exStyle = User32.GetWindowLong(hWnd, -20);
			if (((long)exStyle & 524288L) == 0L)
			{
				return false;
			}
			if ((exStyle & 32) != 0)
			{
				return true;
			}
			uint crKey;
			byte alpha;
			uint dwFlags;
			if (!User32.GetLayeredWindowAttributes(hWnd, out crKey, out alpha, out dwFlags))
			{
				return false;
			}
			bool flag = (dwFlags & 2U) != 0U && alpha < byte.MaxValue;
			bool isColorKey = (dwFlags & 1U) > 0U;
			return flag || isColorKey;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00010CDC File Offset: 0x0000EEDC
		public static bool IsWindowFromCurrentProcess(IntPtr windowHandle)
		{
			IntPtr processId;
			User32.GetWindowThreadProcessId(windowHandle, out processId);
			return (int)processId == Process.GetCurrentProcess().Id;
		}

		// Token: 0x060002AF RID: 687
		[DllImport("user32.dll")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x060002B0 RID: 688 RVA: 0x00010D04 File Offset: 0x0000EF04
		public static List<IntPtr> GetForegroundWindows(IntPtr targetHwnd)
		{
			StringBuilder _lptrString2 = new StringBuilder(256);
			User32.GetWindowTextW(targetHwnd, _lptrString2, _lptrString2.Capacity);
			string _title = _lptrString2.ToString();
			User32.GetClassName(targetHwnd, _lptrString2, _lptrString2.Capacity);
			string _className = _lptrString2.ToString();
			NLogService.WriteLog(string.Format("目标窗口：{0}，{1},{2}", targetHwnd, _title, _className));
			List<IntPtr> windows = new List<IntPtr>();
			bool isprd = _title.StartsWith("TX_PRD_");
			bool isCurrentProcessWindow = _title.StartsWith("yl_3d_");
			IntPtr fullScreenHwnd = IntPtr.Zero;
			int cy = Screen.PrimaryScreen.Bounds.Height;
			int cx = Screen.PrimaryScreen.Bounds.Width;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (WindowEnumerationHelper.IsWindowValidForCapture(hWnd))
				{
					if (hWnd == targetHwnd && !isprd && !isCurrentProcessWindow)
					{
						return false;
					}
					if ((isprd | isCurrentProcessWindow) && fullScreenHwnd != IntPtr.Zero)
					{
						User32.GetWindowTextW(fullScreenHwnd, _lptrString2, _lptrString2.Capacity);
						string _title2 = _lptrString2.ToString();
						User32.GetClassName(fullScreenHwnd, _lptrString2, _lptrString2.Capacity);
						string _className2 = _lptrString2.ToString();
						NLogService.WriteLog(string.Format("全屏目标窗口：{0}，{1},{2}", fullScreenHwnd, _title2, _className2));
						if (!_title2.StartsWith("TX_PRD_") && !_className2.StartsWith("yl_3d_"))
						{
							return false;
						}
					}
					if (!User32.IsWindowFromCurrentProcess(hWnd) && !isprd)
					{
						User32.RECT hwnd_rect = default(User32.RECT);
						if (!User32.GetWindowRect(hWnd, ref hwnd_rect) || hwnd_rect.Right - hwnd_rect.Left <= 80 || hwnd_rect.Bottom - hwnd_rect.Top <= 80)
						{
							return true;
						}
						int lengthOfTitle = User32.GetWindowTextLength(hWnd) + 2;
						User32.GetWindowTextW(hWnd, _lptrString2, _lptrString2.Capacity);
						string _title3 = _lptrString2.ToString();
						User32.GetClassName(hWnd, _lptrString2, _lptrString2.Capacity);
						string _className3 = _lptrString2.ToString();
						if (_title3.StartsWith("TX_PRD_") || _title3.StartsWith("yl_3d_"))
						{
							return true;
						}
						if (_title3 == "Program Manager")
						{
							return false;
						}
						if (User32.IsPassThrough(hWnd) && string.IsNullOrWhiteSpace(_title3))
						{
							NLogService.WriteLog(string.Format("过滤透明分层未激活窗口：{0}，{1},{2}", hWnd, _title3, _className3));
							return true;
						}
						if (lengthOfTitle <= 0 && _className3 != "ApplicationFrameWindow" && hWnd != targetHwnd)
						{
							return true;
						}
						if (_className3 == "ApplicationFrameWindow" && User32.FindCoreWindow(hWnd, null, "Windows.UI.Core.CoreWindow") == IntPtr.Zero)
						{
							return true;
						}
						windows.Add(hWnd);
						if (User32.checkFullScreen(hwnd_rect, cx, cy))
						{
							fullScreenHwnd = hWnd;
						}
						NLogService.WriteLog(string.Format("枚举顶层窗口：{0}，{1},{2},{3},{4}", new object[]
						{
							hWnd,
							_title3,
							_className3,
							hwnd_rect.Right - hwnd_rect.Left,
							hwnd_rect.Bottom - hwnd_rect.Top
						}));
					}
				}
				return true;
			}, IntPtr.Zero);
			_lptrString2 = null;
			return windows;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00010E28 File Offset: 0x0000F028
		public static List<IntPtr> GetForegroundWindows(IntPtr targetHwnd, Rectangle screenRect)
		{
			StringBuilder _lptrString2 = new StringBuilder(256);
			User32.GetWindowTextW(targetHwnd, _lptrString2, _lptrString2.Capacity);
			string _title = _lptrString2.ToString();
			User32.GetClassName(targetHwnd, _lptrString2, _lptrString2.Capacity);
			string _className = _lptrString2.ToString();
			List<IntPtr> windows = new List<IntPtr>();
			bool isprd = _title.StartsWith("TX_PRD_");
			bool isCurrentProcessWindow = _title.StartsWith("yl_3d_");
			User32.RECT targetHwndRect = default(User32.RECT);
			bool successed = User32.GetWindowRect(targetHwnd, ref targetHwndRect);
			bool isOutScreen = User32.IsOutScreent(targetHwndRect, screenRect) && successed;
			NLogService.WriteLog(string.Format("目标窗口：{0}，{1},{2};窗口位置：{3},{4},{5},{6};屏幕位置：{7},{8},{9},{10}", new object[]
			{
				targetHwnd,
				_title,
				_className,
				targetHwndRect.Left,
				targetHwndRect.Top,
				targetHwndRect.Right,
				targetHwndRect.Bottom,
				screenRect.Left,
				screenRect.Top,
				screenRect.Right,
				screenRect.Bottom
			}));
			NLogService.WriteLog(string.Format("前台目标窗口：{0}，{1},{2};isOutScreen:{3}", new object[]
			{
				targetHwnd,
				_title,
				_className,
				isOutScreen
			}));
			IntPtr fullScreenHwnd = IntPtr.Zero;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (WindowEnumerationHelper.IsWindowValidForCapture(hWnd))
				{
					if (hWnd == targetHwnd && !isprd && !isCurrentProcessWindow && !isOutScreen)
					{
						return false;
					}
					if ((isprd | isCurrentProcessWindow) && fullScreenHwnd != IntPtr.Zero)
					{
						User32.GetWindowTextW(fullScreenHwnd, _lptrString2, _lptrString2.Capacity);
						string _title2 = _lptrString2.ToString();
						User32.GetClassName(fullScreenHwnd, _lptrString2, _lptrString2.Capacity);
						string _className2 = _lptrString2.ToString();
						NLogService.WriteLog(string.Format("全屏目标窗口：{0}，{1},{2}", fullScreenHwnd, _title2, _className2));
						if (!_title2.StartsWith("TX_PRD_") && !_className2.StartsWith("yl_3d_"))
						{
							return false;
						}
					}
					if (!User32.IsWindowFromCurrentProcess(hWnd) && !isprd)
					{
						User32.RECT hwnd_rect = default(User32.RECT);
						bool hr22 = User32.GetWindowRect(hWnd, ref hwnd_rect);
						if (!hr22 || User32.IsSmallWindow(hwnd_rect))
						{
							return true;
						}
						if (!hr22 || User32.IsOutScreent(hwnd_rect, screenRect))
						{
							return true;
						}
						int lengthOfTitle = User32.GetWindowTextLength(hWnd) + 2;
						User32.GetWindowTextW(hWnd, _lptrString2, _lptrString2.Capacity);
						string _title3 = _lptrString2.ToString();
						User32.GetClassName(hWnd, _lptrString2, _lptrString2.Capacity);
						string _className3 = _lptrString2.ToString();
						if (_title3.StartsWith("TX_PRD_") || _title3.StartsWith("yl_3d_"))
						{
							return true;
						}
						if (_title3 == "Program Manager")
						{
							return false;
						}
						if (User32.IsPassThrough(hWnd) && string.IsNullOrWhiteSpace(_title3))
						{
							NLogService.WriteLog(string.Format("过滤透明分层未激活窗口：{0}，{1},{2}", hWnd, _title3, _className3));
							return true;
						}
						if (lengthOfTitle <= 0 && _className3 != "ApplicationFrameWindow" && hWnd != targetHwnd)
						{
							return true;
						}
						if (_className3 == "ApplicationFrameWindow" && User32.FindCoreWindow(hWnd, null, "Windows.UI.Core.CoreWindow") == IntPtr.Zero)
						{
							return true;
						}
						windows.Add(hWnd);
						if (User32.checkFullScreen(hwnd_rect, screenRect))
						{
							fullScreenHwnd = hWnd;
						}
						NLogService.WriteLog(string.Format("枚举顶层窗口：{0}，{1},{2},{3},{4}", new object[]
						{
							hWnd,
							_title3,
							_className3,
							hwnd_rect.Right - hwnd_rect.Left,
							hwnd_rect.Bottom - hwnd_rect.Top
						}));
					}
				}
				return true;
			}, IntPtr.Zero);
			_lptrString2 = null;
			return windows;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00011020 File Offset: 0x0000F220
		public static ValueTuple<IntPtr, IntPtr, IntPtr> GetTopWindow(IntPtr _targetHwnd, int cx, int cy)
		{
			IntPtr _hwnd = IntPtr.Zero;
			IntPtr _associated = IntPtr.Zero;
			IntPtr _associated2 = IntPtr.Zero;
			IntPtr parent = User32.GetAncestor(_targetHwnd, 3);
			IntPtr targetHwnd = IntPtr.Zero;
			if (parent == IntPtr.Zero || parent == APIWrapper.GetDesktopWindow())
			{
				targetHwnd = _targetHwnd;
			}
			else
			{
				targetHwnd = parent;
			}
			IntPtr processId;
			User32.GetWindowThreadProcessId(targetHwnd, out processId);
			List<IntPtr> foregroundWindows = User32.GetForegroundWindows(targetHwnd);
			bool isFullScreen = User32.IsFullScreen(targetHwnd, cx, cy);
			bool _isPrd = User32.IsPrd(targetHwnd);
			if (isFullScreen && !_isPrd)
			{
				_hwnd = targetHwnd;
			}
			NLogService.WriteLog(string.Format("目标窗口信息：{0}，{1},{2}", targetHwnd, isFullScreen, processId));
			bool isCovered = false;
			foreach (IntPtr hwnd in foregroundWindows)
			{
				IntPtr processId2;
				User32.GetWindowThreadProcessId(hwnd, out processId2);
				if (User32.IsFullScreen(hwnd, cx, cy))
				{
					NLogService.WriteLog(string.Format("find fullscreen hwnd：{0}", hwnd));
					if (hwnd == _hwnd && _hwnd != IntPtr.Zero)
					{
						break;
					}
					if ((processId == processId2 || WindowEnumerationHelper.IsChildOrOwner(_hwnd, hwnd)) && isFullScreen)
					{
						NLogService.WriteLog(string.Format("{0}与{1}属于同一进程", hwnd, _hwnd));
						_associated = hwnd;
						break;
					}
					NLogService.WriteLog(string.Format("{0}为顶层全屏窗口", hwnd));
					_hwnd = hwnd;
					break;
				}
				else if (User32.IsWindowVisible(hwnd) && !User32.IsIconic(hwnd) && !User32.IsWindowMinimized(hwnd) && processId != processId2 && processId2 != IntPtr.Zero)
				{
					User32.RECT _rect = default(User32.RECT);
					if (User32.GetWindowRect(hwnd, ref _rect) && _rect.Left <= cx && _rect.Right >= 0 && _rect.Top <= cy && _rect.Bottom >= 0 && (_rect.Left != 0 || _rect.Right != 0) && (_rect.Top != 0 || _rect.Bottom != 0) && !(_hwnd == IntPtr.Zero) && (!(_hwnd != IntPtr.Zero) || !WindowEnumerationHelper.IsChildOrOwner(_hwnd, hwnd)))
					{
						isCovered = true;
						break;
					}
				}
			}
			if (isCovered || _hwnd == IntPtr.Zero)
			{
				_hwnd = IntPtr.Zero;
				_associated = IntPtr.Zero;
				_associated2 = IntPtr.Zero;
			}
			else
			{
				User32.RECT hwnd_rect = default(User32.RECT);
				bool windowRect = User32.GetWindowRect(targetHwnd, ref hwnd_rect);
				bool isOutScreen = false;
				if (!windowRect || hwnd_rect.Left > cx || hwnd_rect.Right < 0 || hwnd_rect.Top > cy || hwnd_rect.Bottom < 0 || (hwnd_rect.Left == 0 && hwnd_rect.Right == 0) || (hwnd_rect.Top == 0 && hwnd_rect.Bottom == 0))
				{
					isOutScreen = true;
				}
				if (isOutScreen)
				{
					hwnd_rect.Left = 0;
					hwnd_rect.Top = 0;
					hwnd_rect.Right = cx;
					hwnd_rect.Bottom = cy;
				}
				User32.RECT _hwnd_rect = User32.IntersectsWith(hwnd_rect, cx, cy);
				_associated = User32.WindowFromPoint(new Point(_hwnd_rect.Left + (_hwnd_rect.Right - _hwnd_rect.Left) / 2, _hwnd_rect.Top + (_hwnd_rect.Bottom - _hwnd_rect.Top) / 2));
				Point pt = new Point(cx / 2 - 5, cy - 5);
				IntPtr appBarHwnd = IntPtr.Zero;
				try
				{
					APPBARDATA taskbarRect2 = APIWrapper.GetTaskbarRect();
					AstraDepth3D.WinowHelper.RECT taskbarRect = taskbarRect2.rc;
					appBarHwnd = taskbarRect2.hWnd;
					pt = new Point(taskbarRect.left + (taskbarRect.right - taskbarRect.left) / 2, taskbarRect.top + (taskbarRect.bottom - taskbarRect.top) / 2);
					NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 任务栏句柄->{0}", appBarHwnd));
				}
				catch
				{
					NLogService.WriteLog("MainWindow|Error：获取任务栏区域失败");
				}
				_associated2 = User32.WindowFromPoint(pt);
				if (_associated2 != IntPtr.Zero && _associated2 != _hwnd)
				{
					IntPtr taskbar = User32.GetAncestor(_associated2, 2);
					if (taskbar == appBarHwnd && appBarHwnd != IntPtr.Zero)
					{
						_hwnd = IntPtr.Zero;
						_associated = IntPtr.Zero;
						_associated2 = IntPtr.Zero;
						NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 任务栏可见{0}", taskbar));
					}
					else if (taskbar != IntPtr.Zero)
					{
						StringBuilder lptrString = new StringBuilder(256);
						User32.GetClassName(taskbar, lptrString, lptrString.Capacity);
						string className = lptrString.ToString();
						NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 句柄:{0},类名:{1}", taskbar, className));
						if (className == "Shell_TrayWnd")
						{
							NLogService.WriteLog("MainWindow|检测任务栏: 任务栏可见");
							_hwnd = IntPtr.Zero;
							_associated = IntPtr.Zero;
							_associated2 = IntPtr.Zero;
						}
					}
				}
			}
			NLogService.WriteLog(string.Format("顶层窗口：{0}，{1},{2}", _hwnd, _associated, _associated2));
			return new ValueTuple<IntPtr, IntPtr, IntPtr>(_hwnd, _associated, _associated2);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011534 File Offset: 0x0000F734
		public static ValueTuple<IntPtr, IntPtr, IntPtr> GetTopWindow(IntPtr _targetHwnd, Rectangle screenRect)
		{
			IntPtr _hwnd = IntPtr.Zero;
			IntPtr _associated = IntPtr.Zero;
			IntPtr _associated2 = IntPtr.Zero;
			IntPtr parent = User32.GetAncestor(_targetHwnd, 3);
			IntPtr targetHwnd = IntPtr.Zero;
			if (parent == IntPtr.Zero || parent == APIWrapper.GetDesktopWindow())
			{
				targetHwnd = _targetHwnd;
			}
			else
			{
				targetHwnd = parent;
			}
			IntPtr processId;
			User32.GetWindowThreadProcessId(targetHwnd, out processId);
			List<IntPtr> foregroundWindows = User32.GetForegroundWindows(targetHwnd, screenRect);
			bool isFullScreen = User32.IsFullScreen(targetHwnd, screenRect);
			bool _isPrd = User32.IsPrd(targetHwnd);
			bool _isProgramManager = User32.IsProgramManager(targetHwnd);
			if (isFullScreen && !_isPrd && !_isProgramManager)
			{
				_hwnd = targetHwnd;
			}
			NLogService.WriteLog(string.Format("目标窗口信息：{0}，{1},{2}", targetHwnd, isFullScreen, processId));
			bool isCovered = false;
			foreach (IntPtr hwnd in foregroundWindows)
			{
				IntPtr processId2;
				User32.GetWindowThreadProcessId(hwnd, out processId2);
				if (User32.IsFullScreen(hwnd, screenRect))
				{
					NLogService.WriteLog(string.Format("find fullscreen hwnd：{0}", hwnd));
					if (hwnd == _hwnd && _hwnd != IntPtr.Zero)
					{
						break;
					}
					if ((processId == processId2 || WindowEnumerationHelper.IsChildOrOwner(_hwnd, hwnd)) && isFullScreen)
					{
						NLogService.WriteLog(string.Format("{0}与{1}属于同一进程", hwnd, _hwnd));
						_associated = hwnd;
						break;
					}
					NLogService.WriteLog(string.Format("{0}为顶层全屏窗口", hwnd));
					_hwnd = hwnd;
					break;
				}
				else if (User32.IsWindowVisible(hwnd) && !User32.IsIconic(hwnd) && !User32.IsWindowMinimized(hwnd) && processId != processId2 && processId2 != IntPtr.Zero)
				{
					User32.RECT _rect = default(User32.RECT);
					if (User32.GetWindowRect(hwnd, ref _rect) && !User32.IsOutScreent(_rect, screenRect) && !(_hwnd == IntPtr.Zero) && (!(_hwnd != IntPtr.Zero) || !WindowEnumerationHelper.IsChildOrOwner(_hwnd, hwnd)) && (!(_hwnd != IntPtr.Zero) || !User32.IsToolWindows(hwnd)))
					{
						isCovered = true;
						break;
					}
				}
			}
			if (isCovered || _hwnd == IntPtr.Zero)
			{
				_hwnd = IntPtr.Zero;
				_associated = IntPtr.Zero;
				_associated2 = IntPtr.Zero;
			}
			else
			{
				User32.RECT hwnd_rect = default(User32.RECT);
				bool windowRect = User32.GetWindowRect(targetHwnd, ref hwnd_rect);
				bool isOutScreen = false;
				if (!windowRect || hwnd_rect.Left > screenRect.Right || hwnd_rect.Right < screenRect.Left || hwnd_rect.Top > screenRect.Bottom || hwnd_rect.Bottom < screenRect.Top || (hwnd_rect.Left == 0 && hwnd_rect.Right == 0) || (hwnd_rect.Top == 0 && hwnd_rect.Bottom == 0))
				{
					isOutScreen = true;
				}
				if (isOutScreen)
				{
					hwnd_rect.Left = screenRect.Left;
					hwnd_rect.Top = screenRect.Top;
					hwnd_rect.Right = screenRect.Right;
					hwnd_rect.Bottom = screenRect.Bottom;
				}
				User32.RECT _hwnd_rect = User32.IntersectsWith(hwnd_rect, screenRect);
				_associated = User32.WindowFromPoint(new Point(_hwnd_rect.Left + (_hwnd_rect.Right - _hwnd_rect.Left) / 2, _hwnd_rect.Top + (_hwnd_rect.Bottom - _hwnd_rect.Top) / 2));
				Point pt = new Point(screenRect.Width / 2 + screenRect.Left - 5, screenRect.Bottom - 5);
				IntPtr appBarHwnd = IntPtr.Zero;
				try
				{
					APPBARDATA taskbarRect2 = APIWrapper.GetTaskbarRect();
					AstraDepth3D.WinowHelper.RECT taskbarRect = taskbarRect2.rc;
					appBarHwnd = taskbarRect2.hWnd;
					pt = new Point(taskbarRect.left + (taskbarRect.right - taskbarRect.left) / 2, taskbarRect.top + (taskbarRect.bottom - taskbarRect.top) / 2);
					if (pt.X < screenRect.Left || pt.X > screenRect.Right)
					{
						pt.X = screenRect.Width / 2 + screenRect.Left;
					}
					NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 任务栏句柄->{0}，{1},{2}", appBarHwnd, taskbarRect, pt));
				}
				catch
				{
					NLogService.WriteLog("MainWindow|Error：获取任务栏区域失败");
				}
				_associated2 = User32.WindowFromPoint(pt);
				if (_associated2 != IntPtr.Zero && _associated2 != _hwnd)
				{
					IntPtr taskbar = User32.GetAncestor(_associated2, 2);
					if (taskbar == appBarHwnd && appBarHwnd != IntPtr.Zero)
					{
						_hwnd = IntPtr.Zero;
						_associated = IntPtr.Zero;
						_associated2 = IntPtr.Zero;
						NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 任务栏可见{0}", taskbar));
					}
					else if (taskbar != IntPtr.Zero)
					{
						StringBuilder lptrString = new StringBuilder(256);
						User32.GetClassName(taskbar, lptrString, lptrString.Capacity);
						string className = lptrString.ToString();
						NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 句柄:{0},类名:{1}", taskbar, className));
						if (className == "Shell_TrayWnd" || className == "Shell_SecondaryTrayWnd")
						{
							NLogService.WriteLog("MainWindow|检测任务栏: 任务栏可见");
							_hwnd = IntPtr.Zero;
							_associated = IntPtr.Zero;
							_associated2 = IntPtr.Zero;
						}
					}
				}
			}
			NLogService.WriteLog(string.Format("顶层窗口：{0}，{1},{2}", _hwnd, _associated, _associated2));
			return new ValueTuple<IntPtr, IntPtr, IntPtr>(_hwnd, _associated, _associated2);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public static bool IsOutScreent(User32.RECT rect, Rectangle screenRect)
		{
			return rect.Left >= screenRect.Right || rect.Right <= screenRect.Left || rect.Top >= screenRect.Bottom || rect.Bottom <= screenRect.Top || (rect.Left == 0 && rect.Right == 0) || (rect.Top == 0 && rect.Bottom == 0);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00011B20 File Offset: 0x0000FD20
		public static bool IsOutScreent(IntPtr hwnd, Rectangle screenRect)
		{
			User32.RECT rect = default(User32.RECT);
			return !User32.GetWindowRect(hwnd, ref rect) || (rect.Left >= screenRect.Right || rect.Right <= screenRect.Left || rect.Top >= screenRect.Bottom || rect.Bottom <= screenRect.Top || (rect.Left == 0 && rect.Right == 0) || (rect.Top == 0 && rect.Bottom == 0));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public static bool IsSmallWindow(User32.RECT rect)
		{
			return (long)(rect.Right - rect.Left) <= 100L || (long)(rect.Bottom - rect.Top) <= 100L;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public static bool IsWindowMinimized(IntPtr hWnd)
		{
			User32.WINDOWPLACEMENT placement = default(User32.WINDOWPLACEMENT);
			placement.Length = Marshal.SizeOf<User32.WINDOWPLACEMENT>(placement);
			User32.GetWindowPlacement(hWnd, ref placement);
			return placement.ShowCmd == 2;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00011C00 File Offset: 0x0000FE00
		public static User32.RECT IntersectsWith(User32.RECT rect, int cx, int cy)
		{
			User32.RECT _rect = rect;
			if (_rect.Left < 0)
			{
				_rect.Left = 0;
			}
			if (_rect.Top < 0)
			{
				_rect.Top = 0;
			}
			if (_rect.Right > cx)
			{
				_rect.Right = cx;
			}
			if (_rect.Bottom > cy)
			{
				_rect.Bottom = cy;
			}
			return _rect;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011C54 File Offset: 0x0000FE54
		public static User32.RECT IntersectsWith(User32.RECT rect, Rectangle rectangle)
		{
			User32.RECT _rect = rect;
			if (_rect.Left < rectangle.Left)
			{
				_rect.Left = rectangle.Left;
			}
			if (_rect.Top < rectangle.Top)
			{
				_rect.Top = rectangle.Top;
			}
			if (_rect.Right > rectangle.Right)
			{
				_rect.Right = rectangle.Right;
			}
			if (_rect.Bottom > rectangle.Bottom)
			{
				_rect.Bottom = rectangle.Bottom;
			}
			return _rect;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public static bool IsPrd(IntPtr hwnd)
		{
			StringBuilder lptrString = new StringBuilder(256);
			User32.GetWindowTextW(hwnd, lptrString, lptrString.Capacity);
			string title = lptrString.ToString();
			NLogService.WriteLine(string.Format("MainWindow|检测全屏窗口2: 句柄:{0},标题:{1}", hwnd, title));
			return title.StartsWith("TX_PRD_");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00011D28 File Offset: 0x0000FF28
		public static bool IsPassThrough(IntPtr hwnd)
		{
			IntPtr ex_style = User32.GetWindowLongPtr(hwnd, -20);
			return (ex_style.ToInt64() & 32L) != 0L && (ex_style.ToInt64() & 524288L) != 0L && (ex_style.ToInt64() & 134217728L) != 0L;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00011D70 File Offset: 0x0000FF70
		public static bool IsSubWindow(IntPtr hwnd)
		{
			bool flag = (User32.GetWindowLongPtr(hwnd, -16).ToInt64() & 1073741824L) != 0L;
			bool isTransparent = (User32.GetWindowLongPtr(hwnd, -20).ToInt64() & 32L) != 0L;
			return flag && isTransparent;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public static bool IsChildWindow(IntPtr hwnd)
		{
			return (User32.GetWindowLongPtr(hwnd, -16).ToInt64() & 1073741824L) != 0L;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011DDC File Offset: 0x0000FFDC
		public static bool HasWsMinimizeStyle(IntPtr hWnd)
		{
			return (User32.GetWindowLongPtr(hWnd, -16).ToInt64() & 536870912L) != 0L;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00011E04 File Offset: 0x00010004
		public static IntPtr FindCoreWindow(IntPtr parentHwnd, string parentWindowTitle, string parentClassName = null)
		{
			return User32.FindWindowEx(parentHwnd, IntPtr.Zero, parentClassName, parentWindowTitle);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011E14 File Offset: 0x00010014
		public static bool IsTaskBarVisiable(int cx, int cy)
		{
			Point pt = new Point(cx / 2 - 5, cy - 5);
			IntPtr appBarHwnd = IntPtr.Zero;
			try
			{
				APPBARDATA taskbarRect2 = APIWrapper.GetTaskbarRect();
				AstraDepth3D.WinowHelper.RECT taskbarRect = taskbarRect2.rc;
				appBarHwnd = taskbarRect2.hWnd;
				pt = new Point(taskbarRect.left + (taskbarRect.right - taskbarRect.left) / 2, taskbarRect.top + (taskbarRect.bottom - taskbarRect.top) / 2);
			}
			catch
			{
				NLogService.WriteLog("MainWindow|Error：获取任务栏区域失败");
			}
			IntPtr _associated2 = User32.WindowFromPoint(pt);
			if (_associated2 != IntPtr.Zero)
			{
				IntPtr taskbar = User32.GetAncestor(_associated2, 2);
				if (taskbar == appBarHwnd && appBarHwnd != IntPtr.Zero)
				{
					return true;
				}
				if (taskbar != IntPtr.Zero)
				{
					StringBuilder lptrString = new StringBuilder(256);
					User32.GetClassName(taskbar, lptrString, lptrString.Capacity);
					string className = lptrString.ToString();
					NLogService.WriteLog(string.Format("MainWindow|检测任务栏: 句柄:{0},类名:{1}", taskbar, className));
					if (className == "Shell_TrayWnd")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00011F34 File Offset: 0x00010134
		public static ValueTuple<IntPtr, IntPtr> IsTaskBarOrGearVisible(IntPtr targetHwnd)
		{
			IntPtr hTaskbar = User32.FindWindow("Shell_TrayWnd", null);
			if (hTaskbar == IntPtr.Zero)
			{
				return new ValueTuple<IntPtr, IntPtr>(IntPtr.Zero, IntPtr.Zero);
			}
			IntPtr trayWnd = IntPtr.Zero;
			IntPtr gear = IntPtr.Zero;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (hWnd == targetHwnd)
				{
					return false;
				}
				string winTitle = User32.GetWinTitle(hWnd);
				string clsName = User32.GetWinClassName(hWnd);
				if (winTitle.Equals("3D Zone") && clsName.Equals("FLUTTER_RUNNER_WIN32_WINDOW"))
				{
					gear = hWnd;
				}
				if (hTaskbar == hWnd)
				{
					trayWnd = hWnd;
				}
				return true;
			}, IntPtr.Zero);
			return new ValueTuple<IntPtr, IntPtr>(trayWnd, gear);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011FC0 File Offset: 0x000101C0
		public static ValueTuple<IntPtr, IntPtr> IsTaskBarOrGearVisible(IntPtr targetHwnd, Rectangle screenRect)
		{
			IntPtr trayWnd = IntPtr.Zero;
			IntPtr gear = IntPtr.Zero;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (hWnd == targetHwnd)
				{
					return false;
				}
				string winTitle = User32.GetWinTitle(hWnd);
				string clsName = User32.GetWinClassName(hWnd);
				if (winTitle.Equals("3D Zone") && clsName.Equals("FLUTTER_RUNNER_WIN32_WINDOW"))
				{
					gear = hWnd;
				}
				if ((clsName.Equals("Shell_TrayWnd") || clsName.Equals("Shell_SecondaryTrayWnd")) && !User32.IsOutScreent(hWnd, screenRect))
				{
					trayWnd = hWnd;
					return false;
				}
				return true;
			}, IntPtr.Zero);
			return new ValueTuple<IntPtr, IntPtr>(trayWnd, gear);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00012020 File Offset: 0x00010220
		public static ValueTuple<IntPtr, IntPtr> IsTaskBarVisible(IntPtr targetHwnd, IntPtr trayWnd_)
		{
			IntPtr trayWnd = IntPtr.Zero;
			IntPtr gear = IntPtr.Zero;
			User32.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (hWnd == targetHwnd)
				{
					return false;
				}
				string winTitle = User32.GetWinTitle(hWnd);
				string clsName = User32.GetWinClassName(hWnd);
				if (winTitle.Equals("3D Zone") && clsName.Equals("FLUTTER_RUNNER_WIN32_WINDOW"))
				{
					gear = hWnd;
				}
				if (trayWnd_ == hWnd)
				{
					trayWnd = hWnd;
					return false;
				}
				return true;
			}, IntPtr.Zero);
			return new ValueTuple<IntPtr, IntPtr>(trayWnd, gear);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00012080 File Offset: 0x00010280
		public static string GetWinClassName(IntPtr hwnd)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			User32.GetClassName(hwnd, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000120B0 File Offset: 0x000102B0
		public static string GetWinTitle(IntPtr hwnd)
		{
			int length = User32.GetWindowTextLength(hwnd);
			if (length <= 0)
			{
				return string.Empty;
			}
			length += 2;
			StringBuilder builder = new StringBuilder(length);
			User32.GetWindowTextW(hwnd, builder, builder.Capacity);
			return builder.ToString();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000120EF File Offset: 0x000102EF
		public static bool IsWindowsDashBoard(IntPtr hwnd)
		{
			return User32.FindWindowEx(hwnd, IntPtr.Zero, "Chrome_WidgetWin_0", null) != IntPtr.Zero;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001210C File Offset: 0x0001030C
		public static bool IsGameBar(IntPtr hwnd)
		{
			IntPtr child = User32.FindWindowEx(hwnd, IntPtr.Zero, "Windows.UI.Core.CoreWindow", null);
			if (child != IntPtr.Zero)
			{
				StringBuilder builder = new StringBuilder(User32.GetWindowTextLength(child) + 2);
				User32.GetWindowTextW(child, builder, builder.Capacity);
				string title = builder.ToString();
				if (title == "Game Bar")
				{
					NLogService.WriteLog("MainWindow|IsGameBar:" + title);
					return true;
				}
			}
			NLogService.WriteLine("MainWindow|IsGameBar:没有");
			return false;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00012188 File Offset: 0x00010388
		public static bool IsToolWindows(IntPtr hwnd)
		{
			IntPtr ex_style = User32.GetWindowLongPtr(hwnd, -20);
			return (ex_style.ToInt64() & 128L) != 0L && (ex_style.ToInt64() & 8L) != 0L && (ex_style.ToInt64() & 134217728L) != 0L && (ex_style.ToInt64() & 262144L) == 0L;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000121E0 File Offset: 0x000103E0
		public static bool IsProgramManager(IntPtr hwnd)
		{
			StringBuilder _lptrString2 = new StringBuilder(256);
			User32.GetWindowTextW(hwnd, _lptrString2, _lptrString2.Capacity);
			return _lptrString2.ToString() == "Program Manager";
		}

		// Token: 0x0400019B RID: 411
		public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		// Token: 0x0400019C RID: 412
		public const uint SWP_FRAMECHANGED = 32U;

		// Token: 0x0400019D RID: 413
		public const uint SWP_NOMOVE = 2U;

		// Token: 0x0400019E RID: 414
		public const uint SWP_NOSIZE = 1U;

		// Token: 0x0400019F RID: 415
		public const uint SWP_NOACTIVATE = 16U;

		// Token: 0x040001A0 RID: 416
		public const uint SWP_SHOWWINDOW = 64U;

		// Token: 0x040001A1 RID: 417
		public const uint AppNotFoundCode = 2147746293U;

		// Token: 0x040001A2 RID: 418
		public const uint WS_EX_LAYERED = 524288U;

		// Token: 0x040001A3 RID: 419
		public const int WS_EX_TRANSPARENT = 32;

		// Token: 0x040001A4 RID: 420
		public const uint WS_MAXIMIZE = 16777216U;

		// Token: 0x040001A5 RID: 421
		public const uint WS_POPUPWINDOW = 2147483648U;

		// Token: 0x040001A6 RID: 422
		public const int GWL_STYLE = -16;

		// Token: 0x040001A7 RID: 423
		public const int GWL_EXSTYLE = -20;

		// Token: 0x040001A8 RID: 424
		public const int LWA_ALPHA = 0;

		// Token: 0x040001A9 RID: 425
		public const int SW_HIDE = 0;

		// Token: 0x040001AA RID: 426
		public const int SW_NORMAL = 1;

		// Token: 0x040001AB RID: 427
		public const int SW_MAXIMIZE = 3;

		// Token: 0x040001AC RID: 428
		public const int SW_SHOWNOACTIVATE = 4;

		// Token: 0x040001AD RID: 429
		public const int SW_SHOW = 5;

		// Token: 0x040001AE RID: 430
		public const int SW_MINIMIZE = 6;

		// Token: 0x040001AF RID: 431
		public const int SW_RESTORE = 9;

		// Token: 0x040001B0 RID: 432
		public const int SW_SHOWDEFAULT = 10;

		// Token: 0x040001B1 RID: 433
		public const int WDA_EXCLUDEFROMCAPTURE = 17;

		// Token: 0x040001B2 RID: 434
		public const uint WINEVENT_OUTOFCONTEXT = 0U;

		// Token: 0x040001B3 RID: 435
		public const uint EVENT_MIN = 1U;

		// Token: 0x040001B4 RID: 436
		public const uint EVENT_SYSTEM_FOREGROUND = 3U;

		// Token: 0x040001B5 RID: 437
		public const uint EVENT_SYSTEM_DESKTOPSWITCH = 32U;

		// Token: 0x040001B6 RID: 438
		public const uint EVENT_OBJECT_IME_SHOW = 32807U;

		// Token: 0x040001B7 RID: 439
		public const uint EVENT_SYSTEM_SWITCHSTART = 20U;

		// Token: 0x040001B8 RID: 440
		public const uint EVENT_OBJECT_CLOAKED = 32791U;

		// Token: 0x040001B9 RID: 441
		public const uint EVENT_OBJECT_DESTROY = 32769U;

		// Token: 0x040001BA RID: 442
		public const uint EVENT_OBJECT_LOCATIONCHANGE = 32779U;

		// Token: 0x040001BB RID: 443
		public const uint EVENT_SYSTEM_MINIMIZESTART = 22U;

		// Token: 0x040001BC RID: 444
		public const uint EVENT_OBJECT_CREATE = 32768U;

		// Token: 0x040001BD RID: 445
		public const uint EVENT_OBJECT_SHOW = 32770U;

		// Token: 0x040001BE RID: 446
		public const uint EVENT_OBJECT_HIDE = 32771U;

		// Token: 0x040001BF RID: 447
		public const uint EVENT_OBJECT_REORDER = 32772U;

		// Token: 0x040001C0 RID: 448
		public const uint EVENT_MAX = 2147483647U;

		// Token: 0x040001C1 RID: 449
		public const uint WINEVENT_SKIPOWNPROCESS = 2U;

		// Token: 0x040001C2 RID: 450
		public const uint GwOwner = 4U;

		// Token: 0x040001C3 RID: 451
		public const int HOTKEY_ID = 9527;

		// Token: 0x040001C4 RID: 452
		public const uint MOD_NONE = 0U;

		// Token: 0x040001C5 RID: 453
		public const uint MOD_ALT = 1U;

		// Token: 0x040001C6 RID: 454
		public const uint MOD_CONTROL = 2U;

		// Token: 0x040001C7 RID: 455
		public const uint MOD_SHIFT = 4U;

		// Token: 0x040001C8 RID: 456
		public const uint MOD_WIN = 8U;

		// Token: 0x040001C9 RID: 457
		public const uint VK_CAPITAL = 20U;

		// Token: 0x040001CA RID: 458
		public const uint VK_ESCAPE = 27U;

		// Token: 0x040001CB RID: 459
		public const uint VK_ADD = 107U;

		// Token: 0x040001CC RID: 460
		public const uint VK_SUBTRACT = 109U;

		// Token: 0x040001CD RID: 461
		public const uint VK_TAB = 9U;

		// Token: 0x040001CE RID: 462
		public const uint VK_F4 = 115U;

		// Token: 0x040001CF RID: 463
		public const uint VK_F2 = 113U;

		// Token: 0x040001D0 RID: 464
		public const uint VK_T = 84U;

		// Token: 0x040001D1 RID: 465
		public const uint VK_UP_ARROW = 38U;

		// Token: 0x040001D2 RID: 466
		public const uint VK_DW_ARROW = 40U;

		// Token: 0x040001D3 RID: 467
		public const uint SMALL_WINDOW_WIDTH = 100U;

		// Token: 0x040001D4 RID: 468
		public const uint SMALL_WINDOW_HEIGHT = 100U;

		// Token: 0x020000B6 RID: 182
		[Flags]
		public enum WindowExStyles
		{
			// Token: 0x04000469 RID: 1129
			WS_EX_ACCEPTFILES = 16,
			// Token: 0x0400046A RID: 1130
			WS_EX_APPWINDOW = 262144,
			// Token: 0x0400046B RID: 1131
			WS_EX_CLIENTEDGE = 512,
			// Token: 0x0400046C RID: 1132
			WS_EX_COMPOSITED = 33554432,
			// Token: 0x0400046D RID: 1133
			WS_EX_CONTEXTHELP = 1024,
			// Token: 0x0400046E RID: 1134
			WS_EX_CONTROLPARENT = 65536,
			// Token: 0x0400046F RID: 1135
			WS_EX_DLGMODALFRAME = 1,
			// Token: 0x04000470 RID: 1136
			WS_EX_LAYERED = 524288,
			// Token: 0x04000471 RID: 1137
			WS_EX_LAYOUTRTL = 4194304,
			// Token: 0x04000472 RID: 1138
			WS_EX_LEFT = 0,
			// Token: 0x04000473 RID: 1139
			WS_EX_LEFTSCROLLBAR = 16384,
			// Token: 0x04000474 RID: 1140
			WS_EX_LTRREADING = 0,
			// Token: 0x04000475 RID: 1141
			WS_EX_MDICHILD = 64,
			// Token: 0x04000476 RID: 1142
			WS_EX_NOACTIVATE = 134217728,
			// Token: 0x04000477 RID: 1143
			WS_EX_NOINHERITLAYOUT = 1048576,
			// Token: 0x04000478 RID: 1144
			WS_EX_NOPARENTNOTIFY = 4,
			// Token: 0x04000479 RID: 1145
			WS_EX_NOREDIRECTIONBITMAP = 2097152,
			// Token: 0x0400047A RID: 1146
			WS_EX_OVERLAPPEDWINDOW = 768,
			// Token: 0x0400047B RID: 1147
			WS_EX_PALETTEWINDOW = 392,
			// Token: 0x0400047C RID: 1148
			WS_EX_RIGHT = 4096,
			// Token: 0x0400047D RID: 1149
			WS_EX_RIGHTSCROLLBAR = 0,
			// Token: 0x0400047E RID: 1150
			WS_EX_RTLREADING = 8192,
			// Token: 0x0400047F RID: 1151
			WS_EX_STATICEDGE = 131072,
			// Token: 0x04000480 RID: 1152
			WS_EX_TOOLWINDOW = 128,
			// Token: 0x04000481 RID: 1153
			WS_EX_TOPMOST = 8,
			// Token: 0x04000482 RID: 1154
			WS_EX_TRANSPARENT = 32,
			// Token: 0x04000483 RID: 1155
			WS_EX_WINDOWEDGE = 256
		}

		// Token: 0x020000B7 RID: 183
		public enum WM
		{
			// Token: 0x04000485 RID: 1157
			NULL,
			// Token: 0x04000486 RID: 1158
			CREATE,
			// Token: 0x04000487 RID: 1159
			DESTROY,
			// Token: 0x04000488 RID: 1160
			MOVE,
			// Token: 0x04000489 RID: 1161
			SIZE = 5,
			// Token: 0x0400048A RID: 1162
			ACTIVATE,
			// Token: 0x0400048B RID: 1163
			SETFOCUS,
			// Token: 0x0400048C RID: 1164
			KILLFOCUS,
			// Token: 0x0400048D RID: 1165
			ENABLE = 10,
			// Token: 0x0400048E RID: 1166
			SETREDRAW,
			// Token: 0x0400048F RID: 1167
			SETTEXT,
			// Token: 0x04000490 RID: 1168
			GETTEXT,
			// Token: 0x04000491 RID: 1169
			GETTEXTLENGTH,
			// Token: 0x04000492 RID: 1170
			PAINT,
			// Token: 0x04000493 RID: 1171
			CLOSE,
			// Token: 0x04000494 RID: 1172
			QUERYENDSESSION,
			// Token: 0x04000495 RID: 1173
			QUERYOPEN = 19,
			// Token: 0x04000496 RID: 1174
			ENDSESSION = 22,
			// Token: 0x04000497 RID: 1175
			QUIT = 18,
			// Token: 0x04000498 RID: 1176
			ERASEBKGND = 20,
			// Token: 0x04000499 RID: 1177
			SYSCOLORCHANGE,
			// Token: 0x0400049A RID: 1178
			SHOWWINDOW = 24,
			// Token: 0x0400049B RID: 1179
			WININICHANGE = 26,
			// Token: 0x0400049C RID: 1180
			SETTINGCHANGE = 26,
			// Token: 0x0400049D RID: 1181
			DEVMODECHANGE,
			// Token: 0x0400049E RID: 1182
			ACTIVATEAPP,
			// Token: 0x0400049F RID: 1183
			FONTCHANGE,
			// Token: 0x040004A0 RID: 1184
			TIMECHANGE,
			// Token: 0x040004A1 RID: 1185
			CANCELMODE,
			// Token: 0x040004A2 RID: 1186
			SETCURSOR,
			// Token: 0x040004A3 RID: 1187
			MOUSEACTIVATE,
			// Token: 0x040004A4 RID: 1188
			CHILDACTIVATE,
			// Token: 0x040004A5 RID: 1189
			QUEUESYNC,
			// Token: 0x040004A6 RID: 1190
			GETMINMAXINFO,
			// Token: 0x040004A7 RID: 1191
			PAINTICON = 38,
			// Token: 0x040004A8 RID: 1192
			ICONERASEBKGND,
			// Token: 0x040004A9 RID: 1193
			NEXTDLGCTL,
			// Token: 0x040004AA RID: 1194
			SPOOLERSTATUS = 42,
			// Token: 0x040004AB RID: 1195
			DRAWITEM,
			// Token: 0x040004AC RID: 1196
			MEASUREITEM,
			// Token: 0x040004AD RID: 1197
			DELETEITEM,
			// Token: 0x040004AE RID: 1198
			VKEYTOITEM,
			// Token: 0x040004AF RID: 1199
			CHARTOITEM,
			// Token: 0x040004B0 RID: 1200
			SETFONT,
			// Token: 0x040004B1 RID: 1201
			GETFONT,
			// Token: 0x040004B2 RID: 1202
			SETHOTKEY,
			// Token: 0x040004B3 RID: 1203
			GETHOTKEY,
			// Token: 0x040004B4 RID: 1204
			QUERYDRAGICON = 55,
			// Token: 0x040004B5 RID: 1205
			COMPAREITEM = 57,
			// Token: 0x040004B6 RID: 1206
			GETOBJECT = 61,
			// Token: 0x040004B7 RID: 1207
			COMPACTING = 65,
			// Token: 0x040004B8 RID: 1208
			COMMNOTIFY = 68,
			// Token: 0x040004B9 RID: 1209
			WINDOWPOSCHANGING = 70,
			// Token: 0x040004BA RID: 1210
			WINDOWPOSCHANGED,
			// Token: 0x040004BB RID: 1211
			POWER,
			// Token: 0x040004BC RID: 1212
			COPYDATA = 74,
			// Token: 0x040004BD RID: 1213
			CANCELJOURNAL,
			// Token: 0x040004BE RID: 1214
			NOTIFY = 78,
			// Token: 0x040004BF RID: 1215
			INPUTLANGCHANGEREQUEST = 80,
			// Token: 0x040004C0 RID: 1216
			INPUTLANGCHANGE,
			// Token: 0x040004C1 RID: 1217
			TCARD,
			// Token: 0x040004C2 RID: 1218
			HELP,
			// Token: 0x040004C3 RID: 1219
			USERCHANGED,
			// Token: 0x040004C4 RID: 1220
			NOTIFYFORMAT,
			// Token: 0x040004C5 RID: 1221
			CONTEXTMENU = 123,
			// Token: 0x040004C6 RID: 1222
			STYLECHANGING,
			// Token: 0x040004C7 RID: 1223
			STYLECHANGED,
			// Token: 0x040004C8 RID: 1224
			DISPLAYCHANGE,
			// Token: 0x040004C9 RID: 1225
			GETICON,
			// Token: 0x040004CA RID: 1226
			SETICON,
			// Token: 0x040004CB RID: 1227
			NCCREATE,
			// Token: 0x040004CC RID: 1228
			NCDESTROY,
			// Token: 0x040004CD RID: 1229
			NCCALCSIZE,
			// Token: 0x040004CE RID: 1230
			NCHITTEST,
			// Token: 0x040004CF RID: 1231
			NCPAINT,
			// Token: 0x040004D0 RID: 1232
			NCACTIVATE,
			// Token: 0x040004D1 RID: 1233
			GETDLGCODE,
			// Token: 0x040004D2 RID: 1234
			SYNCPAINT,
			// Token: 0x040004D3 RID: 1235
			NCMOUSEMOVE = 160,
			// Token: 0x040004D4 RID: 1236
			NCLBUTTONDOWN,
			// Token: 0x040004D5 RID: 1237
			NCLBUTTONUP,
			// Token: 0x040004D6 RID: 1238
			NCLBUTTONDBLCLK,
			// Token: 0x040004D7 RID: 1239
			NCRBUTTONDOWN,
			// Token: 0x040004D8 RID: 1240
			NCRBUTTONUP,
			// Token: 0x040004D9 RID: 1241
			NCRBUTTONDBLCLK,
			// Token: 0x040004DA RID: 1242
			NCMBUTTONDOWN,
			// Token: 0x040004DB RID: 1243
			NCMBUTTONUP,
			// Token: 0x040004DC RID: 1244
			NCMBUTTONDBLCLK,
			// Token: 0x040004DD RID: 1245
			NCXBUTTONDOWN = 171,
			// Token: 0x040004DE RID: 1246
			NCXBUTTONUP,
			// Token: 0x040004DF RID: 1247
			NCXBUTTONDBLCLK,
			// Token: 0x040004E0 RID: 1248
			INPUT_DEVICE_CHANGE = 254,
			// Token: 0x040004E1 RID: 1249
			INPUT,
			// Token: 0x040004E2 RID: 1250
			KEYFIRST,
			// Token: 0x040004E3 RID: 1251
			KEYDOWN = 256,
			// Token: 0x040004E4 RID: 1252
			KEYUP,
			// Token: 0x040004E5 RID: 1253
			CHAR,
			// Token: 0x040004E6 RID: 1254
			DEADCHAR,
			// Token: 0x040004E7 RID: 1255
			SYSKEYDOWN,
			// Token: 0x040004E8 RID: 1256
			SYSKEYUP,
			// Token: 0x040004E9 RID: 1257
			SYSCHAR,
			// Token: 0x040004EA RID: 1258
			SYSDEADCHAR,
			// Token: 0x040004EB RID: 1259
			UNICHAR = 265,
			// Token: 0x040004EC RID: 1260
			KEYLAST = 265,
			// Token: 0x040004ED RID: 1261
			IME_STARTCOMPOSITION = 269,
			// Token: 0x040004EE RID: 1262
			IME_ENDCOMPOSITION,
			// Token: 0x040004EF RID: 1263
			IME_COMPOSITION,
			// Token: 0x040004F0 RID: 1264
			IME_KEYLAST = 271,
			// Token: 0x040004F1 RID: 1265
			INITDIALOG,
			// Token: 0x040004F2 RID: 1266
			COMMAND,
			// Token: 0x040004F3 RID: 1267
			SYSCOMMAND,
			// Token: 0x040004F4 RID: 1268
			TIMER,
			// Token: 0x040004F5 RID: 1269
			HSCROLL,
			// Token: 0x040004F6 RID: 1270
			VSCROLL,
			// Token: 0x040004F7 RID: 1271
			INITMENU,
			// Token: 0x040004F8 RID: 1272
			INITMENUPOPUP,
			// Token: 0x040004F9 RID: 1273
			GESTURE = 281,
			// Token: 0x040004FA RID: 1274
			GESTURENOTIFY,
			// Token: 0x040004FB RID: 1275
			MENUSELECT = 287,
			// Token: 0x040004FC RID: 1276
			MENUCHAR,
			// Token: 0x040004FD RID: 1277
			ENTERIDLE,
			// Token: 0x040004FE RID: 1278
			MENURBUTTONUP,
			// Token: 0x040004FF RID: 1279
			MENUDRAG,
			// Token: 0x04000500 RID: 1280
			MENUGETOBJECT,
			// Token: 0x04000501 RID: 1281
			UNINITMENUPOPUP,
			// Token: 0x04000502 RID: 1282
			MENUCOMMAND,
			// Token: 0x04000503 RID: 1283
			CHANGEUISTATE,
			// Token: 0x04000504 RID: 1284
			UPDATEUISTATE,
			// Token: 0x04000505 RID: 1285
			QUERYUISTATE,
			// Token: 0x04000506 RID: 1286
			CTLCOLORMSGBOX = 306,
			// Token: 0x04000507 RID: 1287
			CTLCOLOREDIT,
			// Token: 0x04000508 RID: 1288
			CTLCOLORLISTBOX,
			// Token: 0x04000509 RID: 1289
			CTLCOLORBTN,
			// Token: 0x0400050A RID: 1290
			CTLCOLORDLG,
			// Token: 0x0400050B RID: 1291
			CTLCOLORSCROLLBAR,
			// Token: 0x0400050C RID: 1292
			CTLCOLORSTATIC,
			// Token: 0x0400050D RID: 1293
			MOUSEFIRST = 512,
			// Token: 0x0400050E RID: 1294
			MOUSEMOVE = 512,
			// Token: 0x0400050F RID: 1295
			LBUTTONDOWN,
			// Token: 0x04000510 RID: 1296
			LBUTTONUP,
			// Token: 0x04000511 RID: 1297
			LBUTTONDBLCLK,
			// Token: 0x04000512 RID: 1298
			RBUTTONDOWN,
			// Token: 0x04000513 RID: 1299
			RBUTTONUP,
			// Token: 0x04000514 RID: 1300
			RBUTTONDBLCLK,
			// Token: 0x04000515 RID: 1301
			MBUTTONDOWN,
			// Token: 0x04000516 RID: 1302
			MBUTTONUP,
			// Token: 0x04000517 RID: 1303
			MBUTTONDBLCLK,
			// Token: 0x04000518 RID: 1304
			MOUSEWHEEL,
			// Token: 0x04000519 RID: 1305
			XBUTTONDOWN,
			// Token: 0x0400051A RID: 1306
			XBUTTONUP,
			// Token: 0x0400051B RID: 1307
			XBUTTONDBLCLK,
			// Token: 0x0400051C RID: 1308
			MOUSEHWHEEL,
			// Token: 0x0400051D RID: 1309
			MOUSELAST = 526,
			// Token: 0x0400051E RID: 1310
			PARENTNOTIFY = 528,
			// Token: 0x0400051F RID: 1311
			ENTERMENULOOP,
			// Token: 0x04000520 RID: 1312
			EXITMENULOOP,
			// Token: 0x04000521 RID: 1313
			NEXTMENU,
			// Token: 0x04000522 RID: 1314
			SIZING,
			// Token: 0x04000523 RID: 1315
			CAPTURECHANGED,
			// Token: 0x04000524 RID: 1316
			MOVING,
			// Token: 0x04000525 RID: 1317
			POWERBROADCAST = 536,
			// Token: 0x04000526 RID: 1318
			DEVICECHANGE,
			// Token: 0x04000527 RID: 1319
			MDICREATE = 544,
			// Token: 0x04000528 RID: 1320
			MDIDESTROY,
			// Token: 0x04000529 RID: 1321
			MDIACTIVATE,
			// Token: 0x0400052A RID: 1322
			MDIRESTORE,
			// Token: 0x0400052B RID: 1323
			MDINEXT,
			// Token: 0x0400052C RID: 1324
			MDIMAXIMIZE,
			// Token: 0x0400052D RID: 1325
			MDITILE,
			// Token: 0x0400052E RID: 1326
			MDICASCADE,
			// Token: 0x0400052F RID: 1327
			MDIICONARRANGE,
			// Token: 0x04000530 RID: 1328
			MDIGETACTIVE,
			// Token: 0x04000531 RID: 1329
			MDISETMENU = 560,
			// Token: 0x04000532 RID: 1330
			ENTERSIZEMOVE,
			// Token: 0x04000533 RID: 1331
			EXITSIZEMOVE,
			// Token: 0x04000534 RID: 1332
			DROPFILES,
			// Token: 0x04000535 RID: 1333
			MDIREFRESHMENU,
			// Token: 0x04000536 RID: 1334
			POINTERDEVICECHANGE = 568,
			// Token: 0x04000537 RID: 1335
			POINTERDEVICEINRANGE,
			// Token: 0x04000538 RID: 1336
			POINTERDEVICEOUTOFRANGE,
			// Token: 0x04000539 RID: 1337
			TOUCH = 576,
			// Token: 0x0400053A RID: 1338
			NCPOINTERUPDATE,
			// Token: 0x0400053B RID: 1339
			NCPOINTERDOWN,
			// Token: 0x0400053C RID: 1340
			NCPOINTERUP,
			// Token: 0x0400053D RID: 1341
			POINTERUPDATE = 581,
			// Token: 0x0400053E RID: 1342
			POINTERDOWN,
			// Token: 0x0400053F RID: 1343
			POINTERUP,
			// Token: 0x04000540 RID: 1344
			POINTERENTER = 585,
			// Token: 0x04000541 RID: 1345
			POINTERLEAVE,
			// Token: 0x04000542 RID: 1346
			POINTERACTIVATE,
			// Token: 0x04000543 RID: 1347
			POINTERCAPTURECHANGED,
			// Token: 0x04000544 RID: 1348
			TOUCHHITTESTING,
			// Token: 0x04000545 RID: 1349
			POINTERWHEEL,
			// Token: 0x04000546 RID: 1350
			POINTERHWHEEL,
			// Token: 0x04000547 RID: 1351
			IME_SETCONTEXT = 641,
			// Token: 0x04000548 RID: 1352
			IME_NOTIFY,
			// Token: 0x04000549 RID: 1353
			IME_CONTROL,
			// Token: 0x0400054A RID: 1354
			IME_COMPOSITIONFULL,
			// Token: 0x0400054B RID: 1355
			IME_SELECT,
			// Token: 0x0400054C RID: 1356
			IME_CHAR,
			// Token: 0x0400054D RID: 1357
			IME_REQUEST = 648,
			// Token: 0x0400054E RID: 1358
			IME_KEYDOWN = 656,
			// Token: 0x0400054F RID: 1359
			IME_KEYUP,
			// Token: 0x04000550 RID: 1360
			MOUSEHOVER = 673,
			// Token: 0x04000551 RID: 1361
			MOUSELEAVE = 675,
			// Token: 0x04000552 RID: 1362
			NCMOUSEHOVER = 672,
			// Token: 0x04000553 RID: 1363
			NCMOUSELEAVE = 674,
			// Token: 0x04000554 RID: 1364
			WTSSESSION_CHANGE = 689,
			// Token: 0x04000555 RID: 1365
			TABLET_FIRST = 704,
			// Token: 0x04000556 RID: 1366
			TABLET_LAST = 735,
			// Token: 0x04000557 RID: 1367
			DPICHANGED,
			// Token: 0x04000558 RID: 1368
			CUT = 768,
			// Token: 0x04000559 RID: 1369
			COPY,
			// Token: 0x0400055A RID: 1370
			PASTE,
			// Token: 0x0400055B RID: 1371
			CLEAR,
			// Token: 0x0400055C RID: 1372
			UNDO,
			// Token: 0x0400055D RID: 1373
			RENDERFORMAT,
			// Token: 0x0400055E RID: 1374
			RENDERALLFORMATS,
			// Token: 0x0400055F RID: 1375
			DESTROYCLIPBOARD,
			// Token: 0x04000560 RID: 1376
			DRAWCLIPBOARD,
			// Token: 0x04000561 RID: 1377
			PAINTCLIPBOARD,
			// Token: 0x04000562 RID: 1378
			VSCROLLCLIPBOARD,
			// Token: 0x04000563 RID: 1379
			SIZECLIPBOARD,
			// Token: 0x04000564 RID: 1380
			ASKCBFORMATNAME,
			// Token: 0x04000565 RID: 1381
			CHANGECBCHAIN,
			// Token: 0x04000566 RID: 1382
			HSCROLLCLIPBOARD,
			// Token: 0x04000567 RID: 1383
			QUERYNEWPALETTE,
			// Token: 0x04000568 RID: 1384
			PALETTEISCHANGING,
			// Token: 0x04000569 RID: 1385
			PALETTECHANGED,
			// Token: 0x0400056A RID: 1386
			HOTKEY,
			// Token: 0x0400056B RID: 1387
			PRINT = 791,
			// Token: 0x0400056C RID: 1388
			PRINTCLIENT,
			// Token: 0x0400056D RID: 1389
			APPCOMMAND,
			// Token: 0x0400056E RID: 1390
			THEMECHANGED,
			// Token: 0x0400056F RID: 1391
			CLIPBOARDUPDATE = 797,
			// Token: 0x04000570 RID: 1392
			DWMCOMPOSITIONCHANGED,
			// Token: 0x04000571 RID: 1393
			DWMNCRENDERINGCHANGED,
			// Token: 0x04000572 RID: 1394
			DWMCOLORIZATIONCOLORCHANGED,
			// Token: 0x04000573 RID: 1395
			DWMWINDOWMAXIMIZEDCHANGE,
			// Token: 0x04000574 RID: 1396
			DWMSENDICONICTHUMBNAIL = 803,
			// Token: 0x04000575 RID: 1397
			DWMSENDICONICLIVEPREVIEWBITMAP = 806,
			// Token: 0x04000576 RID: 1398
			GETTITLEBARINFOEX = 831,
			// Token: 0x04000577 RID: 1399
			HANDHELDFIRST = 856,
			// Token: 0x04000578 RID: 1400
			HANDHELDLAST = 863,
			// Token: 0x04000579 RID: 1401
			AFXFIRST,
			// Token: 0x0400057A RID: 1402
			AFXLAST = 895,
			// Token: 0x0400057B RID: 1403
			PENWINFIRST,
			// Token: 0x0400057C RID: 1404
			PENWINLAST = 911,
			// Token: 0x0400057D RID: 1405
			APP = 32768,
			// Token: 0x0400057E RID: 1406
			USER = 1024
		}

		// Token: 0x020000B8 RID: 184
		public enum GetWindowLongFields
		{
			// Token: 0x04000580 RID: 1408
			GWL_EXSTYLE = -20,
			// Token: 0x04000581 RID: 1409
			GWL_HINSTANCE = -6,
			// Token: 0x04000582 RID: 1410
			GWL_HWNDPARENT = -8,
			// Token: 0x04000583 RID: 1411
			GWL_ID = -12,
			// Token: 0x04000584 RID: 1412
			GWL_STYLE = -16,
			// Token: 0x04000585 RID: 1413
			GWL_USERDATA = -21,
			// Token: 0x04000586 RID: 1414
			GWL_WNDPROC = -4
		}

		// Token: 0x020000B9 RID: 185
		public struct RECT
		{
			// Token: 0x04000587 RID: 1415
			public int Left;

			// Token: 0x04000588 RID: 1416
			public int Top;

			// Token: 0x04000589 RID: 1417
			public int Right;

			// Token: 0x0400058A RID: 1418
			public int Bottom;
		}

		// Token: 0x020000BA RID: 186
		public struct WINDOWPLACEMENT
		{
			// Token: 0x0400058B RID: 1419
			public int Length;

			// Token: 0x0400058C RID: 1420
			public int Flags;

			// Token: 0x0400058D RID: 1421
			public int ShowCmd;

			// Token: 0x0400058E RID: 1422
			public Point PtMinPosition;

			// Token: 0x0400058F RID: 1423
			public Point PtMaxPosition;

			// Token: 0x04000590 RID: 1424
			public Rectangle RcNormalPosition;
		}

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x0600047C RID: 1148
		public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x06000480 RID: 1152
		public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x06000484 RID: 1156
		public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, long idObject, long idChild, ulong dwEventThread, ulong dwmsEventTime);
	}
}
