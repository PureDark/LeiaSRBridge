using System;
using System.Runtime.InteropServices;
using AstraDepth3D.WindowHelper;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Windows.Graphics.DirectX.Direct3D11;

namespace AstraDepth3D.Composition.WindowsRuntimeHelpers
{
	// Token: 0x0200001C RID: 28
	public static class Direct3D11Helper
	{
		// Token: 0x06000155 RID: 341
		[DllImport("d3d11.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern uint CreateDirect3D11DeviceFromDXGIDevice(IntPtr dxgiDevice, out IntPtr graphicsDevice);

		// Token: 0x06000156 RID: 342
		[DllImport("d3d11.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern uint CreateDirect3D11SurfaceFromDXGISurface(IntPtr dxgiSurface, out IntPtr graphicsSurface);

		// Token: 0x06000157 RID: 343 RVA: 0x0000AF65 File Offset: 0x00009165
		public static IDirect3DDevice CreateDevice()
		{
			return Direct3D11Helper.CreateDevice(false);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AF70 File Offset: 0x00009170
		public static IDirect3DDevice CreateDevice(bool useWARP)
		{
			if (useWARP)
			{
				return Direct3D11Helper.CreateDirect3DDeviceFromSharpDXDevice(new Device(4, 40));
			}
			IDirect3DDevice result;
			using (Factory1 factory = new Factory1())
			{
				Adapter selectedAdapter = null;
				for (int i = 0; i < factory.Adapters.Length; i++)
				{
					Adapter adapter = factory.Adapters[i];
					AdapterDescription description = adapter.Description;
					if (description.VendorId != 32902 && (description.VendorId == 4318 || description.VendorId == 4098))
					{
						selectedAdapter = adapter;
						NLogService.WriteLog("选择独立显卡: " + description.Description);
						break;
					}
					if (description.DedicatedVideoMemory > 1073741824)
					{
						selectedAdapter = adapter;
						break;
					}
				}
				if (selectedAdapter == null && factory.Adapters.Length != 0)
				{
					selectedAdapter = factory.Adapters[0];
				}
				if (selectedAdapter != null)
				{
					result = Direct3D11Helper.CreateDirect3DDeviceFromSharpDXDevice(new Device(selectedAdapter, 40));
				}
				else
				{
					result = Direct3D11Helper.CreateDirect3DDeviceFromSharpDXDevice(new Device(1, 40));
				}
			}
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B078 File Offset: 0x00009278
		public static IDirect3DDevice CreateDirect3DDeviceFromSharpDXDevice(Device d3dDevice)
		{
			IDirect3DDevice device = null;
			using (Device3 dxgiDevice = d3dDevice.QueryInterface<Device3>())
			{
				IntPtr pUnknown;
				if (Direct3D11Helper.CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice.NativePointer, out pUnknown) == 0U)
				{
					device = (Marshal.GetObjectForIUnknown(pUnknown) as IDirect3DDevice);
					Marshal.Release(pUnknown);
				}
			}
			return device;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public static IDirect3DSurface CreateDirect3DSurfaceFromSharpDXTexture(Texture2D texture)
		{
			IDirect3DSurface surface = null;
			using (Surface dxgiSurface = texture.QueryInterface<Surface>())
			{
				IntPtr pUnknown;
				if (Direct3D11Helper.CreateDirect3D11SurfaceFromDXGISurface(dxgiSurface.NativePointer, out pUnknown) == 0U)
				{
					surface = (Marshal.GetObjectForIUnknown(pUnknown) as IDirect3DSurface);
					Marshal.Release(pUnknown);
				}
			}
			return surface;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B128 File Offset: 0x00009328
		public static Device CreateSharpDXDevice(IDirect3DDevice device)
		{
			Direct3D11Helper.IDirect3DDxgiInterfaceAccess direct3DDxgiInterfaceAccess = (Direct3D11Helper.IDirect3DDxgiInterfaceAccess)device;
			Guid id3D11Device = Direct3D11Helper.ID3D11Device;
			return new Device(direct3DDxgiInterfaceAccess.GetInterface(ref id3D11Device));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B14D File Offset: 0x0000934D
		public static Device CreateSharpDXDevice()
		{
			return Direct3D11Helper.CreateSharpDXDevice(false);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000B155 File Offset: 0x00009355
		public static Device CreateSharpDXDevice(bool useWARP)
		{
			return new Device(useWARP ? 4 : 1, 40);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B168 File Offset: 0x00009368
		public static Texture2D CreateSharpDXTexture2D(IDirect3DSurface surface)
		{
			Direct3D11Helper.IDirect3DDxgiInterfaceAccess direct3DDxgiInterfaceAccess = (Direct3D11Helper.IDirect3DDxgiInterfaceAccess)surface;
			Guid id3D11Texture2D = Direct3D11Helper.ID3D11Texture2D;
			return new Texture2D(direct3DDxgiInterfaceAccess.GetInterface(ref id3D11Texture2D));
		}

		// Token: 0x04000090 RID: 144
		private static Guid IInspectable = new Guid("AF86E2E0-B12D-4c6a-9C5A-D7AA65101E90");

		// Token: 0x04000091 RID: 145
		private static Guid ID3D11Resource = new Guid("dc8e63f3-d12b-4952-b47b-5e45026a862d");

		// Token: 0x04000092 RID: 146
		private static Guid IDXGIAdapter3 = new Guid("645967A4-1392-4310-A798-8053CE3E93FD");

		// Token: 0x04000093 RID: 147
		private static Guid ID3D11Device = new Guid("db6f6ddb-ac77-4e88-8253-819df9bbf140");

		// Token: 0x04000094 RID: 148
		private static Guid ID3D11Texture2D = new Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c");

		// Token: 0x0200008F RID: 143
		[Guid("A9B3D012-3DF2-4EE3-B8D1-8695F457D3C1")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[ComImport]
		private interface IDirect3DDxgiInterfaceAccess
		{
			// Token: 0x06000415 RID: 1045
			IntPtr GetInterface([In] ref Guid iid);
		}
	}
}
