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
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select Name from Win32_Processor"))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current["Name"];
						return (((obj != null) ? obj.ToString() : null) ?? "Unknown CPU").Trim();
					}
				}
			}
			return "Unknown CPU";
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006C70 File Offset: 0x00004E70
		public static string obf_3()
		{
			ulong num = 0UL;
			string text = "";
			uint num2 = 0U;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT Capacity, Speed, SMBIOSMemoryType FROM Win32_PhysicalMemory"))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					num += (ulong)managementBaseObject["Capacity"];
					text = obf_30.obf_1((uint)managementBaseObject["SMBIOSMemoryType"]);
					num2 = (uint)(managementBaseObject["Speed"] ?? 0);
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(11, 3);
			defaultInterpolatedStringHandler.AppendFormatted(text);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num / 1073741824UL);
			defaultInterpolatedStringHandler.AppendLiteral(" GB (");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(num2);
			defaultInterpolatedStringHandler.AppendLiteral(" MHz)");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006D84 File Offset: 0x00004F84
		public static string obf_4()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("ProductName");
					string text = ((value != null) ? value.ToString() : null) ?? "Unknown";
					object value2 = registryKey.GetValue("ReleaseId");
					string text2 = ((value2 != null) ? value2.ToString() : null) ?? "";
					object value3 = registryKey.GetValue("DisplayVersion");
					string text3 = ((value3 != null) ? value3.ToString() : null) ?? "";
					object value4 = registryKey.GetValue("CurrentBuildNumber");
					string text4 = ((value4 != null) ? value4.ToString() : null) ?? "Unknown";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
					defaultInterpolatedStringHandler.AppendFormatted(text);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted((text3 != "") ? ("(" + text3 + ")") : text2);
					defaultInterpolatedStringHandler.AppendLiteral(" Build ");
					defaultInterpolatedStringHandler.AppendFormatted(text4);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			return "Unknown Windows Version";
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006EB4 File Offset: 0x000050B4
		public static string obf_5()
		{
			obf_27.obf_28 u = default(obf_27.obf_28);
			u.obf_1 = Marshal.SizeOf<obf_27.obf_28>(u);
			string result = "";
			uint num = 0U;
			while (obf_27.obf_1(null, num, ref u, 0U))
			{
				if ((u.obf_3 & 4) != 0)
				{
					obf_27.obf_12 u2 = default(obf_27.obf_12);
					u2.obf_4 = (short)Marshal.SizeOf<obf_27.obf_12>(u2);
					obf_11.obf_1(obf_30.obf_1, "GPU Name : " + u.obf_3, "GetGPUName");
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
				Console.WriteLine("Fail to Get CudaCore: " + ex.ToString());
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
				return "DDR3";
			case 26U:
				return "DDR4";
			case 27U:
				return "LPDDR";
			case 28U:
				return "LPDDR2";
			case 29U:
				return "LPDDR3";
			case 30U:
				return "LPDDR4";
			case 31U:
				return "Logical non-volatile device";
			case 32U:
				return "HBM";
			case 33U:
				return "HBM2";
			case 34U:
				return "DDR5";
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(10, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Unknown (");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(A_0);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000071B8 File Offset: 0x000053B8
		// Note: this type is marked as 'beforefieldinit'.
		static obf_30()
		{
			List<string> list = new List<string>();
			list.Add("RTX 5090");
			list.Add("RTX 5090 D");
			list.Add("RTX 5080");
			list.Add("RTX 5070 Ti");
			list.Add("RTX 5070");
			list.Add("RTX 5060");
			list.Add("RTX 5060 Ti");
			list.Add("RTX 4090");
			list.Add("RTX 4090 D");
			list.Add("RTX 4080 SUPER");
			list.Add("RTX 4080");
			list.Add("RTX 4070 Ti");
			list.Add("RTX 4070 Ti SUPER");
			list.Add("RTX 4070 SUPER");
			list.Add("RTX 4070");
			list.Add("RTX 4060 Ti");
			list.Add("RTX 4060");
			list.Add("RTX 3090 Ti");
			list.Add("RTX 3090");
			list.Add("RTX 3080 Ti");
			list.Add("RTX 3080");
			list.Add("RTX 3070 Ti");
			list.Add("RTX 3070");
			list.Add("RTX 3060 Ti");
			list.Add("RTX 3060");
			list.Add("RTX 2080 Ti");
			list.Add("RTX 2080 SUPER");
			list.Add("RTX 2080");
			obf_30.obf_1 = list;
		}

		// Token: 0x04000207 RID: 519
		private static string obf_1 = "SystemSpecHelper";

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
