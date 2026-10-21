using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000127 RID: 295
	[ExecuteInEditMode]
	public class LeiaDisplay : MonoBehaviour
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00022EB0 File Offset: 0x000210B0
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00022EB8 File Offset: 0x000210B8
		public LeiaDisplay.DisplayMode displaymode
		{
			get
			{
				return this._displayMode;
			}
			set
			{
				this.Set3DMode(value == LeiaDisplay.DisplayMode._3D);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00022EC4 File Offset: 0x000210C4
		private string _editorPreviewShaderName
		{
			get
			{
				return <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhn();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00022ECB File Offset: 0x000210CB
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00022ED3 File Offset: 0x000210D3
		public LeiaDisplay.EditorPreviewMode DesiredPreviewMode
		{
			get
			{
				return this.desiredPreviewMode;
			}
			set
			{
				this.desiredPreviewMode = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00022EDC File Offset: 0x000210DC
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00022EE4 File Offset: 0x000210E4
		public bool CameraShiftEnabled
		{
			get
			{
				return this.cameraShiftEnabled;
			}
			set
			{
				this.cameraShiftEnabled = value;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000821 RID: 2081 RVA: 0x00022EF0 File Offset: 0x000210F0
		// (remove) Token: 0x06000822 RID: 2082 RVA: 0x00022F24 File Offset: 0x00021124
		public static event Action StateChanged;

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00022F57 File Offset: 0x00021157
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00022F5F File Offset: 0x0002115F
		public bool DivideByPerspectiveFactor
		{
			get
			{
				return this.divideByPerspectiveFactor;
			}
			set
			{
				this.divideByPerspectiveFactor = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00022F68 File Offset: 0x00021168
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x00022F70 File Offset: 0x00021170
		public int ScriptableRendererIndex
		{
			get
			{
				return this.scriptableRendererIndex;
			}
			set
			{
				this.scriptableRendererIndex = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00022F79 File Offset: 0x00021179
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00022F81 File Offset: 0x00021181
		public float FOVFactor
		{
			get
			{
				return this.fovFactor;
			}
			set
			{
				this.fovFactor = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00022F8A File Offset: 0x0002118A
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00022F92 File Offset: 0x00021192
		public float LookAroundFactor
		{
			get
			{
				return this._lookAroundFactor;
			}
			set
			{
				this._desiredLookAroundFactor = value;
				if (this._numViewsX == 2)
				{
					this._lookAroundFactor = value;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00022FAB File Offset: 0x000211AB
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00022FB3 File Offset: 0x000211B3
		public float DepthFactor
		{
			get
			{
				return this._depthFactor;
			}
			set
			{
				this._desiredDepthFactor = value;
				if (this._numViewsX == 2)
				{
					this._depthFactor = value;
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00022FCC File Offset: 0x000211CC
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00022FD4 File Offset: 0x000211D4
		public bool DrawCameraBounds
		{
			get
			{
				return this.drawCameraBounds;
			}
			set
			{
				this.drawCameraBounds = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00022FDD File Offset: 0x000211DD
		[HideInInspector]
		public float WidthMM
		{
			get
			{
				if (Application.isEditor)
				{
					return 266f;
				}
				return Singleton<RenderTrackingDevice>.Instance.GetDisplaySizeInMM().x;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00022FFB File Offset: 0x000211FB
		[HideInInspector]
		public float HeightMM
		{
			get
			{
				if (Application.isEditor)
				{
					return 168f;
				}
				return Singleton<RenderTrackingDevice>.Instance.GetDisplaySizeInMM().y;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00023019 File Offset: 0x00021219
		[HideInInspector]
		public float ViewingDistanceMM
		{
			get
			{
				if (Application.isEditor)
				{
					return 450f;
				}
				return Singleton<RenderTrackingDevice>.Instance.GetViewingDistanceInMM();
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00023032 File Offset: 0x00021232
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0002304B File Offset: 0x0002124B
		[HideInInspector]
		public float IPDMM
		{
			get
			{
				if (Application.isEditor)
				{
					return 63f;
				}
				return Singleton<RenderTrackingDevice>.Instance.IPDInMM;
			}
			set
			{
				Singleton<RenderTrackingDevice>.Instance.IPDInMM = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00023058 File Offset: 0x00021258
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x00023060 File Offset: 0x00021260
		public float MaxDisparity
		{
			get
			{
				return this.maxDisparity;
			}
			set
			{
				this.maxDisparity = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00023069 File Offset: 0x00021269
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x00023071 File Offset: 0x00021271
		public bool UseCameraClippingPlanes
		{
			get
			{
				return this.useCameraClippingPlanes;
			}
			set
			{
				this.useCameraClippingPlanes = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002307A File Offset: 0x0002127A
		[HideInInspector]
		public float VirtualWidth
		{
			get
			{
				return LeiaDisplay.GetGameViewAspectRatio() * this.calculatedVirtualHeight;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00023088 File Offset: 0x00021288
		public float MMToVirtual
		{
			get
			{
				return this.calculatedVirtualHeight / this.HeightMM;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00023097 File Offset: 0x00021297
		public float VirtualToMM
		{
			get
			{
				return this.HeightMM / this.VirtualHeight;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000230A6 File Offset: 0x000212A6
		public Camera HeadCamera
		{
			get
			{
				return this.ViewersHead.headcamera;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000230B3 File Offset: 0x000212B3
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x000230E0 File Offset: 0x000212E0
		public float FocalDistance
		{
			get
			{
				if (this.mode == LeiaDisplay.ControlMode.CameraDriven)
				{
					return Mathf.Max(LeiaDisplay.MinFocalDistance, base.transform.localPosition.z);
				}
				return 0f;
			}
			set
			{
				if (this.mode == LeiaDisplay.ControlMode.CameraDriven)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, value);
					return;
				}
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bho(), Array.Empty<object>());
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00023138 File Offset: 0x00021338
		private void ValidateFocalDistance()
		{
			if (this.mode == LeiaDisplay.ControlMode.CameraDriven && this.focalDistance != this.FocalDistance && this.focalDistance != 0f)
			{
				this.FocalDistance = Mathf.Max(LeiaDisplay.MinFocalDistance, this.focalDistance);
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00023174 File Offset: 0x00021374
		public void SetFocalDistance(float value)
		{
			this.focalDistance = value;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0002317D File Offset: 0x0002137D
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x00023185 File Offset: 0x00021385
		public int AntiAliasingLevel
		{
			get
			{
				return this.antiAliasingLevel;
			}
			set
			{
				this.antiAliasingLevel = value;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00023190 File Offset: 0x00021390
		public Vector3 ScreenToWorldPoint(Vector3 screenPoint)
		{
			Vector3 vector;
			vector..ctor(screenPoint.x / (float)Screen.width * this.VirtualWidth - this.VirtualWidth / 2f, screenPoint.y / (float)Screen.height * this.VirtualHeight - this.VirtualHeight / 2f, 0f);
			return base.transform.position + base.transform.rotation * vector;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002320C File Offset: 0x0002140C
		private void OnEnable()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			Singleton<RenderTrackingDevice>.Instance = Object.FindObjectOfType<RenderTrackingDevice>();
			if (Singleton<RenderTrackingDevice>.Instance == null)
			{
				GameObject gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhp());
				Singleton<RenderTrackingDevice>.Instance = gameObject.AddComponent<RenderTrackingDevice>();
				Object.DontDestroyOnLoad(gameObject);
			}
			Singleton<RenderTrackingDevice>.Instance.Initialize();
			Singleton<RenderTrackingDevice>.Instance.LightfieldModeChanged += this.HandleLightfieldModeChanged;
			this.UpdateLightfieldMode();
			if (!this._initialized)
			{
				this.UpdateInitialViewOffsets();
				this._interlacedTexture = new RenderTexture(Singleton<RenderTrackingDevice>.Instance.GetDevicePanelResolution().x, Singleton<RenderTrackingDevice>.Instance.GetDevicePanelResolution().y, 0);
				this._interlacedTexture.Create();
				this._initialized = true;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000232C9 File Offset: 0x000214C9
		private void OnDisable()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhq(), Array.Empty<object>());
			if (Singleton<RenderTrackingDevice>.Instance)
			{
				Singleton<RenderTrackingDevice>.Instance.LightfieldModeChanged -= this.HandleLightfieldModeChanged;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000232FD File Offset: 0x000214FD
		private void ValidateScale()
		{
			base.transform.localScale = Vector3.one;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00023310 File Offset: 0x00021510
		public void Update()
		{
			if (this.UIEventCamera != null)
			{
				this.UIEventCamera.fieldOfView = this.ViewersHead.headcamera.fieldOfView;
				this.UIEventCamera.transform.position = this.HeadPosition;
				Matrix4x4 projectionMatrixForCamera = this.GetProjectionMatrixForCamera(this.UIEventCamera, Vector3.zero, false);
				this.UIEventCamera.projectionMatrix = projectionMatrixForCamera;
			}
			else
			{
				LeiaEventCamera componentInChildren = base.GetComponentInChildren<LeiaEventCamera>();
				if (componentInChildren != null)
				{
					this.UIEventCamera = componentInChildren.GetComponent<Camera>();
					if (componentInChildren != null)
					{
						this.UIEventCamera = componentInChildren.GetComponent<Camera>();
					}
					else
					{
						LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhr(), Array.Empty<object>());
					}
				}
			}
			this.ValidateFocalDistance();
			this.ValidateScale();
			this.calculatedVirtualHeight = this.VirtualHeight;
			if (Application.isPlaying)
			{
				this.UpdateViews();
			}
			if (!base.GetComponentInChildren<Head>())
			{
				this.OnComponentAddedToGameObject();
				return;
			}
			if (this.mode == LeiaDisplay.ControlMode.CameraDriven)
			{
				this.UpdateVirtualDisplayFromCamera();
				return;
			}
			this.UpdateCameraFromVirtualDisplay();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00019991 File Offset: 0x00017B91
		private bool ValidateSupportAllAxis()
		{
			return false;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00023410 File Offset: 0x00021610
		private void UpdateComfortZoneClippingPlanes()
		{
			if (this.ViewersHead == null)
			{
				return;
			}
			this.comfortZoneFarClipPlane = 1f / (this.HeightMM / this.calculatedVirtualHeight * this.fovFactor * (1f / this.ViewersHead.HeadPositionMM.z - 1f / (6f * this.DepthFactor) * (0f + 1f / this.ViewersHead.HeadPositionMM.z)));
			this.comfortZoneNearClipPlane = 1f / (this.HeightMM / this.calculatedVirtualHeight * this.fovFactor * (1f / this.ViewersHead.HeadPositionMM.z + 1f / (5f * this.DepthFactor) * (0f + 1f / this.ViewersHead.HeadPositionMM.z)));
			if (this.comfortZoneFarClipPlane < 0f)
			{
				this.comfortZoneFarClipPlane = 1000f;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00023512 File Offset: 0x00021712
		public static float GetGameViewAspectRatio()
		{
			return (float)Screen.width / Mathf.Max(1f, (float)Screen.height);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002352C File Offset: 0x0002172C
		private void OnDrawGizmos()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhs(), Array.Empty<object>());
			if (this.ViewersHead == null)
			{
				return;
			}
			float gameViewAspectRatio = LeiaDisplay.GetGameViewAspectRatio();
			float num = this.calculatedVirtualHeight * gameViewAspectRatio;
			Vector3 vector;
			vector..ctor(num / 2f, this.calculatedVirtualHeight / 2f, 0f);
			Vector3 vector2;
			vector2..ctor(-num / 2f, this.calculatedVirtualHeight / 2f, 0f);
			Vector3 vector3;
			vector3..ctor(num / 2f, -this.calculatedVirtualHeight / 2f, 0f);
			Vector3 vector4;
			vector4..ctor(-num / 2f, -this.calculatedVirtualHeight / 2f, 0f);
			vector = base.transform.position + base.transform.rotation * vector;
			vector2 = base.transform.position + base.transform.rotation * vector2;
			vector3 = base.transform.position + base.transform.rotation * vector3;
			vector4 = base.transform.position + base.transform.rotation * vector4;
			if (base.enabled)
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawLine(vector2, vector);
				Gizmos.DrawLine(vector3, vector4);
				Gizmos.DrawLine(vector2, vector4);
				Gizmos.DrawLine(vector, vector3);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000236A5 File Offset: 0x000218A5
		private void OnDestroy()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bht(), Array.Empty<object>());
			if (this.ViewersHead != null)
			{
				Object.DestroyImmediate(this.ViewersHead.gameObject);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000236D8 File Offset: 0x000218D8
		public void InitLeiaDisplay(List<Vector2> ViewConfig)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhu(), Array.Empty<object>());
			GameObject gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sf());
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = new Vector3(0f, 0f, this.ViewingDistanceMM * this.calculatedVirtualHeight / (this.HeightMM * this.fovFactor));
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			if (this.mode == LeiaDisplay.ControlMode.CameraDriven)
			{
				this.UpdateVirtualDisplayFromCamera();
			}
			this.ViewersHead = gameObject.AddComponent<Head>();
			this.ViewersHead.InitHead(ViewConfig, this);
			this.ViewersHead.HeadUpdate();
			this.UIEventCamera = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhv())
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<Camera>();
			this.UIEventCamera.cullingMask = 1 << LayerMask.NameToLayer(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhe());
			this.UIEventCamera.depth = 100f;
			this.UIEventCamera.clearFlags = 4;
			this.UIEventCamera.cullingMask = 0;
			this.UIEventCamera.fieldOfView = this.ViewersHead.headcamera.fieldOfView;
			this.UIEventCamera.transform.localPosition = this.ViewersHead.headcamera.transform.localPosition;
			this.UIEventCamera.gameObject.AddComponent<LeiaEventCamera>();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00023854 File Offset: 0x00021A54
		private void OnComponentAddedToGameObject()
		{
			this.DriverCamera = base.GetComponent<Camera>();
			Camera camera = null;
			if (base.transform.parent != null)
			{
				camera = base.transform.parent.GetComponent<Camera>();
			}
			if (this.DriverCamera != null)
			{
				this.DriverCamera.enabled = false;
				this.mode = LeiaDisplay.ControlMode.CameraDriven;
				this.fovFactor = this.ViewingDistanceMM * this.calculatedVirtualHeight / (this.HeightMM * base.transform.localPosition.z);
				GameObject gameObject = new GameObject(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhw());
				gameObject.transform.parent = base.transform;
				gameObject.AddComponent<LeiaDisplay>().useCameraClippingPlanes = true;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 2f / (1f / this.DriverCamera.nearClipPlane + 1f / this.DriverCamera.farClipPlane));
				gameObject.transform.localRotation = Quaternion.identity;
				Object.DestroyImmediate(this);
				return;
			}
			if (camera != null)
			{
				this.DriverCamera = camera;
				List<Vector2> viewConfig = new List<Vector2>
				{
					new Vector2(-0.5f, 0f),
					new Vector2(0.5f, 0f)
				};
				this.mode = LeiaDisplay.ControlMode.CameraDriven;
				this.InitLeiaDisplay(viewConfig);
				return;
			}
			MeshRenderer componentInChildren = base.GetComponentInChildren<MeshRenderer>();
			if (componentInChildren != null)
			{
				float y = componentInChildren.bounds.size.y;
				this.calculatedVirtualHeight = y * 4f;
			}
			List<Vector2> viewConfig2 = new List<Vector2>
			{
				new Vector2(-0.5f, 0f),
				new Vector2(0.5f, 0f)
			};
			this.mode = LeiaDisplay.ControlMode.DisplayDriven;
			this.InitLeiaDisplay(viewConfig2);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00023A23 File Offset: 0x00021C23
		public Vector3 HeadPosition
		{
			get
			{
				return (this.GetEyeCamera(0).transform.position + this.GetEyeCamera(1).transform.position) * 0.5f;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00023A56 File Offset: 0x00021C56
		public int GetViewCount()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhx(), Array.Empty<object>());
			if (this.ViewersHead == null || this.ViewersHead.ViewConfig == null)
			{
				return 1;
			}
			return this.ViewersHead.ViewConfig.Count;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00023A95 File Offset: 0x00021C95
		public Camera GetEyeCamera(int index)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhy(), Array.Empty<object>());
			return this.ViewersHead.eyes[index].Eyecamera;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00023ABD File Offset: 0x00021CBD
		public Eye GetEye(int index)
		{
			if (this.ViewersHead == null)
			{
				return null;
			}
			return this.ViewersHead.eyes[index];
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00023AE0 File Offset: 0x00021CE0
		private void UpdateCameraFromVirtualDisplay()
		{
			if (this.ViewersHead != null)
			{
				this.ViewersHead.HeadUpdate();
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00023AFC File Offset: 0x00021CFC
		private void UpdateVirtualDisplayFromCamera()
		{
			this.VirtualHeight = 2f * Mathf.Tan(0.017453292f * (this.DriverCamera.fieldOfView / 2f)) * base.transform.localPosition.z;
			this.fovFactor = this.ViewingDistanceMM * this.VirtualHeight / (this.HeightMM * base.transform.localPosition.z);
			if (this.ViewersHead != null)
			{
				this.ViewersHead.HeadUpdate();
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00023B88 File Offset: 0x00021D88
		public Vector3 RealToVirtualCenterFacePosition(Vector3 FacePositionMM)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhz(), Array.Empty<object>());
			float x = FacePositionMM.x;
			float y = FacePositionMM.y;
			float num;
			if (this.fovFactor < 1f)
			{
				num = -(this.ViewingDistanceMM / this.fovFactor + (FacePositionMM.z - this.ViewingDistanceMM));
			}
			else
			{
				num = -(this.ViewingDistanceMM + (FacePositionMM.z - this.ViewingDistanceMM)) / this.fovFactor;
			}
			return new Vector3(x, y, num) * this.VirtualHeight / this.HeightMM;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00023C1E File Offset: 0x00021E1E
		public void RenderImage()
		{
			Graphics.Blit(this._interlacedTexture, Camera.current.activeTexture);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00023C38 File Offset: 0x00021E38
		public void UpdateViews()
		{
			int x = Singleton<RenderTrackingDevice>.Instance.GetDeviceViewResolution().x;
			int y = Singleton<RenderTrackingDevice>.Instance.GetDeviceViewResolution().y;
			for (int i = 0; i < this.GetViewCount(); i++)
			{
				Eye eye = this.GetEye(i);
				if (eye != null)
				{
					eye.SetTextureParams(3840, 2160);
				}
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00023C9D File Offset: 0x00021E9D
		private bool IsLandscape(ScreenOrientation orientation)
		{
			return orientation == 3 || orientation == 4;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00023CA9 File Offset: 0x00021EA9
		private bool IsPortrait(ScreenOrientation orientation)
		{
			return orientation == 1 || orientation == 2;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00023CB8 File Offset: 0x00021EB8
		private void UpdateInterlacedTexture(int width, int height)
		{
			if (this._interlacedTexture.width != width || this._interlacedTexture.height != height)
			{
				RenderTexture interlacedTexture = this._interlacedTexture;
				if (interlacedTexture != null)
				{
					interlacedTexture.Release();
				}
				this._interlacedTexture = new RenderTexture(width, height, 0);
				this._interlacedTexture.Create();
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00023D0C File Offset: 0x00021F0C
		private void UpdateInitialViewOffsets()
		{
			this._initialViewOffsets = new Vector2[this._numViewsX * this._numViewsY];
			float num = -0.5f * ((float)this._numViewsX - 1f);
			float num2 = -0.5f * ((float)this._numViewsY - 1f);
			for (int i = 0; i < this._numViewsY; i++)
			{
				for (int j = 0; j < this._numViewsX; j++)
				{
					float num3 = num + (float)j;
					float num4 = num2 + (float)i;
					this._initialViewOffsets[j + i * this._numViewsX] = new Vector2(num3, num4);
				}
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00023DA5 File Offset: 0x00021FA5
		private float GetInitialViewOffsetX(int viewX, int viewY)
		{
			return this._initialViewOffsets[viewX + viewY * this._numViewsX].x;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00023DC1 File Offset: 0x00021FC1
		private float GetInitialViewOffsetY(int viewX, int viewY)
		{
			return this._initialViewOffsets[viewX + viewY * this._numViewsX].y;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00023DE0 File Offset: 0x00021FE0
		public static float GetViewportAspectFor(Camera renderingCamera)
		{
			return renderingCamera.pixelRect.width * 1f / renderingCamera.pixelRect.height;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00023E10 File Offset: 0x00022010
		public static Matrix4x4 GetConvergedProjectionMatrixForPosition(Camera Camera, Vector3 convergencePoint)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bia(), Array.Empty<object>());
			Matrix4x4 zero = Matrix4x4.zero;
			Vector3 vector = convergencePoint - Camera.transform.position;
			float farClipPlane = Camera.farClipPlane;
			float nearClipPlane = Camera.nearClipPlane;
			float num = -1f * Vector3.Dot(vector, Camera.transform.right);
			float num2 = -1f * Vector3.Dot(vector, Camera.transform.up);
			float num3 = Mathf.Max(Vector3.Dot(vector, Camera.transform.forward), 1E-05f);
			if (Camera.orthographic)
			{
				float num4 = Camera.orthographicSize * LeiaDisplay.GetViewportAspectFor(Camera);
				float orthographicSize = Camera.orthographicSize;
				zero[0, 0] = 1f / num4;
				zero[0, 1] = 0f;
				zero[0, 2] = -num / (num4 * num3);
				zero[0, 3] = 0f;
				zero[1, 0] = 0f;
				zero[1, 1] = 1f / orthographicSize;
				zero[1, 2] = -num2 / (orthographicSize * num3);
				zero[1, 3] = 0f;
				zero[2, 0] = 0f;
				zero[2, 1] = 0f;
				zero[2, 2] = -2f / (farClipPlane - nearClipPlane);
				zero[2, 3] = -(farClipPlane + nearClipPlane) / (farClipPlane - nearClipPlane);
				zero[3, 0] = 0f;
				zero[3, 1] = 0f;
				zero[3, 2] = 0f;
				zero[3, 3] = 1f;
			}
			else
			{
				float num5 = Mathf.Max(LeiaDisplay.GetViewportAspectFor(Camera), 1E-05f);
				float num6 = num3 * Mathf.Tan(Camera.fieldOfView * 3.1415927f / 360f);
				float num7 = num5 * num6;
				zero[0, 0] = num3 / num7;
				zero[0, 1] = 0f;
				zero[0, 2] = -num / num7;
				zero[0, 3] = 0f;
				zero[1, 0] = 0f;
				zero[1, 1] = num3 / num6;
				zero[1, 2] = -num2 / num6;
				zero[1, 3] = 0f;
				zero[2, 0] = 0f;
				zero[2, 1] = 0f;
				zero[2, 2] = -(farClipPlane + nearClipPlane) / (farClipPlane - nearClipPlane);
				zero[2, 3] = -2f * farClipPlane * nearClipPlane / (farClipPlane - nearClipPlane);
				zero[3, 0] = 0f;
				zero[3, 1] = 0f;
				zero[3, 2] = -1f;
				zero[3, 3] = 0f;
			}
			return zero;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000240E0 File Offset: 0x000222E0
		public Matrix4x4 GetProjectionMatrixForCamera(Camera camera, Vector3 offset, bool isEye)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bib(), Array.Empty<object>());
			float virtualWidth = this.VirtualWidth;
			float num = this.calculatedVirtualHeight;
			Vector3 vector = -camera.transform.InverseTransformPoint(base.transform.position);
			if (vector.z == 0f)
			{
				return Matrix4x4.zero;
			}
			float x = vector.x;
			float y = vector.y;
			float z = vector.z;
			float num2 = -(virtualWidth / 2f - x) / z;
			float num3 = -(-virtualWidth / 2f - x) / z;
			float num4 = -(num / 2f - y) / z;
			float num5 = -(-num / 2f - y) / z;
			float farClipPlane = camera.farClipPlane;
			float nearClipPlane = camera.nearClipPlane;
			Matrix4x4 result = default(Matrix4x4);
			result.m00 = 2f / (num2 - num3);
			result.m11 = 2f / (num4 - num5);
			result.m02 = (num2 + num3) / (num2 - num3);
			result.m12 = (num4 + num5) / (num4 - num5);
			result.m22 = -(farClipPlane + nearClipPlane) / (farClipPlane - nearClipPlane);
			result.m32 = -1f;
			result.m23 = -(2f * farClipPlane * nearClipPlane) / (farClipPlane - nearClipPlane);
			result.m10 = 0f;
			result.m13 = 0f;
			result.m20 = 0f;
			result.m21 = 0f;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m33 = 0f;
			return result;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00024280 File Offset: 0x00022480
		public void DrawFrustum(Camera camera)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bic(), Array.Empty<object>());
			if (camera == null)
			{
				return;
			}
			Vector3[] array = new Vector3[4];
			Vector3[] array2 = new Vector3[4];
			this.UpdateComfortZoneClippingPlanes();
			camera.CalculateFrustumCorners(new Rect(0f, 0f, 1f, 1f), this.comfortZoneNearClipPlane, 2, array);
			camera.CalculateFrustumCorners(new Rect(0f, 0f, 1f, 1f), this.comfortZoneFarClipPlane, 2, array2);
			for (int i = 0; i < 4; i++)
			{
				array[i] = camera.transform.TransformPoint(array[i]);
				array2[i] = camera.transform.TransformPoint(array2[i]);
			}
			Gizmos.color = Color.magenta;
			for (int j = 0; j < 4; j++)
			{
				int num = (j + 1) % 4;
				Gizmos.DrawLine(array[j], array[num]);
				Gizmos.DrawLine(array2[j], array2[num]);
				Gizmos.DrawLine(array[j], array2[j]);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002439F File Offset: 0x0002259F
		public void Set3DMode(bool toggle)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bid() + toggle.ToString(), Array.Empty<object>());
			if (toggle)
			{
				this._displayMode = LeiaDisplay.DisplayMode._3D;
			}
			else
			{
				this._displayMode = LeiaDisplay.DisplayMode._2D;
			}
			this.UpdateLightfieldMode();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000243D6 File Offset: 0x000225D6
		private void UpdateLightfieldMode()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.displaymode == LeiaDisplay.DisplayMode._3D)
			{
				Singleton<RenderTrackingDevice>.Instance.DesiredLightfieldMode = RenderTrackingDevice.LightfieldMode.On;
				return;
			}
			Singleton<RenderTrackingDevice>.Instance.DesiredLightfieldMode = RenderTrackingDevice.LightfieldMode.Off;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00024400 File Offset: 0x00022600
		private void HandleLightfieldModeChanged(RenderTrackingDevice.LightfieldMode lightfieldMode)
		{
			if (lightfieldMode == RenderTrackingDevice.LightfieldMode.On)
			{
				this._numViewsX = 2;
				this._depthFactor = this._desiredDepthFactor;
				this._lookAroundFactor = this._desiredLookAroundFactor;
			}
			else
			{
				this._numViewsX = 1;
				this._depthFactor = 0f;
				this._lookAroundFactor = 0f;
			}
			LeiaDisplay.StateChanged();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00024458 File Offset: 0x00022658
		public Vector2Int GetDeviceViewResolution()
		{
			if (Singleton<RenderTrackingDevice>.Instance != null)
			{
				return Singleton<RenderTrackingDevice>.Instance.GetDeviceViewResolution();
			}
			return new Vector2Int(1280, 800);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00024481 File Offset: 0x00022681
		public float GetDeviceSystemDisparityPixels()
		{
			if (Singleton<RenderTrackingDevice>.Instance != null)
			{
				return Singleton<RenderTrackingDevice>.Instance.GetDeviceSystemDisparityPixels();
			}
			return 4f;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000244A0 File Offset: 0x000226A0
		private void EnsureEditorPreivewMaterialInitialized()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bie(), Array.Empty<object>());
			if (this._editorPreviewMaterial == null)
			{
				this._editorPreviewMaterial = new Material(Resources.Load<Shader>(this._editorPreviewShaderName));
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000244D8 File Offset: 0x000226D8
		private void SetEditorPreviewProperties()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bif(), Array.Empty<object>());
			Vector3 vector;
			vector..ctor(-0.333f, 0f, 0.333f);
			Vector3 vector2;
			vector2..ctor(0f, 0f, 0f);
			if (this.GetViewCount() == 2)
			{
				this._editorPreviewMaterial.SetTexture(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.big(), this.GetEyeCamera(0).targetTexture);
				this._editorPreviewMaterial.SetTexture(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bih(), this.GetEyeCamera(1).targetTexture);
			}
			if (this.desiredPreviewMode == LeiaDisplay.EditorPreviewMode.SideBySide)
			{
				this._editorPreviewMaterial.EnableKeyword(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bii());
			}
			else if (this.desiredPreviewMode == LeiaDisplay.EditorPreviewMode.Interlaced)
			{
				this._editorPreviewMaterial.DisableKeyword(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bii());
				this._editorPreviewMaterial.SetInt(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bij(), 2560);
				this._editorPreviewMaterial.SetInt(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bik(), 1600);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bil(), 0.12f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bim(), 0.10389f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bin(), 1.6f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bio(), 0.6926f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bip(), 10.687498f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.biq(), 0.17592919f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bir(), 4.63f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bis(), 1.99f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bit(), 0.05f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.biu(), 2.6666667f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.biv(), 0.33333334f);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.biw(), this.viewerPositionPredicted.x);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bix(), this.viewerPositionPredicted.y);
				this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.biy(), this.viewerPositionPredicted.z);
				this._editorPreviewMaterial.SetVector(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.biz(), vector);
				this._editorPreviewMaterial.SetVector(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bja(), vector2);
			}
			this._editorPreviewMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjb(), 8f);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000247E9 File Offset: 0x000229E9
		// Note: this type is marked as 'beforefieldinit'.
		static LeiaDisplay()
		{
			LeiaDisplay.StateChanged = delegate()
			{
			};
			LeiaDisplay.MinFocalDistance = 0.0001f;
		}

		// Token: 0x0400065A RID: 1626
		[SerializeField]
		private LeiaDisplay.DisplayMode _displayMode;

		// Token: 0x0400065B RID: 1627
		private int _numViewsX = 2;

		// Token: 0x0400065C RID: 1628
		private readonly int _numViewsY = 1;

		// Token: 0x0400065D RID: 1629
		private Vector3 viewerPositionNonPredicted = new Vector3(0f, 0f, 535.964f);

		// Token: 0x0400065E RID: 1630
		private Vector3 viewerPositionPredicted = new Vector3(0f, 0f, 535.964f);

		// Token: 0x0400065F RID: 1631
		private Vector2[] _initialViewOffsets;

		// Token: 0x04000660 RID: 1632
		private RenderTexture _interlacedTexture;

		// Token: 0x04000661 RID: 1633
		private Material _editorPreviewMaterial;

		// Token: 0x04000662 RID: 1634
		private ScreenOrientation _previousOrientation;

		// Token: 0x04000663 RID: 1635
		private float calculatedVirtualHeight;

		// Token: 0x04000664 RID: 1636
		private bool drawCameraBounds;

		// Token: 0x04000665 RID: 1637
		[SerializeField]
		private int antiAliasingLevel;

		// Token: 0x04000666 RID: 1638
		private bool divideByPerspectiveFactor = true;

		// Token: 0x04000667 RID: 1639
		private LeiaDisplay.EditorPreviewMode desiredPreviewMode;

		// Token: 0x04000668 RID: 1640
		private bool useCameraClippingPlanes;

		// Token: 0x04000669 RID: 1641
		[Range(0.1f, 20f)]
		private float maxDisparity = 10f;

		// Token: 0x0400066A RID: 1642
		[SerializeField]
		private float fovFactor = 1f;

		// Token: 0x0400066B RID: 1643
		private bool cameraShiftEnabled;

		// Token: 0x0400066C RID: 1644
		private int scriptableRendererIndex;

		// Token: 0x0400066D RID: 1645
		private bool _initialized;

		// Token: 0x0400066E RID: 1646
		[SerializeField]
		private float _desiredDepthFactor = 1f;

		// Token: 0x0400066F RID: 1647
		[SerializeField]
		private float _depthFactor = 1f;

		// Token: 0x04000670 RID: 1648
		[SerializeField]
		private float _desiredLookAroundFactor = 1f;

		// Token: 0x04000671 RID: 1649
		[SerializeField]
		private float _lookAroundFactor = 1f;

		// Token: 0x04000673 RID: 1651
		public float VirtualHeight = 10f;

		// Token: 0x04000674 RID: 1652
		private float comfortZoneNearClipPlane;

		// Token: 0x04000675 RID: 1653
		private float comfortZoneFarClipPlane;

		// Token: 0x04000676 RID: 1654
		public Camera DriverCamera;

		// Token: 0x04000677 RID: 1655
		[HideInInspector]
		public LeiaDisplay.ControlMode mode;

		// Token: 0x04000678 RID: 1656
		public Head ViewersHead;

		// Token: 0x04000679 RID: 1657
		private Camera UIEventCamera;

		// Token: 0x0400067A RID: 1658
		public static float MinFocalDistance;

		// Token: 0x0400067B RID: 1659
		private float focalDistance;

		// Token: 0x0400067C RID: 1660
		[SerializeField]
		public bool LateLatchingEnabled;

		// Token: 0x02000128 RID: 296
		public enum DisplayMode
		{
			// Token: 0x0400067E RID: 1662
			_3D,
			// Token: 0x0400067F RID: 1663
			_2D
		}

		// Token: 0x02000129 RID: 297
		[SerializeField]
		public enum EditorPreviewMode
		{
			// Token: 0x04000681 RID: 1665
			Interlaced,
			// Token: 0x04000682 RID: 1666
			SideBySide
		}

		// Token: 0x0200012A RID: 298
		public enum ControlMode
		{
			// Token: 0x04000684 RID: 1668
			DisplayDriven,
			// Token: 0x04000685 RID: 1669
			CameraDriven
		}
	}
}
