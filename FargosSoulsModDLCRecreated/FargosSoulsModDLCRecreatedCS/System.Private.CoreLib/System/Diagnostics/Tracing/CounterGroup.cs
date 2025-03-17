using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000708 RID: 1800
	internal class CounterGroup
	{
		// Token: 0x060059AC RID: 22956 RVA: 0x001B2805 File Offset: 0x001B1A05
		internal CounterGroup(EventSource eventSource)
		{
			this._eventSource = eventSource;
			this._counters = new List<DiagnosticCounter>();
			this.RegisterCommandCallback();
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x001B2828 File Offset: 0x001B1A28
		internal void Add(DiagnosticCounter eventCounter)
		{
			object obj = CounterGroup.s_counterGroupLock;
			lock (obj)
			{
				this._counters.Add(eventCounter);
			}
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x001B2870 File Offset: 0x001B1A70
		internal void Remove(DiagnosticCounter eventCounter)
		{
			object obj = CounterGroup.s_counterGroupLock;
			lock (obj)
			{
				this._counters.Remove(eventCounter);
			}
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x001B28B8 File Offset: 0x001B1AB8
		private void RegisterCommandCallback()
		{
			this._eventSource.EventCommandExecuted += this.OnEventSourceCommand;
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x001B28D4 File Offset: 0x001B1AD4
		private void OnEventSourceCommand(object sender, EventCommandEventArgs e)
		{
			if (e.Command == EventCommand.Enable || e.Command == EventCommand.Update)
			{
				string s;
				float pollingIntervalInSeconds;
				if (!e.Arguments.TryGetValue("EventCounterIntervalSec", out s) || !float.TryParse(s, out pollingIntervalInSeconds))
				{
					return;
				}
				object obj = CounterGroup.s_counterGroupLock;
				lock (obj)
				{
					this.EnableTimer(pollingIntervalInSeconds);
					return;
				}
			}
			if (e.Command == EventCommand.Disable)
			{
				object obj2 = CounterGroup.s_counterGroupLock;
				lock (obj2)
				{
					this.DisableTimer();
				}
			}
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x001B2984 File Offset: 0x001B1B84
		private static void EnsureEventSourceIndexAvailable(int eventSourceIndex)
		{
			if (CounterGroup.s_counterGroups == null)
			{
				CounterGroup.s_counterGroups = new WeakReference<CounterGroup>[eventSourceIndex + 1];
				return;
			}
			if (eventSourceIndex >= CounterGroup.s_counterGroups.Length)
			{
				WeakReference<CounterGroup>[] destinationArray = new WeakReference<CounterGroup>[eventSourceIndex + 1];
				Array.Copy(CounterGroup.s_counterGroups, destinationArray, CounterGroup.s_counterGroups.Length);
				CounterGroup.s_counterGroups = destinationArray;
			}
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x001B29D4 File Offset: 0x001B1BD4
		internal static CounterGroup GetCounterGroup(EventSource eventSource)
		{
			object obj = CounterGroup.s_counterGroupLock;
			CounterGroup result;
			lock (obj)
			{
				int num = EventListener.EventSourceIndex(eventSource);
				CounterGroup.EnsureEventSourceIndexAvailable(num);
				WeakReference<CounterGroup> weakReference = CounterGroup.s_counterGroups[num];
				CounterGroup counterGroup;
				if (weakReference == null || !weakReference.TryGetTarget(out counterGroup))
				{
					counterGroup = new CounterGroup(eventSource);
					CounterGroup.s_counterGroups[num] = new WeakReference<CounterGroup>(counterGroup);
				}
				result = counterGroup;
			}
			return result;
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x001B2A4C File Offset: 0x001B1C4C
		private void EnableTimer(float pollingIntervalInSeconds)
		{
			if (pollingIntervalInSeconds <= 0f)
			{
				this._pollingIntervalInMilliseconds = 0;
				return;
			}
			if (this._pollingIntervalInMilliseconds == 0 || pollingIntervalInSeconds * 1000f < (float)this._pollingIntervalInMilliseconds)
			{
				this._pollingIntervalInMilliseconds = (int)(pollingIntervalInSeconds * 1000f);
				this.ResetCounters();
				this._timeStampSinceCollectionStarted = DateTime.UtcNow;
				bool flag = false;
				try
				{
					if (!ExecutionContext.IsFlowSuppressed())
					{
						ExecutionContext.SuppressFlow();
						flag = true;
					}
					this._nextPollingTimeStamp = DateTime.UtcNow + new TimeSpan(0, 0, (int)pollingIntervalInSeconds);
					if (CounterGroup.s_pollingThread == null)
					{
						CounterGroup.s_pollingThreadSleepEvent = new AutoResetEvent(false);
						CounterGroup.s_counterGroupEnabledList = new List<CounterGroup>();
						CounterGroup.s_pollingThread = new Thread(new ThreadStart(CounterGroup.PollForValues))
						{
							IsBackground = true
						};
						CounterGroup.s_pollingThread.Start();
					}
					if (!CounterGroup.s_counterGroupEnabledList.Contains(this))
					{
						CounterGroup.s_counterGroupEnabledList.Add(this);
					}
					CounterGroup.s_pollingThreadSleepEvent.Set();
				}
				finally
				{
					if (flag)
					{
						ExecutionContext.RestoreFlow();
					}
				}
			}
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x001B2B50 File Offset: 0x001B1D50
		private void DisableTimer()
		{
			this._pollingIntervalInMilliseconds = 0;
			List<CounterGroup> list = CounterGroup.s_counterGroupEnabledList;
			if (list == null)
			{
				return;
			}
			list.Remove(this);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x001B2B6C File Offset: 0x001B1D6C
		private void ResetCounters()
		{
			object obj = CounterGroup.s_counterGroupLock;
			lock (obj)
			{
				foreach (DiagnosticCounter diagnosticCounter in this._counters)
				{
					IncrementingEventCounter incrementingEventCounter = diagnosticCounter as IncrementingEventCounter;
					if (incrementingEventCounter != null)
					{
						incrementingEventCounter.UpdateMetric();
					}
					else
					{
						IncrementingPollingCounter incrementingPollingCounter = diagnosticCounter as IncrementingPollingCounter;
						if (incrementingPollingCounter != null)
						{
							incrementingPollingCounter.UpdateMetric();
						}
						else
						{
							EventCounter eventCounter = diagnosticCounter as EventCounter;
							if (eventCounter != null)
							{
								eventCounter.ResetStatistics();
							}
						}
					}
				}
			}
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x001B2C1C File Offset: 0x001B1E1C
		private void OnTimer()
		{
			if (this._eventSource.IsEnabled())
			{
				object obj = CounterGroup.s_counterGroupLock;
				DateTime utcNow;
				TimeSpan timeSpan;
				int pollingIntervalInMilliseconds;
				DiagnosticCounter[] array;
				lock (obj)
				{
					utcNow = DateTime.UtcNow;
					timeSpan = utcNow - this._timeStampSinceCollectionStarted;
					pollingIntervalInMilliseconds = this._pollingIntervalInMilliseconds;
					array = new DiagnosticCounter[this._counters.Count];
					this._counters.CopyTo(array);
				}
				foreach (DiagnosticCounter diagnosticCounter in array)
				{
					diagnosticCounter.WritePayload((float)timeSpan.TotalSeconds, pollingIntervalInMilliseconds);
				}
				object obj2 = CounterGroup.s_counterGroupLock;
				lock (obj2)
				{
					this._timeStampSinceCollectionStarted = utcNow;
					do
					{
						this._nextPollingTimeStamp += new TimeSpan(0, 0, 0, 0, this._pollingIntervalInMilliseconds);
					}
					while (this._nextPollingTimeStamp <= utcNow);
				}
			}
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x001B2D30 File Offset: 0x001B1F30
		private static void PollForValues()
		{
			AutoResetEvent autoResetEvent = null;
			List<Action> list = new List<Action>();
			for (;;)
			{
				list.Clear();
				int num = int.MaxValue;
				object obj = CounterGroup.s_counterGroupLock;
				lock (obj)
				{
					autoResetEvent = CounterGroup.s_pollingThreadSleepEvent;
					using (List<CounterGroup>.Enumerator enumerator = CounterGroup.s_counterGroupEnabledList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CounterGroup counterGroup = enumerator.Current;
							DateTime utcNow = DateTime.UtcNow;
							if (counterGroup._nextPollingTimeStamp < utcNow + new TimeSpan(0, 0, 0, 0, 1))
							{
								list.Add(delegate
								{
									counterGroup.OnTimer();
								});
							}
							int val = (int)(counterGroup._nextPollingTimeStamp - utcNow).TotalMilliseconds;
							val = Math.Max(1, val);
							num = Math.Min(num, val);
						}
					}
				}
				foreach (Action action in list)
				{
					action();
				}
				if (num == 2147483647)
				{
					num = -1;
				}
				if (autoResetEvent != null)
				{
					autoResetEvent.WaitOne(num);
				}
			}
		}

		// Token: 0x040019F6 RID: 6646
		private readonly EventSource _eventSource;

		// Token: 0x040019F7 RID: 6647
		private readonly List<DiagnosticCounter> _counters;

		// Token: 0x040019F8 RID: 6648
		private static readonly object s_counterGroupLock = new object();

		// Token: 0x040019F9 RID: 6649
		private static WeakReference<CounterGroup>[] s_counterGroups;

		// Token: 0x040019FA RID: 6650
		private DateTime _timeStampSinceCollectionStarted;

		// Token: 0x040019FB RID: 6651
		private int _pollingIntervalInMilliseconds;

		// Token: 0x040019FC RID: 6652
		private DateTime _nextPollingTimeStamp;

		// Token: 0x040019FD RID: 6653
		private static Thread s_pollingThread;

		// Token: 0x040019FE RID: 6654
		private static AutoResetEvent s_pollingThreadSleepEvent;

		// Token: 0x040019FF RID: 6655
		private static List<CounterGroup> s_counterGroupEnabledList;
	}
}
