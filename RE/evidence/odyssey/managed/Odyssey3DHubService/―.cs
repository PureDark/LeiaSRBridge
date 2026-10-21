using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace \u00A0
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2015
	{
		// Token: 0x0600012D RID: 301
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "newSRContext")]
		public static extern IntPtr \u00A0();

		// Token: 0x0600012E RID: 302
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "initializeSRContext")]
		public static extern void \u00A0(IntPtr);

		// Token: 0x0600012F RID: 303
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "deleteSRContext")]
		public static extern void \u1680(IntPtr);

		// Token: 0x06000130 RID: 304
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "createSystemSense")]
		public static extern IntPtr \u2000(IntPtr);

		// Token: 0x06000131 RID: 305
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "createSystemEventListener")]
		public static extern IntPtr \u00A0(IntPtr, \u2015.\u00A0);

		// Token: 0x06000132 RID: 306
		[DllImport("SimulatedRealityCore.dll", EntryPoint = "deleteSystemEventListener")]
		public static extern IntPtr \u2001(IntPtr);

		// Token: 0x06000133 RID: 307
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "createSwitchableLensHint")]
		public static extern IntPtr \u2002(IntPtr);

		// Token: 0x06000134 RID: 308
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "deleteSwitchableLensHint")]
		public static extern IntPtr \u2003(IntPtr);

		// Token: 0x06000135 RID: 309
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "lensDisableHint")]
		public static extern void \u2004(IntPtr);

		// Token: 0x06000136 RID: 310
		[DllImport("SimulatedRealityDisplays.dll", EntryPoint = "lensEnableHint")]
		public static extern void \u2005(IntPtr);

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005E3A File Offset: 0x0000403A
		public static \u2015 Instance
		{
			get
			{
				return \u2015.\u00A0.Value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005E48 File Offset: 0x00004048
		public void \u00A0(\u2015.\u00A0 A_1)
		{
			this.\u00A0 = \u2015.\u00A0();
			this.\u1680 = \u2015.\u2000(this.\u00A0);
			this.\u00A0 = A_1;
			this.\u2000 = \u2015.\u00A0(this.\u1680, this.\u00A0);
			this.\u2001 = \u2015.\u2002(this.\u00A0);
			\u2015.\u2004(this.\u2001);
			\u2015.\u00A0(this.\u00A0);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005EB8 File Offset: 0x000040B8
		public void \u1680()
		{
			if (this.\u2001 != IntPtr.Zero)
			{
				\u2015.\u2003(this.\u2001);
			}
			if (this.\u2000 != IntPtr.Zero)
			{
				\u2015.\u2001(this.\u2000);
			}
			if (this.\u00A0 != IntPtr.Zero)
			{
				\u2015.\u1680(this.\u00A0);
			}
			this.\u00A0 = IntPtr.Zero;
			this.\u1680 = IntPtr.Zero;
			this.\u2000 = IntPtr.Zero;
			this.\u2001 = IntPtr.Zero;
		}

		// Token: 0x040000FF RID: 255
		private static readonly Lazy<\u2015> \u00A0 = new Lazy<\u2015>(new Func<\u2015>(\u2015.\u1680.\u00A0.\u00A0));

		// Token: 0x04000100 RID: 256
		private IntPtr \u00A0 = IntPtr.Zero;

		// Token: 0x04000101 RID: 257
		private IntPtr \u1680 = IntPtr.Zero;

		// Token: 0x04000102 RID: 258
		private IntPtr \u2000 = IntPtr.Zero;

		// Token: 0x04000103 RID: 259
		private IntPtr \u2001 = IntPtr.Zero;

		// Token: 0x04000104 RID: 260
		[Nullable(2)]
		private \u2015.\u00A0 \u00A0;

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x0600013D RID: 317
		[NullableContext(0)]
		[UnmanagedFunctionPointer(2)]
		public delegate void \u00A0(\u2024);

		// Token: 0x02000034 RID: 52
		[CompilerGenerated]
		[Serializable]
		private sealed class \u1680
		{
			// Token: 0x06000142 RID: 322 RVA: 0x00005F97 File Offset: 0x00004197
			internal \u2015 \u00A0()
			{
				return new \u2015();
			}

			// Token: 0x04000105 RID: 261
			[Nullable(0)]
			public static readonly \u2015.\u1680 \u00A0 = new \u2015.\u1680();
		}
	}
}
