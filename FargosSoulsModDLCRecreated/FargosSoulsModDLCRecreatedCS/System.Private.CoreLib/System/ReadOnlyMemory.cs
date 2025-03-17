using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000175 RID: 373
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly struct ReadOnlyMemory<[Nullable(2)] T> : IEquatable<ReadOnlyMemory<T>>
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x000E8022 File Offset: 0x000E7222
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlyMemory<T>);
				return;
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000E8046 File Offset: 0x000E7246
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory([Nullable(new byte[]
		{
			2,
			1
		})] T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(ReadOnlyMemory<T>);
				return;
			}
			if ((ulong)start + (ulong)length > (ulong)array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000E8083 File Offset: 0x000E7283
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlyMemory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000E809A File Offset: 0x000E729A
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator ReadOnlyMemory<T>([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000E80A2 File Offset: 0x000E72A2
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator ReadOnlyMemory<T>([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x000E80C0 File Offset: 0x000E72C0
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlyMemory<T> Empty
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return default(ReadOnlyMemory<T>);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000E80D6 File Offset: 0x000E72D6
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x000E80DE File Offset: 0x000E72DE
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000E80EC File Offset: 0x000E72EC
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.ReadOnlyMemory<{0}>[{1}]", typeof(T).Name, this._length);
			}
			string text = this._object as string;
			if (text == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000E816F File Offset: 0x000E736F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlyMemory<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, this._length - start);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000E819C File Offset: 0x000E739C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			if ((ulong)start + (ulong)length > (ulong)this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, length);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000E81C8 File Offset: 0x000E73C8
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlySpan<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				ref T ptr = ref Unsafe.NullRef<T>();
				int num = 0;
				object @object = this._object;
				if (@object != null)
				{
					if (typeof(T) == typeof(char) && @object.GetType() == typeof(string))
					{
						ptr = Unsafe.As<char, T>(Unsafe.As<string>(@object).GetRawStringData());
						num = Unsafe.As<string>(@object).Length;
					}
					else if (RuntimeHelpers.ObjectHasComponentSize(@object))
					{
						ptr = MemoryMarshal.GetArrayDataReference<T>(Unsafe.As<T[]>(@object));
						num = Unsafe.As<T[]>(@object).Length;
					}
					else
					{
						Span<T> span = Unsafe.As<MemoryManager<T>>(@object).GetSpan();
						ptr = MemoryMarshal.GetReference<T>(span);
						num = span.Length;
					}
					UIntPtr uintPtr = (UIntPtr)(this._index & int.MaxValue);
					int length = this._length;
					if ((ulong)uintPtr + (ulong)length > (ulong)num)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException();
					}
					ptr = Unsafe.Add<T>(ref ptr, (IntPtr)uintPtr);
					num = length;
				}
				return new ReadOnlySpan<T>(ref ptr, num);
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000E82B4 File Offset: 0x000E74B4
		public void CopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000E82D8 File Offset: 0x000E74D8
		public bool TryCopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000E82FC File Offset: 0x000E74FC
		public unsafe MemoryHandle Pin()
		{
			object @object = this._object;
			if (@object == null)
			{
				return default(MemoryHandle);
			}
			if (typeof(T) == typeof(char))
			{
				string text = @object as string;
				if (text != null)
				{
					GCHandle handle = GCHandle.Alloc(@object, GCHandleType.Pinned);
					ref char value = ref Unsafe.Add<char>(text.GetRawStringData(), this._index);
					return new MemoryHandle(Unsafe.AsPointer<char>(ref value), handle, null);
				}
			}
			if (!RuntimeHelpers.ObjectHasComponentSize(@object))
			{
				return Unsafe.As<MemoryManager<T>>(@object).Pin(this._index);
			}
			if (this._index < 0)
			{
				void* pointer = Unsafe.Add<T>(Unsafe.AsPointer<T>(MemoryMarshal.GetArrayDataReference<T>(Unsafe.As<T[]>(@object))), this._index & int.MaxValue);
				return new MemoryHandle(pointer, default(GCHandle), null);
			}
			GCHandle handle2 = GCHandle.Alloc(@object, GCHandleType.Pinned);
			void* pointer2 = Unsafe.Add<T>(Unsafe.AsPointer<T>(MemoryMarshal.GetArrayDataReference<T>(Unsafe.As<T[]>(@object))), this._index);
			return new MemoryHandle(pointer2, handle2, null);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000E83F8 File Offset: 0x000E75F8
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000E8414 File Offset: 0x000E7614
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				ReadOnlyMemory<T> other = (ReadOnlyMemory<T>)obj;
				return this.Equals(other);
			}
			if (obj is Memory<T>)
			{
				Memory<T> memory = (Memory<T>)obj;
				return this.Equals(memory);
			}
			return false;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000E8455 File Offset: 0x000E7655
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlyMemory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000E8483 File Offset: 0x000E7683
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return HashCode.Combine<int, int, int>(RuntimeHelpers.GetHashCode(this._object), this._index, this._length);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000E84AB File Offset: 0x000E76AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal object GetObjectStartLength(out int start, out int length)
		{
			start = this._index;
			length = this._length;
			return this._object;
		}

		// Token: 0x04000480 RID: 1152
		private readonly object _object;

		// Token: 0x04000481 RID: 1153
		private readonly int _index;

		// Token: 0x04000482 RID: 1154
		private readonly int _length;

		// Token: 0x04000483 RID: 1155
		internal const int RemoveFlagsBitMask = 2147483647;
	}
}
