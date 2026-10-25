using System;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using AiStereoKitWrapper;
using DepthTransformer.Composition.WindowsRuntimeHelpers;
using DepthTransformer.EventBus;
using DepthTransformer.Tracker;
using DepthTransformer.WindowHelper;
using ManageLF3DRenderCore;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Windows.Foundation;
using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI.Composition;

namespace DepthTransformer.CaptureSampleCore
{
	// Token: 0x02000062 RID: 98
	public class BasicCapture : ICapture, IDisposable
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0001A088 File Offset: 0x00018288
		public BasicCapture(IDirect3DDevice d, GraphicsCaptureItem i, YLStereoMatcher matcher)
		{
			int num = Interlocked.CompareExchange(ref this._state, 1, 0);
			if (num != 0)
			{
				throw new InvalidOperationException(string.Format("BasicCaptureSafe|Cannot initialize from state: {0}", (CaptureState)num));
			}
			try
			{
				ValueTuple<float, float, float, float> viewableArea = Facetracker.Instance.GetViewableArea();
				this.near = viewableArea.Item1;
				this.far = viewableArea.Item2;
				this.hor_angle = viewableArea.Item3;
				this.ver_angle = viewableArea.Item4;
				this.item = i;
				this.device = d;
				if (matcher == null)
				{
					throw new ArgumentNullException("matcher");
				}
				this.stereoMatcher = matcher;
				this.d3dDevice = Direct3D11Helper.CreateSharpDXDevice(this.device);
				this._immediateContext = this.d3dDevice.ImmediateContext;
				using (DeviceMultithread deviceMultithread = this.d3dDevice.QueryInterface<DeviceMultithread>())
				{
					deviceMultithread.SetMultithreadProtected(true);
				}
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
				SwapChainDescription1 swapChainDescription2 = swapChainDescription;
				using (Factory2 factory = new Factory2(false))
				{
					this.swapChain = new SwapChain1(factory, this.d3dDevice, ref swapChainDescription2, null);
					if (this.swapChain == null)
					{
						throw new Exception("BasicCapture|create swapChain failed!");
					}
				}
				this.framePool = Direct3D11CaptureFramePool.CreateFreeThreaded(this.device, 87, 2, i.Size);
				this.session = this.framePool.CreateCaptureSession(i);
				BasicCapture.TrySetCaptureProperty(this.session, "IsBorderRequired", false);
				BasicCapture.TrySetCaptureProperty(this.session, "IsCursorCaptureEnabled", false);
				this.lastSize = i.Size;
				Direct3D11CaptureFramePool @object = this.framePool;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Direct3D11CaptureFramePool, object>>(new Func<TypedEventHandler<Direct3D11CaptureFramePool, object>, EventRegistrationToken>(@object.add_FrameArrived), new Action<EventRegistrationToken>(@object.remove_FrameArrived), new TypedEventHandler<Direct3D11CaptureFramePool, object>(this.OnFrameArrived));
				using (Texture2D backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
				{
					this.backRenderTargetView = new RenderTargetView(this.d3dDevice, backBuffer);
				}
				this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.item.Size.Width, this.item.Size.Height);
				this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
				this.formatRecognition = new FormatRecognition();
				this.formatRecognition.Initialize(this.d3dDevice);
				this.frameConvert = new FrameConvert(this.d3dDevice, this._immediateContext, i.Size.Width, i.Size.Height);
				string text = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "user.ylks";
				NLogService.WriteLog("license:" + text);
				LF3DRenderCoreManager.LoadLicense(text);
				this.lF3DRenderCore = new LF3DRenderCoreManager(this.d3dDevice.NativePointer);
				this.GetCompFile();
				this.lF3DRenderCore.SharpInitialize("compFile");
				this.lF3DRenderCore.SharpSetFormat(2, 1);
				string config = this.GetConfig();
				NLogService.WriteLog("config:" + config);
				this.lF3DRenderCore.SharpLoadDeviceConfigFromFile(config, false, false);
				this.lF3DRenderCore.SharpSetWindowPos(0, 0, this.item.Size.Width, this.item.Size.Height);
				this.lF3DRenderCore.SharpSetTexture(this.frameConvert.GetTexture().NativePointer, 0, 0);
				this.lF3DRenderCore.SharpUpdateEyeInfo(0f, 0f, 1.3f, true, 1f, 0f);
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
			catch (Exception ex)
			{
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureSafe|failed to initialize：" + ex.Message);
				this.CleanupResources();
				throw;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001A648 File Offset: 0x00018848
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
				int num = Interlocked.CompareExchange(ref this._state, 4, 3);
				if (num == 2)
				{
					Interlocked.Exchange(ref this._state, 4);
				}
				else if (num != 3 && num != 2)
				{
					NLogService.WriteLog(string.Format("BasicCaptureSafe|Cannot dispose from state: {0}", (CaptureState)num));
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
					NLogService.WriteLog("状态跟踪:FrameConvert已释放\n");
					Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
					if (direct3D11CaptureFramePool != null)
					{
						direct3D11CaptureFramePool.Dispose();
					}
					this.framePool = null;
					FormatRecognition formatRecognition = this.formatRecognition;
					if (formatRecognition != null)
					{
						formatRecognition.Dispose();
					}
					this.formatRecognition = null;
					LF3DRenderCoreManager lf3DRenderCoreManager = this.lF3DRenderCore;
					if (lf3DRenderCoreManager != null)
					{
						lf3DRenderCoreManager.SharpRelease();
					}
					LF3DRenderCoreManager lf3DRenderCoreManager2 = this.lF3DRenderCore;
					if (lf3DRenderCoreManager2 != null)
					{
						lf3DRenderCoreManager2.Dispose();
					}
					this.lF3DRenderCore = null;
					NLogService.WriteLog("状态跟踪:3D交织已释放\n");
					this.UninitResource();
					DeviceContext immediateContext = this._immediateContext;
					if (immediateContext != null)
					{
						immediateContext.ClearState();
					}
					SwapChain1 swapChain = this.swapChain;
					if (swapChain != null)
					{
						swapChain.Dispose();
					}
					this.swapChain = null;
					this._immediateContext.Dispose();
					this._immediateContext = null;
					Device device = this.d3dDevice;
					if (device != null)
					{
						device.Dispose();
					}
					this.d3dDevice = null;
					NLogService.WriteLog("状态跟踪:释放所有资源\n");
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
				NLogService.WriteLog("BasicCaptureSafe|Dispose completed");
			}
			finally
			{
				this._disposeSemaphore.Release();
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		public void StartCapture()
		{
			int num = Interlocked.CompareExchange(ref this._state, 3, 2);
			if (num != 2)
			{
				NLogService.WriteLog(string.Format("BasicCaptureSafe|Cannot start capture from state: {0}", (CaptureState)num));
				return;
			}
			try
			{
				NLogService.WriteLog("状态跟踪:开始抓屏Session\n");
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
				NLogService.WriteLog("状态跟踪:开始抓屏线程\n");
			}
			catch (Exception ex)
			{
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("状态跟踪:开始捕获失败: " + ex.Message);
				throw;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001A9AC File Offset: 0x00018BAC
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001A9B5 File Offset: 0x00018BB5
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001A9C3 File Offset: 0x00018BC3
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001A9D8 File Offset: 0x00018BD8
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

		// Token: 0x060003AA RID: 938 RVA: 0x0001AA18 File Offset: 0x00018C18
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

		// Token: 0x060003AB RID: 939 RVA: 0x0001AA7C File Offset: 0x00018C7C
		private bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001AA90 File Offset: 0x00018C90
		private void OnFrameArrived(Direct3D11CaptureFramePool sender, object args)
		{
			if (this.IsDisposing() || this._isDisposed || Interlocked.CompareExchange(ref this._state, -1, -1) != 3)
			{
				return;
			}
			try
			{
				this.UpdateFaceTrackingState();
				bool newSize = false;
				using (Direct3D11CaptureFrame direct3D11CaptureFrame = sender.TryGetNextFrame())
				{
					if (direct3D11CaptureFrame.ContentSize.Width != this.lastSize.Width || direct3D11CaptureFrame.ContentSize.Height != this.lastSize.Height)
					{
						newSize = true;
						this.lastSize = direct3D11CaptureFrame.ContentSize;
						object frameLock = this._frameLock;
						lock (frameLock)
						{
							if (this.IsDisposing())
							{
								return;
							}
							this.SafeDispose<Texture2D>(ref this.frameTexture);
							this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
							this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
							this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
							RenderTargetView renderTargetView = this.backRenderTargetView;
							if (renderTargetView != null)
							{
								renderTargetView.Dispose();
							}
							this.backRenderTargetView = null;
							this.swapChain.ResizeBuffers(2, this.lastSize.Width, this.lastSize.Height, 87, 0);
							using (Texture2D backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
							{
								this.backRenderTargetView = new RenderTargetView(this.d3dDevice, backBuffer);
							}
						}
					}
					using (Texture2D texture2D = Direct3D11Helper.CreateSharpDXTexture2D(direct3D11CaptureFrame.Surface))
					{
						object frameLock = this._frameLock;
						lock (frameLock)
						{
							if (this.IsDisposing())
							{
								return;
							}
							this._immediateContext.CopyResource(texture2D, this.frameTexture);
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

		// Token: 0x060003AD RID: 941 RVA: 0x0001AD18 File Offset: 0x00018F18
		private void ProcessFrame(bool newSize)
		{
			LocalEnumeration.InterWaveType interWaveType = this.GetCurrentInterwaveStatus();
			object frameLock;
			if (interWaveType == LocalEnumeration.InterWaveType.InterWave_None)
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
						ValueTuple<byte[], int> valueTuple = this.formatRecognition.DrawFrame(this._immediateContext, this.frameResourceView);
						byte[] item = valueTuple.Item1;
						int item2 = valueTuple.Item2;
						YLStereoMatcher ylstereoMatcher = this.stereoMatcher;
						bool? flag2 = (ylstereoMatcher != null) ? new bool?(ylstereoMatcher.ExtractAndMatch(item, this.formatRecognition.scaleW, this.formatRecognition.scaleH, item2)) : null;
						this.ResetCurrentInterwaveStatus(flag2.GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
						NLogService.WriteLog(string.Format("check stereo format:{0}", flag2));
						FormatRecognition formatRecognition = this.formatRecognition;
						if (formatRecognition != null)
						{
							formatRecognition.DrawFrame(this._immediateContext, this.frameResourceView, this.backRenderTargetView, this.viewportF);
						}
						goto IL_1DB;
					}
					catch (Exception ex)
					{
						NLogService.WriteLog("stereoMatcher error:" + ex.Message);
						this.ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType.InterWave_AI);
						goto IL_1DB;
					}
				}
			}
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert != null)
			{
				frameConvert.DrawFrame(this._immediateContext, this.frameResourceView, interWaveType);
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
					this._immediateContext.OutputMerger.SetRenderTargets(this.backRenderTargetView);
					this._immediateContext.Rasterizer.SetViewport(this.viewportF);
					RawColor4 rawColor;
					rawColor..ctor(0f, 0f, 0f, 1f);
					this._immediateContext.ClearRenderTargetView(this.backRenderTargetView, rawColor);
					this.lF3DRenderCore.SharpDraw();
				}
				catch (Exception ex2)
				{
					NLogService.WriteLog("Render error: " + ex2.Message);
				}
			}
			IL_1DB:
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType interWaveType2 = this.currentInterwaveStatus;
			LocalEnumeration.InterWaveType interWaveType3 = this.lastInterwaveStatus;
			this._statusLock.ExitReadLock();
			if (interWaveType3 != interWaveType2)
			{
				this.lastInterwaveStatus = interWaveType2;
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher != null)
				{
					localEventPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)interWaveType2);
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

		// Token: 0x060003AE RID: 942 RVA: 0x0001AFBC File Offset: 0x000191BC
		private void UpdateFPS()
		{
			DateTime now = DateTime.Now;
			if ((now - this._lastHeartbeatTime).TotalMilliseconds >= 50.0)
			{
				this._lastHeartbeatTime = now;
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher == null)
				{
					return;
				}
				localEventPublisher.DoSomethingAsync("FaceTrackingStatusChanged", this.faceTrackingState);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001B010 File Offset: 0x00019210
		private void UpdateFaceTrackingState()
		{
			ValueTuple<float, float, float, float, float> pose = Facetracker.Instance.GetPose();
			float item = pose.Item1;
			float item2 = pose.Item2;
			float item3 = pose.Item3;
			double num = Math.Atan((double)Math.Abs(item / item3)) * 57.29577951308232;
			double num2 = Math.Atan((double)Math.Abs(item2 / item3)) * 57.29577951308232;
			if (num <= (double)this.hor_angle && num2 <= (double)this.ver_angle && item3 <= this.far && item3 >= this.near)
			{
				this.lF3DRenderCore.SharpUpdateEyeInfo(item, item2, item3, item3 > 0f, 1f, 0f);
				this.faceTrackingState = 99;
				return;
			}
			if (item3 < this.near && item3 > 0f)
			{
				this.faceTrackingState = 1;
				return;
			}
			if (item3 <= 0f)
			{
				this.faceTrackingState = 0;
				return;
			}
			this.faceTrackingState = 2;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001B0EC File Offset: 0x000192EC
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
			Texture2DDescription texture2DDescription2 = texture2DDescription;
			return new Texture2D(device, texture2DDescription2);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001B161 File Offset: 0x00019361
		private void UninitResource()
		{
			this.SafeDispose<ShaderResourceView>(ref this.frameResourceView);
			this.SafeDispose<Texture2D>(ref this.frameTexture);
			this.SafeDispose<RenderTargetView>(ref this.backRenderTargetView);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001B188 File Offset: 0x00019388
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
				LF3DRenderCoreManager lf3DRenderCoreManager = this.lF3DRenderCore;
				if (lf3DRenderCoreManager != null)
				{
					lf3DRenderCoreManager.SharpRelease();
				}
				LF3DRenderCoreManager lf3DRenderCoreManager2 = this.lF3DRenderCore;
				if (lf3DRenderCoreManager2 != null)
				{
					lf3DRenderCoreManager2.Dispose();
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

		// Token: 0x060003B3 RID: 947 RVA: 0x0001B308 File Offset: 0x00019508
		private string GetConfig()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			int length = folderPath.LastIndexOf('\\');
			return folderPath.Substring(0, length) + "\\LocalLow\\ylkj\\deviceConfig.bin";
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001B338 File Offset: 0x00019538
		private string GetCompFile()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			int length = folderPath.LastIndexOf('\\');
			return folderPath.Substring(0, length) + "\\LocalLow\\ylkj\\Textures\\CpMatrix.png";
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001B368 File Offset: 0x00019568
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

		// Token: 0x060003B6 RID: 950 RVA: 0x0001B3C0 File Offset: 0x000195C0
		private static void TrySetCaptureProperty(GraphicsCaptureSession session, string propertyName, bool value)
		{
			try
			{
				PropertyInfo property = session.GetType().GetProperty(propertyName);
				if (property != null && property.CanWrite)
				{
					property.SetValue(session, value);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog(propertyName + " not supported: " + ex.Message);
			}
		}

		// Token: 0x040002FD RID: 765
		private GraphicsCaptureItem item;

		// Token: 0x040002FE RID: 766
		private Direct3D11CaptureFramePool framePool;

		// Token: 0x040002FF RID: 767
		private GraphicsCaptureSession session;

		// Token: 0x04000300 RID: 768
		private SizeInt32 lastSize;

		// Token: 0x04000301 RID: 769
		private IDirect3DDevice device;

		// Token: 0x04000302 RID: 770
		private Device d3dDevice;

		// Token: 0x04000303 RID: 771
		private DeviceContext _immediateContext;

		// Token: 0x04000304 RID: 772
		private SwapChain1 swapChain;

		// Token: 0x04000305 RID: 773
		private RenderTargetView backRenderTargetView;

		// Token: 0x04000306 RID: 774
		private Texture2D frameTexture;

		// Token: 0x04000307 RID: 775
		private ShaderResourceView frameResourceView;

		// Token: 0x04000308 RID: 776
		private readonly YLStereoMatcher stereoMatcher;

		// Token: 0x04000309 RID: 777
		private FormatRecognition formatRecognition;

		// Token: 0x0400030A RID: 778
		private FrameConvert frameConvert;

		// Token: 0x0400030B RID: 779
		private LF3DRenderCoreManager lF3DRenderCore;

		// Token: 0x0400030C RID: 780
		private int faceTrackingState = -1;

		// Token: 0x0400030D RID: 781
		private LocalEventPublisher localPublisher;

		// Token: 0x0400030E RID: 782
		private LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x0400030F RID: 783
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x04000310 RID: 784
		private readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x04000311 RID: 785
		private RawViewportF viewportF;

		// Token: 0x04000312 RID: 786
		private DateTime _lastFpsTime;

		// Token: 0x04000313 RID: 787
		private int _frameCount;

		// Token: 0x04000314 RID: 788
		private int _fps;

		// Token: 0x04000315 RID: 789
		private DateTime _lastHeartbeatTime;

		// Token: 0x04000316 RID: 790
		private float near = 0.3f;

		// Token: 0x04000317 RID: 791
		private float far = 0.6f;

		// Token: 0x04000318 RID: 792
		private float hor_angle = 25f;

		// Token: 0x04000319 RID: 793
		private float ver_angle = 40f;

		// Token: 0x0400031A RID: 794
		private int _state;

		// Token: 0x0400031B RID: 795
		private long _isDisposingFlag;

		// Token: 0x0400031C RID: 796
		private readonly object _initLock = new object();

		// Token: 0x0400031D RID: 797
		private readonly object _frameLock = new object();

		// Token: 0x0400031E RID: 798
		private readonly object _sdkLock = new object();

		// Token: 0x0400031F RID: 799
		private readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000320 RID: 800
		private volatile bool _isDisposed;
	}
}
