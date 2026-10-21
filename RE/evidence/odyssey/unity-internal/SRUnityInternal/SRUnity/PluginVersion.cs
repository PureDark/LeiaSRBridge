using System;

namespace SRUnity
{
	// Token: 0x02000013 RID: 19
	public static class PluginVersion
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000020FA File Offset: 0x000002FA
		public static string GetVersion()
		{
			return PluginVersion.pluginVersion;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002101 File Offset: 0x00000301
		public static string GetFullVersion()
		{
			return PluginVersion.pluginVersion + "." + PluginVersion.pluginVersionTag;
		}

		// Token: 0x0400003C RID: 60
		private static string pluginVersion = "2.5.1";

		// Token: 0x0400003D RID: 61
		private static string pluginVersionTag = "preview-27eff24";
	}
}
