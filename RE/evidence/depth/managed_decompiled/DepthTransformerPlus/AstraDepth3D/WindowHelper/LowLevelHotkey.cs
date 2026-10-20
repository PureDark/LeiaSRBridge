using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x02000034 RID: 52
	public class LowLevelHotkey : IDisposable
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001E7 RID: 487 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		// (remove) Token: 0x060001E8 RID: 488 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public event EventHandler<HotkeyPressedEventArgs> HotkeyPressed;

		// Token: 0x060001E9 RID: 489 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public LowLevelHotkey()
		{
			this._proc = new LowLevelHotkey.LowLevelKeyboardProc(this.HookCallback);
			this._hookId = this.SetHook(this._proc);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public bool Register(int id, int virtualKeyCode, bool control = true, bool alt = false, bool shift = false, bool tab = false)
		{
			foreach (KeyValuePair<int, HotkeyConfig> existing in this._hotkeys)
			{
				if (existing.Key != id && existing.Value.VirtualKeyCode == virtualKeyCode && existing.Value.Control == control && existing.Value.Alt == alt && existing.Value.Shift == shift && existing.Value.Tab == tab)
				{
					return false;
				}
			}
			this._hotkeys[id] = new HotkeyConfig(id, virtualKeyCode, control, alt, shift, tab);
			return true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public bool Register(int id, int virtualKeyCode, int modifierKeys)
		{
			if (modifierKeys == 9)
			{
				return this.Register(id, virtualKeyCode, false, false, false, true);
			}
			bool control = (modifierKeys & 2) != 0;
			bool alt = (modifierKeys & 1) != 0;
			bool shift = (modifierKeys & 4) != 0;
			return this.Register(id, virtualKeyCode, control, alt, shift, false);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000DCCC File Offset: 0x0000BECC
		public void RegisterOrThrow(int id, int virtualKeyCode, bool control = true, bool alt = false, bool shift = false, bool tab = false)
		{
			if (!this.Register(id, virtualKeyCode, control, alt, shift, tab))
			{
				throw new InvalidOperationException("热键组合 " + this.GetHotkeyDisplayString(virtualKeyCode, control, alt, shift, tab) + " 已被注册");
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DD04 File Offset: 0x0000BF04
		private string GetHotkeyDisplayString(int virtualKeyCode, bool control, bool alt, bool shift, bool tab)
		{
			List<string> parts = new List<string>();
			if (control)
			{
				parts.Add("Ctrl");
			}
			if (alt)
			{
				parts.Add("Alt");
			}
			if (shift)
			{
				parts.Add("Shift");
			}
			if (tab)
			{
				parts.Add("Tab");
			}
			parts.Add(this.GetVirtualKeyName(virtualKeyCode));
			return string.Join("+", parts);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		private string GetVirtualKeyName(int vkCode)
		{
			switch (vkCode)
			{
			case 8:
				return "Backspace";
			case 9:
				return "Tab";
			case 10:
			case 11:
			case 12:
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 28:
			case 29:
			case 30:
			case 31:
			case 41:
			case 42:
			case 43:
			case 47:
			case 58:
			case 59:
			case 60:
			case 61:
			case 62:
			case 63:
			case 64:
			case 94:
			case 95:
			case 108:
			case 124:
			case 125:
			case 126:
			case 127:
			case 128:
			case 129:
			case 130:
			case 131:
			case 132:
			case 133:
			case 134:
			case 135:
			case 136:
			case 137:
			case 138:
			case 139:
			case 140:
			case 141:
			case 142:
			case 143:
			case 146:
			case 147:
			case 148:
			case 149:
			case 150:
			case 151:
			case 152:
			case 153:
			case 154:
			case 155:
			case 156:
			case 157:
			case 158:
			case 159:
			case 160:
			case 161:
			case 162:
			case 163:
			case 164:
			case 165:
			case 166:
			case 167:
			case 168:
			case 169:
			case 170:
			case 171:
			case 172:
			case 173:
			case 174:
			case 175:
			case 176:
			case 177:
			case 178:
			case 179:
			case 180:
			case 181:
			case 182:
			case 183:
			case 184:
			case 185:
				break;
			case 13:
				return "Enter";
			case 19:
				return "Pause";
			case 20:
				return "CapsLock";
			case 27:
				return "Esc";
			case 32:
				return "Space";
			case 33:
				return "PageUp";
			case 34:
				return "PageDown";
			case 35:
				return "End";
			case 36:
				return "Home";
			case 37:
				return "Left";
			case 38:
				return "Up";
			case 39:
				return "Right";
			case 40:
				return "Down";
			case 44:
				return "PrintScreen";
			case 45:
				return "Insert";
			case 46:
				return "Delete";
			case 48:
				return "0";
			case 49:
				return "1";
			case 50:
				return "2";
			case 51:
				return "3";
			case 52:
				return "4";
			case 53:
				return "5";
			case 54:
				return "6";
			case 55:
				return "7";
			case 56:
				return "8";
			case 57:
				return "9";
			case 65:
				return "A";
			case 66:
				return "B";
			case 67:
				return "C";
			case 68:
				return "D";
			case 69:
				return "E";
			case 70:
				return "F";
			case 71:
				return "G";
			case 72:
				return "H";
			case 73:
				return "I";
			case 74:
				return "J";
			case 75:
				return "K";
			case 76:
				return "L";
			case 77:
				return "M";
			case 78:
				return "N";
			case 79:
				return "O";
			case 80:
				return "P";
			case 81:
				return "Q";
			case 82:
				return "R";
			case 83:
				return "S";
			case 84:
				return "T";
			case 85:
				return "U";
			case 86:
				return "V";
			case 87:
				return "W";
			case 88:
				return "X";
			case 89:
				return "Y";
			case 90:
				return "Z";
			case 91:
				return "LWin";
			case 92:
				return "RWin";
			case 93:
				return "Apps";
			case 96:
				return "NumPad0";
			case 97:
				return "NumPad1";
			case 98:
				return "NumPad2";
			case 99:
				return "NumPad3";
			case 100:
				return "NumPad4";
			case 101:
				return "NumPad5";
			case 102:
				return "NumPad6";
			case 103:
				return "NumPad7";
			case 104:
				return "NumPad8";
			case 105:
				return "NumPad9";
			case 106:
				return "NumPad*";
			case 107:
				return "NumPad+";
			case 109:
				return "NumPad-";
			case 110:
				return "NumPad.";
			case 111:
				return "NumPad/";
			case 112:
				return "F1";
			case 113:
				return "F2";
			case 114:
				return "F3";
			case 115:
				return "F4";
			case 116:
				return "F5";
			case 117:
				return "F6";
			case 118:
				return "F7";
			case 119:
				return "F8";
			case 120:
				return "F9";
			case 121:
				return "F10";
			case 122:
				return "F11";
			case 123:
				return "F12";
			case 144:
				return "NumLock";
			case 145:
				return "ScrollLock";
			case 186:
				return ";";
			case 187:
				return "=";
			case 188:
				return ",";
			case 189:
				return "-";
			case 190:
				return ".";
			case 191:
				return "/";
			case 192:
				return "`";
			default:
				switch (vkCode)
				{
				case 219:
					return "[";
				case 220:
					return "\\";
				case 221:
					return "]";
				case 222:
					return "'";
				}
				break;
			}
			return string.Format("0x{0:X}", vkCode);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public bool IsHotkeyRegistered(int virtualKeyCode, bool control = true, bool alt = false, bool shift = false, bool tab = false)
		{
			foreach (HotkeyConfig hotkey in this._hotkeys.Values)
			{
				if (hotkey.VirtualKeyCode == virtualKeyCode && hotkey.Control == control && hotkey.Alt == alt && hotkey.Shift == shift && hotkey.Tab == tab)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000E364 File Offset: 0x0000C564
		public bool IsHotkeyRegistered(int virtualKeyCode, int modifier)
		{
			bool control = (modifier & 2) != 0;
			bool alt = (modifier & 1) != 0;
			bool shift = (modifier & 4) != 0;
			bool tab = modifier == 9;
			return this.IsHotkeyRegistered(virtualKeyCode, control, alt, shift, tab);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E397 File Offset: 0x0000C597
		public void Unregister(int id)
		{
			this._hotkeys.Remove(id);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		public bool IsRegistered(int id)
		{
			return this._hotkeys.ContainsKey(id);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public IEnumerable<HotkeyConfig> GetAllHotkeys()
		{
			return this._hotkeys.Values;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		private IntPtr SetHook(LowLevelHotkey.LowLevelKeyboardProc proc)
		{
			IntPtr result;
			using (Process curProcess = Process.GetCurrentProcess())
			{
				using (ProcessModule curModule = curProcess.MainModule)
				{
					result = LowLevelHotkey.SetWindowsHookEx(13, proc, LowLevelHotkey.GetModuleHandle(curModule.ModuleName), 0U);
				}
			}
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E428 File Offset: 0x0000C628
		private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				int vkCode = Marshal.ReadInt32(lParam);
				bool isKeyDown = wParam == (IntPtr)256 || wParam == (IntPtr)260;
				bool isKeyUp = wParam == (IntPtr)257 || wParam == (IntPtr)261;
				if (vkCode == 9)
				{
					if (isKeyDown)
					{
						this._tabPressed = true;
					}
					else if (isKeyUp)
					{
						this._tabPressed = false;
					}
				}
				if (isKeyDown)
				{
					bool ctrlPressed = ((int)LowLevelHotkey.ControlGetKeyState(17) & 32768) != 0;
					bool altPressed = ((int)LowLevelHotkey.ControlGetKeyState(18) & 32768) != 0;
					bool shiftPressed = ((int)LowLevelHotkey.ControlGetKeyState(16) & 32768) != 0;
					using (Dictionary<int, HotkeyConfig>.ValueCollection.Enumerator enumerator = this._hotkeys.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							HotkeyConfig hotkey = enumerator.Current;
							bool tabCondition = hotkey.Tab == this._tabPressed;
							if (vkCode == hotkey.VirtualKeyCode && ctrlPressed == hotkey.Control && altPressed == hotkey.Alt && shiftPressed == hotkey.Shift && tabCondition)
							{
								if (this._triggeredHotkeys.Contains(hotkey.Id))
								{
									break;
								}
								this._triggeredHotkeys.Add(hotkey.Id);
								EventHandler<HotkeyPressedEventArgs> hotkeyPressed = this.HotkeyPressed;
								if (hotkeyPressed == null)
								{
									break;
								}
								hotkeyPressed(this, new HotkeyPressedEventArgs(hotkey.Id, hotkey.VirtualKeyCode, hotkey.Control, hotkey.Alt, hotkey.Shift, hotkey.Tab));
								break;
							}
						}
						goto IL_1F2;
					}
				}
				if (isKeyUp)
				{
					foreach (HotkeyConfig hotkey2 in this._hotkeys.Values)
					{
						if (vkCode == hotkey2.VirtualKeyCode)
						{
							this._triggeredHotkeys.Remove(hotkey2.Id);
						}
					}
				}
			}
			IL_1F2:
			return LowLevelHotkey.CallNextHookEx(this._hookId, nCode, wParam, lParam);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E654 File Offset: 0x0000C854
		public void Dispose()
		{
			LowLevelHotkey.UnhookWindowsHookEx(this._hookId);
			this._hotkeys.Clear();
		}

		// Token: 0x060001F7 RID: 503
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelHotkey.LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		// Token: 0x060001F8 RID: 504
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		// Token: 0x060001F9 RID: 505
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x060001FA RID: 506
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x060001FB RID: 507
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		// Token: 0x060001FC RID: 508 RVA: 0x0000E66D File Offset: 0x0000C86D
		private static short ControlGetKeyState(int vKey)
		{
			return LowLevelHotkey.GetAsyncKeyState(vKey);
		}

		// Token: 0x04000164 RID: 356
		private IntPtr _hookId = IntPtr.Zero;

		// Token: 0x04000165 RID: 357
		private LowLevelHotkey.LowLevelKeyboardProc _proc;

		// Token: 0x04000166 RID: 358
		private readonly Dictionary<int, HotkeyConfig> _hotkeys = new Dictionary<int, HotkeyConfig>();

		// Token: 0x04000168 RID: 360
		private bool _tabPressed;

		// Token: 0x04000169 RID: 361
		private readonly HashSet<int> _triggeredHotkeys = new HashSet<int>();

		// Token: 0x0400016A RID: 362
		private const int WH_KEYBOARD_LL = 13;

		// Token: 0x0400016B RID: 363
		private const int WM_KEYDOWN = 256;

		// Token: 0x0400016C RID: 364
		private const int WM_SYSKEYDOWN = 260;

		// Token: 0x0400016D RID: 365
		private const int WM_KEYUP = 257;

		// Token: 0x0400016E RID: 366
		private const int WM_SYSKEYUP = 261;

		// Token: 0x0400016F RID: 367
		private const int VK_CONTROL = 17;

		// Token: 0x04000170 RID: 368
		private const int VK_MENU = 18;

		// Token: 0x04000171 RID: 369
		private const int VK_SHIFT = 16;

		// Token: 0x04000172 RID: 370
		private const int VK_TAB = 9;

		// Token: 0x020000AA RID: 170
		// (Invoke) Token: 0x06000459 RID: 1113
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
	}
}
