using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Ookii.Dialogs;

// Token: 0x020000F9 RID: 249
public class ef : eb
{
	// Token: 0x06000693 RID: 1683 RVA: 0x0001CE5C File Offset: 0x0001B05C
	private static string ghm(ec[] a)
	{
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bci();
		foreach (ec ec in a)
		{
			text = text + ec.cti + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcj();
			foreach (string str in ec.ctj)
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bck() + str + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcl();
			}
			text = text.Remove(text.Length - 1);
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcm();
			foreach (string str2 in ec.ctj)
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcn() + str2 + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bco();
			}
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcp();
		}
		return text.Remove(text.Length - 1);
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0001CF40 File Offset: 0x0001B140
	public string[] bjt(string a, string b, ec[] c, bool d)
	{
		VistaOpenFileDialog vistaOpenFileDialog = new VistaOpenFileDialog();
		vistaOpenFileDialog.Title = a;
		if (c != null)
		{
			vistaOpenFileDialog.Filter = ef.bkj(c);
			vistaOpenFileDialog.FilterIndex = 1;
		}
		else
		{
			vistaOpenFileDialog.Filter = string.Empty;
		}
		vistaOpenFileDialog.Multiselect = d;
		if (!string.IsNullOrEmpty(b))
		{
			vistaOpenFileDialog.FileName = ef.bkk(b);
		}
		string[] result = (vistaOpenFileDialog.ShowDialog(new ee(ef.GetActiveWindow())) == DialogResult.OK) ? vistaOpenFileDialog.FileNames : new string[0];
		vistaOpenFileDialog.Dispose();
		return result;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
	private static string bkj(ec[] a)
	{
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		foreach (ec ec in a)
		{
			text = text + ec.cti + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.yb();
			foreach (string str in ec.ctj)
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcq() + str + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fy();
			}
			text = text.Remove(text.Length - 1);
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcr();
			foreach (string str2 in ec.ctj)
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcq() + str2 + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcs();
			}
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bct();
		}
		return text.Remove(text.Length - 1);
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
	public string[] bju(string a, string b, bool c)
	{
		VistaFolderBrowserDialog vistaFolderBrowserDialog = new VistaFolderBrowserDialog();
		vistaFolderBrowserDialog.Description = a;
		if (!string.IsNullOrEmpty(b))
		{
			vistaFolderBrowserDialog.SelectedPath = ef.bkk(b);
		}
		string[] result;
		if (vistaFolderBrowserDialog.ShowDialog(new ee(ef.GetActiveWindow())) != DialogResult.OK)
		{
			result = new string[0];
		}
		else
		{
			(result = new string[1])[0] = vistaFolderBrowserDialog.SelectedPath;
		}
		vistaFolderBrowserDialog.Dispose();
		return result;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0001D104 File Offset: 0x0001B304
	private static string fyr(ec[] a)
	{
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcu();
		foreach (ec ec in a)
		{
			text = text + ec.cti + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcv();
			foreach (string str in ec.ctj)
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcw() + str + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcx();
			}
			text = text.Remove(text.Length - 1);
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcy();
			foreach (string str2 in ec.ctj)
			{
				text = text + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bcz() + str2 + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bda();
			}
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdb();
		}
		return text.Remove(text.Length - 1);
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
	public string bjv(string a, string b, string c, ec[] d)
	{
		VistaSaveFileDialog vistaSaveFileDialog = new VistaSaveFileDialog();
		vistaSaveFileDialog.Title = a;
		string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		if (!string.IsNullOrEmpty(b))
		{
			text = ef.bkk(b);
		}
		if (!string.IsNullOrEmpty(c))
		{
			text += c;
		}
		vistaSaveFileDialog.FileName = text;
		if (d != null)
		{
			vistaSaveFileDialog.Filter = ef.bkj(d);
			vistaSaveFileDialog.FilterIndex = 1;
			vistaSaveFileDialog.DefaultExt = d[0].ctj[0];
			vistaSaveFileDialog.AddExtension = true;
		}
		else
		{
			vistaSaveFileDialog.DefaultExt = string.Empty;
			vistaSaveFileDialog.Filter = string.Empty;
			vistaSaveFileDialog.AddExtension = false;
		}
		string result = (vistaSaveFileDialog.ShowDialog(new ee(ef.GetActiveWindow())) == DialogResult.OK) ? vistaSaveFileDialog.FileName : <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
		vistaSaveFileDialog.Dispose();
		return result;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0001D2A9 File Offset: 0x0001B4A9
	public void bjx(string a, string b, bool c, Action<string[]> d)
	{
		d(this.bju(a, b, c));
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0001D2BC File Offset: 0x0001B4BC
	private static string bkk(string a)
	{
		string text = Path.GetFullPath(a);
		if (!text.EndsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdc()))
		{
			text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdc();
		}
		if (Path.GetPathRoot(text) == text)
		{
			return a;
		}
		return Path.GetDirectoryName(text) + Path.DirectorySeparatorChar.ToString();
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0001D311 File Offset: 0x0001B511
	public void bjy(string a, string b, string c, ec[] d, Action<string> e)
	{
		e(this.bjv(a, b, c, d));
	}

	// Token: 0x0600069C RID: 1692
	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	// Token: 0x0600069E RID: 1694 RVA: 0x0001D325 File Offset: 0x0001B525
	public void bjw(string a, string b, ec[] c, bool d, Action<string[]> e)
	{
		e(this.bjt(a, b, c, d));
	}
}
