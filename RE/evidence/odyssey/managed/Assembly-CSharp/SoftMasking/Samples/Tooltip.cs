using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	// Token: 0x02000150 RID: 336
	public class Tooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x000270D0 File Offset: 0x000252D0
		public void LateUpdate()
		{
			Vector2 vector;
			if (this.tooltip.gameObject.activeInHierarchy && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.tooltip.parent.GetComponent<RectTransform>(), Input.mousePosition, null, ref vector))
			{
				this.tooltip.anchoredPosition = vector + new Vector2(10f, -20f);
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00027133 File Offset: 0x00025333
		void IPointerEnterHandler.bls(PointerEventData a)
		{
			this.tooltip.gameObject.SetActive(true);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00027146 File Offset: 0x00025346
		void IPointerExitHandler.blt(PointerEventData a)
		{
			this.tooltip.gameObject.SetActive(false);
		}

		// Token: 0x0400071E RID: 1822
		public RectTransform tooltip;
	}
}
