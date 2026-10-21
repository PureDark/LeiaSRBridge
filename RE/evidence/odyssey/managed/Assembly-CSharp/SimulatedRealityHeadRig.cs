using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using SRUnity;
using UnityEngine;

// Token: 0x020000DA RID: 218
[ExecuteInEditMode]
[AddComponentMenu("Simulated Reality/Simulated Reality Head")]
public class SimulatedRealityHeadRig : MonoBehaviour
{
	// Token: 0x060005B3 RID: 1459 RVA: 0x0001AE19 File Offset: 0x00019019
	public void OnEnable()
	{
		this.bgc();
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001AE19 File Offset: 0x00019019
	public void Awake()
	{
		this.bgc();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001AE19 File Offset: 0x00019019
	public void Start()
	{
		this.bgc();
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001AE24 File Offset: 0x00019024
	public void Update()
	{
		Vector3[] eyes = SimulatedRealityModule<SRHead>.Instance.GetEyes(@do.bgf(null));
		Vector3[] ears = SimulatedRealityModule<SRHead>.Instance.GetEars(@do.bgf(null));
		Vector3 headPosition = SimulatedRealityModule<SRHead>.Instance.GetHeadPosition(@do.bgf(null));
		Quaternion headOrientation = SimulatedRealityModule<SRHead>.Instance.GetHeadOrientation();
		for (int i = 0; i < 2; i++)
		{
			if (this.cqn[i] != null)
			{
				this.cqn[i].transform.localPosition = eyes[i];
			}
			if (this.cqp[i] != null)
			{
				this.cqp[i].transform.localPosition = ears[i];
			}
		}
		if (this.cqr != null)
		{
			this.cqr.transform.localPosition = headPosition;
			this.cqr.transform.localRotation = headOrientation;
		}
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001AF08 File Offset: 0x00019108
	public void bft()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.cqn[i] == null)
			{
				this.cqn[i] = new GameObject();
			}
			this.cqn[i].name = this.cqo[i];
			this.cqn[i].transform.parent = base.gameObject.transform;
			this.cqn[i].transform.localPosition = SimulatedRealityModule<SREyes>.Instance.GetDefaultEyePosition(@do.bgf(null));
		}
		this.bgc();
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001AF98 File Offset: 0x00019198
	public void bfu()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.cqn[i] != null)
			{
				Object.DestroyImmediate(this.cqn[i]);
				this.cqn[i] = null;
			}
		}
		this.bgc();
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001AFDD File Offset: 0x000191DD
	public bool bfv()
	{
		return this.cqn[0] != null || this.cqn[1] != null;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001B000 File Offset: 0x00019200
	public void bfw()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.cqp[i] == null)
			{
				this.cqp[i] = new GameObject();
			}
			this.cqp[i].name = this.cqq[i];
			this.cqp[i].transform.parent = base.gameObject.transform;
			this.cqp[i].transform.localPosition = SimulatedRealityModule<SREyes>.Instance.GetDefaultEyePosition(@do.bgf(null));
		}
		this.bgc();
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0001B090 File Offset: 0x00019290
	public void bfx()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.cqp[i] != null)
			{
				Object.DestroyImmediate(this.cqp[i]);
				this.cqp[i] = null;
			}
		}
		this.bgc();
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001B0D5 File Offset: 0x000192D5
	public bool bfy()
	{
		return this.cqp[0] != null || this.cqp[1] != null;
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0001B0F8 File Offset: 0x000192F8
	public void bfz()
	{
		if (this.cqr == null)
		{
			this.cqr = new GameObject();
		}
		this.cqr.name = this.cqs;
		this.cqr.transform.parent = base.gameObject.transform;
		this.cqr.transform.localPosition = SimulatedRealityModule<SREyes>.Instance.GetDefaultEyePosition(@do.bgf(null));
		this.bgc();
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0001B170 File Offset: 0x00019370
	public void bga()
	{
		if (this.cqr != null)
		{
			Object.DestroyImmediate(this.cqr);
			this.cqr = null;
		}
		this.bgc();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0001B198 File Offset: 0x00019398
	public bool bgb()
	{
		return this.cqr != null;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0001B1A8 File Offset: 0x000193A8
	private void bgc()
	{
		for (int i = 0; i < 2; i++)
		{
			this.cqn[i] = SRUtility.FindChildObject(base.gameObject, this.cqo[i]);
		}
		for (int j = 0; j < 2; j++)
		{
			this.cqp[j] = SRUtility.FindChildObject(base.gameObject, this.cqq[j]);
		}
		this.cqr = SRUtility.FindChildObject(base.gameObject, this.cqs);
	}

	// Token: 0x04000533 RID: 1331
	private readonly GameObject[] cqn = new GameObject[2];

	// Token: 0x04000534 RID: 1332
	private readonly string[] cqo = new string[]
	{
		<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bad(),
		<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bae()
	};

	// Token: 0x04000535 RID: 1333
	private readonly GameObject[] cqp = new GameObject[2];

	// Token: 0x04000536 RID: 1334
	private readonly string[] cqq = new string[]
	{
		<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baf(),
		<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bag()
	};

	// Token: 0x04000537 RID: 1335
	private GameObject cqr;

	// Token: 0x04000538 RID: 1336
	private readonly string cqs = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bah();
}
