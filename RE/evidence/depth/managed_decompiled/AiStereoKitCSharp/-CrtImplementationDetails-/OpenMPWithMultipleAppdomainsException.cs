using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200033D RID: 829
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003AA8 File Offset: 0x00002EA8
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003A94 File Offset: 0x00002E94
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
