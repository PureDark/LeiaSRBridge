using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using AiStereoKitWrapper;
using AstraDepth3D.BOEStereoRender;
using AstraDepth3D.Composition.WindowsRuntimeHelpers;
using AstraDepth3D.EventBus;
using AstraDepth3D.Tracker;
using AstraDepth3D.WindowHelper;
using BOEInterleaving;
using BOEReasterTest;
using Microsoft.Win32;
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
	// Token: 0x02000054 RID: 84
	public class BasicCapture : ICapture, IDisposable
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0001B390 File Offset: 0x00019590
		public BasicCapture(IDirect3DDevice d, GraphicsCaptureItem i, YLStereoMatcher matcher)
		{
			lock (this._lock)
			{
				try
				{
					this.item = i;
					this.device = d;
					this.d3dDevice = Direct3D11Helper.CreateSharpDXDevice(this.device);
					this.d3dDevice.QueryInterface<DeviceMultithread>().SetMultithreadProtected(true);
					this.stereoMatcher = matcher;
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
							throw new Exception("BasicCapture|create swapChain failed!");
						}
					}
					this.framePool = Direct3D11CaptureFramePool.CreateFreeThreaded(this.device, 87, 2, i.Size);
					this.session = this.framePool.CreateCaptureSession(i);
					if (this.IsSupportBorderRequired())
					{
						this.session.put_IsBorderRequired(false);
					}
					if (this.IsSupportCursorCapture())
					{
						this.session.put_IsCursorCaptureEnabled(false);
					}
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
					if (MCUSdk.BOE_MCU_openMCU() != 0)
					{
						NLogService.WriteLog("BOE_MCU_openMCU failed");
					}
					double[] deviceParam = new double[6];
					if (MCUSdk.BOE_MCU_getDeviceParam(deviceParam) != 0)
					{
						NLogService.WriteLog("BOE_MCU_getDeviceParam failed");
					}
					try
					{
						NLogService.WriteLog("BOE_MCU_getDeviceParam values: " + string.Join<double>(",", deviceParam));
					}
					catch
					{
					}
					double[] wholeMat = new double[6];
					if (MCUSdk.BOE_MCU_getWholeMat(wholeMat) != 0)
					{
						NLogService.WriteLog("BOE_MCU_getWholeMat failed");
					}
					try
					{
						NLogService.WriteLog("BOE_MCU_getWholeMat values: " + string.Join<double>(",", wholeMat));
					}
					catch
					{
					}
					double[] refractionParam = new double[8];
					if (MCUSdk.BOE_MCU_getRefractionDeviceParam(refractionParam) != 0)
					{
						NLogService.WriteLog("BOE_MCU_getRefractionDeviceParam failed");
					}
					try
					{
						NLogService.WriteLog("BOE_MCU_getRefractionDeviceParam values: " + string.Join<double>(",", refractionParam));
					}
					catch
					{
					}
					if (Interleaving.InitSDK(this.d3dDevice.NativePointer, IntPtr.Zero, deviceParam, wholeMat, refractionParam, D3DVersion.dx11, 0, 0, i.Size.Width, i.Size.Height, 87, "./logs/") != 0)
					{
						NLogService.WriteLog("BOE_Interleaving_initSDK failed");
					}
					Interleaving.SetWholeParam(wholeMat);
					Interleaving.SetDeviceParam(deviceParam);
					Interleaving.SetRefractDeviceParam(refractionParam);
					NLogService.WriteLog("BasicCapture|intertwineRender init success!");
					RawViewportF rawViewportF = default(RawViewportF);
					rawViewportF.X = 0f;
					rawViewportF.Y = 0f;
					rawViewportF.Width = (float)this.item.Size.Width;
					rawViewportF.Height = (float)this.item.Size.Height;
					rawViewportF.MinDepth = 0f;
					rawViewportF.MaxDepth = 0f;
					this.viewportF = rawViewportF;
					this._lastTime = DateTime.Now;
					NLogService.WriteLog("BasicCapture|init success!");
				}
				catch (Exception e)
				{
					NLogService.WriteLog("BasicCapture初始化失败：" + e.Message);
				}
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		public void Dispose()
		{
			this._isDisposing = true;
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert != null)
			{
				frameConvert.Dispose();
			}
			this.frameConvert = null;
			NLogService.WriteLog("状态跟踪:FrameConvert已释放\n");
			Monitor.Enter(this._lock);
			try
			{
				NLogService.WriteLog("状态跟踪:开始关闭抓屏Session\n");
				GraphicsCaptureSession graphicsCaptureSession = this.session;
				if (graphicsCaptureSession != null)
				{
					graphicsCaptureSession.Dispose();
				}
				this.session = null;
				NLogService.WriteLog("状态跟踪:抓屏Session已经关闭\n");
				if (this.framePool != null)
				{
					WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<Direct3D11CaptureFramePool, object>>(new Action<EventRegistrationToken>(this.framePool.remove_FrameArrived), new TypedEventHandler<Direct3D11CaptureFramePool, object>(this.OnFrameArrived));
					Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
					if (direct3D11CaptureFramePool != null)
					{
						direct3D11CaptureFramePool.Dispose();
					}
					this.framePool = null;
				}
				FormatRecognition formatRecognition = this.formatRecognition;
				if (formatRecognition != null)
				{
					formatRecognition.Dispose();
				}
				this.formatRecognition = null;
				Interleaving.ReleaseSDK();
				NLogService.WriteLog("状态跟踪:3D交织已释放\n");
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
				NLogService.WriteLog("状态跟踪:释放所有资源\n");
			}
			finally
			{
				if (MCUSdk.BOE_MCU_set3DMode(false) == 0)
				{
					NLogService.WriteLog("状态跟踪:透镜已经关闭\n");
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_OFF;
				}
				else
				{
					NLogService.WriteLog("状态跟踪:透镜关闭失败\n");
					GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ERROR;
				}
				MCUSdk.BOE_MCU_closeMCU();
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

		// Token: 0x0600036A RID: 874 RVA: 0x0001BAD8 File Offset: 0x00019CD8
		public void StartCapture()
		{
			if (MCUSdk.BOE_MCU_set3DMode(true) == 0)
			{
				GlobalStatusManager.Instance._iRasterStatus = LocalEnumeration.RasterStatus.RasterStatus_ON;
				NLogService.WriteLog("状态跟踪:透镜已经打开\n");
			}
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

		// Token: 0x0600036B RID: 875 RVA: 0x0001BB4B File Offset: 0x00019D4B
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001BB54 File Offset: 0x00019D54
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this.swapChain);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001BB62 File Offset: 0x00019D62
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this.frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001BB76 File Offset: 0x00019D76
		public LocalEnumeration.InterWaveType GetCurrentInterwaveStatus()
		{
			return this.currentInterwaveStatus;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001BB80 File Offset: 0x00019D80
		public void ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType status)
		{
			if (this.currentInterwaveStatus != status)
			{
				this.currentInterwaveStatus = status;
				NLogService.WriteLog("ResetCurrentInterwaveStatus:" + status.ToString());
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		private void OnFrameArrived(Direct3D11CaptureFramePool sender, object args)
		{
			if (this._isDisposing || this.framePool == null)
			{
				return;
			}
			try
			{
				bool newSize = false;
				using (Direct3D11CaptureFrame frame = sender.TryGetNextFrame())
				{
					if (frame.ContentSize.Width != this.lastSize.Width || frame.ContentSize.Height != this.lastSize.Height)
					{
						newSize = true;
						this.lastSize = frame.ContentSize;
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
						if (this.backRenderTargetView != null)
						{
							this.backRenderTargetView.Dispose();
							this.backRenderTargetView = null;
						}
						this.swapChain.ResizeBuffers(2, this.lastSize.Width, this.lastSize.Height, 87, 0);
						using (Texture2D _backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
						{
							this.backRenderTargetView = new RenderTargetView(this.d3dDevice, _backBuffer);
						}
					}
					using (Texture2D bitmap = Direct3D11Helper.CreateSharpDXTexture2D(frame.Surface))
					{
						this.d3dDevice.ImmediateContext.CopyResource(bitmap, this.frameTexture);
					}
				}
				if (this.currentInterwaveStatus == LocalEnumeration.InterWaveType.InterWave_None)
				{
					lock (this._lock)
					{
						try
						{
							ValueTuple<byte[], int> valueTuple = this.formatRecognition.DrawFrame(this.d3dDevice, this.frameResourceView);
							byte[] buffer = valueTuple.Item1;
							int rowPitch = valueTuple.Item2;
							YLStereoMatcher ylstereoMatcher = this.stereoMatcher;
							bool? isStereoContent = (ylstereoMatcher != null) ? new bool?(ylstereoMatcher.ExtractAndMatch(buffer, this.formatRecognition.scaleW, this.formatRecognition.scaleH, rowPitch)) : null;
							this.currentInterwaveStatus = (isStereoContent.GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
							NLogService.WriteLog(string.Format("check stereo format:{0}", isStereoContent));
							FormatRecognition formatRecognition = this.formatRecognition;
							if (formatRecognition != null)
							{
								formatRecognition.DrawFrame(this.d3dDevice, this.frameResourceView, this.backRenderTargetView, this.viewportF);
							}
						}
						catch (Exception e)
						{
							NLogService.WriteLog("stereoMatcher error:" + e.Message);
							this.currentInterwaveStatus = LocalEnumeration.InterWaveType.InterWave_AI;
						}
						goto IL_3BA;
					}
				}
				FrameConvert frameConvert = this.frameConvert;
				if (frameConvert != null)
				{
					frameConvert.DrawFrame(this.d3dDevice, this.frameResourceView, this.currentInterwaveStatus);
				}
				lock (this._lock)
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
				IL_3BA:
				if (this.lastInterwaveStatus != this.currentInterwaveStatus)
				{
					this.lastInterwaveStatus = this.currentInterwaveStatus;
					LocalEventPublisher localEventPublisher = this.localPublisher;
					if (localEventPublisher != null)
					{
						localEventPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)this.currentInterwaveStatus);
					}
				}
				SwapChain1 swapChain = this.swapChain;
				if (swapChain != null)
				{
					swapChain.Present(0, 0);
				}
				DateTime currentTime = DateTime.Now;
				if ((currentTime - this._lastTime).TotalMilliseconds >= 50.0)
				{
					this._lastTime = currentTime;
					LocalEventPublisher localEventPublisher2 = this.localPublisher;
					if (localEventPublisher2 != null)
					{
						localEventPublisher2.DoSomethingAsync("FaceTrackingStatusChanged", this.faceTrackingState);
					}
				}
				if (newSize)
				{
					Direct3D11CaptureFramePool direct3D11CaptureFramePool = this.framePool;
					if (direct3D11CaptureFramePool != null)
					{
						direct3D11CaptureFramePool.Recreate(this.device, 87, 2, this.lastSize);
					}
				}
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("OnFrameArrived:" + ex.Message);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001C100 File Offset: 0x0001A300
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

		// Token: 0x06000372 RID: 882 RVA: 0x0001C290 File Offset: 0x0001A490
		private ValueTuple<float, float, float> GetPose()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float x = pose.Item1;
			float y = pose.Item2;
			float z = pose.Item3;
			float z_r = pose.Item4;
			NLogService.WriteLine(string.Format("状态跟踪:人眼坐标获取成功:{0},{1},{2},{3}\n", new object[]
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

		// Token: 0x06000373 RID: 883 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		private bool IsSupportBorderRequired()
		{
			RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
			if ((string)registry.GetValue("ProductName") != null)
			{
				string currentBuild = (string)registry.GetValue("CurrentBuild");
				if (currentBuild != null && int.Parse(currentBuild) >= 20348)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		private bool IsSupportCursorCapture()
		{
			RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
			if ((string)registry.GetValue("ProductName") != null)
			{
				string currentBuild = (string)registry.GetValue("CurrentBuild");
				if (currentBuild != null && int.Parse(currentBuild) >= 19041)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001C450 File Offset: 0x0001A650
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

		// Token: 0x06000376 RID: 886 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
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

		// Token: 0x040002EF RID: 751
		private GraphicsCaptureItem item;

		// Token: 0x040002F0 RID: 752
		private Direct3D11CaptureFramePool framePool;

		// Token: 0x040002F1 RID: 753
		private GraphicsCaptureSession session;

		// Token: 0x040002F2 RID: 754
		private SizeInt32 lastSize;

		// Token: 0x040002F3 RID: 755
		private IDirect3DDevice device;

		// Token: 0x040002F4 RID: 756
		private Device d3dDevice;

		// Token: 0x040002F5 RID: 757
		private SwapChain1 swapChain;

		// Token: 0x040002F6 RID: 758
		private RenderTargetView backRenderTargetView;

		// Token: 0x040002F7 RID: 759
		private Texture2D frameTexture;

		// Token: 0x040002F8 RID: 760
		private ShaderResourceView frameResourceView;

		// Token: 0x040002F9 RID: 761
		private readonly YLStereoMatcher stereoMatcher;

		// Token: 0x040002FA RID: 762
		private FormatRecognition formatRecognition;

		// Token: 0x040002FB RID: 763
		private FrameConvert frameConvert;

		// Token: 0x040002FC RID: 764
		private int faceTrackingState = -1;

		// Token: 0x040002FD RID: 765
		private LocalEventPublisher localPublisher;

		// Token: 0x040002FE RID: 766
		private volatile LocalEnumeration.InterWaveType currentInterwaveStatus;

		// Token: 0x040002FF RID: 767
		private LocalEnumeration.InterWaveType lastInterwaveStatus;

		// Token: 0x04000300 RID: 768
		private RawViewportF viewportF;

		// Token: 0x04000301 RID: 769
		private DateTime _lastTime;

		// Token: 0x04000302 RID: 770
		private int _frameCount;

		// Token: 0x04000303 RID: 771
		private int _fps;

		// Token: 0x04000304 RID: 772
		private float hist_conf = 0.75f;

		// Token: 0x04000305 RID: 773
		private float diff_conf = 30f;

		// Token: 0x04000306 RID: 774
		private float ssim_conf = 0.55f;

		// Token: 0x04000307 RID: 775
		private float near = 400f;

		// Token: 0x04000308 RID: 776
		private float far = 1000f;

		// Token: 0x04000309 RID: 777
		private float hor_angle = 33f;

		// Token: 0x0400030A RID: 778
		private float ver_angle = 26f;

		// Token: 0x0400030B RID: 779
		private readonly object _lock = new object();

		// Token: 0x0400030C RID: 780
		private volatile bool _isDisposing;
	}
}
