using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020001FA RID: 506
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000033F4 File Offset: 0x000027F4
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000033DC File Offset: 0x000027DC
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000033C8 File Offset: 0x000027C8
		public Exception(string message) : base(message)
		{
		}
	}
}
