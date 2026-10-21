using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using SRUnity;
using UnityEngine;

// Token: 0x020000D7 RID: 215
[ExecuteInEditMode]
[AddComponentMenu("Simulated Reality/Simulated Reality Camera")]
public class SimulatedRealityCamera : MonoBehaviour
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001A145 File Offset: 0x00018345
	// (set) Token: 0x06000586 RID: 1414 RVA: 0x0001A14D File Offset: 0x0001834D
	public bool dbp
	{
		get
		{
			return this.enableLookaround;
		}
		set
		{
			this.enableLookaround = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001A156 File Offset: 0x00018356
	private LeiaDisplay dbq
	{
		get
		{
			if (this.cqe == null)
			{
				this.cqe = Object.FindObjectOfType<LeiaDisplay>();
			}
			if (this.cqe == null)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zu(), Array.Empty<object>());
			}
			return this.cqe;
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0001A198 File Offset: 0x00018398
	public void OnEnable()
	{
		SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zv());
		this.Init();
		SRCompositor.OnCompositorChanged += this.bew;
		SRCore.OnContextChanged += this.bev;
		this.cqd[0].Enabled = true;
		this.cqd[1].Enabled = true;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001A1F4 File Offset: 0x000183F4
	public void OnDisable()
	{
		SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zw());
		SRCompositor.OnCompositorChanged -= this.bew;
		SRCore.OnContextChanged -= this.bev;
		for (int i = 0; i < 2; i++)
		{
			if (this.cqc[i] != null)
			{
				Object.DestroyImmediate(this.cqc[i].gameObject);
				this.cqc[i] = null;
			}
		}
		this.cqd[0].Enabled = false;
		this.cqd[1].Enabled = false;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001A280 File Offset: 0x00018480
	public void Awake()
	{
		SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zx());
		this.Init();
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0001A292 File Offset: 0x00018492
	public void Start()
	{
		SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zy());
		this.Init();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0001A2A4 File Offset: 0x000184A4
	private void Init()
	{
		this.ben();
		this.ber();
		if (this.beo() && this.bep())
		{
			this.beu();
		}
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0001A2C8 File Offset: 0x000184C8
	public void Update()
	{
		this.bes();
		this.bet();
		this.bex();
		if (this.beo() && this.bep())
		{
			this.beu();
		}
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0001A2F4 File Offset: 0x000184F4
	public void OnValidate()
	{
		this.ber();
		if (this.cqc[0] != null)
		{
			this.bem(this.cqc[0].gameObject, false);
		}
		if (this.cqc[1] != null)
		{
			this.bem(this.cqc[1].gameObject, false);
		}
		this.bex();
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0001A355 File Offset: 0x00018555
	public void bem(GameObject a, bool b)
	{
		if (SRCore.IsSimulatedRealityAvailable())
		{
			if (!this.cqb)
			{
				SRUtility.SetSrGameObjectVisibility(a);
				return;
			}
			a.hideFlags = 0;
			return;
		}
		else
		{
			if (b)
			{
				a.hideFlags = 60;
				return;
			}
			a.hideFlags = 61;
			return;
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0001A38C File Offset: 0x0001858C
	private void ben()
	{
		string[] array;
		if (SRCore.IsSimulatedRealityAvailable())
		{
			array = new string[]
			{
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zz(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baa()
			};
		}
		else
		{
			array = new string[]
			{
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bab(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baa()
			};
		}
		for (int i = 0; i < 2; i++)
		{
			GameObject gameObject = SRUtility.FindChildObject(base.gameObject, array[i]);
			if (gameObject == null)
			{
				gameObject = new GameObject();
				gameObject.transform.parent = base.gameObject.transform;
			}
			gameObject.name = array[i];
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			if (i == 0)
			{
				this.bem(gameObject, true);
			}
			else
			{
				this.bem(gameObject, false);
			}
			this.cqc[i] = gameObject.GetComponent<Camera>();
			if (this.cqc[i] == null)
			{
				this.cqc[i] = gameObject.AddComponent<Camera>();
			}
			this.cqd[i] = SimulatedRealityModule<SRRender>.Instance.GetCompositor().GetFrameBuffer(this.cqc[i].GetInstanceID().ToString());
			this.cqd[i].viewIndex = i;
			this.cqd[i].Enabled = true;
		}
		Camera camera = base.transform.gameObject.GetComponent<Camera>();
		if (camera == null)
		{
			camera = base.transform.gameObject.AddComponent<Camera>();
		}
		camera.enabled = false;
		camera.hideFlags = 55;
		this.bex();
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0001A509 File Offset: 0x00018709
	public bool beo()
	{
		return this.cpv.width > 0f && this.cpv.height > 0f;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001A532 File Offset: 0x00018732
	public bool bep()
	{
		return !(this.dbq == null) && this.dbq.GetViewCount() == 2;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001A558 File Offset: 0x00018758
	public Vector2 beq()
	{
		float num = @do.bgf(this).bgj();
		return SimulatedRealityModule<SRCore>.Instance.getPhysicalSize() / 2f * num * new Vector2(this.cpv.width, this.cpv.height);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0001A5AC File Offset: 0x000187AC
	private void ber()
	{
		if (this.cqc[0] == null || this.cqc[1] == null)
		{
			return;
		}
		Camera[] array = this.cqc;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transform.position = base.gameObject.transform.position;
		}
		this.bes();
		this.bet();
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0001A618 File Offset: 0x00018818
	private void bes()
	{
		foreach (Camera camera in this.cqc)
		{
			camera.depth = (float)this.cpw;
			camera.clearFlags = this.cpr;
			camera.backgroundColor = this.cps;
			camera.useOcclusionCulling = this.cpy;
			camera.renderingPath = this.cpx;
		}
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0001A67C File Offset: 0x0001887C
	private void bet()
	{
		if (this.cqc != null && this.cqc[0] != null && this.cqc[1] != null)
		{
			if (SRCore.IsSimulatedRealityAvailable())
			{
				this.cqc[0].enabled = true;
				this.cqc[1].enabled = true;
				this.cqd[0].screenRect = new Rect(this.cpv.x, this.cpv.y, this.cpv.width, this.cpv.height);
				this.cqd[1].screenRect = new Rect(this.cpv.x, this.cpv.y, this.cpv.width, this.cpv.height);
				this.cqd[0].Update();
				this.cqd[1].Update();
				this.cqc[0].targetTexture = this.cqd[0].frameBuffer;
				this.cqc[1].targetTexture = this.cqd[1].frameBuffer;
				return;
			}
			this.cqc[0].enabled = true;
			this.cqc[1].enabled = false;
			this.cqc[0].rect = new Rect(this.cpv.x, this.cpv.y, this.cpv.width, this.cpv.height);
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0001A808 File Offset: 0x00018A08
	private void beu()
	{
		if (SRCore.IsSimulatedRealityAvailable())
		{
			for (int i = 0; i < this.cqc.Length; i++)
			{
				this.cqc[i].transform.position = this.dbq.GetEyeCamera(i).transform.position;
				this.cqc[i].transform.rotation = this.dbq.GetEyeCamera(i).transform.rotation;
				this.cqc[i].projectionMatrix = this.dbq.GetEyeCamera(i).projectionMatrix;
				LeiaUtils.CopyCameraParameters(this.dbq.GetEyeCamera(i), this.cqc[i]);
			}
			return;
		}
		this.cqc[0].nearClipPlane = this.cpt;
		this.cqc[0].farClipPlane = this.cpu;
		this.cqc[0].fieldOfView = this.cpz;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001A8F9 File Offset: 0x00018AF9
	private void bev(SRContextChangeReason a)
	{
		SRUtility.Debug(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bac(), a.ToString()));
		this.ber();
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001A91D File Offset: 0x00018B1D
	private void bew()
	{
		this.ber();
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0001A928 File Offset: 0x00018B28
	private void bex()
	{
		if (this.cqc == null)
		{
			return;
		}
		if (this.cqa == null)
		{
			return;
		}
		if (this.cqa.Count == 0)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.cqc[i] != null)
			{
				GameObject gameObject = this.cqc[i].transform.gameObject;
				List<Component> list = gameObject.GetComponents<Component>().ToList<Component>();
				list.Remove(this.cqc[i]);
				list.Remove(this.cqc[i].transform);
				foreach (Component component in this.cqa)
				{
					if (!(component == null))
					{
						Type type = component.GetType();
						Component component2 = gameObject.GetComponent(type);
						if (component2 == null)
						{
							component2 = gameObject.AddComponent(type);
						}
						list.Remove(component2);
						component2.hideFlags = 52;
						foreach (FieldInfo fieldInfo in type.GetFields())
						{
							fieldInfo.SetValue(component2, fieldInfo.GetValue(component));
						}
					}
				}
				foreach (Component component3 in list)
				{
					Object.DestroyImmediate(component3);
				}
			}
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0001AAB4 File Offset: 0x00018CB4
	private float bey(Vector2 a, Vector2 b, float c)
	{
		float num = Mathf.Clamp(Mathf.InverseLerp(a.x, a.y, c), 0f, 1f);
		return Mathf.Lerp(b.x, b.y, num);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0001AAF8 File Offset: 0x00018CF8
	public Vector3 bez(Vector2 a)
	{
		Vector2 vector = SimulatedRealityModule<SRCore>.Instance.getResolution() * new Vector2(this.cpv.width, this.cpv.height);
		Vector2 vector2 = SimulatedRealityModule<SRCore>.Instance.getPhysicalSize() / 2f;
		float num = this.bey(new Vector2(0f, vector.x), new Vector2(-vector2.x, vector2.x), a.x);
		float num2 = this.bey(new Vector2(0f, vector.y), new Vector2(-vector2.y, vector2.y), a.y);
		Vector3 vector3;
		vector3..ctor(num, num2, 0f);
		return base.transform.TransformPoint(vector3);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001ABC3 File Offset: 0x00018DC3
	public Vector3 bfa()
	{
		return this.bez(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001ABE4 File Offset: 0x00018DE4
	public Ray bfb()
	{
		Vector3[] eyes = SimulatedRealityModule<SREyes>.Instance.GetEyes(@do.bgf(this));
		Vector3 vector = (eyes[0] + eyes[1]) / 2f;
		Vector3 vector2 = base.transform.TransformPoint(vector);
		Vector3 normalized = (this.bfa() - vector2).normalized;
		return new Ray(vector2, normalized);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0001AC4A File Offset: 0x00018E4A
	public Camera[] bfc()
	{
		return this.cqc;
	}

	// Token: 0x0400051C RID: 1308
	[Header("Clearing")]
	private CameraClearFlags cpr = 1;

	// Token: 0x0400051D RID: 1309
	private readonly Color cps = Color.black;

	// Token: 0x0400051E RID: 1310
	[Header("Clipping")]
	[Tooltip("Near clipping plane distance in real world centimeters")]
	[Min(0.1f)]
	private float cpt = 10f;

	// Token: 0x0400051F RID: 1311
	[Tooltip("Far clipping plane distance in real world centimeters")]
	[Min(10f)]
	private float cpu = 1000f;

	// Token: 0x04000520 RID: 1312
	[Header("Output")]
	private Rect cpv = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x04000521 RID: 1313
	private readonly int cpw;

	// Token: 0x04000522 RID: 1314
	[Header("Rendering")]
	private RenderingPath cpx = -1;

	// Token: 0x04000523 RID: 1315
	private readonly bool cpy = true;

	// Token: 0x04000524 RID: 1316
	[Tooltip("Camera FOV to use when the Simulated Reality Runtime is not available")]
	private float cpz = 90f;

	// Token: 0x04000525 RID: 1317
	[Tooltip("Enable lookaround or show static 3D")]
	[SerializeField]
	private bool enableLookaround = true;

	// Token: 0x04000526 RID: 1318
	[Tooltip("These components will be replicated to the internal SR cameras")]
	private List<Component> cqa = new List<Component>();

	// Token: 0x04000527 RID: 1319
	[Tooltip("Allow access to camera components")]
	[Header("Warning: if not used correctly, this setting can cause issues in the project.")]
	private bool cqb;

	// Token: 0x04000528 RID: 1320
	private readonly Camera[] cqc = new Camera[2];

	// Token: 0x04000529 RID: 1321
	private readonly SRCompositor.SRFrameBuffer[] cqd = new SRCompositor.SRFrameBuffer[2];

	// Token: 0x0400052A RID: 1322
	private LeiaDisplay cqe;
}
