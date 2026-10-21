using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000B7 RID: 183
public class CustomSelectable : Dropdown
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000163F4 File Offset: 0x000145F4
	// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000163FC File Offset: 0x000145FC
	public bool cmx { get; private set; }

	// Token: 0x060004BA RID: 1210 RVA: 0x00016405 File Offset: 0x00014605
	public void bay()
	{
		this.cmx = false;
		this.OnSelectionChanged = null;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00016415 File Offset: 0x00014615
	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		if (eventData.selectedObject != base.gameObject)
		{
			return;
		}
		if (base.currentSelectionState == 3)
		{
			Action<bool> onSelectionChanged = this.OnSelectionChanged;
			if (onSelectionChanged == null)
			{
				return;
			}
			onSelectionChanged(false);
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0001644C File Offset: 0x0001464C
	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		if (eventData.selectedObject != base.gameObject)
		{
			return;
		}
		if (base.currentSelectionState != null)
		{
			if (base.currentSelectionState == 1)
			{
				Action<bool> onSelectionChanged = this.OnSelectionChanged;
				if (onSelectionChanged == null)
				{
					return;
				}
				onSelectionChanged(true);
			}
			return;
		}
		Action<bool> onSelectionChanged2 = this.OnSelectionChanged;
		if (onSelectionChanged2 == null)
		{
			return;
		}
		onSelectionChanged2(false);
	}

	// Token: 0x04000471 RID: 1137
	public Action<bool> OnSelectionChanged;
}
