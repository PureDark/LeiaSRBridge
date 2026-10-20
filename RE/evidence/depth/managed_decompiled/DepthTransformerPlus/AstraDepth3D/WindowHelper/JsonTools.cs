using System;
using System.IO;
using System.Text;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000030 RID: 48
	public class JsonTools
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000D98C File Offset: 0x0000BB8C
		public static string GetJsonFile(string filepath)
		{
			string json = string.Empty;
			using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
				{
					json = sr.ReadToEnd().ToString();
				}
			}
			return json;
		}
	}
}
