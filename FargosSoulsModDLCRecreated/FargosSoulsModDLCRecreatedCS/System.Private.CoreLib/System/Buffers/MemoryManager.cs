using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x0200024E RID: 590
	public abstract class MemoryManager<[Nullable(2)] T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x001395D8 File Offset: 0x001387D8
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public virtual Memory<T> Memory
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x06002440 RID: 9280
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public abstract Span<T> GetSpan();

		// Token: 0x06002441 RID: 9281
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x06002442 RID: 9282
		public abstract void Unpin();

		// Token: 0x06002443 RID: 9283 RVA: 0x001395F9 File Offset: 0x001387F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00139602 File Offset: 0x00138802
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x0013960C File Offset: 0x0013880C
		protected internal virtual bool TryGetArray([Nullable(new byte[]
		{
			0,
			1
		})] out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00139616 File Offset: 0x00138816
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002447 RID: 9287
		protected abstract void Dispose(bool disposing);
	}
}
