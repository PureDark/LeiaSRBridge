using System;
using UnityEngine;

namespace LeiaLoft.Examples
{
	// Token: 0x020000FB RID: 251
	public class TrafficMovement : MonoBehaviour
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001D34C File Offset: 0x0001B54C
		private void Start()
		{
			if (this.randomizeStartVelocity)
			{
				this.speed = new Vector3(Random.value * this.speed.x * 2f - this.speed.x, Random.value * this.speed.y * 2f - this.speed.y, Random.value * this.speed.z * 2f - this.speed.z);
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		private void Update()
		{
			if (this.relativeDirection)
			{
				base.transform.position += base.transform.rotation * this.speed * Time.deltaTime;
				return;
			}
			base.transform.position += this.speed * Time.deltaTime;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001D44A File Offset: 0x0001B64A
		public void SetXSpeed(float xspeed)
		{
			this.speed = new Vector3(xspeed, this.speed.y, this.speed.z);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001D46E File Offset: 0x0001B66E
		public void SetYSpeed(float yspeed)
		{
			this.speed = new Vector3(this.speed.x, yspeed, this.speed.z);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001D492 File Offset: 0x0001B692
		public void SetZSpeed(float zspeed)
		{
			this.speed = new Vector3(this.speed.x, this.speed.y, zspeed);
		}

		// Token: 0x04000597 RID: 1431
		[SerializeField]
		private Vector3 speed;

		// Token: 0x04000598 RID: 1432
		[SerializeField]
		private bool randomizeStartVelocity;

		// Token: 0x04000599 RID: 1433
		[SerializeField]
		private bool relativeDirection;
	}
}
