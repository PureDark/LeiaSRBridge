using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2013
	{
		// Token: 0x06000111 RID: 273
		[DllImport("KPILogger.dll", CallingConvention = 2, EntryPoint = "Initialize")]
		private static extern \u2011 \u00A0(\u2013.\u00A0, string);

		// Token: 0x06000112 RID: 274
		[DllImport("KPILogger.dll", CallingConvention = 2, EntryPoint = "IsValidEvent")]
		public static extern \u2011 \u00A0(string, string);

		// Token: 0x06000113 RID: 275
		[DllImport("KPILogger.dll", CallingConvention = 2, EntryPoint = "AddKPILog")]
		private static extern \u2011 \u00A0(\u2013.\u1680);

		// Token: 0x06000114 RID: 276
		[DllImport("KPILogger.dll", CallingConvention = 2, EntryPoint = "FlushKPILog")]
		public static extern \u2011 \u00A0(string);

		// Token: 0x06000115 RID: 277
		[DllImport("KPILogger.dll", CallingConvention = 2, EntryPoint = "FlushAllKPILog")]
		public static extern \u2011 \u00A0();

		// Token: 0x06000116 RID: 278
		[DllImport("KPILogger.dll", CallingConvention = 2, EntryPoint = "SendKPILogDirectly")]
		private static extern \u2011 \u1680(\u2013.\u1680);

		// Token: 0x06000117 RID: 279
		[DllImport("KPILogger.dll", CallingConvention = 2, CharSet = 2, EntryPoint = "GetCountryCode")]
		public static extern \u2011 \u00A0(out IntPtr, string);

		// Token: 0x06000118 RID: 280 RVA: 0x000052D4 File Offset: 0x000034D4
		public static void \u1680()
		{
			if (!global::\u00A0.\u2002.\u1680())
			{
				return;
			}
			\u2013.\u00A0 u00A = new \u2013.\u00A0
			{
				\u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u204F(),
				\u1680 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u202D(),
				\u2000 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2050(),
				\u2001 = \u2013.\u2000(),
				\u2002 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032(),
				\u2003 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032(),
				\u2004 = (FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032()),
				\u2005 = global::\u00A0.\u00A0.Default.Proxy,
				\u00A0 = (\u2010)global::\u00A0.\u00A0.Default.KPIServerType
			};
			\u2025.\u00A0(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2051() + global::\u00A0.\u00A0.Default.Proxy, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
			string u00A2 = \u2013.\u00A0;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(13, 1);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2052());
			defaultInterpolatedStringHandler.AppendFormatted<\u2010>((\u2010)global::\u00A0.\u00A0.Default.KPIServerType);
			\u2025.\u00A0(u00A2, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
			try
			{
				\u2013.\u2002();
				string text = Path.Combine(\u2006.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2053());
				\u2011 u = \u2013.\u00A0(u00A, text);
				\u2025.\u00A0(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2054() + u.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
				\u2013.\u00A0 = (u == \u2011.NONE);
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2055() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005468 File Offset: 0x00003668
		public static \u2011 \u00A0(\u2009 A_0, \u200B A_1, string A_2 = "1")
		{
			\u2012 u = new \u2012(A_1, A_2);
			return \u2013.\u00A0(A_0, u);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005484 File Offset: 0x00003684
		public static \u2011 \u00A0(\u2009 A_0, \u2012 A_1)
		{
			if (!\u2013.\u00A0)
			{
				return \u2011.NOT_INITIALIZED;
			}
			\u2013.\u1680? u = \u2013.\u2000(A_0, A_1);
			if (u != null)
			{
				\u2013.\u1680 valueOrDefault = u.GetValueOrDefault();
				string u00A = \u2013.\u00A0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(16, 4);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2056());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u00A0);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2057());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u1680);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2057());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u2000);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2057());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u2001);
				\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2058());
				try
				{
					\u2011 result = \u2013.\u00A0(valueOrDefault);
					\u2025.\u00A0(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2059() + result.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2058());
					return result;
				}
				catch (Exception ex)
				{
					\u2025.\u1680(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2055() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2058());
				}
				return \u2011.OTHERS;
			}
			return \u2011.OTHERS;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000055AC File Offset: 0x000037AC
		public static \u2011 \u1680(\u2009 A_0, \u200B A_1, string A_2)
		{
			\u2012 u = new \u2012(A_1, A_2);
			return \u2013.\u1680(A_0, u);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000055C8 File Offset: 0x000037C8
		public static \u2011 \u1680(\u2009 A_0, \u2012 A_1)
		{
			if (!\u2013.\u00A0)
			{
				return \u2011.NOT_INITIALIZED;
			}
			\u2013.\u1680? u = \u2013.\u2000(A_0, A_1);
			if (u != null)
			{
				\u2013.\u1680 valueOrDefault = u.GetValueOrDefault();
				string u00A = \u2013.\u00A0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(17, 4);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205A());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u00A0);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2057());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u1680);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2057());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u2000);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2057());
				defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.\u2001);
				\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205B());
				try
				{
					\u2011 result = \u2013.\u1680(valueOrDefault);
					\u2025.\u00A0(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205C() + result.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205B());
					return result;
				}
				catch (Exception ex)
				{
					\u2025.\u1680(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2055() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205B());
				}
				return \u2011.OTHERS;
			}
			return \u2011.OTHERS;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000056F0 File Offset: 0x000038F0
		private static string \u2000()
		{
			if (!string.IsNullOrEmpty(global::\u00A0.\u00A0.Default.Duid))
			{
				return global::\u00A0.\u00A0.Default.Duid;
			}
			string text = \u2013.\u2001();
			global::\u00A0.\u00A0.Default.Duid = text;
			global::\u00A0.\u00A0.Default.Save();
			return text;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005738 File Offset: 0x00003938
		private static string \u2001()
		{
			char[] array = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205D().ToCharArray();
			Random random = new Random();
			string text = Guid.NewGuid().ToString(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205E()).ToUpperInvariant().Substring(0, 13);
			IEnumerable<char> enumerable = array;
			string text2 = text;
			if (!Enumerable.Contains<char>(enumerable, text2.get_Chars(text2.Length - 1)))
			{
				char c = array[random.Next(array.Length)];
				ReadOnlySpan<char> readOnlySpan = text.Substring(0, text.Length - 1);
				char c2 = c;
				text = readOnlySpan + new ReadOnlySpan<char>(ref c2);
			}
			return text;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000057C0 File Offset: 0x000039C0
		public static string \u1680(string A_0)
		{
			try
			{
				IntPtr intPtr;
				\u2011 u = \u2013.\u00A0(out intPtr, A_0);
				string text = Marshal.PtrToStringAnsi(intPtr);
				Marshal.FreeCoTaskMem(intPtr);
				\u2025.\u00A0(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u205F() + u.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2060());
				return text ?? string.Empty;
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2013.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2061() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2060());
			}
			return string.Empty;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005850 File Offset: 0x00003A50
		private static \u2013.\u1680? \u2000(\u2009 A_0, \u2012 A_1)
		{
			\u2013.\u1680 u;
			if (\u2013.\u00A0.TryGetValue(A_0, ref u))
			{
				foreach (KeyValuePair<\u200B, string> keyValuePair in A_1.\u00A0())
				{
					string text = u.\u2001;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendFormatted<\u200B>(keyValuePair.Key);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2062());
					defaultInterpolatedStringHandler.AppendFormatted(keyValuePair.Value);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2063());
					u.\u2001 = text + defaultInterpolatedStringHandler.ToStringAndClear();
				}
				u.\u2001 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2064() + u.\u2001 + 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206A();
				return new \u2013.\u1680?(u);
			}
			return default(\u2013.\u1680?);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005930 File Offset: 0x00003B30
		private static void \u2002()
		{
			for (int i = 0; i < Enum.GetValues(typeof(\u2009)).Length; i++)
			{
				\u2013.\u00A0.Add((\u2009)i, new \u2013.\u1680(\u2013.\u1680, (\u2009)i, (\u200A)i, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032()));
			}
		}

		// Token: 0x040000E8 RID: 232
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206B();

		// Token: 0x040000E9 RID: 233
		private static string \u1680 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206C();

		// Token: 0x040000EA RID: 234
		private const string \u2000 = "KPILogger.dll";

		// Token: 0x040000EB RID: 235
		private static bool \u00A0 = false;

		// Token: 0x040000EC RID: 236
		private static readonly Dictionary<\u2009, \u2013.\u1680> \u00A0 = new Dictionary<\u2009, \u2013.\u1680>();

		// Token: 0x0200002F RID: 47
		[Nullable(0)]
		private struct \u00A0
		{
			// Token: 0x040000ED RID: 237
			public string \u00A0;

			// Token: 0x040000EE RID: 238
			public string \u1680;

			// Token: 0x040000EF RID: 239
			public string \u2000;

			// Token: 0x040000F0 RID: 240
			public string \u2001;

			// Token: 0x040000F1 RID: 241
			public string \u2002;

			// Token: 0x040000F2 RID: 242
			public string \u2003;

			// Token: 0x040000F3 RID: 243
			public string \u2004;

			// Token: 0x040000F4 RID: 244
			public string \u2005;

			// Token: 0x040000F5 RID: 245
			public \u2010 \u00A0;
		}

		// Token: 0x02000030 RID: 48
		[Nullable(0)]
		private struct \u1680
		{
			// Token: 0x06000124 RID: 292 RVA: 0x0000599E File Offset: 0x00003B9E
			public \u1680(string A_1, \u2009 A_2, \u200A A_3, string A_4 = "")
			{
				this.\u00A0 = A_1;
				this.\u1680 = A_2.ToString();
				this.\u2000 = A_3.ToString();
				this.\u2001 = A_4;
			}

			// Token: 0x040000F6 RID: 246
			public string \u00A0;

			// Token: 0x040000F7 RID: 247
			public string \u1680;

			// Token: 0x040000F8 RID: 248
			public string \u2000;

			// Token: 0x040000F9 RID: 249
			public string \u2001;
		}
	}
}
