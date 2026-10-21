using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine.Rendering;

namespace LeiaUnity
{
	// Token: 0x0200012F RID: 303
	public static class RenderPipelineUtils
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00024A14 File Offset: 0x00022C14
		public static bool IsUnityRenderPipeline()
		{
			if (GraphicsSettings.renderPipelineAsset == null)
			{
				return false;
			}
			string name = GraphicsSettings.renderPipelineAsset.GetType().Name;
			return name.Contains(RenderPipelineUtils.assetTypeURP) || name.Contains(RenderPipelineUtils.assetTypeHDRP);
		}

		// Token: 0x04000699 RID: 1689
		private static readonly string assetTypeURP = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjc();

		// Token: 0x0400069A RID: 1690
		private static readonly string assetTypeHDRP = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjd();
	}
}
