using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200002E RID: 46
	public class ComRegistrar
	{
		// Token: 0x060001C3 RID: 451
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int GetLongPathName(string shortPath, StringBuilder longPath, int bufferSize);

		// Token: 0x060001C4 RID: 452 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public static bool IsComRegisteredByProgId(string progId)
		{
			bool result;
			try
			{
				result = (Type.GetTypeFromProgID(progId) != null);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D610 File Offset: 0x0000B810
		public static bool IsComRegisteredByClsid(string clsid)
		{
			bool result;
			try
			{
				using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("CLSID\\" + clsid))
				{
					result = (key != null);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("检测COM组件注册过程中发生错误: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D67C File Offset: 0x0000B87C
		public static bool IsComRegisteredByClsidWithPath(string clsid)
		{
			bool result;
			try
			{
				using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("CLSID\\" + clsid + "\\InprocServer32"))
				{
					if (key == null)
					{
						result = false;
					}
					else
					{
						string path = key.GetValue(null) as string;
						if (string.IsNullOrEmpty(path))
						{
							result = false;
						}
						else
						{
							string comPath = ComRegistrar.ConvertShortPathToLongPath(path);
							string currentDllPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "eyetracking\\FaceTrackingManagerService.dll");
							NLogService.WriteLog("COM register path:" + comPath + ";\n current Dll Path:" + currentDllPath);
							result = (comPath.Equals(currentDllPath) || comPath.Contains("AstraDepth3D") || comPath.Contains("ASTRAD~1"));
						}
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("检测COM组件注册过程中发生错误: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000D770 File Offset: 0x0000B970
		public static string ConvertShortPathToLongPath(string shortPath)
		{
			if (string.IsNullOrEmpty(shortPath))
			{
				return shortPath;
			}
			if (!shortPath.Contains("~"))
			{
				return shortPath;
			}
			StringBuilder longPath = new StringBuilder(255);
			if (ComRegistrar.GetLongPathName(shortPath, longPath, longPath.Capacity) > 0)
			{
				return longPath.ToString();
			}
			return shortPath;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		public static void RegisterComDll(string dllPath)
		{
			Process process = new Process();
			process.StartInfo.FileName = "regsvr32.exe";
			process.StartInfo.Arguments = "/s \"" + dllPath + "\"";
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.Verb = "runas";
			try
			{
				process.Start();
				process.WaitForExit();
				if (process.ExitCode != 0)
				{
					throw new Exception(string.Format("注册失败，错误代码：{0}", process.ExitCode));
				}
				NLogService.WriteLog("注册COM组件成功");
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("注册过程中发生错误: " + ex.Message);
			}
			finally
			{
				process.Dispose();
			}
		}
	}
}
