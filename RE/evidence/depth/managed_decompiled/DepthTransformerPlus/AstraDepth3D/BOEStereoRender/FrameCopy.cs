using System;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;

namespace AstraDepth3D.BOEStereoRender
{
	// Token: 0x02000059 RID: 89
	internal class FrameCopy : IDisposable
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0001CD08 File Offset: 0x0001AF08
		public FrameCopy()
		{
			this.clearColor = new RawColor4(0f, 0f, 0f, 1f);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001CD8E File Offset: 0x0001AF8E
		public void Initialize(Device device)
		{
			this.CreateShadersAndInputLayout(device);
			this.CreateVertexBuffer(device);
			this.CreateSamplerState(device);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
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

		// Token: 0x060003A9 RID: 937 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		public void Release()
		{
			if (this.inputLayout != null)
			{
				this.inputLayout.Dispose();
				this.inputLayout = null;
			}
			if (this.inputSignature != null)
			{
				this.inputSignature.Dispose();
				this.inputSignature = null;
			}
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
				this.vertexBuffer = null;
			}
			if (this.indexBuffer != null)
			{
				this.indexBuffer.Dispose();
				this.indexBuffer = null;
			}
			if (this.vertexShader != null)
			{
				this.vertexShader.Dispose();
				this.vertexShader = null;
			}
			if (this.pixelShader != null)
			{
				this.pixelShader.Dispose();
				this.pixelShader = null;
			}
			if (this.m_sampleState != null)
			{
				SamplerState sampleState = this.m_sampleState;
				if (sampleState != null)
				{
					sampleState.Dispose();
				}
				this.m_sampleState = null;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001CF78 File Offset: 0x0001B178
		private void CreateShadersAndInputLayout(Device device)
		{
			this.inputSignature = ShaderSignature.GetInputSignature(Shader.g_VertexShader);
			this.vertexShader = new VertexShader(device, Shader.g_VertexShader, null);
			this.pixelShader = new PixelShader(device, Shader.g_normalPS, null);
			this.inputLayout = new InputLayout(device, this.inputSignature, this.inputElements);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
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

		// Token: 0x060003AC RID: 940 RVA: 0x0001D048 File Offset: 0x0001B248
		private void CreateSamplerState(Device device)
		{
			SamplerStateDescription description = SamplerStateDescription.Default();
			this.m_sampleState = new SamplerState(device, description);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001D068 File Offset: 0x0001B268
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x0400031B RID: 795
		private VertexShader vertexShader;

		// Token: 0x0400031C RID: 796
		private PixelShader pixelShader;

		// Token: 0x0400031D RID: 797
		private ShaderSignature inputSignature;

		// Token: 0x0400031E RID: 798
		private InputLayout inputLayout;

		// Token: 0x0400031F RID: 799
		private SamplerState m_sampleState;

		// Token: 0x04000320 RID: 800
		private readonly InputElement[] inputElements = new InputElement[]
		{
			new InputElement("myPOSITION", 0, 6, 0, 0, 0, 0),
			new InputElement("myTEXCOORD", 0, 16, 12, 0, 0, 0)
		};

		// Token: 0x04000321 RID: 801
		private readonly float[] vertices = new float[]
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

		// Token: 0x04000322 RID: 802
		private Buffer vertexBuffer;

		// Token: 0x04000323 RID: 803
		private Buffer indexBuffer;

		// Token: 0x04000324 RID: 804
		private int m_indicesCount;

		// Token: 0x04000325 RID: 805
		private VertexBufferBinding vertexBufferBinding;

		// Token: 0x04000326 RID: 806
		private RawColor4 clearColor;
	}
}
