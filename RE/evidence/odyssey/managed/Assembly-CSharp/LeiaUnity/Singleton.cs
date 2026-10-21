using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200012C RID: 300
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00024816 File Offset: 0x00022A16
		public virtual string ObjectName
		{
			get
			{
				return typeof(T).Name;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00024827 File Offset: 0x00022A27
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00024846 File Offset: 0x00022A46
		public static T Instance
		{
			get
			{
				if (Singleton<T>._instance == null)
				{
					bool isQuiting = Singleton<T>._isQuiting;
				}
				return Singleton<T>._instance;
			}
			set
			{
				Singleton<T>._instance = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0002484E File Offset: 0x00022A4E
		public static bool InstanceIsNull
		{
			get
			{
				return Singleton<T>._instance == null;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00024860 File Offset: 0x00022A60
		private void Awake()
		{
			if (Singleton<T>._instance == null)
			{
				Singleton<T>._instance = (T)((object)this);
			}
			if (Singleton<T>._instance != null && Singleton<T>._instance != this)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000248B9 File Offset: 0x00022AB9
		private void OnApplicationQuit()
		{
			Singleton<T>._isQuiting = true;
		}

		// Token: 0x04000687 RID: 1671
		private static T _instance;

		// Token: 0x04000688 RID: 1672
		private static bool _isQuiting;
	}
}
