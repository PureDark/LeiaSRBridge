using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AstraDepth3D.EventBus;
using AstraDepth3D.WindowHelper;
using FaceTrackingManagerServiceLib;

namespace AstraDepth3D.Tracker
{
	// Token: 0x02000029 RID: 41
	public class Facetracker : IDisposable
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000CE88 File Offset: 0x0000B088
		public static Facetracker Instance
		{
			get
			{
				return Facetracker._instance.Value;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000CE94 File Offset: 0x0000B094
		public Facetracker()
		{
			try
			{
				this._faceTracking = (SimpleFaceTracking)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("D98A272F-DE1C-4E73-A1D0-A93A8F068B4B")));
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("Facetracker init error:" + ex.Message);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public Task OpenAsync()
		{
			Facetracker.<OpenAsync>d__6 <OpenAsync>d__;
			<OpenAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OpenAsync>d__.<>4__this = this;
			<OpenAsync>d__.<>1__state = -1;
			<OpenAsync>d__.<>t__builder.Start<Facetracker.<OpenAsync>d__6>(ref <OpenAsync>d__);
			return <OpenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000CF34 File Offset: 0x0000B134
		public Task StartCamera()
		{
			Facetracker.<StartCamera>d__7 <StartCamera>d__;
			<StartCamera>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartCamera>d__.<>4__this = this;
			<StartCamera>d__.<>1__state = -1;
			<StartCamera>d__.<>t__builder.Start<Facetracker.<StartCamera>d__7>(ref <StartCamera>d__);
			return <StartCamera>d__.<>t__builder.Task;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000CF78 File Offset: 0x0000B178
		public Task StopCamera()
		{
			Facetracker.<StopCamera>d__8 <StopCamera>d__;
			<StopCamera>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StopCamera>d__.<>4__this = this;
			<StopCamera>d__.<>1__state = -1;
			<StopCamera>d__.<>t__builder.Start<Facetracker.<StopCamera>d__8>(ref <StopCamera>d__);
			return <StopCamera>d__.<>t__builder.Task;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		private void ForceStop()
		{
			try
			{
				SimpleFaceTracking faceTracking = this._faceTracking;
				if (faceTracking != null)
				{
					faceTracking.Close();
				}
				GlobalStatusManager.Instance._iEyeStatus = LocalEnumeration.EyeStatus.EyeStatus_OFF;
				NLogService.WriteLog("强制关闭眼追");
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("ForceStopCamera操作失败: " + ex.Message);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000D01C File Offset: 0x0000B21C
		private void ForceStart()
		{
			this.ForceStop();
			try
			{
				SimpleFaceTracking faceTracking = this._faceTracking;
				if (faceTracking != null)
				{
					faceTracking.StartCamera();
				}
				GlobalStatusManager.Instance._iEyeStatus = LocalEnumeration.EyeStatus.EyeStatus_ON;
				NLogService.WriteLog("状态跟踪:强制打开眼追成功\n");
			}
			catch (Exception ex)
			{
				NLogService.WriteLog(ex.ToString());
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000D078 File Offset: 0x0000B278
		private void CloseAsync()
		{
			if (this._disposed)
			{
				return;
			}
			try
			{
				SimpleFaceTracking faceTracking = this._faceTracking;
				if (faceTracking != null)
				{
					faceTracking.Close();
				}
				NLogService.WriteLog("眼追已关闭");
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("CloseCamera操作失败: " + ex.Message);
			}
			finally
			{
				if (!this._disposed)
				{
					if (Marshal.IsComObject(this._faceTracking) && this._faceTracking != null)
					{
						Marshal.ReleaseComObject(this._faceTracking);
						NLogService.WriteLog("_faceTracking release!");
					}
					this._disposed = true;
				}
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000D11C File Offset: 0x0000B31C
		public ValueTuple<float, float, float, float> GetPose()
		{
			float x = -1f;
			float y = -1f;
			float z = -1f;
			float z_r = 0f;
			try
			{
				if (!this._disposed)
				{
					SimpleFaceTracking faceTracking = this._faceTracking;
					if (faceTracking != null)
					{
						float num;
						faceTracking.GetPosition(out x, out y, out z, out num, out z_r);
					}
				}
				NLogService.WriteLine(string.Format("状态跟踪:眼追坐标获取成功:{0},{1},{2},{3}\n", new object[]
				{
					x,
					y,
					z,
					z_r
				}));
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("状态跟踪:眼追坐标获取失败:" + ex.Message + "\n");
			}
			return new ValueTuple<float, float, float, float>(x, y, z, z_r);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		public void Dispose()
		{
			this.CloseAsync();
		}

		// Token: 0x040000D9 RID: 217
		private SimpleFaceTracking _faceTracking;

		// Token: 0x040000DA RID: 218
		private bool _disposed;

		// Token: 0x040000DB RID: 219
		private static readonly Lazy<Facetracker> _instance = new Lazy<Facetracker>(() => new Facetracker());
	}
}
