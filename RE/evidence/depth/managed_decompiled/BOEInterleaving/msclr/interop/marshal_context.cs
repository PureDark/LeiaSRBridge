using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace msclr.interop
{
	// Token: 0x02000009 RID: 9
	internal class marshal_context : IDisposable
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000019DC File Offset: 0x00000DDC
		public unsafe sbyte* marshal_as<char\u0020const\u0020*,System::String>(string _from)
		{
			sbyte* result = marshal_context.internal_marshaler<char\u0020const\u0020*,System::String\u0020^,1>.marshal_as(ref _from, this._clean_up_list);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00001558 File Offset: 0x00000958
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

		// Token: 0x0600009F RID: 159 RVA: 0x000015A0 File Offset: 0x000009A0
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

		// Token: 0x060000A0 RID: 160 RVA: 0x000018F4 File Offset: 0x00000CF4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x040000C6 RID: 198
		internal readonly LinkedList<object> _clean_up_list = new LinkedList<object>();

		// Token: 0x0200000B RID: 11
		public class internal_marshaler<char\u0020const\u0020*,System::String\u0020^,1>
		{
			// Token: 0x060000AD RID: 173 RVA: 0x00001960 File Offset: 0x00000D60
			public unsafe static sbyte* marshal_as(string* _from, LinkedList<object> _clean_up_list)
			{
				sbyte* _to_object;
				context_node<char\u0020const\u0020*,System::String\u0020^> _obj = new context_node<char\u0020const\u0020*,System::String\u0020^>(ref _to_object, *_from);
				_clean_up_list.AddLast(_obj);
				return _to_object;
			}
		}
	}
}
