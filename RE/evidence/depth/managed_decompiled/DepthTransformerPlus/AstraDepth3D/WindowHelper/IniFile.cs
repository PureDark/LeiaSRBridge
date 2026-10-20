using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200002F RID: 47
	public class IniFile
	{
		// Token: 0x060001CA RID: 458
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

		// Token: 0x060001CB RID: 459
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);

		// Token: 0x060001CC RID: 460 RVA: 0x0000D898 File Offset: 0x0000BA98
		public IniFile(string iniPath)
		{
			this.path = iniPath;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public string Read(string section, string key, string defaultValue = "")
		{
			StringBuilder retVal = new StringBuilder(255);
			IniFile.GetPrivateProfileString(section, key, defaultValue, retVal, 255, this.path);
			return retVal.ToString();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D8DC File Offset: 0x0000BADC
		public float ReadFloat(string section, string key, float defaultValue)
		{
			StringBuilder retVal = new StringBuilder(255);
			IniFile.GetPrivateProfileString(section, key, "", retVal, 255, this.path);
			float value;
			if (float.TryParse(retVal.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out value))
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D92C File Offset: 0x0000BB2C
		public float ReadInt(string section, string key, int defaultValue)
		{
			StringBuilder retVal = new StringBuilder(255);
			IniFile.GetPrivateProfileString(section, key, "", retVal, 255, this.path);
			int value;
			if (int.TryParse(retVal.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out value))
			{
				return (float)value;
			}
			return (float)defaultValue;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D97B File Offset: 0x0000BB7B
		public void Write(string section, string key, string value)
		{
			IniFile.WritePrivateProfileString(section, key, value, this.path);
		}

		// Token: 0x040000DF RID: 223
		private string path;
	}
}
