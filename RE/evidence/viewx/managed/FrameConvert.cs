using System;
using AiStereoKitWrapper;
using DepthTransformer.BOEStereoRender;
using DepthTransformer.EventBus;
using DepthTransformer.WindowHelper;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace DepthTransformer.CaptureSampleCore
{
	// Token: 0x0200005D RID: 93
	internal class FrameConvert : IDisposable
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00019CDF File Offset: 0x00017EDF
		public FrameConvert(Device device, DeviceContext immediateContext, int width, int height)
		{
			this.Initialize(device, immediateContext, width, height);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00019D00 File Offset: 0x00017F00
		public void DrawFrame(DeviceContext immediateContext, ShaderResourceView shaderResourceView, LocalEnumeration.InterWaveType format)
		{
			lock (this.g_lock)
			{
				if (format == LocalEnumeration.InterWaveType.InterWave_SBS)
				{
					FrameCopy frameCopy = this.frameCopy;
					if (frameCopy != null)
					{
						frameCopy.DrawFrame(immediateContext, shaderResourceView, this.stereoTargetView, this.viewportF);
					}
				}
				else
				{
					NLogService.WriteLog("begin frameConvert?.DrawFrame");
					YLStereoRenderCSharp ylstereoRenderCSharp = this.stereoRenderCSharp;
					bool? flag = (ylstereoRenderCSharp != null) ? new bool?(ylstereoRenderCSharp.Render(shaderResourceView.NativePointer, this.stereoTargetView.NativePointer, 1)) : null;
					NLogService.WriteLog("end frameConvert?.DrawFrame");
					if (!flag.GetValueOrDefault())
					{
						NLogService.WriteLog("Error:stereoRender render Failed!");
					}
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00019DB4 File Offset: 0x00017FB4
		public Texture2D GetTexture()
		{
			if (this.stereoTexture == null)
			{
				NLogService.WriteLog("Error:stereoResourceView is null!");
			}
			return this.stereoTexture;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00019DCE File Offset: 0x00017FCE
		public void SetParax(float parax, float zerpPlane)
		{
			NLogService.WriteLog(string.Format("BasicCapture|para:{0},zerpPlane:{1}", parax, zerpPlane));
			this.stereoRenderCSharp.SetParax(parax, zerpPlane);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00019E00 File Offset: 0x00018000
		private void Initialize(Device device, DeviceContext immediateContext, int width, int height)
		{
			this.stereoRenderCSharp = new YLStereoRenderCSharp();
			if (!this.stereoRenderCSharp.InitGraphicsDevice(device.NativePointer, immediateContext.NativePointer))
			{
				NLogService.WriteLog("Error:stereoRender InitGraphicsDevice Failed!");
			}
			this.stereoRenderCSharp.SetFrameSize(width, height);
			NLogService.WriteLog("stereoRender InitGraphicsDevice Success!");
			this.frameCopy = new FrameCopy();
			this.frameCopy.Initialize(device);
			this.InitStereoTextureView(device, width, height);
			RawViewportF rawViewportF = default(RawViewportF);
			rawViewportF.X = 0f;
			rawViewportF.Y = 0f;
			rawViewportF.Width = (float)width;
			rawViewportF.Height = (float)height;
			rawViewportF.MinDepth = 0f;
			rawViewportF.MaxDepth = 0f;
			this.viewportF = rawViewportF;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019EC8 File Offset: 0x000180C8
		private void Release()
		{
			lock (this.g_lock)
			{
				YLStereoRenderCSharp ylstereoRenderCSharp = this.stereoRenderCSharp;
				if (ylstereoRenderCSharp != null)
				{
					ylstereoRenderCSharp.Release();
				}
				this.stereoRenderCSharp = null;
				FrameCopy frameCopy = this.frameCopy;
				if (frameCopy != null)
				{
					frameCopy.Dispose();
				}
				this.frameCopy = null;
				ShaderResourceView shaderResourceView = this.stereoResourceView;
				if (shaderResourceView != null)
				{
					shaderResourceView.Dispose();
				}
				RenderTargetView renderTargetView = this.stereoTargetView;
				if (renderTargetView != null)
				{
					renderTargetView.Dispose();
				}
				Texture2D texture2D = this.stereoTexture;
				if (texture2D != null)
				{
					texture2D.Dispose();
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00019F64 File Offset: 0x00018164
		private void InitStereoTextureView(Device device, int width, int height)
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
			Texture2DDescription texture2DDescription2 = texture2DDescription;
			if (this.stereoTexture != null)
			{
				this.stereoTexture.Dispose();
				this.stereoTexture = null;
			}
			this.stereoTexture = new Texture2D(device, texture2DDescription2);
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

		// Token: 0x040002E7 RID: 743
		private Texture2D stereoTexture;

		// Token: 0x040002E8 RID: 744
		private RenderTargetView stereoTargetView;

		// Token: 0x040002E9 RID: 745
		private ShaderResourceView stereoResourceView;

		// Token: 0x040002EA RID: 746
		private YLStereoRenderCSharp stereoRenderCSharp;

		// Token: 0x040002EB RID: 747
		private FrameCopy frameCopy;

		// Token: 0x040002EC RID: 748
		private RawViewportF viewportF;

		// Token: 0x040002ED RID: 749
		private readonly object g_lock = new object();
	}
}
