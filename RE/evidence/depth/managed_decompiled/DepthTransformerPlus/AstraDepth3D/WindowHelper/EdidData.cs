using System;
using System.Linq;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000039 RID: 57
	public class EdidData
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000F22A File Offset: 0x0000D42A
		public EdidData(byte[] data)
		{
			this._data = data;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F239 File Offset: 0x0000D439
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F241 File Offset: 0x0000D441
		public EdidData Clone()
		{
			return new EdidData(this._data.ToArray<byte>());
		}

		// Token: 0x04000182 RID: 386
		private readonly byte[] _data;
	}
}
