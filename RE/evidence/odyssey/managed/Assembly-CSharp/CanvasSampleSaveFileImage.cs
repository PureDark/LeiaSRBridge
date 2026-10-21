using System;
using System.IO;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000F1 RID: 241
[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileImage : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000661 RID: 1633 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
	private void Awake()
	{
		int num = 100;
		int num2 = 100;
		Texture2D texture2D = new Texture2D(num, num2, 3, false);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				texture2D.SetPixel(i, j, Color.red);
			}
		}
		texture2D.Apply();
		this.ctb = ImageConversion.EncodeToPNG(texture2D);
		Object.Destroy(texture2D);
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0000238E File Offset: 0x0000058E
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0001C943 File Offset: 0x0001AB43
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0001C964 File Offset: 0x0001AB64
	public void OnClick()
	{
		string text = ed.bkf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bby(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbb());
		if (!string.IsNullOrEmpty(text))
		{
			File.WriteAllBytes(text, this.ctb);
		}
	}

	// Token: 0x04000589 RID: 1417
	public Text output;

	// Token: 0x0400058A RID: 1418
	private byte[] ctb;
}
