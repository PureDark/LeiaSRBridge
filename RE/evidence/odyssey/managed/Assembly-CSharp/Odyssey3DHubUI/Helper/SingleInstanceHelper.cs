using System;
using System.Diagnostics;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};

namespace Odyssey3DHubUI.Helper
{
	// Token: 0x02000156 RID: 342
	public static class SingleInstanceHelper
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x00027298 File Offset: 0x00025498
		public static bool EnsureSingleInstance()
		{
			bd.ih(SingleInstanceHelper.cun, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bkf());
			bool result = false;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				if (currentProcess != null)
				{
					string text = currentProcess.ProcessName;
					Process[] array = new Process[0];
					string empty = string.Empty;
					Process[] array2 = new Process[0];
					int num = 0;
					int num2 = -1;
					int num3 = -1;
					if (text.Contains(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bu()))
					{
						array2 = Process.GetProcessesByName(text.Substring(0, text.IndexOf(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bu())));
						array = Process.GetProcessesByName(text);
					}
					else
					{
						array2 = Process.GetProcessesByName(text);
						text += <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bkg();
						array = Process.GetProcessesByName(text);
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].MainWindowHandle != IntPtr.Zero)
						{
							num++;
							if (num3 == -1)
							{
								num3 = i;
							}
						}
						else if (array[i].Id == Process.GetCurrentProcess().Id)
						{
							num++;
							num2 = i;
						}
					}
					if (array2.Length + num > 1)
					{
						if (array2.Length > 1)
						{
							if (array2[0].MainWindowHandle != IntPtr.Zero || array2[1].MainWindowHandle != IntPtr.Zero)
							{
								int num4;
								if (array2[0].Id == Process.GetCurrentProcess().Id)
								{
									num4 = 1;
								}
								else
								{
									num4 = 0;
								}
								if (array2[num4].Responding)
								{
									SingleInstanceHelper.ShowAndSetForegroundWindow(array2[num4].MainWindowHandle);
								}
							}
						}
						else if (array2.Length == 1 && num > 0)
						{
							if (array2[0].Id == Process.GetCurrentProcess().Id)
							{
								if (num3 != -1)
								{
									SingleInstanceHelper.ShowAndSetForegroundWindow(array[num3].MainWindowHandle);
								}
							}
							else
							{
								SingleInstanceHelper.ShowAndSetForegroundWindow(array2[0].MainWindowHandle);
							}
						}
						else if (num > 1)
						{
							if (array[num2].Id == Process.GetCurrentProcess().Id && num3 != -1)
							{
								SingleInstanceHelper.ShowAndSetForegroundWindow(array[num3].MainWindowHandle);
							}
							else
							{
								SingleInstanceHelper.ShowAndSetForegroundWindow(array[num2].MainWindowHandle);
							}
						}
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000274BC File Offset: 0x000256BC
		private static void ShowAndSetForegroundWindow(IntPtr mainWindowHandle)
		{
			bd.ih(SingleInstanceHelper.cun, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bkh());
			bl.ShowWindow(mainWindowHandle, 5);
			bl.SetForegroundWindow(mainWindowHandle);
		}

		// Token: 0x0400072B RID: 1835
		private const int cum = 5;

		// Token: 0x0400072C RID: 1836
		private static string cun = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bki();
	}
}
