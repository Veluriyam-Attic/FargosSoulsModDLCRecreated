using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200070C RID: 1804
	[EventData]
	internal class IncrementingCounterPayload : IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x001B327F File Offset: 0x001B247F
		// (set) Token: 0x060059E0 RID: 23008 RVA: 0x001B3287 File Offset: 0x001B2487
		public string Name { get; set; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060059E1 RID: 23009 RVA: 0x001B3290 File Offset: 0x001B2490
		// (set) Token: 0x060059E2 RID: 23010 RVA: 0x001B3298 File Offset: 0x001B2498
		public string DisplayName { get; set; }

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x001B32A1 File Offset: 0x001B24A1
		// (set) Token: 0x060059E4 RID: 23012 RVA: 0x001B32A9 File Offset: 0x001B24A9
		public string DisplayRateTimeScale { get; set; }

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x001B32B2 File Offset: 0x001B24B2
		// (set) Token: 0x060059E6 RID: 23014 RVA: 0x001B32BA File Offset: 0x001B24BA
		public double Increment { get; set; }

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x001B32C3 File Offset: 0x001B24C3
		// (set) Token: 0x060059E8 RID: 23016 RVA: 0x001B32CB File Offset: 0x001B24CB
		public float IntervalSec { get; internal set; }

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x001B32D4 File Offset: 0x001B24D4
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x001B32DC File Offset: 0x001B24DC
		public string Metadata { get; set; }

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060059EB RID: 23019 RVA: 0x001B32E5 File Offset: 0x001B24E5
		// (set) Token: 0x060059EC RID: 23020 RVA: 0x001B32ED File Offset: 0x001B24ED
		public string Series { get; set; }

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x001B32F6 File Offset: 0x001B24F6
		// (set) Token: 0x060059EE RID: 23022 RVA: 0x001B32FE File Offset: 0x001B24FE
		public string CounterType { get; set; }

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060059EF RID: 23023 RVA: 0x001B3307 File Offset: 0x001B2507
		// (set) Token: 0x060059F0 RID: 23024 RVA: 0x001B330F File Offset: 0x001B250F
		public string DisplayUnits { get; set; }

		// Token: 0x060059F1 RID: 23025 RVA: 0x001B3318 File Offset: 0x001B2518
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this.ForEnumeration.GetEnumerator();
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x001B3318 File Offset: 0x001B2518
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ForEnumeration.GetEnumerator();
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060059F3 RID: 23027 RVA: 0x001B3328 File Offset: 0x001B2528
		private IEnumerable<KeyValuePair<string, object>> ForEnumeration
		{
			get
			{
				yield return new KeyValuePair<string, object>("Name", this.Name);
				yield return new KeyValuePair<string, object>("DisplayName", this.DisplayName);
				yield return new KeyValuePair<string, object>("DisplayRateTimeScale", this.DisplayRateTimeScale);
				yield return new KeyValuePair<string, object>("Increment", this.Increment);
				yield return new KeyValuePair<string, object>("IntervalSec", this.IntervalSec);
				yield return new KeyValuePair<string, object>("Series", string.Format("Interval={0}", this.IntervalSec));
				yield return new KeyValuePair<string, object>("CounterType", "Sum");
				yield return new KeyValuePair<string, object>("Metadata", this.Metadata);
				yield return new KeyValuePair<string, object>("DisplayUnits", this.DisplayUnits);
				yield break;
			}
		}
	}
}
