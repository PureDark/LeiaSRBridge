using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000057 RID: 87
public class ActiveIdleTransition : MonoBehaviour
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060001CB RID: 459 RVA: 0x000094CC File Offset: 0x000076CC
	public GameItemMenu czn
	{
		get
		{
			return this.cck = ((this.cck != null) ? this.cck : Object.FindAnyObjectByType<GameItemMenu>());
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00009500 File Offset: 0x00007700
	private void Start()
	{
		bd.ih(this.cce, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		for (int i = 0; i < this.activeMaterialObjs.Count; i++)
		{
			if (this.activeMaterialObjs[i].GetComponent<SkinnedMeshRenderer>() != null)
			{
				this.activeMaterials.Add(this.activeMaterialObjs[i].GetComponent<SkinnedMeshRenderer>().materials[0]);
			}
			if (this.activeMaterialObjs[i].GetComponent<MeshRenderer>() != null)
			{
				this.activeMaterials.Add(this.activeMaterialObjs[i].GetComponent<MeshRenderer>().materials[0]);
			}
		}
		for (int j = 0; j < this.idleMaterialObjs.Count; j++)
		{
			if (this.idleMaterialObjs[j].GetComponent<SkinnedMeshRenderer>() != null)
			{
				this.idleMaterials.Add(this.idleMaterialObjs[j].GetComponent<SkinnedMeshRenderer>().materials[0]);
			}
			if (this.idleMaterialObjs[j].GetComponent<MeshRenderer>() != null)
			{
				this.idleMaterials.Add(this.idleMaterialObjs[j].GetComponent<MeshRenderer>().materials[0]);
			}
		}
		this.activeLightsOriginalIntensity.Clear();
		for (int k = 0; k < this.activeLights.Count; k++)
		{
			this.activeLightsOriginalIntensity.Add(this.activeLights[k].intensity);
		}
		this.idleLightsOriginalIntensity.Clear();
		for (int l = 0; l < this.idleLights.Count; l++)
		{
			this.idleLightsOriginalIntensity.Add(this.idleLights[l].intensity);
		}
		this.Transition();
		this.czn.vw();
		this.cci = Input.mousePosition;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x000096DC File Offset: 0x000078DC
	private void Update()
	{
		bool flag = bl.jk(bl.caj);
		bool flag2 = bl.jl();
		bool flag3 = this.cci != Input.mousePosition;
		bool flag4 = flag && Input.anyKeyDown && !this.ccl;
		bool flag5 = flag2 && (flag3 || Input.GetMouseButton(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.mouseScrollDelta.y != 0f);
		bool flag6 = PopupManager.bvl || SearchBar.cln || !bl.jg();
		if (flag4 || flag5)
		{
			this.ccg = 0f;
			if (!ActiveIdleTransition.isActive)
			{
				this.SwitchMode();
			}
		}
		else if (flag6)
		{
			this.ccg = 0f;
		}
		else
		{
			this.ccg += Time.deltaTime;
			if (this.ccg >= this.ccf && ActiveIdleTransition.isActive)
			{
				this.SwitchMode();
			}
		}
		this.cci = Input.mousePosition;
		this.ccl = false;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x000097DF File Offset: 0x000079DF
	private void SwitchMode()
	{
		bd.ih(this.cce, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.so());
		ActiveIdleTransition.isActive = !ActiveIdleTransition.isActive;
		this.Transition();
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00009804 File Offset: 0x00007A04
	private void Transition()
	{
		bd.ih(this.cce, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sp());
		if (ActiveIdleTransition.isActive)
		{
			if (MouseCursor.currentCursor != null)
			{
				MouseCursor.currentCursor.SetActive(true);
			}
			base.StopCoroutine(this.IdleToActive());
			base.StartCoroutine(this.IdleToActive());
		}
		else
		{
			this.nb();
			if (MouseCursor.currentCursor != null)
			{
				MouseCursor.currentCursor.SetActive(false);
			}
			base.StopCoroutine(this.ActiveToIdle());
			base.StartCoroutine(this.ActiveToIdle());
		}
		this.landRotation.oj(ActiveIdleTransition.isActive);
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x000098A4 File Offset: 0x00007AA4
	private void nb()
	{
		this.ccj = ActiveIdleTransition.nd<Game>(bn.cyp.cyq.Games, this.cch, this.ccj);
		for (int i = 0; i < this.ccj.Length; i++)
		{
			ActiveIdleTransition.bs bs = new ActiveIdleTransition.bs();
			bs.cbn = this;
			bs.cbm = i;
			bn.cyp.cyq.GetGameSprite(this.ccj[bs.cbm].ID_Thumbnail.AllInOneImage, new Action<Sprite>(bs.mg), null);
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00009934 File Offset: 0x00007B34
	private void nc(int a, float b)
	{
		ActiveIdleTransition.bt bt = new ActiveIdleTransition.bt();
		bt.cbo = this;
		bt.cbp = a;
		Game[] array = ActiveIdleTransition.nd<Game>(bn.cyp.cyq.Games, 1, this.ccj);
		if (array.Length == 0)
		{
			return;
		}
		bn.cyp.cyq.GetGameSprite(array[0].ID_Thumbnail.AllInOneImage, new Action<Sprite>(bt.mh), null);
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x000099A0 File Offset: 0x00007BA0
	private static a[] nd<a>(IList<a> a, int b, IEnumerable<a> c = null)
	{
		ActiveIdleTransition.bu<a> bu = new ActiveIdleTransition.bu<a>();
		bu.cbq = ((c != null) ? new HashSet<a>(c) : null);
		a[] array = (bu.cbq == null || bu.cbq.Count == 0) ? a.ToArray<a>() : a.Where(new Func<a, bool>(bu.mi)).ToArray<a>();
		int num = Mathf.Min(b, array.Length);
		for (int i = 0; i < num; i++)
		{
			int num2 = Random.Range(i, array.Length);
			a[] array2 = array;
			int num3 = i;
			a[] array3 = array;
			int num4 = num2;
			a a2 = array[num2];
			a a3 = array[i];
			array2[num3] = a2;
			array3[num4] = a3;
		}
		return array.Take(num).ToArray<a>();
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00009A59 File Offset: 0x00007C59
	private IEnumerator ActiveToIdle()
	{
		bd.id(this.cce, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sk());
		foreach (BaseGameItem baseGameItem in Object.FindObjectsByType(typeof(BaseGameItem), 0).Cast<BaseGameItem>().ToArray<BaseGameItem>())
		{
			UnityEvent onEventEndHover = baseGameItem.dao.OnEventEndHover;
			if (onEventEndHover != null)
			{
				onEventEndHover.Invoke();
			}
			baseGameItem.dao.ry(CustomButton.ck.cgt);
			baseGameItem.dao.isEnabled = false;
		}
		for (int j = 0; j < this.activeCanvasGroups.Count; j++)
		{
			DOTweenModuleUI.DOFade(this.activeCanvasGroups[j], 0f, this.duration);
		}
		for (int k = 0; k < this.activeMaterials.Count; k++)
		{
			ShortcutExtensions.DOFloat(this.activeMaterials[k], 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sl(), this.duration);
		}
		for (int l = 0; l < this.activeLights.Count; l++)
		{
			ActiveIdleTransition.bv bv = new ActiveIdleTransition.bv();
			bv.cbr = this.activeLights[l];
			DOTween.To(new DOGetter<float>(bv.mj), new DOSetter<float>(bv.mk), 0f, this.duration);
		}
		yield return new WaitForSeconds(this.duration);
		for (int m = 0; m < this.activeCanvasGroups.Count; m++)
		{
			this.activeCanvasGroups[m].gameObject.SetActive(false);
		}
		for (int n = 0; n < this.activeObjects.Count; n++)
		{
			this.activeObjects[n].SetActive(false);
		}
		for (int num = 0; num < this.idleObjects.Count; num++)
		{
			this.idleObjects[num].SetActive(true);
		}
		for (int num2 = 0; num2 < this.idleCanvasGroups.Count; num2++)
		{
			DOTweenModuleUI.DOFade(this.idleCanvasGroups[num2], 1f, this.duration);
		}
		for (int num3 = 0; num3 < this.idleMaterials.Count; num3++)
		{
			ShortcutExtensions.DOFloat(this.idleMaterials[num3], 1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sl(), this.duration);
		}
		for (int num4 = 0; num4 < this.idleLights.Count; num4++)
		{
			ActiveIdleTransition.bw bw = new ActiveIdleTransition.bw();
			bw.cbs = this.idleLights[num4];
			DOTween.To(new DOGetter<float>(bw.ml), new DOSetter<float>(bw.mm), this.idleLightsOriginalIntensity[num4], this.duration);
		}
		yield break;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00009A68 File Offset: 0x00007C68
	private IEnumerator IdleToActive()
	{
		ActiveIdleTransition.bx bx = new ActiveIdleTransition.bx();
		bx.cbt = this;
		bd.id(this.cce, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sm(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sn());
		bx.cbu = 0;
		while (bx.cbu < this.idleCanvasGroups.Count)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTweenModuleUI.DOFade(this.idleCanvasGroups[bx.cbu], 0f, this.duration);
			TweenCallback tweenCallback;
			if ((tweenCallback = bx.cbv) == null)
			{
				tweenCallback = (bx.cbv = new TweenCallback(bx.mn));
			}
			TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(tweenerCore, tweenCallback);
			int i = bx.cbu;
			bx.cbu = i + 1;
		}
		for (int j = 0; j < this.idleMaterials.Count; j++)
		{
			ShortcutExtensions.DOFloat(this.idleMaterials[j], 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sl(), this.duration);
		}
		for (int k = 0; k < this.idleLights.Count; k++)
		{
			ActiveIdleTransition.by by = new ActiveIdleTransition.by();
			by.cbw = this.idleLights[k];
			DOTween.To(new DOGetter<float>(by.mo), new DOSetter<float>(by.mp), 0f, this.duration);
		}
		yield return new WaitForSeconds(this.duration);
		for (int l = 0; l < this.idleObjects.Count; l++)
		{
			this.idleObjects[l].SetActive(false);
		}
		for (int m = 0; m < this.activeObjects.Count; m++)
		{
			this.activeObjects[m].SetActive(true);
		}
		for (int n = 0; n < this.activeCanvasGroups.Count; n++)
		{
			DOTweenModuleUI.DOFade(this.activeCanvasGroups[n], 1f, this.duration);
		}
		for (int num = 0; num < this.activeMaterials.Count; num++)
		{
			ShortcutExtensions.DOFloat(this.activeMaterials[num], 1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sl(), this.duration);
		}
		for (int num2 = 0; num2 < this.activeLights.Count; num2++)
		{
			ActiveIdleTransition.bz bz = new ActiveIdleTransition.bz();
			bz.cbx = this.activeLights[num2];
			DOTween.To(new DOGetter<float>(bz.mq), new DOSetter<float>(bz.mr), this.activeLightsOriginalIntensity[num2], this.duration);
		}
		yield return new WaitForSeconds(this.duration);
		foreach (BaseGameItem baseGameItem in Object.FindObjectsByType(typeof(BaseGameItem), 0).Cast<BaseGameItem>().ToArray<BaseGameItem>())
		{
			UnityEvent onEventEndHover = baseGameItem.dao.OnEventEndHover;
			if (onEventEndHover != null)
			{
				onEventEndHover.Invoke();
			}
			baseGameItem.dao.ry(CustomButton.ck.cgt);
			baseGameItem.dao.isEnabled = true;
		}
		yield break;
	}

	// Token: 0x040001EF RID: 495
	private string cce = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sq();

	// Token: 0x040001F0 RID: 496
	public float duration;

	// Token: 0x040001F1 RID: 497
	private float ccf = 30f;

	// Token: 0x040001F2 RID: 498
	private float ccg;

	// Token: 0x040001F3 RID: 499
	private int cch = 7;

	// Token: 0x040001F4 RID: 500
	private Vector3 cci;

	// Token: 0x040001F5 RID: 501
	public static bool isActive = true;

	// Token: 0x040001F6 RID: 502
	public List<CanvasGroup> activeCanvasGroups;

	// Token: 0x040001F7 RID: 503
	public List<GameObject> activeObjects;

	// Token: 0x040001F8 RID: 504
	public List<GameObject> activeMaterialObjs;

	// Token: 0x040001F9 RID: 505
	public List<Material> activeMaterials;

	// Token: 0x040001FA RID: 506
	public List<Light> activeLights;

	// Token: 0x040001FB RID: 507
	public List<float> activeLightsOriginalIntensity;

	// Token: 0x040001FC RID: 508
	public List<CanvasGroup> idleCanvasGroups;

	// Token: 0x040001FD RID: 509
	public List<GameObject> idleMaterialObjs;

	// Token: 0x040001FE RID: 510
	public List<GameObject> idleObjects;

	// Token: 0x040001FF RID: 511
	public List<Material> idleMaterials;

	// Token: 0x04000200 RID: 512
	public List<Light> idleLights;

	// Token: 0x04000201 RID: 513
	public List<float> idleLightsOriginalIntensity;

	// Token: 0x04000202 RID: 514
	public LandRotation landRotation;

	// Token: 0x04000203 RID: 515
	private Game[] ccj;

	// Token: 0x04000204 RID: 516
	private GameItemMenu cck;

	// Token: 0x04000205 RID: 517
	private bool ccl;

	// Token: 0x02000058 RID: 88
	[CompilerGenerated]
	private sealed class bs
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00009AA4 File Offset: 0x00007CA4
		internal void hru(Sprite a)
		{
			IdleThumbnailChanger component = this.cbn.idleMaterialObjs[this.cbm].GetComponent<IdleThumbnailChanger>();
			component.rawImage.texture = a.texture;
			component.index = this.cbm;
			component.cee -= this.cbn.nc;
			component.cee += this.cbn.nc;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009B18 File Offset: 0x00007D18
		internal void hag(Sprite a)
		{
			IdleThumbnailChanger component = this.cbn.idleMaterialObjs[this.cbm].GetComponent<IdleThumbnailChanger>();
			component.rawImage.texture = a.texture;
			component.index = this.cbm;
			component.cee -= this.cbn.nc;
			component.cee += this.cbn.nc;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009B8C File Offset: 0x00007D8C
		internal void mg(Sprite a)
		{
			IdleThumbnailChanger component = this.cbn.idleMaterialObjs[this.cbm].GetComponent<IdleThumbnailChanger>();
			component.rawImage.texture = a.texture;
			component.index = this.cbm;
			component.cee -= this.cbn.nc;
			component.cee += this.cbn.nc;
		}

		// Token: 0x04000206 RID: 518
		public int cbm;

		// Token: 0x04000207 RID: 519
		public ActiveIdleTransition cbn;
	}

	// Token: 0x02000059 RID: 89
	[CompilerGenerated]
	private sealed class bt
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00009BFE File Offset: 0x00007DFE
		internal void mh(Sprite a)
		{
			this.cbo.idleMaterialObjs[this.cbp].GetComponent<IdleThumbnailChanger>().rawImage.texture = a.texture;
		}

		// Token: 0x04000208 RID: 520
		public ActiveIdleTransition cbo;

		// Token: 0x04000209 RID: 521
		public int cbp;
	}

	// Token: 0x0200005A RID: 90
	[CompilerGenerated]
	private sealed class bu<a>
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00009C2B File Offset: 0x00007E2B
		internal bool mi(a a)
		{
			return !this.cbq.Contains(a);
		}

		// Token: 0x0400020A RID: 522
		public HashSet<a> cbq;
	}

	// Token: 0x0200005B RID: 91
	[CompilerGenerated]
	private sealed class bv
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00009C3C File Offset: 0x00007E3C
		internal void mk(float a)
		{
			this.cbr.intensity = a;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009C3C File Offset: 0x00007E3C
		internal void oqm(float a)
		{
			this.cbr.intensity = a;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009C4A File Offset: 0x00007E4A
		internal float kdm()
		{
			return this.cbr.intensity;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009C3C File Offset: 0x00007E3C
		internal void nss(float a)
		{
			this.cbr.intensity = a;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009C3C File Offset: 0x00007E3C
		internal void egf(float a)
		{
			this.cbr.intensity = a;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009C4A File Offset: 0x00007E4A
		internal float mj()
		{
			return this.cbr.intensity;
		}

		// Token: 0x0400020B RID: 523
		public Light cbr;
	}

	// Token: 0x0200005C RID: 92
	[CompilerGenerated]
	private sealed class bw
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00009C57 File Offset: 0x00007E57
		internal float mtp()
		{
			return this.cbs.intensity;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009C64 File Offset: 0x00007E64
		internal void mm(float a)
		{
			this.cbs.intensity = a;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009C57 File Offset: 0x00007E57
		internal float ml()
		{
			return this.cbs.intensity;
		}

		// Token: 0x0400020C RID: 524
		public Light cbs;
	}

	// Token: 0x0200005D RID: 93
	[CompilerGenerated]
	private sealed class bx
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00009C72 File Offset: 0x00007E72
		internal void mn()
		{
			this.cbt.activeCanvasGroups[this.cbu].gameObject.SetActive(false);
		}

		// Token: 0x0400020D RID: 525
		public ActiveIdleTransition cbt;

		// Token: 0x0400020E RID: 526
		public int cbu;

		// Token: 0x0400020F RID: 527
		public TweenCallback cbv;
	}

	// Token: 0x0200005E RID: 94
	[CompilerGenerated]
	private sealed class by
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00009C95 File Offset: 0x00007E95
		internal float mo()
		{
			return this.cbw.intensity;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009CA2 File Offset: 0x00007EA2
		internal void mp(float a)
		{
			this.cbw.intensity = a;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009CA2 File Offset: 0x00007EA2
		internal void fsg(float a)
		{
			this.cbw.intensity = a;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009C95 File Offset: 0x00007E95
		internal float kzq()
		{
			return this.cbw.intensity;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009CA2 File Offset: 0x00007EA2
		internal void kdm(float a)
		{
			this.cbw.intensity = a;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009C95 File Offset: 0x00007E95
		internal float khw()
		{
			return this.cbw.intensity;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009CA2 File Offset: 0x00007EA2
		internal void evo(float a)
		{
			this.cbw.intensity = a;
		}

		// Token: 0x04000210 RID: 528
		public Light cbw;
	}

	// Token: 0x0200005F RID: 95
	[CompilerGenerated]
	private sealed class bz
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00009CB0 File Offset: 0x00007EB0
		internal float mq()
		{
			return this.cbx.intensity;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009CB0 File Offset: 0x00007EB0
		internal float kvl()
		{
			return this.cbx.intensity;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009CBD File Offset: 0x00007EBD
		internal void mr(float a)
		{
			this.cbx.intensity = a;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009CB0 File Offset: 0x00007EB0
		internal float cun()
		{
			return this.cbx.intensity;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009CB0 File Offset: 0x00007EB0
		internal float lm()
		{
			return this.cbx.intensity;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009CBD File Offset: 0x00007EBD
		internal void ins(float a)
		{
			this.cbx.intensity = a;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009CBD File Offset: 0x00007EBD
		internal void nbg(float a)
		{
			this.cbx.intensity = a;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009CB0 File Offset: 0x00007EB0
		internal float hqu()
		{
			return this.cbx.intensity;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009CBD File Offset: 0x00007EBD
		internal void nmm(float a)
		{
			this.cbx.intensity = a;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009CBD File Offset: 0x00007EBD
		internal void nma(float a)
		{
			this.cbx.intensity = a;
		}

		// Token: 0x04000211 RID: 529
		public Light cbx;
	}
}
