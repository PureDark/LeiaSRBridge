using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000118 RID: 280
	[DefaultExecutionOrder(3)]
	public abstract class LeiaFocus : MonoBehaviour
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00020CC9 File Offset: 0x0001EEC9
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00020CD1 File Offset: 0x0001EED1
		public bool SetFocalDistance
		{
			get
			{
				return this._setFocalDistance;
			}
			set
			{
				this._setFocalDistance = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00020CDA File Offset: 0x0001EEDA
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00020CE2 File Offset: 0x0001EEE2
		public bool SetDepthFactor
		{
			get
			{
				return this._setDepthFactor;
			}
			set
			{
				this._setDepthFactor = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00020CEB File Offset: 0x0001EEEB
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00020CF3 File Offset: 0x0001EEF3
		public float DepthScale
		{
			get
			{
				return this._depthScale;
			}
			set
			{
				this._depthScale = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00020D0B File Offset: 0x0001EF0B
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00020D18 File Offset: 0x0001EF18
		public float MinDepthFactor
		{
			get
			{
				return this._depthFactorRange.min;
			}
			set
			{
				this._depthFactorRange.min = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00020D35 File Offset: 0x0001EF35
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00020D42 File Offset: 0x0001EF42
		public float MaxDepthFactor
		{
			get
			{
				return this._depthFactorRange.max;
			}
			set
			{
				this._depthFactorRange.max = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00020D5F File Offset: 0x0001EF5F
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00020D6C File Offset: 0x0001EF6C
		public float MinFocalDistance
		{
			get
			{
				return this._focalDistanceRange.min;
			}
			set
			{
				this._focalDistanceRange.min = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00020D89 File Offset: 0x0001EF89
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00020D96 File Offset: 0x0001EF96
		public float MaxFocalDistance
		{
			get
			{
				return this._focalDistanceRange.max;
			}
			set
			{
				this._focalDistanceRange.max = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00020DB3 File Offset: 0x0001EFB3
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00020DBB File Offset: 0x0001EFBB
		public float FocusSpeed
		{
			get
			{
				return this._focusSpeed;
			}
			set
			{
				this._focusSpeed = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00020DC4 File Offset: 0x0001EFC4
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00020DCC File Offset: 0x0001EFCC
		public float DepthFocusSpeed
		{
			get
			{
				return this._depthFocusSpeed;
			}
			set
			{
				this._depthFocusSpeed = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00020DD5 File Offset: 0x0001EFD5
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00020DDD File Offset: 0x0001EFDD
		public float FocusOffset
		{
			get
			{
				return this._focusOffset;
			}
			set
			{
				this._focusOffset = value;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00020DE8 File Offset: 0x0001EFE8
		protected virtual void OnEnable()
		{
			this.localPositionPrev = base.transform.parent.localPosition;
			this.leiaDisplay = base.GetComponent<LeiaDisplay>();
			this.targetFocalDistanceHistory = new ds(5);
			this.targetDepthFactorHistory = new ds(15);
			this.targetFocalDistanceHistory.bhw(this.leiaDisplay.FocalDistance);
			this.targetDepthFactorHistory.bhw(this.leiaDisplay.DepthFactor);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00020E5C File Offset: 0x0001F05C
		protected void SetTargetFocalDistance(float newTargetFocalDistance)
		{
			this.targetFocalDistanceHistory.bhw(newTargetFocalDistance);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00020E6A File Offset: 0x0001F06A
		protected void SetTargetDepthFactor(float newTargetDepthFactor)
		{
			this.targetDepthFactorHistory.bhw(newTargetDepthFactor);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00020E78 File Offset: 0x0001F078
		private void UpdateFocalDistance()
		{
			float num = this.targetFocalDistanceHistory.dco;
			if (Mathf.Abs(num - this.targetFocalDistance) > this.focalDistanceChangeThreshold * this.targetFocalDistance && Mathf.Abs(num - this.targetFocalDistancePrev) > this.focalDistanceChangeThreshold * this.targetFocalDistance)
			{
				this.targetFocalDistancePrev = this.targetFocalDistance;
				this.targetFocalDistance = num;
			}
			float num2 = this.CalculateNewFocalDistance();
			num2 = Mathf.Clamp(num2, this._focalDistanceRange.min, this._focalDistanceRange.max);
			if (this.FocusOffset + this.targetFocalDistance < 1E-05f)
			{
				this.FocusOffset = -this.targetFocalDistance + 1E-05f;
				num2 = this.CalculateNewFocalDistance();
			}
			this.leiaDisplay.FocalDistance = num2;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00020F3C File Offset: 0x0001F13C
		private float CalculateNewFocalDistance()
		{
			float num = Mathf.Min(this.FocusSpeed * Time.deltaTime * 60f, 1f);
			float num2 = this.leiaDisplay.FocalDistance + (this.targetFocalDistance + this.FocusOffset - this.leiaDisplay.FocalDistance) * num;
			return Mathf.Max(this.leiaDisplay.HeadCamera.nearClipPlane, num2);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		private void UpdateDepthFactor()
		{
			float num = this.targetDepthFactorHistory.dco;
			if (Mathf.Abs(num - this.targetDepthFactor) > this.depthFactorChangeThreshold * this.targetDepthFactor && Mathf.Abs(num - this.targetDepthFactorPrev) > this.depthFactorChangeThreshold * this.targetDepthFactor)
			{
				this.targetDepthFactorPrev = this.targetDepthFactor;
				this.targetDepthFactor = Mathf.Clamp(num, this._depthFactorRange.min, this._depthFactorRange.max);
			}
			float num2 = Mathf.Min(this.DepthFocusSpeed * Time.deltaTime * 60f, 1f);
			this.leiaDisplay.DepthFactor += (this.targetDepthFactor * this.DepthScale - this.leiaDisplay.DepthFactor) * num2;
			this.leiaDisplay.DepthFactor = Mathf.Clamp(this.leiaDisplay.DepthFactor, this.MinDepthFactor, this.MaxDepthFactor);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00021098 File Offset: 0x0001F298
		protected virtual void LateUpdate()
		{
			if (this.leiaDisplay.mode == LeiaDisplay.ControlMode.DisplayDriven)
			{
				return;
			}
			if (this.SetDepthFactor)
			{
				this.UpdateDepthFactor();
			}
			if (this.SetFocalDistance)
			{
				this.UpdateFocalDistance();
			}
			if (this.FocusSpace == LeiaFocus.FocusSpaceType.World)
			{
				Vector3 vector = base.transform.parent.localPosition - this.localPositionPrev;
				this.leiaDisplay.FocalDistance -= vector.z;
				this.localPositionPrev = base.transform.parent.localPosition;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00021122 File Offset: 0x0001F322
		public void AddOffset(float offset)
		{
			this.targetFocalDistanceHistory.bhy(offset);
		}

		// Token: 0x04000605 RID: 1541
		protected LeiaDisplay leiaDisplay;

		// Token: 0x04000606 RID: 1542
		[Tooltip("The range of allowable depth factor values for the Leia Display. Can be used to prevent depth from becoming too large or too small.")]
		[SerializeField]
		private MinMaxPair _depthFactorRange = new MinMaxPair(0.01f, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgx(), 10f, 100f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgy());

		// Token: 0x04000607 RID: 1543
		[Tooltip("The range of allowable focal distances for the Leia Display. Can be used to prevent the focal plane from becoming too far away or too close to the camera.")]
		[SerializeField]
		private MinMaxPair _focalDistanceRange = new MinMaxPair(1f, 1E-05f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgx(), 1000f, 10000000f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgy());

		// Token: 0x04000608 RID: 1544
		[Tooltip("After the LeiaDisplay's baseline is determined by the auto depth algorithm, and is clamped between the min and max baseline, it will be scaled by this amount. A value of 1 is the default, recommended value. This setting can be used to implement a settings slider which allows a user of the app to adjust how much depth it has.")]
		[SerializeField]
		[Range(0.01f, 1f)]
		private float _depthScale = 0.1f;

		// Token: 0x04000609 RID: 1545
		[Tooltip("Minimum percentage the computed focal distance must change by before the target value is updated. If the focus is jittery, try increasing this value.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float focalDistanceChangeThreshold = 0.01f;

		// Token: 0x0400060A RID: 1546
		[Tooltip("Minimum percentage the computed depth factor must change by before the target value is updated. If the Leia Display bounds are jittery, try increasing this value.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float depthFactorChangeThreshold = 0.05f;

		// Token: 0x0400060B RID: 1547
		protected float targetFocalDistance;

		// Token: 0x0400060C RID: 1548
		private float targetFocalDistancePrev;

		// Token: 0x0400060D RID: 1549
		private float targetDepthFactor;

		// Token: 0x0400060E RID: 1550
		private float targetDepthFactorPrev;

		// Token: 0x0400060F RID: 1551
		[SerializeField]
		private bool _setFocalDistance = true;

		// Token: 0x04000610 RID: 1552
		[SerializeField]
		private bool _setDepthFactor = true;

		// Token: 0x04000611 RID: 1553
		[Tooltip("To have the focal plane stay in the same place relative to the camera when the camera moves forward / backward on it's local z-axis, use the \"Camera\" focus space. To have the focal plane stay in the same place relative to the world when the camera moves forward / backward on it's local z-axis, use \"World\" focus space.")]
		[SerializeField]
		private LeiaFocus.FocusSpaceType FocusSpace;

		// Token: 0x04000612 RID: 1554
		private const float idealFramerate = 60f;

		// Token: 0x04000613 RID: 1555
		[Tooltip("Speed at which the focal distance changes from its current value")]
		[Range(0.01f, 1f)]
		[SerializeField]
		private float _focusSpeed = 0.5f;

		// Token: 0x04000614 RID: 1556
		[Tooltip("Speed at which the depth factor changes from its current value")]
		[Range(0.01f, 1f)]
		[SerializeField]
		private float _depthFocusSpeed = 0.1f;

		// Token: 0x04000615 RID: 1557
		[Tooltip("Offset the LeiaDisplay focal distance from its computed optimal value by this amount. This can be useful to bring objects more to the foreground to make them pop out more, or push them more into the background so that (for example) UI can be drawn over them.")]
		[SerializeField]
		private float _focusOffset;

		// Token: 0x04000616 RID: 1558
		private ds targetFocalDistanceHistory;

		// Token: 0x04000617 RID: 1559
		private const int targetFocalDistanceHistoryLength = 5;

		// Token: 0x04000618 RID: 1560
		private ds targetDepthFactorHistory;

		// Token: 0x04000619 RID: 1561
		private const int targetDepthFactorHistoryLength = 15;

		// Token: 0x0400061A RID: 1562
		private Vector3 localPositionPrev;

		// Token: 0x02000119 RID: 281
		public enum FocusSpaceType
		{
			// Token: 0x0400061C RID: 1564
			Camera,
			// Token: 0x0400061D RID: 1565
			World
		}
	}
}
