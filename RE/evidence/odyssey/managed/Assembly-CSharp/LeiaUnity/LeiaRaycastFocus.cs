using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200011A RID: 282
	[RequireComponent(typeof(LeiaDisplay))]
	[DisallowMultipleComponent]
	[HelpURL("https://support.leiainc.com/sdk/unity-plugin/leiasr-tm-unity-plugin-guide/extensions/auto-focus/leiaraycastfocus")]
	public class LeiaRaycastFocus : LeiaFocus
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x000211DA File Offset: 0x0001F3DA
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x000211E2 File Offset: 0x0001F3E2
		public LayerMask layers
		{
			get
			{
				return this._layers;
			}
			set
			{
				this._layers = value;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000211EB File Offset: 0x0001F3EB
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000211F3 File Offset: 0x0001F3F3
		protected override void LateUpdate()
		{
			if (this.leiaDisplay == null || this.leiaDisplay.mode == LeiaDisplay.ControlMode.DisplayDriven)
			{
				return;
			}
			this.CalculateTargetConvergenceAndBaseline();
			base.LateUpdate();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0002121D File Offset: 0x0001F41D
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00021225 File Offset: 0x0001F425
		public float MaxRaycastDistance
		{
			get
			{
				return this._maxRaycastDistance;
			}
			set
			{
				this._maxRaycastDistance = Mathf.Max(value, 0f);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00021238 File Offset: 0x0001F438
		private void CalculateTargetConvergenceAndBaseline()
		{
			this.samples = Mathf.Max(1, this.samples);
			if (this.samples != this.previous_samples)
			{
				Array.Resize<Vector2>(ref this.cameraNearPlaneRaysOrigins, this.samples);
				int num = Mathf.CeilToInt(Mathf.Sqrt((float)this.samples));
				int num2 = num + 1;
				float num3 = 1f / (float)num2;
				int num4 = 0;
				this.cameraNearPlaneRaysOrigins[num4++] = new Vector2(0f, 0f);
				float num5 = -0.5f + num3;
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num; j++)
					{
						if (num4 < this.samples)
						{
							this.cameraNearPlaneRaysOrigins[num4] = new Vector2(num5 + (float)i * num3, num5 + (float)j * num3);
							num4++;
						}
					}
				}
				this.previous_samples = this.samples;
			}
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			Vector3 position = this.leiaDisplay.DriverCamera.transform.position;
			Vector3 right = this.leiaDisplay.DriverCamera.transform.right;
			Vector3 forward = this.leiaDisplay.DriverCamera.transform.forward;
			Vector3 up = this.leiaDisplay.DriverCamera.transform.up;
			float nearClipPlane = this.leiaDisplay.DriverCamera.nearClipPlane;
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			if (this.leiaDisplay.DriverCamera.orthographic)
			{
				float orthographicSize = this.leiaDisplay.DriverCamera.orthographicSize;
				float num6 = orthographicSize * this.leiaDisplay.DriverCamera.aspect;
				vector = position + forward * nearClipPlane - up * orthographicSize - right * num6;
				vector2 = position + forward * nearClipPlane + up * orthographicSize - right * num6;
				vector3 = position + forward * nearClipPlane + up * orthographicSize + right * num6;
				vector4 = position + forward * nearClipPlane - up * orthographicSize + right * num6;
			}
			else
			{
				this.leiaDisplay.DriverCamera.CalculateFrustumCorners(new Rect(0f, 0f, 1f, 1f), nearClipPlane, 2, this.frustrumCorners);
				vector = position + localToWorldMatrix.MultiplyVector(this.frustrumCorners[0]);
				vector2 = position + localToWorldMatrix.MultiplyVector(this.frustrumCorners[1]);
				vector3 = position + localToWorldMatrix.MultiplyVector(this.frustrumCorners[2]);
				vector4 = position + localToWorldMatrix.MultiplyVector(this.frustrumCorners[3]);
			}
			this.nearPlaneTransformMatrix = Matrix4x4.Translate(Vector3.LerpUnclamped(vector, vector3, 0.5f));
			this.nearPlaneTransformMatrix.SetColumn(0, vector4 - vector);
			this.nearPlaneTransformMatrix.SetColumn(1, vector2 - vector);
			if (this.showDebugRaycasts)
			{
				this.offsettedNearPlaneTM = this.nearPlaneTransformMatrix;
				this.offsettedNearPlaneTM.SetColumn(3, this.offsettedNearPlaneTM.GetColumn(3) + forward * 0.001f);
			}
			this.furthestDistance = 0f;
			this.closestDistance = float.MaxValue;
			this.avgDistance = 0f;
			this.hits = 0f;
			if (this.leiaDisplay.DriverCamera.orthographic)
			{
				float num7 = this.leiaDisplay.DriverCamera.orthographicSize * 2f;
				for (int k = 0; k < this.cameraNearPlaneRaysOrigins.Length; k++)
				{
					Vector3 startPoint = this.nearPlaneTransformMatrix.MultiplyPoint3x4(this.cameraNearPlaneRaysOrigins[k]);
					this.SampleSpherecast(startPoint, forward, num7 / ((float)this.samples * 2f), k);
				}
			}
			else
			{
				float num7 = this.leiaDisplay.FocalDistance * Mathf.Sin(this.leiaDisplay.DriverCamera.fieldOfView * 0.017453292f) / 2f;
				for (int l = 0; l < this.cameraNearPlaneRaysOrigins.Length; l++)
				{
					Vector3 vector5 = this.nearPlaneTransformMatrix.MultiplyPoint3x4(this.cameraNearPlaneRaysOrigins[l]);
					Vector3 normalized = (vector5 - this.leiaDisplay.DriverCamera.transform.position).normalized;
					this.SampleSpherecast(vector5, normalized, num7 / ((float)this.samples * 2f), l);
				}
			}
			if (this.hits > 0f)
			{
				this.avgDistance /= this.hits;
				float targetFocalDistance = this.avgDistance + this.leiaDisplay.DriverCamera.nearClipPlane;
				base.SetTargetFocalDistance(targetFocalDistance);
				float recommendedDepthFactorWithNearPlane = LeiaDisplayUtils.GetRecommendedDepthFactorWithNearPlane(this.leiaDisplay, this.closestDistance);
				float recommendedDepthFactorWithFarPlane = LeiaDisplayUtils.GetRecommendedDepthFactorWithFarPlane(this.leiaDisplay, this.furthestDistance);
				float targetDepthFactor = Mathf.Min(recommendedDepthFactorWithNearPlane, recommendedDepthFactorWithFarPlane);
				base.SetTargetDepthFactor(targetDepthFactor);
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00021790 File Offset: 0x0001F990
		private void SampleSpherecast(Vector3 startPoint, Vector3 direction, float radius, int sampleIndex)
		{
			RaycastHit raycastHit;
			Physics.SphereCast(startPoint, radius, direction, ref raycastHit, this.MaxRaycastDistance, this.layers);
			if (raycastHit.collider != null)
			{
				if (raycastHit.distance > this.furthestDistance)
				{
					this.furthestDistance = raycastHit.distance;
				}
				if (raycastHit.distance < this.closestDistance)
				{
					this.closestDistance = raycastHit.distance;
				}
				float num = Mathf.Pow(raycastHit.distance, 1.1f);
				float num2 = 1f / Mathf.Max(0.1f, num);
				this.avgDistance += raycastHit.distance * num2;
				this.hits += 1f * num2;
				if (this.showDebugRaycasts)
				{
					Debug.DrawLine(startPoint, startPoint + direction * raycastHit.distance, Color.green);
					Vector2 vector = this.cameraNearPlaneRaysOrigins[sampleIndex];
					Vector2 vector2 = vector + new Vector2(0f, -this.crossScale);
					Vector2 vector3 = vector + new Vector2(0f, this.crossScale);
					Vector2 vector4 = vector + new Vector2(-this.crossScale, 0f);
					Vector2 vector5 = vector + new Vector2(this.crossScale, 0f);
					Debug.DrawLine(this.offsettedNearPlaneTM.MultiplyPoint3x4(vector3), this.nearPlaneTransformMatrix.MultiplyPoint3x4(vector2), Color.green);
					Debug.DrawLine(this.offsettedNearPlaneTM.MultiplyPoint3x4(vector4), this.nearPlaneTransformMatrix.MultiplyPoint3x4(vector5), Color.green);
					return;
				}
			}
			else if (this.showDebugRaycasts)
			{
				Debug.DrawRay(startPoint, direction * this.targetFocalDistance * 2f, Color.red);
				Vector2 vector6 = this.cameraNearPlaneRaysOrigins[sampleIndex];
				Vector2 vector7 = vector6 + new Vector2(0f, -this.crossScale);
				Vector2 vector8 = vector6 + new Vector2(0f, this.crossScale);
				Vector2 vector9 = vector6 + new Vector2(-this.crossScale, 0f);
				Vector2 vector10 = vector6 + new Vector2(this.crossScale, 0f);
				Debug.DrawLine(this.offsettedNearPlaneTM.MultiplyPoint3x4(vector8), this.nearPlaneTransformMatrix.MultiplyPoint3x4(vector7), Color.red);
				Debug.DrawLine(this.offsettedNearPlaneTM.MultiplyPoint3x4(vector9), this.nearPlaneTransformMatrix.MultiplyPoint3x4(vector10), Color.red);
			}
		}

		// Token: 0x0400061E RID: 1566
		[Tooltip("Layers the camera should focus on")]
		[SerializeField]
		private LayerMask _layers = -1;

		// Token: 0x0400061F RID: 1567
		[SerializeField]
		[Tooltip("The maximum distance the auto focus algorithm is allowed to raycast to collect sample points. Note that if this is smaller than the convergence range max value, then it will take precedence over the convergence range max value when determining the max convergence distance.")]
		private float _maxRaycastDistance = 1000f;

		// Token: 0x04000620 RID: 1568
		[Range(1f, 1000f)]
		[Tooltip("Raycast samples to take")]
		[SerializeField]
		private int samples = 500;

		// Token: 0x04000621 RID: 1569
		private int previous_samples = -1;

		// Token: 0x04000622 RID: 1570
		private Vector2[] cameraNearPlaneRaysOrigins = new Vector2[500];

		// Token: 0x04000623 RID: 1571
		[Tooltip("Show debug rays in the scene editor")]
		[SerializeField]
		private bool showDebugRaycasts;

		// Token: 0x04000624 RID: 1572
		private float furthestDistance;

		// Token: 0x04000625 RID: 1573
		private float closestDistance;

		// Token: 0x04000626 RID: 1574
		private float avgDistance;

		// Token: 0x04000627 RID: 1575
		private float hits;

		// Token: 0x04000628 RID: 1576
		private const float distanceWeightPower = 1.1f;

		// Token: 0x04000629 RID: 1577
		private const float minDistancePowered = 0.1f;

		// Token: 0x0400062A RID: 1578
		private readonly Vector3[] frustrumCorners = new Vector3[4];

		// Token: 0x0400062B RID: 1579
		private Matrix4x4 nearPlaneTransformMatrix;

		// Token: 0x0400062C RID: 1580
		private Matrix4x4 offsettedNearPlaneTM;

		// Token: 0x0400062D RID: 1581
		private readonly float crossScale = 0.01f;
	}
}
