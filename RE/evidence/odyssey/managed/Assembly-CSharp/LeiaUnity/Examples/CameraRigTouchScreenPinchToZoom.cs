using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x02000141 RID: 321
	public class CameraRigTouchScreenPinchToZoom : MonoBehaviour
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00026307 File Offset: 0x00024507
		private void Start()
		{
			this.childCamera = base.GetComponentInChildren<Camera>();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00026318 File Offset: 0x00024518
		private void LateUpdate()
		{
			if (Input.touchCount > 1)
			{
				float num = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
				if (Input.GetTouch(1).phase == null)
				{
					this.startTouchDistance = num;
					this.startCameraDistance = -this.childCamera.transform.localPosition.z;
					this.startOrthographicSize = this.childCamera.orthographicSize;
					return;
				}
				float num2;
				if (this.childCamera.orthographic)
				{
					num2 = this.startOrthographicSize - (num - this.startTouchDistance) * this.orthographicSensitivity;
					num2 = Mathf.Clamp(num2, this.orthoZoomRange.min, this.orthoZoomRange.max);
					this.childCamera.orthographicSize = num2;
					return;
				}
				num2 = this.startCameraDistance - (num - this.startTouchDistance) * this.perspectiveSensitivity;
				num2 = Mathf.Clamp(num2, this.perspZoomRange.min, this.perspZoomRange.max);
				this.childCamera.transform.localPosition = new Vector3(0f, 0f, -num2);
			}
		}

		// Token: 0x040006D9 RID: 1753
		[SerializeField]
		private MinMaxPair perspZoomRange = new MinMaxPair(10f, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjm(), 50f, float.MaxValue, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjn());

		// Token: 0x040006DA RID: 1754
		[SerializeField]
		private MinMaxPair orthoZoomRange = new MinMaxPair(1f, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjo(), 10f, float.MaxValue, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjp());

		// Token: 0x040006DB RID: 1755
		[SerializeField]
		[Tooltip("Zoom sensitivity when using a perspective camera")]
		private float perspectiveSensitivity = 0.01f;

		// Token: 0x040006DC RID: 1756
		[SerializeField]
		[Tooltip("Zoom sensitivity when using an orthographic camera")]
		private float orthographicSensitivity = 0.004f;

		// Token: 0x040006DD RID: 1757
		private float startTouchDistance;

		// Token: 0x040006DE RID: 1758
		private float startCameraDistance;

		// Token: 0x040006DF RID: 1759
		private float startOrthographicSize;

		// Token: 0x040006E0 RID: 1760
		private Camera childCamera;
	}
}
