// Original: RE\evidence\odyssey\managed\Odyssey3DHubService\‵.cs
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Odyssey3DHub.Dialogs;

namespace obf_1
{
	// Token: 0x02000075 RID: 117
	[NullableContext(1)]
	[Nullable(0)]
	public class obf_104
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000081A7 File Offset: 0x000063A7
		public static obf_104 Instance
		{
			get
			{
				return obf_104.obf_1.Value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000081B4 File Offset: 0x000063B4
		private bool obf_1()
		{
			do
			{
				obf_94.obf_7();
				Thread.Sleep(1000);
				this.obf_5 = (obf_94.obf_9() == 1);
				obf_94.obf_6();
				Thread.Sleep(1000);
				this.obf_4 = (obf_94.obf_8() == 1);
				this.obf_3 = (obf_94.obf_96() == 1);
				string u00A = obf_104.obf_1;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(53, 3);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_105());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_4);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_106());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_5);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_107());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_3);
				obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_108());
			}
			while (!this.obf_4 || !this.obf_5 || !this.obf_3);
			obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_109(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_108());
			return true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000082AC File Offset: 0x000064AC
		private void obf_3()
		{
			if (obf_25.obf_1())
			{
				this.obf_95 = true;
				obf_110.Instance.obf_1(12);
				this.obf_96 = obf_25.obf_3();
				obf_110.Instance.obf_1(this.obf_96 ? 14 : 15);
				return;
			}
			this.obf_95 = false;
			obf_110.Instance.obf_1(13);
			this.obf_96 = true;
			obf_110.Instance.obf_1(14);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008320 File Offset: 0x00006520
		private void obf_4()
		{
			do
			{
				int num = obf_94.obf_102();
				this.obf_3 = (num == 1);
			}
			while (this.obf_3);
			obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_111(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_112());
			global::obf_1.obf_2.Instance.obf_3();
			obf_94.obf_5();
			this.obf_1 = false;
			this.obf_3 = false;
			this.obf_6 = false;
			obf_110.Instance.obf_1(4);
			this.obf_1(obf_113.obf_5, true);
			Thread.Sleep(obf_104.obf_1);
			obf_104.Instance.obf_95();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000083A8 File Offset: 0x000065A8
		private void obf_5()
		{
			while (this.obf_3)
			{
				bool flag = obf_94.obf_12() == 1;
				if (this.obf_7 != flag)
				{
					string u00A = obf_104.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(49, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_114());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_115());
					this.obf_7 = flag;
					if (!this.obf_7)
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_116(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_115());
						obf_110.Instance.obf_1(2);
						this.obf_1(obf_113.obf_9, true);
					}
					else
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_117(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_115());
						obf_110.Instance.obf_1(3);
					}
				}
				Thread.Sleep(obf_104.obf_1);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008470 File Offset: 0x00006670
		private void obf_6()
		{
			while (this.obf_3)
			{
				bool flag = obf_25.obf_1();
				if (this.obf_95 != flag)
				{
					string u00A = obf_104.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(38, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_118());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_119());
					this.obf_95 = flag;
					if (!this.obf_95)
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_120(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_119());
						obf_110.Instance.obf_1(13);
						this.obf_1(obf_113.obf_6, true);
					}
					else
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_121(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_119());
						obf_110.Instance.obf_1(12);
					}
				}
				Thread.Sleep(obf_104.obf_1);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008538 File Offset: 0x00006738
		private void obf_7()
		{
			while (this.obf_3)
			{
				if (!this.obf_95)
				{
					this.obf_96 = true;
				}
				else
				{
					bool flag = obf_25.obf_3();
					if (this.obf_96 != flag)
					{
						string u00A = obf_104.obf_1;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(28, 1);
						defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_122());
						defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
						obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_123());
						this.obf_96 = flag;
						if (!this.obf_96)
						{
							obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_124(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_123());
							obf_110.Instance.obf_1(15);
							this.obf_1(obf_113.obf_8, true);
						}
						else
						{
							obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_125(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_123());
							obf_110.Instance.obf_1(14);
						}
					}
					Thread.Sleep(obf_104.obf_1);
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008614 File Offset: 0x00006814
		private void obf_8()
		{
			while (this.obf_3)
			{
				bool flag = obf_94.obf_28() == 1;
				if (this.obf_99 != flag)
				{
					string u00A = obf_104.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(29, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_126());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_127());
					this.obf_99 = flag;
					if (this.obf_99)
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_128(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_127());
						obf_110.Instance.obf_1(1);
						this.obf_1(obf_113.obf_97, true);
					}
					else
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_129(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_127());
						obf_110.Instance.obf_1(0);
					}
				}
				Thread.Sleep(obf_104.obf_1);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000086DC File Offset: 0x000068DC
		private void obf_9()
		{
			while (this.obf_3)
			{
				bool flag = obf_94.obf_95() == 1;
				if (this.obf_98 != flag)
				{
					string u00A = obf_104.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(25, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_130());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_131());
					this.obf_98 = flag;
					if (this.obf_98)
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_132(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_131());
						obf_110.Instance.obf_1(16);
						this.obf_1(obf_113.obf_12, true);
					}
					else
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_133(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_131());
						obf_110.Instance.obf_1(17);
					}
				}
				Thread.Sleep(obf_104.obf_1);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000087A8 File Offset: 0x000069A8
		private void obf_12()
		{
			while (this.obf_3)
			{
				bool flag = obf_25.obf_4();
				if (this.obf_9 != flag)
				{
					string u00A = obf_104.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(52, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_134());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_135());
					this.obf_9 = flag;
					if (this.obf_9)
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_136(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_135());
						if (!this.obf_28)
						{
							this.obf_1(obf_113.obf_95, true);
						}
					}
					obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_137(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_135());
				}
				Thread.Sleep(obf_104.obf_1);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000885C File Offset: 0x00006A5C
		private void obf_28()
		{
			while (this.obf_3)
			{
				bool flag = obf_94.obf_98() == 1;
				if (this.obf_8 != flag)
				{
					string u00A = obf_104.obf_1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(48, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_138());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_139());
					this.obf_8 = flag;
					if (this.obf_8)
					{
						obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_140(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_139());
						this.obf_1(obf_113.obf_28, true);
					}
					obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_141(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_139());
				}
				Thread.Sleep(obf_104.obf_1);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000890C File Offset: 0x00006B0C
		private void obf_1(obf_10 A_1)
		{
			if (A_1.obf_1 == global::obf_1.obf_142.obf_99)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_143(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_144());
				obf_110.obf_1(true);
				this.obf_97 = true;
				return;
			}
			if (A_1.obf_1 == global::obf_1.obf_142.obf_100)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_145(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_144());
				obf_110.obf_1(false);
				this.obf_97 = false;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008970 File Offset: 0x00006B70
		public void obf_95()
		{
			obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_146(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_8());
			Task.Run(new Action(this.obf_98));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008998 File Offset: 0x00006B98
		public static void obf_96()
		{
			obf_94.obf_5();
			global::obf_1.obf_2.Instance.obf_3();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000089AC File Offset: 0x00006BAC
		public obf_113 obf_97()
		{
			int num = 0;
			while (num++ <= 10 && !this.obf_1)
			{
				Thread.Sleep(1000);
			}
			if (!this.obf_6)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_3.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_3;
			}
			this.obf_8 = (obf_94.obf_98() == 1);
			if (this.obf_8)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_28.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_28;
			}
			this.obf_98 = (obf_94.obf_95() == 1);
			if (this.obf_98)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_12.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_12;
			}
			this.obf_99 = (obf_94.obf_28() == 1);
			if (this.obf_99)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_96.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_96;
			}
			this.obf_7 = (obf_94.obf_12() == 1);
			if (!this.obf_7)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_9.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_9;
			}
			this.obf_9 = obf_25.obf_4();
			if (this.obf_9)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_95.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				if (!this.obf_28)
				{
					this.obf_1(obf_113.obf_95, false);
				}
			}
			this.obf_95 = obf_25.obf_1();
			if (!this.obf_95)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_6.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_6;
			}
			this.obf_96 = (!this.obf_95 || obf_25.obf_3());
			if (!this.obf_96)
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_147() + obf_113.obf_7.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
				return obf_113.obf_7;
			}
			obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_149(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_148());
			return obf_113.obf_1;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public void obf_1(obf_113 A_1, bool A_2 = false)
		{
			obf_104.obf_3 u = new obf_104.obf_3();
			u.obf_1 = this;
			u.obf_1 = A_1;
			if (A_2 && !this.obf_97 && !obf_29.obf_6())
			{
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_150(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_151());
				return;
			}
			u.obf_1 = u.obf_1.obf_1();
			string text = u.obf_1.obf_1();
			obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_152() + u.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_151());
			global::obf_1.obf_102.obf_1(global::obf_1.obf_96.DEBUG, global::obf_1.obf_98.debug, text);
			Application.Current.Dispatcher.Invoke(new Action(u.obf_1));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008CA2 File Offset: 0x00006EA2
		private void obf_1([Nullable(2)] object sender, CancelEventArgs A_2)
		{
			if (Environment.StackTrace.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_153()))
			{
				A_2.Cancel = true;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008CF4 File Offset: 0x00006EF4
		[CompilerGenerated]
		private void obf_98()
		{
			this.obf_9 = obf_25.obf_4();
			this.obf_3();
			this.obf_12 = obf_30.obf_6();
			obf_94.obf_1();
			obf_94.obf_1(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_154());
			this.obf_6 = this.obf_1();
			obf_110.Instance.obf_1(this.obf_6 ? 5 : 4);
			if (this.obf_6)
			{
				this.obf_8 = (obf_94.obf_98() == 1);
				this.obf_7 = (obf_94.obf_12() == 1);
				obf_110.Instance.obf_1(this.obf_7 ? 3 : 2);
				this.obf_97 = (obf_94.obf_97() == 1);
				this.obf_98 = (obf_94.obf_95() == 1);
				obf_110.Instance.obf_1(this.obf_98 ? 16 : 17);
				this.obf_99 = (obf_94.obf_28() == 1);
				obf_110.Instance.obf_1(this.obf_99 ? 1 : 0);
				string u00A = obf_104.obf_1;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(158, 7);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_155());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_6);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_156());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_7);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_157());
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_158());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_97);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_159());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_98);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_160());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_99);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_161());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_95);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_162());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.obf_96);
				obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_8());
				obf_11.obf_1(obf_104.obf_1, 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_163(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_8());
				global::obf_1.obf_2.Instance.obf_1(new obf_2.obf_1(this.obf_1));
				Task.Run(new Action(this.obf_99));
				Task.Run(new Action(this.obf_100));
				Task.Run(new Action(this.obf_101));
				Task.Run(new Action(this.obf_102));
				Task.Run(new Action(this.obf_103));
				Task.Run(new Action(this.obf_2));
				Task.Run(new Action(this.obf_142));
				Task.Run(new Action(this.obf_10));
			}
			this.obf_1 = true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008F84 File Offset: 0x00007184
		[CompilerGenerated]
		private void obf_99()
		{
			this.obf_4();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008F8C File Offset: 0x0000718C
		[CompilerGenerated]
		private void obf_100()
		{
			this.obf_5();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008F94 File Offset: 0x00007194
		[CompilerGenerated]
		private void obf_101()
		{
			this.obf_6();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008F9C File Offset: 0x0000719C
		[CompilerGenerated]
		private void obf_102()
		{
			this.obf_7();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008FA4 File Offset: 0x000071A4
		[CompilerGenerated]
		private void obf_103()
		{
			this.obf_9();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008FAC File Offset: 0x000071AC
		[CompilerGenerated]
		private void obf_2()
		{
			this.obf_8();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008FB4 File Offset: 0x000071B4
		[CompilerGenerated]
		private void obf_142()
		{
			this.obf_28();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008FBC File Offset: 0x000071BC
		[CompilerGenerated]
		private void obf_10()
		{
			this.obf_12();
		}

		// Token: 0x04000260 RID: 608
		private static readonly Lazy<obf_104> obf_1 = new Lazy<obf_104>(new Func<obf_104>(obf_104.obf_1.obf_1.obf_1));

		// Token: 0x04000261 RID: 609
		private static string obf_1 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_164();

		// Token: 0x04000262 RID: 610
		private static int obf_1 = 1000;

		// Token: 0x04000263 RID: 611
		private static int obf_3 = 10;

		// Token: 0x04000264 RID: 612
		[Nullable(2)]
		private BaseDialog obf_1;

		// Token: 0x04000265 RID: 613
		public bool obf_1;

		// Token: 0x04000266 RID: 614
		public bool obf_3;

		// Token: 0x04000267 RID: 615
		public bool obf_4;

		// Token: 0x04000268 RID: 616
		public bool obf_5;

		// Token: 0x04000269 RID: 617
		public bool obf_6;

		// Token: 0x0400026A RID: 618
		public bool obf_7;

		// Token: 0x0400026B RID: 619
		public bool obf_8;

		// Token: 0x0400026C RID: 620
		public bool obf_9;

		// Token: 0x0400026D RID: 621
		public bool obf_12;

		// Token: 0x0400026E RID: 622
		public bool obf_28;

		// Token: 0x0400026F RID: 623
		public bool obf_95;

		// Token: 0x04000270 RID: 624
		public bool obf_96;

		// Token: 0x04000271 RID: 625
		public bool obf_97;

		// Token: 0x04000272 RID: 626
		public bool obf_98;

		// Token: 0x04000273 RID: 627
		public bool obf_99;

		// Token: 0x02000076 RID: 118
		[CompilerGenerated]
		[Serializable]
		private sealed class obf_1
		{
			// Token: 0x06000203 RID: 515 RVA: 0x00008FD0 File Offset: 0x000071D0
			internal obf_104 obf_1()
			{
				return new obf_104();
			}

			// Token: 0x04000274 RID: 628
			[Nullable(0)]
			public static readonly obf_104.obf_1 obf_1 = new obf_104.obf_1();
		}

		// Token: 0x02000077 RID: 119
		[CompilerGenerated]
		private sealed class obf_3
		{
			// Token: 0x06000205 RID: 517 RVA: 0x00008FD8 File Offset: 0x000071D8
			internal void obf_1()
			{
				if (this.obf_1.obf_1 != null)
				{
					this.obf_1.obf_1.Close();
				}
				if (this.obf_1 == obf_113.obf_95)
				{
					global::obf_1.obf_1 @default = global::obf_1.obf_1.Default;
					if (@default.Multi3DPopupCount >= 2 || !@default.MultipleDevicePopupShow)
					{
						string u00A = obf_104.obf_1;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(66, 2);
						defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_165());
						defaultInterpolatedStringHandler.AppendFormatted<int>(@default.Multi3DPopupCount);
						defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_166());
						defaultInterpolatedStringHandler.AppendFormatted<bool>(@default.MultipleDevicePopupShow);
						obf_11.obf_1(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.obf_151());
						return;
					}
					global::obf_1.obf_1 u00A2 = @default;
					int multi3DPopupCount = u00A2.Multi3DPopupCount;
					u00A2.Multi3DPopupCount = multi3DPopupCount + 1;
					@default.Save();
					this.obf_1.obf_1 = new CheckBoxDialog(this.obf_1);
				}
				else
				{
					this.obf_1.obf_1 = new OkDialog(this.obf_1);
				}
				if (this.obf_1.obf_1 != null && Screen.PrimaryScreen != null)
				{
					global::obf_1.obf_28.obf_4();
					this.obf_1.obf_1.PositionWindowAtCenter();
					this.obf_1.obf_1.SetTimeout(obf_104.obf_3);
					this.obf_1.obf_1.Topmost = true;
					this.obf_1.obf_1.Closing += new CancelEventHandler(this.obf_1.obf_1);
					this.obf_1.obf_1.Show();
				}
			}

			// Token: 0x04000275 RID: 629
			[Nullable(0)]
			public obf_104 obf_1;

			// Token: 0x04000276 RID: 630
			public obf_113 obf_1;

			// Token: 0x04000277 RID: 631
			[Nullable(0)]
			public string obf_1;
		}
	}
}
