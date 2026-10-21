using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using GameLauncher.Lib;
using GameLauncher.Lib.Structs;
using \u1680;

namespace \u00A0
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	public class \u204A
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000A1F6 File Offset: 0x000083F6
		private \u204A()
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A209 File Offset: 0x00008409
		public static \u204A Instance
		{
			get
			{
				if (\u204A.\u00A0 == null)
				{
					\u204A.\u00A0 = new \u204A();
				}
				return \u204A.\u00A0;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A221 File Offset: 0x00008421
		public void \u00A0()
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u206D());
			this.\u00A0 = new \u204A();
			this.\u00A0 = new \u2049();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A248 File Offset: 0x00008448
		public static void \u00A0(bool A_0)
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2054());
			if (A_0)
			{
				\u202C.\u00A0(32, 20, 1);
				return;
			}
			\u202C.\u00A0(32, 20, 0);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A273 File Offset: 0x00008473
		public void \u1680(bool A_1)
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2055());
			if (A_1)
			{
				\u2033 u00A = this.\u00A0;
				if (u00A == null)
				{
					return;
				}
				u00A.\u00A0();
				return;
			}
			else
			{
				\u2033 u00A2 = this.\u00A0;
				if (u00A2 == null)
				{
					return;
				}
				u00A2.\u2000();
				return;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A2A8 File Offset: 0x000084A8
		public void \u1680()
		{
			\u2033 u00A = this.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.\u1680();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A2BC File Offset: 0x000084BC
		public void \u00A0(IntPtr A_1, bool A_2)
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2056());
			try
			{
				Process[] processesByName = Process.GetProcessesByName(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204C());
				for (int i = 0; i < processesByName.Length; i++)
				{
					processesByName[i].Kill();
				}
			}
			catch (Exception ex)
			{
				\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2057() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2056());
			}
			\u2048 u00A = this.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.\u00A0(A_1, A_2);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A340 File Offset: 0x00008540
		public void \u00A0(string A_1, string A_2, \u2036 A_3, string A_4)
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2058());
			\u2049 u00A = this.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.\u00A0(A_1, A_2, A_3, A_4);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A367 File Offset: 0x00008567
		public void \u2000()
		{
			\u2025.\u00A0(\u204A.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2059());
			\u2049 u00A = this.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.\u00A0();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A388 File Offset: 0x00008588
		public void \u00A0(DeviceEvent A_1)
		{
			\u2049 u00A = this.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.\u00A0(A_1);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A39B File Offset: 0x0000859B
		public List<IGame> \u2001()
		{
			if (this.\u00A0 != null)
			{
				return this.\u00A0.\u1680();
			}
			return new List<IGame>();
		}

		// Token: 0x040002A9 RID: 681
		[Nullable(2)]
		private \u2049 \u00A0;

		// Token: 0x040002AA RID: 682
		[Nullable(2)]
		private \u2048 \u00A0;

		// Token: 0x040002AB RID: 683
		private static readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u205A();

		// Token: 0x040002AC RID: 684
		private readonly \u2033 \u00A0 = new \u2033();

		// Token: 0x040002AD RID: 685
		[Nullable(2)]
		private static \u204A \u00A0 = null;
	}
}
