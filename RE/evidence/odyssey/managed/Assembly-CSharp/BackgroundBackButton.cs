using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x0200007D RID: 125
public class BackgroundBackButton : MonoBehaviour
{
	// Token: 0x060002D7 RID: 727 RVA: 0x0000E67B File Offset: 0x0000C87B
	private void Start()
	{
		base.Invoke(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uf(), 1f);
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0000E68D File Offset: 0x0000C88D
	private void Disactive()
	{
		base.gameObject.SetActive(false);
	}
}
