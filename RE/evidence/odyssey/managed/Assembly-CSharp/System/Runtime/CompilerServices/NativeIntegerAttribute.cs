using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000003 RID: 3
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NativeIntegerAttribute : Attribute
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public NativeIntegerAttribute()
		{
			this.TransformFlags = new bool[]
			{
				true
			};
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		public NativeIntegerAttribute(bool[] A_1)
		{
			this.TransformFlags = A_1;
		}

		// Token: 0x04000001 RID: 1
		public readonly bool[] TransformFlags;
	}
}
