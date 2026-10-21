using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x0200004C RID: 76
public sealed class bm
{
	// Token: 0x0600016F RID: 367 RVA: 0x00008484 File Offset: 0x00006684
	private static string hkn()
	{
		string result = Application.persistentDataPath;
		string text = Environment.GetEnvironmentVariable(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qi()) ?? string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			result = Path.Combine(new string[]
			{
				text,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qj(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qk(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ql(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qm()
			});
		}
		return result;
	}

	// Token: 0x06000171 RID: 369
	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern uint GetEnvironmentVariable(string lpName, StringBuilder lpBuffer, uint nSize);

	// Token: 0x06000172 RID: 370 RVA: 0x00008558 File Offset: 0x00006758
	private static string kvt()
	{
		string result = Application.persistentDataPath;
		string text = Environment.GetEnvironmentVariable(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qq()) ?? string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			result = Path.Combine(new string[]
			{
				text,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qr(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qs(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qt(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qu()
			});
		}
		return result;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000085B8 File Offset: 0x000067B8
	private static string enr()
	{
		string result = Application.persistentDataPath;
		string text = Environment.GetEnvironmentVariable(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qv()) ?? string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			result = Path.Combine(new string[]
			{
				text,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qw(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qx(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qy(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qz()
			});
		}
		return result;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00008618 File Offset: 0x00006818
	private static string kf()
	{
		string result = Application.persistentDataPath;
		string text = Environment.GetEnvironmentVariable(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ra()) ?? string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			result = Path.Combine(new string[]
			{
				text,
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rb(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rc(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.rd(),
				<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.mv()
			});
		}
		return result;
	}

	// Token: 0x040001BB RID: 443
	public static readonly string cao = Path.Combine(bm.kf(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qn());

	// Token: 0x040001BC RID: 444
	public static readonly string cap = Path.Combine(bm.cao, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qo());

	// Token: 0x040001BD RID: 445
	public static readonly string caq = Path.Combine(bm.kf(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bb());

	// Token: 0x040001BE RID: 446
	public static readonly string car = Path.Combine(bm.caq, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bc());

	// Token: 0x040001BF RID: 447
	public static readonly string cas = Path.Combine(bm.cao, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.qp());
}
