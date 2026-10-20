using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace msclr.interop
{
	// Token: 0x02000009 RID: 9
	internal class context_node<wchar_t\u0020const\u0020*,System::String\u0020^> : context_node_base, IDisposable
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000014EC File Offset: 0x000008EC
		public unsafe context_node<wchar_t\u0020const\u0020*,System::String\u0020^>(char** _to_object, string _from_object)
		{
			IntPtr ip = Marshal.StringToHGlobalUni(_from_object);
			this._ip = ip;
			*_to_object = this._ip.ToPointer();
			GC.KeepAlive(this);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000019AC File Offset: 0x00000DAC
		private void ~context_node<wchar_t\u0020const\u0020*,System::String\u0020^>()
		{
			this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00001520 File Offset: 0x00000920
		private void !context_node<wchar_t\u0020const\u0020*,System::String\u0020^>()
		{
			if (this._ip != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this._ip);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000019C0 File Offset: 0x00000DC0
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

		// Token: 0x06000097 RID: 151 RVA: 0x00001A94 File Offset: 0x00000E94
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00001A04 File Offset: 0x00000E04
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040000CB RID: 203
		private IntPtr _ip;
	}
}
