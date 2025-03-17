using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200070A RID: 1802
	[EventData]
	internal class CounterPayload : IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x001B2EB5 File Offset: 0x001B20B5
		// (set) Token: 0x060059BC RID: 22972 RVA: 0x001B2EBD File Offset: 0x001B20BD
		public string Name { get; set; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060059BD RID: 22973 RVA: 0x001B2EC6 File Offset: 0x001B20C6
		// (set) Token: 0x060059BE RID: 22974 RVA: 0x001B2ECE File Offset: 0x001B20CE
		public string DisplayName { get; set; }

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x001B2ED7 File Offset: 0x001B20D7
		// (set) Token: 0x060059C0 RID: 22976 RVA: 0x001B2EDF File Offset: 0x001B20DF
		public double Mean { get; set; }

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x001B2EE8 File Offset: 0x001B20E8
		// (set) Token: 0x060059C2 RID: 22978 RVA: 0x001B2EF0 File Offset: 0x001B20F0
		public double StandardDeviation { get; set; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x001B2EF9 File Offset: 0x001B20F9
		// (set) Token: 0x060059C4 RID: 22980 RVA: 0x001B2F01 File Offset: 0x001B2101
		public int Count { get; set; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x001B2F0A File Offset: 0x001B210A
		// (set) Token: 0x060059C6 RID: 22982 RVA: 0x001B2F12 File Offset: 0x001B2112
		public double Min { get; set; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060059C7 RID: 22983 RVA: 0x001B2F1B File Offset: 0x001B211B
		// (set) Token: 0x060059C8 RID: 22984 RVA: 0x001B2F23 File Offset: 0x001B2123
		public double Max { get; set; }

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060059C9 RID: 22985 RVA: 0x001B2F2C File Offset: 0x001B212C
		// (set) Token: 0x060059CA RID: 22986 RVA: 0x001B2F34 File Offset: 0x001B2134
		public float IntervalSec { get; internal set; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060059CB RID: 22987 RVA: 0x001B2F3D File Offset: 0x001B213D
		// (set) Token: 0x060059CC RID: 22988 RVA: 0x001B2F45 File Offset: 0x001B2145
		public string Series { get; set; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060059CD RID: 22989 RVA: 0x001B2F4E File Offset: 0x001B214E
		// (set) Token: 0x060059CE RID: 22990 RVA: 0x001B2F56 File Offset: 0x001B2156
		public string CounterType { get; set; }

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x001B2F5F File Offset: 0x001B215F
		// (set) Token: 0x060059D0 RID: 22992 RVA: 0x001B2F67 File Offset: 0x001B2167
		public string Metadata { get; set; }

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x001B2F70 File Offset: 0x001B2170
		// (set) Token: 0x060059D2 RID: 22994 RVA: 0x001B2F78 File Offset: 0x001B2178
		public string DisplayUnits { get; set; }

		// Token: 0x060059D3 RID: 22995 RVA: 0x001B2F81 File Offset: 0x001B2181
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this.ForEnumeration.GetEnumerator();
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x001B2F81 File Offset: 0x001B2181
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ForEnumeration.GetEnumerator();
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x001B2F90 File Offset: 0x001B2190
		private IEnumerable<KeyValuePair<string, object>> ForEnumeration
		{
			get
			{
				yield return new KeyValuePair<string, object>("Name", this.Name);
				yield return new KeyValuePair<string, object>("DisplayName", this.DisplayName);
				yield return new KeyValuePair<string, object>("DisplayUnits", this.DisplayUnits);
				yield return new KeyValuePair<string, object>("Mean", this.Mean);
				yield return new KeyValuePair<string, object>("StandardDeviation", this.StandardDeviation);
				yield return new KeyValuePair<string, object>("Count", this.Count);
				yield return new KeyValuePair<string, object>("Min", this.Min);
				yield return new KeyValuePair<string, object>("Max", this.Max);
				yield return new KeyValuePair<string, object>("IntervalSec", this.IntervalSec);
				yield return new KeyValuePair<string, object>("Series", string.Format("Interval={0}", this.IntervalSec));
				yield return new KeyValuePair<string, object>("CounterType", "Mean");
				yield return new KeyValuePair<string, object>("Metadata", this.Metadata);
				yield break;
			}
		}
	}
}
