using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		// Note: this type is marked as 'beforefieldinit'.
		static WebSocketFleck()
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		[CompilerGenerated]
		private void <Start>b__28_0(IWebSocketConnection socket)
		{
			socket.OnOpen = delegate()
			{
				string requestUri = socket.ConnectionInfo.Host + socket.ConnectionInfo.Path;
				NLogService.WriteLog("Client url: " + requestUri);
				Dictionary<string, string> queryParams = this.ParseQueryParameters(requestUri);
				string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					if (string.IsNullOrEmpty(client_id))
					{
						client_id = Guid.NewGuid().ToString("N");
					}
					if (this._sockets.ContainsKey(client_id))
					{
						this._sockets.Remove(client_id);
					}
					this._sockets.Add(client_id, socket);
					NLogService.WriteLog("WebSocketServer:connected:uuid:" + client_id);
				}
			};
			socket.OnClose = delegate()
			{
				string requestUri = socket.ConnectionInfo.Host + socket.ConnectionInfo.Path;
				NLogService.WriteLog("Client url: " + requestUri);
				Dictionary<string, string> queryParams = this.ParseQueryParameters(requestUri);
				string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					if (!string.IsNullOrEmpty(client_id))
					{
						this._sockets.Remove(client_id);
						socket.Close();
						NLogService.WriteLog("WebSocketServer:Close:uuid:" + client_id);
					}
					else if (this._sockets.ContainsValue(socket))
					{
						string temp_key = null;
						foreach (KeyValuePair<string, IWebSocketConnection> pair in this._sockets)
						{
							if (pair.Value == socket)
							{
								temp_key = pair.Key;
								break;
							}
						}
						if (temp_key != null)
						{
							this._sockets.Remove(temp_key);
							socket.Close();
							NLogService.WriteLog("WebSocketServer:Close:uuid:" + temp_key);
						}
					}
				}
			};
			socket.OnMessage = delegate(string message)
			{
				try
				{
					string requestUri = socket.ConnectionInfo.Host + socket.ConnectionInfo.Path;
					Dictionary<string, string> queryParams = this.ParseQueryParameters(requestUri);
					string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
					NLogService.WriteLine("uuid:" + client_id + "=>收到客户端信息: " + message);
					this.parse_recv(socket, message);
				}
				catch (Exception ex)
				{
					NLogService.WriteLog(ex.ToString() + DateTime.Now.ToString() + "\n");
				}
			};
			socket.OnError = delegate(Exception error)
			{
				NLogService.WriteLog("发生错误: " + ((error != null) ? error.ToString() : null));
				string requestUri = socket.ConnectionInfo.Host + socket.ConnectionInfo.Path;
				NLogService.WriteLog("Client url: " + requestUri);
				Dictionary<string, string> queryParams = this.ParseQueryParameters(requestUri);
				string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					if (client_id != null)
					{
						this._sockets.Remove(client_id);
						socket.Close();
						NLogService.WriteLog("WebSocketServer:Close:uuid:" + client_id);
					}
					else
					{
						string temp_key = null;
						foreach (KeyValuePair<string, IWebSocketConnection> pair in this._sockets)
						{
							if (pair.Value == socket)
							{
								temp_key = pair.Key;
								break;
							}
						}
						if (temp_key != null)
						{
							this._sockets.Remove(temp_key);
							socket.Close();
							NLogService.WriteLog("WebSocketServer:Close:uuid:" + temp_key);
						}
					}
				}
			};
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

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000443 RID: 1091 RVA: 0x0001F662 File Offset: 0x0001D862
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x0001F66E File Offset: 0x0001D86E
			public <>c()
			{
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x0001F676 File Offset: 0x0001D876
			internal void <.ctor>b__26_0(LogLevel level, string message, Exception ex)
			{
				NLogService.WriteLog(string.Format("FleckLog: {0} - {1}-{2}", level, message, (ex != null) ? ex.Message : null));
			}

			// Token: 0x04000403 RID: 1027
			public static readonly WebSocketFleck.<>c <>9 = new WebSocketFleck.<>c();

			// Token: 0x04000404 RID: 1028
			public static Action<LogLevel, string, Exception> <>9__26_0;
		}

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_0
		{
			// Token: 0x06000446 RID: 1094 RVA: 0x0001F69A File Offset: 0x0001D89A
			public <>c__DisplayClass28_0()
			{
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
			internal void <Start>b__1()
			{
				string requestUri = this.socket.ConnectionInfo.Host + this.socket.ConnectionInfo.Path;
				NLogService.WriteLog("Client url: " + requestUri);
				Dictionary<string, string> queryParams = this.<>4__this.ParseQueryParameters(requestUri);
				string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					if (string.IsNullOrEmpty(client_id))
					{
						client_id = Guid.NewGuid().ToString("N");
					}
					if (this.<>4__this._sockets.ContainsKey(client_id))
					{
						this.<>4__this._sockets.Remove(client_id);
					}
					this.<>4__this._sockets.Add(client_id, this.socket);
					NLogService.WriteLog("WebSocketServer:connected:uuid:" + client_id);
				}
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
			internal void <Start>b__2()
			{
				string requestUri = this.socket.ConnectionInfo.Host + this.socket.ConnectionInfo.Path;
				NLogService.WriteLog("Client url: " + requestUri);
				Dictionary<string, string> queryParams = this.<>4__this.ParseQueryParameters(requestUri);
				string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					if (!string.IsNullOrEmpty(client_id))
					{
						this.<>4__this._sockets.Remove(client_id);
						this.socket.Close();
						NLogService.WriteLog("WebSocketServer:Close:uuid:" + client_id);
					}
					else if (this.<>4__this._sockets.ContainsValue(this.socket))
					{
						string temp_key = null;
						foreach (KeyValuePair<string, IWebSocketConnection> pair in this.<>4__this._sockets)
						{
							if (pair.Value == this.socket)
							{
								temp_key = pair.Key;
								break;
							}
						}
						if (temp_key != null)
						{
							this.<>4__this._sockets.Remove(temp_key);
							this.socket.Close();
							NLogService.WriteLog("WebSocketServer:Close:uuid:" + temp_key);
						}
					}
				}
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x0001F92C File Offset: 0x0001DB2C
			internal void <Start>b__3(string message)
			{
				try
				{
					string requestUri = this.socket.ConnectionInfo.Host + this.socket.ConnectionInfo.Path;
					Dictionary<string, string> queryParams = this.<>4__this.ParseQueryParameters(requestUri);
					string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
					NLogService.WriteLine("uuid:" + client_id + "=>收到客户端信息: " + message);
					this.<>4__this.parse_recv(this.socket, message);
				}
				catch (Exception ex)
				{
					NLogService.WriteLog(ex.ToString() + DateTime.Now.ToString() + "\n");
				}
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x0001F9E8 File Offset: 0x0001DBE8
			internal void <Start>b__4(Exception error)
			{
				NLogService.WriteLog("发生错误: " + ((error != null) ? error.ToString() : null));
				string requestUri = this.socket.ConnectionInfo.Host + this.socket.ConnectionInfo.Path;
				NLogService.WriteLog("Client url: " + requestUri);
				Dictionary<string, string> queryParams = this.<>4__this.ParseQueryParameters(requestUri);
				string client_id = queryParams.ContainsKey("ClientId") ? queryParams["ClientId"] : null;
				object @lock = WebSocketFleck._lock;
				lock (@lock)
				{
					if (client_id != null)
					{
						this.<>4__this._sockets.Remove(client_id);
						this.socket.Close();
						NLogService.WriteLog("WebSocketServer:Close:uuid:" + client_id);
					}
					else
					{
						string temp_key = null;
						foreach (KeyValuePair<string, IWebSocketConnection> pair in this.<>4__this._sockets)
						{
							if (pair.Value == this.socket)
							{
								temp_key = pair.Key;
								break;
							}
						}
						if (temp_key != null)
						{
							this.<>4__this._sockets.Remove(temp_key);
							this.socket.Close();
							NLogService.WriteLog("WebSocketServer:Close:uuid:" + temp_key);
						}
					}
				}
			}

			// Token: 0x04000405 RID: 1029
			public IWebSocketConnection socket;

			// Token: 0x04000406 RID: 1030
			public WebSocketFleck <>4__this;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Start>d__28 : IAsyncStateMachine
		{
			// Token: 0x0600044B RID: 1099 RVA: 0x0001FB68 File Offset: 0x0001DD68
			void IAsyncStateMachine.MoveNext()
			{
				WebSocketFleck webSocketFleck = this.<>4__this;
				try
				{
					try
					{
						webSocketFleck._service.Start(delegate(IWebSocketConnection socket)
						{
							WebSocketFleck.<>c__DisplayClass28_0 CS$<>8__locals1 = new WebSocketFleck.<>c__DisplayClass28_0();
							CS$<>8__locals1.<>4__this = webSocketFleck;
							CS$<>8__locals1.socket = socket;
							CS$<>8__locals1.socket.OnOpen = new Action(CS$<>8__locals1.<Start>b__1);
							CS$<>8__locals1.socket.OnClose = new Action(CS$<>8__locals1.<Start>b__2);
							CS$<>8__locals1.socket.OnMessage = new Action<string>(CS$<>8__locals1.<Start>b__3);
							CS$<>8__locals1.socket.OnError = new Action<Exception>(CS$<>8__locals1.<Start>b__4);
						});
						NLogService.WriteLog("WebSocketFleck|服务已启动");
					}
					catch (Exception ex)
					{
						NLogService.WriteLog("WebSocketServer:" + ex.ToString() + DateTime.Now.ToString() + "\n");
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x0001FC14 File Offset: 0x0001DE14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000407 RID: 1031
			public int <>1__state;

			// Token: 0x04000408 RID: 1032
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000409 RID: 1033
			public WebSocketFleck <>4__this;
		}
	}
}
