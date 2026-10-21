using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200011F RID: 287
	[RequireComponent(typeof(Camera))]
	public class CameraUIOverlay : MonoBehaviour
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00021F63 File Offset: 0x00020163
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00021F6B File Offset: 0x0002016B
		public RenderTexture UIRenderTexture
		{
			get
			{
				return this.uiRenderTexture;
			}
			set
			{
				this.uiRenderTexture = value;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00021F74 File Offset: 0x00020174
		private void Start()
		{
			this.OverlayMaterial = new Material(Shader.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhb()));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00021F8B File Offset: 0x0002018B
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.OverlayMaterial.SetTexture(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhc(), this.UIRenderTexture);
			Graphics.Blit(src, dest, this.OverlayMaterial);
		}

		// Token: 0x04000636 RID: 1590
		private RenderTexture uiRenderTexture;

		// Token: 0x04000637 RID: 1591
		private Material OverlayMaterial;
	}
}
