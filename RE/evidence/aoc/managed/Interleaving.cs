using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using msclr.interop;

namespace BOEInterleaving
{
	// Token: 0x0200000A RID: 10
	public class Interleaving : IDisposable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000015D4 File Offset: 0x000009D4
		private void ~Interleaving()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000015E4 File Offset: 0x000009E4
		private void !Interleaving()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000019FC File Offset: 0x00000DFC
		public unsafe static int InitSDK(IntPtr device, IntPtr inputResource, double[] deviceParams, double[] wholeMat, double[] refractParams, D3DVersion dxVersion, int viewportTopleftX, int viewportTopleftY, int viewportWidth, int viewportHeight, int outDXGI_FORMAT, string logpath)
		{
			if (deviceParams.Length == 6 && wholeMat.Length == 6 && refractParams.Length == 8)
			{
				ref double double& = ref deviceParams[0];
				ref double double&2 = ref wholeMat[0];
				ref double double&3 = ref refractParams[0];
				marshal_context marshal_context = new marshal_context();
				sbyte* ptr = marshal_context.marshal_as<char\u0020const\u0020*,System::String>(logpath);
				int result = <Module>.BOE_Interleaving_initSDK(device.ToPointer(), null, ref double&, ref double&2, ref double&3, (D3DVersion)dxVersion, viewportTopleftX, viewportTopleftY, viewportWidth, viewportHeight, outDXGI_FORMAT, ptr);
				if (marshal_context != null)
				{
					((IDisposable)marshal_context).Dispose();
				}
				return result;
			}
			throw new ArgumentException("参数数组长度不正确");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000015F4 File Offset: 0x000009F4
		public static void ReleaseSDK()
		{
			<Module>.BOE_Interleaving_releaseSDK();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00001608 File Offset: 0x00000A08
		public static void SetWholeParam(double[] param)
		{
			if (param.Length != 6)
			{
				throw new ArgumentException("参数数组长度必须为6");
			}
			ref double double& = ref param[0];
			<Module>.BOE_Interleaving_setWholeParam(ref double&);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00001634 File Offset: 0x00000A34
		public static void SetDeviceParam(double[] param)
		{
			if (param.Length != 6)
			{
				throw new ArgumentException("参数数组长度必须为6");
			}
			ref double double& = ref param[0];
			<Module>.BOE_Interleaving_setDeviceParam(ref double&);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00001660 File Offset: 0x00000A60
		public static void SetRefractDeviceParam(double[] param)
		{
			if (param.Length != 8)
			{
				throw new ArgumentException("参数数组长度必须为8");
			}
			ref double double& = ref param[0];
			<Module>.BOE_Interleaving_setRefractDeviceParam(ref double&);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000168C File Offset: 0x00000A8C
		public static int DoInterleave(float x, float y, float z, IntPtr context, IntPtr renderTargetView, IntPtr inShaderResourceView, int width, int height, int inDXGI_FORMAT, InterMode inter_mode, int sRGBFix)
		{
			return <Module>.BOE_Interleaving_doInterleave(x, y, z, context.ToPointer(), renderTargetView.ToPointer(), inShaderResourceView.ToPointer(), width, height, inDXGI_FORMAT, (InterMode)inter_mode, sRGBFix);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00001914 File Offset: 0x00000D14
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (!A_0)
			{
				try
				{
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000019C0 File Offset: 0x00000DC0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000194C File Offset: 0x00000D4C
		protected override void Finalize()
		{
			this.Dispose(false);
		}
	}
}
