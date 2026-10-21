using System;
using System.Linq;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class MouseCursor : MonoBehaviour
{
	// Token: 0x060004A0 RID: 1184 RVA: 0x00015FD1 File Offset: 0x000141D1
	public static void ban()
	{
		if (MouseCursor.currentCursor)
		{
			MouseCursor.currentCursor.gameObject.SetActive(true);
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00015FEF File Offset: 0x000141EF
	public static void bao()
	{
		if (MouseCursor.currentCursor)
		{
			MouseCursor.currentCursor.gameObject.SetActive(false);
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00016010 File Offset: 0x00014210
	private void Start()
	{
		bd.ih(MouseCursor.cmp, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		Cursor.visible = false;
		if (MouseCursor.cmr == null)
		{
			if (this.mainCamera.transform.name.Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.st()))
			{
				MouseCursor.cmr = this.mainCamera.transform.GetComponentsInChildren<MouseCursor>(true);
			}
			else
			{
				bd.id(MouseCursor.cmp, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wm() + this.mainCamera.transform.parent.parent.name, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
				MouseCursor.cmr = this.mainCamera.transform.parent.parent.GetComponentsInChildren<MouseCursor>(true);
			}
			bd.id(MouseCursor.cmp, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wn() + this.mainCamera.transform.name, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
		}
		this.cmq = Object.FindAnyObjectByType<EyeTrackingInput>();
		if ((int)(base.gameObject.name.ElementAt(base.gameObject.name.Length - 1) - '0') != bn.cyp.cyr.cyv)
		{
			base.gameObject.SetActive(false);
			return;
		}
		MouseCursor.currentCursor = base.gameObject;
		bd.id(MouseCursor.cmp, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wo() + MouseCursor.currentCursor.transform.name, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bp());
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00016170 File Offset: 0x00014370
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		Vector2 inputPosition = this.cmq.inputPosition;
		Vector2 screenPosition = this.cmq.screenPosition;
		Vector2 vector;
		vector..ctor((screenPosition.x - LeiaPluginHelper.ia().x / 2f) * 0.07f, (screenPosition.y - LeiaPluginHelper.ia().y / 2f) * 0.05f);
		mousePosition.z = this.depth;
		float num = (base.gameObject.name != <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wp()) ? 25f : 0f;
		float num2 = (base.gameObject.name != <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wp()) ? -65f : 0f;
		mousePosition.x = mousePosition.x + num + vector.x;
		mousePosition.y = mousePosition.y + num2 - vector.y;
		Vector3 position = this.mainCamera.ScreenToWorldPoint(mousePosition);
		base.transform.position = position;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001627C File Offset: 0x0001447C
	public static void ChangeCursor(int idx)
	{
		bd.ih(MouseCursor.cmp, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wq());
		if (MouseCursor.currentCursor == null)
		{
			return;
		}
		Vector3 position = MouseCursor.currentCursor.transform.position;
		MouseCursor.currentCursor.SetActive(false);
		MouseCursor.currentCursor = MouseCursor.cmr[idx].transform.gameObject;
		MouseCursor.currentCursor.transform.position = position;
		bn.cyp.cyr.cyv = idx;
		MouseCursor.currentCursor.SetActive(true);
	}

	// Token: 0x04000464 RID: 1124
	private static string cmp = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.wr();

	// Token: 0x04000465 RID: 1125
	public Camera mainCamera;

	// Token: 0x04000466 RID: 1126
	public float depth = 4.8f;

	// Token: 0x04000467 RID: 1127
	public static GameObject currentCursor;

	// Token: 0x04000468 RID: 1128
	private EyeTrackingInput cmq;

	// Token: 0x04000469 RID: 1129
	private static MouseCursor[] cmr;

	// Token: 0x0400046A RID: 1130
	public static int cursorIDX;
}
