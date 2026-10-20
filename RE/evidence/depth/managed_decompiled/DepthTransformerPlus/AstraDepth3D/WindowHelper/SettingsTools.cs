using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000041 RID: 65
	internal class SettingsTools
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000FE14 File Offset: 0x0000E014
		public static void ResetLanguage(int langIndex)
		{
			switch (langIndex)
			{
			case 0:
				SettingsTools.SwitchLanguage(CultureInfo.CurrentUICulture.Name);
				return;
			case 1:
				SettingsTools.SwitchLanguage("zh-CN");
				return;
			case 2:
				SettingsTools.SwitchLanguage("en-US");
				return;
			case 3:
				SettingsTools.SwitchLanguage("de-DE");
				return;
			case 4:
				SettingsTools.SwitchLanguage("es-ES");
				return;
			case 5:
				SettingsTools.SwitchLanguage("fr-FR");
				return;
			case 6:
				SettingsTools.SwitchLanguage("ja-JP");
				return;
			case 7:
				SettingsTools.SwitchLanguage("ko-KR");
				return;
			default:
				SettingsTools.SwitchLanguage("en-US");
				return;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		public static void SwitchLanguage(string culture_name)
		{
			string defaultLanguage = "en-US";
			if (culture_name == "zh-TW" || culture_name == "zh-HK")
			{
				defaultLanguage = "zh-CN";
			}
			Uri language_uri = new Uri("pack://application:,,,/Language/" + culture_name + ".xaml");
			bool is_exist = false;
			try
			{
				StreamResourceInfo streamInfo = Application.GetResourceStream(language_uri);
				if (streamInfo != null || streamInfo.Stream != null)
				{
					is_exist = true;
				}
			}
			catch (IOException e)
			{
				NLogService.WriteLog("MainWindow|SwitchLanguage:" + e.Message);
			}
			Application.Current.Resources.MergedDictionaries[0].Source = (is_exist ? language_uri : new Uri("pack://application:,,,/Language/" + defaultLanguage + ".xaml"));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000FF70 File Offset: 0x0000E170
		public static bool isShortcutCanbeRegister(int modifier, int key)
		{
			bool result;
			try
			{
				if (modifier == 9 && key > 0)
				{
					result = true;
				}
				else if (User32.RegisterHotKey(IntPtr.Zero, 9999, (uint)modifier, (uint)key))
				{
					User32.UnregisterHotKey(IntPtr.Zero, 9999);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		public static int GetVirtualKeyFromModifierKeys(int modifier)
		{
			if (modifier == 1)
			{
				return 18;
			}
			if (modifier == 2)
			{
				return 17;
			}
			if (modifier == 9)
			{
				return 9;
			}
			return 0;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		public static int GetModifierKeysFromVirtualKey(int virtualKey)
		{
			if (virtualKey == 18)
			{
				return 1;
			}
			if (virtualKey == 17)
			{
				return 2;
			}
			if (virtualKey == 9)
			{
				return 9;
			}
			return 0;
		}
	}
}
