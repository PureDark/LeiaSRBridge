using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000067 RID: 103
public class EdgeLightEffect : MonoBehaviour
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000AE00 File Offset: 0x00009000
	private void Start()
	{
		this.cde = this.ny(this.child.sizeDelta);
		this.cda = this.mask.anchoredPosition;
		this.cdb = this.child.anchoredPosition;
		if (this.rayReceiver != null)
		{
			this.rayReceiver.OnFirstHitByRay.AddListener(new UnityAction(this.nu));
			this.rayReceiver.OnNotFirstHitByRay.AddListener(new UnityAction(this.nv));
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000AE8C File Offset: 0x0000908C
	private void Update()
	{
		this.nw(this.nx(Input.mousePosition));
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000AEA4 File Offset: 0x000090A4
	public void nu()
	{
		DOTweenModuleUI.DOFade(base.GetComponent<CanvasGroup>(), 1f, 0.3f);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000AEBC File Offset: 0x000090BC
	public void nv()
	{
		DOTweenModuleUI.DOFade(base.GetComponent<CanvasGroup>(), 0f, 0.3f);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000AED4 File Offset: 0x000090D4
	private void nw(float a)
	{
		Vector2 vector = this.oa(a);
		this.cda = Vector2.SmoothDamp(this.cda, vector, ref this.cdc, this.smoothTime);
		this.cdb = Vector2.SmoothDamp(this.cdb, new Vector2(vector.x * -1f, vector.y * -1f), ref this.cdd, this.smoothTime);
		this.mask.anchoredPosition = this.cda;
		this.child.anchoredPosition = this.cdb;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000AF64 File Offset: 0x00009164
	private float nx(Vector2 a)
	{
		Vector2 vector = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.st()).GetComponent<Camera>().WorldToScreenPoint(this.child.transform.position);
		Vector2 vector2 = a - new Vector2(vector.x, vector.y);
		float num = (float)((int)(Mathf.Atan2(vector2.y, vector2.x) * 57.29578f - 90f));
		if (num > 0f)
		{
			num -= 360f;
		}
		return Mathf.Abs(num);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000AFEC File Offset: 0x000091EC
	private List<ValueTuple<int, int>> ny(Vector2 a)
	{
		List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
		double num = (double)((int)a.x / 2);
		int num2 = (int)a.y / 2;
		float num3 = (float)Math.Sqrt(Math.Pow(num, 2.0) + Math.Pow((double)num2, 2.0));
		int num4 = 0;
		int num5 = Mathf.RoundToInt((float)(Math.Acos((double)((float)num2 / num3)) * 57.29577951308232));
		int num6 = 90 - num5;
		foreach (int num7 in new int[]
		{
			num5,
			num6,
			num6,
			num5,
			num5,
			num6,
			num6,
			num5
		})
		{
			int num8 = num4 + num7;
			if (num8 > 360)
			{
				num8 = 360;
			}
			list.Add(new ValueTuple<int, int>(num4, num8));
			num4 = num8;
		}
		return list;
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000B0D0 File Offset: 0x000092D0
	public cc nz(float a, List<ValueTuple<int, int>> b)
	{
		cc result = cc.ccs;
		for (int i = 0; i < b.Count; i++)
		{
			if ((float)b[i].Item1 <= a && a < (float)b[i].Item2)
			{
				result = (cc)i;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000B118 File Offset: 0x00009318
	private Vector2 oa(float a)
	{
		a %= 360f;
		if (a < 0f)
		{
			a = 360f + a;
		}
		float num = 0f;
		float num2 = 0f;
		Vector2 sizeDelta = this.child.sizeDelta;
		cc cc = this.nz(a, this.cde);
		if (cc == cc.ccz || cc == cc.ccs)
		{
			num2 = sizeDelta.y * 0.5f - this.padding;
			num2 *= 1f;
			if (cc == cc.ccs)
			{
				num = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, 0f, sizeDelta.x * 0.5f - this.padding);
			}
			else if (cc == cc.ccz)
			{
				num = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, -(sizeDelta.x * 0.5f - this.padding), 0f);
			}
		}
		else if (cc == cc.ccv || cc == cc.ccw)
		{
			num2 = sizeDelta.y * 0.5f - this.padding;
			num2 *= -1f;
			if (cc == cc.ccv)
			{
				num = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, sizeDelta.x * 0.5f - this.padding, 0f);
			}
			else if (cc == cc.ccw)
			{
				num = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, 0f, -(sizeDelta.x * 0.5f - this.padding));
			}
		}
		else if (cc == cc.cct || cc == cc.ccu)
		{
			num = sizeDelta.x * 0.5f - this.padding;
			num *= 1f;
			if (cc == cc.cct)
			{
				num2 = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, sizeDelta.y * 0.5f - this.padding, 0f);
			}
			else if (cc == cc.ccu)
			{
				num2 = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, 0f, -(sizeDelta.y * 0.5f - this.padding));
			}
		}
		else if (cc == cc.ccx || cc == cc.ccy)
		{
			num = sizeDelta.x * 0.5f - this.padding;
			num *= -1f;
			if (cc == cc.ccx)
			{
				num2 = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, -(sizeDelta.y * 0.5f - this.padding), 0f);
			}
			else if (cc == cc.ccy)
			{
				num2 = this.ob(a, (float)this.cde[(int)cc].Item1, (float)this.cde[(int)cc].Item2, 0f, sizeDelta.y * 0.5f - this.padding);
			}
		}
		return new Vector2(num, num2);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000B478 File Offset: 0x00009678
	private float ob(float a, float b, float c, float d, float e)
	{
		float num = c - b;
		float num2 = e - d;
		if (a == 0f)
		{
			return 0f;
		}
		return (a - b) * num2 / num + d;
	}

	// Token: 0x04000238 RID: 568
	public RectTransform mask;

	// Token: 0x04000239 RID: 569
	public RectTransform child;

	// Token: 0x0400023A RID: 570
	public Tweener tweener;

	// Token: 0x0400023B RID: 571
	public float padding;

	// Token: 0x0400023C RID: 572
	public RayReceiver rayReceiver;

	// Token: 0x0400023D RID: 573
	private Vector2 cda;

	// Token: 0x0400023E RID: 574
	private Vector2 cdb;

	// Token: 0x0400023F RID: 575
	private Vector2 cdc = Vector2.zero;

	// Token: 0x04000240 RID: 576
	private Vector2 cdd = Vector2.zero;

	// Token: 0x04000241 RID: 577
	public float smoothTime;

	// Token: 0x04000242 RID: 578
	private List<ValueTuple<int, int>> cde = new List<ValueTuple<int, int>>();
}
