using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using SRUnity;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000125 RID: 293
	public class FaceTrackingStateEngine : Singleton<FaceTrackingStateEngine>
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00022C68 File Offset: 0x00020E68
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00022C70 File Offset: 0x00020E70
		public FaceTrackingStateEngine.FaceTransitionState faceTransitionState
		{
			get
			{
				return this._faceTransitionState;
			}
			set
			{
				this._faceTransitionState = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00022C79 File Offset: 0x00020E79
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00022C81 File Offset: 0x00020E81
		public float eyeTrackingAnimatedBaselineScalar
		{
			get
			{
				return this._eyeTrackingAnimatedBaselineScalar;
			}
			set
			{
				this._eyeTrackingAnimatedBaselineScalar = value;
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00022C8A File Offset: 0x00020E8A
		private void Awake()
		{
			this.ResetTrackingState();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00022C92 File Offset: 0x00020E92
		private void Update()
		{
			if (SRCore.IsSimulatedRealityAvailable())
			{
				this.Handle2DTrigger();
				this.UpdateFaceTrackingState();
			}
			this.UpdateBaseline();
			this.LogTrackingStatus();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00022CB3 File Offset: 0x00020EB3
		private void Handle2DTrigger()
		{
			if (!SrRenderModeHint.ShouldRender3D() && !this._triggered2D)
			{
				this._triggered2D = true;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00022CCC File Offset: 0x00020ECC
		private void UpdateFaceTrackingState()
		{
			if (this._triggered2D)
			{
				this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.SlidingCameras;
				this._triggered2D = false;
			}
			Vector3 eyePosition = SimulatedRealityModule<SRHead>.Instance.GetEyePosition(@do.bgf(null));
			Vector3 defaultHeadPosition = SimulatedRealityModule<SRHead>.Instance.GetDefaultHeadPosition(@do.bgf(null));
			if (eyePosition != defaultHeadPosition)
			{
				this.HandleEyePositionDetected();
				return;
			}
			this.HandleNoEyePositionDetected();
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00022D25 File Offset: 0x00020F25
		private void HandleEyePositionDetected()
		{
			if (this.faceTransitionState == FaceTrackingStateEngine.FaceTransitionState.SlidingCameras || this.faceTransitionState == FaceTrackingStateEngine.FaceTransitionState.NoFace)
			{
				this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.IncreasingBaseline;
			}
			this._previousFaceDetected = true;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00022D46 File Offset: 0x00020F46
		private void HandleNoEyePositionDetected()
		{
			if (this.faceTransitionState == FaceTrackingStateEngine.FaceTransitionState.FaceLocked && this._previousFaceDetected)
			{
				this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.ReducingBaseline;
			}
			this._previousFaceDetected = false;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000238E File Offset: 0x0000058E
		private void LogTrackingStatus()
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00022D67 File Offset: 0x00020F67
		private void OnApplicationFocus(bool focus)
		{
			if (focus)
			{
				this.ResetTrackingState();
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00022D72 File Offset: 0x00020F72
		private void ResetTrackingState()
		{
			this._triggered2D = false;
			this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.NoFace;
			this._previousFaceDetected = false;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00022D8C File Offset: 0x00020F8C
		private void UpdateBaseline()
		{
			switch (this.faceTransitionState)
			{
			case FaceTrackingStateEngine.FaceTransitionState.NoFace:
				if (Singleton<RenderTrackingDevice>.Instance.NumFaces > 0)
				{
					this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.SlidingCameras;
					return;
				}
				break;
			case FaceTrackingStateEngine.FaceTransitionState.FaceLocked:
				this.eyeTrackingAnimatedBaselineScalar = 1f;
				return;
			case FaceTrackingStateEngine.FaceTransitionState.ReducingBaseline:
				this.eyeTrackingAnimatedBaselineScalar += (0f - this.eyeTrackingAnimatedBaselineScalar) * Mathf.Min(Time.deltaTime * 5f, 1f);
				if (this.eyeTrackingAnimatedBaselineScalar < 0.1f)
				{
					this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.SlidingCameras;
					return;
				}
				break;
			case FaceTrackingStateEngine.FaceTransitionState.SlidingCameras:
				this.eyeTrackingAnimatedBaselineScalar = 0f;
				return;
			case FaceTrackingStateEngine.FaceTransitionState.IncreasingBaseline:
				this.eyeTrackingAnimatedBaselineScalar += (1f - this.eyeTrackingAnimatedBaselineScalar) * Mathf.Min(Time.deltaTime * 5f, 1f);
				if (Mathf.Abs(this.eyeTrackingAnimatedBaselineScalar - 1f) < 0.1f)
				{
					this.faceTransitionState = FaceTrackingStateEngine.FaceTransitionState.FaceLocked;
					return;
				}
				break;
			default:
				Debug.LogError(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhm() + this.faceTransitionState.ToString());
				break;
			}
		}

		// Token: 0x04000650 RID: 1616
		private FaceTrackingStateEngine.FaceTransitionState _faceTransitionState;

		// Token: 0x04000651 RID: 1617
		private bool _previousFaceDetected;

		// Token: 0x04000652 RID: 1618
		private bool _triggered2D;

		// Token: 0x04000653 RID: 1619
		private float _eyeTrackingAnimatedBaselineScalar;

		// Token: 0x02000126 RID: 294
		public enum FaceTransitionState
		{
			// Token: 0x04000655 RID: 1621
			NoFace,
			// Token: 0x04000656 RID: 1622
			FaceLocked,
			// Token: 0x04000657 RID: 1623
			ReducingBaseline,
			// Token: 0x04000658 RID: 1624
			SlidingCameras,
			// Token: 0x04000659 RID: 1625
			IncreasingBaseline
		}
	}
}
