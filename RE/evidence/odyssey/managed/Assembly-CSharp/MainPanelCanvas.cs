using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class MainPanelCanvas : MonoBehaviour
{
	// Token: 0x060003F7 RID: 1015 RVA: 0x000135D0 File Offset: 0x000117D0
	public void xq(cn a)
	{
		MainPanelCanvas.cx cx = new MainPanelCanvas.cx();
		cx.ckv = this;
		Vector2 vector = (a == cn.chy) ? MainPanelCanvas.ThumbnailViewBlendShapeSize : MainPanelCanvas.ListViewBlendShapeSize;
		cx.cku = this.skinnedMeshRenderer.GetBlendShapeWeight(0);
		cx.ckw = this.skinnedMeshRenderer.GetBlendShapeWeight(1);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnUpdate<TweenerCore<float, float, FloatOptions>>(DOTween.To(new DOGetter<float>(cx.xk), new DOSetter<float>(cx.xl), vector.x, 0.3f), new TweenCallback(cx.xm)), 9);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.OnUpdate<TweenerCore<float, float, FloatOptions>>(DOTween.To(new DOGetter<float>(cx.xn), new DOSetter<float>(cx.xo), vector.y, 0.3f), new TweenCallback(cx.xp)), 9);
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x000136A0 File Offset: 0x000118A0
	private void Start()
	{
		this.xq(cn.chy);
	}

	// Token: 0x040003EE RID: 1006
	public static Vector2 ThumbnailViewPanelSize = new Vector2(2150f, 1500f);

	// Token: 0x040003EF RID: 1007
	public static Vector2 ListViewPanelSize = new Vector2(1676f, 1423f);

	// Token: 0x040003F0 RID: 1008
	public static Vector2 ThumbnailViewBlendShapeSize = new Vector2(38.5f, 40f);

	// Token: 0x040003F1 RID: 1009
	public static Vector2 ListViewBlendShapeSize = new Vector2(61f, 40f);

	// Token: 0x040003F2 RID: 1010
	public SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x020000A4 RID: 164
	[CompilerGenerated]
	private sealed class cx
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00013709 File Offset: 0x00011909
		internal float xn()
		{
			return this.ckw;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013711 File Offset: 0x00011911
		internal void fwb()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(1, this.ckw);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001372A File Offset: 0x0001192A
		internal void dac()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cku);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013709 File Offset: 0x00011909
		internal float kso()
		{
			return this.ckw;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00013711 File Offset: 0x00011911
		internal void xp()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(1, this.ckw);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013743 File Offset: 0x00011943
		internal void xl(float a)
		{
			this.cku = a;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001372A File Offset: 0x0001192A
		internal void lxj()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cku);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013709 File Offset: 0x00011909
		internal float iym()
		{
			return this.ckw;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001374C File Offset: 0x0001194C
		internal float kzy()
		{
			return this.cku;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013743 File Offset: 0x00011943
		internal void bed(float a)
		{
			this.cku = a;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00013743 File Offset: 0x00011943
		internal void kie(float a)
		{
			this.cku = a;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001372A File Offset: 0x0001192A
		internal void hff()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cku);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001374C File Offset: 0x0001194C
		internal float hfk()
		{
			return this.cku;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013754 File Offset: 0x00011954
		internal void xo(float a)
		{
			this.ckw = a;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013754 File Offset: 0x00011954
		internal void mun(float a)
		{
			this.ckw = a;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00013709 File Offset: 0x00011909
		internal float ltt()
		{
			return this.ckw;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013754 File Offset: 0x00011954
		internal void ock(float a)
		{
			this.ckw = a;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001374C File Offset: 0x0001194C
		internal float xk()
		{
			return this.cku;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001372A File Offset: 0x0001192A
		internal void xm()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cku);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00013709 File Offset: 0x00011909
		internal float dpb()
		{
			return this.ckw;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001372A File Offset: 0x0001192A
		internal void hiq()
		{
			this.ckv.skinnedMeshRenderer.SetBlendShapeWeight(0, this.cku);
		}

		// Token: 0x040003F3 RID: 1011
		public float cku;

		// Token: 0x040003F4 RID: 1012
		public MainPanelCanvas ckv;

		// Token: 0x040003F5 RID: 1013
		public float ckw;
	}
}
