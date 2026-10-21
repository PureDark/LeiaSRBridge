using System;
using System.Runtime.CompilerServices;
using System.Threading;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B9 RID: 185
public class GameScanPopup : MonoBehaviour
{
	// Token: 0x060004D2 RID: 1234 RVA: 0x00016BF6 File Offset: 0x00014DF6
	private void Start()
	{
		this.cancelButton.onClick.AddListener(new UnityAction(GameScanPopup.<>c.<>9.bbm));
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00016C27 File Offset: 0x00014E27
	private void OnEnable()
	{
		bd.ih(GameScanPopup.cnm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ws());
		this.cancelButton.isEnabled = true;
		this.cno = false;
		this.StartGameScan();
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00016C51 File Offset: 0x00014E51
	private void OnDisable()
	{
		bd.ih(GameScanPopup.cnm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xg());
		this.StopGameScan();
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00016C68 File Offset: 0x00014E68
	private void StartGameScan()
	{
		GameScanPopup.df df;
		df.cne = AsyncVoidMethodBuilder.Create();
		df.cnf = this;
		df.cnd = -1;
		df.cne.Start<GameScanPopup.df>(ref df);
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00016C9F File Offset: 0x00014E9F
	private void StopGameScan()
	{
		bd.ih(GameScanPopup.cnm, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xh());
		CancellationTokenSource cancellationTokenSource = this.cnn;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		CancellationTokenSource cancellationTokenSource2 = this.cnn;
		if (cancellationTokenSource2 != null)
		{
			cancellationTokenSource2.Dispose();
		}
		this.cno = true;
	}

	// Token: 0x0400048D RID: 1165
	private static readonly int cnl = 2000;

	// Token: 0x0400048E RID: 1166
	private static string cnm = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xi();

	// Token: 0x0400048F RID: 1167
	public CustomButton cancelButton;

	// Token: 0x04000490 RID: 1168
	private CancellationTokenSource cnn;

	// Token: 0x04000491 RID: 1169
	private bool cno = true;
}
