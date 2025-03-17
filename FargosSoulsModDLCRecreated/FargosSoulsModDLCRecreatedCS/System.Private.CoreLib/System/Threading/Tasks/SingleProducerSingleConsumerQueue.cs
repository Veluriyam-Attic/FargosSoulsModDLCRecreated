using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Internal;

namespace System.Threading.Tasks
{
	// Token: 0x02000307 RID: 775
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06002A05 RID: 10757 RVA: 0x0014D0D0 File Offset: 0x0014C2D0
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0014D100 File Offset: 0x0014C300
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = last + 1 & array.Length - 1;
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0014D164 File Offset: 0x0014C364
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0014D22C File Offset: 0x0014C42C
		public bool TryDequeue([MaybeNullWhen(false)] out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1 & array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0014D2A8 File Offset: 0x0014C4A8
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, [MaybeNullWhen(false)] out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1 & segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0014D3B8 File Offset: 0x0014C5B8
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0014D411 File Offset: 0x0014C611
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1 & segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0014D420 File Offset: 0x0014C620
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x0014D428 File Offset: 0x0014C628
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first & num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x04000B88 RID: 2952
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04000B89 RID: 2953
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x02000308 RID: 776
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x06002A0E RID: 10766 RVA: 0x0014D48E File Offset: 0x0014C68E
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x04000B8A RID: 2954
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x04000B8B RID: 2955
			internal readonly T[] m_array;

			// Token: 0x04000B8C RID: 2956
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x02000309 RID: 777
		private struct SegmentState
		{
			// Token: 0x04000B8D RID: 2957
			internal PaddingFor32 m_pad0;

			// Token: 0x04000B8E RID: 2958
			internal volatile int m_first;

			// Token: 0x04000B8F RID: 2959
			internal int m_lastCopy;

			// Token: 0x04000B90 RID: 2960
			internal PaddingFor32 m_pad1;

			// Token: 0x04000B91 RID: 2961
			internal int m_firstCopy;

			// Token: 0x04000B92 RID: 2962
			internal volatile int m_last;

			// Token: 0x04000B93 RID: 2963
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x0200030A RID: 778
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x06002A0F RID: 10767 RVA: 0x0014D4A2 File Offset: 0x0014C6A2
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x04000B94 RID: 2964
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}
	}
}
