using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200003A RID: 58
	public static class MonitorHelper
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000F254 File Offset: 0x0000D454
		public static MonitorFullInfo FindMonitor(JArray displayDeviceList)
		{
			if (displayDeviceList == null)
			{
				return null;
			}
			Dictionary<string, MonitorFullInfo> monitorDict = (from m in MonitorHelper.GetMatchedMonitors()
			where m != null && m.Manufacturer != null && m.ModelName != null
			group m by m.Manufacturer.ToUpperInvariant() + "|" + m.ModelName.ToUpperInvariant()).ToDictionary((IGrouping<string, MonitorFullInfo> g) => g.Key, (IGrouping<string, MonitorFullInfo> g) => g.First<MonitorFullInfo>());
			foreach (JToken jtoken in displayDeviceList)
			{
				JObject jobject = (JObject)jtoken;
				string manufacturer = jobject.Value<string>("Manufacturer");
				string modelName = jobject.Value<string>("ModelName");
				string lens = jobject.Value<string>("Lens");
				if (!string.IsNullOrWhiteSpace(manufacturer) && !string.IsNullOrWhiteSpace(modelName))
				{
					string key = manufacturer.ToUpperInvariant() + "|" + modelName.ToUpperInvariant();
					MonitorFullInfo monitor;
					if (monitorDict.TryGetValue(key, out monitor))
					{
						NLogService.WriteLog("找到匹配的显示器: " + manufacturer + " " + modelName);
						MonitorFullInfo monitorFullInfo = monitor.Clone();
						monitorFullInfo.Lens = lens;
						return monitorFullInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000F3BC File Offset: 0x0000D5BC
		public static List<MonitorFullInfo> GetMatchedMonitors()
		{
			List<MonitorFullInfo> finalResult = new List<MonitorFullInfo>();
			IntPtr pGuid = Marshal.AllocHGlobal(Marshal.SizeOf<Guid>(MonitorHelper.GUID_CLASS_MONITOR));
			Marshal.StructureToPtr<Guid>(MonitorHelper.GUID_CLASS_MONITOR, pGuid, false);
			MonitorHelper.SetupDiGetClassDevsEx(pGuid, null, (IntPtr)0, 2, (IntPtr)0, null, (IntPtr)0);
			MonitorHelper.DISPLAY_DEVICE dd = default(MonitorHelper.DISPLAY_DEVICE);
			dd.cb = Marshal.SizeOf(typeof(MonitorHelper.DISPLAY_DEVICE));
			uint dev = 0U;
			Dictionary<string, string> gdiToPnpDict = new Dictionary<string, string>();
			while (MonitorHelper.EnumDisplayDevices(null, dev, ref dd, 0U))
			{
				MonitorHelper.DISPLAY_DEVICE ddMon = default(MonitorHelper.DISPLAY_DEVICE);
				ddMon.cb = Marshal.SizeOf(typeof(MonitorHelper.DISPLAY_DEVICE));
				uint devMon = 0U;
				while (MonitorHelper.EnumDisplayDevices(dd.DeviceName, devMon, ref ddMon, 0U))
				{
					if ((ddMon.StateFlags & MonitorHelper.DisplayDeviceStateFlags.AttachedToDesktop) != (MonitorHelper.DisplayDeviceStateFlags)0 && (ddMon.StateFlags & MonitorHelper.DisplayDeviceStateFlags.MirroringDriver) == (MonitorHelper.DisplayDeviceStateFlags)0)
					{
						gdiToPnpDict[dd.DeviceName] = ddMon.DeviceID;
						break;
					}
					devMon += 1U;
					ddMon = default(MonitorHelper.DISPLAY_DEVICE);
					ddMon.cb = Marshal.SizeOf(typeof(MonitorHelper.DISPLAY_DEVICE));
				}
				dd = default(MonitorHelper.DISPLAY_DEVICE);
				dd.cb = Marshal.SizeOf(typeof(MonitorHelper.DISPLAY_DEVICE));
				dev += 1U;
			}
			Dictionary<string, EdidData> pnpToEdidDict = new Dictionary<string, EdidData>();
			MonitorHelper.GetActualEDID(pnpToEdidDict);
			foreach (KeyValuePair<string, string> kvp in gdiToPnpDict)
			{
				string gdiName = kvp.Key;
				string pnpDeviceId = kvp.Value;
				EdidData edid;
				if (pnpToEdidDict.TryGetValue(pnpDeviceId, out edid))
				{
					ValueTuple<string, string> valueTuple = MonitorHelper.Parse(edid.GetData());
					string manufacturer = valueTuple.Item1;
					string modelName = valueTuple.Item2;
					finalResult.Add(new MonitorFullInfo
					{
						GdiName = gdiName,
						PnpDeviceID = pnpDeviceId,
						EdidData = edid,
						Manufacturer = manufacturer,
						ModelName = modelName
					});
				}
			}
			return finalResult;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000F598 File Offset: 0x0000D798
		private static bool GetActualEDID(Dictionary<string, EdidData> dict)
		{
			IntPtr pGuid = Marshal.AllocHGlobal(Marshal.SizeOf<Guid>(MonitorHelper.GUID_CLASS_MONITOR));
			Marshal.StructureToPtr<Guid>(MonitorHelper.GUID_CLASS_MONITOR, pGuid, false);
			IntPtr hDevInfo = MonitorHelper.SetupDiGetClassDevsEx(pGuid, null, (IntPtr)0, 2, (IntPtr)0, null, (IntPtr)0);
			int i = 0;
			while (Marshal.GetLastWin32Error() != 259)
			{
				MonitorHelper.SP_DEVINFO_DATA devInfoData = default(MonitorHelper.SP_DEVINFO_DATA);
				devInfoData.cbSize = Marshal.SizeOf(typeof(MonitorHelper.SP_DEVINFO_DATA));
				if (MonitorHelper.SetupDiEnumDeviceInfo(hDevInfo, i, ref devInfoData) > 0)
				{
					string driverValue = string.Empty;
					string hardwareIdValue = string.Empty;
					byte[] propBuffer = new byte[256];
					uint propType;
					uint reqSize;
					if (MonitorHelper.SetupDiGetDeviceRegistryProperty(hDevInfo, ref devInfoData, 9U, out propType, propBuffer, (uint)propBuffer.Length, out reqSize))
					{
						driverValue = Encoding.Unicode.GetString(propBuffer).TrimEnd(new char[1]);
					}
					if (MonitorHelper.SetupDiGetDeviceRegistryProperty(hDevInfo, ref devInfoData, 1U, out propType, propBuffer, (uint)propBuffer.Length, out reqSize))
					{
						string[] allHardwareIds = Encoding.Unicode.GetString(propBuffer).TrimEnd(new char[1]).Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
						hardwareIdValue = ((allHardwareIds.Length != 0) ? allHardwareIds[0] : string.Empty);
					}
					string PnpDeviceId = hardwareIdValue + "\\" + driverValue;
					NLogService.WriteLog("hardwareId+driverValue: " + PnpDeviceId);
					UIntPtr hDevRegKey = MonitorHelper.SetupDiOpenDevRegKey(hDevInfo, ref devInfoData, 1, 0, 1, 131097);
					NLogService.WriteLog(string.Format("devInfoData:{0}", devInfoData.ClassGuid));
					EdidData si = MonitorHelper.PullEDID(hDevRegKey);
					if (si != null)
					{
						dict[PnpDeviceId] = si;
					}
					MonitorHelper.RegCloseKey(hDevRegKey);
				}
				i++;
			}
			Marshal.FreeHGlobal(pGuid);
			return true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F720 File Offset: 0x0000D920
		private static EdidData PullEDID([NativeInteger] UIntPtr hDevRegKey)
		{
			EdidData si = null;
			StringBuilder valueName = new StringBuilder(128);
			uint ActualValueNameLength = 128U;
			byte[] EDIdata = new byte[1024];
			IntPtr pEDIdata = Marshal.AllocHGlobal(EDIdata.Length);
			Marshal.Copy(EDIdata, 0, pEDIdata, EDIdata.Length);
			int size = 1024;
			uint i = 0U;
			uint retValue = 0U;
			while (retValue != 259U)
			{
				ActualValueNameLength = 128U;
				size = 1024;
				retValue = MonitorHelper.RegEnumValue(hDevRegKey, i, valueName, ref ActualValueNameLength, (IntPtr)0, (IntPtr)0, pEDIdata, ref size);
				string data = valueName.ToString();
				NLogService.WriteLog("valueName:" + data);
				if (retValue == 0U && data.Contains("EDID") && size >= 1)
				{
					byte[] actualData = new byte[size];
					Marshal.Copy(pEDIdata, actualData, 0, size);
					si = new EdidData(actualData);
				}
				i += 1U;
			}
			Marshal.FreeHGlobal(pEDIdata);
			return si;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		public static ValueTuple<string, string> Parse(byte[] data)
		{
			string item = MonitorHelper.Manufacturer_Name(data);
			string ModelName = MonitorHelper.GetModelName(data);
			return new ValueTuple<string, string>(item, ModelName);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F818 File Offset: 0x0000DA18
		public static string Manufacturer_Name(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 128)
			{
				return "";
			}
			int num = (int)EDID[8] << 8 | (int)EDID[9];
			int a = (num & 31744) >> 10;
			int b = (num & 992) >> 5;
			int c = num & 31;
			string str = MonitorHelper.ToCharByASCIIShort(a).ToString() ?? "";
			string B = MonitorHelper.ToCharByASCIIShort(b).ToString() ?? "";
			string C = MonitorHelper.ToCharByASCIIShort(c).ToString() ?? "";
			return str + B + C;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public static string GetModelName(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 128)
			{
				return "";
			}
			for (int i = 0; i < 4; i++)
			{
				int offset = 54 + i * 18;
				if (offset + 17 >= EDID.Length)
				{
					break;
				}
				if (EDID[offset] == 0 && EDID[offset + 1] == 0 && EDID[offset + 2] == 0 && EDID[offset + 3] == 252 && EDID[offset + 4] == 0)
				{
					return MonitorHelper.ExtractModelNameFromDescriptor(EDID, offset);
				}
			}
			return "Not Specified";
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000F920 File Offset: 0x0000DB20
		private static string ExtractModelNameFromDescriptor(byte[] EDID, int offset)
		{
			string result;
			try
			{
				byte[] nameBytes = new byte[13];
				Array.Copy(EDID, offset + 5, nameBytes, 0, 13);
				string modelName = Encoding.ASCII.GetString(nameBytes);
				modelName = Regex.Replace(modelName, "\\s", "");
				if (!string.IsNullOrWhiteSpace(modelName))
				{
					if (!modelName.All((char c) => c == '\0'))
					{
						return modelName;
					}
				}
				result = "Not Specified";
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("解析型号名称时出错: " + ex.Message);
				result = "Error";
			}
			return result;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public static char ToCharByASCIIShort(int a)
		{
			return (char)(a + 64);
		}

		// Token: 0x06000245 RID: 581
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern uint RegEnumValue([NativeInteger] UIntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcValueName, [NativeInteger] IntPtr lpReserved, [NativeInteger] IntPtr lpType, [NativeInteger] IntPtr lpData, ref int lpcbData);

		// Token: 0x06000246 RID: 582
		[DllImport("setupapi.dll")]
		[return: NativeInteger]
		private static extern IntPtr SetupDiGetClassDevsEx([NativeInteger] IntPtr ClassGuid, [MarshalAs(UnmanagedType.LPStr)] string enumerator, [NativeInteger] IntPtr hwndParent, int Flags, [NativeInteger] IntPtr DeviceInfoSet, [MarshalAs(UnmanagedType.LPStr)] string MachineName, [NativeInteger] IntPtr Reserved);

		// Token: 0x06000247 RID: 583
		[DllImport("setupapi.dll", SetLastError = true)]
		private static extern int SetupDiEnumDeviceInfo([NativeInteger] IntPtr DeviceInfoSet, int MemberIndex, ref MonitorHelper.SP_DEVINFO_DATA DeviceInterfaceData);

		// Token: 0x06000248 RID: 584
		[DllImport("Setupapi", CharSet = CharSet.Auto, SetLastError = true)]
		[return: NativeInteger]
		private static extern UIntPtr SetupDiOpenDevRegKey([NativeInteger] IntPtr hDeviceInfoSet, ref MonitorHelper.SP_DEVINFO_DATA deviceInfoData, int scope, int hwProfile, int parameterRegistryValueKind, int samDesired);

		// Token: 0x06000249 RID: 585
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref MonitorHelper.SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, int DeviceInstanceIdSize, out int RequiredSize);

		// Token: 0x0600024A RID: 586
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref MonitorHelper.SP_DEVINFO_DATA DeviceInfoData, uint Property, out uint PropertyRegDataType, byte[] PropertyBuffer, uint PropertyBufferSize, out uint RequiredSize);

		// Token: 0x0600024B RID: 587
		[DllImport("user32.dll")]
		private static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref MonitorHelper.DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

		// Token: 0x0600024C RID: 588
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegCloseKey([NativeInteger] UIntPtr hKey);

		// Token: 0x04000183 RID: 387
		private const int DICS_FLAG_GLOBAL = 1;

		// Token: 0x04000184 RID: 388
		private const int DIREG_DEV = 1;

		// Token: 0x04000185 RID: 389
		private const int KEY_READ = 131097;

		// Token: 0x04000186 RID: 390
		private const int ERROR_SUCCESS = 0;

		// Token: 0x04000187 RID: 391
		private static readonly Guid GUID_CLASS_MONITOR = new Guid(1295444334U, 58149, 4558, 191, 193, 8, 0, 43, 225, 3, 24);

		// Token: 0x04000188 RID: 392
		private const int DIGCF_PRESENT = 2;

		// Token: 0x04000189 RID: 393
		private const int ERROR_NO_MORE_ITEMS = 259;

		// Token: 0x0400018A RID: 394
		private const uint SPDRP_DRIVER = 9U;

		// Token: 0x0400018B RID: 395
		private const uint SPDRP_HARDWAREID = 1U;

		// Token: 0x020000AF RID: 175
		[Flags]
		private enum DisplayDeviceStateFlags
		{
			// Token: 0x0400043D RID: 1085
			AttachedToDesktop = 1,
			// Token: 0x0400043E RID: 1086
			MultiDriver = 2,
			// Token: 0x0400043F RID: 1087
			PrimaryDevice = 4,
			// Token: 0x04000440 RID: 1088
			MirroringDriver = 8,
			// Token: 0x04000441 RID: 1089
			VGACompatible = 16,
			// Token: 0x04000442 RID: 1090
			Removable = 32,
			// Token: 0x04000443 RID: 1091
			ModesPruned = 134217728,
			// Token: 0x04000444 RID: 1092
			Remote = 67108864,
			// Token: 0x04000445 RID: 1093
			Disconnect = 33554432
		}

		// Token: 0x020000B0 RID: 176
		private struct DISPLAY_DEVICE
		{
			// Token: 0x04000446 RID: 1094
			[MarshalAs(UnmanagedType.U4)]
			public int cb;

			// Token: 0x04000447 RID: 1095
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DeviceName;

			// Token: 0x04000448 RID: 1096
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string DeviceString;

			// Token: 0x04000449 RID: 1097
			[MarshalAs(UnmanagedType.U4)]
			public MonitorHelper.DisplayDeviceStateFlags StateFlags;

			// Token: 0x0400044A RID: 1098
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string DeviceID;

			// Token: 0x0400044B RID: 1099
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string DeviceKey;
		}

		// Token: 0x020000B1 RID: 177
		private struct SP_DEVINFO_DATA
		{
			// Token: 0x0400044C RID: 1100
			public int cbSize;

			// Token: 0x0400044D RID: 1101
			public Guid ClassGuid;

			// Token: 0x0400044E RID: 1102
			public uint DevInst;

			// Token: 0x0400044F RID: 1103
			[NativeInteger]
			public IntPtr Reserved;
		}
	}
}
