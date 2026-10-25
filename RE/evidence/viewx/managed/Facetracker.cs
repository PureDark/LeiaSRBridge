using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DepthTransformer.WindowHelper;
using FaceTrackingManagerServiceLib;

namespace DepthTransformer.Tracker
{
	// Token: 0x02000026 RID: 38
	public class Facetracker : IDisposable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000C054 File Offset: 0x0000A254
		public static Facetracker Instance
		{
			get
			{
				return Facetracker._instance.Value;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000C060 File Offset: 0x0000A260
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
			IniFile iniFile = new IniFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "config.ini"));
			this.near = iniFile.ReadFloat("ViewableArea", "near", 0.3f);
			this.far = iniFile.ReadFloat("ViewableArea", "far", 0.6f);
			this.hor_angle = iniFile.ReadFloat("ViewableArea", "hor_angle", 25f);
			this.ver_angle = iniFile.ReadFloat("ViewableArea", "ver_angle", 40f);
			NLogService.WriteLog(string.Format("状态跟踪:获取视场参数成功:{0},{1},{2},{3}\n", new object[]
			{
				this.near,
				this.far,
				this.hor_angle,
				this.ver_angle
			}));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public Task OpenAsync()
		{
			Facetracker.<OpenAsync>d__10 <OpenAsync>d__;
			<OpenAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OpenAsync>d__.<>1__state = -1;
			<OpenAsync>d__.<>t__builder.Start<Facetracker.<OpenAsync>d__10>(ref <OpenAsync>d__);
			return <OpenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public Task StartCamera()
		{
			Facetracker.<StartCamera>d__11 <StartCamera>d__;
			<StartCamera>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartCamera>d__.<>4__this = this;
			<StartCamera>d__.<>1__state = -1;
			<StartCamera>d__.<>t__builder.Start<Facetracker.<StartCamera>d__11>(ref <StartCamera>d__);
			return <StartCamera>d__.<>t__builder.Task;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000C240 File Offset: 0x0000A440
		public Task StopCamera()
		{
			Facetracker.<StopCamera>d__12 <StopCamera>d__;
			<StopCamera>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StopCamera>d__.<>4__this = this;
			<StopCamera>d__.<>1__state = -1;
			<StopCamera>d__.<>t__builder.Start<Facetracker.<StopCamera>d__12>(ref <StopCamera>d__);
			return <StopCamera>d__.<>t__builder.Task;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000C284 File Offset: 0x0000A484
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

		// Token: 0x0600018B RID: 395 RVA: 0x0000C328 File Offset: 0x0000A528
		public ValueTuple<float, float, float, float, float> GetPose()
		{
			float num = -1f;
			float num2 = -1f;
			float num3 = -1f;
			float num4 = 0f;
			float item = 0f;
			try
			{
				if (!this._disposed)
				{
					SimpleFaceTracking faceTracking = this._faceTracking;
					if (faceTracking != null)
					{
						faceTracking.GetPosition(out num, out num2, out num3, out num4, out item);
					}
				}
				NLogService.WriteLine(string.Format("状态跟踪:眼追坐标获取成功:{0},{1},{2},{3}\n", new object[]
				{
					num,
					num2,
					num3,
					num4
				}));
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("状态跟踪:眼追坐标获取失败:" + ex.Message + "\n");
			}
			return new ValueTuple<float, float, float, float, float>(num, num2, num3, num4, item);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public ValueTuple<float, float, float, float> GetViewableArea()
		{
			return new ValueTuple<float, float, float, float>(this.near, this.far, this.hor_angle, this.ver_angle);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C40B File Offset: 0x0000A60B
		public void Dispose()
		{
			this.CloseAsync();
		}

		// Token: 0x040000EC RID: 236
		private SimpleFaceTracking _faceTracking;

		// Token: 0x040000ED RID: 237
		private bool _disposed;

		// Token: 0x040000EE RID: 238
		private static readonly Lazy<Facetracker> _instance = new Lazy<Facetracker>(() => new Facetracker());

		// Token: 0x040000EF RID: 239
		private float near = 0.3f;

		// Token: 0x040000F0 RID: 240
		private float far = 0.6f;

		// Token: 0x040000F1 RID: 241
		private float hor_angle = 25f;

		// Token: 0x040000F2 RID: 242
		private float ver_angle = 40f;
	}
}
