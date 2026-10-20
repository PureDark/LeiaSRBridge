using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002604 File Offset: 0x00001A04
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000025EC File Offset: 0x000019EC
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000025D8 File Offset: 0x000019D8
		public Exception(string message) : base(message)
		{
		}
	}
}
