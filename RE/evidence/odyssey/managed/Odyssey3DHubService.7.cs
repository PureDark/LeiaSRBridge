using System;
using System.IO;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Newtonsoft.Json;

namespace \u00A0
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	[JsonObject("DevConfig")]
	internal class \u2004
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003EDC File Offset: 0x000020DC
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003EE4 File Offset: 0x000020E4
		public int KPIServerType
		{
			get
			{
				return this.\u00A0;
			}
			set
			{
				if (this.\u00A0 != value)
				{
					this.\u00A0 = value;
					global::\u00A0.\u00A0.Default.KPIServerType = this.\u00A0;
					global::\u00A0.\u00A0.Default.Save();
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003F10 File Offset: 0x00002110
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003F18 File Offset: 0x00002118
		public string Proxy
		{
			get
			{
				return this.\u00A0;
			}
			set
			{
				if (this.\u00A0 != value)
				{
					this.\u00A0 = value;
					global::\u00A0.\u00A0.Default.Proxy = this.\u00A0;
					global::\u00A0.\u00A0.Default.Save();
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003F49 File Offset: 0x00002149
		public static \u2004 Instance
		{
			get
			{
				if (\u2004.\u00A0 == null)
				{
					\u2004.\u00A0 = new \u2004();
				}
				return \u2004.\u00A0;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003F64 File Offset: 0x00002164
		public static void \u00A0()
		{
			if (File.Exists(\u2006.\u2000))
			{
				try
				{
					\u2004 u = \u2029.\u00A0<\u2004>(\u2006.\u2000, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202D());
					if (u != null)
					{
						global::\u00A0.\u00A0.Default.KPIServerType = u.KPIServerType;
						global::\u00A0.\u00A0.Default.Proxy = u.Proxy;
						global::\u00A0.\u00A0.Default.Save();
					}
				}
				catch (Exception ex)
				{
					\u2025.\u1680(\u2004.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202E() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202F());
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003FF0 File Offset: 0x000021F0
		public static void \u1680()
		{
			\u2029.\u00A0(\u2004.Instance, \u2006.\u2000, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202D());
		}

		// Token: 0x04000057 RID: 87
		private int \u00A0 = 2;

		// Token: 0x04000058 RID: 88
		private string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2032();

		// Token: 0x04000059 RID: 89
		private static readonly string \u1680 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2035();

		// Token: 0x0400005A RID: 90
		[Nullable(2)]
		private static \u2004 \u00A0;
	}
}
