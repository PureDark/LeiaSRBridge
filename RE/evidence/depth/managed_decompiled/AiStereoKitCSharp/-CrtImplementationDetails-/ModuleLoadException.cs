using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200033A RID: 826
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003608 File Offset: 0x00002A08
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000035F0 File Offset: 0x000029F0
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000035DC File Offset: 0x000029DC
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x0400039D RID: 925
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
