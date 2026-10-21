using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2008
	{
		// Token: 0x060000FD RID: 253
		[DllImport("user32.dll", CharSet = 4, EntryPoint = "SetWindowsHookEx", SetLastError = true)]
		private static extern IntPtr \u00A0(int, \u2008.\u00A0, IntPtr, uint);

		// Token: 0x060000FE RID: 254
		[DllImport("user32.dll", CharSet = 4, EntryPoint = "UnhookWindowsHookEx", SetLastError = true)]
		[return: MarshalAs(2)]
		private static extern bool \u00A0(IntPtr);

		// Token: 0x060000FF RID: 255
		[DllImport("user32.dll", CharSet = 4, EntryPoint = "CallNextHookEx", SetLastError = true)]
		private static extern IntPtr \u00A0(IntPtr, int, IntPtr, IntPtr);

		// Token: 0x06000100 RID: 256
		[DllImport("kernel32.dll", CharSet = 4, EntryPoint = "GetModuleHandle", SetLastError = true)]
		private static extern IntPtr \u00A0(string);

		// Token: 0x06000101 RID: 257 RVA: 0x000050E9 File Offset: 0x000032E9
		public \u2008()
		{
			this.\u00A0 = new \u2008.\u00A0(this.\u00A0);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005110 File Offset: 0x00003310
		public void \u00A0()
		{
			\u2025.\u00A0(\u2008.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u204B());
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				if (currentProcess != null && currentProcess.MainModule != null)
				{
					this.\u00A0 = \u2008.\u00A0(\u2008.\u00A0, this.\u00A0, \u2008.\u00A0(currentProcess.MainModule.ModuleName), 0U);
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005180 File Offset: 0x00003380
		public void \u1680()
		{
			\u2025.\u00A0(\u2008.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u204C());
			if (this.\u00A0 != IntPtr.Zero)
			{
				\u2008.\u00A0(this.\u00A0);
				this.\u00A0 = IntPtr.Zero;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000051B8 File Offset: 0x000033B8
		private IntPtr \u00A0(int A_1, IntPtr A_2, IntPtr A_3)
		{
			if (A_1 >= 0 && (A_2 == (IntPtr)\u2008.\u1680 || A_2 == (IntPtr)\u2008.\u2000))
			{
				int num = Marshal.ReadInt32(A_3);
				if (num == \u2008.\u2001)
				{
					Application.Current.Dispatcher.Invoke(new Action(\u2008.\u1680.\u00A0.\u00A0));
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(10, 1);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u204D());
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				Console.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return \u2008.\u00A0(this.\u00A0, A_1, A_2, A_3);
		}

		// Token: 0x04000096 RID: 150
		private static int \u00A0 = 13;

		// Token: 0x04000097 RID: 151
		private static int \u1680 = 256;

		// Token: 0x04000098 RID: 152
		private static int \u2000 = 257;

		// Token: 0x04000099 RID: 153
		private static int \u2001 = 27;

		// Token: 0x0400009A RID: 154
		private IntPtr \u00A0 = IntPtr.Zero;

		// Token: 0x0400009B RID: 155
		private readonly \u2008.\u00A0 \u00A0;

		// Token: 0x0400009C RID: 156
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u204E();

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x06000107 RID: 263
		[NullableContext(0)]
		private delegate IntPtr \u00A0(int, IntPtr, IntPtr);

		// Token: 0x02000027 RID: 39
		[CompilerGenerated]
		[Serializable]
		private sealed class \u1680
		{
			// Token: 0x0600010C RID: 268 RVA: 0x0000528A File Offset: 0x0000348A
			internal void \u00A0()
			{
			}

			// Token: 0x0400009D RID: 157
			[Nullable(0)]
			public static readonly \u2008.\u1680 \u00A0 = new \u2008.\u1680();

			// Token: 0x0400009E RID: 158
			[Nullable(0)]
			public static Action \u00A0;
		}
	}
}
