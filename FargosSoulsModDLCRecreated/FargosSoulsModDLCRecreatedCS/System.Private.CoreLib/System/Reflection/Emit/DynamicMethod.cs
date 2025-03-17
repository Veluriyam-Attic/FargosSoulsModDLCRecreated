using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000620 RID: 1568
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DynamicMethod : MethodInfo
	{
		// Token: 0x06004F41 RID: 20289 RVA: 0x0018FE7C File Offset: 0x0018F07C
		public DynamicMethod(string name, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, false, true);
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x0018FEA0 File Offset: 0x0018F0A0
		public DynamicMethod(string name, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, bool restrictedSkipVisibility)
		{
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, restrictedSkipVisibility, true);
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x0018FEC4 File Offset: 0x0018F0C4
		public DynamicMethod(string name, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, m, false, false);
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0018FF00 File Offset: 0x0018F100
		public DynamicMethod(string name, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, Module m, bool skipVisibility)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, m, skipVisibility, false);
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x0018FF3C File Offset: 0x0018F13C
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, Module m, bool skipVisibility)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			this.Init(name, attributes, callingConvention, returnType, parameterTypes, null, m, skipVisibility, false);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x0018FF78 File Offset: 0x0018F178
		public DynamicMethod(string name, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, Type owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, null, false, false);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x0018FFB4 File Offset: 0x0018F1B4
		public DynamicMethod(string name, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, Type owner, bool skipVisibility)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, null, skipVisibility, false);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0018FFF0 File Offset: 0x0018F1F0
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, Type owner, bool skipVisibility)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Init(name, attributes, callingConvention, returnType, parameterTypes, owner, null, skipVisibility, false);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x0019002C File Offset: 0x0018F22C
		private static void CheckConsistency(MethodAttributes attributes, CallingConventions callingConvention)
		{
			if ((attributes & ~MethodAttributes.MemberAccessMask) != MethodAttributes.Static)
			{
				throw new NotSupportedException(SR.NotSupported_DynamicMethodFlags);
			}
			if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
			{
				throw new NotSupportedException(SR.NotSupported_DynamicMethodFlags);
			}
			if (callingConvention != CallingConventions.Standard && callingConvention != CallingConventions.VarArgs)
			{
				throw new NotSupportedException(SR.NotSupported_DynamicMethodFlags);
			}
			if (callingConvention == CallingConventions.VarArgs)
			{
				throw new NotSupportedException(SR.NotSupported_DynamicMethodFlags);
			}
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x00190080 File Offset: 0x0018F280
		private static RuntimeModule GetDynamicMethodsModule()
		{
			if (DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != null)
			{
				return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
			}
			object obj = DynamicMethod.s_anonymouslyHostedDynamicMethodsModuleLock;
			lock (obj)
			{
				if (DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != null)
				{
					return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
				}
				AssemblyName name = new AssemblyName("Anonymously Hosted DynamicMethods Assembly");
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
				AssemblyBuilder assemblyBuilder = AssemblyBuilder.InternalDefineDynamicAssembly(name, AssemblyBuilderAccess.Run, ref stackCrawlMark, null);
				DynamicMethod.s_anonymouslyHostedDynamicMethodsModule = (InternalModuleBuilder)assemblyBuilder.ManifestModule;
			}
			return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x00190120 File Offset: 0x0018F320
		[MemberNotNull("m_dynMethod")]
		[MemberNotNull("m_returnType")]
		[MemberNotNull("m_parameterTypes")]
		private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] signature, Type owner, Module m, bool skipVisibility, bool transparentMethod)
		{
			DynamicMethod.CheckConsistency(attributes, callingConvention);
			if (signature != null)
			{
				this.m_parameterTypes = new RuntimeType[signature.Length];
				for (int i = 0; i < signature.Length; i++)
				{
					if (signature[i] == null)
					{
						throw new ArgumentException(SR.Arg_InvalidTypeInSignature);
					}
					this.m_parameterTypes[i] = (signature[i].UnderlyingSystemType as RuntimeType);
					if (this.m_parameterTypes[i] == null || this.m_parameterTypes[i] == typeof(void))
					{
						throw new ArgumentException(SR.Arg_InvalidTypeInSignature);
					}
				}
			}
			else
			{
				this.m_parameterTypes = Array.Empty<RuntimeType>();
			}
			this.m_returnType = ((returnType == null) ? ((RuntimeType)typeof(void)) : (returnType.UnderlyingSystemType as RuntimeType));
			if (this.m_returnType == null)
			{
				throw new NotSupportedException(SR.Arg_InvalidTypeInRetType);
			}
			if (transparentMethod)
			{
				this.m_module = DynamicMethod.GetDynamicMethodsModule();
				if (skipVisibility)
				{
					this.m_restrictedSkipVisibility = true;
				}
			}
			else
			{
				if (m != null)
				{
					this.m_module = m.ModuleHandle.GetRuntimeModule();
				}
				else
				{
					RuntimeType runtimeType = null;
					if (owner != null)
					{
						runtimeType = (owner.UnderlyingSystemType as RuntimeType);
					}
					if (runtimeType != null)
					{
						if (runtimeType.HasElementType || runtimeType.ContainsGenericParameters || runtimeType.IsGenericParameter || runtimeType.IsInterface)
						{
							throw new ArgumentException(SR.Argument_InvalidTypeForDynamicMethod);
						}
						this.m_typeOwner = runtimeType;
						this.m_module = runtimeType.GetRuntimeModule();
					}
				}
				this.m_skipVisibility = skipVisibility;
			}
			this.m_ilGenerator = null;
			this.m_fInitLocals = true;
			this.m_methodHandle = null;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_dynMethod = new DynamicMethod.RTDynamicMethod(this, name, attributes, callingConvention);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x001902EC File Offset: 0x0018F4EC
		public sealed override Delegate CreateDelegate(Type delegateType)
		{
			if (this.m_restrictedSkipVisibility)
			{
				this.GetMethodDescriptor();
				IRuntimeMethodInfo methodHandle = this.m_methodHandle;
				RuntimeHelpers._CompileMethod((methodHandle != null) ? methodHandle.Value : RuntimeMethodHandleInternal.EmptyHandle);
				GC.KeepAlive(methodHandle);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)Delegate.CreateDelegateNoSecurityCheck(delegateType, null, this.GetMethodDescriptor());
			multicastDelegate.StoreDynamicMethod(this.GetMethodInfo());
			return multicastDelegate;
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0019034C File Offset: 0x0018F54C
		public sealed override Delegate CreateDelegate(Type delegateType, [Nullable(2)] object target)
		{
			if (this.m_restrictedSkipVisibility)
			{
				this.GetMethodDescriptor();
				IRuntimeMethodInfo methodHandle = this.m_methodHandle;
				RuntimeHelpers._CompileMethod((methodHandle != null) ? methodHandle.Value : RuntimeMethodHandleInternal.EmptyHandle);
				GC.KeepAlive(methodHandle);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)Delegate.CreateDelegateNoSecurityCheck(delegateType, target, this.GetMethodDescriptor());
			multicastDelegate.StoreDynamicMethod(this.GetMethodInfo());
			return multicastDelegate;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x001903AC File Offset: 0x0018F5AC
		internal RuntimeMethodHandle GetMethodDescriptor()
		{
			if (this.m_methodHandle == null)
			{
				lock (this)
				{
					if (this.m_methodHandle == null)
					{
						if (this.m_DynamicILInfo != null)
						{
							this.m_DynamicILInfo.GetCallableMethod(this.m_module, this);
						}
						else
						{
							if (this.m_ilGenerator == null || this.m_ilGenerator.ILOffset == 0)
							{
								throw new InvalidOperationException(SR.Format(SR.InvalidOperation_BadEmptyMethodBody, this.Name));
							}
							this.m_ilGenerator.GetCallableMethod(this.m_module, this);
						}
					}
				}
			}
			return new RuntimeMethodHandle(this.m_methodHandle);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00190458 File Offset: 0x0018F658
		public override string ToString()
		{
			return this.m_dynMethod.ToString();
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x00190465 File Offset: 0x0018F665
		public override string Name
		{
			get
			{
				return this.m_dynMethod.Name;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x00190472 File Offset: 0x0018F672
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this.m_dynMethod.DeclaringType;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x0019047F File Offset: 0x0018F67F
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.m_dynMethod.ReflectedType;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x0019048C File Offset: 0x0018F68C
		public override Module Module
		{
			get
			{
				return this.m_dynMethod.Module;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x0019049C File Offset: 0x0018F69C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotAllowedInDynamicMethod);
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x001904B3 File Offset: 0x0018F6B3
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_dynMethod.Attributes;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x001904C0 File Offset: 0x0018F6C0
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_dynMethod.CallingConvention;
			}
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x000AC098 File Offset: 0x000AB298
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x001904CD File Offset: 0x0018F6CD
		public override ParameterInfo[] GetParameters()
		{
			return this.m_dynMethod.GetParameters();
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x001904DA File Offset: 0x0018F6DA
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dynMethod.GetMethodImplementationFlags();
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004F5A RID: 20314 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x001904E8 File Offset: 0x0018F6E8
		[NullableContext(2)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				throw new NotSupportedException(SR.NotSupported_CallToVarArg);
			}
			this.GetMethodDescriptor();
			Signature signature = new Signature(this.m_methodHandle, this.m_parameterTypes, this.m_returnType, this.CallingConvention);
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			if (num != num2)
			{
				throw new TargetParameterCountException(SR.Arg_ParmCnt);
			}
			bool wrapExceptions = (invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
			object result;
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				result = RuntimeMethodHandle.InvokeMethod(null, array, signature, false, wrapExceptions);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
			}
			else
			{
				result = RuntimeMethodHandle.InvokeMethod(null, null, signature, false, wrapExceptions);
			}
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x001905B2 File Offset: 0x0018F7B2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_dynMethod.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x001905C1 File Offset: 0x0018F7C1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_dynMethod.GetCustomAttributes(inherit);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x001905CF File Offset: 0x0018F7CF
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_dynMethod.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x001905DE File Offset: 0x0018F7DE
		public override Type ReturnType
		{
			get
			{
				return this.m_dynMethod.ReturnType;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x001905EB File Offset: 0x0018F7EB
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return this.m_dynMethod.ReturnParameter;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x001905F8 File Offset: 0x0018F7F8
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this.m_dynMethod.ReturnTypeCustomAttributes;
			}
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x00190608 File Offset: 0x0018F808
		[NullableContext(2)]
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
		{
			if (position < 0 || position > this.m_parameterTypes.Length)
			{
				throw new ArgumentOutOfRangeException(SR.ArgumentOutOfRange_ParamSequence);
			}
			position--;
			if (position >= 0)
			{
				RuntimeParameterInfo[] array = this.m_dynMethod.LoadParameters();
				array[position].SetName(parameterName);
				array[position].SetAttributes(attributes);
			}
			return null;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00190658 File Offset: 0x0018F858
		public DynamicILInfo GetDynamicILInfo()
		{
			if (this.m_DynamicILInfo == null)
			{
				Module scope = null;
				CallingConventions callingConvention = this.CallingConvention;
				Type returnType = this.ReturnType;
				Type[] requiredReturnTypeCustomModifiers = null;
				Type[] optionalReturnTypeCustomModifiers = null;
				Type[] parameterTypes = this.m_parameterTypes;
				byte[] signature = SignatureHelper.GetMethodSigHelper(scope, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, null, null).GetSignature(true);
				this.m_DynamicILInfo = new DynamicILInfo(this, signature);
			}
			return this.m_DynamicILInfo;
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x001906A5 File Offset: 0x0018F8A5
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x001906B0 File Offset: 0x0018F8B0
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_ilGenerator == null)
			{
				Module scope = null;
				CallingConventions callingConvention = this.CallingConvention;
				Type returnType = this.ReturnType;
				Type[] requiredReturnTypeCustomModifiers = null;
				Type[] optionalReturnTypeCustomModifiers = null;
				Type[] parameterTypes = this.m_parameterTypes;
				byte[] signature = SignatureHelper.GetMethodSigHelper(scope, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, null, null).GetSignature(true);
				this.m_ilGenerator = new DynamicILGenerator(this, signature, streamSize);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x001906FE File Offset: 0x0018F8FE
		// (set) Token: 0x06004F69 RID: 20329 RVA: 0x00190706 File Offset: 0x0018F906
		public bool InitLocals
		{
			get
			{
				return this.m_fInitLocals;
			}
			set
			{
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0019070F File Offset: 0x0018F90F
		internal MethodInfo GetMethodInfo()
		{
			return this.m_dynMethod;
		}

		// Token: 0x0400143E RID: 5182
		private RuntimeType[] m_parameterTypes;

		// Token: 0x0400143F RID: 5183
		internal IRuntimeMethodInfo m_methodHandle;

		// Token: 0x04001440 RID: 5184
		private RuntimeType m_returnType;

		// Token: 0x04001441 RID: 5185
		private DynamicILGenerator m_ilGenerator;

		// Token: 0x04001442 RID: 5186
		private DynamicILInfo m_DynamicILInfo;

		// Token: 0x04001443 RID: 5187
		private bool m_fInitLocals;

		// Token: 0x04001444 RID: 5188
		private RuntimeModule m_module;

		// Token: 0x04001445 RID: 5189
		internal bool m_skipVisibility;

		// Token: 0x04001446 RID: 5190
		internal RuntimeType m_typeOwner;

		// Token: 0x04001447 RID: 5191
		private DynamicMethod.RTDynamicMethod m_dynMethod;

		// Token: 0x04001448 RID: 5192
		internal DynamicResolver m_resolver;

		// Token: 0x04001449 RID: 5193
		internal bool m_restrictedSkipVisibility;

		// Token: 0x0400144A RID: 5194
		private static volatile InternalModuleBuilder s_anonymouslyHostedDynamicMethodsModule;

		// Token: 0x0400144B RID: 5195
		private static readonly object s_anonymouslyHostedDynamicMethodsModuleLock = new object();

		// Token: 0x02000621 RID: 1569
		internal sealed class RTDynamicMethod : MethodInfo
		{
			// Token: 0x06004F6C RID: 20332 RVA: 0x00190723 File Offset: 0x0018F923
			internal RTDynamicMethod(DynamicMethod owner, string name, MethodAttributes attributes, CallingConventions callingConvention)
			{
				this.m_owner = owner;
				this.m_name = name;
				this.m_attributes = attributes;
				this.m_callingConvention = callingConvention;
			}

			// Token: 0x06004F6D RID: 20333 RVA: 0x00190748 File Offset: 0x0018F948
			public override string ToString()
			{
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(100);
				valueStringBuilder.Append(this.ReturnType.FormatTypeName());
				valueStringBuilder.Append(' ');
				valueStringBuilder.Append(this.Name);
				valueStringBuilder.Append('(');
				MethodBase.AppendParameters(ref valueStringBuilder, this.GetParameterTypes(), this.CallingConvention);
				valueStringBuilder.Append(')');
				return valueStringBuilder.ToString();
			}

			// Token: 0x17000CFA RID: 3322
			// (get) Token: 0x06004F6E RID: 20334 RVA: 0x001907B8 File Offset: 0x0018F9B8
			public override string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17000CFB RID: 3323
			// (get) Token: 0x06004F6F RID: 20335 RVA: 0x000C26FD File Offset: 0x000C18FD
			public override Type DeclaringType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000CFC RID: 3324
			// (get) Token: 0x06004F70 RID: 20336 RVA: 0x000C26FD File Offset: 0x000C18FD
			public override Type ReflectedType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000CFD RID: 3325
			// (get) Token: 0x06004F71 RID: 20337 RVA: 0x001907C0 File Offset: 0x0018F9C0
			public override Module Module
			{
				get
				{
					return this.m_owner.m_module;
				}
			}

			// Token: 0x17000CFE RID: 3326
			// (get) Token: 0x06004F72 RID: 20338 RVA: 0x0019049C File Offset: 0x0018F69C
			public override RuntimeMethodHandle MethodHandle
			{
				get
				{
					throw new InvalidOperationException(SR.InvalidOperation_NotAllowedInDynamicMethod);
				}
			}

			// Token: 0x17000CFF RID: 3327
			// (get) Token: 0x06004F73 RID: 20339 RVA: 0x001907CD File Offset: 0x0018F9CD
			public override MethodAttributes Attributes
			{
				get
				{
					return this.m_attributes;
				}
			}

			// Token: 0x17000D00 RID: 3328
			// (get) Token: 0x06004F74 RID: 20340 RVA: 0x001907D5 File Offset: 0x0018F9D5
			public override CallingConventions CallingConvention
			{
				get
				{
					return this.m_callingConvention;
				}
			}

			// Token: 0x06004F75 RID: 20341 RVA: 0x000AC098 File Offset: 0x000AB298
			public override MethodInfo GetBaseDefinition()
			{
				return this;
			}

			// Token: 0x06004F76 RID: 20342 RVA: 0x001907E0 File Offset: 0x0018F9E0
			public override ParameterInfo[] GetParameters()
			{
				ParameterInfo[] array = this.LoadParameters();
				ParameterInfo[] array2 = array;
				ParameterInfo[] array3 = new ParameterInfo[array2.Length];
				Array.Copy(array2, array3, array2.Length);
				return array3;
			}

			// Token: 0x06004F77 RID: 20343 RVA: 0x000DAEBB File Offset: 0x000DA0BB
			public override MethodImplAttributes GetMethodImplementationFlags()
			{
				return MethodImplAttributes.NoInlining;
			}

			// Token: 0x06004F78 RID: 20344 RVA: 0x0019080A File Offset: 0x0018FA0A
			public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "this");
			}

			// Token: 0x06004F79 RID: 20345 RVA: 0x0019081C File Offset: 0x0018FA1C
			public override object[] GetCustomAttributes(Type attributeType, bool inherit)
			{
				if (attributeType == null)
				{
					throw new ArgumentNullException("attributeType");
				}
				if (attributeType.IsAssignableFrom(typeof(MethodImplAttribute)))
				{
					return new object[]
					{
						new MethodImplAttribute((MethodImplOptions)this.GetMethodImplementationFlags())
					};
				}
				return Array.Empty<object>();
			}

			// Token: 0x06004F7A RID: 20346 RVA: 0x00190869 File Offset: 0x0018FA69
			public override object[] GetCustomAttributes(bool inherit)
			{
				return new object[]
				{
					new MethodImplAttribute((MethodImplOptions)this.GetMethodImplementationFlags())
				};
			}

			// Token: 0x06004F7B RID: 20347 RVA: 0x0019087F File Offset: 0x0018FA7F
			public override bool IsDefined(Type attributeType, bool inherit)
			{
				if (attributeType == null)
				{
					throw new ArgumentNullException("attributeType");
				}
				return attributeType.IsAssignableFrom(typeof(MethodImplAttribute));
			}

			// Token: 0x17000D01 RID: 3329
			// (get) Token: 0x06004F7C RID: 20348 RVA: 0x001908AA File Offset: 0x0018FAAA
			public override bool IsSecurityCritical
			{
				get
				{
					return this.m_owner.IsSecurityCritical;
				}
			}

			// Token: 0x17000D02 RID: 3330
			// (get) Token: 0x06004F7D RID: 20349 RVA: 0x001908B7 File Offset: 0x0018FAB7
			public override bool IsSecuritySafeCritical
			{
				get
				{
					return this.m_owner.IsSecuritySafeCritical;
				}
			}

			// Token: 0x17000D03 RID: 3331
			// (get) Token: 0x06004F7E RID: 20350 RVA: 0x001908C4 File Offset: 0x0018FAC4
			public override bool IsSecurityTransparent
			{
				get
				{
					return this.m_owner.IsSecurityTransparent;
				}
			}

			// Token: 0x17000D04 RID: 3332
			// (get) Token: 0x06004F7F RID: 20351 RVA: 0x001908D1 File Offset: 0x0018FAD1
			public override Type ReturnType
			{
				get
				{
					return this.m_owner.m_returnType;
				}
			}

			// Token: 0x17000D05 RID: 3333
			// (get) Token: 0x06004F80 RID: 20352 RVA: 0x001908DE File Offset: 0x0018FADE
			public override ParameterInfo ReturnParameter
			{
				get
				{
					return new RuntimeParameterInfo(this, null, this.m_owner.m_returnType, -1);
				}
			}

			// Token: 0x17000D06 RID: 3334
			// (get) Token: 0x06004F81 RID: 20353 RVA: 0x001908F3 File Offset: 0x0018FAF3
			public override ICustomAttributeProvider ReturnTypeCustomAttributes
			{
				get
				{
					return new EmptyCAHolder();
				}
			}

			// Token: 0x06004F82 RID: 20354 RVA: 0x001908FC File Offset: 0x0018FAFC
			internal RuntimeParameterInfo[] LoadParameters()
			{
				if (this.m_parameters == null)
				{
					Type[] parameterTypes = this.m_owner.m_parameterTypes;
					Type[] array = parameterTypes;
					RuntimeParameterInfo[] array2 = new RuntimeParameterInfo[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new RuntimeParameterInfo(this, null, array[i], i);
					}
					if (this.m_parameters == null)
					{
						this.m_parameters = array2;
					}
				}
				return this.m_parameters;
			}

			// Token: 0x0400144C RID: 5196
			internal DynamicMethod m_owner;

			// Token: 0x0400144D RID: 5197
			private RuntimeParameterInfo[] m_parameters;

			// Token: 0x0400144E RID: 5198
			private string m_name;

			// Token: 0x0400144F RID: 5199
			private MethodAttributes m_attributes;

			// Token: 0x04001450 RID: 5200
			private CallingConventions m_callingConvention;
		}
	}
}
