using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	// Token: 0x0200014C RID: 332
	[RequireComponent(typeof(RectTransform))]
	public class ShowOnHover : UIBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00026F02 File Offset: 0x00025102
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00026F0A File Offset: 0x0002510A
		public bool ddi
		{
			get
			{
				return this.cuh;
			}
			set
			{
				if (this.cuh != value)
				{
					this.cuh = value;
					this.blm();
				}
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00026F22 File Offset: 0x00025122
		protected override void Start()
		{
			base.Start();
			this.blm();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00026F30 File Offset: 0x00025130
		private void blm()
		{
			this.blo(this.bln());
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00026F3E File Offset: 0x0002513E
		private bool bln()
		{
			return this.cuh || this.cui;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00026F50 File Offset: 0x00025150
		private void blo(bool a)
		{
			if (this.targetGroup)
			{
				this.targetGroup.alpha = (a ? 1f : 0f);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00026F79 File Offset: 0x00025179
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.cui = true;
			this.blm();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00026F88 File Offset: 0x00025188
		public void OnPointerExit(PointerEventData eventData)
		{
			this.cui = false;
			this.blm();
		}

		// Token: 0x04000716 RID: 1814
		public CanvasGroup targetGroup;

		// Token: 0x04000717 RID: 1815
		private bool cuh;

		// Token: 0x04000718 RID: 1816
		private bool cui;
	}
}
