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
using ?A0x790f775b;
using AiStereo;
using msclr.interop.details;
using std;
using StereoMatcher;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00001090 File Offset: 0x00000490
	internal unsafe static void* @new(ulong __unnamed000, void* _Where, __clr_placement_new_t __unnamed002)
	{
		return _Where;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00001104 File Offset: 0x00000504
	internal unsafe static exception* {ctor}(exception* A_0, exception* _Other)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		exception* ptr = A_0 / sizeof(exception) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(_Other / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		return A_0;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00001130 File Offset: 0x00000530
	internal unsafe static void {dtor}(exception* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00001C40 File Offset: 0x00001040
	internal unsafe static sbyte* what(exception* A_0)
	{
		ulong num = (ulong)(*(A_0 + 8L));
		return (num != 0UL) ? num : ref <Module>.??_C@_0BC@EOODALEL@Unknown?5exception@;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00001BE4 File Offset: 0x00000FE4
	internal unsafe static void* __vecDelDtor(exception* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			exception* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), <Module>.__unep@??1exception@std@@$$FUEAA@XZ);
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

	// Token: 0x06000006 RID: 6 RVA: 0x00001D6C File Offset: 0x0000116C
	internal unsafe static void* __vecDelDtor(bad_alloc* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_alloc* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), <Module>.__unep@??1bad_alloc@std@@$$FUEAA@XZ);
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

	// Token: 0x06000007 RID: 7 RVA: 0x00001150 File Offset: 0x00000550
	internal unsafe static void {dtor}(bad_alloc* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00001DC8 File Offset: 0x000011C8
	internal unsafe static void* __vecDelDtor(bad_array_new_length* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_array_new_length* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), <Module>.__unep@??1bad_array_new_length@std@@$$FUEAA@XZ);
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

	// Token: 0x06000009 RID: 9 RVA: 0x00001E24 File Offset: 0x00001224
	internal unsafe static void {dtor}(bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00001170 File Offset: 0x00000570
	internal unsafe static void {dtor}(exception_ptr* A_0)
	{
		<Module>.__ExceptionPtrDestroy(A_0);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00001184 File Offset: 0x00000584
	internal unsafe static exception_ptr* {ctor}(exception_ptr* A_0, exception_ptr* _Rhs)
	{
		*A_0 = 0L;
		*(A_0 + 8L) = 0L;
		<Module>.__ExceptionPtrCopy(A_0, _Rhs);
		return A_0;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00001CDC File Offset: 0x000010DC
	internal unsafe static sbyte* what(bad_variant_access* A_0)
	{
		return ref <Module>.??_C@_0BD@FGIAGBIG@bad?5variant?5access@;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00001CBC File Offset: 0x000010BC
	internal unsafe static void {dtor}(bad_variant_access* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000011DC File Offset: 0x000005DC
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

	// Token: 0x0600000F RID: 15 RVA: 0x00001C60 File Offset: 0x00001060
	internal unsafe static void* __vecDelDtor(bad_variant_access* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_variant_access* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), <Module>.__unep@??1bad_variant_access@std@@$$FUEAA@XZ);
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

	// Token: 0x06000010 RID: 16 RVA: 0x00001CF0 File Offset: 0x000010F0
	internal unsafe static void* __vecDelDtor(bad_cast* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_cast* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), <Module>.__unep@??1bad_cast@std@@$$FUEAA@XZ);
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

	// Token: 0x06000011 RID: 17 RVA: 0x00001D4C File Offset: 0x0000114C
	internal unsafe static void {dtor}(bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000123C File Offset: 0x0000063C
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

	// Token: 0x06000013 RID: 19 RVA: 0x0000129C File Offset: 0x0000069C
	internal unsafe static tuple<>* {ctor}(tuple<>* A_0, tuple<>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000130C File Offset: 0x0000070C
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

	// Token: 0x06000015 RID: 21 RVA: 0x000012AC File Offset: 0x000006AC
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

	// Token: 0x06000016 RID: 22 RVA: 0x00001848 File Offset: 0x00000C48
	internal unsafe static _Iterator_base12* {ctor}(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*(A_0 + 8L) = 0L;
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00001354 File Offset: 0x00000754
	internal unsafe static _Iterator_base12* =(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00001EC0 File Offset: 0x000012C0
	internal unsafe static sbyte* what(bad_weak_ptr* A_0)
	{
		return ref <Module>.??_C@_0N@FJHHFFAF@bad_weak_ptr@;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00001E44 File Offset: 0x00001244
	internal unsafe static void* __vecDelDtor(bad_weak_ptr* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_weak_ptr* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), <Module>.__unep@??1bad_weak_ptr@std@@$$FUEAA@XZ);
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

	// Token: 0x0600001A RID: 26 RVA: 0x00001EA0 File Offset: 0x000012A0
	internal unsafe static void {dtor}(bad_weak_ptr* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00001368 File Offset: 0x00000768
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

	// Token: 0x0600001C RID: 28 RVA: 0x000013C8 File Offset: 0x000007C8
	internal static ref char PtrToStringChars(string s)
	{
		ref byte ptr = s;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		return ref ptr;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000013E4 File Offset: 0x000007E4
	internal static ulong GetAnsiStringSize(string _str)
	{
		ref byte ptr = _str;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		ref char char_u0020modopt(IsConst)& = ref ptr;
		ulong num = (ulong)<Module>.WideCharToMultiByte(3U, 1024, ref char_u0020modopt(IsConst)&, _str.Length, null, 0, null, null);
		if (num == 0UL && _str.Length != 0)
		{
			throw new ArgumentException("Conversion from WideChar to MultiByte failed.  Please check the content of the string and/or locale settings.");
		}
		return num + 1UL;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00001438 File Offset: 0x00000838
	internal unsafe static void WriteAnsiString(sbyte* _buf, ulong _size, string _str)
	{
		ref byte ptr = _str;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		ref char char_u0020modopt(IsConst)& = ref ptr;
		if (_size > 2147483647UL)
		{
			throw new ArgumentOutOfRangeException("Size of string exceeds INT_MAX.");
		}
		ulong num = <Module>.WideCharToMultiByte(3U, 1024, ref char_u0020modopt(IsConst)&, _str.Length, _buf, (int)_size, null, null);
		if (num < _size && (num != null || _size == 1UL))
		{
			*(byte*)(num / sizeof(sbyte) + _buf) = 0;
			return;
		}
		throw new ArgumentException("Conversion from WideChar to MultiByte failed.  Please check the content of the string and/or locale settings.");
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000017C4 File Offset: 0x00000BC4
	internal unsafe static sbyte* release(char_buffer<char>* A_0)
	{
		sbyte* result = *A_0;
		*A_0 = 0L;
		return result;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000017D8 File Offset: 0x00000BD8
	internal unsafe static sbyte* get(char_buffer<char>* A_0)
	{
		return *A_0;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000017E8 File Offset: 0x00000BE8
	internal unsafe static void {dtor}(char_buffer<char>* A_0)
	{
		<Module>.delete[](*A_0);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000017FC File Offset: 0x00000BFC
	internal unsafe static char_buffer<char>* {ctor}(char_buffer<char>* A_0, ulong _size)
	{
		*A_0 = <Module>.new[](_size);
		return A_0;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00001828 File Offset: 0x00000C28
	internal unsafe static allocator<wchar_t>* {ctor}(allocator<wchar_t>* A_0, allocator<wchar_t>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00001838 File Offset: 0x00000C38
	internal unsafe static allocator<char>* {ctor}(allocator<char>* A_0, allocator<char>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000034B4 File Offset: 0x000028B4
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
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

	// Token: 0x06000026 RID: 38 RVA: 0x000034F8 File Offset: 0x000028F8
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x06000027 RID: 39 RVA: 0x0000353C File Offset: 0x0000293C
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
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

	// Token: 0x06000028 RID: 40 RVA: 0x00003DC0 File Offset: 0x000031C0
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003774 File Offset: 0x00002B74
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003788 File Offset: 0x00002B88
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000038A4 File Offset: 0x00002CA4
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000038BC File Offset: 0x00002CBC
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000390C File Offset: 0x00002D0C
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), riid).ToPointer();
			goto IL_36;
		}
		catch (System.Exception e)
		{
			num = Marshal.GetHRForException(e);
		}
		if (num < 0)
		{
			return num;
		}
		IL_36:
		long num2 = *(*(long*)ptr + 104L);
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, num2);
		ICorRuntimeHost* ptr2 = ptr;
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		return num;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000398C File Offset: 0x00002D8C
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000039A8 File Offset: 0x00002DA8
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr pUnk = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003A08 File Offset: 0x00002E08
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), riid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, id, function, cookie, num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003AC0 File Offset: 0x00002EC0
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

	// Token: 0x06000032 RID: 50 RVA: 0x00003AF0 File Offset: 0x00002EF0
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003B10 File Offset: 0x00002F10
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xc_mp_a);
			if (ref <Module>.__xc_mp_a < ref <Module>.__xc_mp_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_mp_z))
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

	// Token: 0x06000034 RID: 52 RVA: 0x00003B64 File Offset: 0x00002F64
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if (ref <Module>.__xi_a < ref <Module>.__xi_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_35:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if (ref <Module>.__xc_a < ref <Module>.__xc_z)
			{
				while (*(long*)ptr2 == 0L)
				{
					ptr2 += 8L / (long)sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
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

	// Token: 0x06000035 RID: 53 RVA: 0x00003BE4 File Offset: 0x00002FE4
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

	// Token: 0x06000036 RID: 54 RVA: 0x00003C1C File Offset: 0x0000301C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003C30 File Offset: 0x00003030
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00001000 File Offset: 0x00000400
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00001014 File Offset: 0x00000414
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00001028 File Offset: 0x00000428
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000103C File Offset: 0x0000043C
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00001050 File Offset: 0x00000450
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00001064 File Offset: 0x00000464
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00001078 File Offset: 0x00000478
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003E14 File Offset: 0x00003214
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003E48 File Offset: 0x00003248
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003E68 File Offset: 0x00003268
	[SecuritySafeCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000042 RID: 66 RVA: 0x00003EF8 File Offset: 0x000032F8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.__xc_mp_a), (method*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003F38 File Offset: 0x00003338
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

	// Token: 0x06000044 RID: 68 RVA: 0x00003F74 File Offset: 0x00003374
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003FA0 File Offset: 0x000033A0
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
		{
			<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		}
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
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
				num3 = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : num3);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003C4C File Offset: 0x0000304C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003C60 File Offset: 0x00003060
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

	// Token: 0x06000048 RID: 72 RVA: 0x00003C98 File Offset: 0x00003098
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

	// Token: 0x06000049 RID: 73 RVA: 0x00003CD0 File Offset: 0x000030D0
	[PrePrepareMethod]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x0600004A RID: 74 RVA: 0x0000409C File Offset: 0x0000349C
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (System.Exception nestedException)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, nestedException);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004110 File Offset: 0x00003510
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00004128 File Offset: 0x00003528
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000413C File Offset: 0x0000353C
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
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
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (System.Exception innerException)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, innerException);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), innerException);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000041F8 File Offset: 0x000035F8
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

	// Token: 0x0600004F RID: 79 RVA: 0x00003D0C File Offset: 0x0000310C
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003D30 File Offset: 0x00003130
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003D58 File Offset: 0x00003158
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003D80 File Offset: 0x00003180
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004280 File Offset: 0x00003680
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(value);
		}
		return null;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00004768 File Offset: 0x00003B68
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000042B0 File Offset: 0x000036B0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)GCHandle.Alloc(value)).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000042FC File Offset: 0x000036FC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00004320 File Offset: 0x00003720
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004348 File Offset: 0x00003748
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00004784 File Offset: 0x00003B84
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004364 File Offset: 0x00003764
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000438C File Offset: 0x0000378C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000043A4 File Offset: 0x000037A4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000043BC File Offset: 0x000037BC
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_lock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			result = true;
		}
		return result;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000043DC File Offset: 0x000037DC
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_unlock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			result = true;
		}
		return result;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000047B4 File Offset: 0x00003BB4
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000043FC File Offset: 0x000037FC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004410 File Offset: 0x00003810
	[SecurityCritical]
	internal unsafe static int _atexit_helper(method func, ulong* __pexit_list_size, method** __ponexitend_e, method** __ponexitbegin_e)
	{
		method system.Void_u0020() = 0L;
		if (func == null)
		{
			return -1;
		}
		if (<Module>.?A0xdf32f105.__global_lock() == 1)
		{
			try
			{
				method* ptr = (method*)<Module>.DecodePointer(*(long*)__ponexitbegin_e);
				method* ptr2 = (method*)<Module>.DecodePointer(*(long*)__ponexitend_e);
				long num = (long)(ptr2 - ptr);
				if (*__pexit_list_size - 1UL < (ulong)num >> 3)
				{
					try
					{
						ulong num2 = *__pexit_list_size * 8UL;
						ulong num3 = (num2 < 4096UL) ? num2 : 4096UL;
						IntPtr cb = new IntPtr((int)(num2 + num3));
						IntPtr pv = new IntPtr((void*)ptr);
						IntPtr intPtr = Marshal.ReAllocHGlobal(pv, cb);
						ptr2 = (method*)((byte*)intPtr.ToPointer() + num);
						ptr = (method*)intPtr.ToPointer();
						ulong num4 = *__pexit_list_size;
						ulong num5 = (512UL < num4) ? 512UL : num4;
						*__pexit_list_size = num4 + num5;
					}
					catch (OutOfMemoryException)
					{
						IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
						IntPtr pv2 = new IntPtr((void*)ptr);
						IntPtr intPtr2 = Marshal.ReAllocHGlobal(pv2, cb2);
						ptr2 = (intPtr2.ToPointer() - ptr) / (IntPtr)sizeof(method) + ptr2;
						ptr = (method*)intPtr2.ToPointer();
						*__pexit_list_size += 4UL;
					}
				}
				*(long*)ptr2 = func;
				ptr2 += 8L / (long)sizeof(method);
				system.Void_u0020() = func;
				*(long*)__ponexitbegin_e = <Module>.EncodePointer((void*)ptr);
				*(long*)__ponexitend_e = <Module>.EncodePointer((void*)ptr2);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				<Module>.?A0xdf32f105.__global_unlock();
			}
			if (system.Void_u0020() != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004588 File Offset: 0x00003988
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0xdf32f105.__exit_list_size != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitbegin_m);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				method* ptr3 = ptr;
				method* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(method);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>.EncodePointer(null))
					{
						void* ptr5 = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), ptr5);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitbegin_m);
						method* ptr7 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitend_m);
						if (ptr3 != ptr6 || ptr4 != ptr7)
						{
							ptr3 = ptr6;
							ptr = ptr6;
							ptr4 = ptr7;
							ptr2 = ptr7;
						}
					}
				}
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
			}
			<Module>.?A0xdf32f105.__dealloc_global_lock();
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000047CC File Offset: 0x00003BCC
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000064 RID: 100 RVA: 0x00004814 File Offset: 0x00003C14
	internal static method _onexit_m(method _Function)
	{
		return (<Module>._atexit_m(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004638 File Offset: 0x00003A38
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xdf32f105.__exit_list_size, &<Module>.?A0xdf32f105.__onexitend_m, &<Module>.?A0xdf32f105.__onexitbegin_m);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004830 File Offset: 0x00003C30
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

	// Token: 0x06000067 RID: 103 RVA: 0x00004660 File Offset: 0x00003A60
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					method* ptr3 = ptr;
					method* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(method);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						void* ptr5 = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), ptr5);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr7 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr6 || ptr4 != ptr7)
						{
							ptr3 = ptr6;
							ptr = ptr6;
							ptr4 = ptr7;
							ptr2 = ptr7;
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?A0xdf32f105.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004874 File Offset: 0x00003C74
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004740 File Offset: 0x00003B40
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x0600006A RID: 106
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x0600006B RID: 107
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x0600006C RID: 108 RVA: 0x000048A4 File Offset: 0x00003CA4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initterm_e(method* pfbegin, method* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				ulong num2 = (ulong)(*(long*)pfbegin);
				if (num2 != 0UL)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num2);
				}
				pfbegin += 8L / (long)sizeof(method);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000048D4 File Offset: 0x00003CD4
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

	// Token: 0x0600006E RID: 110 RVA: 0x000048FC File Offset: 0x00003CFC
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000494C File Offset: 0x00003D4C
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000070 RID: 112 RVA: 0x00004920 File Offset: 0x00003D20
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004BFC File Offset: 0x00003FFC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int WideCharToMultiByte(uint, uint, char*, int, sbyte*, int, sbyte*, int*);

	// Token: 0x06000072 RID: 114 RVA: 0x00004AA0 File Offset: 0x00003EA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* new[](ulong);

	// Token: 0x06000073 RID: 115 RVA: 0x00004A71 File Offset: 0x00003E71
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void UnloadStereoEngine();

	// Token: 0x06000074 RID: 116 RVA: 0x00004A77 File Offset: 0x00003E77
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool IsValidLicense(sbyte*, ulong);

	// Token: 0x06000075 RID: 117 RVA: 0x00004A6B File Offset: 0x00003E6B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern IStereoRenderer* CreateStereoRenderer();

	// Token: 0x06000076 RID: 118 RVA: 0x00004A7D File Offset: 0x00003E7D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void LoadStereoEngine(sbyte*, sbyte*);

	// Token: 0x06000077 RID: 119 RVA: 0x00004A89 File Offset: 0x00003E89
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __ExceptionPtrDestroy(void*);

	// Token: 0x06000078 RID: 120 RVA: 0x000026C8 File Offset: 0x00001AC8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x06000079 RID: 121 RVA: 0x000049D3 File Offset: 0x00003DD3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __std_exception_copy(__std_exception_data*, __std_exception_data*);

	// Token: 0x0600007A RID: 122 RVA: 0x00004AA8 File Offset: 0x00003EA8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void __ehvec_dtor(void*, ulong, ulong, method);

	// Token: 0x0600007B RID: 123 RVA: 0x00004A8F File Offset: 0x00003E8F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __ExceptionPtrCopy(void*, void*);

	// Token: 0x0600007C RID: 124 RVA: 0x00004BB8 File Offset: 0x00003FB8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete[](void*, ulong);

	// Token: 0x0600007D RID: 125 RVA: 0x00004A98 File Offset: 0x00003E98
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x0600007E RID: 126 RVA: 0x000049D9 File Offset: 0x00003DD9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __std_exception_destroy(__std_exception_data*);

	// Token: 0x0600007F RID: 127 RVA: 0x00004A83 File Offset: 0x00003E83
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern IStereoMatcher* CreateStereoMatcher();

	// Token: 0x06000080 RID: 128 RVA: 0x00004C08 File Offset: 0x00004008
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x06000081 RID: 129 RVA: 0x00003AE0 File Offset: 0x00002EE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000082 RID: 130 RVA: 0x00004A3F File Offset: 0x00003E3F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x06000083 RID: 131 RVA: 0x00004C02 File Offset: 0x00004002
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x06000084 RID: 132 RVA: 0x00004C0E File Offset: 0x0000400E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void abort();

	// Token: 0x06000085 RID: 133 RVA: 0x00002740 File Offset: 0x00001B40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x04000001 RID: 1 RVA: 0x000062D0 File Offset: 0x00004CD0
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000002 RID: 2 RVA: 0x000062F0 File Offset: 0x00004CF0
	internal static $ArrayType$$$BY0BD@$$CBD ??_C@_0BD@FGIAGBIG@bad?5variant?5access@;

	// Token: 0x04000003 RID: 3 RVA: 0x00006328 File Offset: 0x00004D28
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@FJHHFFAF@bad_weak_ptr@;

	// Token: 0x04000004 RID: 4 RVA: 0x000062C0 File Offset: 0x00004CC0
	internal static $ArrayType$$$BY04$$CBD ??_C@_04HAPDLBFF@auto@;

	// Token: 0x04000005 RID: 5 RVA: 0x0001D060 File Offset: 0x0001B060
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_variant_access@std@@6B@;

	// Token: 0x04000006 RID: 6 RVA: 0x0001B198 File Offset: 0x00019B98
	internal static _s__RTTICompleteObjectLocator2 ??_R4exception@std@@6B@;

	// Token: 0x04000007 RID: 7 RVA: 0x0001B368 File Offset: 0x00019D68
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2bad_array_new_length@std@@8;

	// Token: 0x04000008 RID: 8 RVA: 0x0001D868 File Offset: 0x0001B868
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVbad_alloc@std@@@8;

	// Token: 0x04000009 RID: 9 RVA: 0x0001B3C8 File Offset: 0x00019DC8
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_weak_ptr@std@@8;

	// Token: 0x0400000A RID: 10 RVA: 0x0001B218 File Offset: 0x00019C18
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_variant_access@std@@6B@;

	// Token: 0x0400000B RID: 11 RVA: 0x0001B408 File Offset: 0x00019E08
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_weak_ptr@std@@8;

	// Token: 0x0400000C RID: 12 RVA: 0x0001B240 File Offset: 0x00019C40
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_cast@std@@8;

	// Token: 0x0400000D RID: 13 RVA: 0x0001B2E8 File Offset: 0x00019CE8
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_alloc@std@@8;

	// Token: 0x0400000E RID: 14 RVA: 0x0001D0E8 File Offset: 0x0001B0E8
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_array_new_length@std@@6B@;

	// Token: 0x0400000F RID: 15 RVA: 0x0001B148 File Offset: 0x00019B48
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x04000010 RID: 16 RVA: 0x0001B2C0 File Offset: 0x00019CC0
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_alloc@std@@8;

	// Token: 0x04000011 RID: 17 RVA: 0x0001B1C0 File Offset: 0x00019BC0
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_variant_access@std@@8;

	// Token: 0x04000012 RID: 18 RVA: 0x0001D0C0 File Offset: 0x0001B0C0
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_alloc@std@@6B@;

	// Token: 0x04000013 RID: 19 RVA: 0x0001B268 File Offset: 0x00019C68
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_cast@std@@8;

	// Token: 0x04000014 RID: 20 RVA: 0x0001B170 File Offset: 0x00019B70
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2exception@std@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x0001D088 File Offset: 0x0001B088
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_cast@std@@6B@;

	// Token: 0x04000016 RID: 22 RVA: 0x0001D840 File Offset: 0x0001B840
	internal static $_TypeDescriptor$_extraBytes_19 ??_R0?AVbad_cast@std@@@8;

	// Token: 0x04000017 RID: 23 RVA: 0x0001B3F0 File Offset: 0x00019DF0
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_weak_ptr@std@@8;

	// Token: 0x04000018 RID: 24 RVA: 0x0001D008 File Offset: 0x0001B008
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x04000019 RID: 25 RVA: 0x0001B180 File Offset: 0x00019B80
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x0400001A RID: 26 RVA: 0x0001B1E8 File Offset: 0x00019BE8
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_variant_access@std@@8;

	// Token: 0x0400001B RID: 27 RVA: 0x0001B420 File Offset: 0x00019E20
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_weak_ptr@std@@6B@;

	// Token: 0x0400001C RID: 28 RVA: 0x0001B280 File Offset: 0x00019C80
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_cast@std@@8;

	// Token: 0x0400001D RID: 29 RVA: 0x0001B318 File Offset: 0x00019D18
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_alloc@std@@6B@;

	// Token: 0x0400001E RID: 30 RVA: 0x0001B298 File Offset: 0x00019C98
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_cast@std@@6B@;

	// Token: 0x0400001F RID: 31 RVA: 0x0001B388 File Offset: 0x00019D88
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_array_new_length@std@@8;

	// Token: 0x04000020 RID: 32 RVA: 0x0001B300 File Offset: 0x00019D00
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_alloc@std@@8;

	// Token: 0x04000021 RID: 33 RVA: 0x0001B200 File Offset: 0x00019C00
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_variant_access@std@@8;

	// Token: 0x04000022 RID: 34 RVA: 0x0001B3A0 File Offset: 0x00019DA0
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_array_new_length@std@@6B@;

	// Token: 0x04000023 RID: 35 RVA: 0x0001B340 File Offset: 0x00019D40
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_array_new_length@std@@8;

	// Token: 0x04000024 RID: 36 RVA: 0x0001D810 File Offset: 0x0001B810
	internal static $_TypeDescriptor$_extraBytes_29 ??_R0?AVbad_variant_access@std@@@8;

	// Token: 0x04000025 RID: 37 RVA: 0x0001D7E8 File Offset: 0x0001B7E8
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x04000026 RID: 38 RVA: 0x0001D110 File Offset: 0x0001B110
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_weak_ptr@std@@6B@;

	// Token: 0x04000027 RID: 39 RVA: 0x0001D8C0 File Offset: 0x0001B8C0
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVbad_weak_ptr@std@@@8;

	// Token: 0x04000028 RID: 40 RVA: 0x0001D890 File Offset: 0x0001B890
	internal static $_TypeDescriptor$_extraBytes_31 ??_R0?AVbad_array_new_length@std@@@8;

	// Token: 0x04000029 RID: 41 RVA: 0x0001D160 File Offset: 0x0001B160
	public static method __m2mep@??2@$$FYAPEAX_KPEAXU__clr_placement_new_t@?A0x790f775b@@@Z;

	// Token: 0x0400002A RID: 42 RVA: 0x0001D018 File Offset: 0x0001B018
	public static method __m2mep@??0exception@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400002B RID: 43 RVA: 0x0001D190 File Offset: 0x0001B190
	public static method __m2mep@??1exception@std@@$$FUEAA@XZ;

	// Token: 0x0400002C RID: 44 RVA: 0x0001D180 File Offset: 0x0001B180
	public static method __m2mep@?what@exception@std@@$$FUEBAPEBDXZ;

	// Token: 0x0400002D RID: 45 RVA: 0x0001D170 File Offset: 0x0001B170
	public static method __m2mep@??_Eexception@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400002E RID: 46 RVA: 0x0001D1F0 File Offset: 0x0001B1F0
	public static method __m2mep@??_Ebad_alloc@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400002F RID: 47 RVA: 0x0001D1A0 File Offset: 0x0001B1A0
	public static method __m2mep@??1bad_alloc@std@@$$FUEAA@XZ;

	// Token: 0x04000030 RID: 48 RVA: 0x0001D200 File Offset: 0x0001B200
	public static method __m2mep@??_Ebad_array_new_length@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000031 RID: 49 RVA: 0x0001D2D0 File Offset: 0x0001B2D0
	public static method __m2mep@??1bad_array_new_length@std@@$$FUEAA@XZ;

	// Token: 0x04000032 RID: 50 RVA: 0x0001D1B0 File Offset: 0x0001B1B0
	public static method __m2mep@??1exception_ptr@std@@$$FQEAA@XZ;

	// Token: 0x04000033 RID: 51 RVA: 0x0001D028 File Offset: 0x0001B028
	public static method __m2mep@??0exception_ptr@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000034 RID: 52 RVA: 0x0001D038 File Offset: 0x0001B038
	public static method __m2mep@?<MarshalCopy>@exception_ptr@std@@$$FSMXPEAV12@0@Z;

	// Token: 0x04000035 RID: 53 RVA: 0x0001D048 File Offset: 0x0001B048
	public static method __m2mep@?<MarshalDestroy>@exception_ptr@std@@$$FSMXPEAV12@@Z;

	// Token: 0x04000036 RID: 54 RVA: 0x0001D1D0 File Offset: 0x0001B1D0
	public static method __m2mep@?what@bad_variant_access@std@@$$FUEBAPEBDXZ;

	// Token: 0x04000037 RID: 55 RVA: 0x0001D2B0 File Offset: 0x0001B2B0
	public static method __m2mep@??1bad_variant_access@std@@$$FUEAA@XZ;

	// Token: 0x04000038 RID: 56 RVA: 0x0001D070 File Offset: 0x0001B070
	public static method __m2mep@??0bad_variant_access@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000039 RID: 57 RVA: 0x0001D1C0 File Offset: 0x0001B1C0
	public static method __m2mep@??_Ebad_variant_access@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400003A RID: 58 RVA: 0x0001D1E0 File Offset: 0x0001B1E0
	public static method __m2mep@??_Ebad_cast@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400003B RID: 59 RVA: 0x0001D2C0 File Offset: 0x0001B2C0
	public static method __m2mep@??1bad_cast@std@@$$FUEAA@XZ;

	// Token: 0x0400003C RID: 60 RVA: 0x0001D098 File Offset: 0x0001B098
	public static method __m2mep@??0bad_cast@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400003D RID: 61 RVA: 0x0001D0A8 File Offset: 0x0001B0A8
	public static method __m2mep@??0?$tuple@$$V@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400003E RID: 62 RVA: 0x0001D0F8 File Offset: 0x0001B0F8
	public static method __m2mep@??0bad_array_new_length@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400003F RID: 63 RVA: 0x0001D0D0 File Offset: 0x0001B0D0
	public static method __m2mep@??0bad_alloc@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000040 RID: 64 RVA: 0x0001D150 File Offset: 0x0001B150
	public static method __m2mep@??0_Iterator_base12@std@@$$FQEAA@AEBU01@@Z;

	// Token: 0x04000041 RID: 65 RVA: 0x0001D210 File Offset: 0x0001B210
	public static method __m2mep@??4_Iterator_base12@std@@$$FQEAAAEAU01@AEBU01@@Z;

	// Token: 0x04000042 RID: 66 RVA: 0x0001D230 File Offset: 0x0001B230
	public static method __m2mep@?what@bad_weak_ptr@std@@$$FUEBAPEBDXZ;

	// Token: 0x04000043 RID: 67 RVA: 0x0001D220 File Offset: 0x0001B220
	public static method __m2mep@??_Ebad_weak_ptr@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000044 RID: 68 RVA: 0x0001D2E0 File Offset: 0x0001B2E0
	public static method __m2mep@??1bad_weak_ptr@std@@$$FUEAA@XZ;

	// Token: 0x04000045 RID: 69 RVA: 0x0001D120 File Offset: 0x0001B120
	public static method __m2mep@??0bad_weak_ptr@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000046 RID: 70 RVA: 0x0001D240 File Offset: 0x0001B240
	public static method __m2mep@?PtrToStringChars@@$$FYMPE$CB_WPE$ABVString@System@@@Z;

	// Token: 0x04000047 RID: 71 RVA: 0x0001D250 File Offset: 0x0001B250
	public static method __m2mep@?GetAnsiStringSize@details@interop@msclr@@$$FYM_KPE$AAVString@System@@@Z;

	// Token: 0x04000048 RID: 72 RVA: 0x0001D260 File Offset: 0x0001B260
	public static method __m2mep@?WriteAnsiString@details@interop@msclr@@$$FYMXPEAD_KPE$AAVString@System@@@Z;

	// Token: 0x04000049 RID: 73 RVA: 0x0001D270 File Offset: 0x0001B270
	public static method __m2mep@?release@?$char_buffer@D@details@interop@msclr@@$$FQEAAPEADXZ;

	// Token: 0x0400004A RID: 74 RVA: 0x0001D280 File Offset: 0x0001B280
	public static method __m2mep@?get@?$char_buffer@D@details@interop@msclr@@$$FQEBAPEADXZ;

	// Token: 0x0400004B RID: 75 RVA: 0x0001D290 File Offset: 0x0001B290
	public static method __m2mep@??1?$char_buffer@D@details@interop@msclr@@$$FQEAA@XZ;

	// Token: 0x0400004C RID: 76 RVA: 0x0001D2A0 File Offset: 0x0001B2A0
	public static method __m2mep@??0?$char_buffer@D@details@interop@msclr@@$$FQEAA@_K@Z;

	// Token: 0x0400004D RID: 77 RVA: 0x0001D130 File Offset: 0x0001B130
	public static method __m2mep@??0?$allocator@_W@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400004E RID: 78 RVA: 0x0001D140 File Offset: 0x0001B140
	public static method __m2mep@??0?$allocator@D@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400004F RID: 79 RVA: 0x000062C8 File Offset: 0x00004CC8
	public unsafe static int** __unep@??1exception@std@@$$FUEAA@XZ;

	// Token: 0x04000050 RID: 80 RVA: 0x00006310 File Offset: 0x00004D10
	public unsafe static int** __unep@??1bad_alloc@std@@$$FUEAA@XZ;

	// Token: 0x04000051 RID: 81 RVA: 0x000062E8 File Offset: 0x00004CE8
	public unsafe static int** __unep@??1bad_variant_access@std@@$$FUEAA@XZ;

	// Token: 0x04000052 RID: 82 RVA: 0x00006308 File Offset: 0x00004D08
	public unsafe static int** __unep@??1bad_cast@std@@$$FUEAA@XZ;

	// Token: 0x04000053 RID: 83 RVA: 0x00006318 File Offset: 0x00004D18
	public unsafe static int** __unep@??1bad_array_new_length@std@@$$FUEAA@XZ;

	// Token: 0x04000054 RID: 84 RVA: 0x00006320 File Offset: 0x00004D20
	public unsafe static int** __unep@??1bad_weak_ptr@std@@$$FUEAA@XZ;

	// Token: 0x04000055 RID: 85 RVA: 0x0001D370 File Offset: 0x0001B370
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x04000056 RID: 86 RVA: 0x0001D380 File Offset: 0x0001B380
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x04000057 RID: 87 RVA: 0x0001D390 File Offset: 0x0001B390
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x04000058 RID: 88 RVA: 0x000063E0 File Offset: 0x00004DE0
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000059 RID: 89 RVA: 0x000063D0 File Offset: 0x00004DD0
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400005A RID: 90
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400005B RID: 91 RVA: 0x00006260 File Offset: 0x00004C60
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400005C RID: 92
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400005D RID: 93 RVA: 0x00006278 File Offset: 0x00004C78
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400005E RID: 94 RVA: 0x000063F0 File Offset: 0x00004DF0
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400005F RID: 95 RVA: 0x00006400 File Offset: 0x00004E00
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000060 RID: 96
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000061 RID: 97 RVA: 0x0001D9A4 File Offset: 0x0001B9A4
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000062 RID: 98 RVA: 0x0001D3A4 File Offset: 0x0001B3A4
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000063 RID: 99 RVA: 0x0001D9A7 File Offset: 0x0001B9A7
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000064 RID: 100 RVA: 0x0001D9A0 File Offset: 0x0001B9A0
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000065 RID: 101
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000066 RID: 102 RVA: 0x0001D9A6 File Offset: 0x0001B9A6
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000067 RID: 103
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000068 RID: 104
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000069 RID: 105 RVA: 0x0001D9A5 File Offset: 0x0001B9A5
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400006A RID: 106
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400006B RID: 107 RVA: 0x0001D3A0 File Offset: 0x0001B3A0
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400006C RID: 108 RVA: 0x000062A0 File Offset: 0x00004CA0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400006D RID: 109 RVA: 0x000062B0 File Offset: 0x00004CB0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400006E RID: 110 RVA: 0x00006280 File Offset: 0x00004C80
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400006F RID: 111 RVA: 0x00006250 File Offset: 0x00004C50
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000070 RID: 112 RVA: 0x00006290 File Offset: 0x00004C90
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x04000071 RID: 113 RVA: 0x00006288 File Offset: 0x00004C88
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000072 RID: 114 RVA: 0x000062A8 File Offset: 0x00004CA8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000073 RID: 115 RVA: 0x00006258 File Offset: 0x00004C58
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000074 RID: 116 RVA: 0x00006298 File Offset: 0x00004C98
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000075 RID: 117 RVA: 0x00006270 File Offset: 0x00004C70
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000076 RID: 118 RVA: 0x00006268 File Offset: 0x00004C68
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000077 RID: 119 RVA: 0x0001D528 File Offset: 0x0001B528
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x04000078 RID: 120 RVA: 0x0001D3B8 File Offset: 0x0001B3B8
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x04000079 RID: 121 RVA: 0x0001D3C8 File Offset: 0x0001B3C8
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x0400007A RID: 122 RVA: 0x0001D3D8 File Offset: 0x0001B3D8
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x0400007B RID: 123 RVA: 0x0001D3E8 File Offset: 0x0001B3E8
	public static method __m2mep@?FromGUID@<CrtImplementationDetails>@@$$FYM?AVGuid@System@@AEBU_GUID@@@Z;

	// Token: 0x0400007C RID: 124 RVA: 0x0001D3F8 File Offset: 0x0001B3F8
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x0400007D RID: 125 RVA: 0x0001D408 File Offset: 0x0001B408
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x0400007E RID: 126 RVA: 0x0001D418 File Offset: 0x0001B418
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x0400007F RID: 127 RVA: 0x0001D428 File Offset: 0x0001B428
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x04000080 RID: 128 RVA: 0x0001D438 File Offset: 0x0001B438
	public static method __m2mep@?__scrt_is_safe_for_managed_code@@$$FYA_NXZ;

	// Token: 0x04000081 RID: 129 RVA: 0x0001D448 File Offset: 0x0001B448
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000082 RID: 130 RVA: 0x0001D458 File Offset: 0x0001B458
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000083 RID: 131 RVA: 0x0001D468 File Offset: 0x0001B468
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000084 RID: 132 RVA: 0x0001D478 File Offset: 0x0001B478
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000085 RID: 133 RVA: 0x0001D488 File Offset: 0x0001B488
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000086 RID: 134 RVA: 0x0001D498 File Offset: 0x0001B498
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000087 RID: 135 RVA: 0x0001D538 File Offset: 0x0001B538
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000088 RID: 136 RVA: 0x0001D548 File Offset: 0x0001B548
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000089 RID: 137 RVA: 0x0001D558 File Offset: 0x0001B558
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400008A RID: 138 RVA: 0x0001D568 File Offset: 0x0001B568
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400008B RID: 139 RVA: 0x0001D578 File Offset: 0x0001B578
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400008C RID: 140 RVA: 0x0001D588 File Offset: 0x0001B588
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400008D RID: 141 RVA: 0x0001D598 File Offset: 0x0001B598
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400008E RID: 142 RVA: 0x0001D4A8 File Offset: 0x0001B4A8
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400008F RID: 143 RVA: 0x0001D4B8 File Offset: 0x0001B4B8
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000090 RID: 144 RVA: 0x0001D4C8 File Offset: 0x0001B4C8
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000091 RID: 145 RVA: 0x0001D4D8 File Offset: 0x0001B4D8
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x04000092 RID: 146 RVA: 0x0001D5A8 File Offset: 0x0001B5A8
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x04000093 RID: 147 RVA: 0x0001D5B8 File Offset: 0x0001B5B8
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000094 RID: 148 RVA: 0x0001D5C8 File Offset: 0x0001B5C8
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000095 RID: 149 RVA: 0x0001D5D8 File Offset: 0x0001B5D8
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x04000096 RID: 150 RVA: 0x0001D3A8 File Offset: 0x0001B3A8
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000097 RID: 151 RVA: 0x0001D4E8 File Offset: 0x0001B4E8
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x04000098 RID: 152 RVA: 0x0001D4F8 File Offset: 0x0001B4F8
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x04000099 RID: 153 RVA: 0x0001D508 File Offset: 0x0001B508
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x0400009A RID: 154 RVA: 0x0001D518 File Offset: 0x0001B518
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x0400009B RID: 155 RVA: 0x00006410 File Offset: 0x00004E10
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400009C RID: 156 RVA: 0x00006418 File Offset: 0x00004E18
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400009D RID: 157 RVA: 0x0001DB30 File Offset: 0x0001BB30
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x0400009E RID: 158 RVA: 0x0001DB28 File Offset: 0x0001BB28
	internal static ulong __exit_list_size;

	// Token: 0x0400009F RID: 159
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x040000A0 RID: 160
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x040000A1 RID: 161
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x040000A2 RID: 162 RVA: 0x0001DB38 File Offset: 0x0001BB38
	internal unsafe static method* __onexitend_m;

	// Token: 0x040000A3 RID: 163
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x040000A4 RID: 164
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x040000A5 RID: 165 RVA: 0x0001D6B0 File Offset: 0x0001B6B0
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AA__ZVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x040000A6 RID: 166 RVA: 0x0001D760 File Offset: 0x0001B760
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x040000A7 RID: 167 RVA: 0x0001D6C0 File Offset: 0x0001B6C0
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x040000A8 RID: 168 RVA: 0x0001D6D0 File Offset: 0x0001B6D0
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x040000A9 RID: 169 RVA: 0x0001D6E0 File Offset: 0x0001B6E0
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x040000AA RID: 170 RVA: 0x0001D6F0 File Offset: 0x0001B6F0
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000AB RID: 171 RVA: 0x0001D770 File Offset: 0x0001B770
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000AC RID: 172 RVA: 0x0001D700 File Offset: 0x0001B700
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000AD RID: 173 RVA: 0x0001D710 File Offset: 0x0001B710
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000AE RID: 174 RVA: 0x0001D720 File Offset: 0x0001B720
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000AF RID: 175 RVA: 0x0001D730 File Offset: 0x0001B730
	public static method __m2mep@?__global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040000B0 RID: 176 RVA: 0x0001D740 File Offset: 0x0001B740
	public static method __m2mep@?__global_unlock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040000B1 RID: 177 RVA: 0x0001D780 File Offset: 0x0001B780
	public static method __m2mep@?__alloc_global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040000B2 RID: 178 RVA: 0x0001D750 File Offset: 0x0001B750
	public static method __m2mep@?__dealloc_global_lock@?A0xdf32f105@@$$FYAXXZ;

	// Token: 0x040000B3 RID: 179 RVA: 0x0001D620 File Offset: 0x0001B620
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x040000B4 RID: 180 RVA: 0x0001D630 File Offset: 0x0001B630
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x040000B5 RID: 181 RVA: 0x0001D670 File Offset: 0x0001B670
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x040000B6 RID: 182 RVA: 0x0001D680 File Offset: 0x0001B680
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040000B7 RID: 183 RVA: 0x0001D640 File Offset: 0x0001B640
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040000B8 RID: 184 RVA: 0x0001D690 File Offset: 0x0001B690
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x040000B9 RID: 185 RVA: 0x0001D650 File Offset: 0x0001B650
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x040000BA RID: 186 RVA: 0x0001D6A0 File Offset: 0x0001B6A0
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040000BB RID: 187 RVA: 0x0001D660 File Offset: 0x0001B660
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040000BC RID: 188 RVA: 0x0001D790 File Offset: 0x0001B790
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x040000BD RID: 189 RVA: 0x0001D7A0 File Offset: 0x0001B7A0
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x040000BE RID: 190 RVA: 0x0001D7C0 File Offset: 0x0001B7C0
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x040000BF RID: 191 RVA: 0x0001D7B0 File Offset: 0x0001B7B0
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x040000C0 RID: 192 RVA: 0x0001D7D0 File Offset: 0x0001B7D0
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x040000C1 RID: 193 RVA: 0x00006388 File Offset: 0x00004D88
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x040000C2 RID: 194 RVA: 0x00006228 File Offset: 0x00004C28
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x040000C3 RID: 195 RVA: 0x0001D940 File Offset: 0x0001B940
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x040000C4 RID: 196 RVA: 0x0001D948 File Offset: 0x0001B948
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x040000C5 RID: 197 RVA: 0x00006210 File Offset: 0x00004C10
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x040000C6 RID: 198 RVA: 0x00006220 File Offset: 0x00004C20
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x040000C7 RID: 199 RVA: 0x0001D2F4 File Offset: 0x0001B2F4
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x040000C8 RID: 200 RVA: 0x00006218 File Offset: 0x00004C18
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
