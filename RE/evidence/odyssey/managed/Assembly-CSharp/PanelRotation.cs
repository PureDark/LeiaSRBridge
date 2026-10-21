using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x0200006C RID: 108
public class PanelRotation : MonoBehaviour
{
	// Token: 0x0600024B RID: 587 RVA: 0x0000BAA8 File Offset: 0x00009CA8
	private void Start()
	{
		this.cdz = Object.FindAnyObjectByType<EyeTrackingInput>();
		this.panel = base.gameObject;
		this.cdt = base.GetComponent<RectTransform>();
		this.cdu = this.panel.transform.localRotation.eulerAngles.y;
		this.minRotation = this.cdu - 30f;
		this.maxRotation = this.cdu + 30f;
		this.cdv = this.panel.GetComponent<RectTransform>().sizeDelta.x / 2f * this.panel.GetComponent<RectTransform>().localScale.y;
		this.cdx = this.cdu;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000BB64 File Offset: 0x00009D64
	private void Update()
	{
		Vector2 inputPosition = this.cdz.inputPosition;
		if (this.cdt.rect.Contains(inputPosition))
		{
			Camera component = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sf()).GetComponent<Camera>();
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.cdz.trackingArea, inputPosition, component, ref vector);
			if (inputPosition.x <= -this.cdv)
			{
				this.cdw = this.minRotation;
			}
			else if (inputPosition.x >= this.cdv)
			{
				this.cdw = this.maxRotation;
			}
			else
			{
				float num = (inputPosition.x + this.cdv) / (2f * this.cdv);
				this.cdw = Mathf.Lerp(this.minRotation, this.maxRotation, num);
			}
			this.cdx = Mathf.SmoothDamp(this.cdx, this.cdw, ref this.cdy, this.smoothTime);
			this.panel.transform.localRotation = Quaternion.Euler(0f, this.cdx, 0f);
		}
	}

	// Token: 0x04000260 RID: 608
	private static string cds = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sy();

	// Token: 0x04000261 RID: 609
	[HideInInspector]
	public GameObject panel;

	// Token: 0x04000262 RID: 610
	public float smoothTime;

	// Token: 0x04000263 RID: 611
	private RectTransform cdt;

	// Token: 0x04000264 RID: 612
	private float cdu;

	// Token: 0x04000265 RID: 613
	public float minRotation;

	// Token: 0x04000266 RID: 614
	public float maxRotation;

	// Token: 0x04000267 RID: 615
	private float cdv;

	// Token: 0x04000268 RID: 616
	private float cdw;

	// Token: 0x04000269 RID: 617
	private float cdx;

	// Token: 0x0400026A RID: 618
	private float cdy;

	// Token: 0x0400026B RID: 619
	private EyeTrackingInput cdz;
}
