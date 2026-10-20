using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using msclr.interop.details;

namespace msclr.interop
{
	// Token: 0x02000007 RID: 7
	internal class context_node<char\u0020const\u0020*,System::String\u0020^> : context_node_base, IDisposable
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00001740 File Offset: 0x00000B40
		public unsafe context_node<char\u0020const\u0020*,System::String\u0020^>(sbyte** _to_object, string _from_object)
		{
			this._ptr = null;
			char_buffer<char> _char_buf;
			if (_from_object == null)
			{
				*_to_object = 0L;
				GC.KeepAlive(this);
			}
			else
			{
				ulong _size = <Module>.msclr.interop.details.GetAnsiStringSize(_from_object);
				_char_buf = <Module>.new[](_size);
				try
				{
					if (_char_buf == null)
					{
						throw new InsufficientMemoryException();
					}
					<Module>.msclr.interop.details.WriteAnsiString(_char_buf, _size, _from_object);
					sbyte* ptr = _char_buf;
					_char_buf = 0L;
					this._ptr = ptr;
					*_to_object = ptr;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<char>.{dtor}), (void*)(&_char_buf));
					throw;
				}
				<Module>.delete[](null);
			}
			try
			{
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<char>.{dtor}), (void*)(&_char_buf));
				throw;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00001804 File Offset: 0x00000C04
		private unsafe void ~context_node<char\u0020const\u0020*,System::String\u0020^>()
		{
			<Module>.delete[]((void*)this._ptr);
			GC.KeepAlive(this);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000014A8 File Offset: 0x000008A8
		private unsafe void !context_node<char\u0020const\u0020*,System::String\u0020^>()
		{
			<Module>.delete[]((void*)this._ptr);
			GC.KeepAlive(this);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00001824 File Offset: 0x00000C24
		[HandleProcessCorruptedStateExceptions]
		protected unsafe virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				<Module>.delete[]((void*)this._ptr);
				GC.KeepAlive(this);
			}
			else
			{
				try
				{
					this.!context_node<char\u0020const\u0020*,System::String\u0020^>();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00001980 File Offset: 0x00000D80
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00001874 File Offset: 0x00000C74
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040000C4 RID: 196
		private unsafe sbyte* _ptr;
	}
}
