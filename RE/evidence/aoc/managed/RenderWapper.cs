using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace BOERenderWapper
{
	// Token: 0x02000006 RID: 6
	public class RenderWapper : IDisposable
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00001870 File Offset: 0x00000C70
		public unsafe RenderWapper()
		{
			StereoscopicRender* ptr = <Module>.@new(80UL);
			StereoscopicRender* pStereoscopicRender;
			try
			{
				if (ptr != null)
				{
					pStereoscopicRender = <Module>.StereoscopicRender.{ctor}(ptr);
				}
				else
				{
					pStereoscopicRender = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr, 80UL);
				throw;
			}
			this.m_pStereoscopicRender = pStereoscopicRender;
			GC.KeepAlive(this);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00001E88 File Offset: 0x00001288
		private void ~RenderWapper()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00001D7C File Offset: 0x0000117C
		private void !RenderWapper()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000018D0 File Offset: 0x00000CD0
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool Initiallize(IntPtr mDevice)
		{
			if (this.m_pStereoscopicRender != null)
			{
				bool result = <Module>.StereoscopicRender.Initiallize(this.m_pStereoscopicRender, (ID3D11Device*)mDevice.ToPointer()) != null;
				GC.KeepAlive(this);
				return result;
			}
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00001908 File Offset: 0x00000D08
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool RenderTexture(IntPtr mDevice, IntPtr mDeviceContext, IntPtr SRV, IntPtr RTV)
		{
			if (this.m_pStereoscopicRender != null)
			{
				bool result = <Module>.StereoscopicRender.renderTexture(this.m_pStereoscopicRender, (ID3D11Device*)mDevice.ToPointer(), (ID3D11DeviceContext*)mDeviceContext.ToPointer(), (ID3D11ShaderResourceView*)SRV.ToPointer(), (ID3D11RenderTargetView*)RTV.ToPointer()) != null;
				GC.KeepAlive(this);
				return result;
			}
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00001C00 File Offset: 0x00001000
		public unsafe void Release()
		{
			StereoscopicRender* pStereoscopicRender = this.m_pStereoscopicRender;
			if (pStereoscopicRender != null)
			{
				StereoscopicRender* ptr = pStereoscopicRender;
				<Module>.StereoscopicRender.{dtor}(ptr);
				<Module>.delete((void*)ptr, 80UL);
				this.m_pStereoscopicRender = null;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00001FAC File Offset: 0x000013AC
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
					this.!RenderWapper();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000020B8 File Offset: 0x000014B8
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00001FF8 File Offset: 0x000013F8
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040001C2 RID: 450
		private unsafe StereoscopicRender* m_pStereoscopicRender;
	}
}
