// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\‫.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using ManagedCuda;
using Microsoft.Win32;

namespace obf_1
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(0)]
	public class obf_30
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00006BDC File Offset: 0x00004DDC
		public static string obf_1()
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_31()))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current[0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_32()];
						return (((obj != null) ? obj.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_33()).Trim();
					}
				}
			}
			return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_33();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006C70 File Offset: 0x00004E70
		public static string obf_3()
		{
			ulong num = 0UL;
			string text = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_24();
			uint num2 = 0U;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_34()))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					num += (ulong)managementBaseObject[0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_35()];
					text = obf_30.obf_1((uint)managementBaseObject[0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_36()]);
					num2 = (uint)(managementBaseObject[0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_37()] ?? 0);
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(11, 3);
			defaultInterpolatedStringHandler.AppendFormatted(text);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_38());
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num / 1073741824UL);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_39());
			defaultInterpolatedStringHandler.AppendFormatted<uint>(num2);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_40());
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006D84 File Offset: 0x00004F84
		public static string obf_4()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_41()))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42());
					string text = ((value != null) ? value.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_43();
					object value2 = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_44());
					string text2 = ((value2 != null) ? value2.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_24();
					object value3 = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_45());
					string text3 = ((value3 != null) ? value3.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_24();
					object value4 = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_46());
					string text4 = ((value4 != null) ? value4.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_43();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
					defaultInterpolatedStringHandler.AppendFormatted(text);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_38());
					defaultInterpolatedStringHandler.AppendFormatted((text3 != 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_24()) ? (0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_47() + text3 + 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_48()) : text2);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_49());
					defaultInterpolatedStringHandler.AppendFormatted(text4);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_50();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006EB4 File Offset: 0x000050B4
		public static string obf_5()
		{
			obf_27.obf_28 u = default(obf_27.obf_28);
			u.obf_1 = Marshal.SizeOf<obf_27.obf_28>(u);
			string result = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_24();
			uint num = 0U;
			while (obf_27.obf_1(null, num, ref u, 0U))
			{
				if ((u.obf_3 & 4) != 0)
				{
					obf_27.obf_12 u2 = default(obf_27.obf_12);
					u2.obf_4 = (short)Marshal.SizeOf<obf_27.obf_12>(u2);
					obf_11.obf_1(obf_30.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_51() + u.obf_3, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_52());
					result = u.obf_3;
					break;
				}
				num += 1U;
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006F39 File Offset: 0x00005139
		public static bool obf_6()
		{
			return obf_30.obf_1(obf_30.obf_5()) || obf_30.obf_7() > 0;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006F54 File Offset: 0x00005154
		public static int obf_7()
		{
			if (obf_30.obf_1 != -1)
			{
				return obf_30.obf_1;
			}
			try
			{
				using (CudaContext cudaContext = new CudaContext(0))
				{
					CudaDeviceProperties deviceInfo = cudaContext.GetDeviceInfo();
					obf_30.obf_1 = deviceInfo.MultiProcessorCount * obf_30.obf_1(deviceInfo.ComputeCapability.Major, deviceInfo.ComputeCapability.Minor);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_53() + ex.ToString());
				obf_30.obf_1 = 0;
			}
			return obf_30.obf_1;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006FF4 File Offset: 0x000051F4
		private static int obf_1(int A_0, int A_1)
		{
			if (A_0 == 2)
			{
				return 32;
			}
			if (A_0 == 3)
			{
				return 192;
			}
			if (A_0 == 5)
			{
				return 128;
			}
			if (A_0 == 6)
			{
				return 128;
			}
			if (A_0 == 7 && A_1 == 0)
			{
				return 64;
			}
			if (A_0 == 7 && A_1 == 5)
			{
				return 64;
			}
			if (A_0 == 8 && A_1 < 9)
			{
				return 128;
			}
			if (A_0 == 8 && A_1 == 9)
			{
				return 128;
			}
			return 128;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007064 File Offset: 0x00005264
		private static bool obf_1(string A_0)
		{
			obf_30.obf_1 u00A = new obf_30.obf_1();
			u00A.obf_1 = A_0;
			foreach (string text in obf_30.obf_1)
			{
				IEnumerable<string> enumerable = text.Split(' ', 0);
				Func<string, bool> func;
				if ((func = u00A.obf_1) == null)
				{
					func = (u00A.obf_1 = new Func<string, bool>(u00A.obf_1));
				}
				if (Enumerable.All<string>(enumerable, func))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000070F0 File Offset: 0x000052F0
		private static string obf_1(uint A_0)
		{
			switch (A_0)
			{
			case 24U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_54();
			case 26U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_55();
			case 27U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_56();
			case 28U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_57();
			case 29U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_58();
			case 30U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_59();
			case 31U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_60();
			case 32U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_61();
			case 33U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_62();
			case 34U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_63();
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(10, 1);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_64());
			defaultInterpolatedStringHandler.AppendFormatted<uint>(A_0);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_48());
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000071B8 File Offset: 0x000053B8
		// Note: this type is marked as 'beforefieldinit'.
		static obf_30()
		{
			List<string> list = new List<string>();
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_65());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_66());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_67());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_68());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_69());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_70());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_71());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_72());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_73());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_74());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_75());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_76());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_77());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_78());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_79());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_80());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_81());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_82());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_83());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_84());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_85());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_86());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_87());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_88());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_89());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_90());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_91());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_92());
			obf_30.obf_1 = list;
		}

		// Token: 0x04000207 RID: 519
		private static string obf_1 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_93();

		// Token: 0x04000208 RID: 520
		private static int obf_1 = -1;

		// Token: 0x04000209 RID: 521
		private static List<string> obf_1;

		// Token: 0x02000065 RID: 101
		[CompilerGenerated]
		private sealed class obf_1
		{
			// Token: 0x0600018A RID: 394 RVA: 0x00007313 File Offset: 0x00005513
			[NullableContext(0)]
			internal bool obf_1(string A_1)
			{
				return this.obf_1.Contains(A_1);
			}

			// Token: 0x0400020A RID: 522
			[Nullable(0)]
			public string obf_1;

			// Token: 0x0400020B RID: 523
			[Nullable(0)]
			public Func<string, bool> obf_1;
		}
	}
}
