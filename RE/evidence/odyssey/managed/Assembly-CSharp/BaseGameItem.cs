using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000092 RID: 146
[RequireComponent(typeof(RectTransform))]
public class BaseGameItem : MonoBehaviour
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000378 RID: 888 RVA: 0x00011943 File Offset: 0x0000FB43
	public CustomButton dao
	{
		get
		{
			return this.button;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000379 RID: 889 RVA: 0x0001194B File Offset: 0x0000FB4B
	public RectTransform dap
	{
		get
		{
			if (this.cix == null)
			{
				this.cix = base.GetComponent<RectTransform>();
			}
			return this.cix;
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00011970 File Offset: 0x0000FB70
	public virtual void ui(int a, UnityAction<int, PointerEventData> b)
	{
		this.ciu = a;
		CustomButton customButton = this.button;
		if (customButton != null)
		{
			customButton.onClick.RemoveAllListeners();
		}
		CustomButton customButton2 = this.button;
		if (customButton2 != null)
		{
			customButton2.OnEventClick.RemoveAllListeners();
		}
		this.civ = null;
		this.civ = b;
		CustomButton customButton3 = this.button;
		if (customButton3 != null)
		{
			customButton3.onClick.AddListener(new UnityAction(this.uj));
		}
		CustomButton customButton4 = this.button;
		if (customButton4 == null)
		{
			return;
		}
		customButton4.OnEventClick.AddListener(new UnityAction<PointerEventData>(this.uk));
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00011A03 File Offset: 0x0000FC03
	protected virtual void uj()
	{
		UnityAction<int, PointerEventData> unityAction = this.civ;
		if (unityAction == null)
		{
			return;
		}
		unityAction.Invoke(this.ciu, this.ciw);
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00011A21 File Offset: 0x0000FC21
	protected virtual void uk(PointerEventData a)
	{
		this.ciw = a;
		if (this.ciw.button == 1)
		{
			UnityAction<int, PointerEventData> unityAction = this.civ;
			if (unityAction == null)
			{
				return;
			}
			unityAction.Invoke(this.ciu, this.ciw);
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00011A54 File Offset: 0x0000FC54
	protected virtual void OnDestroy()
	{
		CustomButton customButton = this.button;
		if (customButton == null)
		{
			return;
		}
		customButton.onClick.RemoveAllListeners();
	}

	// Token: 0x0400039B RID: 923
	[SerializeField]
	private CustomButton button;

	// Token: 0x0400039C RID: 924
	protected int ciu;

	// Token: 0x0400039D RID: 925
	protected UnityAction<int, PointerEventData> civ;

	// Token: 0x0400039E RID: 926
	protected PointerEventData ciw;

	// Token: 0x0400039F RID: 927
	private RectTransform cix;
}
