using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000748 RID: 1864
	[NullableContext(1)]
	[Nullable(0)]
	public class IncrementingEventCounter : DiagnosticCounter
	{
		// Token: 0x06005B99 RID: 23449 RVA: 0x001BE64F File Offset: 0x001BD84F
		public IncrementingEventCounter(string name, EventSource eventSource) : base(name, eventSource)
		{
			base.Publish();
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x001BE660 File Offset: 0x001BD860
		public void Increment(double increment = 1.0)
		{
			lock (this)
			{
				this._increment += increment;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x001BE6A4 File Offset: 0x001BD8A4
		// (set) Token: 0x06005B9C RID: 23452 RVA: 0x001BE6AC File Offset: 0x001BD8AC
		public TimeSpan DisplayRateTimeScale { get; set; }

		// Token: 0x06005B9D RID: 23453 RVA: 0x001BE6B5 File Offset: 0x001BD8B5
		public override string ToString()
		{
			return string.Format("IncrementingEventCounter '{0}' Increment {1}", base.Name, this._increment);
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x001BE6D4 File Offset: 0x001BD8D4
		internal override void WritePayload(float intervalSec, int pollingIntervalMillisec)
		{
			lock (this)
			{
				IncrementingCounterPayload incrementingCounterPayload = new IncrementingCounterPayload();
				incrementingCounterPayload.Name = base.Name;
				incrementingCounterPayload.IntervalSec = intervalSec;
				incrementingCounterPayload.DisplayName = (base.DisplayName ?? "");
				incrementingCounterPayload.DisplayRateTimeScale = ((this.DisplayRateTimeScale == TimeSpan.Zero) ? "" : this.DisplayRateTimeScale.ToString("c"));
				incrementingCounterPayload.Series = string.Format("Interval={0}", pollingIntervalMillisec);
				incrementingCounterPayload.CounterType = "Sum";
				incrementingCounterPayload.Metadata = base.GetMetadataString();
				incrementingCounterPayload.Increment = this._increment - this._prevIncrement;
				incrementingCounterPayload.DisplayUnits = (base.DisplayUnits ?? "");
				this._prevIncrement = this._increment;
				base.EventSource.Write<IncrementingEventCounterPayloadType>("EventCounters", new EventSourceOptions
				{
					Level = EventLevel.LogAlways
				}, new IncrementingEventCounterPayloadType(incrementingCounterPayload));
			}
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x001BE7F4 File Offset: 0x001BD9F4
		internal void UpdateMetric()
		{
			lock (this)
			{
				this._prevIncrement = this._increment;
			}
		}

		// Token: 0x04001B28 RID: 6952
		private double _increment;

		// Token: 0x04001B29 RID: 6953
		private double _prevIncrement;
	}
}
