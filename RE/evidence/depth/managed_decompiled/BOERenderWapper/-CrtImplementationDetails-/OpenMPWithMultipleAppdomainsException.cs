using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020003CF RID: 975
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004288 File Offset: 0x00003688
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004274 File Offset: 0x00003674
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
