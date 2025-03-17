using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200074C RID: 1868
	[EventSource(Guid = "5E5BB766-BBFC-5662-0548-1D44FAD9BB56", Name = "Microsoft-Windows-DotNETRuntime")]
	internal sealed class NativeRuntimeEventSource : EventSource
	{
		// Token: 0x06005BAC RID: 23468 RVA: 0x001BEA68 File Offset: 0x001BDC68
		private NativeRuntimeEventSource() : base(new Guid(1583069030U, 48124, 22114, 5, 72, 29, 68, 250, 217, 187, 86), "Microsoft-Windows-DotNETRuntime")
		{
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x001BEAAC File Offset: 0x001BDCAC
		[NonEvent]
		internal unsafe void ProcessEvent(uint eventID, uint osThreadID, DateTime timeStamp, Guid activityId, Guid childActivityId, ReadOnlySpan<byte> payload)
		{
			if ((ulong)eventID >= (ulong)((long)this.m_eventData.Length))
			{
				return;
			}
			object[] args = EventPipePayloadDecoder.DecodePayload(ref this.m_eventData[(int)eventID], payload);
			base.WriteToAllListeners((int)eventID, &osThreadID, &timeStamp, &activityId, &childActivityId, args);
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x001BEAF4 File Offset: 0x001BDCF4
		[Event(1, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCStart_V2(uint Count, uint Depth, uint Reason, uint Type, ushort ClrInstanceID, ulong ClientSequenceNumber)
		{
			base.WriteEvent(1, new object[]
			{
				Count,
				Depth,
				Reason,
				Type,
				ClrInstanceID,
				ClientSequenceNumber
			});
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x001BEB47 File Offset: 0x001BDD47
		[Event(2, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCEnd_V1(uint Count, uint Depth, ushort ClrInstanceID)
		{
			base.WriteEvent(2, (long)((ulong)Count), (long)((ulong)Depth), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x001BEB56 File Offset: 0x001BDD56
		[Event(3, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCRestartEEEnd_V1(ushort ClrInstanceID)
		{
			base.WriteEvent(3, (int)ClrInstanceID);
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x001BEB60 File Offset: 0x001BDD60
		[Event(4, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCHeapStats_V2(ulong GenerationSize0, ulong TotalPromotedSize0, ulong GenerationSize1, ulong TotalPromotedSize1, ulong GenerationSize2, ulong TotalPromotedSize2, ulong GenerationSize3, ulong TotalPromotedSize3, ulong FinalizationPromotedSize, ulong FinalizationPromotedCount, uint PinnedObjectCount, uint SinkBlockCount, uint GCHandleCount, ushort ClrInstanceID, ulong GenerationSize4, ulong TotalPromotedSize4)
		{
			base.WriteEvent(4, new object[]
			{
				GenerationSize0,
				TotalPromotedSize0,
				GenerationSize1,
				TotalPromotedSize1,
				GenerationSize2,
				TotalPromotedSize2,
				GenerationSize3,
				TotalPromotedSize3,
				FinalizationPromotedSize,
				FinalizationPromotedCount,
				PinnedObjectCount,
				SinkBlockCount,
				GCHandleCount,
				ClrInstanceID,
				GenerationSize4,
				TotalPromotedSize4
			});
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x001BEC1F File Offset: 0x001BDE1F
		[Event(5, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCCreateSegment_V1(ulong Address, ulong Size, uint Type, ushort ClrInstanceID)
		{
			base.WriteEvent(5, new object[]
			{
				Address,
				Size,
				Type,
				ClrInstanceID
			});
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x001BEC53 File Offset: 0x001BDE53
		[Event(6, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCFreeSegment_V1(ulong Address, ushort ClrInstanceID)
		{
			base.WriteEvent(6, new object[]
			{
				Address,
				ClrInstanceID
			});
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001BEC74 File Offset: 0x001BDE74
		[Event(7, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCRestartEEBegin_V1(ushort ClrInstanceID)
		{
			base.WriteEvent(7, (int)ClrInstanceID);
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x001BEC7E File Offset: 0x001BDE7E
		[Event(8, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCSuspendEEEnd_V1(ushort ClrInstanceID)
		{
			base.WriteEvent(8, (int)ClrInstanceID);
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x001BEC88 File Offset: 0x001BDE88
		[Event(9, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCSuspendEEBegin_V1(uint Reason, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(9, (long)((ulong)Reason), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x001BEC98 File Offset: 0x001BDE98
		[Event(10, Version = 3, Level = EventLevel.Verbose, Keywords = (EventKeywords)1L)]
		private void GCAllocationTick_V3(uint AllocationAmount, uint AllocationKind, ushort ClrInstanceID, ulong AllocationAmount64, IntPtr TypeID, string TypeName, uint HeapIndex, IntPtr Address)
		{
			base.WriteEvent(10, new object[]
			{
				AllocationAmount,
				AllocationKind,
				ClrInstanceID,
				AllocationAmount64,
				TypeID,
				TypeName,
				HeapIndex,
				Address
			});
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x001BECFB File Offset: 0x001BDEFB
		[Event(11, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)65537L)]
		private void GCCreateConcurrentThread_V1(ushort ClrInstanceID)
		{
			base.WriteEvent(11, (int)ClrInstanceID);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x001BED06 File Offset: 0x001BDF06
		[Event(12, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)65537L)]
		private void GCTerminateConcurrentThread_V1(ushort ClrInstanceID)
		{
			base.WriteEvent(12, (int)ClrInstanceID);
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x001BED11 File Offset: 0x001BDF11
		[Event(13, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCFinalizersEnd_V1(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(13, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x001BED1F File Offset: 0x001BDF1F
		[Event(14, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCFinalizersBegin_V1(ushort ClrInstanceID)
		{
			base.WriteEvent(14, (int)ClrInstanceID);
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x001BED2A File Offset: 0x001BDF2A
		[Event(15, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)524288L)]
		private void BulkType(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(15, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x001BED38 File Offset: 0x001BDF38
		[Event(16, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkRootEdge(uint Index, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(16, (long)((ulong)Index), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x001BED48 File Offset: 0x001BDF48
		[Event(17, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkRootConditionalWeakTableElementEdge(uint Index, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(17, (long)((ulong)Index), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x001BED58 File Offset: 0x001BDF58
		[Event(18, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkNode(uint Index, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(18, (long)((ulong)Index), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x001BED68 File Offset: 0x001BDF68
		[Event(19, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkEdge(uint Index, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(19, (long)((ulong)Index), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x001BED78 File Offset: 0x001BDF78
		[Event(20, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2097152L)]
		private void GCSampledObjectAllocationHigh(IntPtr Address, IntPtr TypeID, uint ObjectCountForTypeSample, ulong TotalSizeForTypeSample, ushort ClrInstanceID)
		{
			base.WriteEvent(20, new object[]
			{
				Address,
				TypeID,
				ObjectCountForTypeSample,
				TotalSizeForTypeSample,
				ClrInstanceID
			});
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x001BEDB7 File Offset: 0x001BDFB7
		[Event(21, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4194304L)]
		private void GCBulkSurvivingObjectRanges(uint Index, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(21, (long)((ulong)Index), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x001BEDC7 File Offset: 0x001BDFC7
		[Event(22, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4194304L)]
		private void GCBulkMovedObjectRanges(uint Index, uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(22, (long)((ulong)Index), (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x001BEDD7 File Offset: 0x001BDFD7
		[Event(23, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4194304L)]
		private void GCGenerationRange(byte Generation, IntPtr RangeStart, ulong RangeUsedLength, ulong RangeReservedLength, ushort ClrInstanceID)
		{
			base.WriteEvent(23, new object[]
			{
				Generation,
				RangeStart,
				RangeUsedLength,
				RangeReservedLength,
				ClrInstanceID
			});
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x001BEE16 File Offset: 0x001BE016
		[Event(25, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCMarkStackRoots(uint HeapNum, ushort ClrInstanceID)
		{
			base.WriteEvent(25, (long)((ulong)HeapNum), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x001BEE24 File Offset: 0x001BE024
		[Event(26, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCMarkFinalizeQueueRoots(uint HeapNum, ushort ClrInstanceID)
		{
			base.WriteEvent(26, (long)((ulong)HeapNum), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x001BEE32 File Offset: 0x001BE032
		[Event(27, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCMarkHandles(uint HeapNum, ushort ClrInstanceID)
		{
			base.WriteEvent(27, (long)((ulong)HeapNum), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x001BEE40 File Offset: 0x001BE040
		[Event(28, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCMarkOlderGenerationRoots(uint HeapNum, ushort ClrInstanceID)
		{
			base.WriteEvent(28, (long)((ulong)HeapNum), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x001BEE4E File Offset: 0x001BE04E
		[Event(29, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)1L)]
		private void FinalizeObject(IntPtr TypeID, IntPtr ObjectID, ushort ClrInstanceID)
		{
			base.WriteEvent(29, new object[]
			{
				TypeID,
				ObjectID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x001BEE7C File Offset: 0x001BE07C
		[Event(30, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2L)]
		private void SetGCHandle(IntPtr HandleID, IntPtr ObjectID, uint Kind, uint Generation, ulong AppDomainID, ushort ClrInstanceID)
		{
			base.WriteEvent(30, new object[]
			{
				HandleID,
				ObjectID,
				Kind,
				Generation,
				AppDomainID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x001BEED0 File Offset: 0x001BE0D0
		[Event(31, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2L)]
		private void DestroyGCHandle(IntPtr HandleID, ushort ClrInstanceID)
		{
			base.WriteEvent(31, new object[]
			{
				HandleID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x001BEEF2 File Offset: 0x001BE0F2
		[Event(32, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)33554432L)]
		private void GCSampledObjectAllocationLow(IntPtr Address, IntPtr TypeID, uint ObjectCountForTypeSample, ulong TotalSizeForTypeSample, ushort ClrInstanceID)
		{
			base.WriteEvent(32, new object[]
			{
				Address,
				TypeID,
				ObjectCountForTypeSample,
				TotalSizeForTypeSample,
				ClrInstanceID
			});
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x001BEF31 File Offset: 0x001BE131
		[Event(33, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)1L)]
		private void PinObjectAtGCTime(IntPtr HandleID, IntPtr ObjectID, ulong ObjectSize, string TypeName, ushort ClrInstanceID)
		{
			base.WriteEvent(33, new object[]
			{
				HandleID,
				ObjectID,
				ObjectSize,
				TypeName,
				ClrInstanceID
			});
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x001BEF6B File Offset: 0x001BE16B
		[Event(35, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCTriggered(uint Reason, ushort ClrInstanceID)
		{
			base.WriteEvent(35, (long)((ulong)Reason), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x001BEF79 File Offset: 0x001BE179
		[Event(36, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkRootCCW(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(36, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x001BEF87 File Offset: 0x001BE187
		[Event(37, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkRCW(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(37, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x001BEF95 File Offset: 0x001BE195
		[Event(38, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GCBulkRootStaticVar(uint Count, ulong AppDomainID, ushort ClrInstanceID)
		{
			base.WriteEvent(38, new object[]
			{
				Count,
				AppDomainID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x001BEFC0 File Offset: 0x001BE1C0
		[Event(39, Version = 0, Level = EventLevel.LogAlways, Keywords = (EventKeywords)66060291L)]
		private void GCDynamicEvent(string Name, uint DataSize)
		{
			base.WriteEvent(39, Name, (long)((ulong)DataSize));
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x001BEFCD File Offset: 0x001BE1CD
		[Event(40, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void WorkerThreadCreate(uint WorkerThreadCount, uint RetiredWorkerThreads)
		{
			base.WriteEvent(40, (long)((ulong)WorkerThreadCount), (long)((ulong)RetiredWorkerThreads));
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x001BEFDB File Offset: 0x001BE1DB
		[Event(41, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void WorkerThreadTerminate(uint WorkerThreadCount, uint RetiredWorkerThreads)
		{
			base.WriteEvent(41, (long)((ulong)WorkerThreadCount), (long)((ulong)RetiredWorkerThreads));
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x001BEFE9 File Offset: 0x001BE1E9
		[Event(42, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void WorkerThreadRetire(uint WorkerThreadCount, uint RetiredWorkerThreads)
		{
			base.WriteEvent(42, (long)((ulong)WorkerThreadCount), (long)((ulong)RetiredWorkerThreads));
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x001BEFF7 File Offset: 0x001BE1F7
		[Event(43, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void WorkerThreadUnretire(uint WorkerThreadCount, uint RetiredWorkerThreads)
		{
			base.WriteEvent(43, (long)((ulong)WorkerThreadCount), (long)((ulong)RetiredWorkerThreads));
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x001BF005 File Offset: 0x001BE205
		[Event(44, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void IOThreadCreate_V1(uint IOThreadCount, uint RetiredIOThreads, ushort ClrInstanceID)
		{
			base.WriteEvent(44, (long)((ulong)IOThreadCount), (long)((ulong)RetiredIOThreads), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x001BF015 File Offset: 0x001BE215
		[Event(45, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void IOThreadTerminate_V1(uint IOThreadCount, uint RetiredIOThreads, ushort ClrInstanceID)
		{
			base.WriteEvent(45, (long)((ulong)IOThreadCount), (long)((ulong)RetiredIOThreads), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x001BF025 File Offset: 0x001BE225
		[Event(46, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void IOThreadRetire_V1(uint IOThreadCount, uint RetiredIOThreads, ushort ClrInstanceID)
		{
			base.WriteEvent(46, (long)((ulong)IOThreadCount), (long)((ulong)RetiredIOThreads), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x001BF035 File Offset: 0x001BE235
		[Event(47, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void IOThreadUnretire_V1(uint IOThreadCount, uint RetiredIOThreads, ushort ClrInstanceID)
		{
			base.WriteEvent(47, (long)((ulong)IOThreadCount), (long)((ulong)RetiredIOThreads), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x001BF045 File Offset: 0x001BE245
		[Event(48, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadpoolSuspensionSuspendThread(uint ClrThreadID, uint CpuUtilization)
		{
			base.WriteEvent(48, (long)((ulong)ClrThreadID), (long)((ulong)CpuUtilization));
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x001BF053 File Offset: 0x001BE253
		[Event(49, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadpoolSuspensionResumeThread(uint ClrThreadID, uint CpuUtilization)
		{
			base.WriteEvent(49, (long)((ulong)ClrThreadID), (long)((ulong)CpuUtilization));
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x001BF061 File Offset: 0x001BE261
		[Event(50, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadStart(uint ActiveWorkerThreadCount, uint RetiredWorkerThreadCount, ushort ClrInstanceID)
		{
			base.WriteEvent(50, (long)((ulong)ActiveWorkerThreadCount), (long)((ulong)RetiredWorkerThreadCount), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x001BF071 File Offset: 0x001BE271
		[Event(51, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadStop(uint ActiveWorkerThreadCount, uint RetiredWorkerThreadCount, ushort ClrInstanceID)
		{
			base.WriteEvent(51, (long)((ulong)ActiveWorkerThreadCount), (long)((ulong)RetiredWorkerThreadCount), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x001BF081 File Offset: 0x001BE281
		[Event(52, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadRetirementStart(uint ActiveWorkerThreadCount, uint RetiredWorkerThreadCount, ushort ClrInstanceID)
		{
			base.WriteEvent(52, (long)((ulong)ActiveWorkerThreadCount), (long)((ulong)RetiredWorkerThreadCount), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x001BF091 File Offset: 0x001BE291
		[Event(53, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadRetirementStop(uint ActiveWorkerThreadCount, uint RetiredWorkerThreadCount, ushort ClrInstanceID)
		{
			base.WriteEvent(53, (long)((ulong)ActiveWorkerThreadCount), (long)((ulong)RetiredWorkerThreadCount), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x001BF0A1 File Offset: 0x001BE2A1
		[Event(54, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadAdjustmentSample(double Throughput, ushort ClrInstanceID)
		{
			base.WriteEvent(54, new object[]
			{
				Throughput,
				ClrInstanceID
			});
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x001BF0C3 File Offset: 0x001BE2C3
		[Event(55, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadAdjustmentAdjustment(double AverageThroughput, uint NewWorkerThreadCount, uint Reason, ushort ClrInstanceID)
		{
			base.WriteEvent(55, new object[]
			{
				AverageThroughput,
				NewWorkerThreadCount,
				Reason,
				ClrInstanceID
			});
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x001BF0F8 File Offset: 0x001BE2F8
		[Event(56, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadAdjustmentStats(double Duration, double Throughput, double ThreadWave, double ThroughputWave, double ThroughputErrorEstimate, double AverageThroughputErrorEstimate, double ThroughputRatio, double Confidence, double NewControlSetting, ushort NewThreadWaveMagnitude, ushort ClrInstanceID)
		{
			base.WriteEvent(56, new object[]
			{
				Duration,
				Throughput,
				ThreadWave,
				ThroughputWave,
				ThroughputErrorEstimate,
				AverageThroughputErrorEstimate,
				ThroughputRatio,
				Confidence,
				NewControlSetting,
				NewThreadWaveMagnitude,
				ClrInstanceID
			});
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x001BF181 File Offset: 0x001BE381
		[Event(57, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkerThreadWait(uint ActiveWorkerThreadCount, uint RetiredWorkerThreadCount, ushort ClrInstanceID)
		{
			base.WriteEvent(57, (long)((ulong)ActiveWorkerThreadCount), (long)((ulong)RetiredWorkerThreadCount), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x001BF191 File Offset: 0x001BE391
		[Event(60, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolWorkingThreadCount(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(60, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x001BF19F File Offset: 0x001BE39F
		[Event(61, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)2147549184L)]
		private void ThreadPoolEnqueue(IntPtr WorkID, ushort ClrInstanceID)
		{
			base.WriteEvent(61, new object[]
			{
				WorkID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x001BF1C1 File Offset: 0x001BE3C1
		[Event(62, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)2147549184L)]
		private void ThreadPoolDequeue(IntPtr WorkID, ushort ClrInstanceID)
		{
			base.WriteEvent(62, new object[]
			{
				WorkID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x001BF1E3 File Offset: 0x001BE3E3
		[Event(63, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)2147549184L)]
		private void ThreadPoolIOEnqueue(IntPtr NativeOverlapped, IntPtr Overlapped, bool MultiDequeues, ushort ClrInstanceID)
		{
			base.WriteEvent(63, new object[]
			{
				NativeOverlapped,
				Overlapped,
				MultiDequeues,
				ClrInstanceID
			});
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x001BF218 File Offset: 0x001BE418
		[Event(64, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)2147549184L)]
		private void ThreadPoolIODequeue(IntPtr NativeOverlapped, IntPtr Overlapped, ushort ClrInstanceID)
		{
			base.WriteEvent(64, new object[]
			{
				NativeOverlapped,
				Overlapped,
				ClrInstanceID
			});
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x001BF243 File Offset: 0x001BE443
		[Event(65, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)65536L)]
		private void ThreadPoolIOPack(IntPtr NativeOverlapped, IntPtr Overlapped, ushort ClrInstanceID)
		{
			base.WriteEvent(65, new object[]
			{
				NativeOverlapped,
				Overlapped,
				ClrInstanceID
			});
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x001BF26E File Offset: 0x001BE46E
		[Event(70, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2147549184L)]
		private void ThreadCreating(IntPtr ID, ushort ClrInstanceID)
		{
			base.WriteEvent(70, new object[]
			{
				ID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x001BF290 File Offset: 0x001BE490
		[Event(71, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2147549184L)]
		private void ThreadRunning(IntPtr ID, ushort ClrInstanceID)
		{
			base.WriteEvent(71, new object[]
			{
				ID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x001BF2B2 File Offset: 0x001BE4B2
		[Event(72, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)274877906944L)]
		private void MethodDetails(ulong MethodID, ulong TypeID, uint MethodToken, uint TypeParameterCount, ulong LoaderModuleID)
		{
			base.WriteEvent(72, new object[]
			{
				MethodID,
				TypeID,
				MethodToken,
				TypeParameterCount,
				LoaderModuleID
			});
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x001BF2F1 File Offset: 0x001BE4F1
		[Event(73, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)549755813888L)]
		private void TypeLoadStart(uint TypeLoadStartID, ushort ClrInstanceID)
		{
			base.WriteEvent(73, (long)((ulong)TypeLoadStartID), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x001BF2FF File Offset: 0x001BE4FF
		[Event(74, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)549755813888L)]
		private void TypeLoadStop(uint TypeLoadStartID, ushort ClrInstanceID, ushort LoadLevel, ulong TypeID, string TypeName)
		{
			base.WriteEvent(74, new object[]
			{
				TypeLoadStartID,
				ClrInstanceID,
				LoadLevel,
				TypeID,
				TypeName
			});
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x001BF339 File Offset: 0x001BE539
		[Event(80, Version = 1, Level = EventLevel.Error, Keywords = (EventKeywords)8589967360L)]
		private void ExceptionThrown_V1(string ExceptionType, string ExceptionMessage, IntPtr ExceptionEIP, uint ExceptionHRESULT, ushort ExceptionFlags, ushort ClrInstanceID)
		{
			base.WriteEvent(80, new object[]
			{
				ExceptionType,
				ExceptionMessage,
				ExceptionEIP,
				ExceptionHRESULT,
				ExceptionFlags,
				ClrInstanceID
			});
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x001BF378 File Offset: 0x001BE578
		[Event(250, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionCatchStart(ulong EntryEIP, ulong MethodID, string MethodName, ushort ClrInstanceID)
		{
			base.WriteEvent(250, new object[]
			{
				EntryEIP,
				MethodID,
				MethodName,
				ClrInstanceID
			});
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x001BF3AB File Offset: 0x001BE5AB
		[Event(251, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionCatchStop()
		{
			base.WriteEvent(251);
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x001BF3B8 File Offset: 0x001BE5B8
		[Event(252, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionFinallyStart(ulong EntryEIP, ulong MethodID, string MethodName, ushort ClrInstanceID)
		{
			base.WriteEvent(252, new object[]
			{
				EntryEIP,
				MethodID,
				MethodName,
				ClrInstanceID
			});
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x001BF3EB File Offset: 0x001BE5EB
		[Event(253, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionFinallyStop()
		{
			base.WriteEvent(253);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x001BF3F8 File Offset: 0x001BE5F8
		[Event(254, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionFilterStart(ulong EntryEIP, ulong MethodID, string MethodName, ushort ClrInstanceID)
		{
			base.WriteEvent(254, new object[]
			{
				EntryEIP,
				MethodID,
				MethodName,
				ClrInstanceID
			});
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x001BF42B File Offset: 0x001BE62B
		[Event(255, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionFilterStop()
		{
			base.WriteEvent(255);
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x001BF438 File Offset: 0x001BE638
		[Event(256, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)32768L)]
		private void ExceptionThrownStop()
		{
			base.WriteEvent(256);
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x001BF445 File Offset: 0x001BE645
		[Event(81, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)16384L)]
		private void ContentionStart_V1(byte ContentionFlags, ushort ClrInstanceID)
		{
			base.WriteEvent(81, (int)ContentionFlags, (int)ClrInstanceID);
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x001BF451 File Offset: 0x001BE651
		[Event(91, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)16384L)]
		private void ContentionStop_V1(byte ContentionFlags, ushort ClrInstanceID, double DurationNs)
		{
			base.WriteEvent(91, new object[]
			{
				ContentionFlags,
				ClrInstanceID,
				DurationNs
			});
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x001BF47C File Offset: 0x001BE67C
		[Event(82, Version = 0, Level = EventLevel.LogAlways, Keywords = (EventKeywords)1073741824L)]
		private void CLRStackWalk(ushort ClrInstanceID, byte Reserved1, byte Reserved2, uint FrameCount)
		{
			base.WriteEvent(82, new object[]
			{
				ClrInstanceID,
				Reserved1,
				Reserved2,
				FrameCount
			});
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x001BF4B1 File Offset: 0x001BE6B1
		[Event(83, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2048L)]
		private void AppDomainMemAllocated(ulong AppDomainID, ulong Allocated, ushort ClrInstanceID)
		{
			base.WriteEvent(83, new object[]
			{
				AppDomainID,
				Allocated,
				ClrInstanceID
			});
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x001BF4DC File Offset: 0x001BE6DC
		[Event(84, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)2048L)]
		private void AppDomainMemSurvived(ulong AppDomainID, ulong Survived, ulong ProcessSurvived, ushort ClrInstanceID)
		{
			base.WriteEvent(84, new object[]
			{
				AppDomainID,
				Survived,
				ProcessSurvived,
				ClrInstanceID
			});
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x001BF514 File Offset: 0x001BE714
		[Event(85, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)67584L)]
		private void ThreadCreated(ulong ManagedThreadID, ulong AppDomainID, uint Flags, uint ManagedThreadIndex, uint OSThreadID, ushort ClrInstanceID)
		{
			base.WriteEvent(85, new object[]
			{
				ManagedThreadID,
				AppDomainID,
				Flags,
				ManagedThreadIndex,
				OSThreadID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x001BF568 File Offset: 0x001BE768
		[Event(86, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)67584L)]
		private void ThreadTerminated(ulong ManagedThreadID, ulong AppDomainID, ushort ClrInstanceID)
		{
			base.WriteEvent(86, new object[]
			{
				ManagedThreadID,
				AppDomainID,
				ClrInstanceID
			});
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x001BF593 File Offset: 0x001BE793
		[Event(87, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)67584L)]
		private void ThreadDomainEnter(ulong ManagedThreadID, ulong AppDomainID, ushort ClrInstanceID)
		{
			base.WriteEvent(87, new object[]
			{
				ManagedThreadID,
				AppDomainID,
				ClrInstanceID
			});
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x001BF5C0 File Offset: 0x001BE7C0
		[Event(88, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)8192L)]
		private void ILStubGenerated(ushort ClrInstanceID, ulong ModuleID, ulong StubMethodID, uint StubFlags, uint ManagedInteropMethodToken, string ManagedInteropMethodNamespace, string ManagedInteropMethodName, string ManagedInteropMethodSignature, string NativeMethodSignature, string StubMethodSignature, string StubMethodILCode)
		{
			base.WriteEvent(88, new object[]
			{
				ClrInstanceID,
				ModuleID,
				StubMethodID,
				StubFlags,
				ManagedInteropMethodToken,
				ManagedInteropMethodNamespace,
				ManagedInteropMethodName,
				ManagedInteropMethodSignature,
				NativeMethodSignature,
				StubMethodSignature,
				StubMethodILCode
			});
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x001BF62C File Offset: 0x001BE82C
		[Event(89, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)8192L)]
		private void ILStubCacheHit(ushort ClrInstanceID, ulong ModuleID, ulong StubMethodID, uint ManagedInteropMethodToken, string ManagedInteropMethodNamespace, string ManagedInteropMethodName, string ManagedInteropMethodSignature)
		{
			base.WriteEvent(89, new object[]
			{
				ClrInstanceID,
				ModuleID,
				StubMethodID,
				ManagedInteropMethodToken,
				ManagedInteropMethodNamespace,
				ManagedInteropMethodName,
				ManagedInteropMethodSignature
			});
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x001BF67B File Offset: 0x001BE87B
		[Event(135, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void DCStartCompleteV2()
		{
			base.WriteEvent(135);
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x001BF688 File Offset: 0x001BE888
		[Event(136, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void DCEndCompleteV2()
		{
			base.WriteEvent(136);
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x001BF698 File Offset: 0x001BE898
		[Event(137, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodDCStartV2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags)
		{
			base.WriteEvent(137, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags
			});
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x001BF6F0 File Offset: 0x001BE8F0
		[Event(138, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodDCEndV2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags)
		{
			base.WriteEvent(138, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags
			});
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x001BF748 File Offset: 0x001BE948
		[Event(139, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodDCStartVerboseV2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags, string MethodNamespace, string MethodName, string MethodSignature)
		{
			base.WriteEvent(139, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags,
				MethodNamespace,
				MethodName,
				MethodSignature
			});
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x001BF7B0 File Offset: 0x001BE9B0
		[Event(140, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodDCEndVerboseV2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags, string MethodNamespace, string MethodName, string MethodSignature)
		{
			base.WriteEvent(140, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags,
				MethodNamespace,
				MethodName,
				MethodSignature
			});
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x001BF818 File Offset: 0x001BEA18
		[Event(141, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodLoad_V2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags, ushort ClrInstanceID, ulong ReJITID)
		{
			base.WriteEvent(141, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags,
				ClrInstanceID,
				ReJITID
			});
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x001BF883 File Offset: 0x001BEA83
		[Event(159, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)137438953472L)]
		private void R2RGetEntryPoint(ulong MethodID, string MethodNamespace, string MethodName, string MethodSignature, ulong EntryPoint, ushort ClrInstanceID)
		{
			base.WriteEvent(159, new object[]
			{
				MethodID,
				MethodNamespace,
				MethodName,
				MethodSignature,
				EntryPoint,
				ClrInstanceID
			});
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x001BF8C0 File Offset: 0x001BEAC0
		[Event(160, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)137438953472L)]
		private void R2RGetEntryPointStart(ulong MethodID, ushort ClrInstanceID)
		{
			base.WriteEvent(160, new object[]
			{
				MethodID,
				ClrInstanceID
			});
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x001BF8E8 File Offset: 0x001BEAE8
		[Event(142, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodUnload_V2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags, ushort ClrInstanceID, ulong ReJITID)
		{
			base.WriteEvent(142, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags,
				ClrInstanceID,
				ReJITID
			});
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x001BF954 File Offset: 0x001BEB54
		[Event(143, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodLoadVerbose_V2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags, string MethodNamespace, string MethodName, string MethodSignature, ushort ClrInstanceID, ulong ReJITID)
		{
			base.WriteEvent(143, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags,
				MethodNamespace,
				MethodName,
				MethodSignature,
				ClrInstanceID,
				ReJITID
			});
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x001BF9D4 File Offset: 0x001BEBD4
		[Event(144, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)48L)]
		private void MethodUnloadVerbose_V2(ulong MethodID, ulong ModuleID, ulong MethodStartAddress, uint MethodSize, uint MethodToken, uint MethodFlags, string MethodNamespace, string MethodName, string MethodSignature, ushort ClrInstanceID, ulong ReJITID)
		{
			base.WriteEvent(144, new object[]
			{
				MethodID,
				ModuleID,
				MethodStartAddress,
				MethodSize,
				MethodToken,
				MethodFlags,
				MethodNamespace,
				MethodName,
				MethodSignature,
				ClrInstanceID,
				ReJITID
			});
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x001BFA54 File Offset: 0x001BEC54
		[Event(145, Version = 1, Level = EventLevel.Verbose, Keywords = (EventKeywords)16L)]
		private void MethodJittingStarted_V1(ulong MethodID, ulong ModuleID, uint MethodToken, uint MethodILSize, string MethodNamespace, string MethodName, string MethodSignature, ushort ClrInstanceID)
		{
			base.WriteEvent(145, new object[]
			{
				MethodID,
				ModuleID,
				MethodToken,
				MethodILSize,
				MethodNamespace,
				MethodName,
				MethodSignature,
				ClrInstanceID
			});
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x001BFAB0 File Offset: 0x001BECB0
		[Event(185, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)4096L)]
		private void MethodJitInliningSucceeded(string MethodBeingCompiledNamespace, string MethodBeingCompiledName, string MethodBeingCompiledNameSignature, string InlinerNamespace, string InlinerName, string InlinerNameSignature, string InlineeNamespace, string InlineeName, string InlineeNameSignature, ushort ClrInstanceID)
		{
			base.WriteEvent(185, new object[]
			{
				MethodBeingCompiledNamespace,
				MethodBeingCompiledName,
				MethodBeingCompiledNameSignature,
				InlinerNamespace,
				InlinerName,
				InlinerNameSignature,
				InlineeNamespace,
				InlineeName,
				InlineeNameSignature,
				ClrInstanceID
			});
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x001BFB04 File Offset: 0x001BED04
		[Event(186, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)4096L)]
		private void MethodJitInliningFailedAnsi(string MethodBeingCompiledNamespace, string MethodBeingCompiledName, string MethodBeingCompiledNameSignature, string InlinerNamespace, string InlinerName, string InlinerNameSignature, string InlineeNamespace, string InlineeName, string InlineeNameSignature, bool FailAlways)
		{
			base.WriteEvent(186, new object[]
			{
				MethodBeingCompiledNamespace,
				MethodBeingCompiledName,
				MethodBeingCompiledNameSignature,
				InlinerNamespace,
				InlinerName,
				InlinerNameSignature,
				InlineeNamespace,
				InlineeName,
				InlineeNameSignature,
				FailAlways
			});
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x001BFB58 File Offset: 0x001BED58
		[Event(188, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)4096L)]
		private void MethodJitTailCallSucceeded(string MethodBeingCompiledNamespace, string MethodBeingCompiledName, string MethodBeingCompiledNameSignature, string CallerNamespace, string CallerName, string CallerNameSignature, string CalleeNamespace, string CalleeName, string CalleeNameSignature, bool TailPrefix, uint TailCallType, ushort ClrInstanceID)
		{
			base.WriteEvent(188, new object[]
			{
				MethodBeingCompiledNamespace,
				MethodBeingCompiledName,
				MethodBeingCompiledNameSignature,
				CallerNamespace,
				CallerName,
				CallerNameSignature,
				CalleeNamespace,
				CalleeName,
				CalleeNameSignature,
				TailPrefix,
				TailCallType,
				ClrInstanceID
			});
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x001BFBC4 File Offset: 0x001BEDC4
		[Event(189, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)4096L)]
		private void MethodJitTailCallFailedAnsi(string MethodBeingCompiledNamespace, string MethodBeingCompiledName, string MethodBeingCompiledNameSignature, string CallerNamespace, string CallerName, string CallerNameSignature, string CalleeNamespace, string CalleeName, string CalleeNameSignature, bool TailPrefix)
		{
			base.WriteEvent(189, new object[]
			{
				MethodBeingCompiledNamespace,
				MethodBeingCompiledName,
				MethodBeingCompiledNameSignature,
				CallerNamespace,
				CallerName,
				CallerNameSignature,
				CalleeNamespace,
				CalleeName,
				CalleeNameSignature,
				TailPrefix
			});
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x001BFC18 File Offset: 0x001BEE18
		[Event(190, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)131072L)]
		private void MethodILToNativeMap(ulong MethodID, ulong ReJITID, byte MethodExtent, ushort CountOfMapEntries)
		{
			base.WriteEvent(190, new object[]
			{
				MethodID,
				ReJITID,
				MethodExtent,
				CountOfMapEntries
			});
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x001BFC50 File Offset: 0x001BEE50
		[Event(191, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)4096L)]
		private void MethodJitTailCallFailed(string MethodBeingCompiledNamespace, string MethodBeingCompiledName, string MethodBeingCompiledNameSignature, string CallerNamespace, string CallerName, string CallerNameSignature, string CalleeNamespace, string CalleeName, string CalleeNameSignature, bool TailPrefix, string FailReason, ushort ClrInstanceID)
		{
			base.WriteEvent(191, new object[]
			{
				MethodBeingCompiledNamespace,
				MethodBeingCompiledName,
				MethodBeingCompiledNameSignature,
				CallerNamespace,
				CallerName,
				CallerNameSignature,
				CalleeNamespace,
				CalleeName,
				CalleeNameSignature,
				TailPrefix,
				FailReason,
				ClrInstanceID
			});
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x001BFCB8 File Offset: 0x001BEEB8
		[Event(192, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)4096L)]
		private void MethodJitInliningFailed(string MethodBeingCompiledNamespace, string MethodBeingCompiledName, string MethodBeingCompiledNameSignature, string InlinerNamespace, string InlinerName, string InlinerNameSignature, string InlineeNamespace, string InlineeName, string InlineeNameSignature, bool FailAlways, string FailReason, ushort ClrInstanceID)
		{
			base.WriteEvent(192, new object[]
			{
				MethodBeingCompiledNamespace,
				MethodBeingCompiledName,
				MethodBeingCompiledNameSignature,
				InlinerNamespace,
				InlinerName,
				InlinerNameSignature,
				InlineeNamespace,
				InlineeName,
				InlineeNameSignature,
				FailAlways,
				FailReason,
				ClrInstanceID
			});
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x001BFD20 File Offset: 0x001BEF20
		[Event(149, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void ModuleDCStartV2(ulong ModuleID, ulong AssemblyID, uint ModuleFlags, uint Reserved1, string ModuleILPath, string ModuleNativePath)
		{
			base.WriteEvent(149, new object[]
			{
				ModuleID,
				AssemblyID,
				ModuleFlags,
				Reserved1,
				ModuleILPath,
				ModuleNativePath
			});
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x001BFD70 File Offset: 0x001BEF70
		[Event(150, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void ModuleDCEndV2(ulong ModuleID, ulong AssemblyID, uint ModuleFlags, uint Reserved1, string ModuleILPath, string ModuleNativePath)
		{
			base.WriteEvent(150, new object[]
			{
				ModuleID,
				AssemblyID,
				ModuleFlags,
				Reserved1,
				ModuleILPath,
				ModuleNativePath
			});
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x001BFDC0 File Offset: 0x001BEFC0
		[Event(151, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void DomainModuleLoad_V1(ulong ModuleID, ulong AssemblyID, ulong AppDomainID, uint ModuleFlags, uint Reserved1, string ModuleILPath, string ModuleNativePath, ushort ClrInstanceID)
		{
			base.WriteEvent(151, new object[]
			{
				ModuleID,
				AssemblyID,
				AppDomainID,
				ModuleFlags,
				Reserved1,
				ModuleILPath,
				ModuleNativePath,
				ClrInstanceID
			});
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x001BFE24 File Offset: 0x001BF024
		[Event(152, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)536870920L)]
		private void ModuleLoad_V2(ulong ModuleID, ulong AssemblyID, uint ModuleFlags, uint Reserved1, string ModuleILPath, string ModuleNativePath, ushort ClrInstanceID, Guid ManagedPdbSignature, uint ManagedPdbAge, string ManagedPdbBuildPath, Guid NativePdbSignature, uint NativePdbAge, string NativePdbBuildPath)
		{
			base.WriteEvent(152, new object[]
			{
				ModuleID,
				AssemblyID,
				ModuleFlags,
				Reserved1,
				ModuleILPath,
				ModuleNativePath,
				ClrInstanceID,
				ManagedPdbSignature,
				ManagedPdbAge,
				ManagedPdbBuildPath,
				NativePdbSignature,
				NativePdbAge,
				NativePdbBuildPath
			});
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x001BFEB4 File Offset: 0x001BF0B4
		[Event(153, Version = 2, Level = EventLevel.Informational, Keywords = (EventKeywords)536870920L)]
		private void ModuleUnload_V2(ulong ModuleID, ulong AssemblyID, uint ModuleFlags, uint Reserved1, string ModuleILPath, string ModuleNativePath, ushort ClrInstanceID, Guid ManagedPdbSignature, uint ManagedPdbAge, string ManagedPdbBuildPath, Guid NativePdbSignature, uint NativePdbAge, string NativePdbBuildPath)
		{
			base.WriteEvent(153, new object[]
			{
				ModuleID,
				AssemblyID,
				ModuleFlags,
				Reserved1,
				ModuleILPath,
				ModuleNativePath,
				ClrInstanceID,
				ManagedPdbSignature,
				ManagedPdbAge,
				ManagedPdbBuildPath,
				NativePdbSignature,
				NativePdbAge,
				NativePdbBuildPath
			});
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x001BFF44 File Offset: 0x001BF144
		[Event(154, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void AssemblyLoad_V1(ulong AssemblyID, ulong AppDomainID, ulong BindingID, uint AssemblyFlags, string FullyQualifiedAssemblyName, ushort ClrInstanceID)
		{
			base.WriteEvent(154, new object[]
			{
				AssemblyID,
				AppDomainID,
				BindingID,
				AssemblyFlags,
				FullyQualifiedAssemblyName,
				ClrInstanceID
			});
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x001BFF98 File Offset: 0x001BF198
		[Event(155, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void AssemblyUnload_V1(ulong AssemblyID, ulong AppDomainID, ulong BindingID, uint AssemblyFlags, string FullyQualifiedAssemblyName, ushort ClrInstanceID)
		{
			base.WriteEvent(155, new object[]
			{
				AssemblyID,
				AppDomainID,
				BindingID,
				AssemblyFlags,
				FullyQualifiedAssemblyName,
				ClrInstanceID
			});
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x001BFFEA File Offset: 0x001BF1EA
		[Event(156, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void AppDomainLoad_V1(ulong AppDomainID, uint AppDomainFlags, string AppDomainName, uint AppDomainIndex, ushort ClrInstanceID)
		{
			base.WriteEvent(156, new object[]
			{
				AppDomainID,
				AppDomainFlags,
				AppDomainName,
				AppDomainIndex,
				ClrInstanceID
			});
		}

		// Token: 0x06005C1E RID: 23582 RVA: 0x001C0027 File Offset: 0x001BF227
		[Event(157, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		private void AppDomainUnload_V1(ulong AppDomainID, uint AppDomainFlags, string AppDomainName, uint AppDomainIndex, ushort ClrInstanceID)
		{
			base.WriteEvent(157, new object[]
			{
				AppDomainID,
				AppDomainFlags,
				AppDomainName,
				AppDomainIndex,
				ClrInstanceID
			});
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x001C0064 File Offset: 0x001BF264
		[Event(158, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)536870912L)]
		private void ModuleRangeLoad(ushort ClrInstanceID, ulong ModuleID, uint RangeBegin, uint RangeSize, byte RangeType)
		{
			base.WriteEvent(158, new object[]
			{
				ClrInstanceID,
				ModuleID,
				RangeBegin,
				RangeSize,
				RangeType
			});
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x001C00B1 File Offset: 0x001BF2B1
		[Event(181, Version = 1, Level = EventLevel.Verbose, Keywords = (EventKeywords)1024L)]
		private void StrongNameVerificationStart_V1(uint VerificationFlags, uint ErrorCode, string FullyQualifiedAssemblyName, ushort ClrInstanceID)
		{
			base.WriteEvent(181, new object[]
			{
				VerificationFlags,
				ErrorCode,
				FullyQualifiedAssemblyName,
				ClrInstanceID
			});
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x001C00E4 File Offset: 0x001BF2E4
		[Event(182, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1024L)]
		private void StrongNameVerificationStop_V1(uint VerificationFlags, uint ErrorCode, string FullyQualifiedAssemblyName, ushort ClrInstanceID)
		{
			base.WriteEvent(182, new object[]
			{
				VerificationFlags,
				ErrorCode,
				FullyQualifiedAssemblyName,
				ClrInstanceID
			});
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x001C0117 File Offset: 0x001BF317
		[Event(183, Version = 1, Level = EventLevel.Verbose, Keywords = (EventKeywords)1024L)]
		private void AuthenticodeVerificationStart_V1(uint VerificationFlags, uint ErrorCode, string ModulePath, ushort ClrInstanceID)
		{
			base.WriteEvent(183, new object[]
			{
				VerificationFlags,
				ErrorCode,
				ModulePath,
				ClrInstanceID
			});
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x001C014A File Offset: 0x001BF34A
		[Event(184, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)1024L)]
		private void AuthenticodeVerificationStop_V1(uint VerificationFlags, uint ErrorCode, string ModulePath, ushort ClrInstanceID)
		{
			base.WriteEvent(184, new object[]
			{
				VerificationFlags,
				ErrorCode,
				ModulePath,
				ClrInstanceID
			});
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x001C0180 File Offset: 0x001BF380
		[Event(187, Version = 0, Level = EventLevel.Informational)]
		private void RuntimeInformationStart(ushort ClrInstanceID, ushort Sku, ushort BclMajorVersion, ushort BclMinorVersion, ushort BclBuildNumber, ushort BclQfeNumber, ushort VMMajorVersion, ushort VMMinorVersion, ushort VMBuildNumber, ushort VMQfeNumber, uint StartupFlags, byte StartupMode, string CommandLine, Guid ComObjectGuid, string RuntimeDllPath)
		{
			base.WriteEvent(187, new object[]
			{
				ClrInstanceID,
				Sku,
				BclMajorVersion,
				BclMinorVersion,
				BclBuildNumber,
				BclQfeNumber,
				VMMajorVersion,
				VMMinorVersion,
				VMBuildNumber,
				VMQfeNumber,
				StartupFlags,
				StartupMode,
				CommandLine,
				ComObjectGuid,
				RuntimeDllPath
			});
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x001C022E File Offset: 0x001BF42E
		[Event(200, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)1L)]
		private void IncreaseMemoryPressure(ulong BytesAllocated, ushort ClrInstanceID)
		{
			base.WriteEvent(200, new object[]
			{
				BytesAllocated,
				ClrInstanceID
			});
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x001C0253 File Offset: 0x001BF453
		[Event(201, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)1L)]
		private void DecreaseMemoryPressure(ulong BytesFreed, ushort ClrInstanceID)
		{
			base.WriteEvent(201, new object[]
			{
				BytesFreed,
				ClrInstanceID
			});
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x001C0278 File Offset: 0x001BF478
		[Event(202, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCMarkWithType(uint HeapNum, ushort ClrInstanceID, uint Type, ulong Bytes)
		{
			base.WriteEvent(202, new object[]
			{
				HeapNum,
				ClrInstanceID,
				Type,
				Bytes
			});
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x001C02B0 File Offset: 0x001BF4B0
		[Event(203, Version = 2, Level = EventLevel.Verbose, Keywords = (EventKeywords)1L)]
		private void GCJoin_V2(uint Heap, uint JoinTime, uint JoinType, ushort ClrInstanceID, uint JoinID)
		{
			base.WriteEvent(203, new object[]
			{
				Heap,
				JoinTime,
				JoinType,
				ClrInstanceID,
				JoinID
			});
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x001C0300 File Offset: 0x001BF500
		[Event(204, Version = 3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCPerHeapHistory_V3(ushort ClrInstanceID, IntPtr FreeListAllocated, IntPtr FreeListRejected, IntPtr EndOfSegAllocated, IntPtr CondemnedAllocated, IntPtr PinnedAllocated, IntPtr PinnedAllocatedAdvance, uint RunningFreeListEfficiency, uint CondemnReasons0, uint CondemnReasons1, uint CompactMechanisms, uint ExpandMechanisms, uint HeapIndex, IntPtr ExtraGen0Commit, uint Count)
		{
			base.WriteEvent(204, new object[]
			{
				ClrInstanceID,
				FreeListAllocated,
				FreeListRejected,
				EndOfSegAllocated,
				CondemnedAllocated,
				PinnedAllocated,
				PinnedAllocatedAdvance,
				RunningFreeListEfficiency,
				CondemnReasons0,
				CondemnReasons1,
				CompactMechanisms,
				ExpandMechanisms,
				HeapIndex,
				ExtraGen0Commit,
				Count
			});
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x001C03B8 File Offset: 0x001BF5B8
		[Event(205, Version = 3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void GCGlobalHeapHistory_V3(ulong FinalYoungestDesired, int NumHeaps, uint CondemnedGeneration, uint Gen0ReductionCount, uint Reason, uint GlobalMechanisms, ushort ClrInstanceID, uint PauseMode, uint MemoryPressure, uint CondemnReasons0, uint CondemnReasons1)
		{
			base.WriteEvent(205, new object[]
			{
				FinalYoungestDesired,
				NumHeaps,
				CondemnedGeneration,
				Gen0ReductionCount,
				Reason,
				GlobalMechanisms,
				ClrInstanceID,
				PauseMode,
				MemoryPressure,
				CondemnReasons0,
				CondemnReasons1
			});
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x001C0444 File Offset: 0x001BF644
		[Event(206, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GenAwareBegin(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(206, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x001C0455 File Offset: 0x001BF655
		[Event(207, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)1048576L)]
		private void GenAwareEnd(uint Count, ushort ClrInstanceID)
		{
			base.WriteEvent(207, (long)((ulong)Count), (long)((ulong)ClrInstanceID));
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x001C0466 File Offset: 0x001BF666
		[Event(240, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4294967296L)]
		private void DebugIPCEventStart()
		{
			base.WriteEvent(240);
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x001C0473 File Offset: 0x001BF673
		[Event(241, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4294967296L)]
		private void DebugIPCEventEnd()
		{
			base.WriteEvent(241);
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x001C0480 File Offset: 0x001BF680
		[Event(242, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4294967296L)]
		private void DebugExceptionProcessingStart()
		{
			base.WriteEvent(242);
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x001C048D File Offset: 0x001BF68D
		[Event(243, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4294967296L)]
		private void DebugExceptionProcessingEnd()
		{
			base.WriteEvent(243);
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x001C049A File Offset: 0x001BF69A
		[Event(260, Version = 0, Level = EventLevel.Verbose, Keywords = (EventKeywords)17179869184L)]
		private void CodeSymbols(ulong ModuleId, ushort TotalChunks, ushort ChunkNumber, uint ChunkLength)
		{
			base.WriteEvent(260, new object[]
			{
				ModuleId,
				TotalChunks,
				ChunkNumber,
				ChunkLength
			});
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x001C04D2 File Offset: 0x001BF6D2
		[Event(270, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)34359738368L)]
		private void EventSource(int EventID, string EventName, string EventSourceName, string Payload)
		{
			base.WriteEvent(270, new object[]
			{
				EventID,
				EventName,
				EventSourceName,
				Payload
			});
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x001C04FB File Offset: 0x001BF6FB
		[Event(280, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)68719476736L)]
		private void TieredCompilationSettings(ushort ClrInstanceID, uint Flags)
		{
			base.WriteEvent(280, (long)((ulong)ClrInstanceID), (long)((ulong)Flags));
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x001C050C File Offset: 0x001BF70C
		[Event(281, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)68719476736L)]
		private void TieredCompilationPause(ushort ClrInstanceID)
		{
			base.WriteEvent(281, (int)ClrInstanceID);
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x001C051A File Offset: 0x001BF71A
		[Event(282, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)68719476736L)]
		private void TieredCompilationResume(ushort ClrInstanceID, uint NewMethodCount)
		{
			base.WriteEvent(282, (long)((ulong)ClrInstanceID), (long)((ulong)NewMethodCount));
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x001C052B File Offset: 0x001BF72B
		[Event(283, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)68719476736L)]
		private void TieredCompilationBackgroundJitStart(ushort ClrInstanceID, uint PendingMethodCount)
		{
			base.WriteEvent(283, (long)((ulong)ClrInstanceID), (long)((ulong)PendingMethodCount));
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x001C053C File Offset: 0x001BF73C
		[Event(284, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)68719476736L)]
		private void TieredCompilationBackgroundJitStop(ushort ClrInstanceID, uint PendingMethodCount, uint JittedMethodCount)
		{
			base.WriteEvent(284, (long)((ulong)ClrInstanceID), (long)((ulong)PendingMethodCount), (long)((ulong)JittedMethodCount));
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x001C054F File Offset: 0x001BF74F
		[Event(290, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void AssemblyLoadStart(ushort ClrInstanceID, string AssemblyName, string AssemblyPath, string RequestingAssembly, string AssemblyLoadContext, string RequestingAssemblyLoadContext)
		{
			base.WriteEvent(290, new object[]
			{
				ClrInstanceID,
				AssemblyName,
				AssemblyPath,
				RequestingAssembly,
				AssemblyLoadContext,
				RequestingAssemblyLoadContext
			});
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x001C0584 File Offset: 0x001BF784
		[Event(291, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void AssemblyLoadStop(ushort ClrInstanceID, string AssemblyName, string AssemblyPath, string RequestingAssembly, string AssemblyLoadContext, string RequestingAssemblyLoadContext, bool Success, string ResultAssemblyName, string ResultAssemblyPath, bool Cached)
		{
			base.WriteEvent(291, new object[]
			{
				ClrInstanceID,
				AssemblyName,
				AssemblyPath,
				RequestingAssembly,
				AssemblyLoadContext,
				RequestingAssemblyLoadContext,
				Success,
				ResultAssemblyName,
				ResultAssemblyPath,
				Cached
			});
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x001C05E4 File Offset: 0x001BF7E4
		[Event(292, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void ResolutionAttempted(ushort ClrInstanceID, string AssemblyName, ushort Stage, string AssemblyLoadContext, ushort Result, string ResultAssemblyName, string ResultAssemblyPath, string ErrorMessage)
		{
			base.WriteEvent(292, new object[]
			{
				ClrInstanceID,
				AssemblyName,
				Stage,
				AssemblyLoadContext,
				Result,
				ResultAssemblyName,
				ResultAssemblyPath,
				ErrorMessage
			});
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x001C0636 File Offset: 0x001BF836
		[Event(293, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void AssemblyLoadContextResolvingHandlerInvoked(ushort ClrInstanceID, string AssemblyName, string HandlerName, string AssemblyLoadContext, string ResultAssemblyName, string ResultAssemblyPath)
		{
			base.WriteEvent(293, new object[]
			{
				ClrInstanceID,
				AssemblyName,
				HandlerName,
				AssemblyLoadContext,
				ResultAssemblyName,
				ResultAssemblyPath
			});
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x001C0669 File Offset: 0x001BF869
		[Event(294, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void AppDomainAssemblyResolveHandlerInvoked(ushort ClrInstanceID, string AssemblyName, string HandlerName, string ResultAssemblyName, string ResultAssemblyPath)
		{
			base.WriteEvent(294, new object[]
			{
				ClrInstanceID,
				AssemblyName,
				HandlerName,
				ResultAssemblyName,
				ResultAssemblyPath
			});
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x001C0697 File Offset: 0x001BF897
		[Event(295, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void AssemblyLoadFromResolveHandlerInvoked(ushort ClrInstanceID, string AssemblyName, bool IsTrackedLoad, string RequestingAssemblyPath, string ComputedRequestedAssemblyPath)
		{
			base.WriteEvent(295, new object[]
			{
				ClrInstanceID,
				AssemblyName,
				IsTrackedLoad,
				RequestingAssemblyPath,
				ComputedRequestedAssemblyPath
			});
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x001C06CA File Offset: 0x001BF8CA
		[Event(296, Version = 0, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void KnownPathProbed(ushort ClrInstanceID, string FilePath, ushort Source, int Result)
		{
			base.WriteEvent(296, new object[]
			{
				ClrInstanceID,
				FilePath,
				Source,
				Result
			});
		}

		// Token: 0x04001B30 RID: 6960
		internal const string EventSourceName = "Microsoft-Windows-DotNETRuntime";

		// Token: 0x04001B31 RID: 6961
		internal static NativeRuntimeEventSource Log = new NativeRuntimeEventSource();

		// Token: 0x0200074D RID: 1869
		public class Keywords
		{
			// Token: 0x04001B32 RID: 6962
			public const EventKeywords GCKeyword = (EventKeywords)1L;

			// Token: 0x04001B33 RID: 6963
			public const EventKeywords GCHandleKeyword = (EventKeywords)2L;

			// Token: 0x04001B34 RID: 6964
			public const EventKeywords AssemblyLoaderKeyword = (EventKeywords)4L;

			// Token: 0x04001B35 RID: 6965
			public const EventKeywords LoaderKeyword = (EventKeywords)8L;

			// Token: 0x04001B36 RID: 6966
			public const EventKeywords JitKeyword = (EventKeywords)16L;

			// Token: 0x04001B37 RID: 6967
			public const EventKeywords NGenKeyword = (EventKeywords)32L;

			// Token: 0x04001B38 RID: 6968
			public const EventKeywords StartEnumerationKeyword = (EventKeywords)64L;

			// Token: 0x04001B39 RID: 6969
			public const EventKeywords EndEnumerationKeyword = (EventKeywords)128L;

			// Token: 0x04001B3A RID: 6970
			public const EventKeywords SecurityKeyword = (EventKeywords)1024L;

			// Token: 0x04001B3B RID: 6971
			public const EventKeywords AppDomainResourceManagementKeyword = (EventKeywords)2048L;

			// Token: 0x04001B3C RID: 6972
			public const EventKeywords JitTracingKeyword = (EventKeywords)4096L;

			// Token: 0x04001B3D RID: 6973
			public const EventKeywords InteropKeyword = (EventKeywords)8192L;

			// Token: 0x04001B3E RID: 6974
			public const EventKeywords ContentionKeyword = (EventKeywords)16384L;

			// Token: 0x04001B3F RID: 6975
			public const EventKeywords ExceptionKeyword = (EventKeywords)32768L;

			// Token: 0x04001B40 RID: 6976
			public const EventKeywords ThreadingKeyword = (EventKeywords)65536L;

			// Token: 0x04001B41 RID: 6977
			public const EventKeywords JittedMethodILToNativeMapKeyword = (EventKeywords)131072L;

			// Token: 0x04001B42 RID: 6978
			public const EventKeywords OverrideAndSuppressNGenEventsKeyword = (EventKeywords)262144L;

			// Token: 0x04001B43 RID: 6979
			public const EventKeywords TypeKeyword = (EventKeywords)524288L;

			// Token: 0x04001B44 RID: 6980
			public const EventKeywords GCHeapDumpKeyword = (EventKeywords)1048576L;

			// Token: 0x04001B45 RID: 6981
			public const EventKeywords GCSampledObjectAllocationHighKeyword = (EventKeywords)2097152L;

			// Token: 0x04001B46 RID: 6982
			public const EventKeywords GCHeapSurvivalAndMovementKeyword = (EventKeywords)4194304L;

			// Token: 0x04001B47 RID: 6983
			public const EventKeywords GCHeapCollectKeyword = (EventKeywords)8388608L;

			// Token: 0x04001B48 RID: 6984
			public const EventKeywords GCHeapAndTypeNamesKeyword = (EventKeywords)16777216L;

			// Token: 0x04001B49 RID: 6985
			public const EventKeywords GCSampledObjectAllocationLowKeyword = (EventKeywords)33554432L;

			// Token: 0x04001B4A RID: 6986
			public const EventKeywords PerfTrackKeyword = (EventKeywords)536870912L;

			// Token: 0x04001B4B RID: 6987
			public const EventKeywords StackKeyword = (EventKeywords)1073741824L;

			// Token: 0x04001B4C RID: 6988
			public const EventKeywords ThreadTransferKeyword = (EventKeywords)2147483648L;

			// Token: 0x04001B4D RID: 6989
			public const EventKeywords DebuggerKeyword = (EventKeywords)4294967296L;

			// Token: 0x04001B4E RID: 6990
			public const EventKeywords MonitoringKeyword = (EventKeywords)8589934592L;

			// Token: 0x04001B4F RID: 6991
			public const EventKeywords CodeSymbolsKeyword = (EventKeywords)17179869184L;

			// Token: 0x04001B50 RID: 6992
			public const EventKeywords EventSourceKeyword = (EventKeywords)34359738368L;

			// Token: 0x04001B51 RID: 6993
			public const EventKeywords CompilationKeyword = (EventKeywords)68719476736L;

			// Token: 0x04001B52 RID: 6994
			public const EventKeywords CompilationDiagnosticKeyword = (EventKeywords)137438953472L;

			// Token: 0x04001B53 RID: 6995
			public const EventKeywords MethodDiagnosticKeyword = (EventKeywords)274877906944L;

			// Token: 0x04001B54 RID: 6996
			public const EventKeywords TypeDiagnosticKeyword = (EventKeywords)549755813888L;
		}
	}
}
