using System;
using System.Drawing;
using System.Timers;
using AstraDepth3D.EventBus;

namespace AstraDepth3D.WindowHelper
{
	// Token: 0x0200003E RID: 62
	public class ReorderHandler : IDisposable
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public ReorderHandler(Action action)
		{
			this._action = action;
			this._reorderHandler = new Timer(150.0);
			this._reorderHandler.Elapsed += this.CheckObjectReorderEvent;
			this._reorderHandler.AutoReset = true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		public void Dispose()
		{
			Timer reorderHandler = this._reorderHandler;
			if (reorderHandler != null)
			{
				reorderHandler.Close();
			}
			this._reorderHandler = null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000FBE6 File Offset: 0x0000DDE6
		public void SetFullScreenHwndHandle(IntPtr hwnd, Rectangle lastScreenRect)
		{
			this.fullScreenHwndHandle = hwnd;
			this.screenRect = lastScreenRect;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000FBF6 File Offset: 0x0000DDF6
		public void EnableReorderCheck()
		{
			this._isReorder = true;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000FC01 File Offset: 0x0000DE01
		public void Start()
		{
			Timer reorderHandler = this._reorderHandler;
			if (reorderHandler != null && !reorderHandler.Enabled)
			{
				Timer reorderHandler2 = this._reorderHandler;
				if (reorderHandler2 != null)
				{
					reorderHandler2.Start();
				}
				NLogService.WriteLog("ReorderHandler:start...");
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000FC35 File Offset: 0x0000DE35
		public void Stop()
		{
			Timer reorderHandler = this._reorderHandler;
			if (reorderHandler == null)
			{
				return;
			}
			reorderHandler.Stop();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000FC47 File Offset: 0x0000DE47
		public void Reset()
		{
			Timer reorderHandler = this._reorderHandler;
			if (reorderHandler != null)
			{
				reorderHandler.Stop();
			}
			this._isReorder = false;
			this.fullScreenHwndHandle = IntPtr.Zero;
			this.screenRect = Rectangle.Empty;
			NLogService.WriteLog("ReorderHandler:reset...");
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000FC84 File Offset: 0x0000DE84
		private void CheckObjectReorderEvent(object sender, ElapsedEventArgs e)
		{
			if (this._isReorder)
			{
				if (this.fullScreenHwndHandle == IntPtr.Zero)
				{
					return;
				}
				ValueTuple<IntPtr, IntPtr> valueTuple = User32.IsTaskBarOrGearVisible(this.fullScreenHwndHandle, this.screenRect);
				IntPtr trayWnd = valueTuple.Item1;
				IntPtr gear = valueTuple.Item2;
				if (trayWnd != IntPtr.Zero || gear != IntPtr.Zero)
				{
					if (GlobalStatusManager.Instance._iInterWaveType == LocalEnumeration.InterWaveType.InterWave_SBS)
					{
						NLogService.WriteLog("MainWindow|WinEventProc：桌面任务栏显示或3D Zone显示;event:EVENT_OBJECT_REORDER");
						this._action();
					}
					else if (GlobalStatusManager.Instance._iInterWaveType == LocalEnumeration.InterWaveType.InterWave_AI && gear != IntPtr.Zero)
					{
						NLogService.WriteLog("MainWindow|WinEventProc：3D Zone显示;event:EVENT_OBJECT_REORDER");
						this._action();
					}
				}
				else
				{
					IntPtr foregroundWindow = User32.GetForegroundWindow();
					string winTilte = User32.GetWinTitle(foregroundWindow);
					string winClass = User32.GetWinClassName(foregroundWindow);
					if (winTilte == "Program Manager" && winClass == "Progman")
					{
						NLogService.WriteLog("MainWindow|WinEventProc：桌面显示退出交织;event:EVENT_OBJECT_REORDER");
						this._action();
						return;
					}
				}
				this._isReorder = false;
			}
		}

		// Token: 0x04000196 RID: 406
		private Timer _reorderHandler;

		// Token: 0x04000197 RID: 407
		private volatile bool _isReorder;

		// Token: 0x04000198 RID: 408
		private IntPtr fullScreenHwndHandle;

		// Token: 0x04000199 RID: 409
		private Rectangle screenRect = Rectangle.Empty;

		// Token: 0x0400019A RID: 410
		private Action _action;
	}
}
