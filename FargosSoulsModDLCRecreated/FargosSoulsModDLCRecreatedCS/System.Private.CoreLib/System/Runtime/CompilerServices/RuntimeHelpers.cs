using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004F3 RID: 1267
	[Nullable(0)]
	[NullableContext(1)]
	public static class RuntimeHelpers
	{
		// Token: 0x0600460B RID: 17931
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

		// Token: 0x0600460C RID: 17932
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: NotNullIfNotNull("obj")]
		public static extern object GetObjectValue(object obj);

		// Token: 0x0600460D RID: 17933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunClassConstructor(RuntimeType type);

		// Token: 0x0600460E RID: 17934 RVA: 0x0017A908 File Offset: 0x00179B08
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			RuntimeHelpers._RunClassConstructor(type.GetRuntimeType());
		}

		// Token: 0x0600460F RID: 17935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunModuleConstructor(RuntimeModule module);

		// Token: 0x06004610 RID: 17936 RVA: 0x0017A916 File Offset: 0x00179B16
		public static void RunModuleConstructor(ModuleHandle module)
		{
			RuntimeHelpers._RunModuleConstructor(module.GetRuntimeModule());
		}

		// Token: 0x06004611 RID: 17937
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _CompileMethod(RuntimeMethodHandleInternal method);

		// Token: 0x06004612 RID: 17938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _PrepareMethod(IRuntimeMethodInfo method, IntPtr* pInstantiation, int cInstantiation);

		// Token: 0x06004613 RID: 17939 RVA: 0x0017A924 File Offset: 0x00179B24
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
			RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), null, 0);
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x0017A938 File Offset: 0x00179B38
		[NullableContext(2)]
		public unsafe static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
			int cInstantiation;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(instantiation, out cInstantiation);
			IntPtr[] array2;
			IntPtr* pInstantiation;
			if ((array2 = array) == null || array2.Length == 0)
			{
				pInstantiation = null;
			}
			else
			{
				pInstantiation = &array2[0];
			}
			RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), pInstantiation, cInstantiation);
			GC.KeepAlive(instantiation);
			array2 = null;
		}

		// Token: 0x06004615 RID: 17941
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareDelegate(Delegate d);

		// Token: 0x06004616 RID: 17942
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHashCode(object o);

		// Token: 0x06004617 RID: 17943
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public new static extern bool Equals(object o1, object o2);

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x000FCB9D File Offset: 0x000FBD9D
		public static int OffsetToStringData
		{
			[NonVersionable]
			get
			{
				return 12;
			}
		}

		// Token: 0x06004619 RID: 17945
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnsureSufficientExecutionStack();

		// Token: 0x0600461A RID: 17946
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryEnsureSufficientExecutionStack();

		// Token: 0x0600461B RID: 17947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetUninitializedObjectInternal([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type type);

		// Token: 0x0600461C RID: 17948
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AllocateUninitializedClone(object obj);

		// Token: 0x0600461D RID: 17949 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		[NullableContext(2)]
		[Intrinsic]
		public static bool IsReferenceOrContainsReferences<T>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		[Intrinsic]
		internal static bool IsBitwiseEquatable<T>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0017A97C File Offset: 0x00179B7C
		[Intrinsic]
		internal static bool EnumEquals<T>(T x, T y) where T : struct, Enum
		{
			return x.Equals(y);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x0017A991 File Offset: 0x00179B91
		[Intrinsic]
		internal static int EnumCompareTo<T>(T x, T y) where T : struct, Enum
		{
			return x.CompareTo(y);
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x0017A9A6 File Offset: 0x00179BA6
		internal static ref byte GetRawData(this object obj)
		{
			return ref Unsafe.As<RawData>(obj).Data;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x0017A9B4 File Offset: 0x00179BB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		internal unsafe static UIntPtr GetRawObjectDataSize(object obj)
		{
			MethodTable* methodTable = RuntimeHelpers.GetMethodTable(obj);
			UIntPtr uintPtr = (UIntPtr)methodTable->BaseSize - (UIntPtr)((IntPtr)(2 * sizeof(IntPtr)));
			if (methodTable->HasComponentSize)
			{
				uintPtr += (UIntPtr)Unsafe.As<RawArrayData>(obj).Length * (UIntPtr)methodTable->ComponentSize;
			}
			GC.KeepAlive(obj);
			return uintPtr;
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x0017AA00 File Offset: 0x00179C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static ref byte GetRawArrayData(this Array array)
		{
			return Unsafe.AddByteOffset<byte>(ref Unsafe.As<RawData>(array).Data, (UIntPtr)RuntimeHelpers.GetMethodTable(array)->BaseSize - (UIntPtr)((IntPtr)(2 * sizeof(IntPtr))));
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x0017AA28 File Offset: 0x00179C28
		internal unsafe static ushort GetElementSize(this Array array)
		{
			return RuntimeHelpers.GetMethodTable(array)->ComponentSize;
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x0017AA35 File Offset: 0x00179C35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref int GetMultiDimensionalArrayBounds(Array array)
		{
			return Unsafe.As<byte, int>(ref Unsafe.As<RawArrayData>(array).Data);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x0017AA48 File Offset: 0x00179C48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static int GetMultiDimensionalArrayRank(Array array)
		{
			int multiDimensionalArrayRank = RuntimeHelpers.GetMethodTable(array)->MultiDimensionalArrayRank;
			GC.KeepAlive(array);
			return multiDimensionalArrayRank;
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x0017AA68 File Offset: 0x00179C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static bool ObjectHasComponentSize(object obj)
		{
			return RuntimeHelpers.GetMethodTable(obj)->HasComponentSize;
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x0017AA75 File Offset: 0x00179C75
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static MethodTable* GetMethodTable(object obj)
		{
			return (MethodTable*)((void*)(*Unsafe.Add<IntPtr>(Unsafe.As<byte, IntPtr>(obj.GetRawData()), -1)));
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0017AA90 File Offset: 0x00179C90
		public static IntPtr AllocateTypeAssociatedMemory(Type type, int size)
		{
			RuntimeType left = type as RuntimeType;
			if (left == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "type");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return RuntimeHelpers.AllocateTypeAssociatedMemoryInternal(new QCallTypeHandle(ref left), (uint)size);
		}

		// Token: 0x0600462A RID: 17962
		[DllImport("QCall")]
		private static extern IntPtr AllocateTypeAssociatedMemoryInternal(QCallTypeHandle type, uint size);

		// Token: 0x0600462B RID: 17963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AllocTailCallArgBuffer(int size, IntPtr gcDesc);

		// Token: 0x0600462C RID: 17964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern TailCallTls* GetTailCallInfo(IntPtr retAddrSlot, IntPtr* retAddr);

		// Token: 0x0600462D RID: 17965 RVA: 0x0017AADC File Offset: 0x00179CDC
		private unsafe static void DispatchTailCalls(IntPtr callersRetAddrSlot, method callTarget, IntPtr retVal)
		{
			IntPtr value;
			TailCallTls* tailCallInfo = RuntimeHelpers.GetTailCallInfo(callersRetAddrSlot, &value);
			PortableTailCallFrame* frame = tailCallInfo->Frame;
			if (value == frame->TailCallAwareReturnAddress)
			{
				frame->NextCall = callTarget;
				return;
			}
			PortableTailCallFrame portableTailCallFrame;
			portableTailCallFrame.Prev = frame;
			try
			{
				tailCallInfo->Frame = &portableTailCallFrame;
				do
				{
					portableTailCallFrame.NextCall = (UIntPtr)0;
					method system.Void_u0020(System.IntPtr,System.IntPtr,System.IntPtr*) = callTarget;
					calli(System.Void(System.IntPtr,System.IntPtr,System.IntPtr*), tailCallInfo->ArgBuffer, retVal, &portableTailCallFrame.TailCallAwareReturnAddress, system.Void_u0020(System.IntPtr,System.IntPtr,System.IntPtr*));
					callTarget = portableTailCallFrame.NextCall;
				}
				while (callTarget != (UIntPtr)0);
			}
			finally
			{
				tailCallInfo->Frame = frame;
				if (tailCallInfo->ArgBuffer != IntPtr.Zero && *(int*)((void*)tailCallInfo->ArgBuffer) == 1)
				{
					*(int*)((void*)tailCallInfo->ArgBuffer) = 2;
				}
			}
		}

		// Token: 0x0600462E RID: 17966
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetILBytesJitted();

		// Token: 0x0600462F RID: 17967
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMethodsJittedCount();

		// Token: 0x06004630 RID: 17968 RVA: 0x0017AB98 File Offset: 0x00179D98
		public static T[] GetSubArray<[Nullable(2)] T>(T[] array, Range range)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			T[] array2;
			if (typeof(T).IsValueType || typeof(T[]) == array.GetType())
			{
				if (item2 == 0)
				{
					return Array.Empty<T>();
				}
				array2 = new T[item2];
			}
			else
			{
				array2 = Unsafe.As<T[]>(Array.CreateInstance(array.GetType().GetElementType(), item2));
			}
			Buffer.Memmove<T>(MemoryMarshal.GetArrayDataReference<T>(array2), Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), item), (UIntPtr)item2);
			return array2;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x0017AC30 File Offset: 0x00179E30
		public static object GetUninitializedObject([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", SR.ArgumentNull_Type);
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new SerializationException(SR.Format(SR.Serialization_InvalidType, type.ToString()));
			}
			return RuntimeHelpers.GetUninitializedObjectInternal(type);
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x0017AC6C File Offset: 0x00179E6C
		[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public static void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, [Nullable(2)] object userData)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			if (backoutCode == null)
			{
				throw new ArgumentNullException("backoutCode");
			}
			bool exceptionThrown = true;
			try
			{
				code(userData);
				exceptionThrown = false;
			}
			finally
			{
				backoutCode(userData, exceptionThrown);
			}
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public static void PrepareContractedDelegate(Delegate d)
		{
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public static void ProbeForSufficientStack()
		{
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public static void PrepareConstrainedRegions()
		{
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x0017ACBC File Offset: 0x00179EBC
		internal static bool IsPrimitiveType(this CorElementType et)
		{
			return (1 << (int)et & 50348028) != 0;
		}

		// Token: 0x020004F4 RID: 1268
		// (Invoke) Token: 0x06004639 RID: 17977
		[NullableContext(0)]
		public delegate void TryCode(object userData);

		// Token: 0x020004F5 RID: 1269
		// (Invoke) Token: 0x0600463D RID: 17981
		[NullableContext(0)]
		public delegate void CleanupCode(object userData, bool exceptionThrown);
	}
}
