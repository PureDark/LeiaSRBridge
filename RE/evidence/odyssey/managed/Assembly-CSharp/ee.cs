using System;
using System.Windows.Forms;

// Token: 0x020000F8 RID: 248
public class ee : IWin32Window
{
	// Token: 0x06000691 RID: 1681 RVA: 0x0001CE45 File Offset: 0x0001B045
	public ee(IntPtr a)
	{
		this.ctl = a;
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001CE54 File Offset: 0x0001B054
	public IntPtr Handle
	{
		get
		{
			return this.ctl;
		}
	}

	// Token: 0x04000595 RID: 1429
	private IntPtr ctl;
}
