using System;
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x0200013A RID: 314
	[DefaultExecutionOrder(1000)]
	public class CameraRigBoundary : MonoBehaviour
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x00025994 File Offset: 0x00023B94
		private void LateUpdate()
		{
			base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, -this.dimensions.x / 2f, this.dimensions.x / 2f), Mathf.Clamp(base.transform.position.y, -this.dimensions.y / 2f, this.dimensions.y / 2f), Mathf.Clamp(base.transform.position.z, -this.dimensions.z / 2f, this.dimensions.z / 2f));
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00025A59 File Offset: 0x00023C59
		private void OnDrawGizmos()
		{
			if (this.showBounds)
			{
				Gizmos.DrawWireCube(Vector3.zero, this.dimensions);
			}
		}

		// Token: 0x040006BA RID: 1722
		[SerializeField]
		private Vector3 dimensions;

		// Token: 0x040006BB RID: 1723
		[SerializeField]
		private bool showBounds;
	}
}
