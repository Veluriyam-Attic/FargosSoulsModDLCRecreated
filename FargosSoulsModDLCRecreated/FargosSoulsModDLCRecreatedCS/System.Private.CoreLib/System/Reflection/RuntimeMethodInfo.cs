using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005B0 RID: 1456
	internal sealed class RuntimeMethodInfo : MethodInfo, IRuntimeMethodInfo
	{
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x0018881C File Offset: 0x00187A1C
		internal INVOCATION_FLAGS InvocationFlags
		{
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
					Type declaringType = this.DeclaringType;
					if (this.ContainsGenericParameters || RuntimeMethodInfo.IsDisallowedByRefType(this.ReturnType) || (declaringType != null && declaringType.ContainsGenericParameters) || (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
					{
						invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					else if ((declaringType != null && declaringType.IsByRefLike) || this.ReturnType.IsByRefLike)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS;
					}
					this.m_invocationFlags = (invocation_FLAGS | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED);
				}
				return this.m_invocationFlags;
			}
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x001888A8 File Offset: 0x00187AA8
		private static bool IsDisallowedByRefType(Type type)
		{
			if (!type.IsByRef)
			{
				return false;
			}
			Type elementType = type.GetElementType();
			return elementType.IsByRefLike || elementType == typeof(void);
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x001888E0 File Offset: 0x00187AE0
		internal RuntimeMethodInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags, object keepalive)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_keepalive = keepalive;
			this.m_handle = handle.Value;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_methodAttributes = methodAttributes;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0018891B File Offset: 0x00187B1B
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			get
			{
				return new RuntimeMethodHandleInternal(this.m_handle);
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x00188928 File Offset: 0x00187B28
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x00188938 File Offset: 0x00187B38
		private ParameterInfo[] FetchNonReturnParameters()
		{
			ParameterInfo[] result;
			if ((result = this.m_parameters) == null)
			{
				result = (this.m_parameters = RuntimeParameterInfo.GetParameters(this, this, this.Signature));
			}
			return result;
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x00188968 File Offset: 0x00187B68
		private ParameterInfo FetchReturnParameter()
		{
			ParameterInfo result;
			if ((result = this.m_returnParameter) == null)
			{
				result = (this.m_returnParameter = RuntimeParameterInfo.GetReturnParameter(this, this, this.Signature));
			}
			return result;
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x00188998 File Offset: 0x00187B98
		internal override bool CacheEquals(object o)
		{
			RuntimeMethodInfo runtimeMethodInfo = o as RuntimeMethodInfo;
			return runtimeMethodInfo != null && runtimeMethodInfo.m_handle == this.m_handle;
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004B1D RID: 19229 RVA: 0x001889C4 File Offset: 0x00187BC4
		internal Signature Signature
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

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x001889F0 File Offset: 0x00187BF0
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x001889F8 File Offset: 0x00187BF8
		internal RuntimeMethodInfo GetParentDefinition()
		{
			if (!base.IsVirtual || this.m_declaringType.IsInterface)
			{
				return null;
			}
			RuntimeType runtimeType = (RuntimeType)this.m_declaringType.BaseType;
			if (runtimeType == null)
			{
				return null;
			}
			int slot = RuntimeMethodHandle.GetSlot(this);
			if (RuntimeTypeHandle.GetNumVirtuals(runtimeType) <= slot)
			{
				return null;
			}
			return (RuntimeMethodInfo)RuntimeType.GetMethodBase(runtimeType, RuntimeTypeHandle.GetMethodAt(runtimeType, slot));
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x00188A5C File Offset: 0x00187C5C
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x00188A64 File Offset: 0x00187C64
		internal sealed override int GenericParameterCount
		{
			get
			{
				return RuntimeMethodHandle.GetGenericParameterCount(this);
			}
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x00188A6C File Offset: 0x00187C6C
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(100);
				valueStringBuilder.Append(this.ReturnType.FormatTypeName());
				valueStringBuilder.Append(' ');
				valueStringBuilder.Append(this.Name);
				if (this.IsGenericMethod)
				{
					valueStringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, TypeNameFormatFlags.FormatBasic));
				}
				valueStringBuilder.Append('(');
				MethodBase.AppendParameters(ref valueStringBuilder, this.GetParameterTypes(), this.CallingConvention);
				valueStringBuilder.Append(')');
				this.m_toString = valueStringBuilder.ToString();
			}
			return this.m_toString;
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x00188B06 File Offset: 0x00187D06
		public override int GetHashCode()
		{
			if (this.IsGenericMethod)
			{
				return ValueType.GetHashCodeOfPtr(this.m_handle);
			}
			return base.GetHashCode();
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x00188B24 File Offset: 0x00187D24
		public override bool Equals(object obj)
		{
			if (!this.IsGenericMethod)
			{
				return obj == this;
			}
			RuntimeMethodInfo runtimeMethodInfo = obj as RuntimeMethodInfo;
			if (runtimeMethodInfo == null || !runtimeMethodInfo.IsGenericMethod)
			{
				return false;
			}
			IRuntimeMethodInfo runtimeMethodInfo2 = RuntimeMethodHandle.StripMethodInstantiation(this);
			IRuntimeMethodInfo runtimeMethodInfo3 = RuntimeMethodHandle.StripMethodInstantiation(runtimeMethodInfo);
			if (runtimeMethodInfo2.Value.Value != runtimeMethodInfo3.Value.Value)
			{
				return false;
			}
			Type[] genericArguments = this.GetGenericArguments();
			Type[] genericArguments2 = runtimeMethodInfo.GetGenericArguments();
			if (genericArguments.Length != genericArguments2.Length)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] != genericArguments2[i])
				{
					return false;
				}
			}
			return !(this.DeclaringType != runtimeMethodInfo.DeclaringType) && !(this.ReflectedType != runtimeMethodInfo.ReflectedType);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x00188BF6 File Offset: 0x00187DF6
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType, inherit);
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00188C10 File Offset: 0x00187E10
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
			return CustomAttribute.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x00188C60 File Offset: 0x00187E60
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
			return CustomAttribute.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x00188CAE File Offset: 0x00187EAE
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x00188CB8 File Offset: 0x00187EB8
		public override string Name
		{
			get
			{
				string result;
				if ((result = this.m_name) == null)
				{
					result = (this.m_name = RuntimeMethodHandle.GetName(this));
				}
				return result;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x00188CDE File Offset: 0x00187EDE
		public override Type DeclaringType
		{
			get
			{
				if (this.m_reflectedTypeCache.IsGlobal)
				{
					return null;
				}
				return this.m_declaringType;
			}
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00188CF5 File Offset: 0x00187EF5
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeMethodInfo>(other);
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004B2C RID: 19244 RVA: 0x00188CFE File Offset: 0x00187EFE
		public override Type ReflectedType
		{
			get
			{
				if (this.m_reflectedTypeCache.IsGlobal)
				{
					return null;
				}
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x000DAEBB File Offset: 0x000DA0BB
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004B2E RID: 19246 RVA: 0x00187DB2 File Offset: 0x00186FB2
		public override int MetadataToken
		{
			get
			{
				return RuntimeMethodHandle.GetMethodDef(this);
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x00188D1A File Offset: 0x00187F1A
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x00188A5C File Offset: 0x00187C5C
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00188D22 File Offset: 0x00187F22
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004B32 RID: 19250 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004B34 RID: 19252 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00188D2F File Offset: 0x00187F2F
		internal override ParameterInfo[] GetParametersNoCopy()
		{
			return this.FetchNonReturnParameters();
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x00188D38 File Offset: 0x00187F38
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] array = this.FetchNonReturnParameters();
			if (array.Length == 0)
			{
				return array;
			}
			ParameterInfo[] array2 = new ParameterInfo[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x00187E46 File Offset: 0x00187046
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return RuntimeMethodHandle.GetImplAttributes(this);
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x00187E4E File Offset: 0x0018704E
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this);
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x00188D66 File Offset: 0x00187F66
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodAttributes;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x00188D6E File Offset: 0x00187F6E
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.Signature.CallingConvention;
			}
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00188D7C File Offset: 0x00187F7C
		public override MethodBody GetMethodBody()
		{
			RuntimeMethodBody methodBody = RuntimeMethodHandle.GetMethodBody(this, this.ReflectedTypeInternal);
			if (methodBody != null)
			{
				methodBody._methodBase = this;
			}
			return methodBody;
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x00188DA1 File Offset: 0x00187FA1
		private void CheckConsistency(object target)
		{
			if ((this.m_methodAttributes & MethodAttributes.Static) == MethodAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(SR.RFLCT_Targ_StatMethReqTarg);
			}
			throw new TargetException(SR.RFLCT_Targ_ITargMismatch);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00188DD8 File Offset: 0x00187FD8
		[DoesNotReturn]
		private void ThrowNoInvokeException()
		{
			if ((this.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new NotSupportedException();
			}
			if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				throw new NotSupportedException();
			}
			if (this.DeclaringType.ContainsGenericParameters || this.ContainsGenericParameters)
			{
				throw new InvalidOperationException(SR.Arg_UnboundGenParam);
			}
			if (base.IsAbstract)
			{
				throw new MemberAccessException();
			}
			if (this.ReturnType.IsByRef)
			{
				Type elementType = this.ReturnType.GetElementType();
				if (elementType.IsByRefLike)
				{
					throw new NotSupportedException(SR.NotSupported_ByRefToByRefLikeReturn);
				}
				if (elementType == typeof(void))
				{
					throw new NotSupportedException(SR.NotSupported_ByRefToVoidReturn);
				}
			}
			throw new TargetException();
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x00188E88 File Offset: 0x00188088
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] array = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
			bool wrapExceptions = (invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
			if (array == null || array.Length == 0)
			{
				return RuntimeMethodHandle.InvokeMethod(obj, null, this.Signature, false, wrapExceptions);
			}
			object result = RuntimeMethodHandle.InvokeMethod(obj, array, this.Signature, false, wrapExceptions);
			for (int i = 0; i < array.Length; i++)
			{
				parameters[i] = array[i];
			}
			return result;
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00188EEC File Offset: 0x001880EC
		[DebuggerStepThrough]
		[DebuggerHidden]
		private object[] InvokeArgumentsCheck(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			this.CheckConsistency(obj);
			if (num != num2)
			{
				throw new TargetParameterCountException(SR.Arg_ParmCnt);
			}
			if (num2 != 0)
			{
				return base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
			}
			return null;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004B40 RID: 19264 RVA: 0x00188F53 File Offset: 0x00188153
		public override Type ReturnType
		{
			get
			{
				return this.Signature.ReturnType;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x00188F60 File Offset: 0x00188160
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this.ReturnParameter;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06004B42 RID: 19266 RVA: 0x00188F68 File Offset: 0x00188168
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return this.FetchReturnParameter();
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x00188F70 File Offset: 0x00188170
		public override bool IsCollectible
		{
			get
			{
				return RuntimeMethodHandle.GetIsCollectible(new RuntimeMethodHandleInternal(this.m_handle)) > Interop.BOOL.FALSE;
			}
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00188F88 File Offset: 0x00188188
		public override MethodInfo GetBaseDefinition()
		{
			if (!base.IsVirtual || base.IsStatic || this.m_declaringType == null || this.m_declaringType.IsInterface)
			{
				return this;
			}
			int slot = RuntimeMethodHandle.GetSlot(this);
			RuntimeType runtimeType = (RuntimeType)this.DeclaringType;
			RuntimeType reflectedType = runtimeType;
			RuntimeMethodHandleInternal methodHandle = default(RuntimeMethodHandleInternal);
			do
			{
				int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
				if (numVirtuals <= slot)
				{
					break;
				}
				methodHandle = RuntimeTypeHandle.GetMethodAt(runtimeType, slot);
				reflectedType = runtimeType;
				runtimeType = (RuntimeType)runtimeType.BaseType;
			}
			while (runtimeType != null);
			return (MethodInfo)RuntimeType.GetMethodBase(reflectedType, methodHandle);
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x00189017 File Offset: 0x00188217
		public override Delegate CreateDelegate(Type delegateType)
		{
			return this.CreateDelegateInternal(delegateType, null, (DelegateBindingFlags)68);
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x00189023 File Offset: 0x00188223
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			return this.CreateDelegateInternal(delegateType, target, DelegateBindingFlags.RelaxedSignature);
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00189030 File Offset: 0x00188230
		private Delegate CreateDelegateInternal(Type delegateType, object firstArgument, DelegateBindingFlags bindingFlags)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			RuntimeType runtimeType = delegateType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "delegateType");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "delegateType");
			}
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, this, firstArgument, bindingFlags);
			if (@delegate == null)
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
			return @delegate;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x001890A4 File Offset: 0x001882A4
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			RuntimeType[] array = new RuntimeType[methodInstantiation.Length];
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(SR.Format(SR.Arg_NotGenericMethodDefinition, this));
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				Type type = methodInstantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					Type[] array2 = new Type[methodInstantiation.Length];
					for (int j = 0; j < methodInstantiation.Length; j++)
					{
						array2[j] = methodInstantiation[j];
					}
					methodInstantiation = array2;
					return MethodBuilderInstantiation.MakeGenericMethod(this, methodInstantiation);
				}
				array[i] = runtimeType;
			}
			RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
			RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
			MethodInfo result = null;
			try
			{
				result = (RuntimeType.GetMethodBase(this.ReflectedTypeInternal, RuntimeMethodHandle.GetStubIfNeeded(new RuntimeMethodHandleInternal(this.m_handle), this.m_declaringType, array)) as MethodInfo);
			}
			catch (VerificationException e)
			{
				RuntimeType.ValidateGenericArguments(this, array, e);
				throw;
			}
			return result;
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x001891A8 File Offset: 0x001883A8
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return RuntimeMethodHandle.GetMethodInstantiationInternal(this);
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x001891B0 File Offset: 0x001883B0
		public override Type[] GetGenericArguments()
		{
			return RuntimeMethodHandle.GetMethodInstantiationPublic(this) ?? Array.Empty<Type>();
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x001891C1 File Offset: 0x001883C1
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return RuntimeType.GetMethodBase(this.m_declaringType, RuntimeMethodHandle.StripMethodInstantiation(this)) as MethodInfo;
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x001891E7 File Offset: 0x001883E7
		public override bool IsGenericMethod
		{
			get
			{
				return RuntimeMethodHandle.HasMethodInstantiation(this);
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06004B4D RID: 19277 RVA: 0x001891EF File Offset: 0x001883EF
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return RuntimeMethodHandle.IsGenericMethodDefinition(this);
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x001891F8 File Offset: 0x001883F8
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters)
				{
					return true;
				}
				if (!this.IsGenericMethod)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x00189250 File Offset: 0x00188450
		internal static MethodBase InternalGetCurrentMethod(ref StackCrawlMark stackMark)
		{
			IRuntimeMethodInfo currentMethod = RuntimeMethodHandle.GetCurrentMethod(ref stackMark);
			if (currentMethod == null)
			{
				return null;
			}
			return RuntimeType.GetMethodBase(currentMethod);
		}

		// Token: 0x040012A6 RID: 4774
		private IntPtr m_handle;

		// Token: 0x040012A7 RID: 4775
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x040012A8 RID: 4776
		private string m_name;

		// Token: 0x040012A9 RID: 4777
		private string m_toString;

		// Token: 0x040012AA RID: 4778
		private ParameterInfo[] m_parameters;

		// Token: 0x040012AB RID: 4779
		private ParameterInfo m_returnParameter;

		// Token: 0x040012AC RID: 4780
		private BindingFlags m_bindingFlags;

		// Token: 0x040012AD RID: 4781
		private MethodAttributes m_methodAttributes;

		// Token: 0x040012AE RID: 4782
		private Signature m_signature;

		// Token: 0x040012AF RID: 4783
		private RuntimeType m_declaringType;

		// Token: 0x040012B0 RID: 4784
		private object m_keepalive;

		// Token: 0x040012B1 RID: 4785
		private INVOCATION_FLAGS m_invocationFlags;
	}
}
