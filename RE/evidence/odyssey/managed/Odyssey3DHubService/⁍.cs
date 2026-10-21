using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace \u00A0
{
	// Token: 0x0200008C RID: 140
	[CompilerGenerated]
	[DefaultMember("FullName")]
	[Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
	[TypeIdentifier]
	[ComImport]
	public interface \u204D
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000248 RID: 584
		[DispId(0)]
		[IndexerName("FullName")]
		string FullName { [DispId(0)] [MethodImpl(4096)] [return: MarshalAs(19)] get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000249 RID: 585
		// (set) Token: 0x0600024A RID: 586
		[DispId(1000)]
		string Arguments { [DispId(1000)] [MethodImpl(4096)] [return: MarshalAs(19)] get; [DispId(1000)] [MethodImpl(4096)] [param: MarshalAs(19)] [param: In] set; }

		// Token: 0x0600024B RID: 587
		void _VtblGap1_7();

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600024C RID: 588
		// (set) Token: 0x0600024D RID: 589
		[DispId(1005)]
		string TargetPath { [DispId(1005)] [MethodImpl(4096)] [return: MarshalAs(19)] get; [DispId(1005)] [MethodImpl(4096)] [param: MarshalAs(19)] [param: In] set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600024E RID: 590
		// (set) Token: 0x0600024F RID: 591
		[DispId(1006)]
		int WindowStyle { [DispId(1006)] [MethodImpl(4096)] get; [DispId(1006)] [MethodImpl(4096)] [param: In] set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000250 RID: 592
		// (set) Token: 0x06000251 RID: 593
		[DispId(1007)]
		string WorkingDirectory { [DispId(1007)] [MethodImpl(4096)] [return: MarshalAs(19)] get; [DispId(1007)] [MethodImpl(4096)] [param: MarshalAs(19)] [param: In] set; }

		// Token: 0x06000252 RID: 594
		void _VtblGap2_1();

		// Token: 0x06000253 RID: 595
		[DispId(2001)]
		[MethodImpl(4096)]
		void Save();
	}
}
