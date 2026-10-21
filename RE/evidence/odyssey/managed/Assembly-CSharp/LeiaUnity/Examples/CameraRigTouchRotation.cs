using System;
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x02000140 RID: 320
	public class CameraRigTouchRotation : MonoBehaviour
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x000261D0 File Offset: 0x000243D0
		private void Update()
		{
			if (Input.touchCount > 1)
			{
				if (Input.GetTouch(1).phase == null)
				{
					Vector3 vector = Input.GetTouch(0).position;
					Vector3 vector2 = Input.GetTouch(1).position;
					this.averageStartPoint = (vector + vector2) / 2f;
					this.startRotation = base.transform.eulerAngles;
					return;
				}
				Vector3 vector3 = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2f - this.averageStartPoint;
				Vector3 vector4 = this.startRotation - new Vector3(vector3.y / 20f, -vector3.x / 20f, 0f);
				base.transform.eulerAngles = new Vector3(Mathf.Clamp(vector4.x, 1f, 89f), vector4.y, vector4.z);
			}
		}

		// Token: 0x040006D7 RID: 1751
		private Vector3 averageStartPoint = Vector3.zero;

		// Token: 0x040006D8 RID: 1752
		private Vector3 startRotation = Vector3.zero;
	}
}
