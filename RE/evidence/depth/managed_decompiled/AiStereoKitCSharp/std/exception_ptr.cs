using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace std
{
	// Token: 0x02000249 RID: 585
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	internal struct exception_ptr
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000011A4 File Offset: 0x000005A4
		public unsafe static void <MarshalCopy>(exception_ptr* A_0, exception_ptr* A_1)
		{
			if (A_0 != null)
			{
				*(long*)A_0 = 0L;
				*(long*)(A_0 + 8L / (long)sizeof(exception_ptr)) = 0L;
				<Module>.__ExceptionPtrCopy((void*)A_0, (void*)A_1);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000011C8 File Offset: 0x000005C8
		public unsafe static void <MarshalDestroy>(exception_ptr* A_0)
		{
			<Module>.__ExceptionPtrDestroy((void*)A_0);
		}

		// Token: 0x040002F0 RID: 752
		private long <alignment\u0020member>;
	}
}
