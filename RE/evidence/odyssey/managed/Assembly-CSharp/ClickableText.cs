using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000063 RID: 99
public class ClickableText : MonoBehaviour
{
	// Token: 0x06000210 RID: 528 RVA: 0x0000A430 File Offset: 0x00008630
	private void OnEnable()
	{
		if (this.ccn == null)
		{
			this.ccn = base.GetComponentInParent<BaseCustomButton>();
		}
		this.ccn.OnEventClick.RemoveAllListeners();
		this.ccn.OnEventClick.AddListener(new UnityAction<PointerEventData>(this.nf));
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000A483 File Offset: 0x00008683
	public void nf(PointerEventData a)
	{
		Application.OpenURL(this.URL);
	}

	// Token: 0x0400021C RID: 540
	private BaseCustomButton ccn;

	// Token: 0x0400021D RID: 541
	public string URL = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
}
