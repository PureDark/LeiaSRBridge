using System;
using Microsoft.Win32;

namespace SRUnity
{
	// Token: 0x02000012 RID: 18
	public class SRInternalPlatform
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000020D7 File Offset: 0x000002D7
		public static string GetSimulatedRealityRuntimePath()
		{
			return (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Dimenco\\Simulated Reality", "", "");
		}
	}
}
