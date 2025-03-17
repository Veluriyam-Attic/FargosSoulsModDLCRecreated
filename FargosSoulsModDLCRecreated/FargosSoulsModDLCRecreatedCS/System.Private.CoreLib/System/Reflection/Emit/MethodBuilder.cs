using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000647 RID: 1607
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class MethodBuilder : MethodInfo
	{
		// Token: 0x060050C6 RID: 20678 RVA: 0x00193824 File Offset: 0x00192A24
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TypeBuilder type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(SR.Argument_IllegalName, "name");
			}
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (parameterTypes != null)
			{
				foreach (Type left in parameterTypes)
				{
					if (left == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.m_strName = name;
			this.m_module = mod;
			this.m_containingType = type;
			this.m_returnType = (returnType ?? typeof(void));
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				callingConvention |= CallingConventions.HasThis;
			}
			else if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(SR.Arg_NoStaticVirtual);
			}
			this.m_callingConvention = callingConvention;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = null;
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
			this.m_iAttributes = attributes;
			this.m_bIsBaked = false;
			this.m_fInitLocals = true;
			this.m_localSymInfo = new LocalSymInfo();
			this.m_ubBody = null;
			this.m_ilGenerator = null;
			this.m_dwMethodImplFlags = MethodImplAttributes.IL;
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x0019398D File Offset: 0x00192B8D
		internal void CheckContext(params Type[][] typess)
		{
			this.m_module.CheckContext(typess);
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x0019399B File Offset: 0x00192B9B
		internal void CheckContext(params Type[] types)
		{
			this.m_module.CheckContext(types);
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x001939AC File Offset: 0x00192BAC
		internal void CreateMethodBodyHelper(ILGenerator il)
		{
			if (il == null)
			{
				throw new ArgumentNullException("il");
			}
			int num = 0;
			ModuleBuilder module = this.m_module;
			this.m_containingType.ThrowIfCreated();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(SR.InvalidOperation_MethodHasBody);
			}
			if (il.m_methodBuilder != this && il.m_methodBuilder != null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_BadILGeneratorUsage);
			}
			this.ThrowIfShouldNotHaveBody();
			if (il.m_ScopeTree.m_iOpenScopeCount != 0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_OpenLocalVariableScope);
			}
			this.m_ubBody = il.BakeByteArray();
			this.m_mdMethodFixups = il.GetTokenFixups();
			__ExceptionInfo[] exceptions = il.GetExceptions();
			int num2 = MethodBuilder.CalculateNumberOfExceptions(exceptions);
			if (num2 > 0)
			{
				this.m_exceptions = new ExceptionHandler[num2];
				for (int i = 0; i < exceptions.Length; i++)
				{
					int[] filterAddresses = exceptions[i].GetFilterAddresses();
					int[] catchAddresses = exceptions[i].GetCatchAddresses();
					int[] catchEndAddresses = exceptions[i].GetCatchEndAddresses();
					Type[] catchClass = exceptions[i].GetCatchClass();
					int numberOfCatches = exceptions[i].GetNumberOfCatches();
					int startAddress = exceptions[i].GetStartAddress();
					int endAddress = exceptions[i].GetEndAddress();
					int[] exceptionTypes = exceptions[i].GetExceptionTypes();
					for (int j = 0; j < numberOfCatches; j++)
					{
						int exceptionTypeToken = 0;
						if (catchClass[j] != null)
						{
							exceptionTypeToken = module.GetTypeTokenInternal(catchClass[j]).Token;
						}
						switch (exceptionTypes[j])
						{
						case 0:
						case 1:
						case 4:
							this.m_exceptions[num++] = new ExceptionHandler(startAddress, endAddress, filterAddresses[j], catchAddresses[j], catchEndAddresses[j], exceptionTypes[j], exceptionTypeToken);
							break;
						case 2:
							this.m_exceptions[num++] = new ExceptionHandler(startAddress, exceptions[i].GetFinallyEndAddress(), filterAddresses[j], catchAddresses[j], catchEndAddresses[j], exceptionTypes[j], exceptionTypeToken);
							break;
						}
					}
				}
			}
			this.m_bIsBaked = true;
			if (module.GetSymWriter() != null)
			{
				SymbolToken method = new SymbolToken(this.MetadataTokenInternal);
				ISymbolWriter symWriter = module.GetSymWriter();
				symWriter.OpenMethod(method);
				symWriter.OpenScope(0);
				if (this.m_localSymInfo != null)
				{
					this.m_localSymInfo.EmitLocalSymInfo(symWriter);
				}
				il.m_ScopeTree.EmitScopeTree(symWriter);
				il.m_LineNumberInfo.EmitLineNumberInfo(symWriter);
				symWriter.CloseScope(il.ILOffset);
				symWriter.CloseMethod();
			}
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00193C1E File Offset: 0x00192E1E
		internal void ReleaseBakedStructures()
		{
			if (!this.m_bIsBaked)
			{
				return;
			}
			this.m_ubBody = null;
			this.m_localSymInfo = null;
			this.m_mdMethodFixups = null;
			this.m_localSignature = null;
			this.m_exceptions = null;
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x00193C4C File Offset: 0x00192E4C
		internal override Type[] GetParameterTypes()
		{
			Type[] result;
			if ((result = this.m_parameterTypes) == null)
			{
				result = (this.m_parameterTypes = Array.Empty<Type>());
			}
			return result;
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x00193C74 File Offset: 0x00192E74
		internal static Type GetMethodBaseReturnType(MethodBase method)
		{
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo != null)
			{
				return methodInfo.ReturnType;
			}
			ConstructorInfo constructorInfo = method as ConstructorInfo;
			if (constructorInfo != null)
			{
				return constructorInfo.GetReturnType();
			}
			return null;
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x00193CA4 File Offset: 0x00192EA4
		internal void SetToken(MethodToken token)
		{
			this.m_tkMethod = token;
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x00193CAD File Offset: 0x00192EAD
		internal byte[] GetBody()
		{
			return this.m_ubBody;
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00193CB5 File Offset: 0x00192EB5
		internal int[] GetTokenFixups()
		{
			return this.m_mdMethodFixups;
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x00193CC0 File Offset: 0x00192EC0
		internal SignatureHelper GetMethodSignature()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = Array.Empty<Type>();
			}
			this.m_signature = SignatureHelper.GetMethodSigHelper(this.m_module, this.m_callingConvention, (this.m_inst != null) ? this.m_inst.Length : 0, this.m_returnType, this.m_returnTypeRequiredCustomModifiers, this.m_returnTypeOptionalCustomModifiers, this.m_parameterTypes, this.m_parameterTypeRequiredCustomModifiers, this.m_parameterTypeOptionalCustomModifiers);
			return this.m_signature;
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00193D34 File Offset: 0x00192F34
		internal byte[] GetLocalSignature(out int signatureLength)
		{
			if (this.m_localSignature != null)
			{
				signatureLength = this.m_localSignature.Length;
				return this.m_localSignature;
			}
			if (this.m_ilGenerator != null && this.m_ilGenerator.m_localCount != 0)
			{
				return this.m_ilGenerator.m_localSignature.InternalGetSignature(out signatureLength);
			}
			return SignatureHelper.GetLocalVarSigHelper(this.m_module).InternalGetSignature(out signatureLength);
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00193D92 File Offset: 0x00192F92
		internal int GetMaxStack()
		{
			if (this.m_ilGenerator != null)
			{
				return this.m_ilGenerator.GetMaxStackSize() + this.ExceptionHandlerCount;
			}
			return 16;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x00193DB1 File Offset: 0x00192FB1
		internal ExceptionHandler[] GetExceptionHandlers()
		{
			return this.m_exceptions;
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x00193DB9 File Offset: 0x00192FB9
		internal int ExceptionHandlerCount
		{
			get
			{
				if (this.m_exceptions == null)
				{
					return 0;
				}
				return this.m_exceptions.Length;
			}
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0018F590 File Offset: 0x0018E790
		internal static int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00193DCD File Offset: 0x00192FCD
		internal bool IsTypeCreated()
		{
			return this.m_containingType != null && this.m_containingType.IsCreated();
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x00193DEA File Offset: 0x00192FEA
		[return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_containingType;
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00193DF2 File Offset: 0x00192FF2
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.m_module;
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x00193DFC File Offset: 0x00192FFC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!(obj is MethodBuilder))
			{
				return false;
			}
			if (!this.m_strName.Equals(((MethodBuilder)obj).m_strName))
			{
				return false;
			}
			if (this.m_iAttributes != ((MethodBuilder)obj).m_iAttributes)
			{
				return false;
			}
			SignatureHelper methodSignature = ((MethodBuilder)obj).GetMethodSignature();
			return methodSignature.Equals(this.GetMethodSignature());
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00193E5F File Offset: 0x0019305F
		public override int GetHashCode()
		{
			return this.m_strName.GetHashCode();
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x00193E6C File Offset: 0x0019306C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			stringBuilder.Append("Name: ").Append(this.m_strName).AppendLine(" ");
			stringBuilder.Append("Attributes: ").Append((int)this.m_iAttributes).AppendLine();
			stringBuilder.Append("Method Signature: ").Append(this.GetMethodSignature()).AppendLine();
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060050DC RID: 20700 RVA: 0x00193EEA File Offset: 0x001930EA
		public override string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x00193EF4 File Offset: 0x001930F4
		internal int MetadataTokenInternal
		{
			get
			{
				return this.GetToken().Token;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060050DE RID: 20702 RVA: 0x00193F0F File Offset: 0x0019310F
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x00193F1C File Offset: 0x0019311C
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				if (this.m_containingType.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_containingType;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x001908F3 File Offset: 0x0018FAF3
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return new EmptyCAHolder();
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x000BC298 File Offset: 0x000BB498
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		[return: Nullable(1)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x00193F33 File Offset: 0x00193133
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dwMethodImplFlags;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x00193F3B File Offset: 0x0019313B
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_iAttributes;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060050E5 RID: 20709 RVA: 0x00193F43 File Offset: 0x00193143
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x00193F4C File Offset: 0x0019314C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_DynamicModule);
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060050E7 RID: 20711 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x000AC098 File Offset: 0x000AB298
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x00193F63 File Offset: 0x00193163
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x00193F6C File Offset: 0x0019316C
		public override ParameterInfo[] GetParameters()
		{
			if (!this.m_bIsBaked || this.m_containingType == null || this.m_containingType.BakedRuntimeType == null)
			{
				throw new NotSupportedException(SR.InvalidOperation_TypeNotCreated);
			}
			MethodInfo method = this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes);
			return method.GetParameters();
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060050ED RID: 20717 RVA: 0x00193FCC File Offset: 0x001931CC
		public override ParameterInfo ReturnParameter
		{
			get
			{
				if (!this.m_bIsBaked || this.m_containingType == null || this.m_containingType.BakedRuntimeType == null)
				{
					throw new InvalidOperationException(SR.InvalidOperation_TypeNotCreated);
				}
				MethodInfo method = this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes);
				return method.ReturnParameter;
			}
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x0019402B File Offset: 0x0019322B
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_bIsGenMethDef;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x000C279F File Offset: 0x000C199F
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00194033 File Offset: 0x00193233
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060050F4 RID: 20724 RVA: 0x00194044 File Offset: 0x00193244
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_inst != null;
			}
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00194050 File Offset: 0x00193250
		public override Type[] GetGenericArguments()
		{
			Type[] inst = this.m_inst;
			return inst ?? Array.Empty<Type>();
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x0019406E File Offset: 0x0019326E
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArguments);
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x00194078 File Offset: 0x00193278
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException(SR.Arg_EmptyArray, "names");
			}
			if (this.m_inst != null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_GenericParametersAlreadySet);
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i] == null)
				{
					throw new ArgumentNullException("names");
				}
			}
			if (this.m_tkMethod.Token != 0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_MethodBuilderBaked);
			}
			this.m_bIsGenMethDef = true;
			this.m_inst = new GenericTypeParameterBuilder[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				this.m_inst[j] = new GenericTypeParameterBuilder(new TypeBuilder(names[j], j, this));
			}
			return this.m_inst;
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x00194130 File Offset: 0x00193330
		internal void ThrowIfGeneric()
		{
			if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x00194148 File Offset: 0x00193348
		public MethodToken GetToken()
		{
			if (this.m_tkMethod.Token != 0)
			{
				return this.m_tkMethod;
			}
			MethodToken tokenNoLock = new MethodToken(0);
			List<MethodBuilder> listMethods = this.m_containingType.m_listMethods;
			lock (listMethods)
			{
				if (this.m_tkMethod.Token != 0)
				{
					return this.m_tkMethod;
				}
				int i;
				for (i = this.m_containingType.m_lastTokenizedMethod + 1; i < this.m_containingType.m_listMethods.Count; i++)
				{
					MethodBuilder methodBuilder = this.m_containingType.m_listMethods[i];
					tokenNoLock = methodBuilder.GetTokenNoLock();
					if (methodBuilder == this)
					{
						break;
					}
				}
				this.m_containingType.m_lastTokenizedMethod = i;
			}
			return tokenNoLock;
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x00194218 File Offset: 0x00193418
		private MethodToken GetTokenNoLock()
		{
			int sigLength;
			byte[] signature = this.GetMethodSignature().InternalGetSignature(out sigLength);
			ModuleBuilder module = this.m_module;
			int num = TypeBuilder.DefineMethod(new QCallModule(ref module), this.m_containingType.MetadataTokenInternal, this.m_strName, signature, sigLength, this.Attributes);
			this.m_tkMethod = new MethodToken(num);
			if (this.m_inst != null)
			{
				foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in this.m_inst)
				{
					if (!genericTypeParameterBuilder.m_type.IsCreated())
					{
						genericTypeParameterBuilder.m_type.CreateType();
					}
				}
			}
			TypeBuilder.SetMethodImpl(new QCallModule(ref module), num, this.m_dwMethodImplFlags);
			return this.m_tkMethod;
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x001942CA File Offset: 0x001934CA
		public void SetParameters(params Type[] parameterTypes)
		{
			this.CheckContext(parameterTypes);
			this.SetSignature(null, null, null, parameterTypes, null, null);
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x001942DF File Offset: 0x001934DF
		[NullableContext(2)]
		public void SetReturnType(Type returnType)
		{
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.SetSignature(returnType, null, null, null, null, null);
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00194300 File Offset: 0x00193500
		[NullableContext(2)]
		public void SetSignature(Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] returnTypeRequiredCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] returnTypeOptionalCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] parameterTypeRequiredCustomModifiers, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (this.m_tkMethod.Token != 0)
			{
				return;
			}
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.CheckContext(new Type[][]
			{
				returnTypeRequiredCustomModifiers,
				returnTypeOptionalCustomModifiers,
				parameterTypes
			});
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			this.ThrowIfGeneric();
			if (returnType != null)
			{
				this.m_returnType = returnType;
			}
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x001943AC File Offset: 0x001935AC
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, [Nullable(2)] string strParamName)
		{
			if (position < 0)
			{
				throw new ArgumentOutOfRangeException(SR.ArgumentOutOfRange_ParamSequence);
			}
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (position > 0 && (this.m_parameterTypes == null || position > this.m_parameterTypes.Length))
			{
				throw new ArgumentOutOfRangeException(SR.ArgumentOutOfRange_ParamSequence);
			}
			attributes &= ~(ParameterAttributes.HasDefault | ParameterAttributes.HasFieldMarshal | ParameterAttributes.Reserved3 | ParameterAttributes.Reserved4);
			return new ParameterBuilder(this, position, attributes, strParamName);
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x00194410 File Offset: 0x00193610
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			this.m_dwMethodImplFlags = attributes;
			this.m_canBeRuntimeImpl = true;
			ModuleBuilder module = this.m_module;
			TypeBuilder.SetMethodImpl(new QCallModule(ref module), this.MetadataTokenInternal, attributes);
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x00194458 File Offset: 0x00193658
		public ILGenerator GetILGenerator()
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			ILGenerator result;
			if ((result = this.m_ilGenerator) == null)
			{
				result = (this.m_ilGenerator = new ILGenerator(this));
			}
			return result;
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0019448C File Offset: 0x0019368C
		public ILGenerator GetILGenerator(int size)
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			ILGenerator result;
			if ((result = this.m_ilGenerator) == null)
			{
				result = (this.m_ilGenerator = new ILGenerator(this, size));
			}
			return result;
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x001944BF File Offset: 0x001936BF
		private void ThrowIfShouldNotHaveBody()
		{
			if ((this.m_dwMethodImplFlags & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.m_dwMethodImplFlags & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL || (this.m_iAttributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope || this.m_isDllImport)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ShouldNotHaveMethodBody);
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x001944F6 File Offset: 0x001936F6
		// (set) Token: 0x06005104 RID: 20740 RVA: 0x00194504 File Offset: 0x00193704
		public bool InitLocals
		{
			get
			{
				this.ThrowIfGeneric();
				return this.m_fInitLocals;
			}
			set
			{
				this.ThrowIfGeneric();
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x00194513 File Offset: 0x00193713
		public Module GetModule()
		{
			return this.GetModuleBuilder();
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06005106 RID: 20742 RVA: 0x0019451B File Offset: 0x0019371B
		public string Signature
		{
			get
			{
				return this.GetMethodSignature().ToString();
			}
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x00194528 File Offset: 0x00193728
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.ThrowIfGeneric();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.MetadataTokenInternal, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
			if (MethodBuilder.IsKnownCA(con))
			{
				this.ParseCA(con);
			}
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x00194590 File Offset: 0x00193790
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.ThrowIfGeneric();
			customBuilder.CreateCustomAttribute(this.m_module, this.MetadataTokenInternal);
			if (MethodBuilder.IsKnownCA(customBuilder.m_con))
			{
				this.ParseCA(customBuilder.m_con);
			}
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x001945DC File Offset: 0x001937DC
		private static bool IsKnownCA(ConstructorInfo con)
		{
			Type declaringType = con.DeclaringType;
			return declaringType == typeof(MethodImplAttribute) || declaringType == typeof(DllImportAttribute);
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00194614 File Offset: 0x00193814
		private void ParseCA(ConstructorInfo con)
		{
			Type declaringType = con.DeclaringType;
			if (declaringType == typeof(MethodImplAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				return;
			}
			if (declaringType == typeof(DllImportAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				this.m_isDllImport = true;
			}
		}

		// Token: 0x040014BC RID: 5308
		internal string m_strName;

		// Token: 0x040014BD RID: 5309
		private MethodToken m_tkMethod;

		// Token: 0x040014BE RID: 5310
		private readonly ModuleBuilder m_module;

		// Token: 0x040014BF RID: 5311
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		internal TypeBuilder m_containingType;

		// Token: 0x040014C0 RID: 5312
		private int[] m_mdMethodFixups;

		// Token: 0x040014C1 RID: 5313
		private byte[] m_localSignature;

		// Token: 0x040014C2 RID: 5314
		internal LocalSymInfo m_localSymInfo;

		// Token: 0x040014C3 RID: 5315
		internal ILGenerator m_ilGenerator;

		// Token: 0x040014C4 RID: 5316
		private byte[] m_ubBody;

		// Token: 0x040014C5 RID: 5317
		private ExceptionHandler[] m_exceptions;

		// Token: 0x040014C6 RID: 5318
		internal bool m_bIsBaked;

		// Token: 0x040014C7 RID: 5319
		private bool m_fInitLocals;

		// Token: 0x040014C8 RID: 5320
		private readonly MethodAttributes m_iAttributes;

		// Token: 0x040014C9 RID: 5321
		private readonly CallingConventions m_callingConvention;

		// Token: 0x040014CA RID: 5322
		private MethodImplAttributes m_dwMethodImplFlags;

		// Token: 0x040014CB RID: 5323
		private SignatureHelper m_signature;

		// Token: 0x040014CC RID: 5324
		internal Type[] m_parameterTypes;

		// Token: 0x040014CD RID: 5325
		private Type m_returnType;

		// Token: 0x040014CE RID: 5326
		private Type[] m_returnTypeRequiredCustomModifiers;

		// Token: 0x040014CF RID: 5327
		private Type[] m_returnTypeOptionalCustomModifiers;

		// Token: 0x040014D0 RID: 5328
		private Type[][] m_parameterTypeRequiredCustomModifiers;

		// Token: 0x040014D1 RID: 5329
		private Type[][] m_parameterTypeOptionalCustomModifiers;

		// Token: 0x040014D2 RID: 5330
		private GenericTypeParameterBuilder[] m_inst;

		// Token: 0x040014D3 RID: 5331
		private bool m_bIsGenMethDef;

		// Token: 0x040014D4 RID: 5332
		internal bool m_canBeRuntimeImpl;

		// Token: 0x040014D5 RID: 5333
		internal bool m_isDllImport;
	}
}
