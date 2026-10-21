using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000087 RID: 135
public class ScrollButtonGroup : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000328 RID: 808 RVA: 0x00010A6B File Offset: 0x0000EC6B
	// (set) Token: 0x06000329 RID: 809 RVA: 0x00010A73 File Offset: 0x0000EC73
	public bool dad
	{
		get
		{
			return this.chx;
		}
		set
		{
			if (this.chx == value)
			{
				return;
			}
			this.chx = value;
			UnityAction<bool> onFocusChangeCallback = this.OnFocusChangeCallback;
			if (onFocusChangeCallback == null)
			{
				return;
			}
			onFocusChangeCallback.Invoke(this.chx);
		}
	}

	// Token: 0x0600032A RID: 810 RVA: 0x00010A9C File Offset: 0x0000EC9C
	private void Start()
	{
		this.chx = false;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00010AA5 File Offset: 0x0000ECA5
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.su();
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00010AAD File Offset: 0x0000ECAD
	private void su()
	{
		this.dad = true;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00010AB6 File Offset: 0x0000ECB6
	public void OnPointerExit(PointerEventData eventData)
	{
		this.sv();
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00010ABE File Offset: 0x0000ECBE
	private void sv()
	{
		this.dad = false;
	}

	// Token: 0x0400035A RID: 858
	public UnityAction<bool> OnFocusChangeCallback;

	// Token: 0x0400035B RID: 859
	private bool chx;
}
