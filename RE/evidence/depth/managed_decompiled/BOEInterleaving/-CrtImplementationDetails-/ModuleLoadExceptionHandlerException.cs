using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003564 File Offset: 0x00002964
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string name = "NestedException";
			this.NestedException = (Exception)info.GetValue(name, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003BD4 File Offset: 0x00002FD4
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003450 File Offset: 0x00002850
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003464 File Offset: 0x00002864
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

		// Token: 0x060000C0 RID: 192 RVA: 0x00003478 File Offset: 0x00002878
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

		// Token: 0x060000C1 RID: 193 RVA: 0x00003528 File Offset: 0x00002928
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			info.AddValue("NestedException", nestedException, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x0400028C RID: 652
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x0400028D RID: 653
		private Exception <backing_store>NestedException;
	}
}
