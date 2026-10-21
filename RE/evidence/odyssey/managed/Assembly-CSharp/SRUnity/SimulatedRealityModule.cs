using System;

namespace SRUnity
{
	// Token: 0x020000FD RID: 253
	public abstract class SimulatedRealityModule<ModuleType> : ISimulatedRealityModule where ModuleType : class, new()
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001D4B6 File Offset: 0x0001B6B6
		public static ModuleType Instance
		{
			get
			{
				if (SimulatedRealityModule<ModuleType>._instance == null)
				{
					SimulatedRealityModule<ModuleType>._instance = Activator.CreateInstance<ModuleType>();
				}
				return SimulatedRealityModule<ModuleType>._instance;
			}
		}

		// Token: 0x0400059A RID: 1434
		private static ModuleType _instance;
	}
}
