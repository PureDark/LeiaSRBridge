using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SRUnity
{
	// Token: 0x0200010F RID: 271
	public class SRRender : SimulatedRealityModule<SRRender>
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0001FB1F File Offset: 0x0001DD1F
		public override void InitModule()
		{
			SRCore.OnContextChanged += this.OnContextChanged;
			SystemHandler.OnSceneChanged += this.OnSceneChanged;
			SystemHandler.OnWindowFocus += this.OnFocusChanged;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public override void UpdateModule()
		{
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfj());
			if (SystemInfo.graphicsDeviceType == 2 && Application.isPlaying && !this.initialLateLatchingCheck && this.weaver.CanWeave())
			{
				this.SetLateLatchingDX11();
				this.initialLateLatchingCheck = true;
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001FB91 File Offset: 0x0001DD91
		public override void DestroyModule()
		{
			SRCore.OnContextChanged -= this.OnContextChanged;
			SystemHandler.OnSceneChanged -= this.OnSceneChanged;
			SystemHandler.OnWindowFocus -= this.OnFocusChanged;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001FBC6 File Offset: 0x0001DDC6
		public void OnContextChanged(SRContextChangeReason contextChangeReason)
		{
			if (SRCore.IsSimulatedRealityAvailable() && SimulatedRealityModule<SRCore>.Instance.GetSrContext() != IntPtr.Zero)
			{
				this.compositor.Init();
				this.weaver.Init();
				this.SetResolution();
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001FC01 File Offset: 0x0001DE01
		public void OnFocusChanged(bool hasFocus)
		{
			if (hasFocus)
			{
				SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfk());
				this.SetResolution();
				return;
			}
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfl());
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001FC24 File Offset: 0x0001DE24
		private void SetResolution()
		{
			if (SRCore.IsSimulatedRealityAvailable())
			{
				Vector2Int resolution = SimulatedRealityModule<SRCore>.Instance.getResolution();
				if (resolution.x > Vector2Int.one.x || resolution.y > Vector2Int.one.y)
				{
					SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfm() + resolution.x.ToString() + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfn() + resolution.y.ToString());
					Screen.SetResolution(resolution.x, resolution.y, 1);
				}
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		public SRCompositor GetCompositor()
		{
			return this.compositor;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		public SRWeaver GetWeaver()
		{
			return this.weaver;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001FCC8 File Offset: 0x0001DEC8
		public void SetLatency(int latency)
		{
			this.weaver.PredictingWeaverSetLatency(latency);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001FCD6 File Offset: 0x0001DED6
		public void SetLatencyInFrames(int latency)
		{
			this.weaver.PredictingWeaverSetLatencyInFrames(latency);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		private void OnSceneChanged(Scene scene)
		{
			SRUtility.Trace(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfo(), scene.name));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		private void SetLateLatchingDX11()
		{
			if (this.weaver == null)
			{
				LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfp(), Array.Empty<object>());
				return;
			}
			if (this.weaver.CanWeave())
			{
				this.weaver.EnableLateLatchingDX11(Singleton<RenderTrackingDevice>.Instance.GetLateLatchingFromLeiaDisplay());
				return;
			}
			LogUtil.Log(LogLevel.Warning, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfq(), Array.Empty<object>());
		}

		// Token: 0x040005E4 RID: 1508
		private readonly SRCompositor compositor = new SRCompositor();

		// Token: 0x040005E5 RID: 1509
		private readonly SRWeaver weaver = new SRWeaver();

		// Token: 0x040005E6 RID: 1510
		private bool initialLateLatchingCheck;
	}
}
