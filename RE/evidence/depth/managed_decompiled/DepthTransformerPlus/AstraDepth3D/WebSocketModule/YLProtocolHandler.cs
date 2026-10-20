using System;
using AstraDepth3D.WindowHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AstraDepth3D.WebSocketModule
{
	// Token: 0x02000028 RID: 40
	internal class YLProtocolHandler
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public static string response_YLClibrationInterWaveOpen_msg(string msg, WebSocketFleck.YLClientMsg_Delegate callback)
		{
			JObject obj = new JObject();
			obj[YLProtocolHandler.YL_CMD_TYPE] = 1101;
			obj["Ret"] = 200;
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(msg);
				int InterWaveOpen = 0;
				int.TryParse(jobject[YLProtocolHandler.YL_Clibration_InterWaveOpen].ToString(), out InterWaveOpen);
				if (InterWaveOpen == 1)
				{
					obj["YLClibrationInterWaveOpen"] = 1;
					obj["Msg"] = "LR InterWave Open Success";
				}
				else if (InterWaveOpen == 0)
				{
					obj["YLClibrationInterWaveOpen"] = 0;
					obj["Msg"] = "LR InterWave Close Success";
				}
				if (callback != null)
				{
					callback(1101, (float)InterWaveOpen);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("response_YLClibrationInterWaveOpen_msg has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
			return obj.ToString();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000CB64 File Offset: 0x0000AD64
		public static string response_YLClibrationOffsetX_msg(string msg, WebSocketFleck.YLClientMsg_Delegate callback)
		{
			JObject obj = new JObject();
			obj[YLProtocolHandler.YL_CMD_TYPE] = 1102;
			obj["Ret"] = 200;
			obj["Msg"] = "Recv OffsetX Success";
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(msg);
				float offsetX = 0f;
				float.TryParse(jobject["offsetX"].ToString(), out offsetX);
				if (callback != null)
				{
					callback(1102, offsetX);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("response_YLClibrationInterWaveOpen_msg has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
			return obj.ToString();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000CC24 File Offset: 0x0000AE24
		public static string response_YLClibrationOffsetZ_msg(string msg, WebSocketFleck.YLClientMsg_Delegate callback)
		{
			JObject obj = new JObject();
			obj[YLProtocolHandler.YL_CMD_TYPE] = 1104;
			obj["Ret"] = 200;
			obj["Msg"] = "Recv OffsetZ Success";
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(msg);
				float offsetZ = 0f;
				float.TryParse(jobject["offsetZ"].ToString(), out offsetZ);
				if (callback != null)
				{
					callback(1104, offsetZ);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("response_YLClibrationInterWaveOpen_msg has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
			return obj.ToString();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
		public static string response_YLWriteMCUValue_msg(string msg, WebSocketFleck.YLMcuVlue_Delegate callback)
		{
			JObject obj = new JObject();
			obj[YLProtocolHandler.YL_CMD_TYPE] = 1103;
			obj["Ret"] = -1;
			obj["Msg"] = "Write MCU Success";
			try
			{
				JArray jsonArr = (JArray)JsonConvert.DeserializeObject(JsonConvert.DeserializeObject<JObject>(msg)["MCUValue"].ToString());
				if (callback != null)
				{
					float value0 = 0f;
					float value = 0f;
					float value2 = 0f;
					float value3 = 0f;
					if (jsonArr.Count == 4)
					{
						float.TryParse(jsonArr[0].ToString(), out value0);
						float.TryParse(jsonArr[1].ToString(), out value);
						float.TryParse(jsonArr[2].ToString(), out value2);
						float.TryParse(jsonArr[3].ToString(), out value3);
						int nRet = callback(value0, value, value2, value3);
						if (nRet == 0)
						{
							obj["Ret"] = 200;
							obj["Msg"] = "Write MCU Success";
						}
						else
						{
							obj["Ret"] = nRet;
							obj["Msg"] = "Write MCU Fail";
						}
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("response_YLWriteMCUValue_msg has exception," + ex.Message + "\r\n" + ex.StackTrace);
			}
			return obj.ToString();
		}

		// Token: 0x040000D3 RID: 211
		public static string YL_Clibration_InterWaveOpen = "YLClibrationInterWaveOpen";

		// Token: 0x040000D4 RID: 212
		public static string YL_Clibration_offsetX = "offsetX";

		// Token: 0x040000D5 RID: 213
		public static string YL_Clibration_offsetZ = "offsetZ";

		// Token: 0x040000D6 RID: 214
		public static string YL_Clibration_MCUValue = "MCUValue";

		// Token: 0x040000D7 RID: 215
		public static string YL_CLIENT_TYPE = "yl_internal";

		// Token: 0x040000D8 RID: 216
		public static string YL_CMD_TYPE = "YLCmdType";
	}
}
