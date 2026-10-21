using System;
using System.Globalization;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000136 RID: 310
	public static class LogUtil
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x000255D8 File Offset: 0x000237D8
		public static void Log(LogLevel level, string msg, params object[] objects)
		{
			if (LogUtil._level <= level)
			{
				string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
				CultureInfo cultureInfo = new CultureInfo(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.je());
				if (objects.Length != 0)
				{
					text = string.Concat(new string[]
					{
						level.ToString().ToUpper(cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjf(),
						DateTime.UtcNow.ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjg(), cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bds(),
						DateTime.UtcNow.ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjh(), cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bu(),
						DateTime.UtcNow.Millisecond.ToString(cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bji(),
						string.Format(cultureInfo, msg, objects)
					});
				}
				else
				{
					text = string.Concat(new string[]
					{
						level.ToString().ToUpper(cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjf(),
						DateTime.UtcNow.ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjg(), cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bds(),
						DateTime.UtcNow.ToString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjh(), cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bu(),
						DateTime.UtcNow.Millisecond.ToString(cultureInfo),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bji(),
						msg
					});
				}
				if (level <= LogLevel.Info)
				{
					UnityEngine.Debug.Log(text);
					return;
				}
				if (level <= LogLevel.Warning)
				{
					UnityEngine.Debug.LogWarning(text);
					return;
				}
				UnityEngine.Debug.LogError(text);
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00025748 File Offset: 0x00023948
		public static void Trace(string msg, params object[] objects)
		{
			LogUtil.Log(LogLevel.Trace, msg, objects);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00025752 File Offset: 0x00023952
		public static void Debug(string msg, params object[] objects)
		{
			LogUtil.Log(LogLevel.Debug, msg, objects);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002575C File Offset: 0x0002395C
		public static void Info(string msg, params object[] objects)
		{
			LogUtil.Log(LogLevel.Info, msg, objects);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00025766 File Offset: 0x00023966
		public static void Warning(string msg, params object[] objects)
		{
			LogUtil.Log(LogLevel.Warning, msg, objects);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00025770 File Offset: 0x00023970
		public static void Error(string msg, params object[] objects)
		{
			LogUtil.Log(LogLevel.Error, msg, objects);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002577A File Offset: 0x0002397A
		public static void Fatal(string msg, params object[] objects)
		{
			LogUtil.Log(LogLevel.Fatal, msg, objects);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025784 File Offset: 0x00023984
		public static void ObjLog(this object obj, LogLevel level, string msg, params object[] objects)
		{
			if (LogUtil._level <= level)
			{
				LogUtil.Log(level, obj.GetType().Name + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.be() + msg, objects);
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000257AB File Offset: 0x000239AB
		public static void Trace(this object obj, string msg, params object[] objects)
		{
			obj.ObjLog(LogLevel.Trace, msg, objects);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000257B6 File Offset: 0x000239B6
		public static void Debug(this object obj, string msg, params object[] objects)
		{
			obj.ObjLog(LogLevel.Debug, msg, objects);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000257C1 File Offset: 0x000239C1
		public static void Info(this object obj, string msg, params object[] objects)
		{
			obj.ObjLog(LogLevel.Info, msg, objects);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000257CC File Offset: 0x000239CC
		public static void Warning(this object obj, string msg, params object[] objects)
		{
			obj.ObjLog(LogLevel.Warning, msg, objects);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000257D7 File Offset: 0x000239D7
		public static void Error(this object obj, string msg, params object[] objects)
		{
			obj.ObjLog(LogLevel.Error, msg, objects);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000257E2 File Offset: 0x000239E2
		public static void Fatal(this object obj, string msg, params object[] objects)
		{
			obj.ObjLog(LogLevel.Fatal, msg, objects);
		}

		// Token: 0x040006B2 RID: 1714
		private static LogLevel _level = LogLevel.Warning;
	}
}
