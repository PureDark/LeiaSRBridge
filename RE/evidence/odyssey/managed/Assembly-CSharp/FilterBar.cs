using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200008D RID: 141
public class FilterBar : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x0600035E RID: 862 RVA: 0x00011414 File Offset: 0x0000F614
	// (remove) Token: 0x0600035F RID: 863 RVA: 0x0001144C File Offset: 0x0000F64C
	public event FilterBar.cp cip;

	// Token: 0x06000360 RID: 864 RVA: 0x00011484 File Offset: 0x0000F684
	private void Start()
	{
		if (this.controller != null)
		{
			this.controller.OnItemClickCallback.RemoveAllListeners();
			this.controller.OnItemClickCallback.AddListener(new UnityAction<int>(this.OnButtonClickCallback));
		}
		LanguageHelper.byp -= this.ud;
		LanguageHelper.byp += this.ud;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x000114F0 File Offset: 0x0000F6F0
	public void uc()
	{
		foreach (Image image in this.buttonImages)
		{
			if (image != null)
			{
				image.sprite = this.normalTexture;
			}
		}
		if (this.ciq < 0)
		{
			this.SelectIndex(0);
		}
		else
		{
			this.SelectIndex(this.ciq);
		}
		this.ud();
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00011551 File Offset: 0x0000F751
	private void OnButtonClickCallback(int index)
	{
		if (this.ciq == index)
		{
			return;
		}
		this.SelectIndex(index);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00011568 File Offset: 0x0000F768
	private bool SelectIndex(int index)
	{
		if (this.Filters == null || this.Filters.Length == 0)
		{
			bd.ie(FilterBar.cio, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.us(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ut());
			return false;
		}
		int num = this.Filters.Length;
		if (index >= num)
		{
			bd.ie(FilterBar.cio, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uu(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ut());
			return false;
		}
		if (this.ciq >= 0)
		{
			this.buttonImages[this.ciq].sprite = this.normalTexture;
		}
		this.buttonImages[index].sprite = this.clickedTexture;
		this.ciq = index;
		FilterBar.cp cp = this.cip;
		if (cp != null)
		{
			cp(this.Filters[index]);
		}
		bd.id(FilterBar.cio, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uv(), index, this.Filters[index].name), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ut());
		return true;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00011640 File Offset: 0x0000F840
	private void ud()
	{
		float num = 40f;
		float num2 = 144f;
		float num3 = 0f;
		float num4 = num2;
		float num5 = this.minWidth - 72f;
		for (int i = 0; i < this.textRects.Length; i++)
		{
			float preferredWidth = LayoutUtility.GetPreferredWidth(this.textRects[i]);
			float num6 = (preferredWidth >= num5) ? (preferredWidth + 72f) : this.minWidth;
			RectTransform rectTransform = this.buttonRect[i];
			rectTransform.sizeDelta = new Vector2(num6, rectTransform.sizeDelta.y);
			if (i == 0)
			{
				num4 = num2;
			}
			else
			{
				num4 = num4 + num3 / 2f + num + num6 / 2f;
			}
			rectTransform.anchoredPosition = new Vector2(num4, rectTransform.anchoredPosition.y);
			num3 = num6;
		}
	}

	// Token: 0x04000386 RID: 902
	private static readonly string cio = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uw();

	// Token: 0x04000387 RID: 903
	[SerializeField]
	private BarController controller;

	// Token: 0x04000388 RID: 904
	[SerializeField]
	private Image[] buttonImages;

	// Token: 0x04000389 RID: 905
	[SerializeField]
	private RectTransform[] textRects;

	// Token: 0x0400038A RID: 906
	[SerializeField]
	private RectTransform[] buttonRect;

	// Token: 0x0400038B RID: 907
	[SerializeField]
	private float minWidth = 170f;

	// Token: 0x0400038C RID: 908
	[SerializeField]
	private Sprite normalTexture;

	// Token: 0x0400038D RID: 909
	[SerializeField]
	private Sprite clickedTexture;

	// Token: 0x0400038E RID: 910
	public BaseFilter[] Filters;

	// Token: 0x04000390 RID: 912
	private int ciq = -1;

	// Token: 0x0200008E RID: 142
	// (Invoke) Token: 0x06000368 RID: 872
	public delegate void cp(BaseFilter filter);
}
