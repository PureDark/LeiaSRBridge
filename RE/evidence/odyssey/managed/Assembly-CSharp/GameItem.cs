using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NonObfuscatable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200008B RID: 139
public class GameItem : MonoBehaviour
{
	// Token: 0x06000349 RID: 841 RVA: 0x00010F2C File Offset: 0x0000F12C
	private void Start()
	{
		this.cif = base.gameObject.GetComponent<CustomButton>();
		this.cif.onClick.AddListener(new UnityAction(this.ti));
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00010F5C File Offset: 0x0000F15C
	private void ti()
	{
		if (this.game.GameInfo.IsRegistered)
		{
			this.cig = PopupManager.bvn.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.un()).gameObject;
		}
		else if (this.game.Status == GameStatus.Upcoming.ToString())
		{
			this.cig = PopupManager.bvn.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uo()).gameObject;
		}
		else
		{
			this.cig = PopupManager.bvn.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.up()).gameObject;
		}
		GameInfoPopup component = this.cig.GetComponent<GameInfoPopup>();
		component.bbd(this.cih);
		if (component != null)
		{
			component.cmz = this.game;
		}
		ViewAreaController.cmk -= this.tk;
		ViewAreaController.cmk += this.tk;
		PopupManager.OpenPopup(this.cig);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00011057 File Offset: 0x0000F257
	public void tj(GameObject a)
	{
		this.cih = a;
		if (this.m_MainText != null)
		{
			this.cii = this.m_MainText.text;
			this.tl();
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00011088 File Offset: 0x0000F288
	private void tk(Game a, bool b)
	{
		if (a == null || this.game == null || a.Id != this.game.Id)
		{
			return;
		}
		Image component = base.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uq()).GetComponent<Image>();
		if (component != null)
		{
			component.gameObject.SetActive(b);
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x000110E4 File Offset: 0x0000F2E4
	private void OnDestroy()
	{
		ViewAreaController.cmk -= this.tk;
		this.tp();
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00011100 File Offset: 0x0000F300
	private void tl()
	{
		if (!this.tm())
		{
			return;
		}
		if (!this.m_Button)
		{
			return;
		}
		this.m_Button.OnEventStartHover.RemoveListener(new UnityAction(this.tn));
		this.m_Button.OnEventEndHover.RemoveListener(new UnityAction(this.tp));
		this.m_Button.OnEventStartHover.AddListener(new UnityAction(this.tn));
		this.m_Button.OnEventEndHover.AddListener(new UnityAction(this.tp));
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00011194 File Offset: 0x0000F394
	private bool tm()
	{
		Canvas.ForceUpdateCanvases();
		float preferredWidth = this.m_MainText.preferredWidth;
		float width = this.m_MainText.rectTransform.rect.width;
		if (preferredWidth <= width)
		{
			this.cil = 0f;
			this.m_MainText.rectTransform.anchoredPosition = Vector2.zero;
			return false;
		}
		float num = preferredWidth - width;
		this.cil = Mathf.Max(0f, ((num > width) ? preferredWidth : width) / 2f);
		this.cik = this.cil / 60f;
		int characterCount = this.m_MainText.cachedTextGenerator.characterCount;
		string text = this.m_MainText.text.Substring(0, characterCount - 3) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ur();
		this.m_MainText.text = text;
		return true;
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00011268 File Offset: 0x0000F468
	private void tn()
	{
		if (this.m_MainText == null || this.cin)
		{
			return;
		}
		this.cin = true;
		if (this.cij != null)
		{
			TweenExtensions.Kill(this.cij, false);
			this.cij = null;
		}
		this.m_MainText.rectTransform.anchoredPosition = Vector2.zero;
		this.m_MainText.horizontalOverflow = 1;
		this.m_MainText.text = this.cii;
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.AppendInterval(sequence, 0.3f);
		TweenSettingsExtensions.Append(sequence, TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.m_MainText.rectTransform, -this.cil, this.cik, false), 1));
		TweenSettingsExtensions.AppendInterval(sequence, 1f);
		TweenSettingsExtensions.SetLoops<Sequence>(sequence, -1);
		this.cij = sequence;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00011338 File Offset: 0x0000F538
	private void tp()
	{
		if (!this.cin)
		{
			return;
		}
		if (this.cij != null)
		{
			Tween tween = this.cij;
			if (tween != null)
			{
				TweenExtensions.Kill(tween, false);
			}
			this.cij = null;
		}
		this.cin = false;
		this.m_MainText.horizontalOverflow = 0;
		this.m_MainText.text = this.cii;
		this.tm();
		this.m_MainText.rectTransform.anchoredPosition = Vector2.zero;
	}

	// Token: 0x04000370 RID: 880
	[SerializeField]
	private Text m_MainText;

	// Token: 0x04000371 RID: 881
	[SerializeField]
	private CustomButton m_Button;

	// Token: 0x04000372 RID: 882
	public Game game;

	// Token: 0x04000373 RID: 883
	private CustomButton cif;

	// Token: 0x04000374 RID: 884
	private GameObject cig;

	// Token: 0x04000375 RID: 885
	private GameObject cih;

	// Token: 0x04000376 RID: 886
	private string cii = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();

	// Token: 0x04000377 RID: 887
	private Tween cij;

	// Token: 0x04000378 RID: 888
	private float cik;

	// Token: 0x04000379 RID: 889
	private float cil;

	// Token: 0x0400037A RID: 890
	private const float cim = 60f;

	// Token: 0x0400037B RID: 891
	private bool cin;
}
