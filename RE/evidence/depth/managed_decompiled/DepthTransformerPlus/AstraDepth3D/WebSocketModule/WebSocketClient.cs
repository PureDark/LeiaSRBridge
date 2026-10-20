using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AstraDepth3D.WebSocketModule
{
	// Token: 0x02000026 RID: 38
	public class WebSocketClient : IDisposable
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000C040 File Offset: 0x0000A240
		public WebSocketClient(WebSocketClient.OnReceiveMessage action)
		{
			this.handleReceiveMessage = action;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000C066 File Offset: 0x0000A266
		public string GenerateMsg(string key, int status)
		{
			JObject jobject = new JObject();
			jobject.Add(key, status);
			return jobject.ToString();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C080 File Offset: 0x0000A280
		public Task SendMessageAsync(string message)
		{
			WebSocketClient.<SendMessageAsync>d__7 <SendMessageAsync>d__;
			<SendMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendMessageAsync>d__.<>4__this = this;
			<SendMessageAsync>d__.message = message;
			<SendMessageAsync>d__.<>1__state = -1;
			<SendMessageAsync>d__.<>t__builder.Start<WebSocketClient.<SendMessageAsync>d__7>(ref <SendMessageAsync>d__);
			return <SendMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		private void ReceiveMessage(string message)
		{
			Console.WriteLine("收到消息: " + message);
			try
			{
				string pattern = "(\\\\[^bfrnt\\\\/'\\\"])";
				JObject jsonObject = JsonConvert.DeserializeObject<JObject>(Regex.Replace(message, pattern, "\\$1"));
				if (jsonObject != null && jsonObject.ContainsKey("what"))
				{
					int what = (int)jsonObject["what"];
					if (this.handleReceiveMessage != null)
					{
						this.handleReceiveMessage(what);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString() + DateTime.Now.ToString() + "\n");
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C16C File Offset: 0x0000A36C
		public Task Run(string uri, CancellationToken cancellationToken)
		{
			WebSocketClient.<Run>d__9 <Run>d__;
			<Run>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Run>d__.<>4__this = this;
			<Run>d__.uri = uri;
			<Run>d__.cancellationToken = cancellationToken;
			<Run>d__.<>1__state = -1;
			<Run>d__.<>t__builder.Start<WebSocketClient.<Run>d__9>(ref <Run>d__);
			return <Run>d__.<>t__builder.Task;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C1BF File Offset: 0x0000A3BF
		public void Dispose()
		{
			ClientWebSocket clientWebSocket = this.webSocket;
			if (clientWebSocket == null)
			{
				return;
			}
			clientWebSocket.Dispose();
		}

		// Token: 0x040000C5 RID: 197
		public WebSocketClient.OnReceiveMessage handleReceiveMessage;

		// Token: 0x040000C6 RID: 198
		private ClientWebSocket webSocket;

		// Token: 0x040000C7 RID: 199
		private Queue<string> sendQueue = new Queue<string>();

		// Token: 0x040000C8 RID: 200
		private SemaphoreSlim sendSemaphore = new SemaphoreSlim(1);

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x06000424 RID: 1060
		public delegate void OnReceiveMessage(int what);
	}
}
