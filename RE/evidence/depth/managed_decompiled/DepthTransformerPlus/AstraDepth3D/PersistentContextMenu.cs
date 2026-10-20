using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AstraDepth3D
{
	// Token: 0x0200000D RID: 13
	public class PersistentContextMenu : ContextMenu
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003010 File Offset: 0x00001210
		protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003012 File Offset: 0x00001212
		protected override void OnLostMouseCapture(MouseEventArgs e)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003014 File Offset: 0x00001214
		protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003016 File Offset: 0x00001216
		protected override void OnClosed(RoutedEventArgs e)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003018 File Offset: 0x00001218
		protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
			if (!base.IsMouseOver)
			{
				e.Handled = true;
			}
			base.OnPreviewMouseDown(e);
		}
	}
}
