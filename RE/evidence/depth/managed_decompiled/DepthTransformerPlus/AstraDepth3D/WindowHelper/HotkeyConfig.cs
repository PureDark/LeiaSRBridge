using System;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000032 RID: 50
	public class HotkeyConfig
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public int Id { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000DA0D File Offset: 0x0000BC0D
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000DA15 File Offset: 0x0000BC15
		public int VirtualKeyCode { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000DA26 File Offset: 0x0000BC26
		public bool Control { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000DA2F File Offset: 0x0000BC2F
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000DA37 File Offset: 0x0000BC37
		public bool Alt { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000DA40 File Offset: 0x0000BC40
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public bool Shift { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000DA51 File Offset: 0x0000BC51
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000DA59 File Offset: 0x0000BC59
		public bool Tab { get; set; }

		// Token: 0x060001DF RID: 479 RVA: 0x0000DA62 File Offset: 0x0000BC62
		public HotkeyConfig(int id, int virtualKeyCode, bool control = true, bool alt = false, bool shift = false, bool tab = false)
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
