using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200011B RID: 283
	[RequireComponent(typeof(LeiaDisplay))]
	[DefaultExecutionOrder(-1000)]
	[HelpURL("https://support.leiainc.com/sdk/unity-plugin/leiasr-tm-unity-plugin-guide/extensions/auto-focus/leiatargetfocus")]
	public class LeiaTargetFocus : LeiaFocus
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00021A9F File Offset: 0x0001FC9F
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00021AA7 File Offset: 0x0001FCA7
		public int Samples
		{
			get
			{
				return this.samples;
			}
			set
			{
				this.samples = Mathf.Clamp(value, 1, 1000);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00021ABB File Offset: 0x0001FCBB
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00021AC3 File Offset: 0x0001FCC3
		public GameObject target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
				this.InitTarget();
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00021AD2 File Offset: 0x0001FCD2
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InitTarget();
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		public void InitTarget()
		{
			if (this.target == null)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgz(), Array.Empty<object>());
				return;
			}
			this.previousTarget = this.target;
			this.previousChildCount = this.target.transform.childCount;
			MeshFilter[] componentsInChildren = this.target.GetComponentsInChildren<MeshFilter>();
			if (componentsInChildren.Length != 0)
			{
				int num = componentsInChildren.Length;
				this.meshInfos = new LeiaTargetFocus.MeshInfo[num];
				for (int i = 0; i < num; i++)
				{
					this.meshInfos[i] = new LeiaTargetFocus.MeshInfo(componentsInChildren[i]);
				}
				for (int j = 0; j < this.meshInfos.Length; j++)
				{
					this.totalVertices += this.meshInfos[j].vertices.Length;
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00021B9C File Offset: 0x0001FD9C
		protected override void LateUpdate()
		{
			if (this.leiaDisplay == null || this.leiaDisplay.mode == LeiaDisplay.ControlMode.DisplayDriven)
			{
				return;
			}
			if (this.target == null && this.target != this.previousTarget)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bha(), Array.Empty<object>());
				base.enabled = false;
				return;
			}
			if (this.target != this.previousTarget || this.target.transform.childCount != this.previousChildCount)
			{
				this.InitTarget();
			}
			if (this.meshInfos != null && this.meshInfos.Length != 0)
			{
				int num = Mathf.Max(1, this.totalVertices / this.samples);
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				float num6 = float.MaxValue;
				for (int i = 0; i < this.meshInfos.Length; i++)
				{
					int num7 = this.meshInfos[i].vertices.Length;
					for (int j = 0; j < num7; j += num)
					{
						Vector3 vector = this.meshInfos[i].transform.TransformPoint(this.meshInfos[i].vertices[j]);
						Vector3 vector2 = this.leiaDisplay.DriverCamera.WorldToViewportPoint(vector);
						if (vector2.x > 0f && vector2.x < 1f && vector2.y > 0f && vector2.y < 1f)
						{
							num5 += 1f;
							float num8 = Vector3.Distance(vector, this.leiaDisplay.DriverCamera.transform.position);
							num4 += num8;
							num3 += 1f;
							num2 += num8;
							if (num8 < num6)
							{
								num6 = num8;
							}
						}
					}
				}
				if (num3 > 0f)
				{
					float targetFocalDistance = Mathf.Clamp(num2 / Mathf.Max(num3, 1f), 1f, 1000000f);
					base.SetTargetFocalDistance(targetFocalDistance);
					float recommendedDepthFactorWithNearPlane = LeiaDisplayUtils.GetRecommendedDepthFactorWithNearPlane(this.leiaDisplay, num6);
					base.SetTargetDepthFactor(recommendedDepthFactorWithNearPlane);
				}
			}
			else
			{
				base.SetTargetFocalDistance(Vector3.Distance(this.leiaDisplay.DriverCamera.transform.position, this.target.transform.position));
			}
			base.LateUpdate();
		}

		// Token: 0x0400062E RID: 1582
		[Tooltip("How many mesh vertices to take as sample points for determining baseline and convergence. Larger sample count will be more costly performancewise.")]
		[SerializeField]
		[Range(1f, 1000f)]
		private int samples = 200;

		// Token: 0x0400062F RID: 1583
		[SerializeField]
		private GameObject _target;

		// Token: 0x04000630 RID: 1584
		private LeiaTargetFocus.MeshInfo[] meshInfos;

		// Token: 0x04000631 RID: 1585
		private GameObject previousTarget;

		// Token: 0x04000632 RID: 1586
		private int totalVertices;

		// Token: 0x04000633 RID: 1587
		private int previousChildCount;

		// Token: 0x0200011C RID: 284
		private class MeshInfo
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x00021E0A File Offset: 0x0002000A
			public MeshInfo(MeshFilter meshFilter)
			{
				this.transform = meshFilter.transform;
				if (meshFilter.sharedMesh != null)
				{
					this.vertices = meshFilter.sharedMesh.vertices;
					return;
				}
				this.vertices = new Vector3[0];
			}

			// Token: 0x04000634 RID: 1588
			public Transform transform;

			// Token: 0x04000635 RID: 1589
			public Vector3[] vertices;
		}
	}
}
