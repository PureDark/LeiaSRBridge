using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	// Token: 0x02000145 RID: 325
	public class Item : MonoBehaviour
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x000265A0 File Offset: 0x000247A0
		public void bkl(string a, Sprite b, Color c, float d, float e)
		{
			if (this.image)
			{
				this.image.sprite = b;
				this.image.color = c;
			}
			if (this.title)
			{
				this.title.text = a;
			}
			if (this.description)
			{
				this.description.text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjt() + a;
			}
			if (this.healthBar)
			{
				this.healthBar.anchorMax = new Vector2(d, 1f);
			}
			if (this.damageBar)
			{
				this.damageBar.anchorMax = new Vector2(e, 1f);
			}
		}

		// Token: 0x040006E7 RID: 1767
		public Image image;

		// Token: 0x040006E8 RID: 1768
		public Text title;

		// Token: 0x040006E9 RID: 1769
		public Text description;

		// Token: 0x040006EA RID: 1770
		public RectTransform healthBar;

		// Token: 0x040006EB RID: 1771
		public RectTransform damageBar;
	}
}
