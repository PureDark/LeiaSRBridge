using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002648 File Offset: 0x00001A48
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002630 File Offset: 0x00001A30
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000261C File Offset: 0x00001A1C
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x0400007B RID: 123
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
