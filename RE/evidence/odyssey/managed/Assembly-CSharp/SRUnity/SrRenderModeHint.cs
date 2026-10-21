using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using SimulatedReality;

namespace SRUnity
{
	// Token: 0x02000107 RID: 263
	public class SrRenderModeHint
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001E604 File Offset: 0x0001C804
		public SrRenderModeHint()
		{
			this.currentPreference = SrRenderModeHint.ERenderModePreference.LHS_Indifferent;
			Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary = SrRenderModeHint.triggers;
			SrRenderModeHint.ERenderModePreference key = this.currentPreference;
			int num = dictionary[key];
			dictionary[key] = num + 1;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001E63C File Offset: 0x0001C83C
		protected override void Finalize()
		{
			try
			{
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary = SrRenderModeHint.triggers;
				SrRenderModeHint.ERenderModePreference key = this.currentPreference;
				int num = dictionary[key];
				dictionary[key] = num - 1;
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001E680 File Offset: 0x0001C880
		public static void SetLensHintInstance(IntPtr instance)
		{
			SrRenderModeHint.lensHint = instance;
			if (SrRenderModeHint.lensHint != IntPtr.Zero)
			{
				SRDisplays.lensDisableHint(SrRenderModeHint.lensHint);
				SrRenderModeHint.currentGlobalState = false;
				SrRenderModeHint.UpdateGlobalStateAndLensHint();
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001E6AE File Offset: 0x0001C8AE
		public static bool ShouldRender3D()
		{
			return SrRenderModeHint.currentGlobalState;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		public void Prefer3D()
		{
			if (this.currentPreference != SrRenderModeHint.ERenderModePreference.LHS_3D)
			{
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary = SrRenderModeHint.triggers;
				SrRenderModeHint.ERenderModePreference key = this.currentPreference;
				int num = dictionary[key];
				dictionary[key] = num - 1;
				this.currentPreference = SrRenderModeHint.ERenderModePreference.LHS_3D;
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary2 = SrRenderModeHint.triggers;
				key = this.currentPreference;
				num = dictionary2[key];
				dictionary2[key] = num + 1;
				SrRenderModeHint.UpdateGlobalStateAndLensHint();
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001E714 File Offset: 0x0001C914
		public void Prefer2D()
		{
			if (this.currentPreference != SrRenderModeHint.ERenderModePreference.LHS_2D)
			{
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary = SrRenderModeHint.triggers;
				SrRenderModeHint.ERenderModePreference key = this.currentPreference;
				int num = dictionary[key];
				dictionary[key] = num - 1;
				this.currentPreference = SrRenderModeHint.ERenderModePreference.LHS_2D;
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary2 = SrRenderModeHint.triggers;
				key = this.currentPreference;
				num = dictionary2[key];
				dictionary2[key] = num + 1;
				SrRenderModeHint.UpdateGlobalStateAndLensHint();
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001E770 File Offset: 0x0001C970
		public void Force2D()
		{
			if (this.currentPreference != SrRenderModeHint.ERenderModePreference.LHS_Force2D)
			{
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary = SrRenderModeHint.triggers;
				SrRenderModeHint.ERenderModePreference key = this.currentPreference;
				int num = dictionary[key];
				dictionary[key] = num - 1;
				this.currentPreference = SrRenderModeHint.ERenderModePreference.LHS_Force2D;
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary2 = SrRenderModeHint.triggers;
				key = this.currentPreference;
				num = dictionary2[key];
				dictionary2[key] = num + 1;
				SrRenderModeHint.UpdateGlobalStateAndLensHint();
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public void BecomeIndifferent()
		{
			if (this.currentPreference != SrRenderModeHint.ERenderModePreference.LHS_Indifferent)
			{
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary = SrRenderModeHint.triggers;
				SrRenderModeHint.ERenderModePreference key = this.currentPreference;
				int num = dictionary[key];
				dictionary[key] = num - 1;
				this.currentPreference = SrRenderModeHint.ERenderModePreference.LHS_Indifferent;
				Dictionary<SrRenderModeHint.ERenderModePreference, int> dictionary2 = SrRenderModeHint.triggers;
				key = this.currentPreference;
				num = dictionary2[key];
				dictionary2[key] = num + 1;
				SrRenderModeHint.UpdateGlobalStateAndLensHint();
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001E828 File Offset: 0x0001CA28
		private static void UpdateGlobalStateAndLensHint()
		{
			bool flag = SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_Force2D] <= 0 && (SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_3D] > 0 || SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_2D] == 0);
			SRUtility.Trace(string.Concat(new string[]
			{
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.beq(),
				flag ? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bes() : <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ber(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bet(),
				SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_3D].ToString(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.beu(),
				SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_2D].ToString(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bev(),
				SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_Force2D].ToString(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bew(),
				SrRenderModeHint.triggers[SrRenderModeHint.ERenderModePreference.LHS_Indifferent].ToString(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bex()
			}));
			if (SrRenderModeHint.currentGlobalState != flag)
			{
				SrRenderModeHint.currentGlobalState = flag;
				if (SrRenderModeHint.lensHint != IntPtr.Zero)
				{
					if (SrRenderModeHint.currentGlobalState)
					{
						SRDisplays.lensEnableHint(SrRenderModeHint.lensHint);
						return;
					}
					SRDisplays.lensDisableHint(SrRenderModeHint.lensHint);
					return;
				}
				else
				{
					SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bey());
				}
			}
		}

		// Token: 0x040005C2 RID: 1474
		private static bool currentGlobalState;

		// Token: 0x040005C3 RID: 1475
		private static IntPtr lensHint;

		// Token: 0x040005C4 RID: 1476
		private static Dictionary<SrRenderModeHint.ERenderModePreference, int> triggers = new Dictionary<SrRenderModeHint.ERenderModePreference, int>
		{
			{
				SrRenderModeHint.ERenderModePreference.LHS_Indifferent,
				0
			},
			{
				SrRenderModeHint.ERenderModePreference.LHS_3D,
				0
			},
			{
				SrRenderModeHint.ERenderModePreference.LHS_2D,
				0
			},
			{
				SrRenderModeHint.ERenderModePreference.LHS_Force2D,
				0
			}
		};

		// Token: 0x040005C5 RID: 1477
		private SrRenderModeHint.ERenderModePreference currentPreference;

		// Token: 0x02000108 RID: 264
		private enum ERenderModePreference
		{
			// Token: 0x040005C7 RID: 1479
			LHS_Indifferent,
			// Token: 0x040005C8 RID: 1480
			LHS_3D,
			// Token: 0x040005C9 RID: 1481
			LHS_2D,
			// Token: 0x040005CA RID: 1482
			LHS_Force2D
		}
	}
}
