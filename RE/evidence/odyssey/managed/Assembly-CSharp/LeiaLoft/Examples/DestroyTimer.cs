using System;
using UnityEngine;

namespace LeiaLoft.Examples
{
	// Token: 0x020000FA RID: 250
	public class DestroyTimer : MonoBehaviour
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0001D339 File Offset: 0x0001B539
		private void Start()
		{
			Object.Destroy(base.gameObject, this.time);
		}

		// Token: 0x04000596 RID: 1430
		[SerializeField]
		private float time;
	}
}
