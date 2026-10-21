using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x020000B4 RID: 180
[RequireComponent(typeof(RectTransform))]
public class ViewAreaController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000495 RID: 1173 RVA: 0x00015C40 File Offset: 0x00013E40
	// (remove) Token: 0x06000496 RID: 1174 RVA: 0x00015C74 File Offset: 0x00013E74
	public static event Action<Game, bool> cmk;

	// Token: 0x06000497 RID: 1175 RVA: 0x00015CA7 File Offset: 0x00013EA7
	public static void bah(Game a, bool b)
	{
		Action<Game, bool> action = ViewAreaController.cmk;
		if (action == null)
		{
			return;
		}
		action(a, b);
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000498 RID: 1176 RVA: 0x00015CBC File Offset: 0x00013EBC
	public RectTransform dbk
	{
		get
		{
			RectTransform result;
			if ((result = this.cml) == null)
			{
				result = (this.cml = base.GetComponent<RectTransform>());
			}
			return result;
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00015CE4 File Offset: 0x00013EE4
	public void baj(ViewGameItem a)
	{
		if (this.views == null || this.views.Length == 0 || a == null)
		{
			return;
		}
		this.views[0].ur(a.RecentGameItems, new Action<Game, PointerEventData>(this.bak));
		this.views[1].ur(a.TabGameItems, new Action<Game, PointerEventData>(this.bak));
		if (this.menu != null)
		{
			this.bal();
		}
		this.cmo = base.GetComponentsInChildren<NestedVerticalScrollRect>();
		if (this.cmo != null && this.cmo.Length != 0)
		{
			foreach (NestedVerticalScrollRect nestedVerticalScrollRect in this.cmo)
			{
				nestedVerticalScrollRect.OnScrollCallback = (UnityAction)Delegate.Remove(nestedVerticalScrollRect.OnScrollCallback, new UnityAction(this.bal));
				nestedVerticalScrollRect.OnScrollCallback = (UnityAction)Delegate.Combine(nestedVerticalScrollRect.OnScrollCallback, new UnityAction(this.bal));
			}
		}
		this.cmn = base.GetComponentInChildren<RootNestedVerticalScrollRect>();
		if (this.cmn != null)
		{
			RootNestedVerticalScrollRect rootNestedVerticalScrollRect = this.cmn;
			rootNestedVerticalScrollRect.OnScrollCallback = (UnityAction)Delegate.Remove(rootNestedVerticalScrollRect.OnScrollCallback, new UnityAction(this.bal));
			RootNestedVerticalScrollRect rootNestedVerticalScrollRect2 = this.cmn;
			rootNestedVerticalScrollRect2.OnScrollCallback = (UnityAction)Delegate.Combine(rootNestedVerticalScrollRect2.OnScrollCallback, new UnityAction(this.bal));
			this.cmn.verticalNormalizedPosition = 1f;
			this.cmn.ra(Vector2.zero);
		}
		if (this.filterBar != null)
		{
			this.filterBar.uc();
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00015E74 File Offset: 0x00014074
	private void bak(Game a, PointerEventData b)
	{
		PointerEventData.InputButton button = b.button;
		if (button == null)
		{
			this.ShowGameInforPopup(a);
			return;
		}
		if (button != 1)
		{
			this.bal();
			return;
		}
		this.bam(a, b);
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00015EA8 File Offset: 0x000140A8
	public void bal()
	{
		if (this.menu != null && this.menu.daw)
		{
			this.menu.vw();
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00015ED0 File Offset: 0x000140D0
	private void bam(Game a, PointerEventData b)
	{
		if (b.pointerCurrentRaycast.isValid)
		{
			this.menu.vr(a, b.pointerCurrentRaycast.worldPosition);
		}
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00015F04 File Offset: 0x00014104
	private void ShowGameInforPopup(Game game)
	{
		this.bal();
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wk() + game.GameInfo.Title, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wl());
		GameObject gameObject;
		if (game.GameInfo.IsRegistered)
		{
			gameObject = PopupManager.bvn.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.un()).gameObject;
		}
		else if (game.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rg())
		{
			gameObject = PopupManager.bvn.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uo()).gameObject;
		}
		else
		{
			gameObject = PopupManager.bvn.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.up()).gameObject;
		}
		GameInfoPopup component = gameObject.GetComponent<GameInfoPopup>();
		if (component != null)
		{
			component.cmz = game;
		}
		PopupManager.OpenPopup(gameObject);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00015FC9 File Offset: 0x000141C9
	public void OnPointerClick(PointerEventData eventData)
	{
		this.bal();
	}

	// Token: 0x0400045D RID: 1117
	private RectTransform cml;

	// Token: 0x0400045E RID: 1118
	private const string cmm = "ViewAreaController";

	// Token: 0x0400045F RID: 1119
	[SerializeField]
	private ViewArea[] views;

	// Token: 0x04000460 RID: 1120
	[SerializeField]
	private GameItemMenu menu;

	// Token: 0x04000461 RID: 1121
	[SerializeField]
	private FilterBar filterBar;

	// Token: 0x04000462 RID: 1122
	private RootNestedVerticalScrollRect cmn;

	// Token: 0x04000463 RID: 1123
	private NestedVerticalScrollRect[] cmo;
}
