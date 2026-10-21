using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class LandRotation : MonoBehaviour
{
	// Token: 0x0600023E RID: 574 RVA: 0x0000B60D File Offset: 0x0000980D
	private void Start()
	{
		this.cdo = base.transform.rotation;
		if (this.subLand != null)
		{
			this.cdq = this.subLand.eulerAngles;
			this.cdp = Vector3.zero;
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000B64A File Offset: 0x0000984A
	private void Update()
	{
		if (this.cdr)
		{
			this.og();
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000B65C File Offset: 0x0000985C
	public void og()
	{
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = (mousePosition.x - num / 2f) / (num / 2f);
		float num4 = (mousePosition.y - num2 / 2f) / (num2 / 2f);
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		if (this.rotateOnX)
		{
			float num5 = (float)(this.invertX ? -1 : 1);
			eulerAngles.x = num4 * this.maxAngleX * num5;
			eulerAngles.x = Mathf.Clamp(eulerAngles.x, -this.maxAngleX, this.maxAngleX);
		}
		if (this.rotateOnY)
		{
			float num6 = (float)(this.invertY ? -1 : 1);
			eulerAngles.y = -num3 * this.maxAngleY * num6;
			eulerAngles.y = Mathf.Clamp(eulerAngles.y, -this.maxAngleY, this.maxAngleY);
		}
		if (this.rotateOnZ)
		{
			float num7 = (float)(this.invertZ ? -1 : 1);
			eulerAngles.z = (num3 + num4) * this.maxAngleZ * num7;
			eulerAngles.z = Mathf.Clamp(eulerAngles.z, -this.maxAngleZ, this.maxAngleZ);
		}
		this.cdo = Quaternion.Euler(eulerAngles);
		if (this.useSmoothDamp)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.cdo, Time.deltaTime / this.smoothTime);
			return;
		}
		base.transform.rotation = this.cdo;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000B7F4 File Offset: 0x000099F4
	public void oh()
	{
		this.cdr = false;
		float a = this.rotateClockwise ? 1f : -1f;
		base.StartCoroutine(this.oi(a));
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000B82B File Offset: 0x00009A2B
	private IEnumerator oi(float a)
	{
		Vector3 localEulerAngles = base.transform.localEulerAngles;
		while (!this.cdr)
		{
			base.transform.Rotate(Vector3.up * this.idleRotationSpeed * a * Time.deltaTime);
			float num = Mathf.MoveTowardsAngle(base.transform.localEulerAngles.x, 0f, Time.deltaTime * this.resetRotationSpeed);
			float num2 = Mathf.MoveTowardsAngle(base.transform.localEulerAngles.z, 0f, Time.deltaTime * this.resetRotationSpeed);
			base.transform.localEulerAngles = new Vector3(num, base.transform.localEulerAngles.y, num2);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000B844 File Offset: 0x00009A44
	public void oj(bool a)
	{
		this.cdr = a;
		if (a)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, Vector3.one, 3f), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(base.transform, Vector3.zero, 3f, false), 9);
			TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(ShortcutExtensions.DOLocalRotate(this.subLand, this.cdq, 3f, 0), 9);
			base.StopAllCoroutines();
			this.og();
			return;
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(1.1f, 1.1f, 1.1f), 3f), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(base.transform, new Vector3(0f, -0.06f, 0f), 3f, false), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(ShortcutExtensions.DOLocalRotate(this.subLand, this.cdp, 3f, 0), 9);
		this.oh();
	}

	// Token: 0x04000249 RID: 585
	[Header("X Axis Rotation Settings")]
	[Range(0f, 100f)]
	public float maxAngleX;

	// Token: 0x0400024A RID: 586
	public bool rotateOnX = true;

	// Token: 0x0400024B RID: 587
	public bool invertX;

	// Token: 0x0400024C RID: 588
	[Header("Y Axis Rotation Settings")]
	[Range(0f, 100f)]
	public float maxAngleY;

	// Token: 0x0400024D RID: 589
	public bool rotateOnY = true;

	// Token: 0x0400024E RID: 590
	public bool invertY;

	// Token: 0x0400024F RID: 591
	[Header("Z Axis Rotation Settings")]
	[Range(0f, 100f)]
	public float maxAngleZ;

	// Token: 0x04000250 RID: 592
	public bool rotateOnZ;

	// Token: 0x04000251 RID: 593
	public bool invertZ;

	// Token: 0x04000252 RID: 594
	[Header("SmoothDamp Settings")]
	public bool useSmoothDamp = true;

	// Token: 0x04000253 RID: 595
	public float smoothTime;

	// Token: 0x04000254 RID: 596
	[Header("Idle Rotation Settings")]
	public float idleRotationSpeed = 10f;

	// Token: 0x04000255 RID: 597
	public float resetRotationSpeed = 1f;

	// Token: 0x04000256 RID: 598
	public bool rotateClockwise = true;

	// Token: 0x04000257 RID: 599
	[Header("SubLand Settings")]
	public Transform subLand;

	// Token: 0x04000258 RID: 600
	private Quaternion cdo;

	// Token: 0x04000259 RID: 601
	private Vector3 cdp;

	// Token: 0x0400025A RID: 602
	private Vector3 cdq;

	// Token: 0x0400025B RID: 603
	private bool cdr = true;
}
