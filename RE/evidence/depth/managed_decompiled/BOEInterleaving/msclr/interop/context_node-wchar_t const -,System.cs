using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace msclr.interop
{
	// Token: 0x02000008 RID: 8
	internal class context_node<wchar_t\u0020const\u0020*,System::String\u0020^> : context_node_base, IDisposable
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000014C8 File Offset: 0x000008C8
		public unsafe context_node<wchar_t\u0020const\u0020*,System::String\u0020^>(char** _to_object, string _from_object)
		{
			IntPtr ip = Marshal.StringToHGlobalUni(_from_object);
			this._ip = ip;
			*_to_object = this._ip.ToPointer();
			GC.KeepAlive(this);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00001888 File Offset: 0x00000C88
		private void ~context_node<wchar_t\u0020const\u0020*,System::String\u0020^>()
		{
			this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000014FC File Offset: 0x000008FC
		private void !context_node<wchar_t\u0020const\u0020*,System::String\u0020^>()
		{
			if (this._ip != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this._ip);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000189C File Offset: 0x00000C9C
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
			}
			else
			{
				try
				{
					this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000019A0 File Offset: 0x00000DA0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000018E0 File Offset: 0x00000CE0
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040000C5 RID: 197
		private IntPtr _ip;
	}
}
