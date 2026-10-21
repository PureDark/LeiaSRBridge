using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SimulatedReality
{
	// Token: 0x02000011 RID: 17
	public static class WeaverInterface
	{
		// Token: 0x0600001E RID: 30
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		private static extern WeaverErrorCode LoadTexture(WeaverTextureType type, out IntPtr pTexture, out int width, out int height, out int channels, out int bitsPerPixel);

		// Token: 0x0600001F RID: 31
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		private static extern void UnLoadCorrectionTexture(ref IntPtr pTexture);

		// Token: 0x06000020 RID: 32
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetGlobalParameters(Vector3 userPos);

		// Token: 0x06000021 RID: 33
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetXTalkFactor();

		// Token: 0x06000022 RID: 34
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetViewfilterSlope();

		// Token: 0x06000023 RID: 35
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetPattern();

		// Token: 0x06000024 RID: 36
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetSlant();

		// Token: 0x06000025 RID: 37
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetPx();

		// Token: 0x06000026 RID: 38
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetN();

		// Token: 0x06000027 RID: 39
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetDoN();

		// Token: 0x06000028 RID: 40
		[DllImport("DimencoWeaving", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FillInterpolators(Vector2 TextureRes, out Vector4 phases_offset, out Vector4 phases_scale_x, out Vector4 phases_scale_y, out Vector4 dxy_offset, out Vector4 dxy_scale_x, out Vector4 dxy_scale_y, out Vector2 ScreenPos_offset, out Vector2 ScreenPos_scale_x, out Vector2 ScreenPos_scale_y, out Vector2 WeaverVars_offset, out Vector2 WeaverVars_scale_x, out Vector2 WeaverVars_scale_y);

		// Token: 0x06000029 RID: 41 RVA: 0x00002050 File Offset: 0x00000250
		public static Texture2D CreateTextureResource(WeaverTextureType type)
		{
			IntPtr intPtr;
			int num;
			int num2;
			int channels;
			int bitsPerPixel;
			if (WeaverInterface.LoadTexture(type, out intPtr, out num, out num2, out channels, out bitsPerPixel) == WeaverErrorCode.WeaverSuccess)
			{
				TextureFormat textureFormat = WeaverInterface.GetTextureFormat(channels, bitsPerPixel);
				float num3 = (float)WeaverInterface.CalculateTextureSize(bitsPerPixel, num, num2, channels);
				Texture2D texture2D = new Texture2D(num, num2, textureFormat, false);
				texture2D.LoadRawTextureData(intPtr, (int)num3);
				texture2D.Apply();
				WeaverInterface.UnLoadCorrectionTexture(ref intPtr);
				return texture2D;
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000020A9 File Offset: 0x000002A9
		private static TextureFormat GetTextureFormat(int channels, int bitsPerPixel)
		{
			if (channels == 1 && bitsPerPixel == 8)
			{
				return 63;
			}
			if (channels == 1 && bitsPerPixel == 16)
			{
				return 9;
			}
			throw new ApplicationException("Unsupported weaving-texture format detected.");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000020CC File Offset: 0x000002CC
		private static int CalculateTextureSize(int bitsPerPixel, int width, int height, int channels)
		{
			return bitsPerPixel / 8 * width * height * channels;
		}

		// Token: 0x0400003B RID: 59
		private const string dllPostFix = "";
	}
}
