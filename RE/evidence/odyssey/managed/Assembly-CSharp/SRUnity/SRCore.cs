using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using SimulatedReality;
using UnityEngine;

namespace SRUnity
{
	// Token: 0x02000103 RID: 259
	public class SRCore : SimulatedRealityModule<SRCore>
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060006D4 RID: 1748 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		// (remove) Token: 0x060006D5 RID: 1749 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		public static event SRCore.SRSystemEventDelegate OnUSBNotConnected;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060006D6 RID: 1750 RVA: 0x0001DD24 File Offset: 0x0001BF24
		// (remove) Token: 0x060006D7 RID: 1751 RVA: 0x0001DD58 File Offset: 0x0001BF58
		public static event SRCore.SRSystemEventDelegate OnUSBNotConnectedResolved;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060006D8 RID: 1752 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		// (remove) Token: 0x060006D9 RID: 1753 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		public static event SRCore.SRSystemEventDelegate OnDisplayNotConnected;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060006DA RID: 1754 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		// (remove) Token: 0x060006DB RID: 1755 RVA: 0x0001DE28 File Offset: 0x0001C028
		public static event SRCore.SRSystemEventDelegate OnDisplayNotConnectedResolved;

		// Token: 0x060006DC RID: 1756 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public override void PreInitModule()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdw());
			string simulatedRealityRuntimePath = SRInternalPlatform.GetSimulatedRealityRuntimePath();
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdx() + simulatedRealityRuntimePath);
			if (simulatedRealityRuntimePath == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.m() || !Directory.Exists(simulatedRealityRuntimePath + <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdy()))
			{
				SRUtility.Warning(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdz());
				SRCore.srRuntimeAvailable = false;
				this.HandleContextChanged(SRContextChangeReason.SRUnavailable);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001DEBF File Offset: 0x0001C0BF
		public override void InitModule()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bea());
			if (SRCore.IsSimulatedRealityAvailable())
			{
				SRCore.StartContextThread();
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		public override void UpdateModule()
		{
			object obj = this.systemEventsMutex;
			lock (obj)
			{
				using (List<SR_systemEvent>.Enumerator enumerator = this.systemEvents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						switch (enumerator.Current.eventType)
						{
						case 1:
							SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.beb());
							if (this.GetSrContext() != IntPtr.Zero)
							{
								this.ReleaseSrContext();
								SRCore.StartContextThread();
							}
							break;
						case 2:
							SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bec());
							this.eventRenderModeHint.Force2D();
							break;
						case 3:
							SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bed());
							this.eventRenderModeHint.BecomeIndifferent();
							break;
						case 4:
							this.HandleUSBNotConnected();
							break;
						case 5:
							this.HandleUSBNotConnectedResolved();
							break;
						case 6:
							this.HandleDisplayNotConnected();
							break;
						case 7:
							this.HandleDisplayNotConnectedResolved();
							break;
						}
					}
				}
				this.systemEvents.Clear();
			}
			bool flag2 = false;
			obj = this.contextMutex;
			lock (obj)
			{
				if (this.newContext != IntPtr.Zero)
				{
					this.srContext = this.newContext;
					this.newContext = IntPtr.Zero;
					flag2 = true;
				}
			}
			if (flag2)
			{
				this.HandleContextChanged(SRContextChangeReason.SRAvailable);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001E064 File Offset: 0x0001C264
		public override void DestroyModule()
		{
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bee());
			if (this.srContext != IntPtr.Zero && SRCore.IsSimulatedRealityAvailable())
			{
				this.ReleaseSrContext();
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001E090 File Offset: 0x0001C290
		private void ReleaseSrContext()
		{
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bef());
			object obj = this.contextMutex;
			IntPtr intPtr;
			lock (obj)
			{
				intPtr = this.srContext;
				this.srContext = IntPtr.Zero;
			}
			this.HandleContextChanged(SRContextChangeReason.Shutdown);
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.beg());
			SRCore.deleteSRContext(intPtr);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001E100 File Offset: 0x0001C300
		public static bool IsSimulatedRealityAvailable()
		{
			return SRCore.srRuntimeAvailable;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001E108 File Offset: 0x0001C308
		public void InitializeContext()
		{
			if (this.srContext != IntPtr.Zero)
			{
				object obj = this.contextMutex;
				lock (obj)
				{
					SRCore.initializeSRContext(this.srContext);
				}
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001E160 File Offset: 0x0001C360
		public IntPtr GetSrContext()
		{
			object obj = this.contextMutex;
			IntPtr result;
			lock (obj)
			{
				result = this.srContext;
			}
			return result;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		private void HandleUSBNotConnected()
		{
			LogUtil.Log(LogLevel.Warning, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.beh(), Array.Empty<object>());
			SRCore.SRSystemEventDelegate onUSBNotConnected = SRCore.OnUSBNotConnected;
			if (onUSBNotConnected == null)
			{
				return;
			}
			onUSBNotConnected();
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001E1C5 File Offset: 0x0001C3C5
		private void HandleUSBNotConnectedResolved()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bei(), Array.Empty<object>());
			SRCore.SRSystemEventDelegate onUSBNotConnectedResolved = SRCore.OnUSBNotConnectedResolved;
			if (onUSBNotConnectedResolved == null)
			{
				return;
			}
			onUSBNotConnectedResolved();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
		private void HandleDisplayNotConnected()
		{
			LogUtil.Log(LogLevel.Warning, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bej(), Array.Empty<object>());
			SRCore.SRSystemEventDelegate onDisplayNotConnected = SRCore.OnDisplayNotConnected;
			if (onDisplayNotConnected == null)
			{
				return;
			}
			onDisplayNotConnected();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001E207 File Offset: 0x0001C407
		private void HandleDisplayNotConnectedResolved()
		{
			LogUtil.Log(LogLevel.Debug, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bek(), Array.Empty<object>());
			SRCore.SRSystemEventDelegate onDisplayNotConnectedResolved = SRCore.OnDisplayNotConnectedResolved;
			if (onDisplayNotConnectedResolved == null)
			{
				return;
			}
			onDisplayNotConnectedResolved();
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060006E8 RID: 1768 RVA: 0x0001E228 File Offset: 0x0001C428
		// (remove) Token: 0x060006E9 RID: 1769 RVA: 0x0001E25C File Offset: 0x0001C45C
		public static event SRCore.OnContextChangedDelegate OnContextChanged;

		// Token: 0x060006EA RID: 1770 RVA: 0x0001E290 File Offset: 0x0001C490
		private void HandleContextChanged(SRContextChangeReason contextChangeReason)
		{
			SRUtility.Trace(string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bel(), contextChangeReason.ToString()));
			try
			{
				IntPtr intPtr = this.GetSrContext();
				if (intPtr != IntPtr.Zero)
				{
					this.srDisplay = SRDisplays.createScreen(intPtr);
					this.srSystemSense = SRCore.createSystemSense(intPtr);
					this.srSystemSenseCallback = new SRCore.acceptSystemSenseCallback(this.OnReceiveSystemEvent);
					this.srSystemSenseListener = SRCore.createSystemEventListener(this.srSystemSense, this.srSystemSenseCallback);
					SrRenderModeHint.SetLensHintInstance(SRDisplays.createSwitchableLensHint(intPtr));
					this.lastKnownPhysicalSize = new Vector2(SRDisplays.getPhysicalSizeWidth(this.srDisplay), SRDisplays.getPhysicalSizeHeight(this.srDisplay));
					this.lastKnownResolution = new Vector2Int(SRDisplays.getResolutionWidth(this.srDisplay), SRDisplays.getResolutionHeight(this.srDisplay));
					this.lastKnownPhysicalResolution = new Vector2Int(SRDisplays.getPhysicalResolutionWidth(this.srDisplay), SRDisplays.getPhysicalResolutionHeight(this.srDisplay));
					this.lastKnownDotPitch = SRDisplays.getDotPitch(this.srDisplay);
					this.lastknownSlant = WeaverInterface.GetSlant();
					this.lastknownPx = WeaverInterface.GetPx();
					this.lastknownN = WeaverInterface.GetN();
					this.lastknownDoN = WeaverInterface.GetDoN();
					this.InitializeContext();
				}
				else
				{
					if (this.srSystemSenseListener != IntPtr.Zero)
					{
						SRCore.deleteSystemEventListener(this.srSystemSenseListener);
						this.srSystemSenseListener = IntPtr.Zero;
					}
					this.srSystemSenseCallback = null;
					this.srSystemSense = IntPtr.Zero;
					this.srDisplay = IntPtr.Zero;
					SrRenderModeHint.SetLensHintInstance(IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bem() + ex.Message);
			}
			SRCore.OnContextChangedDelegate onContextChanged = SRCore.OnContextChanged;
			if (onContextChanged == null)
			{
				return;
			}
			onContextChanged(contextChangeReason);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001E45C File Offset: 0x0001C65C
		public Vector2 getPhysicalSize()
		{
			return this.lastKnownPhysicalSize;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001E464 File Offset: 0x0001C664
		public Vector2Int getResolution()
		{
			return this.lastKnownResolution;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001E46C File Offset: 0x0001C66C
		public Vector2Int getPhysicalResolution()
		{
			return this.lastKnownPhysicalResolution;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001E474 File Offset: 0x0001C674
		public float getDotPitch()
		{
			return this.lastKnownDotPitch;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001E47C File Offset: 0x0001C67C
		public float getSlant()
		{
			return this.lastknownSlant;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001E484 File Offset: 0x0001C684
		public float getPx()
		{
			return this.lastknownPx;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001E48C File Offset: 0x0001C68C
		public float getN()
		{
			return this.lastknownN;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001E494 File Offset: 0x0001C694
		public float getDoN()
		{
			return this.lastknownDoN;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001E49C File Offset: 0x0001C69C
		private void OnReceiveSystemEvent(SR_systemEvent systemEvent)
		{
			object obj = this.systemEventsMutex;
			lock (obj)
			{
				this.systemEvents.Add(systemEvent);
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		private static void StartContextThread()
		{
			new Thread(new ThreadStart(SRCore.ObtainNewContext)).Start();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		private static void ObtainNewContext()
		{
			IntPtr value;
			for (;;)
			{
				value = SimulatedRealityModule<SRCore>.Instance.GetSrContext();
				if (!(value == IntPtr.Zero))
				{
					goto IL_6C;
				}
				SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.ben());
				value = SRCore.newSRContext();
				if (value != IntPtr.Zero)
				{
					break;
				}
				Thread.Sleep(100);
			}
			SRUtility.Trace(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.beo());
			object obj = SimulatedRealityModule<SRCore>.Instance.contextMutex;
			lock (obj)
			{
				SimulatedRealityModule<SRCore>.Instance.newContext = value;
			}
			return;
			IL_6C:
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bep());
		}

		// Token: 0x040005A5 RID: 1445
		private IntPtr srContext;

		// Token: 0x040005A6 RID: 1446
		private IntPtr srDisplay;

		// Token: 0x040005A7 RID: 1447
		private IntPtr srSystemSense;

		// Token: 0x040005A8 RID: 1448
		private SRCore.acceptSystemSenseCallback srSystemSenseCallback;

		// Token: 0x040005A9 RID: 1449
		private IntPtr srSystemSenseListener;

		// Token: 0x040005AA RID: 1450
		private readonly object contextMutex = new object();

		// Token: 0x040005AB RID: 1451
		private IntPtr newContext;

		// Token: 0x040005AC RID: 1452
		private readonly object systemEventsMutex = new object();

		// Token: 0x040005AD RID: 1453
		private List<SR_systemEvent> systemEvents = new List<SR_systemEvent>();

		// Token: 0x040005AE RID: 1454
		private static bool srRuntimeAvailable = true;

		// Token: 0x040005B3 RID: 1459
		private readonly SrRenderModeHint eventRenderModeHint = new SrRenderModeHint();

		// Token: 0x040005B5 RID: 1461
		private Vector2 lastKnownPhysicalSize = Vector2.one;

		// Token: 0x040005B6 RID: 1462
		private Vector2Int lastKnownResolution = Vector2Int.one;

		// Token: 0x040005B7 RID: 1463
		private Vector2Int lastKnownPhysicalResolution = Vector2Int.one;

		// Token: 0x040005B8 RID: 1464
		private float lastKnownDotPitch;

		// Token: 0x040005B9 RID: 1465
		private float lastknownSlant;

		// Token: 0x040005BA RID: 1466
		private float lastknownPx;

		// Token: 0x040005BB RID: 1467
		private float lastknownN;

		// Token: 0x040005BC RID: 1468
		private float lastknownDoN;

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x060006F9 RID: 1785
		public delegate void SRSystemEventDelegate();

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x060006FD RID: 1789
		public delegate void OnContextChangedDelegate(SRContextChangeReason contextChangeReason);
	}
}
