using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000097 RID: 151
[RequireComponent(typeof(RectTransform))]
public class ViewContent : MonoBehaviour
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600038B RID: 907 RVA: 0x00011B74 File Offset: 0x0000FD74
	public bool dar
	{
		get
		{
			return this.TrackingArea != null;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600038C RID: 908 RVA: 0x00011B84 File Offset: 0x0000FD84
	public ScrollRect das
	{
		get
		{
			return this.cjf = ((this.cjf != null) ? this.cjf : base.gameObject.GetComponentInParent<ScrollRect>());
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600038D RID: 909 RVA: 0x00011BBC File Offset: 0x0000FDBC
	public RectTransform dat
	{
		get
		{
			return this.cjg = ((this.cjg != null) ? this.cjg : base.GetComponent<RectTransform>());
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600038E RID: 910 RVA: 0x00011BF0 File Offset: 0x0000FDF0
	public RectTransform dau
	{
		get
		{
			return this.cjh = ((this.cjh != null) ? this.cjh : this.das.GetComponent<RectTransform>());
		}
	}

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x0600038F RID: 911 RVA: 0x00011C28 File Offset: 0x0000FE28
	// (remove) Token: 0x06000390 RID: 912 RVA: 0x00011C60 File Offset: 0x0000FE60
	public event Action<Vector2> cji;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000391 RID: 913 RVA: 0x00011C98 File Offset: 0x0000FE98
	// (remove) Token: 0x06000392 RID: 914 RVA: 0x00011CD0 File Offset: 0x0000FED0
	public event Action<ViewContent.ct> cjj;

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000393 RID: 915 RVA: 0x00011D05 File Offset: 0x0000FF05
	// (set) Token: 0x06000394 RID: 916 RVA: 0x00011D0D File Offset: 0x0000FF0D
	public ViewContent.ct dav
	{
		get
		{
			return this.cjk;
		}
		set
		{
			if (this.cjk == value)
			{
				return;
			}
			this.cjk = value;
			Action<ViewContent.ct> action = this.cjj;
			if (action == null)
			{
				return;
			}
			action(this.cjk);
		}
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00011D38 File Offset: 0x0000FF38
	private void Start()
	{
		if (this.das != null)
		{
			this.das.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.vg));
			this.das.onValueChanged.AddListener(new UnityAction<Vector2>(this.vg));
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00011D8C File Offset: 0x0000FF8C
	private ViewContent.ct ve()
	{
		Vector3[] array = new Vector3[4];
		Vector3[] array2 = new Vector3[4];
		if (this.dar)
		{
			this.TrackingArea.GetWorldCorners(array);
		}
		else
		{
			this.dat.GetWorldCorners(array);
		}
		this.dau.GetWorldCorners(array2);
		bool flag = array[0].x < array2[0].x;
		bool flag2 = array[2].x > array2[2].x;
		bool flag3 = array[1].y > array2[1].y;
		bool flag4 = array[3].y < array2[3].y;
		bool flag5 = array[2].x < array2[0].x;
		bool flag6 = array[0].x > array2[2].x;
		bool flag7 = array[3].y > array2[1].y;
		bool flag8 = array[1].y < array2[3].y;
		if (flag5 || flag6 || flag7 || flag8)
		{
			return ViewContent.ct.cje;
		}
		if (!flag && !flag2 && !flag3 && !flag4)
		{
			return ViewContent.ct.cjc;
		}
		return ViewContent.ct.cjd;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00011ECC File Offset: 0x000100CC
	public bool vf(RectTransform a)
	{
		Vector3[] array = new Vector3[4];
		Vector3[] array2 = new Vector3[4];
		a.GetWorldCorners(array);
		this.dau.GetWorldCorners(array2);
		bool flag = array[0].x < array2[0].x;
		bool flag2 = array[2].x > array2[2].x;
		bool flag3 = array[1].y > array2[1].y;
		bool flag4 = array[3].y < array2[3].y;
		return !flag && !flag2 && !flag3 && !flag4;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00011F72 File Offset: 0x00010172
	private void vg(Vector2 a)
	{
		this.vh();
		Action<Vector2> action = this.cji;
		if (action == null)
		{
			return;
		}
		action(a);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00011F8B File Offset: 0x0001018B
	private void vh()
	{
		this.dav = this.ve();
	}

	// Token: 0x040003A4 RID: 932
	[SerializeField]
	private RectTransform TrackingArea;

	// Token: 0x040003A5 RID: 933
	private ScrollRect cjf;

	// Token: 0x040003A6 RID: 934
	private RectTransform cjg;

	// Token: 0x040003A7 RID: 935
	private RectTransform cjh;

	// Token: 0x040003AA RID: 938
	private ViewContent.ct cjk;

	// Token: 0x02000098 RID: 152
	public enum ct
	{
		// Token: 0x040003AC RID: 940
		cjc,
		// Token: 0x040003AD RID: 941
		cjd,
		// Token: 0x040003AE RID: 942
		cje
	}
}
