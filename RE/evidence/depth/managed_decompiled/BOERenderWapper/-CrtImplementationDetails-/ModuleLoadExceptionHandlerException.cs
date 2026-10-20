using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x020003CD RID: 973
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00003F14 File Offset: 0x00003314
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string name = "NestedException";
			this.NestedException = (Exception)info.GetValue(name, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004584 File Offset: 0x00003984
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003E00 File Offset: 0x00003200
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00003E14 File Offset: 0x00003214
		public Exception NestedException
		{
			get
			{
				return this.<backing_store>NestedException;
			}
			set
			{
				this.<backing_store>NestedException = value;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003E28 File Offset: 0x00003228
		public override string ToString()
		{
			string text;
			if (this.InnerException != null)
			{
				text = this.InnerException.ToString();
			}
			else
			{
				text = string.Empty;
			}
			string text2;
			if (this.NestedException != null)
			{
				text2 = this.NestedException.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			object[] array = new object[4];
			Type type = this.GetType();
			array[0] = type;
			string text3;
			if (this.Message != null)
			{
				text3 = this.Message;
			}
			else
			{
				text3 = string.Empty;
			}
			array[1] = text3;
			string text4;
			if (text != null)
			{
				text4 = text;
			}
			else
			{
				text4 = string.Empty;
			}
			array[2] = text4;
			string text5;
			if (text2 != null)
			{
				text5 = text2;
			}
			else
			{
				text5 = string.Empty;
			}
			array[3] = text5;
			string result = string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", array);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003ED8 File Offset: 0x000032D8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			info.AddValue("NestedException", nestedException, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x04000503 RID: 1283
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x04000504 RID: 1284
		private Exception <backing_store>NestedException;
	}
}
