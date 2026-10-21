using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using SRUnity;
using UnityEngine;
using UnityEngine.Video;

// Token: 0x020000E5 RID: 229
public class LeiaMedia : MonoBehaviour
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001BB48 File Offset: 0x00019D48
	// (set) Token: 0x06000620 RID: 1568 RVA: 0x0001BB50 File Offset: 0x00019D50
	public LeiaMedia.dv dcs
	{
		get
		{
			return this._mediaScaleMode;
		}
		set
		{
			this._mediaScaleMode = value;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001BB59 File Offset: 0x00019D59
	// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001BB61 File Offset: 0x00019D61
	public Rect dct
	{
		get
		{
			return this.onscreenPercent;
		}
		set
		{
			this.onscreenPercent = value;
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0001BB6A File Offset: 0x00019D6A
	private void Start()
	{
		this.bij();
		if (this.mediaType == LeiaMedia.du.cro)
		{
			this.videoPlayer.errorReceived += new VideoPlayer.ErrorEventHandler(this.bil);
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0001BB94 File Offset: 0x00019D94
	private void Update()
	{
		if (!this.csb && this.crr.targetTexture != null && this.crs.targetTexture != null)
		{
			this.cry = new RenderTexture(this.crr.targetTexture.width, this.crr.targetTexture.height, this.crr.targetTexture.depth);
			this.crz = new RenderTexture(this.crs.targetTexture.width, this.crs.targetTexture.height, this.crs.targetTexture.depth);
			this.csb = true;
		}
		if (this.crx != Singleton<RenderTrackingDevice>.Instance.DesiredLightfieldMode)
		{
			this.crx = Singleton<RenderTrackingDevice>.Instance.DesiredLightfieldMode;
			this.bij();
		}
		if (this.dcs == LeiaMedia.dv.crp)
		{
			Vector3 localScale = base.transform.localScale;
			this.onscreenPercent.width = localScale.x;
			this.onscreenPercent.height = localScale.y;
			this.onscreenPercent.x = 0.5f - this.onscreenPercent.width / 2f;
			this.onscreenPercent.y = 0.5f - this.onscreenPercent.height / 2f;
			this.bik();
		}
		if (this.mediaType == LeiaMedia.du.cro)
		{
			this.OnRenderObject();
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0001BD0C File Offset: 0x00019F0C
	private void bij()
	{
		this.cru = (this.cru ?? new Material(Shader.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ban())));
		this.crv = (this.crv ?? new Material(Shader.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bao())));
		this.crw = (this.crw ?? new Material(Shader.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ban())));
		GameObject gameObject = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bap());
		this.csa = gameObject.GetComponent<SimulatedRealityCamera>();
		this.crr = this.csa.bfc()[0];
		this.crs = this.csa.bfc()[1];
		if (this.mediaType == LeiaMedia.du.crn)
		{
			this.cru.mainTexture = this.sbsTexture;
			this.crv.mainTexture = this.sbsTexture;
		}
		this.bik();
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001BDE4 File Offset: 0x00019FE4
	private void bik()
	{
		Vector4 vector;
		vector..ctor(this.onscreenPercent.x, this.onscreenPercent.y, this.onscreenPercent.width, this.onscreenPercent.height);
		float num = (this.dcs == LeiaMedia.dv.crq || this.dcs == LeiaMedia.dv.crp) ? 1f : 0f;
		this.cru.SetVector(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baq(), vector);
		this.cru.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bar(), num);
		this.crv.SetVector(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baq(), vector);
		this.crv.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bar(), num);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001BE88 File Offset: 0x0001A088
	private void OnRenderObject()
	{
		if (this.mediaType == LeiaMedia.du.cro)
		{
			Graphics.Blit(this.videoPlayer.texture, this.cry, this.cru);
			Graphics.Blit(this.videoPlayer.texture, this.crz, this.crv);
		}
		else if (this.mediaType == LeiaMedia.du.crn)
		{
			Graphics.Blit(this.sbsTexture, this.cry, this.cru);
			Graphics.Blit(this.sbsTexture, this.crz, this.crv);
		}
		SRCompositor compositor = SimulatedRealityModule<SRRender>.Instance.GetCompositor();
		SRCompositor.SRFrameBuffer frameBuffer = compositor.GetFrameBuffer(this.csa.bfc()[0].GetInstanceID().ToString());
		SRCompositor.SRFrameBuffer frameBuffer2 = compositor.GetFrameBuffer(this.csa.bfc()[1].GetInstanceID().ToString());
		frameBuffer.frameBuffer = this.cry;
		frameBuffer2.frameBuffer = this.crz;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0001BF6F File Offset: 0x0001A16F
	private void bil(VideoPlayer a, string b)
	{
		LogUtil.Log(LogLevel.Error, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bas(), a, b), Array.Empty<object>());
	}

	// Token: 0x04000556 RID: 1366
	[SerializeField]
	private LeiaMedia.du mediaType;

	// Token: 0x04000557 RID: 1367
	[SerializeField]
	private Texture2D sbsTexture;

	// Token: 0x04000558 RID: 1368
	[SerializeField]
	private VideoPlayer videoPlayer;

	// Token: 0x04000559 RID: 1369
	[SerializeField]
	private LeiaDisplay leiaDisplay;

	// Token: 0x0400055A RID: 1370
	private Camera crr;

	// Token: 0x0400055B RID: 1371
	private Camera crs;

	// Token: 0x0400055C RID: 1372
	private Camera crt;

	// Token: 0x0400055D RID: 1373
	private Material cru;

	// Token: 0x0400055E RID: 1374
	private Material crv;

	// Token: 0x0400055F RID: 1375
	private Material crw;

	// Token: 0x04000560 RID: 1376
	private RenderTrackingDevice.LightfieldMode crx;

	// Token: 0x04000561 RID: 1377
	private RenderTexture cry;

	// Token: 0x04000562 RID: 1378
	private RenderTexture crz;

	// Token: 0x04000563 RID: 1379
	private SimulatedRealityCamera csa;

	// Token: 0x04000564 RID: 1380
	private bool csb;

	// Token: 0x04000565 RID: 1381
	[SerializeField]
	private LeiaMedia.dv _mediaScaleMode;

	// Token: 0x04000566 RID: 1382
	[Tooltip("X,Y: offset from left bottom screen corner, W : width H: height")]
	[SerializeField]
	private Rect onscreenPercent = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x020000E6 RID: 230
	public enum du
	{
		// Token: 0x04000568 RID: 1384
		crn,
		// Token: 0x04000569 RID: 1385
		cro
	}

	// Token: 0x020000E7 RID: 231
	public enum dv
	{
		// Token: 0x0400056B RID: 1387
		crp,
		// Token: 0x0400056C RID: 1388
		crq
	}
}
