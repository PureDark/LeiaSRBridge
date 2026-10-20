using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002AE8 File Offset: 0x00001EE8
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002AD4 File Offset: 0x00001ED4
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
