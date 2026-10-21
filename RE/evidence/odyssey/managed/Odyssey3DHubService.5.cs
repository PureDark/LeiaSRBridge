using System;
using System.IO;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Newtonsoft.Json;

namespace \u00A0
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	[JsonObject("UserAgreements")]
	internal class \u2002
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003CDF File Offset: 0x00001EDF
		internal static \u2002 Instance
		{
			get
			{
				if (\u2002.\u00A0 == null)
				{
					\u2002.\u00A0 = new \u2002();
				}
				return \u2002.\u00A0;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003CF7 File Offset: 0x00001EF7
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003CFF File Offset: 0x00001EFF
		public string IsEULAConfirm
		{
			get
			{
				return this.\u2000;
			}
			set
			{
				if (this.\u2000 != value)
				{
					this.\u2000 = value;
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003D16 File Offset: 0x00001F16
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003D1E File Offset: 0x00001F1E
		public string IsPNConfirm
		{
			get
			{
				return this.\u2001;
			}
			set
			{
				if (this.\u2001 != value)
				{
					this.\u2001 = value;
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003D35 File Offset: 0x00001F35
		public static bool \u00A0()
		{
			return \u2002.Instance.\u2000 == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2024();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D4B File Offset: 0x00001F4B
		public static bool \u1680()
		{
			return \u2002.Instance.\u2001 == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2024();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D64 File Offset: 0x00001F64
		public static void \u2000()
		{
			try
			{
				\u2002 u = \u2029.\u00A0<\u2002>(\u2002.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2025());
				if (u != null)
				{
					\u2002.Instance.\u2000 = u.\u2000;
					\u2002.Instance.\u2001 = u.\u2001;
				}
			}
			catch (Exception ex)
			{
				\u2025.\u1680(\u2002.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2027() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2028());
			}
		}

		// Token: 0x0400004F RID: 79
		private static readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202B();

		// Token: 0x04000050 RID: 80
		private static readonly string \u1680 = Path.Combine(\u2006.\u1680, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202C());

		// Token: 0x04000051 RID: 81
		[Nullable(2)]
		private static \u2002 \u00A0;

		// Token: 0x04000052 RID: 82
		private string \u2000 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2029();

		// Token: 0x04000053 RID: 83
		private string \u2001 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u202A();
	}
}
