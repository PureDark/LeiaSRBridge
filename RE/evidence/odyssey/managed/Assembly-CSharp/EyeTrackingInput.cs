using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000008 RID: 8
public class EyeTrackingInput : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x00002898 File Offset: 0x00000A98
	private void Start()
	{
		bd.ih(EyeTrackingInput.bse, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		this.trackingArea = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000028B8 File Offset: 0x00000AB8
	private void Update()
	{
		Vector3 vector = LeiaPluginHelper.hz();
		this.screenPosition = LeiaPluginHelper.ib(vector);
		this.inputPosition = vector;
	}

	// Token: 0x0400000E RID: 14
	public Vector2 screenPosition;

	// Token: 0x0400000F RID: 15
	public Vector2 inputPosition;

	// Token: 0x04000010 RID: 16
	private static string bse = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bq();

	// Token: 0x04000011 RID: 17
	[HideInInspector]
	public RectTransform trackingArea;
}
