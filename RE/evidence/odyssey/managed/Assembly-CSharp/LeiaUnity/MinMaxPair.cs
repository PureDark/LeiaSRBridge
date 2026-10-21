using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	public class MinMaxPair : IEquatable<MinMaxPair>
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x00022162 File Offset: 0x00020362
		private MinMaxPair() : this(0f, 0f)
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00022174 File Offset: 0x00020374
		public MinMaxPair(float currentMin, float currentMax) : this(currentMin, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgx(), currentMax, <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bgy())
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00022188 File Offset: 0x00020388
		public MinMaxPair(float currentMin, string minLabel, float currentMax, string maxLabel) : this(currentMin, float.MinValue, minLabel, currentMax, float.MaxValue, maxLabel)
		{
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000221A0 File Offset: 0x000203A0
		public MinMaxPair(float currentMin, float lifetimeMin, string minLabel, float currentMax, float lifetimeMax, string maxLabel)
		{
			this._lifetimeMin = lifetimeMin;
			this._lifetimeMax = lifetimeMax;
			this._min = Mathf.Max(lifetimeMin, Mathf.Min(currentMin, currentMax));
			this._minLabel = minLabel;
			this._max = Mathf.Min(lifetimeMax, Mathf.Max(currentMin, currentMax));
			this._maxLabel = maxLabel;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00022210 File Offset: 0x00020410
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00022218 File Offset: 0x00020418
		public float min
		{
			get
			{
				return this._min;
			}
			set
			{
				this._min = Mathf.Max(this._lifetimeMin, value);
				this._max = Mathf.Max(this._min, this._max);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00022243 File Offset: 0x00020443
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0002224B File Offset: 0x0002044B
		public float max
		{
			get
			{
				return this._max;
			}
			set
			{
				this._max = Mathf.Min(this._lifetimeMax, value);
				this._min = Mathf.Min(this._min, this._max);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00022276 File Offset: 0x00020476
		public bool IsValueBetweenMinAndMax(float value)
		{
			return this.min <= value && value <= this.max;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002228F File Offset: 0x0002048F
		public override string ToString()
		{
			return string.Format(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bhf(), new object[]
			{
				this._minLabel,
				this._min,
				this._maxLabel,
				this._max
			});
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000222CF File Offset: 0x000204CF
		public bool Equals(MinMaxPair other)
		{
			return Mathf.Approximately(other._min, this._min) && Mathf.Approximately(other._max, this._max);
		}

		// Token: 0x0400063A RID: 1594
		[SerializeField]
		[HideInInspector]
		private float _min;

		// Token: 0x0400063B RID: 1595
		[SerializeField]
		[HideInInspector]
		private float _lifetimeMin = float.MinValue;

		// Token: 0x0400063C RID: 1596
		[SerializeField]
		[HideInInspector]
		private string _minLabel;

		// Token: 0x0400063D RID: 1597
		[SerializeField]
		[HideInInspector]
		private float _max;

		// Token: 0x0400063E RID: 1598
		[SerializeField]
		[HideInInspector]
		private float _lifetimeMax = float.MaxValue;

		// Token: 0x0400063F RID: 1599
		[SerializeField]
		[HideInInspector]
		private string _maxLabel;
	}
}
