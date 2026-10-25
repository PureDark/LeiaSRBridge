using System;
using AiStereoKitWrapper;
using AstraDepth3D.BOEStereoRender;
using AstraDepth3D.EventBus;
using AstraDepth3D.WindowHelper;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x0200005D RID: 93
	internal class FrameConvert : IDisposable
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001D2FB File Offset: 0x0001B4FB
		public FrameConvert(Device device, DeviceContext immediateContext, int width, int height)
		{
			this.Initialize(device, immediateContext, width, height);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001D31C File Offset: 0x0001B51C
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
					YLStereoRenderCSharp ylstereoRenderCSharp = this.stereoRenderCSharp;
					if (!((ylstereoRenderCSharp != null) ? new bool?(ylstereoRenderCSharp.Render(shaderResourceView.NativePointer, this.stereoTargetView.NativePointer, 0)) : null).GetValueOrDefault())
					{
						NLogService.WriteLog("Error:stereoRender render Failed!");
					}
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		public ShaderResourceView GetShaderResource()
		{
			if (this.stereoResourceView == null)
			{
				NLogService.WriteLog("Error:stereoResourceView is null!");
			}
			return this.stereoResourceView;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D3D6 File Offset: 0x0001B5D6
		public void SetParax(float parax, float zerpPlane)
		{
			NLogService.WriteLog(string.Format("BasicCapture|para:{0},zerpPlane:{1}", parax, zerpPlane));
			this.stereoRenderCSharp.SetParax(parax, zerpPlane);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001D400 File Offset: 0x0001B600
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001D408 File Offset: 0x0001B608
		private void Initialize(Device device, DeviceContext immediateContext, int width, int height)
		{
			this.stereoRenderCSharp = new YLStereoRenderCSharp();
			if (!this.stereoRenderCSharp.InitGraphicsDevice(device.NativePointer, immediateContext.NativePointer))
			{
				NLogService.WriteLog("Error:stereoRender InitGraphicsDevice Failed!");
			}
			this.stereoRenderCSharp.SetFrameSize(width, height);
			this.stereoRenderCSharp.SetParax(0.5f, 0.5f);
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

		// Token: 0x0600041E RID: 1054 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
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

		// Token: 0x0600041F RID: 1055 RVA: 0x0001D580 File Offset: 0x0001B780
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

		// Token: 0x0400031B RID: 795
		private Texture2D stereoTexture;

		// Token: 0x0400031C RID: 796
		private RenderTargetView stereoTargetView;

		// Token: 0x0400031D RID: 797
		private ShaderResourceView stereoResourceView;

		// Token: 0x0400031E RID: 798
		private YLStereoRenderCSharp stereoRenderCSharp;

		// Token: 0x0400031F RID: 799
		private FrameCopy frameCopy;

		// Token: 0x04000320 RID: 800
		private RawViewportF viewportF;

		// Token: 0x04000321 RID: 801
		private readonly object g_lock = new object();
	}
}
