using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005A7 RID: 1447
	internal sealed class RtFieldInfo : RuntimeFieldInfo, IRuntimeFieldInfo
	{
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x00186D24 File Offset: 0x00185F24
		internal INVOCATION_FLAGS InvocationFlags
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					return this.InitializeInvocationFlags();
				}
				return this.m_invocationFlags;
			}
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x00186D40 File Offset: 0x00185F40
		[MethodImpl(MethodImplOptions.NoInlining)]
		private INVOCATION_FLAGS InitializeInvocationFlags()
		{
			Type declaringType = this.DeclaringType;
			INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
			if (declaringType != null && declaringType.ContainsGenericParameters)
			{
				invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
			}
			if (invocation_FLAGS == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if ((this.m_fieldAttributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope)
				{
					invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
				}
				if ((this.m_fieldAttributes & FieldAttributes.HasFieldRVA) != FieldAttributes.PrivateScope)
				{
					invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
				}
				Type fieldType = this.FieldType;
				if (fieldType.IsPointer || fieldType.IsEnum || fieldType.IsPrimitive)
				{
					invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_FIELD_SPECIAL_CAST;
				}
			}
			return this.m_invocationFlags = (invocation_FLAGS | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED);
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x00186DC1 File Offset: 0x00185FC1
		internal RtFieldInfo(RuntimeFieldHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags) : base(reflectedTypeCache, declaringType, bindingFlags)
		{
			this.m_fieldHandle = handle.Value;
			this.m_fieldAttributes = RuntimeFieldHandle.GetAttributes(handle);
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x00186DE6 File Offset: 0x00185FE6
		RuntimeFieldHandleInternal IRuntimeFieldInfo.Value
		{
			get
			{
				return new RuntimeFieldHandleInternal(this.m_fieldHandle);
			}
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00186DF4 File Offset: 0x00185FF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void CheckConsistency(object target)
		{
			if ((this.m_fieldAttributes & FieldAttributes.Static) == FieldAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(SR.RFLCT_Targ_StatFldReqTarg);
			}
			throw new ArgumentException(SR.Format(SR.Arg_FieldDeclTarget, this.Name, this.m_declaringType, target.GetType()));
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00186E4C File Offset: 0x0018604C
		internal override bool CacheEquals(object o)
		{
			RtFieldInfo rtFieldInfo = o as RtFieldInfo;
			return rtFieldInfo != null && rtFieldInfo.m_fieldHandle == this.m_fieldHandle;
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x00186E78 File Offset: 0x00186078
		public override string Name
		{
			get
			{
				string result;
				if ((result = this.m_name) == null)
				{
					result = (this.m_name = RuntimeFieldHandle.GetName(this));
				}
				return result;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x00186E9E File Offset: 0x0018609E
		public override int MetadataToken
		{
			get
			{
				return RuntimeFieldHandle.GetToken(this);
			}
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00186EA6 File Offset: 0x001860A6
		internal override RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(RuntimeFieldHandle.GetApproxDeclaringType(this));
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00186EB4 File Offset: 0x001860B4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object GetValue(object obj)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if (runtimeType != null && this.DeclaringType.ContainsGenericParameters)
				{
					throw new InvalidOperationException(SR.Arg_UnboundGenField);
				}
				throw new FieldAccessException();
			}
			else
			{
				this.CheckConsistency(obj);
				RuntimeType fieldType = (RuntimeType)this.FieldType;
				bool domainInitialized = false;
				if (runtimeType == null)
				{
					return RuntimeFieldHandle.GetValue(this, obj, fieldType, null, ref domainInitialized);
				}
				domainInitialized = runtimeType.DomainInitialized;
				object value = RuntimeFieldHandle.GetValue(this, obj, fieldType, runtimeType, ref domainInitialized);
				runtimeType.DomainInitialized = domainInitialized;
				return value;
			}
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override object GetRawConstantValue()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00186F47 File Offset: 0x00186147
		[DebuggerHidden]
		[DebuggerStepThrough]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(SR.Arg_TypedReference_Null);
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x00186F7C File Offset: 0x0018617C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if (runtimeType != null && runtimeType.ContainsGenericParameters)
				{
					throw new InvalidOperationException(SR.Arg_UnboundGenField);
				}
				throw new FieldAccessException();
			}
			else
			{
				this.CheckConsistency(obj);
				RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
				value = runtimeType2.CheckValue(value, binder, culture, invokeAttr);
				bool domainInitialized = false;
				if (runtimeType == null)
				{
					RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, null, ref domainInitialized);
					return;
				}
				domainInitialized = runtimeType.DomainInitialized;
				RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, runtimeType, ref domainInitialized);
				runtimeType.DomainInitialized = domainInitialized;
				return;
			}
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x00187022 File Offset: 0x00186222
		[DebuggerHidden]
		[DebuggerStepThrough]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(SR.Arg_TypedReference_Null);
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x00187058 File Offset: 0x00186258
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return new RuntimeFieldHandle(this);
			}
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00187060 File Offset: 0x00186260
		internal IntPtr GetFieldHandle()
		{
			return this.m_fieldHandle;
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x00187068 File Offset: 0x00186268
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x00187070 File Offset: 0x00186270
		public override Type FieldType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_fieldType ?? this.InitializeFieldType();
			}
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00187084 File Offset: 0x00186284
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RuntimeType InitializeFieldType()
		{
			return this.m_fieldType = new Signature(this, this.m_declaringType).FieldType;
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x001870AB File Offset: 0x001862AB
		public override Type[] GetRequiredCustomModifiers()
		{
			return new Signature(this, this.m_declaringType).GetCustomModifiers(1, true);
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x001870C0 File Offset: 0x001862C0
		public override Type[] GetOptionalCustomModifiers()
		{
			return new Signature(this, this.m_declaringType).GetCustomModifiers(1, false);
		}

		// Token: 0x04001270 RID: 4720
		private IntPtr m_fieldHandle;

		// Token: 0x04001271 RID: 4721
		private FieldAttributes m_fieldAttributes;

		// Token: 0x04001272 RID: 4722
		private string m_name;

		// Token: 0x04001273 RID: 4723
		private RuntimeType m_fieldType;

		// Token: 0x04001274 RID: 4724
		private INVOCATION_FLAGS m_invocationFlags;
	}
}
