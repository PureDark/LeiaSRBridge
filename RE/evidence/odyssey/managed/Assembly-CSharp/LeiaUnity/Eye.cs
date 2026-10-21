using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000123 RID: 291
	[ExecuteInEditMode]
	public class Eye : MonoBehaviour
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000222F8 File Offset: 0x000204F8
		public Camera Eyecamera
		{
			get
			{
				if (this.eyecamera == null)
				{
					this.eyecamera = base.transform.GetComponent<Camera>();
				}
				if (this.eyecamera == null)
				{
					this.eyecamera = base.transform.gameObject.AddComponent<Camera>();
				}
				return this.eyecamera;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0002234E File Offset: 0x0002054E
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0002236A File Offset: 0x0002056A
		public RenderTexture TargetTexture
		{
			get
			{
				if (this.Eyecamera)
				{
					return this.Eyecamera.targetTexture;
				}
				return null;
			}
			set
			{
				if (this.Eyecamera)
				{
					this.Eyecamera.targetTexture = value;
				}
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00022385 File Offset: 0x00020585
		private void Start()
		{
			if (this.Eyecamera == null)
			{
				this.eyecamera = base.transform.gameObject.AddComponent<Camera>();
			}
			this.eyecamera.enabled = false;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000223B7 File Offset: 0x000205B7
		public void SetTextureParams(int width, int height)
		{
			if (this.Eyecamera == null)
			{
				return;
			}
			if (this.Eyecamera.targetTexture == null)
			{
				this.TargetTexture = Eye.CreateRenderTexture(width, height, this.leiaDisplay.AntiAliasingLevel);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000223F4 File Offset: 0x000205F4
		private static RenderTexture CreateRenderTexture(int width, int height, int antiAliasingLevel)
		{
			width = ((width > 0) ? width : 1920);
			height = ((height > 0) ? height : 1200);
			antiAliasingLevel = ((antiAliasingLevel > 0) ? antiAliasingLevel : 1);
			RenderTexture renderTexture = new RenderTexture(width, height, 24);
			renderTexture.antiAliasing = antiAliasingLevel;
			renderTexture.Create();
			return renderTexture;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00022444 File Offset: 0x00020644
		public void Release()
		{
			if (this.TargetTexture != null)
			{
				if (Application.isPlaying)
				{
					this.TargetTexture.Release();
					Object.Destroy(this.TargetTexture);
				}
				else
				{
					this.TargetTexture.Release();
					Object.DestroyImmediate(this.TargetTexture);
				}
				this.TargetTexture = null;
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002249C File Offset: 0x0002069C
		public void EyeUpdate()
		{
			if (this.leiaDisplay == null)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			if (!this.IsCamParameterCopied)
			{
				LeiaUtils.CopyCameraParameters(this.leiaDisplay.HeadCamera, this.Eyecamera);
				this.IsCamParameterCopied = true;
			}
			this.Eyecamera.projectionMatrix = this.leiaDisplay.GetProjectionMatrixForCamera(this.Eyecamera, base.transform.localPosition, true);
			if (Application.isPlaying)
			{
				this.Eyecamera.enabled = true;
				this.Eyecamera.cullingMask = this.leiaDisplay.ViewersHead.CullingMask;
			}
			else
			{
				this.Eyecamera.enabled = false;
			}
			this.eyecamera.enabled = false;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002255D File Offset: 0x0002075D
		private void OnDrawGizmos()
		{
			this.leiaDisplay.DrawFrustum(this.eyecamera);
		}

		// Token: 0x04000640 RID: 1600
		public LeiaDisplay leiaDisplay;

		// Token: 0x04000641 RID: 1601
		private Camera eyecamera;

		// Token: 0x04000642 RID: 1602
		private bool IsCamParameterCopied;

		// Token: 0x04000643 RID: 1603
		public Vector2 offset;
	}
}
