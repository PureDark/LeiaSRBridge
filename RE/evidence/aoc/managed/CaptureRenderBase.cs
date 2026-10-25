using System;
using System.Threading;
using System.Threading.Tasks;
using AiStereoKitWrapper;
using AstraDepth3D.BOEStereoRender;
using AstraDepth3D.Composition.WindowsRuntimeHelpers;
using AstraDepth3D.EventBus;
using AstraDepth3D.Tracker;
using AstraDepth3D.WindowHelper;
using BOEInterleaving;
using BOEReasterTest;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Windows.Graphics;
using Windows.UI.Composition;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x02000056 RID: 86
	public abstract class CaptureRenderBase : ICapture, IDisposable
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		protected CaptureRenderBase(YLStereoMatcher matcher)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException("matcher");
			}
			this._stereoMatcher = matcher;
			this._lastFpsTime = DateTime.Now;
			this._lastHeartbeatTime = DateTime.Now;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		protected virtual void InitializeBoeRenderer(int width, int height)
		{
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				if (MCUSdk.BOE_MCU_openMCU() != 0)
				{
					NLogService.WriteLog("BOE_MCU_openMCU failed");
				}
				double[] array = new double[6];
				if (MCUSdk.BOE_MCU_getDeviceParam(array) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getDeviceParam failed");
				}
				double[] array2 = new double[6];
				if (MCUSdk.BOE_MCU_getWholeMat(array2) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getWholeMat failed");
				}
				double[] array3 = new double[8];
				if (MCUSdk.BOE_MCU_getRefractionDeviceParam(array3) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getRefractionDeviceParam failed");
				}
				if (Interleaving.InitSDK(this._d3dDevice.NativePointer, IntPtr.Zero, array, array2, array3, D3DVersion.dx11, 0, 0, width, height, 87, "./logs/") != 0)
				{
					NLogService.WriteLog("BOE_Interleaving_initSDK failed");
				}
				Interleaving.SetWholeParam(array2);
				Interleaving.SetDeviceParam(array);
				Interleaving.SetRefractDeviceParam(array3);
				NLogService.WriteLog(base.GetType().Name + "|Interweaving SDK initialized successfully!");
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001C09C File Offset: 0x0001A29C
		protected virtual void OpenRasterSafe()
		{
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				if (MCUSdk.BOE_MCU_set3DMode(true) == 0)
				{
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ON;
					bool flag2 = MCUSdk.BOE_MCU_get3DMode();
					NLogService.WriteLog(string.Format("状态跟踪:透镜已经打开{0}", flag2));
				}
				else
				{
					NLogService.WriteLog("状态跟踪:透镜打开失败");
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001C114 File Offset: 0x0001A314
		protected virtual void CloseRasterSafe()
		{
			if (!Task.Run(delegate()
			{
				object sdkLock = this._sdkLock;
				lock (sdkLock)
				{
					try
					{
						if (MCUSdk.BOE_MCU_set3DMode(false) == 0)
						{
							NLogService.WriteLog("状态跟踪:透镜已经关闭");
						}
						else
						{
							NLogService.WriteLog("状态跟踪:透镜关闭失败");
						}
						MCUSdk.BOE_MCU_closeMCU();
						GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_OFF;
					}
					catch (Exception ex)
					{
						NLogService.WriteLog("状态跟踪:关闭透镜异常: " + ex.Message);
					}
				}
			}).Wait(TimeSpan.FromSeconds(2.0)))
			{
				NLogService.WriteLog("状态跟踪:关闭透镜超时");
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001C148 File Offset: 0x0001A348
		protected void RenderInterleavedFrame(ShaderResourceView inputSrv, SizeInt32 frameSize)
		{
			LocalEnumeration.InterWaveType currentInterwaveStatus = this.GetCurrentInterwaveStatus();
			if (currentInterwaveStatus == LocalEnumeration.InterWaveType.InterWave_None)
			{
				try
				{
					ValueTuple<byte[], int> valueTuple = this._formatRecognition.DrawFrame(this._immediateContext, inputSrv);
					byte[] item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					YLStereoMatcher stereoMatcher = this._stereoMatcher;
					this.ResetCurrentInterwaveStatus(((stereoMatcher != null) ? new bool?(stereoMatcher.ExtractAndMatch(item, this._formatRecognition.scaleW, this._formatRecognition.scaleH, item2)) : null).GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
					FormatRecognition formatRecognition = this._formatRecognition;
					if (formatRecognition != null)
					{
						formatRecognition.DrawFrame(this._immediateContext, inputSrv, this._backRenderTargetView, this._viewportF);
					}
					goto IL_1E5;
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("stereoMatcher error: " + ex.Message);
					this.ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType.InterWave_AI);
					goto IL_1E5;
				}
			}
			FrameConvert frameConvert = this._frameConvert;
			if (frameConvert != null)
			{
				frameConvert.DrawFrame(this._immediateContext, inputSrv, currentInterwaveStatus);
			}
			try
			{
				FrameConvert frameConvert2 = this._frameConvert;
				ShaderResourceView shaderResourceView = (frameConvert2 != null) ? frameConvert2.GetShaderResource() : null;
				if (shaderResourceView == null || shaderResourceView.NativePointer == IntPtr.Zero)
				{
					NLogService.WriteLog("RenderLoop|Skipping BOE render: invalid boeSrv");
					return;
				}
				RawColor4 rawColor;
				rawColor..ctor(0f, 0f, 0f, 1f);
				this._immediateContext.OutputMerger.SetRenderTargets(this._backRenderTargetView);
				this._immediateContext.Rasterizer.SetViewport(this._viewportF);
				this._immediateContext.ClearRenderTargetView(this._backRenderTargetView, rawColor);
				ValueTuple<float, float, float> pose = this.GetPose();
				float item3 = pose.Item1;
				float item4 = pose.Item2;
				float item5 = pose.Item3;
				Interleaving.DoInterleave(item3, item4, item5, this._immediateContext.NativePointer, this._backRenderTargetView.NativePointer, shaderResourceView.NativePointer, frameSize.Width, frameSize.Height, 87, InterMode.RLInter, 0);
			}
			catch (Exception ex2)
			{
				NLogService.WriteLog("Render error: " + ex2.Message);
			}
			IL_1E5:
			this.NotifyInterwaveStatusChange();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001C35C File Offset: 0x0001A55C
		protected void NotifyInterwaveStatusChange()
		{
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType currentInterwaveStatus = this._currentInterwaveStatus;
			LocalEnumeration.InterWaveType lastInterwaveStatus = this._lastInterwaveStatus;
			this._statusLock.ExitReadLock();
			if (lastInterwaveStatus != currentInterwaveStatus)
			{
				this._lastInterwaveStatus = currentInterwaveStatus;
				LocalEventPublisher localPublisher = this._localPublisher;
				if (localPublisher == null)
				{
					return;
				}
				localPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)currentInterwaveStatus);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		protected int EvaluateTrackingRegion(float x, float y, float z)
		{
			if (z <= 0f)
			{
				return 0;
			}
			if (z < this._trackNearZ)
			{
				return 1;
			}
			if (z > this._trackFarZ)
			{
				return 2;
			}
			float num = (z - this._trackNearZ) / (this._trackFarZ - this._trackNearZ);
			float num2 = this._trackNearHalfX + (this._trackFarHalfX - this._trackNearHalfX) * num;
			float num3 = this._trackNearHalfY + (this._trackFarHalfY - this._trackNearHalfY) * num;
			if (Math.Abs(x) > num2 || Math.Abs(y) > num3)
			{
				return 2;
			}
			return 99;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001C434 File Offset: 0x0001A634
		protected ValueTuple<float, float, float> GetPose()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float item = pose.Item1;
			float item2 = pose.Item2;
			float item3 = pose.Item3;
			float item4 = pose.Item4;
			this._faceTrackingState = this.EvaluateTrackingRegion(item, item2, item3);
			if (item4 == 0f)
			{
				this._faceTrackingState = -1;
			}
			return new ValueTuple<float, float, float>(item, item2, item3);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001C48C File Offset: 0x0001A68C
		protected void UpdateFaceTrackingState()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float item = pose.Item1;
			float item2 = pose.Item2;
			float item3 = pose.Item3;
			float item4 = pose.Item4;
			double num = Math.Atan((double)Math.Abs(item / item3)) * 57.29577951308232;
			double num2 = Math.Atan((double)Math.Abs(item2 / item3)) * 57.29577951308232;
			if (num <= (double)this._horAngle && num2 <= (double)this._verAngle && item3 <= this._far && item3 >= this._near)
			{
				if (GlobalStatusManager.Instance._iRasterStatus != LocalEnumeration.RasterStatus.RasterStatus_ON)
				{
					NLogService.WriteLog("状态跟踪:透镜未开启");
					return;
				}
				float x = GlobalStatusManager.Instance._calibration_a * (item * 1000f) + GlobalStatusManager.Instance._calibration_b;
				if (GlobalStatusManager.Instance._calibration_b == 0f)
				{
					x = item * 1000f + GlobalStatusManager.Instance._calibration_a;
				}
				try
				{
					if (LoadBoeMCUDll.UpdateEyeCoordinate(x, item3 * 1000f) != 0)
					{
						NLogService.WriteLog("状态跟踪:透镜更新失败");
					}
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("状态跟踪:透镜更新失败:" + ex.Message);
				}
				this._faceTrackingState = 99;
			}
			else if (item3 < this._near && item3 > 0f)
			{
				this._faceTrackingState = 1;
			}
			else if (item3 <= 0f)
			{
				this._faceTrackingState = 0;
			}
			else
			{
				this._faceTrackingState = 2;
			}
			if (item4 == 0f)
			{
				this._faceTrackingState = -1;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001C61C File Offset: 0x0001A81C
		protected void UpdateFPS()
		{
			DateTime now = DateTime.Now;
			if ((now - this._lastHeartbeatTime).TotalMilliseconds >= 200.0)
			{
				this._lastHeartbeatTime = now;
				LocalEventPublisher localPublisher = this._localPublisher;
				if (localPublisher == null)
				{
					return;
				}
				localPublisher.DoSomethingAsync("FaceTrackingStatusChanged", this._faceTrackingState);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001C670 File Offset: 0x0001A870
		public LocalEnumeration.InterWaveType GetCurrentInterwaveStatus()
		{
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType currentInterwaveStatus;
			try
			{
				currentInterwaveStatus = this._currentInterwaveStatus;
			}
			finally
			{
				this._statusLock.ExitReadLock();
			}
			return currentInterwaveStatus;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
		public void ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType status)
		{
			this._statusLock.EnterWriteLock();
			try
			{
				if (this._currentInterwaveStatus != status)
				{
					this._currentInterwaveStatus = status;
					NLogService.WriteLog("ResetCurrentInterwaveStatus:" + status.ToString());
				}
			}
			finally
			{
				this._statusLock.ExitWriteLock();
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001C714 File Offset: 0x0001A914
		protected bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001C725 File Offset: 0x0001A925
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this._localPublisher = publisher;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001C72E File Offset: 0x0001A92E
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this._swapChain);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001C73C File Offset: 0x0001A93C
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this._frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x060003FD RID: 1021
		public abstract void StartCapture();

		// Token: 0x060003FE RID: 1022
		public abstract void Dispose();

		// Token: 0x060003FF RID: 1023 RVA: 0x0001C750 File Offset: 0x0001A950
		protected static Texture2D CreateFrameTexture(Device device, int w, int h)
		{
			Texture2DDescription texture2DDescription = default(Texture2DDescription);
			texture2DDescription.Width = w;
			texture2DDescription.Height = h;
			texture2DDescription.Format = 87;
			texture2DDescription.MipLevels = 1;
			texture2DDescription.ArraySize = 1;
			texture2DDescription.SampleDescription = new SampleDescription(1, 0);
			texture2DDescription.Usage = 0;
			texture2DDescription.BindFlags = 8;
			texture2DDescription.CpuAccessFlags = 0;
			texture2DDescription.OptionFlags = 0;
			return new Texture2D(device, texture2DDescription);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		protected static void SafeDispose<T>(ref T resource) where T : class, IDisposable
		{
			if (resource != null)
			{
				try
				{
					resource.Dispose();
				}
				catch
				{
				}
				resource = default(T);
			}
		}

		// Token: 0x040002CC RID: 716
		protected Device _d3dDevice;

		// Token: 0x040002CD RID: 717
		protected DeviceContext _immediateContext;

		// Token: 0x040002CE RID: 718
		protected SwapChain1 _swapChain;

		// Token: 0x040002CF RID: 719
		protected RenderTargetView _backRenderTargetView;

		// Token: 0x040002D0 RID: 720
		protected Texture2D _frameTexture;

		// Token: 0x040002D1 RID: 721
		protected ShaderResourceView _frameResourceView;

		// Token: 0x040002D2 RID: 722
		private protected FormatRecognition _formatRecognition;

		// Token: 0x040002D3 RID: 723
		private protected FrameConvert _frameConvert;

		// Token: 0x040002D4 RID: 724
		protected readonly YLStereoMatcher _stereoMatcher;

		// Token: 0x040002D5 RID: 725
		protected int _faceTrackingState = -1;

		// Token: 0x040002D6 RID: 726
		protected LocalEventPublisher _localPublisher;

		// Token: 0x040002D7 RID: 727
		protected LocalEnumeration.InterWaveType _currentInterwaveStatus;

		// Token: 0x040002D8 RID: 728
		protected LocalEnumeration.InterWaveType _lastInterwaveStatus;

		// Token: 0x040002D9 RID: 729
		protected readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x040002DA RID: 730
		protected RawViewportF _viewportF;

		// Token: 0x040002DB RID: 731
		protected float _near = 400f;

		// Token: 0x040002DC RID: 732
		protected float _far = 1000f;

		// Token: 0x040002DD RID: 733
		protected float _horAngle = 33f;

		// Token: 0x040002DE RID: 734
		protected float _verAngle = 26f;

		// Token: 0x040002DF RID: 735
		protected float _trackNearZ = 400f;

		// Token: 0x040002E0 RID: 736
		protected float _trackFarZ = 900f;

		// Token: 0x040002E1 RID: 737
		protected float _trackNearHalfX = 250f;

		// Token: 0x040002E2 RID: 738
		protected float _trackNearHalfY = 200f;

		// Token: 0x040002E3 RID: 739
		protected float _trackFarHalfX = 300f;

		// Token: 0x040002E4 RID: 740
		protected float _trackFarHalfY = 300f;

		// Token: 0x040002E5 RID: 741
		protected DateTime _lastFpsTime;

		// Token: 0x040002E6 RID: 742
		protected int _frameCount;

		// Token: 0x040002E7 RID: 743
		protected int _fps;

		// Token: 0x040002E8 RID: 744
		protected DateTime _lastHeartbeatTime;

		// Token: 0x040002E9 RID: 745
		protected double _heartbeatIntervalMs = 50.0;

		// Token: 0x040002EA RID: 746
		protected long _isDisposingFlag;

		// Token: 0x040002EB RID: 747
		protected volatile bool _isDisposed;

		// Token: 0x040002EC RID: 748
		protected readonly object _sdkLock = new object();

		// Token: 0x040002ED RID: 749
		protected readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);
	}
}
