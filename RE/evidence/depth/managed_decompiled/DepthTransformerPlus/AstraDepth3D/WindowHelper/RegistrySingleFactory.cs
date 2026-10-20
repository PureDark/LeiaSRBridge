using System;
using Microsoft.Win32;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200003D RID: 61
	internal class RegistrySingleFactory
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public static void WriteRegistry(string valueName, string value)
		{
			using (RegistryKey key = Registry.LocalMachine.CreateSubKey(RegistrySingleFactory.keyPath))
			{
				key.SetValue(valueName, value);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		public static string ReadRegistry(string valueName)
		{
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey(RegistrySingleFactory.keyPath))
			{
				object value = (key != null) ? key.GetValue(valueName) : null;
				if (value != null)
				{
					return value.ToString();
				}
			}
			return null;
		}

		// Token: 0x04000192 RID: 402
		public static string keyPath = "SOFTWARE\\BOE";

		// Token: 0x04000193 RID: 403
		public static string valueName = "MACHINE_MCU_UUID";

		// Token: 0x04000194 RID: 404
		public static string value = "";

		// Token: 0x04000195 RID: 405
		public static string valueNameCalidata = "Calidata";
	}
}
