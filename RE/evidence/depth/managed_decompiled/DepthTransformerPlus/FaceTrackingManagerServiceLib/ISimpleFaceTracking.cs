using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FaceTrackingManagerServiceLib
{
	// Token: 0x0200005C RID: 92
	[CompilerGenerated]
	[Guid("E26EE08A-BCF1-49F8-B01F-0044D2897F28")]
	[TypeIdentifier]
	[ComImport]
	public interface ISimpleFaceTracking
	{
		// Token: 0x060003B0 RID: 944
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Open();

		// Token: 0x060003B1 RID: 945
		void _VtblGap1_3();

		// Token: 0x060003B2 RID: 946
		[DispId(5)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Close();

		// Token: 0x060003B3 RID: 947
		[DispId(6)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPosition(out float x, out float y, out float z, out float z_l, out float z_r);

		// Token: 0x060003B4 RID: 948
		void _VtblGap2_4();

		// Token: 0x060003B5 RID: 949
		[DispId(11)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void StartCamera();

		// Token: 0x060003B6 RID: 950
		[DispId(12)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void StopCamera();
	}
}
