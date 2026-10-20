using System;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000038 RID: 56
	public class MonitorFullInfo : IEquatable<MonitorFullInfo>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		public IntPtr Hmon { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
		public string GdiName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public string PnpDeviceID { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000EFDD File Offset: 0x0000D1DD
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000EFE5 File Offset: 0x0000D1E5
		public EdidData EdidData { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000EFEE File Offset: 0x0000D1EE
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000EFF6 File Offset: 0x0000D1F6
		public string Manufacturer { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000EFFF File Offset: 0x0000D1FF
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000F007 File Offset: 0x0000D207
		public string ModelName { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000F010 File Offset: 0x0000D210
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000F018 File Offset: 0x0000D218
		public string Lens { get; set; }

		// Token: 0x06000232 RID: 562 RVA: 0x0000F024 File Offset: 0x0000D224
		public MonitorFullInfo Clone()
		{
			MonitorFullInfo monitorFullInfo = new MonitorFullInfo();
			monitorFullInfo.Hmon = this.Hmon;
			monitorFullInfo.GdiName = this.GdiName;
			monitorFullInfo.PnpDeviceID = this.PnpDeviceID;
			EdidData edidData = this.EdidData;
			monitorFullInfo.EdidData = ((edidData != null) ? edidData.Clone() : null);
			monitorFullInfo.Manufacturer = this.Manufacturer;
			monitorFullInfo.ModelName = this.ModelName;
			monitorFullInfo.Lens = this.Lens;
			return monitorFullInfo;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000F098 File Offset: 0x0000D298
		public bool Equals(MonitorFullInfo other)
		{
			return other != null && (this == other || (this.Hmon.Equals(other.Hmon) && string.Equals(this.GdiName, other.GdiName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.PnpDeviceID, other.PnpDeviceID, StringComparison.OrdinalIgnoreCase) && string.Equals(this.Manufacturer, other.Manufacturer, StringComparison.OrdinalIgnoreCase) && string.Equals(this.ModelName, other.ModelName, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000F11B File Offset: 0x0000D31B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MonitorFullInfo);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000F12C File Offset: 0x0000D32C
		public override int GetHashCode()
		{
			return ((((((17 * 23 + this.Hmon.GetHashCode()) * 23 + ((this.GdiName != null) ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.GdiName) : 0)) * 23 + ((this.PnpDeviceID != null) ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.PnpDeviceID) : 0)) * 23 + ((this.EdidData != null) ? this.EdidData.GetHashCode() : 0)) * 23 + ((this.Manufacturer != null) ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.Manufacturer) : 0)) * 23 + ((this.ModelName != null) ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.ModelName) : 0)) * 23 + ((this.Lens != null) ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.Lens) : 0);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F202 File Offset: 0x0000D402
		public static bool operator ==(MonitorFullInfo left, MonitorFullInfo right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F216 File Offset: 0x0000D416
		public static bool operator !=(MonitorFullInfo left, MonitorFullInfo right)
		{
			return !(left == right);
		}
	}
}
