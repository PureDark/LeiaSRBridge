using System;
using System.Collections;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000EB RID: 235
[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000640 RID: 1600 RVA: 0x0000238E File Offset: 0x0000058E
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0001C567 File Offset: 0x0001A767
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0001C588 File Offset: 0x0001A788
	private void OnClick()
	{
		string[] array = ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.go(), false);
		if (array.Length != 0)
		{
			base.StartCoroutine(this.bjh(new Uri(array[0]).AbsoluteUri));
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0001C5C9 File Offset: 0x0001A7C9
	private IEnumerator bjh(string a)
	{
		WWW www = new WWW(a);
		yield return www;
		this.output.texture = www.texture;
		yield break;
	}

	// Token: 0x04000575 RID: 1397
	public RawImage output;
}
