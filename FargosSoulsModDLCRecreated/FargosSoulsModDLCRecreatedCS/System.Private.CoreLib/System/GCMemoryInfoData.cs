using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200010D RID: 269
	[StructLayout(LayoutKind.Sequential)]
	internal class GCMemoryInfoData
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000D0165 File Offset: 0x000CF365
		internal ReadOnlySpan<GCGenerationInfo> GenerationInfoAsSpan
		{
			get
			{
				return MemoryMarshal.CreateReadOnlySpan<GCGenerationInfo>(ref this._generationInfo0, 5);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000D0173 File Offset: 0x000CF373
		internal ReadOnlySpan<TimeSpan> PauseDurationsAsSpan
		{
			get
			{
				return MemoryMarshal.CreateReadOnlySpan<TimeSpan>(ref this._pauseDuration0, 2);
			}
		}

		// Token: 0x040002F7 RID: 759
		internal long _highMemoryLoadThresholdBytes;

		// Token: 0x040002F8 RID: 760
		internal long _totalAvailableMemoryBytes;

		// Token: 0x040002F9 RID: 761
		internal long _memoryLoadBytes;

		// Token: 0x040002FA RID: 762
		internal long _heapSizeBytes;

		// Token: 0x040002FB RID: 763
		internal long _fragmentedBytes;

		// Token: 0x040002FC RID: 764
		internal long _totalCommittedBytes;

		// Token: 0x040002FD RID: 765
		internal long _promotedBytes;

		// Token: 0x040002FE RID: 766
		internal long _pinnedObjectsCount;

		// Token: 0x040002FF RID: 767
		internal long _finalizationPendingCount;

		// Token: 0x04000300 RID: 768
		internal long _index;

		// Token: 0x04000301 RID: 769
		internal int _generation;

		// Token: 0x04000302 RID: 770
		internal int _pauseTimePercentage;

		// Token: 0x04000303 RID: 771
		internal bool _compacted;

		// Token: 0x04000304 RID: 772
		internal bool _concurrent;

		// Token: 0x04000305 RID: 773
		private GCGenerationInfo _generationInfo0;

		// Token: 0x04000306 RID: 774
		private GCGenerationInfo _generationInfo1;

		// Token: 0x04000307 RID: 775
		private GCGenerationInfo _generationInfo2;

		// Token: 0x04000308 RID: 776
		private GCGenerationInfo _generationInfo3;

		// Token: 0x04000309 RID: 777
		private GCGenerationInfo _generationInfo4;

		// Token: 0x0400030A RID: 778
		private TimeSpan _pauseDuration0;

		// Token: 0x0400030B RID: 779
		private TimeSpan _pauseDuration1;
	}
}
