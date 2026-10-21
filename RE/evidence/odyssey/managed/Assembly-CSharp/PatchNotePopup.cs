using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000BF RID: 191
public class PatchNotePopup : MonoBehaviour
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x00017643 File Offset: 0x00015843
	private void Start()
	{
		this.okButton.onClick.AddListener(new UnityAction(this.bbt));
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00017661 File Offset: 0x00015861
	private void OnEnable()
	{
		if (!this.coa)
		{
			return;
		}
		this.titleText.text = this.title;
		this.patchNoteText.text = this.patchNote;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0001768E File Offset: 0x0001588E
	private void OnDisable()
	{
		if (this.coa)
		{
			PatchNoteHelper.CleanUpPatchNoteJSON();
		}
		this.coa = true;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00016D0A File Offset: 0x00014F0A
	private void bbt()
	{
		PopupManager.ClosePopup();
	}

	// Token: 0x040004B5 RID: 1205
	public CustomButton okButton;

	// Token: 0x040004B6 RID: 1206
	public Text titleText;

	// Token: 0x040004B7 RID: 1207
	public Text patchNoteText;

	// Token: 0x040004B8 RID: 1208
	public string title;

	// Token: 0x040004B9 RID: 1209
	public string patchNote;

	// Token: 0x040004BA RID: 1210
	private bool coa;
}
