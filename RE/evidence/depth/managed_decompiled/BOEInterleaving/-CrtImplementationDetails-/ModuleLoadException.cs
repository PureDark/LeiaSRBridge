using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003438 File Offset: 0x00002838
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003420 File Offset: 0x00002820
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000340C File Offset: 0x0000280C
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x0400028B RID: 651
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
