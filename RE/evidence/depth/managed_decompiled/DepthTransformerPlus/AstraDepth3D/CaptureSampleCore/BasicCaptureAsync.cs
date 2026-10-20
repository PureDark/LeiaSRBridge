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
	// Token: 0x02000048 RID: 72
	public class BasicCaptureAsync : ICapture, IDisposable
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00016790 File Offset: 0x00014990
		public BasicCaptureAsync(IDirect3DDevice d, GraphicsCaptureItem i, YLStereoMatcher matcher)
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 1, 0);
			if (previousState != 0)
			{
				throw new InvalidOperationException(string.Format("BasicCaptureAsync|Cannot initialize from state: {0}", (CaptureState)previousState));
			}
			try
			{
				if (i == null)
				{
					throw new ArgumentNullException("i");
				}
				this.item = i;
				if (d == null)
				{
					throw new ArgumentNullException("d");
				}
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
				swapChainDescription.SampleDescription = new SampleDescription(1, 0);
				swapChainDescription.Usage = 32;
				swapChainDescription.BufferCount = 2;
				swapChainDescription.Scaling = 0;
				swapChainDescription.SwapEffect = 4;
				swapChainDescription.AlphaMode = 1;
				swapChainDescription.Flags = 2048;
				SwapChainDescription1 description = swapChainDescription;
				using (Factory2 dxgiFactory = new Factory2(false))
				{
					this.swapChain = new SwapChain1(dxgiFactory, this.d3dDevice, ref description, null);
					if (this.swapChain == null)
					{
						throw new Exception("BasicCaptureAsync|create swapChain failed!");
					}
				}
				this.framePool = Direct3D11CaptureFramePool.CreateFreeThreaded(this.device, 87, 3, i.Size);
				this.session = this.framePool.CreateCaptureSession(i);
				BasicCaptureAsync.TrySetCaptureProperty(this.session, "IsBorderRequired", false);
				BasicCaptureAsync.TrySetCaptureProperty(this.session, "IsCursorCaptureEnabled", false);
				this.lastSize = i.Size;
				using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
				{
					this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
				}
				this._stagingTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
				this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
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
				this._lastCaptureFpsTime = DateTime.Now;
				Direct3D11CaptureFramePool @object = this.framePool;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Direct3D11CaptureFramePool, object>>(new Func<TypedEventHandler<Direct3D11CaptureFramePool, object>, EventRegistrationToken>(@object.add_FrameArrived), new Action<EventRegistrationToken>(@object.remove_FrameArrived), new TypedEventHandler<Direct3D11CaptureFramePool, object>(this.OnFrameArrived));
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("BasicCaptureAsync|initialized successfully!");
			}
			catch (Exception e)
			{
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureAsync|failed to initialize: " + e.Message);
				this.CleanupResources();
				throw;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00016C20 File Offset: 0x00014E20
		private void InitializeBoeRenderer(int width, int height)
		{
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				MCUSdk.BOE_MCU_setCallback(null);
				if (MCUSdk.BOE_MCU_openMCU() != 0)
				{
					NLogService.WriteLog("BOE_MCU_openMCU failed");
				}
				double[] deviceParam = new double[6];
				if (MCUSdk.BOE_MCU_getDeviceParam(deviceParam) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getDeviceParam failed");
				}
				double[] wholeMat = new double[6];
				if (MCUSdk.BOE_MCU_getWholeMat(wholeMat) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getWholeMat failed");
				}
				double[] refractionParam = new double[8];
				if (MCUSdk.BOE_MCU_getRefractionDeviceParam(refractionParam) != 0)
				{
					NLogService.WriteLog("BOE_MCU_getRefractionDeviceParam failed");
				}
				if (Interleaving.InitSDK(this.d3dDevice.NativePointer, IntPtr.Zero, deviceParam, wholeMat, refractionParam, D3DVersion.dx11, 0, 0, width, height, 87, "./logs/") != 0)
				{
					NLogService.WriteLog("BOE_Interleaving_initSDK failed");
				}
				Interleaving.SetWholeParam(wholeMat);
				Interleaving.SetDeviceParam(deviceParam);
				Interleaving.SetRefractDeviceParam(refractionParam);
				NLogService.WriteLog("BasicCaptureAsync|Interweaving SDK initialized successfully!");
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00016D10 File Offset: 0x00014F10
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

		// Token: 0x0600030E RID: 782 RVA: 0x00016D88 File Offset: 0x00014F88
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

		// Token: 0x0600030F RID: 783 RVA: 0x00016DBC File Offset: 0x00014FBC
		public void StartCapture()
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 3, 2);
			if (previousState != 2)
			{
				NLogService.WriteLog(string.Format("BasicCaptureAsync|Cannot start capture from state: {0}", (CaptureState)previousState));
				return;
			}
			try
			{
				this.OpenRasterSafe();
				this._renderCts = new CancellationTokenSource();
				this._renderTask = Task.Run(delegate()
				{
					this.RenderLoop();
				}, this._renderCts.Token);
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
				NLogService.WriteLog("状态跟踪:开始抓屏Session + 渲染线程");
			}
			catch (Exception ex)
			{
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("状态跟踪:开始捕获失败: " + ex.Message);
				throw;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00016E9C File Offset: 0x0001509C
		public void Dispose()
		{
			if (!this._disposeSemaphore.Wait(0))
			{
				NLogService.WriteLog("BasicCaptureAsync|Dispose already in progress, skipping...");
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
					NLogService.WriteLog(string.Format("BasicCaptureAsync|Cannot dispose from state: {0}", (CaptureState)currentState));
					return;
				}
				this._frameReadyEvent.Set();
				CancellationTokenSource renderCts = this._renderCts;
				if (renderCts != null)
				{
					renderCts.Cancel();
				}
				try
				{
					Task renderTask = this._renderTask;
					if (renderTask != null)
					{
						renderTask.Wait(TimeSpan.FromSeconds(2.0));
					}
				}
				catch
				{
				}
				this._renderTask = null;
				try
				{
					GraphicsCaptureSession graphicsCaptureSession = this.session;
					if (graphicsCaptureSession != null)
					{
						graphicsCaptureSession.Dispose();
					}
					this.session = null;
				}
				catch
				{
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
					FormatRecognition formatRecognition = this.formatRecognition;
					if (formatRecognition != null)
					{
						formatRecognition.Dispose();
					}
					this.formatRecognition = null;
					Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
					if (direct3D11CaptureFramePool != null)
					{
						direct3D11CaptureFramePool.Dispose();
					}
					this.framePool = null;
					object sdkLock = this._sdkLock;
					lock (sdkLock)
					{
						Interleaving.ReleaseSDK();
					}
					this.SafeDispose<Texture2D>(ref this._stagingTexture);
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
				}
				this.CloseRasterSafe();
				AutoResetEvent frameReadyEvent = this._frameReadyEvent;
				if (frameReadyEvent != null)
				{
					frameReadyEvent.Dispose();
				}
				CancellationTokenSource renderCts2 = this._renderCts;
				if (renderCts2 != null)
				{
					renderCts2.Dispose();
				}
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
				NLogService.WriteLog("BasicCaptureAsync|Dispose completed");
			}
			finally
			{
				this._disposeSemaphore.Release();
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000171A8 File Offset: 0x000153A8
		private void OnFrameArrived(Direct3D11CaptureFramePool sender, object args)
		{
			if (this.IsDisposing() || this._isDisposed)
			{
				return;
			}
			try
			{
				using (Direct3D11CaptureFrame frame = sender.TryGetNextFrame())
				{
					if (frame != null)
					{
						if (frame.ContentSize.Width != this.lastSize.Width || frame.ContentSize.Height != this.lastSize.Height)
						{
							this._pendingSize = frame.ContentSize;
							this._needsResize = true;
						}
						Texture2D staging = this._stagingTexture;
						if (staging != null && !this._needsResize)
						{
							using (Texture2D bitmap = Direct3D11Helper.CreateSharpDXTexture2D(frame.Surface))
							{
								this.d3dDevice.ImmediateContext.CopyResource(bitmap, staging);
								this._hasNewFrame = true;
								this._frameReadyEvent.Set();
							}
						}
					}
				}
			}
			catch (Exception ex) when (!this.IsDisposing())
			{
				NLogService.WriteLog("OnFrameArrived error: " + ex.Message);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000172E4 File Offset: 0x000154E4
		private void RenderLoop()
		{
			while (!this._renderCts.IsCancellationRequested)
			{
				if (!this._frameReadyEvent.WaitOne(10))
				{
					this.ProcessFrameRender();
					SwapChain1 swapChain = this.swapChain;
					if (swapChain != null)
					{
						swapChain.Present(0, 512);
					}
					this.UpdateFPS();
				}
				else if (this._hasNewFrame)
				{
					this._hasNewFrame = false;
					while (this._frameReadyEvent.WaitOne(0))
					{
					}
					try
					{
						if (this._needsResize)
						{
							this.HandleResize();
						}
						else
						{
							if (this._stagingTexture != null && this.frameTexture != null)
							{
								this.d3dDevice.ImmediateContext.CopyResource(this._stagingTexture, this.frameTexture);
							}
							this.ProcessFrameRender();
							SwapChain1 swapChain2 = this.swapChain;
							if (swapChain2 != null)
							{
								swapChain2.Present(0, 512);
							}
							this.UpdateFPS();
						}
					}
					catch (Exception ex) when (!this.IsDisposing())
					{
						NLogService.WriteLog("RenderLoop error: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00017410 File Offset: 0x00015610
		private void HandleResize()
		{
			object resizeLock = this._resizeLock;
			lock (resizeLock)
			{
				if (!this.IsDisposing())
				{
					this.lastSize = this._pendingSize;
					this._needsResize = false;
					NLogService.WriteLog(string.Format("HandleResize: {0}x{1}", this.lastSize.Width, this.lastSize.Height));
					this.SafeDispose<Texture2D>(ref this._stagingTexture);
					this.SafeDispose<Texture2D>(ref this.frameTexture);
					this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
					this._stagingTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
					this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
					this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
					this.SafeDispose<RenderTargetView>(ref this.backRenderTargetView);
					this.swapChain.ResizeBuffers(2, this.lastSize.Width, this.lastSize.Height, 87, 2048);
					using (Texture2D bb = this.swapChain.GetBackBuffer<Texture2D>(0))
					{
						this.backRenderTargetView = new RenderTargetView(this.d3dDevice, bb);
						this.viewportF.Width = (float)this.lastSize.Width;
						this.viewportF.Height = (float)this.lastSize.Height;
						Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
						if (direct3D11CaptureFramePool != null)
						{
							direct3D11CaptureFramePool.Recreate(this.device, 87, 2, this.lastSize);
						}
					}
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000175F8 File Offset: 0x000157F8
		private void ProcessFrameRender()
		{
			LocalEnumeration.InterWaveType status = this.GetCurrentInterwaveStatus();
			if (status == LocalEnumeration.InterWaveType.InterWave_None)
			{
				try
				{
					ValueTuple<byte[], int> valueTuple = this.formatRecognition.DrawFrame(this.d3dDevice, this.frameResourceView);
					byte[] buffer = valueTuple.Item1;
					int rowPitch = valueTuple.Item2;
					YLStereoMatcher ylstereoMatcher = this.stereoMatcher;
					this.ResetCurrentInterwaveStatus(((ylstereoMatcher != null) ? new bool?(ylstereoMatcher.ExtractAndMatch(buffer, this.formatRecognition.scaleW, this.formatRecognition.scaleH, rowPitch)) : null).GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
					FormatRecognition formatRecognition = this.formatRecognition;
					if (formatRecognition != null)
					{
						formatRecognition.DrawFrame(this.d3dDevice, this.frameResourceView, this.backRenderTargetView, this.viewportF);
					}
					goto IL_1E2;
				}
				catch (Exception e)
				{
					NLogService.WriteLog("stereoMatcher error: " + e.Message);
					this.ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType.InterWave_AI);
					goto IL_1E2;
				}
			}
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert != null)
			{
				frameConvert.DrawFrame(this.d3dDevice, this.frameResourceView, status);
			}
			try
			{
				RawColor4 clearColor;
				clearColor..ctor(0f, 0f, 0f, 1f);
				this.d3dDevice.ImmediateContext.OutputMerger.SetRenderTargets(this.backRenderTargetView);
				this.d3dDevice.ImmediateContext.Rasterizer.SetViewport(this.viewportF);
				this.d3dDevice.ImmediateContext.ClearRenderTargetView(this.backRenderTargetView, clearColor);
				ValueTuple<float, float, float> pose = this.GetPose();
				float x = pose.Item1;
				float y = pose.Item2;
				float z = pose.Item3;
				Interleaving.DoInterleave(x, y, z, this.d3dDevice.ImmediateContext.NativePointer, this.backRenderTargetView.NativePointer, this.frameConvert.GetShaderResource().NativePointer, this.lastSize.Width, this.lastSize.Height, 87, InterMode.RLInter, 0);
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("Render error: " + ex.Message);
			}
			IL_1E2:
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType cur = this.currentInterwaveStatus;
			LocalEnumeration.InterWaveType interWaveType = this.lastInterwaveStatus;
			this._statusLock.ExitReadLock();
			if (interWaveType != cur)
			{
				this.lastInterwaveStatus = cur;
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher == null)
				{
					return;
				}
				localEventPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)cur);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00017848 File Offset: 0x00015A48
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

		// Token: 0x06000316 RID: 790 RVA: 0x000179D8 File Offset: 0x00015BD8
		private ValueTuple<float, float, float> GetPose()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float x = pose.Item1;
			float y = pose.Item2;
			float z = pose.Item3;
			float item = pose.Item4;
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
			if (item == 0f)
			{
				this.faceTrackingState = -1;
			}
			return new ValueTuple<float, float, float>(x, y, z);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00017AB1 File Offset: 0x00015CB1
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00017ABA File Offset: 0x00015CBA
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00017ADC File Offset: 0x00015CDC
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

		// Token: 0x0600031B RID: 795 RVA: 0x00017B1C File Offset: 0x00015D1C
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

		// Token: 0x0600031C RID: 796 RVA: 0x00017B80 File Offset: 0x00015D80
		private bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00017B94 File Offset: 0x00015D94
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

		// Token: 0x0600031E RID: 798 RVA: 0x00017BE8 File Offset: 0x00015DE8
		private void UpdateCaptureFPS()
		{
			try
			{
				DateTime currentTime = DateTime.Now;
				if ((currentTime - this._lastCaptureFpsTime).TotalMilliseconds >= 1000.0)
				{
					this._captureFps = this._captureFrameCount;
					this._captureFrameCount = 0;
					this._lastCaptureFpsTime = currentTime;
					NLogService.WriteLog(string.Format("Capture FPS: {0}", this._captureFps));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00017C64 File Offset: 0x00015E64
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
			return new Texture2D(device, texture2DDescription);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00017CD7 File Offset: 0x00015ED7
		private void UninitResource()
		{
			this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
			this.SafeDispose<Texture2D>(ref this.frameTexture);
			this.SafeDispose<RenderTargetView>(ref this.backRenderTargetView);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00017D00 File Offset: 0x00015F00
		private void SafeDispose<T>(ref T resource) where T : class, IDisposable
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

		// Token: 0x06000322 RID: 802 RVA: 0x00017D44 File Offset: 0x00015F44
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
				Texture2D stagingTexture = this._stagingTexture;
				if (stagingTexture != null)
				{
					stagingTexture.Dispose();
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

		// Token: 0x06000323 RID: 803 RVA: 0x00017EB4 File Offset: 0x000160B4
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

		// Token: 0x04000264 RID: 612
		private GraphicsCaptureItem item;

		// Token: 0x04000265 RID: 613
		private Direct3D11CaptureFramePool framePool;

		// Token: 0x04000266 RID: 614
		private GraphicsCaptureSession session;

		// Token: 0x04000267 RID: 615
		private SizeInt32 lastSize;

		// Token: 0x04000268 RID: 616
		private IDirect3DDevice device;

		// Token: 0x04000269 RID: 617
		private Device d3dDevice;

		// Token: 0x0400026A RID: 618
		private SwapChain1 swapChain;

		// Token: 0x0400026B RID: 619
		private RenderTargetView backRenderTargetView;

		// Token: 0x0400026C RID: 620
		private Texture2D _stagingTexture;

		// Token: 0x0400026D RID: 621
		private Texture2D frameTexture;

		// Token: 0x0400026E RID: 622
		private ShaderResourceView frameResourceView;

		// Token: 0x0400026F RID: 623
		private readonly AutoResetEvent _frameReadyEvent = new AutoResetEvent(false);

		// Token: 0x04000270 RID: 624
		private volatile bool _hasNewFrame;

		// Token: 0x04000271 RID: 625
		private volatile bool _needsResize;

		// Token: 0x04000272 RID: 626
		private SizeInt32 _pendingSize;

		// Token: 0x04000273 RID: 627
		private readonly object _resizeLock = new object();

		// Token: 0x04000274 RID: 628
		private CancellationTokenSource _renderCts;

		// Token: 0x04000275 RID: 629
		private Task _renderTask;

		// Token: 0x04000276 RID: 630
		private readonly YLStereoMatcher stereoMatcher;

		// Token: 0x04000277 RID: 631
		private FormatRecognition formatRecognition;

		// Token: 0x04000278 RID: 632
		private FrameConvert frameConvert;

		// Token: 0x04000279 RID: 633
		private int faceTrackingState = -1;

		// Token: 0x0400027A RID: 634
		private LocalEventPublisher localPublisher;

		// Token: 0x0400027B RID: 635
		private LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x0400027C RID: 636
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x0400027D RID: 637
		private readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x0400027E RID: 638
		private RawViewportF viewportF;

		// Token: 0x0400027F RID: 639
		private DateTime _lastFpsTime;

		// Token: 0x04000280 RID: 640
		private int _frameCount;

		// Token: 0x04000281 RID: 641
		private int _fps;

		// Token: 0x04000282 RID: 642
		private DateTime _lastCaptureFpsTime;

		// Token: 0x04000283 RID: 643
		private int _captureFrameCount;

		// Token: 0x04000284 RID: 644
		private int _captureFps;

		// Token: 0x04000285 RID: 645
		private DateTime _lastHeartbeatTime;

		// Token: 0x04000286 RID: 646
		private float near = 400f;

		// Token: 0x04000287 RID: 647
		private float far = 1000f;

		// Token: 0x04000288 RID: 648
		private float hor_angle = 33f;

		// Token: 0x04000289 RID: 649
		private float ver_angle = 26f;

		// Token: 0x0400028A RID: 650
		private int _state;

		// Token: 0x0400028B RID: 651
		private long _isDisposingFlag;

		// Token: 0x0400028C RID: 652
		private readonly object _initLock = new object();

		// Token: 0x0400028D RID: 653
		private readonly object _sdkLock = new object();

		// Token: 0x0400028E RID: 654
		private readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x0400028F RID: 655
		private volatile bool _isDisposed;
	}
}
