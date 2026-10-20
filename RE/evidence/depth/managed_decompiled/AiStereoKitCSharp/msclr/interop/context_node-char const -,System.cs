using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using msclr.interop.details;

namespace msclr.interop
{
	// Token: 0x02000008 RID: 8
	internal class context_node<char\u0020const\u0020*,System::String\u0020^> : context_node_base, IDisposable
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00001864 File Offset: 0x00000C64
		public unsafe context_node<char\u0020const\u0020*,System::String\u0020^>(sbyte** _to_object, string _from_object)
		{
			this._ptr = null;
			char_buffer<char> char_buffer<char>;
			if (_from_object == null)
			{
				*_to_object = 0L;
				GC.KeepAlive(this);
			}
			else
			{
				ulong num = <Module>.msclr.interop.details.GetAnsiStringSize(_from_object);
				char_buffer<char> = <Module>.new[](num);
				try
				{
					if (char_buffer<char> == null)
					{
						throw new InsufficientMemoryException();
					}
					<Module>.msclr.interop.details.WriteAnsiString(char_buffer<char>, num, _from_object);
					sbyte* ptr = char_buffer<char>;
					char_buffer<char> = 0L;
					this._ptr = ptr;
					*_to_object = ptr;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<char>.{dtor}), (void*)(&char_buffer<char>));
					throw;
				}
				<Module>.delete[](null);
			}
			try
			{
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<char>.{dtor}), (void*)(&char_buffer<char>));
				throw;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00001928 File Offset: 0x00000D28
		private unsafe void ~context_node<char\u0020const\u0020*,System::String\u0020^>()
		{
			<Module>.delete[]((void*)this._ptr);
			GC.KeepAlive(this);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000014CC File Offset: 0x000008CC
		private unsafe void !context_node<char\u0020const\u0020*,System::String\u0020^>()
		{
			<Module>.delete[]((void*)this._ptr);
			GC.KeepAlive(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00001948 File Offset: 0x00000D48
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

		// Token: 0x06000091 RID: 145 RVA: 0x00001A74 File Offset: 0x00000E74
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00001998 File Offset: 0x00000D98
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040000CA RID: 202
		private unsafe sbyte* _ptr;
	}
}
