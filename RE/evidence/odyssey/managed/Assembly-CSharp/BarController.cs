using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000090 RID: 144
public class BarController : MonoBehaviour
{
	// Token: 0x06000370 RID: 880 RVA: 0x00011813 File Offset: 0x0000FA13
	private void Start()
	{
		this.SetupController();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001181C File Offset: 0x0000FA1C
	private void SetupController()
	{
		bd.id(BarController.cit, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vb(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vc());
		try
		{
			int num = this.items.Length;
			for (int i = 0; i < num; i++)
			{
				this.items[i].Init(this, i);
			}
		}
		catch (Exception ex)
		{
			bd.ie(BarController.cit, ex.Message, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vc());
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001188C File Offset: 0x0000FA8C
	private void OnButtonClick(int index)
	{
		bd.id(BarController.cit, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vd(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ve());
		UnityEvent<int> onItemClickCallback = this.OnItemClickCallback;
		if (onItemClickCallback == null)
		{
			return;
		}
		onItemClickCallback.Invoke(index);
	}

	// Token: 0x04000395 RID: 917
	private static readonly string cit = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vf();

	// Token: 0x04000396 RID: 918
	[SerializeField]
	private BarController.BarItem[] items;

	// Token: 0x04000397 RID: 919
	[HideInInspector]
	public UnityEvent<int> OnItemClickCallback;

	// Token: 0x02000091 RID: 145
	[Serializable]
	public class BarItem
	{
		// Token: 0x06000375 RID: 885 RVA: 0x000118CC File Offset: 0x0000FACC
		public void Init(BarController controller, int index)
		{
			if (this.Button != null)
			{
				this.Button.onClick.RemoveAllListeners();
				this.Button.onClick.AddListener(new UnityAction(this.uf));
			}
			this.index = index;
			this.cis = controller;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011921 File Offset: 0x0000FB21
		public void uf()
		{
			if (this.cis == null)
			{
				return;
			}
			this.cis.OnButtonClick(this.index);
		}

		// Token: 0x04000398 RID: 920
		public int index;

		// Token: 0x04000399 RID: 921
		public CustomButton Button;

		// Token: 0x0400039A RID: 922
		private BarController cis;
	}
}
