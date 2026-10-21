using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace \u00A0
{
	// Token: 0x0200006B RID: 107
	[NullableContext(1)]
	[Nullable(0)]
	internal class \u202E
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000752C File Offset: 0x0000572C
		public static void \u00A0()
		{
			\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202F());
			\u204A.Instance.\u1680(true);
			Task.Factory.StartNew(new Action(\u202E.\u2000.\u00A0.\u00A0));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007580 File Offset: 0x00005780
		private static Task \u00A0(NamedPipeServerStream A_0, Guid A_1)
		{
			\u202E.\u2004 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = A_0;
			u.\u00A0 = A_1;
			u.\u00A0 = -1;
			u.\u00A0.Start<\u202E.\u2004>(ref u);
			return u.\u00A0.Task;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000075CC File Offset: 0x000057CC
		public static void \u00A0(\u202E.\u00A0 A_0, \u202E.\u1680 A_1, string A_2 = "")
		{
			\u202E.\u2003 u = new \u202E.\u2003();
			u.\u00A0 = A_2;
			\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2032());
			u.\u00A0 = \u202E.\u1680(A_0, A_1, u.\u00A0);
			Task.Run(new Action(u.\u00A0));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000761C File Offset: 0x0000581C
		private static string \u1680(\u202E.\u00A0 A_0, \u202E.\u1680 A_1, string A_2 = "None")
		{
			\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2035());
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(3, 4);
			defaultInterpolatedStringHandler.AppendFormatted(\u202E.\u1680);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2033());
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)A_0);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2033());
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)A_1);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2033());
			defaultInterpolatedStringHandler.AppendFormatted(A_2);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007690 File Offset: 0x00005890
		private static void \u00A0(string A_0)
		{
			\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2036());
			if (!A_0.StartsWith(\u202E.\u1680 + 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2033()))
			{
				return;
			}
			string[] array = A_0.Split(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2033(), 0);
			if (array.Length < 4)
			{
				return;
			}
			try
			{
				\u202E.\u00A0 u00A = (\u202E.\u00A0)int.Parse(array[1]);
				\u202E.\u1680 u = (\u202E.\u1680)int.Parse(array[2]);
				switch (u00A)
				{
				case \u202E.\u00A0.\u00A0:
					\u202E.\u00A0(\u202E.\u00A0.\u00A0, \u202E.\u1680.\u00A0, array[3]);
					break;
				case \u202E.\u00A0.\u1680:
					if (u == \u202E.\u1680.\u1680)
					{
						string text = array[3];
						string text2 = array[4];
						\u2036 u2 = (\u2036)Enum.Parse(typeof(\u2036), array[5]);
						string text3 = (array.Length > 6) ? string.Join(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2033(), Enumerable.ToArray<string>(Enumerable.Skip<string>(array, 6))) : string.Empty;
						\u204A.Instance.\u00A0(text, text2, u2, text3);
					}
					else if (u == \u202E.\u1680.\u2000)
					{
						\u204A.Instance.\u2000();
					}
					break;
				case \u202E.\u00A0.\u2000:
					if (u != \u202E.\u1680.\u2002)
					{
						if (u == \u202E.\u1680.\u2003)
						{
							bool isFirstLaunch = global::\u00A0.\u00A0.Default.IsFirstLaunch;
							\u202E.\u00A0(\u202E.\u00A0.\u2000, \u202E.\u1680.\u2003, isFirstLaunch.ToString());
							if (isFirstLaunch)
							{
								global::\u00A0.\u00A0.Default.IsFirstLaunch = false;
								global::\u00A0.\u00A0.Default.Save();
							}
						}
					}
					else
					{
						\u2007.\u1680();
					}
					break;
				case \u202E.\u00A0.\u2001:
					if (u == \u202E.\u1680.\u2006)
					{
						\u202F u202F = \u2035.Instance.\u200A();
						\u202E.\u00A0 u00A2 = \u202E.\u00A0.\u2001;
						\u202E.\u1680 u3 = \u202E.\u1680.\u2007;
						int num = (int)u202F;
						\u202E.\u00A0(u00A2, u3, num.ToString());
						bool u4 = \u2035.Instance.\u2006;
						\u202E.\u00A0 u00A3 = \u202E.\u00A0.\u2001;
						\u202E.\u1680 u5 = \u202E.\u1680.\u2008;
						bool flag = u4;
						\u202E.\u00A0(u00A3, u5, flag.ToString());
						if (u202F != \u202F.\u00A0)
						{
							\u2035.Instance.\u00A0(u202F, false);
						}
						else if (\u202E.\u00A0)
						{
							\u202E.\u1680();
							\u202E.\u00A0 = false;
						}
					}
					break;
				case \u202E.\u00A0.\u2002:
					if (u == \u202E.\u1680.\u2009 && array.Length >= 16 && global::\u00A0.\u2003.Instance.SettingsVM != null)
					{
						global::\u00A0.\u2003.Instance.SettingsVM.CurrOverlayKey = array[3];
						global::\u00A0.\u2003.Instance.SettingsVM.Curr3DModeKey = array[4];
						global::\u00A0.\u2003.Instance.SettingsVM.Curr3DDepthNegKey = array[5];
						global::\u00A0.\u2003.Instance.SettingsVM.Curr3DDepthPosKey = array[6];
						global::\u00A0.\u2003.Instance.SettingsVM.Curr3DPopoutNegKey = array[7];
						global::\u00A0.\u2003.Instance.SettingsVM.Curr3DPopoutPosKey = array[8];
						global::\u00A0.\u2003.Instance.SettingsVM.Language = array[9];
						global::\u00A0.\u2003.Instance.SettingsVM.CursorType = int.Parse(array[10], CultureInfo.InvariantCulture);
						global::\u00A0.\u2003.Instance.SettingsVM.IsConvertVideoTo3D = int.Parse(array[11], CultureInfo.InvariantCulture);
						global::\u00A0.\u2003.Instance.SettingsVM.IsAutoGameScan = int.Parse(array[12], CultureInfo.InvariantCulture);
						global::\u00A0.\u2003.Instance.SettingsVM.IsAutoRun = int.Parse(array[13], CultureInfo.InvariantCulture);
						global::\u00A0.\u2003.Instance.SettingsVM.IsAutoConvert = int.Parse(array[14], CultureInfo.InvariantCulture);
						global::\u00A0.\u2003.Instance.SettingsVM.IsStandardVideo = int.Parse(array[15], CultureInfo.InvariantCulture);
					}
					break;
				case \u202E.\u00A0.\u2003:
					if (array.Length >= 5)
					{
						\u2009 u6 = (\u2009)int.Parse(array[3]);
						\u200B u200B = (\u200B)int.Parse(array[4]);
						string text4 = array[5] ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u203E();
						if (u != \u202E.\u1680.\u200A)
						{
							if (u != \u202E.\u1680.\u2010)
							{
							}
						}
						else
						{
							try
							{
								\u2013.\u00A0(u6, u200B, text4);
							}
							catch (ArgumentException ex)
							{
								\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2047() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2036());
							}
						}
					}
					break;
				case \u202E.\u00A0.\u2004:
					if (u == \u202E.\u1680.\u2011 && array.Length >= 2 && global::\u00A0.\u2003.Instance.SettingsVM != null)
					{
						global::\u00A0.\u2003.Instance.SettingsVM.IsConvertVideoTo3D = int.Parse(array[2], CultureInfo.InvariantCulture);
					}
					break;
				}
			}
			catch (Exception ex2)
			{
				\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2048() + ex2.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2036());
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007AB4 File Offset: 0x00005CB4
		private static void \u1680()
		{
			\u2013.\u00A0(\u2009.SPECS_CPU, \u200B.specs_cpu, \u202B.\u00A0());
			\u2013.\u00A0(\u2009.SPECS_RAM, \u200B.specs_ram, \u202B.\u1680());
			\u2013.\u00A0(\u2009.SPECS_WINDOWSVER, \u200B.specs_windowsver, \u202B.\u2000());
			\u2013.\u00A0(\u2009.SPECS_GPU, \u200B.specs_gpu, \u202B.\u2001());
		}

		// Token: 0x04000222 RID: 546
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2049();

		// Token: 0x04000223 RID: 547
		private static string \u1680 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u204A();

		// Token: 0x04000224 RID: 548
		private static string \u2000 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u204B();

		// Token: 0x04000225 RID: 549
		private static ConcurrentDictionary<Guid, NamedPipeServerStream> \u00A0 = new ConcurrentDictionary<Guid, NamedPipeServerStream>();

		// Token: 0x04000226 RID: 550
		private static bool \u00A0 = true;

		// Token: 0x0200006C RID: 108
		[NullableContext(0)]
		public enum \u00A0
		{
			// Token: 0x04000228 RID: 552
			\u00A0,
			// Token: 0x04000229 RID: 553
			\u1680,
			// Token: 0x0400022A RID: 554
			\u2000,
			// Token: 0x0400022B RID: 555
			\u2001,
			// Token: 0x0400022C RID: 556
			\u2002,
			// Token: 0x0400022D RID: 557
			\u2003,
			// Token: 0x0400022E RID: 558
			\u2004
		}

		// Token: 0x0200006D RID: 109
		[NullableContext(0)]
		public enum \u1680
		{
			// Token: 0x04000230 RID: 560
			\u00A0,
			// Token: 0x04000231 RID: 561
			\u1680,
			// Token: 0x04000232 RID: 562
			\u2000,
			// Token: 0x04000233 RID: 563
			\u2001,
			// Token: 0x04000234 RID: 564
			\u2002,
			// Token: 0x04000235 RID: 565
			\u2003,
			// Token: 0x04000236 RID: 566
			\u2004,
			// Token: 0x04000237 RID: 567
			\u2005,
			// Token: 0x04000238 RID: 568
			\u2006,
			// Token: 0x04000239 RID: 569
			\u2007,
			// Token: 0x0400023A RID: 570
			\u2008,
			// Token: 0x0400023B RID: 571
			\u2009,
			// Token: 0x0400023C RID: 572
			\u200A,
			// Token: 0x0400023D RID: 573
			\u200B,
			// Token: 0x0400023E RID: 574
			\u2010,
			// Token: 0x0400023F RID: 575
			\u2011,
			// Token: 0x04000240 RID: 576
			\u2012,
			// Token: 0x04000241 RID: 577
			\u2013
		}

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		[Serializable]
		private sealed class \u2000
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00007B30 File Offset: 0x00005D30
			internal void \u00A0()
			{
				for (;;)
				{
					try
					{
						\u202E.\u2001 u = new \u202E.\u2001();
						u.\u00A0 = new NamedPipeServerStream(\u202E.\u2000, 3, 10, 0, 1073741824);
						\u2025.\u00A0(\u202E.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u205B(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202F());
						u.\u00A0.WaitForConnection();
						if (u.\u00A0.IsConnected)
						{
							u.\u00A0 = Guid.NewGuid();
							string u00A = \u202E.\u00A0;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
							defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u205C());
							defaultInterpolatedStringHandler.AppendFormatted<Guid>(u.\u00A0);
							\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202F());
							\u202E.\u00A0.TryAdd(u.\u00A0, u.\u00A0);
							Task.Run(new Func<Task>(u.\u00A0));
						}
					}
					catch (Exception ex)
					{
						\u2025.\u1680(\u202E.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u202F());
					}
				}
			}

			// Token: 0x04000242 RID: 578
			[Nullable(0)]
			public static readonly \u202E.\u2000 \u00A0 = new \u202E.\u2000();

			// Token: 0x04000243 RID: 579
			[Nullable(0)]
			public static Action \u00A0;
		}

		// Token: 0x0200006F RID: 111
		[CompilerGenerated]
		private sealed class \u2001
		{
			// Token: 0x060001DB RID: 475 RVA: 0x00007C30 File Offset: 0x00005E30
			[NullableContext(2)]
			internal Task \u00A0()
			{
				return \u202E.\u00A0(this.\u00A0, this.\u00A0);
			}

			// Token: 0x04000244 RID: 580
			[Nullable(0)]
			public NamedPipeServerStream \u00A0;

			// Token: 0x04000245 RID: 581
			public Guid \u00A0;
		}

		// Token: 0x02000070 RID: 112
		[CompilerGenerated]
		private sealed class \u2002
		{
			// Token: 0x060001DD RID: 477 RVA: 0x00007C43 File Offset: 0x00005E43
			internal void \u00A0()
			{
				\u202E.\u00A0(this.\u00A0);
			}

			// Token: 0x04000246 RID: 582
			[Nullable(0)]
			public string \u00A0;
		}

		// Token: 0x02000071 RID: 113
		[CompilerGenerated]
		private sealed class \u2003
		{
			// Token: 0x060001DF RID: 479 RVA: 0x00007C50 File Offset: 0x00005E50
			internal void \u00A0()
			{
				foreach (KeyValuePair<Guid, NamedPipeServerStream> keyValuePair in \u202E.\u00A0)
				{
					if (keyValuePair.Value != null && keyValuePair.Value.IsConnected)
					{
						try
						{
							StreamWriter streamWriter = new StreamWriter(keyValuePair.Value)
							{
								AutoFlush = true
							};
							if (streamWriter != null)
							{
								streamWriter.WriteLine(this.\u00A0);
							}
						}
						catch (Exception ex)
						{
							\u2025.\u1680(\u202E.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2032());
						}
					}
				}
			}

			// Token: 0x04000247 RID: 583
			[Nullable(0)]
			public string \u00A0;
		}
	}
}
