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
using ?A0xab5de71f;
using DirectX;
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
	internal unsafe static XMMATRIX* {ctor}(XMMATRIX* A_0, XMMATRIX* A_0)
	{
		cpblk(A_0, A_0, 64);
		return A_0;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00001104 File Offset: 0x00000504
	internal unsafe static void* @new(ulong _Size, void* _Where)
	{
		return _Where;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00001114 File Offset: 0x00000514
	internal unsafe static exception* {ctor}(exception* A_0, sbyte* _Message, int __unnamed001)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		exception* ptr = A_0 / sizeof(exception) + 8L;
		initblk(ptr, 0, 16L);
		*(long*)ptr = _Message;
		return A_0;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00001138 File Offset: 0x00000538
	internal unsafe static exception* {ctor}(exception* A_0, exception* _Other)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		exception* ptr = A_0 / sizeof(exception) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(_Other / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		return A_0;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00001164 File Offset: 0x00000564
	internal unsafe static void {dtor}(exception* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000226C File Offset: 0x0000166C
	internal unsafe static sbyte* what(exception* A_0)
	{
		ulong num = (ulong)(*(A_0 + 8L));
		return (num != 0UL) ? num : ref <Module>.??_C@_0BC@EOODALEL@Unknown?5exception@;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000220C File Offset: 0x0000160C
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

	// Token: 0x06000009 RID: 9 RVA: 0x00001184 File Offset: 0x00000584
	internal unsafe static bad_alloc* {ctor}(bad_alloc* A_0, sbyte* _Message)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_alloc* ptr = A_0 / sizeof(bad_alloc) + 8L;
		initblk(ptr, 0, 16L);
		*(long*)ptr = _Message;
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

	// Token: 0x0600000A RID: 10 RVA: 0x0000228C File Offset: 0x0000168C
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

	// Token: 0x0600000B RID: 11 RVA: 0x000011DC File Offset: 0x000005DC
	internal unsafe static void {dtor}(bad_alloc* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000011FC File Offset: 0x000005FC
	internal unsafe static bad_array_new_length* {ctor}(bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_array_new_length* ptr = A_0 / sizeof(bad_array_new_length) + 8L;
		initblk(ptr, 0, 16L);
		*(long*)ptr = ref <Module>.??_C@_0BF@KINCDENJ@bad?5array?5new?5length@;
		try
		{
			*A_0 = ref <Module>.??_7bad_alloc@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
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

	// Token: 0x0600000D RID: 13 RVA: 0x000022EC File Offset: 0x000016EC
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

	// Token: 0x0600000E RID: 14 RVA: 0x0000234C File Offset: 0x0000174C
	internal unsafe static void {dtor}(bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00001288 File Offset: 0x00000688
	internal unsafe static void {dtor}(exception_ptr* A_0)
	{
		<Module>.__ExceptionPtrDestroy(A_0);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000129C File Offset: 0x0000069C
	internal unsafe static exception_ptr* {ctor}(exception_ptr* A_0, exception_ptr* _Rhs)
	{
		*A_0 = 0L;
		*(A_0 + 8L) = 0L;
		<Module>.__ExceptionPtrCopy(A_0, _Rhs);
		return A_0;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000023EC File Offset: 0x000017EC
	internal unsafe static sbyte* what(bad_variant_access* A_0)
	{
		return ref <Module>.??_C@_0BD@FGIAGBIG@bad?5variant?5access@;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000023CC File Offset: 0x000017CC
	internal unsafe static void {dtor}(bad_variant_access* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000012F4 File Offset: 0x000006F4
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

	// Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000176C
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

	// Token: 0x06000015 RID: 21 RVA: 0x00001354 File Offset: 0x00000754
	internal unsafe static void _Throw_bad_array_new_length()
	{
		bad_array_new_length bad_array_new_length;
		<Module>.std.bad_array_new_length.{ctor}(ref bad_array_new_length);
		<Module>._CxxThrowException((void*)(&bad_array_new_length), (_s__ThrowInfo*)(&<Module>._TI3?AVbad_array_new_length@std@@));
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000013D8 File Offset: 0x000007D8
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

	// Token: 0x06000017 RID: 23 RVA: 0x00001378 File Offset: 0x00000778
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

	// Token: 0x06000018 RID: 24 RVA: 0x00001420 File Offset: 0x00000820
	internal unsafe static void* _Allocate(ulong _Bytes)
	{
		return <Module>.@new(_Bytes);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00001434 File Offset: 0x00000834
	internal unsafe static void _Adjust_manually_vector_aligned(void** _Ptr, ulong* _Bytes)
	{
		*_Bytes += 39L;
		long num = *_Ptr;
		ulong _Ptr_container = *(num - 8L);
		if (num - _Ptr_container - 8L <= 31L)
		{
			*_Ptr = _Ptr_container;
		}
		else
		{
			<Module>._invoke_watson(null, null, null, 0U, 0UL);
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00001470 File Offset: 0x00000870
	internal unsafe static void _Orphan_all(_Container_base0* A_0)
	{
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00001480 File Offset: 0x00000880
	internal unsafe static void _Swap_proxy_and_iterators(_Container_base0* A_0, _Container_base0* A_0)
	{
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00001490 File Offset: 0x00000890
	internal unsafe static void _Alloc_proxy(_Container_base0* A_0, _Fake_allocator* A_0)
	{
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00001BE4 File Offset: 0x00000FE4
	internal unsafe static _Iterator_base12* {ctor}(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*(A_0 + 8L) = 0L;
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000014A0 File Offset: 0x000008A0
	internal unsafe static _Iterator_base12* =(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000014B4 File Offset: 0x000008B4
	internal unsafe static _Fake_proxy_ptr_impl* {ctor}(_Fake_proxy_ptr_impl* A_0, _Fake_allocator* A_0, _Container_base0* A_1)
	{
		return A_0;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000014C4 File Offset: 0x000008C4
	internal unsafe static void _Release(_Fake_proxy_ptr_impl* A_0)
	{
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000014D4 File Offset: 0x000008D4
	internal unsafe static void _Xlen_string()
	{
		<Module>.std._Xlength_error((sbyte*)(&<Module>.??_C@_0BA@JFNIOLAK@string?5too?5long@));
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002400 File Offset: 0x00001800
	internal unsafe static void* __vecDelDtor(runtime_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			runtime_error* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.runtime_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				runtime_error* ptr2 = ptr;
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

	// Token: 0x06000023 RID: 35 RVA: 0x000014EC File Offset: 0x000008EC
	internal unsafe static void {dtor}(runtime_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002460 File Offset: 0x00001860
	internal unsafe static void* __vecDelDtor(range_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			range_error* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*(long*)ptr), ldftn(std.range_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				range_error* ptr2 = ptr;
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

	// Token: 0x06000025 RID: 37 RVA: 0x000024C0 File Offset: 0x000018C0
	internal unsafe static void {dtor}(range_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000156C File Offset: 0x0000096C
	internal unsafe static range_error* {ctor}(range_error* A_0, range_error* A_0)
	{
		<Module>.std.runtime_error.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7range_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.runtime_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000150C File Offset: 0x0000090C
	internal unsafe static runtime_error* {ctor}(runtime_error* A_0, runtime_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		runtime_error* ptr = A_0 / sizeof(runtime_error) + 8L;
		initblk(ptr, 0, 16L);
		<Module>.__std_exception_copy(A_0 / sizeof(__std_exception_data) + 8L, (__std_exception_data*)ptr);
		try
		{
			*A_0 = ref <Module>.??_7runtime_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000024E0 File Offset: 0x000018E0
	internal unsafe static void* __vecDelDtor(_System_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			_System_error* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 40UL, (ulong)(*(long*)ptr), ldftn(std._System_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				_System_error* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 40L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 40UL);
		}
		return A_0;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000015B4 File Offset: 0x000009B4
	internal unsafe static void {dtor}(_System_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002540 File Offset: 0x00001940
	internal unsafe static void* __vecDelDtor(system_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			system_error* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 40UL, (ulong)(*(long*)ptr), ldftn(std.system_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				system_error* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 40L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 40UL);
		}
		return A_0;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000015D4 File Offset: 0x000009D4
	internal unsafe static void {dtor}(system_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000164C File Offset: 0x00000A4C
	internal unsafe static system_error* {ctor}(system_error* A_0, system_error* A_0)
	{
		<Module>.std.runtime_error.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7_System_error@std@@6B@;
			cpblk(A_0 + 24L, A_0 + 24L, 16);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.runtime_error.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7system_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._System_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000015F4 File Offset: 0x000009F4
	internal unsafe static _System_error* {ctor}(_System_error* A_0, _System_error* A_0)
	{
		<Module>.std.runtime_error.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7_System_error@std@@6B@;
			cpblk(A_0 + 24L, A_0 + 24L, 16);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.runtime_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000025A0 File Offset: 0x000019A0
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

	// Token: 0x0600002F RID: 47 RVA: 0x00002600 File Offset: 0x00001A00
	internal unsafe static void {dtor}(bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000016D4 File Offset: 0x00000AD4
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

	// Token: 0x06000031 RID: 49 RVA: 0x000026A0 File Offset: 0x00001AA0
	internal unsafe static sbyte* what(bad_weak_ptr* A_0)
	{
		return ref <Module>.??_C@_0N@FJHHFFAF@bad_weak_ptr@;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002620 File Offset: 0x00001A20
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

	// Token: 0x06000033 RID: 51 RVA: 0x00002680 File Offset: 0x00001A80
	internal unsafe static void {dtor}(bad_weak_ptr* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00001734 File Offset: 0x00000B34
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

	// Token: 0x06000035 RID: 53 RVA: 0x00001794 File Offset: 0x00000B94
	internal unsafe static locale* {ctor}(locale* A_0, locale* _Right)
	{
		long num = *(_Right + 8L);
		*(A_0 + 8L) = num;
		long num2 = num;
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num2, *(*num2 + 8L));
		return A_0;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000026B4 File Offset: 0x00001AB4
	internal unsafe static void* __vecDelDtor(ios_base.failure* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			ios_base.failure* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 40UL, (ulong)(*(long*)ptr), ldftn(std.ios_base.failure.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				ios_base.failure* ptr2 = ptr;
				<Module>.delete[]((void*)ptr2, (ulong)(*(long*)ptr2 * 40L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 40UL);
		}
		return A_0;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002714 File Offset: 0x00001B14
	internal unsafe static void {dtor}(ios_base.failure* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000017BC File Offset: 0x00000BBC
	internal unsafe static ios_base.failure* {ctor}(ios_base.failure* A_0, ios_base.failure* A_0)
	{
		<Module>.std.runtime_error.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7_System_error@std@@6B@;
			cpblk(A_0 + 24L, A_0 + 24L, 16);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.runtime_error.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7system_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._System_error.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7failure@ios_base@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.system_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00001954 File Offset: 0x00000D54
	internal unsafe static void* __delDtor(StereoscopicRender* A_0, uint A_0)
	{
		<Module>.StereoscopicRender.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 80UL);
		}
		return A_0;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000200C File Offset: 0x0000140C
	internal unsafe static void {dtor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00001EA4 File Offset: 0x000012A4
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* {ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		initblk(A_0, 0, 16L);
		try
		{
			*(A_0 + 16L) = 0L;
			*(A_0 + 24L) = 0L;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, _Right);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000217C File Offset: 0x0000157C
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* {ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		initblk(A_0, 0, 16L);
		try
		{
			*(A_0 + 16L) = 0L;
			*(A_0 + 24L) = 0L;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			ulong count = (ulong)(*(_Right + 16L));
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct<2,char\u0020const\u0020*>(A_0, <Module>.std._String_val<std::_Simple_types<char>\u0020>._Myptr(_Right), count);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00001D98 File Offset: 0x00001198
	internal unsafe static void {dtor}(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0)
	{
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00001C30 File Offset: 0x00001030
	internal unsafe static void {dtor}(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00001978 File Offset: 0x00000D78
	internal unsafe static ulong* min<unsigned\u0020__int64>(ulong* _Left, ulong* _Right)
	{
		return (*_Right < *_Left) ? _Right : _Left;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00001990 File Offset: 0x00000D90
	internal unsafe static allocator<char>* move<class\u0020std::allocator<char>\u0020&>(allocator<char>* _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00001DA8 File Offset: 0x000011A8
	internal unsafe static _Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* {ctor}<class\u0020std::allocator<char>\u0020>(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0, _One_then_variadic_args_t __unnamed000, allocator<char>* _Val1)
	{
		initblk(A_0, 0, 16L);
		try
		{
			*(A_0 + 16L) = 0L;
			*(A_0 + 24L) = 0L;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000020D8 File Offset: 0x000014D8
	internal unsafe static void _Construct<2,char\u0020const\u0020*>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Arg, ulong _Count)
	{
		if (_Count > <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0))
		{
			<Module>.std._Xlen_string();
		}
		if (_Count <= 15L)
		{
			*(A_0 + 16L) = _Count;
			*(A_0 + 24L) = 15L;
			cpblk(A_0, _Arg, 16L);
		}
		else
		{
			ulong num = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0);
			ulong num2 = _Count | 15L;
			ulong num3;
			if (num2 > num)
			{
				num3 = num;
			}
			else if (15L > num - 7L)
			{
				num3 = num;
			}
			else
			{
				ulong num4 = 22L;
				num3 = *(ref (num2 < 22L) ? ref num4 : ref num2);
			}
			ulong _New_capacity = num3 + 1L;
			sbyte* _New_ptr = <Module>.std.allocator<char>.allocate(A_0, _New_capacity);
			*A_0 = _New_ptr;
			*(A_0 + 16L) = _Count;
			*(A_0 + 24L) = (long)(_New_capacity - 1UL);
			cpblk(_New_ptr, _Arg, _Count + 1L);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000019A0 File Offset: 0x00000DA0
	internal unsafe static void {dtor}(_String_val<std::_Simple_types<char>\u0020>._Bxty* A_0)
	{
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00001C40 File Offset: 0x00001040
	internal unsafe static sbyte* _Myptr(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		sbyte* result = A_0;
		if (((*(A_0 + 24L) > 15L) ? 1 : 0) != 0)
		{
			result = *A_0;
		}
		return result;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000019B0 File Offset: 0x00000DB0
	internal unsafe static allocator<char>* select_on_container_copy_construction(allocator<char>* A_0, allocator<char>* _Al)
	{
		return A_0;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00001C60 File Offset: 0x00001060
	internal unsafe static allocator<char>* _Getal(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00001C70 File Offset: 0x00001070
	internal unsafe static allocator<char>* _Getal(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00001F28 File Offset: 0x00001328
	internal unsafe static void _Tidy_deallocate(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		ulong num = (ulong)(*(A_0 + 24L));
		if (((num > 15UL) ? 1 : 0) != 0)
		{
			ulong num2 = num + 1UL;
			void* ptr = *A_0;
			if (num2 >= 4096UL)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num2);
			}
			<Module>.delete(ptr, num2);
		}
		*(A_0 + 16L) = 0L;
		*(A_0 + 24L) = 15L;
		*A_0 = 0;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00001DFC File Offset: 0x000011FC
	internal unsafe static void _Take_contents(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		cpblk(A_0, _Right, 32);
		*(_Right + 16L) = 0L;
		*(_Right + 24L) = 15L;
		*_Right = 0;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000019C0 File Offset: 0x00000DC0
	internal unsafe static sbyte* copy(sbyte* _First1, sbyte* _First2, ulong _Count)
	{
		cpblk(_First1, _First2, _Count);
		return _First1;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000019D8 File Offset: 0x00000DD8
	internal unsafe static allocator<char>* {ctor}(allocator<char>* A_0, allocator<char>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000019E8 File Offset: 0x00000DE8
	internal unsafe static allocator<char>* forward<class\u0020std::allocator<char>\u0020>(allocator<char>* _Arg)
	{
		return _Arg;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000204C File Offset: 0x0000144C
	internal unsafe static sbyte* _Allocate_for_capacity<0>(allocator<char>* _Al, ulong* _Capacity)
	{
		*_Capacity += 1L;
		ulong count = (ulong)(*_Capacity);
		sbyte* result = <Module>.std.allocator<char>.allocate(_Al, count);
		*_Capacity += -1L;
		return result;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00001C80 File Offset: 0x00001080
	internal unsafe static void _Construct_in_place<char\u0020*,char\u0020*\u0020const\u0020&>(sbyte** _Obj, sbyte** <_Args_0>)
	{
		*_Obj = *<_Args_0>;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000019F8 File Offset: 0x00000DF8
	internal unsafe static sbyte* _Unfancy<char>(sbyte* _Ptr)
	{
		return _Ptr;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00001A08 File Offset: 0x00000E08
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>* addressof<class\u0020std::_String_val<struct\u0020std::_Simple_types<char>\u0020>\u0020>(_String_val<std::_Simple_types<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00001C94 File Offset: 0x00001094
	internal unsafe static void _Construct_in_place<char\u0020*,char\u0020*\u0020&>(sbyte** _Obj, sbyte** <_Args_0>)
	{
		*_Obj = *<_Args_0>;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00001A18 File Offset: 0x00000E18
	internal unsafe static allocator<char>* _Get_first(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00001A28 File Offset: 0x00000E28
	internal unsafe static allocator<char>* _Get_first(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00001CA8 File Offset: 0x000010A8
	internal unsafe static void _Switch_to_buf(_String_val<std::_Simple_types<char>\u0020>._Bxty* A_0)
	{
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00001A38 File Offset: 0x00000E38
	internal unsafe static void _Activate_SSO_buffer(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00001A48 File Offset: 0x00000E48
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool _Large_mode_engaged(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		return (*(A_0 + 24L) > 15L) ? 1 : 0;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00001CB8 File Offset: 0x000010B8
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>* {ctor}(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		initblk(A_0, 0, 16L);
		try
		{
			*(A_0 + 16L) = 0L;
			*(A_0 + 24L) = 0L;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00001A64 File Offset: 0x00000E64
	internal unsafe static ulong* max<unsigned\u0020__int64>(ulong* _Left, ulong* _Right)
	{
		return (*_Left < *_Right) ? _Right : _Left;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00001A7C File Offset: 0x00000E7C
	internal unsafe static void _Swap_proxy_and_iterators(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00001A8C File Offset: 0x00000E8C
	internal unsafe static ulong _Calculate_growth(ulong _Requested, ulong _Old, ulong _Max)
	{
		ulong _Masked = _Requested | 15L;
		if (_Masked > _Max)
		{
			return _Max;
		}
		ulong num = _Old >> 1;
		if (_Old > _Max - num)
		{
			return _Max;
		}
		ulong num2 = num + _Old;
		ulong num3 = num2;
		return *(ref (_Masked < num2) ? ref num3 : ref _Masked);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00001D0C File Offset: 0x0000110C
	internal unsafe static ulong max_size(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return long.MaxValue;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00001E28 File Offset: 0x00001228
	internal unsafe static void _Deallocate_for_capacity(allocator<char>* _Al, sbyte* _Old_ptr, ulong _Capacity)
	{
		ulong num = _Capacity + 1L;
		void* ptr = _Old_ptr;
		if (num >= 4096UL)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num);
		}
		<Module>.delete(ptr, num);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00001AC4 File Offset: 0x00000EC4
	internal unsafe static void assign(sbyte* _Left, sbyte* _Right)
	{
		*_Left = (byte)(*_Right);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00001F7C File Offset: 0x0000137C
	internal unsafe static sbyte* _Allocate_at_least_helper<class\u0020std::allocator<char>\u0020>(allocator<char>* _Al, ulong* _Count)
	{
		return <Module>.std.allocator<char>.allocate(_Al, *_Count);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00001AD8 File Offset: 0x00000ED8
	internal unsafe static sbyte** addressof<char\u0020*>(sbyte** _Val)
	{
		return _Val;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00001AE8 File Offset: 0x00000EE8
	internal unsafe static sbyte** forward<char\u0020*\u0020const\u0020&>(sbyte** _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00001AF8 File Offset: 0x00000EF8
	internal unsafe static sbyte** forward<char\u0020*\u0020&>(sbyte** _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00001B08 File Offset: 0x00000F08
	internal unsafe static void _Destroy_in_place<char\u0020*>(sbyte** _Obj)
	{
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00001B18 File Offset: 0x00000F18
	internal static long _Max_limit<__int64>()
	{
		return long.MaxValue;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00001B30 File Offset: 0x00000F30
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>._Bxty* {ctor}(_String_val<std::_Simple_types<char>\u0020>._Bxty* A_0)
	{
		initblk(A_0, 0, 16L);
		return A_0;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00001B48 File Offset: 0x00000F48
	internal unsafe static ulong max_size(allocator<char>* A_0)
	{
		return -1L;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00001D24 File Offset: 0x00001124
	internal unsafe static void deallocate(allocator<char>* A_0, sbyte* _Ptr, ulong _Count)
	{
		ulong num = _Count;
		void* ptr = _Ptr;
		if (_Count >= 4096L)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num);
		}
		<Module>.delete(ptr, num);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00001B60 File Offset: 0x00000F60
	internal unsafe static void _Deallocate<16>(void* _Ptr, ulong _Bytes)
	{
		if (_Bytes >= 4096UL)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref _Ptr, ref _Bytes);
		}
		<Module>.delete(_Ptr, _Bytes);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00001E58 File Offset: 0x00001258
	internal unsafe static sbyte* allocate(allocator<char>* A_0, ulong _Count)
	{
		void* result;
		if (_Count == null)
		{
			result = null;
		}
		else if (_Count >= 4096L)
		{
			result = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(_Count);
		}
		else
		{
			result = <Module>.@new(_Count);
		}
		return result;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00001D50 File Offset: 0x00001150
	internal unsafe static void* _Allocate<16,struct\u0020std::_Default_allocate_traits>(ulong _Bytes)
	{
		if (_Bytes == null)
		{
			return 0L;
		}
		if (_Bytes >= 4096L)
		{
			return <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(_Bytes);
		}
		return <Module>.@new(_Bytes);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00001B88 File Offset: 0x00000F88
	internal static ulong _Get_size_of_n<1>(ulong _Count)
	{
		return _Count;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00001B98 File Offset: 0x00000F98
	internal unsafe static void* _Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(ulong _Bytes)
	{
		ulong _Block_size = _Bytes + 39L;
		if (_Block_size <= _Bytes)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		ulong _Ptr_container = <Module>.@new(_Block_size);
		if (_Ptr_container == null)
		{
			<Module>._invoke_watson(null, null, null, 0U, 0UL);
		}
		void* _Ptr = _Ptr_container + 39L & -32L;
		*(_Ptr - 8L) = _Ptr_container;
		return _Ptr;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003B78 File Offset: 0x00002F78
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

	// Token: 0x0600006D RID: 109 RVA: 0x00003BBC File Offset: 0x00002FBC
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
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

	// Token: 0x0600006E RID: 110 RVA: 0x00003C00 File Offset: 0x00003000
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x0600006F RID: 111 RVA: 0x00003CEC File Offset: 0x000030EC
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x06000070 RID: 112 RVA: 0x00003C5C File Offset: 0x0000305C
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

	// Token: 0x06000071 RID: 113 RVA: 0x00003C94 File Offset: 0x00003094
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
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

	// Token: 0x06000072 RID: 114 RVA: 0x00003D4C File Offset: 0x0000314C
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void __ehvec_dtor(void* ptr, ulong size, int count, method destructor)
	{
		<Module>.__ehvec_dtor(ptr, size, (ulong)((long)count), destructor);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000045A0 File Offset: 0x000039A0
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003F54 File Offset: 0x00003354
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003F68 File Offset: 0x00003368
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00004084 File Offset: 0x00003484
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000409C File Offset: 0x0000349C
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000040EC File Offset: 0x000034EC
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

	// Token: 0x06000079 RID: 121 RVA: 0x0000416C File Offset: 0x0000356C
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004188 File Offset: 0x00003588
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

	// Token: 0x0600007B RID: 123 RVA: 0x000041E8 File Offset: 0x000035E8
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

	// Token: 0x0600007C RID: 124 RVA: 0x000042A0 File Offset: 0x000036A0
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

	// Token: 0x0600007D RID: 125 RVA: 0x000042D0 File Offset: 0x000036D0
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000042F0 File Offset: 0x000036F0
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

	// Token: 0x0600007F RID: 127 RVA: 0x00004344 File Offset: 0x00003744
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

	// Token: 0x06000080 RID: 128 RVA: 0x000043C4 File Offset: 0x000037C4
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

	// Token: 0x06000081 RID: 129 RVA: 0x000043FC File Offset: 0x000037FC
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00004410 File Offset: 0x00003810
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00001000 File Offset: 0x00000400
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00001014 File Offset: 0x00000414
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00001028 File Offset: 0x00000428
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000103C File Offset: 0x0000043C
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00001050 File Offset: 0x00000450
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00001064 File Offset: 0x00000464
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00001078 File Offset: 0x00000478
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000045F4 File Offset: 0x000039F4
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004628 File Offset: 0x00003A28
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00004648 File Offset: 0x00003A48
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

	// Token: 0x0600008D RID: 141 RVA: 0x000046D8 File Offset: 0x00003AD8
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

	// Token: 0x0600008E RID: 142 RVA: 0x00004718 File Offset: 0x00003B18
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

	// Token: 0x0600008F RID: 143 RVA: 0x00004754 File Offset: 0x00003B54
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00004780 File Offset: 0x00003B80
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

	// Token: 0x06000091 RID: 145 RVA: 0x0000442C File Offset: 0x0000382C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004440 File Offset: 0x00003840
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

	// Token: 0x06000093 RID: 147 RVA: 0x00004478 File Offset: 0x00003878
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

	// Token: 0x06000094 RID: 148 RVA: 0x000044B0 File Offset: 0x000038B0
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

	// Token: 0x06000095 RID: 149 RVA: 0x0000487C File Offset: 0x00003C7C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x06000096 RID: 150 RVA: 0x000048F0 File Offset: 0x00003CF0
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00004908 File Offset: 0x00003D08
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000491C File Offset: 0x00003D1C
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x06000099 RID: 153 RVA: 0x000049D8 File Offset: 0x00003DD8
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x0600009A RID: 154 RVA: 0x000044EC File Offset: 0x000038EC
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00004510 File Offset: 0x00003910
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00004538 File Offset: 0x00003938
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00004560 File Offset: 0x00003960
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00004A60 File Offset: 0x00003E60
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

	// Token: 0x0600009F RID: 159 RVA: 0x00004F48 File Offset: 0x00004348
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00004A90 File Offset: 0x00003E90
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

	// Token: 0x060000A1 RID: 161 RVA: 0x00004ADC File Offset: 0x00003EDC
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

	// Token: 0x060000A2 RID: 162 RVA: 0x00004B00 File Offset: 0x00003F00
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType handle = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (handle != null)
		{
			((GCHandle)handle).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00004B28 File Offset: 0x00003F28
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00004F64 File Offset: 0x00004364
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

	// Token: 0x060000A5 RID: 165 RVA: 0x00004B44 File Offset: 0x00003F44
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

	// Token: 0x060000A6 RID: 166 RVA: 0x00004B6C File Offset: 0x00003F6C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00004B84 File Offset: 0x00003F84
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00004B9C File Offset: 0x00003F9C
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

	// Token: 0x060000A9 RID: 169 RVA: 0x00004BBC File Offset: 0x00003FBC
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

	// Token: 0x060000AA RID: 170 RVA: 0x00004F94 File Offset: 0x00004394
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00004BDC File Offset: 0x00003FDC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00004BF0 File Offset: 0x00003FF0
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

	// Token: 0x060000AD RID: 173 RVA: 0x00004D68 File Offset: 0x00004168
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

	// Token: 0x060000AE RID: 174 RVA: 0x00004FAC File Offset: 0x000043AC
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

	// Token: 0x060000AF RID: 175 RVA: 0x00004FF4 File Offset: 0x000043F4
	internal static method _onexit_m(method _Function)
	{
		return (<Module>._atexit_m(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00004E18 File Offset: 0x00004218
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xdf32f105.__exit_list_size, &<Module>.?A0xdf32f105.__onexitend_m, &<Module>.?A0xdf32f105.__onexitbegin_m);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00005010 File Offset: 0x00004410
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x060000B2 RID: 178 RVA: 0x00004E40 File Offset: 0x00004240
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
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

	// Token: 0x060000B3 RID: 179 RVA: 0x00005054 File Offset: 0x00004454
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00004F20 File Offset: 0x00004320
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x060000B5 RID: 181
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x060000B6 RID: 182
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x060000B7 RID: 183 RVA: 0x00005084 File Offset: 0x00004484
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

	// Token: 0x060000B8 RID: 184 RVA: 0x000050B4 File Offset: 0x000044B4
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

	// Token: 0x060000B9 RID: 185 RVA: 0x000050DC File Offset: 0x000044DC
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000512C File Offset: 0x0000452C
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

	// Token: 0x060000BB RID: 187 RVA: 0x00005100 File Offset: 0x00004500
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x000051BF File Offset: 0x000045BF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void _CxxThrowException(void*, _s__ThrowInfo*);

	// Token: 0x060000BD RID: 189 RVA: 0x00005ED5 File Offset: 0x000052D5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __ExceptionPtrDestroy(void*);

	// Token: 0x060000BE RID: 190 RVA: 0x00002D80 File Offset: 0x00002180
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x060000BF RID: 191 RVA: 0x000051B3 File Offset: 0x000045B3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __std_exception_copy(__std_exception_data*, __std_exception_data*);

	// Token: 0x060000C0 RID: 192 RVA: 0x000054F0 File Offset: 0x000048F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern StereoscopicRender* {ctor}(StereoscopicRender*);

	// Token: 0x060000C1 RID: 193 RVA: 0x00005500 File Offset: 0x00004900
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void {dtor}(StereoscopicRender*);

	// Token: 0x060000C2 RID: 194 RVA: 0x00005600 File Offset: 0x00004A00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool Initiallize(StereoscopicRender*, ID3D11Device*);

	// Token: 0x060000C3 RID: 195 RVA: 0x00005CB0 File Offset: 0x000050B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool renderTexture(StereoscopicRender*, ID3D11Device*, ID3D11DeviceContext*, ID3D11ShaderResourceView*, ID3D11RenderTargetView*);

	// Token: 0x060000C4 RID: 196 RVA: 0x00005EE1 File Offset: 0x000052E1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void _Xlength_error(sbyte*);

	// Token: 0x060000C5 RID: 197 RVA: 0x00005EDB File Offset: 0x000052DB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __ExceptionPtrCopy(void*, void*);

	// Token: 0x060000C6 RID: 198 RVA: 0x00005F50 File Offset: 0x00005350
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* @new(ulong);

	// Token: 0x060000C7 RID: 199 RVA: 0x0000642C File Offset: 0x0000582C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void _invoke_watson(char*, char*, char*, uint, ulong);

	// Token: 0x060000C8 RID: 200 RVA: 0x00005F8C File Offset: 0x0000538C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete[](void*, ulong);

	// Token: 0x060000C9 RID: 201 RVA: 0x000051B9 File Offset: 0x000045B9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void __std_exception_destroy(__std_exception_data*);

	// Token: 0x060000CA RID: 202 RVA: 0x0000641A File Offset: 0x0000581A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x060000CB RID: 203 RVA: 0x000051D7 File Offset: 0x000045D7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void** __current_exception_context();

	// Token: 0x060000CC RID: 204 RVA: 0x00005225 File Offset: 0x00004625
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void terminate();

	// Token: 0x060000CD RID: 205 RVA: 0x000051D1 File Offset: 0x000045D1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void** __current_exception();

	// Token: 0x060000CE RID: 206 RVA: 0x000042C0 File Offset: 0x000036C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x060000CF RID: 207 RVA: 0x0000521F File Offset: 0x0000461F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x060000D0 RID: 208 RVA: 0x00006408 File Offset: 0x00005808
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x060000D1 RID: 209 RVA: 0x00006432 File Offset: 0x00005832
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void abort();

	// Token: 0x060000D2 RID: 210 RVA: 0x00002DF8 File Offset: 0x000021F8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x04000001 RID: 1 RVA: 0x00008C58 File Offset: 0x00007258
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000002 RID: 2 RVA: 0x00008C30 File Offset: 0x00007230
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@KINCDENJ@bad?5array?5new?5length@;

	// Token: 0x04000003 RID: 3 RVA: 0x00008C70 File Offset: 0x00007270
	internal static $ArrayType$$$BY0BD@$$CBD ??_C@_0BD@FGIAGBIG@bad?5variant?5access@;

	// Token: 0x04000004 RID: 4 RVA: 0x00008C48 File Offset: 0x00007248
	internal static $ArrayType$$$BY0BA@$$CBD ??_C@_0BA@JFNIOLAK@string?5too?5long@;

	// Token: 0x04000005 RID: 5 RVA: 0x00008C88 File Offset: 0x00007288
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@FJHHFFAF@bad_weak_ptr@;

	// Token: 0x04000006 RID: 6 RVA: 0x000270A0 File Offset: 0x000254A0
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_variant_access@std@@6B@;

	// Token: 0x04000007 RID: 7 RVA: 0x00027EE8 File Offset: 0x000262E8
	internal static $_TypeDescriptor$_extraBytes_27 ??_R0?AVfailure@ios_base@std@@@8;

	// Token: 0x04000008 RID: 8 RVA: 0x00027DF8 File Offset: 0x000261F8
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AVruntime_error@std@@@8;

	// Token: 0x04000009 RID: 9 RVA: 0x00025068 File Offset: 0x00023668
	internal static _s__RTTICompleteObjectLocator2 ??_R4exception@std@@6B@;

	// Token: 0x0400000A RID: 10 RVA: 0x00025138 File Offset: 0x00023738
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2bad_array_new_length@std@@8;

	// Token: 0x0400000B RID: 11 RVA: 0x000253D0 File Offset: 0x000239D0
	internal static $_s__RTTIBaseClassArray$_extraBytes_32 ??_R2system_error@std@@8;

	// Token: 0x0400000C RID: 12 RVA: 0x000253A8 File Offset: 0x000239A8
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@system_error@std@@8;

	// Token: 0x0400000D RID: 13 RVA: 0x00027D70 File Offset: 0x00026170
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVbad_alloc@std@@@8;

	// Token: 0x0400000E RID: 14 RVA: 0x000254B8 File Offset: 0x00023AB8
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_weak_ptr@std@@8;

	// Token: 0x0400000F RID: 15 RVA: 0x00025270 File Offset: 0x00023870
	internal static _s__RTTICompleteObjectLocator2 ??_R4runtime_error@std@@6B@;

	// Token: 0x04000010 RID: 16 RVA: 0x000251F0 File Offset: 0x000237F0
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_variant_access@std@@6B@;

	// Token: 0x04000011 RID: 17 RVA: 0x00025380 File Offset: 0x00023980
	internal static _s__RTTICompleteObjectLocator2 ??_R4_System_error@std@@6B@;

	// Token: 0x04000012 RID: 18 RVA: 0x000254F8 File Offset: 0x00023AF8
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_weak_ptr@std@@8;

	// Token: 0x04000013 RID: 19 RVA: 0x00025438 File Offset: 0x00023A38
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_cast@std@@8;

	// Token: 0x04000014 RID: 20 RVA: 0x000250B8 File Offset: 0x000236B8
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_alloc@std@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x00027110 File Offset: 0x00025510
	internal static $ArrayType$$$BY02Q6AXXZ ??_7range_error@std@@6B@;

	// Token: 0x04000016 RID: 22 RVA: 0x00027E20 File Offset: 0x00026220
	internal static $_TypeDescriptor$_extraBytes_22 ??_R0?AVrange_error@std@@@8;

	// Token: 0x04000017 RID: 23 RVA: 0x00025218 File Offset: 0x00023818
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@runtime_error@std@@8;

	// Token: 0x04000018 RID: 24 RVA: 0x00025348 File Offset: 0x00023948
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2_System_error@std@@8;

	// Token: 0x04000019 RID: 25 RVA: 0x00027E70 File Offset: 0x00026270
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVsystem_error@std@@@8;

	// Token: 0x0400001A RID: 26 RVA: 0x00025FD0 File Offset: 0x000245D0
	internal static $_s__CatchableTypeArray$_extraBytes_24 _CTA3?AVbad_array_new_length@std@@;

	// Token: 0x0400001B RID: 27 RVA: 0x00027058 File Offset: 0x00025458
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_array_new_length@std@@6B@;

	// Token: 0x0400001C RID: 28 RVA: 0x00025018 File Offset: 0x00023618
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x0400001D RID: 29 RVA: 0x00025410 File Offset: 0x00023A10
	internal static _s__RTTICompleteObjectLocator2 ??_R4system_error@std@@6B@;

	// Token: 0x0400001E RID: 30 RVA: 0x00025090 File Offset: 0x00023690
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_alloc@std@@8;

	// Token: 0x0400001F RID: 31 RVA: 0x00025368 File Offset: 0x00023968
	internal static _s__RTTIClassHierarchyDescriptor ??_R3_System_error@std@@8;

	// Token: 0x04000020 RID: 32 RVA: 0x00025198 File Offset: 0x00023798
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_variant_access@std@@8;

	// Token: 0x04000021 RID: 33 RVA: 0x00027040 File Offset: 0x00025440
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_alloc@std@@6B@;

	// Token: 0x04000022 RID: 34 RVA: 0x000252C0 File Offset: 0x000238C0
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2range_error@std@@8;

	// Token: 0x04000023 RID: 35 RVA: 0x00025460 File Offset: 0x00023A60
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_cast@std@@8;

	// Token: 0x04000024 RID: 36 RVA: 0x00025040 File Offset: 0x00023640
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2exception@std@@8;

	// Token: 0x04000025 RID: 37 RVA: 0x000255A8 File Offset: 0x00023BA8
	internal static _s__RTTICompleteObjectLocator2 ??_R4failure@ios_base@std@@6B@;

	// Token: 0x04000026 RID: 38 RVA: 0x00027188 File Offset: 0x00025588
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_cast@std@@6B@;

	// Token: 0x04000027 RID: 39 RVA: 0x00027E98 File Offset: 0x00026298
	internal static $_TypeDescriptor$_extraBytes_19 ??_R0?AVbad_cast@std@@@8;

	// Token: 0x04000028 RID: 40 RVA: 0x00025560 File Offset: 0x00023B60
	internal static $_s__RTTIBaseClassArray$_extraBytes_40 ??_R2failure@ios_base@std@@8;

	// Token: 0x04000029 RID: 41 RVA: 0x000254E0 File Offset: 0x00023AE0
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_weak_ptr@std@@8;

	// Token: 0x0400002A RID: 42 RVA: 0x00025590 File Offset: 0x00023B90
	internal static _s__RTTIClassHierarchyDescriptor ??_R3failure@ios_base@std@@8;

	// Token: 0x0400002B RID: 43 RVA: 0x00027018 File Offset: 0x00025418
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x0400002C RID: 44 RVA: 0x00025298 File Offset: 0x00023898
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@range_error@std@@8;

	// Token: 0x0400002D RID: 45 RVA: 0x00025050 File Offset: 0x00023650
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x0400002E RID: 46 RVA: 0x000270E8 File Offset: 0x000254E8
	internal static $ArrayType$$$BY02Q6AXXZ ??_7runtime_error@std@@6B@;

	// Token: 0x0400002F RID: 47 RVA: 0x00027138 File Offset: 0x00025538
	internal static $ArrayType$$$BY02Q6AXXZ ??_7_System_error@std@@6B@;

	// Token: 0x04000030 RID: 48 RVA: 0x00025538 File Offset: 0x00023B38
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@failure@ios_base@std@@8;

	// Token: 0x04000031 RID: 49 RVA: 0x000251C0 File Offset: 0x000237C0
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_variant_access@std@@8;

	// Token: 0x04000032 RID: 50 RVA: 0x00025510 File Offset: 0x00023B10
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_weak_ptr@std@@6B@;

	// Token: 0x04000033 RID: 51 RVA: 0x00025F58 File Offset: 0x00024558
	internal static _s__CatchableType _CT??_R0?AVbad_array_new_length@std@@@8??0bad_array_new_length@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x04000034 RID: 52 RVA: 0x00025320 File Offset: 0x00023920
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@_System_error@std@@8;

	// Token: 0x04000035 RID: 53 RVA: 0x00025478 File Offset: 0x00023A78
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_cast@std@@8;

	// Token: 0x04000036 RID: 54 RVA: 0x00008950 File Offset: 0x00006F50
	internal static XMVECTORF32 g_XMAddDec4;

	// Token: 0x04000037 RID: 55 RVA: 0x000088C0 File Offset: 0x00006EC0
	internal static XMVECTORU32 g_XMFlipY;

	// Token: 0x04000038 RID: 56 RVA: 0x00008930 File Offset: 0x00006F30
	internal static XMVECTORI32 g_XMXorDec4;

	// Token: 0x04000039 RID: 57 RVA: 0x00008B80 File Offset: 0x00007180
	internal static XMVECTORF32 g_XMLogEst7;

	// Token: 0x0400003A RID: 58 RVA: 0x00008390 File Offset: 0x00006990
	internal static XMVECTORF32 g_XMTanCoefficients0;

	// Token: 0x0400003B RID: 59 RVA: 0x000083D0 File Offset: 0x000069D0
	internal static XMVECTORF32 g_XMArcCoefficients1;

	// Token: 0x0400003C RID: 60 RVA: 0x00008710 File Offset: 0x00006D10
	internal static XMVECTORU32 g_XMMaskA2B10G10R10;

	// Token: 0x0400003D RID: 61 RVA: 0x00008420 File Offset: 0x00006A20
	internal static XMVECTORF32 g_XMTanEstCoefficients;

	// Token: 0x0400003E RID: 62 RVA: 0x00008660 File Offset: 0x00006C60
	internal static XMVECTORI32 g_XMInfinity;

	// Token: 0x0400003F RID: 63 RVA: 0x00008A60 File Offset: 0x00007060
	internal static XMVECTORU32 g_XMNegQNaN;

	// Token: 0x04000040 RID: 64 RVA: 0x00008460 File Offset: 0x00006A60
	internal static XMVECTORF32 g_XMIdentityR1;

	// Token: 0x04000041 RID: 65 RVA: 0x000084A0 File Offset: 0x00006AA0
	internal static XMVECTORF32 g_XMNegIdentityR1;

	// Token: 0x04000042 RID: 66 RVA: 0x000089B0 File Offset: 0x00006FB0
	internal static XMVECTORF32 g_XMMaxInt;

	// Token: 0x04000043 RID: 67 RVA: 0x00008BD0 File Offset: 0x000071D0
	internal static XMVECTORF32 g_UByteMax;

	// Token: 0x04000044 RID: 68 RVA: 0x00008380 File Offset: 0x00006980
	internal static XMVECTORF32 g_XMCosCoefficients1;

	// Token: 0x04000045 RID: 69 RVA: 0x000088D0 File Offset: 0x00006ED0
	internal static XMVECTORU32 g_XMFlipZ;

	// Token: 0x04000046 RID: 70 RVA: 0x00008BF0 File Offset: 0x000071F0
	internal static XMVECTORF32 g_ByteMax;

	// Token: 0x04000047 RID: 71 RVA: 0x00008910 File Offset: 0x00006F10
	internal static XMVECTORU32 g_XMFlipYW;

	// Token: 0x04000048 RID: 72 RVA: 0x00008B00 File Offset: 0x00007100
	internal static XMVECTORF32 g_XMExpEst7;

	// Token: 0x04000049 RID: 73 RVA: 0x00008AB0 File Offset: 0x000070B0
	internal static XMVECTORF32 g_XMExpEst2;

	// Token: 0x0400004A RID: 74 RVA: 0x00008970 File Offset: 0x00006F70
	internal static XMVECTORU32 g_XMMaskByte4;

	// Token: 0x0400004B RID: 75 RVA: 0x00008A10 File Offset: 0x00007010
	internal static XMVECTORI32 g_XMExponentBias;

	// Token: 0x0400004C RID: 76 RVA: 0x000083F0 File Offset: 0x000069F0
	internal static XMVECTORF32 g_XMATanCoefficients1;

	// Token: 0x0400004D RID: 77 RVA: 0x00008C10 File Offset: 0x00007210
	internal static XMVECTORF32 g_ShortMax;

	// Token: 0x0400004E RID: 78 RVA: 0x00008670 File Offset: 0x00006C70
	internal static XMVECTORI32 g_XMQNaN;

	// Token: 0x0400004F RID: 79 RVA: 0x00008B50 File Offset: 0x00007150
	internal static XMVECTORF32 g_XMLogEst4;

	// Token: 0x04000050 RID: 80 RVA: 0x000089F0 File Offset: 0x00006FF0
	internal static XMVECTORF32 g_XMsrgbA;

	// Token: 0x04000051 RID: 81 RVA: 0x00008890 File Offset: 0x00006E90
	internal static XMVECTORU32 g_XMSelect1011;

	// Token: 0x04000052 RID: 82 RVA: 0x00008C00 File Offset: 0x00007200
	internal static XMVECTORF32 g_ShortMin;

	// Token: 0x04000053 RID: 83 RVA: 0x00008630 File Offset: 0x00006C30
	internal static XMVECTORF32 g_XMTwoPi;

	// Token: 0x04000054 RID: 84 RVA: 0x00008AD0 File Offset: 0x000070D0
	internal static XMVECTORF32 g_XMExpEst4;

	// Token: 0x04000055 RID: 85 RVA: 0x000084E0 File Offset: 0x00006AE0
	internal static XMVECTORU32 g_XMNegate3;

	// Token: 0x04000056 RID: 86 RVA: 0x00008750 File Offset: 0x00006D50
	internal static XMVECTORU32 g_XMMaskX16Y16;

	// Token: 0x04000057 RID: 87 RVA: 0x00008610 File Offset: 0x00006C10
	internal static XMVECTORF32 g_XMPi;

	// Token: 0x04000058 RID: 88 RVA: 0x00008450 File Offset: 0x00006A50
	internal static XMVECTORF32 g_XMIdentityR0;

	// Token: 0x04000059 RID: 89 RVA: 0x000088A0 File Offset: 0x00006EA0
	internal static XMVECTORF32 g_XMFixupY16;

	// Token: 0x0400005A RID: 90 RVA: 0x00008AC0 File Offset: 0x000070C0
	internal static XMVECTORF32 g_XMExpEst3;

	// Token: 0x0400005B RID: 91 RVA: 0x00008A80 File Offset: 0x00007080
	internal static XMVECTORU32 g_XMBinNeg150;

	// Token: 0x0400005C RID: 92 RVA: 0x00008AA0 File Offset: 0x000070A0
	internal static XMVECTORF32 g_XMExpEst1;

	// Token: 0x0400005D RID: 93 RVA: 0x00008940 File Offset: 0x00006F40
	internal static XMVECTORF32 g_XMAddUDec4;

	// Token: 0x0400005E RID: 94 RVA: 0x00008A30 File Offset: 0x00007030
	internal static XMVECTORI32 g_XMNumTrailing;

	// Token: 0x0400005F RID: 95 RVA: 0x00008B20 File Offset: 0x00007120
	internal static XMVECTORF32 g_XMLogEst1;

	// Token: 0x04000060 RID: 96 RVA: 0x000084B0 File Offset: 0x00006AB0
	internal static XMVECTORF32 g_XMNegIdentityR2;

	// Token: 0x04000061 RID: 97 RVA: 0x00008620 File Offset: 0x00006C20
	internal static XMVECTORF32 g_XMReciprocalPi;

	// Token: 0x04000062 RID: 98 RVA: 0x00008800 File Offset: 0x00006E00
	internal static XMVECTORF32 g_XMNegateY;

	// Token: 0x04000063 RID: 99 RVA: 0x00008BA0 File Offset: 0x000071A0
	internal static XMVECTORF32 g_XMInvLgE;

	// Token: 0x04000064 RID: 100 RVA: 0x00008820 File Offset: 0x00006E20
	internal static XMVECTORF32 g_XMNegateW;

	// Token: 0x04000065 RID: 101 RVA: 0x000088E0 File Offset: 0x00006EE0
	internal static XMVECTORU32 g_XMFlipW;

	// Token: 0x04000066 RID: 102 RVA: 0x000088F0 File Offset: 0x00006EF0
	internal static XMVECTORU32 g_XMFlipYZ;

	// Token: 0x04000067 RID: 103 RVA: 0x000083A0 File Offset: 0x000069A0
	internal static XMVECTORF32 g_XMTanCoefficients1;

	// Token: 0x04000068 RID: 104 RVA: 0x00008C20 File Offset: 0x00007220
	internal static XMVECTORF32 g_UShortMax;

	// Token: 0x04000069 RID: 105 RVA: 0x00008490 File Offset: 0x00006A90
	internal static XMVECTORF32 g_XMNegIdentityR0;

	// Token: 0x0400006A RID: 106 RVA: 0x000089A0 File Offset: 0x00006FA0
	internal static XMVECTORF32 g_XMFixUnsigned;

	// Token: 0x0400006B RID: 107 RVA: 0x000086E0 File Offset: 0x00006CE0
	internal static XMVECTORU32 g_XMFlipA8R8G8B8;

	// Token: 0x0400006C RID: 108 RVA: 0x000087F0 File Offset: 0x00006DF0
	internal static XMVECTORF32 g_XMNegateX;

	// Token: 0x0400006D RID: 109 RVA: 0x00008A00 File Offset: 0x00007000
	internal static XMVECTORF32 g_XMsrgbA1;

	// Token: 0x0400006E RID: 110 RVA: 0x00008A90 File Offset: 0x00007090
	internal static XMVECTORI32 g_XM253;

	// Token: 0x0400006F RID: 111 RVA: 0x00008590 File Offset: 0x00006B90
	internal static XMVECTORF32 g_XMFour;

	// Token: 0x04000070 RID: 112 RVA: 0x00008470 File Offset: 0x00006A70
	internal static XMVECTORF32 g_XMIdentityR2;

	// Token: 0x04000071 RID: 113 RVA: 0x00008680 File Offset: 0x00006C80
	internal static XMVECTORI32 g_XMQNaNTest;

	// Token: 0x04000072 RID: 114 RVA: 0x000085D0 File Offset: 0x00006BD0
	internal static XMVECTORF32 g_XMNegativeOneHalf;

	// Token: 0x04000073 RID: 115 RVA: 0x00008880 File Offset: 0x00006E80
	internal static XMVECTORU32 g_XMSelect1110;

	// Token: 0x04000074 RID: 116 RVA: 0x00008690 File Offset: 0x00006C90
	internal static XMVECTORI32 g_XMAbsMask;

	// Token: 0x04000075 RID: 117 RVA: 0x00008B40 File Offset: 0x00007140
	internal static XMVECTORF32 g_XMLogEst3;

	// Token: 0x04000076 RID: 118 RVA: 0x000087C0 File Offset: 0x00006DC0
	internal static XMVECTORF32 g_XMNormalizeX16Y16Z16W16;

	// Token: 0x04000077 RID: 119 RVA: 0x00008350 File Offset: 0x00006950
	internal static XMVECTORF32 g_XMSinCoefficients0;

	// Token: 0x04000078 RID: 120 RVA: 0x00008400 File Offset: 0x00006A00
	internal static XMVECTORF32 g_XMATanEstCoefficients0;

	// Token: 0x04000079 RID: 121 RVA: 0x000084D0 File Offset: 0x00006AD0
	internal static XMVECTORU32 g_XMNegativeZero;

	// Token: 0x0400007A RID: 122 RVA: 0x00008760 File Offset: 0x00006D60
	internal static XMVECTORI32 g_XMFlipX16Y16;

	// Token: 0x0400007B RID: 123 RVA: 0x00008560 File Offset: 0x00006B60
	internal static XMVECTORF32 g_XMOne3;

	// Token: 0x0400007C RID: 124 RVA: 0x00008650 File Offset: 0x00006C50
	internal static XMVECTORF32 g_XMEpsilon;

	// Token: 0x0400007D RID: 125 RVA: 0x00008B90 File Offset: 0x00007190
	internal static XMVECTORF32 g_XMLgE;

	// Token: 0x0400007E RID: 126 RVA: 0x00008B60 File Offset: 0x00007160
	internal static XMVECTORF32 g_XMLogEst5;

	// Token: 0x0400007F RID: 127 RVA: 0x00008440 File Offset: 0x00006A40
	internal static XMVECTORF32 g_XMPiConstants0;

	// Token: 0x04000080 RID: 128 RVA: 0x00008720 File Offset: 0x00006D20
	internal static XMVECTORU32 g_XMFlipA2B10G10R10;

	// Token: 0x04000081 RID: 129 RVA: 0x000086C0 File Offset: 0x00006CC0
	internal static XMVECTORU32 g_XMNegOneMask;

	// Token: 0x04000082 RID: 130 RVA: 0x00008510 File Offset: 0x00006B10
	internal static XMVECTORU32 g_XMMaskX;

	// Token: 0x04000083 RID: 131 RVA: 0x00008740 File Offset: 0x00006D40
	internal static XMVECTORF32 g_XMNormalizeA2B10G10R10;

	// Token: 0x04000084 RID: 132 RVA: 0x000083B0 File Offset: 0x000069B0
	internal static XMVECTORF32 g_XMTanCoefficients2;

	// Token: 0x04000085 RID: 133 RVA: 0x00008360 File Offset: 0x00006960
	internal static XMVECTORF32 g_XMSinCoefficients1;

	// Token: 0x04000086 RID: 134 RVA: 0x00008860 File Offset: 0x00006E60
	internal static XMVECTORU32 g_XMSelect1000;

	// Token: 0x04000087 RID: 135 RVA: 0x00008B30 File Offset: 0x00007130
	internal static XMVECTORF32 g_XMLogEst2;

	// Token: 0x04000088 RID: 136 RVA: 0x00008B10 File Offset: 0x00007110
	internal static XMVECTORF32 g_XMLogEst0;

	// Token: 0x04000089 RID: 137 RVA: 0x00008500 File Offset: 0x00006B00
	internal static XMVECTORU32 g_XMMask3;

	// Token: 0x0400008A RID: 138 RVA: 0x00008AF0 File Offset: 0x000070F0
	internal static XMVECTORF32 g_XMExpEst6;

	// Token: 0x0400008B RID: 139 RVA: 0x00008900 File Offset: 0x00006F00
	internal static XMVECTORU32 g_XMFlipZW;

	// Token: 0x0400008C RID: 140 RVA: 0x000085B0 File Offset: 0x00006BB0
	internal static XMVECTORF32 g_XMNegativeOne;

	// Token: 0x0400008D RID: 141 RVA: 0x00008920 File Offset: 0x00006F20
	internal static XMVECTORI32 g_XMMaskDec4;

	// Token: 0x0400008E RID: 142 RVA: 0x00008520 File Offset: 0x00006B20
	internal static XMVECTORU32 g_XMMaskY;

	// Token: 0x0400008F RID: 143 RVA: 0x00008770 File Offset: 0x00006D70
	internal static XMVECTORF32 g_XMFixX16Y16;

	// Token: 0x04000090 RID: 144 RVA: 0x000086F0 File Offset: 0x00006CF0
	internal static XMVECTORF32 g_XMFixAA8R8G8B8;

	// Token: 0x04000091 RID: 145 RVA: 0x000086B0 File Offset: 0x00006CB0
	internal static XMVECTORI32 g_XMFltMax;

	// Token: 0x04000092 RID: 146 RVA: 0x000083E0 File Offset: 0x000069E0
	internal static XMVECTORF32 g_XMATanCoefficients0;

	// Token: 0x04000093 RID: 147 RVA: 0x00008840 File Offset: 0x00006E40
	internal static XMVECTORU32 g_XMSelect1010;

	// Token: 0x04000094 RID: 148 RVA: 0x000089C0 File Offset: 0x00006FC0
	internal static XMVECTORF32 g_XMMaxUInt;

	// Token: 0x04000095 RID: 149 RVA: 0x000087B0 File Offset: 0x00006DB0
	internal static XMVECTORF32 g_XMFixX16Y16Z16W16;

	// Token: 0x04000096 RID: 150 RVA: 0x000087D0 File Offset: 0x00006DD0
	internal static XMVECTORF32 g_XMNoFraction;

	// Token: 0x04000097 RID: 151 RVA: 0x00008850 File Offset: 0x00006E50
	internal static XMVECTORI32 g_XMOneHalfMinusEpsilon;

	// Token: 0x04000098 RID: 152 RVA: 0x00008980 File Offset: 0x00006F80
	internal static XMVECTORI32 g_XMXorByte4;

	// Token: 0x04000099 RID: 153 RVA: 0x00008480 File Offset: 0x00006A80
	internal static XMVECTORF32 g_XMIdentityR3;

	// Token: 0x0400009A RID: 154 RVA: 0x000089D0 File Offset: 0x00006FD0
	internal static XMVECTORF32 g_XMUnsignedFix;

	// Token: 0x0400009B RID: 155 RVA: 0x00008BE0 File Offset: 0x000071E0
	internal static XMVECTORF32 g_ByteMin;

	// Token: 0x0400009C RID: 156 RVA: 0x00008A50 File Offset: 0x00007050
	internal static XMVECTORU32 g_XMNegInfinity;

	// Token: 0x0400009D RID: 157 RVA: 0x000085C0 File Offset: 0x00006BC0
	internal static XMVECTORF32 g_XMOneHalf;

	// Token: 0x0400009E RID: 158 RVA: 0x00008640 File Offset: 0x00006C40
	internal static XMVECTORF32 g_XMReciprocalTwoPi;

	// Token: 0x0400009F RID: 159 RVA: 0x00008960 File Offset: 0x00006F60
	internal static XMVECTORF32 g_XMMulDec4;

	// Token: 0x040000A0 RID: 160 RVA: 0x000089E0 File Offset: 0x00006FE0
	internal static XMVECTORF32 g_XMsrgbScale;

	// Token: 0x040000A1 RID: 161 RVA: 0x000084F0 File Offset: 0x00006AF0
	internal static XMVECTORU32 g_XMMaskXY;

	// Token: 0x040000A2 RID: 162 RVA: 0x000086D0 File Offset: 0x00006CD0
	internal static XMVECTORU32 g_XMMaskA8R8G8B8;

	// Token: 0x040000A3 RID: 163 RVA: 0x00008B70 File Offset: 0x00007170
	internal static XMVECTORF32 g_XMLogEst6;

	// Token: 0x040000A4 RID: 164 RVA: 0x00008370 File Offset: 0x00006970
	internal static XMVECTORF32 g_XMCosCoefficients0;

	// Token: 0x040000A5 RID: 165 RVA: 0x000085E0 File Offset: 0x00006BE0
	internal static XMVECTORF32 g_XMNegativeTwoPi;

	// Token: 0x040000A6 RID: 166 RVA: 0x00008A20 File Offset: 0x00007020
	internal static XMVECTORI32 g_XMSubnormalExponent;

	// Token: 0x040000A7 RID: 167 RVA: 0x00008550 File Offset: 0x00006B50
	internal static XMVECTORF32 g_XMOne;

	// Token: 0x040000A8 RID: 168 RVA: 0x00008A70 File Offset: 0x00007070
	internal static XMVECTORI32 g_XMBin128;

	// Token: 0x040000A9 RID: 169 RVA: 0x00008580 File Offset: 0x00006B80
	internal static XMVECTORF32 g_XMTwo;

	// Token: 0x040000AA RID: 170 RVA: 0x000085F0 File Offset: 0x00006BF0
	internal static XMVECTORF32 g_XMNegativePi;

	// Token: 0x040000AB RID: 171 RVA: 0x00008410 File Offset: 0x00006A10
	internal static XMVECTORF32 g_XMATanEstCoefficients1;

	// Token: 0x040000AC RID: 172 RVA: 0x00008810 File Offset: 0x00006E10
	internal static XMVECTORF32 g_XMNegateZ;

	// Token: 0x040000AD RID: 173 RVA: 0x00008570 File Offset: 0x00006B70
	internal static XMVECTORF32 g_XMZero;

	// Token: 0x040000AE RID: 174 RVA: 0x00008540 File Offset: 0x00006B40
	internal static XMVECTORU32 g_XMMaskW;

	// Token: 0x040000AF RID: 175 RVA: 0x00008780 File Offset: 0x00006D80
	internal static XMVECTORF32 g_XMNormalizeX16Y16;

	// Token: 0x040000B0 RID: 176 RVA: 0x00008AE0 File Offset: 0x000070E0
	internal static XMVECTORF32 g_XMExpEst5;

	// Token: 0x040000B1 RID: 177 RVA: 0x000084C0 File Offset: 0x00006AC0
	internal static XMVECTORF32 g_XMNegIdentityR3;

	// Token: 0x040000B2 RID: 178 RVA: 0x00008530 File Offset: 0x00006B30
	internal static XMVECTORU32 g_XMMaskZ;

	// Token: 0x040000B3 RID: 179 RVA: 0x000088B0 File Offset: 0x00006EB0
	internal static XMVECTORF32 g_XMFixupY16W16;

	// Token: 0x040000B4 RID: 180 RVA: 0x00008430 File Offset: 0x00006A30
	internal static XMVECTORF32 g_XMArcEstCoefficients;

	// Token: 0x040000B5 RID: 181 RVA: 0x000087A0 File Offset: 0x00006DA0
	internal static XMVECTORI32 g_XMFlipX16Y16Z16W16;

	// Token: 0x040000B6 RID: 182 RVA: 0x00008600 File Offset: 0x00006C00
	internal static XMVECTORF32 g_XMHalfPi;

	// Token: 0x040000B7 RID: 183 RVA: 0x00008790 File Offset: 0x00006D90
	internal static XMVECTORU32 g_XMMaskX16Y16Z16W16;

	// Token: 0x040000B8 RID: 184 RVA: 0x00008730 File Offset: 0x00006D30
	internal static XMVECTORF32 g_XMFixAA2B10G10R10;

	// Token: 0x040000B9 RID: 185 RVA: 0x00008990 File Offset: 0x00006F90
	internal static XMVECTORF32 g_XMAddByte4;

	// Token: 0x040000BA RID: 186 RVA: 0x00008BC0 File Offset: 0x000071C0
	internal static XMVECTORF32 g_XMInvLg10;

	// Token: 0x040000BB RID: 187 RVA: 0x00008BB0 File Offset: 0x000071B0
	internal static XMVECTORF32 g_XMLg10;

	// Token: 0x040000BC RID: 188 RVA: 0x000083C0 File Offset: 0x000069C0
	internal static XMVECTORF32 g_XMArcCoefficients0;

	// Token: 0x040000BD RID: 189 RVA: 0x000085A0 File Offset: 0x00006BA0
	internal static XMVECTORF32 g_XMSix;

	// Token: 0x040000BE RID: 190 RVA: 0x000087E0 File Offset: 0x00006DE0
	internal static XMVECTORI32 g_XMMaskByte;

	// Token: 0x040000BF RID: 191 RVA: 0x00008830 File Offset: 0x00006E30
	internal static XMVECTORU32 g_XMSelect0101;

	// Token: 0x040000C0 RID: 192 RVA: 0x00008700 File Offset: 0x00006D00
	internal static XMVECTORF32 g_XMNormalizeA8R8G8B8;

	// Token: 0x040000C1 RID: 193 RVA: 0x000086A0 File Offset: 0x00006CA0
	internal static XMVECTORI32 g_XMFltMin;

	// Token: 0x040000C2 RID: 194 RVA: 0x00008870 File Offset: 0x00006E70
	internal static XMVECTORU32 g_XMSelect1100;

	// Token: 0x040000C3 RID: 195 RVA: 0x00008A40 File Offset: 0x00007040
	internal static XMVECTORI32 g_XMMinNormal;

	// Token: 0x040000C4 RID: 196 RVA: 0x000250E8 File Offset: 0x000236E8
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_alloc@std@@6B@;

	// Token: 0x040000C5 RID: 197 RVA: 0x00025490 File Offset: 0x00023A90
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_cast@std@@6B@;

	// Token: 0x040000C6 RID: 198 RVA: 0x00025158 File Offset: 0x00023758
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_array_new_length@std@@8;

	// Token: 0x040000C7 RID: 199 RVA: 0x000250D0 File Offset: 0x000236D0
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_alloc@std@@8;

	// Token: 0x040000C8 RID: 200 RVA: 0x00025240 File Offset: 0x00023840
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2runtime_error@std@@8;

	// Token: 0x040000C9 RID: 201 RVA: 0x000251D8 File Offset: 0x000237D8
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_variant_access@std@@8;

	// Token: 0x040000CA RID: 202 RVA: 0x00025170 File Offset: 0x00023770
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_array_new_length@std@@6B@;

	// Token: 0x040000CB RID: 203 RVA: 0x000252E0 File Offset: 0x000238E0
	internal static _s__RTTIClassHierarchyDescriptor ??_R3range_error@std@@8;

	// Token: 0x040000CC RID: 204 RVA: 0x00027E48 File Offset: 0x00026248
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AV_System_error@std@@@8;

	// Token: 0x040000CD RID: 205 RVA: 0x00025110 File Offset: 0x00023710
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_array_new_length@std@@8;

	// Token: 0x040000CE RID: 206 RVA: 0x00025F80 File Offset: 0x00024580
	internal static _s__CatchableType _CT??_R0?AVbad_alloc@std@@@8??0bad_alloc@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x040000CF RID: 207 RVA: 0x00027DC8 File Offset: 0x000261C8
	internal static $_TypeDescriptor$_extraBytes_29 ??_R0?AVbad_variant_access@std@@@8;

	// Token: 0x040000D0 RID: 208 RVA: 0x00025FA8 File Offset: 0x000245A8
	internal static _s__CatchableType _CT??_R0?AVexception@std@@@8??0exception@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x040000D1 RID: 209 RVA: 0x000253F8 File Offset: 0x000239F8
	internal static _s__RTTIClassHierarchyDescriptor ??_R3system_error@std@@8;

	// Token: 0x040000D2 RID: 210 RVA: 0x00025FF0 File Offset: 0x000245F0
	internal static _s__ThrowInfo _TI3?AVbad_array_new_length@std@@;

	// Token: 0x040000D3 RID: 211 RVA: 0x00027D48 File Offset: 0x00026148
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x040000D4 RID: 212 RVA: 0x000271B0 File Offset: 0x000255B0
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_weak_ptr@std@@6B@;

	// Token: 0x040000D5 RID: 213 RVA: 0x000252F8 File Offset: 0x000238F8
	internal static _s__RTTICompleteObjectLocator2 ??_R4range_error@std@@6B@;

	// Token: 0x040000D6 RID: 214 RVA: 0x00027160 File Offset: 0x00025560
	internal static $ArrayType$$$BY02Q6AXXZ ??_7system_error@std@@6B@;

	// Token: 0x040000D7 RID: 215 RVA: 0x00027EC0 File Offset: 0x000262C0
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVbad_weak_ptr@std@@@8;

	// Token: 0x040000D8 RID: 216 RVA: 0x00025258 File Offset: 0x00023858
	internal static _s__RTTIClassHierarchyDescriptor ??_R3runtime_error@std@@8;

	// Token: 0x040000D9 RID: 217 RVA: 0x00027D98 File Offset: 0x00026198
	internal static $_TypeDescriptor$_extraBytes_31 ??_R0?AVbad_array_new_length@std@@@8;

	// Token: 0x040000DA RID: 218 RVA: 0x000271E8 File Offset: 0x000255E8
	internal static $ArrayType$$$BY02Q6AXXZ ??_7failure@ios_base@std@@6B@;

	// Token: 0x040000DB RID: 219 RVA: 0x00027258 File Offset: 0x00025658
	public static method __m2mep@??2@$$FYAPEAX_KPEAXU__clr_placement_new_t@?A0xab5de71f@@@Z;

	// Token: 0x040000DC RID: 220 RVA: 0x00027000 File Offset: 0x00025400
	public static method __m2mep@??0XMMATRIX@DirectX@@$$FQEAA@AEBU01@@Z;

	// Token: 0x040000DD RID: 221 RVA: 0x00027268 File Offset: 0x00025668
	public static method __m2mep@??2@$$FYAPEAX_KPEAX@Z;

	// Token: 0x040000DE RID: 222 RVA: 0x00027278 File Offset: 0x00025678
	public static method __m2mep@??0exception@std@@$$FQEAA@QEBDH@Z;

	// Token: 0x040000DF RID: 223 RVA: 0x00027028 File Offset: 0x00025428
	public static method __m2mep@??0exception@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x040000E0 RID: 224 RVA: 0x000272A8 File Offset: 0x000256A8
	public static method __m2mep@??1exception@std@@$$FUEAA@XZ;

	// Token: 0x040000E1 RID: 225 RVA: 0x00027298 File Offset: 0x00025698
	public static method __m2mep@?what@exception@std@@$$FUEBAPEBDXZ;

	// Token: 0x040000E2 RID: 226 RVA: 0x00027288 File Offset: 0x00025688
	public static method __m2mep@??_Eexception@std@@$$FUEAAPEAXI@Z;

	// Token: 0x040000E3 RID: 227 RVA: 0x000272B8 File Offset: 0x000256B8
	public static method __m2mep@??0bad_alloc@std@@$$FAEAA@QEBD@Z;

	// Token: 0x040000E4 RID: 228 RVA: 0x000272C8 File Offset: 0x000256C8
	public static method __m2mep@??_Ebad_alloc@std@@$$FUEAAPEAXI@Z;

	// Token: 0x040000E5 RID: 229 RVA: 0x000272D8 File Offset: 0x000256D8
	public static method __m2mep@??1bad_alloc@std@@$$FUEAA@XZ;

	// Token: 0x040000E6 RID: 230 RVA: 0x000272E8 File Offset: 0x000256E8
	public static method __m2mep@??0bad_array_new_length@std@@$$FQEAA@XZ;

	// Token: 0x040000E7 RID: 231 RVA: 0x000272F8 File Offset: 0x000256F8
	public static method __m2mep@??_Ebad_array_new_length@std@@$$FUEAAPEAXI@Z;

	// Token: 0x040000E8 RID: 232 RVA: 0x00027348 File Offset: 0x00025748
	public static method __m2mep@??1bad_array_new_length@std@@$$FUEAA@XZ;

	// Token: 0x040000E9 RID: 233 RVA: 0x00027308 File Offset: 0x00025708
	public static method __m2mep@??1exception_ptr@std@@$$FQEAA@XZ;

	// Token: 0x040000EA RID: 234 RVA: 0x00027068 File Offset: 0x00025468
	public static method __m2mep@??0exception_ptr@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x040000EB RID: 235 RVA: 0x00027078 File Offset: 0x00025478
	public static method __m2mep@?<MarshalCopy>@exception_ptr@std@@$$FSMXPEAV12@0@Z;

	// Token: 0x040000EC RID: 236 RVA: 0x00027088 File Offset: 0x00025488
	public static method __m2mep@?<MarshalDestroy>@exception_ptr@std@@$$FSMXPEAV12@@Z;

	// Token: 0x040000ED RID: 237 RVA: 0x00027328 File Offset: 0x00025728
	public static method __m2mep@?what@bad_variant_access@std@@$$FUEBAPEBDXZ;

	// Token: 0x040000EE RID: 238 RVA: 0x000277A8 File Offset: 0x00025BA8
	public static method __m2mep@??1bad_variant_access@std@@$$FUEAA@XZ;

	// Token: 0x040000EF RID: 239 RVA: 0x000270B0 File Offset: 0x000254B0
	public static method __m2mep@??0bad_variant_access@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x040000F0 RID: 240 RVA: 0x00027318 File Offset: 0x00025718
	public static method __m2mep@??_Ebad_variant_access@std@@$$FUEAAPEAXI@Z;

	// Token: 0x040000F1 RID: 241 RVA: 0x00027338 File Offset: 0x00025738
	public static method __m2mep@?_Throw_bad_array_new_length@std@@$$FYAXXZ;

	// Token: 0x040000F2 RID: 242 RVA: 0x000270D0 File Offset: 0x000254D0
	public static method __m2mep@??0bad_array_new_length@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x040000F3 RID: 243 RVA: 0x000270C0 File Offset: 0x000254C0
	public static method __m2mep@??0bad_alloc@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x040000F4 RID: 244 RVA: 0x00027358 File Offset: 0x00025758
	public static method __m2mep@?_Allocate@_Default_allocate_traits@std@@$$FSAPEAX_K@Z;

	// Token: 0x040000F5 RID: 245 RVA: 0x00027368 File Offset: 0x00025768
	public static method __m2mep@?_Adjust_manually_vector_aligned@std@@$$FYAXAEAPEAXAEA_K@Z;

	// Token: 0x040000F6 RID: 246 RVA: 0x00027378 File Offset: 0x00025778
	public static method __m2mep@?_Orphan_all@_Container_base0@std@@$$FQEAAXXZ;

	// Token: 0x040000F7 RID: 247 RVA: 0x00027388 File Offset: 0x00025788
	public static method __m2mep@?_Swap_proxy_and_iterators@_Container_base0@std@@$$FQEAAXAEAU12@@Z;

	// Token: 0x040000F8 RID: 248 RVA: 0x00027398 File Offset: 0x00025798
	public static method __m2mep@?_Alloc_proxy@_Container_base0@std@@$$FQEAAXAEBU_Fake_allocator@2@@Z;

	// Token: 0x040000F9 RID: 249 RVA: 0x00027218 File Offset: 0x00025618
	public static method __m2mep@??0_Iterator_base12@std@@$$FQEAA@AEBU01@@Z;

	// Token: 0x040000FA RID: 250 RVA: 0x000273A8 File Offset: 0x000257A8
	public static method __m2mep@??4_Iterator_base12@std@@$$FQEAAAEAU01@AEBU01@@Z;

	// Token: 0x040000FB RID: 251 RVA: 0x000273B8 File Offset: 0x000257B8
	public static method __m2mep@??0_Fake_proxy_ptr_impl@std@@$$FQEAA@AEBU_Fake_allocator@1@AEBU_Container_base0@1@@Z;

	// Token: 0x040000FC RID: 252 RVA: 0x000273C8 File Offset: 0x000257C8
	public static method __m2mep@?_Release@_Fake_proxy_ptr_impl@std@@$$FQEAAXXZ;

	// Token: 0x040000FD RID: 253 RVA: 0x000273D8 File Offset: 0x000257D8
	public static method __m2mep@?_Xlen_string@std@@$$FYAXXZ;

	// Token: 0x040000FE RID: 254 RVA: 0x000273F8 File Offset: 0x000257F8
	public static method __m2mep@??_Eruntime_error@std@@$$FUEAAPEAXI@Z;

	// Token: 0x040000FF RID: 255 RVA: 0x000273E8 File Offset: 0x000257E8
	public static method __m2mep@??1runtime_error@std@@$$FUEAA@XZ;

	// Token: 0x04000100 RID: 256 RVA: 0x00027408 File Offset: 0x00025808
	public static method __m2mep@??_Erange_error@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000101 RID: 257 RVA: 0x000277B8 File Offset: 0x00025BB8
	public static method __m2mep@??1range_error@std@@$$FUEAA@XZ;

	// Token: 0x04000102 RID: 258 RVA: 0x00027120 File Offset: 0x00025520
	public static method __m2mep@??0range_error@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000103 RID: 259 RVA: 0x000270F8 File Offset: 0x000254F8
	public static method __m2mep@??0runtime_error@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000104 RID: 260 RVA: 0x00027438 File Offset: 0x00025838
	public static method __m2mep@??_E_System_error@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000105 RID: 261 RVA: 0x00027418 File Offset: 0x00025818
	public static method __m2mep@??1_System_error@std@@$$FUEAA@XZ;

	// Token: 0x04000106 RID: 262 RVA: 0x00027228 File Offset: 0x00025628
	public static method __m2mep@?<MarshalCopy>@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FSMXPEAV12@0@Z;

	// Token: 0x04000107 RID: 263 RVA: 0x00027238 File Offset: 0x00025638
	public static method __m2mep@?<MarshalDestroy>@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FSMXPEAV12@@Z;

	// Token: 0x04000108 RID: 264 RVA: 0x00027448 File Offset: 0x00025848
	public static method __m2mep@??_Esystem_error@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000109 RID: 265 RVA: 0x00027428 File Offset: 0x00025828
	public static method __m2mep@??1system_error@std@@$$FUEAA@XZ;

	// Token: 0x0400010A RID: 266 RVA: 0x00027170 File Offset: 0x00025570
	public static method __m2mep@??0system_error@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400010B RID: 267 RVA: 0x00027148 File Offset: 0x00025548
	public static method __m2mep@??0_System_error@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400010C RID: 268 RVA: 0x00027458 File Offset: 0x00025858
	public static method __m2mep@??_Ebad_cast@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400010D RID: 269 RVA: 0x000277C8 File Offset: 0x00025BC8
	public static method __m2mep@??1bad_cast@std@@$$FUEAA@XZ;

	// Token: 0x0400010E RID: 270 RVA: 0x00027198 File Offset: 0x00025598
	public static method __m2mep@??0bad_cast@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400010F RID: 271 RVA: 0x00027478 File Offset: 0x00025878
	public static method __m2mep@?what@bad_weak_ptr@std@@$$FUEBAPEBDXZ;

	// Token: 0x04000110 RID: 272 RVA: 0x00027468 File Offset: 0x00025868
	public static method __m2mep@??_Ebad_weak_ptr@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000111 RID: 273 RVA: 0x000277D8 File Offset: 0x00025BD8
	public static method __m2mep@??1bad_weak_ptr@std@@$$FUEAA@XZ;

	// Token: 0x04000112 RID: 274 RVA: 0x000271C0 File Offset: 0x000255C0
	public static method __m2mep@??0bad_weak_ptr@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000113 RID: 275 RVA: 0x000271D0 File Offset: 0x000255D0
	public static method __m2mep@??0locale@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x04000114 RID: 276 RVA: 0x00027488 File Offset: 0x00025888
	public static method __m2mep@??_Efailure@ios_base@std@@$$FUEAAPEAXI@Z;

	// Token: 0x04000115 RID: 277 RVA: 0x000277E8 File Offset: 0x00025BE8
	public static method __m2mep@??1failure@ios_base@std@@$$FUEAA@XZ;

	// Token: 0x04000116 RID: 278 RVA: 0x000271F8 File Offset: 0x000255F8
	public static method __m2mep@??0failure@ios_base@std@@$$FQEAA@AEBV012@@Z;

	// Token: 0x04000117 RID: 279 RVA: 0x00027498 File Offset: 0x00025898
	public static method __m2mep@??_GStereoscopicRender@@$$FQEAAPEAXI@Z;

	// Token: 0x04000118 RID: 280 RVA: 0x00027778 File Offset: 0x00025B78
	public static method __m2mep@??1?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQEAA@XZ;

	// Token: 0x04000119 RID: 281 RVA: 0x00027748 File Offset: 0x00025B48
	public static method __m2mep@??0?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQEAA@$$QEAV01@@Z;

	// Token: 0x0400011A RID: 282 RVA: 0x00027248 File Offset: 0x00025648
	public static method __m2mep@??0?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400011B RID: 283 RVA: 0x000276F8 File Offset: 0x00025AF8
	public static method __m2mep@??1?$_Compressed_pair@V?$allocator@D@std@@V?$_String_val@U?$_Simple_types@D@std@@@2@$00@std@@$$FQEAA@XZ;

	// Token: 0x0400011C RID: 284 RVA: 0x00027648 File Offset: 0x00025A48
	public static method __m2mep@??1?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEAA@XZ;

	// Token: 0x0400011D RID: 285 RVA: 0x000274A8 File Offset: 0x000258A8
	public static method __m2mep@??$min@_K@std@@$$FYAAEB_KAEB_K0@Z;

	// Token: 0x0400011E RID: 286 RVA: 0x000274B8 File Offset: 0x000258B8
	public static method __m2mep@??$move@AEAV?$allocator@D@std@@@std@@$$FYA$$QEAV?$allocator@D@0@AEAV10@@Z;

	// Token: 0x0400011F RID: 287 RVA: 0x00027708 File Offset: 0x00025B08
	public static method __m2mep@??$?0V?$allocator@D@std@@$$V@?$_Compressed_pair@V?$allocator@D@std@@V?$_String_val@U?$_Simple_types@D@std@@@2@$00@std@@$$FQEAA@U_One_then_variadic_args_t@1@$$QEAV?$allocator@D@1@@Z;

	// Token: 0x04000120 RID: 288 RVA: 0x00027798 File Offset: 0x00025B98
	public static method __m2mep@??$_Construct@$01PEBD@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FAEAAXQEBD_K@Z;

	// Token: 0x04000121 RID: 289 RVA: 0x000274C8 File Offset: 0x000258C8
	public static method __m2mep@??1_Bxty@?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEAA@XZ;

	// Token: 0x04000122 RID: 290 RVA: 0x00027658 File Offset: 0x00025A58
	public static method __m2mep@?_Myptr@?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEBAPEBDXZ;

	// Token: 0x04000123 RID: 291 RVA: 0x000274D8 File Offset: 0x000258D8
	public static method __m2mep@?select_on_container_copy_construction@?$_Default_allocator_traits@V?$allocator@D@std@@@std@@$$FSA?AV?$allocator@D@2@AEBV32@@Z;

	// Token: 0x04000124 RID: 292 RVA: 0x00027668 File Offset: 0x00025A68
	public static method __m2mep@?_Getal@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FAEBAAEBV?$allocator@D@2@XZ;

	// Token: 0x04000125 RID: 293 RVA: 0x00027678 File Offset: 0x00025A78
	public static method __m2mep@?_Getal@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FAEAAAEAV?$allocator@D@2@XZ;

	// Token: 0x04000126 RID: 294 RVA: 0x00027758 File Offset: 0x00025B58
	public static method __m2mep@?_Tidy_deallocate@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FAEAAXXZ;

	// Token: 0x04000127 RID: 295 RVA: 0x00027718 File Offset: 0x00025B18
	public static method __m2mep@?_Take_contents@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FAEAAXAEAV12@@Z;

	// Token: 0x04000128 RID: 296 RVA: 0x000274E8 File Offset: 0x000258E8
	public static method __m2mep@?copy@?$_Char_traits@DH@std@@$$FSAPEADQEADQEBD_K@Z;

	// Token: 0x04000129 RID: 297 RVA: 0x00027208 File Offset: 0x00025608
	public static method __m2mep@??0?$allocator@D@std@@$$FQEAA@AEBV01@@Z;

	// Token: 0x0400012A RID: 298 RVA: 0x000274F8 File Offset: 0x000258F8
	public static method __m2mep@??$forward@V?$allocator@D@std@@@std@@$$FYA$$QEAV?$allocator@D@0@AEAV10@@Z;

	// Token: 0x0400012B RID: 299 RVA: 0x00027788 File Offset: 0x00025B88
	public static method __m2mep@??$_Allocate_for_capacity@$0A@@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FCAPEADAEAV?$allocator@D@1@AEA_K@Z;

	// Token: 0x0400012C RID: 300 RVA: 0x00027688 File Offset: 0x00025A88
	public static method __m2mep@??$_Construct_in_place@PEADAEBQEAD@std@@$$FYAXAEAPEADAEBQEAD@Z;

	// Token: 0x0400012D RID: 301 RVA: 0x00027508 File Offset: 0x00025908
	public static method __m2mep@??$_Unfancy@D@std@@$$FYAPEADPEAD@Z;

	// Token: 0x0400012E RID: 302 RVA: 0x00027518 File Offset: 0x00025918
	public static method __m2mep@??$addressof@V?$_String_val@U?$_Simple_types@D@std@@@std@@@std@@$$FYAPEAV?$_String_val@U?$_Simple_types@D@std@@@0@AEAV10@@Z;

	// Token: 0x0400012F RID: 303 RVA: 0x00027698 File Offset: 0x00025A98
	public static method __m2mep@??$_Construct_in_place@PEADAEAPEAD@std@@$$FYAXAEAPEAD0@Z;

	// Token: 0x04000130 RID: 304 RVA: 0x00027528 File Offset: 0x00025928
	public static method __m2mep@?_Get_first@?$_Compressed_pair@V?$allocator@D@std@@V?$_String_val@U?$_Simple_types@D@std@@@2@$00@std@@$$FQEBAAEBV?$allocator@D@2@XZ;

	// Token: 0x04000131 RID: 305 RVA: 0x00027538 File Offset: 0x00025938
	public static method __m2mep@?_Get_first@?$_Compressed_pair@V?$allocator@D@std@@V?$_String_val@U?$_Simple_types@D@std@@@2@$00@std@@$$FQEAAAEAV?$allocator@D@2@XZ;

	// Token: 0x04000132 RID: 306 RVA: 0x000276A8 File Offset: 0x00025AA8
	public static method __m2mep@?_Switch_to_buf@_Bxty@?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEAAXXZ;

	// Token: 0x04000133 RID: 307 RVA: 0x00027548 File Offset: 0x00025948
	public static method __m2mep@?_Activate_SSO_buffer@?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEAAXXZ;

	// Token: 0x04000134 RID: 308 RVA: 0x00027558 File Offset: 0x00025958
	public static method __m2mep@?_Large_mode_engaged@?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEBA_NXZ;

	// Token: 0x04000135 RID: 309 RVA: 0x000276B8 File Offset: 0x00025AB8
	public static method __m2mep@??0?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEAA@XZ;

	// Token: 0x04000136 RID: 310 RVA: 0x00027568 File Offset: 0x00025968
	public static method __m2mep@??$max@_K@std@@$$FYAAEB_KAEB_K0@Z;

	// Token: 0x04000137 RID: 311 RVA: 0x00027578 File Offset: 0x00025978
	public static method __m2mep@?_Swap_proxy_and_iterators@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FAEAAXAEAV12@@Z;

	// Token: 0x04000138 RID: 312 RVA: 0x00027588 File Offset: 0x00025988
	public static method __m2mep@?_Calculate_growth@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FCA_K_K00@Z;

	// Token: 0x04000139 RID: 313 RVA: 0x000276C8 File Offset: 0x00025AC8
	public static method __m2mep@?max_size@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQEBA_KXZ;

	// Token: 0x0400013A RID: 314 RVA: 0x00027728 File Offset: 0x00025B28
	public static method __m2mep@?_Deallocate_for_capacity@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FCAXAEAV?$allocator@D@2@QEAD_K@Z;

	// Token: 0x0400013B RID: 315 RVA: 0x00027598 File Offset: 0x00025998
	public static method __m2mep@?assign@?$_Narrow_char_traits@DH@std@@$$FSAXAEADAEBD@Z;

	// Token: 0x0400013C RID: 316 RVA: 0x00027768 File Offset: 0x00025B68
	public static method __m2mep@??$_Allocate_at_least_helper@V?$allocator@D@std@@@std@@$$FYAPEADAEAV?$allocator@D@0@AEA_K@Z;

	// Token: 0x0400013D RID: 317 RVA: 0x000275A8 File Offset: 0x000259A8
	public static method __m2mep@??$addressof@PEAD@std@@$$FYAPEAPEADAEAPEAD@Z;

	// Token: 0x0400013E RID: 318 RVA: 0x000275B8 File Offset: 0x000259B8
	public static method __m2mep@??$forward@AEBQEAD@std@@$$FYAAEBQEADAEBQEAD@Z;

	// Token: 0x0400013F RID: 319 RVA: 0x000275C8 File Offset: 0x000259C8
	public static method __m2mep@??$forward@AEAPEAD@std@@$$FYAAEAPEADAEAPEAD@Z;

	// Token: 0x04000140 RID: 320 RVA: 0x000275D8 File Offset: 0x000259D8
	public static method __m2mep@??$_Destroy_in_place@PEAD@std@@$$FYAXAEAPEAD@Z;

	// Token: 0x04000141 RID: 321 RVA: 0x000275E8 File Offset: 0x000259E8
	public static method __m2mep@??$_Max_limit@_J@std@@$$FYA_JXZ;

	// Token: 0x04000142 RID: 322 RVA: 0x000275F8 File Offset: 0x000259F8
	public static method __m2mep@??0_Bxty@?$_String_val@U?$_Simple_types@D@std@@@std@@$$FQEAA@XZ;

	// Token: 0x04000143 RID: 323 RVA: 0x00027608 File Offset: 0x00025A08
	public static method __m2mep@?max_size@?$_Default_allocator_traits@V?$allocator@D@std@@@std@@$$FSA_KAEBV?$allocator@D@2@@Z;

	// Token: 0x04000144 RID: 324 RVA: 0x000276D8 File Offset: 0x00025AD8
	public static method __m2mep@?deallocate@?$allocator@D@std@@$$FQEAAXQEAD_K@Z;

	// Token: 0x04000145 RID: 325 RVA: 0x00027618 File Offset: 0x00025A18
	public static method __m2mep@??$_Deallocate@$0BA@@std@@$$FYAXPEAX_K@Z;

	// Token: 0x04000146 RID: 326 RVA: 0x00027738 File Offset: 0x00025B38
	public static method __m2mep@?allocate@?$allocator@D@std@@$$FQEAAPEAD_K@Z;

	// Token: 0x04000147 RID: 327 RVA: 0x000276E8 File Offset: 0x00025AE8
	public static method __m2mep@??$_Allocate@$0BA@U_Default_allocate_traits@std@@@std@@$$FYAPEAX_K@Z;

	// Token: 0x04000148 RID: 328 RVA: 0x00027628 File Offset: 0x00025A28
	public static method __m2mep@??$_Get_size_of_n@$00@std@@$$FYA_K_K@Z;

	// Token: 0x04000149 RID: 329 RVA: 0x00027638 File Offset: 0x00025A38
	public static method __m2mep@??$_Allocate_manually_vector_aligned@U_Default_allocate_traits@std@@@std@@$$FYAPEAX_K@Z;

	// Token: 0x0400014A RID: 330 RVA: 0x00027870 File Offset: 0x00025C70
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400014B RID: 331 RVA: 0x00027880 File Offset: 0x00025C80
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400014C RID: 332 RVA: 0x00027890 File Offset: 0x00025C90
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x0400014D RID: 333 RVA: 0x000278B0 File Offset: 0x00025CB0
	public static method __m2mep@??_M@$$FYMXPEAX_K1P6MX0@Z@Z;

	// Token: 0x0400014E RID: 334 RVA: 0x000278D0 File Offset: 0x00025CD0
	public static method __m2mep@?ArrayUnwindFilter@?A0x9c83a709@@$$FYAHPEAU_EXCEPTION_POINTERS@@@Z;

	// Token: 0x0400014F RID: 335 RVA: 0x000278A0 File Offset: 0x00025CA0
	public static method __m2mep@?__ArrayUnwind@@$$FYMXPEAX_K1P6MX0@Z@Z;

	// Token: 0x04000150 RID: 336 RVA: 0x000278C0 File Offset: 0x00025CC0
	public static method __m2mep@??_M@$$FYMXPEAX_KHP6MX0@Z@Z;

	// Token: 0x04000151 RID: 337 RVA: 0x00008D30 File Offset: 0x00007330
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000152 RID: 338 RVA: 0x00008D20 File Offset: 0x00007320
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000153 RID: 339
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000154 RID: 340 RVA: 0x000082F8 File Offset: 0x000068F8
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000155 RID: 341
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000156 RID: 342 RVA: 0x00008310 File Offset: 0x00006910
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000157 RID: 343 RVA: 0x00008D40 File Offset: 0x00007340
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000158 RID: 344 RVA: 0x00008D50 File Offset: 0x00007350
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000159 RID: 345
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400015A RID: 346 RVA: 0x00027FD4 File Offset: 0x000263D4
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400015B RID: 347 RVA: 0x000278E4 File Offset: 0x00025CE4
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400015C RID: 348 RVA: 0x00027FD7 File Offset: 0x000263D7
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400015D RID: 349 RVA: 0x00027FD0 File Offset: 0x000263D0
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x0400015E RID: 350
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400015F RID: 351 RVA: 0x00027FD6 File Offset: 0x000263D6
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000160 RID: 352
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000161 RID: 353
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000162 RID: 354 RVA: 0x00027FD5 File Offset: 0x000263D5
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000163 RID: 355
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000164 RID: 356 RVA: 0x000278E0 File Offset: 0x00025CE0
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000165 RID: 357 RVA: 0x00008338 File Offset: 0x00006938
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x04000166 RID: 358 RVA: 0x00008348 File Offset: 0x00006948
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x04000167 RID: 359 RVA: 0x00008318 File Offset: 0x00006918
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000168 RID: 360 RVA: 0x000082E8 File Offset: 0x000068E8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000169 RID: 361 RVA: 0x00008328 File Offset: 0x00006928
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400016A RID: 362 RVA: 0x00008320 File Offset: 0x00006920
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400016B RID: 363 RVA: 0x00008340 File Offset: 0x00006940
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x0400016C RID: 364 RVA: 0x000082F0 File Offset: 0x000068F0
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400016D RID: 365 RVA: 0x00008330 File Offset: 0x00006930
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x0400016E RID: 366 RVA: 0x00008308 File Offset: 0x00006908
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400016F RID: 367 RVA: 0x00008300 File Offset: 0x00006900
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000170 RID: 368 RVA: 0x00027A68 File Offset: 0x00025E68
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x04000171 RID: 369 RVA: 0x000278F8 File Offset: 0x00025CF8
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x04000172 RID: 370 RVA: 0x00027908 File Offset: 0x00025D08
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x04000173 RID: 371 RVA: 0x00027918 File Offset: 0x00025D18
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x04000174 RID: 372 RVA: 0x00027928 File Offset: 0x00025D28
	public static method __m2mep@?FromGUID@<CrtImplementationDetails>@@$$FYM?AVGuid@System@@AEBU_GUID@@@Z;

	// Token: 0x04000175 RID: 373 RVA: 0x00027938 File Offset: 0x00025D38
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x04000176 RID: 374 RVA: 0x00027948 File Offset: 0x00025D48
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x04000177 RID: 375 RVA: 0x00027958 File Offset: 0x00025D58
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x04000178 RID: 376 RVA: 0x00027968 File Offset: 0x00025D68
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x04000179 RID: 377 RVA: 0x00027978 File Offset: 0x00025D78
	public static method __m2mep@?__scrt_is_safe_for_managed_code@@$$FYA_NXZ;

	// Token: 0x0400017A RID: 378 RVA: 0x00027988 File Offset: 0x00025D88
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400017B RID: 379 RVA: 0x00027998 File Offset: 0x00025D98
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400017C RID: 380 RVA: 0x000279A8 File Offset: 0x00025DA8
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400017D RID: 381 RVA: 0x000279B8 File Offset: 0x00025DB8
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400017E RID: 382 RVA: 0x000279C8 File Offset: 0x00025DC8
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400017F RID: 383 RVA: 0x000279D8 File Offset: 0x00025DD8
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000180 RID: 384 RVA: 0x00027A78 File Offset: 0x00025E78
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000181 RID: 385 RVA: 0x00027A88 File Offset: 0x00025E88
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000182 RID: 386 RVA: 0x00027A98 File Offset: 0x00025E98
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000183 RID: 387 RVA: 0x00027AA8 File Offset: 0x00025EA8
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000184 RID: 388 RVA: 0x00027AB8 File Offset: 0x00025EB8
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000185 RID: 389 RVA: 0x00027AC8 File Offset: 0x00025EC8
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000186 RID: 390 RVA: 0x00027AD8 File Offset: 0x00025ED8
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000187 RID: 391 RVA: 0x000279E8 File Offset: 0x00025DE8
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000188 RID: 392 RVA: 0x000279F8 File Offset: 0x00025DF8
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000189 RID: 393 RVA: 0x00027A08 File Offset: 0x00025E08
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400018A RID: 394 RVA: 0x00027A18 File Offset: 0x00025E18
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x0400018B RID: 395 RVA: 0x00027AE8 File Offset: 0x00025EE8
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x0400018C RID: 396 RVA: 0x00027AF8 File Offset: 0x00025EF8
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x0400018D RID: 397 RVA: 0x00027B08 File Offset: 0x00025F08
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x0400018E RID: 398 RVA: 0x00027B18 File Offset: 0x00025F18
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x0400018F RID: 399 RVA: 0x000278E8 File Offset: 0x00025CE8
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000190 RID: 400 RVA: 0x00027A28 File Offset: 0x00025E28
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x04000191 RID: 401 RVA: 0x00027A38 File Offset: 0x00025E38
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x04000192 RID: 402 RVA: 0x00027A48 File Offset: 0x00025E48
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000193 RID: 403 RVA: 0x00027A58 File Offset: 0x00025E58
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000194 RID: 404 RVA: 0x00008D60 File Offset: 0x00007360
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000195 RID: 405 RVA: 0x00008D68 File Offset: 0x00007368
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000196 RID: 406 RVA: 0x00028160 File Offset: 0x00026560
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x04000197 RID: 407 RVA: 0x00028158 File Offset: 0x00026558
	internal static ulong __exit_list_size;

	// Token: 0x04000198 RID: 408
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x04000199 RID: 409
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x0400019A RID: 410
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x0400019B RID: 411 RVA: 0x00028168 File Offset: 0x00026568
	internal unsafe static method* __onexitend_m;

	// Token: 0x0400019C RID: 412
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400019D RID: 413
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x0400019E RID: 414 RVA: 0x00027BF0 File Offset: 0x00025FF0
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AA__ZVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x0400019F RID: 415 RVA: 0x00027CA0 File Offset: 0x000260A0
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x040001A0 RID: 416 RVA: 0x00027C00 File Offset: 0x00026000
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x040001A1 RID: 417 RVA: 0x00027C10 File Offset: 0x00026010
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x040001A2 RID: 418 RVA: 0x00027C20 File Offset: 0x00026020
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x040001A3 RID: 419 RVA: 0x00027C30 File Offset: 0x00026030
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040001A4 RID: 420 RVA: 0x00027CB0 File Offset: 0x000260B0
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040001A5 RID: 421 RVA: 0x00027C40 File Offset: 0x00026040
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040001A6 RID: 422 RVA: 0x00027C50 File Offset: 0x00026050
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040001A7 RID: 423 RVA: 0x00027C60 File Offset: 0x00026060
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040001A8 RID: 424 RVA: 0x00027C70 File Offset: 0x00026070
	public static method __m2mep@?__global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040001A9 RID: 425 RVA: 0x00027C80 File Offset: 0x00026080
	public static method __m2mep@?__global_unlock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040001AA RID: 426 RVA: 0x00027CC0 File Offset: 0x000260C0
	public static method __m2mep@?__alloc_global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x040001AB RID: 427 RVA: 0x00027C90 File Offset: 0x00026090
	public static method __m2mep@?__dealloc_global_lock@?A0xdf32f105@@$$FYAXXZ;

	// Token: 0x040001AC RID: 428 RVA: 0x00027B60 File Offset: 0x00025F60
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x040001AD RID: 429 RVA: 0x00027B70 File Offset: 0x00025F70
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x040001AE RID: 430 RVA: 0x00027BB0 File Offset: 0x00025FB0
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x040001AF RID: 431 RVA: 0x00027BC0 File Offset: 0x00025FC0
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040001B0 RID: 432 RVA: 0x00027B80 File Offset: 0x00025F80
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040001B1 RID: 433 RVA: 0x00027BD0 File Offset: 0x00025FD0
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x040001B2 RID: 434 RVA: 0x00027B90 File Offset: 0x00025F90
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x040001B3 RID: 435 RVA: 0x00027BE0 File Offset: 0x00025FE0
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040001B4 RID: 436 RVA: 0x00027BA0 File Offset: 0x00025FA0
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040001B5 RID: 437 RVA: 0x00027CD0 File Offset: 0x000260D0
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x040001B6 RID: 438 RVA: 0x00027CE0 File Offset: 0x000260E0
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x040001B7 RID: 439 RVA: 0x00027D00 File Offset: 0x00026100
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x040001B8 RID: 440 RVA: 0x00027CF0 File Offset: 0x000260F0
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x040001B9 RID: 441 RVA: 0x00027D10 File Offset: 0x00026110
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x040001BA RID: 442 RVA: 0x00008CE8 File Offset: 0x000072E8
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x040001BB RID: 443 RVA: 0x000082C0 File Offset: 0x000068C0
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x040001BC RID: 444 RVA: 0x00027F70 File Offset: 0x00026370
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x040001BD RID: 445 RVA: 0x00027F78 File Offset: 0x00026378
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x040001BE RID: 446 RVA: 0x000082A8 File Offset: 0x000068A8
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x040001BF RID: 447 RVA: 0x000082B8 File Offset: 0x000068B8
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x040001C0 RID: 448 RVA: 0x000277F8 File Offset: 0x00025BF8
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x040001C1 RID: 449 RVA: 0x000082B0 File Offset: 0x000068B0
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
