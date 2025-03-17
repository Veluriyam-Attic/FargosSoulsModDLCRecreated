using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200074E RID: 1870
	[NullableContext(1)]
	[Nullable(0)]
	public class PollingCounter : DiagnosticCounter
	{
		// Token: 0x06005C40 RID: 23616 RVA: 0x001C0709 File Offset: 0x001BF909
		public PollingCounter(string name, EventSource eventSource, Func<double> metricProvider) : base(name, eventSource)
		{
			if (metricProvider == null)
			{
				throw new ArgumentNullException("metricProvider");
			}
			this._metricProvider = metricProvider;
			base.Publish();
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x001C072E File Offset: 0x001BF92E
		public override string ToString()
		{
			return "PollingCounter '" + base.Name + "' Count 1 Mean " + this._lastVal.ToString("n3");
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x001C0758 File Offset: 0x001BF958
		internal override void WritePayload(float intervalSec, int pollingIntervalMillisec)
		{
			lock (this)
			{
				double num = 0.0;
				try
				{
					num = this._metricProvider();
				}
				catch (Exception ex)
				{
					base.ReportOutOfBandMessage("ERROR: Exception during EventCounter " + base.Name + " metricProvider callback: " + ex.Message);
				}
				CounterPayload counterPayload = new CounterPayload();
				counterPayload.Name = base.Name;
				counterPayload.DisplayName = (base.DisplayName ?? "");
				counterPayload.Count = 1;
				counterPayload.IntervalSec = intervalSec;
				counterPayload.Series = string.Format("Interval={0}", pollingIntervalMillisec);
				counterPayload.CounterType = "Mean";
				counterPayload.Mean = num;
				counterPayload.Max = num;
				counterPayload.Min = num;
				counterPayload.Metadata = base.GetMetadataString();
				counterPayload.StandardDeviation = 0.0;
				counterPayload.DisplayUnits = (base.DisplayUnits ?? "");
				this._lastVal = num;
				base.EventSource.Write<PollingPayloadType>("EventCounters", new EventSourceOptions
				{
					Level = EventLevel.LogAlways
				}, new PollingPayloadType(counterPayload));
			}
		}

		// Token: 0x04001B55 RID: 6997
		private readonly Func<double> _metricProvider;

		// Token: 0x04001B56 RID: 6998
		private double _lastVal;
	}
}
