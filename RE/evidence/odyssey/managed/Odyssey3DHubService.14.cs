using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Microsoft.Win32;

namespace \u00A0
{
	// Token: 0x0200003B RID: 59
	public static class \u2028
	{
		// Token: 0x06000158 RID: 344
		[DllImport("user32.dll", EntryPoint = "GetDisplayConfigBufferSizes")]
		public static extern int \u00A0(global::\u00A0.\u2028.\u00A0, out uint, out uint);

		// Token: 0x06000159 RID: 345
		[NullableContext(1)]
		[DllImport("user32.dll", EntryPoint = "QueryDisplayConfig")]
		public static extern int \u00A0(global::\u00A0.\u2028.\u00A0, ref uint, [Out] global::\u00A0.\u2028.\u2010[], ref uint, [Out] global::\u00A0.\u2028.\u2024[], IntPtr);

		// Token: 0x0600015A RID: 346
		[NullableContext(2)]
		[DllImport("user32.dll", CharSet = 3, EntryPoint = "EnumDisplayDevices")]
		public static extern bool \u00A0(string, uint, ref global::\u00A0.\u2028.\u2007, uint);

		// Token: 0x0600015B RID: 347
		[NullableContext(1)]
		[DllImport("user32.dll", EntryPoint = "EnumDisplaySettings")]
		public static extern bool \u00A0(string, int, ref global::\u00A0.\u2028.\u2006);

		// Token: 0x0600015C RID: 348
		[DllImport("user32", EntryPoint = "GetDisplayConfigBufferSizes")]
		private static extern int \u00A0(global::\u00A0.\u2028.\u202E, out int, out int);

		// Token: 0x0600015D RID: 349
		[NullableContext(1)]
		[DllImport("user32", EntryPoint = "QueryDisplayConfig")]
		private static extern int \u00A0(global::\u00A0.\u2028.\u202E, ref int, [In] [Out] global::\u00A0.\u2028.\u2010[], ref int, [In] [Out] global::\u00A0.\u2028.\u2024[], out global::\u00A0.\u2028.\u202A);

		// Token: 0x0600015E RID: 350
		[NullableContext(1)]
		[DllImport("user32", EntryPoint = "QueryDisplayConfig")]
		private static extern int \u00A0(global::\u00A0.\u2028.\u202E, ref int, [In] [Out] global::\u00A0.\u2028.\u2010[], ref int, [In] [Out] global::\u00A0.\u2028.\u2024[], IntPtr);

		// Token: 0x0600015F RID: 351
		[DllImport("user32", EntryPoint = "DisplayConfigGetDeviceInfo")]
		private static extern int \u00A0(ref global::\u00A0.\u2028.\u202F);

		// Token: 0x06000160 RID: 352
		[DllImport("user32", EntryPoint = "DisplayConfigGetDeviceInfo")]
		private static extern int \u00A0(ref global::\u00A0.\u2028.\u2033);

		// Token: 0x06000161 RID: 353
		[DllImport("user32", EntryPoint = "DisplayConfigGetDeviceInfo")]
		private static extern int \u00A0(ref global::\u00A0.\u2028.\u2028);

		// Token: 0x06000162 RID: 354 RVA: 0x00006330 File Offset: 0x00004530
		static \u2028()
		{
			if (!global::\u00A0.\u2028.\u00A0)
			{
				global::\u00A0.\u2028.\u00A0 = true;
				SystemEvents.DisplaySettingsChanged += new EventHandler(global::\u00A0.\u2028.\u203E.\u00A0.\u00A0);
				AppDomain.CurrentDomain.ProcessExit += new EventHandler(global::\u00A0.\u2028.\u203E.\u00A0.\u1680);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000638C File Offset: 0x0000458C
		[NullableContext(1)]
		private static string \u00A0(global::\u00A0.\u2028.\u2008 A_0, uint A_1)
		{
			global::\u00A0.\u2028.\u2028 u = default(global::\u00A0.\u2028.\u2028);
			u.\u00A0.\u00A0 = (uint)Marshal.SizeOf(typeof(global::\u00A0.\u2028.\u2028));
			u.\u00A0.\u00A0 = A_0;
			u.\u00A0.\u1680 = A_1;
			u.\u00A0.\u00A0 = global::\u00A0.\u2028.\u2005.\u1680;
			global::\u00A0.\u2028.\u2028 u2 = u;
			int num = global::\u00A0.\u2028.\u00A0(ref u2);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			return u2.\u00A0;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006400 File Offset: 0x00004600
		[NullableContext(1)]
		private static Dictionary<string, string> \u00A0()
		{
			object u00A = global::\u00A0.\u2028.\u00A0;
			Dictionary<string, string> u00A2;
			lock (u00A)
			{
				if (global::\u00A0.\u2028.\u00A0 != null)
				{
					u00A2 = global::\u00A0.\u2028.\u00A0;
				}
				else
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
					uint num2;
					uint num3;
					int num = global::\u00A0.\u2028.\u00A0(global::\u00A0.\u2028.\u00A0.\u1680, out num2, out num3);
					if (num != 0)
					{
						throw new Win32Exception(num);
					}
					global::\u00A0.\u2028.\u2010[] array = new global::\u00A0.\u2028.\u2010[num2];
					global::\u00A0.\u2028.\u2024[] array2 = new global::\u00A0.\u2028.\u2024[num3];
					num = global::\u00A0.\u2028.\u00A0(global::\u00A0.\u2028.\u00A0.\u1680, ref num2, array, ref num3, array2, IntPtr.Zero);
					if (num != 0)
					{
						throw new Win32Exception(num);
					}
					foreach (global::\u00A0.\u2028.\u2010 u in array)
					{
						global::\u00A0.\u2028.\u2033 u2 = default(global::\u00A0.\u2028.\u2033);
						u2.\u00A0.\u00A0 = (uint)Marshal.SizeOf(typeof(global::\u00A0.\u2028.\u2033));
						u2.\u00A0.\u00A0 = u.\u00A0.\u00A0;
						u2.\u00A0.\u1680 = u.\u00A0.\u00A0;
						u2.\u00A0.\u00A0 = global::\u00A0.\u2028.\u2005.\u00A0;
						global::\u00A0.\u2028.\u2033 u3 = u2;
						if (global::\u00A0.\u2028.\u00A0(ref u3) == 0)
						{
							string u00A3 = u3.\u00A0;
							string text = global::\u00A0.\u2028.\u00A0(u.\u00A0.\u00A0, u.\u00A0.\u00A0);
							if (!dictionary.ContainsKey(u00A3))
							{
								dictionary[u00A3] = text;
							}
						}
					}
					global::\u00A0.\u2028.\u00A0 = dictionary;
					u00A2 = global::\u00A0.\u2028.\u00A0;
				}
			}
			return u00A2;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006594 File Offset: 0x00004794
		private static bool \u1680(global::\u00A0.\u2028.\u2008 A_0, uint A_1)
		{
			global::\u00A0.\u2028.\u202F u202F = default(global::\u00A0.\u2028.\u202F);
			u202F.\u00A0.\u00A0 = (uint)Marshal.SizeOf(typeof(global::\u00A0.\u2028.\u202F));
			u202F.\u00A0.\u00A0 = A_0;
			u202F.\u00A0.\u1680 = A_1;
			u202F.\u00A0.\u00A0 = global::\u00A0.\u2028.\u2005.\u2006;
			global::\u00A0.\u2028.\u202F u202F2 = u202F;
			int num = global::\u00A0.\u2028.\u00A0(ref u202F2);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			return u202F2.advancedColorEnabled;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006607 File Offset: 0x00004807
		[NullableContext(1)]
		private static IEnumerable<bool> \u1680()
		{
			return new global::\u00A0.\u2028.\u2047(-2);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006610 File Offset: 0x00004810
		[NullableContext(1)]
		public static string \u00A0(this Screen A_0)
		{
			string result;
			if (global::\u00A0.\u2028.\u00A0().TryGetValue(A_0.DeviceName, ref result))
			{
				return result;
			}
			return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006638 File Offset: 0x00004838
		[NullableContext(1)]
		public static bool \u1680(this Screen A_0)
		{
			IEnumerable<bool> enumerable = global::\u00A0.\u2028.\u1680();
			for (int i = 0; i < Screen.AllScreens.Length; i++)
			{
				if (object.Equals(A_0, Screen.AllScreens[i]))
				{
					return Enumerable.ToArray<bool>(enumerable)[i];
				}
			}
			return false;
		}

		// Token: 0x0400012D RID: 301
		private const int \u00A0 = 0;

		// Token: 0x0400012E RID: 302
		public const int \u1680 = 4;

		// Token: 0x0400012F RID: 303
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private static Dictionary<string, string> \u00A0;

		// Token: 0x04000130 RID: 304
		[Nullable(1)]
		private static readonly object \u00A0 = new object();

		// Token: 0x04000131 RID: 305
		private static bool \u00A0 = false;

		// Token: 0x0200003C RID: 60
		public enum \u00A0 : uint
		{
			// Token: 0x04000133 RID: 307
			\u00A0 = 1U,
			// Token: 0x04000134 RID: 308
			\u1680,
			// Token: 0x04000135 RID: 309
			\u2000 = 4U
		}

		// Token: 0x0200003D RID: 61
		public enum \u1680 : uint
		{
			// Token: 0x04000137 RID: 311
			\u00A0 = 4294967295U,
			// Token: 0x04000138 RID: 312
			\u1680 = 0U,
			// Token: 0x04000139 RID: 313
			\u2000,
			// Token: 0x0400013A RID: 314
			\u2001,
			// Token: 0x0400013B RID: 315
			\u2002,
			// Token: 0x0400013C RID: 316
			\u2003,
			// Token: 0x0400013D RID: 317
			\u2004,
			// Token: 0x0400013E RID: 318
			\u2005,
			// Token: 0x0400013F RID: 319
			\u2006 = 8U,
			// Token: 0x04000140 RID: 320
			\u2007,
			// Token: 0x04000141 RID: 321
			\u2008,
			// Token: 0x04000142 RID: 322
			\u2009,
			// Token: 0x04000143 RID: 323
			\u200A,
			// Token: 0x04000144 RID: 324
			\u200B,
			// Token: 0x04000145 RID: 325
			\u2010,
			// Token: 0x04000146 RID: 326
			\u2011,
			// Token: 0x04000147 RID: 327
			\u2012 = 2147483648U,
			// Token: 0x04000148 RID: 328
			\u2013 = 4294967295U
		}

		// Token: 0x0200003E RID: 62
		public enum \u2000 : uint
		{
			// Token: 0x0400014A RID: 330
			\u00A0,
			// Token: 0x0400014B RID: 331
			\u1680,
			// Token: 0x0400014C RID: 332
			\u2000,
			// Token: 0x0400014D RID: 333
			\u2001 = 2U,
			// Token: 0x0400014E RID: 334
			\u2002,
			// Token: 0x0400014F RID: 335
			\u2003 = 4294967295U
		}

		// Token: 0x0200003F RID: 63
		public enum \u2001 : uint
		{
			// Token: 0x04000151 RID: 337
			\u00A0 = 1U,
			// Token: 0x04000152 RID: 338
			\u1680,
			// Token: 0x04000153 RID: 339
			\u2000,
			// Token: 0x04000154 RID: 340
			\u2001,
			// Token: 0x04000155 RID: 341
			\u2002 = 4294967295U
		}

		// Token: 0x02000040 RID: 64
		public enum \u2002 : uint
		{
			// Token: 0x04000157 RID: 343
			\u00A0 = 1U,
			// Token: 0x04000158 RID: 344
			\u1680,
			// Token: 0x04000159 RID: 345
			\u2000,
			// Token: 0x0400015A RID: 346
			\u2001,
			// Token: 0x0400015B RID: 347
			\u2002,
			// Token: 0x0400015C RID: 348
			\u2003 = 128U,
			// Token: 0x0400015D RID: 349
			\u2004 = 4294967295U
		}

		// Token: 0x02000041 RID: 65
		public enum \u2003 : uint
		{
			// Token: 0x0400015F RID: 351
			\u00A0 = 1U,
			// Token: 0x04000160 RID: 352
			\u1680,
			// Token: 0x04000161 RID: 353
			\u2000,
			// Token: 0x04000162 RID: 354
			\u2001,
			// Token: 0x04000163 RID: 355
			\u2002,
			// Token: 0x04000164 RID: 356
			\u2003 = 4294967295U
		}

		// Token: 0x02000042 RID: 66
		public enum \u2004 : uint
		{
			// Token: 0x04000166 RID: 358
			\u00A0 = 1U,
			// Token: 0x04000167 RID: 359
			\u1680,
			// Token: 0x04000168 RID: 360
			\u2000 = 4294967295U
		}

		// Token: 0x02000043 RID: 67
		public enum \u2005 : uint
		{
			// Token: 0x0400016A RID: 362
			\u00A0 = 1U,
			// Token: 0x0400016B RID: 363
			\u1680,
			// Token: 0x0400016C RID: 364
			\u2000,
			// Token: 0x0400016D RID: 365
			\u2001,
			// Token: 0x0400016E RID: 366
			\u2002,
			// Token: 0x0400016F RID: 367
			\u2003,
			// Token: 0x04000170 RID: 368
			\u2004,
			// Token: 0x04000171 RID: 369
			\u2005,
			// Token: 0x04000172 RID: 370
			\u2006,
			// Token: 0x04000173 RID: 371
			\u2007,
			// Token: 0x04000174 RID: 372
			\u2008
		}

		// Token: 0x02000044 RID: 68
		[NullableContext(1)]
		[Nullable(0)]
		public struct \u2006
		{
			// Token: 0x04000175 RID: 373
			[MarshalAs(23, SizeConst = 32)]
			public string \u00A0;

			// Token: 0x04000176 RID: 374
			public short \u00A0;

			// Token: 0x04000177 RID: 375
			public short \u1680;

			// Token: 0x04000178 RID: 376
			public short \u2000;

			// Token: 0x04000179 RID: 377
			public short \u2001;

			// Token: 0x0400017A RID: 378
			public int \u00A0;

			// Token: 0x0400017B RID: 379
			public int \u1680;

			// Token: 0x0400017C RID: 380
			public int \u2000;

			// Token: 0x0400017D RID: 381
			public int \u2001;

			// Token: 0x0400017E RID: 382
			public int \u2002;

			// Token: 0x0400017F RID: 383
			public short \u2002;

			// Token: 0x04000180 RID: 384
			public short \u2003;

			// Token: 0x04000181 RID: 385
			public short \u2004;

			// Token: 0x04000182 RID: 386
			public short \u2005;

			// Token: 0x04000183 RID: 387
			public short \u2006;

			// Token: 0x04000184 RID: 388
			[MarshalAs(23, SizeConst = 32)]
			public string \u1680;

			// Token: 0x04000185 RID: 389
			public short \u2007;

			// Token: 0x04000186 RID: 390
			public short \u2008;

			// Token: 0x04000187 RID: 391
			public int \u2003;

			// Token: 0x04000188 RID: 392
			public int \u2004;

			// Token: 0x04000189 RID: 393
			public int \u2005;

			// Token: 0x0400018A RID: 394
			public int \u2006;

			// Token: 0x0400018B RID: 395
			public int \u2007;

			// Token: 0x0400018C RID: 396
			public int \u2008;

			// Token: 0x0400018D RID: 397
			public int \u2009;

			// Token: 0x0400018E RID: 398
			public int \u200A;

			// Token: 0x0400018F RID: 399
			public int \u200B;

			// Token: 0x04000190 RID: 400
			public int \u2010;

			// Token: 0x04000191 RID: 401
			public int \u2011;

			// Token: 0x04000192 RID: 402
			public int \u2012;
		}

		// Token: 0x02000045 RID: 69
		[NullableContext(1)]
		[Nullable(0)]
		[StructLayout(0, CharSet = 3)]
		public struct \u2007
		{
			// Token: 0x04000193 RID: 403
			public int \u00A0;

			// Token: 0x04000194 RID: 404
			[MarshalAs(23, SizeConst = 32)]
			public string \u00A0;

			// Token: 0x04000195 RID: 405
			[MarshalAs(23, SizeConst = 128)]
			public string \u1680;

			// Token: 0x04000196 RID: 406
			public int \u1680;

			// Token: 0x04000197 RID: 407
			[MarshalAs(23, SizeConst = 128)]
			public string \u2000;

			// Token: 0x04000198 RID: 408
			[MarshalAs(23, SizeConst = 128)]
			public string \u2001;
		}

		// Token: 0x02000046 RID: 70
		public struct \u2008
		{
			// Token: 0x04000199 RID: 409
			public uint \u00A0;

			// Token: 0x0400019A RID: 410
			public int \u00A0;
		}

		// Token: 0x02000047 RID: 71
		public struct \u2009
		{
			// Token: 0x0400019B RID: 411
			public global::\u00A0.\u2028.\u2008 \u00A0;

			// Token: 0x0400019C RID: 412
			public uint \u00A0;

			// Token: 0x0400019D RID: 413
			public uint \u1680;

			// Token: 0x0400019E RID: 414
			public uint \u2000;
		}

		// Token: 0x02000048 RID: 72
		public struct \u200A
		{
			// Token: 0x0400019F RID: 415
			public global::\u00A0.\u2028.\u2008 \u00A0;

			// Token: 0x040001A0 RID: 416
			public uint \u00A0;

			// Token: 0x040001A1 RID: 417
			public uint \u1680;

			// Token: 0x040001A2 RID: 418
			private global::\u00A0.\u2028.\u1680 \u00A0;

			// Token: 0x040001A3 RID: 419
			private global::\u00A0.\u2028.\u2001 \u00A0;

			// Token: 0x040001A4 RID: 420
			private global::\u00A0.\u2028.\u2002 \u00A0;

			// Token: 0x040001A5 RID: 421
			private global::\u00A0.\u2028.\u200B \u00A0;

			// Token: 0x040001A6 RID: 422
			private global::\u00A0.\u2028.\u2000 \u00A0;

			// Token: 0x040001A7 RID: 423
			public bool \u00A0;

			// Token: 0x040001A8 RID: 424
			public uint \u2000;
		}

		// Token: 0x02000049 RID: 73
		public struct \u200B
		{
			// Token: 0x040001A9 RID: 425
			public uint \u00A0;

			// Token: 0x040001AA RID: 426
			public uint \u1680;
		}

		// Token: 0x0200004A RID: 74
		public struct \u2010
		{
			// Token: 0x040001AB RID: 427
			public global::\u00A0.\u2028.\u2009 \u00A0;

			// Token: 0x040001AC RID: 428
			public global::\u00A0.\u2028.\u200A \u00A0;

			// Token: 0x040001AD RID: 429
			public uint \u00A0;
		}

		// Token: 0x0200004B RID: 75
		public struct \u2011
		{
			// Token: 0x040001AE RID: 430
			public uint \u00A0;

			// Token: 0x040001AF RID: 431
			public uint \u1680;
		}

		// Token: 0x0200004C RID: 76
		public struct \u2012
		{
			// Token: 0x040001B0 RID: 432
			public ulong \u00A0;

			// Token: 0x040001B1 RID: 433
			public global::\u00A0.\u2028.\u200B \u00A0;

			// Token: 0x040001B2 RID: 434
			public global::\u00A0.\u2028.\u200B \u1680;

			// Token: 0x040001B3 RID: 435
			public global::\u00A0.\u2028.\u2011 \u00A0;

			// Token: 0x040001B4 RID: 436
			public global::\u00A0.\u2028.\u2011 \u1680;

			// Token: 0x040001B5 RID: 437
			public uint \u00A0;

			// Token: 0x040001B6 RID: 438
			public global::\u00A0.\u2028.\u2000 \u00A0;
		}

		// Token: 0x0200004D RID: 77
		public struct \u2013
		{
			// Token: 0x040001B7 RID: 439
			public global::\u00A0.\u2028.\u2012 \u00A0;
		}

		// Token: 0x0200004E RID: 78
		public struct \u2014
		{
			// Token: 0x040001B8 RID: 440
			private int \u00A0;

			// Token: 0x040001B9 RID: 441
			private int \u1680;
		}

		// Token: 0x0200004F RID: 79
		public struct \u2015
		{
			// Token: 0x040001BA RID: 442
			public uint \u00A0;

			// Token: 0x040001BB RID: 443
			public uint \u1680;

			// Token: 0x040001BC RID: 444
			public global::\u00A0.\u2028.\u2003 \u00A0;

			// Token: 0x040001BD RID: 445
			public global::\u00A0.\u2028.\u2014 \u00A0;
		}

		// Token: 0x02000050 RID: 80
		[StructLayout(2)]
		public struct \u2022
		{
			// Token: 0x040001BE RID: 446
			[FieldOffset(0)]
			public global::\u00A0.\u2028.\u2013 \u00A0;

			// Token: 0x040001BF RID: 447
			[FieldOffset(0)]
			public global::\u00A0.\u2028.\u2015 \u00A0;
		}

		// Token: 0x02000051 RID: 81
		public struct \u2024
		{
			// Token: 0x040001C0 RID: 448
			public global::\u00A0.\u2028.\u2004 \u00A0;

			// Token: 0x040001C1 RID: 449
			public uint \u00A0;

			// Token: 0x040001C2 RID: 450
			public global::\u00A0.\u2028.\u2008 \u00A0;

			// Token: 0x040001C3 RID: 451
			public global::\u00A0.\u2028.\u2022 \u00A0;
		}

		// Token: 0x02000052 RID: 82
		public struct \u2025
		{
			// Token: 0x040001C4 RID: 452
			public uint \u00A0;
		}

		// Token: 0x02000053 RID: 83
		public struct \u2027
		{
			// Token: 0x040001C5 RID: 453
			public global::\u00A0.\u2028.\u2005 \u00A0;

			// Token: 0x040001C6 RID: 454
			public uint \u00A0;

			// Token: 0x040001C7 RID: 455
			public global::\u00A0.\u2028.\u2008 \u00A0;

			// Token: 0x040001C8 RID: 456
			public uint \u1680;
		}

		// Token: 0x02000054 RID: 84
		[NullableContext(1)]
		[Nullable(0)]
		[StructLayout(0, CharSet = 3)]
		public struct \u2028
		{
			// Token: 0x040001C9 RID: 457
			public global::\u00A0.\u2028.\u2027 \u00A0;

			// Token: 0x040001CA RID: 458
			public global::\u00A0.\u2028.\u2025 \u00A0;

			// Token: 0x040001CB RID: 459
			public global::\u00A0.\u2028.\u1680 \u00A0;

			// Token: 0x040001CC RID: 460
			public ushort \u00A0;

			// Token: 0x040001CD RID: 461
			public ushort \u1680;

			// Token: 0x040001CE RID: 462
			public uint \u00A0;

			// Token: 0x040001CF RID: 463
			[MarshalAs(23, SizeConst = 64)]
			public string \u00A0;

			// Token: 0x040001D0 RID: 464
			[MarshalAs(23, SizeConst = 128)]
			public string \u1680;
		}

		// Token: 0x02000055 RID: 85
		private enum \u2029
		{
			// Token: 0x040001D2 RID: 466
			\u00A0,
			// Token: 0x040001D3 RID: 467
			\u1680,
			// Token: 0x040001D4 RID: 468
			\u2000,
			// Token: 0x040001D5 RID: 469
			\u2001,
			// Token: 0x040001D6 RID: 470
			\u2002
		}

		// Token: 0x02000056 RID: 86
		private enum \u202A
		{
			// Token: 0x040001D8 RID: 472
			\u00A0 = 1,
			// Token: 0x040001D9 RID: 473
			\u1680,
			// Token: 0x040001DA RID: 474
			\u2000 = 4,
			// Token: 0x040001DB RID: 475
			\u2001 = 8
		}

		// Token: 0x02000057 RID: 87
		private enum \u202B
		{
			// Token: 0x040001DD RID: 477
			\u00A0 = 1,
			// Token: 0x040001DE RID: 478
			\u1680 = 4,
			// Token: 0x040001DF RID: 479
			\u2000 = 8
		}

		// Token: 0x02000058 RID: 88
		private enum \u202C
		{
			// Token: 0x040001E1 RID: 481
			\u00A0 = 1
		}

		// Token: 0x02000059 RID: 89
		private enum \u202D
		{
			// Token: 0x040001E3 RID: 483
			\u00A0 = 1,
			// Token: 0x040001E4 RID: 484
			\u1680,
			// Token: 0x040001E5 RID: 485
			\u2000 = 4,
			// Token: 0x040001E6 RID: 486
			\u2001 = 8,
			// Token: 0x040001E7 RID: 487
			\u2002 = 16,
			// Token: 0x040001E8 RID: 488
			\u2003 = 32
		}

		// Token: 0x0200005A RID: 90
		private enum \u202E
		{
			// Token: 0x040001EA RID: 490
			\u00A0 = 1,
			// Token: 0x040001EB RID: 491
			\u1680,
			// Token: 0x040001EC RID: 492
			\u2000 = 4,
			// Token: 0x040001ED RID: 493
			\u2001 = 16,
			// Token: 0x040001EE RID: 494
			\u2002 = 32
		}

		// Token: 0x0200005B RID: 91
		private struct \u202F
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000169 RID: 361 RVA: 0x00006676 File Offset: 0x00004876
			public bool advancedColorSupported
			{
				get
				{
					return (this.\u00A0 & 1U) == 1U;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600016A RID: 362 RVA: 0x00006683 File Offset: 0x00004883
			public bool advancedColorEnabled
			{
				get
				{
					return (this.\u00A0 & 2U) == 2U;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x0600016B RID: 363 RVA: 0x00006690 File Offset: 0x00004890
			public bool wideColorEnforced
			{
				get
				{
					return (this.\u00A0 & 4U) == 4U;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600016C RID: 364 RVA: 0x0000669D File Offset: 0x0000489D
			public bool advancedColorForceDisabled
			{
				get
				{
					return (this.\u00A0 & 8U) == 8U;
				}
			}

			// Token: 0x040001EF RID: 495
			public global::\u00A0.\u2028.\u2027 \u00A0;

			// Token: 0x040001F0 RID: 496
			public uint \u00A0;

			// Token: 0x040001F1 RID: 497
			public global::\u00A0.\u2028.\u2029 \u00A0;

			// Token: 0x040001F2 RID: 498
			public int \u00A0;
		}

		// Token: 0x0200005C RID: 92
		private struct \u2032
		{
			// Token: 0x040001F3 RID: 499
			public global::\u00A0.\u2028.\u2014 \u00A0;

			// Token: 0x040001F4 RID: 500
			public global::\u00A0.\u2028.\u2036 \u00A0;

			// Token: 0x040001F5 RID: 501
			public global::\u00A0.\u2028.\u2036 \u1680;
		}

		// Token: 0x0200005D RID: 93
		[StructLayout(2)]
		private struct \u2035
		{
			// Token: 0x040001F6 RID: 502
			[FieldOffset(0)]
			public global::\u00A0.\u2028.\u2013 \u00A0;

			// Token: 0x040001F7 RID: 503
			[FieldOffset(0)]
			public global::\u00A0.\u2028.\u2015 \u00A0;

			// Token: 0x040001F8 RID: 504
			[FieldOffset(0)]
			public global::\u00A0.\u2028.\u2032 \u00A0;
		}

		// Token: 0x0200005E RID: 94
		[StructLayout(0, CharSet = 3)]
		private struct \u2033
		{
			// Token: 0x040001F9 RID: 505
			public global::\u00A0.\u2028.\u2027 \u00A0;

			// Token: 0x040001FA RID: 506
			[Nullable(1)]
			[MarshalAs(23, SizeConst = 32)]
			public string \u00A0;
		}

		// Token: 0x0200005F RID: 95
		private struct \u2036
		{
			// Token: 0x040001FB RID: 507
			public int \u00A0;

			// Token: 0x040001FC RID: 508
			public int \u1680;

			// Token: 0x040001FD RID: 509
			public int \u2000;

			// Token: 0x040001FE RID: 510
			public int \u2001;
		}

		// Token: 0x02000060 RID: 96
		[CompilerGenerated]
		[Serializable]
		private sealed class \u203E
		{
			// Token: 0x0600016F RID: 367 RVA: 0x000066B8 File Offset: 0x000048B8
			[NullableContext(1)]
			internal void \u00A0([Nullable(2)] object s, EventArgs A_2)
			{
				object u00A = global::\u00A0.\u2028.\u00A0;
				lock (u00A)
				{
					global::\u00A0.\u2028.\u00A0 = null;
				}
			}

			// Token: 0x06000170 RID: 368 RVA: 0x000066F8 File Offset: 0x000048F8
			[NullableContext(1)]
			internal void \u1680([Nullable(2)] object s, EventArgs A_2)
			{
				SystemEvents.DisplaySettingsChanged -= null;
			}

			// Token: 0x040001FF RID: 511
			public static readonly global::\u00A0.\u2028.\u203E \u00A0 = new global::\u00A0.\u2028.\u203E();
		}
	}
}
