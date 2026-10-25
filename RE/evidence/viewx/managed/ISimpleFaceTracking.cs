using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FaceTrackingManagerServiceLib
{
	// Token: 0x02000065 RID: 101
	[CompilerGenerated]
	[Guid("E26EE08A-BCF1-49F8-B01F-0044D2897F28")]
	[TypeIdentifier]
	[ComImport]
	public interface ISimpleFaceTracking
	{
		// Token: 0x060003C5 RID: 965
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Open();

		// Token: 0x060003C6 RID: 966
		void _VtblGap1_3();

		// Token: 0x060003C7 RID: 967
		[DispId(5)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Close();

		// Token: 0x060003C8 RID: 968
		[DispId(6)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPosition(out float x, out float y, out float z, out float z_l, out float z_r);
	}
}
