using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

// Token: 0x02000017 RID: 23
public class EncryptHelper : MonoBehaviour
{
	// Token: 0x06000051 RID: 81 RVA: 0x00004042 File Offset: 0x00002242
	public static string ga(string a)
	{
		return Encoding.UTF8.GetString(EncryptHelper.gc(a));
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004054 File Offset: 0x00002254
	public static void gb(string a, bool b = true)
	{
		string path = a + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gk();
		using (Aes aes = Aes.Create())
		{
			byte[] array = Convert.FromBase64String(EncryptHelper.key);
			byte[] array2 = Convert.FromBase64String(EncryptHelper.iv);
			aes.Key = array;
			aes.IV = array2;
			using (FileStream fileStream = new FileStream(a, FileMode.Open, FileAccess.Read))
			{
				using (FileStream fileStream2 = new FileStream(path, FileMode.Create, FileAccess.Write))
				{
					using (CryptoStream cryptoStream = new CryptoStream(fileStream2, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						fileStream.CopyTo(cryptoStream);
					}
				}
			}
			if (b)
			{
				File.Delete(a);
			}
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004134 File Offset: 0x00002334
	public static byte[] gc(string a)
	{
		byte[] array = File.ReadAllBytes(a);
		byte[] array2 = Convert.FromBase64String(EncryptHelper.key);
		byte[] array3 = Convert.FromBase64String(EncryptHelper.iv);
		byte[] result;
		using (Aes aes = Aes.Create())
		{
			aes.Key = array2;
			aes.IV = array3;
			using (ICryptoTransform cryptoTransform = aes.CreateDecryptor())
			{
				result = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			}
		}
		return result;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000041C0 File Offset: 0x000023C0
	public static Dictionary<string, Sprite> gd(string a)
	{
		Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();
		try
		{
			string[] files = Directory.GetFiles(a, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gl());
			for (int i = 0; i < files.Length; i++)
			{
				EncryptHelper.ge(EncryptHelper.gc(files[i]), dictionary);
			}
			Debug.Log(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gm(), dictionary.Count));
		}
		catch (Exception ex)
		{
			Debug.LogError(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gn() + ex.Message);
		}
		return dictionary;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00004240 File Offset: 0x00002440
	private static void ge(byte[] a, Dictionary<string, Sprite> b)
	{
		using (MemoryStream memoryStream = new MemoryStream(a))
		{
			using (ZipArchive zipArchive = new ZipArchive(memoryStream, 0))
			{
				foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
				{
					if (zipArchiveEntry.FullName.EndsWith(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.go(), StringComparison.OrdinalIgnoreCase))
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							zipArchiveEntry.Open().CopyTo(memoryStream2);
							Sprite sprite = EncryptHelper.gf(memoryStream2.ToArray());
							if (sprite != null)
							{
								b[zipArchiveEntry.FullName] = sprite;
								Debug.Log(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gp() + zipArchiveEntry.FullName);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004344 File Offset: 0x00002544
	private static Sprite gf(byte[] a)
	{
		Texture2D texture2D = new Texture2D(2, 2, 4, false);
		if (ImageConversion.LoadImage(texture2D, a))
		{
			texture2D.filterMode = 1;
			texture2D.wrapMode = 1;
			texture2D.Apply();
			return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f);
		}
		Debug.LogError(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.gq());
		Object.Destroy(texture2D);
		return null;
	}

	// Token: 0x04000071 RID: 113
	public static readonly string key = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();

	// Token: 0x04000072 RID: 114
	public static readonly string iv = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m();
}
