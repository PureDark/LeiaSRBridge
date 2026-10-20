using System;
using AstraDepth3D.WindowHelper;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AstraDepth3D.WebSocketModule
{
	// Token: 0x02000025 RID: 37
	internal class TencentProtocolHandler
	{
		// Token: 0x0600017B RID: 379 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		public static string request_client_support_sbsType_msg(string ExeName, string ExePath)
		{
			JObject jobject = new JObject();
			jobject["Cmd"] = 2001;
			jobject["ExeName"] = ExeName;
			jobject["ExePath"] = ExePath;
			return jobject.ToString();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B944 File Offset: 0x00009B44
		public static string request_Set3DHotkey_msg(int KeyId, int Modifier, int Key, string Desc)
		{
			JObject msg = new JObject();
			msg["KeyId"] = KeyId;
			msg["Modifier"] = Modifier;
			msg["Key"] = Key;
			msg["Desc"] = Desc;
			JObject jobject = new JObject();
			jobject["Set3DHotkey"] = msg;
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public static string response_Set3DHotkey_msg(int errorCode, string msg)
		{
			JObject jobject = new JObject();
			jobject["Cmd"] = 1010;
			jobject["Ret"] = errorCode;
			jobject["Msg"] = msg;
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000BA02 File Offset: 0x00009C02
		public static string request_Query3DHotkey_msg(int KeyId)
		{
			JObject jobject = new JObject();
			jobject["Query3DHotkey"] = KeyId;
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000BA20 File Offset: 0x00009C20
		public static string response_Query3DHotkey_msg(int KeyId, int Modifier, int Key)
		{
			JObject jobject = new JObject();
			jobject["Cmd"] = 1011;
			jobject["Ret"] = 200;
			jobject["Msg"] = "ok";
			jobject["KeyId"] = KeyId;
			jobject["Modifier"] = Modifier;
			jobject["Key"] = Key;
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public static string response_FunctionStatus_msg(bool bInterWaveRet, bool bEyeCrackrRet, bool bRasterRet)
		{
			JObject obj = new JObject();
			obj["Cmd"] = 1001;
			if (bInterWaveRet)
			{
				obj["InterWaveRet"] = 200;
				obj["InterWaveMsg"] = "交织状态正常";
			}
			else
			{
				obj["InterWaveRet"] = 201;
				obj["InterWaveMsg"] = "交织未开启";
			}
			if (bEyeCrackrRet)
			{
				obj["EyeCrackrRet"] = 200;
				obj["EyeCrackMsg"] = "眼追状态正常";
			}
			else
			{
				obj["EyeCrackrRet"] = 202;
				obj["EyeCrackMsg"] = "眼追未开启";
			}
			if (bRasterRet)
			{
				obj["RasterRet"] = 200;
				obj["RasterMsg"] = "光栅状态正常";
			}
			else
			{
				obj["RasterRet"] = 203;
				obj["RasterMsg"] = "光栅未开启";
			}
			return obj.ToString();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		public static string response_Query3DStatus_msg(bool bInterWaveRet, bool bEyeCrackrRet, bool bRasterRet)
		{
			JObject obj = new JObject();
			obj["Cmd"] = 1001;
			if (bInterWaveRet)
			{
				obj["InterWaveRet"] = 1;
				obj["InterWaveMsg"] = "InterWave Success";
			}
			else
			{
				obj["InterWaveRet"] = 0;
				obj["InterWaveMsg"] = "InterWave Close";
			}
			if (bEyeCrackrRet)
			{
				obj["EyeCrackrRet"] = 1;
				obj["EyeCrackMsg"] = "EyeCrack Success";
			}
			else
			{
				obj["EyeCrackrRet"] = 0;
				obj["EyeCrackMsg"] = "EyeCrack Close";
			}
			if (bRasterRet)
			{
				obj["RasterRet"] = 1;
				obj["RasterMsg"] = "Raster open";
			}
			else
			{
				obj["RasterRet"] = 0;
				obj["RasterMsg"] = "Raster close";
			}
			return obj.ToString();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public static string broadcast_GlobalOpen_msg(int bInterWaveRet)
		{
			JObject obj = new JObject();
			obj["Cmd"] = 1002;
			obj["Ret"] = 200;
			if (bInterWaveRet == 1)
			{
				obj["Msg"] = "InterWave Success";
				obj["Status"] = 1;
			}
			else
			{
				obj["Msg"] = "InterWave Close";
				obj["Status"] = 0;
			}
			return obj.ToString();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public static string response_Query_UUID_msg(string client_id)
		{
			JObject jobject = new JObject();
			jobject["Cmd"] = 1003;
			jobject["Ret"] = 200;
			jobject["Msg"] = "ok";
			jobject["UUID"] = client_id;
			return jobject.ToString();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public static void parse_client_support_sbsType(string msg, WebSocketFleck.GetExe3DInfos_Delegate callback)
		{
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(msg);
				int nResultCode = -1;
				int.TryParse(jobject["Ret"].ToString(), out nResultCode);
				int SbsTypeCode = -1;
				int.TryParse(jobject["SbsType"].ToString(), out SbsTypeCode);
				if (callback != null)
				{
					callback(nResultCode == 200, SbsTypeCode);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("TencentProtocolHandler:parse_Process_info has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public static void parse_GlobalOpen_msg(string msg, WebSocketFleck.GlobalOpen_Notify_Delegate callback)
		{
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(msg);
				int GlobalOpenCode = -1;
				int.TryParse(jobject["GlobalOpen"].ToString(), out GlobalOpenCode);
				if (callback != null)
				{
					callback(GlobalOpenCode);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("TencentProtocolHandler:parse_Process_info has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BF08 File Offset: 0x0000A108
		public static void parse_Query3DHotkey_msg(IWebSocketConnection sock, string msg, WebSocketFleck.Query3DHotkey_Notify_Delegate callback)
		{
			try
			{
				JToken jtoken = JsonConvert.DeserializeObject<JObject>(msg)["Query3DHotkey"];
				int KeyId = (jtoken != null) ? Extensions.Value<int>(jtoken) : 0;
				if (callback != null)
				{
					callback(sock, KeyId);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("TencentProtocolHandler:parse_Process_info has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BF74 File Offset: 0x0000A174
		public static void parse_Set3DHotkey_msg(IWebSocketConnection sock, string recv_msg, WebSocketFleck.Set3DHotkey_Notify_Delegate callback)
		{
			try
			{
				JObject obj = JsonConvert.DeserializeObject<JObject>(recv_msg);
				if (obj != null)
				{
					JToken jtoken = obj["Set3DHotkey"];
					JToken jtoken2 = jtoken["KeyId"];
					int KeyId = (jtoken2 != null) ? Extensions.Value<int>(jtoken2) : 0;
					JToken jtoken3 = jtoken["Modifier"];
					int Modifier = (jtoken3 != null) ? Extensions.Value<int>(jtoken3) : 0;
					JToken jtoken4 = jtoken["Key"];
					int Key = (jtoken4 != null) ? Extensions.Value<int>(jtoken4) : 0;
					string Desc = jtoken["Desc"].ToString();
					if (callback != null)
					{
						callback(sock, KeyId, Modifier, Key, Desc);
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("TencentProtocolHandler:parse_Set3DHotkey_msg has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x040000BE RID: 190
		public static string QUERY_3D_STATUS = "Query3DStatus";

		// Token: 0x040000BF RID: 191
		public static string FUNCTION_STATUS = "FunctionStatus";

		// Token: 0x040000C0 RID: 192
		public static string GLOBAL_OPEN = "GlobalOpen";

		// Token: 0x040000C1 RID: 193
		public static string QUERY_UUID = "Query";

		// Token: 0x040000C2 RID: 194
		public static string SBSTYPE = "SbsType";

		// Token: 0x040000C3 RID: 195
		public static string SET_3D_HOTKEY = "Set3DHotkey";

		// Token: 0x040000C4 RID: 196
		public static string QUERY_3D_HOTKEY = "Query3DHotkey";
	}
}
