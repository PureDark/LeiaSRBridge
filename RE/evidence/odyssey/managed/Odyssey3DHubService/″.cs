using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Odyssey3DHub.Dialogs;

namespace \u00A0
{
	// Token: 0x02000078 RID: 120
	[NullableContext(1)]
	[Nullable(0)]
	public class \u2033
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00009140 File Offset: 0x00007340
		public void \u00A0()
		{
			\u2025.\u00A0(this.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2048());
			this.\u00A0 = true;
			if (this.\u00A0 == IntPtr.Zero)
			{
				this.\u00A0 = new \u202D.\u00A0(this.\u00A0);
				this.\u00A0 = \u202D.\u00A0(this.\u1680, this.\u1680, IntPtr.Zero, this.\u00A0, 0U, 0U, this.\u00A0);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000091B0 File Offset: 0x000073B0
		public void \u1680()
		{
			\u2025.\u00A0(this.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2049());
			if (this.\u00A0 != IntPtr.Zero)
			{
				\u202D.\u2001(this.\u00A0);
				this.\u00A0 = IntPtr.Zero;
				this.\u00A0 = null;
				this.\u00A0 = false;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000091FF File Offset: 0x000073FF
		public void \u2000()
		{
			\u2025.\u00A0(this.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204A());
			this.\u00A0 = false;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009218 File Offset: 0x00007418
		public void \u00A0(IntPtr A_1, uint A_2, IntPtr A_3, int A_4, int A_5, uint A_6, uint A_7)
		{
			try
			{
				bool flag = \u204A.Instance.\u2001().Count > 0;
				bool flag2 = global::\u00A0.\u2003.Instance.SettingsVM.IsConvertVideoTo3D == 0;
				if (!flag && this.\u00A0 && !flag2)
				{
					if (\u202D.\u2003(A_3))
					{
						IntPtr intPtr = \u202D.\u2000();
						if (A_3 == intPtr)
						{
							string text = \u202D.\u2005(A_3);
							if (!string.IsNullOrEmpty(text) && !text.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2050()) && !text.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204B()) && !text.Contains(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204C()))
							{
								int num = \u202D.\u2000(A_3, \u202D.\u00A0);
								bool flag3 = ((long)num & (long)((ulong)\u202D.\u2002)) == 0L && ((long)num & (long)((ulong)\u202D.\u2003)) == 0L;
								if (this.\u1680(A_3) && flag3 && A_4 != -4)
								{
									\u202F u202F = \u2035.Instance.\u200A();
									if (u202F != \u202F.\u00A0)
									{
										\u2025.\u00A0(this.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204D() + u202F.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204E());
									}
									else if (!this.\u00A0.ContainsKey(A_3) || !this.\u00A0[A_3])
									{
										this.\u00A0.TryAdd(A_3, true);
										if (Screen.PrimaryScreen != null)
										{
											this.\u1680 = A_3;
											this.\u2000(A_3);
										}
									}
								}
								else if (this.\u00A0.ContainsKey(A_3) && !\u202D.\u1680(A_3))
								{
									this.\u00A0.Remove(A_3);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				\u2025.\u1680(this.\u00A0, ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204E());
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000093D0 File Offset: 0x000075D0
		private void \u00A0(IntPtr A_1)
		{
			int num = \u202D.\u2000(A_1, \u202D.\u1680);
			\u202D.\u00A0(A_1, \u202D.\u1680, (uint)(num | (int)\u202D.\u00A0 | (int)\u202D.\u2000));
			while (\u202D.\u2004(A_1))
			{
				\u202D.\u00A0(A_1, \u202D.\u1680, 0, 0, 0, 0, \u202D.\u2005 | \u202D.\u2004 | \u202D.\u2008);
				\u202D.\u00A0(A_1, \u202D.\u2002);
				\u202D.\u2000(A_1);
				if (this.\u1680 != IntPtr.Zero && (!\u202D.\u2004(this.\u1680) || !this.\u1680(this.\u1680)))
				{
					Application.Current.Dispatcher.Invoke(new Action(this.\u2002));
				}
				Task.Delay(500).Wait();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009498 File Offset: 0x00007698
		private bool \u1680(IntPtr A_1)
		{
			\u2027.\u1680 u = default(\u2027.\u1680);
			u.\u00A0 = Marshal.SizeOf<\u2027.\u1680>(u);
			\u2027.\u00A0(\u2027.\u00A0(A_1, 1), ref u);
			\u2027.\u00A0 u00A;
			\u202D.\u00A0(A_1, out u00A);
			if (((ulong)u.\u00A0 & (ulong)((long)\u2027.\u00A0)) == 0UL)
			{
				return false;
			}
			int num = u00A.\u2000 - u00A.\u00A0;
			int num2 = u00A.\u2001 - u00A.\u1680;
			int num3 = u.\u00A0.\u2000 - u.\u00A0.\u00A0;
			int num4 = u.\u00A0.\u2001 - u.\u00A0.\u1680;
			int num5 = num3 - this.\u1680;
			int num6 = num4 - this.\u1680;
			int num7 = num3 + this.\u1680;
			int num8 = num4 + this.\u1680;
			return num2 >= num6 && num2 <= num8 && num >= num5 && num <= num7 && u00A.\u1680 == 0;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009578 File Offset: 0x00007778
		private void \u2000(IntPtr A_1)
		{
			while (this.\u2001(A_1))
			{
				this.\u2002(A_1);
				if (global::\u00A0.\u2003.Instance.SettingsVM.IsAutoConvert == 1)
				{
					this.\u2003(A_1);
					return;
				}
				Odyssey3DHubDialogResult odyssey3DHubDialogResult = this.\u2001();
				if (odyssey3DHubDialogResult != Odyssey3DHubDialogResult.Help)
				{
					this.\u00A0(A_1, odyssey3DHubDialogResult);
					return;
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000095C5 File Offset: 0x000077C5
		private bool \u2001(IntPtr A_1)
		{
			return A_1 != IntPtr.Zero && \u202D.\u2004(A_1) && this.\u1680(A_1);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000095E0 File Offset: 0x000077E0
		private void \u2002(IntPtr A_1)
		{
			Size u00A = \u2006.\u00A0;
			\u202D.\u00A0(A_1, \u202D.\u00A0, 0, 0, (int)u00A.Width, (int)u00A.Height, \u202D.\u2006 | \u202D.\u2007);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000961C File Offset: 0x0000781C
		private void \u2003(IntPtr A_1)
		{
			\u2033.\u00A0 u00A = new \u2033.\u00A0();
			u00A.\u00A0 = A_1;
			\u202E.\u00A0(\u202E.\u00A0.\u2000, \u202E.\u1680.\u2005, u00A.\u00A0.ToInt64().ToString());
			u00A.\u00A0 = (!\u2035.Instance.\u2006 || global::\u00A0.\u2003.Instance.SettingsVM.IsStandardVideo != 1);
			Task.Run(new Action(u00A.\u00A0));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000968C File Offset: 0x0000788C
		private void \u00A0(IntPtr A_1, Odyssey3DHubDialogResult A_2)
		{
			\u2033.\u1680 u = new \u2033.\u1680();
			u.\u00A0 = A_1;
			if (A_2 == Odyssey3DHubDialogResult.Converion2D || A_2 == Odyssey3DHubDialogResult.ConversionSBS)
			{
				\u202E.\u00A0(\u202E.\u00A0.\u2000, \u202E.\u1680.\u2005, u.\u00A0.ToInt64().ToString());
				u.\u00A0 = (A_2 == Odyssey3DHubDialogResult.ConversionSBS);
				Task.Run(new Action(u.\u00A0));
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000096E4 File Offset: 0x000078E4
		private Odyssey3DHubDialogResult \u2001()
		{
			if (this.\u00A0 != null)
			{
				this.\u00A0.Close();
			}
			if (\u2035.Instance.\u2006)
			{
				this.\u00A0 = new ConversionDialog();
			}
			else
			{
				this.\u00A0 = new ConversionDialogForOnlySBS();
			}
			this.\u00A0.PositionWindowAtBottomRight();
			this.\u00A0.SetTimeout(this.\u00A0);
			this.\u00A0.Title = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u204F();
			this.\u00A0.Loaded += new RoutedEventHandler(this.\u00A0);
			this.\u00A0.ShowDialog();
			bool? dialogResult = this.\u00A0.DialogResult;
			return this.\u00A0.Result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009790 File Offset: 0x00007990
		private void \u00A0(object A_1, RoutedEventArgs A_2)
		{
			\u2033.\u2000 u = new \u2033.\u2000();
			u.\u00A0 = this;
			Window window = A_1 as Window;
			if (window != null)
			{
				WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
				u.\u00A0 = windowInteropHelper.Handle;
				Task.Run(new Action(u.\u00A0));
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009835 File Offset: 0x00007A35
		[CompilerGenerated]
		private void \u2002()
		{
			BaseDialog u00A = this.\u00A0;
			if (u00A == null)
			{
				return;
			}
			u00A.Close();
		}

		// Token: 0x04000278 RID: 632
		private readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2002\u2050();

		// Token: 0x04000279 RID: 633
		private readonly uint \u00A0;

		// Token: 0x0400027A RID: 634
		private readonly uint \u1680 = 32779U;

		// Token: 0x0400027B RID: 635
		private readonly int \u00A0 = 10;

		// Token: 0x0400027C RID: 636
		private readonly int \u1680 = 2;

		// Token: 0x0400027D RID: 637
		private bool \u00A0;

		// Token: 0x0400027E RID: 638
		private IntPtr \u00A0 = IntPtr.Zero;

		// Token: 0x0400027F RID: 639
		[Nullable(2)]
		private \u202D.\u00A0 \u00A0;

		// Token: 0x04000280 RID: 640
		private IntPtr \u1680 = IntPtr.Zero;

		// Token: 0x04000281 RID: 641
		private Dictionary<IntPtr, bool> \u00A0 = new Dictionary<IntPtr, bool>();

		// Token: 0x04000282 RID: 642
		[Nullable(2)]
		private BaseDialog \u00A0;

		// Token: 0x02000079 RID: 121
		[CompilerGenerated]
		private sealed class \u00A0
		{
			// Token: 0x06000216 RID: 534 RVA: 0x00009847 File Offset: 0x00007A47
			internal void \u00A0()
			{
				\u204A.Instance.\u00A0(this.\u00A0, this.\u00A0);
			}

			// Token: 0x04000283 RID: 643
			public IntPtr \u00A0;

			// Token: 0x04000284 RID: 644
			public bool \u00A0;
		}

		// Token: 0x0200007A RID: 122
		[CompilerGenerated]
		private sealed class \u1680
		{
			// Token: 0x06000218 RID: 536 RVA: 0x0000985F File Offset: 0x00007A5F
			internal void \u00A0()
			{
				\u204A.Instance.\u00A0(this.\u00A0, this.\u00A0);
			}

			// Token: 0x04000285 RID: 645
			public IntPtr \u00A0;

			// Token: 0x04000286 RID: 646
			public bool \u00A0;
		}

		// Token: 0x0200007B RID: 123
		[CompilerGenerated]
		private sealed class \u2000
		{
			// Token: 0x0600021A RID: 538 RVA: 0x00009877 File Offset: 0x00007A77
			internal void \u00A0()
			{
				this.\u00A0.\u00A0(this.\u00A0);
			}

			// Token: 0x04000287 RID: 647
			[Nullable(0)]
			public \u2033 \u00A0;

			// Token: 0x04000288 RID: 648
			public IntPtr \u00A0;
		}
	}
}
