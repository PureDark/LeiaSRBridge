using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000038D8 File Offset: 0x00002CD8
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038C4 File Offset: 0x00002CC4
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
