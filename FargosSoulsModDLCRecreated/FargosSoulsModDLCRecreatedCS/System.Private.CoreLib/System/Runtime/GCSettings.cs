using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x020003D3 RID: 979
	public static class GCSettings
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060031D5 RID: 12757
		public static extern bool IsServerGC { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060031D6 RID: 12758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GCLatencyMode GetGCLatencyMode();

		// Token: 0x060031D7 RID: 12759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GCSettings.SetLatencyModeStatus SetGCLatencyMode(GCLatencyMode newLatencyMode);

		// Token: 0x060031D8 RID: 12760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GCLargeObjectHeapCompactionMode GetLOHCompactionMode();

		// Token: 0x060031D9 RID: 12761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLOHCompactionMode(GCLargeObjectHeapCompactionMode newLOHCompactionMode);

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x0016A0E9 File Offset: 0x001692E9
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x0016A0F0 File Offset: 0x001692F0
		public static GCLatencyMode LatencyMode
		{
			get
			{
				return GCSettings.GetGCLatencyMode();
			}
			set
			{
				if (value < GCLatencyMode.Batch || value > GCLatencyMode.SustainedLowLatency)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_Enum);
				}
				GCSettings.SetLatencyModeStatus setLatencyModeStatus = GCSettings.SetGCLatencyMode(value);
				if (setLatencyModeStatus == GCSettings.SetLatencyModeStatus.NoGCInProgress)
				{
					throw new InvalidOperationException(SR.InvalidOperation_SetLatencyModeNoGC);
				}
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x0016A12B File Offset: 0x0016932B
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x0016A132 File Offset: 0x00169332
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			get
			{
				return GCSettings.GetLOHCompactionMode();
			}
			set
			{
				if (value < GCLargeObjectHeapCompactionMode.Default || value > GCLargeObjectHeapCompactionMode.CompactOnce)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_Enum);
				}
				GCSettings.SetLOHCompactionMode(value);
			}
		}

		// Token: 0x020003D4 RID: 980
		private enum SetLatencyModeStatus
		{
			// Token: 0x04000DDD RID: 3549
			Succeeded,
			// Token: 0x04000DDE RID: 3550
			NoGCInProgress
		}
	}
}
