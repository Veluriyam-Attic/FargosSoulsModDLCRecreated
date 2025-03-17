using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000707 RID: 1799
	[Nullable(0)]
	[NullableContext(1)]
	public abstract class DiagnosticCounter : IDisposable
	{
		// Token: 0x0600599F RID: 22943 RVA: 0x001B2604 File Offset: 0x001B1804
		internal DiagnosticCounter(string name, EventSource eventSource)
		{
			if (name == null)
			{
				throw new ArgumentNullException("Name");
			}
			if (eventSource == null)
			{
				throw new ArgumentNullException("EventSource");
			}
			this.Name = name;
			this.EventSource = eventSource;
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x001B2657 File Offset: 0x001B1857
		private protected void Publish()
		{
			this._group = CounterGroup.GetCounterGroup(this.EventSource);
			this._group.Add(this);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x001B2676 File Offset: 0x001B1876
		public void Dispose()
		{
			if (this._group != null)
			{
				this._group.Remove(this);
				this._group = null;
			}
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x001B2694 File Offset: 0x001B1894
		public void AddMetadata(string key, [Nullable(2)] string value)
		{
			lock (this)
			{
				if (this._metadata == null)
				{
					this._metadata = new Dictionary<string, string>();
				}
				this._metadata.Add(key, value);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x001B26EC File Offset: 0x001B18EC
		// (set) Token: 0x060059A4 RID: 22948 RVA: 0x001B26F4 File Offset: 0x001B18F4
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DisplayName");
				}
				this._displayName = value;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x001B270B File Offset: 0x001B190B
		// (set) Token: 0x060059A6 RID: 22950 RVA: 0x001B2713 File Offset: 0x001B1913
		public string DisplayUnits
		{
			get
			{
				return this._displayUnits;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DisplayUnits");
				}
				this._displayUnits = value;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060059A7 RID: 22951 RVA: 0x001B272A File Offset: 0x001B192A
		public string Name { get; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x001B2732 File Offset: 0x001B1932
		public EventSource EventSource { get; }

		// Token: 0x060059A9 RID: 22953
		internal abstract void WritePayload(float intervalSec, int pollingIntervalMillisec);

		// Token: 0x060059AA RID: 22954 RVA: 0x001B273A File Offset: 0x001B193A
		internal void ReportOutOfBandMessage(string message)
		{
			this.EventSource.ReportOutOfBandMessage(message);
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x001B2748 File Offset: 0x001B1948
		internal string GetMetadataString()
		{
			if (this._metadata == null)
			{
				return "";
			}
			Dictionary<string, string>.Enumerator enumerator = this._metadata.GetEnumerator();
			bool flag = enumerator.MoveNext();
			KeyValuePair<string, string> keyValuePair = enumerator.Current;
			if (!enumerator.MoveNext())
			{
				return keyValuePair.Key + ":" + keyValuePair.Value;
			}
			StringBuilder stringBuilder = new StringBuilder().Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value);
			do
			{
				keyValuePair = enumerator.Current;
				stringBuilder.Append(',').Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value);
			}
			while (enumerator.MoveNext());
			return stringBuilder.ToString();
		}

		// Token: 0x040019F0 RID: 6640
		private string _displayName = "";

		// Token: 0x040019F1 RID: 6641
		private string _displayUnits = "";

		// Token: 0x040019F4 RID: 6644
		private CounterGroup _group;

		// Token: 0x040019F5 RID: 6645
		private Dictionary<string, string> _metadata;
	}
}
