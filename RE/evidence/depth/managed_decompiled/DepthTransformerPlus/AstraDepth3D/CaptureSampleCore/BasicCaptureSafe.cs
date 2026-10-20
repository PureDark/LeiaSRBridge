using System;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
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
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Windows.Foundation;
using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI.Composition;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x0200004A RID: 74
	public class BasicCaptureSafe : ICapture, IDisposable
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00017FB0 File Offset: 0x000161B0
		public BasicCaptureSafe(IDirect3DDevice d, GraphicsCaptureItem i, YLStereoMatcher matcher)
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 1, 0);
			if (previousState != 0)
			{
				throw new InvalidOperationException(string.Format("BasicCaptureSafe|Cannot initialize from state: {0}", (CaptureState)previousState));
			}
			try
			{
				this.item = i;
				this.device = d;
				if (matcher == null)
				{
					throw new ArgumentNullException("matcher");
				}
				this.stereoMatcher = matcher;
				this.d3dDevice = Direct3D11Helper.CreateSharpDXDevice(this.device);
				this.d3dDevice.QueryInterface<DeviceMultithread>().SetMultithreadProtected(true);
				SwapChainDescription1 swapChainDescription = default(SwapChainDescription1);
				swapChainDescription.Width = this.item.Size.Width;
				swapChainDescription.Height = this.item.Size.Height;
				swapChainDescription.Format = 87;
				swapChainDescription.Stereo = true;
				SampleDescription sampleDescription = default(SampleDescription);
				sampleDescription.Count = 1;
				sampleDescription.Quality = 0;
				swapChainDescription.SampleDescription = sampleDescription;
				swapChainDescription.Usage = 32;
				swapChainDescription.BufferCount = 2;
				swapChainDescription.Scaling = 0;
				swapChainDescription.SwapEffect = 3;
				swapChainDescription.AlphaMode = 1;
				swapChainDescription.Flags = 0;
				SwapChainDescription1 description = swapChainDescription;
				using (Factory2 dxgiFactory = new Factory2(false))
				{
					this.swapChain = new SwapChain1(dxgiFactory, this.d3dDevice, ref description, null);
					if (this.swapChain == null)
					{
						throw new Exception("BasicCaptureSafe|create swapChain failed!");
					}
				}
				this.framePool = Direct3D11CaptureFramePool.CreateFreeThreaded(this.device, 87, 2, i.Size);
				this.session = this.framePool.CreateCaptureSession(i);
				BasicCaptureSafe.TrySetCaptureProperty(this.session, "IsBorderRequired", false);
				BasicCaptureSafe.TrySetCaptureProperty(this.session, "IsCursorCaptureEnabled", false);
				this.lastSize = i.Size;
				Direct3D11CaptureFramePool @object = this.framePool;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Direct3D11CaptureFramePool, object>>(new Func<TypedEventHandler<Direct3D11CaptureFramePool, object>, EventRegistrationToken>(@object.add_FrameArrived), new Action<EventRegistrationToken>(@object.remove_FrameArrived), new TypedEventHandler<Direct3D11CaptureFramePool, object>(this.OnFrameArrived));
				using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
				{
					this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
				}
				this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.item.Size.Width, this.item.Size.Height);
				this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
				this.formatRecognition = new FormatRecognition();
				this.formatRecognition.Initialize(this.d3dDevice);
				this.frameConvert = new FrameConvert(this.d3dDevice, i.Size.Width, i.Size.Height);
				this.InitializeBoeRenderer(i.Size.Width, i.Size.Height);
				RawViewportF rawViewportF = default(RawViewportF);
				rawViewportF.X = 0f;
				rawViewportF.Y = 0f;
				rawViewportF.Width = (float)this.item.Size.Width;
				rawViewportF.Height = (float)this.item.Size.Height;
				rawViewportF.MinDepth = 0f;
				rawViewportF.MaxDepth = 0f;
				this.viewportF = rawViewportF;
				this._lastFpsTime = DateTime.Now;
				this._lastHeartbeatTime = DateTime.Now;
				NLogService.WriteLog("BasicCaptureSafe|Initialization complete!");
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("BasicCaptureSafe|initialized successfully!");
			}
			catch (Exception e)
			{
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureSafe|failed to initialize：" + e.Message);
				this.CleanupResources();
				throw;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001840C File Offset: 0x0001660C
		private void InitializeBoeRenderer(int width, int height)
		{
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				if (MCUSdk.BOE_MCU_openMCU() != 0)
				{
					NLogService.WriteLog("BOE_MCU_openMCU failed");
				}
				double[] deviceParam = new double[6];
				if (MCUSdk.BOE_MCU_getDeviceParam(deviceParam) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getDeviceParam failed");
				}
				NLogService.WriteLog("BOE_MCU_getDeviceParam: " + string.Join<double>(", ", deviceParam));
				double[] wholeMat = new double[6];
				if (MCUSdk.BOE_MCU_getWholeMat(wholeMat) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getWholeMat failed");
				}
				NLogService.WriteLog("BOE_MCU_getWholeMat: " + string.Join<double>(", ", wholeMat));
				double[] refractionParam = new double[8];
				if (MCUSdk.BOE_MCU_getRefractionDeviceParam(refractionParam) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getRefractionDeviceParam failed");
				}
				NLogService.WriteLog("BOE_MCU_getRefractionDeviceParam: " + string.Join<double>(", ", refractionParam));
				if (Interleaving.InitSDK(this.d3dDevice.NativePointer, IntPtr.Zero, deviceParam, wholeMat, refractionParam, D3DVersion.dx11, 0, 0, width, height, 87, "./logs/") != 0)
				{
					NLogService.WriteLog("BOE_Interleaving_initSDK failed");
				}
				Interleaving.SetWholeParam(wholeMat);
				Interleaving.SetDeviceParam(deviceParam);
				Interleaving.SetRefractDeviceParam(refractionParam);
				NLogService.WriteLog("BasicCaptureSafe|Interweaving SDK initialized successfully!");
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00018550 File Offset: 0x00016750
		public void Dispose()
		{
			if (!this._disposeSemaphore.Wait(0))
			{
				NLogService.WriteLog("BasicCaptureSafe|Dispose already in progress, skipping...");
				return;
			}
			try
			{
				Interlocked.Exchange(ref this._isDisposingFlag, 1L);
				int currentState = Interlocked.CompareExchange(ref this._state, 4, 3);
				if (currentState == 2)
				{
					Interlocked.Exchange(ref this._state, 4);
				}
				else if (currentState != 3 && currentState != 2)
				{
					NLogService.WriteLog(string.Format("BasicCaptureSafe|Cannot dispose from state: {0}", (CaptureState)currentState));
					return;
				}
				try
				{
					GraphicsCaptureSession graphicsCaptureSession = this.session;
					if (graphicsCaptureSession != null)
					{
						graphicsCaptureSession.Dispose();
					}
					this.session = null;
					NLogService.WriteLog("状态跟踪:抓屏Session已经关闭");
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("状态跟踪:关闭Session异常: " + ex.Message);
				}
				if (this.framePool != null)
				{
					WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<Direct3D11CaptureFramePool, object>>(new Action<EventRegistrationToken>(this.framePool.remove_FrameArrived), new TypedEventHandler<Direct3D11CaptureFramePool, object>(this.OnFrameArrived));
				}
				Thread.Sleep(50);
				object initLock = this._initLock;
				lock (initLock)
				{
					FrameConvert frameConvert = this.frameConvert;
					if (frameConvert != null)
					{
						frameConvert.Dispose();
					}
					this.frameConvert = null;
					NLogService.WriteLog("状态跟踪:FrameConvert已释放");
					FormatRecognition formatRecognition = this.formatRecognition;
					if (formatRecognition != null)
					{
						formatRecognition.Dispose();
					}
					this.formatRecognition = null;
					if (this.framePool != null)
					{
						Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
						if (direct3D11CaptureFramePool != null)
						{
							direct3D11CaptureFramePool.Dispose();
						}
						this.framePool = null;
					}
					object sdkLock = this._sdkLock;
					lock (sdkLock)
					{
						Interleaving.ReleaseSDK();
					}
					NLogService.WriteLog("状态跟踪:3D交织已释放");
					this.UninitResource();
					Device device = this.d3dDevice;
					if (device != null)
					{
						device.ImmediateContext.ClearState();
					}
					SwapChain1 swapChain = this.swapChain;
					if (swapChain != null)
					{
						swapChain.Dispose();
					}
					this.swapChain = null;
					Device device2 = this.d3dDevice;
					if (device2 != null)
					{
						device2.Dispose();
					}
					this.d3dDevice = null;
					NLogService.WriteLog("状态跟踪:释放所有资源");
				}
				this.CloseRasterSafe();
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher != null)
				{
					localEventPublisher.DoSomethingAsync("RasterStatusChanged", (int)GlobalStatusManager.Instance._iRasterStatus);
				}
				LocalEventPublisher localEventPublisher2 = this.localPublisher;
				if (localEventPublisher2 != null)
				{
					localEventPublisher2.DoSomethingAsync("FaceTrackingStatusChanged", -2);
				}
				this.localPublisher = null;
				GC.SuppressFinalize(this);
				this._isDisposed = true;
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureSafe|Dispose completed");
			}
			finally
			{
				this._disposeSemaphore.Release();
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00018818 File Offset: 0x00016A18
		private void CloseRasterSafe()
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
							GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_OFF;
						}
						else
						{
							NLogService.WriteLog("状态跟踪:透镜关闭失败");
							GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ERROR;
						}
						MCUSdk.BOE_MCU_closeMCU();
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

		// Token: 0x0600032A RID: 810 RVA: 0x0001884C File Offset: 0x00016A4C
		public void StartCapture()
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 3, 2);
			if (previousState != 2)
			{
				NLogService.WriteLog(string.Format("BasicCaptureSafe|Cannot start capture from state: {0}", (CaptureState)previousState));
				return;
			}
			try
			{
				this.OpenRasterSafe();
				NLogService.WriteLog("状态跟踪:开始抓屏Session");
				GraphicsCaptureSession graphicsCaptureSession = this.session;
				if (graphicsCaptureSession != null)
				{
					graphicsCaptureSession.StartCapture();
				}
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher != null)
				{
					localEventPublisher.DoSomethingSync("RasterStatusChanged", (int)GlobalStatusManager.Instance._iRasterStatus);
				}
				NLogService.WriteLog("状态跟踪:开始抓屏线程");
			}
			catch (Exception ex)
			{
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("状态跟踪:开始捕获失败: " + ex.Message);
				throw;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00018908 File Offset: 0x00016B08
		private void OpenRasterSafe()
		{
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				if (MCUSdk.BOE_MCU_set3DMode(true) == 0)
				{
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ON;
					bool mode = MCUSdk.BOE_MCU_get3DMode();
					NLogService.WriteLog(string.Format("状态跟踪:透镜已经打开{0}", mode));
				}
				else
				{
					NLogService.WriteLog("状态跟踪:透镜打开失败");
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00018980 File Offset: 0x00016B80
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00018989 File Offset: 0x00016B89
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00018997 File Offset: 0x00016B97
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000189AC File Offset: 0x00016BAC
		public LocalEnumeration.InterWaveType GetCurrentInterwaveStatus()
		{
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType result;
			try
			{
				result = this.currentInterwaveStatus;
			}
			finally
			{
				this._statusLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000189EC File Offset: 0x00016BEC
		public void ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType status)
		{
			this._statusLock.EnterWriteLock();
			try
			{
				if (this.currentInterwaveStatus != status)
				{
					this.currentInterwaveStatus = status;
					NLogService.WriteLog("ResetCurrentInterwaveStatus:" + status.ToString());
				}
			}
			finally
			{
				this._statusLock.ExitWriteLock();
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00018A50 File Offset: 0x00016C50
		private bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00018A64 File Offset: 0x00016C64
		private void OnFrameArrived(Direct3D11CaptureFramePool sender, object args)
		{
			if (this.IsDisposing() || this._isDisposed || Interlocked.CompareExchange(ref this._state, -1, -1) != 3)
			{
				return;
			}
			try
			{
				bool newSize = false;
				using (Direct3D11CaptureFrame frame = sender.TryGetNextFrame())
				{
					if (frame == null)
					{
						return;
					}
					if (frame.ContentSize.Width != this.lastSize.Width || frame.ContentSize.Height != this.lastSize.Height)
					{
						newSize = true;
						this.lastSize = frame.ContentSize;
						object frameLock = this._frameLock;
						lock (frameLock)
						{
							if (this.IsDisposing())
							{
								return;
							}
							Texture2D texture2D = this.frameTexture;
							if (texture2D != null)
							{
								texture2D.Dispose();
							}
							this.frameTexture = null;
							ShaderResourceView shaderResourceView = this.frameResourceView;
							if (shaderResourceView != null)
							{
								shaderResourceView.Dispose();
							}
							this.frameResourceView = null;
							this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
							this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
							RenderTargetView renderTargetView = this.backRenderTargetView;
							if (renderTargetView != null)
							{
								renderTargetView.Dispose();
							}
							this.backRenderTargetView = null;
							this.swapChain.ResizeBuffers(2, this.lastSize.Width, this.lastSize.Height, 87, 0);
							using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
							{
								this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
							}
						}
					}
					using (Texture2D bitmap = Direct3D11Helper.CreateSharpDXTexture2D(frame.Surface))
					{
						object frameLock = this._frameLock;
						lock (frameLock)
						{
							if (this.IsDisposing())
							{
								return;
							}
							this.d3dDevice.ImmediateContext.CopyResource(bitmap, this.frameTexture);
						}
					}
				}
				this.ProcessFrame(newSize);
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("OnFrameArrived:" + ex.Message);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00018D0C File Offset: 0x00016F0C
		private void ProcessFrame(bool newSize)
		{
			LocalEnumeration.InterWaveType status = this.GetCurrentInterwaveStatus();
			object frameLock;
			if (status == LocalEnumeration.InterWaveType.InterWave_None)
			{
				frameLock = this._frameLock;
				lock (frameLock)
				{
					if (this.IsDisposing())
					{
						return;
					}
					try
					{
						ValueTuple<byte[], int> valueTuple = this.formatRecognition.DrawFrame(this.d3dDevice, this.frameResourceView);
						byte[] buffer = valueTuple.Item1;
						int rowPitch = valueTuple.Item2;
						YLStereoMatcher ylstereoMatcher = this.stereoMatcher;
						bool? isStereoContent = (ylstereoMatcher != null) ? new bool?(ylstereoMatcher.ExtractAndMatch(buffer, this.formatRecognition.scaleW, this.formatRecognition.scaleH, rowPitch)) : null;
						this.ResetCurrentInterwaveStatus(isStereoContent.GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
						NLogService.WriteLog(string.Format("check stereo format:{0}", isStereoContent));
						FormatRecognition formatRecognition = this.formatRecognition;
						if (formatRecognition != null)
						{
							formatRecognition.DrawFrame(this.d3dDevice, this.frameResourceView, this.backRenderTargetView, this.viewportF);
						}
						goto IL_274;
					}
					catch (Exception e)
					{
						NLogService.WriteLog("stereoMatcher error:" + e.Message);
						this.ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType.InterWave_AI);
						goto IL_274;
					}
				}
			}
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert != null)
			{
				frameConvert.DrawFrame(this.d3dDevice, this.frameResourceView, status);
			}
			frameLock = this._frameLock;
			lock (frameLock)
			{
				if (this.IsDisposing())
				{
					return;
				}
				try
				{
					RawColor4 clearColor;
					clearColor..ctor(0f, 0f, 0f, 1f);
					Device device = this.d3dDevice;
					if (device != null)
					{
						device.ImmediateContext.OutputMerger.SetRenderTargets(this.backRenderTargetView);
					}
					Device device2 = this.d3dDevice;
					if (device2 != null)
					{
						device2.ImmediateContext.Rasterizer.SetViewport(this.viewportF);
					}
					Device device3 = this.d3dDevice;
					if (device3 != null)
					{
						device3.ImmediateContext.ClearRenderTargetView(this.backRenderTargetView, clearColor);
					}
					ValueTuple<float, float, float> pose = this.GetPose();
					float _x = pose.Item1;
					float _y = pose.Item2;
					float _z = pose.Item3;
					Interleaving.DoInterleave(_x, _y, _z, this.d3dDevice.ImmediateContext.NativePointer, this.backRenderTargetView.NativePointer, this.frameConvert.GetShaderResource().NativePointer, this.lastSize.Width, this.lastSize.Height, 87, InterMode.RLInter, 0);
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("Render error: " + ex.Message);
				}
			}
			IL_274:
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType currentStatus = this.currentInterwaveStatus;
			LocalEnumeration.InterWaveType interWaveType = this.lastInterwaveStatus;
			this._statusLock.ExitReadLock();
			if (interWaveType != currentStatus)
			{
				this.lastInterwaveStatus = currentStatus;
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher != null)
				{
					localEventPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)currentStatus);
				}
			}
			SwapChain1 swapChain = this.swapChain;
			if (swapChain != null)
			{
				swapChain.Present(0, 0);
			}
			this.UpdateFPS();
			if (newSize && !this.IsDisposing())
			{
				Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
				if (direct3D11CaptureFramePool == null)
				{
					return;
				}
				direct3D11CaptureFramePool.Recreate(this.device, 87, 2, this.lastSize);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001907C File Offset: 0x0001727C
		private void UpdateFPS()
		{
			DateTime currentTime = DateTime.Now;
			if ((currentTime - this._lastHeartbeatTime).TotalMilliseconds >= 50.0)
			{
				this._lastHeartbeatTime = currentTime;
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher == null)
				{
					return;
				}
				localEventPublisher.DoSomethingAsync("FaceTrackingStatusChanged", this.faceTrackingState);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000190D0 File Offset: 0x000172D0
		private void UpdateFaceTrackingState()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float x = pose.Item1;
			float y = pose.Item2;
			float z = pose.Item3;
			float z_r = pose.Item4;
			double num = Math.Atan((double)Math.Abs(x / z)) * 57.29577951308232;
			double nVerAngle = Math.Atan((double)Math.Abs(y / z)) * 57.29577951308232;
			if (num <= (double)this.hor_angle && nVerAngle <= (double)this.ver_angle && z <= this.far && z >= this.near)
			{
				if (GlobalStatusManager.Instance._iRasterStatus != LocalEnumeration.RasterStatus.RasterStatus_ON)
				{
					NLogService.WriteLog("状态跟踪:透镜未开启");
					return;
				}
				float _X = GlobalStatusManager.Instance._calibration_a * (x * 1000f) + GlobalStatusManager.Instance._calibration_b;
				if (GlobalStatusManager.Instance._calibration_b == 0f)
				{
					_X = x * 1000f + GlobalStatusManager.Instance._calibration_a;
				}
				try
				{
					if (LoadBoeMCUDll.UpdateEyeCoordinate(_X, z * 1000f) != 0)
					{
						NLogService.WriteLog("状态跟踪:透镜更新失败");
					}
				}
				catch (Exception e)
				{
					NLogService.WriteLog("状态跟踪:透镜更新失败:" + e.Message);
				}
				this.faceTrackingState = 99;
			}
			else if (z < this.near && z > 0f)
			{
				this.faceTrackingState = 1;
			}
			else if (z <= 0f)
			{
				this.faceTrackingState = 0;
			}
			else
			{
				this.faceTrackingState = 2;
			}
			if (z_r == 0f)
			{
				this.faceTrackingState = -1;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00019260 File Offset: 0x00017460
		private ValueTuple<float, float, float> GetPose()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float x = pose.Item1;
			float y = pose.Item2;
			float z = pose.Item3;
			float z_r = pose.Item4;
			NLogService.WriteLine(string.Format("状态跟踪:人眼坐标获取成功:{0},{1},{2},{3}", new object[]
			{
				x,
				y,
				z,
				z_r
			}));
			double num = Math.Atan((double)Math.Abs(x / z)) * 57.29577951308232;
			double nVerAngle = Math.Atan((double)Math.Abs(y / z)) * 57.29577951308232;
			if (num <= (double)this.hor_angle && nVerAngle <= (double)this.ver_angle && z <= this.far && z >= this.near)
			{
				this.faceTrackingState = 99;
			}
			else if (z < this.near && z > 0f)
			{
				this.faceTrackingState = 1;
			}
			else if (z <= 0f)
			{
				this.faceTrackingState = 0;
			}
			else
			{
				this.faceTrackingState = 2;
			}
			if (z_r == 0f)
			{
				this.faceTrackingState = -1;
			}
			return new ValueTuple<float, float, float>(x, y, z);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00019378 File Offset: 0x00017578
		private Texture2D CreateFrameTexture(Device device, int w, int h)
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
			Texture2DDescription description = texture2DDescription;
			return new Texture2D(device, description);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000193F0 File Offset: 0x000175F0
		private void UninitResource()
		{
			ShaderResourceView shaderResourceView = this.frameResourceView;
			if (shaderResourceView != null)
			{
				shaderResourceView.Dispose();
			}
			this.frameResourceView = null;
			Texture2D texture2D = this.frameTexture;
			if (texture2D != null)
			{
				texture2D.Dispose();
			}
			this.frameTexture = null;
			RenderTargetView renderTargetView = this.backRenderTargetView;
			if (renderTargetView != null)
			{
				renderTargetView.Dispose();
			}
			this.backRenderTargetView = null;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00019448 File Offset: 0x00017648
		private void CleanupResources()
		{
			try
			{
				FrameConvert frameConvert = this.frameConvert;
				if (frameConvert != null)
				{
					frameConvert.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				FormatRecognition formatRecognition = this.formatRecognition;
				if (formatRecognition != null)
				{
					formatRecognition.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				ShaderResourceView shaderResourceView = this.frameResourceView;
				if (shaderResourceView != null)
				{
					shaderResourceView.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				Texture2D texture2D = this.frameTexture;
				if (texture2D != null)
				{
					texture2D.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				RenderTargetView renderTargetView = this.backRenderTargetView;
				if (renderTargetView != null)
				{
					renderTargetView.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				SwapChain1 swapChain = this.swapChain;
				if (swapChain != null)
				{
					swapChain.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				Device device = this.d3dDevice;
				if (device != null)
				{
					device.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				GraphicsCaptureSession graphicsCaptureSession = this.session;
				if (graphicsCaptureSession != null)
				{
					graphicsCaptureSession.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
				if (direct3D11CaptureFramePool != null)
				{
					direct3D11CaptureFramePool.Dispose();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00019594 File Offset: 0x00017794
		private static void TrySetCaptureProperty(GraphicsCaptureSession session, string propertyName, bool value)
		{
			try
			{
				PropertyInfo prop = session.GetType().GetProperty(propertyName);
				if (prop != null && prop.CanWrite)
				{
					prop.SetValue(session, value);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog(propertyName + " not supported: " + ex.Message);
			}
		}

		// Token: 0x04000296 RID: 662
		private GraphicsCaptureItem item;

		// Token: 0x04000297 RID: 663
		private Direct3D11CaptureFramePool framePool;

		// Token: 0x04000298 RID: 664
		private GraphicsCaptureSession session;

		// Token: 0x04000299 RID: 665
		private SizeInt32 lastSize;

		// Token: 0x0400029A RID: 666
		private IDirect3DDevice device;

		// Token: 0x0400029B RID: 667
		private Device d3dDevice;

		// Token: 0x0400029C RID: 668
		private SwapChain1 swapChain;

		// Token: 0x0400029D RID: 669
		private RenderTargetView backRenderTargetView;

		// Token: 0x0400029E RID: 670
		private Texture2D frameTexture;

		// Token: 0x0400029F RID: 671
		private ShaderResourceView frameResourceView;

		// Token: 0x040002A0 RID: 672
		private readonly YLStereoMatcher stereoMatcher;

		// Token: 0x040002A1 RID: 673
		private FormatRecognition formatRecognition;

		// Token: 0x040002A2 RID: 674
		private FrameConvert frameConvert;

		// Token: 0x040002A3 RID: 675
		private int faceTrackingState = -1;

		// Token: 0x040002A4 RID: 676
		private LocalEventPublisher localPublisher;

		// Token: 0x040002A5 RID: 677
		private LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x040002A6 RID: 678
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x040002A7 RID: 679
		private readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x040002A8 RID: 680
		private RawViewportF viewportF;

		// Token: 0x040002A9 RID: 681
		private DateTime _lastFpsTime;

		// Token: 0x040002AA RID: 682
		private int _frameCount;

		// Token: 0x040002AB RID: 683
		private int _fps;

		// Token: 0x040002AC RID: 684
		private DateTime _lastHeartbeatTime;

		// Token: 0x040002AD RID: 685
		private float near = 400f;

		// Token: 0x040002AE RID: 686
		private float far = 1000f;

		// Token: 0x040002AF RID: 687
		private float hor_angle = 33f;

		// Token: 0x040002B0 RID: 688
		private float ver_angle = 26f;

		// Token: 0x040002B1 RID: 689
		private int _state;

		// Token: 0x040002B2 RID: 690
		private long _isDisposingFlag;

		// Token: 0x040002B3 RID: 691
		private readonly object _initLock = new object();

		// Token: 0x040002B4 RID: 692
		private readonly object _frameLock = new object();

		// Token: 0x040002B5 RID: 693
		private readonly object _sdkLock = new object();

		// Token: 0x040002B6 RID: 694
		private readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x040002B7 RID: 695
		private volatile bool _isDisposed;
	}
}
