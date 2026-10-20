using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using ?A0x5d73e011;
using msclr.interop.details;
using std;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00001090 File Offset: 0x00000490
	internal unsafe static void* @new(ulong __unnamed000, void* _Where, __clr_placement_new_t __unnamed002)
	{
		return _Where;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000010F0 File Offset: 0x000004F0
	internal unsafe static exception* {ctor}(exception* A_0, exception* _Other)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		exception* ptr = A_0 / sizeof(exception) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(_Other / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		return A_0;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000111C File Offset: 0x0000051C
	internal unsafe static void {dtor}(exception* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00001AD8 File Offset: 0x00000ED8
	internal unsafe static sbyte* what(exception* A_0)
	{
		ulong num = (ulong)(*(A_0 + 8L));
		return (num != 0UL) ? num : ref <Module>.??_C@_0BC@EOODALEL@Unknown?5exception@;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00001A78 File Offset: 0x00000E78
	internal unsafe static void* __vecDelDtor(exception* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			exception* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.exception.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				exception* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00001C0C File Offset: 0x0000100C
	internal unsafe static void* __vecDelDtor(bad_alloc* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_alloc* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.bad_alloc.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_alloc* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000113C File Offset: 0x0000053C
	internal unsafe static void {dtor}(bad_alloc* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00001C6C File Offset: 0x0000106C
	internal unsafe static void* __vecDelDtor(bad_array_new_length* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_array_new_length* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.bad_array_new_length.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_array_new_length* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00001CCC File Offset: 0x000010CC
	internal unsafe static void {dtor}(bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000115C File Offset: 0x0000055C
	internal unsafe static void {dtor}(exception_ptr* A_0)
	{
		<Module>.__ExceptionPtrDestroy(A_0);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00001170 File Offset: 0x00000570
	internal unsafe static exception_ptr* {ctor}(exception_ptr* A_0, exception_ptr* _Rhs)
	{
		*A_0 = 0L;
		*(A_0 + 8L) = 0L;
		<Module>.__ExceptionPtrCopy(A_0, _Rhs);
		return A_0;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00001B78 File Offset: 0x00000F78
	internal unsafe static sbyte* what(bad_variant_access* A_0)
	{
		return ref <Module>.??_C@_0BD@FGIAGBIG@bad?5variant?5access@;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00001B58 File Offset: 0x00000F58
	internal unsafe static void {dtor}(bad_variant_access* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000011C8 File Offset: 0x000005C8
	internal unsafe static bad_variant_access* {ctor}(bad_variant_access* A_0, bad_variant_access* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_variant_access* ptr = A_0 / sizeof(bad_variant_access) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(A_0 / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_variant_access@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00001AF8 File Offset: 0x00000EF8
	internal unsafe static void* __vecDelDtor(bad_variant_access* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_variant_access* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.bad_variant_access.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_variant_access* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00001B8C File Offset: 0x00000F8C
	internal unsafe static void* __vecDelDtor(bad_cast* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_cast* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.bad_cast.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_cast* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00001BEC File Offset: 0x00000FEC
	internal unsafe static void {dtor}(bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00001228 File Offset: 0x00000628
	internal unsafe static bad_cast* {ctor}(bad_cast* A_0, bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_cast* ptr = A_0 / sizeof(bad_cast) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(A_0 / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_cast@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000012E8 File Offset: 0x000006E8
	internal unsafe static bad_array_new_length* {ctor}(bad_array_new_length* A_0, bad_array_new_length* A_0)
	{
		<Module>.std.bad_alloc.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7bad_array_new_length@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.bad_alloc.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00001288 File Offset: 0x00000688
	internal unsafe static bad_alloc* {ctor}(bad_alloc* A_0, bad_alloc* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_alloc* ptr = A_0 / sizeof(bad_alloc) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(A_0 / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_alloc@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00001724 File Offset: 0x00000B24
	internal unsafe static _Iterator_base12* {ctor}(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*(A_0 + 8L) = 0L;
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00001330 File Offset: 0x00000730
	internal unsafe static _Iterator_base12* =(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00001D6C File Offset: 0x0000116C
	internal unsafe static sbyte* what(bad_weak_ptr* A_0)
	{
		return ref <Module>.??_C@_0N@FJHHFFAF@bad_weak_ptr@;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00001CEC File Offset: 0x000010EC
	internal unsafe static void* __vecDelDtor(bad_weak_ptr* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_weak_ptr* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.bad_weak_ptr.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_weak_ptr* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00001D4C File Offset: 0x0000114C
	internal unsafe static void {dtor}(bad_weak_ptr* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00001344 File Offset: 0x00000744
	internal unsafe static bad_weak_ptr* {ctor}(bad_weak_ptr* A_0, bad_weak_ptr* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_weak_ptr* ptr = A_0 / sizeof(bad_weak_ptr) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(A_0 / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_weak_ptr@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000013A4 File Offset: 0x000007A4
	internal static ref char PtrToStringChars(string s)
	{
		ref byte bp = s;
		if (ref bp != null)
		{
			bp = (long)RuntimeHelpers.OffsetToStringData + ref bp;
		}
		return ref bp;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000013C0 File Offset: 0x000007C0
	internal static ulong GetAnsiStringSize(string _str)
	{
		ref byte ptr = _str;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		ref char _pinned_ptr = ref ptr;
		ulong _size = (ulong)<Module>.WideCharToMultiByte(3U, 1024, ref _pinned_ptr, _str.Length, null, 0, null, null);
		if (_size == 0UL && _str.Length != 0)
		{
			throw new ArgumentException("Conversion from WideChar to MultiByte failed.  Please check the content of the string and/or locale settings.");
		}
		return _size + 1UL;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00001414 File Offset: 0x00000814
	internal unsafe static void WriteAnsiString(sbyte* _buf, ulong _size, string _str)
	{
		ref byte ptr = _str;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		ref char _pinned_ptr = ref ptr;
		if (_size > 2147483647UL)
		{
			throw new ArgumentOutOfRangeException("Size of string exceeds INT_MAX.");
		}
		ulong _written = <Module>.WideCharToMultiByte(3U, 1024, ref _pinned_ptr, _str.Length, _buf, (int)_size, null, null);
		if (_written < _size && (_written != null || _size == 1UL))
		{
			*(byte*)(_written / sizeof(sbyte) + _buf) = 0;
			return;
		}
		throw new ArgumentException("Conversion from WideChar to MultiByte failed.  Please check the content of the string and/or locale settings.");
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000016C0 File Offset: 0x00000AC0
	internal unsafe static sbyte* release(char_buffer<char>* A_0)
	{
		sbyte* result = *A_0;
		*A_0 = 0L;
		return result;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000016D4 File Offset: 0x00000AD4
	internal unsafe static sbyte* get(char_buffer<char>* A_0)
	{
		return *A_0;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000016E4 File Offset: 0x00000AE4
	internal unsafe static void {dtor}(char_buffer<char>* A_0)
	{
		<Module>.delete[](*A_0);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000016F8 File Offset: 0x00000AF8
	internal unsafe static char_buffer<char>* {ctor}(char_buffer<char>* A_0, ulong _size)
	{
		*A_0 = <Module>.new[](_size);
		return A_0;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000031C8 File Offset: 0x000025C8
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000320C File Offset: 0x0000260C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDelDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00003250 File Offset: 0x00002650
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindVecDtor(method pVecDtor, void* ptr, ulong size, int count, method pDtor)
	{
		try
		{
			calli(System.Void(System.Void*,System.UInt64,System.Int32,System.Void (System.Void*)), ptr, size, count, pDtor, pVecDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000333C File Offset: 0x0000273C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void __ehvec_dtor(void* ptr, ulong size, ulong count, method destructor)
	{
		bool success = false;
		ptr = (void*)(size * count + (byte*)ptr);
		try
		{
			for (;;)
			{
				long num = (long)count;
				count -= 1UL;
				if (num == 0L)
				{
					break;
				}
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
			success = true;
		}
		finally
		{
			if (!success)
			{
				<Module>.__ArrayUnwind(ptr, size, count, destructor);
			}
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000032AC File Offset: 0x000026AC
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static int ArrayUnwindFilter(_EXCEPTION_POINTERS* pExPtrs)
	{
		EHExceptionRecord* pExcept = *(long*)pExPtrs;
		if (*(int*)pExcept != -529697949)
		{
			return 0;
		}
		*<Module>.__current_exception() = pExcept;
		long num = *(long*)(pExPtrs + 8L / (long)sizeof(_EXCEPTION_POINTERS));
		*<Module>.__current_exception_context() = num;
		<Module>.terminate();
		return 0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000032E4 File Offset: 0x000026E4
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void __ArrayUnwind(void* ptr, ulong size, ulong count, method destructor)
	{
		try
		{
			for (ulong i = 0UL; i != count; i += 1UL)
			{
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
		}
		catch when (endfilter(<Module>.?A0x9c83a709.ArrayUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000339C File Offset: 0x0000279C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void __ehvec_dtor(void* ptr, ulong size, int count, method destructor)
	{
		<Module>.__ehvec_dtor(ptr, size, (ulong)((long)count), destructor);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003BF0 File Offset: 0x00002FF0
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000035A4 File Offset: 0x000029A4
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000035B8 File Offset: 0x000029B8
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000036D4 File Offset: 0x00002AD4
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000036EC File Offset: 0x00002AEC
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000373C File Offset: 0x00002B3C
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* pHost = null;
		int hr;
		try
		{
			Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			pHost = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), riid).ToPointer();
			goto IL_36;
		}
		catch (System.Exception e)
		{
			hr = Marshal.GetHRForException(e);
		}
		if (hr < 0)
		{
			return hr;
		}
		IL_36:
		long num = *(*(long*)pHost + 104L);
		hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), pHost, ppUnk, num);
		ICorRuntimeHost* ptr = pHost;
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
		return hr;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000037BC File Offset: 0x00002BBC
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000037D8 File Offset: 0x00002BD8
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* pUnk = null;
		int hresult = <Module>.__get_default_appdomain(&pUnk);
		if (hresult >= 0)
		{
			try
			{
				IntPtr pUnk2 = new IntPtr((void*)pUnk);
				return (AppDomain)Marshal.GetObjectForIUnknown(pUnk2);
			}
			finally
			{
				<Module>.__release_appdomain(pUnk);
			}
		}
		Marshal.ThrowExceptionForHR(hresult);
		return null;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003838 File Offset: 0x00002C38
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* pClrHost = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), riid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)pClrHost + 64L);
			uint id = (uint)appDomain.Id;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), pClrHost, id, function, cookie, num);
			if (hr < 0)
			{
				Marshal.ThrowExceptionForHR(hr);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr = pClrHost;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000038F0 File Offset: 0x00002CF0
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		if (_scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003920 File Offset: 0x00002D20
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003940 File Offset: 0x00002D40
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** pBeg = (void**)(&<Module>.__xc_mp_a);
			if (ref <Module>.__xc_mp_a < ref <Module>.__xc_mp_z)
			{
				while (*(long*)pBeg == 0L)
				{
					pBeg += 8L / (long)sizeof(void*);
					if (pBeg >= (void**)(&<Module>.__xc_mp_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_35:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003994 File Offset: 0x00002D94
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** pBeg = (void**)(&<Module>.__xi_a);
			if (ref <Module>.__xi_a < ref <Module>.__xi_z)
			{
				while (*(long*)pBeg == 0L)
				{
					pBeg += 8L / (long)sizeof(void*);
					if (pBeg >= (void**)(&<Module>.__xi_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_35:
			void** ptr = (void**)(&<Module>.__xc_a);
			if (ref <Module>.__xc_a < ref <Module>.__xc_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_z))
					{
						goto IL_62;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_62:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003A14 File Offset: 0x00002E14
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003A4C File Offset: 0x00002E4C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003A60 File Offset: 0x00002E60
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00001000 File Offset: 0x00000400
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00001014 File Offset: 0x00000414
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00001028 File Offset: 0x00000428
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000103C File Offset: 0x0000043C
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00001050 File Offset: 0x00000450
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00001064 File Offset: 0x00000464
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00001078 File Offset: 0x00000478
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003C44 File Offset: 0x00003044
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003C78 File Offset: 0x00003078
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003C98 File Offset: 0x00003098
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((method*)(&<Module>.__xi_a), (method*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((method*)(&<Module>.__xc_a), (method*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003D28 File Offset: 0x00003128
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.__xc_mp_a), (method*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003D68 File Offset: 0x00003168
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.__xc_ma_a), (method*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003DA4 File Offset: 0x000031A4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003DD0 File Offset: 0x000031D0
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
		{
			<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		}
		void* fiberid = <Module>._getFiberPtrId();
		int nested = 0;
		int done = 0;
		int doInitializeDefaultAppDomain = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (done == 0)
			{
				try
				{
				}
				finally
				{
					void* lock_free = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, fiberid, 0L);
					if (lock_free == null)
					{
						done = 1;
					}
					else if (lock_free == fiberid)
					{
						nested = 1;
						done = 1;
					}
				}
				if (done == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else
			{
				doInitializeDefaultAppDomain = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : doInitializeDefaultAppDomain);
			}
		}
		finally
		{
			if (nested == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (doInitializeDefaultAppDomain != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003A7C File Offset: 0x00002E7C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003A90 File Offset: 0x00002E90
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003AC8 File Offset: 0x00002EC8
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003B00 File Offset: 0x00002F00
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = (Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003ECC File Offset: 0x000032CC
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
	{
		try
		{
			bool lastDomain = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (lastDomain)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (System.Exception exception)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, exception);
		}
		catch (object exception2)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003F40 File Offset: 0x00003340
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003F58 File Offset: 0x00003358
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003F6C File Offset: 0x0000336C
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool allDomainsCountIncremented = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				allDomainsCountIncremented = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (System.Exception exception)
		{
			if (allDomainsCountIncremented)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, exception);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), exception);
		}
		catch (object exception2)
		{
			if (allDomainsCountIncremented)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00004028 File Offset: 0x00003428
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003B3C File Offset: 0x00002F3C
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003B60 File Offset: 0x00002F60
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003B88 File Offset: 0x00002F88
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003BB0 File Offset: 0x00002FB0
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000040B0 File Offset: 0x000034B0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(value);
		}
		return null;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00004598 File Offset: 0x00003998
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000040E0 File Offset: 0x000034E0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType handle = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (handle == null)
		{
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)GCHandle.Alloc(value)).ToPointer();
		}
		else
		{
			((GCHandle)handle).Target = value;
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000412C File Offset: 0x0000352C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType handle = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (handle != null)
		{
			return ((GCHandle)handle).Target;
		}
		return null;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004150 File Offset: 0x00003550
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType handle = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (handle != null)
		{
			((GCHandle)handle).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00004178 File Offset: 0x00003578
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000045B4 File Offset: 0x000039B4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004194 File Offset: 0x00003594
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000041BC File Offset: 0x000035BC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000041D4 File Offset: 0x000035D4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000041EC File Offset: 0x000035EC
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_lock()
	{
		bool retval = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			retval = true;
		}
		return retval;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000420C File Offset: 0x0000360C
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_unlock()
	{
		bool retval = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			retval = true;
		}
		return retval;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000045E4 File Offset: 0x000039E4
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000422C File Offset: 0x0000362C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004240 File Offset: 0x00003640
	[SecurityCritical]
	internal unsafe static int _atexit_helper(method func, ulong* __pexit_list_size, method** __ponexitend_e, method** __ponexitbegin_e)
	{
		method retval = 0L;
		if (func == null)
		{
			return -1;
		}
		if (<Module>.?A0xdf32f105.__global_lock() == 1)
		{
			try
			{
				method* __onexitbegin = (method*)<Module>.DecodePointer(*(long*)__ponexitbegin_e);
				method* __onexitend = (method*)<Module>.DecodePointer(*(long*)__ponexitend_e);
				long num = (long)(__onexitend - __onexitbegin);
				if (*__pexit_list_size - 1UL < (ulong)num >> 3)
				{
					try
					{
						ulong num2 = *__pexit_list_size * 8UL;
						ulong num3 = (num2 < 4096UL) ? num2 : 4096UL;
						IntPtr cb = new IntPtr((int)(num2 + num3));
						IntPtr pv = new IntPtr((void*)__onexitbegin);
						IntPtr tmp_ptr = Marshal.ReAllocHGlobal(pv, cb);
						__onexitend = (method*)((byte*)tmp_ptr.ToPointer() + num);
						__onexitbegin = (method*)tmp_ptr.ToPointer();
						ulong num4 = *__pexit_list_size;
						ulong num5 = (512UL < num4) ? 512UL : num4;
						*__pexit_list_size = num4 + num5;
					}
					catch (OutOfMemoryException)
					{
						IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
						IntPtr pv2 = new IntPtr((void*)__onexitbegin);
						IntPtr tmp_ptr2 = Marshal.ReAllocHGlobal(pv2, cb2);
						__onexitend = (tmp_ptr2.ToPointer() - __onexitbegin) / (IntPtr)sizeof(method) + __onexitend;
						__onexitbegin = (method*)tmp_ptr2.ToPointer();
						*__pexit_list_size += 4UL;
					}
				}
				*(long*)__onexitend = func;
				__onexitend += 8L / (long)sizeof(method);
				retval = func;
				*(long*)__ponexitbegin_e = <Module>.EncodePointer((void*)__onexitbegin);
				*(long*)__ponexitend_e = <Module>.EncodePointer((void*)__onexitend);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				<Module>.?A0xdf32f105.__global_unlock();
			}
			if (retval != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000043B8 File Offset: 0x000037B8
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0xdf32f105.__exit_list_size != 0UL)
		{
			method* onexitbegin_m = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitbegin_m);
			method* onexitend_m = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitend_m);
			if (onexitbegin_m != -1L && onexitbegin_m != null && onexitend_m != null)
			{
				method* onexitbegin_m_saved = onexitbegin_m;
				method* onexitend_m_saved = onexitend_m;
				for (;;)
				{
					onexitend_m -= 8L / (long)sizeof(method);
					if (onexitend_m < onexitbegin_m)
					{
						break;
					}
					if (*(long*)onexitend_m != <Module>.EncodePointer(null))
					{
						void* ptr = <Module>.DecodePointer(*(long*)onexitend_m);
						*(long*)onexitend_m = <Module>.EncodePointer(null);
						calli(System.Void(), ptr);
						method* onexitbegin_m_new = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitbegin_m);
						method* onexitend_m_new = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitend_m);
						if (onexitbegin_m_saved != onexitbegin_m_new || onexitend_m_saved != onexitend_m_new)
						{
							onexitbegin_m_saved = onexitbegin_m_new;
							onexitbegin_m = onexitbegin_m_new;
							onexitend_m_saved = onexitend_m_new;
							onexitend_m = onexitend_m_new;
						}
					}
				}
				IntPtr hglobal = new IntPtr((void*)onexitbegin_m);
				Marshal.FreeHGlobal(hglobal);
			}
			<Module>.?A0xdf32f105.__dealloc_global_lock();
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000045FC File Offset: 0x000039FC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_m()
	{
		int result = 0;
		if (<Module>.?A0xdf32f105.__alloc_global_lock() == 1)
		{
			<Module>.?A0xdf32f105.__onexitbegin_m = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0xdf32f105.__onexitend_m = <Module>.?A0xdf32f105.__onexitbegin_m;
			<Module>.?A0xdf32f105.__exit_list_size = 32UL;
			result = 1;
		}
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004644 File Offset: 0x00003A44
	internal static method _onexit_m(method _Function)
	{
		return (<Module>._atexit_m(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004468 File Offset: 0x00003868
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xdf32f105.__exit_list_size, &<Module>.?A0xdf32f105.__onexitend_m, &<Module>.?A0xdf32f105.__onexitbegin_m);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004660 File Offset: 0x00003A60
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0xdf32f105.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004490 File Offset: 0x00003890
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* onexitbegin_app_domain = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* onexitend_app_domain = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (onexitbegin_app_domain != -1L && onexitbegin_app_domain != null && onexitend_app_domain != null)
				{
					method* onexitbegin_app_domain_saved = onexitbegin_app_domain;
					method* onexitend_app_domain_saved = onexitend_app_domain;
					for (;;)
					{
						do
						{
							onexitend_app_domain -= 8L / (long)sizeof(method);
						}
						while (onexitend_app_domain >= onexitbegin_app_domain && *(long*)onexitend_app_domain == <Module>.EncodePointer(null));
						if (onexitend_app_domain < onexitbegin_app_domain)
						{
							break;
						}
						void* ptr = <Module>.DecodePointer(*(long*)onexitend_app_domain);
						*(long*)onexitend_app_domain = <Module>.EncodePointer(null);
						calli(System.Void(), ptr);
						method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr3 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (onexitbegin_app_domain_saved != ptr2 || onexitend_app_domain_saved != ptr3)
						{
							onexitbegin_app_domain_saved = ptr2;
							onexitbegin_app_domain = ptr2;
							onexitend_app_domain_saved = ptr3;
							onexitend_app_domain = ptr3;
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)onexitbegin_app_domain);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?A0xdf32f105.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000046A4 File Offset: 0x00003AA4
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004570 File Offset: 0x00003970
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x0600006B RID: 107
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x0600006C RID: 108
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x0600006D RID: 109 RVA: 0x000046D4 File Offset: 0x00003AD4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initterm_e(method* pfbegin, method* pfend)
	{
		int ret = 0;
		if (pfbegin < pfend)
		{
			while (ret == 0)
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					ret = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num);
				}
				pfbegin += 8L / (long)sizeof(method);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return ret;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00004704 File Offset: 0x00003B04
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _initterm(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num);
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000472C File Offset: 0x00003B2C
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0000477C File Offset: 0x00003B7C
	[DebuggerStepThrough]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					object obj = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004750 File Offset: 0x00003B50
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000048BF File Offset: 0x00003CBF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int WideCharToMultiByte(uint, uint, char*, int, sbyte*, int, sbyte*, int*);

	// Token: 0x06000073 RID: 115 RVA: 0x000048E0 File Offset: 0x00003CE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* new[](ulong);

	// Token: 0x06000074 RID: 116 RVA: 0x000048B3 File Offset: 0x00003CB3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int BOE_Interleaving_doInterleave(float, float, float, void*, void*, void*, int, int, int, InterMode, int);

	// Token: 0x06000075 RID: 117 RVA: 0x000048A7 File Offset: 0x00003CA7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void BOE_Interleaving_setDeviceParam(double*);

	// Token: 0x06000076 RID: 118 RVA: 0x000048B9 File Offset: 0x00003CB9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int BOE_Interleaving_initSDK(void*, void*, double*, double*, double*, D3DVersion, int, int, int, int, int, sbyte*);

	// Token: 0x06000077 RID: 119 RVA: 0x000048CB File Offset: 0x00003CCB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __ExceptionPtrDestroy(void*);

	// Token: 0x06000078 RID: 120 RVA: 0x000048A1 File Offset: 0x00003CA1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void BOE_Interleaving_setWholeParam(double*);

	// Token: 0x06000079 RID: 121 RVA: 0x000023CC File Offset: 0x000017CC
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x0600007A RID: 122 RVA: 0x000048AD File Offset: 0x00003CAD
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void BOE_Interleaving_setRefractDeviceParam(double*);

	// Token: 0x0600007B RID: 123 RVA: 0x000047A9 File Offset: 0x00003BA9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __std_exception_copy(__std_exception_data*, __std_exception_data*);

	// Token: 0x0600007C RID: 124 RVA: 0x0000489B File Offset: 0x00003C9B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void BOE_Interleaving_releaseSDK();

	// Token: 0x0600007D RID: 125 RVA: 0x000048D1 File Offset: 0x00003CD1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __ExceptionPtrCopy(void*, void*);

	// Token: 0x0600007E RID: 126 RVA: 0x000048E8 File Offset: 0x00003CE8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete[](void*, ulong);

	// Token: 0x0600007F RID: 127 RVA: 0x000048D8 File Offset: 0x00003CD8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x06000080 RID: 128 RVA: 0x000047AF File Offset: 0x00003BAF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __std_exception_destroy(__std_exception_data*);

	// Token: 0x06000081 RID: 129 RVA: 0x0000492C File Offset: 0x00003D2C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x06000082 RID: 130 RVA: 0x000047CD File Offset: 0x00003BCD
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void** __current_exception_context();

	// Token: 0x06000083 RID: 131 RVA: 0x0000481B File Offset: 0x00003C1B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void terminate();

	// Token: 0x06000084 RID: 132 RVA: 0x000047C7 File Offset: 0x00003BC7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void** __current_exception();

	// Token: 0x06000085 RID: 133 RVA: 0x00003910 File Offset: 0x00002D10
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000086 RID: 134 RVA: 0x00004815 File Offset: 0x00003C15
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x06000087 RID: 135 RVA: 0x000048C5 File Offset: 0x00003CC5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x06000088 RID: 136 RVA: 0x00004932 File Offset: 0x00003D32
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void abort();

	// Token: 0x06000089 RID: 137 RVA: 0x00002444 File Offset: 0x00001844
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x04000001 RID: 1 RVA: 0x000062C0 File Offset: 0x00004AC0
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000002 RID: 2 RVA: 0x000062D8 File Offset: 0x00004AD8
	internal static $ArrayType$$$BY0BD@$$CBD ??_C@_0BD@FGIAGBIG@bad?5variant?5access@;

	// Token: 0x04000003 RID: 3 RVA: 0x000062F0 File Offset: 0x00004AF0
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@FJHHFFAF@bad_weak_ptr@;

	// Token: 0x04000004 RID: 4 RVA: 0x00018060 File Offset: 0x00016860
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_variant_access@std@@6B@;

	// Token: 0x04000005 RID: 5 RVA: 0x00016B28 File Offset: 0x00015328
	internal static _s__RTTICompleteObjectLocator2 ??_R4exception@std@@6B@;

	// Token: 0x04000006 RID: 6 RVA: 0x00016CF8 File Offset: 0x000154F8
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2bad_array_new_length@std@@8;

	// Token: 0x04000007 RID: 7 RVA: 0x000188A8 File Offset: 0x000170A8
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVbad_alloc@std@@@8;

	// Token: 0x04000008 RID: 8 RVA: 0x00016D58 File Offset: 0x00015558
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_weak_ptr@std@@8;

	// Token: 0x04000009 RID: 9 RVA: 0x00016BA8 File Offset: 0x000153A8
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_variant_access@std@@6B@;

	// Token: 0x0400000A RID: 10 RVA: 0x00016D98 File Offset: 0x00015598
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_weak_ptr@std@@8;

	// Token: 0x0400000B RID: 11 RVA: 0x00016BD0 File Offset: 0x000153D0
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_cast@std@@8;

	// Token: 0x0400000C RID: 12 RVA: 0x00016C78 File Offset: 0x00015478
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_alloc@std@@8;

	// Token: 0x0400000D RID: 13 RVA: 0x000180D8 File Offset: 0x000168D8
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_array_new_length@std@@6B@;

	// Token: 0x0400000E RID: 14 RVA: 0x00016AD8 File Offset: 0x000152D8
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x0400000F RID: 15 RVA: 0x00016C50 File Offset: 0x00015450
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_alloc@std@@8;

	// Token: 0x04000010 RID: 16 RVA: 0x00016B50 File Offset: 0x00015350
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_variant_access@std@@8;

	// Token: 0x04000011 RID: 17 RVA: 0x000180B0 File Offset: 0x000168B0
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_alloc@std@@6B@;

	// Token: 0x04000012 RID: 18 RVA: 0x00016BF8 File Offset: 0x000153F8
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_cast@std@@8;

	// Token: 0x04000013 RID: 19 RVA: 0x00016B00 File Offset: 0x00015300
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2exception@std@@8;

	// Token: 0x04000014 RID: 20 RVA: 0x00018088 File Offset: 0x00016888
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_cast@std@@6B@;

	// Token: 0x04000015 RID: 21 RVA: 0x00018880 File Offset: 0x00017080
	internal static $_TypeDescriptor$_extraBytes_19 ??_R0?AVbad_cast@std@@@8;

	// Token: 0x04000016 RID: 22 RVA: 0x00016D80 File Offset: 0x00015580
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_weak_ptr@std@@8;

	// Token: 0x04000017 RID: 23 RVA: 0x00018008 File Offset: 0x00016808
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x04000018 RID: 24 RVA: 0x00016B10 File Offset: 0x00015310
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x04000019 RID: 25 RVA: 0x00016B78 File Offset: 0x00015378
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_variant_access@std@@8;

	// Token: 0x0400001A RID: 26 RVA: 0x00016DB0 File Offset: 0x000155B0
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_weak_ptr@std@@6B@;

	// Token: 0x0400001B RID: 27 RVA: 0x00016C10 File Offset: 0x00015410
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_cast@std@@8;

	// Token: 0x0400001C RID: 28 RVA: 0x00016CA8 File Offset: 0x000154A8
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_alloc@std@@6B@;

	// Token: 0x0400001D RID: 29 RVA: 0x00016C28 File Offset: 0x00015428
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_cast@std@@6B@;

	// Token: 0x0400001E RID: 30 RVA: 0x00016D18 File Offset: 0x00015518
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_array_new_length@std@@8;

	// Token: 0x0400001F RID: 31 RVA: 0x00016C90 File Offset: 0x00015490
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_alloc@std@@8;

	// Token: 0x04000020 RID: 32 RVA: 0x00016B90 File Offset: 0x00015390
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_variant_access@std@@8;

	// Token: 0x04000021 RID: 33 RVA: 0x00016D30 File Offset: 0x00015530
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_array_new_length@std@@6B@;

	// Token: 0x04000022 RID: 34 RVA: 0x00016CD0 File Offset: 0x000154D0
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_array_new_length@std@@8;

	// Token: 0x04000023 RID: 35 RVA: 0x00018850 File Offset: 0x00017050
	internal static $_TypeDescriptor$_extraBytes_29 ??_R0?AVbad_variant_access@std@@@8;

	// Token: 0x04000024 RID: 36 RVA: 0x00018828 File Offset: 0x00017028
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x04000025 RID: 37 RVA: 0x00018100 File Offset: 0x00016900
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_weak_ptr@std@@6B@;

	// Token: 0x04000026 RID: 38 RVA: 0x00018900 File Offset: 0x00017100
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVbad_weak_ptr@std@@@8;

	// Token: 0x04000027 RID: 39 RVA: 0x000188D0 File Offset: 0x000170D0
	internal static $_TypeDescriptor$_extraBytes_31 ??_R0?AVbad_array_new_length@std@@@8;

	// Token: 0x04000028 RID: 40 RVA: 0x00018130 File Offset: 0x00016930
	public static method __m2mep@??2@$$FYAPEAX_KPEAXU__clr_placement_new_t@?A0x5d73e011@@@Z;

	// Token: 0x04000029 RID: 41 RVA: 0x00018018 File Offset: 0x00016818
	public static method __m2mep@??0exception@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400002A RID: 42 RVA: 0x00018160 File Offset: 0x00016960
	public static method __m2mep@??1exception@std@@$$FUEAA@XZ;

	// Token: 0x0400002B RID: 43 RVA: 0x00018150 File Offset: 0x00016950
	public static method __m2mep@?what@exception@std@@$$FUEBAPEBDXZ;

	// Token: 0x0400002C RID: 44 RVA: 0x00018140 File Offset: 0x00016940
	public static method __m2mep@??_Eexception@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400002D RID: 45 RVA: 0x000181C0 File Offset: 0x000169C0
	public static method __m2mep@??_Ebad_alloc@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400002E RID: 46 RVA: 0x00018170 File Offset: 0x00016970
	public static method __m2mep@??1bad_alloc@std@@$$FUEAA@XZ;

	// Token: 0x0400002F RID: 47 RVA: 0x000181D0 File Offset: 0x000169D0
	public static method __m2mep@??_Ebad_array_new_length@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000030 RID: 48 RVA: 0x000182A0 File Offset: 0x00016AA0
	public static method __m2mep@??1bad_array_new_length@std@@$$FUEAA@XZ;

	// Token: 0x04000031 RID: 49 RVA: 0x00018180 File Offset: 0x00016980
	public static method __m2mep@??1exception_ptr@std@@$$FQEAA@XZ;

	// Token: 0x04000032 RID: 50 RVA: 0x00018028 File Offset: 0x00016828
	public static method __m2mep@??0exception_ptr@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000033 RID: 51 RVA: 0x00018038 File Offset: 0x00016838
	public static method __m2mep@?<MarshalCopy>@exception_ptr@std@@$$FSMXPEAV12@0@Z;

	// Token: 0x04000034 RID: 52 RVA: 0x00018048 File Offset: 0x00016848
	public static method __m2mep@?<MarshalDestroy>@exception_ptr@std@@$$FSMXPEAV12@@Z;

	// Token: 0x04000035 RID: 53 RVA: 0x000181A0 File Offset: 0x000169A0
	public static method __m2mep@?what@bad_variant_access@std@@$$FUEBAPEBDXZ;

	// Token: 0x04000036 RID: 54 RVA: 0x00018280 File Offset: 0x00016A80
	public static method __m2mep@??1bad_variant_access@std@@$$FUEAA@XZ;

	// Token: 0x04000037 RID: 55 RVA: 0x00018070 File Offset: 0x00016870
	public static method __m2mep@??0bad_variant_access@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000038 RID: 56 RVA: 0x00018190 File Offset: 0x00016990
	public static method __m2mep@??_Ebad_variant_access@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000039 RID: 57 RVA: 0x000181B0 File Offset: 0x000169B0
	public static method __m2mep@??_Ebad_cast@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400003A RID: 58 RVA: 0x00018290 File Offset: 0x00016A90
	public static method __m2mep@??1bad_cast@std@@$$FUEAA@XZ;

	// Token: 0x0400003B RID: 59 RVA: 0x00018098 File Offset: 0x00016898
	public static method __m2mep@??0bad_cast@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400003C RID: 60 RVA: 0x000180E8 File Offset: 0x000168E8
	public static method __m2mep@??0bad_array_new_length@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400003D RID: 61 RVA: 0x000180C0 File Offset: 0x000168C0
	public static method __m2mep@??0bad_alloc@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400003E RID: 62 RVA: 0x00018120 File Offset: 0x00016920
	public static method __m2mep@??0_Iterator_base12@std@@$$FQEAA@AEBU01@@Z;

	// Token: 0x0400003F RID: 63 RVA: 0x000181E0 File Offset: 0x000169E0
	public static method __m2mep@??4_Iterator_base12@std@@$$FQEAAAEAU01@AEBU01@@Z;

	// Token: 0x04000040 RID: 64 RVA: 0x00018200 File Offset: 0x00016A00
	public static method __m2mep@?what@bad_weak_ptr@std@@$$FUEBAPEBDXZ;

	// Token: 0x04000041 RID: 65 RVA: 0x000181F0 File Offset: 0x000169F0
	public static method __m2mep@??_Ebad_weak_ptr@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000042 RID: 66 RVA: 0x000182B0 File Offset: 0x00016AB0
	public static method __m2mep@??1bad_weak_ptr@std@@$$FUEAA@XZ;

	// Token: 0x04000043 RID: 67 RVA: 0x00018110 File Offset: 0x00016910
	public static method __m2mep@??0bad_weak_ptr@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000044 RID: 68 RVA: 0x00018210 File Offset: 0x00016A10
	public static method __m2mep@?PtrToStringChars@@$$FYMPE$CB_WPE$ABVString@System@@@Z;

	// Token: 0x04000045 RID: 69 RVA: 0x00018220 File Offset: 0x00016A20
	public static method __m2mep@?GetAnsiStringSize@details@interop@msclr@@$$FYM_KPE$AAVString@System@@@Z;

	// Token: 0x04000046 RID: 70 RVA: 0x00018230 File Offset: 0x00016A30
	public static method __m2mep@?WriteAnsiString@details@interop@msclr@@$$FYMXPEAD_KPE$AAVString@System@@@Z;

	// Token: 0x04000047 RID: 71 RVA: 0x00018240 File Offset: 0x00016A40
	public static method __m2mep@?release@?$char_buffer@D@details@interop@msclr@@$$FQEAAPEADXZ;

	// Token: 0x04000048 RID: 72 RVA: 0x00018250 File Offset: 0x00016A50
	public static method __m2mep@?get@?$char_buffer@D@details@interop@msclr@@$$FQEBAPEADXZ;

	// Token: 0x04000049 RID: 73 RVA: 0x00018260 File Offset: 0x00016A60
	public static method __m2mep@??1?$char_buffer@D@details@interop@msclr@@$$FQEAA@XZ;

	// Token: 0x0400004A RID: 74 RVA: 0x00018270 File Offset: 0x00016A70
	public static method __m2mep@??0?$char_buffer@D@details@interop@msclr@@$$FQEAA@_K@Z;

	// Token: 0x0400004B RID: 75 RVA: 0x00018370 File Offset: 0x00016B70
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400004C RID: 76 RVA: 0x00018380 File Offset: 0x00016B80
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400004D RID: 77 RVA: 0x00018390 File Offset: 0x00016B90
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x0400004E RID: 78 RVA: 0x000183B0 File Offset: 0x00016BB0
	public static method __m2mep@??_M@$$FYMXPEAX_K1P6MX0@Z@Z;

	// Token: 0x0400004F RID: 79 RVA: 0x000183D0 File Offset: 0x00016BD0
	public static method __m2mep@?ArrayUnwindFilter@?A0x9c83a709@@$$FYAHPEAU_EXCEPTION_POINTERS@@@Z;

	// Token: 0x04000050 RID: 80 RVA: 0x000183A0 File Offset: 0x00016BA0
	public static method __m2mep@?__ArrayUnwind@@$$FYMXPEAX_K1P6MX0@Z@Z;

	// Token: 0x04000051 RID: 81 RVA: 0x000183C0 File Offset: 0x00016BC0
	public static method __m2mep@??_M@$$FYMXPEAX_KHP6MX0@Z@Z;

	// Token: 0x04000052 RID: 82 RVA: 0x000063B0 File Offset: 0x00004BB0
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000053 RID: 83 RVA: 0x000063A0 File Offset: 0x00004BA0
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000054 RID: 84
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000055 RID: 85 RVA: 0x00006260 File Offset: 0x00004A60
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000056 RID: 86
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000057 RID: 87 RVA: 0x00006278 File Offset: 0x00004A78
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000058 RID: 88 RVA: 0x000063C0 File Offset: 0x00004BC0
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000059 RID: 89 RVA: 0x000063D0 File Offset: 0x00004BD0
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400005A RID: 90
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400005B RID: 91 RVA: 0x000189E4 File Offset: 0x000171E4
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400005C RID: 92 RVA: 0x000183E4 File Offset: 0x00016BE4
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400005D RID: 93 RVA: 0x000189E7 File Offset: 0x000171E7
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400005E RID: 94 RVA: 0x000189E0 File Offset: 0x000171E0
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x0400005F RID: 95
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000060 RID: 96 RVA: 0x000189E6 File Offset: 0x000171E6
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000061 RID: 97
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000062 RID: 98
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000063 RID: 99 RVA: 0x000189E5 File Offset: 0x000171E5
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000064 RID: 100
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000065 RID: 101 RVA: 0x000183E0 File Offset: 0x00016BE0
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000066 RID: 102 RVA: 0x000062A0 File Offset: 0x00004AA0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x04000067 RID: 103 RVA: 0x000062B0 File Offset: 0x00004AB0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x04000068 RID: 104 RVA: 0x00006280 File Offset: 0x00004A80
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000069 RID: 105 RVA: 0x00006250 File Offset: 0x00004A50
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x0400006A RID: 106 RVA: 0x00006290 File Offset: 0x00004A90
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400006B RID: 107 RVA: 0x00006288 File Offset: 0x00004A88
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400006C RID: 108 RVA: 0x000062A8 File Offset: 0x00004AA8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x0400006D RID: 109 RVA: 0x00006258 File Offset: 0x00004A58
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400006E RID: 110 RVA: 0x00006298 File Offset: 0x00004A98
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x0400006F RID: 111 RVA: 0x00006270 File Offset: 0x00004A70
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000070 RID: 112 RVA: 0x00006268 File Offset: 0x00004A68
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000071 RID: 113 RVA: 0x00018568 File Offset: 0x00016D68
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x04000072 RID: 114 RVA: 0x000183F8 File Offset: 0x00016BF8
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x04000073 RID: 115 RVA: 0x00018408 File Offset: 0x00016C08
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x04000074 RID: 116 RVA: 0x00018418 File Offset: 0x00016C18
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x04000075 RID: 117 RVA: 0x00018428 File Offset: 0x00016C28
	public static method __m2mep@?FromGUID@<CrtImplementationDetails>@@$$FYM?AVGuid@System@@AEBU_GUID@@@Z;

	// Token: 0x04000076 RID: 118 RVA: 0x00018438 File Offset: 0x00016C38
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x04000077 RID: 119 RVA: 0x00018448 File Offset: 0x00016C48
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x04000078 RID: 120 RVA: 0x00018458 File Offset: 0x00016C58
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x04000079 RID: 121 RVA: 0x00018468 File Offset: 0x00016C68
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x0400007A RID: 122 RVA: 0x00018478 File Offset: 0x00016C78
	public static method __m2mep@?__scrt_is_safe_for_managed_code@@$$FYA_NXZ;

	// Token: 0x0400007B RID: 123 RVA: 0x00018488 File Offset: 0x00016C88
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400007C RID: 124 RVA: 0x00018498 File Offset: 0x00016C98
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400007D RID: 125 RVA: 0x000184A8 File Offset: 0x00016CA8
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400007E RID: 126 RVA: 0x000184B8 File Offset: 0x00016CB8
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400007F RID: 127 RVA: 0x000184C8 File Offset: 0x00016CC8
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000080 RID: 128 RVA: 0x000184D8 File Offset: 0x00016CD8
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000081 RID: 129 RVA: 0x00018578 File Offset: 0x00016D78
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000082 RID: 130 RVA: 0x00018588 File Offset: 0x00016D88
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000083 RID: 131 RVA: 0x00018598 File Offset: 0x00016D98
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000084 RID: 132 RVA: 0x000185A8 File Offset: 0x00016DA8
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000085 RID: 133 RVA: 0x000185B8 File Offset: 0x00016DB8
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000086 RID: 134 RVA: 0x000185C8 File Offset: 0x00016DC8
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000087 RID: 135 RVA: 0x000185D8 File Offset: 0x00016DD8
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000088 RID: 136 RVA: 0x000184E8 File Offset: 0x00016CE8
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000089 RID: 137 RVA: 0x000184F8 File Offset: 0x00016CF8
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400008A RID: 138 RVA: 0x00018508 File Offset: 0x00016D08
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400008B RID: 139 RVA: 0x00018518 File Offset: 0x00016D18
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x0400008C RID: 140 RVA: 0x000185E8 File Offset: 0x00016DE8
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x0400008D RID: 141 RVA: 0x000185F8 File Offset: 0x00016DF8
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x0400008E RID: 142 RVA: 0x00018608 File Offset: 0x00016E08
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x0400008F RID: 143 RVA: 0x00018618 File Offset: 0x00016E18
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x04000090 RID: 144 RVA: 0x000183E8 File Offset: 0x00016BE8
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000091 RID: 145 RVA: 0x00018528 File Offset: 0x00016D28
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x04000092 RID: 146 RVA: 0x00018538 File Offset: 0x00016D38
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x04000093 RID: 147 RVA: 0x00018548 File Offset: 0x00016D48
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000094 RID: 148 RVA: 0x00018558 File Offset: 0x00016D58
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000095 RID: 149 RVA: 0x000063E0 File Offset: 0x00004BE0
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000096 RID: 150 RVA: 0x000063E8 File Offset: 0x00004BE8
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000097 RID: 151 RVA: 0x00018B70 File Offset: 0x00017370
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x04000098 RID: 152 RVA: 0x00018B68 File Offset: 0x00017368
	internal static ulong __exit_list_size;

	// Token: 0x04000099 RID: 153
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x0400009A RID: 154
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x0400009B RID: 155
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x0400009C RID: 156 RVA: 0x00018B78 File Offset: 0x00017378
	internal unsafe static method* __onexitend_m;

	// Token: 0x0400009D RID: 157
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400009E RID: 158
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x0400009F RID: 159 RVA: 0x000186F0 File Offset: 0x00016EF0
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AA__ZVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x040000A0 RID: 160 RVA: 0x000187A0 File Offset: 0x00016FA0
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x040000A1 RID: 161 RVA: 0x00018700 File Offset: 0x00016F00
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x040000A2 RID: 162 RVA: 0x00018710 File Offset: 0x00016F10
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x040000A3 RID: 163 RVA: 0x00018720 File Offset: 0x00016F20
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x040000A4 RID: 164 RVA: 0x00018730 File Offset: 0x00016F30
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000A5 RID: 165 RVA: 0x000187B0 File Offset: 0x00016FB0
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000A6 RID: 166 RVA: 0x00018740 File Offset: 0x00016F40
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000A7 RID: 167 RVA: 0x00018750 File Offset: 0x00016F50
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000A8 RID: 168 RVA: 0x00018760 File Offset: 0x00016F60
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000A9 RID: 169 RVA: 0x00018770 File Offset: 0x00016F70
	public static method __m2mep@?__global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040000AA RID: 170 RVA: 0x00018780 File Offset: 0x00016F80
	public static method __m2mep@?__global_unlock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040000AB RID: 171 RVA: 0x000187C0 File Offset: 0x00016FC0
	public static method __m2mep@?__alloc_global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040000AC RID: 172 RVA: 0x00018790 File Offset: 0x00016F90
	public static method __m2mep@?__dealloc_global_lock@?A0xdf32f105@@$$FYAXXZ;

	// Token: 0x040000AD RID: 173 RVA: 0x00018660 File Offset: 0x00016E60
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x040000AE RID: 174 RVA: 0x00018670 File Offset: 0x00016E70
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x040000AF RID: 175 RVA: 0x000186B0 File Offset: 0x00016EB0
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x040000B0 RID: 176 RVA: 0x000186C0 File Offset: 0x00016EC0
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040000B1 RID: 177 RVA: 0x00018680 File Offset: 0x00016E80
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040000B2 RID: 178 RVA: 0x000186D0 File Offset: 0x00016ED0
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x040000B3 RID: 179 RVA: 0x00018690 File Offset: 0x00016E90
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x040000B4 RID: 180 RVA: 0x000186E0 File Offset: 0x00016EE0
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040000B5 RID: 181 RVA: 0x000186A0 File Offset: 0x00016EA0
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040000B6 RID: 182 RVA: 0x000187D0 File Offset: 0x00016FD0
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x040000B7 RID: 183 RVA: 0x000187E0 File Offset: 0x00016FE0
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x040000B8 RID: 184 RVA: 0x00018800 File Offset: 0x00017000
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x040000B9 RID: 185 RVA: 0x000187F0 File Offset: 0x00016FF0
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x040000BA RID: 186 RVA: 0x00018810 File Offset: 0x00017010
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x040000BB RID: 187 RVA: 0x00006350 File Offset: 0x00004B50
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x040000BC RID: 188 RVA: 0x00006228 File Offset: 0x00004A28
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x040000BD RID: 189 RVA: 0x00018980 File Offset: 0x00017180
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x040000BE RID: 190 RVA: 0x00018988 File Offset: 0x00017188
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x040000BF RID: 191 RVA: 0x00006210 File Offset: 0x00004A10
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x040000C0 RID: 192 RVA: 0x00006220 File Offset: 0x00004A20
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x040000C1 RID: 193 RVA: 0x000182C4 File Offset: 0x00016AC4
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x040000C2 RID: 194 RVA: 0x00006218 File Offset: 0x00004A18
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
