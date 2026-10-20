using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AiStereoKitWrapper;
using AstraDepth3D.BOEStereoRender;
using AstraDepth3D.Composition.WindowsRuntimeHelpers;
using AstraDepth3D.EventBus;
using AstraDepth3D.WindowHelper;
using BOEReasterTest;
using BOERenderWapper;
using FaceTrackingManagerServiceLib;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Windows.Graphics;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI.Composition;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x02000044 RID: 68
	public class BasicCaptureDXGI : IDisposable
	{
		// Token: 0x060002CE RID: 718 RVA: 0x000122B0 File Offset: 0x000104B0
		public BasicCaptureDXGI(IDirect3DDevice device)
		{
			this.d3dDevice = Direct3D11Helper.CreateSharpDXDevice(device);
			Output output = new Factory1().GetAdapter1(0).GetOutput(0);
			Output1 output2 = output.QueryInterface<Output1>();
			NLogService.WriteLine("BasicCapture|init success0!");
			this._outputDuplication = output2.DuplicateOutput(this.d3dDevice);
			this._outputDesc = output.Description;
			NLogService.WriteLine("BasicCapture|init success1!");
			this.lastSize = default(SizeInt32);
			this.lastSize.Width = Math.Abs(this._outputDesc.DesktopBounds.Right - this._outputDesc.DesktopBounds.Left);
			this.lastSize.Height = Math.Abs(this._outputDesc.DesktopBounds.Bottom - this._outputDesc.DesktopBounds.Top);
			NLogService.WriteLine(string.Format("DesktopBounds:{0},{1},{2},{3}", new object[]
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
			swapChainDescription.Stereo = false;
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
					throw new Exception("BasicCapture|create swapChain failed!");
				}
			}
			using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
			{
				this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
			}
			this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
			this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
			this.CreateStereoTextureView(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
			this.CreateDisplayTextureView(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
			this.stereoRenderCSharp = new YLStereoRenderCSharp();
			if (!this.stereoRenderCSharp.InitGraphicsDevice(this.d3dDevice.NativePointer, this.d3dDevice.ImmediateContext.NativePointer))
			{
				NLogService.WriteLog("Error:stereoRender InitGraphicsDevice Failed!");
			}
			this.stereoRenderCSharp.SetFrameSize(this.lastSize.Width, this.lastSize.Height);
			this.stereoRenderCSharp.SetParax(0.5f, 0.5f);
			NLogService.WriteLog("BasicCapture|stereoRender init success!");
			NLogService.WriteLog(string.Format("BasicCapture|item:{0},{1}", this.lastSize.Width, this.lastSize.Height));
			this.intertwineRender = new RenderWapper();
			this.intertwineRender.Initiallize(this.d3dDevice.NativePointer);
			NLogService.WriteLog("BasicCapture|intertwineRender init success!");
			this.frameCopy = new FrameCopy();
			this.frameCopy.Initialize(this.d3dDevice);
			this.displayManager = new DisplayManager();
			this.displayManager.Initialize(this.d3dDevice);
			RawViewportF rawViewportF = default(RawViewportF);
			rawViewportF.X = 0f;
			rawViewportF.Y = 0f;
			rawViewportF.Width = (float)this.lastSize.Width;
			rawViewportF.Height = (float)this.lastSize.Height;
			rawViewportF.MinDepth = 0f;
			rawViewportF.MaxDepth = 0f;
			this.viewportF = rawViewportF;
			this._cancellationTokenSource = new CancellationTokenSource();
			this._lastTime = DateTime.Now;
			NLogService.WriteLine("BasicCapture|init success!");
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001279C File Offset: 0x0001099C
		public void Dispose()
		{
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this._isDisposing = true;
			Interlocked.Exchange<SimpleFaceTracking>(ref this._simpleFaceTracking, null);
			this._simpleFaceTracking = null;
			Monitor.Enter(this._lock);
			try
			{
				OutputDuplication outputDuplication = this._outputDuplication;
				if (outputDuplication != null)
				{
					outputDuplication.Dispose();
				}
				YLStereoRenderCSharp ylstereoRenderCSharp = this.stereoRenderCSharp;
				if (ylstereoRenderCSharp != null)
				{
					ylstereoRenderCSharp.Dispose();
				}
				this.stereoRenderCSharp = null;
				NLogService.WriteLog("状态跟踪:2转3已释放\n");
				RenderWapper renderWapper = this.intertwineRender;
				if (renderWapper != null)
				{
					renderWapper.Dispose();
				}
				this.intertwineRender = null;
				NLogService.WriteLog("状态跟踪:3D交织已释放\n");
				FrameCopy frameCopy = this.frameCopy;
				if (frameCopy != null)
				{
					frameCopy.Dispose();
				}
				this.frameCopy = null;
				DisplayManager displayManager = this.displayManager;
				if (displayManager != null)
				{
					displayManager.Dispose();
				}
				this.displayManager = null;
				ShaderResourceView shaderResourceView = this.frameResourceView;
				if (shaderResourceView != null)
				{
					shaderResourceView.Dispose();
				}
				string format = "frameResourceView:{0}";
				ShaderResourceView shaderResourceView2 = this.frameResourceView;
				NLogService.WriteLine(string.Format(format, (shaderResourceView2 != null) ? new bool?(shaderResourceView2.IsDisposed) : null));
				this.frameResourceView = null;
				Texture2D texture2D = this.frameTexture;
				if (texture2D != null)
				{
					texture2D.Dispose();
				}
				string format2 = "frameTexture:{0}";
				Texture2D texture2D2 = this.frameTexture;
				NLogService.WriteLine(string.Format(format2, (texture2D2 != null) ? new bool?(texture2D2.IsDisposed) : null));
				this.frameTexture = null;
				RenderTargetView renderTargetView = this.stereoTargetView;
				if (renderTargetView != null)
				{
					renderTargetView.Dispose();
				}
				this.stereoTargetView = null;
				ShaderResourceView shaderResourceView3 = this.stereoResourceView;
				if (shaderResourceView3 != null)
				{
					shaderResourceView3.Dispose();
				}
				this.stereoResourceView = null;
				Texture2D texture2D3 = this.stereoTexture;
				if (texture2D3 != null)
				{
					texture2D3.Dispose();
				}
				this.stereoTexture = null;
				Texture2D texture2D4 = this.displayTexture;
				if (texture2D4 != null)
				{
					texture2D4.Dispose();
				}
				this.displayTexture = null;
				ShaderResourceView shaderResourceView4 = this.displayResourceView;
				if (shaderResourceView4 != null)
				{
					shaderResourceView4.Dispose();
				}
				this.displayResourceView = null;
				RenderTargetView renderTargetView2 = this.displayTargetView;
				if (renderTargetView2 != null)
				{
					renderTargetView2.Dispose();
				}
				this.displayTargetView = null;
				RenderTargetView renderTargetView3 = this.backRenderTargetView;
				if (renderTargetView3 != null)
				{
					renderTargetView3.Dispose();
				}
				this.backRenderTargetView = null;
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
				this.confidenceList = null;
				NLogService.WriteLog("状态跟踪:释放所有资源\n");
			}
			finally
			{
				if (LoadBoeMCUDll.CloseRaster() == 0)
				{
					NLogService.WriteLog("状态跟踪:透镜已经关闭\n");
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_OFF;
				}
				else
				{
					NLogService.WriteLog("状态跟踪:透镜关闭失败\n");
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ERROR;
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
				Monitor.Exit(this._lock);
			}
			this._isDisposing = false;
			NLogService.WriteLine("Dispose()");
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00012ABC File Offset: 0x00010CBC
		public void StartCapture()
		{
			if (LoadBoeMCUDll.OpenRaster() == 0)
			{
				GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ON;
				NLogService.WriteLog("状态跟踪:透镜已经打开\n");
				if (LoadBoeMCUDll.UpdateCalidata() != 0)
				{
					NLogService.WriteLog("状态跟踪:透镜更新MCU失败\n");
				}
				else
				{
					LoadBoeMCUDll.UpdateEyeCoordinate(100f, 400f);
				}
			}
			else if (LoadBoeMCUDll.Init())
			{
				if (LoadBoeMCUDll.OpenRaster() == 0)
				{
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ON;
					NLogService.WriteLog("状态跟踪:透镜已经打开\n");
					if (LoadBoeMCUDll.UpdateCalidata() != 0)
					{
						NLogService.WriteLog("状态跟踪:透镜更新MCU失败\n");
					}
					else
					{
						LoadBoeMCUDll.UpdateEyeCoordinate(100f, 400f);
					}
				}
			}
			else
			{
				GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ERROR;
				NLogService.WriteLog("状态跟踪:透镜无法打开\n");
			}
			NLogService.WriteLog("状态跟踪:开始抓屏Session\n");
			Task.Run(delegate()
			{
				try
				{
					while (!this._cancellationTokenSource.IsCancellationRequested)
					{
						this.OnFrameArrived();
					}
				}
				catch (Exception)
				{
				}
			}, this._cancellationTokenSource.Token);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00012B94 File Offset: 0x00010D94
		public void SetFaceTracking(SimpleFaceTracking faceTracking)
		{
			this._simpleFaceTracking = faceTracking;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00012B9D File Offset: 0x00010D9D
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012BA6 File Offset: 0x00010DA6
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00012BB4 File Offset: 0x00010DB4
		public void SetParax(float parax, float zerpPlane)
		{
			NLogService.WriteLog(string.Format("BasicCapture|para:{0},zerpPlane:{1}", parax, zerpPlane));
			YLStereoRenderCSharp ylstereoRenderCSharp = this.stereoRenderCSharp;
			if (ylstereoRenderCSharp == null)
			{
				return;
			}
			ylstereoRenderCSharp.SetParax(parax, zerpPlane);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00012BE3 File Offset: 0x00010DE3
		public LocalEnumeration.InterWaveType GetCurrentInterwaveStatus()
		{
			return this.currentInterwaveStatus;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00012BED File Offset: 0x00010DED
		public void ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType status)
		{
			if (this.currentInterwaveStatus != status)
			{
				this.currentInterwaveStatus = status;
				NLogService.WriteLog("ResetCurrentInterwaveStatus:" + status.ToString());
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00012C20 File Offset: 0x00010E20
		private void OnFrameArrived()
		{
			if (this._isDisposing)
			{
				return;
			}
			this.UpdateFaceTrackingState();
			OutputDuplicateFrameInformation frameInfo;
			Resource screenResource;
			Result result = this._outputDuplication.TryAcquireNextFrame(33, ref frameInfo, ref screenResource);
			NLogService.WriteLine(string.Format("AcquireNextFrame result code:{0}", result.Code));
			if (result.Failure && result.Code != -2005270489)
			{
				return;
			}
			using (this.swapChain.GetBackBuffer<Texture2D>(0))
			{
				NLogService.WriteLine("GetBackBuffer");
				if (result.Success)
				{
					using (screenResource)
					{
						using (Texture2D bitmap = screenResource.QueryInterface<Texture2D>())
						{
							Texture2DDescription desc = bitmap.Description;
							if (desc.Width != this.lastSize.Width || desc.Height != this.lastSize.Height)
							{
								this.lastSize.Width = desc.Width;
								this.lastSize.Height = desc.Height;
								if (this.frameTexture != null)
								{
									this.frameTexture.Dispose();
									this.frameTexture = null;
								}
								if (this.frameResourceView != null)
								{
									this.frameResourceView.Dispose();
									this.frameResourceView = null;
								}
								this.frameTexture = this.CreateFrameTexture(this.d3dDevice, this.lastSize.Width, this.lastSize.Height);
								this.frameResourceView = new ShaderResourceView(this.d3dDevice, this.frameTexture);
							}
							this.d3dDevice.ImmediateContext.CopyResource(bitmap, this.frameTexture);
							NLogService.WriteLine(string.Format("desc:{0},{1}", desc.Width, desc.Height));
							this.displayManager.DrawFrame(this.d3dDevice, this.frameResourceView, this.displayTargetView, this.viewportF);
							NLogService.WriteLine("displayManager.DrawFrame");
						}
					}
					this._outputDuplication.ReleaseFrame();
				}
				if (this.currentInterwaveStatus != LocalEnumeration.InterWaveType.InterWave_None)
				{
					lock (this._lock)
					{
						this.frameCopy.DrawFrame(this.d3dDevice, this.displayResourceView, this.stereoTargetView, this.viewportF);
					}
					RenderWapper renderWapper = this.intertwineRender;
					if (!((renderWapper != null) ? new bool?(renderWapper.RenderTexture(this.d3dDevice.NativePointer, this.d3dDevice.ImmediateContext.NativePointer, this.stereoResourceView.NativePointer, this.backRenderTargetView.NativePointer)) : null).GetValueOrDefault())
					{
						NLogService.WriteLog("BOE SDK: 渲染失败！");
					}
				}
				if (this.lastInterwaveStatus != this.currentInterwaveStatus)
				{
					this.lastInterwaveStatus = this.currentInterwaveStatus;
					LocalEventPublisher localEventPublisher = this.localPublisher;
					if (localEventPublisher != null)
					{
						localEventPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)this.currentInterwaveStatus);
					}
				}
			}
			SwapChain1 swapChain = this.swapChain;
			if (swapChain != null)
			{
				swapChain.Present(0, 0);
			}
			NLogService.WriteLine("swapChain.Present");
			DateTime currentTime = DateTime.Now;
			double totalMilliseconds = (currentTime - this._lastTime).TotalMilliseconds;
			this._frameCount++;
			if (totalMilliseconds >= 1000.0)
			{
				this._fps = this._frameCount;
				this._frameCount = 0;
				this._lastTime = currentTime;
				NLogService.WriteLog(string.Format("FPS: {0}", this._fps));
				LocalEventPublisher localEventPublisher2 = this.localPublisher;
				if (localEventPublisher2 == null)
				{
					return;
				}
				localEventPublisher2.DoSomethingAsync("FaceTrackingStatusChanged", this.faceTrackingState);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001300C File Offset: 0x0001120C
		private void UpdateFaceTrackingState()
		{
			float x = -1f;
			float y = -1f;
			float z = -1f;
			float z_r = 0f;
			try
			{
				SimpleFaceTracking simpleFaceTracking = this._simpleFaceTracking;
				if (simpleFaceTracking != null)
				{
					float num;
					simpleFaceTracking.GetPosition(out x, out y, out z, out num, out z_r);
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("状态跟踪:眼追坐标获取失败:" + ex.Message + "\n");
			}
			double num2 = Math.Atan((double)Math.Abs(x / z)) * 57.29577951308232;
			double nVerAngle = Math.Atan((double)Math.Abs(y / z)) * 57.29577951308232;
			if (num2 <= (double)this.hor_angle && nVerAngle <= (double)this.ver_angle && z <= this.far && z >= this.near)
			{
				if (GlobalStatusManager.Instance._iRasterStatus != LocalEnumeration.RasterStatus.RasterStatus_ON || this._simpleFaceTracking == null)
				{
					NLogService.WriteLog("状态跟踪:透镜未开启\n");
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

		// Token: 0x060002D9 RID: 729 RVA: 0x000131E0 File Offset: 0x000113E0
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

		// Token: 0x060002DA RID: 730 RVA: 0x00013258 File Offset: 0x00011458
		private void CreateStereoTextureView(Device device, int width, int height)
		{
			Texture2DDescription texture2DDescription = default(Texture2DDescription);
			texture2DDescription.Width = width;
			texture2DDescription.Height = height;
			texture2DDescription.Format = 87;
			texture2DDescription.MipLevels = 1;
			texture2DDescription.ArraySize = 1;
			texture2DDescription.SampleDescription = new SampleDescription(1, 0);
			texture2DDescription.Usage = 0;
			texture2DDescription.BindFlags = 40;
			texture2DDescription.CpuAccessFlags = 0;
			Texture2DDescription description = texture2DDescription;
			if (this.stereoTexture != null)
			{
				this.stereoTexture.Dispose();
				this.stereoTexture = null;
			}
			this.stereoTexture = new Texture2D(device, description);
			if (this.stereoResourceView != null)
			{
				this.stereoResourceView.Dispose();
				this.stereoResourceView = null;
			}
			this.stereoResourceView = new ShaderResourceView(device, this.stereoTexture);
			if (this.stereoTargetView != null)
			{
				this.stereoTargetView.Dispose();
				this.stereoTargetView = null;
			}
			this.stereoTargetView = new RenderTargetView(device, this.stereoTexture);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00013340 File Offset: 0x00011540
		private void CreateDisplayTextureView(Device device, int width, int height)
		{
			Texture2DDescription texture2DDescription = default(Texture2DDescription);
			texture2DDescription.Width = width;
			texture2DDescription.Height = height;
			texture2DDescription.Format = 87;
			texture2DDescription.MipLevels = 1;
			texture2DDescription.ArraySize = 1;
			texture2DDescription.SampleDescription = new SampleDescription(1, 0);
			texture2DDescription.Usage = 0;
			texture2DDescription.BindFlags = 40;
			texture2DDescription.CpuAccessFlags = 0;
			Texture2DDescription description = texture2DDescription;
			if (this.displayTexture != null)
			{
				this.displayTexture.Dispose();
				this.displayTexture = null;
			}
			this.displayTexture = new Texture2D(device, description);
			if (this.displayResourceView != null)
			{
				this.displayResourceView.Dispose();
				this.displayResourceView = null;
			}
			this.displayResourceView = new ShaderResourceView(device, this.displayTexture);
			if (this.displayTargetView != null)
			{
				this.displayTargetView.Dispose();
				this.displayTargetView = null;
			}
			this.displayTargetView = new RenderTargetView(device, this.displayTexture);
		}

		// Token: 0x040001D5 RID: 469
		private SizeInt32 lastSize;

		// Token: 0x040001D6 RID: 470
		private OutputDuplication _outputDuplication;

		// Token: 0x040001D7 RID: 471
		private OutputDescription _outputDesc;

		// Token: 0x040001D8 RID: 472
		private Device d3dDevice;

		// Token: 0x040001D9 RID: 473
		private SwapChain1 swapChain;

		// Token: 0x040001DA RID: 474
		private OutputDuplicateMoveRectangle[] m_MetaDataBuffer;

		// Token: 0x040001DB RID: 475
		private int m_MetaDataSize;

		// Token: 0x040001DC RID: 476
		private RawRectangle[] m_DirtyData;

		// Token: 0x040001DD RID: 477
		private int m_DirtyDataSize;

		// Token: 0x040001DE RID: 478
		private RenderTargetView backRenderTargetView;

		// Token: 0x040001DF RID: 479
		private Texture2D frameTexture;

		// Token: 0x040001E0 RID: 480
		private ShaderResourceView frameResourceView;

		// Token: 0x040001E1 RID: 481
		public Texture2D stereoTexture;

		// Token: 0x040001E2 RID: 482
		private RenderTargetView stereoTargetView;

		// Token: 0x040001E3 RID: 483
		private ShaderResourceView stereoResourceView;

		// Token: 0x040001E4 RID: 484
		public Texture2D displayTexture;

		// Token: 0x040001E5 RID: 485
		private RenderTargetView displayTargetView;

		// Token: 0x040001E6 RID: 486
		private ShaderResourceView displayResourceView;

		// Token: 0x040001E7 RID: 487
		private YLStereoRenderCSharp stereoRenderCSharp;

		// Token: 0x040001E8 RID: 488
		private RenderWapper intertwineRender;

		// Token: 0x040001E9 RID: 489
		private FrameCopy frameCopy;

		// Token: 0x040001EA RID: 490
		private DisplayManager displayManager;

		// Token: 0x040001EB RID: 491
		private SimpleFaceTracking _simpleFaceTracking;

		// Token: 0x040001EC RID: 492
		private int faceTrackingState;

		// Token: 0x040001ED RID: 493
		private LocalEventPublisher localPublisher;

		// Token: 0x040001EE RID: 494
		private volatile LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x040001EF RID: 495
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x040001F0 RID: 496
		private RawViewportF viewportF;

		// Token: 0x040001F1 RID: 497
		private DateTime _lastTime;

		// Token: 0x040001F2 RID: 498
		private int _frameCount;

		// Token: 0x040001F3 RID: 499
		private int _fps;

		// Token: 0x040001F4 RID: 500
		private float hist_conf = 0.75f;

		// Token: 0x040001F5 RID: 501
		private float diff_conf = 30f;

		// Token: 0x040001F6 RID: 502
		private float ssim_conf = 0.55f;

		// Token: 0x040001F7 RID: 503
		private float near = 0.3f;

		// Token: 0x040001F8 RID: 504
		private float far = 0.6f;

		// Token: 0x040001F9 RID: 505
		private float hor_angle = 25f;

		// Token: 0x040001FA RID: 506
		private float ver_angle = 40f;

		// Token: 0x040001FB RID: 507
		private List<float> confidenceList = new List<float>();

		// Token: 0x040001FC RID: 508
		private int confidenceCount;

		// Token: 0x040001FD RID: 509
		private readonly object _lock = new object();

		// Token: 0x040001FE RID: 510
		private volatile bool _isDisposing;

		// Token: 0x040001FF RID: 511
		private CancellationTokenSource _cancellationTokenSource;
	}
}
