using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000750 RID: 1872
	[EventSource(Guid = "49592C0F-5A05-516D-AA4B-A64E02026C89", Name = "System.Runtime")]
	internal sealed class RuntimeEventSource : EventSource
	{
		// Token: 0x06005C46 RID: 23622 RVA: 0x001C08DC File Offset: 0x001BFADC
		public static void Initialize()
		{
			RuntimeEventSource.s_RuntimeEventSource = new RuntimeEventSource();
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x001C08E8 File Offset: 0x001BFAE8
		private RuntimeEventSource() : base(new Guid(1230580751, 23045, 20845, 170, 75, 166, 78, 2, 2, 108, 137), "System.Runtime", EventSourceSettings.EtwSelfDescribingEventFormat, null)
		{
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x001C0930 File Offset: 0x001BFB30
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			if (command.Command == EventCommand.Enable)
			{
				if (this._cpuTimeCounter == null)
				{
					this._cpuTimeCounter = new PollingCounter("cpu-usage", this, () => (double)RuntimeEventSourceHelper.GetCpuUsage())
					{
						DisplayName = "CPU Usage",
						DisplayUnits = "%"
					};
				}
				if (this._workingSetCounter == null)
				{
					this._workingSetCounter = new PollingCounter("working-set", this, () => (double)(Environment.WorkingSet / 1000000L))
					{
						DisplayName = "Working Set",
						DisplayUnits = "MB"
					};
				}
				if (this._gcHeapSizeCounter == null)
				{
					this._gcHeapSizeCounter = new PollingCounter("gc-heap-size", this, () => (double)(GC.GetTotalMemory(false) / 1000000L))
					{
						DisplayName = "GC Heap Size",
						DisplayUnits = "MB"
					};
				}
				if (this._gen0GCCounter == null)
				{
					this._gen0GCCounter = new IncrementingPollingCounter("gen-0-gc-count", this, () => (double)GC.CollectionCount(0))
					{
						DisplayName = "Gen 0 GC Count",
						DisplayRateTimeScale = new TimeSpan(0, 1, 0)
					};
				}
				if (this._gen1GCCounter == null)
				{
					this._gen1GCCounter = new IncrementingPollingCounter("gen-1-gc-count", this, () => (double)GC.CollectionCount(1))
					{
						DisplayName = "Gen 1 GC Count",
						DisplayRateTimeScale = new TimeSpan(0, 1, 0)
					};
				}
				if (this._gen2GCCounter == null)
				{
					this._gen2GCCounter = new IncrementingPollingCounter("gen-2-gc-count", this, () => (double)GC.CollectionCount(2))
					{
						DisplayName = "Gen 2 GC Count",
						DisplayRateTimeScale = new TimeSpan(0, 1, 0)
					};
				}
				if (this._threadPoolThreadCounter == null)
				{
					this._threadPoolThreadCounter = new PollingCounter("threadpool-thread-count", this, () => (double)ThreadPool.ThreadCount)
					{
						DisplayName = "ThreadPool Thread Count"
					};
				}
				if (this._monitorContentionCounter == null)
				{
					this._monitorContentionCounter = new IncrementingPollingCounter("monitor-lock-contention-count", this, () => (double)Monitor.LockContentionCount)
					{
						DisplayName = "Monitor Lock Contention Count",
						DisplayRateTimeScale = new TimeSpan(0, 0, 1)
					};
				}
				if (this._threadPoolQueueCounter == null)
				{
					this._threadPoolQueueCounter = new PollingCounter("threadpool-queue-length", this, () => (double)ThreadPool.PendingWorkItemCount)
					{
						DisplayName = "ThreadPool Queue Length"
					};
				}
				if (this._completedItemsCounter == null)
				{
					this._completedItemsCounter = new IncrementingPollingCounter("threadpool-completed-items-count", this, () => (double)ThreadPool.CompletedWorkItemCount)
					{
						DisplayName = "ThreadPool Completed Work Item Count",
						DisplayRateTimeScale = new TimeSpan(0, 0, 1)
					};
				}
				if (this._allocRateCounter == null)
				{
					this._allocRateCounter = new IncrementingPollingCounter("alloc-rate", this, () => (double)GC.GetTotalAllocatedBytes(false))
					{
						DisplayName = "Allocation Rate",
						DisplayUnits = "B",
						DisplayRateTimeScale = new TimeSpan(0, 0, 1)
					};
				}
				if (this._timerCounter == null)
				{
					this._timerCounter = new PollingCounter("active-timer-count", this, () => (double)Timer.ActiveCount)
					{
						DisplayName = "Number of Active Timers"
					};
				}
				if (this._fragmentationCounter == null)
				{
					this._fragmentationCounter = new PollingCounter("gc-fragmentation", this, delegate()
					{
						GCMemoryInfo gcmemoryInfo = GC.GetGCMemoryInfo();
						return (double)gcmemoryInfo.FragmentedBytes * 100.0 / (double)gcmemoryInfo.HeapSizeBytes;
					})
					{
						DisplayName = "GC Fragmentation",
						DisplayUnits = "%"
					};
				}
				if (this._exceptionCounter == null)
				{
					this._exceptionCounter = new IncrementingPollingCounter("exception-count", this, () => Exception.GetExceptionCount())
					{
						DisplayName = "Exception Count",
						DisplayRateTimeScale = new TimeSpan(0, 0, 1)
					};
				}
				if (this._gcTimeCounter == null)
				{
					this._gcTimeCounter = new PollingCounter("time-in-gc", this, () => (double)GC.GetLastGCPercentTimeInGC())
					{
						DisplayName = "% Time in GC since last GC",
						DisplayUnits = "%"
					};
				}
				if (this._gen0SizeCounter == null)
				{
					this._gen0SizeCounter = new PollingCounter("gen-0-size", this, () => GC.GetGenerationSize(0))
					{
						DisplayName = "Gen 0 Size",
						DisplayUnits = "B"
					};
				}
				if (this._gen1SizeCounter == null)
				{
					this._gen1SizeCounter = new PollingCounter("gen-1-size", this, () => GC.GetGenerationSize(1))
					{
						DisplayName = "Gen 1 Size",
						DisplayUnits = "B"
					};
				}
				if (this._gen2SizeCounter == null)
				{
					this._gen2SizeCounter = new PollingCounter("gen-2-size", this, () => GC.GetGenerationSize(2))
					{
						DisplayName = "Gen 2 Size",
						DisplayUnits = "B"
					};
				}
				if (this._lohSizeCounter == null)
				{
					this._lohSizeCounter = new PollingCounter("loh-size", this, () => GC.GetGenerationSize(3))
					{
						DisplayName = "LOH Size",
						DisplayUnits = "B"
					};
				}
				if (this._pohSizeCounter == null)
				{
					this._pohSizeCounter = new PollingCounter("poh-size", this, () => GC.GetGenerationSize(4))
					{
						DisplayName = "POH (Pinned Object Heap) Size",
						DisplayUnits = "B"
					};
				}
				if (this._assemblyCounter == null)
				{
					this._assemblyCounter = new PollingCounter("assembly-count", this, () => Assembly.GetAssemblyCount())
					{
						DisplayName = "Number of Assemblies Loaded"
					};
				}
				if (this._ilBytesJittedCounter == null)
				{
					this._ilBytesJittedCounter = new PollingCounter("il-bytes-jitted", this, () => (double)RuntimeHelpers.GetILBytesJitted())
					{
						DisplayName = "IL Bytes Jitted",
						DisplayUnits = "B"
					};
				}
				if (this._methodsJittedCounter == null)
				{
					this._methodsJittedCounter = new PollingCounter("methods-jitted-count", this, () => (double)RuntimeHelpers.GetMethodsJittedCount())
					{
						DisplayName = "Number of Methods Jitted"
					};
				}
			}
		}

		// Token: 0x04001B58 RID: 7000
		private static RuntimeEventSource s_RuntimeEventSource;

		// Token: 0x04001B59 RID: 7001
		private PollingCounter _gcHeapSizeCounter;

		// Token: 0x04001B5A RID: 7002
		private IncrementingPollingCounter _gen0GCCounter;

		// Token: 0x04001B5B RID: 7003
		private IncrementingPollingCounter _gen1GCCounter;

		// Token: 0x04001B5C RID: 7004
		private IncrementingPollingCounter _gen2GCCounter;

		// Token: 0x04001B5D RID: 7005
		private PollingCounter _cpuTimeCounter;

		// Token: 0x04001B5E RID: 7006
		private PollingCounter _workingSetCounter;

		// Token: 0x04001B5F RID: 7007
		private PollingCounter _threadPoolThreadCounter;

		// Token: 0x04001B60 RID: 7008
		private IncrementingPollingCounter _monitorContentionCounter;

		// Token: 0x04001B61 RID: 7009
		private PollingCounter _threadPoolQueueCounter;

		// Token: 0x04001B62 RID: 7010
		private IncrementingPollingCounter _completedItemsCounter;

		// Token: 0x04001B63 RID: 7011
		private IncrementingPollingCounter _allocRateCounter;

		// Token: 0x04001B64 RID: 7012
		private PollingCounter _timerCounter;

		// Token: 0x04001B65 RID: 7013
		private PollingCounter _fragmentationCounter;

		// Token: 0x04001B66 RID: 7014
		private IncrementingPollingCounter _exceptionCounter;

		// Token: 0x04001B67 RID: 7015
		private PollingCounter _gcTimeCounter;

		// Token: 0x04001B68 RID: 7016
		private PollingCounter _gen0SizeCounter;

		// Token: 0x04001B69 RID: 7017
		private PollingCounter _gen1SizeCounter;

		// Token: 0x04001B6A RID: 7018
		private PollingCounter _gen2SizeCounter;

		// Token: 0x04001B6B RID: 7019
		private PollingCounter _lohSizeCounter;

		// Token: 0x04001B6C RID: 7020
		private PollingCounter _pohSizeCounter;

		// Token: 0x04001B6D RID: 7021
		private PollingCounter _assemblyCounter;

		// Token: 0x04001B6E RID: 7022
		private PollingCounter _ilBytesJittedCounter;

		// Token: 0x04001B6F RID: 7023
		private PollingCounter _methodsJittedCounter;
	}
}
