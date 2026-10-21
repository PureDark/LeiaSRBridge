using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000062 RID: 98
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2029
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00006894 File Offset: 0x00004A94
		public static void \u00A0(object A_0, string A_1, string A_2)
		{
			try
			{
				\u2025.\u00A0(\u2029.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2033());
				string text = JsonSerializer.Serialize<object>(A_0, new JsonSerializerOptions
				{
					WriteIndented = true,
					Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				});
				text = string.Concat(new string[]
				{
					0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2036(),
					A_2,
					0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u203E(),
					text,
					0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2047()
				});
				File.WriteAllText(A_1, text);
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2029.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2033());
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000692C File Offset: 0x00004B2C
		[return: Nullable(2)]
		public static T \u00A0<[Nullable(2)] T>(string A_0, string A_1)
		{
			\u2025.\u00A0(\u2029.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2048());
			T result = default(T);
			try
			{
				if (File.Exists(A_0))
				{
					using (FileStream fileStream = new FileStream(A_0, 3, 1, 1))
					{
						Dictionary<string, T> dictionary = JsonSerializer.Deserialize<Dictionary<string, T>>(new StreamReader(fileStream).ReadToEnd(), null);
						T t;
						if (dictionary != null && dictionary.TryGetValue(A_1, ref t))
						{
							result = t;
						}
					}
				}
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2029.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2048());
			}
			return result;
		}

		// Token: 0x04000206 RID: 518
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u2049();
	}
}
