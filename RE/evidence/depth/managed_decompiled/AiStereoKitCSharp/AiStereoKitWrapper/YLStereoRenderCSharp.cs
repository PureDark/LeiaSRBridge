using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using <CppImplementationDetails>;
using AiStereo;
using msclr.interop;

namespace AiStereoKitWrapper
{
	// Token: 0x0200000B RID: 11
	public class YLStereoRenderCSharp : IDisposable
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000015E4 File Offset: 0x000009E4
		public YLStereoRenderCSharp()
		{
			this.m_pStereoRenderer = <Module>.CreateStereoRenderer();
			GC.KeepAlive(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00001AB4 File Offset: 0x00000EB4
		private void ~YLStereoRenderCSharp()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00001A38 File Offset: 0x00000E38
		private void !YLStereoRenderCSharp()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00001608 File Offset: 0x00000A08
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool InitGraphicsDevice(IntPtr device, IntPtr deviceContext)
		{
			IStereoRenderer* pStereoRenderer = this.m_pStereoRenderer;
			if (pStereoRenderer != null)
			{
				IStereoRenderer* ptr = pStereoRenderer;
				long num = *(*(long*)pStereoRenderer + 8L);
				void* ptr2 = (void*)deviceContext;
				void* ptr3 = (void*)device;
				bool result = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.Void*), ptr, ptr3, ptr2, num);
				GC.KeepAlive(this);
				return result;
			}
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00001650 File Offset: 0x00000A50
		public unsafe void SetFrameSize(int width, int height)
		{
			IStereoRenderer* pStereoRenderer = this.m_pStereoRenderer;
			if (pStereoRenderer != null)
			{
				long num = *(*(long*)pStereoRenderer + 16L);
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Int32), pStereoRenderer, width, height, num);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000016CC File Offset: 0x00000ACC
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool Render(IntPtr sourceShaderResourceView, IntPtr renderTargetView, int arrangement)
		{
			IStereoRenderer* pStereoRenderer = this.m_pStereoRenderer;
			if (pStereoRenderer != null)
			{
				IStereoRenderer* ptr = pStereoRenderer;
				long num = *(*(long*)pStereoRenderer + 32L);
				void* ptr2 = (void*)renderTargetView;
				void* ptr3 = (void*)sourceShaderResourceView;
				bool result = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.Void*,System.Int32), ptr, ptr3, ptr2, arrangement, num);
				GC.KeepAlive(this);
				return result;
			}
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00001680 File Offset: 0x00000A80
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool Render(IntPtr sourceShaderResourceView, IntPtr renderTargetView, int arrangement, int flipY)
		{
			IStereoRenderer* pStereoRenderer = this.m_pStereoRenderer;
			if (pStereoRenderer != null)
			{
				IStereoRenderer* ptr = pStereoRenderer;
				long num = *(*(long*)pStereoRenderer + 24L);
				void* ptr2 = (void*)renderTargetView;
				void* ptr3 = (void*)sourceShaderResourceView;
				bool result = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.Void*,System.Int32,System.Int32), ptr, ptr3, ptr2, arrangement, flipY, num);
				GC.KeepAlive(this);
				return result;
			}
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00001718 File Offset: 0x00000B18
		public unsafe void SetParax(float paraxPercent, float zeroPanelPercent)
		{
			IStereoRenderer* pStereoRenderer = this.m_pStereoRenderer;
			if (pStereoRenderer != null)
			{
				long num = *(*(long*)pStereoRenderer + 40L);
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single,System.Single), pStereoRenderer, paraxPercent, zeroPanelPercent, num);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00001748 File Offset: 0x00000B48
		public unsafe void Release()
		{
			IStereoRenderer* pStereoRenderer = this.m_pStereoRenderer;
			if (pStereoRenderer != null)
			{
				IStereoRenderer* ptr = pStereoRenderer;
				long num = *(*(long*)ptr);
				object obj = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), ptr, 1, num);
				this.m_pStereoRenderer = null;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00001B50 File Offset: 0x00000F50
		public unsafe static void LoadStereoEngineCSharp(string cacheDir, string device)
		{
			marshal_context marshal_context = null;
			marshal_context marshal_context2 = new marshal_context();
			try
			{
				marshal_context = marshal_context2;
				sbyte* ptr = null;
				sbyte* ptr2 = (sbyte*)(&<Module>.??_C@_04HAPDLBFF@auto@);
				if (cacheDir != null)
				{
					ptr = marshal_context.marshal_as<char\u0020const\u0020*,System::String>(cacheDir);
				}
				if (device != null)
				{
					ptr2 = marshal_context.marshal_as<char\u0020const\u0020*,System::String>(device);
				}
				<Module>.LoadStereoEngine(ptr, ptr2);
			}
			catch
			{
				((IDisposable)marshal_context).Dispose();
				throw;
			}
			((IDisposable)marshal_context).Dispose();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00001778 File Offset: 0x00000B78
		public static void UnloadStereoEngineCSharp()
		{
			<Module>.UnloadStereoEngine();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000178C File Offset: 0x00000B8C
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool IsValidLicenseCSharp(StringBuilder hardware)
		{
			$ArrayType$$$BY0BAA@D $ArrayType$$$BY0BAA@D;
			initblk(ref $ArrayType$$$BY0BAA@D, 0, 256L);
			bool result = <Module>.IsValidLicense((sbyte*)(&$ArrayType$$$BY0BAA@D), 256UL) != null;
			hardware.Append(new string((sbyte*)(&$ArrayType$$$BY0BAA@D)));
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00001AF0 File Offset: 0x00000EF0
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.Release();
				GC.SuppressFinalize(this);
			}
			else
			{
				try
				{
					this.!YLStereoRenderCSharp();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00001BC4 File Offset: 0x00000FC4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00001B3C File Offset: 0x00000F3C
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040000CD RID: 205
		private unsafe IStereoRenderer* m_pStereoRenderer;
	}
}
