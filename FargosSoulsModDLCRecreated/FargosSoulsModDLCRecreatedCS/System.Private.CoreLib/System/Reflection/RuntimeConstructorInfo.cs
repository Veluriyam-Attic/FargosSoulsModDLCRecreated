using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020005AA RID: 1450
	internal sealed class RuntimeConstructorInfo : ConstructorInfo, IRuntimeMethodInfo
	{
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x00187A8C File Offset: 0x00186C8C
		internal INVOCATION_FLAGS InvocationFlags
		{
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
					Type declaringType = this.DeclaringType;
					if (declaringType == typeof(void) || (declaringType != null && declaringType.ContainsGenericParameters) || (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					else if (base.IsStatic)
					{
						invocation_FLAGS |= (INVOCATION_FLAGS.INVOCATION_FLAGS_RUN_CLASS_CONSTRUCTOR | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE);
					}
					else if (declaringType != null && declaringType.IsAbstract)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE;
					}
					else
					{
						if (declaringType != null && declaringType.IsByRefLike)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS;
						}
						if (typeof(Delegate).IsAssignableFrom(this.DeclaringType))
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR;
						}
					}
					this.m_invocationFlags = (invocation_FLAGS | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED);
				}
				return this.m_invocationFlags;
			}
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x00187B51 File Offset: 0x00186D51
		internal RuntimeConstructorInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaringType;
			this.m_handle = handle.Value;
			this.m_methodAttributes = methodAttributes;
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x00187B86 File Offset: 0x00186D86
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			get
			{
				return new RuntimeMethodHandleInternal(this.m_handle);
			}
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x00187B94 File Offset: 0x00186D94
		internal override bool CacheEquals(object o)
		{
			RuntimeConstructorInfo runtimeConstructorInfo = o as RuntimeConstructorInfo;
			return runtimeConstructorInfo != null && runtimeConstructorInfo.m_handle == this.m_handle;
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x00187BC0 File Offset: 0x00186DC0
		private Signature Signature
		{
			get
			{
				Signature result;
				if ((result = this.m_signature) == null)
				{
					result = (this.m_signature = new Signature(this, this.m_declaringType));
				}
				return result;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x00187BF2 File Offset: 0x00186DF2
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00187BFF File Offset: 0x00186DFF
		private void CheckConsistency(object target)
		{
			if (target == null && base.IsStatic)
			{
				return;
			}
			if (this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(SR.RFLCT_Targ_StatMethReqTarg);
			}
			throw new TargetException(SR.RFLCT_Targ_ITargMismatch);
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x00187C36 File Offset: 0x00186E36
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00187C40 File Offset: 0x00186E40
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(100);
				valueStringBuilder.Append("Void ");
				valueStringBuilder.Append(this.Name);
				valueStringBuilder.Append('(');
				MethodBase.AppendParameters(ref valueStringBuilder, this.GetParameterTypes(), this.CallingConvention);
				valueStringBuilder.Append(')');
				this.m_toString = valueStringBuilder.ToString();
			}
			return this.m_toString;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00187CB5 File Offset: 0x00186EB5
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00187CCC File Offset: 0x00186ECC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00187D1C File Offset: 0x00186F1C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00187D69 File Offset: 0x00186F69
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06004AC1 RID: 19137 RVA: 0x00187D71 File Offset: 0x00186F71
		public override string Name
		{
			get
			{
				return RuntimeMethodHandle.GetName(this);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x00187D79 File Offset: 0x00186F79
		public override Type DeclaringType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.m_declaringType;
				}
				return null;
			}
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x00187D92 File Offset: 0x00186F92
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeConstructorInfo>(other);
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x00187D9B File Offset: 0x00186F9B
		public override Type ReflectedType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.ReflectedTypeInternal;
				}
				return null;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x00187DB2 File Offset: 0x00186FB2
		public override int MetadataToken
		{
			get
			{
				return RuntimeMethodHandle.GetMethodDef(this);
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x00187DBA File Offset: 0x00186FBA
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00187DC2 File Offset: 0x00186FC2
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00187DCC File Offset: 0x00186FCC
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this.m_declaringType);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00187DDB File Offset: 0x00186FDB
		internal override Type GetReturnType()
		{
			return this.Signature.ReturnType;
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00187DE8 File Offset: 0x00186FE8
		internal override ParameterInfo[] GetParametersNoCopy()
		{
			ParameterInfo[] result;
			if ((result = this.m_parameters) == null)
			{
				result = (this.m_parameters = RuntimeParameterInfo.GetParameters(this, this, this.Signature));
			}
			return result;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00187E18 File Offset: 0x00187018
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			if (parametersNoCopy.Length == 0)
			{
				return parametersNoCopy;
			}
			ParameterInfo[] array = new ParameterInfo[parametersNoCopy.Length];
			Array.Copy(parametersNoCopy, array, parametersNoCopy.Length);
			return array;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00187E46 File Offset: 0x00187046
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return RuntimeMethodHandle.GetImplAttributes(this);
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x00187E4E File Offset: 0x0018704E
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this);
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x00187E56 File Offset: 0x00187056
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodAttributes;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x00187E5E File Offset: 0x0018705E
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.Signature.CallingConvention;
			}
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00187E6C File Offset: 0x0018706C
		internal static void CheckCanCreateInstance(Type declaringType, bool isVarArg)
		{
			if (declaringType == null)
			{
				throw new ArgumentNullException("declaringType");
			}
			if (declaringType.IsInterface)
			{
				throw new MemberAccessException(SR.Format(SR.Acc_CreateInterfaceEx, declaringType));
			}
			if (declaringType.IsAbstract)
			{
				throw new MemberAccessException(SR.Format(SR.Acc_CreateAbstEx, declaringType));
			}
			if (declaringType.GetRootElementType() == typeof(ArgIterator))
			{
				throw new NotSupportedException();
			}
			if (isVarArg)
			{
				throw new NotSupportedException();
			}
			if (declaringType.ContainsGenericParameters)
			{
				throw new MemberAccessException(SR.Format(SR.Acc_CreateGenericEx, declaringType));
			}
			if (declaringType == typeof(void))
			{
				throw new MemberAccessException(SR.Access_Void);
			}
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00187F1B File Offset: 0x0018711B
		[DoesNotReturn]
		internal void ThrowNoInvokeException()
		{
			RuntimeConstructorInfo.CheckCanCreateInstance(this.DeclaringType, (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs);
			if ((this.Attributes & MethodAttributes.Static) == MethodAttributes.Static)
			{
				throw new MemberAccessException(SR.Acc_NotClassInit);
			}
			throw new TargetException();
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00187F50 File Offset: 0x00187150
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			this.CheckConsistency(obj);
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RUN_CLASS_CONSTRUCTOR) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				Type declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					RuntimeHelpers.RunClassConstructor(declaringType.TypeHandle);
				}
				else
				{
					RuntimeHelpers.RunModuleConstructor(this.Module.ModuleHandle);
				}
				return null;
			}
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			if (num != num2)
			{
				throw new TargetParameterCountException(SR.Arg_ParmCnt);
			}
			bool wrapExceptions = (invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				object result = RuntimeMethodHandle.InvokeMethod(obj, array, signature, false, wrapExceptions);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
				return result;
			}
			return RuntimeMethodHandle.InvokeMethod(obj, null, signature, false, wrapExceptions);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00188030 File Offset: 0x00187230
		public override MethodBody GetMethodBody()
		{
			RuntimeMethodBody methodBody = RuntimeMethodHandle.GetMethodBody(this, this.ReflectedTypeInternal);
			if (methodBody != null)
			{
				methodBody._methodBase = this;
			}
			return methodBody;
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06004AD6 RID: 19158 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06004AD8 RID: 19160 RVA: 0x00188055 File Offset: 0x00187255
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00188074 File Offset: 0x00187274
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			if (num != num2)
			{
				throw new TargetParameterCountException(SR.Arg_ParmCnt);
			}
			bool wrapExceptions = (invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				object result = RuntimeMethodHandle.InvokeMethod(null, array, signature, true, wrapExceptions);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
				return result;
			}
			return RuntimeMethodHandle.InvokeMethod(null, null, signature, true, wrapExceptions);
		}

		// Token: 0x0400127A RID: 4730
		private volatile RuntimeType m_declaringType;

		// Token: 0x0400127B RID: 4731
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x0400127C RID: 4732
		private string m_toString;

		// Token: 0x0400127D RID: 4733
		private ParameterInfo[] m_parameters;

		// Token: 0x0400127E RID: 4734
		private object _empty1;

		// Token: 0x0400127F RID: 4735
		private object _empty2;

		// Token: 0x04001280 RID: 4736
		private object _empty3;

		// Token: 0x04001281 RID: 4737
		private IntPtr m_handle;

		// Token: 0x04001282 RID: 4738
		private MethodAttributes m_methodAttributes;

		// Token: 0x04001283 RID: 4739
		private BindingFlags m_bindingFlags;

		// Token: 0x04001284 RID: 4740
		private volatile Signature m_signature;

		// Token: 0x04001285 RID: 4741
		private INVOCATION_FLAGS m_invocationFlags;
	}
}
