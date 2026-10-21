using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000069 RID: 105
public class FocussingFrameRotation : MonoBehaviour
{
	// Token: 0x0600023A RID: 570 RVA: 0x0000B58F File Offset: 0x0000978F
	private void Start()
	{
		this.cdi = Object.FindAnyObjectByType<PanelRotation>();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000B59C File Offset: 0x0000979C
	private void Update()
	{
		if (this.cdi != null)
		{
			base.transform.eulerAngles = new Vector3(0f, -this.cdi.panel.transform.eulerAngles.y, 0f);
			return;
		}
		bd.id(FocussingFrameRotation.cdj, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sv(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sw());
	}

	// Token: 0x04000247 RID: 583
	private PanelRotation cdi;

	// Token: 0x04000248 RID: 584
	private static string cdj = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sx();
}
