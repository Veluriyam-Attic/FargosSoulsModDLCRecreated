using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200070F RID: 1807
	[NullableContext(1)]
	[Nullable(0)]
	public class EventCounter : DiagnosticCounter
	{
		// Token: 0x060059FD RID: 23037 RVA: 0x001B3584 File Offset: 0x001B2784
		public EventCounter(string name, EventSource eventSource) : base(name, eventSource)
		{
			this._min = double.PositiveInfinity;
			this._max = double.NegativeInfinity;
			double[] array = new double[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = double.NegativeInfinity;
			}
			this._bufferedValues = array;
			base.Publish();
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x001B35E6 File Offset: 0x001B27E6
		public void WriteMetric(float value)
		{
			this.Enqueue((double)value);
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x001B35F0 File Offset: 0x001B27F0
		public void WriteMetric(double value)
		{
			this.Enqueue(value);
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x001B35FC File Offset: 0x001B27FC
		public override string ToString()
		{
			int num = Volatile.Read(ref this._count);
			if (num != 0)
			{
				return string.Format("EventCounter '{0}' Count {1} Mean {2}", base.Name, num, (this._sum / (double)num).ToString("n3"));
			}
			return "EventCounter '" + base.Name + "' Count 0";
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x001B365C File Offset: 0x001B285C
		internal void OnMetricWritten(double value)
		{
			this._sum += value;
			this._sumSquared += value * value;
			if (value > this._max)
			{
				this._max = value;
			}
			if (value < this._min)
			{
				this._min = value;
			}
			this._count++;
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x001B36B8 File Offset: 0x001B28B8
		internal override void WritePayload(float intervalSec, int pollingIntervalMillisec)
		{
			lock (this)
			{
				this.Flush();
				CounterPayload counterPayload = new CounterPayload();
				counterPayload.Count = this._count;
				counterPayload.IntervalSec = intervalSec;
				if (0 < this._count)
				{
					counterPayload.Mean = this._sum / (double)this._count;
					counterPayload.StandardDeviation = Math.Sqrt(this._sumSquared / (double)this._count - this._sum * this._sum / (double)this._count / (double)this._count);
				}
				else
				{
					counterPayload.Mean = 0.0;
					counterPayload.StandardDeviation = 0.0;
				}
				counterPayload.Min = this._min;
				counterPayload.Max = this._max;
				counterPayload.Series = string.Format("Interval={0}", pollingIntervalMillisec);
				counterPayload.CounterType = "Mean";
				counterPayload.Metadata = base.GetMetadataString();
				counterPayload.DisplayName = (base.DisplayName ?? "");
				counterPayload.DisplayUnits = (base.DisplayUnits ?? "");
				counterPayload.Name = base.Name;
				this.ResetStatistics();
				base.EventSource.Write<CounterPayloadType>("EventCounters", new EventSourceOptions
				{
					Level = EventLevel.LogAlways
				}, new CounterPayloadType(counterPayload));
			}
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x001B3834 File Offset: 0x001B2A34
		internal void ResetStatistics()
		{
			lock (this)
			{
				this._count = 0;
				this._sum = 0.0;
				this._sumSquared = 0.0;
				this._min = double.PositiveInfinity;
				this._max = double.NegativeInfinity;
			}
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x001B38AC File Offset: 0x001B2AAC
		private void Enqueue(double value)
		{
			int num = this._bufferedValuesIndex;
			double num2;
			do
			{
				num2 = Interlocked.CompareExchange(ref this._bufferedValues[num], value, double.NegativeInfinity);
				num++;
				if (this._bufferedValues.Length <= num)
				{
					lock (this)
					{
						this.Flush();
					}
					num = 0;
				}
			}
			while (num2 != double.NegativeInfinity);
			this._bufferedValuesIndex = num;
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x001B3934 File Offset: 0x001B2B34
		protected void Flush()
		{
			for (int i = 0; i < this._bufferedValues.Length; i++)
			{
				double num = Interlocked.Exchange(ref this._bufferedValues[i], double.NegativeInfinity);
				if (num != double.NegativeInfinity)
				{
					this.OnMetricWritten(num);
				}
			}
			this._bufferedValuesIndex = 0;
		}

		// Token: 0x04001A23 RID: 6691
		private int _count;

		// Token: 0x04001A24 RID: 6692
		private double _sum;

		// Token: 0x04001A25 RID: 6693
		private double _sumSquared;

		// Token: 0x04001A26 RID: 6694
		private double _min;

		// Token: 0x04001A27 RID: 6695
		private double _max;

		// Token: 0x04001A28 RID: 6696
		private readonly double[] _bufferedValues;

		// Token: 0x04001A29 RID: 6697
		private volatile int _bufferedValuesIndex;
	}
}
