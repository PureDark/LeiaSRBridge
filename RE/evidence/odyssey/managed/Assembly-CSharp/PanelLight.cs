using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class PanelLight : MonoBehaviour
{
	// Token: 0x06000411 RID: 1041 RVA: 0x00013760 File Offset: 0x00011960
	private void Start()
	{
		bd.ih(PanelLight.ckx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		if (this.rayReceiver == null)
		{
			bd.ie(PanelLight.ckx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tw(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
			return;
		}
		this.cky = base.GetComponent<SkinnedMeshRenderer>().material;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x000137B0 File Offset: 0x000119B0
	private void Update()
	{
		if (this.rayReceiver.isFirstHit)
		{
			this.xs(this.xRangeOnFirstHit);
			return;
		}
		if (this.rayReceiver.isHit)
		{
			this.xs(this.xRangeOnSubHit);
			this.xr();
			return;
		}
		this.xs(this.xRangeOnNotHit);
		ShortcutExtensions.DOFloat(this.cky, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vr(), this.duration);
		ShortcutExtensions.DOFloat(this.cky, 0.1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vs(), this.duration);
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001383C File Offset: 0x00011A3C
	private void xr()
	{
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width;
		float num2 = num - 900f;
		if (mousePosition.x < 450f)
		{
			this.ckz = -0.5f;
		}
		else if (mousePosition.x > num - 450f)
		{
			this.ckz = 0.5f;
		}
		else
		{
			float num3 = (mousePosition.x - 450f) / num2;
			this.ckz = Mathf.Lerp(-0.5f, 0.5f, num3);
		}
		this.cky.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vr(), this.ckz);
		if (mousePosition.y >= 400f)
		{
			if (this.cky.GetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vs()) < -0.09f)
			{
				ShortcutExtensions.DOFloat(this.cky, 0.1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vs(), this.duration);
				return;
			}
		}
		else if (this.cky.GetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vs()) > 0.09f)
		{
			ShortcutExtensions.DOFloat(this.cky, -0.1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vs(), this.duration);
		}
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00013943 File Offset: 0x00011B43
	private void xs(float a)
	{
		ShortcutExtensions.DOFloat(this.cky, a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vt(), this.duration);
	}

	// Token: 0x040003F6 RID: 1014
	[SerializeField]
	private float duration;

	// Token: 0x040003F7 RID: 1015
	[SerializeField]
	private RayReceiver rayReceiver;

	// Token: 0x040003F8 RID: 1016
	[SerializeField]
	private float xRangeOnFirstHit;

	// Token: 0x040003F9 RID: 1017
	[SerializeField]
	private float xRangeOnSubHit;

	// Token: 0x040003FA RID: 1018
	[SerializeField]
	private float xRangeOnNotHit;

	// Token: 0x040003FB RID: 1019
	private static string ckx = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vu();

	// Token: 0x040003FC RID: 1020
	private Material cky;

	// Token: 0x040003FD RID: 1021
	private float ckz;
}
