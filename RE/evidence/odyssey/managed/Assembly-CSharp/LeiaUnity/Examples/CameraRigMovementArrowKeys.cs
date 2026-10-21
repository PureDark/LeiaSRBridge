using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x0200013E RID: 318
	public class CameraRigMovementArrowKeys : MonoBehaviour
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x00025F34 File Offset: 0x00024134
		private void Start()
		{
			this.rb = base.transform.GetComponent<Rigidbody>();
			if (this.rb == null)
			{
				this.rb = base.gameObject.AddComponent<Rigidbody>();
				this.rb.useGravity = false;
				this.rb.drag = this.drag;
			}
			this.childCamera = base.GetComponentInChildren<Camera>().transform;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00025F9F File Offset: 0x0002419F
		private void LateUpdate()
		{
			this.MoveCamera(-Input.GetAxis(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjq()), -Input.GetAxis(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjr()));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00025FC0 File Offset: 0x000241C0
		public void MoveCamera(float horizontal, float vertical)
		{
			Vector3 vector;
			vector..ctor(horizontal * this.speed * (this.childCamera.localPosition.z + 10f), 0f, vertical * this.speed * (this.childCamera.localPosition.z + 10f));
			Vector3 vector2 = Quaternion.AngleAxis(base.transform.eulerAngles.y, Vector3.up) * vector;
			this.rb.AddForce(vector2);
		}

		// Token: 0x040006CD RID: 1741
		[SerializeField]
		private float speed = 5f;

		// Token: 0x040006CE RID: 1742
		[SerializeField]
		private float drag = 5f;

		// Token: 0x040006CF RID: 1743
		private Rigidbody rb;

		// Token: 0x040006D0 RID: 1744
		private Transform childCamera;
	}
}
