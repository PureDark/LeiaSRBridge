using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Resources;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Hardcodet.Wpf.TaskbarNotification;
using Odyssey3DHub;
using Odyssey3DHub.ViewModels;

namespace \u00A0
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(0)]
	public class \u1680
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002390 File Offset: 0x00000590
		public static void \u00A0()
		{
			\u2025.\u00A0(global::\u00A0.\u1680.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
			\u2025.\u00A0(global::\u00A0.\u1680.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2005(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2004());
			global::\u00A0.\u1680.\u2000();
			global::\u00A0.\u1680.\u2001();
			Action action;
			if ((action = global::\u00A0.\u1680.\u1680.\u00A0) == null)
			{
				action = (global::\u00A0.\u1680.\u1680.\u00A0 = new Action(global::\u00A0.\u1680.\u1680));
			}
			\u2014.\u00A0(action);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023EA File Offset: 0x000005EA
		private static void \u1680()
		{
			global::\u00A0.\u1680.\u2001();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023F4 File Offset: 0x000005F4
		private static void \u2000()
		{
			global::\u00A0.\u1680.\u00A0 = new TaskbarIcon
			{
				Icon = SystemIcons.Application
			};
			ToolTip toolTip = new ToolTip();
			toolTip.Content = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2006();
			toolTip.Background = Brushes.White;
			toolTip.Placement = 11;
			CustomPopupPlacementCallback customPopupPlacementCallback;
			if ((customPopupPlacementCallback = global::\u00A0.\u1680.\u1680.\u00A0) == null)
			{
				customPopupPlacementCallback = (global::\u00A0.\u1680.\u1680.\u00A0 = new CustomPopupPlacementCallback(global::\u00A0.\u1680.\u00A0));
			}
			toolTip.CustomPopupPlacementCallback = customPopupPlacementCallback;
			ToolTip trayToolTip = toolTip;
			global::\u00A0.\u1680.\u00A0.TrayToolTip = trayToolTip;
			try
			{
				StreamResourceInfo resourceStream = Application.GetResourceStream(new Uri(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2007()));
				Stream stream = (resourceStream != null) ? resourceStream.Stream : null;
				if (stream != null)
				{
					using (Bitmap bitmap = new Bitmap(stream))
					{
						global::\u00A0.\u1680.\u00A0.Icon = Icon.FromHandle(bitmap.GetHicon());
					}
					stream.Dispose();
				}
			}
			catch (Exception ex)
			{
				\u2025.\u1680(global::\u00A0.\u1680.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2008() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2009());
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024F4 File Offset: 0x000006F4
		private static void \u2001()
		{
			\u2000 u = new \u2000();
			u.DataContext = TrayVM.Instance;
			if (global::\u00A0.\u1680.\u00A0 != null)
			{
				global::\u00A0.\u1680.\u00A0.ContextMenu = u;
				TaskbarIcon u00A = global::\u00A0.\u1680.\u00A0;
				RoutedEventHandler value;
				if ((value = global::\u00A0.\u1680.\u1680.\u00A0) == null)
				{
					value = (global::\u00A0.\u1680.\u1680.\u00A0 = new RoutedEventHandler(global::\u00A0.\u1680.\u00A0));
				}
				u00A.TrayLeftMouseDown += value;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002549 File Offset: 0x00000749
		private static void \u00A0([Nullable(2)] object sender, RoutedEventArgs A_1)
		{
			TrayVM.Instance.RunCommand.Execute();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000255C File Offset: 0x0000075C
		public static void \u00A0(global::\u00A0.\u1680.\u00A0 A_0)
		{
			switch (A_0)
			{
			case global::\u00A0.\u1680.\u00A0.\u00A0:
				TrayVM.Instance.IsConvertTo3DEnabled = (global::\u00A0.\u2003.Instance.SettingsVM.IsConvertVideoTo3D == 1);
				global::\u00A0.\u1680.\u00A0(\u202E.\u1680.\u2011, global::\u00A0.\u2003.Instance.SettingsVM.IsConvertVideoTo3D.ToString());
				break;
			case global::\u00A0.\u1680.\u00A0.\u1680:
			{
				bool isAutoConvertEnabled = global::\u00A0.\u2003.Instance.SettingsVM.IsAutoConvert == 1;
				TrayVM.Instance.IsAutoConvertEnabled = isAutoConvertEnabled;
				global::\u00A0.\u1680.\u00A0(\u202E.\u1680.\u2012, global::\u00A0.\u2003.Instance.SettingsVM.IsAutoConvert.ToString());
				break;
			}
			case global::\u00A0.\u1680.\u00A0.\u2000:
			{
				bool isStandardVideo = global::\u00A0.\u2003.Instance.SettingsVM.IsStandardVideo == 1;
				TrayVM.Instance.IsStandardVideo = isStandardVideo;
				global::\u00A0.\u1680.\u00A0(\u202E.\u1680.\u2013, global::\u00A0.\u2003.Instance.SettingsVM.IsStandardVideo.ToString());
				break;
			}
			}
			\u2005.\u2001();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000263B File Offset: 0x0000083B
		private static void \u00A0(\u202E.\u1680 A_0, string A_1)
		{
			\u202E.\u00A0(\u202E.\u00A0.\u2004, A_0, A_1);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002645 File Offset: 0x00000845
		public static void \u2002()
		{
			if (global::\u00A0.\u1680.\u00A0 != null)
			{
				TaskbarIcon u00A = global::\u00A0.\u1680.\u00A0;
				RoutedEventHandler value;
				if ((value = global::\u00A0.\u1680.\u1680.\u00A0) == null)
				{
					value = (global::\u00A0.\u1680.\u1680.\u00A0 = new RoutedEventHandler(global::\u00A0.\u1680.\u00A0));
				}
				u00A.TrayLeftMouseDown -= value;
				global::\u00A0.\u1680.\u00A0.Dispose();
				global::\u00A0.\u1680.\u00A0 = null;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002684 File Offset: 0x00000884
		private static Point \u2003()
		{
			Point mousePosition = Control.MousePosition;
			return new Point((double)mousePosition.X, (double)mousePosition.Y);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026AC File Offset: 0x000008AC
		private static CustomPopupPlacement[] \u00A0(Size A_0, Size A_1, Point A_2)
		{
			Point point = global::\u00A0.\u1680.\u2003();
			double num = point.X - A_0.Width / 2.0;
			double num2 = point.Y - A_0.Height - 25.0;
			Point point2;
			point2..ctor(num, num2);
			return new CustomPopupPlacement[]
			{
				new CustomPopupPlacement(point2, 2)
			};
		}

		// Token: 0x04000007 RID: 7
		private static readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u200A();

		// Token: 0x04000008 RID: 8
		[Nullable(2)]
		private static TaskbarIcon \u00A0 = null;

		// Token: 0x02000008 RID: 8
		[NullableContext(0)]
		public enum \u00A0
		{
			// Token: 0x0400000A RID: 10
			\u00A0,
			// Token: 0x0400000B RID: 11
			\u1680,
			// Token: 0x0400000C RID: 12
			\u2000
		}

		// Token: 0x02000009 RID: 9
		[CompilerGenerated]
		private static class \u1680
		{
			// Token: 0x0400000D RID: 13
			[Nullable(0)]
			public static Action \u00A0;

			// Token: 0x0400000E RID: 14
			[Nullable(0)]
			public static CustomPopupPlacementCallback \u00A0;

			// Token: 0x0400000F RID: 15
			[Nullable(0)]
			public static RoutedEventHandler \u00A0;
		}
	}
}
