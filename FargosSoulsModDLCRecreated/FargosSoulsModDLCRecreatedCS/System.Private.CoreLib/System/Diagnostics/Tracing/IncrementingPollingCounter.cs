using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200074A RID: 1866
	[NullableContext(1)]
	[Nullable(0)]
	public class IncrementingPollingCounter : DiagnosticCounter
	{
		// Token: 0x06005BA3 RID: 23459 RVA: 0x001BE858 File Offset: 0x001BDA58
		public IncrementingPollingCounter(string name, EventSource eventSource, Func<double> totalValueProvider) : base(name, eventSource)
		{
			if (totalValueProvider == null)
			{
				throw new ArgumentNullException("totalValueProvider");
			}
			this._totalValueProvider = totalValueProvider;
			base.Publish();
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x001BE87D File Offset: 0x001BDA7D
		public override string ToString()
		{
			return string.Format("IncrementingPollingCounter '{0}' Increment {1}", base.Name, this._increment);
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005BA5 RID: 23461 RVA: 0x001BE89A File Offset: 0x001BDA9A
		// (set) Token: 0x06005BA6 RID: 23462 RVA: 0x001BE8A2 File Offset: 0x001BDAA2
		public TimeSpan DisplayRateTimeScale { get; set; }

		// Token: 0x06005BA7 RID: 23463 RVA: 0x001BE8AC File Offset: 0x001BDAAC
		internal void UpdateMetric()
		{
			try
			{
				lock (this)
				{
					this._prevIncrement = this._increment;
					this._increment = this._totalValueProvider();
				}
			}
			catch (Exception ex)
			{
				base.ReportOutOfBandMessage("ERROR: Exception during EventCounter " + base.Name + " getMetricFunction callback: " + ex.Message);
			}
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001BE930 File Offset: 0x001BDB30
		internal override void WritePayload(float intervalSec, int pollingIntervalMillisec)
		{
			this.UpdateMetric();
			lock (this)
			{
				IncrementingCounterPayload incrementingCounterPayload = new IncrementingCounterPayload();
				incrementingCounterPayload.Name = base.Name;
				incrementingCounterPayload.DisplayName = (base.DisplayName ?? "");
				incrementingCounterPayload.DisplayRateTimeScale = ((this.DisplayRateTimeScale == TimeSpan.Zero) ? "" : this.DisplayRateTimeScale.ToString("c"));
				incrementingCounterPayload.IntervalSec = intervalSec;
				incrementingCounterPayload.Series = string.Format("Interval={0}", pollingIntervalMillisec);
				incrementingCounterPayload.CounterType = "Sum";
				incrementingCounterPayload.Metadata = base.GetMetadataString();
				incrementingCounterPayload.Increment = this._increment - this._prevIncrement;
				incrementingCounterPayload.DisplayUnits = (base.DisplayUnits ?? "");
				base.EventSource.Write<IncrementingPollingCounterPayloadType>("EventCounters", new EventSourceOptions
				{
					Level = EventLevel.LogAlways
				}, new IncrementingPollingCounterPayloadType(incrementingCounterPayload));
			}
		}

		// Token: 0x04001B2C RID: 6956
		private double _increment;

		// Token: 0x04001B2D RID: 6957
		private double _prevIncrement;

		// Token: 0x04001B2E RID: 6958
		private readonly Func<double> _totalValueProvider;
	}
}
