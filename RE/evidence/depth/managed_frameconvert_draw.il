// Token: 0x06000359 RID: 857 RVA: 0x0001B004 File Offset: 0x00019204
.method public hidebysig 
	instance void DrawFrame (
		class [SharpDX.Direct3D11]SharpDX.Direct3D11.Device device,
		class [SharpDX.Direct3D11]SharpDX.Direct3D11.ShaderResourceView shaderResourceView,
		valuetype AstraDepth3D.EventBus.LocalEnumeration/InterWaveType format
	) cil managed 
{
	// Header Size: 12 bytes
	// Code Size: 132 (0x84) bytes
	// LocalVarSig Token: 0x110000EA RID: 234
	.maxstack 5
	.locals init (
		[0] valuetype [mscorlib]System.Nullable`1<bool> 'ret',
		[1] valuetype [mscorlib]System.Nullable`1<bool>
	)

	/* (31,13)-(31,35) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
	/* 0x00019210 02           */ IL_0000: ldarg.0
	/* 0x00019211 7BEC020004   */ IL_0001: ldfld     object AstraDepth3D.CaptureSampleCore.FrameConvert::g_lock
	/* 0x00019216 28E202000A   */ IL_0006: call      void [mscorlib]System.Threading.Monitor::Enter(object)
	.try
	{
		/* (34,17)-(34,59) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x0001921B 05           */ IL_000B: ldarg.3
		/* 0x0001921C 17           */ IL_000C: ldc.i4.1
		/* 0x0001921D 3321         */ IL_000D: bne.un.s  IL_0030

		/* (36,21)-(36,99) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x0001921F 02           */ IL_000F: ldarg.0
		/* 0x00019220 7BEA020004   */ IL_0010: ldfld     class AstraDepth3D.BOEStereoRender.FrameCopy AstraDepth3D.CaptureSampleCore.FrameConvert::frameCopy
		/* 0x00019225 25           */ IL_0015: dup
		/* 0x00019226 2D03         */ IL_0016: brtrue.s  IL_001B

		/* 0x00019228 26           */ IL_0018: pop
		/* 0x00019229 DE68         */ IL_0019: leave.s   IL_0083

		/* 0x0001922B 03           */ IL_001B: ldarg.1
		/* 0x0001922C 04           */ IL_001C: ldarg.2
		/* 0x0001922D 02           */ IL_001D: ldarg.0
		/* 0x0001922E 7BE7020004   */ IL_001E: ldfld     class [SharpDX.Direct3D11]SharpDX.Direct3D11.RenderTargetView AstraDepth3D.CaptureSampleCore.FrameConvert::stereoTargetView
		/* 0x00019233 02           */ IL_0023: ldarg.0
		/* 0x00019234 7BEB020004   */ IL_0024: ldfld     valuetype [SharpDX]SharpDX.Mathematics.Interop.RawViewportF AstraDepth3D.CaptureSampleCore.FrameConvert::viewportF
		/* 0x00019239 28A8030006   */ IL_0029: call      instance void AstraDepth3D.BOEStereoRender.FrameCopy::DrawFrame(class [SharpDX.Direct3D11]SharpDX.Direct3D11.Device, class [SharpDX.Direct3D11]SharpDX.Direct3D11.ShaderResourceView, class [SharpDX.Direct3D11]SharpDX.Direct3D11.RenderTargetView, valuetype [SharpDX]SharpDX.Mathematics.Interop.RawViewportF)
		/* (hidden)-(hidden) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x0001923E DE53         */ IL_002E: leave.s   IL_0083

		/* (41,21)-(41,127) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x00019240 02           */ IL_0030: ldarg.0
		/* 0x00019241 7BE9020004   */ IL_0031: ldfld     class [AiStereoKitCSharp]AiStereoKitWrapper.YLStereoRenderCSharp AstraDepth3D.CaptureSampleCore.FrameConvert::stereoRenderCSharp
		/* 0x00019246 25           */ IL_0036: dup
		/* 0x00019247 2D0C         */ IL_0037: brtrue.s  IL_0045

		/* 0x00019249 26           */ IL_0039: pop
		/* 0x0001924A 1201         */ IL_003A: ldloca.s  V_1
		/* 0x0001924C FE150700001B */ IL_003C: initobj   valuetype [mscorlib]System.Nullable`1<bool>
		/* 0x00019252 07           */ IL_0042: ldloc.1
		/* 0x00019253 2B1C         */ IL_0043: br.s      IL_0061

		/* 0x00019255 04           */ IL_0045: ldarg.2
		/* 0x00019256 6FBA01000A   */ IL_0046: callvirt  instance native int [SharpDX]SharpDX.CppObject::get_NativePointer()
		/* 0x0001925B 02           */ IL_004B: ldarg.0
		/* 0x0001925C 7BE7020004   */ IL_004C: ldfld     class [SharpDX.Direct3D11]SharpDX.Direct3D11.RenderTargetView AstraDepth3D.CaptureSampleCore.FrameConvert::stereoTargetView
		/* 0x00019261 6FBA01000A   */ IL_0051: callvirt  instance native int [SharpDX]SharpDX.CppObject::get_NativePointer()
		/* 0x00019266 16           */ IL_0056: ldc.i4.0
		/* 0x00019267 287F03000A   */ IL_0057: call      instance bool [AiStereoKitCSharp]AiStereoKitWrapper.YLStereoRenderCSharp::Render(native int, native int, int32)
		/* 0x0001926C 733700000A   */ IL_005C: newobj    instance void valuetype [mscorlib]System.Nullable`1<bool>::.ctor(!0)

		/* 0x00019271 0A           */ IL_0061: stloc.0
		/* (42,21)-(42,37) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x00019272 1200         */ IL_0062: ldloca.s  'ret'
		/* 0x00019274 280001000A   */ IL_0064: call      instance !0 valuetype [mscorlib]System.Nullable`1<bool>::GetValueOrDefault()
		/* 0x00019279 2D0A         */ IL_0069: brtrue.s  IL_0075

		/* (44,25)-(44,83) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x0001927B 72F0800070   */ IL_006B: ldstr     "Error:stereoRender render Failed!"
		/* 0x00019280 284E020006   */ IL_0070: call      void AstraDepth3D.WindowHelper.NLogService::WriteLog(string)

		/* (47,13)-(47,14) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x00019285 DE0C         */ IL_0075: leave.s   IL_0083
	} // end .try
	finally
	{
		/* (47,23)-(47,44) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x00019287 02           */ IL_0077: ldarg.0
		/* 0x00019288 7BEC020004   */ IL_0078: ldfld     object AstraDepth3D.CaptureSampleCore.FrameConvert::g_lock
		/* 0x0001928D 286A01000A   */ IL_007D: call      void [mscorlib]System.Threading.Monitor::Exit(object)
		/* (47,45)-(47,46) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
		/* 0x00019292 DC           */ IL_0082: endfinally
	} // end handler

	/* (48,9)-(48,10) J:\Project\VSProjects\AiStereoKit\StereoFloatingWindow\StereoFloatingWindow\CaptureSampleCore\FrameConvert.cs */
	/* 0x00019293 2A           */ IL_0083: ret
} // end of method FrameConvert::DrawFrame
