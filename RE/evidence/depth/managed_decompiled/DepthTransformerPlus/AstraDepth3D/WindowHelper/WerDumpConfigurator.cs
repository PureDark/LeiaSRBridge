using System;
using System.IO;
using Microsoft.Win32;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200002D RID: 45
	internal class WerDumpConfigurator
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000D434 File Offset: 0x0000B634
		public static void ConfigureLocalDumps(string exeName, string dumpFolder = null, int maxDumpCount = 5, int dumpType = 2)
		{
			string keyPath = "Software\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps\\" + exeName;
			using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath, true))
			{
				if (key == null)
				{
					throw new InvalidOperationException("无法创建注册表项: " + keyPath);
				}
				if (dumpFolder == null)
				{
					dumpFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CrashDumps", exeName);
				}
				Directory.CreateDirectory(dumpFolder);
				key.SetValue("DumpFolder", dumpFolder, RegistryValueKind.ExpandString);
				key.SetValue("DumpCount", maxDumpCount, RegistryValueKind.DWord);
				key.SetValue("DumpType", dumpType, RegistryValueKind.DWord);
				NLogService.WriteLog("[WER] LocalDumps 已配置:");
				NLogService.WriteLog("  EXE:       " + exeName);
				NLogService.WriteLog("  Dump目录:  " + dumpFolder);
				NLogService.WriteLog(string.Format("  最大数量:  {0}", maxDumpCount));
				NLogService.WriteLog("  Dump类型:  " + ((dumpType == 2) ? "Full" : ((dumpType == 1) ? "Mini" : "Custom")));
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000D544 File Offset: 0x0000B744
		public static void RemoveLocalDumps(string exeName)
		{
			string keyPath = "Software\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps\\" + exeName;
			Registry.CurrentUser.DeleteSubKeyTree(keyPath, false);
			NLogService.WriteLog("[WER] LocalDumps 配置已移除: " + exeName);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D57C File Offset: 0x0000B77C
		public static bool IsConfigured(string exeName)
		{
			string keyPath = "Software\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps\\" + exeName;
			bool result;
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
			{
				result = (key != null && key.GetValue("DumpFolder") != null);
			}
			return result;
		}
	}
}
