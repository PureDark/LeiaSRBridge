using System;
using System.Runtime.InteropServices;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using UnityEngine;
using UnityEngine.Rendering;

namespace SRUnity
{
	// Token: 0x02000110 RID: 272
	public class SRWeaver
	{
		// Token: 0x06000746 RID: 1862
		[DllImport("SRUnityNative")]
		private static extern IntPtr GetNativeGraphicsEvent();

		// Token: 0x06000747 RID: 1863
		[DllImport("SRUnityNative")]
		private static extern void SetWeaverContextPtr(IntPtr context);

		// Token: 0x06000748 RID: 1864
		[DllImport("SRUnityNative")]
		private static extern void SetWeaverResourcePtr(IntPtr src, IntPtr dst);

		// Token: 0x06000749 RID: 1865
		[DllImport("SRUnityNative")]
		private static extern void SetWeaverOutputResolution(int Width, int Height);

		// Token: 0x0600074A RID: 1866
		[DllImport("SRUnityNative")]
		private static extern bool GetWeaverEnabled();

		// Token: 0x0600074B RID: 1867
		[DllImport("SRUnityNative")]
		private static extern void EnableLateLatching(bool enable);

		// Token: 0x0600074C RID: 1868
		[DllImport("SRUnityNative")]
		private static extern bool IsLateLatchingEnabled();

		// Token: 0x0600074D RID: 1869
		[DllImport("SRUnityNative")]
		private static extern bool SetLatency(ulong latency);

		// Token: 0x0600074E RID: 1870
		[DllImport("SRUnityNative")]
		private static extern ulong GetLatency();

		// Token: 0x0600074F RID: 1871
		[DllImport("SRUnityNative")]
		private static extern bool SetLatencyInFrames(ulong latency);

		// Token: 0x06000750 RID: 1872
		[DllImport("SRUnityNative")]
		private static extern void SetWeaverRenderTextureFormat(int format);

		// Token: 0x06000751 RID: 1873
		[DllImport("SRUnityNative")]
		private static extern void DestroyWeaver();

		// Token: 0x06000752 RID: 1874
		[DllImport("SRUnityNative", CallingConvention = CallingConvention.StdCall)]
		private static extern void SetUnityWindowHandle(IntPtr hwnd);

		// Token: 0x06000753 RID: 1875
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000754 RID: 1876
		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();

		// Token: 0x06000755 RID: 1877
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x06000756 RID: 1878
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentProcessId();

		// Token: 0x06000757 RID: 1879
		[DllImport("user32.dll")]
		private static extern bool EnumWindows(SRWeaver.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x06000758 RID: 1880
		[DllImport("SRUnityNative")]
		private static extern void SetACTMode(int mode);

		// Token: 0x06000759 RID: 1881
		[DllImport("SRUnityNative")]
		private static extern int GetACTMode();

		// Token: 0x0600075A RID: 1882
		[DllImport("SRUnityNative")]
		private static extern void SetCrosstalkStaticFactor(float factor);

		// Token: 0x0600075B RID: 1883
		[DllImport("SRUnityNative")]
		private static extern float GetCrosstalkStaticFactor();

		// Token: 0x0600075C RID: 1884
		[DllImport("SRUnityNative")]
		private static extern void SetCrosstalkDynamicFactor(float factor);

		// Token: 0x0600075D RID: 1885
		[DllImport("SRUnityNative")]
		private static extern float GetCrosstalkDynamicFactor();

		// Token: 0x0600075E RID: 1886 RVA: 0x0001FD74 File Offset: 0x0001DF74
		public void Init()
		{
			SRWeaver.SetWeaverRenderTextureFormat(this.UnityToDXGIFormat(SRProjectSettings.dce.dcc));
			this.UpdateWeavingData(null, null);
			IntPtr unityWindowHandle = this.GetUnityWindowHandle();
			if (unityWindowHandle != IntPtr.Zero)
			{
				Debug.Log(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfr() + unityWindowHandle.ToString());
				SRWeaver.SetUnityWindowHandle(unityWindowHandle);
				return;
			}
			Debug.LogError(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfs());
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001FDDC File Offset: 0x0001DFDC
		public void Destroy()
		{
			try
			{
				SRWeaver.DestroyWeaver();
				LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bft(), Array.Empty<object>());
			}
			catch (Exception ex)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfu() + ex.Message, Array.Empty<object>());
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001FE30 File Offset: 0x0001E030
		public bool CanWeave()
		{
			return SRWeaver.GetWeaverEnabled();
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001FE38 File Offset: 0x0001E038
		public void WeaveToContext(ScriptableRenderContext context, Texture frameBuffer)
		{
			this.UpdateWeavingData(frameBuffer, null);
			CommandBuffer commandBuffer = new CommandBuffer();
			commandBuffer.name = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfv();
			commandBuffer.SetRenderTarget(2);
			commandBuffer.IssuePluginEvent(SRWeaver.GetNativeGraphicsEvent(), 1);
			context.ExecuteCommandBuffer(commandBuffer);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001FE7E File Offset: 0x0001E07E
		public void WeaveToTarget(RenderTexture target, RenderTexture frameBuffer, bool clearFramebuffer)
		{
			this.UpdateWeavingData(frameBuffer, target);
			RenderTexture.active = target;
			GL.IssuePluginEvent(SRWeaver.GetNativeGraphicsEvent(), 1);
			if (clearFramebuffer)
			{
				RenderTexture.active = frameBuffer;
				GL.Clear(true, true, Color.clear);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
		private void UpdateWeavingData(Texture frameBuffer, Texture target)
		{
			SRWeaver.SetWeaverContextPtr(SimulatedRealityModule<SRCore>.Instance.GetSrContext());
			IntPtr src = (frameBuffer != null) ? frameBuffer.GetNativeTexturePtr() : IntPtr.Zero;
			IntPtr dst = (target != null) ? target.GetNativeTexturePtr() : IntPtr.Zero;
			SRWeaver.SetWeaverResourcePtr(src, dst);
			SRWeaver.SetWeaverOutputResolution(Screen.width, Screen.height);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001FF0E File Offset: 0x0001E10E
		private int UnityToDXGIFormat(RenderTextureFormat format)
		{
			if (format == null)
			{
				return 28;
			}
			if (format == 2)
			{
				return 10;
			}
			if (format != 11)
			{
				LogUtil.Log(LogLevel.Warning, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfw(), format), Array.Empty<object>());
				return 28;
			}
			return 2;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001FF43 File Offset: 0x0001E143
		public void EnableLateLatchingDX11(bool enable)
		{
			SRWeaver.EnableLateLatching(enable);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001FF4B File Offset: 0x0001E14B
		public bool IsLateLatchingEnabledDX11()
		{
			return SRWeaver.IsLateLatchingEnabled();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001FF52 File Offset: 0x0001E152
		public void PredictingWeaverSetLatency(int latency)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfx() + latency.ToString(), Array.Empty<object>());
			if (latency < 0)
			{
				throw new ArgumentOutOfRangeException(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfy(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfz());
			}
			SRWeaver.SetLatency((ulong)((long)latency));
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public void PredictingWeaverSetLatencyInFrames(int latency)
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bga() + latency.ToString(), Array.Empty<object>());
			if (latency < 0)
			{
				throw new ArgumentOutOfRangeException(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfy(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfz());
			}
			SRWeaver.SetLatencyInFrames((ulong)((long)latency));
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		private IntPtr GetUnityWindowHandle()
		{
			IntPtr intPtr = IntPtr.Zero;
			uint currentProcessId = SRWeaver.GetCurrentProcessId();
			intPtr = this.GetWindowHandleByProcessId(currentProcessId);
			if (intPtr != IntPtr.Zero)
			{
				return intPtr;
			}
			string productName = Application.productName;
			intPtr = SRWeaver.FindWindow(null, productName);
			if (intPtr != IntPtr.Zero)
			{
				return intPtr;
			}
			intPtr = SRWeaver.GetActiveWindow();
			if (intPtr != IntPtr.Zero)
			{
				return intPtr;
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002002F File Offset: 0x0001E22F
		private IntPtr GetWindowHandleByProcessId(uint processId)
		{
			IntPtr foundHwnd = IntPtr.Zero;
			SRWeaver.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				uint num;
				SRWeaver.GetWindowThreadProcessId(hWnd, out num);
				if (num == processId)
				{
					foundHwnd = hWnd;
					return false;
				}
				return true;
			}, IntPtr.Zero);
			return foundHwnd;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00020064 File Offset: 0x0001E264
		public static void SetActMode(int mode)
		{
			try
			{
				LogUtil.Log(LogLevel.Debug, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgb(), mode), Array.Empty<object>());
				SRWeaver.SetACTMode(mode);
			}
			catch (Exception ex)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgc() + ex.Message, Array.Empty<object>());
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000200C4 File Offset: 0x0001E2C4
		public static int GetActMode()
		{
			int result;
			try
			{
				int actmode = SRWeaver.GetACTMode();
				LogUtil.Log(LogLevel.Debug, string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgd(), actmode), Array.Empty<object>());
				result = actmode;
			}
			catch (Exception ex)
			{
				LogUtil.Log(LogLevel.Error, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bge() + ex.Message, Array.Empty<object>());
				result = 0;
			}
			return result;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00020128 File Offset: 0x0001E328
		public static void setCrosstalkFactor(float factor)
		{
			SRWeaver.SetCrosstalkStaticFactor(factor);
			Debug.Log(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgf(), factor));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00020145 File Offset: 0x0001E345
		public static float getCrosstalkFactor()
		{
			return SRWeaver.GetCrosstalkStaticFactor();
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002014C File Offset: 0x0001E34C
		public static void setCrosstalkAngularFactor(float factor)
		{
			SRWeaver.SetCrosstalkDynamicFactor(factor);
			Debug.Log(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgg(), factor));
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00020169 File Offset: 0x0001E369
		public static float getCrosstalkAngularFactor()
		{
			return SRWeaver.GetCrosstalkDynamicFactor();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00020170 File Offset: 0x0001E370
		public static float getLatency()
		{
			return SRWeaver.GetLatency();
		}

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x06000774 RID: 1908
		private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		// Token: 0x02000112 RID: 274
		private static class DXGI
		{
			// Token: 0x040005E7 RID: 1511
			public const int DXGI_FORMAT_R8G8B8A8_UNORM = 28;

			// Token: 0x040005E8 RID: 1512
			public const int DXGI_FORMAT_R16G16B16A16_FLOAT = 10;

			// Token: 0x040005E9 RID: 1513
			public const int DXGI_FORMAT_R32G32B32A32_FLOAT = 2;
		}
	}
}
