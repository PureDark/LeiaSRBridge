using System;
using UnityEngine;

namespace SRUnity
{
	// Token: 0x0200010E RID: 270
	[ExecuteInEditMode]
	internal class CompositorCameraHook : MonoBehaviour
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x0001FAF2 File Offset: 0x0001DCF2
		private void OnRenderImage(RenderTexture Source, RenderTexture Target)
		{
			if (SystemInfo.graphicsDeviceType == 18)
			{
				SimulatedRealityModule<SRRender>.Instance.GetCompositor().renderToTargetDX12(Source, Target);
				return;
			}
			SimulatedRealityModule<SRRender>.Instance.GetCompositor().renderToTarget(Target);
		}
	}
}
