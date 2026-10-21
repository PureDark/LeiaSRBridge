using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000CA RID: 202
public class UnregisterGamePopup : MonoBehaviour
{
	// Token: 0x0600055A RID: 1370 RVA: 0x00019C01 File Offset: 0x00017E01
	private void Start()
	{
		this.okButton.onClick.AddListener(new UnityAction(this.bdu));
		this.cancelButton.onClick.AddListener(new UnityAction(this.bdv));
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00019C3B File Offset: 0x00017E3B
	private void OnEnable()
	{
		this.bdt(this.okButton);
		this.bdt(this.cancelButton);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00019C55 File Offset: 0x00017E55
	private void bdt(CustomButton a)
	{
		if (a != null)
		{
			a.isHovering = false;
			a.ry(CustomButton.ck.cgt);
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00019C6E File Offset: 0x00017E6E
	private void bdu()
	{
		if (this.game != null)
		{
			bn.cyp.cyq.UnregisterGame(this.game);
			bn.cyp.cyq.SerializeData();
			MainArea.dbb.InstantiateScene();
		}
		PopupManager.ClosePopup();
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00019CAB File Offset: 0x00017EAB
	private void bdv()
	{
		if (this.gameInfoPopup != null)
		{
			PopupManager.OpenPopup(this.gameInfoPopup);
			return;
		}
		PopupManager.ClosePopup();
	}

	// Token: 0x04000500 RID: 1280
	public CustomButton okButton;

	// Token: 0x04000501 RID: 1281
	public CustomButton cancelButton;

	// Token: 0x04000502 RID: 1282
	public GameObject gameInfoPopup;

	// Token: 0x04000503 RID: 1283
	public Game game;
}
