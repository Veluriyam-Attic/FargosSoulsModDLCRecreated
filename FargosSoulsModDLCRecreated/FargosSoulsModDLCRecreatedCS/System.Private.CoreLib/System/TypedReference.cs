using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200009B RID: 155
	[Nullable(0)]
	[NullableContext(1)]
	[CLSCompliant(false)]
	[NonVersionable]
	public ref struct TypedReference
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x000C3BA8 File Offset: 0x000C2DA8
		public unsafe static TypedReference MakeTypedReference(object target, FieldInfo[] flds)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (flds == null)
			{
				throw new ArgumentNullException("flds");
			}
			if (flds.Length == 0)
			{
				throw new ArgumentException(SR.Arg_ArrayZeroError, "flds");
			}
			IntPtr[] array = new IntPtr[flds.Length];
			RuntimeType runtimeType = (RuntimeType)target.GetType();
			for (int i = 0; i < flds.Length; i++)
			{
				RuntimeFieldInfo runtimeFieldInfo = flds[i] as RuntimeFieldInfo;
				if (runtimeFieldInfo == null)
				{
					throw new ArgumentException(SR.Argument_MustBeRuntimeFieldInfo);
				}
				if (runtimeFieldInfo.IsStatic)
				{
					throw new ArgumentException(SR.Format(SR.Argument_TypedReferenceInvalidField, runtimeFieldInfo.Name));
				}
				if (runtimeType != runtimeFieldInfo.GetDeclaringTypeInternal() && !runtimeType.IsSubclassOf(runtimeFieldInfo.GetDeclaringTypeInternal()))
				{
					throw new MissingMemberException(SR.MissingMemberTypeRef);
				}
				RuntimeType runtimeType2 = (RuntimeType)runtimeFieldInfo.FieldType;
				if (runtimeType2.IsPrimitive)
				{
					throw new ArgumentException(SR.Format(SR.Arg_TypeRefPrimitve, runtimeFieldInfo.Name));
				}
				if (i < flds.Length - 1 && !runtimeType2.IsValueType)
				{
					throw new MissingMemberException(SR.MissingMemberNestErr);
				}
				array[i] = runtimeFieldInfo.FieldHandle.Value;
				runtimeType = runtimeType2;
			}
			TypedReference result = default(TypedReference);
			TypedReference.InternalMakeTypedReference((void*)(&result), target, array, runtimeType);
			return result;
		}

		// Token: 0x06000843 RID: 2115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalMakeTypedReference(void* result, object target, IntPtr[] flds, RuntimeType lastFieldType);

		// Token: 0x06000844 RID: 2116 RVA: 0x000C3CEA File Offset: 0x000C2EEA
		public override int GetHashCode()
		{
			if (this._type == IntPtr.Zero)
			{
				return 0;
			}
			return __reftype(this).GetHashCode();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000AB331 File Offset: 0x000AA531
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			throw new NotSupportedException(SR.NotSupported_NYI);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000C3D12 File Offset: 0x000C2F12
		public unsafe static object ToObject(TypedReference value)
		{
			return TypedReference.InternalToObject((void*)(&value));
		}

		// Token: 0x06000847 RID: 2119
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object InternalToObject(void* value);

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000C3D1C File Offset: 0x000C2F1C
		internal bool IsNull
		{
			get
			{
				return Unsafe.IsNullRef<byte>(this._value.Value) && this._type == IntPtr.Zero;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000C3D42 File Offset: 0x000C2F42
		public static Type GetTargetType(TypedReference value)
		{
			return __reftype(value);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000C3D4C File Offset: 0x000C2F4C
		public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
		{
			return __reftype(value).TypeHandle;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000C279F File Offset: 0x000C199F
		[NullableContext(2)]
		public static void SetTypedReference(TypedReference target, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400021C RID: 540
		private readonly ByReference<byte> _value;

		// Token: 0x0400021D RID: 541
		private readonly IntPtr _type;
	}
}
