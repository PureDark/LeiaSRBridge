using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace std
{
	// Token: 0x0200025A RID: 602
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	internal struct exception_ptr
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000012BC File Offset: 0x000006BC
		public unsafe static void <MarshalCopy>(exception_ptr* A_0, exception_ptr* A_1)
		{
			if (A_0 != null)
			{
				*(long*)A_0 = 0L;
				*(long*)(A_0 + 8L / (long)sizeof(exception_ptr)) = 0L;
				<Module>.__ExceptionPtrCopy((void*)A_0, (void*)A_1);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000012E0 File Offset: 0x000006E0
		public unsafe static void <MarshalDestroy>(exception_ptr* A_0)
		{
			<Module>.__ExceptionPtrDestroy((void*)A_0);
		}

		// Token: 0x040003F6 RID: 1014
		private long <alignment\u0020member>;
	}
}
