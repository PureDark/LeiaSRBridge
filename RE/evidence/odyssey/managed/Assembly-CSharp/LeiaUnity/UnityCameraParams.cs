using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200012D RID: 301
	public struct UnityCameraParams
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x000248C1 File Offset: 0x00022AC1
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x000248C9 File Offset: 0x00022AC9
		public float FieldOfView { readonly get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000248D2 File Offset: 0x00022AD2
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x000248DA File Offset: 0x00022ADA
		public float Near { readonly get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x000248E3 File Offset: 0x00022AE3
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x000248EB File Offset: 0x00022AEB
		public float Far { readonly get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000248F4 File Offset: 0x00022AF4
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x000248FC File Offset: 0x00022AFC
		public float Depth { readonly get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00024905 File Offset: 0x00022B05
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0002490D File Offset: 0x00022B0D
		public int CullingMask { readonly get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00024916 File Offset: 0x00022B16
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0002491E File Offset: 0x00022B1E
		public CameraClearFlags ClearFlags { readonly get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00024927 File Offset: 0x00022B27
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0002492F File Offset: 0x00022B2F
		public Color BackgroundColor { readonly get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00024938 File Offset: 0x00022B38
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x00024940 File Offset: 0x00022B40
		public bool AllowHDR { readonly get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00024949 File Offset: 0x00022B49
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00024951 File Offset: 0x00022B51
		public bool Orthographic { readonly get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0002495A File Offset: 0x00022B5A
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x00024962 File Offset: 0x00022B62
		public float OrthographicSize { readonly get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0002496B File Offset: 0x00022B6B
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00024973 File Offset: 0x00022B73
		public Rect ViewportRect { readonly get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002497C File Offset: 0x00022B7C
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00024984 File Offset: 0x00022B84
		public RenderingPath RenderingPath { readonly get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002498D File Offset: 0x00022B8D
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x00024995 File Offset: 0x00022B95
		public bool UseOcclusionCulling { readonly get; set; }
	}
}
