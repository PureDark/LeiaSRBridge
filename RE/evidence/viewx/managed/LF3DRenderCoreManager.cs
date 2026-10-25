using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace ManageLF3DRenderCore
{
	// Token: 0x02000006 RID: 6
	public class LF3DRenderCoreManager : IDisposable
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00001108 File Offset: 0x00000508
		public unsafe static void LoadLicense(string license, int length)
		{
			IntPtr hglobal = Marshal.StringToHGlobalAnsi(license);
			<Module>.LoadLicenseFromBuffer((sbyte*)hglobal.ToPointer(), length);
			Marshal.FreeHGlobal(hglobal);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000010E0 File Offset: 0x000004E0
		public unsafe static void LoadLicense(string path)
		{
			IntPtr hglobal = Marshal.StringToHGlobalAnsi(path);
			<Module>.LoadLicenseFromFile((sbyte*)hglobal.ToPointer());
			Marshal.FreeHGlobal(hglobal);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00001154 File Offset: 0x00000554
		public unsafe LF3DRenderCoreManager(IntPtr d3dDevice)
		{
			this.m_pImp = <Module>.CreateDX11Render((void*)d3dDevice);
			GC.KeepAlive(this);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00001130 File Offset: 0x00000530
		public LF3DRenderCoreManager()
		{
			this.m_pImp = <Module>.CreateGLCoreRender();
			GC.KeepAlive(this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000013B8 File Offset: 0x000007B8
		private void ~LF3DRenderCoreManager()
		{
			this.!LF3DRenderCoreManager();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00001180 File Offset: 0x00000580
		private unsafe void !LF3DRenderCoreManager()
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			if (pImp != null)
			{
				ILF3DBaseRender* ptr = pImp;
				object obj = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), ptr, 1, *(*(long*)ptr));
				this.m_pImp = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000011B4 File Offset: 0x000005B4
		public unsafe void SharpInitialize(string compFile)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAnsi(compFile);
			ILF3DBaseRender* pImp = this.m_pImp;
			long num = *(*(long*)pImp + 8L);
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), pImp, intPtr.ToPointer(), num);
			GC.KeepAlive(this);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000011EC File Offset: 0x000005EC
		public unsafe void SharpSetTexture(IntPtr textureHandle, int w, int h)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			long num = *(*(long*)pImp + 16L);
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.Int32,System.Int32), pImp, (void*)textureHandle, w, h, num);
			GC.KeepAlive(this);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00001220 File Offset: 0x00000620
		public unsafe void SharpSetFormat(int h, int v)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Int32), pImp, h, v, *(*(long*)pImp + 24L));
			GC.KeepAlive(this);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000124C File Offset: 0x0000064C
		public unsafe void SharpLoadDeviceConfigFromFile(string path, [MarshalAs(UnmanagedType.U1)] bool viewpointOffset, [MarshalAs(UnmanagedType.U1)] bool colorInversion)
		{
			IntPtr hglobal = Marshal.StringToHGlobalAnsi(path);
			ILF3DBaseRender* pImp = this.m_pImp;
			long num = *(*(long*)pImp + 32L);
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte)*,System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride),System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride)), pImp, hglobal.ToPointer(), viewpointOffset, colorInversion, num);
			Marshal.FreeHGlobal(hglobal);
			GC.KeepAlive(this);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000128C File Offset: 0x0000068C
		public unsafe void SharpSetScreenSize(int width, int height)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Int32), pImp, width, height, *(*(long*)pImp + 40L));
			GC.KeepAlive(this);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000012B8 File Offset: 0x000006B8
		public unsafe void SharpSetWindowPos(int x, int y, int width, int height)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Int32,System.Int32,System.Int32), pImp, x, y, width, height, *(*(long*)pImp + 48L));
			GC.KeepAlive(this);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000012E4 File Offset: 0x000006E4
		public unsafe void SharpUpdateEyeInfo(float x, float y, float z, [MarshalAs(UnmanagedType.U1)] bool enabled, float z_l, float z_r)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single,System.Single,System.Single,System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride),System.Single,System.Single), pImp, x, y, z, enabled, z_l, z_r, *(*(long*)pImp + 56L));
			GC.KeepAlive(this);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00001314 File Offset: 0x00000714
		public unsafe void SharpUpdateOffsetXb(float v)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single), pImp, v, *(*(long*)pImp + 64L));
			GC.KeepAlive(this);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000133C File Offset: 0x0000073C
		public unsafe void SharpSetDepth(float fDpt, float dD3)
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single,System.Single), pImp, fDpt, dD3, *(*(long*)pImp + 120L));
			GC.KeepAlive(this);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00001368 File Offset: 0x00000768
		public unsafe void SharpDraw()
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pImp, *(*(long*)pImp + 136L));
			GC.KeepAlive(this);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00001390 File Offset: 0x00000790
		public unsafe void SharpRelease()
		{
			ILF3DBaseRender* pImp = this.m_pImp;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pImp, *(*(long*)pImp + 144L));
			GC.KeepAlive(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000013CC File Offset: 0x000007CC
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!LF3DRenderCoreManager();
			}
			else
			{
				try
				{
					this.!LF3DRenderCoreManager();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00001424 File Offset: 0x00000824
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00001410 File Offset: 0x00000810
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000074 RID: 116
		private unsafe ILF3DBaseRender* m_pImp;
	}
}
