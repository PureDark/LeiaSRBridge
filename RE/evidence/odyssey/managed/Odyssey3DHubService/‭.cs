using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000067 RID: 103
	[NullableContext(1)]
	[Nullable(0)]
	public class \u202D
	{
		// Token: 0x060001A3 RID: 419
		[NullableContext(2)]
		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		public static extern IntPtr \u00A0(string, string);

		// Token: 0x060001A4 RID: 420
		[DllImport("user32.dll", EntryPoint = "PostMessage")]
		public static extern bool \u00A0(IntPtr, uint, IntPtr, IntPtr);

		// Token: 0x060001A5 RID: 421
		[DllImport("user32.dll", EntryPoint = "ShowWindow")]
		public static extern bool \u00A0(IntPtr, int);

		// Token: 0x060001A6 RID: 422
		[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
		public static extern bool \u00A0(IntPtr);

		// Token: 0x060001A7 RID: 423
		[DllImport("user32.dll", EntryPoint = "IsIconic")]
		public static extern bool \u1680(IntPtr);

		// Token: 0x060001A8 RID: 424
		[DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
		public static extern int \u00A0(IntPtr, int, uint);

		// Token: 0x060001A9 RID: 425
		[DllImport("user32.dll", EntryPoint = "BringWindowToTop")]
		public static extern bool \u2000(IntPtr);

		// Token: 0x060001AA RID: 426
		[DllImport("user32.dll", EntryPoint = "RegisterHotKey")]
		public static extern bool \u00A0(IntPtr, int, int, int);

		// Token: 0x060001AB RID: 427
		[DllImport("user32.dll", EntryPoint = "UnregisterHotKey")]
		public static extern bool \u1680(IntPtr, int);

		// Token: 0x060001AC RID: 428
		[DllImport("user32.dll", EntryPoint = "SetWinEventHook")]
		public static extern IntPtr \u00A0(uint, uint, IntPtr, \u202D.\u00A0, uint, uint, uint);

		// Token: 0x060001AD RID: 429
		[DllImport("user32.dll", EntryPoint = "GetShellWindow")]
		public static extern IntPtr \u00A0();

		// Token: 0x060001AE RID: 430
		[DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
		public static extern IntPtr \u1680();

		// Token: 0x060001AF RID: 431
		[DllImport("user32.dll", EntryPoint = "UnhookWinEvent")]
		public static extern bool \u2001(IntPtr);

		// Token: 0x060001B0 RID: 432
		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern bool \u00A0(IntPtr, IntPtr, int, int, int, int, uint);

		// Token: 0x060001B1 RID: 433
		[DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
		public static extern IntPtr \u2000();

		// Token: 0x060001B2 RID: 434
		[DllImport("user32.dll", CharSet = 4, EntryPoint = "GetWindowText", SetLastError = true)]
		public static extern int \u00A0(IntPtr, StringBuilder, int);

		// Token: 0x060001B3 RID: 435
		[DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
		public static extern int \u2000(IntPtr, int);

		// Token: 0x060001B4 RID: 436
		[DllImport("user32.dll", EntryPoint = "GetWindowRect")]
		[return: MarshalAs(2)]
		public static extern bool \u00A0(IntPtr, out \u2027.\u00A0);

		// Token: 0x060001B5 RID: 437
		[DllImport("user32.dll", EntryPoint = "GetWindow", SetLastError = true)]
		private static extern IntPtr \u00A0(IntPtr, uint);

		// Token: 0x060001B6 RID: 438
		[DllImport("user32.dll", CharSet = 3, EntryPoint = "GetWindowTextLength")]
		public static extern int \u2002(IntPtr);

		// Token: 0x060001B7 RID: 439
		[DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
		public static extern bool \u2003(IntPtr);

		// Token: 0x060001B8 RID: 440
		[DllImport("user32.dll", EntryPoint = "IsWindow", SetLastError = true)]
		public static extern bool \u2004(IntPtr);

		// Token: 0x060001B9 RID: 441
		[DllImport("user32.dll", EntryPoint = "EnumWindows", SetLastError = true)]
		public static extern bool \u00A0(\u202D.\u1680, IntPtr);

		// Token: 0x060001BA RID: 442
		[DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
		public static extern uint \u00A0(IntPtr, out uint);

		// Token: 0x060001BB RID: 443 RVA: 0x00007321 File Offset: 0x00005521
		public static IntPtr \u00A0(string A_0)
		{
			return \u202D.\u00A0(null, A_0);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000732A File Offset: 0x0000552A
		public static IntPtr \u2001()
		{
			return \u202D.\u00A0(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u200A());
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007338 File Offset: 0x00005538
		public static bool \u2002()
		{
			IntPtr intPtr = \u202D.\u2001();
			return intPtr != IntPtr.Zero && \u202D.\u2003(intPtr);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007360 File Offset: 0x00005560
		public static string \u2005(IntPtr A_0)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			if (\u202D.\u00A0(A_0, stringBuilder, 512) > 0)
			{
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007393 File Offset: 0x00005593
		public static void \u2006(IntPtr A_0)
		{
			\u202D.\u2000(A_0, \u202D.\u1680);
			\u202D.\u00A0(A_0, \u202D.\u2000);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000073AD File Offset: 0x000055AD
		public static IntPtr \u00A0(int? A_0)
		{
			\u202D.\u2000 u = new \u202D.\u2000();
			u.\u00A0 = A_0;
			u.\u00A0 = IntPtr.Zero;
			\u202D.\u00A0(new \u202D.\u1680(u.\u00A0), IntPtr.Zero);
			return u.\u00A0;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000073E2 File Offset: 0x000055E2
		public static void \u2007(IntPtr A_0)
		{
			\u202D.\u00A0(A_0, \u202D.\u2001);
			\u202D.\u00A0(A_0);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000073F7 File Offset: 0x000055F7
		public static void \u2008(IntPtr A_0)
		{
			\u202D.\u00A0(A_0, \u202D.\u2002);
			\u202D.\u00A0(A_0);
		}

		// Token: 0x0400020C RID: 524
		public static readonly uint \u00A0 = 128U;

		// Token: 0x0400020D RID: 525
		public static readonly uint \u1680 = 8U;

		// Token: 0x0400020E RID: 526
		public static readonly uint \u2000 = 134217728U;

		// Token: 0x0400020F RID: 527
		public static readonly uint \u2001 = 2147483648U;

		// Token: 0x04000210 RID: 528
		public static readonly uint \u2002 = 8388608U;

		// Token: 0x04000211 RID: 529
		public static readonly uint \u2003 = 4194304U;

		// Token: 0x04000212 RID: 530
		public static readonly int \u00A0 = -16;

		// Token: 0x04000213 RID: 531
		public static readonly int \u1680 = -20;

		// Token: 0x04000214 RID: 532
		public static readonly int \u2000 = 0;

		// Token: 0x04000215 RID: 533
		public static readonly int \u2001 = 5;

		// Token: 0x04000216 RID: 534
		public static readonly int \u2002 = 9;

		// Token: 0x04000217 RID: 535
		public static readonly IntPtr \u00A0 = new IntPtr(0);

		// Token: 0x04000218 RID: 536
		public static readonly IntPtr \u1680 = new IntPtr(-1);

		// Token: 0x04000219 RID: 537
		public static readonly IntPtr \u2000 = new IntPtr(-2);

		// Token: 0x0400021A RID: 538
		public static readonly uint \u2004 = 2U;

		// Token: 0x0400021B RID: 539
		public static readonly uint \u2005 = 1U;

		// Token: 0x0400021C RID: 540
		public static readonly uint \u2006 = 4U;

		// Token: 0x0400021D RID: 541
		public static readonly uint \u2007 = 16U;

		// Token: 0x0400021E RID: 542
		public static readonly uint \u2008 = 64U;

		// Token: 0x0400021F RID: 543
		public static readonly uint \u2009 = \u202D.\u2005 | \u202D.\u2004;

		// Token: 0x02000068 RID: 104
		// (Invoke) Token: 0x060001C6 RID: 454
		[NullableContext(0)]
		public delegate void \u00A0(IntPtr, uint, IntPtr, int, int, uint, uint);

		// Token: 0x02000069 RID: 105
		// (Invoke) Token: 0x060001CA RID: 458
		[NullableContext(0)]
		public delegate bool \u1680(IntPtr, IntPtr);

		// Token: 0x0200006A RID: 106
		[CompilerGenerated]
		private sealed class \u2000
		{
			// Token: 0x060001CE RID: 462 RVA: 0x000074C4 File Offset: 0x000056C4
			internal bool \u00A0(IntPtr A_1, IntPtr A_2)
			{
				uint num;
				\u202D.\u00A0(A_1, out num);
				long num2 = (long)((ulong)num);
				int? u00A = this.\u00A0;
				long? num3 = (u00A != null) ? new long?((long)u00A.GetValueOrDefault()) : default(long?);
				if ((num2 == num3.GetValueOrDefault() & num3 != null) && \u202D.\u2003(A_1))
				{
					this.\u00A0 = A_1;
					return false;
				}
				return true;
			}

			// Token: 0x04000220 RID: 544
			public int? \u00A0;

			// Token: 0x04000221 RID: 545
			public IntPtr \u00A0;
		}
	}
}
