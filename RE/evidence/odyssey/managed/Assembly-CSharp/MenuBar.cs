using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200008F RID: 143
public class MenuBar : MonoBehaviour
{
	// Token: 0x0600036B RID: 875 RVA: 0x00011738 File Offset: 0x0000F938
	private void Start()
	{
		this.controller.OnItemClickCallback.RemoveAllListeners();
		this.controller.OnItemClickCallback.AddListener(new UnityAction<int>(this.OnButtonClickCallback));
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00011766 File Offset: 0x0000F966
	private void ue()
	{
		IntroOutroAnimation.cet.StartOutroAnimation();
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00011774 File Offset: 0x0000F974
	private void OnButtonClickCallback(int index)
	{
		bd.ih(MenuBar.cir, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ux());
		if (this.Popups == null || this.Popups.Length == 0)
		{
			bd.ie(MenuBar.cir, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uy(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ux());
			return;
		}
		int num = this.Popups.Length;
		if (index == this.quitIndexPopup)
		{
			this.ue();
			return;
		}
		GameObject gameObject = this.Popups[index];
		if (gameObject == null)
		{
			bd.ie(MenuBar.cir, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uz(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ux());
			return;
		}
		PopupManager.OpenPopup(gameObject);
	}

	// Token: 0x04000391 RID: 913
	private static string cir = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.va();

	// Token: 0x04000392 RID: 914
	[SerializeField]
	private BarController controller;

	// Token: 0x04000393 RID: 915
	public GameObject[] Popups;

	// Token: 0x04000394 RID: 916
	[SerializeField]
	private int quitIndexPopup;
}
