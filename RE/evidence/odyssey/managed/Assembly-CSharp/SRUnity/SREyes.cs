using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using AOT;
using SimulatedReality;
using UnityEngine;

namespace SRUnity
{
	// Token: 0x02000109 RID: 265
	public class SREyes : SimulatedRealityModule<SREyes>
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001E988 File Offset: 0x0001CB88
		public override void InitModule()
		{
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bez());
			SRCore.OnContextChanged += this.OnContextChanged;
			this.OnContextChanged(SRContextChangeReason.Unknown);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		public void OnContextChanged(SRContextChangeReason contextChangeReason)
		{
			IntPtr srContext = SimulatedRealityModule<SRCore>.Instance.GetSrContext();
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfa());
			if (srContext != IntPtr.Zero)
			{
				this.srEyeTracker = SREyeTrackers.createEyeTracker(srContext);
				this.srEyeCallback = new SREyeTrackers.AcceptEyePairCallback(SREyes.OnReceiveEye);
				this.srEyeListener = SREyeTrackers.createEyePairListener(this.srEyeTracker, this.srEyeCallback);
				SimulatedRealityModule<SRCore>.Instance.InitializeContext();
				return;
			}
			if (this.srEyeListener != IntPtr.Zero)
			{
				this.srEyeListener = IntPtr.Zero;
			}
			if (this.srEyeCallback != null)
			{
				this.srEyeCallback = null;
			}
			if (this.srEyeTracker != IntPtr.Zero)
			{
				this.srEyeTracker = IntPtr.Zero;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001EA64 File Offset: 0x0001CC64
		public override void UpdateModule()
		{
			if (!SRCore.IsSimulatedRealityAvailable())
			{
				this.eyes = new Vector3[]
				{
					SREyes.GetDefaultEyePositionCM(),
					SREyes.GetDefaultEyePositionCM()
				};
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001EA91 File Offset: 0x0001CC91
		public override void DestroyModule()
		{
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfb());
			SRCore.OnContextChanged -= this.OnContextChanged;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001EAAE File Offset: 0x0001CCAE
		public Vector3[] GetEyesPosition(@do settings)
		{
			return this.GetEyes(settings);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		public Vector3 GetEyePosition(@do settings)
		{
			Vector3[] array = this.GetEyes(settings);
			Vector3 vector = array[0];
			Vector3 vector2 = array[1];
			return (vector + vector2) / 2f;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001EAEC File Offset: 0x0001CCEC
		public Vector3[] GetEyes(@do settings)
		{
			object obj = SREyes.eyeMutex;
			Vector3[] array;
			lock (obj)
			{
				array = this.eyes;
			}
			Vector3[] array2 = (Vector3[])array.Clone();
			float num = settings.bgj();
			array2[0] *= num;
			array2[1] *= num;
			return array2;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001EB70 File Offset: 0x0001CD70
		public static Vector3 GetDefaultEyePositionCM()
		{
			return SRHead.GetDefaultHeadPositionCM();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001EB77 File Offset: 0x0001CD77
		public Vector3 GetDefaultEyePosition(@do settings)
		{
			return SimulatedRealityModule<SRHead>.Instance.GetDefaultHeadPosition(settings);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001EB84 File Offset: 0x0001CD84
		[MonoPInvokeCallback(typeof(SREyeTrackers.AcceptEyePairCallback))]
		private static void OnReceiveEye(SR_eyePair eye)
		{
			float num = 0.1f;
			object obj = SREyes.eyeMutex;
			lock (obj)
			{
				SimulatedRealityModule<SREyes>.Instance.eyes[0] = SRUtility.SrToUnityCoords(eye.eyes[0], eye.eyes[1], eye.eyes[2]) * num;
				SimulatedRealityModule<SREyes>.Instance.eyes[1] = SRUtility.SrToUnityCoords(eye.eyes[3], eye.eyes[4], eye.eyes[5]) * num;
			}
		}

		// Token: 0x040005CB RID: 1483
		private IntPtr srEyeTracker = IntPtr.Zero;

		// Token: 0x040005CC RID: 1484
		private SREyeTrackers.AcceptEyePairCallback srEyeCallback;

		// Token: 0x040005CD RID: 1485
		private IntPtr srEyeListener = IntPtr.Zero;

		// Token: 0x040005CE RID: 1486
		private static readonly object eyeMutex = new object();

		// Token: 0x040005CF RID: 1487
		private Vector3[] eyes = new Vector3[]
		{
			SREyes.GetDefaultEyePositionCM(),
			SREyes.GetDefaultEyePositionCM()
		};
	}
}
