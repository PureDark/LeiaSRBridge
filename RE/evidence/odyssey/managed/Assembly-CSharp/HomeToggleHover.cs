using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000085 RID: 133
public class HomeToggleHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600031D RID: 797 RVA: 0x000108F9 File Offset: 0x0000EAF9
	private void Start()
	{
		if (this.canvasGroup != null)
		{
			this.canvasGroup.alpha = 0f;
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00010919 File Offset: 0x0000EB19
	public void OnPointerEnter(PointerEventData eventData)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.sr(this.targetAlpha));
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00010934 File Offset: 0x0000EB34
	public void OnPointerExit(PointerEventData eventData)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.sr(0f));
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0001094E File Offset: 0x0000EB4E
	private IEnumerator sr(float a)
	{
		if (this.canvasGroup != null)
		{
			float alpha = this.canvasGroup.alpha;
			float num = 0f;
			while (num < this.fadeDuration)
			{
				num += Time.deltaTime;
				this.canvasGroup.alpha = Mathf.Lerp(alpha, a, num / this.fadeDuration);
				yield return null;
			}
			this.canvasGroup.alpha = a;
		}
		yield break;
	}

	// Token: 0x04000350 RID: 848
	public Image SelectedImage;

	// Token: 0x04000351 RID: 849
	public CanvasGroup canvasGroup;

	// Token: 0x04000352 RID: 850
	public float fadeDuration = 0.3f;

	// Token: 0x04000353 RID: 851
	public float targetAlpha = 1f;
}
