using System;
using System.Threading;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using SRUnity;
using UnityEngine;

// Token: 0x02000029 RID: 41
internal class LeiaPluginHelper
{
	// Token: 0x060000C5 RID: 197 RVA: 0x000060AC File Offset: 0x000042AC
	public static void hy(CancellationToken a)
	{
		while (!a.IsCancellationRequested)
		{
			Thread.Sleep(500);
			IntPtr intPtr = bl.iy();
			if (intPtr != IntPtr.Zero && bl.caj != IntPtr.Zero)
			{
				if (intPtr == bl.caj)
				{
					LeiaPluginHelper.byx.BecomeIndifferent();
				}
				else
				{
					bl.bg bg = bl.ja(intPtr);
					if ((bg.bzk - bg.bzi) * (bg.bzl - bg.bzj) > 518400)
					{
						LeiaPluginHelper.byx.Force2D();
					}
				}
			}
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00006144 File Offset: 0x00004344
	public static void StartLeiaModules()
	{
		object obj = LeiaPluginHelper.byy;
		lock (obj)
		{
			bd.id(LeiaPluginHelper.byw, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kh(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ki());
			if (SystemHandler.Instance != null)
			{
				SystemHandler.Instance.StartModules();
			}
			LeiaPluginHelper.Set3DMode(true);
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000061B0 File Offset: 0x000043B0
	public static void DestroyLeiaModules()
	{
		object obj = LeiaPluginHelper.byy;
		lock (obj)
		{
			bd.id(LeiaPluginHelper.byw, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kk());
			LeiaPluginHelper.Set3DMode(false);
			if (SystemHandler.Instance != null)
			{
				SystemHandler.Instance.DestroyModules();
			}
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000621C File Offset: 0x0000441C
	public static void Set3DMode(bool toggle)
	{
		bd.ih(LeiaPluginHelper.byw, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.kl());
		if (LeiaPluginHelper.byq)
		{
			LeiaPluginHelper.byq.Set3DMode(toggle);
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00006244 File Offset: 0x00004444
	public static Vector3 hz()
	{
		Vector3[] lookAroundEyesPosition = Singleton<RenderTrackingDevice>.Instance.GetLookAroundEyesPosition();
		return (lookAroundEyesPosition[0] + lookAroundEyesPosition[1]) / 2f;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00006279 File Offset: 0x00004479
	public static Vector3 ia()
	{
		return LeiaPluginHelper.byv;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00006288 File Offset: 0x00004488
	public static Vector2 ib(Vector3 a)
	{
		float x = Singleton<RenderTrackingDevice>.Instance.GetDisplaySizeInMM().x;
		float y = Singleton<RenderTrackingDevice>.Instance.GetDisplaySizeInMM().y;
		int x2 = Singleton<RenderTrackingDevice>.Instance.GetDevicePanelResolution().x;
		int y2 = Singleton<RenderTrackingDevice>.Instance.GetDevicePanelResolution().y;
		LeiaPluginHelper.byr = new Vector2(-x / 2f, x / 2f);
		LeiaPluginHelper.bys = new Vector2(-y / 2f, y / 2f);
		LeiaPluginHelper.byt = new Vector2(0f, (float)x2);
		LeiaPluginHelper.byu = new Vector2(0f, (float)y2);
		LeiaPluginHelper.byv = new Vector2(LeiaPluginHelper.byt.y, LeiaPluginHelper.byu.y);
		float num = LeiaPluginHelper.ic(LeiaPluginHelper.byr, LeiaPluginHelper.byt, a.x);
		float num2 = LeiaPluginHelper.ic(LeiaPluginHelper.bys, LeiaPluginHelper.byu, -a.y);
		return new Vector2(num, num2);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00006384 File Offset: 0x00004584
	private static float ic(Vector2 a, Vector2 b, float c)
	{
		float num = Mathf.Clamp01(Mathf.InverseLerp(a.x, a.y, c));
		return Mathf.Lerp(b.x, b.y, num);
	}

	// Token: 0x040000CD RID: 205
	private static LeiaDisplay byq = new LeiaDisplay();

	// Token: 0x040000CE RID: 206
	private static Vector2 byr;

	// Token: 0x040000CF RID: 207
	private static Vector2 bys;

	// Token: 0x040000D0 RID: 208
	private static Vector2 byt;

	// Token: 0x040000D1 RID: 209
	private static Vector2 byu;

	// Token: 0x040000D2 RID: 210
	private static Vector2 byv;

	// Token: 0x040000D3 RID: 211
	private static string byw = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.km();

	// Token: 0x040000D4 RID: 212
	private static readonly SrRenderModeHint byx = new SrRenderModeHint();

	// Token: 0x040000D5 RID: 213
	private static readonly object byy = new object();
}
