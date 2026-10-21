using System;

namespace SRUnity
{
	// Token: 0x020000FC RID: 252
	public abstract class ISimulatedRealityModule
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0000238E File Offset: 0x0000058E
		public virtual void PreInitModule()
		{
		}

		// Token: 0x060006A8 RID: 1704
		public abstract void InitModule();

		// Token: 0x060006A9 RID: 1705
		public abstract void DestroyModule();

		// Token: 0x060006AA RID: 1706
		public abstract void UpdateModule();
	}
}
