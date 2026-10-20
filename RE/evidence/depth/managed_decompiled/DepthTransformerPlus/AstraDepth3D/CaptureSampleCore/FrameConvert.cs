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
	// Token: 0x02000051 RID: 81
	internal class FrameConvert : IDisposable
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0001AFE7 File Offset: 0x000191E7
		public FrameConvert(Device device, int width, int height)
		{
			this.Initialize(device, width, height);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001B004 File Offset: 0x00019204
		public void DrawFrame(Device device, ShaderResourceView shaderResourceView, LocalEnumeration.InterWaveType format)
		{
			lock (this.g_lock)
			{
				if (format == LocalEnumeration.InterWaveType.InterWave_SBS)
				{
					FrameCopy frameCopy = this.frameCopy;
					if (frameCopy != null)
					{
						frameCopy.DrawFrame(device, shaderResourceView, this.stereoTargetView, this.viewportF);
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

		// Token: 0x0600035A RID: 858 RVA: 0x0001B0A4 File Offset: 0x000192A4
		public ShaderResourceView GetShaderResource()
		{
			if (this.stereoResourceView == null)
			{
				NLogService.WriteLog("Error:stereoResourceView is null!");
			}
			return this.stereoResourceView;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001B0BE File Offset: 0x000192BE
		public void SetParax(float parax, float zerpPlane)
		{
			NLogService.WriteLog(string.Format("BasicCapture|para:{0},zerpPlane:{1}", parax, zerpPlane));
			this.stereoRenderCSharp.SetParax(parax, zerpPlane);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001B0E8 File Offset: 0x000192E8
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001B0F0 File Offset: 0x000192F0
		private void Initialize(Device device, int width, int height)
		{
			this.stereoRenderCSharp = new YLStereoRenderCSharp();
			if (!this.stereoRenderCSharp.InitGraphicsDevice(device.NativePointer, device.ImmediateContext.NativePointer))
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

		// Token: 0x0600035E RID: 862 RVA: 0x0001B1D0 File Offset: 0x000193D0
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

		// Token: 0x0600035F RID: 863 RVA: 0x0001B26C File Offset: 0x0001946C
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

		// Token: 0x040002E6 RID: 742
		private Texture2D stereoTexture;

		// Token: 0x040002E7 RID: 743
		private RenderTargetView stereoTargetView;

		// Token: 0x040002E8 RID: 744
		private ShaderResourceView stereoResourceView;

		// Token: 0x040002E9 RID: 745
		private YLStereoRenderCSharp stereoRenderCSharp;

		// Token: 0x040002EA RID: 746
		private FrameCopy frameCopy;

		// Token: 0x040002EB RID: 747
		private RawViewportF viewportF;

		// Token: 0x040002EC RID: 748
		private readonly object g_lock = new object();
	}
}
