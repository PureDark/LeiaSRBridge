using System;
using System.IO;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using Microsoft.Win32;
using UnityEngine;

namespace Odyssey3DHubUI.Helper
{
	// Token: 0x02000155 RID: 341
	public class RegistryHelper
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x0002715C File Offset: 0x0002535C
		public static string GetInstallStatus()
		{
			string result = string.Empty;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjw()))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjx());
						result = (((value != null) ? value.ToString() : null) ?? string.Empty);
					}
				}
			}
			catch (Exception ex)
			{
				bd.ie(RegistryHelper.cuk, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjy() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjz());
			}
			return result;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000271F0 File Offset: 0x000253F0
		public static bool CheckEULAConfirmation()
		{
			try
			{
				if (File.Exists(RegistryHelper.cul))
				{
					RootObject rootObject = JsonUtility.FromJson<RootObject>(File.ReadAllText(RegistryHelper.cul));
					return rootObject.UserAgreements != null && rootObject.UserAgreements.IsEULAConfirm == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bka();
				}
				return false;
			}
			catch (Exception ex)
			{
				bd.ie(RegistryHelper.cuk, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bkb() + ex.ToString(), <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bkc());
			}
			return false;
		}

		// Token: 0x04000729 RID: 1833
		private static readonly string cuk = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bkd();

		// Token: 0x0400072A RID: 1834
		private static readonly string cul = Path.Combine(bm.cao, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bke());
	}
}
