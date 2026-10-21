using System;
using System.Runtime.CompilerServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x020000EA RID: 234
public class BasicSample : MonoBehaviour
{
	// Token: 0x06000639 RID: 1593 RVA: 0x0001C074 File Offset: 0x0001A274
	private void OnGUI()
	{
		Vector3 vector;
		vector..ctor((float)Screen.width / 800f, (float)Screen.height / 600f, 1f);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, vector);
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Space(20f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), Array.Empty<GUILayoutOption>()))
		{
			this.biy(ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), false));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bau(), Array.Empty<GUILayoutOption>()))
		{
			ed.bkb(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), false, new Action<string[]>(this.bja));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bav(), Array.Empty<GUILayoutOption>()))
		{
			this.biy(ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), true));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baw(), Array.Empty<GUILayoutOption>()))
		{
			this.biy(ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bax(), true));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bay(), Array.Empty<GUILayoutOption>()))
		{
			this.biy(ed.bjz(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), Application.dataPath, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), true));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.baz(), Array.Empty<GUILayoutOption>()))
		{
			ec[] c = new ec[]
			{
				new ec(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bba(), new string[]
				{
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbb(),
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbc(),
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbd()
				}),
				new ec(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbe(), new string[]
				{
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbf(),
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbg()
				}),
				new ec(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbh(), new string[]
				{
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbi()
				})
			};
			this.biy(ed.bka(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bat(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), c, true));
		}
		GUILayout.Space(15f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbj(), Array.Empty<GUILayoutOption>()))
		{
			string[] a = ed.bkd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbk(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), true);
			this.biy(a);
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbl(), Array.Empty<GUILayoutOption>()))
		{
			ed.bke(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbk(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), true, new Action<string[]>(this.bjb));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbm(), Array.Empty<GUILayoutOption>()))
		{
			string[] a2 = ed.bkd(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbk(), Application.dataPath, true);
			this.biy(a2);
		}
		GUILayout.Space(15f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), Array.Empty<GUILayoutOption>()))
		{
			this.csj = ed.bkf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbo(), Array.Empty<GUILayoutOption>()))
		{
			ed.bkh(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), new Action<string>(this.bjc));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbp(), Array.Empty<GUILayoutOption>()))
		{
			this.csj = ed.bkf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbq(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbr(), Array.Empty<GUILayoutOption>()))
		{
			this.csj = ed.bkf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbq(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbs());
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbt(), Array.Empty<GUILayoutOption>()))
		{
			this.csj = ed.bkf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), Application.dataPath, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbu(), Array.Empty<GUILayoutOption>()))
		{
			ec[] d = new ec[]
			{
				new ec(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbv(), new string[]
				{
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbw()
				}),
				new ec(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ge(), new string[]
				{
					<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bax()
				})
			};
			this.csj = ed.bkg(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bbq(), d);
		}
		GUILayout.EndVertical();
		GUILayout.Space(20f);
		GUILayout.Label(this.csj, Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0001C504 File Offset: 0x0001A704
	public void biy(string[] a)
	{
		if (a.Length == 0)
		{
			return;
		}
		this.csj = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		foreach (string str in a)
		{
			this.csj = this.csj + str + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.lu();
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0001C54C File Offset: 0x0001A74C
	public void biz(string a)
	{
		this.csj = a;
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0001C555 File Offset: 0x0001A755
	[CompilerGenerated]
	private void bja(string[] a)
	{
		this.biy(a);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0001C555 File Offset: 0x0001A755
	[CompilerGenerated]
	private void bjb(string[] a)
	{
		this.biy(a);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0001C55E File Offset: 0x0001A75E
	[CompilerGenerated]
	private void bjc(string a)
	{
		this.biz(a);
	}

	// Token: 0x04000574 RID: 1396
	private string csj;
}
