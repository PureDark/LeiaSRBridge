using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000011 RID: 17
public static class SharedMemoryManager
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000031 RID: 49 RVA: 0x00003496 File Offset: 0x00001696
	// (set) Token: 0x06000032 RID: 50 RVA: 0x0000349D File Offset: 0x0000169D
	public static bool cyk
	{
		get
		{
			return SharedMemoryManager.bvd;
		}
		set
		{
			SharedMemoryManager.bvd = value;
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000034A5 File Offset: 0x000016A5
	static SharedMemoryManager()
	{
		SharedMemoryManager.InitializeSharedMemory();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000034AC File Offset: 0x000016AC
	private static void InitializeSharedMemory()
	{
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fc(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fc());
		SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
		MutexSecurity mutexSecurity = new MutexSecurity();
		mutexSecurity.AddAccessRule(new MutexAccessRule(identity, MutexRights.FullControl, AccessControlType.Allow));
		SharedMemoryManager.buz = new Mutex(false, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fd());
		try
		{
			SharedMemoryManager.buz.SetAccessControl(mutexSecurity);
		}
		catch
		{
		}
		EventWaitHandleSecurity eventWaitHandleSecurity = new EventWaitHandleSecurity();
		eventWaitHandleSecurity.AddAccessRule(new EventWaitHandleAccessRule(identity, EventWaitHandleRights.FullControl, AccessControlType.Allow));
		SharedMemoryManager.bva = SharedMemoryManager.CreateEventWithSecurity(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fe(), eventWaitHandleSecurity);
		SharedMemoryManager.bvb = SharedMemoryManager.CreateEventWithSecurity(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ff(), eventWaitHandleSecurity);
		SharedMemoryManager.bux = MemoryMappedFile.CreateOrOpen(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fg(), 1024L);
		SharedMemoryManager.buy = SharedMemoryManager.bux.CreateViewAccessor();
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000357C File Offset: 0x0000177C
	private static EventWaitHandle CreateEventWithSecurity(string name, EventWaitHandleSecurity security)
	{
		EventWaitHandle result;
		try
		{
			EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, name);
			try
			{
				eventWaitHandle.SetAccessControl(security);
			}
			catch
			{
				bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fh(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fi());
			}
			result = eventWaitHandle;
		}
		catch
		{
			result = EventWaitHandle.OpenExisting(name);
		}
		return result;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000035E0 File Offset: 0x000017E0
	public static void SendData(SharedMemoryManager.j command, string data = "")
	{
		SharedMemoryManager.k k = new SharedMemoryManager.k();
		k.bup = data;
		k.buq = command;
		if (!bl.jh())
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fj(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ez());
			return;
		}
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fk(), (int)k.buq), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ez());
		Task.Run(new Action(k.fq));
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003654 File Offset: 0x00001854
	public static void StartReceiver()
	{
		if (SharedMemoryManager.bve)
		{
			bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fl(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fm());
			return;
		}
		SharedMemoryManager.bvc = new CancellationTokenSource();
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fm(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fm());
		SharedMemoryManager.bve = true;
		Task.Run(new Action(SharedMemoryManager.<>c.<>9.fp));
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000036C8 File Offset: 0x000018C8
	private static void RunReceiverRoutine(CancellationToken token)
	{
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fn(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
		while (!token.IsCancellationRequested)
		{
			try
			{
				if (SharedMemoryManager.bvb.WaitOne(-1))
				{
					SharedMemoryManager.buz.WaitOne();
					try
					{
						SharedMemoryManager.j j = (SharedMemoryManager.j)SharedMemoryManager.buy.ReadInt32(0L);
						int num = SharedMemoryManager.buy.ReadInt32(4L);
						if (num >= 0 && num <= 1016)
						{
							byte[] array = new byte[num];
							SharedMemoryManager.buy.ReadArray<byte>(8L, array, 0, num);
							Encoding.UTF8.GetString(array);
							switch (j)
							{
							case SharedMemoryManager.j.buj:
								bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fr(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
								break;
							case SharedMemoryManager.j.buk:
								bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fp(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
								if (!bl.cym)
								{
									bl.jd();
									CancellationTokenSource cancellationTokenSource = SharedMemoryManager.bvc;
									if (cancellationTokenSource != null)
									{
										cancellationTokenSource.Cancel();
									}
								}
								break;
							case SharedMemoryManager.j.bul:
								bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fq(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
								SharedMemoryManager.cyk = false;
								if (Application.runInBackground)
								{
									bl.je();
								}
								break;
							case SharedMemoryManager.j.bum:
								bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fs(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
								SharedMemoryManager.cyk = true;
								break;
							default:
								bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ft(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
								break;
							}
						}
					}
					finally
					{
						SharedMemoryManager.buz.ReleaseMutex();
					}
				}
			}
			catch (Exception ex)
			{
				bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fu() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
			}
		}
		SharedMemoryManager.bve = false;
		bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fv(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fo());
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000038A8 File Offset: 0x00001AA8
	public static void ft()
	{
		CancellationTokenSource cancellationTokenSource = SharedMemoryManager.bvc;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		MemoryMappedViewAccessor memoryMappedViewAccessor = SharedMemoryManager.buy;
		if (memoryMappedViewAccessor != null)
		{
			memoryMappedViewAccessor.Dispose();
		}
		MemoryMappedFile memoryMappedFile = SharedMemoryManager.bux;
		if (memoryMappedFile != null)
		{
			memoryMappedFile.Dispose();
		}
		Mutex mutex = SharedMemoryManager.buz;
		if (mutex != null)
		{
			mutex.Dispose();
		}
		EventWaitHandle eventWaitHandle = SharedMemoryManager.bva;
		if (eventWaitHandle != null)
		{
			eventWaitHandle.Dispose();
		}
		EventWaitHandle eventWaitHandle2 = SharedMemoryManager.bvb;
		if (eventWaitHandle2 != null)
		{
			eventWaitHandle2.Dispose();
		}
		SharedMemoryManager.bve = false;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000391C File Offset: 0x00001B1C
	public static void SendHotkeysData(string[] hotkeys)
	{
		if (hotkeys == null || hotkeys.Length != 6)
		{
			bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fw(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fx());
			return;
		}
		string data = string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fy(), hotkeys);
		SharedMemoryManager.SendData(SharedMemoryManager.j.buj, data);
	}

	// Token: 0x0400004F RID: 79
	private const string bur = "SharedMemoryManager";

	// Token: 0x04000050 RID: 80
	private const string bus = "O3H_MemoryMap";

	// Token: 0x04000051 RID: 81
	private const string but = "O3H_MemoryMap_Mutex";

	// Token: 0x04000052 RID: 82
	private const string buu = "O3H_MemoryMap_Send_Event";

	// Token: 0x04000053 RID: 83
	private const string buv = "O3H_MemoryMap_Receive_Event";

	// Token: 0x04000054 RID: 84
	private const int buw = 1024;

	// Token: 0x04000055 RID: 85
	private static MemoryMappedFile bux;

	// Token: 0x04000056 RID: 86
	private static MemoryMappedViewAccessor buy;

	// Token: 0x04000057 RID: 87
	private static Mutex buz;

	// Token: 0x04000058 RID: 88
	private static EventWaitHandle bva;

	// Token: 0x04000059 RID: 89
	private static EventWaitHandle bvb;

	// Token: 0x0400005A RID: 90
	private static CancellationTokenSource bvc;

	// Token: 0x0400005B RID: 91
	private static bool bvd;

	// Token: 0x0400005C RID: 92
	private static bool bve;

	// Token: 0x02000012 RID: 18
	public enum j
	{
		// Token: 0x0400005E RID: 94
		buj,
		// Token: 0x0400005F RID: 95
		buk,
		// Token: 0x04000060 RID: 96
		bul,
		// Token: 0x04000061 RID: 97
		bum,
		// Token: 0x04000062 RID: 98
		bun,
		// Token: 0x04000063 RID: 99
		buo
	}

	// Token: 0x02000014 RID: 20
	[CompilerGenerated]
	private sealed class k
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003978 File Offset: 0x00001B78
		internal void kfr()
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.bup ?? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ed());
				if (bytes.Length > 19)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ee(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ef(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.eg());
				}
				else
				{
					SharedMemoryManager.buz.WaitOne();
					try
					{
						SharedMemoryManager.buy.Write(0L, (int)this.buq);
						SharedMemoryManager.buy.Write(4L, bytes.Length);
						SharedMemoryManager.buy.WriteArray<byte>(8L, bytes, 0, bytes.Length);
					}
					finally
					{
						SharedMemoryManager.buz.ReleaseMutex();
					}
					SharedMemoryManager.bva.Set();
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.eh(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ei(), (int)this.buq), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ej());
				}
			}
			catch (Exception ex)
			{
				bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ek(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.el() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.em());
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003A78 File Offset: 0x00001C78
		internal void dba()
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.bup ?? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.en());
				if (bytes.Length > -6)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.eo(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ep(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.eq());
				}
				else
				{
					SharedMemoryManager.buz.WaitOne();
					try
					{
						SharedMemoryManager.buy.Write(0L, (int)this.buq);
						SharedMemoryManager.buy.Write(4L, bytes.Length);
						SharedMemoryManager.buy.WriteArray<byte>(8L, bytes, 0, bytes.Length);
					}
					finally
					{
						SharedMemoryManager.buz.ReleaseMutex();
					}
					SharedMemoryManager.bva.Set();
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.er(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.es(), (int)this.buq), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.et());
				}
			}
			catch (Exception ex)
			{
				bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.eu(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ev() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ew());
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003B78 File Offset: 0x00001D78
		internal void fq()
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.bup ?? <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m());
				if (bytes.Length > 1016)
				{
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ey(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ez());
				}
				else
				{
					SharedMemoryManager.buz.WaitOne();
					try
					{
						SharedMemoryManager.buy.Write(0L, (int)this.buq);
						SharedMemoryManager.buy.Write(4L, bytes.Length);
						SharedMemoryManager.buy.WriteArray<byte>(8L, bytes, 0, bytes.Length);
					}
					finally
					{
						SharedMemoryManager.buz.ReleaseMutex();
					}
					SharedMemoryManager.bva.Set();
					bd.id(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fa(), (int)this.buq), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ez());
				}
			}
			catch (Exception ex)
			{
				bd.ie(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ex(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.fb() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ez());
			}
		}

		// Token: 0x04000066 RID: 102
		public string bup;

		// Token: 0x04000067 RID: 103
		public SharedMemoryManager.j buq;
	}
}
