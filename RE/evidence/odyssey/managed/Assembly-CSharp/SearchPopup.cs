using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using NonObfuscatable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000C3 RID: 195
public class SearchPopup : MonoBehaviour
{
	// Token: 0x0600050F RID: 1295 RVA: 0x00017B24 File Offset: 0x00015D24
	private void Start()
	{
		SearchPopup.lastSearchText = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xs();
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.bce));
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00017B4C File Offset: 0x00015D4C
	private void OnEnable()
	{
		SearchPopup.lastSearchText = this.inputField.text.Trim();
		this.bcg();
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00017B6C File Offset: 0x00015D6C
	private void bce(string a)
	{
		if (!string.IsNullOrEmpty(a.Trim()))
		{
			SearchPopup.lastSearchText = a;
			this.bcg();
			return;
		}
		if (this.com == null)
		{
			this.com = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xt());
		}
		if (this.com == null)
		{
			return;
		}
		if (this.cop == null)
		{
			this.cop = this.com.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xu()).gameObject;
		}
		if (this.con == null)
		{
			this.con = this.com.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xv()).gameObject;
		}
		if (this.con != null)
		{
			GameObject gameObject = this.con.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xw()).gameObject;
			if (gameObject.transform.childCount > 0)
			{
				foreach (object obj in gameObject.transform)
				{
					Object.Destroy(((Transform)obj).gameObject);
				}
			}
			if (this.coo == null)
			{
				this.coo = this.con.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xx()).gameObject;
			}
			if (this.coo != null)
			{
				this.coo.SetActive(true);
			}
			RectTransform component = this.con.GetComponent<RectTransform>();
			if (component != null)
			{
				Vector2 sizeDelta = component.sizeDelta;
				sizeDelta.y = (float)(this.coq / 2) + this.coo.GetComponent<RectTransform>().sizeDelta.y;
				component.sizeDelta = sizeDelta;
			}
			if (this.cop != null)
			{
				RectTransform component2 = this.cop.GetComponent<RectTransform>();
				if (component2 != null)
				{
					Vector2 sizeDelta2 = component2.sizeDelta;
					sizeDelta2.y = component.anchoredPosition3D.y * -1f + component.sizeDelta.y + (float)this.coq;
					component2.sizeDelta = sizeDelta2;
				}
			}
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00017D84 File Offset: 0x00015F84
	public void bcf(bool a)
	{
		if (this.com == null)
		{
			this.com = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xt());
		}
		if (this.com == null)
		{
			return;
		}
		if (this.com != null && this.con == null)
		{
			this.con = this.com.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xv()).gameObject;
		}
		CustomButton[] componentsInChildren = this.con.GetComponentsInChildren<CustomButton>();
		if (componentsInChildren != null && componentsInChildren.Length != 0)
		{
			foreach (CustomButton customButton in componentsInChildren)
			{
				if (a)
				{
					customButton.isEnabled = true;
				}
				else
				{
					customButton.sb();
				}
			}
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00017E28 File Offset: 0x00016028
	public void bcg()
	{
		if (bn.cyp.cyq != null)
		{
			List<Game> list = bn.cyp.cyq.lf(SearchPopup.lastSearchText);
			if (list != null)
			{
				this.LoadSearchedItems(list, SearchPopup.lastSearchText);
			}
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00017E68 File Offset: 0x00016068
	private void LoadSearchedItems(List<Game> searchedGameList, string searchedText)
	{
		if (this.com == null)
		{
			this.com = GameObject.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xt());
		}
		if (this.com == null)
		{
			return;
		}
		if (this.com != null)
		{
			if (this.cop == null)
			{
				this.cop = this.com.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xu()).gameObject;
			}
			if (this.con == null)
			{
				this.con = this.com.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xv()).gameObject;
			}
		}
		bd.id(SearchPopup.col, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xy() + searchedGameList.Count.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xz());
		int num = 0;
		if (this.con != null)
		{
			GameObject gameObject = this.con.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xw()).gameObject;
			if (gameObject.transform.childCount > 0)
			{
				foreach (object obj in gameObject.transform)
				{
					Object.Destroy(((Transform)obj).gameObject);
				}
			}
			GameObject gameObject2 = this.con.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ya()).gameObject;
			GameObject gameObject3 = this.con.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xx()).gameObject;
			gameObject2.GetComponent<Text>().text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yb() + searchedGameList.Count.ToString() + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pa();
			if (searchedGameList.Count > 0)
			{
				gameObject3.SetActive(false);
				using (List<Game>.Enumerator enumerator2 = searchedGameList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Game b = enumerator2.Current;
						this.bch(gameObject, b, num, searchedText);
						num++;
					}
					goto IL_1DB;
				}
			}
			gameObject3.SetActive(true);
			IL_1DB:
			RectTransform component = this.con.GetComponent<RectTransform>();
			Vector2 sizeDelta = component.sizeDelta;
			if (searchedGameList.Count > 0)
			{
				if (gameObject.GetComponent<RectTransform>() != null)
				{
					sizeDelta.y = (float)this.coq + gameObject.GetComponent<RectTransform>().sizeDelta.y;
				}
			}
			else
			{
				sizeDelta.y = (float)(this.coq / 2) + gameObject3.GetComponent<RectTransform>().sizeDelta.y;
			}
			component.sizeDelta = sizeDelta;
			RectTransform component2 = this.cop.GetComponent<RectTransform>();
			if (component2 != null)
			{
				Vector2 sizeDelta2 = component2.sizeDelta;
				sizeDelta2.y = component.anchoredPosition3D.y * -1f + component.sizeDelta.y + (float)this.coq;
				component2.sizeDelta = sizeDelta2;
			}
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0001813C File Offset: 0x0001633C
	private void bch(GameObject a, Game b, int c, string d)
	{
		float num = 40f;
		if (a != null && b != null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yc()), a.transform);
			if (gameObject != null)
			{
				SearchPopup.di di = new SearchPopup.di();
				gameObject.transform.name = b.GameInfo.Title;
				di.cok = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yd()).GetComponent<Image>();
				Text component = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ye()).GetComponent<Text>();
				Text component2 = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yf()).GetComponent<Text>();
				Image component3 = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yg()).GetComponent<Image>();
				Image component4 = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yh()).GetComponent<Image>();
				Image component5 = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yi()).GetComponent<Image>();
				Image component6 = gameObject.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.uq()).GetComponent<Image>();
				string allInOneImage = b.S_Thumbnail.AllInOneImage;
				bn.cyp.cyq.GetGameSprite(allInOneImage, new Action<Sprite>(di.bcd), null);
				if (b.GameInfo.Tier == GameTier.Ultimate.ToString())
				{
					component3.gameObject.SetActive(true);
				}
				component.supportRichText = true;
				string title = b.GameInfo.Title;
				if (title.Contains(d, StringComparison.OrdinalIgnoreCase))
				{
					int num2 = title.IndexOf(d, StringComparison.OrdinalIgnoreCase);
					component.text = string.Concat(new string[]
					{
						title.Substring(0, num2),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yj(),
						title.Substring(num2, d.Length),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yk(),
						title.Substring(num2 + d.Length)
					});
				}
				else
				{
					component.text = title;
				}
				string platform = b.GameInfo.Platform;
				component2.supportRichText = true;
				if (platform.Contains(d, StringComparison.OrdinalIgnoreCase))
				{
					int num3 = platform.IndexOf(d, StringComparison.OrdinalIgnoreCase);
					component2.text = string.Concat(new string[]
					{
						platform.Substring(0, num3),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yj(),
						platform.Substring(num3, d.Length),
						<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yk(),
						platform.Substring(num3 + d.Length)
					});
				}
				else
				{
					component2.text = platform;
				}
				if (component4 != null)
				{
					if (b.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rf())
					{
						bool flag = !b.GameInfo.DiscoveredByUser;
					}
					component4.gameObject.SetActive(b.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rf() && !b.GameInfo.DiscoveredByUser);
				}
				if (component5 != null)
				{
					bool active = b.Status == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rg();
					component5.gameObject.SetActive(active);
				}
				if (component6 != null)
				{
					component6.gameObject.SetActive(b.GameInfo.FavoritedByUser);
				}
				GameItem component7 = gameObject.GetComponent<GameItem>();
				component7.tj(gameObject);
				if (component7 != null)
				{
					component7.game = b;
				}
				RectTransform component8 = gameObject.GetComponent<RectTransform>();
				component8.pivot = new Vector2(0.5f, 0.5f);
				component8.anchorMin = new Vector2(0.5f, 1f);
				component8.anchorMax = new Vector2(0.5f, 1f);
				component8.anchoredPosition3D = new Vector3(0f, component8.rect.height / 2f * -1f + (component8.rect.height + num) * (float)c * -1f, 0f);
				RectTransform component9 = a.GetComponent<RectTransform>();
				Vector2 sizeDelta = component9.sizeDelta;
				sizeDelta.y = component8.rect.height * (float)(c + 1) + num * (float)c;
				component9.sizeDelta = sizeDelta;
			}
		}
	}

	// Token: 0x040004CE RID: 1230
	private static string col = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yl();

	// Token: 0x040004CF RID: 1231
	public RayReceiver rayReceiver;

	// Token: 0x040004D0 RID: 1232
	public static string lastSearchText;

	// Token: 0x040004D1 RID: 1233
	public InputField inputField;

	// Token: 0x040004D2 RID: 1234
	private GameObject com;

	// Token: 0x040004D3 RID: 1235
	private GameObject con;

	// Token: 0x040004D4 RID: 1236
	private GameObject coo;

	// Token: 0x040004D5 RID: 1237
	private GameObject cop;

	// Token: 0x040004D6 RID: 1238
	private int coq = 100;

	// Token: 0x020000C4 RID: 196
	[CompilerGenerated]
	private sealed class di
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0001856F File Offset: 0x0001676F
		internal void gri(Sprite a)
		{
			if (this.cok != null && a != null)
			{
				this.cok.sprite = a;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001856F File Offset: 0x0001676F
		internal void bcd(Sprite a)
		{
			if (this.cok != null && a != null)
			{
				this.cok.sprite = a;
			}
		}

		// Token: 0x040004D7 RID: 1239
		public Image cok;
	}
}
