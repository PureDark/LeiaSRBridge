using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002774 File Offset: 0x00001B74
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string name = "NestedException";
			this.NestedException = (Exception)info.GetValue(name, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DE4 File Offset: 0x000021E4
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002660 File Offset: 0x00001A60
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002674 File Offset: 0x00001A74
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

		// Token: 0x0600007C RID: 124 RVA: 0x00002688 File Offset: 0x00001A88
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

		// Token: 0x0600007D RID: 125 RVA: 0x00002738 File Offset: 0x00001B38
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			info.AddValue("NestedException", nestedException, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x0400007C RID: 124
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x0400007D RID: 125
		private Exception <backing_store>NestedException;
	}
}
