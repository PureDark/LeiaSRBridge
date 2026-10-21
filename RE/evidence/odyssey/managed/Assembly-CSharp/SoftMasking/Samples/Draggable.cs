using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	// Token: 0x02000143 RID: 323
	[RequireComponent(typeof(RectTransform))]
	public class Draggable : UIBehaviour, IDragHandler, IEventSystemHandler
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x00026538 File Offset: 0x00024738
		protected override void Awake()
		{
			base.Awake();
			this.ctm = base.GetComponent<RectTransform>();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002654C File Offset: 0x0002474C
		public void OnDrag(PointerEventData eventData)
		{
			this.ctm.anchoredPosition += eventData.delta;
		}

		// Token: 0x040006E4 RID: 1764
		private RectTransform ctm;
	}
}
