using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SRUnity
{
	// Token: 0x020000FE RID: 254
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SystemHandler : MonoBehaviour
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x0001D4DB File Offset: 0x0001B6DB
		[RuntimeInitializeOnLoadMethod(0)]
		public static void OnApplicationLoad()
		{
			if (Object.FindObjectOfType<LeiaDisplay>() != null)
			{
				SystemHandler.LoadHandler();
			}
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdd());
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(SystemHandler.OnSceneChangedEvent);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001D50A File Offset: 0x0001B70A
		public static void LoadHandler()
		{
			SystemHandler.LoadHandler(false);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001D514 File Offset: 0x0001B714
		public static void LoadHandler(bool forceReload)
		{
			SRUtility.Trace(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bde(), PluginVersion.GetFullVersion()));
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdf());
			if (SystemHandler.Instance == null || forceReload)
			{
				string text = <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdg();
				GameObject gameObject = GameObject.Find(text);
				if (forceReload && gameObject != null)
				{
					SRUtility.Debug(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdh(), text));
					Object.DestroyImmediate(gameObject);
					gameObject = null;
				}
				if (gameObject == null)
				{
					SRUtility.Debug(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdi(), text));
					gameObject = new GameObject();
					gameObject.name = text;
				}
				SRUtility.SetSrGameObjectVisibility(gameObject);
				SystemHandler.Instance = gameObject.GetComponent<SystemHandler>();
				if (SystemHandler.Instance == null)
				{
					SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdj());
					SystemHandler.Instance = gameObject.AddComponent<SystemHandler>();
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060006B1 RID: 1713 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		// (remove) Token: 0x060006B2 RID: 1714 RVA: 0x0001D610 File Offset: 0x0001B810
		public static event SystemHandler.OnSceneChangedDelegate OnSceneChanged;

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001D644 File Offset: 0x0001B844
		public static void TransitionHandlerToScene(Scene scene)
		{
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				if (rootGameObjects[i].GetComponent<LeiaDisplay>() != null && Application.platform == 2)
				{
					if (!SystemHandler.Instance)
					{
						SystemHandler.LoadHandler();
					}
					SystemHandler.Instance.gameObject.SetActive(true);
					break;
				}
			}
			if (SystemHandler.Instance != null)
			{
				SceneManager.MoveGameObjectToScene(SystemHandler.Instance.gameObject, scene);
			}
			SystemHandler.OnSceneChangedDelegate onSceneChanged = SystemHandler.OnSceneChanged;
			if (onSceneChanged == null)
			{
				return;
			}
			onSceneChanged(scene);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		private static void OnSceneChangedEvent(Scene current, Scene next)
		{
			SRUtility.Trace(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdk(), current.name));
			SystemHandler.TransitionHandlerToScene(next);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001D6F3 File Offset: 0x0001B8F3
		public void OnEnable()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdl());
			this.StartModules();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001D708 File Offset: 0x0001B908
		public void Update()
		{
			foreach (ISimulatedRealityModule simulatedRealityModule in this.SrModules)
			{
				simulatedRealityModule.UpdateModule();
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001D758 File Offset: 0x0001B958
		public void OnDisable()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdm());
			this.DestroyModules();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001D76A File Offset: 0x0001B96A
		private void OnDomainUnload(object sender, EventArgs e)
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdn());
			this.DestroyModules();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001D77C File Offset: 0x0001B97C
		public void StartModules()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdo());
			if (this.IsLoaded)
			{
				SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdp());
				return;
			}
			this.IsLoaded = true;
			Type moduleType = typeof(ISimulatedRealityModule);
			foreach (Type type in from t in moduleType.Assembly.GetTypes()
			where t.IsSubclassOf(moduleType)
			select t)
			{
				if (!type.IsAbstract)
				{
					ISimulatedRealityModule item = (ISimulatedRealityModule)type.GetProperty(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdq(), BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null, null);
					this.SrModules.Add(item);
				}
			}
			foreach (ISimulatedRealityModule simulatedRealityModule in this.SrModules)
			{
				simulatedRealityModule.PreInitModule();
			}
			foreach (ISimulatedRealityModule simulatedRealityModule2 in this.SrModules)
			{
				simulatedRealityModule2.InitModule();
			}
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdr() + string.Join<ISimulatedRealityModule>(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bds(), this.SrModules));
			AppDomain.CurrentDomain.DomainUnload += this.OnDomainUnload;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		public void DestroyModules()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdt());
			if (!this.IsLoaded)
			{
				SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdu());
				return;
			}
			this.IsLoaded = false;
			foreach (ISimulatedRealityModule simulatedRealityModule in this.SrModules)
			{
				simulatedRealityModule.DestroyModule();
			}
			this.SrModules.Clear();
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060006BB RID: 1723 RVA: 0x0001D97C File Offset: 0x0001BB7C
		// (remove) Token: 0x060006BC RID: 1724 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		public static event SystemHandler.OnWindowFocusDelegate OnWindowFocus;

		// Token: 0x060006BD RID: 1725 RVA: 0x0001D9E3 File Offset: 0x0001BBE3
		private void OnApplicationFocus(bool hasFocus)
		{
			if (hasFocus)
			{
				this.windowFocusHint.BecomeIndifferent();
			}
			SystemHandler.OnWindowFocusDelegate onWindowFocus = SystemHandler.OnWindowFocus;
			if (onWindowFocus == null)
			{
				return;
			}
			onWindowFocus(hasFocus);
		}

		// Token: 0x0400059B RID: 1435
		public static SystemHandler Instance;

		// Token: 0x0400059D RID: 1437
		private readonly List<ISimulatedRealityModule> SrModules = new List<ISimulatedRealityModule>();

		// Token: 0x0400059E RID: 1438
		private bool IsLoaded;

		// Token: 0x040005A0 RID: 1440
		private readonly SrRenderModeHint windowFocusHint = new SrRenderModeHint();

		// Token: 0x020000FF RID: 255
		// (Invoke) Token: 0x060006C0 RID: 1728
		public delegate void OnSceneChangedDelegate(Scene scene);

		// Token: 0x02000100 RID: 256
		// (Invoke) Token: 0x060006C4 RID: 1732
		public delegate void OnWindowFocusDelegate(bool hasFocus);
	}
}
