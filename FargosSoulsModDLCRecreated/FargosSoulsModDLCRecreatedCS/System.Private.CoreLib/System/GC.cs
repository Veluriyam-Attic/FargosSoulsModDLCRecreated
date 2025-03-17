using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000070 RID: 112
	[NullableContext(1)]
	[Nullable(0)]
	public static class GC
	{
		// Token: 0x0600039A RID: 922
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMemoryInfo(GCMemoryInfoData data, int kind);

		// Token: 0x0600039B RID: 923 RVA: 0x000B695A File Offset: 0x000B5B5A
		public static GCMemoryInfo GetGCMemoryInfo()
		{
			return GC.GetGCMemoryInfo(GCKind.Any);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000B6964 File Offset: 0x000B5B64
		public static GCMemoryInfo GetGCMemoryInfo(GCKind kind)
		{
			if (kind < GCKind.Any || kind > GCKind.Background)
			{
				throw new ArgumentOutOfRangeException("kind", SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, GCKind.Any, GCKind.Background));
			}
			GCMemoryInfoData data = new GCMemoryInfoData();
			GC.GetMemoryInfo(data, (int)kind);
			return new GCMemoryInfo(data);
		}

		// Token: 0x0600039D RID: 925
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int _StartNoGCRegion(long totalSize, bool lohSizeKnown, long lohSize, bool disallowFullBlockingGC);

		// Token: 0x0600039E RID: 926
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int _EndNoGCRegion();

		// Token: 0x0600039F RID: 927
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Array AllocateNewArray(IntPtr typeHandle, int length, GC.GC_ALLOC_FLAGS flags);

		// Token: 0x060003A0 RID: 928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenerationWR(IntPtr handle);

		// Token: 0x060003A1 RID: 929
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetTotalMemory();

		// Token: 0x060003A2 RID: 930
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _Collect(int generation, int mode);

		// Token: 0x060003A3 RID: 931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x060003A4 RID: 932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _CollectionCount(int generation, int getSpecialGCCount);

		// Token: 0x060003A5 RID: 933
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetSegmentSize();

		// Token: 0x060003A6 RID: 934
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLastGCPercentTimeInGC();

		// Token: 0x060003A7 RID: 935
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetGenerationSize(int gen);

		// Token: 0x060003A8 RID: 936
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _AddMemoryPressure(ulong bytesAllocated);

		// Token: 0x060003A9 RID: 937
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _RemoveMemoryPressure(ulong bytesAllocated);

		// Token: 0x060003AA RID: 938 RVA: 0x000B69AD File Offset: 0x000B5BAD
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (4 == IntPtr.Size)
			{
			}
			GC._AddMemoryPressure((ulong)bytesAllocated);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000B69D2 File Offset: 0x000B5BD2
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (4 == IntPtr.Size)
			{
			}
			GC._RemoveMemoryPressure((ulong)bytesAllocated);
		}

		// Token: 0x060003AC RID: 940
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetGeneration(object obj);

		// Token: 0x060003AD RID: 941 RVA: 0x000B69F7 File Offset: 0x000B5BF7
		public static void Collect(int generation)
		{
			GC.Collect(generation, GCCollectionMode.Default);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000B6A00 File Offset: 0x000B5C00
		public static void Collect()
		{
			GC._Collect(-1, 2);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000B6A09 File Offset: 0x000B5C09
		public static void Collect(int generation, GCCollectionMode mode)
		{
			GC.Collect(generation, mode, true);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000B6A13 File Offset: 0x000B5C13
		public static void Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			GC.Collect(generation, mode, blocking, false);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000B6A20 File Offset: 0x000B5C20
		public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
			{
				throw new ArgumentOutOfRangeException("mode", SR.ArgumentOutOfRange_Enum);
			}
			int num = 0;
			if (mode == GCCollectionMode.Optimized)
			{
				num |= 4;
			}
			if (compacting)
			{
				num |= 8;
			}
			if (blocking)
			{
				num |= 2;
			}
			else if (!compacting)
			{
				num |= 1;
			}
			GC._Collect(generation, num);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000B6A81 File Offset: 0x000B5C81
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", SR.ArgumentOutOfRange_GenericPositive);
			}
			return GC._CollectionCount(generation, 0);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(2)]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000B6AA0 File Offset: 0x000B5CA0
		public static int GetGeneration(WeakReference wo)
		{
			int generationWR = GC.GetGenerationWR(wo.m_handle);
			GC.KeepAlive(wo);
			return generationWR;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000B6AC0 File Offset: 0x000B5CC0
		public static int MaxGeneration
		{
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		// Token: 0x060003B6 RID: 950
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _WaitForPendingFinalizers();

		// Token: 0x060003B7 RID: 951 RVA: 0x000B6AC7 File Offset: 0x000B5CC7
		public static void WaitForPendingFinalizers()
		{
			GC._WaitForPendingFinalizers();
		}

		// Token: 0x060003B8 RID: 952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		// Token: 0x060003B9 RID: 953 RVA: 0x000B6ACE File Offset: 0x000B5CCE
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x060003BA RID: 954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		// Token: 0x060003BB RID: 955 RVA: 0x000B6AE4 File Offset: 0x000B5CE4
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000B6AFC File Offset: 0x000B5CFC
		public static long GetTotalMemory(bool forceFullCollection)
		{
			long num = GC.GetTotalMemory();
			if (!forceFullCollection)
			{
				return num;
			}
			int num2 = 20;
			long num3 = num;
			float num4;
			do
			{
				GC.WaitForPendingFinalizers();
				GC.Collect();
				num = num3;
				num3 = GC.GetTotalMemory();
				num4 = (float)(num3 - num) / (float)num;
			}
			while (num2-- > 0 && (-0.05 >= (double)num4 || (double)num4 >= 0.05));
			return num3;
		}

		// Token: 0x060003BD RID: 957
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr _RegisterFrozenSegment(IntPtr sectionAddress, [NativeInteger] IntPtr sectionSize);

		// Token: 0x060003BE RID: 958
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _UnregisterFrozenSegment(IntPtr segmentHandle);

		// Token: 0x060003BF RID: 959
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetAllocatedBytesForCurrentThread();

		// Token: 0x060003C0 RID: 960
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalAllocatedBytes(bool precise = false);

		// Token: 0x060003C1 RID: 961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage);

		// Token: 0x060003C2 RID: 962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _CancelFullGCNotification();

		// Token: 0x060003C3 RID: 963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _WaitForFullGCApproach(int millisecondsTimeout);

		// Token: 0x060003C4 RID: 964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _WaitForFullGCComplete(int millisecondsTimeout);

		// Token: 0x060003C5 RID: 965 RVA: 0x000B6B58 File Offset: 0x000B5D58
		public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
		{
			if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("maxGenerationThreshold", SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, 1, 99));
			}
			if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, 1, 99));
			}
			if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotWithConcurrentGC);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000B6BCF File Offset: 0x000B5DCF
		public static void CancelFullGCNotification()
		{
			if (!GC._CancelFullGCNotification())
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotWithConcurrentGC);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000B6BE3 File Offset: 0x000B5DE3
		public static GCNotificationStatus WaitForFullGCApproach()
		{
			return (GCNotificationStatus)GC._WaitForFullGCApproach(-1);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000B6BEB File Offset: 0x000B5DEB
		public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return (GCNotificationStatus)GC._WaitForFullGCApproach(millisecondsTimeout);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000B6C07 File Offset: 0x000B5E07
		public static GCNotificationStatus WaitForFullGCComplete()
		{
			return (GCNotificationStatus)GC._WaitForFullGCComplete(-1);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000B6C0F File Offset: 0x000B5E0F
		public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return (GCNotificationStatus)GC._WaitForFullGCComplete(millisecondsTimeout);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000B6C2C File Offset: 0x000B5E2C
		private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
		{
			if (totalSize <= 0L)
			{
				throw new ArgumentOutOfRangeException("totalSize", "totalSize can't be zero or negative");
			}
			if (hasLohSize)
			{
				if (lohSize <= 0L)
				{
					throw new ArgumentOutOfRangeException("lohSize", "lohSize can't be zero or negative");
				}
				if (lohSize > totalSize)
				{
					throw new ArgumentOutOfRangeException("lohSize", "lohSize can't be greater than totalSize");
				}
			}
			switch (GC._StartNoGCRegion(totalSize, hasLohSize, lohSize, disallowFullBlockingGC))
			{
			case 1:
				return false;
			case 2:
				throw new ArgumentOutOfRangeException("totalSize", "totalSize is too large. For more information about setting the maximum size, see \"Latency Modes\" in http://go.microsoft.com/fwlink/?LinkId=522706");
			case 3:
				throw new InvalidOperationException("The NoGCRegion mode was already in progress");
			default:
				return true;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000B6CB8 File Offset: 0x000B5EB8
		public static bool TryStartNoGCRegion(long totalSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000B6CC4 File Offset: 0x000B5EC4
		public static bool TryStartNoGCRegion(long totalSize, long lohSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000B6CCF File Offset: 0x000B5ECF
		public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000B6CDB File Offset: 0x000B5EDB
		public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000B6CE8 File Offset: 0x000B5EE8
		public static void EndNoGCRegion()
		{
			GC.EndNoGCRegionStatus endNoGCRegionStatus = (GC.EndNoGCRegionStatus)GC._EndNoGCRegion();
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.NotInProgress)
			{
				throw new InvalidOperationException("NoGCRegion mode must be set");
			}
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.GCInduced)
			{
				throw new InvalidOperationException("Garbage collection was induced in NoGCRegion mode");
			}
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.AllocationExceeded)
			{
				throw new InvalidOperationException("Allocated memory exceeds specified memory for NoGCRegion mode");
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000B6D28 File Offset: 0x000B5F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] AllocateUninitializedArray<[Nullable(2)] T>(int length, bool pinned = false)
		{
			if (!pinned)
			{
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					return new T[length];
				}
				if (length < 2048 / Unsafe.SizeOf<T>())
				{
					return new T[length];
				}
			}
			else if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return GC.<AllocateUninitializedArray>g__AllocateNewUninitializedArray|66_0<T>(length, pinned);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000B6D78 File Offset: 0x000B5F78
		public static T[] AllocateArray<[Nullable(2)] T>(int length, bool pinned = false)
		{
			GC.GC_ALLOC_FLAGS flags = GC.GC_ALLOC_FLAGS.GC_ALLOC_NO_FLAGS;
			if (pinned)
			{
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
				}
				flags = GC.GC_ALLOC_FLAGS.GC_ALLOC_PINNED_OBJECT_HEAP;
			}
			return Unsafe.As<T[]>(GC.AllocateNewArray(typeof(T[]).TypeHandle.Value, length, flags));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000B6DC8 File Offset: 0x000B5FC8
		[CompilerGenerated]
		internal static T[] <AllocateUninitializedArray>g__AllocateNewUninitializedArray|66_0<T>(int length, bool pinned)
		{
			GC.GC_ALLOC_FLAGS gc_ALLOC_FLAGS = GC.GC_ALLOC_FLAGS.GC_ALLOC_ZEROING_OPTIONAL;
			if (pinned)
			{
				gc_ALLOC_FLAGS |= GC.GC_ALLOC_FLAGS.GC_ALLOC_PINNED_OBJECT_HEAP;
			}
			return Unsafe.As<T[]>(GC.AllocateNewArray(typeof(T[]).TypeHandle.Value, length, gc_ALLOC_FLAGS));
		}

		// Token: 0x02000071 RID: 113
		internal enum GC_ALLOC_FLAGS
		{
			// Token: 0x04000174 RID: 372
			GC_ALLOC_NO_FLAGS,
			// Token: 0x04000175 RID: 373
			GC_ALLOC_ZEROING_OPTIONAL = 16,
			// Token: 0x04000176 RID: 374
			GC_ALLOC_PINNED_OBJECT_HEAP = 64
		}

		// Token: 0x02000072 RID: 114
		private enum StartNoGCRegionStatus
		{
			// Token: 0x04000178 RID: 376
			Succeeded,
			// Token: 0x04000179 RID: 377
			NotEnoughMemory,
			// Token: 0x0400017A RID: 378
			AmountTooLarge,
			// Token: 0x0400017B RID: 379
			AlreadyInProgress
		}

		// Token: 0x02000073 RID: 115
		private enum EndNoGCRegionStatus
		{
			// Token: 0x0400017D RID: 381
			Succeeded,
			// Token: 0x0400017E RID: 382
			NotInProgress,
			// Token: 0x0400017F RID: 383
			GCInduced,
			// Token: 0x04000180 RID: 384
			AllocationExceeded
		}
	}
}
