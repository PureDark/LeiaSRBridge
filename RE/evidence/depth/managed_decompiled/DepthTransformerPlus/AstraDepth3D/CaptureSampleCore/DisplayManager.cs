using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x0200004F RID: 79
	internal class DisplayManager : IDisposable
	{
		// Token: 0x0600033E RID: 830 RVA: 0x000196A4 File Offset: 0x000178A4
		public DisplayManager()
		{
			this.clearColor = new RawColor4(0f, 0f, 0f, 1f);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001972A File Offset: 0x0001792A
		public void Initialize(Device device)
		{
			this.Initialize(device, 0);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00019734 File Offset: 0x00017934
		public void Initialize(Device device, DisplayModeRotation rotation)
		{
			this.CreateShadersAndInputLayout(device);
			this.CreateVertexBuffer(device, rotation);
			this.CreateSamplerState(device);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001974C File Offset: 0x0001794C
		public void RotateTexcoord(Device device, DisplayModeRotation rotation)
		{
			this.CreateVertexBuffer(device, rotation);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00019758 File Offset: 0x00017958
		public void SetDirtyVert(OutputDescription desc, Texture2DDescription FullDesc, Texture2DDescription thisDesc)
		{
			RuntimeHelpers.InitializeArray(new float[20], fieldof(<PrivateImplementationDetails>.862ADEE26AC9FFF8C889150BCC0CF3C102D11D3943ADCEB0F9EB792E24D7F48E).FieldHandle);
			switch (desc.Rotation)
			{
			default:
				return;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001979C File Offset: 0x0001799C
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

		// Token: 0x06000344 RID: 836 RVA: 0x000198A0 File Offset: 0x00017AA0
		public DUPL_RETURN ProcessFrame(Device device, in FRAME_DATA Data, [In] [Out] Texture2D SharedSurf, int OffsetX, int OffsetY, in OutputDescription DeskDesc)
		{
			DUPL_RETURN Ret = DUPL_RETURN.DUPL_RETURN_SUCCESS;
			if (Data.FrameInfo.TotalMetadataBufferSize != 0)
			{
				Texture2DDescription Desc = Data.Frame.Description;
				if (Data.MoveCount != 0)
				{
					Ret = this.CopyMove(device, SharedSurf, Data.MetaData, Data.MoveCount, OffsetX, OffsetY, DeskDesc, Desc.Width, Desc.Height);
					if (Ret != DUPL_RETURN.DUPL_RETURN_SUCCESS)
					{
						return Ret;
					}
				}
				if (Data.DirtyCount != 0)
				{
					Ret = this.CopyDirty(device, Data.Frame, SharedSurf, Data.dirtyData, Data.DirtyCount, OffsetX, OffsetY, DeskDesc);
				}
			}
			return Ret;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00019930 File Offset: 0x00017B30
		private DUPL_RETURN CopyDirty(Device device, [In] Texture2D SrcSurface, [In] [Out] Texture2D SharedSurf, [In] RawRectangle[] DirtyBuffer, int DirtyCount, int OffsetX, int OffsetY, in OutputDescription DeskDesc)
		{
			Texture2DDescription FullDesc = SharedSurf.Description;
			Texture2DDescription ThisDesc = SrcSurface.Description;
			if (this.m_RTV == null)
			{
				this.m_RTV = new RenderTargetView(device, SharedSurf);
			}
			ShaderResourceViewDescription shaderResourceViewDescription = default(ShaderResourceViewDescription);
			shaderResourceViewDescription.Format = ThisDesc.Format;
			shaderResourceViewDescription.Dimension = 4;
			shaderResourceViewDescription.Texture2D.MostDetailedMip = ThisDesc.MipLevels - 1;
			shaderResourceViewDescription.Texture2D.MipLevels = ThisDesc.MipLevels;
			ShaderResourceViewDescription ShaderDesc = shaderResourceViewDescription;
			ShaderResourceView ShaderResource = new ShaderResourceView(device, SharedSurf, ShaderDesc);
			device.ImmediateContext.OutputMerger.SetBlendState(null, new RawColor4?(this.clearColor), uint.MaxValue);
			device.ImmediateContext.OutputMerger.SetRenderTargets(this.m_RTV);
			device.ImmediateContext.VertexShader.Set(this.vertexShader);
			device.ImmediateContext.PixelShader.Set(this.pixelShader);
			device.ImmediateContext.PixelShader.SetShaderResource(0, ShaderResource);
			device.ImmediateContext.PixelShader.SetSampler(0, this.m_sampleState);
			device.ImmediateContext.InputAssembler.PrimitiveTopology = 4;
			device.ImmediateContext.InputAssembler.InputLayout = this.inputLayout;
			if (6 * DirtyCount > this.m_DirtyVertexBufferAlloc.Length)
			{
				this.m_DirtyVertexBufferAlloc = new VERTEX[6 * DirtyCount];
			}
			Span<VERTEX> DirtyVertex = new Span<VERTEX>(this.m_DirtyVertexBufferAlloc);
			for (int i = 0; i < DirtyCount; i++)
			{
				this.SetDirtyVert(DirtyVertex.Slice(i * 6, 6), DirtyBuffer[i], OffsetX, OffsetY, DeskDesc, FullDesc, ThisDesc);
			}
			Buffer VertBuf = Buffer.Create<VERTEX>(device, 1, this.m_DirtyVertexBufferAlloc, 0, 0, 0, 0, 0);
			VertexBufferBinding vertexBufferBinding;
			vertexBufferBinding..ctor(VertBuf, Utilities.SizeOf<Vector3>() + Utilities.SizeOf<Vector2>(), 0);
			device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
			RawViewportF rawViewportF = default(RawViewportF);
			rawViewportF.X = 0f;
			rawViewportF.Y = 0f;
			rawViewportF.Width = (float)FullDesc.Width;
			rawViewportF.Height = (float)FullDesc.Height;
			rawViewportF.MinDepth = 0f;
			rawViewportF.MaxDepth = 1f;
			RawViewportF VP = rawViewportF;
			device.ImmediateContext.Rasterizer.SetViewport(VP);
			device.ImmediateContext.Draw(6 * DirtyCount, 0);
			return DUPL_RETURN.DUPL_RETURN_SUCCESS;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00019B78 File Offset: 0x00017D78
		private void SetMoveRect(out RawRectangle SrcRect, out RawRectangle DestRect, in OutputDescription DeskDesc, in OutputDuplicateMoveRectangle MoveRect, int TexWidth, int TexHeight)
		{
			switch (DeskDesc.Rotation)
			{
			case 0:
			case 1:
				SrcRect.Left = MoveRect.SourcePoint.X;
				SrcRect.Top = MoveRect.SourcePoint.Y;
				SrcRect.Right = MoveRect.SourcePoint.X + MoveRect.DestinationRect.Right - MoveRect.DestinationRect.Left;
				SrcRect.Bottom = MoveRect.SourcePoint.Y + MoveRect.DestinationRect.Bottom - MoveRect.DestinationRect.Top;
				DestRect = MoveRect.DestinationRect;
				return;
			case 2:
				SrcRect.Left = TexHeight - (MoveRect.SourcePoint.Y + MoveRect.DestinationRect.Bottom - MoveRect.DestinationRect.Top);
				SrcRect.Top = MoveRect.SourcePoint.X;
				SrcRect.Right = TexHeight - MoveRect.SourcePoint.Y;
				SrcRect.Bottom = MoveRect.SourcePoint.X + MoveRect.DestinationRect.Right - MoveRect.DestinationRect.Left;
				DestRect.Left = TexHeight - MoveRect.DestinationRect.Bottom;
				DestRect.Top = MoveRect.DestinationRect.Left;
				DestRect.Right = TexHeight - MoveRect.DestinationRect.Top;
				DestRect.Bottom = MoveRect.DestinationRect.Right;
				return;
			case 3:
				SrcRect.Left = TexWidth - (MoveRect.SourcePoint.X + MoveRect.DestinationRect.Right - MoveRect.DestinationRect.Left);
				SrcRect.Top = TexHeight - (MoveRect.SourcePoint.Y + MoveRect.DestinationRect.Bottom - MoveRect.DestinationRect.Top);
				SrcRect.Right = TexWidth - MoveRect.SourcePoint.X;
				SrcRect.Bottom = TexHeight - MoveRect.SourcePoint.Y;
				DestRect.Left = TexWidth - MoveRect.DestinationRect.Right;
				DestRect.Top = TexHeight - MoveRect.DestinationRect.Bottom;
				DestRect.Right = TexWidth - MoveRect.DestinationRect.Left;
				DestRect.Bottom = TexHeight - MoveRect.DestinationRect.Top;
				return;
			case 4:
				SrcRect.Left = MoveRect.SourcePoint.X;
				SrcRect.Top = TexWidth - (MoveRect.SourcePoint.X + MoveRect.DestinationRect.Right - MoveRect.DestinationRect.Left);
				SrcRect.Right = MoveRect.SourcePoint.Y + MoveRect.DestinationRect.Bottom - MoveRect.DestinationRect.Top;
				SrcRect.Bottom = TexWidth - MoveRect.SourcePoint.X;
				DestRect.Left = MoveRect.DestinationRect.Top;
				DestRect.Top = TexWidth - MoveRect.DestinationRect.Right;
				DestRect.Right = MoveRect.DestinationRect.Bottom;
				DestRect.Bottom = TexWidth - MoveRect.DestinationRect.Left;
				return;
			default:
				DestRect = default(RawRectangle);
				SrcRect = default(RawRectangle);
				return;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00019EC4 File Offset: 0x000180C4
		private void SetDirtyVert([Out] Span<VERTEX> Vertices, in RawRectangle Dirty, int OffsetX, int OffsetY, in OutputDescription DeskDesc, in Texture2DDescription FullDesc, in Texture2DDescription ThisDesc)
		{
			int CenterX = FullDesc.Width / 2;
			int CenterY = FullDesc.Height / 2;
			int Width = DeskDesc.DesktopBounds.Right - DeskDesc.DesktopBounds.Left;
			int Height = DeskDesc.DesktopBounds.Bottom - DeskDesc.DesktopBounds.Top;
			RawRectangle DestDirty = Dirty;
			switch (DeskDesc.Rotation)
			{
			case 2:
				DestDirty.Left = Width - Dirty.Bottom;
				DestDirty.Top = Dirty.Left;
				DestDirty.Right = Width - Dirty.Top;
				DestDirty.Bottom = Dirty.Right;
				Vertices[0].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
				Vertices[1].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
				Vertices[2].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
				Vertices[5].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
				goto IL_454;
			case 3:
				DestDirty.Left = Width - Dirty.Right;
				DestDirty.Top = Height - Dirty.Bottom;
				DestDirty.Right = Width - Dirty.Left;
				DestDirty.Bottom = Height - Dirty.Top;
				Vertices[0].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
				Vertices[1].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
				Vertices[2].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
				Vertices[5].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
				goto IL_454;
			case 4:
				DestDirty.Left = Dirty.Top;
				DestDirty.Top = Height - Dirty.Right;
				DestDirty.Right = Dirty.Bottom;
				DestDirty.Bottom = Height - Dirty.Left;
				Vertices[0].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
				Vertices[1].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
				Vertices[2].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
				Vertices[5].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
				goto IL_454;
			}
			Vertices[0].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
			Vertices[1].TexCoord = new Vector2((float)Dirty.Left / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
			Vertices[2].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Bottom / (float)ThisDesc.Height);
			Vertices[5].TexCoord = new Vector2((float)Dirty.Right / (float)ThisDesc.Width, (float)Dirty.Top / (float)ThisDesc.Height);
			IL_454:
			Vertices[0].Pos = new Vector3((float)(DestDirty.Left + DeskDesc.DesktopBounds.Left - OffsetX - CenterX) / (float)CenterX, (float)(-1 * (DestDirty.Bottom + DeskDesc.DesktopBounds.Top - OffsetY - CenterY)) / (float)CenterY, 0f);
			Vertices[1].Pos = new Vector3((float)(DestDirty.Left + DeskDesc.DesktopBounds.Left - OffsetX - CenterX) / (float)CenterX, (float)(-1 * (DestDirty.Top + DeskDesc.DesktopBounds.Top - OffsetY - CenterY)) / (float)CenterY, 0f);
			Vertices[2].Pos = new Vector3((float)(DestDirty.Right + DeskDesc.DesktopBounds.Left - OffsetX - CenterX) / (float)CenterX, (float)(-1 * (DestDirty.Bottom + DeskDesc.DesktopBounds.Top - OffsetY - CenterY)) / (float)CenterY, 0f);
			Vertices[3].Pos = Vertices[2].Pos;
			Vertices[4].Pos = Vertices[1].Pos;
			Vertices[5].Pos = new Vector3((float)(DestDirty.Right + DeskDesc.DesktopBounds.Left - OffsetX - CenterX) / (float)CenterX, (float)(-1 * (DestDirty.Top + DeskDesc.DesktopBounds.Top - OffsetY - CenterY)) / (float)CenterY, 0f);
			Vertices[3].TexCoord = Vertices[2].TexCoord;
			Vertices[4].TexCoord = Vertices[1].TexCoord;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001A4D8 File Offset: 0x000186D8
		private DUPL_RETURN CopyMove(Device device, [In] [Out] Texture2D SharedSurf, [In] OutputDuplicateMoveRectangle[] MoveBuffer, int MoveCount, int OffsetX, int OffsetY, in OutputDescription DeskDesc, int TexWidth, int TexHeight)
		{
			Texture2DDescription FullDesc = SharedSurf.Description;
			if (this.m_MoveSurf == null)
			{
				Texture2DDescription MoveDesc = FullDesc;
				MoveDesc.Width = DeskDesc.DesktopBounds.Right - DeskDesc.DesktopBounds.Left;
				MoveDesc.Height = DeskDesc.DesktopBounds.Bottom - DeskDesc.DesktopBounds.Top;
				MoveDesc.BindFlags = 32;
				MoveDesc.OptionFlags = 0;
				this.m_MoveSurf = new Texture2D(device, MoveDesc);
			}
			uint i = 0U;
			while ((ulong)i < (ulong)((long)MoveCount))
			{
				RawRectangle SrcRect;
				RawRectangle DestRect;
				this.SetMoveRect(out SrcRect, out DestRect, DeskDesc, MoveBuffer[(int)i], TexWidth, TexHeight);
				ResourceRegion Box;
				Box.Left = SrcRect.Left + DeskDesc.DesktopBounds.Left - OffsetX;
				Box.Top = SrcRect.Top + DeskDesc.DesktopBounds.Top - OffsetY;
				Box.Front = 0;
				Box.Right = SrcRect.Right + DeskDesc.DesktopBounds.Left - OffsetX;
				Box.Bottom = SrcRect.Bottom + DeskDesc.DesktopBounds.Top - OffsetY;
				Box.Back = 1;
				device.ImmediateContext.CopySubresourceRegion(SharedSurf, 0, new ResourceRegion?(Box), this.m_MoveSurf, 0, SrcRect.Left, SrcRect.Top, 0);
				Box.Left = SrcRect.Left;
				Box.Top = SrcRect.Top;
				Box.Front = 0;
				Box.Right = SrcRect.Right;
				Box.Bottom = SrcRect.Bottom;
				Box.Back = 1;
				device.ImmediateContext.CopySubresourceRegion(this.m_MoveSurf, 0, new ResourceRegion?(Box), SharedSurf, 0, DestRect.Left + DeskDesc.DesktopBounds.Left - OffsetX, DestRect.Top + DeskDesc.DesktopBounds.Top - OffsetY, 0);
				i += 1U;
			}
			return DUPL_RETURN.DUPL_RETURN_SUCCESS;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001A6BC File Offset: 0x000188BC
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

		// Token: 0x0600034A RID: 842 RVA: 0x0001A788 File Offset: 0x00018988
		private void CreateShadersAndInputLayout(Device device)
		{
			this.inputSignature = ShaderSignature.GetInputSignature(ShaderData.g_Texture_VS);
			this.vertexShader = new VertexShader(device, ShaderData.g_Texture_VS, null);
			this.pixelShader = new PixelShader(device, ShaderData.g_normalPS, null);
			this.inputLayout = new InputLayout(device, this.inputSignature, this.inputElements);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001A7E8 File Offset: 0x000189E8
		private void CreateVertexBuffer(Device device, DisplayModeRotation rotation)
		{
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
			float[] _vertices = this.vertices;
			switch (rotation)
			{
			case 2:
				_vertices[3] = 1f;
				_vertices[4] = 0f;
				_vertices[8] = 1f;
				_vertices[9] = 1f;
				_vertices[13] = 0f;
				_vertices[14] = 0f;
				_vertices[18] = 0f;
				_vertices[19] = 1f;
				break;
			case 3:
				_vertices[3] = 1f;
				_vertices[4] = 1f;
				_vertices[8] = 0f;
				_vertices[9] = 1f;
				_vertices[13] = 1f;
				_vertices[14] = 0f;
				_vertices[18] = 0f;
				_vertices[19] = 0f;
				break;
			case 4:
				_vertices[3] = 0f;
				_vertices[4] = 1f;
				_vertices[8] = 0f;
				_vertices[9] = 0f;
				_vertices[13] = 1f;
				_vertices[14] = 1f;
				_vertices[18] = 1f;
				_vertices[19] = 0f;
				break;
			}
			this.vertexBuffer = Buffer.Create<float>(device, 1, _vertices, 0, 0, 0, 0, 0);
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

		// Token: 0x0600034C RID: 844 RVA: 0x0001A984 File Offset: 0x00018B84
		private void CreateSamplerState(Device device)
		{
			SamplerStateDescription description = SamplerStateDescription.Default();
			this.m_sampleState = new SamplerState(device, description);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x040002C4 RID: 708
		public const int NUMVERTICES = 6;

		// Token: 0x040002C5 RID: 709
		private VertexShader vertexShader;

		// Token: 0x040002C6 RID: 710
		private PixelShader pixelShader;

		// Token: 0x040002C7 RID: 711
		private ShaderSignature inputSignature;

		// Token: 0x040002C8 RID: 712
		private InputLayout inputLayout;

		// Token: 0x040002C9 RID: 713
		private VERTEX[] m_DirtyVertexBufferAlloc;

		// Token: 0x040002CA RID: 714
		private RenderTargetView m_RTV;

		// Token: 0x040002CB RID: 715
		private Texture2D m_MoveSurf;

		// Token: 0x040002CC RID: 716
		private SamplerState m_sampleState;

		// Token: 0x040002CD RID: 717
		private readonly InputElement[] inputElements = new InputElement[]
		{
			new InputElement("myPOSITION", 0, 6, 0, 0, 0, 0),
			new InputElement("myTEXCOORD", 0, 16, 12, 0, 0, 0)
		};

		// Token: 0x040002CE RID: 718
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

		// Token: 0x040002CF RID: 719
		private Buffer vertexBuffer;

		// Token: 0x040002D0 RID: 720
		private Buffer indexBuffer;

		// Token: 0x040002D1 RID: 721
		private int m_indicesCount;

		// Token: 0x040002D2 RID: 722
		private VertexBufferBinding vertexBufferBinding;

		// Token: 0x040002D3 RID: 723
		private RawColor4 clearColor;
	}
}
