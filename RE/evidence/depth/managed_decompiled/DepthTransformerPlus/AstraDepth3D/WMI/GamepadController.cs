using System;
using System.Windows.Threading;
using AstraDepth3D.WindowHelper;
using SharpDX.XInput;

namespace AstraDepth3D.WMI
{
	// Token: 0x0200001D RID: 29
	public class GamepadController
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000160 RID: 352 RVA: 0x0000B1E8 File Offset: 0x000093E8
		// (remove) Token: 0x06000161 RID: 353 RVA: 0x0000B220 File Offset: 0x00009420
		public event GamepadController.JoystickButtonDelegate JoystickButtonEvent;

		// Token: 0x06000163 RID: 355 RVA: 0x0000B260 File Offset: 0x00009460
		public void Connect()
		{
			if (this._controller == null)
			{
				this._controller = new Controller(0);
				this._timer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(50.0)
				};
				this._timer.Tick += this.PollController;
				this._timer.Start();
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public void Disconnect()
		{
			if (this._timer != null)
			{
				this._timer.Tick -= this.PollController;
				this._timer.Stop();
				this._timer = null;
			}
			if (this._controller != null)
			{
				this._controller = null;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B314 File Offset: 0x00009514
		private void PollController(object sender, EventArgs e)
		{
			if (this._controller == null)
			{
				return;
			}
			if (!this._controller.IsConnected)
			{
				NLogService.WriteLine("Xbox控制器未连接。");
				return;
			}
			State state = this._controller.GetState();
			this.ProcessButtons(state);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B358 File Offset: 0x00009558
		private void ProcessButtons(State state)
		{
			this.currentState = state;
			bool rightShoulderKeyDown = false;
			int dPadKeyDown = 0;
			if (this.previousState.Gamepad.Buttons != this.currentState.Gamepad.Buttons)
			{
				if (this.currentState.Gamepad.Buttons.HasFlag(4096))
				{
					NLogService.WriteLine("A 按钮按住不放");
				}
				else if (this.currentState.Gamepad.Buttons.HasFlag(32768))
				{
					NLogService.WriteLine("Y 按钮按住不放");
				}
				if (this.currentState.Gamepad.Buttons.HasFlag(512))
				{
					rightShoulderKeyDown = true;
				}
				if (!this.currentState.Gamepad.Buttons.HasFlag(1) && this.previousState.Gamepad.Buttons.HasFlag(1))
				{
					dPadKeyDown = 1;
				}
				if (!this.currentState.Gamepad.Buttons.HasFlag(2) && this.previousState.Gamepad.Buttons.HasFlag(2))
				{
					dPadKeyDown = 2;
				}
				if (!this.currentState.Gamepad.Buttons.HasFlag(4) && this.previousState.Gamepad.Buttons.HasFlag(4))
				{
					dPadKeyDown = 4;
				}
				if (!this.currentState.Gamepad.Buttons.HasFlag(8) && this.previousState.Gamepad.Buttons.HasFlag(8))
				{
					dPadKeyDown = 8;
				}
				if (rightShoulderKeyDown && dPadKeyDown != 0)
				{
					NLogService.WriteLine(string.Format("按钮释放:{0}", dPadKeyDown));
					GamepadController.JoystickButtonDelegate joystickButtonEvent = this.JoystickButtonEvent;
					if (joystickButtonEvent != null)
					{
						joystickButtonEvent(dPadKeyDown);
					}
				}
				this.previousState = this.currentState;
			}
		}

		// Token: 0x04000096 RID: 150
		private DispatcherTimer _timer;

		// Token: 0x04000097 RID: 151
		private Controller _controller;

		// Token: 0x04000098 RID: 152
		private State previousState;

		// Token: 0x04000099 RID: 153
		private State currentState;

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x06000417 RID: 1047
		public delegate void JoystickButtonDelegate(int KeyNum);
	}
}
