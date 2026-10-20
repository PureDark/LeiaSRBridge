using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020003CC RID: 972
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00003DE8 File Offset: 0x000031E8
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003DD0 File Offset: 0x000031D0
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003DBC File Offset: 0x000031BC
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04000502 RID: 1282
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
