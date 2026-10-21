using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace \u00A0
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	internal class \u2003 : INotifyPropertyChanged
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003E16 File Offset: 0x00002016
		internal static \u2003 Instance
		{
			get
			{
				if (\u2003.\u00A0 == null)
				{
					\u2003.\u00A0 = new \u2003();
				}
				return \u2003.\u00A0;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003E2E File Offset: 0x0000202E
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003E36 File Offset: 0x00002036
		public \u2005 SettingsVM
		{
			get
			{
				return this.\u00A0;
			}
			set
			{
				this.\u00A0 = value;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003E40 File Offset: 0x00002040
		[NullableContext(2)]
		[CompilerGenerated]
		public void add_PropertyChanged(PropertyChangedEventHandler A_1)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.\u00A0;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, A_1);
				propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.\u00A0, propertyChangedEventHandler3, propertyChangedEventHandler2);
			}
			while (propertyChangedEventHandler != propertyChangedEventHandler2);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003E78 File Offset: 0x00002078
		[NullableContext(2)]
		[CompilerGenerated]
		public void remove_PropertyChanged(PropertyChangedEventHandler A_1)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.\u00A0;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, A_1);
				propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.\u00A0, propertyChangedEventHandler3, propertyChangedEventHandler2);
			}
			while (propertyChangedEventHandler != propertyChangedEventHandler2);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003EAD File Offset: 0x000020AD
		private void \u00A0(string A_1)
		{
			if (this.\u00A0 != null)
			{
				this.\u00A0.Invoke(this, new PropertyChangedEventArgs(A_1));
			}
		}

		// Token: 0x04000054 RID: 84
		[Nullable(2)]
		private static \u2003 \u00A0;

		// Token: 0x04000055 RID: 85
		private \u2005 \u00A0 = new \u2005();

		// Token: 0x04000056 RID: 86
		[Nullable(2)]
		[CompilerGenerated]
		private PropertyChangedEventHandler \u00A0;
	}
}
