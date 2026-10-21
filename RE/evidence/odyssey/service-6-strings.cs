// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\⁊.2.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using GameLauncher.Lib;
using GameLauncher.Lib.Structs;
using obf_3;

namespace obf_1
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	public class obf_110
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000A1F6 File Offset: 0x000083F6
		private obf_110()
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A209 File Offset: 0x00008409
		public static obf_110 Instance
		{
			get
			{
				if (obf_110.obf_1 == null)
				{
					obf_110.obf_1 = new obf_110();
				}
				return obf_110.obf_1;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A221 File Offset: 0x00008421
		public void obf_1()
		{
			obf_11.obf_1(obf_110.obf_1, "Init");
			this.obf_1 = new obf_110();
			this.obf_1 = new obf_168();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A248 File Offset: 0x00008448
		public static void obf_1(bool A_0)
		{
			obf_11.obf_1(obf_110.obf_1, "SetMonitor3D");
			if (A_0)
			{
				obf_94.obf_1(32, 20, 1);
				return;
			}
			obf_94.obf_1(32, 20, 0);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A273 File Offset: 0x00008473
		public void obf_3(bool A_1)
		{
			obf_11.obf_1(obf_110.obf_1, "SetFullScreenDetectionMode");
			if (A_1)
			{
				obf_171 u00A = this.obf_1;
				if (u00A == null)
				{
					return;
				}
				u00A.obf_1();
				return;
			}
			else
			{
				obf_171 u00A2 = this.obf_1;
				if (u00A2 == null)
				{
					return;
				}
				u00A2.obf_4();
				return;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A2A8 File Offset: 0x000084A8
		public void obf_3()
		{
			obf_171 u00A = this.obf_1;
			if (u00A == null)
			{
				return;
			}
			u00A.obf_3();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A2BC File Offset: 0x000084BC
		public void obf_1(IntPtr A_1, bool A_2)
		{
			obf_11.obf_1(obf_110.obf_1, "StartConversion");
			try
			{
				Process[] processesByName = Process.GetProcessesByName("Odyssey3DPlayer");
				for (int i = 0; i < processesByName.Length; i++)
				{
					processesByName[i].Kill();
				}
			}
			catch (Exception ex)
			{
				obf_11.obf_1(obf_110.obf_1, "Fail to kill Odyssey3DPlayer:" + ex.ToString(), "StartConversion");
			}
			obf_175 u00A = this.obf_1;
			if (u00A == null)
			{
				return;
			}
			u00A.obf_1(A_1, A_2);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A340 File Offset: 0x00008540
		public void obf_1(string A_1, string A_2, obf_176 A_3, string A_4)
		{
			obf_11.obf_1(obf_110.obf_1, "LaunchGame");
			obf_168 u00A = this.obf_1;
			if (u00A == null)
			{
				return;
			}
			u00A.obf_1(A_1, A_2, A_3, A_4);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A367 File Offset: 0x00008567
		public void obf_4()
		{
			obf_11.obf_1(obf_110.obf_1, "CancelLaunchGame");
			obf_168 u00A = this.obf_1;
			if (u00A == null)
			{
				return;
			}
			u00A.obf_1();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A388 File Offset: 0x00008588
		public void obf_1(DeviceEvent A_1)
		{
			obf_168 u00A = this.obf_1;
			if (u00A == null)
			{
				return;
			}
			u00A.obf_1(A_1);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A39B File Offset: 0x0000859B
		public List<IGame> obf_5()
		{
			if (this.obf_1 != null)
			{
				return this.obf_1.obf_3();
			}
			return new List<IGame>();
		}

		// Token: 0x040002A9 RID: 681
		[Nullable(2)]
		private obf_168 obf_1;

		// Token: 0x040002AA RID: 682
		[Nullable(2)]
		private obf_175 obf_1;

		// Token: 0x040002AB RID: 683
		private static readonly string obf_1 = "Odyssey3DHubController";

		// Token: 0x040002AC RID: 684
		private readonly obf_171 obf_1 = new obf_171();

		// Token: 0x040002AD RID: 685
		[Nullable(2)]
		private static obf_110 obf_1 = null;
	}
}
