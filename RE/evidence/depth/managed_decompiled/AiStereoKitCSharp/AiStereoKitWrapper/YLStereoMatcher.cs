using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using StereoMatcher;

namespace AiStereoKitWrapper
{
	// Token: 0x02000321 RID: 801
	public class YLStereoMatcher : IDisposable
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00001EE8 File Offset: 0x000012E8
		public YLStereoMatcher()
		{
			this.isInit = false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00001FF0 File Offset: 0x000013F0
		private void ~YLStereoMatcher()
		{
			this.!YLStereoMatcher();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00001F04 File Offset: 0x00001304
		private unsafe void !YLStereoMatcher()
		{
			IStereoMatcher* pStereoMatcher = this.m_pStereoMatcher;
			if (pStereoMatcher != null)
			{
				IStereoMatcher* ptr = pStereoMatcher;
				long num = *(*(long*)ptr);
				object obj = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), ptr, 1, num);
				this.m_pStereoMatcher = null;
			}
			this.isInit = false;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00001F40 File Offset: 0x00001340
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool Init()
		{
			if (this.m_pStereoMatcher == null)
			{
				this.m_pStereoMatcher = <Module>.CreateStereoMatcher();
			}
			IStereoMatcher* pStereoMatcher = this.m_pStereoMatcher;
			if (pStereoMatcher != null && !this.isInit)
			{
				IStereoMatcher* ptr = pStereoMatcher;
				this.isInit = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 8L));
			}
			GC.KeepAlive(this);
			return this.isInit;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00001F90 File Offset: 0x00001390
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool ExtractAndMatch(byte[] rgba, int width, int height, int rowPitch)
		{
			if (!this.isInit)
			{
				this.Init();
			}
			ref byte byte& = ref rgba[0];
			byte* ptr = ref byte&;
			IStereoMatcher* pStereoMatcher = this.m_pStereoMatcher;
			if (pStereoMatcher != null && this.isInit)
			{
				IStereoMatcher* ptr2 = pStereoMatcher;
				long num = *(*(long*)pStereoMatcher + 16L);
				bool result = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.Int32,System.Int32,System.Int32), ptr2, ptr, width, height, rowPitch, num);
				GC.KeepAlive(this);
				return result;
			}
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002004 File Offset: 0x00001404
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!YLStereoMatcher();
			}
			else
			{
				try
				{
					this.!YLStereoMatcher();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000205C File Offset: 0x0000145C
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002048 File Offset: 0x00001448
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400038B RID: 907
		private bool isInit;

		// Token: 0x0400038C RID: 908
		private unsafe IStereoMatcher* m_pStereoMatcher;
	}
}
