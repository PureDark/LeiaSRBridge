using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};

// Token: 0x02000009 RID: 9
internal class Odyssey3DHubIPCManager
{
	// Token: 0x0600001D RID: 29 RVA: 0x000028EF File Offset: 0x00000AEF
	public static bool IsClientConnected()
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.du());
		return Odyssey3DHubIPCManager.bug != null && Odyssey3DHubIPCManager.buh != null && Odyssey3DHubIPCManager.bui != null && Odyssey3DHubIPCManager.bug.IsConnected;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002921 File Offset: 0x00000B21
	public static void StartClient()
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bt());
		Task.Factory.StartNew<Task>(new Func<Task>(Odyssey3DHubIPCManager.<>c.<>9.fm));
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000295C File Offset: 0x00000B5C
	public static void fo()
	{
		if (Odyssey3DHubIPCManager.bug != null)
		{
			Odyssey3DHubIPCManager.bug.Dispose();
			Odyssey3DHubIPCManager.bug = null;
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002978 File Offset: 0x00000B78
	private static Task ReadMessagesAsync()
	{
		Odyssey3DHubIPCManager.h h;
		h.bub = AsyncTaskMethodBuilder.Create();
		h.bua = -1;
		h.bub.Start<Odyssey3DHubIPCManager.h>(ref h);
		return h.bub.Task;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000029B4 File Offset: 0x00000BB4
	private static void ParseMessage(string msg)
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dv());
		if (!msg.StartsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dw()))
		{
			return;
		}
		string[] array = msg.Split(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dx(), StringSplitOptions.None);
		if (array.Length < 4)
		{
			return;
		}
		try
		{
			Odyssey3DHubIPCManager.c c = (Odyssey3DHubIPCManager.c)int.Parse(array[1]);
			Odyssey3DHubIPCManager.d d = (Odyssey3DHubIPCManager.d)int.Parse(array[2]);
			switch (c)
			{
			case Odyssey3DHubIPCManager.c.bsg:
				if (d == Odyssey3DHubIPCManager.d.bsp)
				{
					LoadingGamePopup.gameStarted = true;
					Thread.Sleep(300);
					bl.jc();
				}
				break;
			case Odyssey3DHubIPCManager.c.bsh:
				if (d == Odyssey3DHubIPCManager.d.bsr)
				{
					bn.cyp.cys = array[3].Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dy());
				}
				else if (d == Odyssey3DHubIPCManager.d.bss)
				{
					bl.jb();
					IntroOutroAnimation.cet.StartIntroAnimation();
				}
				else if (d == Odyssey3DHubIPCManager.d.bst)
				{
					bl.cak = new IntPtr(long.Parse(array[3]));
					SharedMemoryManager.StartReceiver();
				}
				break;
			case Odyssey3DHubIPCManager.c.bsi:
				if (d == Odyssey3DHubIPCManager.d.bsv)
				{
					App.deviceStatus = (Odyssey3DHubIPCManager.e)int.Parse(array[3]);
				}
				else if (d == Odyssey3DHubIPCManager.d.bsw)
				{
					bn.cyp.cyt = array[3].Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dy());
				}
				break;
			case Odyssey3DHubIPCManager.c.bsl:
				if (d == Odyssey3DHubIPCManager.d.btb)
				{
					bn.cyp.cyr.cyy = (array[3].Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dz()) ? 0 : 1);
				}
				else if (d == Odyssey3DHubIPCManager.d.btc)
				{
					bn.cyp.cyr.czb = (array[3].Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dz()) ? 0 : 1);
				}
				else if (d == Odyssey3DHubIPCManager.d.btd)
				{
					bn.cyp.cyr.czc = (array[3].Equals(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dz()) ? 0 : 1);
				}
				break;
			}
		}
		catch (Exception ex)
		{
			bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dv());
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002B9C File Offset: 0x00000D9C
	public static void SendMessage(Odyssey3DHubIPCManager.c type, Odyssey3DHubIPCManager.d subCMD, string message = "")
	{
		Odyssey3DHubIPCManager.g g = new Odyssey3DHubIPCManager.g();
		g.btx = message;
		g.bty = type;
		g.btz = subCMD;
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@do());
		Odyssey3DHubIPCManager.buf = true;
		Task.Run(new Action(g.fn));
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002BE9 File Offset: 0x00000DE9
	private static string AddHeader(Odyssey3DHubIPCManager.c type, Odyssey3DHubIPCManager.d subCMD, string message = "None")
	{
		bd.ih(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ea());
		return string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.eb(), new object[]
		{
			<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ec(),
			(int)type,
			(int)subCMD,
			message
		});
	}

	// Token: 0x04000012 RID: 18
	private const string bud = "Odyssey3DHubIPCManager";

	// Token: 0x04000013 RID: 19
	private const string bue = "OH";

	// Token: 0x04000014 RID: 20
	public static bool buf;

	// Token: 0x04000015 RID: 21
	private static NamedPipeClientStream bug;

	// Token: 0x04000016 RID: 22
	private static StreamReader buh;

	// Token: 0x04000017 RID: 23
	private static StreamWriter bui;

	// Token: 0x0200000A RID: 10
	public enum c
	{
		// Token: 0x04000019 RID: 25
		bsf,
		// Token: 0x0400001A RID: 26
		bsg,
		// Token: 0x0400001B RID: 27
		bsh,
		// Token: 0x0400001C RID: 28
		bsi,
		// Token: 0x0400001D RID: 29
		bsj,
		// Token: 0x0400001E RID: 30
		bsk,
		// Token: 0x0400001F RID: 31
		bsl
	}

	// Token: 0x0200000B RID: 11
	public enum d
	{
		// Token: 0x04000021 RID: 33
		bsm,
		// Token: 0x04000022 RID: 34
		bsn,
		// Token: 0x04000023 RID: 35
		bso,
		// Token: 0x04000024 RID: 36
		bsp,
		// Token: 0x04000025 RID: 37
		bsq,
		// Token: 0x04000026 RID: 38
		bsr,
		// Token: 0x04000027 RID: 39
		bss,
		// Token: 0x04000028 RID: 40
		bst,
		// Token: 0x04000029 RID: 41
		bsu,
		// Token: 0x0400002A RID: 42
		bsv,
		// Token: 0x0400002B RID: 43
		bsw,
		// Token: 0x0400002C RID: 44
		bsx,
		// Token: 0x0400002D RID: 45
		bsy,
		// Token: 0x0400002E RID: 46
		bsz,
		// Token: 0x0400002F RID: 47
		bta,
		// Token: 0x04000030 RID: 48
		btb,
		// Token: 0x04000031 RID: 49
		btc,
		// Token: 0x04000032 RID: 50
		btd
	}

	// Token: 0x0200000C RID: 12
	public enum e
	{
		// Token: 0x04000034 RID: 52
		bte = -1,
		// Token: 0x04000035 RID: 53
		btf,
		// Token: 0x04000036 RID: 54
		btg,
		// Token: 0x04000037 RID: 55
		bth,
		// Token: 0x04000038 RID: 56
		bti,
		// Token: 0x04000039 RID: 57
		btj,
		// Token: 0x0400003A RID: 58
		btk,
		// Token: 0x0400003B RID: 59
		btl,
		// Token: 0x0400003C RID: 60
		btm,
		// Token: 0x0400003D RID: 61
		btn,
		// Token: 0x0400003E RID: 62
		bto,
		// Token: 0x0400003F RID: 63
		btp,
		// Token: 0x04000040 RID: 64
		btq,
		// Token: 0x04000041 RID: 65
		btr,
		// Token: 0x04000042 RID: 66
		bts
	}

	// Token: 0x0200000F RID: 15
	[CompilerGenerated]
	private sealed class g
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002E8C File Offset: 0x0000108C
		internal void gzo()
		{
			this.btx = Odyssey3DHubIPCManager.AddHeader(this.bty, this.btz, this.btx);
			int num = 0;
			while (!Odyssey3DHubIPCManager.IsClientConnected())
			{
				Thread.Sleep(-67);
				if (num++ > -48)
				{
					return;
				}
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.by(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bz());
			if (Odyssey3DHubIPCManager.bug != null && Odyssey3DHubIPCManager.bug.IsConnected && Odyssey3DHubIPCManager.bui != null)
			{
				try
				{
					StreamWriter bui = Odyssey3DHubIPCManager.bui;
					lock (bui)
					{
						Odyssey3DHubIPCManager.bui.WriteLine(this.btx);
						bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ca(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cb() + this.btx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cc());
					}
					goto IL_103;
				}
				catch (Exception ex)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cd(), ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ce());
					goto IL_103;
				}
			}
			if (!Odyssey3DHubIPCManager.bug.IsConnected)
			{
				bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cf(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cg(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ch());
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ci(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ck());
			IL_103:
			Odyssey3DHubIPCManager.buf = false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal void gax()
		{
			this.btx = Odyssey3DHubIPCManager.AddHeader(this.bty, this.btz, this.btx);
			int num = 0;
			while (!Odyssey3DHubIPCManager.IsClientConnected())
			{
				Thread.Sleep(-74);
				if (num++ > 46)
				{
					return;
				}
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cl(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cm(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cn());
			if (Odyssey3DHubIPCManager.bug != null && Odyssey3DHubIPCManager.bug.IsConnected && Odyssey3DHubIPCManager.bui != null)
			{
				try
				{
					StreamWriter bui = Odyssey3DHubIPCManager.bui;
					lock (bui)
					{
						Odyssey3DHubIPCManager.bui.WriteLine(this.btx);
						bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.co(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cp() + this.btx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cq());
					}
					goto IL_103;
				}
				catch (Exception ex)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cr(), ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cs());
					goto IL_103;
				}
			}
			if (!Odyssey3DHubIPCManager.bug.IsConnected)
			{
				bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ct(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cu(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cv());
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cw(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cx(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cy());
			IL_103:
			Odyssey3DHubIPCManager.buf = false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000030F4 File Offset: 0x000012F4
		internal void kor()
		{
			this.btx = Odyssey3DHubIPCManager.AddHeader(this.bty, this.btz, this.btx);
			int num = 0;
			while (!Odyssey3DHubIPCManager.IsClientConnected())
			{
				Thread.Sleep(-74);
				if (num++ > 59)
				{
					return;
				}
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.cz(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.da(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.db());
			if (Odyssey3DHubIPCManager.bug != null && Odyssey3DHubIPCManager.bug.IsConnected && Odyssey3DHubIPCManager.bui != null)
			{
				try
				{
					StreamWriter bui = Odyssey3DHubIPCManager.bui;
					lock (bui)
					{
						Odyssey3DHubIPCManager.bui.WriteLine(this.btx);
						bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dc(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dd() + this.btx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.de());
					}
					goto IL_103;
				}
				catch (Exception ex)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.df(), ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dg());
					goto IL_103;
				}
			}
			if (!Odyssey3DHubIPCManager.bug.IsConnected)
			{
				bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dh(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.di(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dj());
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dk(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dl(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dm());
			IL_103:
			Odyssey3DHubIPCManager.buf = false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003228 File Offset: 0x00001428
		internal void fn()
		{
			this.btx = Odyssey3DHubIPCManager.AddHeader(this.bty, this.btz, this.btx);
			int num = 0;
			while (!Odyssey3DHubIPCManager.IsClientConnected())
			{
				Thread.Sleep(100);
				if (num++ > 100)
				{
					return;
				}
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@do());
			if (Odyssey3DHubIPCManager.bug != null && Odyssey3DHubIPCManager.bug.IsConnected && Odyssey3DHubIPCManager.bui != null)
			{
				try
				{
					StreamWriter bui = Odyssey3DHubIPCManager.bui;
					lock (bui)
					{
						Odyssey3DHubIPCManager.bui.WriteLine(this.btx);
						bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dp() + this.btx, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@do());
					}
					goto IL_103;
				}
				catch (Exception ex)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@do());
					goto IL_103;
				}
			}
			if (!Odyssey3DHubIPCManager.bug.IsConnected)
			{
				bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dq(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@do());
			}
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.br(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.dr(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.@do());
			IL_103:
			Odyssey3DHubIPCManager.buf = false;
		}

		// Token: 0x04000049 RID: 73
		public string btx;

		// Token: 0x0400004A RID: 74
		public Odyssey3DHubIPCManager.c bty;

		// Token: 0x0400004B RID: 75
		public Odyssey3DHubIPCManager.d btz;
	}
}
