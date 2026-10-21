using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
public class PopupManager : MonoBehaviour
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000042 RID: 66 RVA: 0x00003C7C File Offset: 0x00001E7C
	// (set) Token: 0x06000043 RID: 67 RVA: 0x00003C83 File Offset: 0x00001E83
	public static bool bvl { get; set; }

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000044 RID: 68 RVA: 0x00003C8B File Offset: 0x00001E8B
	// (set) Token: 0x06000045 RID: 69 RVA: 0x00003C92 File Offset: 0x00001E92
	public static GameObject bvm { get; set; }

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000046 RID: 70 RVA: 0x00003C9A File Offset: 0x00001E9A
	// (set) Token: 0x06000047 RID: 71 RVA: 0x00003CA1 File Offset: 0x00001EA1
	public static GameObject bvn { get; set; }

	// Token: 0x06000048 RID: 72 RVA: 0x00003CA9 File Offset: 0x00001EA9
	private void Start()
	{
		PopupManager.bvn = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ga());
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003CBC File Offset: 0x00001EBC
	public static void OpenPopup(GameObject popup)
	{
		PopupManager.l l;
		l.bvg = AsyncVoidMethodBuilder.Create();
		l.bvh = popup;
		l.bvf = -1;
		l.bvg.Start<PopupManager.l>(ref l);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003CF4 File Offset: 0x00001EF4
	public static void ClosePopup()
	{
		bd.ih(PopupManager.bvk, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gb());
		if (PopupManager.bvm != null)
		{
			Popup component = PopupManager.bvm.GetComponent<Popup>();
			if (component != null)
			{
				component.bby();
			}
			PopupManager.bvm = null;
		}
		PopupManager.bvl = false;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003D44 File Offset: 0x00001F44
	public static GameObject CreateTwoButtonPopup(string popupMessage, string firstButtonText, string secondButtonText, UnityAction onFirstButtonClicked, UnityAction onSecondButtonClicked)
	{
		bd.ih(PopupManager.bvk, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gc());
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gd()), PopupManager.bvn.transform);
		if (gameObject != null)
		{
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge()).GetComponent<Text>().text = popupMessage;
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gf()).transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge()).GetComponent<Text>().text = firstButtonText;
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gf()).GetComponent<CustomButton>().onClick.AddListener(onFirstButtonClicked);
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gg()).transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge()).GetComponent<Text>().text = secondButtonText;
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gg()).GetComponent<CustomButton>().onClick.AddListener(onSecondButtonClicked);
			gameObject.transform.GetComponent<Popup>().destroyAfterClose = true;
		}
		return gameObject;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003E50 File Offset: 0x00002050
	public static GameObject CreateOneButtonPopup(string popupMessage, string buttonText, UnityAction onFirstButtonClicked)
	{
		bd.ih(PopupManager.bvk, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gh());
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gi()), PopupManager.bvn.transform);
		if (gameObject != null)
		{
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge()).GetComponent<Text>().text = popupMessage;
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gf()).transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge()).GetComponent<Text>().text = buttonText;
			gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gf()).GetComponent<CustomButton>().onClick.AddListener(onFirstButtonClicked);
			gameObject.transform.GetComponent<Popup>().destroyAfterClose = true;
		}
		return gameObject;
	}

	// Token: 0x04000068 RID: 104
	private static string bvk = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gj();
}
