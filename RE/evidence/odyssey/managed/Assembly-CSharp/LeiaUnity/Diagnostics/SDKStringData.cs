using System;
using System.Collections.Generic;
using System.IO;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity.Diagnostics
{
	// Token: 0x02000137 RID: 311
	public static class SDKStringData
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000257ED File Offset: 0x000239ED
		public static string VersionFileName
		{
			get
			{
				return <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjj();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000257F4 File Offset: 0x000239F4
		public static string SDKVersionFileText
		{
			get
			{
				TextAsset textAsset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(SDKStringData.VersionFileName));
				if (textAsset != null)
				{
					return textAsset.text;
				}
				return <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00025828 File Offset: 0x00023A28
		public static string SDKVersionLine
		{
			get
			{
				string sdkversionFileText = SDKStringData.SDKVersionFileText;
				if (!string.IsNullOrEmpty(sdkversionFileText))
				{
					string[] array = sdkversionFileText.Split('\n', StringSplitOptions.None);
					if (array != null && array.Length >= 0)
					{
						return array[0];
					}
				}
				return <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00025860 File Offset: 0x00023A60
		public static string SDKVersionCommit
		{
			get
			{
				string[] array = SDKStringData.SDKVersionFileText.Split('\n', StringSplitOptions.None);
				if (array != null && array.Length >= 1)
				{
					return array[1];
				}
				return <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00025890 File Offset: 0x00023A90
		public static List<uint> ParseAsSemantic(string mstr)
		{
			List<uint> list = new List<uint>();
			foreach (string text in mstr.Split(new char[]
			{
				' ',
				'.',
				'-',
				'\n'
			}))
			{
				int item;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out item))
				{
					list.Add((uint)item);
				}
			}
			return list;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x000258E8 File Offset: 0x00023AE8
		public static List<uint> SDKVersionSemantic
		{
			get
			{
				return SDKStringData.ParseAsSemantic(SDKStringData.SDKVersionLine);
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000258F4 File Offset: 0x00023AF4
		public static bool IsSemanticPlatformGreaterThanOrEqualTo(List<uint> basePlatform, List<uint> desiredPlatform)
		{
			if (basePlatform == null || desiredPlatform == null)
			{
				return false;
			}
			int num = 0;
			while (num < desiredPlatform.Count && num < basePlatform.Count)
			{
				if (desiredPlatform[num] > basePlatform[num])
				{
					return false;
				}
				if (desiredPlatform[num] < basePlatform[num])
				{
					return true;
				}
				num++;
			}
			return basePlatform.Count >= desiredPlatform.Count;
		}
	}
}
