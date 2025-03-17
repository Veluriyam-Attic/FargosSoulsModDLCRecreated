using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200014C RID: 332
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly struct Memory<[Nullable(2)] T> : IEquatable<Memory<T>>
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x000DBB94 File Offset: 0x000DAD94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			if (array == null)
			{
				this = default(Memory<T>);
				return;
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000DBBF0 File Offset: 0x000DADF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = array.Length - start;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000DBC64 File Offset: 0x000DAE64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory([Nullable(new byte[]
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
				this = default(Memory<T>);
				return;
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if ((ulong)start + (ulong)length > (ulong)array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000DBCD9 File Offset: 0x000DAED9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int length)
		{
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = 0;
			this._length = length;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000DBCF9 File Offset: 0x000DAEF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int start, int length)
		{
			if (length < 0 || start < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000DBD1D File Offset: 0x000DAF1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000DBD34 File Offset: 0x000DAF34
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator Memory<T>([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000DBD3C File Offset: 0x000DAF3C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator Memory<T>([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000DBD58 File Offset: 0x000DAF58
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static implicit operator ReadOnlyMemory<T>([Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> memory)
		{
			return *Unsafe.As<Memory<T>, ReadOnlyMemory<T>>(ref memory);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000DBD68 File Offset: 0x000DAF68
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> Empty
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return default(Memory<T>);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000DBD7E File Offset: 0x000DAF7E
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x000DBD86 File Offset: 0x000DAF86
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000DBD94 File Offset: 0x000DAF94
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.Memory<{0}>[{1}]", typeof(T).Name, this._length);
			}
			string text = this._object as string;
			if (text == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000DBE17 File Offset: 0x000DB017
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public Memory<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(this._object, this._index + start, this._length - start);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000DBE44 File Offset: 0x000DB044
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public Memory<T> Slice(int start, int length)
		{
			if ((ulong)start + (ulong)length > (ulong)this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(this._object, this._index + start, length);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000DBE70 File Offset: 0x000DB070
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public Span<T> Span
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
				return new Span<T>(ref ptr, num);
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000DBF5C File Offset: 0x000DB15C
		public void CopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000DBF80 File Offset: 0x000DB180
		public bool TryCopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000DBFA4 File Offset: 0x000DB1A4
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

		// Token: 0x06001092 RID: 4242 RVA: 0x000DC0A0 File Offset: 0x000DB2A0
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000DC0BC File Offset: 0x000DB2BC
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			if (obj is Memory<T>)
			{
				Memory<T> other = (Memory<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000DC103 File Offset: 0x000DB303
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000DC131 File Offset: 0x000DB331
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return HashCode.Combine<int, int, int>(RuntimeHelpers.GetHashCode(this._object), this._index, this._length);
		}

		// Token: 0x0400041C RID: 1052
		private readonly object _object;

		// Token: 0x0400041D RID: 1053
		private readonly int _index;

		// Token: 0x0400041E RID: 1054
		private readonly int _length;
	}
}
