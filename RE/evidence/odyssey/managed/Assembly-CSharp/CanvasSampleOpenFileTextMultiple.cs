using System;
using System.Collections;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000EF RID: 239
[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileTextMultiple : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000656 RID: 1622 RVA: 0x0000238E File Offset: 0x0000058E
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0001C767 File Offset: 0x0001A967
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0001C788 File Offset: 0x0001A988
	private void OnClick()
	{
		string[] array = ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), true);
		if (array.Length != 0)
		{
			List<string> list = new List<string>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(new Uri(array[i]).AbsoluteUri);
			}
			base.StartCoroutine(this.bjr(list.ToArray()));
		}
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0001C7EC File Offset: 0x0001A9EC
	private IEnumerator bjr(string[] a)
	{
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		int num;
		for (int i = 0; i < a.Length; i = num + 1)
		{
			WWW www = new WWW(a[i]);
			yield return www;
			text += www.text;
			www = null;
			num = i;
		}
		this.output.text = text;
		yield break;
	}

	// Token: 0x04000581 RID: 1409
	public Text output;
}
