// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\‮.cs
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

namespace obf_12
{
	// Token: 0x0200006B RID: 107
	[NullableContext(1)]
	[Nullable(0)]
	internal class obf_36
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000752C File Offset: 0x0000572C
		public static void obf_12()
		{
			obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_38());
			obf_39.Instance.obf_14(true);
			Task.Factory.StartNew(new Action(obf_36.obf_18.obf_12.obf_12));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007580 File Offset: 0x00005780
		private static Task obf_12(NamedPipeServerStream A_0, Guid A_1)
		{
			obf_36.obf_21 u;
			u.obf_12 = AsyncTaskMethodBuilder.Create();
			u.obf_12 = A_0;
			u.obf_12 = A_1;
			u.obf_12 = -1;
			u.obf_12.Start<obf_36.obf_21>(ref u);
			return u.obf_12.Task;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000075CC File Offset: 0x000057CC
		public static void obf_12(obf_36.obf_12 A_0, obf_36.obf_14 A_1, string A_2 = "")
		{
			obf_36.obf_20 u = new obf_36.obf_20();
			u.obf_12 = A_2;
			obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_40());
			u.obf_12 = obf_36.obf_14(A_0, A_1, u.obf_12);
			Task.Run(new Action(u.obf_12));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000761C File Offset: 0x0000581C
		private static string obf_14(obf_36.obf_12 A_0, obf_36.obf_14 A_1, string A_2 = "None")
		{
			obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_41());
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(3, 4);
			defaultInterpolatedStringHandler.AppendFormatted(obf_36.obf_14);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42());
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)A_0);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42());
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)A_1);
			defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42());
			defaultInterpolatedStringHandler.AppendFormatted(A_2);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007690 File Offset: 0x00005890
		private static void obf_12(string A_0)
		{
			obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_43());
			if (!A_0.StartsWith(obf_36.obf_14 + 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42()))
			{
				return;
			}
			string[] array = A_0.Split(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42(), 0);
			if (array.Length < 4)
			{
				return;
			}
			try
			{
				obf_36.obf_12 u00A = (obf_36.obf_12)int.Parse(array[1]);
				obf_36.obf_14 u = (obf_36.obf_14)int.Parse(array[2]);
				switch (u00A)
				{
				case obf_36.obf_12.obf_12:
					obf_36.obf_12(obf_36.obf_12.obf_12, obf_36.obf_14.obf_12, array[3]);
					break;
				case obf_36.obf_12.obf_14:
					if (u == obf_36.obf_14.obf_14)
					{
						string text = array[3];
						string text2 = array[4];
						obf_44 u2 = (obf_44)Enum.Parse(typeof(obf_44), array[5]);
						string text3 = (array.Length > 6) ? string.Join(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_42(), Enumerable.ToArray<string>(Enumerable.Skip<string>(array, 6))) : string.Empty;
						obf_39.Instance.obf_12(text, text2, u2, text3);
					}
					else if (u == obf_36.obf_14.obf_18)
					{
						obf_39.Instance.obf_18();
					}
					break;
				case obf_36.obf_12.obf_18:
					if (u != obf_36.obf_14.obf_24)
					{
						if (u == obf_36.obf_14.obf_20)
						{
							bool isFirstLaunch = global::obf_12.obf_12.Default.IsFirstLaunch;
							obf_36.obf_12(obf_36.obf_12.obf_18, obf_36.obf_14.obf_20, isFirstLaunch.ToString());
							if (isFirstLaunch)
							{
								global::obf_12.obf_12.Default.IsFirstLaunch = false;
								global::obf_12.obf_12.Default.Save();
							}
						}
					}
					else
					{
						obf_17.obf_14();
					}
					break;
				case obf_36.obf_12.obf_22:
					if (u == obf_36.obf_14.obf_19)
					{
						obf_45 u202F = obf_46.Instance.obf_29();
						obf_36.obf_12 u00A2 = obf_36.obf_12.obf_22;
						obf_36.obf_14 u3 = obf_36.obf_14.obf_17;
						int num = (int)u202F;
						obf_36.obf_12(u00A2, u3, num.ToString());
						bool u4 = obf_46.Instance.obf_19;
						obf_36.obf_12 u00A3 = obf_36.obf_12.obf_22;
						obf_36.obf_14 u5 = obf_36.obf_14.obf_27;
						bool flag = u4;
						obf_36.obf_12(u00A3, u5, flag.ToString());
						if (u202F != obf_45.obf_12)
						{
							obf_46.Instance.obf_12(u202F, false);
						}
						else if (obf_36.obf_12)
						{
							obf_36.obf_14();
							obf_36.obf_12 = false;
						}
					}
					break;
				case obf_36.obf_12.obf_24:
					if (u == obf_36.obf_14.obf_28 && array.Length >= 16 && global::obf_12.obf_20.Instance.SettingsVM != null)
					{
						global::obf_12.obf_20.Instance.SettingsVM.CurrOverlayKey = array[3];
						global::obf_12.obf_20.Instance.SettingsVM.Curr3DModeKey = array[4];
						global::obf_12.obf_20.Instance.SettingsVM.Curr3DDepthNegKey = array[5];
						global::obf_12.obf_20.Instance.SettingsVM.Curr3DDepthPosKey = array[6];
						global::obf_12.obf_20.Instance.SettingsVM.Curr3DPopoutNegKey = array[7];
						global::obf_12.obf_20.Instance.SettingsVM.Curr3DPopoutPosKey = array[8];
						global::obf_12.obf_20.Instance.SettingsVM.Language = array[9];
						global::obf_12.obf_20.Instance.SettingsVM.CursorType = int.Parse(array[10], CultureInfo.InvariantCulture);
						global::obf_12.obf_20.Instance.SettingsVM.IsConvertVideoTo3D = int.Parse(array[11], CultureInfo.InvariantCulture);
						global::obf_12.obf_20.Instance.SettingsVM.IsAutoGameScan = int.Parse(array[12], CultureInfo.InvariantCulture);
						global::obf_12.obf_20.Instance.SettingsVM.IsAutoRun = int.Parse(array[13], CultureInfo.InvariantCulture);
						global::obf_12.obf_20.Instance.SettingsVM.IsAutoConvert = int.Parse(array[14], CultureInfo.InvariantCulture);
						global::obf_12.obf_20.Instance.SettingsVM.IsStandardVideo = int.Parse(array[15], CultureInfo.InvariantCulture);
					}
					break;
				case obf_36.obf_12.obf_20:
					if (array.Length >= 5)
					{
						obf_28 u6 = (obf_28)int.Parse(array[3]);
						obf_30 u200B = (obf_30)int.Parse(array[4]);
						string text4 = array[5] ?? 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_47();
						if (u != obf_36.obf_14.obf_29)
						{
							if (u != obf_36.obf_14.obf_31)
							{
							}
						}
						else
						{
							try
							{
								obf_34.obf_12(u6, u200B, text4);
							}
							catch (ArgumentException ex)
							{
								obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_48() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_43());
							}
						}
					}
					break;
				case obf_36.obf_12.obf_21:
					if (u == obf_36.obf_14.obf_32 && array.Length >= 2 && global::obf_12.obf_20.Instance.SettingsVM != null)
					{
						global::obf_12.obf_20.Instance.SettingsVM.IsConvertVideoTo3D = int.Parse(array[2], CultureInfo.InvariantCulture);
					}
					break;
				}
			}
			catch (Exception ex2)
			{
				obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_49() + ex2.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_43());
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007AB4 File Offset: 0x00005CB4
		private static void obf_14()
		{
			obf_34.obf_12(obf_28.SPECS_CPU, obf_30.specs_cpu, obf_50.obf_12());
			obf_34.obf_12(obf_28.SPECS_RAM, obf_30.specs_ram, obf_50.obf_14());
			obf_34.obf_12(obf_28.SPECS_WINDOWSVER, obf_30.specs_windowsver, obf_50.obf_18());
			obf_34.obf_12(obf_28.SPECS_GPU, obf_30.specs_gpu, obf_50.obf_22());
		}

		// Token: 0x04000222 RID: 546
		private static string obf_12 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_51();

		// Token: 0x04000223 RID: 547
		private static string obf_14 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_52();

		// Token: 0x04000224 RID: 548
		private static string obf_18 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_53();

		// Token: 0x04000225 RID: 549
		private static ConcurrentDictionary<Guid, NamedPipeServerStream> obf_12 = new ConcurrentDictionary<Guid, NamedPipeServerStream>();

		// Token: 0x04000226 RID: 550
		private static bool obf_12 = true;

		// Token: 0x0200006C RID: 108
		[NullableContext(0)]
		public enum obf_12
		{
			// Token: 0x04000228 RID: 552
			obf_12,
			// Token: 0x04000229 RID: 553
			obf_14,
			// Token: 0x0400022A RID: 554
			obf_18,
			// Token: 0x0400022B RID: 555
			obf_22,
			// Token: 0x0400022C RID: 556
			obf_24,
			// Token: 0x0400022D RID: 557
			obf_20,
			// Token: 0x0400022E RID: 558
			obf_21
		}

		// Token: 0x0200006D RID: 109
		[NullableContext(0)]
		public enum obf_14
		{
			// Token: 0x04000230 RID: 560
			obf_12,
			// Token: 0x04000231 RID: 561
			obf_14,
			// Token: 0x04000232 RID: 562
			obf_18,
			// Token: 0x04000233 RID: 563
			obf_22,
			// Token: 0x04000234 RID: 564
			obf_24,
			// Token: 0x04000235 RID: 565
			obf_20,
			// Token: 0x04000236 RID: 566
			obf_21,
			// Token: 0x04000237 RID: 567
			obf_26,
			// Token: 0x04000238 RID: 568
			obf_19,
			// Token: 0x04000239 RID: 569
			obf_17,
			// Token: 0x0400023A RID: 570
			obf_27,
			// Token: 0x0400023B RID: 571
			obf_28,
			// Token: 0x0400023C RID: 572
			obf_29,
			// Token: 0x0400023D RID: 573
			obf_30,
			// Token: 0x0400023E RID: 574
			obf_31,
			// Token: 0x0400023F RID: 575
			obf_32,
			// Token: 0x04000240 RID: 576
			obf_33,
			// Token: 0x04000241 RID: 577
			obf_34
		}

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		[Serializable]
		private sealed class obf_18
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00007B30 File Offset: 0x00005D30
			internal void obf_12()
			{
				for (;;)
				{
					try
					{
						obf_36.obf_22 u = new obf_36.obf_22();
						u.obf_12 = new NamedPipeServerStream(obf_36.obf_18, 3, 10, 0, 1073741824);
						obf_37.obf_12(obf_36.obf_12, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_54(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_38());
						u.obf_12.WaitForConnection();
						if (u.obf_12.IsConnected)
						{
							u.obf_12 = Guid.NewGuid();
							string u00A = obf_36.obf_12;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
							defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_55());
							defaultInterpolatedStringHandler.AppendFormatted<Guid>(u.obf_12);
							obf_37.obf_12(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_38());
							obf_36.obf_12.TryAdd(u.obf_12, u.obf_12);
							Task.Run(new Func<Task>(u.obf_12));
						}
					}
					catch (Exception ex)
					{
						obf_37.obf_14(obf_36.obf_12, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_38());
					}
				}
			}

			// Token: 0x04000242 RID: 578
			[Nullable(0)]
			public static readonly obf_36.obf_18 obf_12 = new obf_36.obf_18();

			// Token: 0x04000243 RID: 579
			[Nullable(0)]
			public static Action obf_12;
		}

		// Token: 0x0200006F RID: 111
		[CompilerGenerated]
		private sealed class obf_22
		{
			// Token: 0x060001DB RID: 475 RVA: 0x00007C30 File Offset: 0x00005E30
			[NullableContext(2)]
			internal Task obf_12()
			{
				return obf_36.obf_12(this.obf_12, this.obf_12);
			}

			// Token: 0x04000244 RID: 580
			[Nullable(0)]
			public NamedPipeServerStream obf_12;

			// Token: 0x04000245 RID: 581
			public Guid obf_12;
		}

		// Token: 0x02000070 RID: 112
		[CompilerGenerated]
		private sealed class obf_24
		{
			// Token: 0x060001DD RID: 477 RVA: 0x00007C43 File Offset: 0x00005E43
			internal void obf_12()
			{
				obf_36.obf_12(this.obf_12);
			}

			// Token: 0x04000246 RID: 582
			[Nullable(0)]
			public string obf_12;
		}

		// Token: 0x02000071 RID: 113
		[CompilerGenerated]
		private sealed class obf_20
		{
			// Token: 0x060001DF RID: 479 RVA: 0x00007C50 File Offset: 0x00005E50
			internal void obf_12()
			{
				foreach (KeyValuePair<Guid, NamedPipeServerStream> keyValuePair in obf_36.obf_12)
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
								streamWriter.WriteLine(this.obf_12);
							}
						}
						catch (Exception ex)
						{
							obf_37.obf_14(obf_36.obf_12, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_40());
						}
					}
				}
			}

			// Token: 0x04000247 RID: 583
			[Nullable(0)]
			public string obf_12;
		}
	}
}
