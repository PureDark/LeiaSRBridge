using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeiaUnity.Examples
{
	// Token: 0x0200013D RID: 317
	[DefaultExecutionOrder(1)]
	public class CameraRigMouseWheelZoom : MonoBehaviour
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x00025C4C File Offset: 0x00023E4C
		private void Start()
		{
			this.childCamera = base.GetComponentInChildren<Camera>();
			if (this.childCamera.orthographic)
			{
				this.zoom = this.childCamera.orthographicSize;
			}
			else
			{
				this.zoom = -base.transform.GetChild(0).localPosition.z;
			}
			this.zoomTarget = this.zoom;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00025CB0 File Offset: 0x00023EB0
		private void LateUpdate()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			if (Mathf.Abs(Input.mouseScrollDelta.y) > 0f)
			{
				this.zooming = true;
			}
			if (this.childCamera.orthographic)
			{
				this.zoom = this.childCamera.orthographicSize;
				this.zoomTarget -= Input.mouseScrollDelta.y * (this.zoom / 50f);
				this.zoomTarget = Mathf.Clamp(this.zoomTarget, this.orthoZoomRange.min, this.orthoZoomRange.max);
				if (this.zooming)
				{
					this.zoom += (this.zoomTarget - this.zoom) * Mathf.Min(Time.deltaTime * this.zoomSpeed, 1f);
					if (Mathf.Abs(this.zoomTarget - this.zoom) < 0.001f)
					{
						this.zooming = false;
					}
				}
				this.childCamera.orthographicSize = this.zoom;
				return;
			}
			this.zoom = -base.transform.GetChild(0).localPosition.z;
			this.zoomTarget -= Input.mouseScrollDelta.y * this.zoomSpeed * (this.zoom / 50f);
			this.zoomTarget = Mathf.Clamp(this.zoomTarget, this.perspZoomRange.min, this.perspZoomRange.max);
			if (this.zooming)
			{
				this.zoom += (this.zoomTarget - this.zoom) * Mathf.Min(Time.deltaTime * this.zoomSpeed, 1f);
				if (Mathf.Abs(this.zoomTarget - this.zoom) < 0.001f)
				{
					this.zooming = false;
				}
			}
			this.childCamera.transform.localPosition = new Vector3(0f, 0f, -this.zoom);
		}

		// Token: 0x040006C6 RID: 1734
		[SerializeField]
		private MinMaxPair perspZoomRange = new MinMaxPair(10f, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjm(), 50f, float.MaxValue, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjn());

		// Token: 0x040006C7 RID: 1735
		[SerializeField]
		private MinMaxPair orthoZoomRange = new MinMaxPair(1f, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjo(), 10f, float.MaxValue, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjp());

		// Token: 0x040006C8 RID: 1736
		[SerializeField]
		private float zoom = 20f;

		// Token: 0x040006C9 RID: 1737
		[SerializeField]
		private float zoomSpeed = 10f;

		// Token: 0x040006CA RID: 1738
		private float zoomTarget = 20f;

		// Token: 0x040006CB RID: 1739
		private Camera childCamera;

		// Token: 0x040006CC RID: 1740
		private bool zooming;
	}
}
