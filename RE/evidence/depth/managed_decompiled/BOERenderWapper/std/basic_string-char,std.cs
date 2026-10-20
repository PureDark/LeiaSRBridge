using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace std
{
	// Token: 0x0200027B RID: 635
	[NativeCppClass]
	[UnsafeValueType]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	internal struct basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00001F94 File Offset: 0x00001394
		public unsafe static void <MarshalCopy>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_1)
		{
			if (A_0 != null)
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0, A_1);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002078 File Offset: 0x00001478
		public unsafe static void <MarshalDestroy>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
		{
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)A_0);
				throw;
			}
		}

		// Token: 0x04000401 RID: 1025
		private long <alignment\u0020member>;

		// Token: 0x0200027C RID: 636
		[NativeCppClass]
		internal enum _Allocation_policy
		{

		}

		// Token: 0x0200027D RID: 637
		[NativeCppClass]
		internal enum _Construct_strategy : byte
		{

		}
	}
}
