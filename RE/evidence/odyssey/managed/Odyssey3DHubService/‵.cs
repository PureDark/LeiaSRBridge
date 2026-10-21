using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Odyssey3DHub.Dialogs;

namespace \u00A0
{
	// Token: 0x02000075 RID: 117
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2035
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000081A7 File Offset: 0x000063A7
		public static \u2035 Instance
		{
			get
			{
				return \u2035.\u00A0.Value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000081B4 File Offset: 0x000063B4
		private bool \u00A0()
		{
			do
			{
				\u202C.\u2003();
				Thread.Sleep(1000);
				this.\u2001 = (\u202C.\u2005() == 1);
				\u202C.\u2002();
				Thread.Sleep(1000);
				this.\u2000 = (\u202C.\u2004() == 1);
				this.\u1680 = (\u202C.\u2009() == 1);
				string u00A = \u2035.\u00A0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(53, 3);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2059());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2000);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205A());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2001);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205B());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u1680);
				\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205C());
			}
			while (!this.\u2000 || !this.\u2001 || !this.\u1680);
			\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205D(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205C());
			return true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000082AC File Offset: 0x000064AC
		private void \u1680()
		{
			if (\u2027.\u00A0())
			{
				this.\u2008 = true;
				\u204A.Instance.\u00A0(12);
				this.\u2009 = \u2027.\u1680();
				\u204A.Instance.\u00A0(this.\u2009 ? 14 : 15);
				return;
			}
			this.\u2008 = false;
			\u204A.Instance.\u00A0(13);
			this.\u2009 = true;
			\u204A.Instance.\u00A0(14);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008320 File Offset: 0x00006520
		private void \u2000()
		{
			do
			{
				int num = \u202C.\u2013();
				this.\u1680 = (num == 1);
			}
			while (this.\u1680);
			\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205E(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u205F());
			global::\u00A0.\u2015.Instance.\u1680();
			\u202C.\u2001();
			this.\u00A0 = false;
			this.\u1680 = false;
			this.\u2002 = false;
			\u204A.Instance.\u00A0(4);
			this.\u00A0(\u202F.\u2001, true);
			Thread.Sleep(\u2035.\u00A0);
			\u2035.Instance.\u2008();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000083A8 File Offset: 0x000065A8
		private void \u2001()
		{
			while (this.\u1680)
			{
				bool flag = \u202C.\u2006() == 1;
				if (this.\u2003 != flag)
				{
					string u00A = \u2035.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(49, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2060());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2061());
					this.\u2003 = flag;
					if (!this.\u2003)
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2062(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2061());
						\u204A.Instance.\u00A0(2);
						this.\u00A0(\u202F.\u2005, true);
					}
					else
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2063(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2061());
						\u204A.Instance.\u00A0(3);
					}
				}
				Thread.Sleep(\u2035.\u00A0);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008470 File Offset: 0x00006670
		private void \u2002()
		{
			while (this.\u1680)
			{
				bool flag = \u2027.\u00A0();
				if (this.\u2008 != flag)
				{
					string u00A = \u2035.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(38, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u2064());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206A());
					this.\u2008 = flag;
					if (!this.\u2008)
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206B(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206A());
						\u204A.Instance.\u00A0(13);
						this.\u00A0(\u202F.\u2002, true);
					}
					else
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206C(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206A());
						\u204A.Instance.\u00A0(12);
					}
				}
				Thread.Sleep(\u2035.\u00A0);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008538 File Offset: 0x00006738
		private void \u2003()
		{
			while (this.\u1680)
			{
				if (!this.\u2008)
				{
					this.\u2009 = true;
				}
				else
				{
					bool flag = \u2027.\u1680();
					if (this.\u2009 != flag)
					{
						string u00A = \u2035.\u00A0;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(28, 1);
						defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206D());
						defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
						\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206E());
						this.\u2009 = flag;
						if (!this.\u2009)
						{
							\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206F(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206E());
							\u204A.Instance.\u00A0(15);
							this.\u00A0(\u202F.\u2004, true);
						}
						else
						{
							\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u3000(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2001\u206E());
							\u204A.Instance.\u00A0(14);
						}
					}
					Thread.Sleep(\u2035.\u00A0);
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008614 File Offset: 0x00006814
		private void \u2004()
		{
			while (this.\u1680)
			{
				bool flag = \u202C.\u2007() == 1;
				if (this.\u2010 != flag)
				{
					string u00A = \u2035.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(29, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u00A0());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u1680());
					this.\u2010 = flag;
					if (this.\u2010)
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2000(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u1680());
						\u204A.Instance.\u00A0(1);
						this.\u00A0(\u202F.\u200A, true);
					}
					else
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2001(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u1680());
						\u204A.Instance.\u00A0(0);
					}
				}
				Thread.Sleep(\u2035.\u00A0);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000086DC File Offset: 0x000068DC
		private void \u2005()
		{
			while (this.\u1680)
			{
				bool flag = \u202C.\u2008() == 1;
				if (this.\u200B != flag)
				{
					string u00A = \u2035.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(25, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2002());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2003());
					this.\u200B = flag;
					if (this.\u200B)
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2004(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2003());
						\u204A.Instance.\u00A0(16);
						this.\u00A0(\u202F.\u2006, true);
					}
					else
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2005(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2003());
						\u204A.Instance.\u00A0(17);
					}
				}
				Thread.Sleep(\u2035.\u00A0);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000087A8 File Offset: 0x000069A8
		private void \u2006()
		{
			while (this.\u1680)
			{
				bool flag = \u2027.\u2000();
				if (this.\u2005 != flag)
				{
					string u00A = \u2035.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(52, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2006());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2007());
					this.\u2005 = flag;
					if (this.\u2005)
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2008(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2007());
						if (!this.\u2007)
						{
							this.\u00A0(\u202F.\u2008, true);
						}
					}
					\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2009(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2007());
				}
				Thread.Sleep(\u2035.\u00A0);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000885C File Offset: 0x00006A5C
		private void \u2007()
		{
			while (this.\u1680)
			{
				bool flag = \u202C.\u200B() == 1;
				if (this.\u2004 != flag)
				{
					string u00A = \u2035.\u00A0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(48, 1);
					defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u200A());
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u200B());
					this.\u2004 = flag;
					if (this.\u2004)
					{
						\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2010(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u200B());
						this.\u00A0(\u202F.\u2007, true);
					}
					\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2011(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u200B());
				}
				Thread.Sleep(\u2035.\u00A0);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000890C File Offset: 0x00006B0C
		private void \u00A0(\u2024 A_1)
		{
			if (A_1.\u00A0 == global::\u00A0.\u2022.\u2010)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2012(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2013());
				\u204A.\u00A0(true);
				this.\u200A = true;
				return;
			}
			if (A_1.\u00A0 == global::\u00A0.\u2022.\u2011)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2014(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2013());
				\u204A.\u00A0(false);
				this.\u200A = false;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008970 File Offset: 0x00006B70
		public void \u2008()
		{
			\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2015(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
			Task.Run(new Action(this.\u200B));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008998 File Offset: 0x00006B98
		public static void \u2009()
		{
			\u202C.\u2001();
			global::\u00A0.\u2015.Instance.\u1680();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000089AC File Offset: 0x00006BAC
		public \u202F \u200A()
		{
			int num = 0;
			while (num++ <= 10 && !this.\u00A0)
			{
				Thread.Sleep(1000);
			}
			if (!this.\u2002)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u1680.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u1680;
			}
			this.\u2004 = (\u202C.\u200B() == 1);
			if (this.\u2004)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2007.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u2007;
			}
			this.\u200B = (\u202C.\u2008() == 1);
			if (this.\u200B)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2006.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u2006;
			}
			this.\u2010 = (\u202C.\u2007() == 1);
			if (this.\u2010)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2009.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u2009;
			}
			this.\u2003 = (\u202C.\u2006() == 1);
			if (!this.\u2003)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2005.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u2005;
			}
			this.\u2005 = \u2027.\u2000();
			if (this.\u2005)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2008.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				if (!this.\u2007)
				{
					this.\u00A0(\u202F.\u2008, false);
				}
			}
			this.\u2008 = \u2027.\u00A0();
			if (!this.\u2008)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2002.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u2002;
			}
			this.\u2009 = (!this.\u2008 || \u2027.\u1680());
			if (!this.\u2009)
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2022() + \u202F.\u2003.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
				return \u202F.\u2003;
			}
			\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2025(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2024());
			return \u202F.\u00A0;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public void \u00A0(\u202F A_1, bool A_2 = false)
		{
			\u2035.\u1680 u = new \u2035.\u1680();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			if (A_2 && !this.\u200A && !\u202D.\u2002())
			{
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2027(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2028());
				return;
			}
			u.\u00A0 = u.\u00A0.\u00A0();
			string text = u.\u00A0.\u00A0();
			\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2029() + u.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2028());
			global::\u00A0.\u2013.\u00A0(global::\u00A0.\u2009.DEBUG, global::\u00A0.\u200B.debug, text);
			Application.Current.Dispatcher.Invoke(new Action(u.\u00A0));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008CA2 File Offset: 0x00006EA2
		private void \u00A0([Nullable(2)] object sender, CancelEventArgs A_2)
		{
			if (Environment.StackTrace.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u202A()))
			{
				A_2.Cancel = true;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008CF4 File Offset: 0x00006EF4
		[CompilerGenerated]
		private void \u200B()
		{
			this.\u2005 = \u2027.\u2000();
			this.\u1680();
			this.\u2006 = \u202B.\u2002();
			\u202C.\u00A0();
			\u202C.\u00A0(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u202C());
			this.\u2002 = this.\u00A0();
			\u204A.Instance.\u00A0(this.\u2002 ? 5 : 4);
			if (this.\u2002)
			{
				this.\u2004 = (\u202C.\u200B() == 1);
				this.\u2003 = (\u202C.\u2006() == 1);
				\u204A.Instance.\u00A0(this.\u2003 ? 3 : 2);
				this.\u200A = (\u202C.\u200A() == 1);
				this.\u200B = (\u202C.\u2008() == 1);
				\u204A.Instance.\u00A0(this.\u200B ? 16 : 17);
				this.\u2010 = (\u202C.\u2007() == 1);
				\u204A.Instance.\u00A0(this.\u2010 ? 1 : 0);
				string u00A = \u2035.\u00A0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(158, 7);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u202D());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2002);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u202E());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2003);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u202F());
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2032());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u200A);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2035());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u200B);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2033());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2010);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2036());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2008);
				defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u203E());
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.\u2009);
				\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
				\u2025.\u00A0(\u2035.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2047(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
				global::\u00A0.\u2015.Instance.\u00A0(new \u2015.\u00A0(this.\u00A0));
				Task.Run(new Action(this.\u2010));
				Task.Run(new Action(this.\u2011));
				Task.Run(new Action(this.\u2012));
				Task.Run(new Action(this.\u2013));
				Task.Run(new Action(this.\u2014));
				Task.Run(new Action(this.\u2015));
				Task.Run(new Action(this.\u2022));
				Task.Run(new Action(this.\u2024));
			}
			this.\u00A0 = true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008F84 File Offset: 0x00007184
		[CompilerGenerated]
		private void \u2010()
		{
			this.\u2000();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008F8C File Offset: 0x0000718C
		[CompilerGenerated]
		private void \u2011()
		{
			this.\u2001();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008F94 File Offset: 0x00007194
		[CompilerGenerated]
		private void \u2012()
		{
			this.\u2002();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008F9C File Offset: 0x0000719C
		[CompilerGenerated]
		private void \u2013()
		{
			this.\u2003();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008FA4 File Offset: 0x000071A4
		[CompilerGenerated]
		private void \u2014()
		{
			this.\u2005();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008FAC File Offset: 0x000071AC
		[CompilerGenerated]
		private void \u2015()
		{
			this.\u2004();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008FB4 File Offset: 0x000071B4
		[CompilerGenerated]
		private void \u2022()
		{
			this.\u2007();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008FBC File Offset: 0x000071BC
		[CompilerGenerated]
		private void \u2024()
		{
			this.\u2006();
		}

		// Token: 0x04000260 RID: 608
		private static readonly Lazy<\u2035> \u00A0 = new Lazy<\u2035>(new Func<\u2035>(\u2035.\u00A0.\u00A0.\u00A0));

		// Token: 0x04000261 RID: 609
		private static string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u202B();

		// Token: 0x04000262 RID: 610
		private static int \u00A0 = 1000;

		// Token: 0x04000263 RID: 611
		private static int \u1680 = 10;

		// Token: 0x04000264 RID: 612
		[Nullable(2)]
		private BaseDialog \u00A0;

		// Token: 0x04000265 RID: 613
		public bool \u00A0;

		// Token: 0x04000266 RID: 614
		public bool \u1680;

		// Token: 0x04000267 RID: 615
		public bool \u2000;

		// Token: 0x04000268 RID: 616
		public bool \u2001;

		// Token: 0x04000269 RID: 617
		public bool \u2002;

		// Token: 0x0400026A RID: 618
		public bool \u2003;

		// Token: 0x0400026B RID: 619
		public bool \u2004;

		// Token: 0x0400026C RID: 620
		public bool \u2005;

		// Token: 0x0400026D RID: 621
		public bool \u2006;

		// Token: 0x0400026E RID: 622
		public bool \u2007;

		// Token: 0x0400026F RID: 623
		public bool \u2008;

		// Token: 0x04000270 RID: 624
		public bool \u2009;

		// Token: 0x04000271 RID: 625
		public bool \u200A;

		// Token: 0x04000272 RID: 626
		public bool \u200B;

		// Token: 0x04000273 RID: 627
		public bool \u2010;

		// Token: 0x02000076 RID: 118
		[CompilerGenerated]
		[Serializable]
		private sealed class \u00A0
		{
			// Token: 0x06000203 RID: 515 RVA: 0x00008FD0 File Offset: 0x000071D0
			internal \u2035 \u00A0()
			{
				return new \u2035();
			}

			// Token: 0x04000274 RID: 628
			[Nullable(0)]
			public static readonly \u2035.\u00A0 \u00A0 = new \u2035.\u00A0();
		}

		// Token: 0x02000077 RID: 119
		[CompilerGenerated]
		private sealed class \u1680
		{
			// Token: 0x06000205 RID: 517 RVA: 0x00008FD8 File Offset: 0x000071D8
			internal void \u00A0()
			{
				if (this.\u00A0.\u00A0 != null)
				{
					this.\u00A0.\u00A0.Close();
				}
				if (this.\u00A0 == \u202F.\u2008)
				{
					global::\u00A0.\u00A0 @default = global::\u00A0.\u00A0.Default;
					if (@default.Multi3DPopupCount >= 2 || !@default.MultipleDevicePopupShow)
					{
						string u00A = \u2035.\u00A0;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(66, 2);
						defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2061());
						defaultInterpolatedStringHandler.AppendFormatted<int>(@default.Multi3DPopupCount);
						defaultInterpolatedStringHandler.AppendLiteral(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2062());
						defaultInterpolatedStringHandler.AppendFormatted<bool>(@default.MultipleDevicePopupShow);
						\u2025.\u00A0(u00A, defaultInterpolatedStringHandler.ToStringAndClear(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2028());
						return;
					}
					global::\u00A0.\u00A0 u00A2 = @default;
					int multi3DPopupCount = u00A2.Multi3DPopupCount;
					u00A2.Multi3DPopupCount = multi3DPopupCount + 1;
					@default.Save();
					this.\u00A0.\u00A0 = new CheckBoxDialog(this.\u00A0);
				}
				else
				{
					this.\u00A0.\u00A0 = new OkDialog(this.\u00A0);
				}
				if (this.\u00A0.\u00A0 != null && Screen.PrimaryScreen != null)
				{
					global::\u00A0.\u2007.\u2000();
					this.\u00A0.\u00A0.PositionWindowAtCenter();
					this.\u00A0.\u00A0.SetTimeout(\u2035.\u1680);
					this.\u00A0.\u00A0.Topmost = true;
					this.\u00A0.\u00A0.Closing += new CancelEventHandler(this.\u00A0.\u00A0);
					this.\u00A0.\u00A0.Show();
				}
			}

			// Token: 0x04000275 RID: 629
			[Nullable(0)]
			public \u2035 \u00A0;

			// Token: 0x04000276 RID: 630
			public \u202F \u00A0;

			// Token: 0x04000277 RID: 631
			[Nullable(0)]
			public string \u00A0;
		}
	}
}
