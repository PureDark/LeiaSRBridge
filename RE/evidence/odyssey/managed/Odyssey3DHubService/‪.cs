using System;
using System.Diagnostics;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x02000063 RID: 99
	public static class \u202A
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000069D8 File Offset: 0x00004BD8
		public static bool \u00A0()
		{
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
					if (text.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204A()))
					{
						array2 = Process.GetProcessesByName(text.Substring(0, text.IndexOf(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204A())));
						array = Process.GetProcessesByName(text);
					}
					else
					{
						array2 = Process.GetProcessesByName(text);
						text += 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2000\u204B();
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
									\u202D.\u2007(array2[num4].MainWindowHandle);
								}
							}
						}
						else if (array2.Length == 1 && num > 0)
						{
							if (array2[0].Id == Process.GetCurrentProcess().Id)
							{
								if (num3 != -1)
								{
									\u202D.\u2007(array[num3].MainWindowHandle);
								}
							}
							else
							{
								\u202D.\u2007(array2[0].MainWindowHandle);
							}
						}
						else if (num > 1)
						{
							if (array[num2].Id == Process.GetCurrentProcess().Id && num3 != -1)
							{
								\u202D.\u2007(array[num3].MainWindowHandle);
							}
							else
							{
								\u202D.\u2007(array[num2].MainWindowHandle);
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
	}
}
