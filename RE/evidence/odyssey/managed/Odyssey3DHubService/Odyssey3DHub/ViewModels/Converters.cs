using System;
using System.Runtime.CompilerServices;

namespace Odyssey3DHub.ViewModels
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public static class Converters
	{
		// Token: 0x0400008B RID: 139
		public static readonly OpacityConverter OpacityConverterInstance = new OpacityConverter();

		// Token: 0x0400008C RID: 140
		public static readonly InverseBooleanConverter InverseBooleanConverterInstance = new InverseBooleanConverter();
	}
}
