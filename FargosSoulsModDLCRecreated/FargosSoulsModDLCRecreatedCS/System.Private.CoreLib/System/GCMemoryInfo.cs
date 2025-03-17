using System;

namespace System
{
	// Token: 0x0200010E RID: 270
	public readonly struct GCMemoryInfo
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x000D0181 File Offset: 0x000CF381
		internal GCMemoryInfo(GCMemoryInfoData data)
		{
			this._data = data;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x000D018A File Offset: 0x000CF38A
		public long HighMemoryLoadThresholdBytes
		{
			get
			{
				return this._data._highMemoryLoadThresholdBytes;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x000D0197 File Offset: 0x000CF397
		public long MemoryLoadBytes
		{
			get
			{
				return this._data._memoryLoadBytes;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000D01A4 File Offset: 0x000CF3A4
		public long TotalAvailableMemoryBytes
		{
			get
			{
				return this._data._totalAvailableMemoryBytes;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x000D01B1 File Offset: 0x000CF3B1
		public long HeapSizeBytes
		{
			get
			{
				return this._data._heapSizeBytes;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000D01BE File Offset: 0x000CF3BE
		public long FragmentedBytes
		{
			get
			{
				return this._data._fragmentedBytes;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x000D01CB File Offset: 0x000CF3CB
		public long Index
		{
			get
			{
				return this._data._index;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000D01D8 File Offset: 0x000CF3D8
		public int Generation
		{
			get
			{
				return this._data._generation;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x000D01E5 File Offset: 0x000CF3E5
		public bool Compacted
		{
			get
			{
				return this._data._compacted;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x000D01F2 File Offset: 0x000CF3F2
		public bool Concurrent
		{
			get
			{
				return this._data._concurrent;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x000D01FF File Offset: 0x000CF3FF
		public long TotalCommittedBytes
		{
			get
			{
				return this._data._totalCommittedBytes;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x000D020C File Offset: 0x000CF40C
		public long PromotedBytes
		{
			get
			{
				return this._data._promotedBytes;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x000D0219 File Offset: 0x000CF419
		public long PinnedObjectsCount
		{
			get
			{
				return this._data._pinnedObjectsCount;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x000D0226 File Offset: 0x000CF426
		public long FinalizationPendingCount
		{
			get
			{
				return this._data._finalizationPendingCount;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x000D0233 File Offset: 0x000CF433
		public ReadOnlySpan<TimeSpan> PauseDurations
		{
			get
			{
				return this._data.PauseDurationsAsSpan;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x000D0240 File Offset: 0x000CF440
		public double PauseTimePercentage
		{
			get
			{
				return (double)this._data._pauseTimePercentage / 100.0;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000D0258 File Offset: 0x000CF458
		public ReadOnlySpan<GCGenerationInfo> GenerationInfo
		{
			get
			{
				return this._data.GenerationInfoAsSpan;
			}
		}

		// Token: 0x0400030C RID: 780
		private readonly GCMemoryInfoData _data;
	}
}
