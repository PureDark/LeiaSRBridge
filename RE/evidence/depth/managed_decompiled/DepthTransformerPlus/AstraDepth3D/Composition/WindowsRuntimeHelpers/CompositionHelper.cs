using System;
using System.Runtime.InteropServices;
using SharpDX.DXGI;
using Windows.UI.Composition;
using Windows.UI.Composition.Desktop;

namespace AstraDepth3D.Composition.WindowsRuntimeHelpers
{
	// Token: 0x0200001A RID: 26
	public static class CompositionHelper
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000AEF1 File Offset: 0x000090F1
		public static CompositionTarget CreateDesktopWindowTarget(this Compositor compositor, IntPtr hwnd, bool isTopmost)
		{
			return ((CompositionHelper.ICompositorDesktopInterop)compositor).CreateDesktopWindowTarget(hwnd, isTopmost);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000AF00 File Offset: 0x00009100
		public static ICompositionSurface CreateCompositionSurfaceForSwapChain(this Compositor compositor, SwapChain1 swapChain)
		{
			return ((CompositionHelper.ICompositorInterop)compositor).CreateCompositionSurfaceForSwapChain(swapChain.NativePointer);
		}

		// Token: 0x0200008A RID: 138
		[Guid("25297D5C-3AD4-4C9C-B5CF-E36A38512330")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[ComImport]
		private interface ICompositorInterop
		{
			// Token: 0x06000411 RID: 1041
			ICompositionSurface CreateCompositionSurfaceForHandle(IntPtr swapChain);

			// Token: 0x06000412 RID: 1042
			ICompositionSurface CreateCompositionSurfaceForSwapChain(IntPtr swapChain);

			// Token: 0x06000413 RID: 1043
			CompositionGraphicsDevice CreateGraphicsDevice(IntPtr renderingDevice);
		}

		// Token: 0x0200008B RID: 139
		[Guid("29E691FA-4567-4DCA-B319-D0F207EB6807")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[ComImport]
		private interface ICompositorDesktopInterop
		{
			// Token: 0x06000414 RID: 1044
			DesktopWindowTarget CreateDesktopWindowTarget(IntPtr hwnd, bool isTopmost);
		}
	}
}
