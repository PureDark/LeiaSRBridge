using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000099 RID: 153
public class InfoBar : MonoBehaviour
{
	// Token: 0x0600039B RID: 923 RVA: 0x00011F99 File Offset: 0x00010199
	private void Start()
	{
		LanguageHelper.byo += this.vj;
		this.vj();
		this.versionButton.onClick.AddListener(new UnityAction(this.vk));
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00011FD0 File Offset: 0x000101D0
	private void vi()
	{
		float num = LayoutUtility.GetPreferredWidth(this.versionButtonText.gameObject.GetComponent<RectTransform>()) + 100f;
		RectTransform component = this.versionButton.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(num, component.sizeDelta.y);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001201C File Offset: 0x0001021C
	private void vj()
	{
		this.versionButtonText.text = LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vm()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb() + LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vn());
		this.versionButtonText.text = this.versionButtonText.text.Replace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vo(), Application.version);
		this.vi();
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0001207D File Offset: 0x0001027D
	private void vk()
	{
		PopupManager.OpenPopup(this.versionInfoPopup);
	}

	// Token: 0x040003AF RID: 943
	public CustomButton versionButton;

	// Token: 0x040003B0 RID: 944
	public Text versionButtonText;

	// Token: 0x040003B1 RID: 945
	public GameObject versionInfoPopup;
}
