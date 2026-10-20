using System;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x0200004C RID: 76
	internal class FRAME_DATA
	{
		// Token: 0x040002B8 RID: 696
		public Texture2D Frame;

		// Token: 0x040002B9 RID: 697
		public OutputDuplicateFrameInformation FrameInfo;

		// Token: 0x040002BA RID: 698
		public OutputDuplicateMoveRectangle[] MetaData;

		// Token: 0x040002BB RID: 699
		public RawRectangle[] dirtyData;

		// Token: 0x040002BC RID: 700
		public int DirtyCount;

		// Token: 0x040002BD RID: 701
		public int MoveCount;
	}
}
