using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AstraDepth3D.WindowHelper;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AstraDepth3D.WebSocketModule
{
	// Token: 0x02000027 RID: 39
	internal class WebSocketFleck
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
		public bool existInterWaveProcess()
		{
			return this._sockets.ContainsKey("yl_interWave");
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
		public void set_GlobalOpen_Notify_callback(WebSocketFleck.GlobalOpen_Notify_Delegate callback)
		{
			if (callback != null)
			{
				this.GlobalOpen_Notify_Handler = callback;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C1EF File Offset: 0x0000A3EF
		public void set_Set3DHotkey_Notify_Delegate_callback(WebSocketFleck.Set3DHotkey_Notify_Delegate callback)
		{
			if (callback != null)
			{
				this.Set3DHotkey_Notify_Delegate_Handler = callback;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C1FB File Offset: 0x0000A3FB
		public void set_Query3DHotkey_Notify_Delegate_callback(WebSocketFleck.Query3DHotkey_Notify_Delegate callback)
		{
			if (callback != null)
			{
				this.Query3DHotkey_Notify_Delegate_Handler = callback;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C207 File Offset: 0x0000A407
		public void set_YLClientMsg_Delegate_callback(WebSocketFleck.YLClientMsg_Delegate callback)
		{
			if (callback != null)
			{
				this.YLClientMsg_Handler = callback;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C213 File Offset: 0x0000A413
		public void set_YLMcuVlue_Delegate_callback(WebSocketFleck.YLMcuVlue_Delegate callback)
		{
			if (callback != null)
			{
				this.YLMcuVlue_Handler = callback;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C220 File Offset: 0x0000A420
		public void request_check_client_support_sbsType(string ExeName, string ExePath, WebSocketFleck.GetExe3DInfos_Delegate callback)
		{
			try
			{
				if (callback != null)
				{
					this.GetExe3DInfos_Handler = new WebSocketFleck.GetExe3DInfos_Delegate(callback.Invoke);
					string msg = TencentProtocolHandler.request_client_support_sbsType_msg(ExeName, ExePath);
					this.Broadcast(msg);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("WebSocketServer:request_check_client_support_sbsType has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C288 File Offset: 0x0000A488
		public void broadcast_GlobalOpen_status(int nInterWaveRet)
		{
			try
			{
				string msg = TencentProtocolHandler.broadcast_GlobalOpen_msg(nInterWaveRet);
				this.Broadcast(msg);
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("WebSocketServer:broadcast_GlobalOpen_status has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public void BroadcastMsg(string json_msg)
		{
			try
			{
				this.Broadcast(json_msg);
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("WebSocketServer:broadcast message has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C324 File Offset: 0x0000A524
		public void Response(IWebSocketConnection sock, string json_msg)
		{
			if (sock != null)
			{
				this.SendMsg(sock, json_msg);
				NLogService.WriteLog("Response:" + json_msg);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C344 File Offset: 0x0000A544
		public WebSocketFleck()
		{
			FleckLog.LogAction = delegate(LogLevel level, string message, Exception ex)
			{
				NLogService.WriteLog(string.Format("FleckLog: {0} - {1}-{2}", level, message, (ex != null) ? ex.Message : null));
			};
			this._service = new WebSocketServer("ws://127.0.0.1:20650", true);
			this._service.RestartAfterListenError = true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		private Dictionary<string, string> ParseQueryParameters(string uri)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			string query = new UriBuilder(uri).Query;
			if (!string.IsNullOrEmpty(query))
			{
				query = query.TrimStart(new char[]
				{
					'?'
				});
				string[] array = query.Split(new char[]
				{
					'&'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] keyValue = array[i].Split(new char[]
					{
						'='
					});
					if (keyValue.Length == 2)
					{
						queryParams[keyValue[0]] = Uri.UnescapeDataString(keyValue[1]);
					}
				}
			}
			return queryParams;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C43C File Offset: 0x0000A63C
		public Task Start()
		{
			WebSocketFleck.<Start>d__28 <Start>d__;
			<Start>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Start>d__.<>4__this = this;
			<Start>d__.<>1__state = -1;
			<Start>d__.<>t__builder.Start<WebSocketFleck.<Start>d__28>(ref <Start>d__);
			return <Start>d__.<>t__builder.Task;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C480 File Offset: 0x0000A680
		public void Stop()
		{
			if (this._service != null)
			{
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					foreach (IWebSocketConnection webSocketConnection in this._sockets.Values)
					{
						webSocketConnection.Close();
					}
					this._sockets.Clear();
					this._service.Dispose();
					this._service = null;
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C524 File Offset: 0x0000A724
		public void Dispose()
		{
			WebSocketServer service = this._service;
			if (service == null)
			{
				return;
			}
			service.Dispose();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C538 File Offset: 0x0000A738
		private void Broadcast(string json_message)
		{
			this._sendLock.WaitAsync();
			try
			{
				NLogService.WriteLog("WS:开始广播:" + json_message);
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					foreach (KeyValuePair<string, IWebSocketConnection> pair in this._sockets)
					{
						pair.Value.Send(json_message);
						NLogService.WriteLog("Send clientid(" + pair.Key + ")");
					}
				}
				NLogService.WriteLog("WS:开始广播结束");
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("WS:" + ex.ToString() + DateTime.Now.ToString() + "\n");
			}
			finally
			{
				this._sendLock.Release();
				Task.Delay(10);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C658 File Offset: 0x0000A858
		private void SendMsg(IWebSocketConnection sock, string json_message)
		{
			this._sendLock.WaitAsync();
			try
			{
				sock.Send(json_message).LogExceptions();
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("WS:" + ex.ToString() + DateTime.Now.ToString() + "\n");
			}
			finally
			{
				this._sendLock.Release();
				NLogService.WriteLog("WS:向客户端发送:" + json_message);
				Task.Delay(10);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		private void parse_recv(IWebSocketConnection socket, string recv_msg)
		{
			try
			{
				string pattern = "(\\\\[^bfrnt\\\\/'\\\"])";
				recv_msg = Regex.Replace(recv_msg, pattern, "\\$1");
				JObject jo = JsonConvert.DeserializeObject<JObject>(recv_msg);
				if (jo != null)
				{
					if (jo.ContainsKey(TencentProtocolHandler.QUERY_3D_STATUS))
					{
						NLogService.WriteLog("WS:新协议Request:" + TencentProtocolHandler.QUERY_3D_STATUS);
						string msg = TencentProtocolHandler.response_Query3DStatus_msg(GlobalStatusManager.instance._bInterWaveRet, GlobalStatusManager.instance._bEyeCrackrRet, GlobalStatusManager.instance._bRasterRet);
						this.SendMsg(socket, msg);
					}
					else if (jo.ContainsKey(TencentProtocolHandler.FUNCTION_STATUS))
					{
						NLogService.WriteLog("WS:旧协议Request:" + TencentProtocolHandler.FUNCTION_STATUS);
						string msg2 = TencentProtocolHandler.response_FunctionStatus_msg(GlobalStatusManager.instance._bInterWaveRet, GlobalStatusManager.instance._bEyeCrackrRet, GlobalStatusManager.instance._bRasterRet);
						this.SendMsg(socket, msg2);
					}
					else if (jo.ContainsKey(TencentProtocolHandler.GLOBAL_OPEN))
					{
						NLogService.WriteLog("WS:全局交织开关：" + TencentProtocolHandler.GLOBAL_OPEN);
						TencentProtocolHandler.parse_GlobalOpen_msg(recv_msg, this.GlobalOpen_Notify_Handler);
					}
					else if (jo.ContainsKey(TencentProtocolHandler.QUERY_UUID))
					{
						string msg3 = TencentProtocolHandler.response_Query_UUID_msg(GlobalStatusManager.instance._uuid);
						NLogService.WriteLog("WS:客户端查询设备uuid:" + msg3);
						this.SendMsg(socket, msg3);
					}
					else if (jo.ContainsKey(TencentProtocolHandler.SBSTYPE))
					{
						NLogService.WriteLog("WS:查询前台程序左右图支持情况:" + recv_msg);
						TencentProtocolHandler.parse_client_support_sbsType(recv_msg, this.GetExe3DInfos_Handler);
					}
					else if (jo.ContainsKey(TencentProtocolHandler.SET_3D_HOTKEY))
					{
						NLogService.WriteLog("WS:设置3D热键:" + recv_msg);
						TencentProtocolHandler.parse_Set3DHotkey_msg(socket, recv_msg, this.Set3DHotkey_Notify_Delegate_Handler);
					}
					else if (jo.ContainsKey(TencentProtocolHandler.QUERY_3D_HOTKEY))
					{
						NLogService.WriteLog("WS:查询3D热键:" + recv_msg);
						TencentProtocolHandler.parse_Query3DHotkey_msg(socket, recv_msg, this.Query3DHotkey_Notify_Delegate_Handler);
					}
					else if (jo.ContainsKey(YLProtocolHandler.YL_Clibration_InterWaveOpen))
					{
						string msg4 = YLProtocolHandler.response_YLClibrationInterWaveOpen_msg(recv_msg, this.YLClientMsg_Handler);
						this.SendMsg(socket, msg4);
					}
					else if (jo.ContainsKey(YLProtocolHandler.YL_Clibration_offsetX))
					{
						string msg5 = YLProtocolHandler.response_YLClibrationOffsetX_msg(recv_msg, this.YLClientMsg_Handler);
						this.SendMsg(socket, msg5);
					}
					else if (jo.ContainsKey(YLProtocolHandler.YL_Clibration_offsetZ))
					{
						string msg6 = YLProtocolHandler.response_YLClibrationOffsetZ_msg(recv_msg, this.YLClientMsg_Handler);
						this.SendMsg(socket, msg6);
					}
					else if (jo.ContainsKey(YLProtocolHandler.YL_Clibration_MCUValue))
					{
						string msg7 = YLProtocolHandler.response_YLWriteMCUValue_msg(recv_msg, this.YLMcuVlue_Handler);
						this.SendMsg(socket, msg7);
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("WS:parse_Process_info has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x040000C9 RID: 201
		private static readonly object _lock = new object();

		// Token: 0x040000CA RID: 202
		private SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

		// Token: 0x040000CB RID: 203
		public WebSocketFleck.GetExe3DInfos_Delegate GetExe3DInfos_Handler;

		// Token: 0x040000CC RID: 204
		public WebSocketFleck.GlobalOpen_Notify_Delegate GlobalOpen_Notify_Handler;

		// Token: 0x040000CD RID: 205
		public WebSocketFleck.Set3DHotkey_Notify_Delegate Set3DHotkey_Notify_Delegate_Handler;

		// Token: 0x040000CE RID: 206
		public WebSocketFleck.Query3DHotkey_Notify_Delegate Query3DHotkey_Notify_Delegate_Handler;

		// Token: 0x040000CF RID: 207
		public WebSocketFleck.YLClientMsg_Delegate YLClientMsg_Handler;

		// Token: 0x040000D0 RID: 208
		public WebSocketFleck.YLMcuVlue_Delegate YLMcuVlue_Handler;

		// Token: 0x040000D1 RID: 209
		private Dictionary<string, IWebSocketConnection> _sockets = new Dictionary<string, IWebSocketConnection>();

		// Token: 0x040000D2 RID: 210
		private WebSocketServer _service;

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x0600042C RID: 1068
		public delegate void GetExe3DInfos_Delegate(bool bResult, int nSbsType);

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x06000430 RID: 1072
		public delegate void GlobalOpen_Notify_Delegate(int nGlobalOpen);

		// Token: 0x02000099 RID: 153
		// (Invoke) Token: 0x06000434 RID: 1076
		public delegate void Set3DHotkey_Notify_Delegate(IWebSocketConnection sock, int KeyId, int Modifier, int Key, string Desc);

		// Token: 0x0200009A RID: 154
		// (Invoke) Token: 0x06000438 RID: 1080
		public delegate void Query3DHotkey_Notify_Delegate(IWebSocketConnection sock, int KeyId);

		// Token: 0x0200009B RID: 155
		// (Invoke) Token: 0x0600043C RID: 1084
		public delegate void YLClientMsg_Delegate(int cmd_type, float value = 0f);

		// Token: 0x0200009C RID: 156
		// (Invoke) Token: 0x06000440 RID: 1088
		public delegate int YLMcuVlue_Delegate(float value0, float value1, float value2, float value3);
	}
}
