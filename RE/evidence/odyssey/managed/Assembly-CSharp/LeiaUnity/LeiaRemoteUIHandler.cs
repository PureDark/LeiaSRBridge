using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000121 RID: 289
	public class LeiaRemoteUIHandler : MonoBehaviour
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00021FB0 File Offset: 0x000201B0
		public void HandleScreenSpaceUI()
		{
			this.CreateUICamera();
			this.SetupRenderTexture();
			this.HandleOverlayUI();
			this.OverlayUIToLeiaViews();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00021FCC File Offset: 0x000201CC
		private void CreateUICamera()
		{
			GameObject gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhd());
			gameObject.transform.SetParent(Object.FindObjectOfType<LeiaDisplay>().transform);
			this.uiCamera = gameObject.AddComponent<Camera>();
			this.uiCamera.clearFlags = 2;
			this.uiCamera.backgroundColor = Color.clear;
			this.uiCamera.cullingMask = 1 << LayerMask.NameToLayer(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhe());
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002203B File Offset: 0x0002023B
		private void SetupRenderTexture()
		{
			if (this.renderTexture != null)
			{
				return;
			}
			this.renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
			this.uiCamera.targetTexture = this.renderTexture;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00022074 File Offset: 0x00020274
		private void HandleOverlayUI()
		{
			Canvas[] array = Object.FindObjectsOfType<Canvas>();
			if (array != null)
			{
				foreach (Canvas canvas in array)
				{
					if (canvas.renderMode == null)
					{
						canvas.renderMode = 1;
						canvas.worldCamera = this.uiCamera;
					}
				}
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000220BC File Offset: 0x000202BC
		private void OverlayUIToLeiaViews()
		{
			LeiaDisplay leiaDisplay = Object.FindObjectOfType<LeiaDisplay>();
			for (int i = 0; i < leiaDisplay.GetViewCount(); i++)
			{
				leiaDisplay.GetEyeCamera(i).gameObject.AddComponent<CameraUIOverlay>().UIRenderTexture = this.renderTexture;
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000220FC File Offset: 0x000202FC
		private void OnDestroy()
		{
			if (this.renderTexture != null && this.uiCamera != null && this.renderTexture == this.uiCamera.targetTexture)
			{
				this.uiCamera.targetTexture = null;
			}
			if (this.renderTexture != null)
			{
				Object.DestroyImmediate(this.renderTexture);
			}
		}

		// Token: 0x04000638 RID: 1592
		private Camera uiCamera;

		// Token: 0x04000639 RID: 1593
		private RenderTexture renderTexture;
	}
}
