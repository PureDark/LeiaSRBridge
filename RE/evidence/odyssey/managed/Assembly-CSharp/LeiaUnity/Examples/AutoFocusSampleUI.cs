using System;
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x02000138 RID: 312
	public class AutoFocusSampleUI : MonoBehaviour
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x00025958 File Offset: 0x00023B58
		public void OnValueChange(int chosenMethod)
		{
			this.depthFocus.enabled = (chosenMethod == 0);
			this.raycastFocus.enabled = (chosenMethod == 1);
			this.targetFocus.enabled = (chosenMethod == 2);
		}

		// Token: 0x040006B3 RID: 1715
		[SerializeField]
		private LeiaDepthFocus depthFocus;

		// Token: 0x040006B4 RID: 1716
		[SerializeField]
		private LeiaRaycastFocus raycastFocus;

		// Token: 0x040006B5 RID: 1717
		[SerializeField]
		private LeiaTargetFocus targetFocus;

		// Token: 0x02000139 RID: 313
		private enum Method
		{
			// Token: 0x040006B7 RID: 1719
			DepthCamera,
			// Token: 0x040006B8 RID: 1720
			Raycast,
			// Token: 0x040006B9 RID: 1721
			Target
		}
	}
}
