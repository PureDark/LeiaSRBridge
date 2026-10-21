using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class GlassShaderParamsSetter : MonoBehaviour
{
	// Token: 0x17000063 RID: 99
	// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001A003 File Offset: 0x00018203
	// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001A00B File Offset: 0x0001820B
	public float dbo
	{
		get
		{
			return this.cpq;
		}
		set
		{
			this.cpq = value;
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0001A014 File Offset: 0x00018214
	public void beh(float a)
	{
		this.cpo.SetBlendShapeWeight(0, 100f - a);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0001A029 File Offset: 0x00018229
	public void bei(float a)
	{
		this.cpo.SetBlendShapeWeight(1, 100f - a);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0001A040 File Offset: 0x00018240
	private void Start()
	{
		this.cpo = base.GetComponent<SkinnedMeshRenderer>();
		this.cpp = this.cpo.material;
		if (Application.isEditor)
		{
			this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zp(), 1f);
			this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zq(), 1f);
			this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zr(), 0f);
			this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zs(), 0f);
			return;
		}
		this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zp(), 0.5f);
		this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zq(), 0.5f);
		this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zr(), 0f);
		this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zs(), 0f);
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0001A11A File Offset: 0x0001831A
	private void Update()
	{
		this.cpp.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zt(), this.dbo);
	}

	// Token: 0x04000519 RID: 1305
	private SkinnedMeshRenderer cpo;

	// Token: 0x0400051A RID: 1306
	private Material cpp;

	// Token: 0x0400051B RID: 1307
	private float cpq = 5f;
}
