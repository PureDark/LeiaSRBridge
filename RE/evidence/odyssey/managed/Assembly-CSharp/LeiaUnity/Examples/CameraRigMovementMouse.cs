using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeiaUnity.Examples
{
	// Token: 0x0200013F RID: 319
	public class CameraRigMovementMouse : MonoBehaviour
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x00026062 File Offset: 0x00024262
		private void Start()
		{
			this.childCamera = base.GetComponentInChildren<Camera>().transform;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00026078 File Offset: 0x00024278
		private void LateUpdate()
		{
			if (this.startedOnUI)
			{
				if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(2))
				{
					this.startedOnUI = false;
				}
				return;
			}
			if (Input.touchCount > 1)
			{
				this.multiTouching = true;
				return;
			}
			if (this.multiTouching)
			{
				if (!Input.GetMouseButton(0))
				{
					this.multiTouching = false;
				}
				return;
			}
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
			{
				if (EventSystem.current.IsPointerOverGameObject())
				{
					this.startedOnUI = true;
					return;
				}
				this.startMousePosition = Input.mousePosition;
				this.startPosition = base.transform.position;
			}
			float z = this.childCamera.localPosition.z;
			if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
			{
				Quaternion quaternion = Quaternion.AngleAxis(base.transform.rotation.eulerAngles.y, Vector3.up);
				Vector3 vector;
				vector..ctor(Input.mousePosition.x - this.startMousePosition.x, 0f, Input.mousePosition.y - this.startMousePosition.y);
				base.transform.position = this.startPosition + quaternion * (vector * z * this.sensitivity);
			}
		}

		// Token: 0x040006D1 RID: 1745
		[SerializeField]
		private float sensitivity = 0.01f;

		// Token: 0x040006D2 RID: 1746
		private Vector3 startMousePosition;

		// Token: 0x040006D3 RID: 1747
		private Vector3 startPosition;

		// Token: 0x040006D4 RID: 1748
		private Transform childCamera;

		// Token: 0x040006D5 RID: 1749
		private bool multiTouching;

		// Token: 0x040006D6 RID: 1750
		private bool startedOnUI;
	}
}
