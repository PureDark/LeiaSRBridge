using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using ManagedCuda;
using Microsoft.Win32;

namespace \u00A0
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(0)]
	public class \u202B
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00006BDC File Offset: 0x00004DDC
		public static string \u00A0()
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204C()))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current[0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204D()];
						return (((obj != null) ? obj.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204E()).Trim();
					}
				}
			}
			return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204E();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006C70 File Offset: 0x00004E70
		public static string \u1680()
		{
			ulong num = 0UL;
			string text = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();
			uint num2 = 0U;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204F()))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					num += (ulong)managementBaseObject[0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2050()];
					text = \u202B.\u00A0((uint)managementBaseObject[0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2051()]);
					num2 = (uint)(managementBaseObject[0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2052()] ?? 0);
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(11, 3);
			defaultInterpolatedStringHandler.AppendFormatted(text);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2053());
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num / 1073741824UL);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2054());
			defaultInterpolatedStringHandler.AppendFormatted<uint>(num2);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2055());
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006D84 File Offset: 0x00004F84
		public static string \u2000()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2056()))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2057());
					string text = ((value != null) ? value.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2058();
					object value2 = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2059());
					string text2 = ((value2 != null) ? value2.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();
					object value3 = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205A());
					string text3 = ((value3 != null) ? value3.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();
					object value4 = registryKey.GetValue(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205B());
					string text4 = ((value4 != null) ? value4.ToString() : null) ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2058();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
					defaultInterpolatedStringHandler.AppendFormatted(text);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2053());
					defaultInterpolatedStringHandler.AppendFormatted((text3 != 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032()) ? (0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205C() + text3 + 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205D()) : text2);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205E());
					defaultInterpolatedStringHandler.AppendFormatted(text4);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205F();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006EB4 File Offset: 0x000050B4
		public static string \u2001()
		{
			\u2028.\u2007 u = default(\u2028.\u2007);
			u.\u00A0 = Marshal.SizeOf<\u2028.\u2007>(u);
			string result = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();
			uint num = 0U;
			while (\u2028.\u00A0(null, num, ref u, 0U))
			{
				if ((u.\u1680 & 4) != 0)
				{
					\u2028.\u2006 u2 = default(\u2028.\u2006);
					u2.\u2000 = (short)Marshal.SizeOf<\u2028.\u2006>(u2);
					\u2025.\u00A0(\u202B.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2060() + u.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2061());
					result = u.\u1680;
					break;
				}
				num += 1U;
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006F39 File Offset: 0x00005139
		public static bool \u2002()
		{
			return \u202B.\u00A0(\u202B.\u2001()) || \u202B.\u2003() > 0;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006F54 File Offset: 0x00005154
		public static int \u2003()
		{
			if (\u202B.\u00A0 != -1)
			{
				return \u202B.\u00A0;
			}
			try
			{
				using (CudaContext cudaContext = new CudaContext(0))
				{
					CudaDeviceProperties deviceInfo = cudaContext.GetDeviceInfo();
					\u202B.\u00A0 = deviceInfo.MultiProcessorCount * \u202B.\u00A0(deviceInfo.ComputeCapability.Major, deviceInfo.ComputeCapability.Minor);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2062() + ex.ToString());
				\u202B.\u00A0 = 0;
			}
			return \u202B.\u00A0;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006FF4 File Offset: 0x000051F4
		private static int \u00A0(int A_0, int A_1)
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
		private static bool \u00A0(string A_0)
		{
			\u202B.\u00A0 u00A = new \u202B.\u00A0();
			u00A.\u00A0 = A_0;
			foreach (string text in \u202B.\u00A0)
			{
				IEnumerable<string> enumerable = text.Split(' ', 0);
				Func<string, bool> func;
				if ((func = u00A.\u00A0) == null)
				{
					func = (u00A.\u00A0 = new Func<string, bool>(u00A.\u00A0));
				}
				if (Enumerable.All<string>(enumerable, func))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000070F0 File Offset: 0x000052F0
		private static string \u00A0(uint A_0)
		{
			switch (A_0)
			{
			case 24U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2063();
			case 26U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2064();
			case 27U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u206A();
			case 28U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u206B();
			case 29U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u206C();
			case 30U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u206D();
			case 31U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u206E();
			case 32U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u206F();
			case 33U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u3000();
			case 34U:
				return 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u00A0();
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(10, 1);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u1680());
			defaultInterpolatedStringHandler.AppendFormatted<uint>(A_0);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u205D());
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000071B8 File Offset: 0x000053B8
		// Note: this type is marked as 'beforefieldinit'.
		static \u202B()
		{
			List<string> list = new List<string>();
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2001());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2002());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2003());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2004());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2005());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2006());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2007());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2008());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2009());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u200A());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u200B());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2010());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2011());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2012());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2013());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2014());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2015());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2022());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2024());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2025());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2027());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2028());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2029());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202A());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202B());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202C());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202D());
			list.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202E());
			\u202B.\u00A0 = list;
		}

		// Token: 0x04000207 RID: 519
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2000();

		// Token: 0x04000208 RID: 520
		private static int \u00A0 = -1;

		// Token: 0x04000209 RID: 521
		private static List<string> \u00A0;

		// Token: 0x02000065 RID: 101
		[CompilerGenerated]
		private sealed class \u00A0
		{
			// Token: 0x0600018A RID: 394 RVA: 0x00007313 File Offset: 0x00005513
			[NullableContext(0)]
			internal bool \u00A0(string A_1)
			{
				return this.\u00A0.Contains(A_1);
			}

			// Token: 0x0400020A RID: 522
			[Nullable(0)]
			public string \u00A0;

			// Token: 0x0400020B RID: 523
			[Nullable(0)]
			public Func<string, bool> \u00A0;
		}
	}
}
