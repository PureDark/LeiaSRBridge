using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000339 RID: 825
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x060000BD RID: 189 RVA: 0x000035C4 File Offset: 0x000029C4
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000035AC File Offset: 0x000029AC
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003598 File Offset: 0x00002998
		public Exception(string message) : base(message)
		{
		}
	}
}
