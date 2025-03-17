using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	// Token: 0x02000247 RID: 583
	[EventSource(Guid = "0866B2B8-5CEF-5DB9-2612-0C0FFD814A44", Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		// Token: 0x06002429 RID: 9257 RVA: 0x00138F94 File Offset: 0x00138194
		private ArrayPoolEventSource() : base(new Guid(140948152, 23791, 23993, 38, 18, 12, 15, 253, 129, 74, 68), "System.Buffers.ArrayPoolEventSource")
		{
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00138FD8 File Offset: 0x001381D8
		[Event(1, Level = EventLevel.Verbose)]
		internal unsafe void BufferRented(int bufferId, int bufferSize, int poolId, int bucketId)
		{
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
			ptr->Size = 4;
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr->Reserved = 0;
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[1].Reserved = 0;
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[2].Reserved = 0;
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			ptr[3].Reserved = 0;
			base.WriteEventCore(1, 4, ptr);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x001390BC File Offset: 0x001382BC
		[Event(2, Level = EventLevel.Informational)]
		internal unsafe void BufferAllocated(int bufferId, int bufferSize, int poolId, int bucketId, ArrayPoolEventSource.BufferAllocatedReason reason)
		{
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
			ptr->Size = 4;
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr->Reserved = 0;
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[1].Reserved = 0;
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[2].Reserved = 0;
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			ptr[3].Reserved = 0;
			ptr[4].Size = 4;
			ptr[4].DataPointer = (IntPtr)((void*)(&reason));
			ptr[4].Reserved = 0;
			base.WriteEventCore(2, 5, ptr);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x001391D9 File Offset: 0x001383D9
		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x001391E5 File Offset: 0x001383E5
		[Event(4, Level = EventLevel.Informational)]
		internal void BufferTrimmed(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(4, bufferId, bufferSize, poolId);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x001391F1 File Offset: 0x001383F1
		[Event(5, Level = EventLevel.Informational)]
		internal void BufferTrimPoll(int milliseconds, int pressure)
		{
			base.WriteEvent(5, milliseconds, pressure);
		}

		// Token: 0x04000972 RID: 2418
		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		// Token: 0x02000248 RID: 584
		internal enum BufferAllocatedReason
		{
			// Token: 0x04000974 RID: 2420
			Pooled,
			// Token: 0x04000975 RID: 2421
			OverMaximumSize,
			// Token: 0x04000976 RID: 2422
			PoolExhausted
		}
	}
}
