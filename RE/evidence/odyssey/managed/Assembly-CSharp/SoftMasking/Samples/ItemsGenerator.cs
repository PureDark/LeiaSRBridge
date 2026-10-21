using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace SoftMasking.Samples
{
	// Token: 0x02000146 RID: 326
	public class ItemsGenerator : MonoBehaviour
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00026658 File Offset: 0x00024858
		public void Generate()
		{
			this.bkm();
			int num = Random.Range(0, ItemsGenerator.cto.Length - 1);
			for (int i = 0; i < this.count; i++)
			{
				Item item = Object.Instantiate<Item>(this.itemPrefab);
				item.transform.SetParent(this.target, false);
				item.bkl(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bju(), this.baseName, i + 1), this.image, ItemsGenerator.cto[(num + i) % ItemsGenerator.cto.Length], Random.Range(0.4f, 1f), Random.Range(0.4f, 1f));
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00026702 File Offset: 0x00024902
		private void bkm()
		{
			while (this.target.childCount > 0)
			{
				Object.DestroyImmediate(this.target.GetChild(0).gameObject);
			}
		}

		// Token: 0x040006EC RID: 1772
		public RectTransform target;

		// Token: 0x040006ED RID: 1773
		public Sprite image;

		// Token: 0x040006EE RID: 1774
		public int count;

		// Token: 0x040006EF RID: 1775
		public string baseName;

		// Token: 0x040006F0 RID: 1776
		public Item itemPrefab;

		// Token: 0x040006F1 RID: 1777
		private static readonly Color[] cto = new Color[]
		{
			Color.red,
			Color.green,
			Color.blue,
			Color.cyan,
			Color.yellow,
			Color.magenta,
			Color.gray
		};
	}
}
