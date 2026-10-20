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
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Windows.Graphics;
using Windows.UI.Composition;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x02000045 RID: 69
	public class BasicCaptureDXGIAsync : ICapture, IDisposable
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00013460 File Offset: 0x00011660
		public BasicCaptureDXGIAsync(YLStereoMatcher matcher)
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 1, 0);
			if (previousState != 0)
			{
				throw new InvalidOperationException(string.Format("BasicCaptureDXGIAsync|Cannot initialize from state: {0}", (CaptureStateDXGI)previousState));
			}
			try
			{
				if (matcher == null)
				{
					throw new ArgumentNullException("matcher");
				}
				this.stereoMatcher = matcher;
				Adapter1 desktopAdapter = null;
				Output1 desktopOutput = null;
				OutputDescription desktopOutputDesc = default(OutputDescription);
				bool foundPrimary = false;
				using (Factory1 factory = new Factory1())
				{
					for (int i = 0; i < factory.Adapters.Length; i++)
					{
						Adapter1 adapter = factory.GetAdapter1(i);
						bool adapterSelected = false;
						for (int j = 0; j < adapter.Outputs.Length; j++)
						{
							Output output = adapter.GetOutput(j);
							OutputDescription desc = output.Description;
							if (desc.IsAttachedToDesktop)
							{
								if (desc.DesktopBounds.Left == 0 && desc.DesktopBounds.Top == 0 && !foundPrimary)
								{
									if (desktopOutput != null)
									{
										desktopOutput.Dispose();
									}
									if (desktopAdapter != null && desktopAdapter != adapter)
									{
										desktopAdapter.Dispose();
									}
									desktopAdapter = adapter;
									desktopOutput = output.QueryInterface<Output1>();
									desktopOutputDesc = desc;
									foundPrimary = true;
									adapterSelected = true;
								}
								else if (!foundPrimary && desktopAdapter == null)
								{
									desktopAdapter = adapter;
									desktopOutput = output.QueryInterface<Output1>();
									desktopOutputDesc = desc;
									adapterSelected = true;
								}
								else
								{
									output.Dispose();
								}
							}
							else
							{
								output.Dispose();
							}
						}
						if (!adapterSelected)
						{
							adapter.Dispose();
						}
					}
				}
				if (desktopAdapter == null || desktopOutput == null)
				{
					throw new InvalidOperationException("BasicCaptureDXGIAsync|未找到连接桌面的显示输出");
				}
				NLogService.WriteLog("BasicCaptureDXGIAsync|选择适配器: " + desktopAdapter.Description.Description);
				this.d3dDevice = new Device(desktopAdapter, 40);
				this.d3dDevice.QueryInterface<DeviceMultithread>().SetMultithreadProtected(true);
				this._outputDuplication = desktopOutput.DuplicateOutput(this.d3dDevice);
				this._outputDesc = desktopOutputDesc;
				SizeInt32 sizeInt = default(SizeInt32);
				sizeInt.Width = Math.Abs(this._outputDesc.DesktopBounds.Right - this._outputDesc.DesktopBounds.Left);
				sizeInt.Height = Math.Abs(this._outputDesc.DesktopBounds.Bottom - this._outputDesc.DesktopBounds.Top);
				this.lastSize = sizeInt;
				this.lastRotation = this._outputDesc.Rotation;
				NLogService.WriteLog(string.Format("DesktopBounds:{0},{1},{2},{3}", new object[]
				{
					this._outputDesc.DesktopBounds.Left,
					this._outputDesc.DesktopBounds.Top,
					this._outputDesc.DesktopBounds.Right,
					this._outputDesc.DesktopBounds.Bottom
				}));
				SwapChainDescription1 swapChainDescription = default(SwapChainDescription1);
				swapChainDescription.Width = this.lastSize.Width;
				swapChainDescription.Height = this.lastSize.Height;
				swapChainDescription.Format = 87;
				swapChainDescription.Stereo = true;
				swapChainDescription.SampleDescription = new SampleDescription(1, 0);
				swapChainDescription.Usage = 32;
				swapChainDescription.BufferCount = 2;
				swapChainDescription.Scaling = 0;
				swapChainDescription.SwapEffect = 3;
				swapChainDescription.AlphaMode = 1;
				swapChainDescription.Flags = 0;
				SwapChainDescription1 swapDesc = swapChainDescription;
				using (Factory2 dxgiFactory = new Factory2(false))
				{
					this.swapChain = new SwapChain1(dxgiFactory, this.d3dDevice, ref swapDesc, null);
				}
				using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
				{
					this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
				}
				this.InitResource();
				this.InitDoubleBuffer(this.lastSize.Width, this.lastSize.Height);
				this.displayManager = new DisplayManager();
				this.displayManager.Initialize(this.d3dDevice);
				this.formatRecognition = new FormatRecognition();
				this.formatRecognition.Initialize(this.d3dDevice);
				this.frameConvert = new FrameConvert(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
				this.InitializeBoeRenderer(this.lastSize.Width, this.lastSize.Height);
				RawViewportF rawViewportF = default(RawViewportF);
				rawViewportF.X = 0f;
				rawViewportF.Y = 0f;
				rawViewportF.Width = (float)this.lastSize.Width;
				rawViewportF.Height = (float)this.lastSize.Height;
				rawViewportF.MinDepth = 0f;
				rawViewportF.MaxDepth = 0f;
				this.viewportF = rawViewportF;
				this._cancellationTokenSource = new CancellationTokenSource();
				this._lastFpsTime = DateTime.Now;
				this._lastHeartbeatTime = DateTime.Now;
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("BasicCaptureDXGISafe|init success!");
			}
			catch (Exception)
			{
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureDXGIAsync|init failed, cleaning up resources");
				this.CleanupResources();
				throw;
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00013A40 File Offset: 0x00011C40
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
				NLogService.WriteLog("BasicCaptureDXGIAsync|intertwineRender init success!");
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00013B28 File Offset: 0x00011D28
		private void InitDoubleBuffer(int width, int height)
		{
			this.SafeDispose<Texture2D>(ref this._captureBuffer0);
			this.SafeDispose<ShaderResourceView>(ref this._captureSrv0);
			this.SafeDispose<Texture2D>(ref this._captureBuffer1);
			this.SafeDispose<ShaderResourceView>(ref this._captureSrv1);
			this._captureBuffer0 = this.CreateFrameTexture(this.d3dDevice, width, height);
			this._captureSrv0 = new ShaderResourceView(this.d3dDevice, this._captureBuffer0);
			this._captureBuffer1 = this.CreateFrameTexture(this.d3dDevice, width, height);
			this._captureSrv1 = new ShaderResourceView(this.d3dDevice, this._captureBuffer1);
			this._writeIndex = 0;
			NLogService.WriteLog(string.Format("Double buffer initialized: {0}x{1}", width, height));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00013BE0 File Offset: 0x00011DE0
		public void StartCapture()
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 3, 2);
			if (previousState != 2)
			{
				NLogService.WriteLog(string.Format("BasicCaptureDXGIAsync|Cannot start from state: {0}", (CaptureStateDXGI)previousState));
				return;
			}
			try
			{
				this.OpenRasterSafe();
				NLogService.WriteLog("状态跟踪:开始抓屏Session");
				object taskLock = this._taskLock;
				lock (taskLock)
				{
					if (this._captureTask != null && !this._captureTask.IsCompleted)
					{
						NLogService.WriteLog("BasicCaptureDXGIAsync|Capture task already running");
					}
					else
					{
						this._cancellationTokenSource = new CancellationTokenSource();
						CancellationToken token = this._cancellationTokenSource.Token;
						this._captureTask = Task.Run(delegate()
						{
							this.CaptureLoop(token);
						}, token);
						this._renderTask = Task.Run(delegate()
						{
							this.RenderLoop(token);
						}, token);
					}
				}
			}
			catch (Exception ex)
			{
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("状态跟踪:开始捕获失败: " + ex.Message);
				throw;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00013D10 File Offset: 0x00011F10
		private void CaptureLoop(CancellationToken token)
		{
			for (int i = 0; i < 3; i++)
			{
				if (token.IsCancellationRequested)
				{
					break;
				}
				OutputDuplicateFrameInformation outputDuplicateFrameInformation;
				Resource res;
				if (this._outputDuplication.TryAcquireNextFrame(100, ref outputDuplicateFrameInformation, ref res).Success)
				{
					this._outputDuplication.ReleaseFrame();
					if (res != null)
					{
						res.Dispose();
					}
				}
			}
			while (!token.IsCancellationRequested && !this.IsDisposing())
			{
				try
				{
					OutputDuplicateFrameInformation frameInfo;
					Resource screenResource;
					if (this._outputDuplication.TryAcquireNextFrame(100, ref frameInfo, ref screenResource).Success && screenResource != null)
					{
						using (screenResource)
						{
							using (Texture2D bitmap = screenResource.QueryInterface<Texture2D>())
							{
								Texture2DDescription desc = bitmap.Description;
								object bufferSwapLock;
								if (desc.Width != this.lastSize.Width || desc.Height != this.lastSize.Height || this.lastRotation != this._outputDesc.Rotation)
								{
									bufferSwapLock = this._bufferSwapLock;
									lock (bufferSwapLock)
									{
										this.lastSize.Width = desc.Width;
										this.lastSize.Height = desc.Height;
										NLogService.WriteLog(string.Format("Screen resize detected: {0}x{1}", desc.Width, desc.Height));
										this.InitDoubleBuffer(desc.Width, desc.Height);
										this.InitResource();
										if (this.lastRotation != this._outputDesc.Rotation)
										{
											this.lastRotation = this._outputDesc.Rotation;
											this.displayManager.RotateTexcoord(this.d3dDevice, this.lastRotation);
										}
									}
								}
								bufferSwapLock = this._bufferSwapLock;
								lock (bufferSwapLock)
								{
									Texture2D target = (this._writeIndex == 0) ? this._captureBuffer0 : this._captureBuffer1;
									this.d3dDevice.ImmediateContext.CopyResource(bitmap, target);
								}
								this._frameReadyEvent.Set();
							}
						}
						this._outputDuplication.ReleaseFrame();
					}
				}
				catch (SharpDXException ex2) when (ex2.HResult == -2005270490)
				{
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("CaptureLoop error: " + ex.Message);
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001400C File Offset: 0x0001220C
		private void RenderLoop(CancellationToken token)
		{
			while (!token.IsCancellationRequested && !this.IsDisposing())
			{
				if (this._frameReadyEvent.WaitOne(100))
				{
					try
					{
						object obj = this._bufferSwapLock;
						ShaderResourceView readSrv;
						lock (obj)
						{
							readSrv = ((this._writeIndex == 0) ? this._captureSrv0 : this._captureSrv1);
							this._writeIndex = 1 - this._writeIndex;
						}
						LocalEnumeration.InterWaveType status = this.GetCurrentInterwaveStatus();
						if (status == LocalEnumeration.InterWaveType.InterWave_None)
						{
							ValueTuple<byte[], int> valueTuple = this.formatRecognition.DrawFrame(this.d3dDevice, readSrv);
							byte[] buffer = valueTuple.Item1;
							int rowPitch = valueTuple.Item2;
							YLStereoMatcher ylstereoMatcher = this.stereoMatcher;
							bool? isStereo = (ylstereoMatcher != null) ? new bool?(ylstereoMatcher.ExtractAndMatch(buffer, this.formatRecognition.scaleW, this.formatRecognition.scaleH, rowPitch)) : null;
							this.ResetCurrentInterwaveStatus(isStereo.GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
							NLogService.WriteLog(string.Format("check stereo format:{0}", isStereo));
							FormatRecognition formatRecognition = this.formatRecognition;
							if (formatRecognition != null)
							{
								formatRecognition.DrawFrame(this.d3dDevice, readSrv, this.backRenderTargetView, this.viewportF);
							}
						}
						else
						{
							FrameConvert frameConvert = this.frameConvert;
							if (frameConvert != null)
							{
								frameConvert.DrawFrame(this.d3dDevice, readSrv, status);
							}
							obj = this._sdkLock;
							lock (obj)
							{
								RawColor4 clearColor;
								clearColor..ctor(0f, 0f, 0f, 1f);
								DeviceContext ctx = this.d3dDevice.ImmediateContext;
								ctx.OutputMerger.SetRenderTargets(this.backRenderTargetView);
								ctx.Rasterizer.SetViewport(this.viewportF);
								ctx.ClearRenderTargetView(this.backRenderTargetView, clearColor);
								ValueTuple<float, float, float> pose = this.GetPose();
								float x = pose.Item1;
								float y = pose.Item2;
								float z = pose.Item3;
								Interleaving.DoInterleave(x, y, z, ctx.NativePointer, this.backRenderTargetView.NativePointer, this.frameConvert.GetShaderResource().NativePointer, this.lastSize.Width, this.lastSize.Height, 87, InterMode.RLInter, 0);
							}
						}
						this._statusLock.EnterReadLock();
						LocalEnumeration.InterWaveType cur = this.currentInterwaveStatus;
						LocalEnumeration.InterWaveType interWaveType = this.lastInterwaveStatus;
						this._statusLock.ExitReadLock();
						if (interWaveType != cur)
						{
							this.lastInterwaveStatus = cur;
							LocalEventPublisher localEventPublisher = this.localPublisher;
							if (localEventPublisher != null)
							{
								localEventPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)cur);
							}
						}
						SwapChain1 swapChain = this.swapChain;
						if (swapChain != null)
						{
							swapChain.Present(0, 0);
						}
						this.UpdateFPS();
					}
					catch (Exception ex)
					{
						NLogService.WriteLog("RenderLoop error: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001430C File Offset: 0x0001250C
		public void Dispose()
		{
			if (!this._disposeSemaphore.Wait(0))
			{
				NLogService.WriteLog("BasicCaptureDXGIAsync|Dispose already in progress");
				return;
			}
			try
			{
				Interlocked.Exchange(ref this._isDisposingFlag, 1L);
				int currentState = Interlocked.CompareExchange(ref this._state, 5, 3);
				if (currentState == 2)
				{
					Interlocked.Exchange(ref this._state, 5);
				}
				else if (currentState != 3 && currentState != 2)
				{
					NLogService.WriteLog(string.Format("BasicCaptureDXGISafe|Cannot dispose from state: {0}", (CaptureStateDXGI)currentState));
					return;
				}
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._frameReadyEvent.Set();
				object obj = this._taskLock;
				lock (obj)
				{
					foreach (Task t in new Task[]
					{
						this._captureTask,
						this._renderTask
					})
					{
						if (t != null)
						{
							try
							{
								if (!t.Wait(TimeSpan.FromSeconds(2.0)))
								{
									NLogService.WriteLog("Task stop timeout");
								}
							}
							catch (OperationCanceledException)
							{
							}
							catch (Exception ex)
							{
								NLogService.WriteLog("Wait task error: " + ex.Message);
							}
						}
					}
					this._captureTask = null;
					this._renderTask = null;
				}
				obj = this._initLock;
				lock (obj)
				{
					try
					{
						OutputDuplication outputDuplication = this._outputDuplication;
						if (outputDuplication != null)
						{
							outputDuplication.Dispose();
						}
						this._outputDuplication = null;
					}
					catch (Exception ex2)
					{
						NLogService.WriteLog("Dispose OutputDuplication error: " + ex2.Message);
					}
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
					object sdkLock = this._sdkLock;
					lock (sdkLock)
					{
						Interleaving.ReleaseSDK();
					}
					DisplayManager displayManager = this.displayManager;
					if (displayManager != null)
					{
						displayManager.Dispose();
					}
					this.displayManager = null;
					this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
					this.SafeDispose<Texture2D>(ref this.frameTexture);
					this.SafeDispose<Texture2D>(ref this.displayTexture);
					this.SafeDispose<ShaderResourceView>(ref this.displayResourceView);
					this.SafeDispose<RenderTargetView>(ref this.displayTargetView);
					this.SafeDispose<RenderTargetView>(ref this.backRenderTargetView);
					this.SafeDispose<Texture2D>(ref this._captureBuffer0);
					this.SafeDispose<ShaderResourceView>(ref this._captureSrv0);
					this.SafeDispose<Texture2D>(ref this._captureBuffer1);
					this.SafeDispose<ShaderResourceView>(ref this._captureSrv1);
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
				NLogService.WriteLog("BasicCaptureDXGIAsync|Dispose completed");
			}
			finally
			{
				this._disposeSemaphore.Release();
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000146F4 File Offset: 0x000128F4
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
							int state = MCUSdk.BOE_MCU_getUSBState();
							NLogService.WriteLog(string.Format("状态跟踪:透镜状态:{0};(0=离线,1=打开,2=在线未打开,3=被占用)", state));
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

		// Token: 0x060002E5 RID: 741 RVA: 0x00014728 File Offset: 0x00012928
		private void OpenRasterSafe()
		{
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				if (MCUSdk.BOE_MCU_set3DMode(true) == 0)
				{
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ON;
					NLogService.WriteLog("状态跟踪:透镜已经打开");
				}
				else
				{
					NLogService.WriteLog("状态跟踪:透镜打开失败");
				}
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00014790 File Offset: 0x00012990
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00014799 File Offset: 0x00012999
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000147A7 File Offset: 0x000129A7
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000147BC File Offset: 0x000129BC
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

		// Token: 0x060002EA RID: 746 RVA: 0x000147FC File Offset: 0x000129FC
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

		// Token: 0x060002EB RID: 747 RVA: 0x00014860 File Offset: 0x00012A60
		private bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00014874 File Offset: 0x00012A74
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

		// Token: 0x060002ED RID: 749 RVA: 0x000148C8 File Offset: 0x00012AC8
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

		// Token: 0x060002EE RID: 750 RVA: 0x000149E0 File Offset: 0x00012BE0
		private void InitResource()
		{
			this.SafeDispose<Texture2D>(ref this.frameTexture);
			this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
			this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
			this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
			this.CreateDisplayTextureView(this.d3dDevice, this.lastSize.Width, this.lastSize.Height, this.lastRotation);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00014A6C File Offset: 0x00012C6C
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

		// Token: 0x060002F0 RID: 752 RVA: 0x00014AE0 File Offset: 0x00012CE0
		private void CreateDisplayTextureView(Device device, int width, int height, DisplayModeRotation rotation)
		{
			int rotatedWidth = width;
			int rotatedHeight = height;
			if (rotation == 2 || rotation == 4)
			{
				rotatedWidth = height;
				rotatedHeight = width;
			}
			this.SafeDispose<Texture2D>(ref this.displayTexture);
			Texture2DDescription texture2DDescription = default(Texture2DDescription);
			texture2DDescription.Width = rotatedWidth;
			texture2DDescription.Height = rotatedHeight;
			texture2DDescription.Format = 87;
			texture2DDescription.MipLevels = 1;
			texture2DDescription.ArraySize = 1;
			texture2DDescription.SampleDescription = new SampleDescription(1, 0);
			texture2DDescription.Usage = 0;
			texture2DDescription.BindFlags = 40;
			texture2DDescription.CpuAccessFlags = 0;
			this.displayTexture = new Texture2D(device, texture2DDescription);
			this.SafeDispose<ShaderResourceView>(ref this.displayResourceView);
			this.displayResourceView = new ShaderResourceView(device, this.displayTexture);
			this.SafeDispose<RenderTargetView>(ref this.displayTargetView);
			this.displayTargetView = new RenderTargetView(device, this.displayTexture);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00014BB4 File Offset: 0x00012DB4
		private void SafeDispose<T>(ref T resource) where T : class, IDisposable
		{
			if (resource != null)
			{
				try
				{
					resource.Dispose();
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("SafeDispose error: " + ex.Message);
				}
				resource = default(T);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00014C0C File Offset: 0x00012E0C
		private void CleanupResources()
		{
			try
			{
				OutputDuplication outputDuplication = this._outputDuplication;
				if (outputDuplication != null)
				{
					outputDuplication.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				DisplayManager displayManager = this.displayManager;
				if (displayManager != null)
				{
					displayManager.Dispose();
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
				Texture2D texture2D2 = this.displayTexture;
				if (texture2D2 != null)
				{
					texture2D2.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				ShaderResourceView shaderResourceView2 = this.displayResourceView;
				if (shaderResourceView2 != null)
				{
					shaderResourceView2.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				RenderTargetView renderTargetView = this.displayTargetView;
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
				RenderTargetView renderTargetView2 = this.backRenderTargetView;
				if (renderTargetView2 != null)
				{
					renderTargetView2.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				Texture2D captureBuffer = this._captureBuffer0;
				if (captureBuffer != null)
				{
					captureBuffer.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				ShaderResourceView captureSrv = this._captureSrv0;
				if (captureSrv != null)
				{
					captureSrv.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				Texture2D captureBuffer2 = this._captureBuffer1;
				if (captureBuffer2 != null)
				{
					captureBuffer2.Dispose();
				}
			}
			catch
			{
			}
			try
			{
				ShaderResourceView captureSrv2 = this._captureSrv1;
				if (captureSrv2 != null)
				{
					captureSrv2.Dispose();
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
		}

		// Token: 0x04000200 RID: 512
		private SizeInt32 lastSize;

		// Token: 0x04000201 RID: 513
		private DisplayModeRotation lastRotation;

		// Token: 0x04000202 RID: 514
		private OutputDuplication _outputDuplication;

		// Token: 0x04000203 RID: 515
		private OutputDescription _outputDesc;

		// Token: 0x04000204 RID: 516
		private Device d3dDevice;

		// Token: 0x04000205 RID: 517
		private SwapChain1 swapChain;

		// Token: 0x04000206 RID: 518
		private const int TIMEOUT_INTERVAL = 100;

		// Token: 0x04000207 RID: 519
		private RenderTargetView backRenderTargetView;

		// Token: 0x04000208 RID: 520
		private Texture2D frameTexture;

		// Token: 0x04000209 RID: 521
		private ShaderResourceView frameResourceView;

		// Token: 0x0400020A RID: 522
		public Texture2D displayTexture;

		// Token: 0x0400020B RID: 523
		private RenderTargetView displayTargetView;

		// Token: 0x0400020C RID: 524
		private ShaderResourceView displayResourceView;

		// Token: 0x0400020D RID: 525
		private readonly YLStereoMatcher stereoMatcher;

		// Token: 0x0400020E RID: 526
		private FormatRecognition formatRecognition;

		// Token: 0x0400020F RID: 527
		private FrameConvert frameConvert;

		// Token: 0x04000210 RID: 528
		private DisplayManager displayManager;

		// Token: 0x04000211 RID: 529
		private int faceTrackingState;

		// Token: 0x04000212 RID: 530
		private LocalEventPublisher localPublisher;

		// Token: 0x04000213 RID: 531
		private LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x04000214 RID: 532
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x04000215 RID: 533
		private readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x04000216 RID: 534
		private RawViewportF viewportF;

		// Token: 0x04000217 RID: 535
		private DateTime _lastFpsTime;

		// Token: 0x04000218 RID: 536
		private int _frameCount;

		// Token: 0x04000219 RID: 537
		private int _fps;

		// Token: 0x0400021A RID: 538
		private DateTime _lastHeartbeatTime;

		// Token: 0x0400021B RID: 539
		private float near = 400f;

		// Token: 0x0400021C RID: 540
		private float far = 1000f;

		// Token: 0x0400021D RID: 541
		private float hor_angle = 33f;

		// Token: 0x0400021E RID: 542
		private float ver_angle = 26f;

		// Token: 0x0400021F RID: 543
		private int _state;

		// Token: 0x04000220 RID: 544
		private long _isDisposingFlag;

		// Token: 0x04000221 RID: 545
		private volatile bool _isDisposed;

		// Token: 0x04000222 RID: 546
		private readonly object _initLock = new object();

		// Token: 0x04000223 RID: 547
		private readonly object _sdkLock = new object();

		// Token: 0x04000224 RID: 548
		private readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000225 RID: 549
		private CancellationTokenSource _cancellationTokenSource;

		// Token: 0x04000226 RID: 550
		private Task _captureTask;

		// Token: 0x04000227 RID: 551
		private Task _renderTask;

		// Token: 0x04000228 RID: 552
		private readonly object _taskLock = new object();

		// Token: 0x04000229 RID: 553
		private Texture2D _captureBuffer0;

		// Token: 0x0400022A RID: 554
		private Texture2D _captureBuffer1;

		// Token: 0x0400022B RID: 555
		private ShaderResourceView _captureSrv0;

		// Token: 0x0400022C RID: 556
		private ShaderResourceView _captureSrv1;

		// Token: 0x0400022D RID: 557
		private volatile int _writeIndex;

		// Token: 0x0400022E RID: 558
		private readonly AutoResetEvent _frameReadyEvent = new AutoResetEvent(false);

		// Token: 0x0400022F RID: 559
		private readonly object _bufferSwapLock = new object();
	}
}
