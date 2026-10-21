using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};
using Odyssey3DHub;
using Odyssey3DHub.ViewModels;

namespace \u00A0
{
	// Token: 0x0200000A RID: 10
	public class \u2000 : ContextMenu, IComponentConnector
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000272A File Offset: 0x0000092A
		public \u2000()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002738 File Offset: 0x00000938
		[NullableContext(1)]
		private void \u00A0(object A_1, RoutedEventArgs A_2)
		{
			TrayVM trayVM = base.DataContext as TrayVM;
			if (trayVM != null)
			{
				trayVM.ToggleConvertTo3DCommand.Execute();
			}
			A_2.Handled = true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002768 File Offset: 0x00000968
		[NullableContext(1)]
		private void \u1680(object A_1, RoutedEventArgs A_2)
		{
			TrayVM trayVM = base.DataContext as TrayVM;
			if (trayVM != null)
			{
				trayVM.ToggleAutoConvertCommand.Execute();
			}
			A_2.Handled = true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002798 File Offset: 0x00000998
		private void \u00A0(bool A_1)
		{
			TrayVM trayVM = base.DataContext as TrayVM;
			if (trayVM != null)
			{
				if (A_1)
				{
					trayVM.SelectStandardVideoCommand.Execute();
					return;
				}
				trayVM.SelectSplitVideoCommand.Execute();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027CE File Offset: 0x000009CE
		[NullableContext(1)]
		private void \u2000(object A_1, RoutedEventArgs A_2)
		{
			this.\u00A0(true);
			A_2.Handled = true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027DE File Offset: 0x000009DE
		[NullableContext(1)]
		private void \u2001(object A_1, RoutedEventArgs A_2)
		{
			this.\u00A0(false);
			A_2.Handled = true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027F0 File Offset: 0x000009F0
		[NullableContext(1)]
		protected override void OnPreviewMouseDown(MouseButtonEventArgs A_1)
		{
			try
			{
				DependencyObject dependencyObject = A_1.OriginalSource as DependencyObject;
				if (dependencyObject != null && this.\u1680(dependencyObject))
				{
					base.Focus();
					this.\u00A0(dependencyObject);
					A_1.Handled = true;
					return;
				}
			}
			catch (Exception ex)
			{
				\u2025.\u00A0(global::\u00A0.\u2000.\u00A0, 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u200B() + ex.ToString(), 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2010());
			}
			base.OnPreviewMouseDown(A_1);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002868 File Offset: 0x00000A68
		[NullableContext(1)]
		private void \u00A0(DependencyObject A_1)
		{
			TrayVM trayVM = base.DataContext as TrayVM;
			if (trayVM == null)
			{
				return;
			}
			MenuItem menuItem = this.\u2000<MenuItem>(A_1);
			if (menuItem != null)
			{
				string name = menuItem.Name;
				if (name == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2011())
				{
					trayVM.ToggleConvertTo3DCommand.Execute();
					return;
				}
				if (!(name == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2012()))
				{
					return;
				}
				trayVM.ToggleAutoConvertCommand.Execute();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028CC File Offset: 0x00000ACC
		[NullableContext(1)]
		private bool \u1680(DependencyObject A_1)
		{
			if (A_1 == null)
			{
				return false;
			}
			MenuItem menuItem = this.\u2000<MenuItem>(A_1);
			if (menuItem == null)
			{
				return false;
			}
			string name = menuItem.Name;
			return name == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2011() || name == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2012() || name == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2013() || name == 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2014();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000293C File Offset: 0x00000B3C
		[NullableContext(1)]
		[return: Nullable(2)]
		private T \u2000<[Nullable(0)] T>(DependencyObject A_1) where T : DependencyObject
		{
			DependencyObject parent = VisualTreeHelper.GetParent(A_1);
			if (parent == null)
			{
				return default(T);
			}
			T t = parent as T;
			if (t != null)
			{
				return t;
			}
			return this.\u2000<T>(parent);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000297C File Offset: 0x00000B7C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this.\u00A0)
			{
				return;
			}
			this.\u00A0 = true;
			Uri uri = new Uri(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2015(), 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029AC File Offset: 0x00000BAC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.\u00A0(int A_1, object A_2)
		{
			switch (A_1)
			{
			case 1:
				this.\u00A0 = (MenuItem)A_2;
				return;
			case 2:
				this.\u1680 = (MenuItem)A_2;
				return;
			case 3:
				((ToggleButton)A_2).Click += new RoutedEventHandler(this.\u00A0);
				return;
			case 4:
				this.\u2000 = (MenuItem)A_2;
				return;
			case 5:
				this.\u00A0 = (ToggleButton)A_2;
				this.\u00A0.Click += new RoutedEventHandler(this.\u1680);
				return;
			case 6:
				this.\u2001 = (MenuItem)A_2;
				this.\u2001.Click += new RoutedEventHandler(this.\u2000);
				return;
			case 7:
				this.\u2002 = (MenuItem)A_2;
				this.\u2002.Click += new RoutedEventHandler(this.\u2001);
				return;
			case 8:
				this.\u2003 = (MenuItem)A_2;
				return;
			case 9:
				this.\u2004 = (MenuItem)A_2;
				return;
			case 10:
				this.\u2005 = (MenuItem)A_2;
				return;
			default:
				this.\u00A0 = true;
				return;
			}
		}

		// Token: 0x04000010 RID: 16
		[Nullable(1)]
		private static readonly string \u00A0 = 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u2022();

		// Token: 0x04000011 RID: 17
		internal MenuItem \u00A0;

		// Token: 0x04000012 RID: 18
		internal MenuItem \u1680;

		// Token: 0x04000013 RID: 19
		internal MenuItem \u2000;

		// Token: 0x04000014 RID: 20
		internal ToggleButton \u00A0;

		// Token: 0x04000015 RID: 21
		internal MenuItem \u2001;

		// Token: 0x04000016 RID: 22
		internal MenuItem \u2002;

		// Token: 0x04000017 RID: 23
		internal MenuItem \u2003;

		// Token: 0x04000018 RID: 24
		internal MenuItem \u2004;

		// Token: 0x04000019 RID: 25
		internal MenuItem \u2005;

		// Token: 0x0400001A RID: 26
		private bool \u00A0;
	}
}
