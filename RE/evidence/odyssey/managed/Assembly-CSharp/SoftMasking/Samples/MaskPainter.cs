using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	// Token: 0x02000147 RID: 327
	[RequireComponent(typeof(RectTransform))]
	public class MaskPainter : UIBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00026798 File Offset: 0x00024998
		protected override void Awake()
		{
			base.Awake();
			this.ctp = base.GetComponent<RectTransform>();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000267AC File Offset: 0x000249AC
		protected override void Start()
		{
			base.Start();
			this.spot.enabled = false;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000267C0 File Offset: 0x000249C0
		public void OnPointerDown(PointerEventData eventData)
		{
			this.bkn(eventData, false);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000267CA File Offset: 0x000249CA
		public void OnDrag(PointerEventData eventData)
		{
			this.bkn(eventData, true);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000267D4 File Offset: 0x000249D4
		private void bkn(PointerEventData a, bool b = false)
		{
			this.bko(a.position, b);
			this.bkq(a.button);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000267F0 File Offset: 0x000249F0
		private void bko(Vector2 a, bool b = false)
		{
			Vector2 anchoredPosition;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.ctp, a, null, ref anchoredPosition))
			{
				Vector2 anchoredPosition2 = this.stroke.anchoredPosition;
				this.stroke.anchoredPosition = anchoredPosition;
				if (b)
				{
					this.bkp(anchoredPosition2);
				}
				this.spot.enabled = true;
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002683C File Offset: 0x00024A3C
		private void bkp(Vector2 a)
		{
			Vector2 vector = this.stroke.anchoredPosition - a;
			this.stroke.localRotation = Quaternion.AngleAxis(Mathf.Atan2(vector.y, vector.x) * 57.29578f, Vector3.forward);
			this.spot.rectTransform.offsetMin = new Vector2(-vector.magnitude, 0f);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000268A9 File Offset: 0x00024AA9
		private void bkq(PointerEventData.InputButton a)
		{
			this.spot.materialForRendering.SetInt(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjv(), (a == null) ? 0 : 2);
		}

		// Token: 0x040006F2 RID: 1778
		public Graphic spot;

		// Token: 0x040006F3 RID: 1779
		public RectTransform stroke;

		// Token: 0x040006F4 RID: 1780
		private RectTransform ctp;
	}
}
