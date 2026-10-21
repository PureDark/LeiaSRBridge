using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	// Token: 0x0200014D RID: 333
	public class SoftMaskSampleChooser : MonoBehaviour
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x00026F98 File Offset: 0x00025198
		public void Start()
		{
			SoftMaskSampleChooser.eh eh = new SoftMaskSampleChooser.eh();
			eh.cuj = SceneManager.GetActiveScene().name;
			int num = this.dropdown.options.FindIndex(new Predicate<Dropdown.OptionData>(eh.blp));
			if (num >= 0)
			{
				this.dropdown.value = num;
				this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.blr));
				return;
			}
			this.blq(eh.cuj);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00027014 File Offset: 0x00025214
		private void blq(string a)
		{
			this.dropdown.gameObject.SetActive(false);
			this.fallbackLabel.gameObject.SetActive(true);
			this.fallbackLabel.text = a;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00027044 File Offset: 0x00025244
		public void blr(int a)
		{
			SceneManager.LoadScene(this.dropdown.options[a].text);
		}

		// Token: 0x04000719 RID: 1817
		public Dropdown dropdown;

		// Token: 0x0400071A RID: 1818
		public Text fallbackLabel;

		// Token: 0x0200014E RID: 334
		[CompilerGenerated]
		private sealed class eh
		{
			// Token: 0x06000928 RID: 2344 RVA: 0x00027061 File Offset: 0x00025261
			internal bool blp(Dropdown.OptionData a)
			{
				return a.text == this.cuj;
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x00027061 File Offset: 0x00025261
			internal bool icv(Dropdown.OptionData a)
			{
				return a.text == this.cuj;
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x00027061 File Offset: 0x00025261
			internal bool lli(Dropdown.OptionData a)
			{
				return a.text == this.cuj;
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x00027061 File Offset: 0x00025261
			internal bool dpb(Dropdown.OptionData a)
			{
				return a.text == this.cuj;
			}

			// Token: 0x0400071B RID: 1819
			public string cuj;
		}
	}
}
