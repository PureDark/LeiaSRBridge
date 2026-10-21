using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using SRUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace LeiaUnity
{
	// Token: 0x02000114 RID: 276
	public class RenderTrackingDevice : Singleton<RenderTrackingDevice>
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000779 RID: 1913 RVA: 0x000201A8 File Offset: 0x0001E3A8
		// (remove) Token: 0x0600077A RID: 1914 RVA: 0x000201E0 File Offset: 0x0001E3E0
		public event Action<RenderTrackingDevice.LightfieldMode> LightfieldModeChanged = delegate(RenderTrackingDevice.LightfieldMode <p0>)
		{
		};

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00020215 File Offset: 0x0001E415
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0002021D File Offset: 0x0001E41D
		public RenderTrackingDevice.LightfieldMode DesiredLightfieldMode
		{
			get
			{
				return this._desiredLightfieldMode;
			}
			set
			{
				if (this._desiredLightfieldMode != value)
				{
					this._desiredLightfieldMode = value;
					this.Update2D3D();
					this.LightfieldModeChanged(value);
				}
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00020241 File Offset: 0x0001E441
		private void OnEnable()
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgh(), Array.Empty<object>());
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00020263 File Offset: 0x0001E463
		private void OnDisable()
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgi(), Array.Empty<object>());
			SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00020285 File Offset: 0x0001E485
		private void OnResume()
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgj(), Array.Empty<object>());
			this.Update2D3D();
			SimulatedRealityModule<SRCore>.Instance.GetSrContext();
			SimulatedRealityModule<SRCore>.Instance.InitializeContext();
			SimulatedRealityModule<SRCore>.Instance.InitModule();
			this.DesiredLightfieldMode = RenderTrackingDevice.LightfieldMode.On;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000202C2 File Offset: 0x0001E4C2
		private void OnPause()
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgk(), Array.Empty<object>());
			if (this.Is3DMode())
			{
				this.Set3DMode(false);
			}
			this.DesiredLightfieldMode = RenderTrackingDevice.LightfieldMode.Off;
			SimulatedRealityModule<SRCore>.Instance.DestroyModule();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000238E File Offset: 0x0000058E
		private void OnApplicationFocus(bool focus)
		{
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000238E File Offset: 0x0000058E
		private void OnApplicationPause(bool pauseStatus)
		{
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000202F3 File Offset: 0x0001E4F3
		private void OnApplicationQuit()
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgl(), Array.Empty<object>());
			this.Set3DMode(false);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002030C File Offset: 0x0001E50C
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			LeiaDisplay leiaDisplay = Object.FindObjectOfType<LeiaDisplay>();
			if (leiaDisplay != null && leiaDisplay.enabled)
			{
				this.Update2D3D();
				return;
			}
			this.Set3DMode(false);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00020340 File Offset: 0x0001E540
		public void Initialize()
		{
			string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bap();
			GameObject gameObject = GameObject.Find(text);
			if (gameObject == null)
			{
				gameObject = new GameObject(text);
				gameObject.transform.position = Vector3.zero;
			}
			if (gameObject.GetComponent<SimulatedRealityCamera>() == null)
			{
				this.SRCam = gameObject.gameObject.AddComponent<SimulatedRealityCamera>();
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00020399 File Offset: 0x0001E599
		public void Render(LeiaDisplay leiaDisplay, ref RenderTexture outputTexture)
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgm(), Array.Empty<object>());
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000203AA File Offset: 0x0001E5AA
		public bool GetLateLatchingFromLeiaDisplay()
		{
			return Object.FindObjectOfType<LeiaDisplay>().LateLatchingEnabled;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000203B6 File Offset: 0x0001E5B6
		public void EnableLateLatchingDX11(bool enable)
		{
			SimulatedRealityModule<SRRender>.Instance.GetWeaver().EnableLateLatchingDX11(enable);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000203C8 File Offset: 0x0001E5C8
		public bool IsLateLatchingEnabledDX11()
		{
			return SimulatedRealityModule<SRRender>.Instance.GetWeaver().IsLateLatchingEnabledDX11();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000203D9 File Offset: 0x0001E5D9
		public void SetLatency(int latency)
		{
			SimulatedRealityModule<SRRender>.Instance.GetWeaver().PredictingWeaverSetLatency(latency);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000203EB File Offset: 0x0001E5EB
		public void SetLatencyInFrames(int latency)
		{
			SimulatedRealityModule<SRRender>.Instance.GetWeaver().PredictingWeaverSetLatencyInFrames(latency);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000203FD File Offset: 0x0001E5FD
		public int GetLatency()
		{
			return (int)SRWeaver.getLatency();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00020405 File Offset: 0x0001E605
		public int NumFaces
		{
			get
			{
				if (!SrRenderModeHint.ShouldRender3D())
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00020414 File Offset: 0x0001E614
		public void SetTrackerEnabled(bool trackerEnabled)
		{
			LogUtil.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgn(), Array.Empty<object>());
			SrRenderModeHint srRenderModeHint = new SrRenderModeHint();
			if (trackerEnabled)
			{
				srRenderModeHint.Prefer3D();
				return;
			}
			srRenderModeHint.Prefer2D();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00020448 File Offset: 0x0001E648
		public Vector3[] GetEyesPosition()
		{
			Vector3[] eyesPosition = SimulatedRealityModule<SREyes>.Instance.GetEyesPosition(@do.bgf(null));
			Vector3 vector = new Vector3(eyesPosition[0].x, eyesPosition[0].y, -eyesPosition[0].z) * 10f;
			Vector3 vector2 = new Vector3(eyesPosition[1].x, eyesPosition[1].y, -eyesPosition[1].z) * 10f;
			return new Vector3[]
			{
				vector,
				vector2
			};
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000204E8 File Offset: 0x0001E6E8
		public Vector3 GetNonPredictedFacePosition()
		{
			Vector3 eyePosition = SimulatedRealityModule<SREyes>.Instance.GetEyePosition(@do.bgf(null));
			return new Vector3(eyePosition.x, eyePosition.y, -eyePosition.z) * 10f;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00020528 File Offset: 0x0001E728
		public Vector3[] GetLookAroundEyesPosition()
		{
			Vector3[] eyesPosition = SimulatedRealityModule<SREyes>.Instance.GetEyesPosition(@do.bgf(null));
			this.leftEyePosition = new Vector3(eyesPosition[0].x, eyesPosition[0].y, -eyesPosition[0].z) * 10f;
			this.rightEyePosition = new Vector3(eyesPosition[1].x, eyesPosition[1].y, -eyesPosition[1].z) * 10f;
			return new Vector3[]
			{
				this.leftEyePosition,
				this.rightEyePosition
			};
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000205DA File Offset: 0x0001E7DA
		private void Update2D3D()
		{
			if (this.DesiredLightfieldMode == RenderTrackingDevice.LightfieldMode.Off)
			{
				this.Set3DMode(false);
				return;
			}
			if (this.DesiredLightfieldMode == RenderTrackingDevice.LightfieldMode.On)
			{
				this.Set3DMode(true);
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000205FC File Offset: 0x0001E7FC
		public void Set3DMode(bool toggle)
		{
			if (toggle)
			{
				this.renderHint.Prefer3D();
			}
			else
			{
				this.renderHint.Force2D();
			}
			this.SetTrackerEnabled(toggle);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00020620 File Offset: 0x0001E820
		public bool Is3DMode()
		{
			return SrRenderModeHint.ShouldRender3D();
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00020627 File Offset: 0x0001E827
		public Vector2Int GetDevicePanelResolution()
		{
			new Vector2Int(2560, 1600);
			return SimulatedRealityModule<SRCore>.Instance.getResolution();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00020643 File Offset: 0x0001E843
		public Vector2Int GetDeviceViewResolution()
		{
			new Vector2Int(1280, 800);
			return SimulatedRealityModule<SRCore>.Instance.getResolution() / 2;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00020665 File Offset: 0x0001E865
		public float GetDeviceSystemDisparityPixels()
		{
			return 4f;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002066C File Offset: 0x0001E86C
		public Vector2 GetDeviceDotPitchInMM()
		{
			new Vector2(0.10389f, 0.104375f);
			float dotPitch = SimulatedRealityModule<SRCore>.Instance.getDotPitch();
			return new Vector2(dotPitch, dotPitch);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0002069B File Offset: 0x0001E89B
		public Vector2 GetDisplaySizeInMM()
		{
			new Vector2(266f, 168f);
			return SimulatedRealityModule<SRCore>.Instance.getPhysicalSize() * 10f;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000206C4 File Offset: 0x0001E8C4
		public float GetViewingDistanceInMM()
		{
			float result = 450f;
			if (SRCore.IsSimulatedRealityAvailable() && this.GetDOverN() != 0f && this.GetPX() != 0f && this.GetPixelPitch() != 0f)
			{
				result = 2f * this.IPDInMM * this.GetDOverN() / (this.GetPX() * this.GetPixelPitch());
			}
			return result;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00020731 File Offset: 0x0001E931
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00020728 File Offset: 0x0001E928
		public float IPDInMM
		{
			get
			{
				return this.ipdInMM;
			}
			set
			{
				this.ipdInMM = value;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00020739 File Offset: 0x0001E939
		public float GetDOverN()
		{
			return SimulatedRealityModule<SRCore>.Instance.getDoN();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00020745 File Offset: 0x0001E945
		public float GetPX()
		{
			return SimulatedRealityModule<SRCore>.Instance.getPx();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00020751 File Offset: 0x0001E951
		public float GetPixelPitch()
		{
			return SimulatedRealityModule<SRCore>.Instance.getDotPitch() * 10f;
		}

		// Token: 0x040005EC RID: 1516
		private RenderTrackingDevice.LightfieldMode _desiredLightfieldMode;

		// Token: 0x040005ED RID: 1517
		private int _numFaces;

		// Token: 0x040005EF RID: 1519
		private SimulatedRealityCamera SRCam;

		// Token: 0x040005F0 RID: 1520
		private SrRenderModeHint renderHint = new SrRenderModeHint();

		// Token: 0x040005F1 RID: 1521
		public Vector3 leftEyePosition;

		// Token: 0x040005F2 RID: 1522
		public Vector3 rightEyePosition;

		// Token: 0x040005F3 RID: 1523
		private float ipdInMM = 63f;

		// Token: 0x02000115 RID: 277
		public enum LightfieldMode
		{
			// Token: 0x040005F5 RID: 1525
			On,
			// Token: 0x040005F6 RID: 1526
			Off
		}
	}
}
