using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x020007C8 RID: 1992
	[DebuggerDisplay("Capacity = {Capacity}")]
	internal sealed class ConcurrentQueueSegment<T>
	{
		// Token: 0x06005FF9 RID: 24569 RVA: 0x001CB88C File Offset: 0x001CAA8C
		internal ConcurrentQueueSegment(int boundedLength)
		{
			this._slots = new ConcurrentQueueSegment<T>.Slot[boundedLength];
			this._slotsMask = boundedLength - 1;
			for (int i = 0; i < this._slots.Length; i++)
			{
				this._slots[i].SequenceNumber = i;
			}
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x001CB8D9 File Offset: 0x001CAAD9
		internal static int RoundUpToPowerOf2(int i)
		{
			i--;
			i |= i >> 1;
			i |= i >> 2;
			i |= i >> 4;
			i |= i >> 8;
			i |= i >> 16;
			return i + 1;
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005FFB RID: 24571 RVA: 0x001CB907 File Offset: 0x001CAB07
		internal int Capacity
		{
			get
			{
				return this._slots.Length;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005FFC RID: 24572 RVA: 0x001CB911 File Offset: 0x001CAB11
		internal int FreezeOffset
		{
			get
			{
				return this._slots.Length * 2;
			}
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x001CB91D File Offset: 0x001CAB1D
		internal void EnsureFrozenForEnqueues()
		{
			if (!this._frozenForEnqueues)
			{
				this._frozenForEnqueues = true;
				Interlocked.Add(ref this._headAndTail.Tail, this.FreezeOffset);
			}
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x001CB948 File Offset: 0x001CAB48
		public bool TryDequeue([MaybeNullWhen(false)] out T item)
		{
			ConcurrentQueueSegment<T>.Slot[] slots = this._slots;
			SpinWait spinWait = default(SpinWait);
			int num;
			int num2;
			for (;;)
			{
				num = Volatile.Read(ref this._headAndTail.Head);
				num2 = (num & this._slotsMask);
				int num3 = Volatile.Read(ref slots[num2].SequenceNumber);
				int num4 = num3 - (num + 1);
				if (num4 == 0)
				{
					if (Interlocked.CompareExchange(ref this._headAndTail.Head, num + 1, num) == num)
					{
						break;
					}
				}
				else if (num4 < 0)
				{
					bool frozenForEnqueues = this._frozenForEnqueues;
					int num5 = Volatile.Read(ref this._headAndTail.Tail);
					if (num5 - num <= 0 || (frozenForEnqueues && num5 - this.FreezeOffset - num <= 0))
					{
						goto IL_E3;
					}
				}
				spinWait.SpinOnce(-1);
			}
			item = slots[num2].Item;
			if (!Volatile.Read(ref this._preservedForObservation))
			{
				slots[num2].Item = default(T);
				Volatile.Write(ref slots[num2].SequenceNumber, num + slots.Length);
			}
			return true;
			IL_E3:
			item = default(T);
			return false;
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x001CBA50 File Offset: 0x001CAC50
		public bool TryPeek([MaybeNullWhen(false)] out T result, bool resultUsed)
		{
			if (resultUsed)
			{
				this._preservedForObservation = true;
				Interlocked.MemoryBarrier();
			}
			ConcurrentQueueSegment<T>.Slot[] slots = this._slots;
			SpinWait spinWait = default(SpinWait);
			int num2;
			for (;;)
			{
				int num = Volatile.Read(ref this._headAndTail.Head);
				num2 = (num & this._slotsMask);
				int num3 = Volatile.Read(ref slots[num2].SequenceNumber);
				int num4 = num3 - (num + 1);
				if (num4 == 0)
				{
					break;
				}
				if (num4 < 0)
				{
					bool frozenForEnqueues = this._frozenForEnqueues;
					int num5 = Volatile.Read(ref this._headAndTail.Tail);
					if (num5 - num <= 0 || (frozenForEnqueues && num5 - this.FreezeOffset - num <= 0))
					{
						goto IL_B2;
					}
				}
				spinWait.SpinOnce(-1);
			}
			result = (resultUsed ? slots[num2].Item : default(T));
			return true;
			IL_B2:
			result = default(T);
			return false;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x001CBB24 File Offset: 0x001CAD24
		public bool TryEnqueue(T item)
		{
			ConcurrentQueueSegment<T>.Slot[] slots = this._slots;
			SpinWait spinWait = default(SpinWait);
			int num;
			int num2;
			for (;;)
			{
				num = Volatile.Read(ref this._headAndTail.Tail);
				num2 = (num & this._slotsMask);
				int num3 = Volatile.Read(ref slots[num2].SequenceNumber);
				int num4 = num3 - num;
				if (num4 == 0)
				{
					if (Interlocked.CompareExchange(ref this._headAndTail.Tail, num + 1, num) == num)
					{
						break;
					}
				}
				else if (num4 < 0)
				{
					return false;
				}
				spinWait.SpinOnce(-1);
			}
			slots[num2].Item = item;
			Volatile.Write(ref slots[num2].SequenceNumber, num + 1);
			return true;
		}

		// Token: 0x04001CF3 RID: 7411
		internal readonly ConcurrentQueueSegment<T>.Slot[] _slots;

		// Token: 0x04001CF4 RID: 7412
		internal readonly int _slotsMask;

		// Token: 0x04001CF5 RID: 7413
		internal PaddedHeadAndTail _headAndTail;

		// Token: 0x04001CF6 RID: 7414
		internal bool _preservedForObservation;

		// Token: 0x04001CF7 RID: 7415
		internal bool _frozenForEnqueues;

		// Token: 0x04001CF8 RID: 7416
		internal ConcurrentQueueSegment<T> _nextSegment;

		// Token: 0x020007C9 RID: 1993
		[DebuggerDisplay("Item = {Item}, SequenceNumber = {SequenceNumber}")]
		[StructLayout(LayoutKind.Auto)]
		internal struct Slot
		{
			// Token: 0x04001CF9 RID: 7417
			public T Item;

			// Token: 0x04001CFA RID: 7418
			public int SequenceNumber;
		}
	}
}
