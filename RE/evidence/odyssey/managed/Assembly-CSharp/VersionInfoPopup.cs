using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000CB RID: 203
public class VersionInfoPopup : MonoBehaviour
{
	// Token: 0x06000560 RID: 1376 RVA: 0x00019CCC File Offset: 0x00017ECC
	private void Start()
	{
		this.bdx();
		this.closeButton.onClick.AddListener(new UnityAction(this.bdw));
		this.BGBackButton.onClick.AddListener(new UnityAction(this.bdw));
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00016D0A File Offset: 0x00014F0A
	private void bdw()
	{
		PopupManager.ClosePopup();
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00019D0C File Offset: 0x00017F0C
	public void bdx()
	{
		float num = this.descriptionText.preferredHeight - this.cpm;
		float num2 = this.blendShapeWeight1 - num / this.cpm * 2f;
		if (this.descriptionRecttransform != null)
		{
			this.descriptionRecttransform.sizeDelta = new Vector2(this.descriptionRecttransform.sizeDelta.x, this.descriptionText.preferredHeight);
		}
		float num3 = (this.swVersionText.preferredWidth <= this.cpn) ? 0f : (this.swVersionText.preferredWidth - this.cpn);
		float num4 = this.blendShapeWeight0 - num3 / this.cpn * 8f;
		this.skinnedMeshRenderer.SetBlendShapeWeight(0, num4);
		this.skinnedMeshRenderer.SetBlendShapeWeight(1, num2);
	}

	// Token: 0x04000504 RID: 1284
	public CustomButton closeButton;

	// Token: 0x04000505 RID: 1285
	public SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x04000506 RID: 1286
	public Text descriptionText;

	// Token: 0x04000507 RID: 1287
	public Text swVersionText;

	// Token: 0x04000508 RID: 1288
	public RectTransform descriptionRecttransform;

	// Token: 0x04000509 RID: 1289
	public Button BGBackButton;

	// Token: 0x0400050A RID: 1290
	private float cpm = 35f;

	// Token: 0x0400050B RID: 1291
	private float cpn = 413f;

	// Token: 0x0400050C RID: 1292
	public float blendShapeWeight0 = 87f;

	// Token: 0x0400050D RID: 1293
	public float blendShapeWeight1 = 71f;
}
