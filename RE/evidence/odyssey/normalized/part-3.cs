// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\‧.cs
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace obf_12
{
	// Token: 0x02000038 RID: 56
	public class obf_13
	{
		// Token: 0x0600014B RID: 331
		[DllImport("User32", EntryPoint = "MonitorFromWindow")]
		public static extern IntPtr obf_12(IntPtr, int);

		// Token: 0x0600014C RID: 332
		[DllImport("user32", CharSet = 4, EntryPoint = "GetMonitorInfo", SetLastError = true)]
		public static extern bool obf_12(IntPtr, ref obf_13.obf_14);

		// Token: 0x0600014D RID: 333
		[DllImport("user32.dll", EntryPoint = "GetDC")]
		private static extern IntPtr obf_12(IntPtr);

		// Token: 0x0600014E RID: 334
		[DllImport("user32.dll", EntryPoint = "ReleaseDC")]
		private static extern int obf_12(IntPtr, IntPtr);

		// Token: 0x0600014F RID: 335
		[DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
		private static extern int obf_14(IntPtr, int);

		// Token: 0x06000150 RID: 336 RVA: 0x00006132 File Offset: 0x00004332
		[NullableContext(1)]
		private static bool obf_12(string A_0)
		{
			A_0 = A_0.ToLower();
			if (A_0.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_15()) && A_0.Length >= 2)
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
		public static bool obf_12()
		{
			if (Screen.AllScreens.Length == 1)
			{
				return true;
			}
			Screen primaryScreen = Screen.PrimaryScreen;
			string text = ((primaryScreen != null) ? primaryScreen.obf_12() : null) ?? string.Empty;
			return !string.IsNullOrEmpty(text) && obf_13.obf_12(text);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000061B4 File Offset: 0x000043B4
		public static bool obf_14()
		{
			obf_16.obf_17 u = default(obf_16.obf_17);
			u.obf_12 = Marshal.SizeOf<obf_16.obf_17>(u);
			uint num = 0U;
			while (obf_16.obf_12(null, num, ref u, 0U))
			{
				if ((u.obf_14 & obf_13.obf_18) != 0)
				{
					obf_16.obf_19 u2 = default(obf_16.obf_19);
					u2.obf_18 = (short)Marshal.SizeOf<obf_16.obf_19>(u2);
					if (obf_16.obf_12(u.obf_12, -1, ref u2) && (double)u2.obf_20 == obf_19.obf_12.Width && (double)u2.obf_21 == obf_19.obf_12.Height)
					{
						return true;
					}
				}
				num += 1U;
			}
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006248 File Offset: 0x00004448
		public static bool obf_18()
		{
			int num = 0;
			if (Screen.AllScreens.Length > 1)
			{
				Screen[] allScreens = Screen.AllScreens;
				for (int i = 0; i < allScreens.Length; i++)
				{
					string text = allScreens[i].obf_12();
					if (!string.IsNullOrEmpty(text) && obf_13.obf_12(text))
					{
						num++;
					}
				}
			}
			return num > 1;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000629C File Offset: 0x0000449C
		public static double obf_22()
		{
			IntPtr intPtr = obf_23.obf_14();
			IntPtr intPtr2 = obf_13.obf_12(intPtr);
			double num = (double)obf_13.obf_14(intPtr2, obf_13.obf_22);
			obf_13.obf_12(intPtr, intPtr2);
			return num / 96.0;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000062D4 File Offset: 0x000044D4
		public static double obf_24()
		{
			IntPtr intPtr = obf_23.obf_14();
			IntPtr intPtr2 = obf_13.obf_12(intPtr);
			double num = (double)obf_13.obf_14(intPtr2, obf_13.obf_24);
			obf_13.obf_12(intPtr, intPtr2);
			return num / 96.0;
		}

		// Token: 0x0400011F RID: 287
		public static readonly int obf_12 = 1;

		// Token: 0x04000120 RID: 288
		public static readonly int obf_14 = 2;

		// Token: 0x04000121 RID: 289
		public static readonly int obf_18 = 4;

		// Token: 0x04000122 RID: 290
		public static readonly int obf_22 = 88;

		// Token: 0x04000123 RID: 291
		public static readonly int obf_24 = 90;

		// Token: 0x02000039 RID: 57
		public struct obf_12
		{
			// Token: 0x04000124 RID: 292
			public int obf_12;

			// Token: 0x04000125 RID: 293
			public int obf_14;

			// Token: 0x04000126 RID: 294
			public int obf_18;

			// Token: 0x04000127 RID: 295
			public int obf_22;
		}

		// Token: 0x0200003A RID: 58
		[StructLayout(0, CharSet = 4)]
		public struct obf_14
		{
			// Token: 0x04000128 RID: 296
			public int obf_12;

			// Token: 0x04000129 RID: 297
			public obf_13.obf_12 obf_12;

			// Token: 0x0400012A RID: 298
			public obf_13.obf_12 obf_14;

			// Token: 0x0400012B RID: 299
			public uint obf_12;

			// Token: 0x0400012C RID: 300
			[Nullable(1)]
			[MarshalAs(23, SizeConst = 32)]
			public string obf_12;
		}
	}
}
