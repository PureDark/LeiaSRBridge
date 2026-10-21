using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000062 RID: 98
public class AngleCalculator : MonoBehaviour
{
	// Token: 0x0600020B RID: 523 RVA: 0x0000238E File Offset: 0x0000058E
	private void Start()
	{
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000A34C File Offset: 0x0000854C
	private void Update()
	{
		this.angle = this.ne(Input.mousePosition);
		if (this.needle != null)
		{
			this.needle.eulerAngles = new Vector3(0f, 0f, -this.angle);
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000A3A0 File Offset: 0x000085A0
	private float ne(Vector2 a)
	{
		Vector2 vector = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sf()).GetComponent<Camera>().WorldToScreenPoint(base.transform.position);
		Vector2 vector2 = a - vector;
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f - 90f;
		if (this.clockwise)
		{
			num = -num;
		}
		if (num < 0f)
		{
			num += 360f;
		}
		return num;
	}

	// Token: 0x04000218 RID: 536
	public float angle;

	// Token: 0x04000219 RID: 537
	public RectTransform needle;

	// Token: 0x0400021A RID: 538
	public bool clockwise = true;

	// Token: 0x0400021B RID: 539
	private static string ccm = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sr();
}
