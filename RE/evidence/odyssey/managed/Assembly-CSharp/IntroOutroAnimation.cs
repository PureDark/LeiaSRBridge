using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using Odyssey3DHubUI.Helper;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class IntroOutroAnimation : MonoBehaviour
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000259 RID: 601 RVA: 0x0000C212 File Offset: 0x0000A412
	// (set) Token: 0x0600025A RID: 602 RVA: 0x0000C219 File Offset: 0x0000A419
	public static IntroOutroAnimation cet { get; private set; }

	// Token: 0x0600025B RID: 603 RVA: 0x0000C224 File Offset: 0x0000A424
	private void Start()
	{
		bd.ih(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		IntroOutroAnimation.cet = this;
		this.ceo = SynchronizationContext.Current;
		this.cep = base.GetComponent<Animator>();
		if (Object.FindAnyObjectByType<BackgroundBlur>() != null)
		{
			this.ces = Object.FindAnyObjectByType<BackgroundBlur>();
		}
		IntroOutroAnimation.needIntroAnimation = true;
		this.StartIntroAnimation();
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000C284 File Offset: 0x0000A484
	private void Update()
	{
		if (Input.GetKeyDown(27))
		{
			bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sw());
			if (!IntroOutroAnimation.isPlayingOutro && !PopupManager.bvl && !SearchBar.cln && ActiveIdleTransition.isActive)
			{
				bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tk(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sw());
				this.StartOutroAnimation();
			}
		}
		if (IntroOutroAnimation.isPlayingIntro)
		{
			AnimatorStateInfo currentAnimatorStateInfo = this.cep.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.IsName(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tb()) && currentAnimatorStateInfo.normalizedTime >= 1f)
			{
				this.OnIntroAnimationComplete();
			}
		}
		if (IntroOutroAnimation.isPlayingOutro)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = this.cep.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo2.IsName(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.th()) && currentAnimatorStateInfo2.normalizedTime >= 1f)
			{
				this.OnOutroAnimationComplete();
			}
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000C350 File Offset: 0x0000A550
	public void StartIntroAnimation()
	{
		bd.ih(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tl());
		App.stopwatch.Restart();
		if (!IntroOutroAnimation.isPlayingIntro && !IntroOutroAnimation.isPlayingOutro)
		{
			IntroOutroAnimation.isPlayingIntro = true;
			CustomButton.clickBlocked = true;
			this.ceo.Post(new SendOrPostCallback(this.pa), null);
			this.ceo.Post(new SendOrPostCallback(this.pb), null);
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
	public void StartOutroAnimation()
	{
		bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tm(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tn());
		if (!IntroOutroAnimation.isPlayingIntro && !IntroOutroAnimation.isPlayingOutro)
		{
			bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.to(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tn());
			IntroOutroAnimation.isPlayingOutro = true;
			CustomButton.clickBlocked = true;
			this.ceo.Post(new SendOrPostCallback(this.pc), null);
			this.ceo.Post(new SendOrPostCallback(this.pd), null);
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000C43F File Offset: 0x0000A63F
	private IEnumerator IntroAnimation(Action onComplete = null)
	{
		bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sz(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ta());
		App.MoveToPrimaryMonitor();
		Application.runInBackground = true;
		SharedMemoryManager.SendData(SharedMemoryManager.j.bun, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		if (IntroOutroAnimation.needIntroAnimation)
		{
			IntroOutroAnimation.ce ce = new IntroOutroAnimation.ce();
			this.oz();
			this.cep.enabled = true;
			this.cep.Play(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tb());
			this.oz();
			bool reScan = bn.cyp.cyr.cyz == 1;
			ce.cef = GameScanHelper.ValidateGamesPath(bn.cyp.cyq.Games, false, reScan, default(CancellationToken));
			yield return new WaitUntil(new Func<bool>(ce.oo));
			bn.cyp.cyq.SerializeData();
			MainArea.dbb.InstantiateScene();
		}
		else
		{
			MainArea.dbb.InstantiateScene();
			CustomButton.clickBlocked = false;
		}
		if (onComplete != null)
		{
			onComplete();
		}
		yield return new WaitForSeconds(this.cer);
		object obj = bn.cyp.cys && RegistryHelper.GetInstallStatus() == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tc();
		bool flag = PatchNoteHelper.@in();
		object obj2 = obj;
		bool flag2 = (obj2 & flag) != null;
		string title = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		if ((obj2 | flag) != null)
		{
			bn.cyp.cys = false;
			if (flag && !flag2)
			{
				title = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.td();
				text = PatchNoteHelper.im();
			}
			else
			{
				title = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.te() + Application.version;
				text = PatchNoteHelper.il(flag2);
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				this.patchNotePopup.GetComponent<PatchNotePopup>().title = title;
				this.patchNotePopup.GetComponent<PatchNotePopup>().patchNote = text;
				PopupManager.OpenPopup(this.patchNotePopup);
			}
		}
		CustomButton.clickBlocked = false;
		yield break;
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000C455 File Offset: 0x0000A655
	private IEnumerator OutroAnimation()
	{
		bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tf(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tg());
		this.cep.enabled = true;
		this.cep.Play(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.th());
		base.Invoke(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ti(), 3.5f);
		yield return null;
		yield break;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000C464 File Offset: 0x0000A664
	private void OnIntroAnimationComplete()
	{
		bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tp(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tq());
		this.cep.enabled = false;
		IntroOutroAnimation.needIntroAnimation = false;
		IntroOutroAnimation.isPlayingIntro = false;
		bl.cym = false;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000C498 File Offset: 0x0000A698
	private void OnOutroAnimationComplete()
	{
		bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tr(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ts());
		this.cep.enabled = false;
		this.oz();
		IntroOutroAnimation.isPlayingOutro = false;
		CustomButton.clickBlocked = false;
		IntroOutroAnimation.needIntroAnimation = true;
		bl.jc();
		bd.id(IntroOutroAnimation.cen, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tt(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ts());
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
	private void FadeInDim()
	{
		this.ces.fullScreenBlurMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tu(), 0f);
		ShortcutExtensions.DOFloat(this.ces.fullScreenBlurMaterial, 1f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tu(), 0.3f);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000C532 File Offset: 0x0000A732
	private void oz()
	{
		this.ces.fullScreenBlurMaterial.SetFloat(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tu(), 1f);
		ShortcutExtensions.DOFloat(this.ces.fullScreenBlurMaterial, 0f, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tu(), 0.3f);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000C59F File Offset: 0x0000A79F
	[CompilerGenerated]
	private void pa(object a)
	{
		base.StopCoroutine(this.OutroAnimation());
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000C5AD File Offset: 0x0000A7AD
	[CompilerGenerated]
	private void pb(object a)
	{
		base.StartCoroutine(this.IntroAnimation(null));
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000C5BD File Offset: 0x0000A7BD
	[CompilerGenerated]
	private void pc(object a)
	{
		base.StopCoroutine(this.IntroAnimation(null));
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000C5CC File Offset: 0x0000A7CC
	[CompilerGenerated]
	private void pd(object a)
	{
		base.StartCoroutine(this.OutroAnimation());
	}

	// Token: 0x0400027A RID: 634
	private static string cen = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.tv();

	// Token: 0x0400027B RID: 635
	private SynchronizationContext ceo;

	// Token: 0x0400027C RID: 636
	private Animator cep;

	// Token: 0x0400027D RID: 637
	private IntPtr ceq;

	// Token: 0x0400027E RID: 638
	public static bool isPlayingIntro = false;

	// Token: 0x0400027F RID: 639
	public static bool isPlayingOutro = false;

	// Token: 0x04000280 RID: 640
	public static bool needIntroAnimation = true;

	// Token: 0x04000281 RID: 641
	public GameObject patchNotePopup;

	// Token: 0x04000282 RID: 642
	private float cer = 4.4f;

	// Token: 0x04000283 RID: 643
	private BackgroundBlur ces;

	// Token: 0x02000070 RID: 112
	[CompilerGenerated]
	private sealed class ce
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		internal bool oo()
		{
			return this.cef.IsCompleted;
		}

		// Token: 0x04000285 RID: 645
		public Task<bool> cef;
	}
}
