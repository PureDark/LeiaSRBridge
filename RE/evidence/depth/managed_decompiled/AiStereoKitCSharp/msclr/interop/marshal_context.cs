using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace msclr.interop
{
	// Token: 0x0200000A RID: 10
	internal class marshal_context : IDisposable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00001AD0 File Offset: 0x00000ED0
		public unsafe sbyte* marshal_as<char\u0020const\u0020*,System::String>(string _from)
		{
			sbyte* result = marshal_context.internal_marshaler<char\u0020const\u0020*,System::String\u0020^,1>.marshal_as(ref _from, this._clean_up_list);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000157C File Offset: 0x0000097C
		private void ~marshal_context()
		{
			LinkedList<object>.Enumerator enumerator = this._clean_up_list.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					IDisposable disposable = enumerator.Current as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				while (enumerator.MoveNext());
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000015C4 File Offset: 0x000009C4
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~marshal_context();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00001A18 File Offset: 0x00000E18
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x040000CC RID: 204
		internal readonly LinkedList<object> _clean_up_list = new LinkedList<object>();

		// Token: 0x0200000C RID: 12
		public class internal_marshaler<char\u0020const\u0020*,System::String\u0020^,1>
		{
			// Token: 0x060000AD RID: 173 RVA: 0x00001A54 File Offset: 0x00000E54
			public unsafe static sbyte* marshal_as(string* _from, LinkedList<object> _clean_up_list)
			{
				sbyte* result;
				context_node<char\u0020const\u0020*,System::String\u0020^> value = new context_node<char\u0020const\u0020*,System::String\u0020^>(ref result, *_from);
				_clean_up_list.AddLast(value);
				return result;
			}
		}
	}
}
