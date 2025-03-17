using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000249 RID: 585
	internal sealed class ConfigurableArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x06002430 RID: 9264 RVA: 0x00139208 File Offset: 0x00138408
		internal ConfigurableArrayPool() : this(1048576, 50)
		{
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00139218 File Offset: 0x00138418
		internal ConfigurableArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			int num = Utilities.SelectBucketIndex(maxArrayLength);
			ConfigurableArrayPool<T>.Bucket[] array = new ConfigurableArrayPool<T>.Bucket[num + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ConfigurableArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x0013929F File Offset: 0x0013849F
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x001392A8 File Offset: 0x001384A8
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
			T[] array;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array = this._buckets[num2].Rent();
					if (array != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_84;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array.GetHashCode(), array.Length, this.Id, this._buckets[num2].Id);
				}
				return array;
				IL_84:
				array = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array.GetHashCode();
				int bucketId = -1;
				log.BufferRented(hashCode, array.Length, this.Id, bucketId);
				log.BufferAllocated(hashCode, array.Length, this.Id, bucketId, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x001393A0 File Offset: 0x001385A0
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04000977 RID: 2423
		private readonly ConfigurableArrayPool<T>.Bucket[] _buckets;

		// Token: 0x0200024A RID: 586
		private sealed class Bucket
		{
			// Token: 0x06002435 RID: 9269 RVA: 0x00139412 File Offset: 0x00138612
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06002436 RID: 9270 RVA: 0x0013929F File Offset: 0x0013849F
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x06002437 RID: 9271 RVA: 0x00139444 File Offset: 0x00138644
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = (array == null);
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x06002438 RID: 9272 RVA: 0x00139500 File Offset: 0x00138700
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException(SR.ArgumentException_BufferNotFromPool, "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x04000978 RID: 2424
			internal readonly int _bufferLength;

			// Token: 0x04000979 RID: 2425
			private readonly T[][] _buffers;

			// Token: 0x0400097A RID: 2426
			private readonly int _poolId;

			// Token: 0x0400097B RID: 2427
			private SpinLock _lock;

			// Token: 0x0400097C RID: 2428
			private int _index;
		}
	}
}
