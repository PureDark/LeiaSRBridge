using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000064 RID: 100
public class CustomScrollBar : Scrollbar
{
	// Token: 0x06000213 RID: 531 RVA: 0x0000A4A3 File Offset: 0x000086A3
	protected virtual bool ng(PointerEventData a)
	{
		return this.IsActive() && this.IsInteractable() && a.button == 0;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000A4C0 File Offset: 0x000086C0
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!this.ng(eventData))
		{
			return;
		}
		this.ccp = true;
		Vector2 vector;
		if (base.handleRect != null && RectTransformUtility.ScreenPointToLocalPointInRectangle(base.handleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, ref vector))
		{
			this.cco = vector - base.handleRect.rect.center;
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000A52B File Offset: 0x0000872B
	public override void OnDrag(PointerEventData eventData)
	{
		if (!this.ng(eventData))
		{
			return;
		}
		if (!this.ccp)
		{
			return;
		}
		this.nh(eventData);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000A547 File Offset: 0x00008747
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.ccp = false;
		this.cco = Vector2.zero;
		base.OnPointerUp(eventData);
		if (EventSystem.current != null)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000A57C File Offset: 0x0000877C
	protected virtual void nh(PointerEventData a)
	{
		RectTransform rectTransform = this.ni();
		if (rectTransform == null)
		{
			return;
		}
		Vector2 zero = Vector2.zero;
		if (!this.nj(a, ref zero))
		{
			return;
		}
		Vector2 vector;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, zero, a.pressEventCamera, ref vector))
		{
			return;
		}
		Vector2 vector2 = vector - this.cco - rectTransform.rect.position - (base.handleRect.rect.size - base.handleRect.sizeDelta) * 0.5f;
		float num = ((base.direction == null || base.direction == 1) ? rectTransform.rect.width : rectTransform.rect.height) * (1f - base.size);
		if (num <= 0f)
		{
			return;
		}
		switch (base.direction)
		{
		case 0:
			base.value = Mathf.Clamp01(vector2.x / num);
			return;
		case 1:
			base.value = Mathf.Clamp01(1f - vector2.x / num);
			return;
		case 2:
			base.value = Mathf.Clamp01(vector2.y / num);
			return;
		case 3:
			base.value = Mathf.Clamp01(1f - vector2.y / num);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000A6DB File Offset: 0x000088DB
	private RectTransform ni()
	{
		if (base.handleRect != null && base.handleRect.parent != null)
		{
			return base.handleRect.parent.GetComponent<RectTransform>();
		}
		return null;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000A710 File Offset: 0x00008910
	private bool nj(PointerEventData a, ref Vector2 b)
	{
		int displayIndex = a.pointerPressRaycast.displayIndex;
		Vector3 vector = this.nk(a.position);
		if ((int)vector.z != displayIndex)
		{
			return false;
		}
		b = ((displayIndex != 0) ? vector : a.position);
		return true;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000A75C File Offset: 0x0000895C
	private Vector3 nk(Vector2 a)
	{
		if (Display.main.renderingWidth != Display.main.systemWidth || Display.main.renderingHeight != Display.main.systemHeight)
		{
			float num = (float)Display.main.systemWidth / (float)Display.main.systemHeight;
			Vector2 vector;
			vector..ctor((float)Display.main.renderingWidth, (float)Display.main.renderingHeight);
			Vector2 zero = Vector2.zero;
			if (Screen.fullScreen)
			{
				float num2 = (float)Screen.width / (float)Screen.height;
				if ((float)Display.main.systemHeight * num2 < (float)Display.main.systemWidth)
				{
					vector.x = (float)Display.main.renderingHeight * num;
					zero.x = (vector.x - (float)Display.main.renderingWidth) * 0.5f;
				}
				else
				{
					vector.y = (float)Display.main.renderingWidth / num;
					zero.y = (vector.y - (float)Display.main.renderingHeight) * 0.5f;
				}
			}
			Vector2 vector2 = vector - zero;
			if (a.y < -zero.y || a.y > vector2.y || a.x < -zero.x || a.x > vector2.x)
			{
				Vector2 vector3 = a;
				if (!Screen.fullScreen)
				{
					vector3.x -= (float)(Display.main.renderingWidth - Display.main.systemWidth) * 0.5f;
					vector3.y -= (float)(Display.main.renderingHeight - Display.main.systemHeight) * 0.5f;
				}
				else
				{
					vector3 += zero;
					vector3.x *= (float)Display.main.systemWidth / vector.x;
					vector3.y *= (float)Display.main.systemHeight / vector.y;
				}
				Vector3 vector4 = Display.RelativeMouseAt(vector3);
				if (vector4.z != 0f)
				{
					return vector4;
				}
			}
			return new Vector3(a.x, a.y, 0f);
		}
		return Display.RelativeMouseAt(a);
	}

	// Token: 0x0400021E RID: 542
	private Vector2 cco = Vector2.zero;

	// Token: 0x0400021F RID: 543
	private bool ccp;
}
