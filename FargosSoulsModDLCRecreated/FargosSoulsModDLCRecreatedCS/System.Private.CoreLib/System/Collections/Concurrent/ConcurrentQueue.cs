using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020007C6 RID: 1990
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	public class ConcurrentQueue<[Nullable(2)] T> : IProducerConsumerCollection<T>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06005FDA RID: 24538 RVA: 0x001CAC3C File Offset: 0x001C9E3C
		public ConcurrentQueue()
		{
			this._crossSegmentLock = new object();
			this._tail = (this._head = new ConcurrentQueueSegment<T>(32));
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x001CAC74 File Offset: 0x001C9E74
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this._crossSegmentLock = new object();
			int num = 32;
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > num)
				{
					num = Math.Min(ConcurrentQueueSegment<T>.RoundUpToPowerOf2(count), 1048576);
				}
			}
			this._tail = (this._head = new ConcurrentQueueSegment<T>(num));
			foreach (T item in collection)
			{
				this.Enqueue(item);
			}
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x001CAD20 File Offset: 0x001C9F20
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this.ToArray().CopyTo(array, index);
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005FDE RID: 24542 RVA: 0x001CAD56 File Offset: 0x001C9F56
		object ICollection.SyncRoot
		{
			get
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.ConcurrentCollection_SyncRoot_NotSupported);
				return null;
			}
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x001CAD60 File Offset: 0x001C9F60
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x001CAD68 File Offset: 0x001C9F68
		bool IProducerConsumerCollection<!0>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x0014D0AD File Offset: 0x0014C2AD
		bool IProducerConsumerCollection<!0>.TryTake([MaybeNullWhen(false)] out T item)
		{
			return this.TryDequeue(out item);
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x001CAD74 File Offset: 0x001C9F74
		public bool IsEmpty
		{
			get
			{
				T t;
				return !this.TryPeek(out t, false);
			}
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x001CAD90 File Offset: 0x001C9F90
		public T[] ToArray()
		{
			ConcurrentQueueSegment<T> head;
			int headHead;
			ConcurrentQueueSegment<T> tail;
			int tailTail;
			this.SnapForObservation(out head, out headHead, out tail, out tailTail);
			long count = ConcurrentQueue<T>.GetCount(head, headHead, tail, tailTail);
			T[] array = new T[count];
			using (IEnumerator<T> enumerator = this.Enumerate(head, headHead, tail, tailTail))
			{
				int num = 0;
				while (enumerator.MoveNext())
				{
					!0 ! = enumerator.Current;
					array[num++] = !;
				}
			}
			return array;
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06005FE4 RID: 24548 RVA: 0x001CAE10 File Offset: 0x001CA010
		public int Count
		{
			get
			{
				SpinWait spinWait = default(SpinWait);
				ConcurrentQueueSegment<T> head;
				ConcurrentQueueSegment<T> tail;
				int num;
				int num2;
				int num3;
				int num4;
				for (;;)
				{
					head = this._head;
					tail = this._tail;
					num = Volatile.Read(ref head._headAndTail.Head);
					num2 = Volatile.Read(ref head._headAndTail.Tail);
					if (head == tail)
					{
						if (head == this._head && tail == this._tail && num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail))
						{
							break;
						}
					}
					else if (head._nextSegment == tail)
					{
						num3 = Volatile.Read(ref tail._headAndTail.Head);
						num4 = Volatile.Read(ref tail._headAndTail.Tail);
						if (head == this._head && tail == this._tail && num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail) && num3 == Volatile.Read(ref tail._headAndTail.Head) && num4 == Volatile.Read(ref tail._headAndTail.Tail))
						{
							goto Block_12;
						}
					}
					else
					{
						object crossSegmentLock = this._crossSegmentLock;
						lock (crossSegmentLock)
						{
							if (head == this._head && tail == this._tail)
							{
								int num5 = Volatile.Read(ref tail._headAndTail.Head);
								int num6 = Volatile.Read(ref tail._headAndTail.Tail);
								if (num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail) && num5 == Volatile.Read(ref tail._headAndTail.Head) && num6 == Volatile.Read(ref tail._headAndTail.Tail))
								{
									int num7 = ConcurrentQueue<T>.GetCount(head, num, num2) + ConcurrentQueue<T>.GetCount(tail, num5, num6);
									for (ConcurrentQueueSegment<T> nextSegment = head._nextSegment; nextSegment != tail; nextSegment = nextSegment._nextSegment)
									{
										num7 += nextSegment._headAndTail.Tail - nextSegment.FreezeOffset;
									}
									return num7;
								}
							}
						}
					}
					spinWait.SpinOnce();
				}
				return ConcurrentQueue<T>.GetCount(head, num, num2);
				Block_12:
				return ConcurrentQueue<T>.GetCount(head, num, num2) + ConcurrentQueue<T>.GetCount(tail, num3, num4);
			}
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x001CB08C File Offset: 0x001CA28C
		private static int GetCount(ConcurrentQueueSegment<T> s, int head, int tail)
		{
			if (head == tail || head == tail - s.FreezeOffset)
			{
				return 0;
			}
			head &= s._slotsMask;
			tail &= s._slotsMask;
			if (head >= tail)
			{
				return s._slots.Length - head + tail;
			}
			return tail - head;
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x001CB0C8 File Offset: 0x001CA2C8
		private static long GetCount(ConcurrentQueueSegment<T> head, int headHead, ConcurrentQueueSegment<T> tail, int tailTail)
		{
			long num = 0L;
			int num2 = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
			if (headHead < num2)
			{
				headHead &= head._slotsMask;
				num2 &= head._slotsMask;
				num += (long)((headHead < num2) ? (num2 - headHead) : (head._slots.Length - headHead + num2));
			}
			if (head != tail)
			{
				for (ConcurrentQueueSegment<T> nextSegment = head._nextSegment; nextSegment != tail; nextSegment = nextSegment._nextSegment)
				{
					num += (long)(nextSegment._headAndTail.Tail - nextSegment.FreezeOffset);
				}
				num += (long)(tailTail - tail.FreezeOffset);
			}
			return num;
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x001CB164 File Offset: 0x001CA364
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			ConcurrentQueueSegment<T> head;
			int headHead;
			ConcurrentQueueSegment<T> tail;
			int tailTail;
			this.SnapForObservation(out head, out headHead, out tail, out tailTail);
			long count = ConcurrentQueue<T>.GetCount(head, headHead, tail, tailTail);
			if ((long)index > (long)array.Length - count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int num = index;
			using (IEnumerator<T> enumerator = this.Enumerate(head, headHead, tail, tailTail))
			{
				while (enumerator.MoveNext())
				{
					!0 ! = enumerator.Current;
					array[num++] = !;
				}
			}
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x001CB1FC File Offset: 0x001CA3FC
		public IEnumerator<T> GetEnumerator()
		{
			ConcurrentQueueSegment<T> head;
			int headHead;
			ConcurrentQueueSegment<T> tail;
			int tailTail;
			this.SnapForObservation(out head, out headHead, out tail, out tailTail);
			return this.Enumerate(head, headHead, tail, tailTail);
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x001CB224 File Offset: 0x001CA424
		private void SnapForObservation(out ConcurrentQueueSegment<T> head, out int headHead, out ConcurrentQueueSegment<T> tail, out int tailTail)
		{
			object crossSegmentLock = this._crossSegmentLock;
			lock (crossSegmentLock)
			{
				head = this._head;
				tail = this._tail;
				ConcurrentQueueSegment<T> concurrentQueueSegment = head;
				for (;;)
				{
					concurrentQueueSegment._preservedForObservation = true;
					if (concurrentQueueSegment == tail)
					{
						break;
					}
					concurrentQueueSegment = concurrentQueueSegment._nextSegment;
				}
				tail.EnsureFrozenForEnqueues();
				headHead = Volatile.Read(ref head._headAndTail.Head);
				tailTail = Volatile.Read(ref tail._headAndTail.Tail);
			}
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x001CB2B8 File Offset: 0x001CA4B8
		private static T GetItemWhenAvailable(ConcurrentQueueSegment<T> segment, int i)
		{
			int num = i + 1 & segment._slotsMask;
			if ((segment._slots[i].SequenceNumber & segment._slotsMask) != num)
			{
				SpinWait spinWait = default(SpinWait);
				while ((Volatile.Read(ref segment._slots[i].SequenceNumber) & segment._slotsMask) != num)
				{
					spinWait.SpinOnce();
				}
			}
			return segment._slots[i].Item;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x001CB32D File Offset: 0x001CA52D
		private IEnumerator<T> Enumerate(ConcurrentQueueSegment<T> head, int headHead, ConcurrentQueueSegment<T> tail, int tailTail)
		{
			int headTail = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
			if (headHead < headTail)
			{
				headHead &= head._slotsMask;
				headTail &= head._slotsMask;
				if (headHead < headTail)
				{
					int num;
					for (int i = headHead; i < headTail; i = num + 1)
					{
						yield return ConcurrentQueue<T>.GetItemWhenAvailable(head, i);
						num = i;
					}
				}
				else
				{
					int num;
					for (int i = headHead; i < head._slots.Length; i = num + 1)
					{
						yield return ConcurrentQueue<T>.GetItemWhenAvailable(head, i);
						num = i;
					}
					for (int i = 0; i < headTail; i = num + 1)
					{
						yield return ConcurrentQueue<T>.GetItemWhenAvailable(head, i);
						num = i;
					}
				}
			}
			if (head != tail)
			{
				int num;
				ConcurrentQueueSegment<T> s;
				for (s = head._nextSegment; s != tail; s = s._nextSegment)
				{
					int i = s._headAndTail.Tail - s.FreezeOffset;
					for (int j = 0; j < i; j = num + 1)
					{
						yield return ConcurrentQueue<T>.GetItemWhenAvailable(s, j);
						num = j;
					}
				}
				s = null;
				tailTail -= tail.FreezeOffset;
				for (int i = 0; i < tailTail; i = num + 1)
				{
					yield return ConcurrentQueue<T>.GetItemWhenAvailable(tail, i);
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x001CB352 File Offset: 0x001CA552
		public void Enqueue(T item)
		{
			if (!this._tail.TryEnqueue(item))
			{
				this.EnqueueSlow(item);
			}
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x001CB36C File Offset: 0x001CA56C
		private void EnqueueSlow(T item)
		{
			for (;;)
			{
				ConcurrentQueueSegment<T> tail = this._tail;
				if (tail.TryEnqueue(item))
				{
					break;
				}
				object crossSegmentLock = this._crossSegmentLock;
				lock (crossSegmentLock)
				{
					if (tail == this._tail)
					{
						tail.EnsureFrozenForEnqueues();
						int boundedLength = tail._preservedForObservation ? 32 : Math.Min(tail.Capacity * 2, 1048576);
						ConcurrentQueueSegment<T> concurrentQueueSegment = new ConcurrentQueueSegment<T>(boundedLength);
						tail._nextSegment = concurrentQueueSegment;
						this._tail = concurrentQueueSegment;
					}
				}
			}
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x001CB410 File Offset: 0x001CA610
		public bool TryDequeue([MaybeNullWhen(false)] out T result)
		{
			return this._head.TryDequeue(out result) || this.TryDequeueSlow(out result);
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x001CB42C File Offset: 0x001CA62C
		private bool TryDequeueSlow([MaybeNullWhen(false)] out T item)
		{
			for (;;)
			{
				ConcurrentQueueSegment<T> head = this._head;
				if (head.TryDequeue(out item))
				{
					break;
				}
				if (head._nextSegment == null)
				{
					goto Block_1;
				}
				if (head.TryDequeue(out item))
				{
					return true;
				}
				object crossSegmentLock = this._crossSegmentLock;
				lock (crossSegmentLock)
				{
					if (head == this._head)
					{
						this._head = head._nextSegment;
					}
				}
			}
			return true;
			Block_1:
			item = default(T);
			return false;
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x001CB4BC File Offset: 0x001CA6BC
		public bool TryPeek([MaybeNullWhen(false)] out T result)
		{
			return this.TryPeek(out result, true);
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x001CB4C8 File Offset: 0x001CA6C8
		private bool TryPeek([MaybeNullWhen(false)] out T result, bool resultUsed)
		{
			ConcurrentQueueSegment<T> concurrentQueueSegment = this._head;
			for (;;)
			{
				ConcurrentQueueSegment<T> concurrentQueueSegment2 = Volatile.Read<ConcurrentQueueSegment<T>>(ref concurrentQueueSegment._nextSegment);
				if (concurrentQueueSegment.TryPeek(out result, resultUsed))
				{
					break;
				}
				if (concurrentQueueSegment2 != null)
				{
					concurrentQueueSegment = concurrentQueueSegment2;
				}
				else if (Volatile.Read<ConcurrentQueueSegment<T>>(ref concurrentQueueSegment._nextSegment) == null)
				{
					goto Block_3;
				}
			}
			return true;
			Block_3:
			result = default(T);
			return false;
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x001CB514 File Offset: 0x001CA714
		public void Clear()
		{
			object crossSegmentLock = this._crossSegmentLock;
			lock (crossSegmentLock)
			{
				this._tail.EnsureFrozenForEnqueues();
				this._tail = (this._head = new ConcurrentQueueSegment<T>(32));
			}
		}

		// Token: 0x04001CE6 RID: 7398
		private readonly object _crossSegmentLock;

		// Token: 0x04001CE7 RID: 7399
		private volatile ConcurrentQueueSegment<T> _tail;

		// Token: 0x04001CE8 RID: 7400
		private volatile ConcurrentQueueSegment<T> _head;
	}
}
