using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000251 RID: 593
	internal sealed class TlsOverPerCoreLockedStacksArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x001398C0 File Offset: 0x00138AC0
		public TlsOverPerCoreLockedStacksArrayPool()
		{
			int[] array = new int[17];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Utilities.GetMaxSizeForBucket(i);
			}
			this._bucketArraySizes = array;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00139908 File Offset: 0x00138B08
		private TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks CreatePerCoreLockedStacks(int bucketIndex)
		{
			TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks = new TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks();
			return Interlocked.CompareExchange<TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks>(ref this._buckets[bucketIndex], perCoreLockedStacks, null) ?? perCoreLockedStacks;
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x0013929F File Offset: 0x0013849F
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00139934 File Offset: 0x00138B34
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				return Array.Empty<T>();
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array2;
			if (num < this._buckets.Length)
			{
				T[][] array = TlsOverPerCoreLockedStacksArrayPool<T>.t_tlsBuckets;
				if (array != null)
				{
					array2 = array[num];
					if (array2 != null)
					{
						array[num] = null;
						if (log.IsEnabled())
						{
							log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, num);
						}
						return array2;
					}
				}
				TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks = this._buckets[num];
				if (perCoreLockedStacks != null)
				{
					array2 = perCoreLockedStacks.TryPop();
					if (array2 != null)
					{
						if (log.IsEnabled())
						{
							log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, num);
						}
						return array2;
					}
				}
				array2 = GC.AllocateUninitializedArray<T>(this._bucketArraySizes[num], false);
			}
			else
			{
				array2 = GC.AllocateUninitializedArray<T>(minimumLength, false);
			}
			if (log.IsEnabled())
			{
				int hashCode = array2.GetHashCode();
				int bucketId = -1;
				log.BufferRented(hashCode, array2.Length, this.Id, bucketId);
				log.BufferAllocated(hashCode, array2.Length, this.Id, bucketId, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array2;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00139A40 File Offset: 0x00138C40
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				if (array.Length != this._bucketArraySizes[num])
				{
					throw new ArgumentException(SR.ArgumentException_BufferNotFromPool, "array");
				}
				T[][] array2 = TlsOverPerCoreLockedStacksArrayPool<T>.t_tlsBuckets;
				if (array2 == null)
				{
					array2 = (TlsOverPerCoreLockedStacksArrayPool<T>.t_tlsBuckets = new T[17][]);
					array2[num] = array;
					if (TlsOverPerCoreLockedStacksArrayPool<T>.s_trimBuffers)
					{
						TlsOverPerCoreLockedStacksArrayPool<T>.s_allTlsBuckets.Add(array2, null);
						if (Interlocked.Exchange(ref this._callbackCreated, 1) != 1)
						{
							Gen2GcCallback.Register(new Func<object, bool>(TlsOverPerCoreLockedStacksArrayPool<T>.Gen2GcCallbackFunc), this);
						}
					}
				}
				else
				{
					T[] array3 = array2[num];
					array2[num] = array;
					if (array3 != null)
					{
						TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks = this._buckets[num] ?? this.CreatePerCoreLockedStacks(num);
						perCoreLockedStacks.TryPush(array3);
					}
				}
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00139B38 File Offset: 0x00138D38
		public bool Trim()
		{
			int tickCount = Environment.TickCount;
			TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure memoryPressure = TlsOverPerCoreLockedStacksArrayPool<T>.GetMemoryPressure();
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferTrimPoll(tickCount, (int)memoryPressure);
			}
			TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks[] buckets = this._buckets;
			for (int i = 0; i < buckets.Length; i++)
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks = buckets[i];
				if (perCoreLockedStacks != null)
				{
					perCoreLockedStacks.Trim((uint)tickCount, this.Id, memoryPressure, this._bucketArraySizes[i]);
				}
			}
			if (memoryPressure == TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High)
			{
				if (log.IsEnabled())
				{
					using (IEnumerator<KeyValuePair<T[][], object>> enumerator = ((IEnumerable<KeyValuePair<T[][], object>>)TlsOverPerCoreLockedStacksArrayPool<T>.s_allTlsBuckets).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<T[][], object> keyValuePair = enumerator.Current;
							T[][] key = keyValuePair.Key;
							for (int j = 0; j < key.Length; j++)
							{
								T[] array = Interlocked.Exchange<T[]>(ref key[j], null);
								if (array != null)
								{
									log.BufferTrimmed(array.GetHashCode(), array.Length, this.Id);
								}
							}
						}
						return true;
					}
				}
				foreach (KeyValuePair<T[][], object> keyValuePair2 in ((IEnumerable<KeyValuePair<T[][], object>>)TlsOverPerCoreLockedStacksArrayPool<T>.s_allTlsBuckets))
				{
					T[][] key2 = keyValuePair2.Key;
					Array.Clear(key2, 0, key2.Length);
				}
			}
			return true;
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00139C88 File Offset: 0x00138E88
		private static bool Gen2GcCallbackFunc(object target)
		{
			return ((TlsOverPerCoreLockedStacksArrayPool<T>)target).Trim();
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00139C98 File Offset: 0x00138E98
		private static TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure GetMemoryPressure()
		{
			GCMemoryInfo gcmemoryInfo = GC.GetGCMemoryInfo();
			if ((double)gcmemoryInfo.MemoryLoadBytes >= (double)gcmemoryInfo.HighMemoryLoadThresholdBytes * 0.9)
			{
				return TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High;
			}
			if ((double)gcmemoryInfo.MemoryLoadBytes >= (double)gcmemoryInfo.HighMemoryLoadThresholdBytes * 0.7)
			{
				return TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.Medium;
			}
			return TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.Low;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00139CE8 File Offset: 0x00138EE8
		private static bool GetTrimBuffers()
		{
			return CLRConfig.GetBoolValueWithFallbacks("System.Buffers.ArrayPool.TrimShared", "DOTNET_SYSTEM_BUFFERS_ARRAYPOOL_TRIMSHARED", true);
		}

		// Token: 0x04000989 RID: 2441
		private readonly int[] _bucketArraySizes;

		// Token: 0x0400098A RID: 2442
		private readonly TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks[] _buckets = new TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks[17];

		// Token: 0x0400098B RID: 2443
		[ThreadStatic]
		private static T[][] t_tlsBuckets;

		// Token: 0x0400098C RID: 2444
		private int _callbackCreated;

		// Token: 0x0400098D RID: 2445
		private static readonly bool s_trimBuffers = TlsOverPerCoreLockedStacksArrayPool<T>.GetTrimBuffers();

		// Token: 0x0400098E RID: 2446
		private static readonly ConditionalWeakTable<T[][], object> s_allTlsBuckets = TlsOverPerCoreLockedStacksArrayPool<T>.s_trimBuffers ? new ConditionalWeakTable<T[][], object>() : null;

		// Token: 0x02000252 RID: 594
		private enum MemoryPressure
		{
			// Token: 0x04000990 RID: 2448
			Low,
			// Token: 0x04000991 RID: 2449
			Medium,
			// Token: 0x04000992 RID: 2450
			High
		}

		// Token: 0x02000253 RID: 595
		private sealed class PerCoreLockedStacks
		{
			// Token: 0x06002464 RID: 9316 RVA: 0x00139D1C File Offset: 0x00138F1C
			public PerCoreLockedStacks()
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] array = new TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[Math.Min(Environment.ProcessorCount, 64)];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack();
				}
				this._perCoreStacks = array;
			}

			// Token: 0x06002465 RID: 9317 RVA: 0x00139D60 File Offset: 0x00138F60
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void TryPush(T[] array)
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] perCoreStacks = this._perCoreStacks;
				int num = Thread.GetCurrentProcessorId() % perCoreStacks.Length;
				for (int i = 0; i < perCoreStacks.Length; i++)
				{
					if (perCoreStacks[num].TryPush(array))
					{
						return;
					}
					if (++num == perCoreStacks.Length)
					{
						num = 0;
					}
				}
			}

			// Token: 0x06002466 RID: 9318 RVA: 0x00139DA4 File Offset: 0x00138FA4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public T[] TryPop()
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] perCoreStacks = this._perCoreStacks;
				int num = Thread.GetCurrentProcessorId() % perCoreStacks.Length;
				for (int i = 0; i < perCoreStacks.Length; i++)
				{
					T[] result;
					if ((result = perCoreStacks[num].TryPop()) != null)
					{
						return result;
					}
					if (++num == perCoreStacks.Length)
					{
						num = 0;
					}
				}
				return null;
			}

			// Token: 0x06002467 RID: 9319 RVA: 0x00139DEC File Offset: 0x00138FEC
			public void Trim(uint tickCount, int id, TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure pressure, int bucketSize)
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] perCoreStacks = this._perCoreStacks;
				for (int i = 0; i < perCoreStacks.Length; i++)
				{
					perCoreStacks[i].Trim(tickCount, id, pressure, bucketSize);
				}
			}

			// Token: 0x04000993 RID: 2451
			private readonly TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] _perCoreStacks;
		}

		// Token: 0x02000254 RID: 596
		private sealed class LockedStack
		{
			// Token: 0x06002468 RID: 9320 RVA: 0x00139E1C File Offset: 0x0013901C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryPush(T[] array)
			{
				bool result = false;
				Monitor.Enter(this);
				if (this._count < 8)
				{
					if (TlsOverPerCoreLockedStacksArrayPool<T>.s_trimBuffers && this._count == 0)
					{
						this._firstStackItemMS = (uint)Environment.TickCount;
					}
					T[][] arrays = this._arrays;
					int count = this._count;
					this._count = count + 1;
					arrays[count] = array;
					result = true;
				}
				Monitor.Exit(this);
				return result;
			}

			// Token: 0x06002469 RID: 9321 RVA: 0x00139E78 File Offset: 0x00139078
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public T[] TryPop()
			{
				T[] result = null;
				Monitor.Enter(this);
				if (this._count > 0)
				{
					T[][] arrays = this._arrays;
					int num = this._count - 1;
					this._count = num;
					result = arrays[num];
					this._arrays[this._count] = null;
				}
				Monitor.Exit(this);
				return result;
			}

			// Token: 0x0600246A RID: 9322 RVA: 0x00139EC4 File Offset: 0x001390C4
			public void Trim(uint tickCount, int id, TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure pressure, int bucketSize)
			{
				if (this._count == 0)
				{
					return;
				}
				uint num = (pressure == TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High) ? 10000U : 60000U;
				lock (this)
				{
					if ((this._count > 0 && this._firstStackItemMS > tickCount) || tickCount - this._firstStackItemMS > num)
					{
						ArrayPoolEventSource log = ArrayPoolEventSource.Log;
						int num2 = 1;
						if (pressure != TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.Medium)
						{
							if (pressure == TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High)
							{
								num2 = 8;
								if (bucketSize > 16384)
								{
									num2++;
								}
								if (Unsafe.SizeOf<T>() > 16)
								{
									num2++;
								}
								if (Unsafe.SizeOf<T>() > 32)
								{
									num2++;
								}
							}
						}
						else
						{
							num2 = 2;
						}
						while (this._count > 0 && num2-- > 0)
						{
							T[][] arrays = this._arrays;
							int num3 = this._count - 1;
							this._count = num3;
							T[] array = arrays[num3];
							this._arrays[this._count] = null;
							if (log.IsEnabled())
							{
								log.BufferTrimmed(array.GetHashCode(), array.Length, id);
							}
						}
						if (this._count > 0 && this._firstStackItemMS < 4294952295U)
						{
							this._firstStackItemMS += 15000U;
						}
					}
				}
			}

			// Token: 0x04000994 RID: 2452
			private readonly T[][] _arrays = new T[8][];

			// Token: 0x04000995 RID: 2453
			private int _count;

			// Token: 0x04000996 RID: 2454
			private uint _firstStackItemMS;
		}
	}
}
