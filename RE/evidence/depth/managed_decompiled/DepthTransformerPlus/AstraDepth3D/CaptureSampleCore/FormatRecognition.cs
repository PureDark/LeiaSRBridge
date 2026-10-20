using System;
using System.Numerics;
using System.Runtime.InteropServices;
using AstraDepth3D.WindowHelper;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x02000050 RID: 80
	internal class FormatRecognition : IDisposable
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0001A9AC File Offset: 0x00018BAC
		public FormatRecognition()
		{
			this.clearColor = new RawColor4(0f, 0f, 0f, 1f);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001AA48 File Offset: 0x00018C48
		public void Initialize(Device device)
		{
			this.CreateShadersAndInputLayout(device);
			this.CreateVertexBuffer(device);
			this.CreateSamplerState(device);
			this.InitScaleTextureView(device, this.scaleW, this.scaleH);
			RawViewportF rawViewportF = default(RawViewportF);
			rawViewportF.X = 0f;
			rawViewportF.Y = 0f;
			rawViewportF.Width = (float)this.scaleW;
			rawViewportF.Height = (float)this.scaleH;
			rawViewportF.MinDepth = 0f;
			rawViewportF.MaxDepth = 0f;
			this.viewPort = rawViewportF;
			NLogService.WriteLog("FormatRecognition initialized successfully");
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		public ValueTuple<byte[], int> DrawFrame(Device device, ShaderResourceView shaderResourceView)
		{
			device.ImmediateContext.ClearRenderTargetView(this.scaleTargetView, this.clearColor);
			device.ImmediateContext.OutputMerger.SetRenderTargets(this.scaleTargetView);
			device.ImmediateContext.Rasterizer.SetViewport(this.viewPort);
			device.ImmediateContext.InputAssembler.InputLayout = this.inputLayout;
			device.ImmediateContext.InputAssembler.PrimitiveTopology = 4;
			device.ImmediateContext.InputAssembler.SetVertexBuffers(0, this.vertexBufferBinding);
			device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, 42, 0);
			device.ImmediateContext.VertexShader.Set(this.vertexShader);
			device.ImmediateContext.PixelShader.Set(this.pixelShader);
			device.ImmediateContext.PixelShader.SetSampler(0, this.m_sampleState);
			device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
			device.ImmediateContext.DrawIndexed(this.m_indicesCount, 0, 0);
			device.ImmediateContext.CopyResource(this.scaleTexture, this.scaleCopyTexture);
			DataBox dataBox = device.ImmediateContext.MapSubresource(this.scaleCopyTexture, 0, 1, 0);
			int rowPitch = dataBox.RowPitch;
			byte[] buffer = new byte[dataBox.SlicePitch];
			NLogService.WriteLine(string.Format("RowPitch:{0}", dataBox.RowPitch));
			Marshal.Copy(dataBox.DataPointer, buffer, 0, dataBox.SlicePitch);
			device.ImmediateContext.UnmapSubresource(this.scaleCopyTexture, 0);
			return new ValueTuple<byte[], int>(buffer, rowPitch);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001AC78 File Offset: 0x00018E78
		public void DrawFrame(Device device, ShaderResourceView shaderResourceView, RenderTargetView renderTargetView, RawViewportF viewportF)
		{
			device.ImmediateContext.ClearRenderTargetView(renderTargetView, this.clearColor);
			device.ImmediateContext.OutputMerger.SetRenderTargets(renderTargetView);
			device.ImmediateContext.Rasterizer.SetViewport(viewportF);
			device.ImmediateContext.InputAssembler.InputLayout = this.inputLayout;
			device.ImmediateContext.InputAssembler.PrimitiveTopology = 4;
			device.ImmediateContext.InputAssembler.SetVertexBuffers(0, this.vertexBufferBinding);
			device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, 42, 0);
			device.ImmediateContext.VertexShader.Set(this.vertexShader);
			device.ImmediateContext.PixelShader.Set(this.pixelShader);
			device.ImmediateContext.PixelShader.SetSampler(0, this.m_sampleState);
			device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
			device.ImmediateContext.DrawIndexed(this.m_indicesCount, 0, 0);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001AD7C File Offset: 0x00018F7C
		public void Release()
		{
			InputLayout inputLayout = this.inputLayout;
			if (inputLayout != null)
			{
				inputLayout.Dispose();
			}
			ShaderSignature shaderSignature = this.inputSignature;
			if (shaderSignature != null)
			{
				shaderSignature.Dispose();
			}
			Buffer buffer = this.vertexBuffer;
			if (buffer != null)
			{
				buffer.Dispose();
			}
			Buffer buffer2 = this.indexBuffer;
			if (buffer2 != null)
			{
				buffer2.Dispose();
			}
			VertexShader vertexShader = this.vertexShader;
			if (vertexShader != null)
			{
				vertexShader.Dispose();
			}
			PixelShader pixelShader = this.pixelShader;
			if (pixelShader != null)
			{
				pixelShader.Dispose();
			}
			RenderTargetView renderTargetView = this.scaleTargetView;
			if (renderTargetView != null)
			{
				renderTargetView.Dispose();
			}
			Texture2D texture2D = this.scaleTexture;
			if (texture2D != null)
			{
				texture2D.Dispose();
			}
			Texture2D texture2D2 = this.scaleCopyTexture;
			if (texture2D2 != null)
			{
				texture2D2.Dispose();
			}
			SamplerState sampleState = this.m_sampleState;
			if (sampleState != null)
			{
				sampleState.Dispose();
			}
			NLogService.WriteLog("[FormatRecognition] Release");
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001AE40 File Offset: 0x00019040
		private void CreateShadersAndInputLayout(Device device)
		{
			this.inputSignature = ShaderSignature.GetInputSignature(ShaderData.g_Texture_VS);
			this.vertexShader = new VertexShader(device, ShaderData.g_Texture_VS, null);
			this.pixelShader = new PixelShader(device, ShaderData.g_normalPS, null);
			this.inputLayout = new InputLayout(device, this.inputSignature, this.inputElements);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001AEA0 File Offset: 0x000190A0
		private void CreateVertexBuffer(Device device)
		{
			this.vertexBuffer = Buffer.Create<float>(device, 1, this.vertices, 0, 0, 0, 0, 0);
			int[] indices = new int[]
			{
				0,
				1,
				2,
				1,
				3,
				2
			};
			this.m_indicesCount = indices.Length;
			this.indexBuffer = Buffer.Create<int>(device, 2, indices, 0, 0, 0, 0, 0);
			this.vertexBufferBinding = new VertexBufferBinding(this.vertexBuffer, Utilities.SizeOf<Vector3>() + Utilities.SizeOf<Vector2>(), 0);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001AF10 File Offset: 0x00019110
		private void CreateSamplerState(Device device)
		{
			SamplerStateDescription description = SamplerStateDescription.Default();
			this.m_sampleState = new SamplerState(device, description);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001AF30 File Offset: 0x00019130
		private void InitScaleTextureView(Device device, int width, int height)
		{
			Texture2DDescription texture2DDescription = default(Texture2DDescription);
			texture2DDescription.Width = width;
			texture2DDescription.Height = height;
			texture2DDescription.Format = 87;
			texture2DDescription.MipLevels = 1;
			texture2DDescription.ArraySize = 1;
			texture2DDescription.SampleDescription = new SampleDescription(1, 0);
			texture2DDescription.Usage = 0;
			texture2DDescription.BindFlags = 32;
			texture2DDescription.CpuAccessFlags = 0;
			Texture2DDescription description = texture2DDescription;
			this.scaleTexture = new Texture2D(device, description);
			this.scaleTargetView = new RenderTargetView(device, this.scaleTexture);
			description.Usage = 3;
			description.BindFlags = 0;
			description.CpuAccessFlags = 131072;
			this.scaleCopyTexture = new Texture2D(device, description);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001AFDF File Offset: 0x000191DF
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x040002D4 RID: 724
		private VertexShader vertexShader;

		// Token: 0x040002D5 RID: 725
		private PixelShader pixelShader;

		// Token: 0x040002D6 RID: 726
		private ShaderSignature inputSignature;

		// Token: 0x040002D7 RID: 727
		private InputLayout inputLayout;

		// Token: 0x040002D8 RID: 728
		private SamplerState m_sampleState;

		// Token: 0x040002D9 RID: 729
		private InputElement[] inputElements = new InputElement[]
		{
			new InputElement("myPOSITION", 0, 6, 0, 0, 0, 0),
			new InputElement("myTEXCOORD", 0, 16, 12, 0, 0, 0)
		};

		// Token: 0x040002DA RID: 730
		private float[] vertices = new float[]
		{
			-1f,
			1f,
			0f,
			0f,
			0f,
			1f,
			1f,
			0f,
			1f,
			0f,
			-1f,
			-1f,
			0f,
			0f,
			1f,
			1f,
			-1f,
			0f,
			1f,
			1f
		};

		// Token: 0x040002DB RID: 731
		private Buffer vertexBuffer;

		// Token: 0x040002DC RID: 732
		private Buffer indexBuffer;

		// Token: 0x040002DD RID: 733
		private int m_indicesCount;

		// Token: 0x040002DE RID: 734
		private VertexBufferBinding vertexBufferBinding;

		// Token: 0x040002DF RID: 735
		private RawColor4 clearColor;

		// Token: 0x040002E0 RID: 736
		public int scaleW = 2560;

		// Token: 0x040002E1 RID: 737
		public int scaleH = 1600;

		// Token: 0x040002E2 RID: 738
		private Texture2D scaleCopyTexture;

		// Token: 0x040002E3 RID: 739
		private Texture2D scaleTexture;

		// Token: 0x040002E4 RID: 740
		private RenderTargetView scaleTargetView;

		// Token: 0x040002E5 RID: 741
		private RawViewportF viewPort;
	}
}
