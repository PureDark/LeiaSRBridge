using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C2 RID: 194
public class PopupScrollReset : MonoBehaviour
{
	// Token: 0x0600050A RID: 1290 RVA: 0x00017A7F File Offset: 0x00015C7F
	private void OnEnable()
	{
		if (this.resetOnEnable && this.targetScrollRect != null)
		{
			this.targetScrollRect.verticalNormalizedPosition = this.scrollPosition;
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00017AA8 File Offset: 0x00015CA8
	public void ResetScroll()
	{
		if (this.targetScrollRect != null)
		{
			this.targetScrollRect.verticalNormalizedPosition = this.scrollPosition;
			return;
		}
		bd.id(PopupScrollReset.coj, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xp(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xq());
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00017ADE File Offset: 0x00015CDE
	public void bcc()
	{
		if (this.targetScrollRect != null)
		{
			this.targetScrollRect.verticalNormalizedPosition = 1f;
		}
	}

	// Token: 0x040004CA RID: 1226
	[Header("Scroll Settings")]
	[SerializeField]
	private ScrollRect targetScrollRect;

	// Token: 0x040004CB RID: 1227
	[SerializeField]
	private bool resetOnEnable = true;

	// Token: 0x040004CC RID: 1228
	[SerializeField]
	private float scrollPosition = 1f;

	// Token: 0x040004CD RID: 1229
	private static string coj = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xr();
}
