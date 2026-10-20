// Token: 0x060000A9 RID: 169 RVA: 0x0000168C File Offset: 0x00000A8C
.method public hidebysig static 
	int32 DoInterleave (
		float32 x,
		float32 y,
		float32 z,
		native int context,
		native int renderTargetView,
		native int inShaderResourceView,
		int32 width,
		int32 height,
		int32 inDXGI_FORMAT,
		valuetype BOEInterleaving.InterMode inter_mode,
		int32 sRGBFix
	) cil managed 
{
	// Header Size: 12 bytes
	// Code Size: 40 (0x28) bytes
	.maxstack 11

	/* (106,0)-(106,0) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\BOEInterleaving\Interleaving.cpp */
	/* 0x00000A98 02           */ IL_0000: ldarg.0
	/* 0x00000A99 03           */ IL_0001: ldarg.1
	/* 0x00000A9A 04           */ IL_0002: ldarg.2
	/* 0x00000A9B 0F03         */ IL_0003: ldarga.s  context
	/* 0x00000A9D 281200000A   */ IL_0005: call      instance void* [mscorlib]System.IntPtr::ToPointer()
	/* 0x00000AA2 0F04         */ IL_000A: ldarga.s  renderTargetView
	/* 0x00000AA4 281200000A   */ IL_000C: call      instance void* [mscorlib]System.IntPtr::ToPointer()
	/* 0x00000AA9 0F05         */ IL_0011: ldarga.s  inShaderResourceView
	/* 0x00000AAB 281200000A   */ IL_0013: call      instance void* [mscorlib]System.IntPtr::ToPointer()
	/* 0x00000AB0 0E06         */ IL_0018: ldarg.s   width
	/* 0x00000AB2 0E07         */ IL_001A: ldarg.s   height
	/* 0x00000AB4 0E08         */ IL_001C: ldarg.s   inDXGI_FORMAT
	/* 0x00000AB6 0E09         */ IL_001E: ldarg.s   inter_mode
	/* 0x00000AB8 0E0A         */ IL_0020: ldarg.s   sRGBFix
	/* 0x00000ABA 2874000006   */ IL_0022: call      int32 modopt([mscorlib]System.Runtime.CompilerServices.CallConvCdecl)  '<Module>'::BOE_Interleaving_doInterleave(float32 modopt([mscorlib]System.Runtime.CompilerServices.IsConst) , float32 modopt([mscorlib]System.Runtime.CompilerServices.IsConst) , float32 modopt([mscorlib]System.Runtime.CompilerServices.IsConst) , void*, void*, void*, int32, int32, int32, valuetype InterMode, int32)
	/* 0x00000ABF 2A           */ IL_0027: ret
} // end of method Interleaving::DoInterleave
