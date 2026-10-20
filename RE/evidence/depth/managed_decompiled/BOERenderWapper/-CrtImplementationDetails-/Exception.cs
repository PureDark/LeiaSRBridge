using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020003CB RID: 971
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00003DA4 File Offset: 0x000031A4
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003D8C File Offset: 0x0000318C
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003D78 File Offset: 0x00003178
		public Exception(string message) : base(message)
		{
		}
	}
}
