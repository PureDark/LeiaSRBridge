using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000038 RID: 56
	public class \u2027
	{
		// Token: 0x0600014B RID: 331
		[DllImport("User32", EntryPoint = "MonitorFromWindow")]
		public static extern IntPtr \u00A0(IntPtr, int);

		// Token: 0x0600014C RID: 332
		[DllImport("user32", CharSet = 4, EntryPoint = "GetMonitorInfo", SetLastError = true)]
		public static extern bool \u00A0(IntPtr, ref \u2027.\u1680);

		// Token: 0x0600014D RID: 333
		[DllImport("user32.dll", EntryPoint = "GetDC")]
		private static extern IntPtr \u00A0(IntPtr);

		// Token: 0x0600014E RID: 334
		[DllImport("user32.dll", EntryPoint = "ReleaseDC")]
		private static extern int \u00A0(IntPtr, IntPtr);

		// Token: 0x0600014F RID: 335
		[DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
		private static extern int \u1680(IntPtr, int);

		// Token: 0x06000150 RID: 336 RVA: 0x00006132 File Offset: 0x00004332
		[NullableContext(1)]
		private static bool \u00A0(string A_0)
		{
			A_0 = A_0.ToLower();
			if (A_0.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2035()) && A_0.Length >= 2)
			{
				string text = A_0;
				if (text.get_Chars(text.Length - 2) == 'x')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000616C File Offset: 0x0000436C
		public static bool \u00A0()
		{
			if (Screen.AllScreens.Length == 1)
			{
				return true;
			}
			Screen primaryScreen = Screen.PrimaryScreen;
			string text = ((primaryScreen != null) ? primaryScreen.\u00A0() : null) ?? string.Empty;
			return !string.IsNullOrEmpty(text) && \u2027.\u00A0(text);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000061B4 File Offset: 0x000043B4
		public static bool \u1680()
		{
			\u2028.\u2007 u = default(\u2028.\u2007);
			u.\u00A0 = Marshal.SizeOf<\u2028.\u2007>(u);
			uint num = 0U;
			while (\u2028.\u00A0(null, num, ref u, 0U))
			{
				if ((u.\u1680 & \u2027.\u2000) != 0)
				{
					\u2028.\u2006 u2 = default(\u2028.\u2006);
					u2.\u2000 = (short)Marshal.SizeOf<\u2028.\u2006>(u2);
					if (\u2028.\u00A0(u.\u00A0, -1, ref u2) && (double)u2.\u2003 == \u2006.\u00A0.Width && (double)u2.\u2004 == \u2006.\u00A0.Height)
					{
						return true;
					}
				}
				num += 1U;
			}
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006248 File Offset: 0x00004448
		public static bool \u2000()
		{
			int num = 0;
			if (Screen.AllScreens.Length > 1)
			{
				Screen[] allScreens = Screen.AllScreens;
				for (int i = 0; i < allScreens.Length; i++)
				{
					string text = allScreens[i].\u00A0();
					if (!string.IsNullOrEmpty(text) && \u2027.\u00A0(text))
					{
						num++;
					}
				}
			}
			return num > 1;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000629C File Offset: 0x0000449C
		public static double \u2001()
		{
			IntPtr intPtr = \u202D.\u1680();
			IntPtr intPtr2 = \u2027.\u00A0(intPtr);
			double num = (double)\u2027.\u1680(intPtr2, \u2027.\u2001);
			\u2027.\u00A0(intPtr, intPtr2);
			return num / 96.0;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000062D4 File Offset: 0x000044D4
		public static double \u2002()
		{
			IntPtr intPtr = \u202D.\u1680();
			IntPtr intPtr2 = \u2027.\u00A0(intPtr);
			double num = (double)\u2027.\u1680(intPtr2, \u2027.\u2002);
			\u2027.\u00A0(intPtr, intPtr2);
			return num / 96.0;
		}

		// Token: 0x0400011F RID: 287
		public static readonly int \u00A0 = 1;

		// Token: 0x04000120 RID: 288
		public static readonly int \u1680 = 2;

		// Token: 0x04000121 RID: 289
		public static readonly int \u2000 = 4;

		// Token: 0x04000122 RID: 290
		public static readonly int \u2001 = 88;

		// Token: 0x04000123 RID: 291
		public static readonly int \u2002 = 90;

		// Token: 0x02000039 RID: 57
		public struct \u00A0
		{
			// Token: 0x04000124 RID: 292
			public int \u00A0;

			// Token: 0x04000125 RID: 293
			public int \u1680;

			// Token: 0x04000126 RID: 294
			public int \u2000;

			// Token: 0x04000127 RID: 295
			public int \u2001;
		}

		// Token: 0x0200003A RID: 58
		[StructLayout(0, CharSet = 4)]
		public struct \u1680
		{
			// Token: 0x04000128 RID: 296
			public int \u00A0;

			// Token: 0x04000129 RID: 297
			public \u2027.\u00A0 \u00A0;

			// Token: 0x0400012A RID: 298
			public \u2027.\u00A0 \u1680;

			// Token: 0x0400012B RID: 299
			public uint \u00A0;

			// Token: 0x0400012C RID: 300
			[Nullable(1)]
			[MarshalAs(23, SizeConst = 32)]
			public string \u00A0;
		}
	}
}
