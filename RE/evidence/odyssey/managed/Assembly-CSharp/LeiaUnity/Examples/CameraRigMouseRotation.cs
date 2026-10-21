using System;
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x0200013B RID: 315
	public class CameraRigMouseRotation : MonoBehaviour
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x00025A74 File Offset: 0x00023C74
		private void LateUpdate()
		{
			if (Input.GetMouseButtonDown(1))
			{
				this.startMousePosition = Input.mousePosition;
				this.startRotation = base.transform.rotation;
			}
			if (Input.GetMouseButton(1))
			{
				float num = Input.mousePosition.x - this.startMousePosition.x;
				float num2 = Input.mousePosition.y - this.startMousePosition.y;
				base.transform.rotation = Quaternion.Euler(this.startRotation.eulerAngles.x - num2 * this.sensitivity, this.startRotation.eulerAngles.y + num * this.sensitivity, 0f);
			}
		}

		// Token: 0x040006BC RID: 1724
		[SerializeField]
		[Range(0.01f, 1f)]
		private float sensitivity = 0.1f;

		// Token: 0x040006BD RID: 1725
		private Vector3 startMousePosition = Vector3.zero;

		// Token: 0x040006BE RID: 1726
		private Quaternion startRotation = Quaternion.identity;
	}
}
