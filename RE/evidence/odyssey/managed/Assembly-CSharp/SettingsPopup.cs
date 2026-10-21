using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000C5 RID: 197
public class SettingsPopup : MonoBehaviour
{
	// Token: 0x0600051B RID: 1307 RVA: 0x00018594 File Offset: 0x00016794
	private void Start()
	{
		this.cox = base.gameObject;
		this.cpg = true;
		using (List<Button>.Enumerator enumerator = this.fieldButtons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SettingsPopup.dj dj = new SettingsPopup.dj();
				dj.cos = this;
				dj.cor = enumerator.Current;
				dj.cor.onClick.AddListener(new UnityAction(dj.bcj));
			}
		}
		using (List<Toggle>.Enumerator enumerator2 = this.cursorToggles.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				SettingsPopup.dk dk = new SettingsPopup.dk();
				dk.cou = this;
				dk.cot = enumerator2.Current;
				dk.cot.onValueChanged.AddListener(new UnityAction<bool>(dk.bck));
			}
		}
		this.applyButton.onClick.AddListener(new UnityAction(this.bdf));
		this.resetButton.onClick.AddListener(new UnityAction(this.bdg));
		this.closeButton.onClick.AddListener(new UnityAction(this.bdh));
		this.convertVideoTo3DToggle.onValueChanged.AddListener(new UnityAction<bool>(this.bdi));
		this.autoGameScanToggle.onValueChanged.AddListener(new UnityAction<bool>(this.bdj));
		this.autoRunToggle.onValueChanged.AddListener(new UnityAction<bool>(this.bdk));
		this.bcz();
		this.bcw();
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00018740 File Offset: 0x00016940
	private void Update()
	{
		if (this.coy != null)
		{
			Transform transform = this.coy.transform.Find(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge());
			if (transform != null)
			{
				Text component = transform.GetComponent<Text>();
				bool flag = false;
				int num = 0;
				int num2 = 0;
				if (component != null)
				{
					if (Input.GetKey(8) || Input.GetKey(127))
					{
						return;
					}
					if (Input.GetKey(306) || Input.GetKey(305))
					{
						flag = true;
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ym();
						num++;
					}
					if (Input.GetKey(304) || Input.GetKey(303))
					{
						flag = true;
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yn();
						num++;
					}
					if (Input.GetKey(308) || Input.GetKey(307))
					{
						flag = true;
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yo();
						num++;
					}
					if (num >= 3)
					{
						this.cpb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
						this.bcq(LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yp()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb() + LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yq()));
						return;
					}
					if (!flag)
					{
						this.cpb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
						return;
					}
					for (int i = 0; i <= 9; i++)
					{
						if (Input.GetKeyDown(48 + i) || Input.GetKeyDown(256 + i))
						{
							this.cpb += i.ToString();
							num2++;
							break;
						}
					}
					for (char c = 'A'; c <= 'Z'; c += '\u0001')
					{
						if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), c.ToString())))
						{
							this.cpb += c.ToString();
							num2++;
							break;
						}
					}
					for (int j = 1; j <= 12; j++)
					{
						if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yr() + j.ToString())))
						{
							this.cpb = this.cpb + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yr() + j.ToString();
							num2++;
							break;
						}
					}
					if (Input.GetKeyDown(273))
					{
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ys();
						num2++;
					}
					if (Input.GetKeyDown(274))
					{
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yt();
						num2++;
					}
					if (Input.GetKeyDown(276))
					{
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yu();
						num2++;
					}
					if (Input.GetKeyDown(275))
					{
						this.cpb += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yv();
						num2++;
					}
					if (num2 == 0)
					{
						this.cpb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
						return;
					}
					if (num2 != 1)
					{
						this.cpb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
						this.bcq(LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yp()) + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb() + LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yq()));
						return;
					}
					if (!string.IsNullOrEmpty(this.cpb))
					{
						bd.id(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yw() + this.cpb, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sw());
						if (!this.bcp(this.cpb))
						{
							bd.id(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sw());
							if (!this.cpb.EndsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yy()))
							{
								component.text = this.cpb;
								this.cpf = true;
								this.bco(true);
								if (!this.textFields[SettingsPopup.coz].text.Equals(bn.cyp.cyr.cyx[SettingsPopup.coz]))
								{
									this.bcn(true);
								}
							}
						}
						else
						{
							bd.id(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yz(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.sw());
							this.bcq(LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.za()));
							component.text = bn.cyp.cyr.cyw[SettingsPopup.coz];
						}
						this.cpb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
					}
				}
			}
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00018B98 File Offset: 0x00016D98
	private void OnEnable()
	{
		bd.id(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zb(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ws());
		if (this.cpg)
		{
			this.Initialize();
			this.cpd = bn.cyp.cyr.cyv;
			this.cpg = false;
		}
		this.cpe = false;
		this.cph = false;
		this.bde();
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00018BF8 File Offset: 0x00016DF8
	private void OnDisable()
	{
		bd.id(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zc(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.xg());
		if (!this.cph && this.coy)
		{
			this.coy.GetComponent<HomeToggleHover>().SelectedImage.gameObject.SetActive(false);
			this.coy = null;
		}
		foreach (Button button in this.fieldButtons)
		{
			button.gameObject.GetComponent<HomeToggleHover>().canvasGroup.alpha = 0f;
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00018CA8 File Offset: 0x00016EA8
	public void Initialize()
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zd());
		if (bn.cyp.cyr == null)
		{
			return;
		}
		this.cpf = false;
		this.bco(false);
		this.bcn(false);
		string text = LanguageHelper.hv();
		if (LanguageHelper.hs())
		{
			this.bcn(true);
		}
		int num = LanguageHelper.hw().IndexOf(text);
		if (num >= 0)
		{
			int num2 = this.bda(num);
			if (num2 < this.languageDropDown.options.Count)
			{
				this.languageDropDown.value = num2;
			}
		}
		if (text == LanguageHelper.byl)
		{
			this.bcn(false);
		}
		for (int i = 0; i < this.textFields.Count; i++)
		{
			this.textFields[i].text = bn.cyp.cyr.cyw[i];
		}
		for (int j = 0; j < this.textFields.Count; j++)
		{
			if (this.textFields[j].text != bn.cyp.cyr.cyx[j])
			{
				this.bcn(true);
				break;
			}
		}
		this.cursorToggles[this.cpc].isOn = false;
		this.cpc = bn.cyp.cyr.cyv;
		if (this.cpc != 0)
		{
			this.bcn(true);
		}
		this.autoConvStandardToggle.gameObject.SetActive(bn.cyp.cyt);
		this.cursorToggles[this.cpc].isOn = true;
		MouseCursor.ChangeCursor(this.cpc);
		this.autoGameScanToggle.isOn = (bn.cyp.cyr.cyz == 1);
		this.autoRunToggle.isOn = (bn.cyp.cyr.cza == 1);
		this.convertVideoTo3DToggle.isOn = (bn.cyp.cyr.cyy == 1);
		this.cpj = (bn.cyp.cyr.czc == 1);
		if (bn.cyp.cyr.czb == 0)
		{
			this.manualConvToggle.isOn = true;
			this.autoConvStandardToggle.isOn = false;
			this.autoConvSplitToggle.isOn = false;
		}
		else
		{
			this.manualConvToggle.isOn = false;
			if (bn.cyp.cyt)
			{
				this.autoConvStandardToggle.isOn = (bn.cyp.cyr.czc == 1);
				this.autoConvSplitToggle.isOn = (bn.cyp.cyr.czc == 0);
			}
			else
			{
				this.autoConvSplitToggle.isOn = true;
				this.autoConvStandardToggle.isOn = false;
			}
		}
		if (!this.convertVideoTo3DToggle.isOn)
		{
			this.bcn(true);
			this.bct();
		}
		else
		{
			this.bcu();
		}
		this.manualConvToggle.onValueChanged.AddListener(new UnityAction<bool>(this.bdl));
		this.autoConvStandardToggle.onValueChanged.AddListener(new UnityAction<bool>(this.bdm));
		this.autoConvSplitToggle.onValueChanged.AddListener(new UnityAction<bool>(this.bdn));
		if (!this.autoRunToggle.isOn || !this.autoGameScanToggle.isOn || !this.manualConvToggle.isOn || this.autoConvStandardToggle.isOn || this.autoConvSplitToggle.isOn)
		{
			this.bcn(true);
		}
		this.bco(false);
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00019034 File Offset: 0x00017234
	private void bcn(bool a)
	{
		this.resetButton.isEnabled = a;
		this.resetButton.sc();
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001904D File Offset: 0x0001724D
	private void bco(bool a)
	{
		this.applyButton.isEnabled = a;
		this.applyButton.sc();
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00019068 File Offset: 0x00017268
	private bool bcp(string a)
	{
		for (int i = 0; i < this.textFields.Count; i++)
		{
			if (i != SettingsPopup.coz && this.textFields[i].text.Equals(a))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x000190AF File Offset: 0x000172AF
	private void bcq(string a)
	{
		this.cph = true;
		PopupManager.OpenPopup(PopupManager.CreateOneButtonPopup(a, LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pj()), new UnityAction(this.bdo)));
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x000190DC File Offset: 0x000172DC
	private void RollbackCursor()
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ze());
		MouseCursor.currentCursor.SetActive(false);
		MouseCursor.ChangeCursor(this.cpd);
		bn.cyp.cyr.cyv = this.cpd;
		MouseCursor.currentCursor.SetActive(true);
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00019130 File Offset: 0x00017330
	private void OnApplyButtonClicked()
	{
		SettingsPopup.dl dl = new SettingsPopup.dl();
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zf());
		int cursorType = this.cpc;
		int convertVideoTo3D = this.convertVideoTo3DToggle.isOn ? 1 : 0;
		int autoGameScan = this.autoGameScanToggle.isOn ? 1 : 0;
		int autoRun = this.autoRunToggle.isOn ? 1 : 0;
		int autoConvert = this.manualConvToggle.isOn ? 0 : 1;
		dl.cow = LanguageHelper.GetLanguage(this.bdb(this.languageDropDown.value));
		int isStandardVideo;
		if (this.manualConvToggle.isOn)
		{
			isStandardVideo = (this.cpj ? 1 : 0);
		}
		else
		{
			isStandardVideo = (this.autoConvStandardToggle.isOn ? 1 : 0);
		}
		if (!bn.cyp.cyt)
		{
			isStandardVideo = 0;
		}
		dl.cov = new string[6];
		for (int i = 0; i < this.textFields.Count; i++)
		{
			dl.cov[i] = this.textFields[i].text;
		}
		if (bn.cyp.cyr.cyw != dl.cov)
		{
			Task.Run(new Action(dl.bcl));
		}
		if (bn.cyp.cyr.czd != dl.cow)
		{
			Task.Run(new Action(dl.bcm));
		}
		bn.cyp.cyr.UpdateData(dl.cov, cursorType, convertVideoTo3D, autoGameScan, autoRun, autoConvert, isStandardVideo, dl.cow);
		bn.cyp.cyr.SerializeData();
		this.cpf = false;
		this.bco(false);
		this.cpg = true;
		PopupManager.ClosePopup();
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x000192E8 File Offset: 0x000174E8
	private void OnResetButtonClicked()
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zg());
		this.cpe = true;
		PopupManager.OpenPopup(PopupManager.CreateTwoButtonPopup(LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zh()), LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pj()), LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj()), new UnityAction(this.bdp), new UnityAction(this.bdq)));
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0001934C File Offset: 0x0001754C
	private void bcr()
	{
		if (this.cpf)
		{
			this.cpf = this.applyButton.isEnabled;
		}
		if (this.cpf && !this.cpe)
		{
			PopupManager.OpenPopup(PopupManager.CreateTwoButtonPopup(LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zi()), LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.pj()), LanguageHelper.GetString(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nj()), new UnityAction(this.bdr), new UnityAction(this.bds)));
			return;
		}
		this.cpg = true;
		PopupManager.ClosePopup();
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000193D0 File Offset: 0x000175D0
	private void bcs(Button a)
	{
		if (this.coy)
		{
			this.coy.GetComponent<HomeToggleHover>().SelectedImage.gameObject.SetActive(false);
		}
		a.gameObject.GetComponent<HomeToggleHover>().SelectedImage.gameObject.SetActive(true);
		this.coy = a.gameObject;
		SettingsPopup.coz = this.fieldButtons.IndexOf(a);
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00019440 File Offset: 0x00017640
	private void OnCursorToggleValueChanged(Toggle changedToggle)
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zj());
		int num = Convert.ToInt32(changedToggle.transform.name.Substring(changedToggle.transform.name.Length - 1));
		if (this.cpc != num && !this.cpg)
		{
			this.cpc = num;
			MouseCursor.ChangeCursor(this.cpc);
			if (this.cpc != 0)
			{
				this.bcn(true);
			}
			this.cpf = true;
			this.bco(true);
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x000194C8 File Offset: 0x000176C8
	private void OnConvertVideoTo3DToggleValueChanged(Toggle changedToggle)
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zk());
		int num = this.convertVideoTo3DToggle.isOn ? 1 : 0;
		if (num != bn.cyp.cyr.cyy)
		{
			this.cpf = true;
		}
		this.bco(true);
		if (num != 1)
		{
			this.bcn(true);
		}
		if (this.convertVideoTo3DToggle.isOn)
		{
			this.bcu();
			return;
		}
		this.bct();
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x0001953C File Offset: 0x0001773C
	private void OnAutoGameScanToggleValueChanged(Toggle changedToggle)
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zl());
		int num = this.autoGameScanToggle.isOn ? 1 : 0;
		if (num != bn.cyp.cyr.cyz)
		{
			this.cpf = true;
		}
		this.bco(true);
		if (num != 1)
		{
			this.bcn(true);
		}
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00019594 File Offset: 0x00017794
	private void OnAutoRunToggleValueChanged(Toggle changedToggle)
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zm());
		int num = this.autoRunToggle.isOn ? 1 : 0;
		if (num != bn.cyp.cyr.cza)
		{
			this.cpf = true;
		}
		this.bco(true);
		if (num != 1)
		{
			this.bcn(true);
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000195EC File Offset: 0x000177EC
	private void On3DConvertToggleValueChanged(Toggle changedToggle)
	{
		bd.ih(this.cpa, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zn());
		if (this.cpi)
		{
			return;
		}
		if (changedToggle.isOn)
		{
			this.cpi = true;
			if (changedToggle != this.manualConvToggle)
			{
				this.manualConvToggle.isOn = false;
			}
			if (changedToggle != this.autoConvStandardToggle)
			{
				this.autoConvStandardToggle.isOn = false;
			}
			if (changedToggle != this.autoConvSplitToggle)
			{
				this.autoConvSplitToggle.isOn = false;
			}
			this.cpi = false;
		}
		else
		{
			changedToggle.isOn = true;
		}
		if (changedToggle == this.manualConvToggle)
		{
			int num = this.manualConvToggle.isOn ? 1 : 0;
			if (num == bn.cyp.cyr.czb)
			{
				this.cpf = true;
				this.bco(true);
			}
			if (num == 0)
			{
				this.bcn(true);
			}
		}
		if (changedToggle == this.autoConvStandardToggle)
		{
			int num2 = this.manualConvToggle.isOn ? 1 : 0;
			int num3 = this.autoConvStandardToggle.isOn ? 1 : 0;
			if (num3 != bn.cyp.cyr.czc || num2 == bn.cyp.cyr.czb)
			{
				this.cpf = true;
				this.bco(true);
			}
			if (num3 != 0)
			{
				this.bcn(true);
			}
		}
		if (changedToggle == this.autoConvSplitToggle)
		{
			int num4 = this.manualConvToggle.isOn ? 1 : 0;
			int num5 = this.autoConvSplitToggle.isOn ? 1 : 0;
			if (num5 == bn.cyp.cyr.czc || num4 == bn.cyp.cyr.czb)
			{
				this.cpf = true;
				this.bco(true);
			}
			if (num5 != 0)
			{
				this.bcn(true);
			}
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0001979F File Offset: 0x0001799F
	private void bct()
	{
		this.bcv(this.manualConvToggle, false, 0.3f);
		this.bcv(this.autoConvStandardToggle, false, 0.3f);
		this.bcv(this.autoConvSplitToggle, false, 0.3f);
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x000197D7 File Offset: 0x000179D7
	private void bcu()
	{
		this.bcv(this.manualConvToggle, true, 1f);
		this.bcv(this.autoConvStandardToggle, true, 1f);
		this.bcv(this.autoConvSplitToggle, true, 1f);
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0001980F File Offset: 0x00017A0F
	private void bcv(Toggle a, bool b, float c)
	{
		a.interactable = b;
		a.graphic.GetComponent<CanvasGroup>().alpha = c;
		a.GetComponentInChildren<Text>().GetComponent<CanvasGroup>().alpha = c;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0001983C File Offset: 0x00017A3C
	private void bcw()
	{
		this.cpl = base.GetComponent<CustomSelectable>();
		if (this.cpl == null && this.languageDropDown != null)
		{
			this.cpl = this.languageDropDown.GetComponent<CustomSelectable>();
		}
		if (this.cpl != null)
		{
			this.cpl.bay();
			this.cpl.OnSelectionChanged = new Action<bool>(this.bcx);
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x000198B2 File Offset: 0x00017AB2
	private void bcx(bool a)
	{
		this.bcy(a);
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x000198BC File Offset: 0x00017ABC
	private void bcy(bool a)
	{
		if (this.arrowImage == null)
		{
			return;
		}
		if (a)
		{
			if (this.I_ButtonArrow_up != null)
			{
				this.arrowImage.sprite = this.I_ButtonArrow_up;
				return;
			}
		}
		else if (this.I_ButtonArrow_down != null)
		{
			this.arrowImage.sprite = this.I_ButtonArrow_down;
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001991C File Offset: 0x00017B1C
	private void bcz()
	{
		this.languageDropDown.ClearOptions();
		List<string> list = LanguageHelper.hx();
		for (int i = 0; i < list.Count; i++)
		{
			this.languageDropDown.options.Add(new Dropdown.OptionData(list[i]));
		}
		this.languageDropDown.RefreshShownValue();
		this.languageDropDown.onValueChanged.AddListener(new UnityAction<int>(this.bdd));
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0001998E File Offset: 0x00017B8E
	private int bda(int a)
	{
		return a;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0001998E File Offset: 0x00017B8E
	private int bdb(int a)
	{
		return a;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00019991 File Offset: 0x00017B91
	private bool bdc(int a)
	{
		return false;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00019994 File Offset: 0x00017B94
	private void bdd(int a)
	{
		if (this.bdc(a))
		{
			string item = LanguageHelper.hv();
			int num = LanguageHelper.hw().IndexOf(item);
			if (num >= 0)
			{
				int value = this.bda(num);
				this.languageDropDown.value = value;
			}
			return;
		}
		if (!this.cpf)
		{
			this.cpf = true;
		}
		int index = this.bdb(a);
		string b = LanguageHelper.hw()[index];
		if (LanguageHelper.byl != b)
		{
			this.bcn(true);
		}
		this.bco(true);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00019A18 File Offset: 0x00017C18
	private void bde()
	{
		if (this.resetButton != null)
		{
			this.resetButton.ry(CustomButton.ck.cgt);
		}
		if (this.applyButton != null)
		{
			this.applyButton.ry(CustomButton.ck.cgt);
		}
		if (this.closeButton != null)
		{
			this.closeButton.ry(CustomButton.ck.cgt);
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00019AAD File Offset: 0x00017CAD
	[CompilerGenerated]
	private void bdf()
	{
		this.OnApplyButtonClicked();
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00019AB5 File Offset: 0x00017CB5
	[CompilerGenerated]
	private void bdg()
	{
		this.OnResetButtonClicked();
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00019ABD File Offset: 0x00017CBD
	[CompilerGenerated]
	private void bdh()
	{
		this.bcr();
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00019AC5 File Offset: 0x00017CC5
	[CompilerGenerated]
	private void bdi(bool a)
	{
		this.OnConvertVideoTo3DToggleValueChanged(this.convertVideoTo3DToggle);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00019AD3 File Offset: 0x00017CD3
	[CompilerGenerated]
	private void bdj(bool a)
	{
		this.OnAutoGameScanToggleValueChanged(this.autoGameScanToggle);
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00019AE1 File Offset: 0x00017CE1
	[CompilerGenerated]
	private void bdk(bool a)
	{
		this.OnAutoRunToggleValueChanged(this.autoRunToggle);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00019AEF File Offset: 0x00017CEF
	[CompilerGenerated]
	private void bdl(bool a)
	{
		this.On3DConvertToggleValueChanged(this.manualConvToggle);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00019AFD File Offset: 0x00017CFD
	[CompilerGenerated]
	private void bdm(bool a)
	{
		this.On3DConvertToggleValueChanged(this.autoConvStandardToggle);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00019B0B File Offset: 0x00017D0B
	[CompilerGenerated]
	private void bdn(bool a)
	{
		this.On3DConvertToggleValueChanged(this.autoConvSplitToggle);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00019B19 File Offset: 0x00017D19
	[CompilerGenerated]
	private void bdo()
	{
		this.cpg = false;
		PopupManager.OpenPopup(this.cox);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00019B30 File Offset: 0x00017D30
	[CompilerGenerated]
	private void bdp()
	{
		bn.cyp.cyr.ResetData();
		this.cpg = true;
		PopupManager.OpenPopup(this.cox);
		Task.Run(new Action(SettingsPopup.<>c.<>9.bci));
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00019B19 File Offset: 0x00017D19
	[CompilerGenerated]
	private void bdq()
	{
		this.cpg = false;
		PopupManager.OpenPopup(this.cox);
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00019B83 File Offset: 0x00017D83
	[CompilerGenerated]
	private void bdr()
	{
		this.cpf = false;
		this.cpg = true;
		this.RollbackCursor();
		PopupManager.ClosePopup();
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00019B19 File Offset: 0x00017D19
	[CompilerGenerated]
	private void bds()
	{
		this.cpg = false;
		PopupManager.OpenPopup(this.cox);
	}

	// Token: 0x040004D8 RID: 1240
	public List<Button> fieldButtons;

	// Token: 0x040004D9 RID: 1241
	public List<Toggle> cursorToggles;

	// Token: 0x040004DA RID: 1242
	public List<Text> textFields;

	// Token: 0x040004DB RID: 1243
	public Toggle autoRunToggle;

	// Token: 0x040004DC RID: 1244
	public Toggle autoGameScanToggle;

	// Token: 0x040004DD RID: 1245
	public Toggle convertVideoTo3DToggle;

	// Token: 0x040004DE RID: 1246
	public Toggle manualConvToggle;

	// Token: 0x040004DF RID: 1247
	public Toggle autoConvStandardToggle;

	// Token: 0x040004E0 RID: 1248
	public Toggle autoConvSplitToggle;

	// Token: 0x040004E1 RID: 1249
	public CustomButton closeButton;

	// Token: 0x040004E2 RID: 1250
	public CustomButton applyButton;

	// Token: 0x040004E3 RID: 1251
	public CustomButton resetButton;

	// Token: 0x040004E4 RID: 1252
	public Dropdown languageDropDown;

	// Token: 0x040004E5 RID: 1253
	private GameObject cox;

	// Token: 0x040004E6 RID: 1254
	private GameObject coy;

	// Token: 0x040004E7 RID: 1255
	private static int coz;

	// Token: 0x040004E8 RID: 1256
	private string cpa = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.zo();

	// Token: 0x040004E9 RID: 1257
	private string cpb = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();

	// Token: 0x040004EA RID: 1258
	private int cpc;

	// Token: 0x040004EB RID: 1259
	private int cpd;

	// Token: 0x040004EC RID: 1260
	private bool cpe;

	// Token: 0x040004ED RID: 1261
	private bool cpf;

	// Token: 0x040004EE RID: 1262
	private bool cpg = true;

	// Token: 0x040004EF RID: 1263
	private bool cph = true;

	// Token: 0x040004F0 RID: 1264
	private bool cpi;

	// Token: 0x040004F1 RID: 1265
	private bool cpj = true;

	// Token: 0x040004F2 RID: 1266
	[Header("Language Divider Configuration")]
	[Tooltip("Sprite divider image")]
	[SerializeField]
	private Sprite dividerSprite;

	// Token: 0x040004F3 RID: 1267
	[Header("Arrow Button Sprites")]
	public Sprite I_ButtonArrow_down;

	// Token: 0x040004F4 RID: 1268
	public Sprite I_ButtonArrow_up;

	// Token: 0x040004F5 RID: 1269
	public Image arrowImage;

	// Token: 0x040004F6 RID: 1270
	private bool cpk = true;

	// Token: 0x040004F7 RID: 1271
	private CustomSelectable cpl;

	// Token: 0x020000C7 RID: 199
	[CompilerGenerated]
	private sealed class dj
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00019BC0 File Offset: 0x00017DC0
		internal void bcj()
		{
			this.cos.bcs(this.cor);
		}

		// Token: 0x040004FA RID: 1274
		public Button cor;

		// Token: 0x040004FB RID: 1275
		public SettingsPopup cos;
	}

	// Token: 0x020000C8 RID: 200
	[CompilerGenerated]
	private sealed class dk
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x00019BD3 File Offset: 0x00017DD3
		internal void bck(bool a)
		{
			this.cou.OnCursorToggleValueChanged(this.cot);
		}

		// Token: 0x040004FC RID: 1276
		public Toggle cot;

		// Token: 0x040004FD RID: 1277
		public SettingsPopup cou;
	}

	// Token: 0x020000C9 RID: 201
	[CompilerGenerated]
	private sealed class dl
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x00019BE6 File Offset: 0x00017DE6
		internal void isl()
		{
			SharedMemoryManager.SendData(SharedMemoryManager.j.buo, this.cow);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00019BE6 File Offset: 0x00017DE6
		internal void gls()
		{
			SharedMemoryManager.SendData(SharedMemoryManager.j.buo, this.cow);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00019BF4 File Offset: 0x00017DF4
		internal void bcl()
		{
			SharedMemoryManager.SendHotkeysData(this.cov);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00019BE6 File Offset: 0x00017DE6
		internal void bcm()
		{
			SharedMemoryManager.SendData(SharedMemoryManager.j.buo, this.cow);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00019BF4 File Offset: 0x00017DF4
		internal void fga()
		{
			SharedMemoryManager.SendHotkeysData(this.cov);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00019BE6 File Offset: 0x00017DE6
		internal void mam()
		{
			SharedMemoryManager.SendData(SharedMemoryManager.j.buo, this.cow);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019BF4 File Offset: 0x00017DF4
		internal void cdq()
		{
			SharedMemoryManager.SendHotkeysData(this.cov);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00019BF4 File Offset: 0x00017DF4
		internal void cei()
		{
			SharedMemoryManager.SendHotkeysData(this.cov);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00019BF4 File Offset: 0x00017DF4
		internal void iix()
		{
			SharedMemoryManager.SendHotkeysData(this.cov);
		}

		// Token: 0x040004FE RID: 1278
		public string[] cov;

		// Token: 0x040004FF RID: 1279
		public string cow;
	}
}
