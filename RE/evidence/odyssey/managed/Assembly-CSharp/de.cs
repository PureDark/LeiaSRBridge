using System;
using UnityEngine;
using UnityEngine.Pool;

// Token: 0x020000B6 RID: 182
public class de
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00016321 File Offset: 0x00014521
	// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00016329 File Offset: 0x00014529
	public IObjectPool<GameObject> cms { get; private set; }

	// Token: 0x060004A9 RID: 1193 RVA: 0x00016332 File Offset: 0x00014532
	private GameObject lss()
	{
		return Object.Instantiate<GameObject>(this.cmt, this.cmu);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00016345 File Offset: 0x00014545
	private void bav(GameObject a)
	{
		Object.Destroy(a);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00016332 File Offset: 0x00014532
	private GameObject hqr()
	{
		return Object.Instantiate<GameObject>(this.cmt, this.cmu);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001634D File Offset: 0x0001454D
	private void bat(GameObject a)
	{
		a.SetActive(false);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00016345 File Offset: 0x00014545
	private void nlp(GameObject a)
	{
		Object.Destroy(a);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00016332 File Offset: 0x00014532
	private GameObject bas()
	{
		return Object.Instantiate<GameObject>(this.cmt, this.cmu);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00016345 File Offset: 0x00014545
	private void hau(GameObject a)
	{
		Object.Destroy(a);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0001634D File Offset: 0x0001454D
	private void lui(GameObject a)
	{
		a.SetActive(false);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0001634D File Offset: 0x0001454D
	private void cvd(GameObject a)
	{
		a.SetActive(false);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00016345 File Offset: 0x00014545
	private void iht(GameObject a)
	{
		Object.Destroy(a);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00016345 File Offset: 0x00014545
	private void ech(GameObject a)
	{
		Object.Destroy(a);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00016358 File Offset: 0x00014558
	public de(GameObject a, Transform b, bool c = true)
	{
		this.cmt = a;
		this.cmu = b;
		this.cms = new ObjectPool<GameObject>(new Func<GameObject>(this.bas), new Action<GameObject>(this.bau), new Action<GameObject>(this.bat), new Action<GameObject>(this.bav), c, 20, 1000);
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x000163BC File Offset: 0x000145BC
	private void bau(GameObject a)
	{
		a.SetActive(true);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0001634D File Offset: 0x0001454D
	private void hhf(GameObject a)
	{
		a.SetActive(false);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000163C8 File Offset: 0x000145C8
	public a bar<a>()
	{
		a result;
		if (this.cms.Get().TryGetComponent<a>(ref result))
		{
			return result;
		}
		return default(a);
	}

	// Token: 0x0400046C RID: 1132
	private readonly GameObject cmt;

	// Token: 0x0400046D RID: 1133
	private readonly Transform cmu;

	// Token: 0x0400046E RID: 1134
	private const int cmv = 20;

	// Token: 0x0400046F RID: 1135
	private const int cmw = 1000;
}
