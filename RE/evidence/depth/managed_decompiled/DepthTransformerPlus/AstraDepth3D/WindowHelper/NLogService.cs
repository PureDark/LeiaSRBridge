using System;
using NLog;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200003B RID: 59
	public class NLogService
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public static void WriteLog(string content)
		{
			if (NLogService._logger != null)
			{
				NLogService._logger.Info(content);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public static void WriteLine(string content)
		{
		}

		// Token: 0x0400018C RID: 396
		private static Logger _logger = LogManager.GetLogger("LogFile");

		// Token: 0x0400018D RID: 397
		private static Logger _logger2 = LogManager.GetLogger("Consle");
	}
}
