using System;
using System.Collections;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000ED RID: 237
[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x0600064B RID: 1611 RVA: 0x0000238E File Offset: 0x0000058E
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0001C667 File Offset: 0x0001A867
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0001C688 File Offset: 0x0001A888
	private void OnClick()
	{
		string[] array = ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bax(), false);
		if (array.Length != 0)
		{
			base.StartCoroutine(this.bjm(new Uri(array[0]).AbsoluteUri));
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0001C6C9 File Offset: 0x0001A8C9
	private IEnumerator bjm(string a)
	{
		WWW www = new WWW(a);
		yield return www;
		this.output.text = www.text;
		yield break;
	}

	// Token: 0x0400057B RID: 1403
	public Text output;
}
