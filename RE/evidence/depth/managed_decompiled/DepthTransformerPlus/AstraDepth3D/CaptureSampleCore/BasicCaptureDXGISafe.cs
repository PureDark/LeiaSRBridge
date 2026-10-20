using System;
using System.Runtime.CompilerServices;
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
	// Token: 0x02000047 RID: 71
	public class BasicCaptureDXGISafe : ICapture, IDisposable
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00014EC0 File Offset: 0x000130C0
		public BasicCaptureDXGISafe(YLStereoMatcher matcher)
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 1, 0);
			if (previousState != 0)
			{
				throw new InvalidOperationException(string.Format("BasicCaptureDXGISafe|Cannot initialize from state: {0}", (CaptureStateDXGI)previousState));
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
					throw new InvalidOperationException("BasicCaptureDXGISafe|未找到连接桌面的显示输出，无法进行DXGI截屏");
				}
				NLogService.WriteLog("BasicCaptureDXGISafe|选择适配器: " + desktopAdapter.Description.Description);
				this.d3dDevice = new Device(desktopAdapter, 40);
				this.d3dDevice.QueryInterface<DeviceMultithread>().SetMultithreadProtected(true);
				NLogService.WriteLog("BasicCaptureDXGISafe|D3D Device init success0!");
				this._outputDuplication = desktopOutput.DuplicateOutput(this.d3dDevice);
				this._outputDesc = desktopOutputDesc;
				NLogService.WriteLog("BasicCaptureDXGISafe|DuplicateOutput init success1!");
				this.lastSize = default(SizeInt32);
				this.lastSize.Width = Math.Abs(this._outputDesc.DesktopBounds.Right - this._outputDesc.DesktopBounds.Left);
				this.lastSize.Height = Math.Abs(this._outputDesc.DesktopBounds.Bottom - this._outputDesc.DesktopBounds.Top);
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
						throw new Exception("BasicCaptureDXGISafe|create swapChain failed!");
					}
				}
				using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
				{
					this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
				}
				this.InitResource();
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
				NLogService.WriteLog("BasicCaptureDXGISafe|init failed, cleaning up resources");
				this.CleanupResources();
				throw;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000154B8 File Offset: 0x000136B8
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
				NLogService.WriteLog("BasicCaptureSafe|intertwineRender init success!");
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000155A0 File Offset: 0x000137A0
		public void Dispose()
		{
			if (!this._disposeSemaphore.Wait(0))
			{
				NLogService.WriteLog("BasicCaptureDXGISafe|Dispose already in progress, skipping...");
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
				object obj = this._taskLock;
				lock (obj)
				{
					if (this._captureTask != null)
					{
						try
						{
							if (!this._captureTask.Wait(TimeSpan.FromSeconds(2.0)))
							{
								NLogService.WriteLog("BasicCaptureDXGISafe|Capture task stop timeout");
							}
						}
						catch (OperationCanceledException)
						{
						}
						catch (Exception ex)
						{
							NLogService.WriteLog("BasicCaptureDXGISafe|Wait capture task error: " + ex.Message);
						}
						this._captureTask = null;
					}
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
					NLogService.WriteLog("状态跟踪:FrameConvert已释放");
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
					NLogService.WriteLog("状态跟踪:3D交织已释放");
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
				NLogService.WriteLog("BasicCaptureDXGISafe|Dispose completed");
			}
			finally
			{
				this._disposeSemaphore.Release();
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001592C File Offset: 0x00013B2C
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

		// Token: 0x060002F8 RID: 760 RVA: 0x00015960 File Offset: 0x00013B60
		public void StartCapture()
		{
			int previousState = Interlocked.CompareExchange(ref this._state, 3, 2);
			if (previousState != 2)
			{
				NLogService.WriteLog(string.Format("BasicCaptureDXGISafe|Cannot start capture from state: {0}", (CaptureStateDXGI)previousState));
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
						NLogService.WriteLog("BasicCaptureDXGISafe|Capture task already running");
					}
					else
					{
						this._cancellationTokenSource = new CancellationTokenSource();
						this._captureTask = Task.Run(delegate()
						{
							BasicCaptureDXGISafe.<<StartCapture>b__49_0>d <<StartCapture>b__49_0>d;
							<<StartCapture>b__49_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<StartCapture>b__49_0>d.<>4__this = this;
							<<StartCapture>b__49_0>d.<>1__state = -1;
							<<StartCapture>b__49_0>d.<>t__builder.Start<BasicCaptureDXGISafe.<<StartCapture>b__49_0>d>(ref <<StartCapture>b__49_0>d);
							return <<StartCapture>b__49_0>d.<>t__builder.Task;
						}, this._cancellationTokenSource.Token);
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

		// Token: 0x060002F9 RID: 761 RVA: 0x00015A54 File Offset: 0x00013C54
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

		// Token: 0x060002FA RID: 762 RVA: 0x00015ABC File Offset: 0x00013CBC
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00015AC5 File Offset: 0x00013CC5
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00015AD3 File Offset: 0x00013CD3
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00015AE8 File Offset: 0x00013CE8
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

		// Token: 0x060002FE RID: 766 RVA: 0x00015B28 File Offset: 0x00013D28
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

		// Token: 0x060002FF RID: 767 RVA: 0x00015B8C File Offset: 0x00013D8C
		private bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00015BA0 File Offset: 0x00013DA0
		private void OnFrameArrived()
		{
			if (this.IsDisposing() || this._isDisposed || Interlocked.CompareExchange(ref this._state, -1, -1) != 3)
			{
				return;
			}
			try
			{
				OutputDuplicateFrameInformation frameInfo;
				Resource screenResource;
				Result result = this._outputDuplication.TryAcquireNextFrame(100, ref frameInfo, ref screenResource);
				NLogService.WriteLine(string.Format("AcquireNextFrame result code:{0}", result.Code));
				try
				{
					if (result.Success)
					{
						using (screenResource)
						{
							using (Texture2D bitmap = screenResource.QueryInterface<Texture2D>())
							{
								object frameLock = this._frameLock;
								lock (frameLock)
								{
									if (this.IsDisposing())
									{
										return;
									}
									Texture2DDescription desc = bitmap.Description;
									if (desc.Width != this.lastSize.Width || desc.Height != this.lastSize.Height || this.lastRotation != this._outputDesc.Rotation)
									{
										this.lastSize.Width = desc.Width;
										this.lastSize.Height = desc.Height;
										NLogService.WriteLog(string.Format("screenResource desc:{0},{1}", desc.Width, desc.Height));
										this.InitResource();
										if (this.lastRotation != this._outputDesc.Rotation)
										{
											this.displayManager.RotateTexcoord(this.d3dDevice, this.lastRotation);
										}
									}
									this.d3dDevice.ImmediateContext.CopyResource(bitmap, this.frameTexture);
									this.displayManager.DrawFrame(this.d3dDevice, this.frameResourceView, this.displayTargetView, this.viewportF);
								}
							}
							this._outputDuplication.ReleaseFrame();
						}
					}
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("处理帧时出错: " + ex.Message);
				}
				finally
				{
					if (result.Success && screenResource != null && !screenResource.IsDisposed)
					{
						screenResource.Dispose();
					}
				}
				this.ProcessFrame();
			}
			catch (Exception ex2)
			{
				NLogService.WriteLog("OnFrameArrived error: " + ex2.Message);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00015E54 File Offset: 0x00014054
		private void ProcessFrame()
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
			NLogService.WriteLine("swapChain.Present");
			this.UpdateFPS();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000161A4 File Offset: 0x000143A4
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

		// Token: 0x06000303 RID: 771 RVA: 0x000161F8 File Offset: 0x000143F8
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

		// Token: 0x06000304 RID: 772 RVA: 0x00016310 File Offset: 0x00014510
		private void InitResource()
		{
			this.SafeDispose<Texture2D>(ref this.frameTexture);
			this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
			this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
			this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
			this.CreateDisplayTextureView(this.d3dDevice, this.lastSize.Width, this.lastSize.Height, this.lastRotation);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001639C File Offset: 0x0001459C
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

		// Token: 0x06000306 RID: 774 RVA: 0x00016414 File Offset: 0x00014614
		private void CreateDisplayTextureView(Device device, int width, int height, DisplayModeRotation rotation)
		{
			int rotatedWidth = width;
			int rotatedHeight = height;
			if (rotation == 2 || rotation == 4)
			{
				rotatedWidth = height;
				rotatedHeight = width;
			}
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
			Texture2DDescription description = texture2DDescription;
			this.SafeDispose<Texture2D>(ref this.displayTexture);
			this.displayTexture = new Texture2D(device, description);
			this.SafeDispose<ShaderResourceView>(ref this.displayResourceView);
			this.displayResourceView = new ShaderResourceView(device, this.displayTexture);
			this.SafeDispose<RenderTargetView>(ref this.displayTargetView);
			this.displayTargetView = new RenderTargetView(device, this.displayTexture);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000164E8 File Offset: 0x000146E8
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

		// Token: 0x06000308 RID: 776 RVA: 0x00016540 File Offset: 0x00014740
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

		// Token: 0x04000237 RID: 567
		private SizeInt32 lastSize;

		// Token: 0x04000238 RID: 568
		private DisplayModeRotation lastRotation;

		// Token: 0x04000239 RID: 569
		private OutputDuplication _outputDuplication;

		// Token: 0x0400023A RID: 570
		private OutputDescription _outputDesc;

		// Token: 0x0400023B RID: 571
		private Device d3dDevice;

		// Token: 0x0400023C RID: 572
		private SwapChain1 swapChain;

		// Token: 0x0400023D RID: 573
		private OutputDuplicateMoveRectangle[] m_MetaDataBuffer;

		// Token: 0x0400023E RID: 574
		private int m_MetaDataSize;

		// Token: 0x0400023F RID: 575
		private RawRectangle[] m_DirtyData;

		// Token: 0x04000240 RID: 576
		private int m_DirtyDataSize;

		// Token: 0x04000241 RID: 577
		private const int TIMEOUT_INTERVAL = 100;

		// Token: 0x04000242 RID: 578
		private RenderTargetView backRenderTargetView;

		// Token: 0x04000243 RID: 579
		private Texture2D frameTexture;

		// Token: 0x04000244 RID: 580
		private ShaderResourceView frameResourceView;

		// Token: 0x04000245 RID: 581
		public Texture2D displayTexture;

		// Token: 0x04000246 RID: 582
		private RenderTargetView displayTargetView;

		// Token: 0x04000247 RID: 583
		private ShaderResourceView displayResourceView;

		// Token: 0x04000248 RID: 584
		private readonly YLStereoMatcher stereoMatcher;

		// Token: 0x04000249 RID: 585
		private FormatRecognition formatRecognition;

		// Token: 0x0400024A RID: 586
		private FrameConvert frameConvert;

		// Token: 0x0400024B RID: 587
		private DisplayManager displayManager;

		// Token: 0x0400024C RID: 588
		private int faceTrackingState;

		// Token: 0x0400024D RID: 589
		private LocalEventPublisher localPublisher;

		// Token: 0x0400024E RID: 590
		private LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x0400024F RID: 591
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x04000250 RID: 592
		private readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x04000251 RID: 593
		private RawViewportF viewportF;

		// Token: 0x04000252 RID: 594
		private DateTime _lastFpsTime;

		// Token: 0x04000253 RID: 595
		private int _frameCount;

		// Token: 0x04000254 RID: 596
		private int _fps;

		// Token: 0x04000255 RID: 597
		private DateTime _lastHeartbeatTime;

		// Token: 0x04000256 RID: 598
		private float near = 400f;

		// Token: 0x04000257 RID: 599
		private float far = 1000f;

		// Token: 0x04000258 RID: 600
		private float hor_angle = 33f;

		// Token: 0x04000259 RID: 601
		private float ver_angle = 26f;

		// Token: 0x0400025A RID: 602
		private int _state;

		// Token: 0x0400025B RID: 603
		private long _isDisposingFlag;

		// Token: 0x0400025C RID: 604
		private volatile bool _isDisposed;

		// Token: 0x0400025D RID: 605
		private readonly object _initLock = new object();

		// Token: 0x0400025E RID: 606
		private readonly object _frameLock = new object();

		// Token: 0x0400025F RID: 607
		private readonly object _sdkLock = new object();

		// Token: 0x04000260 RID: 608
		private readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000261 RID: 609
		private CancellationTokenSource _cancellationTokenSource;

		// Token: 0x04000262 RID: 610
		private Task _captureTask;

		// Token: 0x04000263 RID: 611
		private readonly object _taskLock = new object();
	}
}
