using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace AstraDepth3D.Dumper
{
	// Token: 0x02000043 RID: 67
	public static class CrashDumper
	{
		// Token: 0x060002CC RID: 716
		[DllImport("dbghelp.dll", SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, IntPtr hFile, CrashDumper.MINIDUMP_TYPE dumpType, IntPtr exceptionParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x060002CD RID: 717 RVA: 0x0001222C File Offset: 0x0001042C
		public static void WriteDump(string filePath, CrashDumper.MINIDUMP_TYPE type = CrashDumper.MINIDUMP_TYPE.MiniDumpWithFullMemory)
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				Process process = Process.GetCurrentProcess();
				if (!CrashDumper.MiniDumpWriteDump(process.Handle, (uint)process.Id, fs.SafeFileHandle.DangerousGetHandle(), type, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
				{
					int hr = Marshal.GetLastWin32Error();
					throw new InvalidOperationException(string.Format("MiniDumpWriteDump failed. Win32 Error Code: {0}", hr));
				}
			}
		}

		// Token: 0x020000C7 RID: 199
		[Flags]
		public enum MINIDUMP_TYPE : uint
		{
			// Token: 0x040005B6 RID: 1462
			MiniDumpNormal = 0U,
			// Token: 0x040005B7 RID: 1463
			MiniDumpWithFullMemory = 2U,
			// Token: 0x040005B8 RID: 1464
			MiniDumpWithDataSegs = 4U,
			// Token: 0x040005B9 RID: 1465
			MiniDumpWithHandleData = 8U,
			// Token: 0x040005BA RID: 1466
			MiniDumpWithThreadInfo = 4096U,
			// Token: 0x040005BB RID: 1467
			MiniDumpWithUnloadedModules = 8192U
		}
	}
}
