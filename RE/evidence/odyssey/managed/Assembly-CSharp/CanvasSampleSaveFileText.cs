using System;
using System.IO;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000F2 RID: 242
[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000666 RID: 1638 RVA: 0x0000238E File Offset: 0x0000058E
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0001C99F File Offset: 0x0001AB9F
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
	public void OnClick()
	{
		string text = ed.bkf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bby(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bax());
		if (!string.IsNullOrEmpty(text))
		{
			File.WriteAllText(text, this.ctc);
		}
	}

	// Token: 0x0400058B RID: 1419
	public Text output;

	// Token: 0x0400058C RID: 1420
	private string ctc = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbz();
}
