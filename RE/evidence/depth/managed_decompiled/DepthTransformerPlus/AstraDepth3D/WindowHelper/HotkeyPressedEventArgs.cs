using System;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000033 RID: 51
	public class HotkeyPressedEventArgs : EventArgs
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000DA97 File Offset: 0x0000BC97
		public int Id { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000DA9F File Offset: 0x0000BC9F
		public int VirtualKeyCode { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000DAA7 File Offset: 0x0000BCA7
		public bool Control { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000DAAF File Offset: 0x0000BCAF
		public bool Alt { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000DAB7 File Offset: 0x0000BCB7
		public bool Shift { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000DABF File Offset: 0x0000BCBF
		public bool Tab { get; }

		// Token: 0x060001E6 RID: 486 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		public HotkeyPressedEventArgs(int id, int virtualKeyCode, bool control, bool alt, bool shift, bool tab)
		{
			this.Id = id;
			this.VirtualKeyCode = virtualKeyCode;
			this.Control = control;
			this.Alt = alt;
			this.Shift = shift;
			this.Tab = tab;
		}
	}
}
