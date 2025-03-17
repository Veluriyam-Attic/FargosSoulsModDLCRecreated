using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200044C RID: 1100
	[Nullable(0)]
	[NullableContext(2)]
	public struct GCHandle
	{
		// Token: 0x060042C7 RID: 17095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalAlloc(object value, GCHandleType type);

		// Token: 0x060042C8 RID: 17096
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalFree(IntPtr handle);

		// Token: 0x060042C9 RID: 17097 RVA: 0x001754DA File Offset: 0x001746DA
		internal unsafe static object InternalGet(IntPtr handle)
		{
			return *Unsafe.As<IntPtr, object>(ref *(IntPtr*)((void*)handle));
		}

		// Token: 0x060042CA RID: 17098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSet(IntPtr handle, object value);

		// Token: 0x060042CB RID: 17099
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalCompareExchange(IntPtr handle, object value, object oldValue);

		// Token: 0x060042CC RID: 17100 RVA: 0x001754E8 File Offset: 0x001746E8
		private GCHandle(object value, GCHandleType type)
		{
			if (type > GCHandleType.Pinned)
			{
				throw new ArgumentOutOfRangeException("type", SR.ArgumentOutOfRange_Enum);
			}
			if (type == GCHandleType.Pinned && !Marshal.IsPinnable(value))
			{
				throw new ArgumentException(SR.ArgumentException_NotIsomorphic, "value");
			}
			IntPtr intPtr = GCHandle.InternalAlloc(value, type);
			if (type == GCHandleType.Pinned)
			{
				intPtr |= (IntPtr)1;
			}
			this._handle = intPtr;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0017553D File Offset: 0x0017473D
		private GCHandle(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x00175546 File Offset: 0x00174746
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value, GCHandleType.Normal);
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0017554F File Offset: 0x0017474F
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x00175558 File Offset: 0x00174758
		public void Free()
		{
			IntPtr handle = Interlocked.Exchange(ref this._handle, IntPtr.Zero);
			GCHandle.ThrowIfInvalid(handle);
			GCHandle.InternalFree(GCHandle.GetHandleValue(handle));
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x00175588 File Offset: 0x00174788
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x001755B0 File Offset: 0x001747B0
		public object Target
		{
			get
			{
				IntPtr handle = this._handle;
				GCHandle.ThrowIfInvalid(handle);
				return GCHandle.InternalGet(GCHandle.GetHandleValue(handle));
			}
			set
			{
				IntPtr handle = this._handle;
				GCHandle.ThrowIfInvalid(handle);
				if (GCHandle.IsPinned(handle) && !Marshal.IsPinnable(value))
				{
					throw new ArgumentException(SR.ArgumentException_NotIsomorphic, "value");
				}
				GCHandle.InternalSet(GCHandle.GetHandleValue(handle), value);
			}
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x001755F8 File Offset: 0x001747F8
		public IntPtr AddrOfPinnedObject()
		{
			IntPtr handle = this._handle;
			GCHandle.ThrowIfInvalid(handle);
			if (!GCHandle.IsPinned(handle))
			{
				ThrowHelper.ThrowInvalidOperationException_HandleIsNotPinned();
			}
			object obj = GCHandle.InternalGet(GCHandle.GetHandleValue(handle));
			if (obj == null)
			{
				return (IntPtr)0;
			}
			if (!RuntimeHelpers.ObjectHasComponentSize(obj))
			{
				return (IntPtr)Unsafe.AsPointer<byte>(obj.GetRawData());
			}
			if (obj.GetType() == typeof(string))
			{
				return (IntPtr)Unsafe.AsPointer<char>(Unsafe.As<string>(obj).GetRawStringData());
			}
			return (IntPtr)Unsafe.AsPointer<byte>(Unsafe.As<Array>(obj).GetRawArrayData());
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x0017568C File Offset: 0x0017488C
		public bool IsAllocated
		{
			get
			{
				return this._handle != IntPtr.Zero;
			}
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0017569E File Offset: 0x0017489E
		public static explicit operator GCHandle(IntPtr value)
		{
			return GCHandle.FromIntPtr(value);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x001756A6 File Offset: 0x001748A6
		public static GCHandle FromIntPtr(IntPtr value)
		{
			GCHandle.ThrowIfInvalid(value);
			return new GCHandle(value);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x001756B4 File Offset: 0x001748B4
		public static explicit operator IntPtr(GCHandle value)
		{
			return GCHandle.ToIntPtr(value);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x001756BC File Offset: 0x001748BC
		public static IntPtr ToIntPtr(GCHandle value)
		{
			return value._handle;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x001756C4 File Offset: 0x001748C4
		public override int GetHashCode()
		{
			return this._handle.GetHashCode();
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x001756D1 File Offset: 0x001748D1
		public override bool Equals(object o)
		{
			return o is GCHandle && this._handle == ((GCHandle)o)._handle;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x001756F3 File Offset: 0x001748F3
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a._handle == b._handle;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x00175706 File Offset: 0x00174906
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return a._handle != b._handle;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x00175719 File Offset: 0x00174919
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IntPtr GetHandleValue(IntPtr handle)
		{
			return new IntPtr((long)(handle & ~(IntPtr)1));
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x00175726 File Offset: 0x00174926
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsPinned(IntPtr handle)
		{
			return (handle & (IntPtr)1) > (IntPtr)0;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00175730 File Offset: 0x00174930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ThrowIfInvalid(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				ThrowHelper.ThrowInvalidOperationException_HandleIsNotInitialized();
			}
		}

		// Token: 0x04000EA4 RID: 3748
		private IntPtr _handle;
	}
}
