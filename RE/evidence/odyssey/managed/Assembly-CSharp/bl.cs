using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x0200002D RID: 45
public class bl
{
	// Token: 0x060000E8 RID: 232 RVA: 0x00006BEF File Offset: 0x00004DEF
	public static void jd()
	{
		bl.ShowWindow(bl.caj, 8);
		bl.ShowWindow(bl.caj, 3);
		bl.SetForegroundWindow(bl.cak);
		Application.runInBackground = true;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00006C1C File Offset: 0x00004E1C
	public static void ji(string a, string b)
	{
		Process.Start(new ProcessStartInfo
		{
			FileName = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mj(),
			Arguments = string.Concat(new string[]
			{
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mk(),
				a,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ml(),
				b,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mm()
			}),
			CreateNoWindow = true,
			UseShellExecute = false
		}).WaitForExit();
		Console.WriteLine(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mn() + a + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mo() + b);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00006C9C File Offset: 0x00004E9C
	public static bl.bg dgr(IntPtr a)
	{
		bl.bg result;
		bl.GetWindowRect(a, out result);
		return result;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00006CB4 File Offset: 0x00004EB4
	public static void koz(string a, string b)
	{
		Process.Start(new ProcessStartInfo
		{
			FileName = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mp(),
			Arguments = string.Concat(new string[]
			{
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mq(),
				a,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mr(),
				b,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ms()
			}),
			CreateNoWindow = true,
			UseShellExecute = false
		}).WaitForExit();
		Console.WriteLine(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mt() + a + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mu() + b);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00006D32 File Offset: 0x00004F32
	public static bool khl()
	{
		return bl.caj != IntPtr.Zero && bl.IsWindowVisible(bl.caj);
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000ED RID: 237 RVA: 0x00006D54 File Offset: 0x00004F54
	// (set) Token: 0x06000107 RID: 263 RVA: 0x00007095 File Offset: 0x00005295
	public static bool cym
	{
		get
		{
			return bl.can;
		}
		set
		{
			bl.can = value;
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00006D5B File Offset: 0x00004F5B
	public static void jrx()
	{
		bl.BringWindowToTop(bl.caj);
		bl.SetWindowPos(bl.caj, bl.bzz, 0, 0, 0, 0, 4294967254U);
		bl.SetForegroundWindow(bl.caj);
	}

	// Token: 0x060000EF RID: 239
	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	// Token: 0x060000F0 RID: 240
	[DllImport("user32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	// Token: 0x060000F1 RID: 241 RVA: 0x00006D89 File Offset: 0x00004F89
	public static void nqo()
	{
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 6);
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00006DA0 File Offset: 0x00004FA0
	public static void knd()
	{
		App.stopwatch.Stop();
		bb.hk(z.bxg, ba.bxv, ((long)App.stopwatch.Elapsed.TotalSeconds).ToString());
		bl.SetForegroundWindow(bl.caj);
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 0);
		LeiaPluginHelper.DestroyLeiaModules();
		bl.cym = true;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00006BEF File Offset: 0x00004DEF
	public static void khp()
	{
		bl.ShowWindow(bl.caj, 8);
		bl.ShowWindow(bl.caj, 3);
		bl.SetForegroundWindow(bl.cak);
		Application.runInBackground = true;
	}

	// Token: 0x060000F4 RID: 244
	[DllImport("user32.dll")]
	public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	// Token: 0x060000F5 RID: 245 RVA: 0x00006E04 File Offset: 0x00005004
	public static string iz(IntPtr a)
	{
		StringBuilder stringBuilder = new StringBuilder(512);
		if (bl.GetWindowText(a, stringBuilder, 512) > 0)
		{
			return stringBuilder.ToString();
		}
		return string.Empty;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00006E37 File Offset: 0x00005037
	public static void jf()
	{
		bl.BringWindowToTop(bl.caj);
		bl.SetWindowPos(bl.caj, bl.bzz, 0, 0, 0, 0, 67U);
		bl.SetForegroundWindow(bl.caj);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00006E68 File Offset: 0x00005068
	public static bl.bg kxy(IntPtr a)
	{
		bl.bg result;
		bl.GetWindowRect(a, out result);
		return result;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00006D89 File Offset: 0x00004F89
	public static void osq()
	{
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 6);
	}

	// Token: 0x060000FA RID: 250
	[DllImport("user32.dll")]
	public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	// Token: 0x060000FB RID: 251 RVA: 0x00006E80 File Offset: 0x00005080
	public static bool jk(IntPtr a)
	{
		if (a != IntPtr.Zero)
		{
			IntPtr foregroundWindow = bl.GetForegroundWindow();
			return a == foregroundWindow && bl.IsWindowVisible(a);
		}
		return false;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00006EB4 File Offset: 0x000050B4
	private static IntPtr ix()
	{
		bl.bj bj = new bl.bj();
		bj.bzo = IntPtr.Zero;
		bl.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, new bl.bi(bj.ir), IntPtr.Zero);
		return bj.bzo;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00006EFC File Offset: 0x000050FC
	private static IntPtr mfs()
	{
		bl.bj bj = new bl.bj();
		bj.bzo = IntPtr.Zero;
		bl.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, new bl.bi(bj.ir), IntPtr.Zero);
		return bj.bzo;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00006F44 File Offset: 0x00005144
	private static bool iw(IntPtr a, IntPtr b)
	{
		bl.bg bg;
		return bl.GetWindowRect(a, out bg) && bl.MonitorFromRect(ref bg, 0U) == b;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006F6C File Offset: 0x0000516C
	public static bl.bg nam(IntPtr a)
	{
		bl.bg result;
		bl.GetWindowRect(a, out result);
		return result;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00006F83 File Offset: 0x00005183
	public static void egd()
	{
		bl.BringWindowToTop(bl.caj);
		bl.SetWindowPos(bl.caj, bl.bzz, 0, 0, 0, 0, 10U);
		bl.SetForegroundWindow(bl.caj);
	}

	// Token: 0x06000101 RID: 257
	[DllImport("user32.dll", SetLastError = true)]
	public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

	// Token: 0x06000102 RID: 258
	[DllImport("user32.dll")]
	[return: NativeInteger]
	public static extern IntPtr GetForegroundWindow();

	// Token: 0x06000103 RID: 259 RVA: 0x00006FB4 File Offset: 0x000051B4
	public static bool jl()
	{
		bl.bg bg;
		bl.bh bh;
		return !(bl.caj == IntPtr.Zero) && bl.IsWindowVisible(bl.caj) && bl.GetWindowRect(bl.caj, out bg) && bl.GetCursorPos(out bh) && (bh.bzm >= bg.bzi && bh.bzm < bg.bzk && bh.bzn >= bg.bzj) && bh.bzn < bg.bzl;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00007034 File Offset: 0x00005234
	public static void jc()
	{
		App.stopwatch.Stop();
		bb.hk(z.bxg, ba.bxv, ((long)App.stopwatch.Elapsed.TotalSeconds).ToString());
		bl.SetForegroundWindow(bl.caj);
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 0);
		LeiaPluginHelper.DestroyLeiaModules();
		bl.cym = true;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006D32 File Offset: 0x00004F32
	public static bool jg()
	{
		return bl.caj != IntPtr.Zero && bl.IsWindowVisible(bl.caj);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006D89 File Offset: 0x00004F89
	public static void je()
	{
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 6);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000070A0 File Offset: 0x000052A0
	public static bool jh()
	{
		foreach (Process process in Process.GetProcessesByName(bl.cai))
		{
			try
			{
				bl.bk bk = new bl.bk();
				if (process.MainWindowHandle != IntPtr.Zero && bl.IsWindowVisible(process.MainWindowHandle))
				{
					return true;
				}
				bk.bzp = process.Id;
				bk.bzq = false;
				bl.EnumWindows(new bl.be(bk.@is), IntPtr.Zero);
				if (bk.bzq)
				{
					return true;
				}
			}
			catch
			{
			}
		}
		return false;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00007148 File Offset: 0x00005348
	public static IntPtr gtq()
	{
		IntPtr intPtr = bl.ix();
		if (intPtr == IntPtr.Zero)
		{
			return IntPtr.Zero;
		}
		IntPtr intPtr2 = bl.GetTopWindow(IntPtr.Zero);
		while (intPtr2 != IntPtr.Zero)
		{
			if (bl.IsWindowVisible(intPtr2) && bl.iw(intPtr2, intPtr) && !string.IsNullOrEmpty(bl.iz(intPtr2)))
			{
				return intPtr2;
			}
			intPtr2 = bl.GetWindow(intPtr2, 2);
		}
		return IntPtr.Zero;
	}

	// Token: 0x0600010A RID: 266
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

	// Token: 0x0600010C RID: 268 RVA: 0x00007218 File Offset: 0x00005418
	private static IntPtr jdm()
	{
		bl.bj bj = new bl.bj();
		bj.bzo = IntPtr.Zero;
		bl.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, new bl.bi(bj.ir), IntPtr.Zero);
		return bj.bzo;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000725D File Offset: 0x0000545D
	public static void jj(IntPtr a)
	{
		bl.ShowWindow(a, 9);
		bl.SetForegroundWindow(a);
	}

	// Token: 0x0600010E RID: 270
	[DllImport("user32.dll")]
	public static extern bool BringWindowToTop(IntPtr hWnd);

	// Token: 0x0600010F RID: 271 RVA: 0x00007270 File Offset: 0x00005470
	public static bl.bg ebd(IntPtr a)
	{
		bl.bg result;
		bl.GetWindowRect(a, out result);
		return result;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00007288 File Offset: 0x00005488
	private static IntPtr lda()
	{
		bl.bj bj = new bl.bj();
		bj.bzo = IntPtr.Zero;
		bl.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, new bl.bi(bj.ir), IntPtr.Zero);
		return bj.bzo;
	}

	// Token: 0x06000111 RID: 273
	[DllImport("user32.dll")]
	private static extern bool GetCursorPos(out bl.bh lpPoint);

	// Token: 0x06000112 RID: 274
	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr GetActiveWindow();

	// Token: 0x06000113 RID: 275 RVA: 0x000072D0 File Offset: 0x000054D0
	public static void dam()
	{
		App.stopwatch.Stop();
		bb.hk(z.bxg, ba.bxv, ((long)App.stopwatch.Elapsed.TotalSeconds).ToString());
		bl.SetForegroundWindow(bl.caj);
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 0);
		LeiaPluginHelper.DestroyLeiaModules();
		bl.cym = true;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00007331 File Offset: 0x00005531
	public static void jb()
	{
		bl.GetWindowLong(bl.caj, -20);
		bl.ShowWindow(bl.caj, 9);
		LeiaPluginHelper.StartLeiaModules();
		bl.jf();
	}

	// Token: 0x06000115 RID: 277
	[DllImport("user32.dll")]
	public static extern bool IsWindowVisible([NativeInteger] IntPtr hWnd);

	// Token: 0x06000116 RID: 278
	[DllImport("user32.dll")]
	private static extern IntPtr GetTopWindow(IntPtr hWnd);

	// Token: 0x06000117 RID: 279 RVA: 0x00007358 File Offset: 0x00005558
	public static IntPtr iy()
	{
		IntPtr intPtr = bl.ix();
		if (intPtr == IntPtr.Zero)
		{
			return IntPtr.Zero;
		}
		IntPtr intPtr2 = bl.GetTopWindow(IntPtr.Zero);
		while (intPtr2 != IntPtr.Zero)
		{
			if (bl.IsWindowVisible(intPtr2) && bl.iw(intPtr2, intPtr) && !string.IsNullOrEmpty(bl.iz(intPtr2)))
			{
				return intPtr2;
			}
			intPtr2 = bl.GetWindow(intPtr2, 2);
		}
		return IntPtr.Zero;
	}

	// Token: 0x06000118 RID: 280
	[DllImport("user32.dll")]
	private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, bl.bi lpfnEnum, IntPtr dwData);

	// Token: 0x06000119 RID: 281 RVA: 0x00006BEF File Offset: 0x00004DEF
	public static void gur()
	{
		bl.ShowWindow(bl.caj, 8);
		bl.ShowWindow(bl.caj, 3);
		bl.SetForegroundWindow(bl.cak);
		Application.runInBackground = true;
	}

	// Token: 0x0600011A RID: 282
	[DllImport("user32.dll")]
	private static extern bool GetWindowRect(IntPtr hWnd, out bl.bg lpRect);

	// Token: 0x0600011B RID: 283
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern bool GetMonitorInfo(IntPtr hMonitor, ref bl.bf lpmi);

	// Token: 0x0600011C RID: 284
	[DllImport("user32.dll")]
	private static extern IntPtr MonitorFromRect([In] ref bl.bg lprc, uint dwFlags);

	// Token: 0x0600011D RID: 285
	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetWindowText([NativeInteger] IntPtr hWnd, StringBuilder text, int count);

	// Token: 0x0600011E RID: 286 RVA: 0x00006D89 File Offset: 0x00004F89
	public static void ljk()
	{
		Application.runInBackground = false;
		bl.ShowWindow(bl.caj, 6);
	}

	// Token: 0x0600011F RID: 287
	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	// Token: 0x06000120 RID: 288 RVA: 0x000073C5 File Offset: 0x000055C5
	public static void mkk(IntPtr a)
	{
		bl.ShowWindow(a, -41);
		bl.SetForegroundWindow(a);
	}

	// Token: 0x06000121 RID: 289
	[DllImport("user32.dll")]
	public static extern IntPtr GetWindow(IntPtr hWnd, int nCmdShow);

	// Token: 0x06000122 RID: 290 RVA: 0x00006D32 File Offset: 0x00004F32
	public static bool nbc()
	{
		return bl.caj != IntPtr.Zero && bl.IsWindowVisible(bl.caj);
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000073D8 File Offset: 0x000055D8
	public static bl.bg ja(IntPtr a)
	{
		bl.bg result;
		bl.GetWindowRect(a, out result);
		return result;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000073EF File Offset: 0x000055EF
	public static void noc(IntPtr a)
	{
		bl.ShowWindow(a, -37);
		bl.SetForegroundWindow(a);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00007404 File Offset: 0x00005604
	public static bool fna()
	{
		foreach (Process process in Process.GetProcessesByName(bl.cai))
		{
			try
			{
				bl.bk bk = new bl.bk();
				if (process.MainWindowHandle != IntPtr.Zero && bl.IsWindowVisible(process.MainWindowHandle))
				{
					return true;
				}
				bk.bzp = process.Id;
				bk.bzq = false;
				bl.EnumWindows(new bl.be(bk.@is), IntPtr.Zero);
				if (bk.bzq)
				{
					return true;
				}
			}
			catch
			{
			}
		}
		return false;
	}

	// Token: 0x06000126 RID: 294
	[DllImport("user32.dll")]
	private static extern bool EnumWindows(bl.be lpEnumFunc, IntPtr lParam);

	// Token: 0x06000127 RID: 295 RVA: 0x000074AC File Offset: 0x000056AC
	public static void ewz()
	{
		bl.BringWindowToTop(bl.caj);
		bl.SetWindowPos(bl.caj, bl.bzz, 0, 0, 0, 0, 4294967290U);
		bl.SetForegroundWindow(bl.caj);
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00006D32 File Offset: 0x00004F32
	public static bool fvc()
	{
		return bl.caj != IntPtr.Zero && bl.IsWindowVisible(bl.caj);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000074DA File Offset: 0x000056DA
	public static void bnv(IntPtr a)
	{
		bl.ShowWindow(a, -67);
		bl.SetForegroundWindow(a);
	}

	// Token: 0x040000DE RID: 222
	private const string bzr = "WindowsHelper";

	// Token: 0x040000DF RID: 223
	private const int bzs = -20;

	// Token: 0x040000E0 RID: 224
	private const int bzt = 0;

	// Token: 0x040000E1 RID: 225
	private const int bzu = 5;

	// Token: 0x040000E2 RID: 226
	private const int bzv = 9;

	// Token: 0x040000E3 RID: 227
	private const int bzw = 6;

	// Token: 0x040000E4 RID: 228
	private const int bzx = 3;

	// Token: 0x040000E5 RID: 229
	private const int bzy = 8;

	// Token: 0x040000E6 RID: 230
	private static readonly IntPtr bzz = new IntPtr(0);

	// Token: 0x040000E7 RID: 231
	private static readonly IntPtr caa = new IntPtr(-1);

	// Token: 0x040000E8 RID: 232
	private static readonly IntPtr cab = new IntPtr(-2);

	// Token: 0x040000E9 RID: 233
	private const uint cac = 2U;

	// Token: 0x040000EA RID: 234
	private const uint cad = 1U;

	// Token: 0x040000EB RID: 235
	private const uint cae = 64U;

	// Token: 0x040000EC RID: 236
	private const uint caf = 16U;

	// Token: 0x040000ED RID: 237
	private const uint cag = 3U;

	// Token: 0x040000EE RID: 238
	private static string cah = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mv();

	// Token: 0x040000EF RID: 239
	private static string cai = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mw();

	// Token: 0x040000F0 RID: 240
	public static IntPtr caj = bl.FindWindow(null, bl.cah);

	// Token: 0x040000F1 RID: 241
	public static IntPtr cak = IntPtr.Zero;

	// Token: 0x040000F2 RID: 242
	private const uint cal = 0U;

	// Token: 0x040000F3 RID: 243
	private const uint cam = 1U;

	// Token: 0x040000F4 RID: 244
	private static bool can;

	// Token: 0x0200002E RID: 46
	// (Invoke) Token: 0x0600012B RID: 299
	private delegate bool be(IntPtr hWnd, IntPtr lParam);

	// Token: 0x0200002F RID: 47
	public struct bf
	{
		// Token: 0x040000F5 RID: 245
		public uint bze;

		// Token: 0x040000F6 RID: 246
		public bl.bg bzf;

		// Token: 0x040000F7 RID: 247
		public bl.bg bzg;

		// Token: 0x040000F8 RID: 248
		public uint bzh;
	}

	// Token: 0x02000030 RID: 48
	public struct bg
	{
		// Token: 0x040000F9 RID: 249
		public int bzi;

		// Token: 0x040000FA RID: 250
		public int bzj;

		// Token: 0x040000FB RID: 251
		public int bzk;

		// Token: 0x040000FC RID: 252
		public int bzl;
	}

	// Token: 0x02000031 RID: 49
	private struct bh
	{
		// Token: 0x040000FD RID: 253
		public int bzm;

		// Token: 0x040000FE RID: 254
		public int bzn;
	}

	// Token: 0x02000032 RID: 50
	// (Invoke) Token: 0x0600012F RID: 303
	private delegate bool bi(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData);

	// Token: 0x02000033 RID: 51
	[CompilerGenerated]
	private sealed class bj
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000074EC File Offset: 0x000056EC
		internal bool ir(IntPtr a, IntPtr b, IntPtr c, IntPtr d)
		{
			bl.bf bf = default(bl.bf);
			bf.bze = (uint)Marshal.SizeOf(typeof(bl.bf));
			if (bl.GetMonitorInfo(a, ref bf) && (bf.bzh & 1U) != 0U)
			{
				this.bzo = a;
				return false;
			}
			return true;
		}

		// Token: 0x040000FF RID: 255
		public IntPtr bzo;
	}

	// Token: 0x02000034 RID: 52
	[CompilerGenerated]
	private sealed class bk
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00007538 File Offset: 0x00005738
		internal bool efp(IntPtr a, IntPtr b)
		{
			int num;
			bl.GetWindowThreadProcessId(a, out num);
			if (num == this.bzp && bl.IsWindowVisible(a))
			{
				this.bzq = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000756C File Offset: 0x0000576C
		internal bool bng(IntPtr a, IntPtr b)
		{
			int num;
			bl.GetWindowThreadProcessId(a, out num);
			if (num == this.bzp && bl.IsWindowVisible(a))
			{
				this.bzq = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000075A0 File Offset: 0x000057A0
		internal bool @is(IntPtr a, IntPtr b)
		{
			int num;
			bl.GetWindowThreadProcessId(a, out num);
			if (num == this.bzp && bl.IsWindowVisible(a))
			{
				this.bzq = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000075D4 File Offset: 0x000057D4
		internal bool iya(IntPtr a, IntPtr b)
		{
			int num;
			bl.GetWindowThreadProcessId(a, out num);
			if (num == this.bzp && bl.IsWindowVisible(a))
			{
				this.bzq = true;
				return false;
			}
			return true;
		}

		// Token: 0x04000100 RID: 256
		public int bzp;

		// Token: 0x04000101 RID: 257
		public bool bzq;
	}
}
