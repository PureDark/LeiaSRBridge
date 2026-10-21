using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class BackgroundBlur : MonoBehaviour
{
	// Token: 0x060001BC RID: 444 RVA: 0x0000938C File Offset: 0x0000758C
	private void Start()
	{
		this.BlurOff();
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00009394 File Offset: 0x00007594
	public void BlurOn()
	{
		bd.id(this.cbl, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sa(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sb());
		ShortcutExtensions.DOFloat(this.fullScreenBlurMaterial, 1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sc(), this.duration);
		this.ControlCameraAperture(16f);
	}

	// Token: 0x060001BE RID: 446 RVA: 0x000093D2 File Offset: 0x000075D2
	public void BlurOff()
	{
		bd.id(this.cbl, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sd(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.se());
		ShortcutExtensions.DOFloat(this.fullScreenBlurMaterial, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sc(), this.duration);
		this.ControlCameraAperture(0.7f);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00009410 File Offset: 0x00007610
	public void ControlCameraAperture(float aperture)
	{
		BackgroundBlur.bq bq = new BackgroundBlur.bq();
		bq.cbj = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sf()).GetComponent<Camera>();
		if (bq.cbj != null)
		{
			BackgroundBlur.br br = new BackgroundBlur.br();
			br.cbk = bq.cbj.aperture;
			DOTween.To(new DOGetter<float>(br.mf), new DOSetter<float>(bq.me), aperture, this.duration);
			return;
		}
		bd.ie(this.cbl, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sh());
	}

	// Token: 0x040001EA RID: 490
	private string cbl = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.si();

	// Token: 0x040001EB RID: 491
	public Material fullScreenBlurMaterial;

	// Token: 0x040001EC RID: 492
	public float duration = 0.3f;

	// Token: 0x02000055 RID: 85
	[CompilerGenerated]
	private sealed class bq
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000094B4 File Offset: 0x000076B4
		internal void me(float a)
		{
			this.cbj.aperture = a;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000094B4 File Offset: 0x000076B4
		internal void dlo(float a)
		{
			this.cbj.aperture = a;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000094B4 File Offset: 0x000076B4
		internal void mzu(float a)
		{
			this.cbj.aperture = a;
		}

		// Token: 0x040001ED RID: 493
		public Camera cbj;
	}

	// Token: 0x02000056 RID: 86
	[CompilerGenerated]
	private sealed class br
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000094C2 File Offset: 0x000076C2
		internal float gav()
		{
			return this.cbk;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000094C2 File Offset: 0x000076C2
		internal float mf()
		{
			return this.cbk;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000094C2 File Offset: 0x000076C2
		internal float hur()
		{
			return this.cbk;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000094C2 File Offset: 0x000076C2
		internal float ljd()
		{
			return this.cbk;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000094C2 File Offset: 0x000076C2
		internal float ish()
		{
			return this.cbk;
		}

		// Token: 0x040001EE RID: 494
		public float cbk;
	}
}
