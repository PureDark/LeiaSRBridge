using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200012E RID: 302
	public class CameraCalculatedParams
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0002499E File Offset: 0x00022B9E
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x000249A6 File Offset: 0x00022BA6
		public float ScreenHalfHeight { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000249AF File Offset: 0x00022BAF
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x000249B7 File Offset: 0x00022BB7
		public float ScreenHalfWidth { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000249C0 File Offset: 0x00022BC0
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000249C8 File Offset: 0x00022BC8
		public float EmissionRescalingFactor { get; private set; }

		// Token: 0x06000894 RID: 2196 RVA: 0x000249D4 File Offset: 0x00022BD4
		public static float GetViewportAspectFor(Camera renderingCamera)
		{
			return renderingCamera.pixelRect.width * 1f / renderingCamera.pixelRect.height;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00024A04 File Offset: 0x00022C04
		public static float GetViewportAspectFor(Head renderingCamera)
		{
			return CameraCalculatedParams.GetViewportAspectFor(renderingCamera.headcamera);
		}
	}
}
