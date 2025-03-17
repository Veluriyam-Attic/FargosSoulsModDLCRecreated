using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200064C RID: 1612
	[NullableContext(1)]
	[Nullable(0)]
	public class ModuleBuilder : Module
	{
		// Token: 0x06005133 RID: 20787
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr nCreateISymWriterForDynamicModule(Module module, string filename);

		// Token: 0x06005134 RID: 20788 RVA: 0x00194B54 File Offset: 0x00193D54
		internal static string UnmangleTypeName(string typeName)
		{
			int num = typeName.Length - 1;
			for (;;)
			{
				num = typeName.LastIndexOf('+', num);
				if (num == -1)
				{
					break;
				}
				bool flag = true;
				int num2 = num;
				while (typeName[--num2] == '\\')
				{
					flag = !flag;
				}
				if (flag)
				{
					break;
				}
				num = num2;
			}
			return typeName.Substring(num + 1);
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06005135 RID: 20789 RVA: 0x00194BA2 File Offset: 0x00193DA2
		internal AssemblyBuilder ContainingAssemblyBuilder
		{
			get
			{
				return this._assemblyBuilder;
			}
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00194BAA File Offset: 0x00193DAA
		internal ModuleBuilder(AssemblyBuilder assemblyBuilder, InternalModuleBuilder internalModuleBuilder)
		{
			this._internalModuleBuilder = internalModuleBuilder;
			this._assemblyBuilder = assemblyBuilder;
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x00194BC0 File Offset: 0x00193DC0
		internal void AddType(string name, Type type)
		{
			this._typeBuilderDict.Add(name, type);
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x00194BD0 File Offset: 0x00193DD0
		internal void CheckTypeNameConflict(string strTypeName, Type enclosingType)
		{
			Type type;
			if (this._typeBuilderDict.TryGetValue(strTypeName, out type) && type.DeclaringType == enclosingType)
			{
				throw new ArgumentException(SR.Argument_DuplicateTypeName);
			}
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x00194C01 File Offset: 0x00193E01
		private static Type GetType(string strFormat, Type baseType)
		{
			if (string.IsNullOrEmpty(strFormat))
			{
				return baseType;
			}
			return SymbolType.FormCompoundType(strFormat, baseType, 0);
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x00194C15 File Offset: 0x00193E15
		internal void CheckContext(params Type[][] typess)
		{
			AssemblyBuilder.CheckContext(typess);
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x00194C1D File Offset: 0x00193E1D
		internal void CheckContext(params Type[] types)
		{
			AssemblyBuilder.CheckContext(types);
		}

		// Token: 0x0600513C RID: 20796
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetTypeRef(QCallModule module, string strFullName, QCallModule refedModule, string strRefedModuleFileName, int tkResolution);

		// Token: 0x0600513D RID: 20797
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRef(QCallModule module, QCallModule refedModule, int tr, int defToken);

		// Token: 0x0600513E RID: 20798 RVA: 0x00194C28 File Offset: 0x00193E28
		private int GetMemberRef(Module refedModule, int tr, int defToken)
		{
			ModuleBuilder moduleBuilder = this;
			RuntimeModule runtimeModuleFromModule = ModuleBuilder.GetRuntimeModuleFromModule(refedModule);
			return ModuleBuilder.GetMemberRef(new QCallModule(ref moduleBuilder), new QCallModule(ref runtimeModuleFromModule), tr, defToken);
		}

		// Token: 0x0600513F RID: 20799
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRefFromSignature(QCallModule module, int tr, string methodName, byte[] signature, int length);

		// Token: 0x06005140 RID: 20800 RVA: 0x00194C54 File Offset: 0x00193E54
		private int GetMemberRefFromSignature(int tr, string methodName, byte[] signature, int length)
		{
			ModuleBuilder moduleBuilder = this;
			return ModuleBuilder.GetMemberRefFromSignature(new QCallModule(ref moduleBuilder), tr, methodName, signature, length);
		}

		// Token: 0x06005141 RID: 20801
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRefOfMethodInfo(QCallModule module, int tr, RuntimeMethodHandleInternal method);

		// Token: 0x06005142 RID: 20802 RVA: 0x00194C74 File Offset: 0x00193E74
		private int GetMemberRefOfMethodInfo(int tr, RuntimeMethodInfo method)
		{
			ModuleBuilder moduleBuilder = this;
			int memberRefOfMethodInfo = ModuleBuilder.GetMemberRefOfMethodInfo(new QCallModule(ref moduleBuilder), tr, ((IRuntimeMethodInfo)method).Value);
			GC.KeepAlive(method);
			return memberRefOfMethodInfo;
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00194C74 File Offset: 0x00193E74
		private int GetMemberRefOfMethodInfo(int tr, RuntimeConstructorInfo method)
		{
			ModuleBuilder moduleBuilder = this;
			int memberRefOfMethodInfo = ModuleBuilder.GetMemberRefOfMethodInfo(new QCallModule(ref moduleBuilder), tr, ((IRuntimeMethodInfo)method).Value);
			GC.KeepAlive(method);
			return memberRefOfMethodInfo;
		}

		// Token: 0x06005144 RID: 20804
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRefOfFieldInfo(QCallModule module, int tkType, QCallTypeHandle declaringType, int tkField);

		// Token: 0x06005145 RID: 20805 RVA: 0x00194CA0 File Offset: 0x00193EA0
		private int GetMemberRefOfFieldInfo(int tkType, RuntimeTypeHandle declaringType, RuntimeFieldInfo runtimeField)
		{
			ModuleBuilder moduleBuilder = this;
			return ModuleBuilder.GetMemberRefOfFieldInfo(new QCallModule(ref moduleBuilder), tkType, new QCallTypeHandle(ref declaringType), runtimeField.MetadataToken);
		}

		// Token: 0x06005146 RID: 20806
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetTokenFromTypeSpec(QCallModule pModule, byte[] signature, int length);

		// Token: 0x06005147 RID: 20807 RVA: 0x00194CCC File Offset: 0x00193ECC
		private int GetTokenFromTypeSpec(byte[] signature, int length)
		{
			ModuleBuilder moduleBuilder = this;
			return ModuleBuilder.GetTokenFromTypeSpec(new QCallModule(ref moduleBuilder), signature, length);
		}

		// Token: 0x06005148 RID: 20808
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetArrayMethodToken(QCallModule module, int tkTypeSpec, string methodName, byte[] signature, int sigLength);

		// Token: 0x06005149 RID: 20809
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetStringConstant(QCallModule module, string str, int length);

		// Token: 0x0600514A RID: 20810
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetFieldRVAContent(QCallModule module, int fdToken, byte[] data, int length);

		// Token: 0x0600514B RID: 20811 RVA: 0x00194CEC File Offset: 0x00193EEC
		internal virtual Type FindTypeBuilderWithName(string strTypeName, bool ignoreCase)
		{
			if (ignoreCase)
			{
				using (Dictionary<string, Type>.KeyCollection.Enumerator enumerator = this._typeBuilderDict.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (string.Equals(text, strTypeName, StringComparison.OrdinalIgnoreCase))
						{
							return this._typeBuilderDict[text];
						}
					}
					goto IL_62;
				}
			}
			Type result;
			if (this._typeBuilderDict.TryGetValue(strTypeName, out result))
			{
				return result;
			}
			IL_62:
			return null;
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00194D70 File Offset: 0x00193F70
		private int GetTypeRefNested(Type type, Module refedModule, string strRefedModuleFileName)
		{
			Type declaringType = type.DeclaringType;
			int tkResolution = 0;
			string text = type.FullName;
			if (declaringType != null)
			{
				tkResolution = this.GetTypeRefNested(declaringType, refedModule, strRefedModuleFileName);
				text = ModuleBuilder.UnmangleTypeName(text);
			}
			ModuleBuilder moduleBuilder = this;
			RuntimeModule runtimeModuleFromModule = ModuleBuilder.GetRuntimeModuleFromModule(refedModule);
			return ModuleBuilder.GetTypeRef(new QCallModule(ref moduleBuilder), text, new QCallModule(ref runtimeModuleFromModule), strRefedModuleFileName, tkResolution);
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00194DC8 File Offset: 0x00193FC8
		internal MethodToken InternalGetConstructorToken(ConstructorInfo con, bool usingRef)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			ConstructorBuilder constructorBuilder = con as ConstructorBuilder;
			int methodToken;
			if (constructorBuilder != null)
			{
				if (!usingRef && constructorBuilder.Module.Equals(this))
				{
					return constructorBuilder.GetToken();
				}
				int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
				methodToken = this.GetMemberRef(con.ReflectedType.Module, token, constructorBuilder.GetToken().Token);
			}
			else
			{
				ConstructorOnTypeBuilderInstantiation constructorOnTypeBuilderInstantiation = con as ConstructorOnTypeBuilderInstantiation;
				if (constructorOnTypeBuilderInstantiation != null)
				{
					if (usingRef)
					{
						throw new InvalidOperationException();
					}
					int token = this.GetTypeTokenInternal(con.DeclaringType).Token;
					methodToken = this.GetMemberRef(con.DeclaringType.Module, token, constructorOnTypeBuilderInstantiation.MetadataTokenInternal);
				}
				else
				{
					RuntimeConstructorInfo runtimeConstructorInfo = con as RuntimeConstructorInfo;
					if (runtimeConstructorInfo != null && !con.ReflectedType.IsArray)
					{
						int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
						methodToken = this.GetMemberRefOfMethodInfo(token, runtimeConstructorInfo);
					}
					else
					{
						ParameterInfo[] parameters = con.GetParameters();
						if (parameters == null)
						{
							throw new ArgumentException(SR.Argument_InvalidConstructorInfo);
						}
						Type[] array = new Type[parameters.Length];
						Type[][] array2 = new Type[parameters.Length][];
						Type[][] array3 = new Type[parameters.Length][];
						for (int i = 0; i < parameters.Length; i++)
						{
							if (parameters[i] == null)
							{
								throw new ArgumentException(SR.Argument_InvalidConstructorInfo);
							}
							array[i] = parameters[i].ParameterType;
							array2[i] = parameters[i].GetRequiredCustomModifiers();
							array3[i] = parameters[i].GetOptionalCustomModifiers();
						}
						int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
						SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this, con.CallingConvention, null, null, null, array, array2, array3);
						int length;
						byte[] signature = methodSigHelper.InternalGetSignature(out length);
						methodToken = this.GetMemberRefFromSignature(token, con.Name, signature, length);
					}
				}
			}
			return new MethodToken(methodToken);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00194FA8 File Offset: 0x001941A8
		internal void Init(string strModuleName)
		{
			this._moduleData = new ModuleBuilderData(this, strModuleName);
			this._typeBuilderDict = new Dictionary<string, Type>();
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x00194FC2 File Offset: 0x001941C2
		internal void SetSymWriter(ISymbolWriter writer)
		{
			this._iSymWriter = writer;
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06005150 RID: 20816 RVA: 0x00194FCB File Offset: 0x001941CB
		internal object SyncRoot
		{
			get
			{
				return this.ContainingAssemblyBuilder.SyncRoot;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06005151 RID: 20817 RVA: 0x00194FD8 File Offset: 0x001941D8
		internal InternalModuleBuilder InternalModule
		{
			get
			{
				return this._internalModuleBuilder;
			}
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00194FE0 File Offset: 0x001941E0
		protected override ModuleHandle GetModuleHandleImpl()
		{
			return new ModuleHandle(this.GetNativeHandle());
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x00194FED File Offset: 0x001941ED
		internal RuntimeModule GetNativeHandle()
		{
			return this.InternalModule.GetNativeHandle();
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x00194FFC File Offset: 0x001941FC
		private static RuntimeModule GetRuntimeModuleFromModule(Module m)
		{
			ModuleBuilder moduleBuilder = m as ModuleBuilder;
			if (moduleBuilder != null)
			{
				return moduleBuilder.InternalModule;
			}
			return m as RuntimeModule;
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x00195028 File Offset: 0x00194228
		private int GetMemberRefToken(MethodBase method, IEnumerable<Type> optionalParameterTypes)
		{
			int cGenericParameters = 0;
			if (method.IsGenericMethod)
			{
				if (!method.IsGenericMethodDefinition)
				{
					throw new InvalidOperationException();
				}
				cGenericParameters = method.GetGenericArguments().Length;
			}
			if (optionalParameterTypes != null && (method.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotAVarArgCallingConvention);
			}
			MethodInfo methodInfo = method as MethodInfo;
			Type[] parameterTypes;
			Type methodBaseReturnType;
			if (method.DeclaringType.IsGenericType)
			{
				MethodOnTypeBuilderInstantiation methodOnTypeBuilderInstantiation = method as MethodOnTypeBuilderInstantiation;
				MethodBase methodBase;
				if (methodOnTypeBuilderInstantiation != null)
				{
					methodBase = methodOnTypeBuilderInstantiation.m_method;
				}
				else
				{
					ConstructorOnTypeBuilderInstantiation constructorOnTypeBuilderInstantiation = method as ConstructorOnTypeBuilderInstantiation;
					if (constructorOnTypeBuilderInstantiation != null)
					{
						methodBase = constructorOnTypeBuilderInstantiation.m_ctor;
					}
					else if (method is MethodBuilder || method is ConstructorBuilder)
					{
						methodBase = method;
					}
					else if (method.IsGenericMethod)
					{
						methodBase = methodInfo.GetGenericMethodDefinition();
						Module module = methodBase.Module;
						int metadataToken = method.MetadataToken;
						Type declaringType = methodBase.DeclaringType;
						methodBase = module.ResolveMethod(metadataToken, (declaringType != null) ? declaringType.GetGenericArguments() : null, methodBase.GetGenericArguments());
					}
					else
					{
						Module module2 = method.Module;
						int metadataToken2 = method.MetadataToken;
						Type declaringType2 = method.DeclaringType;
						methodBase = module2.ResolveMethod(metadataToken2, (declaringType2 != null) ? declaringType2.GetGenericArguments() : null, null);
					}
				}
				parameterTypes = methodBase.GetParameterTypes();
				methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(methodBase);
			}
			else
			{
				parameterTypes = method.GetParameterTypes();
				methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(method);
			}
			int length;
			byte[] signature = this.GetMemberRefSignature(method.CallingConvention, methodBaseReturnType, parameterTypes, optionalParameterTypes, cGenericParameters).InternalGetSignature(out length);
			int tr;
			if (method.DeclaringType.IsGenericType)
			{
				int length2;
				byte[] signature2 = SignatureHelper.GetTypeSigToken(this, method.DeclaringType).InternalGetSignature(out length2);
				tr = this.GetTokenFromTypeSpec(signature2, length2);
			}
			else if (!method.Module.Equals(this))
			{
				tr = this.GetTypeToken(method.DeclaringType).Token;
			}
			else if (methodInfo != null)
			{
				tr = this.GetMethodToken(methodInfo).Token;
			}
			else
			{
				tr = this.GetConstructorToken(method as ConstructorInfo).Token;
			}
			return this.GetMemberRefFromSignature(tr, method.Name, signature, length);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x00195208 File Offset: 0x00194408
		internal SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, IEnumerable<Type> optionalParameterTypes, int cGenericParameters)
		{
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this, call, returnType, cGenericParameters);
			if (parameterTypes != null)
			{
				foreach (Type clsArgument in parameterTypes)
				{
					methodSigHelper.AddArgument(clsArgument);
				}
			}
			if (optionalParameterTypes != null)
			{
				int num = 0;
				foreach (Type clsArgument2 in optionalParameterTypes)
				{
					if (num == 0)
					{
						methodSigHelper.AddSentinel();
					}
					methodSigHelper.AddArgument(clsArgument2);
					num++;
				}
			}
			return methodSigHelper;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0019529C File Offset: 0x0019449C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.InternalModule.Equals(obj);
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x001952AA File Offset: 0x001944AA
		public override int GetHashCode()
		{
			return this.InternalModule.GetHashCode();
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x001952B7 File Offset: 0x001944B7
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.InternalModule.GetCustomAttributes(inherit);
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x001952C5 File Offset: 0x001944C5
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.InternalModule.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x001952D4 File Offset: 0x001944D4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.InternalModule.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x001952E3 File Offset: 0x001944E3
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this.InternalModule.GetCustomAttributesData();
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x001952F0 File Offset: 0x001944F0
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type[] GetTypes()
		{
			object syncRoot = this.SyncRoot;
			Type[] typesNoLock;
			lock (syncRoot)
			{
				typesNoLock = this.GetTypesNoLock();
			}
			return typesNoLock;
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00195334 File Offset: 0x00194534
		internal Type[] GetTypesNoLock()
		{
			Type[] array = new Type[this._typeBuilderDict.Count];
			int num = 0;
			foreach (Type type in this._typeBuilderDict.Values)
			{
				EnumBuilder enumBuilder = type as EnumBuilder;
				TypeBuilder typeBuilder;
				if (enumBuilder != null)
				{
					typeBuilder = enumBuilder.m_typeBuilder;
				}
				else
				{
					typeBuilder = (TypeBuilder)type;
				}
				if (typeBuilder.IsCreated())
				{
					array[num++] = typeBuilder.UnderlyingSystemType;
				}
				else
				{
					array[num++] = type;
				}
			}
			return array;
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0018BDC2 File Offset: 0x0018AFC2
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public override Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0018BDCD File Offset: 0x0018AFCD
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public override Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x001953E0 File Offset: 0x001945E0
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			object syncRoot = this.SyncRoot;
			Type typeNoLock;
			lock (syncRoot)
			{
				typeNoLock = this.GetTypeNoLock(className, throwOnError, ignoreCase);
			}
			return typeNoLock;
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x00195428 File Offset: 0x00194628
		private Type GetTypeNoLock(string className, bool throwOnError, bool ignoreCase)
		{
			Type type = this.InternalModule.GetType(className, throwOnError, ignoreCase);
			if (type != null)
			{
				return type;
			}
			string text = null;
			string text2 = null;
			int num;
			for (int i = 0; i <= className.Length; i = num + 1)
			{
				num = className.AsSpan(i).IndexOfAny('[', '*', '&');
				if (num == -1)
				{
					text = className;
					text2 = null;
					break;
				}
				num += i;
				int num2 = 0;
				int num3 = num - 1;
				while (num3 >= 0 && className[num3] == '\\')
				{
					num2++;
					num3--;
				}
				if (num2 % 2 != 1)
				{
					text = className.Substring(0, num);
					text2 = className.Substring(num);
					break;
				}
			}
			if (text == null)
			{
				text = className;
				text2 = null;
			}
			text = text.Replace("\\\\", "\\").Replace("\\[", "[").Replace("\\*", "*").Replace("\\&", "&");
			if (text2 != null)
			{
				type = this.InternalModule.GetType(text, false, ignoreCase);
			}
			if (type == null)
			{
				type = this.FindTypeBuilderWithName(text, ignoreCase);
				if (type == null && this.Assembly is AssemblyBuilder)
				{
					List<ModuleBuilder> moduleBuilderList = this.ContainingAssemblyBuilder._assemblyData._moduleBuilderList;
					int count = moduleBuilderList.Count;
					int num4 = 0;
					while (num4 < count && type == null)
					{
						ModuleBuilder moduleBuilder = moduleBuilderList[num4];
						type = moduleBuilder.FindTypeBuilderWithName(text, ignoreCase);
						num4++;
					}
				}
				if (type == null)
				{
					return null;
				}
			}
			if (text2 == null)
			{
				return type;
			}
			return ModuleBuilder.GetType(text2, type);
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x001955B0 File Offset: 0x001947B0
		public override string FullyQualifiedName
		{
			get
			{
				return this._moduleData._moduleName;
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x001955BD File Offset: 0x001947BD
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override byte[] ResolveSignature(int metadataToken)
		{
			return this.InternalModule.ResolveSignature(metadataToken);
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x001955CB File Offset: 0x001947CB
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		[NullableContext(2)]
		public override MethodBase ResolveMethod(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x001955DB File Offset: 0x001947DB
		[NullableContext(2)]
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override FieldInfo ResolveField(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x001955EB File Offset: 0x001947EB
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override Type ResolveType(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x001955FB File Offset: 0x001947FB
		[NullableContext(2)]
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override MemberInfo ResolveMember(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x0019560B File Offset: 0x0019480B
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override string ResolveString(int metadataToken)
		{
			return this.InternalModule.ResolveString(metadataToken);
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x00195619 File Offset: 0x00194819
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			this.InternalModule.GetPEKind(out peKind, out machine);
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x00195628 File Offset: 0x00194828
		public override int MDStreamVersion
		{
			get
			{
				return this.InternalModule.MDStreamVersion;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x00195635 File Offset: 0x00194835
		public override Guid ModuleVersionId
		{
			get
			{
				return this.InternalModule.ModuleVersionId;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x00195642 File Offset: 0x00194842
		public override int MetadataToken
		{
			get
			{
				return this.InternalModule.MetadataToken;
			}
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0019564F File Offset: 0x0019484F
		public override bool IsResource()
		{
			return this.InternalModule.IsResource();
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0019565C File Offset: 0x0019485C
		[RequiresUnreferencedCode("Fields might be removed")]
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			return this.InternalModule.GetFields(bindingFlags);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0019566A File Offset: 0x0019486A
		[RequiresUnreferencedCode("Fields might be removed")]
		[return: Nullable(2)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.InternalModule.GetField(name, bindingAttr);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00195679 File Offset: 0x00194879
		[RequiresUnreferencedCode("Methods might be removed")]
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			return this.InternalModule.GetMethods(bindingFlags);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x00195687 File Offset: 0x00194887
		[RequiresUnreferencedCode("Methods might be removed")]
		[NullableContext(2)]
		protected override MethodInfo GetMethodImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			return this.InternalModule.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06005173 RID: 20851 RVA: 0x0019569D File Offset: 0x0019489D
		public override string ScopeName
		{
			get
			{
				return this.InternalModule.ScopeName;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06005174 RID: 20852 RVA: 0x001956AA File Offset: 0x001948AA
		public override string Name
		{
			get
			{
				return this.InternalModule.Name;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x00194BA2 File Offset: 0x00193DA2
		public override Assembly Assembly
		{
			get
			{
				return this._assemblyBuilder;
			}
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x001956B8 File Offset: 0x001948B8
		public TypeBuilder DefineType(string name)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, TypeAttributes.NotPublic, null, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x00195700 File Offset: 0x00194900
		public TypeBuilder DefineType(string name, TypeAttributes attr)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, null, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x00195748 File Offset: 0x00194948
		public TypeBuilder DefineType(string name, TypeAttributes attr, [Nullable(2)] [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				this.CheckContext(new Type[]
				{
					parent
				});
				result = this.DefineTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x001957A0 File Offset: 0x001949A0
		public TypeBuilder DefineType(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] [Nullable(2)] Type parent, int typesize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, typesize);
			}
			return result;
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x001957EC File Offset: 0x001949EC
		public TypeBuilder DefineType(string name, TypeAttributes attr, [Nullable(2)] [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, PackingSize packingSize, int typesize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, null, packingSize, typesize);
			}
			return result;
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x00195838 File Offset: 0x00194A38
		public TypeBuilder DefineType(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] [Nullable(2)] Type parent, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] interfaces)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x00195884 File Offset: 0x00194A84
		private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
		{
			return new TypeBuilder(name, attr, parent, interfaces, this, packingSize, typesize, null);
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x00195898 File Offset: 0x00194A98
		public TypeBuilder DefineType(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] [Nullable(2)] Type parent, PackingSize packsize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, packsize);
			}
			return result;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x001958E0 File Offset: 0x00194AE0
		private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, PackingSize packsize)
		{
			return new TypeBuilder(name, attr, parent, null, this, packsize, 0, null);
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x001958F0 File Offset: 0x00194AF0
		public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
		{
			this.CheckContext(new Type[]
			{
				underlyingType
			});
			object syncRoot = this.SyncRoot;
			EnumBuilder result;
			lock (syncRoot)
			{
				EnumBuilder enumBuilder = this.DefineEnumNoLock(name, visibility, underlyingType);
				this._typeBuilderDict[name] = enumBuilder;
				result = enumBuilder;
			}
			return result;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00195954 File Offset: 0x00194B54
		private EnumBuilder DefineEnumNoLock(string name, TypeAttributes visibility, Type underlyingType)
		{
			return new EnumBuilder(name, underlyingType, visibility, this);
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00195960 File Offset: 0x00194B60
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x00195984 File Offset: 0x00194B84
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			object syncRoot = this.SyncRoot;
			MethodBuilder result;
			lock (syncRoot)
			{
				if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
				{
					throw new ArgumentException(SR.Argument_GlobalFunctionHasToBeStatic);
				}
				this.CheckContext(new Type[]
				{
					returnType
				});
				this.CheckContext(parameterTypes);
				result = this._moduleData._globalTypeBuilder.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
			}
			return result;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00195A0C File Offset: 0x00194C0C
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x00195A1C File Offset: 0x00194C1C
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x00195A3C File Offset: 0x00194C3C
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] requiredReturnTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalReturnTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] requiredParameterTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] optionalParameterTypeCustomModifiers)
		{
			object syncRoot = this.SyncRoot;
			MethodBuilder result;
			lock (syncRoot)
			{
				result = this.DefineGlobalMethodNoLock(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			}
			return result;
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x00195A90 File Offset: 0x00194C90
		private MethodBuilder DefineGlobalMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (this._moduleData._hasGlobalBeenCreated)
			{
				throw new InvalidOperationException(SR.InvalidOperation_GlobalsHaveBeenCreated);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(SR.Argument_GlobalFunctionHasToBeStatic);
			}
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.CheckContext(new Type[][]
			{
				requiredReturnTypeCustomModifiers,
				optionalReturnTypeCustomModifiers,
				parameterTypes
			});
			this.CheckContext(requiredParameterTypeCustomModifiers);
			this.CheckContext(optionalParameterTypeCustomModifiers);
			return this._moduleData._globalTypeBuilder.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x00195B48 File Offset: 0x00194D48
		public void CreateGlobalFunctions()
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.CreateGlobalFunctionsNoLock();
			}
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x00195B88 File Offset: 0x00194D88
		private void CreateGlobalFunctionsNoLock()
		{
			if (this._moduleData._hasGlobalBeenCreated)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotADebugModule);
			}
			this._moduleData._globalTypeBuilder.CreateType();
			this._moduleData._hasGlobalBeenCreated = true;
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00195BC0 File Offset: 0x00194DC0
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder result;
			lock (syncRoot)
			{
				result = this.DefineInitializedDataNoLock(name, data, attributes);
			}
			return result;
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00195C08 File Offset: 0x00194E08
		private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
		{
			if (this._moduleData._hasGlobalBeenCreated)
			{
				throw new InvalidOperationException(SR.InvalidOperation_GlobalsHaveBeenCreated);
			}
			return this._moduleData._globalTypeBuilder.DefineInitializedData(name, data, attributes);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00195C38 File Offset: 0x00194E38
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder result;
			lock (syncRoot)
			{
				result = this.DefineUninitializedDataNoLock(name, size, attributes);
			}
			return result;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00195C80 File Offset: 0x00194E80
		private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
		{
			if (this._moduleData._hasGlobalBeenCreated)
			{
				throw new InvalidOperationException(SR.InvalidOperation_GlobalsHaveBeenCreated);
			}
			return this._moduleData._globalTypeBuilder.DefineUninitializedData(name, size, attributes);
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00195CAD File Offset: 0x00194EAD
		internal TypeToken GetTypeTokenInternal(Type type)
		{
			return this.GetTypeTokenInternal(type, false);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00195CB8 File Offset: 0x00194EB8
		private TypeToken GetTypeTokenInternal(Type type, bool getGenericDefinition)
		{
			object syncRoot = this.SyncRoot;
			TypeToken typeTokenWorkerNoLock;
			lock (syncRoot)
			{
				typeTokenWorkerNoLock = this.GetTypeTokenWorkerNoLock(type, getGenericDefinition);
			}
			return typeTokenWorkerNoLock;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x00195CFC File Offset: 0x00194EFC
		public TypeToken GetTypeToken(Type type)
		{
			return this.GetTypeTokenInternal(type, true);
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00195D08 File Offset: 0x00194F08
		private TypeToken GetTypeTokenWorkerNoLock(Type type, bool getGenericDefinition)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.CheckContext(new Type[]
			{
				type
			});
			if (type.IsByRef)
			{
				throw new ArgumentException(SR.Argument_CannotGetTypeTokenForByRef);
			}
			if ((type.IsGenericType && (!type.IsGenericTypeDefinition || !getGenericDefinition)) || type.IsGenericParameter || type.IsArray || type.IsPointer)
			{
				int length;
				byte[] signature = SignatureHelper.GetTypeSigToken(this, type).InternalGetSignature(out length);
				return new TypeToken(this.GetTokenFromTypeSpec(signature, length));
			}
			Module module = type.Module;
			if (!module.Equals(this))
			{
				ModuleBuilder moduleBuilder = module as ModuleBuilder;
				string strRefedModuleFileName = string.Empty;
				if (module.Assembly.Equals(this.Assembly))
				{
					if (moduleBuilder == null)
					{
						moduleBuilder = this.ContainingAssemblyBuilder.GetModuleBuilder((InternalModuleBuilder)module);
					}
					strRefedModuleFileName = moduleBuilder._moduleData._moduleName;
				}
				return new TypeToken(this.GetTypeRefNested(type, module, strRefedModuleFileName));
			}
			EnumBuilder enumBuilder = type as EnumBuilder;
			TypeBuilder typeBuilder = (enumBuilder != null) ? enumBuilder.m_typeBuilder : (type as TypeBuilder);
			if (typeBuilder != null)
			{
				return typeBuilder.TypeToken;
			}
			GenericTypeParameterBuilder genericTypeParameterBuilder = type as GenericTypeParameterBuilder;
			if (genericTypeParameterBuilder != null)
			{
				return new TypeToken(genericTypeParameterBuilder.MetadataTokenInternal);
			}
			return new TypeToken(this.GetTypeRefNested(type, this, string.Empty));
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00195E5E File Offset: 0x0019505E
		public TypeToken GetTypeToken(string name)
		{
			return this.GetTypeToken(this.InternalModule.GetType(name, false, true));
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x00195E74 File Offset: 0x00195074
		public MethodToken GetMethodToken(MethodInfo method)
		{
			object syncRoot = this.SyncRoot;
			MethodToken methodTokenNoLock;
			lock (syncRoot)
			{
				methodTokenNoLock = this.GetMethodTokenNoLock(method, true);
			}
			return methodTokenNoLock;
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x00195EB8 File Offset: 0x001950B8
		internal MethodToken GetMethodTokenInternal(MethodInfo method)
		{
			object syncRoot = this.SyncRoot;
			MethodToken methodTokenNoLock;
			lock (syncRoot)
			{
				methodTokenNoLock = this.GetMethodTokenNoLock(method, false);
			}
			return methodTokenNoLock;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x00195EFC File Offset: 0x001950FC
		private MethodToken GetMethodTokenNoLock(MethodInfo method, bool getGenericTypeDefinition)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			MethodBuilder methodBuilder = method as MethodBuilder;
			int methodToken;
			if (methodBuilder != null)
			{
				int metadataTokenInternal = methodBuilder.MetadataTokenInternal;
				if (method.Module.Equals(this))
				{
					return new MethodToken(metadataTokenInternal);
				}
				if (method.DeclaringType == null)
				{
					throw new InvalidOperationException(SR.InvalidOperation_CannotImportGlobalFromDifferentModule);
				}
				int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
				methodToken = this.GetMemberRef(method.DeclaringType.Module, tr, metadataTokenInternal);
			}
			else
			{
				if (method is MethodOnTypeBuilderInstantiation)
				{
					return new MethodToken(this.GetMemberRefToken(method, null));
				}
				SymbolMethod symbolMethod = method as SymbolMethod;
				if (symbolMethod != null)
				{
					if (symbolMethod.GetModule() == this)
					{
						return symbolMethod.GetToken();
					}
					return symbolMethod.GetToken(this);
				}
				else
				{
					Type declaringType = method.DeclaringType;
					if (declaringType == null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_CannotImportGlobalFromDifferentModule);
					}
					if (declaringType.IsArray)
					{
						ParameterInfo[] parameters = method.GetParameters();
						Type[] array = new Type[parameters.Length];
						for (int i = 0; i < parameters.Length; i++)
						{
							array[i] = parameters[i].ParameterType;
						}
						return this.GetArrayMethodToken(declaringType, method.Name, method.CallingConvention, method.ReturnType, array);
					}
					RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
					if (runtimeMethodInfo != null)
					{
						int tr = getGenericTypeDefinition ? this.GetTypeToken(declaringType).Token : this.GetTypeTokenInternal(declaringType).Token;
						methodToken = this.GetMemberRefOfMethodInfo(tr, runtimeMethodInfo);
					}
					else
					{
						ParameterInfo[] parameters2 = method.GetParameters();
						Type[] array2 = new Type[parameters2.Length];
						Type[][] array3 = new Type[array2.Length][];
						Type[][] array4 = new Type[array2.Length][];
						for (int j = 0; j < parameters2.Length; j++)
						{
							array2[j] = parameters2[j].ParameterType;
							array3[j] = parameters2[j].GetRequiredCustomModifiers();
							array4[j] = parameters2[j].GetOptionalCustomModifiers();
						}
						int tr = getGenericTypeDefinition ? this.GetTypeToken(declaringType).Token : this.GetTypeTokenInternal(declaringType).Token;
						SignatureHelper methodSigHelper;
						try
						{
							methodSigHelper = SignatureHelper.GetMethodSigHelper(this, method.CallingConvention, method.ReturnType, method.ReturnParameter.GetRequiredCustomModifiers(), method.ReturnParameter.GetOptionalCustomModifiers(), array2, array3, array4);
						}
						catch (NotImplementedException)
						{
							methodSigHelper = SignatureHelper.GetMethodSigHelper(this, method.ReturnType, array2);
						}
						int length;
						byte[] signature = methodSigHelper.InternalGetSignature(out length);
						methodToken = this.GetMemberRefFromSignature(tr, method.Name, signature, length);
					}
				}
			}
			return new MethodToken(methodToken);
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x001961B0 File Offset: 0x001953B0
		internal int GetMethodTokenInternal(MethodBase method, IEnumerable<Type> optionalParameterTypes, bool useMethodDef)
		{
			MethodInfo methodInfo = method as MethodInfo;
			int num;
			if (method.IsGenericMethod)
			{
				MethodInfo methodInfo2 = methodInfo;
				bool isGenericMethodDefinition = methodInfo.IsGenericMethodDefinition;
				if (!isGenericMethodDefinition)
				{
					methodInfo2 = methodInfo.GetGenericMethodDefinition();
				}
				if (!this.Equals(methodInfo2.Module) || (methodInfo2.DeclaringType != null && methodInfo2.DeclaringType.IsGenericType))
				{
					num = this.GetMemberRefToken(methodInfo2, null);
				}
				else
				{
					num = this.GetMethodTokenInternal(methodInfo2).Token;
				}
				if (isGenericMethodDefinition && useMethodDef)
				{
					return num;
				}
				int sigLength;
				byte[] signature = SignatureHelper.GetMethodSpecSigHelper(this, methodInfo.GetGenericArguments()).InternalGetSignature(out sigLength);
				ModuleBuilder moduleBuilder = this;
				num = TypeBuilder.DefineMethodSpec(new QCallModule(ref moduleBuilder), num, signature, sigLength);
			}
			else if ((method.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0 && (method.DeclaringType == null || !method.DeclaringType.IsGenericType))
			{
				if (methodInfo != null)
				{
					num = this.GetMethodTokenInternal(methodInfo).Token;
				}
				else
				{
					num = this.GetConstructorToken(method as ConstructorInfo).Token;
				}
			}
			else
			{
				num = this.GetMemberRefToken(method, optionalParameterTypes);
			}
			return num;
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x001962C0 File Offset: 0x001954C0
		public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			object syncRoot = this.SyncRoot;
			MethodToken arrayMethodTokenNoLock;
			lock (syncRoot)
			{
				arrayMethodTokenNoLock = this.GetArrayMethodTokenNoLock(arrayClass, methodName, callingConvention, returnType, parameterTypes);
			}
			return arrayMethodTokenNoLock;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x0019630C File Offset: 0x0019550C
		private MethodToken GetArrayMethodTokenNoLock(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			if (arrayClass == null)
			{
				throw new ArgumentNullException("arrayClass");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			if (methodName.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "methodName");
			}
			if (!arrayClass.IsArray)
			{
				throw new ArgumentException(SR.Argument_HasToBeArrayClass);
			}
			this.CheckContext(new Type[]
			{
				returnType,
				arrayClass
			});
			this.CheckContext(parameterTypes);
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this, callingConvention, returnType, null, null, parameterTypes, null, null);
			int sigLength;
			byte[] signature = methodSigHelper.InternalGetSignature(out sigLength);
			TypeToken typeTokenInternal = this.GetTypeTokenInternal(arrayClass);
			ModuleBuilder moduleBuilder = this;
			return new MethodToken(ModuleBuilder.GetArrayMethodToken(new QCallModule(ref moduleBuilder), typeTokenInternal.Token, methodName, signature, sigLength));
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x001963C4 File Offset: 0x001955C4
		public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			this.CheckContext(new Type[]
			{
				returnType,
				arrayClass
			});
			this.CheckContext(parameterTypes);
			MethodToken arrayMethodToken = this.GetArrayMethodToken(arrayClass, methodName, callingConvention, returnType, parameterTypes);
			return new SymbolMethod(this, arrayMethodToken, arrayClass, methodName, callingConvention, returnType, parameterTypes);
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0019640A File Offset: 0x0019560A
		public MethodToken GetConstructorToken(ConstructorInfo con)
		{
			return this.InternalGetConstructorToken(con, false);
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x00196414 File Offset: 0x00195614
		public FieldToken GetFieldToken(FieldInfo field)
		{
			object syncRoot = this.SyncRoot;
			FieldToken fieldTokenNoLock;
			lock (syncRoot)
			{
				fieldTokenNoLock = this.GetFieldTokenNoLock(field);
			}
			return fieldTokenNoLock;
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00196458 File Offset: 0x00195658
		private FieldToken GetFieldTokenNoLock(FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			FieldBuilder fieldBuilder = field as FieldBuilder;
			int fieldToken;
			if (fieldBuilder != null)
			{
				if (field.DeclaringType != null && field.DeclaringType.IsGenericType)
				{
					int length;
					byte[] signature = SignatureHelper.GetTypeSigToken(this, field.DeclaringType).InternalGetSignature(out length);
					int num = this.GetTokenFromTypeSpec(signature, length);
					fieldToken = this.GetMemberRef(this, num, fieldBuilder.GetToken().Token);
				}
				else
				{
					if (fieldBuilder.Module.Equals(this))
					{
						return fieldBuilder.GetToken();
					}
					if (field.DeclaringType == null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_CannotImportGlobalFromDifferentModule);
					}
					int num = this.GetTypeTokenInternal(field.DeclaringType).Token;
					fieldToken = this.GetMemberRef(field.ReflectedType.Module, num, fieldBuilder.GetToken().Token);
				}
			}
			else
			{
				RuntimeFieldInfo runtimeFieldInfo = field as RuntimeFieldInfo;
				if (runtimeFieldInfo != null)
				{
					if (field.DeclaringType == null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_CannotImportGlobalFromDifferentModule);
					}
					if (field.DeclaringType != null && field.DeclaringType.IsGenericType)
					{
						int length2;
						byte[] signature2 = SignatureHelper.GetTypeSigToken(this, field.DeclaringType).InternalGetSignature(out length2);
						int num = this.GetTokenFromTypeSpec(signature2, length2);
						fieldToken = this.GetMemberRefOfFieldInfo(num, field.DeclaringType.GetTypeHandleInternal(), runtimeFieldInfo);
					}
					else
					{
						int num = this.GetTypeTokenInternal(field.DeclaringType).Token;
						fieldToken = this.GetMemberRefOfFieldInfo(num, field.DeclaringType.GetTypeHandleInternal(), runtimeFieldInfo);
					}
				}
				else
				{
					FieldOnTypeBuilderInstantiation fieldOnTypeBuilderInstantiation = field as FieldOnTypeBuilderInstantiation;
					if (fieldOnTypeBuilderInstantiation != null)
					{
						FieldInfo fieldInfo = fieldOnTypeBuilderInstantiation.FieldInfo;
						int length3;
						byte[] signature3 = SignatureHelper.GetTypeSigToken(this, field.DeclaringType).InternalGetSignature(out length3);
						int num = this.GetTokenFromTypeSpec(signature3, length3);
						fieldToken = this.GetMemberRef(fieldInfo.ReflectedType.Module, num, fieldOnTypeBuilderInstantiation.MetadataTokenInternal);
					}
					else
					{
						int num = this.GetTypeTokenInternal(field.ReflectedType).Token;
						SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this);
						fieldSigHelper.AddArgument(field.FieldType, field.GetRequiredCustomModifiers(), field.GetOptionalCustomModifiers());
						int length4;
						byte[] signature4 = fieldSigHelper.InternalGetSignature(out length4);
						fieldToken = this.GetMemberRefFromSignature(num, field.Name, signature4, length4);
					}
				}
			}
			return new FieldToken(fieldToken, field.GetType());
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x001966A8 File Offset: 0x001958A8
		public StringToken GetStringConstant(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			ModuleBuilder moduleBuilder = this;
			return new StringToken(ModuleBuilder.GetStringConstant(new QCallModule(ref moduleBuilder), str, str.Length));
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x001966E0 File Offset: 0x001958E0
		public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
		{
			if (sigHelper == null)
			{
				throw new ArgumentNullException("sigHelper");
			}
			int sigLength;
			byte[] signature = sigHelper.InternalGetSignature(out sigLength);
			ModuleBuilder moduleBuilder = this;
			return new SignatureToken(TypeBuilder.GetTokenFromSig(new QCallModule(ref moduleBuilder), signature, sigLength));
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0019671C File Offset: 0x0019591C
		public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
		{
			if (sigBytes == null)
			{
				throw new ArgumentNullException("sigBytes");
			}
			byte[] array = new byte[sigBytes.Length];
			Buffer.BlockCopy(sigBytes, 0, array, 0, sigBytes.Length);
			ModuleBuilder moduleBuilder = this;
			return new SignatureToken(TypeBuilder.GetTokenFromSig(new QCallModule(ref moduleBuilder), array, sigLength));
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x00196764 File Offset: 0x00195964
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
			TypeBuilder.DefineCustomAttribute(this, 1, this.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x001967AC File Offset: 0x001959AC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute(this, 1);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x001967C4 File Offset: 0x001959C4
		internal ISymbolWriter GetSymWriter()
		{
			return this._iSymWriter;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x001967CC File Offset: 0x001959CC
		[return: Nullable(2)]
		public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			object syncRoot = this.SyncRoot;
			ISymbolDocumentWriter result;
			lock (syncRoot)
			{
				result = this.DefineDocumentNoLock(url, language, languageVendor, documentType);
			}
			return result;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00196824 File Offset: 0x00195A24
		private ISymbolDocumentWriter DefineDocumentNoLock(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			if (this._iSymWriter == null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotADebugModule);
			}
			return this._iSymWriter.DefineDocument(url, language, languageVendor, documentType);
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x00196849 File Offset: 0x00195A49
		public bool IsTransient()
		{
			return this.InternalModule.IsTransientInternal();
		}

		// Token: 0x040014E7 RID: 5351
		private Dictionary<string, Type> _typeBuilderDict;

		// Token: 0x040014E8 RID: 5352
		private ISymbolWriter _iSymWriter;

		// Token: 0x040014E9 RID: 5353
		internal ModuleBuilderData _moduleData;

		// Token: 0x040014EA RID: 5354
		internal InternalModuleBuilder _internalModuleBuilder;

		// Token: 0x040014EB RID: 5355
		private readonly AssemblyBuilder _assemblyBuilder;
	}
}
