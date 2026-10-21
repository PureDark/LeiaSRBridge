using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	// Token: 0x0200014A RID: 330
	[RequireComponent(typeof(RectTransform))]
	public class RectManipulator : UIBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x00026A36 File Offset: 0x00024C36
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.bkv(true, false);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00026A40 File Offset: 0x00024C40
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.cud)
			{
				this.bkv(false, false);
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00026A54 File Offset: 0x00024C54
		private void bkv(bool a, bool b = false)
		{
			if (this.icon)
			{
				float num = a ? this.selectedAlpha : this.normalAlpha;
				float num2 = b ? 0f : this.transitionDuration;
				this.icon.CrossFadeAlpha(num, num2, true);
			}
			if (this.showOnHover)
			{
				this.showOnHover.ddi = a;
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00026AB8 File Offset: 0x00024CB8
		protected override void Start()
		{
			base.Start();
			this.bkv(false, true);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00026AC8 File Offset: 0x00024CC8
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.cud = true;
			this.bkw();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00026AD8 File Offset: 0x00024CD8
		private void bkw()
		{
			if (this.targetTransform)
			{
				this.cue = this.targetTransform.anchoredPosition;
				this.cuf = this.targetTransform.sizeDelta;
				this.cug = this.targetTransform.localRotation.eulerAngles.z;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00026B34 File Offset: 0x00024D34
		public void OnDrag(PointerEventData eventData)
		{
			if (this.targetTransform == null || this.ddh == null || !this.cud)
			{
				return;
			}
			Vector2 vector = this.bkx(eventData.pressPosition, eventData.pressEventCamera);
			Vector2 vector2 = this.bkx(eventData.position, eventData.pressEventCamera);
			this.bld(vector, vector2);
			Vector2 a = vector2 - vector;
			this.bkz(a);
			this.blf(a);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00026BAC File Offset: 0x00024DAC
		private Vector2 bkx(Vector2 a, Camera b)
		{
			Vector2 result;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.ddh, a, b, ref result);
			return result;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00026BCA File Offset: 0x00024DCA
		private RectTransform ddh
		{
			get
			{
				return this.targetTransform.parent as RectTransform;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00026BDC File Offset: 0x00024DDC
		private void bkz(Vector2 a)
		{
			if (this.bla(RectManipulator.eg.ctt))
			{
				this.blb(this.cue + a);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00026BF9 File Offset: 0x00024DF9
		private bool bla(RectManipulator.eg a)
		{
			return (this.manipulation & a) == a;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00026C06 File Offset: 0x00024E06
		private void blb(Vector2 a)
		{
			this.targetTransform.anchoredPosition = this.blc(a);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00026C1C File Offset: 0x00024E1C
		private Vector2 blc(Vector2 a)
		{
			Vector2 vector = this.ddh.rect.size / 2f;
			return new Vector2(Mathf.Clamp(a.x, -vector.x, vector.x), Mathf.Clamp(a.y, -vector.y, vector.y));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00026C7C File Offset: 0x00024E7C
		private void bld(Vector2 a, Vector2 b)
		{
			if (this.bla(RectManipulator.eg.cuc))
			{
				Vector2 a2 = a - this.targetTransform.localPosition;
				Vector2 b2 = b - this.targetTransform.localPosition;
				float num = this.ble(a2, b2);
				this.targetTransform.localRotation = Quaternion.AngleAxis(this.cug + num, Vector3.forward);
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00026CE8 File Offset: 0x00024EE8
		private float ble(Vector2 a, Vector2 b)
		{
			float num = Mathf.Atan2(a.y, a.x) * 57.29578f;
			float num2 = Mathf.Atan2(b.y, b.x) * 57.29578f;
			return Mathf.DeltaAngle(num, num2);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00026D2C File Offset: 0x00024F2C
		private void blf(Vector2 a)
		{
			Vector3 vector = Quaternion.Inverse(this.targetTransform.rotation) * a;
			Vector2 a2 = this.blg(vector);
			if (a2.sqrMagnitude > 0f)
			{
				this.bli(a2, this.blh());
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00026D80 File Offset: 0x00024F80
		private Vector2 blg(Vector2 a)
		{
			bool flag = this.bla(RectManipulator.eg.ctu) || this.bla(RectManipulator.eg.ctw);
			bool flag2 = this.bla(RectManipulator.eg.ctv) || this.bla(RectManipulator.eg.ctx);
			return new Vector2(flag ? a.x : 0f, flag2 ? a.y : 0f);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00026DD9 File Offset: 0x00024FD9
		private Vector2 blh()
		{
			return new Vector2(this.bla(RectManipulator.eg.ctu) ? -1f : 1f, this.bla(RectManipulator.eg.ctx) ? -1f : 1f);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00026E0C File Offset: 0x0002500C
		private void bli(Vector2 a, Vector2 b)
		{
			Vector2 vector = this.blj(this.cuf + Vector2.Scale(a, b));
			this.targetTransform.sizeDelta = vector;
			Vector2 vector2 = Vector2.Scale((vector - this.cuf) / 2f, b);
			this.blb(this.cue + this.targetTransform.TransformVector(vector2));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00026E82 File Offset: 0x00025082
		private Vector2 blj(Vector2 a)
		{
			return new Vector2(Mathf.Max(a.x, this.minSize.x), Mathf.Max(a.y, this.minSize.y));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00026EB5 File Offset: 0x000250B5
		public void OnEndDrag(PointerEventData eventData)
		{
			this.cud = false;
			if (!eventData.hovered.Contains(base.gameObject))
			{
				this.bkv(false, false);
			}
		}

		// Token: 0x040006FE RID: 1790
		public RectTransform targetTransform;

		// Token: 0x040006FF RID: 1791
		public RectManipulator.eg manipulation;

		// Token: 0x04000700 RID: 1792
		public ShowOnHover showOnHover;

		// Token: 0x04000701 RID: 1793
		[Header("Limits")]
		public Vector2 minSize;

		// Token: 0x04000702 RID: 1794
		[Header("Display")]
		public Graphic icon;

		// Token: 0x04000703 RID: 1795
		public float normalAlpha = 0.2f;

		// Token: 0x04000704 RID: 1796
		public float selectedAlpha = 1f;

		// Token: 0x04000705 RID: 1797
		public float transitionDuration = 0.2f;

		// Token: 0x04000706 RID: 1798
		private bool cud;

		// Token: 0x04000707 RID: 1799
		private Vector2 cue;

		// Token: 0x04000708 RID: 1800
		private Vector2 cuf;

		// Token: 0x04000709 RID: 1801
		private float cug;

		// Token: 0x0200014B RID: 331
		[Flags]
		public enum eg
		{
			// Token: 0x0400070B RID: 1803
			cts = 0,
			// Token: 0x0400070C RID: 1804
			ctt = 1,
			// Token: 0x0400070D RID: 1805
			ctu = 2,
			// Token: 0x0400070E RID: 1806
			ctv = 4,
			// Token: 0x0400070F RID: 1807
			ctw = 8,
			// Token: 0x04000710 RID: 1808
			ctx = 16,
			// Token: 0x04000711 RID: 1809
			cty = 6,
			// Token: 0x04000712 RID: 1810
			ctz = 12,
			// Token: 0x04000713 RID: 1811
			cua = 18,
			// Token: 0x04000714 RID: 1812
			cub = 24,
			// Token: 0x04000715 RID: 1813
			cuc = 32
		}
	}
}
