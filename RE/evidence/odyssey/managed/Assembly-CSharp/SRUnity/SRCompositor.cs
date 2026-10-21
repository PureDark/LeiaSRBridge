using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Rendering;

namespace SRUnity
{
	// Token: 0x0200010B RID: 267
	public class SRCompositor
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000724 RID: 1828 RVA: 0x0001F280 File Offset: 0x0001D480
		// (remove) Token: 0x06000725 RID: 1829 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		public static event SRCompositor.OnCompositorChangedDelegate OnCompositorChanged;

		// Token: 0x06000726 RID: 1830 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		public void Init()
		{
			string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bff();
			GameObject gameObject = GameObject.Find(text);
			if (gameObject == null)
			{
				gameObject = new GameObject();
			}
			gameObject.transform.parent = SystemHandler.Instance.transform;
			gameObject.name = text;
			if (gameObject.GetComponent<CompositorCameraHook>() == null)
			{
				gameObject.AddComponent<CompositorCameraHook>();
			}
			this.cameraHook = gameObject.GetComponent<Camera>();
			if (this.cameraHook == null)
			{
				this.cameraHook = gameObject.AddComponent<Camera>();
			}
			this.cameraHook.depth = 9999f;
			this.cameraHook.clearFlags = 4;
			this.cameraHook.nearClipPlane = 1E-05f;
			this.cameraHook.farClipPlane = 2E-05f;
			SRUtility.SetSrGameObjectVisibility(gameObject);
			foreach (KeyValuePair<string, SRCompositor.SRFrameBuffer> keyValuePair in this.compositionFrameBuffers)
			{
				keyValuePair.Value.Update();
			}
			SRCompositor.OnCompositorChangedDelegate onCompositorChanged = SRCompositor.OnCompositorChanged;
			if (onCompositorChanged != null)
			{
				onCompositorChanged();
			}
			RenderPipelineManager.endFrameRendering -= this.renderToContext;
			RenderPipelineManager.endFrameRendering += this.renderToContext;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001F428 File Offset: 0x0001D628
		public void Destroy()
		{
			RenderPipelineManager.endFrameRendering -= this.renderToContext;
			SRCompositor.OnCompositorChangedDelegate onCompositorChanged = SRCompositor.OnCompositorChanged;
			if (onCompositorChanged == null)
			{
				return;
			}
			onCompositorChanged();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001F44C File Offset: 0x0001D64C
		public void Composite()
		{
			this.Composite(null);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001F468 File Offset: 0x0001D668
		private void UpdateFrameBuffer()
		{
			Vector2Int resolution = SimulatedRealityModule<SRCore>.Instance.getResolution();
			Vector2Int vector2Int;
			vector2Int..ctor((int)((float)resolution.x * SRProjectSettings.dce.dcd), (int)((float)resolution.y * SRProjectSettings.dce.dcd / 2f));
			if (this.compositionTexture != null && (this.compositionTexture.width != vector2Int.x || this.compositionTexture.height != vector2Int.y))
			{
				this.compositionTexture = null;
				this.weavingTargetTexture = null;
			}
			if (this.compositionTexture == null)
			{
				RenderTextureDescriptor renderTextureDescriptor;
				renderTextureDescriptor..ctor(vector2Int.x, vector2Int.y, SRProjectSettings.dce.dcc);
				this.compositionTexture = new RenderTexture(renderTextureDescriptor);
				this.compositionTexture.name = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfg();
				this.compositionTextureId = new RenderTargetIdentifier(this.compositionTexture);
				this.weavingTargetTexture = new RenderTexture(renderTextureDescriptor);
				this.weavingTargetTexture.name = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfh();
				this.weavingTargetTextureId = new RenderTargetIdentifier(this.weavingTargetTexture);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001F580 File Offset: 0x0001D780
		public void Composite(ScriptableRenderContext? context)
		{
			this.UpdateFrameBuffer();
			RenderTexture renderTexture = this.compositionTexture;
			SRProjectSettings.dce.dcd;
			Vector2Int vector2Int;
			vector2Int..ctor((int)((float)renderTexture.width / 2f), renderTexture.height);
			CommandBuffer commandBuffer = null;
			RenderTargetIdentifier renderTargetIdentifier = 0;
			if (context != null)
			{
				commandBuffer = new CommandBuffer();
				commandBuffer.name = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfi();
				renderTargetIdentifier = this.compositionTextureId;
				commandBuffer.SetRenderTarget(renderTargetIdentifier);
				commandBuffer.ClearRenderTarget(true, true, Color.clear);
			}
			else
			{
				RenderTexture.active = renderTexture;
				GL.Clear(true, true, Color.clear);
			}
			foreach (KeyValuePair<string, SRCompositor.SRFrameBuffer> keyValuePair in this.compositionFrameBuffers)
			{
				SRCompositor.SRFrameBuffer value = keyValuePair.Value;
				if (value.Enabled && !(value.frameBuffer == null) && value.screenRect.width * value.screenRect.height != 0f)
				{
					Vector2Int vector2Int2;
					vector2Int2..ctor((int)(value.screenRect.x * (float)vector2Int.x), (int)(value.screenRect.y * (float)vector2Int.y));
					Vector2Int vector2Int3 = vector2Int2 + new Vector2Int((int)(value.screenRect.width * (float)vector2Int.x), (int)(value.screenRect.height * (float)vector2Int.y));
					vector2Int3.x = Math.Min(vector2Int3.x, vector2Int.x);
					vector2Int3.y = Math.Min(vector2Int3.y, vector2Int.y);
					Vector2Int vector2Int4;
					vector2Int4..ctor(Math.Max(0, -vector2Int2.x), Math.Max(0, -vector2Int2.y));
					vector2Int2 += vector2Int4;
					if (vector2Int3.x - vector2Int2.x >= 2 && vector2Int3.y - vector2Int2.y >= 2)
					{
						if (value.viewIndex == 1)
						{
							vector2Int2.x += vector2Int.x;
							vector2Int3.x += vector2Int.x;
						}
						if (context != null)
						{
							commandBuffer.CopyTexture(value.frameBufferId, 0, 0, vector2Int4.x, vector2Int4.y, vector2Int3.x - vector2Int2.x, vector2Int3.y - vector2Int2.y, renderTargetIdentifier, 0, 0, vector2Int2.x, vector2Int2.y);
						}
						else
						{
							Graphics.CopyTexture(value.frameBuffer, 0, 0, vector2Int4.x, vector2Int4.y, vector2Int3.x - vector2Int2.x, vector2Int3.y - vector2Int2.y, renderTexture, 0, 0, vector2Int2.x, vector2Int2.y);
						}
					}
				}
			}
			if (context != null)
			{
				context.Value.ExecuteCommandBuffer(commandBuffer);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001F898 File Offset: 0x0001DA98
		public SRCompositor.SRFrameBuffer GetFrameBuffer(string uniqueId)
		{
			if (!this.compositionFrameBuffers.ContainsKey(uniqueId))
			{
				this.compositionFrameBuffers.Add(uniqueId, new SRCompositor.SRFrameBuffer());
			}
			return this.compositionFrameBuffers[uniqueId];
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001F8C8 File Offset: 0x0001DAC8
		private void renderToContext(ScriptableRenderContext context, Camera[] cameras)
		{
			bool flag = false;
			for (int i = 0; i < cameras.Length; i++)
			{
				if (cameras[i] == this.cameraHook)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			this.Composite(new ScriptableRenderContext?(context));
			SimulatedRealityModule<SRRender>.Instance.GetWeaver().WeaveToContext(context, this.compositionTexture);
			context.Submit();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001F928 File Offset: 0x0001DB28
		public void renderToTarget(RenderTexture target)
		{
			this.Composite();
			SimulatedRealityModule<SRRender>.Instance.GetWeaver().WeaveToTarget(target, this.compositionTexture, true);
			RenderTexture.active = target;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001F950 File Offset: 0x0001DB50
		public void renderToTargetDX12(RenderTexture Source, RenderTexture Target)
		{
			this.Composite();
			SimulatedRealityModule<SRRender>.Instance.GetWeaver().WeaveToTarget(this.weavingTargetTexture, this.compositionTexture, false);
			RenderTexture.active = Target;
			Graphics.Blit(this.weavingTargetTexture, Target, new Vector2(1f, -1f), new Vector2(0f, 1f));
		}

		// Token: 0x040005D9 RID: 1497
		private RenderTexture compositionTexture;

		// Token: 0x040005DA RID: 1498
		private RenderTargetIdentifier compositionTextureId;

		// Token: 0x040005DB RID: 1499
		private Dictionary<string, SRCompositor.SRFrameBuffer> compositionFrameBuffers = new Dictionary<string, SRCompositor.SRFrameBuffer>();

		// Token: 0x040005DC RID: 1500
		private RenderTexture weavingTargetTexture;

		// Token: 0x040005DD RID: 1501
		private RenderTargetIdentifier weavingTargetTextureId;

		// Token: 0x040005DE RID: 1502
		private Camera cameraHook;

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x06000731 RID: 1841
		public delegate void OnCompositorChangedDelegate();

		// Token: 0x0200010D RID: 269
		public class SRFrameBuffer
		{
			// Token: 0x06000734 RID: 1844 RVA: 0x0001F9C4 File Offset: 0x0001DBC4
			public void Update()
			{
				if (this.Enabled)
				{
					Vector2Int viewSize = this.GetViewSize();
					if (this.frameBuffer != null && (this.frameBuffer.width != viewSize.x || this.frameBuffer.height != viewSize.y))
					{
						this.frameBuffer = null;
					}
					if (this.frameBuffer == null)
					{
						if (viewSize.x > 0 && viewSize.y > 0)
						{
							RenderTextureDescriptor renderTextureDescriptor;
							renderTextureDescriptor..ctor(viewSize.x, viewSize.y, SRProjectSettings.dce.dcc);
							renderTextureDescriptor.depthBufferBits = 24;
							renderTextureDescriptor.mipCount = 0;
							this.frameBuffer = new RenderTexture(renderTextureDescriptor);
						}
						this.frameBufferId = new RenderTargetIdentifier(this.frameBuffer);
					}
				}
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x0001FA90 File Offset: 0x0001DC90
			public Vector2Int GetViewSize()
			{
				Vector2Int resolution = SimulatedRealityModule<SRCore>.Instance.getResolution();
				float num = SRProjectSettings.dce.dcd;
				return new Vector2Int((int)((float)resolution.x * this.screenRect.width / 2f * num), (int)((float)resolution.y * this.screenRect.height / 2f * num));
			}

			// Token: 0x040005DF RID: 1503
			public RenderTexture frameBuffer;

			// Token: 0x040005E0 RID: 1504
			public int viewIndex;

			// Token: 0x040005E1 RID: 1505
			public Rect screenRect;

			// Token: 0x040005E2 RID: 1506
			public bool Enabled;

			// Token: 0x040005E3 RID: 1507
			public RenderTargetIdentifier frameBufferId;
		}
	}
}
