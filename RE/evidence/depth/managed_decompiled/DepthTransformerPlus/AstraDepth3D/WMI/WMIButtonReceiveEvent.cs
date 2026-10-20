using System;
using System.Management;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Threading;
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D.WMI
{
	// Token: 0x0200001E RID: 30
	internal class WMIButtonReceiveEvent
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000B570 File Offset: 0x00009770
		public bool Start(WMIButtonReceiveEvent.ButtonEvent_Delegate callback, CancellationToken cancellationToken)
		{
			bool bRet = false;
			try
			{
				if (callback == null)
				{
					return bRet;
				}
				new Thread(delegate()
				{
					ManagementScope scope = new ManagementScope("\\\\localhost\\root\\WMI");
					WqlEventQuery query = new WqlEventQuery(this.strQuery);
					ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);
					while (!cancellationToken.IsCancellationRequested)
					{
						watcher.WaitForNextEvent();
						NLogService.WriteLog("Get a event...");
						callback();
					}
					watcher.Stop();
					NLogService.WriteLog("WMIButtonReceiveEvent:Thread end...");
				}).Start();
				return true;
			}
			catch (Exception err)
			{
				NLogService.WriteLog("An error occurred while trying to receive an event: " + err.Message);
			}
			return bRet;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B5EC File Offset: 0x000097EC
		public bool Start(EventArrivedEventHandler ButtonEventArrived)
		{
			WMIButtonReceiveEvent.<>c__DisplayClass5_0 CS$<>8__locals1 = new WMIButtonReceiveEvent.<>c__DisplayClass5_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.ButtonEventArrived = ButtonEventArrived;
			bool bRet = false;
			try
			{
				if (CS$<>8__locals1.ButtonEventArrived == null)
				{
					NLogService.WriteLog("EventArrivedEventHandler 为空！");
					return bRet;
				}
				ManagementScope scope = new ManagementScope("\\\\localhost\\root\\WMI");
				WqlEventQuery query = new WqlEventQuery(this.strQuery);
				ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);
				watcher.Options.Timeout = new TimeSpan(0, 0, 30);
				watcher.EventArrived += CS$<>8__locals1.ButtonEventArrived;
				watcher.Stopped += delegate(object sender, StoppedEventArgs e)
				{
					WMIButtonReceiveEvent.<>c__DisplayClass5_0.<<Start>b__0>d <<Start>b__0>d;
					<<Start>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
					<<Start>b__0>d.<>4__this = CS$<>8__locals1;
					<<Start>b__0>d.e = e;
					<<Start>b__0>d.<>1__state = -1;
					<<Start>b__0>d.<>t__builder.Start<WMIButtonReceiveEvent.<>c__DisplayClass5_0.<<Start>b__0>d>(ref <<Start>b__0>d);
				};
				watcher.Start();
				this.watcher = watcher;
				NLogService.WriteLog("WMIButtonReceiveEvent:start...");
				return true;
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("初始化失败: " + ex.Message);
			}
			return bRet;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public void Stop()
		{
			try
			{
				object obj = this.lockObj;
				lock (obj)
				{
					if (this.watcher != null)
					{
						this.watcher.Stop();
						this.watcher.Dispose();
						this.watcher = null;
						NLogService.WriteLog("WMIButtonReceiveEvent:stop...");
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("停止失败: " + ex.Message);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B754 File Offset: 0x00009954
		private bool IsWmiServiceRunning()
		{
			bool ret = false;
			using (ServiceController service = new ServiceController("Winmgmt"))
			{
				ret = (service.Status == ServiceControllerStatus.Running);
				NLogService.WriteLog("WMIButtonReceiveEvent:IsWmiServiceRunning:" + ret.ToString());
			}
			return ret;
		}

		// Token: 0x0400009A RID: 154
		private string strQuery = "SELECT * FROM BUTTON_EVENT";

		// Token: 0x0400009B RID: 155
		private readonly object lockObj = new object();

		// Token: 0x0400009C RID: 156
		private ManagementEventWatcher watcher;

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x0600041B RID: 1051
		public delegate void ButtonEvent_Delegate();
	}
}
