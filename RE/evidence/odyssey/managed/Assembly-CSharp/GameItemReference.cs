using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008C RID: 140
public class GameItemReference : MonoBehaviour
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000353 RID: 851 RVA: 0x000113C2 File Offset: 0x0000F5C2
	public RectTransform dae
	{
		get
		{
			return this.m_RectTransform;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000354 RID: 852 RVA: 0x000113CA File Offset: 0x0000F5CA
	public Image daf
	{
		get
		{
			return this.m_background;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000355 RID: 853 RVA: 0x000113D2 File Offset: 0x0000F5D2
	public Image dag
	{
		get
		{
			return this.m_character;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000356 RID: 854 RVA: 0x000113DA File Offset: 0x0000F5DA
	public Image dah
	{
		get
		{
			return this.m_logo;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000357 RID: 855 RVA: 0x000113E2 File Offset: 0x0000F5E2
	public Image dai
	{
		get
		{
			return this.m_tier;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000358 RID: 856 RVA: 0x000113EA File Offset: 0x0000F5EA
	public Image daj
	{
		get
		{
			return this.m_newBadge;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000359 RID: 857 RVA: 0x000113F2 File Offset: 0x0000F5F2
	public Image dak
	{
		get
		{
			return this.m_favorite;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600035A RID: 858 RVA: 0x000113FA File Offset: 0x0000F5FA
	public Image dal
	{
		get
		{
			return this.m_status;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600035B RID: 859 RVA: 0x00011402 File Offset: 0x0000F602
	public CustomButton dam
	{
		get
		{
			return this.m_customButton;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600035C RID: 860 RVA: 0x0001140A File Offset: 0x0000F60A
	public GameItem dan
	{
		get
		{
			return this.m_gameItem;
		}
	}

	// Token: 0x0400037C RID: 892
	[Header("Reference Properties")]
	[SerializeField]
	private RectTransform m_RectTransform;

	// Token: 0x0400037D RID: 893
	[SerializeField]
	private Image m_background;

	// Token: 0x0400037E RID: 894
	[SerializeField]
	private Image m_character;

	// Token: 0x0400037F RID: 895
	[SerializeField]
	private Image m_logo;

	// Token: 0x04000380 RID: 896
	[SerializeField]
	private Image m_tier;

	// Token: 0x04000381 RID: 897
	[SerializeField]
	private Image m_newBadge;

	// Token: 0x04000382 RID: 898
	[SerializeField]
	private Image m_favorite;

	// Token: 0x04000383 RID: 899
	[SerializeField]
	private Image m_status;

	// Token: 0x04000384 RID: 900
	[Header("Reference Script")]
	[SerializeField]
	private CustomButton m_customButton;

	// Token: 0x04000385 RID: 901
	[SerializeField]
	private GameItem m_gameItem;
}
