using System;
using System.Runtime.InteropServices;

namespace AstraDepth3D
{
	// Token: 0x02000012 RID: 18
	internal static class WindowEnumerationHelper
	{
		// Token: 0x0600009C RID: 156
		[DllImport("user32.dll")]
		private static extern IntPtr GetShellWindow();

		// Token: 0x0600009D RID: 157
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x0600009E RID: 158
		[DllImport("user32.dll", ExactSpelling = true)]
		private static extern IntPtr GetAncestor(IntPtr hwnd, WindowEnumerationHelper.GetAncestorFlags flags);

		// Token: 0x0600009F RID: 159
		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

		// Token: 0x060000A0 RID: 160
		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

		// Token: 0x060000A1 RID: 161 RVA: 0x0000560F File Offset: 0x0000380F
		private static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			if (IntPtr.Size == 8)
			{
				return WindowEnumerationHelper.GetWindowLongPtr64(hWnd, nIndex);
			}
			return WindowEnumerationHelper.GetWindowLongPtr32(hWnd, nIndex);
		}

		// Token: 0x060000A2 RID: 162
		[DllImport("dwmapi.dll")]
		private static extern int DwmGetWindowAttribute(IntPtr hwnd, WindowEnumerationHelper.DWMWINDOWATTRIBUTE dwAttribute, out bool pvAttribute, int cbAttribute);

		// Token: 0x060000A3 RID: 163 RVA: 0x00005628 File Offset: 0x00003828
		public static bool IsWindowValidForCapture(IntPtr hwnd)
		{
			if (hwnd.ToInt32() == 0)
			{
				return false;
			}
			if (hwnd == WindowEnumerationHelper.GetShellWindow())
			{
				return false;
			}
			if (!WindowEnumerationHelper.IsWindowVisible(hwnd))
			{
				return false;
			}
			WindowEnumerationHelper.WindowStyles style = (WindowEnumerationHelper.WindowStyles)WindowEnumerationHelper.GetWindowLongPtr(hwnd, -16).ToInt64();
			if (style.HasFlag(WindowEnumerationHelper.WindowStyles.WS_DISABLED))
			{
				return false;
			}
			bool cloaked = false;
			return WindowEnumerationHelper.DwmGetWindowAttribute(hwnd, WindowEnumerationHelper.DWMWINDOWATTRIBUTE.Cloaked, out cloaked, Marshal.SizeOf<bool>()) != 0 || !cloaked;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000056A0 File Offset: 0x000038A0
		public static bool IsWindowValid(IntPtr hwnd)
		{
			if (hwnd.ToInt32() == 0)
			{
				return false;
			}
			if (!WindowEnumerationHelper.IsWindowVisible(hwnd))
			{
				return false;
			}
			WindowEnumerationHelper.WindowStyles style = (WindowEnumerationHelper.WindowStyles)WindowEnumerationHelper.GetWindowLongPtr(hwnd, -16).ToInt64();
			if (style.HasFlag(WindowEnumerationHelper.WindowStyles.WS_DISABLED))
			{
				return false;
			}
			bool cloaked = false;
			return WindowEnumerationHelper.DwmGetWindowAttribute(hwnd, WindowEnumerationHelper.DWMWINDOWATTRIBUTE.Cloaked, out cloaked, Marshal.SizeOf<bool>()) != 0 || !cloaked;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005708 File Offset: 0x00003908
		public static bool IsClocked(IntPtr hwnd)
		{
			bool cloaked = false;
			return WindowEnumerationHelper.DwmGetWindowAttribute(hwnd, WindowEnumerationHelper.DWMWINDOWATTRIBUTE.Cloaked, out cloaked, Marshal.SizeOf<bool>()) == 0 && cloaked;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005730 File Offset: 0x00003930
		public static bool IsDisable(IntPtr hwnd)
		{
			WindowEnumerationHelper.WindowStyles style = (WindowEnumerationHelper.WindowStyles)WindowEnumerationHelper.GetWindowLongPtr(hwnd, -16).ToInt64();
			return style.HasFlag(WindowEnumerationHelper.WindowStyles.WS_DISABLED);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000576C File Offset: 0x0000396C
		public static bool IsChildOrOwner(IntPtr hwnd, IntPtr hwnd1)
		{
			IntPtr root = WindowEnumerationHelper.GetAncestor(hwnd1, WindowEnumerationHelper.GetAncestorFlags.GetRootOwner);
			return root == hwnd && root != IntPtr.Zero;
		}

		// Token: 0x0200006E RID: 110
		private enum GetAncestorFlags
		{
			// Token: 0x0400036B RID: 875
			GetParent = 1,
			// Token: 0x0400036C RID: 876
			GetRoot,
			// Token: 0x0400036D RID: 877
			GetRootOwner
		}

		// Token: 0x0200006F RID: 111
		public enum GWL
		{
			// Token: 0x0400036F RID: 879
			GWL_WNDPROC = -4,
			// Token: 0x04000370 RID: 880
			GWL_HINSTANCE = -6,
			// Token: 0x04000371 RID: 881
			GWL_HWNDPARENT = -8,
			// Token: 0x04000372 RID: 882
			GWL_STYLE = -16,
			// Token: 0x04000373 RID: 883
			GWL_EXSTYLE = -20,
			// Token: 0x04000374 RID: 884
			GWL_USERDATA = -21,
			// Token: 0x04000375 RID: 885
			GWL_ID = -12
		}

		// Token: 0x02000070 RID: 112
		[Flags]
		private enum WindowStyles : uint
		{
			// Token: 0x04000377 RID: 887
			WS_BORDER = 8388608U,
			// Token: 0x04000378 RID: 888
			WS_CAPTION = 12582912U,
			// Token: 0x04000379 RID: 889
			WS_CHILD = 1073741824U,
			// Token: 0x0400037A RID: 890
			WS_CLIPCHILDREN = 33554432U,
			// Token: 0x0400037B RID: 891
			WS_CLIPSIBLINGS = 67108864U,
			// Token: 0x0400037C RID: 892
			WS_DISABLED = 134217728U,
			// Token: 0x0400037D RID: 893
			WS_DLGFRAME = 4194304U,
			// Token: 0x0400037E RID: 894
			WS_GROUP = 131072U,
			// Token: 0x0400037F RID: 895
			WS_HSCROLL = 1048576U,
			// Token: 0x04000380 RID: 896
			WS_MAXIMIZE = 16777216U,
			// Token: 0x04000381 RID: 897
			WS_MAXIMIZEBOX = 65536U,
			// Token: 0x04000382 RID: 898
			WS_MINIMIZE = 536870912U,
			// Token: 0x04000383 RID: 899
			WS_MINIMIZEBOX = 131072U,
			// Token: 0x04000384 RID: 900
			WS_OVERLAPPED = 0U,
			// Token: 0x04000385 RID: 901
			WS_OVERLAPPEDWINDOW = 13565952U,
			// Token: 0x04000386 RID: 902
			WS_POPUP = 2147483648U,
			// Token: 0x04000387 RID: 903
			WS_POPUPWINDOW = 2156396544U,
			// Token: 0x04000388 RID: 904
			WS_SIZEFRAME = 262144U,
			// Token: 0x04000389 RID: 905
			WS_SYSMENU = 524288U,
			// Token: 0x0400038A RID: 906
			WS_TABSTOP = 65536U,
			// Token: 0x0400038B RID: 907
			WS_VISIBLE = 268435456U,
			// Token: 0x0400038C RID: 908
			WS_VSCROLL = 2097152U
		}

		// Token: 0x02000071 RID: 113
		private enum DWMWINDOWATTRIBUTE : uint
		{
			// Token: 0x0400038E RID: 910
			NCRenderingEnabled = 1U,
			// Token: 0x0400038F RID: 911
			NCRenderingPolicy,
			// Token: 0x04000390 RID: 912
			TransitionsForceDisabled,
			// Token: 0x04000391 RID: 913
			AllowNCPaint,
			// Token: 0x04000392 RID: 914
			CaptionButtonBounds,
			// Token: 0x04000393 RID: 915
			NonClientRtlLayout,
			// Token: 0x04000394 RID: 916
			ForceIconicRepresentation,
			// Token: 0x04000395 RID: 917
			Flip3DPolicy,
			// Token: 0x04000396 RID: 918
			ExtendedFrameBounds,
			// Token: 0x04000397 RID: 919
			HasIconicBitmap,
			// Token: 0x04000398 RID: 920
			DisallowPeek,
			// Token: 0x04000399 RID: 921
			ExcludedFromPeek,
			// Token: 0x0400039A RID: 922
			Cloak,
			// Token: 0x0400039B RID: 923
			Cloaked,
			// Token: 0x0400039C RID: 924
			FreezeRepresentation
		}
	}
}
