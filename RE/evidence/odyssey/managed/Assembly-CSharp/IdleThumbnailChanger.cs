using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006E RID: 110
public class IdleThumbnailChanger : MonoBehaviour
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000254 RID: 596 RVA: 0x0000C114 File Offset: 0x0000A314
	// (remove) Token: 0x06000255 RID: 597 RVA: 0x0000C14C File Offset: 0x0000A34C
	public event Action<int, float> cee;

	// Token: 0x06000256 RID: 598 RVA: 0x0000C181 File Offset: 0x0000A381
	private void Start()
	{
		this.lastColorAlpha = this.rawImage.color.a;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000C19C File Offset: 0x0000A39C
	private void LateUpdate()
	{
		if (this.index == -1 || this.rawImage == null)
		{
			return;
		}
		float a = this.rawImage.color.a;
		if (a == this.lastColorAlpha)
		{
			return;
		}
		if (a == 0f)
		{
			Action<int, float> action = this.cee;
			if (action != null)
			{
				action(this.index, a);
			}
		}
		this.lastColorAlpha = a;
	}

	// Token: 0x04000276 RID: 630
	public RawImage rawImage;

	// Token: 0x04000277 RID: 631
	public int index = -1;

	// Token: 0x04000278 RID: 632
	[SerializeField]
	private float lastColorAlpha;
}
