using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000117 RID: 279
	[RequireComponent(typeof(LeiaDisplay))]
	[HelpURL("https://support.leiainc.com/sdk/unity-plugin/leiasr-tm-unity-plugin-guide/extensions/auto-focus/leiadepthfocus")]
	public class LeiaDepthFocus : LeiaFocus, IReleasable
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000207C0 File Offset: 0x0001E9C0
		private Camera DepthCamera
		{
			get
			{
				Transform transform = this.ParentCamera.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgo());
				if (!transform)
				{
					this._depthCamera = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgo()).AddComponent<Camera>();
				}
				else
				{
					this._depthCamera = transform.GetComponent<Camera>();
				}
				return this._depthCamera;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00020814 File Offset: 0x0001EA14
		protected override void OnEnable()
		{
			base.OnEnable();
			this.DepthStatsShader = Resources.Load<ComputeShader>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgp());
			this.systemSupportsComputeShaders = SystemInfo.supportsComputeShaders;
			if (!this.systemSupportsComputeShaders)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgq(), Array.Empty<object>());
				base.enabled = false;
				return;
			}
			this.DepthStatsKernelID = this.DepthStatsShader.FindKernel(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgr());
			this.ParentCamera = this.leiaDisplay.DriverCamera;
			this.leiaDisplay = base.GetComponent<LeiaDisplay>();
			this.DepthRenderTexture = new RenderTexture(this.DepthRenderWidth, this.DepthRenderHeight, 16, 0);
			this.DepthRenderTexture.name = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgs();
			this.DepthRenderTexture.antiAliasing = 1;
			this.DepthRenderTexture.autoGenerateMips = false;
			this.DepthRenderTexture.filterMode = 0;
			this.DepthRenderTexture.anisoLevel = 0;
			this.DepthCamera.transform.parent = this.ParentCamera.transform;
			this.DepthCamera.transform.localPosition = Vector3.zero;
			this.DepthCamera.transform.localRotation = Quaternion.identity;
			this.DepthCamera.transform.localScale = Vector3.one;
			this.DepthCamera.SetReplacementShader(Shader.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgt()), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
			this.DepthCamera.depth = -1f;
			this.DepthCamera.enabled = true;
			this.DepthCamera.allowMSAA = false;
			this.DepthCamera.allowHDR = false;
			this.DepthCamera.clearFlags = 2;
			this.DepthCamera.backgroundColor = new Color(1f, 0.5f, 1f, 1f);
			this.DepthCamera.targetTexture = this.DepthRenderTexture;
			this.DepthStatsShader.SetTexture(0, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgu(), this.DepthRenderTexture);
			this.DepthStatsShader.SetTexture(this.DepthStatsKernelID, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgu(), this.DepthRenderTexture);
			this.buffer = new ComputeBuffer(this.resultFloats.Length, 4);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00020A20 File Offset: 0x0001EC20
		protected override void LateUpdate()
		{
			if (this.leiaDisplay == null || this.leiaDisplay.mode == LeiaDisplay.ControlMode.DisplayDriven)
			{
				return;
			}
			if (!this.systemSupportsComputeShaders)
			{
				return;
			}
			this.SyncCameraSettings();
			if (Time.frameCount % 2 == 0)
			{
				this.ComputeDepthFocusTargets();
			}
			base.LateUpdate();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00020A70 File Offset: 0x0001EC70
		private void ComputeDepthFocusTargets()
		{
			this.buffer.SetData(this.resultFloats);
			this.DepthStatsShader.SetBuffer(this.DepthStatsKernelID, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgv(), this.buffer);
			this.DepthStatsShader.Dispatch(this.DepthStatsKernelID, 1, 1, 1);
			this.buffer.GetData(this.resultFloats);
			Color color;
			color..ctor(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2]);
			if (color.r > 0f)
			{
				float depthRange = this.DepthRange;
				float nearClipPlane = this.DepthCamera.nearClipPlane;
				float desiredFarPlane = color.g * this.DepthRange + this.DepthCamera.nearClipPlane;
				float targetFocalDistance = color.b * this.DepthRange + this.DepthCamera.nearClipPlane;
				float targetDepthFactor = Mathf.Abs(LeiaDisplayUtils.GetRecommendedDepthFactorWithFarPlane(this.leiaDisplay, desiredFarPlane) / 100f);
				base.SetTargetFocalDistance(targetFocalDistance);
				base.SetTargetDepthFactor(targetDepthFactor);
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00020B68 File Offset: 0x0001ED68
		public void SyncCameraSettings()
		{
			this.DepthCamera.nearClipPlane = this.leiaDisplay.DriverCamera.nearClipPlane;
			this.DepthCamera.farClipPlane = this.leiaDisplay.DriverCamera.farClipPlane;
			this.DepthRange = this.DepthCamera.farClipPlane - this.DepthCamera.nearClipPlane;
			if (this.LeiaViewCamera == null)
			{
				if (GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgw()))
				{
					this.LeiaViewCamera = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgw()).GetComponent<Camera>();
				}
			}
			else
			{
				this.DepthCamera.cullingMask = this.LeiaViewCamera.cullingMask;
			}
			this.DepthCamera.orthographic = this.ParentCamera.orthographic;
			if (this.DepthCamera.orthographic)
			{
				this.DepthCamera.orthographicSize = this.ParentCamera.orthographicSize;
				return;
			}
			this.DepthCamera.fieldOfView = this.leiaDisplay.DriverCamera.fieldOfView;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020C69 File Offset: 0x0001EE69
		private void OnDisable()
		{
			this.Release();
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00020C71 File Offset: 0x0001EE71
		public void Release()
		{
			if (this.buffer != null)
			{
				this.buffer.Release();
			}
			if (this.DepthRenderTexture != null)
			{
				this.DepthRenderTexture.Release();
			}
		}

		// Token: 0x040005F9 RID: 1529
		private Camera _depthCamera;

		// Token: 0x040005FA RID: 1530
		private ComputeShader DepthStatsShader;

		// Token: 0x040005FB RID: 1531
		private int DepthStatsKernelID;

		// Token: 0x040005FC RID: 1532
		private Camera LeiaViewCamera;

		// Token: 0x040005FD RID: 1533
		private Camera ParentCamera;

		// Token: 0x040005FE RID: 1534
		private readonly int DepthRenderWidth = 512;

		// Token: 0x040005FF RID: 1535
		private readonly int DepthRenderHeight = 288;

		// Token: 0x04000600 RID: 1536
		private float DepthRange;

		// Token: 0x04000601 RID: 1537
		private readonly float[] resultFloats = new float[4];

		// Token: 0x04000602 RID: 1538
		private bool systemSupportsComputeShaders;

		// Token: 0x04000603 RID: 1539
		private RenderTexture DepthRenderTexture;

		// Token: 0x04000604 RID: 1540
		private ComputeBuffer buffer;
	}
}
