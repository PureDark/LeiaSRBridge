// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\―.cs
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace obf_1
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public class obf_2
	{
		// Token: 0x0600012D RID: 301
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "newSRContext")]
		public static extern IntPtr obf_1();

		// Token: 0x0600012E RID: 302
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "initializeSRContext")]
		public static extern void obf_1(IntPtr);

		// Token: 0x0600012F RID: 303
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "deleteSRContext")]
		public static extern void obf_3(IntPtr);

		// Token: 0x06000130 RID: 304
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "createSystemSense")]
		public static extern IntPtr obf_4(IntPtr);

		// Token: 0x06000131 RID: 305
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "createSystemEventListener")]
		public static extern IntPtr obf_1(IntPtr, obf_2.obf_1);

		// Token: 0x06000132 RID: 306
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "deleteSystemEventListener")]
		public static extern IntPtr obf_5(IntPtr);

		// Token: 0x06000133 RID: 307
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "createSwitchableLensHint")]
		public static extern IntPtr obf_6(IntPtr);

		// Token: 0x06000134 RID: 308
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "deleteSwitchableLensHint")]
		public static extern IntPtr obf_7(IntPtr);

		// Token: 0x06000135 RID: 309
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "lensDisableHint")]
		public static extern void obf_8(IntPtr);

		// Token: 0x06000136 RID: 310
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "lensEnableHint")]
		public static extern void obf_9(IntPtr);

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005E3A File Offset: 0x0000403A
		public static obf_2 Instance
		{
			get
			{
				return obf_2.obf_1.Value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005E48 File Offset: 0x00004048
		public void obf_1(obf_2.obf_1 A_1)
		{
			this.obf_1 = obf_2.obf_1();
			this.obf_3 = obf_2.obf_4(this.obf_1);
			this.obf_1 = A_1;
			this.obf_4 = obf_2.obf_1(this.obf_3, this.obf_1);
			this.obf_5 = obf_2.obf_6(this.obf_1);
			obf_2.obf_8(this.obf_5);
			obf_2.obf_1(this.obf_1);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005EB8 File Offset: 0x000040B8
		public void obf_3()
		{
			if (this.obf_5 != IntPtr.Zero)
			{
				obf_2.obf_7(this.obf_5);
			}
			if (this.obf_4 != IntPtr.Zero)
			{
				obf_2.obf_5(this.obf_4);
			}
			if (this.obf_1 != IntPtr.Zero)
			{
				obf_2.obf_3(this.obf_1);
			}
			this.obf_1 = IntPtr.Zero;
			this.obf_3 = IntPtr.Zero;
			this.obf_4 = IntPtr.Zero;
			this.obf_5 = IntPtr.Zero;
		}

		// Token: 0x040000FF RID: 255
		private static readonly Lazy<obf_2> obf_1 = new Lazy<obf_2>(new Func<obf_2>(obf_2.obf_3.obf_1.obf_1));

		// Token: 0x04000100 RID: 256
		private IntPtr obf_1 = IntPtr.Zero;

		// Token: 0x04000101 RID: 257
		private IntPtr obf_3 = IntPtr.Zero;

		// Token: 0x04000102 RID: 258
		private IntPtr obf_4 = IntPtr.Zero;

		// Token: 0x04000103 RID: 259
		private IntPtr obf_5 = IntPtr.Zero;

		// Token: 0x04000104 RID: 260
		[Nullable(2)]
		private obf_2.obf_1 obf_1;

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x0600013D RID: 317
		[NullableContext(0)]
		[UnmanagedFunctionPointer(2)]
		public delegate void obf_1(obf_10);

		// Token: 0x02000034 RID: 52
		[CompilerGenerated]
		[Serializable]
		private sealed class obf_3
		{
			// Token: 0x06000142 RID: 322 RVA: 0x00005F97 File Offset: 0x00004197
			internal obf_2 obf_1()
			{
				return new obf_2();
			}

			// Token: 0x04000105 RID: 261
			[Nullable(0)]
			public static readonly obf_2.obf_3 obf_1 = new obf_2.obf_3();
		}
	}
}
