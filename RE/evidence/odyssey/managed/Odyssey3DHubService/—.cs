using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	internal class \u2014
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000059D8 File Offset: 0x00003BD8
		[NullableContext(2)]
		[CompilerGenerated]
		public static void \u00A0(Action A_0)
		{
			Action action = \u2014.\u00A0;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, A_0);
				action = Interlocked.CompareExchange<Action>(ref \u2014.\u00A0, action3, action2);
			}
			while (action != action2);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005A0C File Offset: 0x00003C0C
		[NullableContext(2)]
		[CompilerGenerated]
		public static void \u1680(Action A_0)
		{
			Action action = \u2014.\u00A0;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, A_0);
				action = Interlocked.CompareExchange<Action>(ref \u2014.\u00A0, action3, action2);
			}
			while (action != action2);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005A40 File Offset: 0x00003C40
		public static void \u00A0()
		{
			try
			{
				\u2025.\u00A0(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
				\u2014.\u1680();
				CultureInfo.CurrentCulture.ClearCachedData();
				string text = Path.Combine(\u2006.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206D());
				if (File.Exists(text))
				{
					try
					{
						using (JsonDocument jsonDocument = JsonDocument.Parse(File.ReadAllText(text), default(JsonDocumentOptions)))
						{
							JsonElement jsonElement;
							JsonElement jsonElement2;
							if (jsonDocument.RootElement.TryGetProperty(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u205F(), out jsonElement) && jsonElement.TryGetProperty(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206E(), out jsonElement2))
							{
								string text2 = jsonElement2.GetString() ?? string.Empty;
								if (!string.IsNullOrEmpty(text2) && \u2014.\u00A0.Contains(text2))
								{
									\u2025.\u00A0(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206F() + text2, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
									\u2014.\u1680 = text2;
								}
								else
								{
									\u2025.\u00A0(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u3000() + text2, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
								}
							}
						}
						goto IL_18A;
					}
					catch (Exception ex)
					{
						\u2025.\u1680(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u00A0() + ex.Message, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
						goto IL_18A;
					}
				}
				CultureInfo.CurrentCulture.ClearCachedData();
				if (\u2014.\u00A0.Contains(CultureInfo.CurrentCulture.Name))
				{
					\u2025.\u00A0(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u1680() + CultureInfo.CurrentCulture.Name, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
					\u2014.\u1680 = CultureInfo.CurrentCulture.Name;
				}
				else
				{
					\u2025.\u00A0(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2000() + CultureInfo.CurrentCulture.Name, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
				}
				IL_18A:
				\u2003.Instance.SettingsVM.Language = \u2014.\u1680;
				\u2014.\u00A0 = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2001()), null);
			}
			catch (Exception ex2)
			{
				\u2025.\u1680(\u2014.\u00A0, ex2.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005C68 File Offset: 0x00003E68
		public static void \u00A0(string A_0)
		{
			if (!\u2014.\u00A0.Contains(A_0))
			{
				\u2025.\u1680(\u2014.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2002() + A_0 + 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2003(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2004());
				return;
			}
			\u2014.\u1680 = A_0;
			Action u00A = \u2014.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.Invoke();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005CB8 File Offset: 0x00003EB8
		private static void \u1680()
		{
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2005());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206C());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2006());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2007());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2008());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2009());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u200A());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u200B());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2010());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2011());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2012());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2013());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2014());
			\u2014.\u00A0.Add(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2015());
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005D98 File Offset: 0x00003F98
		public static string \u1680(string A_0)
		{
			try
			{
				Dictionary<string, string> dictionary = \u2014.\u00A0[A_0];
				return string.IsNullOrEmpty(dictionary[\u2014.\u1680]) ? dictionary[0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206C()] : dictionary[\u2014.\u1680];
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2014.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2022());
			}
			return string.Empty;
		}

		// Token: 0x040000FA RID: 250
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2024();

		// Token: 0x040000FB RID: 251
		private static List<string> \u00A0 = new List<string>();

		// Token: 0x040000FC RID: 252
		private static string \u1680 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u206C();

		// Token: 0x040000FD RID: 253
		[Nullable(2)]
		[CompilerGenerated]
		private static Action \u00A0;

		// Token: 0x040000FE RID: 254
		private static Dictionary<string, Dictionary<string, string>> \u00A0 = new Dictionary<string, Dictionary<string, string>>();
	}
}
