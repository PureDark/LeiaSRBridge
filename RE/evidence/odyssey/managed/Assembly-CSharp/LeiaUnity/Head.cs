using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace LeiaUnity
{
	// Token: 0x02000124 RID: 292
	[ExecuteInEditMode]
	public class Head : MonoBehaviour
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x00022570 File Offset: 0x00020770
		public void InitHead(List<Vector2> ViewConfig, LeiaDisplay leiaDisplay)
		{
			this.CullingMask = -1;
			this.headcamera = base.gameObject.AddComponent<Camera>();
			this.headcamera.depth = 1f;
			if (this.headcamera != null && base.gameObject.GetComponent<HDAdditionalCameraData>() == null)
			{
				base.gameObject.AddComponent<HDAdditionalCameraData>();
			}
			this.HeadPositionMM = new Vector3(0f, 0f, leiaDisplay.ViewingDistanceMM);
			this.leiaDisplay = leiaDisplay;
			this.ViewConfig = ViewConfig;
			this.eyes = new List<Eye>();
			int num = 0;
			foreach (Vector2 offset in ViewConfig)
			{
				GameObject gameObject;
				if (num == 0)
				{
					gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhg());
				}
				else if (num == 1)
				{
					gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhh());
				}
				else
				{
					gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhi());
				}
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				Eye eye = gameObject.AddComponent<Eye>();
				eye.offset = offset;
				eye.leiaDisplay = leiaDisplay;
				this.eyes.Add(eye);
				eye.EyeUpdate();
				num++;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000226DC File Offset: 0x000208DC
		private void Start()
		{
			this.headcamera.enabled = false;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000226EA File Offset: 0x000208EA
		private void OnDisable()
		{
			if (RenderPipelineUtils.IsUnityRenderPipeline())
			{
				RenderPipelineManager.endFrameRendering -= this.EndFrameRenderHook;
				RenderPipelineManager.beginCameraRendering -= this.BeginCameraRenderingHook;
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00022718 File Offset: 0x00020918
		private void BeginCameraRenderingHook(ScriptableRenderContext context, Camera renderingCam)
		{
			LogUtil.Log(LogLevel.Info, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhj(), context), Array.Empty<object>());
			if (this.beginViewRenderingEvents.ContainsKey(renderingCam))
			{
				foreach (Action action in this.beginViewRenderingEvents[renderingCam])
				{
					action();
				}
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00022798 File Offset: 0x00020998
		private void EndCameraRenderingHook(ScriptableRenderContext context, Camera renderingCam)
		{
			LogUtil.Log(LogLevel.Info, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhk(), context), Array.Empty<object>());
			if (this.endViewRenderingEvents.ContainsKey(renderingCam))
			{
				foreach (Action action in this.endViewRenderingEvents[renderingCam])
				{
					action();
				}
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00022818 File Offset: 0x00020A18
		private void EndFrameRenderHook(ScriptableRenderContext context, Camera[] cams)
		{
			LogUtil.Log(LogLevel.Info, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhl(), context), Array.Empty<object>());
			if (cams != null && this.headcamera != null && cams[0] == this.headcamera)
			{
				Camera.SetupCurrent(this.headcamera);
				this.OnPostRender();
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00022874 File Offset: 0x00020A74
		public void HeadUpdate()
		{
			if (this.leiaDisplay == null || this.leiaDisplay.ViewersHead != this)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			base.transform.localPosition = Vector3.zero;
			this.leiaDisplay.RealToVirtualCenterFacePosition(this.HeadPositionMM);
			if (Singleton<RenderTrackingDevice>.Instance != null)
			{
				float num = this.leiaDisplay.VirtualHeight / this.leiaDisplay.HeightMM;
				Vector3[] lookAroundEyesPosition = Singleton<RenderTrackingDevice>.Instance.GetLookAroundEyesPosition();
				if (lookAroundEyesPosition[0] != Vector3.zero && lookAroundEyesPosition[1] != Vector3.zero)
				{
					base.transform.localPosition = Vector3.zero;
					Vector3 vector = 0.5f * (lookAroundEyesPosition[0] + lookAroundEyesPosition[1]);
					Vector3 vector2;
					vector2..ctor(vector.x * this.leiaDisplay.LookAroundFactor, vector.y * this.leiaDisplay.LookAroundFactor, (this.leiaDisplay.FOVFactor > 1f) ? (-((this.leiaDisplay.ViewingDistanceMM + this.leiaDisplay.LookAroundFactor * (vector.z - this.leiaDisplay.ViewingDistanceMM)) / this.leiaDisplay.FOVFactor)) : (-(this.leiaDisplay.ViewingDistanceMM / this.leiaDisplay.FOVFactor + this.leiaDisplay.LookAroundFactor * (vector.z - this.leiaDisplay.ViewingDistanceMM))));
					Vector3 vector3 = lookAroundEyesPosition[1] - lookAroundEyesPosition[0];
					vector3.z *= -1f;
					this.eyes[0].transform.localPosition = (vector2 - 0.5f * (this.leiaDisplay.DepthFactor / this.leiaDisplay.FOVFactor) * vector3) * num;
					this.eyes[1].transform.localPosition = (vector2 + 0.5f * (this.leiaDisplay.DepthFactor / this.leiaDisplay.FOVFactor) * vector3) * num;
				}
				else
				{
					base.transform.localPosition = this.leiaDisplay.RealToVirtualCenterFacePosition(this.HeadPositionMM);
					this.eyes[0].transform.localPosition = Vector3.zero;
					this.eyes[1].transform.localPosition = Vector3.zero;
				}
			}
			foreach (Eye eye in this.eyes)
			{
				eye.EyeUpdate();
			}
			if (this.leiaDisplay.DriverCamera != null)
			{
				this.leiaDisplay.DriverCamera.enabled = false;
				LeiaUtils.CopyCameraParameters(this.leiaDisplay.DriverCamera, this.headcamera);
				this.CullingMask = this.leiaDisplay.DriverCamera.cullingMask;
			}
			if (Application.isPlaying && Singleton<RenderTrackingDevice>.Instance.DesiredLightfieldMode == RenderTrackingDevice.LightfieldMode.On)
			{
				this.headcamera.cullingMask = 0;
			}
			else
			{
				this.headcamera.cullingMask = this.CullingMask;
			}
			this.headcamera.enabled = false;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00022BEC File Offset: 0x00020DEC
		private void OnPostRender()
		{
			if (Application.isPlaying)
			{
				this.leiaDisplay.RenderImage();
			}
		}

		// Token: 0x04000644 RID: 1604
		public LeiaDisplay leiaDisplay;

		// Token: 0x04000645 RID: 1605
		public List<Eye> eyes;

		// Token: 0x04000646 RID: 1606
		public Vector3 HeadPositionMM;

		// Token: 0x04000647 RID: 1607
		public Camera headcamera;

		// Token: 0x04000648 RID: 1608
		public LayerMask CullingMask;

		// Token: 0x04000649 RID: 1609
		public List<Vector2> ViewConfig;

		// Token: 0x0400064A RID: 1610
		private Vector3 InitPosition;

		// Token: 0x0400064B RID: 1611
		private readonly Dictionary<Camera, List<Action>> beginViewRenderingEvents = new Dictionary<Camera, List<Action>>();

		// Token: 0x0400064C RID: 1612
		private readonly Dictionary<Camera, List<Action>> endViewRenderingEvents = new Dictionary<Camera, List<Action>>();

		// Token: 0x0400064D RID: 1613
		public Vector3 LastKnownEyeCenter = new Vector3(0f, 0f, 500f);

		// Token: 0x0400064E RID: 1614
		public Vector3 LastKnownEyeCenterSmoothed = new Vector3(0f, 0f, 500f);

		// Token: 0x0400064F RID: 1615
		public Vector3 LastKnownEyeDelta = Vector3.zero;
	}
}
