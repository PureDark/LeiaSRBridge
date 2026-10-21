using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity.Examples
{
	// Token: 0x02000142 RID: 322
	public class TrafficSpawner : MonoBehaviour
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x000264BF File Offset: 0x000246BF
		private void OnEnable()
		{
			this.SpawnCarTimer();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000264C8 File Offset: 0x000246C8
		public void SpawnCarTimer()
		{
			if (base.enabled)
			{
				int num = (int)(Random.value * (float)this.spawnPoints.Length);
				Object.Instantiate<Transform>(this.carPrefab, this.spawnPoints[num].position, this.spawnPoints[num].rotation);
				base.Invoke(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bjs(), this.spawnInterval);
			}
		}

		// Token: 0x040006E1 RID: 1761
		[SerializeField]
		private Transform carPrefab;

		// Token: 0x040006E2 RID: 1762
		[SerializeField]
		private Transform[] spawnPoints;

		// Token: 0x040006E3 RID: 1763
		[SerializeField]
		private float spawnInterval = 3f;
	}
}
