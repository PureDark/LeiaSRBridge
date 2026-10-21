using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using SimulatedReality;
using UnityEngine;

namespace SRUnity
{
	// Token: 0x0200010A RID: 266
	public class SRHead : SimulatedRealityModule<SRHead>
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x0001EC85 File Offset: 0x0001CE85
		public override void InitModule()
		{
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfc());
			SRCore.OnContextChanged += this.OnContextChanged;
			this.OnContextChanged(SRContextChangeReason.Unknown);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		public override void UpdateModule()
		{
			if (!SRCore.IsSimulatedRealityAvailable())
			{
				this.eyes = new Vector3[]
				{
					SRHead.GetDefaultHeadPositionCM(),
					SRHead.GetDefaultHeadPositionCM()
				};
				this.ears = new Vector3[]
				{
					SRHead.GetDefaultHeadPositionCM(),
					SRHead.GetDefaultHeadPositionCM()
				};
				this.headPosition = SRHead.GetDefaultHeadPositionCM();
				this.headOrientation = Vector3.zero;
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001ED1E File Offset: 0x0001CF1E
		public override void DestroyModule()
		{
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfd());
			SRCore.OnContextChanged -= this.OnContextChanged;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001ED3C File Offset: 0x0001CF3C
		public void OnContextChanged(SRContextChangeReason contextChangeReason)
		{
			IntPtr srContext = SimulatedRealityModule<SRCore>.Instance.GetSrContext();
			SRUtility.Debug(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bfe());
			if (srContext != IntPtr.Zero)
			{
				this.srHeadTracker = SRFaceTrackers.createHeadTracker(srContext);
				this.srHeadCallback = new SRFaceTrackers.acceptHeadCallback(this.OnReceiveHead);
				this.srHeadListener = SRFaceTrackers.createHeadListener(this.srHeadTracker, this.srHeadCallback);
				SimulatedRealityModule<SRCore>.Instance.InitializeContext();
				return;
			}
			if (this.srHeadListener != IntPtr.Zero)
			{
				this.srHeadListener = IntPtr.Zero;
			}
			if (this.srHeadCallback != null)
			{
				this.srHeadCallback = null;
			}
			if (this.srHeadTracker != IntPtr.Zero)
			{
				this.srHeadTracker = IntPtr.Zero;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		public Vector3[] GetEyes(@do settings)
		{
			object obj = SRHead.headMutex;
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

		// Token: 0x0600071B RID: 1819 RVA: 0x0001EE78 File Offset: 0x0001D078
		public Vector3[] GetEars(@do settings)
		{
			object obj = SRHead.headMutex;
			Vector3[] array;
			lock (obj)
			{
				array = this.ears;
			}
			Vector3[] array2 = (Vector3[])array.Clone();
			float num = settings.bgj();
			array2[0] *= num;
			array2[1] *= num;
			return array2;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001EEFC File Offset: 0x0001D0FC
		public Vector3 GetHeadPosition(@do settings)
		{
			object obj = SRHead.headMutex;
			Vector3 result;
			lock (obj)
			{
				result = this.headPosition;
			}
			float num = settings.bgj();
			ref Vector3 ptr = ref result;
			ptr[0] = ptr[0] * num;
			ptr = ref result;
			ptr[1] = ptr[1] * num;
			ptr = ref result;
			ptr[2] = ptr[2] * num;
			return result;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001EF88 File Offset: 0x0001D188
		public Quaternion GetHeadOrientation()
		{
			object obj = SRHead.headMutex;
			Vector3 vector;
			lock (obj)
			{
				vector = this.headOrientation;
			}
			return Quaternion.Euler(-57.29578f * vector.x, 57.29578f * vector.y, 57.29578f * vector.z);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		public static Vector3 GetDefaultHeadPositionCM()
		{
			return new Vector3(0f, 0f, -60f);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001F00A File Offset: 0x0001D20A
		public Vector3 GetDefaultHeadPosition(@do settings)
		{
			return SRHead.GetDefaultHeadPositionCM() * settings.bgj();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001F01C File Offset: 0x0001D21C
		public Vector3 GetEyePosition(@do settings)
		{
			Vector3[] array = this.GetEyes(settings);
			Vector3 vector = array[0];
			Vector3 vector2 = array[1];
			return (vector + vector2) / 2f;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001F050 File Offset: 0x0001D250
		private void OnReceiveHead(SR_head head)
		{
			float num = 0.1f;
			object obj = SRHead.headMutex;
			lock (obj)
			{
				this.eyes[0] = SRUtility.SrToUnityCoords(head.eyes.eyes[0], head.eyes.eyes[1], head.eyes.eyes[2]) * num;
				this.eyes[1] = SRUtility.SrToUnityCoords(head.eyes.eyes[3], head.eyes.eyes[4], head.eyes.eyes[5]) * num;
				this.ears[0] = SRUtility.SrToUnityCoords(head.ears.ears[0], head.ears.ears[1], head.ears.ears[2]) * num;
				this.ears[1] = SRUtility.SrToUnityCoords(head.ears.ears[3], head.ears.ears[4], head.ears.ears[5]) * num;
				this.headPosition = SRUtility.SrToUnityCoords(head.headPose.position[0], head.headPose.position[1], head.headPose.position[2]) * num;
				this.headOrientation = SRUtility.SrToUnityCoords(head.headPose.orientation[0], head.headPose.orientation[1], head.headPose.orientation[2]);
			}
		}

		// Token: 0x040005D0 RID: 1488
		private IntPtr srHeadTracker;

		// Token: 0x040005D1 RID: 1489
		private SRFaceTrackers.acceptHeadCallback srHeadCallback;

		// Token: 0x040005D2 RID: 1490
		private IntPtr srHeadListener;

		// Token: 0x040005D3 RID: 1491
		private static readonly object headMutex = new object();

		// Token: 0x040005D4 RID: 1492
		private Vector3[] eyes = new Vector3[]
		{
			SRHead.GetDefaultHeadPositionCM(),
			SRHead.GetDefaultHeadPositionCM()
		};

		// Token: 0x040005D5 RID: 1493
		private Vector3[] ears = new Vector3[]
		{
			SRHead.GetDefaultHeadPositionCM(),
			SRHead.GetDefaultHeadPositionCM()
		};

		// Token: 0x040005D6 RID: 1494
		private Vector3 headPosition = SRHead.GetDefaultHeadPositionCM();

		// Token: 0x040005D7 RID: 1495
		private Vector3 headOrientation = Vector3.zero;
	}
}
