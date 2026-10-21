using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class LightEffect : MonoBehaviour
{
	// Token: 0x0600027D RID: 637 RVA: 0x0000CD2C File Offset: 0x0000AF2C
	private void Start()
	{
		bd.ih(LightEffect.cfa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		this.cfb = base.GetComponent<PanelLight>();
		if (this.rayReceiver == null)
		{
			bd.ie(LightEffect.cfa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tw(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
			return;
		}
		this.cfc = base.GetComponent<SkinnedMeshRenderer>().material;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000CD88 File Offset: 0x0000AF88
	private void Update()
	{
		if (this.rayReceiver.isFirstHit)
		{
			this.ph();
			this.pg(this.smoothRangeOnFirstHit, this.rotateRangeOnFirstHit);
			return;
		}
		if (this.rayReceiver.isHit)
		{
			this.ph();
			this.pg(this.smoothRangeOnSubHit, this.rotateRangeOnSubHit);
			return;
		}
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(ShortcutExtensions.DOFloat(this.cfc, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tx(), this.duration), this.delay);
		this.pg(this.smoothRangeOnNotHit, this.rotateRangeOnNotHit);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000CE1C File Offset: 0x0000B01C
	private void pg(float a, float b)
	{
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(ShortcutExtensions.DOFloat(this.cfc, a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ty(), this.duration), this.delay);
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(ShortcutExtensions.DOFloat(this.cfc, b, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tz(), this.duration), this.delay);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000CE70 File Offset: 0x0000B070
	private void ph()
	{
		float angle = this.angleCalculator.angle;
		float @float = this.cfc.GetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tx());
		float num = Mathf.DeltaAngle(@float, angle);
		float num2 = @float + num;
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(ShortcutExtensions.DOFloat(this.cfc, num2, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tx(), this.duration), this.delay);
	}

	// Token: 0x040002A2 RID: 674
	public bool useAngle = true;

	// Token: 0x040002A3 RID: 675
	private static string cfa = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ua();

	// Token: 0x040002A4 RID: 676
	[SerializeField]
	private RayReceiver rayReceiver;

	// Token: 0x040002A5 RID: 677
	[SerializeField]
	private AngleCalculator angleCalculator;

	// Token: 0x040002A6 RID: 678
	private PanelLight cfb;

	// Token: 0x040002A7 RID: 679
	[SerializeField]
	private float duration;

	// Token: 0x040002A8 RID: 680
	[SerializeField]
	private float delay;

	// Token: 0x040002A9 RID: 681
	[SerializeField]
	private float smoothRangeOnFirstHit;

	// Token: 0x040002AA RID: 682
	[SerializeField]
	private float smoothRangeOnSubHit;

	// Token: 0x040002AB RID: 683
	[SerializeField]
	private float smoothRangeOnNotHit;

	// Token: 0x040002AC RID: 684
	[SerializeField]
	private float rotateRangeOnFirstHit;

	// Token: 0x040002AD RID: 685
	[SerializeField]
	private float rotateRangeOnSubHit;

	// Token: 0x040002AE RID: 686
	[SerializeField]
	private float rotateRangeOnNotHit;

	// Token: 0x040002AF RID: 687
	private Material cfc;
}
