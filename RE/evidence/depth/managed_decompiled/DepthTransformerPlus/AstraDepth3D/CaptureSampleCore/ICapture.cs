using System;
using AstraDepth3D.EventBus;
using Windows.UI.Composition;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x02000052 RID: 82
	internal interface ICapture : IDisposable
	{
		// Token: 0x06000360 RID: 864
		void StartCapture();

		// Token: 0x06000361 RID: 865
		void SetEventBus(LocalEventPublisher publisher);

		// Token: 0x06000362 RID: 866
		ICompositionSurface CreateSurface(Compositor compositor);

		// Token: 0x06000363 RID: 867
		void SetParax(float parax, float zerpPlane);

		// Token: 0x06000364 RID: 868
		LocalEnumeration.InterWaveType GetCurrentInterwaveStatus();

		// Token: 0x06000365 RID: 869
		void ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType status);
	}
}
