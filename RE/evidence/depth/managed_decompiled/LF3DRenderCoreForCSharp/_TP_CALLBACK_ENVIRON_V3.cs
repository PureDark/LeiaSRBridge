using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x0200012F RID: 303
[NativeCppClass]
[StructLayout(LayoutKind.Sequential, Size = 72)]
internal struct _TP_CALLBACK_ENVIRON_V3
{
	// Token: 0x04000194 RID: 404
	private long <alignment\u0020member>;

	// Token: 0x02000130 RID: 304
	[NativeCppClass]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	public struct <unnamed-type-u>
	{
		// Token: 0x04000195 RID: 405
		[FieldOffset(0)]
		private int <alignment\u0020member>;

		// Token: 0x02000131 RID: 305
		[NativeCppClass]
		[CLSCompliant(false)]
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		public struct <unnamed-type-s>
		{
			// Token: 0x04000196 RID: 406
			private int <alignment\u0020member>;
		}
	}
}
