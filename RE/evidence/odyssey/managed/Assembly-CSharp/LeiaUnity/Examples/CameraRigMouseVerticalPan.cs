using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x0200013C RID: 316
	public class CameraRigMouseVerticalPan : MonoBehaviour
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x00025B4B File Offset: 0x00023D4B
		private void Start()
		{
			this.cameraPivot = base.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjk());
			this.camera3d = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjl()).transform;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00025B78 File Offset: 0x00023D78
		private void LateUpdate()
		{
			if (Input.GetMouseButtonDown(1))
			{
				this.startMousePosition = Input.mousePosition;
				this.startRotation = this.cameraPivot.rotation.eulerAngles.x;
			}
			if (Input.GetMouseButton(1))
			{
				float num = Input.mousePosition.y - this.startMousePosition.y;
				float num2 = Mathf.Clamp(this.startRotation - num * this.sensitivity, this.minAngle, this.maxAngle);
				this.cameraPivot.localRotation = Quaternion.Euler(num2, 0f, 0f);
			}
			this.camera3d.localRotation = Quaternion.identity;
		}

		// Token: 0x040006BF RID: 1727
		[SerializeField]
		[Range(0.01f, 1f)]
		private float sensitivity = 0.1f;

		// Token: 0x040006C0 RID: 1728
		[SerializeField]
		[Range(2f, 90f)]
		private float minAngle = 2f;

		// Token: 0x040006C1 RID: 1729
		[SerializeField]
		[Range(2f, 90f)]
		private float maxAngle = 90f;

		// Token: 0x040006C2 RID: 1730
		private Transform cameraPivot;

		// Token: 0x040006C3 RID: 1731
		private Transform camera3d;

		// Token: 0x040006C4 RID: 1732
		private Vector3 startMousePosition;

		// Token: 0x040006C5 RID: 1733
		private float startRotation;
	}
}
