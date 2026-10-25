using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepthTransformer.NamedPipe
{
	// Token: 0x0200002A RID: 42
	public class NamedPipeClient : IDisposable
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000C7FB File Offset: 0x0000A9FB
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000C803 File Offset: 0x0000AA03
		public bool IsConnected { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000C80C File Offset: 0x0000AA0C
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000C814 File Offset: 0x0000AA14
		public StatusCallBack Callback { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000C81D File Offset: 0x0000AA1D
		public static NamedPipeClient Inst
		{
			get
			{
				return NamedPipeClient._instance.Value;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C829 File Offset: 0x0000AA29
		public NamedPipeClient()
		{
			this.Init();
			this.Connect();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C85D File Offset: 0x0000AA5D
		private void Init()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C860 File Offset: 0x0000AA60
		public bool Connect()
		{
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				if (this.IsConnected)
				{
					result = true;
				}
				else
				{
					try
					{
						this._pipeStream = new NamedPipeClientStream(".", "BOE3DAssistantNamedPipe", PipeDirection.InOut, PipeOptions.Asynchronous);
						this._pipeStream.Connect(1000);
						this._writer = new StreamWriter(this._pipeStream, Encoding.UTF8)
						{
							AutoFlush = true
						};
						this.IsConnected = true;
						this._stereoscopicDisplay = true;
						this._cts = new CancellationTokenSource();
						string location = Assembly.GetExecutingAssembly().Location;
						this.Send("client_path:" + location);
						result = true;
					}
					catch (Exception ex)
					{
						Console.WriteLine("[NamedPipe] 连接失败: " + ex.Message);
						this.Cleanup();
						this._stereoscopicDisplay = false;
						this.IsConnected = false;
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C96C File Offset: 0x0000AB6C
		public void Close()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.IsConnected = false;
				this.Cleanup();
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		public void Open3D()
		{
			this.Send("open3d");
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C9C1 File Offset: 0x0000ABC1
		public void Close3D()
		{
			this.Send("close3d");
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C9CE File Offset: 0x0000ABCE
		public void OpenMouse()
		{
			this.Send("V_MOUSE_ON");
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C9DB File Offset: 0x0000ABDB
		public void CloseMouse()
		{
			this.Send("V_MOUSE_OFF");
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public void Send(bool isOpen)
		{
			if (!this.IsConnected)
			{
				return;
			}
			if (isOpen)
			{
				this.Open3D();
				return;
			}
			this.Close3D();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000CA03 File Offset: 0x0000AC03
		public void SendMouse(bool isOpen)
		{
			if (!this.IsConnected)
			{
				return;
			}
			if (isOpen)
			{
				this.OpenMouse();
				return;
			}
			this.CloseMouse();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public bool Get()
		{
			bool stereoscopicDisplay;
			while (this._messages.TryDequeue(out stereoscopicDisplay))
			{
				this._stereoscopicDisplay = stereoscopicDisplay;
			}
			return this._stereoscopicDisplay;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CA50 File Offset: 0x0000AC50
		private void Send(string text)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.IsConnected && this._writer != null)
				{
					try
					{
						this._writer.WriteLine(text);
					}
					catch (Exception ex)
					{
						Console.WriteLine("[NamedPipe] 发送失败: " + ex.Message);
						this.HandleDisconnect();
					}
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		private void HandleDisconnect()
		{
			this.IsConnected = false;
			this.Cleanup();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		private void Cleanup()
		{
			try
			{
				CancellationTokenSource cts = this._cts;
				if (cts != null)
				{
					cts.Cancel();
				}
				StreamWriter writer = this._writer;
				if (writer != null)
				{
					writer.Dispose();
				}
				NamedPipeClientStream pipeStream = this._pipeStream;
				if (pipeStream != null)
				{
					pipeStream.Dispose();
				}
			}
			catch
			{
			}
			this._writer = null;
			this._pipeStream = null;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public void Dispose()
		{
			this.Close();
			CancellationTokenSource cts = this._cts;
			if (cts != null)
			{
				cts.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x040000F6 RID: 246
		private const string PipeName = "BOE3DAssistantNamedPipe";

		// Token: 0x040000F7 RID: 247
		private NamedPipeClientStream _pipeStream;

		// Token: 0x040000F8 RID: 248
		private StreamWriter _writer;

		// Token: 0x040000F9 RID: 249
		private CancellationTokenSource _cts;

		// Token: 0x040000FA RID: 250
		private Task _receiveTask;

		// Token: 0x040000FB RID: 251
		private readonly ConcurrentQueue<bool> _messages = new ConcurrentQueue<bool>();

		// Token: 0x040000FC RID: 252
		private volatile bool _stereoscopicDisplay = true;

		// Token: 0x040000FD RID: 253
		private readonly object _lock = new object();

		// Token: 0x04000100 RID: 256
		private static readonly Lazy<NamedPipeClient> _instance = new Lazy<NamedPipeClient>(() => new NamedPipeClient());
	}
}
