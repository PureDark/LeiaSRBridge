using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000037 RID: 55
	public class EdidDetail
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
		public static EdidDetail Parse(byte[] data)
		{
			EdidDetail edidDetail = new EdidDetail();
			Encoding.ASCII.GetString(data);
			edidDetail.Manufacturer = EdidDetail.Manufacturer_Name(data);
			edidDetail.ModelName = EdidDetail.GetModelName(data);
			edidDetail.SN = EdidDetail.Serial_Number2(data);
			int DTD_START = 54;
			int HORIZONTAL_DISPLAY_TOP_OFFSET = 4;
			int HORIZONTAL_DISPLAY_TOP_MASK = 15;
			int VERTICAL_DISPLAY_TOP_MASK = 15;
			edidDetail.HorizontalDisplaySize = ((data[DTD_START + 14] >> HORIZONTAL_DISPLAY_TOP_OFFSET & HORIZONTAL_DISPLAY_TOP_MASK) << 8 | (int)data[DTD_START + 12]);
			edidDetail.VerticalDisplaySize = (((int)data[DTD_START + 14] & VERTICAL_DISPLAY_TOP_MASK) << 8 | (int)data[DTD_START + 13]);
			edidDetail.HorizontalImageSize = (int)data[21];
			edidDetail.VerticalImageSize = (int)data[22];
			return edidDetail;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000E968 File Offset: 0x0000CB68
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000E970 File Offset: 0x0000CB70
		public string Manufacturer { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000E979 File Offset: 0x0000CB79
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000E981 File Offset: 0x0000CB81
		public string SN { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000E98A File Offset: 0x0000CB8A
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000E992 File Offset: 0x0000CB92
		public string ModelName { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000E99B File Offset: 0x0000CB9B
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000E9A3 File Offset: 0x0000CBA3
		public int HorizontalDisplaySize { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public int HorizontalImageSize { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000E9BD File Offset: 0x0000CBBD
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000E9C5 File Offset: 0x0000CBC5
		public int VerticalDisplaySize { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000E9CE File Offset: 0x0000CBCE
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000E9D6 File Offset: 0x0000CBD6
		public int VerticalImageSize { get; private set; }

		// Token: 0x06000214 RID: 532 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
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
			string str = EdidDetail.ToCharByASCIIShort(a).ToString() ?? "";
			string B = EdidDetail.ToCharByASCIIShort(b).ToString() ?? "";
			string C = EdidDetail.ToCharByASCIIShort(c).ToString() ?? "";
			return str + B + C;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000EA78 File Offset: 0x0000CC78
		public static string Product_Id(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 128)
			{
				return "";
			}
			return ((int)EDID[11] << 8 | (int)EDID[10]).ToString("X");
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		private static string GetModelCode(byte[] EDID)
		{
			for (int i = 0; i < 4; i++)
			{
				int offset = 54 + i * 18;
				if (offset + 17 < EDID.Length && EDID[offset] == 0 && EDID[offset + 1] == 0 && EDID[offset + 2] == 252)
				{
					byte[] modelBytes = new byte[13];
					Array.Copy(EDID, offset + 5, modelBytes, 0, 13);
					return Encoding.ASCII.GetString(modelBytes).Trim(new char[]
					{
						'\0',
						' ',
						'\t',
						'\n',
						'\r'
					});
				}
			}
			return "";
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public static string Serial_Number(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 128)
			{
				return "";
			}
			return ((int)EDID[15] << 24 | (int)EDID[14] << 16 | (int)EDID[13] << 8 | (int)EDID[12]).ToString();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public static string Serial_Number2(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 128)
			{
				return "";
			}
			string serialFromDescriptor = EdidDetail.GetSerialNumberFromDTD(EDID);
			if (!string.IsNullOrEmpty(serialFromDescriptor))
			{
				return serialFromDescriptor;
			}
			return EdidDetail.GetSerialFromBasicBlock(EDID);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		private static string GetSerialFromDescriptor(byte[] EDID)
		{
			for (int i = 0; i < 4; i++)
			{
				int descriptorOffset = 54 + i * 18;
				if (descriptorOffset + 17 < EDID.Length && EDID[descriptorOffset] == 0 && EDID[descriptorOffset + 1] == 0 && EDID[descriptorOffset + 2] == 255)
				{
					byte[] serialBytes = new byte[13];
					Array.Copy(EDID, descriptorOffset + 5, serialBytes, 0, 13);
					return Encoding.ASCII.GetString(serialBytes).Trim().TrimEnd(new char[]
					{
						'\0',
						' '
					});
				}
			}
			return "";
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		private static string GetSerialFromBasicBlock(byte[] EDID)
		{
			string result;
			try
			{
				int serialInt = (int)EDID[15] << 24 | (int)EDID[14] << 16 | (int)EDID[13] << 8 | (int)EDID[12];
				if (serialInt == 0)
				{
					result = "Not Specified";
				}
				else
				{
					result = serialInt.ToString();
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public static string GetSerialNumberFromDTD(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 128)
			{
				return "";
			}
			for (int i = 0; i < 4; i++)
			{
				int dtdOffset = 54 + i * 18;
				if (dtdOffset + 17 >= EDID.Length)
				{
					break;
				}
				string serial = EdidDetail.ParseSNFromDTDBlock(EDID, dtdOffset);
				if (!string.IsNullOrEmpty(serial))
				{
					return serial;
				}
			}
			return "";
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		private static string ParseSNFromDTDBlock(byte[] EDID, int offset)
		{
			if ((ushort)((int)EDID[offset + 1] << 8 | (int)EDID[offset]) == 0 && EDID[offset] == 0 && EDID[offset + 1] == 0 && EDID[offset + 2] == 0 && EDID[offset + 3] == 255 && EDID[offset + 4] == 0)
			{
				return EdidDetail.ExtractSerialFromDescriptor(EDID, offset);
			}
			return "";
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000ED28 File Offset: 0x0000CF28
		private static string ExtractSerialFromDescriptor(byte[] EDID, int offset)
		{
			string result;
			try
			{
				byte[] serialBytes = new byte[13];
				Array.Copy(EDID, offset + 5, serialBytes, 0, 13);
				string serial = Encoding.ASCII.GetString(serialBytes);
				serial = serial.Trim(new char[]
				{
					'\0',
					' ',
					'\t',
					'\n',
					'\r'
				});
				if (!string.IsNullOrWhiteSpace(serial))
				{
					if (!serial.All((char c) => c == '\0'))
					{
						return serial;
					}
				}
				result = "";
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("解析序列号描述符时出错: " + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		private static string ExtractStringFromDescriptor(byte[] EDID, int offset)
		{
			string result2;
			try
			{
				byte[] stringBytes = new byte[13];
				Array.Copy(EDID, offset + 5, stringBytes, 0, 13);
				string result = Encoding.ASCII.GetString(stringBytes);
				result = result.Trim(new char[]
				{
					'\0',
					' ',
					'\t',
					'\n',
					'\r'
				});
				result2 = (string.IsNullOrWhiteSpace(result) ? "" : result);
			}
			catch
			{
				result2 = "";
			}
			return result2;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000EE4C File Offset: 0x0000D04C
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
					return EdidDetail.ExtractModelNameFromDescriptor(EDID, offset);
				}
			}
			return "Not Specified";
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000EEBC File Offset: 0x0000D0BC
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

		// Token: 0x06000221 RID: 545 RVA: 0x0000EF68 File Offset: 0x0000D168
		public static string Number_Of_Extension_Flag(byte[] EDID)
		{
			if (EDID == null || EDID.Length < 256)
			{
				return "0";
			}
			return (EDID.Length / 128 - 1).ToString();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000EF9B File Offset: 0x0000D19B
		public static char ToCharByASCIIShort(int a)
		{
			return (char)(a + 64);
		}
	}
}
