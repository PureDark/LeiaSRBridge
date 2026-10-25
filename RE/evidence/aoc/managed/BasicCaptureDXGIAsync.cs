using System;
using System.Collections.Generic;
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
	// Token: 0x02000052 RID: 82
	public class BasicCaptureDXGIAsync : ICapture, IDisposable
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00017518 File Offset: 0x00015718
		public BasicCaptureDXGIAsync(YLStereoMatcher matcher, string screenDeviceName)
		{
			int num = Interlocked.CompareExchange(ref this._state, 1, 0);
			if (num != 0)
			{
				throw new InvalidOperationException(string.Format("BasicCaptureDXGIAsync|Cannot initialize from state: {0}", (CaptureStateDXGI)num));
			}
			try
			{
				if (matcher == null)
				{
					throw new ArgumentNullException("matcher");
				}
				this._stereoMatcher = matcher;
				Adapter1 adapter = null;
				Output1 output = null;
				OutputDescription outputDesc = default(OutputDescription);
				bool flag = false;
				NLogService.WriteLog("BasicCaptureDXGI|目标屏幕设备: " + screenDeviceName);
				List<ValueTuple<Adapter1, Output1, OutputDescription>> list = new List<ValueTuple<Adapter1, Output1, OutputDescription>>();
				using (Factory1 factory = new Factory1())
				{
					int num2 = 0;
					for (;;)
					{
						Adapter1 adapter2 = null;
						try
						{
							adapter2 = factory.GetAdapter1(num2);
						}
						catch (SharpDXException ex) when (ex.ResultCode == ResultCode.NotFound)
						{
							break;
						}
						bool flag2 = false;
						int num3 = 0;
						for (;;)
						{
							Output output2 = null;
							try
							{
								output2 = adapter2.GetOutput(num3);
							}
							catch (SharpDXException ex2) when (ex2.ResultCode == ResultCode.NotFound)
							{
								break;
							}
							try
							{
								OutputDescription description = output2.Description;
								if (description.IsAttachedToDesktop && string.Equals(description.DeviceName, screenDeviceName, StringComparison.OrdinalIgnoreCase))
								{
									list.Add(new ValueTuple<Adapter1, Output1, OutputDescription>(adapter2, output2.QueryInterface<Output1>(), description));
									flag2 = true;
								}
							}
							finally
							{
								output2.Dispose();
							}
							num3++;
						}
						if (!flag2)
						{
							adapter2.Dispose();
						}
						num2++;
					}
				}
				if (list.Count == 0)
				{
					throw new InvalidOperationException("BasicCaptureDXGI|未找到与屏幕 " + screenDeviceName + " 匹配的DXGI输出，无法进行截屏");
				}
				NLogService.WriteLog(string.Format("BasicCaptureDXGI|找到 {0} 个候选输出，开始DDA探测...", list.Count));
				foreach (ValueTuple<Adapter1, Output1, OutputDescription> valueTuple in list)
				{
					try
					{
						NLogService.WriteLog("BasicCaptureDXGI|尝试DDA: Adapter=" + valueTuple.Item1.Description.Description + ", Output=" + valueTuple.Item3.DeviceName);
						using (Device device = new Device(valueTuple.Item1, 40, new FeatureLevel[]
						{
							45312,
							45056
						}))
						{
							valueTuple.Item2.DuplicateOutput(device).Dispose();
						}
						adapter = valueTuple.Item1;
						output = valueTuple.Item2;
						outputDesc = valueTuple.Item3;
						flag = true;
						NLogService.WriteLog(string.Concat(new string[]
						{
							"BasicCaptureDXGI|DDA探测成功: Adapter=",
							valueTuple.Item1.Description.Description,
							", ",
							string.Format("Output={0}, Bounds=({1},", valueTuple.Item3.DeviceName, valueTuple.Item3.DesktopBounds.Left),
							string.Format("{0},{1},", valueTuple.Item3.DesktopBounds.Top, valueTuple.Item3.DesktopBounds.Right),
							string.Format("{0})", valueTuple.Item3.DesktopBounds.Bottom)
						}));
						break;
					}
					catch (SharpDXException ex3)
					{
						NLogService.WriteLog("BasicCaptureDXGI|DDA探测失败: Adapter=" + valueTuple.Item1.Description.Description + ", " + string.Format("Output={0}, HR=0x{1:X8}, Msg={2}", valueTuple.Item3.DeviceName, ex3.HResult, ex3.Message));
					}
					catch (Exception ex4)
					{
						NLogService.WriteLog(string.Concat(new string[]
						{
							"BasicCaptureDXGI|DDA探测异常: Adapter=",
							valueTuple.Item1.Description.Description,
							", Output=",
							valueTuple.Item3.DeviceName,
							", Msg=",
							ex4.Message
						}));
					}
				}
				foreach (ValueTuple<Adapter1, Output1, OutputDescription> valueTuple2 in list)
				{
					if (valueTuple2.Item2 != output)
					{
						valueTuple2.Item2.Dispose();
					}
					if (valueTuple2.Item1 != adapter)
					{
						valueTuple2.Item1.Dispose();
					}
				}
				if (!flag || adapter == null || output == null)
				{
					throw new InvalidOperationException("BasicCaptureDXGI|屏幕 " + screenDeviceName + " 的所有适配器组合均不支持DDA，无法截屏");
				}
				NLogService.WriteLog("BasicCaptureDXGI|最终选择适配器: " + adapter.Description.Description);
				this._d3dDevice = new Device(adapter, 40, new FeatureLevel[]
				{
					45312,
					45056
				});
				this._immediateContext = this._d3dDevice.ImmediateContext;
				using (DeviceMultithread deviceMultithread = this._d3dDevice.QueryInterface<DeviceMultithread>())
				{
					deviceMultithread.SetMultithreadProtected(true);
				}
				NLogService.WriteLog("BasicCaptureDXGI|D3D Device init success!");
				this._outputDuplication = output.DuplicateOutput(this._d3dDevice);
				this._outputDesc = outputDesc;
				NLogService.WriteLine("BasicCaptureDXGI|DuplicateOutput init success!");
				SizeInt32 lastSize = default(SizeInt32);
				lastSize.Width = Math.Abs(this._outputDesc.DesktopBounds.Right - this._outputDesc.DesktopBounds.Left);
				lastSize.Height = Math.Abs(this._outputDesc.DesktopBounds.Bottom - this._outputDesc.DesktopBounds.Top);
				this._lastSize = lastSize;
				this._lastRotation = this._outputDesc.Rotation;
				NLogService.WriteLog(string.Format("DesktopBounds:{0},{1},", this._outputDesc.DesktopBounds.Left, this._outputDesc.DesktopBounds.Top) + string.Format("{0},{1}", this._outputDesc.DesktopBounds.Right, this._outputDesc.DesktopBounds.Bottom));
				SwapChainDescription1 swapChainDescription = default(SwapChainDescription1);
				swapChainDescription.Width = this._lastSize.Width;
				swapChainDescription.Height = this._lastSize.Height;
				swapChainDescription.Format = 87;
				swapChainDescription.Stereo = true;
				swapChainDescription.SampleDescription = new SampleDescription(1, 0);
				swapChainDescription.Usage = 32;
				swapChainDescription.BufferCount = 2;
				swapChainDescription.Scaling = 0;
				swapChainDescription.SwapEffect = 3;
				swapChainDescription.AlphaMode = 1;
				swapChainDescription.Flags = 0;
				SwapChainDescription1 swapChainDescription2 = swapChainDescription;
				using (Factory2 factory2 = new Factory2(false))
				{
					this._swapChain = new SwapChain1(factory2, this._d3dDevice, ref swapChainDescription2, null);
				}
				using (Texture2D backBuffer = this._swapChain.GetBackBuffer<Texture2D>(0))
				{
					this._backRenderTargetView = new RenderTargetView(this._d3dDevice, backBuffer);
				}
				this.InitGpuResources(this._lastSize.Width, this._lastSize.Height, this._lastRotation);
				this.InitDoubleBuffer(this._lastSize.Width, this._lastSize.Height);
				this._displayManager = new DisplayManager();
				this._displayManager.Initialize(this._d3dDevice);
				this._formatRecognition = new FormatRecognition();
				this._formatRecognition.Initialize(this._d3dDevice);
				this._frameConvert = new FrameConvert(this._d3dDevice, this._immediateContext, this._lastSize.Width, this._lastSize.Height);
				this.InitializeBoeRenderer(this._lastSize.Width, this._lastSize.Height);
				RawViewportF viewportF = default(RawViewportF);
				viewportF.X = 0f;
				viewportF.Y = 0f;
				viewportF.Width = (float)this._lastSize.Width;
				viewportF.Height = (float)this._lastSize.Height;
				viewportF.MinDepth = 0f;
				viewportF.MaxDepth = 0f;
				this._viewportF = viewportF;
				this._cts = new CancellationTokenSource();
				this._lastFpsTime = DateTime.Now;
				this._lastHeartbeatTime = DateTime.Now;
				Interlocked.Exchange(ref this._state, 2);
				NLogService.WriteLog("BasicCaptureDXGIAsync|init success!");
			}
			catch (Exception)
			{
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureDXGIAsync|init failed, cleaning up resources");
				this.CleanupAllResources();
				throw;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00017F28 File Offset: 0x00016128
		private void InitializeBoeRenderer(int width, int height)
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
				NLogService.WriteLog("BasicCaptureDXGIAsync|intertwineRender init success!");
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00018010 File Offset: 0x00016210
		private void InitDoubleBuffer(int width, int height)
		{
			this.SafeDispose<Texture2D>(ref this._captureBuffer0);
			this.SafeDispose<ShaderResourceView>(ref this._captureSrv0);
			this.SafeDispose<Texture2D>(ref this._captureBuffer1);
			this.SafeDispose<ShaderResourceView>(ref this._captureSrv1);
			this._captureBuffer0 = this.CreateFrameTexture(this._d3dDevice, width, height);
			this._captureSrv0 = new ShaderResourceView(this._d3dDevice, this._captureBuffer0);
			this._captureBuffer1 = this.CreateFrameTexture(this._d3dDevice, width, height);
			this._captureSrv1 = new ShaderResourceView(this._d3dDevice, this._captureBuffer1);
			this._writeIndex = 0;
			this._readIndex = 0;
			NLogService.WriteLog(string.Format("Double buffer initialized: {0}x{1}", width, height));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000180D0 File Offset: 0x000162D0
		public void StartCapture()
		{
			int num = Interlocked.CompareExchange(ref this._state, 3, 2);
			if (num != 2)
			{
				NLogService.WriteLog(string.Format("BasicCaptureDXGIAsync|Cannot start from state: {0}", (CaptureStateDXGI)num));
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
						this._cts = new CancellationTokenSource();
						CancellationToken token = this._cts.Token;
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

		// Token: 0x060003C1 RID: 961 RVA: 0x00018200 File Offset: 0x00016400
		private void CaptureLoop(CancellationToken token)
		{
			for (int i = 0; i < 3; i++)
			{
				if (token.IsCancellationRequested)
				{
					break;
				}
				OutputDuplicateFrameInformation outputDuplicateFrameInformation;
				Resource resource;
				if (this._outputDuplication.TryAcquireNextFrame(50, ref outputDuplicateFrameInformation, ref resource).Success)
				{
					this._outputDuplication.ReleaseFrame();
					if (resource != null)
					{
						resource.Dispose();
					}
				}
			}
			while (!token.IsCancellationRequested && !this.IsDisposing())
			{
				try
				{
					OutputDuplicateFrameInformation outputDuplicateFrameInformation2;
					Resource resource2;
					if (this._resizeRequested)
					{
						this._resizeCompletedEvent.Wait(token);
					}
					else if (this._outputDuplication.TryAcquireNextFrame(50, ref outputDuplicateFrameInformation2, ref resource2).Success)
					{
						try
						{
							if (resource2 != null)
							{
								using (resource2)
								{
									using (Texture2D texture2D = resource2.QueryInterface<Texture2D>())
									{
										Texture2DDescription description = texture2D.Description;
										bool flag = description.Width != this._lastSize.Width || description.Height != this._lastSize.Height;
										bool flag2 = this._lastRotation != this._outputDesc.Rotation;
										if (flag || flag2)
										{
											SizeInt32 pendingResizeSize = default(SizeInt32);
											pendingResizeSize.Width = description.Width;
											pendingResizeSize.Height = description.Height;
											this._pendingResizeSize = pendingResizeSize;
											this._pendingResizeRotation = this._outputDesc.Rotation;
											this._resizeCompletedEvent.Reset();
											this._resizeRequested = true;
											NLogService.WriteLog(string.Format("Screen resize requested: {0}x{1}, ", description.Width, description.Height) + string.Format("rotation={0}", this._outputDesc.Rotation));
											this._outputDuplication.ReleaseFrame();
										}
										else
										{
											object bufferLock = this._bufferLock;
											lock (bufferLock)
											{
												Texture2D texture2D2 = (this._writeIndex == 0) ? this._captureBuffer0 : this._captureBuffer1;
												this._immediateContext.CopyResource(texture2D, texture2D2);
											}
											bufferLock = this._bufferLock;
											lock (bufferLock)
											{
												this._readIndex = this._writeIndex;
												this._writeIndex = 1 - this._writeIndex;
											}
											this._frameReadyEvent.Set();
											this._hasValidContent = true;
										}
									}
								}
							}
						}
						finally
						{
							this._outputDuplication.ReleaseFrame();
						}
					}
				}
				catch (SharpDXException ex) when (ex.HResult == -2005270490)
				{
				}
				catch (OperationCanceledException)
				{
					break;
				}
				catch (Exception ex2)
				{
					NLogService.WriteLog("CaptureLoop error: " + ex2.Message);
				}
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00018590 File Offset: 0x00016790
		private void RenderLoop(CancellationToken token)
		{
			while (!token.IsCancellationRequested && !this.IsDisposing())
			{
				if (this._resizeRequested)
				{
					this.HandleResizeInRenderThread();
				}
				else
				{
					bool flag = this._frameReadyEvent.WaitOne(10);
					if (this._hasValidContent)
					{
						if (!flag)
						{
							this.ProcessFrameRender();
						}
						else if (this._resizeRequested)
						{
							this.HandleResizeInRenderThread();
						}
						else
						{
							try
							{
								object obj = this._bufferLock;
								ShaderResourceView shaderResourceView;
								lock (obj)
								{
									shaderResourceView = ((this._readIndex == 0) ? this._captureSrv0 : this._captureSrv1);
								}
								if (shaderResourceView == null || shaderResourceView.NativePointer == IntPtr.Zero)
								{
									NLogService.WriteLog("RenderLoop|Skipping frame: invalid readSrv");
								}
								else
								{
									LocalEnumeration.InterWaveType currentInterwaveStatus = this.GetCurrentInterwaveStatus();
									if (currentInterwaveStatus == LocalEnumeration.InterWaveType.InterWave_None)
									{
										if (flag)
										{
											ValueTuple<byte[], int> valueTuple = this._formatRecognition.DrawFrame(this._immediateContext, shaderResourceView);
											byte[] item = valueTuple.Item1;
											int item2 = valueTuple.Item2;
											YLStereoMatcher stereoMatcher = this._stereoMatcher;
											bool? flag3 = (stereoMatcher != null) ? new bool?(stereoMatcher.ExtractAndMatch(item, this._formatRecognition.scaleW, this._formatRecognition.scaleH, item2)) : null;
											this.ResetCurrentInterwaveStatus(flag3.GetValueOrDefault() ? LocalEnumeration.InterWaveType.InterWave_SBS : LocalEnumeration.InterWaveType.InterWave_AI);
											NLogService.WriteLog(string.Format("check stereo format:{0}", flag3));
										}
										FormatRecognition formatRecognition = this._formatRecognition;
										if (formatRecognition != null)
										{
											formatRecognition.DrawFrame(this._immediateContext, shaderResourceView, this._backRenderTargetView, this._viewportF);
										}
									}
									else
									{
										FrameConvert frameConvert = this._frameConvert;
										if (frameConvert != null)
										{
											frameConvert.DrawFrame(this._immediateContext, shaderResourceView, currentInterwaveStatus);
										}
										FrameConvert frameConvert2 = this._frameConvert;
										ShaderResourceView shaderResourceView2 = (frameConvert2 != null) ? frameConvert2.GetShaderResource() : null;
										if (shaderResourceView2 == null || shaderResourceView2.NativePointer == IntPtr.Zero)
										{
											NLogService.WriteLog("RenderLoop|Skipping BOE render: invalid boeSrv");
										}
										else
										{
											obj = this._sdkLock;
											lock (obj)
											{
												this._immediateContext.OutputMerger.SetRenderTargets(this._backRenderTargetView);
												this._immediateContext.Rasterizer.SetViewport(this._viewportF);
												this._immediateContext.ClearRenderTargetView(this._backRenderTargetView, new RawColor4(0f, 0f, 0f, 1f));
												ValueTuple<float, float, float> pose = this.GetPose();
												float item3 = pose.Item1;
												float item4 = pose.Item2;
												float item5 = pose.Item3;
												Interleaving.DoInterleave(item3, item4, item5, this._immediateContext.NativePointer, this._backRenderTargetView.NativePointer, shaderResourceView2.NativePointer, this._lastSize.Width, this._lastSize.Height, 87, InterMode.RLInter, 0);
											}
										}
									}
									this._statusLock.EnterReadLock();
									LocalEnumeration.InterWaveType currentInterwaveStatus2 = this._currentInterwaveStatus;
									LocalEnumeration.InterWaveType lastInterwaveStatus = this._lastInterwaveStatus;
									this._statusLock.ExitReadLock();
									if (lastInterwaveStatus != currentInterwaveStatus2)
									{
										this._lastInterwaveStatus = currentInterwaveStatus2;
										LocalEventPublisher localPublisher = this._localPublisher;
										if (localPublisher != null)
										{
											localPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)currentInterwaveStatus2);
										}
									}
									SwapChain1 swapChain = this._swapChain;
									if (swapChain != null)
									{
										swapChain.Present(0, 0);
									}
									this.UpdateFPS();
								}
							}
							catch (OperationCanceledException)
							{
								break;
							}
							catch (Exception ex)
							{
								NLogService.WriteLog("RenderLoop error: " + ex.Message);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001893C File Offset: 0x00016B3C
		private void ProcessFrameRender()
		{
			FrameConvert frameConvert = this._frameConvert;
			ShaderResourceView shaderResourceView = (frameConvert != null) ? frameConvert.GetShaderResource() : null;
			if (shaderResourceView == null || shaderResourceView.NativePointer == IntPtr.Zero)
			{
				NLogService.WriteLog("RenderLoop|Skipping BOE render: invalid boeSrv");
			}
			else
			{
				this._immediateContext.OutputMerger.SetRenderTargets(this._backRenderTargetView);
				this._immediateContext.Rasterizer.SetViewport(this._viewportF);
				this._immediateContext.ClearRenderTargetView(this._backRenderTargetView, new RawColor4(0f, 0f, 0f, 1f));
				ValueTuple<float, float, float> pose = this.GetPose();
				float item = pose.Item1;
				float item2 = pose.Item2;
				float item3 = pose.Item3;
				Interleaving.DoInterleave(item, item2, item3, this._immediateContext.NativePointer, this._backRenderTargetView.NativePointer, shaderResourceView.NativePointer, this._lastSize.Width, this._lastSize.Height, 87, InterMode.RLInter, 0);
			}
			this._statusLock.EnterReadLock();
			LocalEnumeration.InterWaveType currentInterwaveStatus = this._currentInterwaveStatus;
			LocalEnumeration.InterWaveType lastInterwaveStatus = this._lastInterwaveStatus;
			this._statusLock.ExitReadLock();
			if (lastInterwaveStatus != currentInterwaveStatus)
			{
				this._lastInterwaveStatus = currentInterwaveStatus;
				LocalEventPublisher localPublisher = this._localPublisher;
				if (localPublisher != null)
				{
					localPublisher.DoSomethingAsync("InterWaveStatusChanged", (int)currentInterwaveStatus);
				}
			}
			SwapChain1 swapChain = this._swapChain;
			if (swapChain != null)
			{
				swapChain.Present(0, 0);
			}
			this.UpdateFPS();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00018A90 File Offset: 0x00016C90
		private void HandleResizeInRenderThread()
		{
			try
			{
				SizeInt32 pendingResizeSize = this._pendingResizeSize;
				DisplayModeRotation pendingResizeRotation = this._pendingResizeRotation;
				NLogService.WriteLog(string.Format("RenderLoop|Executing resize to {0}x{1}", pendingResizeSize.Width, pendingResizeSize.Height));
				this.InitDoubleBuffer(pendingResizeSize.Width, pendingResizeSize.Height);
				this.InitGpuResources(pendingResizeSize.Width, pendingResizeSize.Height, pendingResizeRotation);
				this._viewportF.Width = (float)pendingResizeSize.Width;
				this._viewportF.Height = (float)pendingResizeSize.Height;
				if (pendingResizeRotation != this._lastRotation)
				{
					this._lastRotation = pendingResizeRotation;
					this._displayManager.RotateTexcoord(this._d3dDevice, pendingResizeRotation);
				}
				this._lastSize = pendingResizeSize;
				FrameConvert frameConvert = this._frameConvert;
				if (frameConvert != null)
				{
					frameConvert.Dispose();
				}
				this._frameConvert = new FrameConvert(this._d3dDevice, this._immediateContext, pendingResizeSize.Width, pendingResizeSize.Height);
				NLogService.WriteLog(string.Format("RenderLoop|Resize completed: {0}x{1}", pendingResizeSize.Width, pendingResizeSize.Height));
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("RenderLoop|Resize failed: " + ex.Message);
			}
			finally
			{
				this._resizeRequested = false;
				this._resizeCompletedEvent.Set();
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00018C00 File Offset: 0x00016E00
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
				int num = Interlocked.CompareExchange(ref this._state, 5, 3);
				if (num == 2)
				{
					Interlocked.Exchange(ref this._state, 5);
				}
				else if (num != 3 && num != 2)
				{
					NLogService.WriteLog(string.Format("BasicCaptureDXGIAsync|Cannot dispose from state: {0}", (CaptureStateDXGI)num));
					return;
				}
				CancellationTokenSource cts = this._cts;
				if (cts != null)
				{
					cts.Cancel();
				}
				this._frameReadyEvent.Set();
				this._resizeCompletedEvent.Set();
				object taskLock = this._taskLock;
				lock (taskLock)
				{
					foreach (Task task in new Task[]
					{
						this._captureTask,
						this._renderTask
					})
					{
						if (task != null)
						{
							try
							{
								if (!task.Wait(TimeSpan.FromSeconds(3.0)))
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
				this.CleanupAllResources();
				CancellationTokenSource cts2 = this._cts;
				if (cts2 != null)
				{
					cts2.Dispose();
				}
				this._cts = null;
				this.CloseRasterSafe();
				LocalEventPublisher localPublisher = this._localPublisher;
				if (localPublisher != null)
				{
					localPublisher.DoSomethingAsync("RasterStatusChanged", (int)GlobalStatusManager.Instance._iRasterStatus);
				}
				LocalEventPublisher localPublisher2 = this._localPublisher;
				if (localPublisher2 != null)
				{
					localPublisher2.DoSomethingAsync("FaceTrackingStatusChanged", -2);
				}
				this._localPublisher = null;
				GC.SuppressFinalize(this);
				this._isDisposed = true;
				Interlocked.Exchange(ref this._state, 0);
				NLogService.WriteLog("BasicCaptureDXGIAsync|Dispose completed");
			}
			finally
			{
				this._disposeSemaphore.Release();
			}
			this._disposeSemaphore.Dispose();
			ReaderWriterLockSlim statusLock = this._statusLock;
			if (statusLock == null)
			{
				return;
			}
			statusLock.Dispose();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00018E5C File Offset: 0x0001705C
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
							int num = MCUSdk.BOE_MCU_getUSBState();
							NLogService.WriteLog(string.Format("状态跟踪:透镜状态:{0};(0=离线,1=打开,2=在线未打开,3=被占用)", num));
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

		// Token: 0x060003C7 RID: 967 RVA: 0x00018E90 File Offset: 0x00017090
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

		// Token: 0x060003C8 RID: 968 RVA: 0x00018EF8 File Offset: 0x000170F8
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this._localPublisher = publisher;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00018F01 File Offset: 0x00017101
		public ICompositionSurface CreateSurface(Compositor compositor)
		{
			return compositor.CreateCompositionSurfaceForSwapChain(this._swapChain);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00018F0F File Offset: 0x0001710F
		public void SetParax(float parax, float zerpPlane)
		{
			FrameConvert frameConvert = this._frameConvert;
			if (frameConvert == null)
			{
				return;
			}
			frameConvert.SetParax(parax, zerpPlane);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00018F24 File Offset: 0x00017124
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

		// Token: 0x060003CC RID: 972 RVA: 0x00018F64 File Offset: 0x00017164
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

		// Token: 0x060003CD RID: 973 RVA: 0x00018FC8 File Offset: 0x000171C8
		private bool IsDisposing()
		{
			return Interlocked.Read(ref this._isDisposingFlag) == 1L;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00018FDC File Offset: 0x000171DC
		private void UpdateFPS()
		{
			DateTime now = DateTime.Now;
			if ((now - this._lastHeartbeatTime).TotalMilliseconds >= 50.0)
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

		// Token: 0x060003CF RID: 975 RVA: 0x00019030 File Offset: 0x00017230
		private ValueTuple<float, float, float> GetPose()
		{
			ValueTuple<float, float, float, float> pose = Facetracker.Instance.GetPose();
			float item = pose.Item1;
			float item2 = pose.Item2;
			float item3 = pose.Item3;
			float item4 = pose.Item4;
			NLogService.WriteLine(string.Format("状态跟踪:人眼坐标获取成功:{0},{1},{2},{3}", new object[]
			{
				item,
				item2,
				item3,
				item4
			}));
			double num = Math.Atan((double)Math.Abs(item / item3)) * 57.29577951308232;
			double num2 = Math.Atan((double)Math.Abs(item2 / item3)) * 57.29577951308232;
			if (num <= (double)this._horAngle && num2 <= (double)this._verAngle && item3 <= this._far && item3 >= this._near)
			{
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
			return new ValueTuple<float, float, float>(item, item2, item3);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00019148 File Offset: 0x00017348
		private void InitGpuResources(int width, int height, DisplayModeRotation rotation)
		{
			this.SafeDispose<Texture2D>(ref this._frameTexture);
			this.SafeDispose<ShaderResourceView>(ref this._frameResourceView);
			this._frameTexture = this.CreateFrameTexture(this._d3dDevice, width, height);
			this._frameResourceView = new ShaderResourceView(this._d3dDevice, this._frameTexture);
			this.CreateDisplayTextureView(this._d3dDevice, width, height, rotation);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000191A8 File Offset: 0x000173A8
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

		// Token: 0x060003D2 RID: 978 RVA: 0x0001921C File Offset: 0x0001741C
		private void CreateDisplayTextureView(Device device, int width, int height, DisplayModeRotation rotation)
		{
			int width2 = width;
			int height2 = height;
			if (rotation == 2 || rotation == 4)
			{
				width2 = height;
				height2 = width;
			}
			this.SafeDispose<Texture2D>(ref this.DisplayTexture);
			Texture2DDescription texture2DDescription = default(Texture2DDescription);
			texture2DDescription.Width = width2;
			texture2DDescription.Height = height2;
			texture2DDescription.Format = 87;
			texture2DDescription.MipLevels = 1;
			texture2DDescription.ArraySize = 1;
			texture2DDescription.SampleDescription = new SampleDescription(1, 0);
			texture2DDescription.Usage = 0;
			texture2DDescription.BindFlags = 40;
			texture2DDescription.CpuAccessFlags = 0;
			this.DisplayTexture = new Texture2D(device, texture2DDescription);
			this.SafeDispose<ShaderResourceView>(ref this._displayResourceView);
			this._displayResourceView = new ShaderResourceView(device, this.DisplayTexture);
			this.SafeDispose<RenderTargetView>(ref this._displayTargetView);
			this._displayTargetView = new RenderTargetView(device, this.DisplayTexture);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000192F0 File Offset: 0x000174F0
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

		// Token: 0x060003D4 RID: 980 RVA: 0x00019348 File Offset: 0x00017548
		private void CleanupAllResources()
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
			this._outputDuplication = null;
			try
			{
				DisplayManager displayManager = this._displayManager;
				if (displayManager != null)
				{
					displayManager.Dispose();
				}
			}
			catch
			{
			}
			this._displayManager = null;
			try
			{
				FrameConvert frameConvert = this._frameConvert;
				if (frameConvert != null)
				{
					frameConvert.Dispose();
				}
			}
			catch
			{
			}
			this._frameConvert = null;
			try
			{
				FormatRecognition formatRecognition = this._formatRecognition;
				if (formatRecognition != null)
				{
					formatRecognition.Dispose();
				}
			}
			catch
			{
			}
			this._formatRecognition = null;
			object sdkLock = this._sdkLock;
			lock (sdkLock)
			{
				try
				{
					Interleaving.ReleaseSDK();
				}
				catch (Exception ex)
				{
					NLogService.WriteLog("Cleanup BOE SDK error: " + ex.Message);
				}
			}
			this.SafeDispose<ShaderResourceView>(ref this._frameResourceView);
			this.SafeDispose<Texture2D>(ref this._frameTexture);
			this.SafeDispose<Texture2D>(ref this.DisplayTexture);
			this.SafeDispose<ShaderResourceView>(ref this._displayResourceView);
			this.SafeDispose<RenderTargetView>(ref this._displayTargetView);
			this.SafeDispose<RenderTargetView>(ref this._backRenderTargetView);
			this.SafeDispose<Texture2D>(ref this._captureBuffer0);
			this.SafeDispose<ShaderResourceView>(ref this._captureSrv0);
			this.SafeDispose<Texture2D>(ref this._captureBuffer1);
			this.SafeDispose<ShaderResourceView>(ref this._captureSrv1);
			try
			{
				DeviceContext immediateContext = this._immediateContext;
				if (immediateContext != null)
				{
					immediateContext.ClearState();
				}
			}
			catch
			{
			}
			try
			{
				SwapChain1 swapChain = this._swapChain;
				if (swapChain != null)
				{
					swapChain.Dispose();
				}
			}
			catch
			{
			}
			this._swapChain = null;
			try
			{
				DeviceContext immediateContext2 = this._immediateContext;
				if (immediateContext2 != null)
				{
					immediateContext2.Dispose();
				}
			}
			catch
			{
			}
			this._immediateContext = null;
			try
			{
				Device d3dDevice = this._d3dDevice;
				if (d3dDevice != null)
				{
					d3dDevice.Dispose();
				}
			}
			catch
			{
			}
			this._d3dDevice = null;
			NLogService.WriteLog("BasicCaptureDXGIAsync|All GPU resources cleaned up");
		}

		// Token: 0x0400026A RID: 618
		private SizeInt32 _lastSize;

		// Token: 0x0400026B RID: 619
		private DisplayModeRotation _lastRotation;

		// Token: 0x0400026C RID: 620
		private OutputDuplication _outputDuplication;

		// Token: 0x0400026D RID: 621
		private OutputDescription _outputDesc;

		// Token: 0x0400026E RID: 622
		private Device _d3dDevice;

		// Token: 0x0400026F RID: 623
		private DeviceContext _immediateContext;

		// Token: 0x04000270 RID: 624
		private SwapChain1 _swapChain;

		// Token: 0x04000271 RID: 625
		private const int TIMEOUT_INTERVAL = 50;

		// Token: 0x04000272 RID: 626
		private const int TARGET_FRAME_INTERVAL_MS = 16;

		// Token: 0x04000273 RID: 627
		private RenderTargetView _backRenderTargetView;

		// Token: 0x04000274 RID: 628
		private Texture2D _frameTexture;

		// Token: 0x04000275 RID: 629
		private ShaderResourceView _frameResourceView;

		// Token: 0x04000276 RID: 630
		public Texture2D DisplayTexture;

		// Token: 0x04000277 RID: 631
		private RenderTargetView _displayTargetView;

		// Token: 0x04000278 RID: 632
		private ShaderResourceView _displayResourceView;

		// Token: 0x04000279 RID: 633
		private readonly YLStereoMatcher _stereoMatcher;

		// Token: 0x0400027A RID: 634
		private FormatRecognition _formatRecognition;

		// Token: 0x0400027B RID: 635
		private FrameConvert _frameConvert;

		// Token: 0x0400027C RID: 636
		private DisplayManager _displayManager;

		// Token: 0x0400027D RID: 637
		private int _faceTrackingState;

		// Token: 0x0400027E RID: 638
		private LocalEventPublisher _localPublisher;

		// Token: 0x0400027F RID: 639
		private LocalEnumeration.InterWaveType _currentInterwaveStatus;

		// Token: 0x04000280 RID: 640
		private LocalEnumeration.InterWaveType _lastInterwaveStatus;

		// Token: 0x04000281 RID: 641
		private readonly ReaderWriterLockSlim _statusLock = new ReaderWriterLockSlim();

		// Token: 0x04000282 RID: 642
		private RawViewportF _viewportF;

		// Token: 0x04000283 RID: 643
		private DateTime _lastFpsTime;

		// Token: 0x04000284 RID: 644
		private int _frameCount;

		// Token: 0x04000285 RID: 645
		private int _fps;

		// Token: 0x04000286 RID: 646
		private DateTime _lastHeartbeatTime;

		// Token: 0x04000287 RID: 647
		private float _near = 400f;

		// Token: 0x04000288 RID: 648
		private float _far = 1000f;

		// Token: 0x04000289 RID: 649
		private float _horAngle = 33f;

		// Token: 0x0400028A RID: 650
		private float _verAngle = 26f;

		// Token: 0x0400028B RID: 651
		private int _state;

		// Token: 0x0400028C RID: 652
		private long _isDisposingFlag;

		// Token: 0x0400028D RID: 653
		private volatile bool _isDisposed;

		// Token: 0x0400028E RID: 654
		private readonly object _sdkLock = new object();

		// Token: 0x0400028F RID: 655
		private readonly SemaphoreSlim _disposeSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000290 RID: 656
		private CancellationTokenSource _cts;

		// Token: 0x04000291 RID: 657
		private Task _captureTask;

		// Token: 0x04000292 RID: 658
		private Task _renderTask;

		// Token: 0x04000293 RID: 659
		private readonly object _taskLock = new object();

		// Token: 0x04000294 RID: 660
		private Texture2D _captureBuffer0;

		// Token: 0x04000295 RID: 661
		private Texture2D _captureBuffer1;

		// Token: 0x04000296 RID: 662
		private ShaderResourceView _captureSrv0;

		// Token: 0x04000297 RID: 663
		private ShaderResourceView _captureSrv1;

		// Token: 0x04000298 RID: 664
		private volatile int _writeIndex;

		// Token: 0x04000299 RID: 665
		private volatile int _readIndex;

		// Token: 0x0400029A RID: 666
		private readonly object _bufferLock = new object();

		// Token: 0x0400029B RID: 667
		private readonly AutoResetEvent _frameReadyEvent = new AutoResetEvent(false);

		// Token: 0x0400029C RID: 668
		private volatile bool _hasValidContent;

		// Token: 0x0400029D RID: 669
		private volatile bool _resizeRequested;

		// Token: 0x0400029E RID: 670
		private SizeInt32 _pendingResizeSize;

		// Token: 0x0400029F RID: 671
		private DisplayModeRotation _pendingResizeRotation;

		// Token: 0x040002A0 RID: 672
		private readonly ManualResetEventSlim _resizeCompletedEvent = new ManualResetEventSlim(true);
	}
}
