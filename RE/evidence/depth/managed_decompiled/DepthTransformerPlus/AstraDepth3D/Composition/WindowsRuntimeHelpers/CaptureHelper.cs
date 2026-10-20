using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics;
using Windows.Graphics.Capture;

namespace AstraDepth3D.Composition.WindowsRuntimeHelpers
{
	// Token: 0x02000019 RID: 25
	public static class CaptureHelper
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0000AE24 File Offset: 0x00009024
		public static void SetWindow(this GraphicsCapturePicker picker, IntPtr hwnd)
		{
			((CaptureHelper.IInitializeWithWindow)picker).Initialize(hwnd);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AE34 File Offset: 0x00009034
		public static GraphicsCaptureItem CreateItemForWindow(IntPtr hwnd)
		{
			CaptureHelper.IGraphicsCaptureItemInterop graphicsCaptureItemInterop = (CaptureHelper.IGraphicsCaptureItemInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
			Guid graphicsCaptureItemGuid = CaptureHelper.GraphicsCaptureItemGuid;
			IntPtr itemPointer = graphicsCaptureItemInterop.CreateForWindow(hwnd, ref graphicsCaptureItemGuid);
			GraphicsCaptureItem result = Marshal.GetObjectForIUnknown(itemPointer) as GraphicsCaptureItem;
			Marshal.Release(itemPointer);
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AE78 File Offset: 0x00009078
		public static GraphicsCaptureItem CreateItemForMonitor(IntPtr hmon)
		{
			CaptureHelper.IGraphicsCaptureItemInterop graphicsCaptureItemInterop = (CaptureHelper.IGraphicsCaptureItemInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
			Guid graphicsCaptureItemGuid = CaptureHelper.GraphicsCaptureItemGuid;
			IntPtr itemPointer = graphicsCaptureItemInterop.CreateForMonitor(hmon, ref graphicsCaptureItemGuid);
			GraphicsCaptureItem result = Marshal.GetObjectForIUnknown(itemPointer) as GraphicsCaptureItem;
			Marshal.Release(itemPointer);
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000AEBC File Offset: 0x000090BC
		public static GraphicsCaptureItem CreateItemForMonitor2(IntPtr hmon)
		{
			DisplayId displayId = default(DisplayId);
			displayId.Value = 0UL;
			return GraphicsCaptureItem.TryCreateFromDisplayId(displayId);
		}

		// Token: 0x0400008F RID: 143
		private static readonly Guid GraphicsCaptureItemGuid = new Guid("79C3F95B-31F7-4EC2-A464-632EF5D30760");

		// Token: 0x02000088 RID: 136
		[Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[ComImport]
		private interface IInitializeWithWindow
		{
			// Token: 0x0600040E RID: 1038
			void Initialize(IntPtr hwnd);
		}

		// Token: 0x02000089 RID: 137
		[Guid("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[ComImport]
		private interface IGraphicsCaptureItemInterop
		{
			// Token: 0x0600040F RID: 1039
			IntPtr CreateForWindow([In] IntPtr window, [In] ref Guid iid);

			// Token: 0x06000410 RID: 1040
			IntPtr CreateForMonitor([In] IntPtr monitor, [In] ref Guid iid);
		}
	}
}
