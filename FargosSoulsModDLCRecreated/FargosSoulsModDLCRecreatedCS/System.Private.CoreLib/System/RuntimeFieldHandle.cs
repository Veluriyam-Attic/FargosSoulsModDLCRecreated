using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000083 RID: 131
	public struct RuntimeFieldHandle : ISerializable
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x000B9211 File Offset: 0x000B8411
		internal RuntimeFieldHandle(IRuntimeFieldInfo fieldInfo)
		{
			this.m_ptr = fieldInfo;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000B921A File Offset: 0x000B841A
		internal IRuntimeFieldInfo GetRuntimeFieldInfo()
		{
			return this.m_ptr;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000B9224 File Offset: 0x000B8424
		public IntPtr Value
		{
			get
			{
				if (this.m_ptr == null)
				{
					return IntPtr.Zero;
				}
				return this.m_ptr.Value.Value;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000B9252 File Offset: 0x000B8452
		internal bool IsNullHandle()
		{
			return this.m_ptr == null;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000B925D File Offset: 0x000B845D
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.Value);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000B926C File Offset: 0x000B846C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is RuntimeFieldHandle && ((RuntimeFieldHandle)obj).Value == this.Value;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000B929C File Offset: 0x000B849C
		public bool Equals(RuntimeFieldHandle handle)
		{
			return handle.Value == this.Value;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000B92B0 File Offset: 0x000B84B0
		public static bool operator ==(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000B92BA File Offset: 0x000B84BA
		public static bool operator !=(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000550 RID: 1360
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetName(RtFieldInfo field);

		// Token: 0x06000551 RID: 1361
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeFieldHandleInternal field);

		// Token: 0x06000552 RID: 1362 RVA: 0x000B92C7 File Offset: 0x000B84C7
		internal static MdUtf8String GetUtf8Name(RuntimeFieldHandleInternal field)
		{
			return new MdUtf8String(RuntimeFieldHandle._GetUtf8Name(field));
		}

		// Token: 0x06000553 RID: 1363
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool MatchesNameHash(RuntimeFieldHandleInternal handle, uint hash);

		// Token: 0x06000554 RID: 1364
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FieldAttributes GetAttributes(RuntimeFieldHandleInternal field);

		// Token: 0x06000555 RID: 1365
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetApproxDeclaringType(RuntimeFieldHandleInternal field);

		// Token: 0x06000556 RID: 1366 RVA: 0x000B92D4 File Offset: 0x000B84D4
		internal static RuntimeType GetApproxDeclaringType(IRuntimeFieldInfo field)
		{
			RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(field.Value);
			GC.KeepAlive(field);
			return approxDeclaringType;
		}

		// Token: 0x06000557 RID: 1367
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RtFieldInfo field);

		// Token: 0x06000558 RID: 1368
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetValue(RtFieldInfo field, object instance, RuntimeType fieldType, RuntimeType declaringType, ref bool domainInitialized);

		// Token: 0x06000559 RID: 1369
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object GetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, RuntimeType contextType);

		// Token: 0x0600055A RID: 1370
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetValue(RtFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized);

		// Token: 0x0600055B RID: 1371
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

		// Token: 0x0600055C RID: 1372
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeFieldHandleInternal GetStaticFieldForGenericType(RuntimeFieldHandleInternal field, RuntimeType declaringType);

		// Token: 0x0600055D RID: 1373
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool AcquiresContextFromThis(RuntimeFieldHandleInternal field);

		// Token: 0x0600055E RID: 1374 RVA: 0x000B3617 File Offset: 0x000B2817
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x040001A4 RID: 420
		private readonly IRuntimeFieldInfo m_ptr;
	}
}
