using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000036 RID: 54
	public class Edid
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000E67D File Offset: 0x0000C87D
		public Edid(byte[] data)
		{
			this._data = data;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E68C File Offset: 0x0000C88C
		public EdidDetail GetDetail()
		{
			return EdidDetail.Parse(this._data);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E69C File Offset: 0x0000C89C
		public override string ToString()
		{
			int i = 0;
			StringBuilder builder = new StringBuilder();
			foreach (byte b in this._data)
			{
				builder.Append(b.ToString("X2")).Append(" ");
				i++;
				if (i % 16 == 0)
				{
					builder.AppendLine();
				}
			}
			return builder.ToString();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		public static Edid[] Get()
		{
			return Edid.EdidUtil.GetEDID();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E708 File Offset: 0x0000C908
		public static string GetProductName()
		{
			string keyPath = "HARDWARE\\DESCRIPTION\\System\\BIOS";
			string valueName = "SystemProductName";
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
			{
				object value = (key != null) ? key.GetValue(valueName) : null;
				if (value != null)
				{
					return Regex.Replace(value.ToString(), "\\s", "");
				}
			}
			return null;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000E778 File Offset: 0x0000C978
		public static string GetBaseBoardSN()
		{
			string result;
			try
			{
				string sn = string.Empty;
				using (ManagementClass mc = new ManagementClass("Win32_BaseBoard"))
				{
					using (ManagementObjectCollection moc = mc.GetInstances())
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = moc.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								PropertyData propertyData = ((ManagementObject)enumerator.Current).Properties["SerialNumber"];
								string text;
								if (propertyData == null)
								{
									text = null;
								}
								else
								{
									object value = propertyData.Value;
									text = ((value != null) ? value.ToString() : null);
								}
								sn = (text ?? string.Empty);
							}
						}
					}
				}
				result = ((!string.IsNullOrEmpty(sn)) ? sn.Trim() : "Unknown");
			}
			catch (Exception ex)
			{
				Console.WriteLine("获取BaseBoardSN时发生异常: " + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000E880 File Offset: 0x0000CA80
		public static List<EdidDetail> GetEdidDetails()
		{
			List<EdidDetail> details = new List<EdidDetail>();
			Edid[] infoList = Edid.Get();
			for (int i = 0; i < infoList.Length; i++)
			{
				if (i != 0)
				{
					NLogService.WriteLog("==========");
				}
				EdidDetail detail = infoList[i].GetDetail();
				if (detail != null)
				{
					details.Add(detail);
				}
				else
				{
					NLogService.WriteLog("read edid failed!");
				}
			}
			return details;
		}

		// Token: 0x04000173 RID: 371
		private readonly byte[] _data;

		// Token: 0x020000AD RID: 173
		private static class EdidUtil
		{
			// Token: 0x06000464 RID: 1124 RVA: 0x0001FF54 File Offset: 0x0001E154
			public static Edid[] GetEDID()
			{
				List<Edid> lsi = new List<Edid>();
				IntPtr pGuid = Marshal.AllocHGlobal(Marshal.SizeOf<Guid>(Edid.EdidUtil.GUID_CLASS_MONITOR));
				Marshal.StructureToPtr<Guid>(Edid.EdidUtil.GUID_CLASS_MONITOR, pGuid, false);
				Edid.EdidUtil.SetupDiGetClassDevsEx(pGuid, null, (IntPtr)0, 2, (IntPtr)0, null, (IntPtr)0);
				Edid.EdidUtil.DISPLAY_DEVICE dd = default(Edid.EdidUtil.DISPLAY_DEVICE);
				dd.cb = Marshal.SizeOf(typeof(Edid.EdidUtil.DISPLAY_DEVICE));
				uint dev = 0U;
				bool bFoundDevice = false;
				while (Edid.EdidUtil.EnumDisplayDevices(null, dev, ref dd, 0U) && !bFoundDevice)
				{
					Edid.EdidUtil.DISPLAY_DEVICE ddMon = default(Edid.EdidUtil.DISPLAY_DEVICE);
					ddMon.cb = Marshal.SizeOf(typeof(Edid.EdidUtil.DISPLAY_DEVICE));
					uint devMon = 0U;
					NLogService.WriteLog(string.Format("DeviceName2222:{0}, DeviceString:{1}, StateFlags:{2}, DeviceID:{3}, DeviceKey:{4}", new object[]
					{
						dd.DeviceName,
						dd.DeviceString,
						dd.StateFlags,
						dd.DeviceID,
						dd.DeviceKey
					}));
					while (Edid.EdidUtil.EnumDisplayDevices(dd.DeviceName, devMon, ref ddMon, 0U) && !bFoundDevice)
					{
						if ((ddMon.StateFlags & Edid.EdidUtil.DisplayDeviceStateFlags.AttachedToDesktop) != (Edid.EdidUtil.DisplayDeviceStateFlags)0 && (ddMon.StateFlags & Edid.EdidUtil.DisplayDeviceStateFlags.MirroringDriver) == (Edid.EdidUtil.DisplayDeviceStateFlags)0)
						{
							NLogService.WriteLog(string.Concat(new string[]
							{
								"PnpDeviceId:",
								ddMon.DeviceID,
								",",
								ddMon.DeviceKey,
								",",
								ddMon.DeviceName,
								",",
								ddMon.DeviceString
							}));
							string DeviceID;
							bFoundDevice = Edid.EdidUtil.GetActualEDID(out DeviceID, lsi);
						}
						devMon += 1U;
						ddMon = default(Edid.EdidUtil.DISPLAY_DEVICE);
						ddMon.cb = Marshal.SizeOf(typeof(Edid.EdidUtil.DISPLAY_DEVICE));
					}
					dd = default(Edid.EdidUtil.DISPLAY_DEVICE);
					dd.cb = Marshal.SizeOf(typeof(Edid.EdidUtil.DISPLAY_DEVICE));
					dev += 1U;
				}
				return lsi.ToArray();
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x00020118 File Offset: 0x0001E318
			private static bool GetActualEDID(out string DeviceID, List<Edid> lsi)
			{
				IntPtr pGuid = Marshal.AllocHGlobal(Marshal.SizeOf<Guid>(Edid.EdidUtil.GUID_CLASS_MONITOR));
				Marshal.StructureToPtr<Guid>(Edid.EdidUtil.GUID_CLASS_MONITOR, pGuid, false);
				IntPtr hDevInfo = Edid.EdidUtil.SetupDiGetClassDevsEx(pGuid, null, (IntPtr)0, 2, (IntPtr)0, null, (IntPtr)0);
				DeviceID = string.Empty;
				int i = 0;
				while (Marshal.GetLastWin32Error() != 259)
				{
					Edid.EdidUtil.SP_DEVINFO_DATA devInfoData = default(Edid.EdidUtil.SP_DEVINFO_DATA);
					devInfoData.cbSize = Marshal.SizeOf(typeof(Edid.EdidUtil.SP_DEVINFO_DATA));
					if (Edid.EdidUtil.SetupDiEnumDeviceInfo(hDevInfo, i, ref devInfoData) > 0)
					{
						StringBuilder instanceIdBuilder = new StringBuilder(256);
						int requiredSize;
						if (Edid.EdidUtil.SetupDiGetDeviceInstanceId(hDevInfo, ref devInfoData, instanceIdBuilder, instanceIdBuilder.Capacity, out requiredSize))
						{
							DeviceID = instanceIdBuilder.ToString();
							NLogService.WriteLog("Found DeviceID: " + DeviceID);
						}
						string driverValue = string.Empty;
						byte[] propBuffer = new byte[256];
						uint propType;
						uint reqSize;
						if (Edid.EdidUtil.SetupDiGetDeviceRegistryProperty(hDevInfo, ref devInfoData, 9U, out propType, propBuffer, (uint)propBuffer.Length, out reqSize))
						{
							driverValue = Encoding.Unicode.GetString(propBuffer).TrimEnd(new char[1]);
						}
						NLogService.WriteLog("DeviceID: " + DeviceID + ", Driver: " + driverValue);
						UIntPtr hDevRegKey = Edid.EdidUtil.SetupDiOpenDevRegKey(hDevInfo, ref devInfoData, 1, 0, 1, 131097);
						NLogService.WriteLog(string.Format("devInfoData:{0}", devInfoData.ClassGuid));
						Edid si = Edid.EdidUtil.PullEDID(hDevRegKey);
						if (si != null)
						{
							lsi.Add(si);
						}
						Edid.EdidUtil.RegCloseKey(hDevRegKey);
					}
					i++;
				}
				Marshal.FreeHGlobal(pGuid);
				return true;
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x00020284 File Offset: 0x0001E484
			private static Edid PullEDID([NativeInteger] UIntPtr hDevRegKey)
			{
				Edid si = null;
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
					retValue = Edid.EdidUtil.RegEnumValue(hDevRegKey, i, valueName, ref ActualValueNameLength, (IntPtr)0, (IntPtr)0, pEDIdata, ref size);
					string data = valueName.ToString();
					NLogService.WriteLog("valueName:" + data);
					if (retValue == 0U && data.Contains("EDID") && size >= 1)
					{
						byte[] actualData = new byte[size];
						Marshal.Copy(pEDIdata, actualData, 0, size);
						si = new Edid(actualData);
					}
					i += 1U;
				}
				Marshal.FreeHGlobal(pEDIdata);
				return si;
			}

			// Token: 0x06000467 RID: 1127
			[DllImport("advapi32.dll", SetLastError = true)]
			private static extern uint RegEnumValue([NativeInteger] UIntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcValueName, [NativeInteger] IntPtr lpReserved, [NativeInteger] IntPtr lpType, [NativeInteger] IntPtr lpData, ref int lpcbData);

			// Token: 0x06000468 RID: 1128
			[DllImport("setupapi.dll")]
			[return: NativeInteger]
			private static extern IntPtr SetupDiGetClassDevsEx([NativeInteger] IntPtr ClassGuid, [MarshalAs(UnmanagedType.LPStr)] string enumerator, [NativeInteger] IntPtr hwndParent, int Flags, [NativeInteger] IntPtr DeviceInfoSet, [MarshalAs(UnmanagedType.LPStr)] string MachineName, [NativeInteger] IntPtr Reserved);

			// Token: 0x06000469 RID: 1129
			[DllImport("setupapi.dll", SetLastError = true)]
			private static extern int SetupDiEnumDeviceInfo([NativeInteger] IntPtr DeviceInfoSet, int MemberIndex, ref Edid.EdidUtil.SP_DEVINFO_DATA DeviceInterfaceData);

			// Token: 0x0600046A RID: 1130
			[DllImport("Setupapi", CharSet = CharSet.Auto, SetLastError = true)]
			[return: NativeInteger]
			private static extern UIntPtr SetupDiOpenDevRegKey([NativeInteger] IntPtr hDeviceInfoSet, ref Edid.EdidUtil.SP_DEVINFO_DATA deviceInfoData, int scope, int hwProfile, int parameterRegistryValueKind, int samDesired);

			// Token: 0x0600046B RID: 1131
			[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref Edid.EdidUtil.SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, int DeviceInstanceIdSize, out int RequiredSize);

			// Token: 0x0600046C RID: 1132
			[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref Edid.EdidUtil.SP_DEVINFO_DATA DeviceInfoData, uint Property, out uint PropertyRegDataType, byte[] PropertyBuffer, uint PropertyBufferSize, out uint RequiredSize);

			// Token: 0x0600046D RID: 1133
			[DllImport("user32.dll")]
			private static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref Edid.EdidUtil.DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

			// Token: 0x0600046E RID: 1134
			[DllImport("advapi32.dll", SetLastError = true)]
			private static extern int RegCloseKey([NativeInteger] UIntPtr hKey);

			// Token: 0x04000430 RID: 1072
			private const int DICS_FLAG_GLOBAL = 1;

			// Token: 0x04000431 RID: 1073
			private const int DIREG_DEV = 1;

			// Token: 0x04000432 RID: 1074
			private const int KEY_READ = 131097;

			// Token: 0x04000433 RID: 1075
			private const int ERROR_SUCCESS = 0;

			// Token: 0x04000434 RID: 1076
			private static readonly Guid GUID_CLASS_MONITOR = new Guid(1295444334U, 58149, 4558, 191, 193, 8, 0, 43, 225, 3, 24);

			// Token: 0x04000435 RID: 1077
			private const int DIGCF_PRESENT = 2;

			// Token: 0x04000436 RID: 1078
			private const int ERROR_NO_MORE_ITEMS = 259;

			// Token: 0x04000437 RID: 1079
			private const uint SPDRP_DRIVER = 9U;

			// Token: 0x04000438 RID: 1080
			private const uint SPDRP_HARDWAREID = 11U;

			// Token: 0x020000D7 RID: 215
			[Flags]
			private enum DisplayDeviceStateFlags
			{
				// Token: 0x040005D3 RID: 1491
				AttachedToDesktop = 1,
				// Token: 0x040005D4 RID: 1492
				MultiDriver = 2,
				// Token: 0x040005D5 RID: 1493
				PrimaryDevice = 4,
				// Token: 0x040005D6 RID: 1494
				MirroringDriver = 8,
				// Token: 0x040005D7 RID: 1495
				VGACompatible = 16,
				// Token: 0x040005D8 RID: 1496
				Removable = 32,
				// Token: 0x040005D9 RID: 1497
				ModesPruned = 134217728,
				// Token: 0x040005DA RID: 1498
				Remote = 67108864,
				// Token: 0x040005DB RID: 1499
				Disconnect = 33554432
			}

			// Token: 0x020000D8 RID: 216
			private struct DISPLAY_DEVICE
			{
				// Token: 0x040005DC RID: 1500
				[MarshalAs(UnmanagedType.U4)]
				public int cb;

				// Token: 0x040005DD RID: 1501
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
				public string DeviceName;

				// Token: 0x040005DE RID: 1502
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
				public string DeviceString;

				// Token: 0x040005DF RID: 1503
				[MarshalAs(UnmanagedType.U4)]
				public Edid.EdidUtil.DisplayDeviceStateFlags StateFlags;

				// Token: 0x040005E0 RID: 1504
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
				public string DeviceID;

				// Token: 0x040005E1 RID: 1505
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
				public string DeviceKey;
			}

			// Token: 0x020000D9 RID: 217
			private struct SP_DEVINFO_DATA
			{
				// Token: 0x040005E2 RID: 1506
				public int cbSize;

				// Token: 0x040005E3 RID: 1507
				public Guid ClassGuid;

				// Token: 0x040005E4 RID: 1508
				public uint DevInst;

				// Token: 0x040005E5 RID: 1509
				[NativeInteger]
				public IntPtr Reserved;
			}
		}
	}
}
