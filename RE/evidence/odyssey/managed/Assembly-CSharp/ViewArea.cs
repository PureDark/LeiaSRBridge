using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000096 RID: 150
[RequireComponent(typeof(RectTransform))]
public abstract class ViewArea : MonoBehaviour
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000382 RID: 898 RVA: 0x00011A6B File Offset: 0x0000FC6B
	// (set) Token: 0x06000383 RID: 899 RVA: 0x00011A73 File Offset: 0x0000FC73
	public bool ciy { get; private set; }

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000384 RID: 900 RVA: 0x00011A7C File Offset: 0x0000FC7C
	public RectTransform daq
	{
		get
		{
			if (this.ciz == null)
			{
				this.ciz = base.GetComponent<RectTransform>();
			}
			return this.ciz;
		}
	}

	// Token: 0x06000385 RID: 901
	protected abstract void InstantiateArea();

	// Token: 0x06000386 RID: 902 RVA: 0x00011AA0 File Offset: 0x0000FCA0
	protected virtual void OnItemClick(int index, PointerEventData evendata)
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
		if (this.cja == null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
			return;
		}
		int num = this.cja.Length;
		if (index > num - 1)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vl(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
			return;
		}
		if (this.cja[index] == null)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vi(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vk(), index), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.vh());
			return;
		}
		Action<Game, PointerEventData> action = this.cjb;
		if (action == null)
		{
			return;
		}
		action(this.cja[index], evendata);
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00011B4B File Offset: 0x0000FD4B
	public virtual void ur(Game[] a, Action<Game, PointerEventData> b)
	{
		this.cja = a;
		this.cjb = null;
		this.cjb = b;
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00011B62 File Offset: 0x0000FD62
	public void us()
	{
		this.ciy = false;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00011B6B File Offset: 0x0000FD6B
	public void ut()
	{
		this.ciy = true;
	}

	// Token: 0x040003A1 RID: 929
	private RectTransform ciz;

	// Token: 0x040003A2 RID: 930
	protected Game[] cja;

	// Token: 0x040003A3 RID: 931
	protected Action<Game, PointerEventData> cjb;
}
