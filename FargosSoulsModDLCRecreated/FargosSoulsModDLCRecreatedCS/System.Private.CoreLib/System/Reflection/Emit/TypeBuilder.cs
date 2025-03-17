using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000654 RID: 1620
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class TypeBuilder : TypeInfo
	{
		// Token: 0x0600524C RID: 21068 RVA: 0x000BC768 File Offset: 0x000BB968
		[NullableContext(2)]
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x00198200 File Offset: 0x00197400
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(SR.Argument_MustBeTypeBuilder);
			}
			if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
			{
				throw new ArgumentException(SR.Argument_NeedGenericMethodDefinition, "method");
			}
			if (method.DeclaringType == null || !method.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException(SR.Argument_MethodNeedGenericDeclaringType, "method");
			}
			if (type.GetGenericTypeDefinition() != method.DeclaringType)
			{
				throw new ArgumentException(SR.Argument_InvalidMethodDeclaringType, "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (!(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "type");
			}
			return MethodOnTypeBuilderInstantiation.GetMethod(method, type as TypeBuilderInstantiation);
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x001982D0 File Offset: 0x001974D0
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(SR.Argument_MustBeTypeBuilder);
			}
			if (!constructor.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException(SR.Argument_ConstructorNeedGenericDeclaringType, "constructor");
			}
			if (!(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "type");
			}
			if (type is TypeBuilder && type.IsGenericTypeDefinition)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (type.GetGenericTypeDefinition() != constructor.DeclaringType)
			{
				throw new ArgumentException(SR.Argument_InvalidConstructorDeclaringType, "type");
			}
			return ConstructorOnTypeBuilderInstantiation.GetConstructor(constructor, type as TypeBuilderInstantiation);
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0019837C File Offset: 0x0019757C
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(SR.Argument_MustBeTypeBuilder);
			}
			if (!field.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException(SR.Argument_FieldNeedGenericDeclaringType, "field");
			}
			if (!(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "type");
			}
			if (type is TypeBuilder && type.IsGenericTypeDefinition)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (type.GetGenericTypeDefinition() != field.DeclaringType)
			{
				throw new ArgumentException(SR.Argument_InvalidFieldDeclaringType, "type");
			}
			return FieldOnTypeBuilderInstantiation.GetField(field, type as TypeBuilderInstantiation);
		}

		// Token: 0x06005250 RID: 21072
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetParentType(QCallModule module, int tdTypeDef, int tkParent);

		// Token: 0x06005251 RID: 21073
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddInterfaceImpl(QCallModule module, int tdTypeDef, int tkInterface);

		// Token: 0x06005252 RID: 21074
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineMethod(QCallModule module, int tkParent, string name, byte[] signature, int sigLength, MethodAttributes attributes);

		// Token: 0x06005253 RID: 21075
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineMethodSpec(QCallModule module, int tkParent, byte[] signature, int sigLength);

		// Token: 0x06005254 RID: 21076
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineField(QCallModule module, int tkParent, string name, byte[] signature, int sigLength, FieldAttributes attributes);

		// Token: 0x06005255 RID: 21077
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetMethodIL(QCallModule module, int tk, bool isInitLocals, byte[] body, int bodyLength, byte[] LocalSig, int sigLength, int maxStackSize, ExceptionHandler[] exceptions, int numExceptions, int[] tokenFixups, int numTokenFixups);

		// Token: 0x06005256 RID: 21078
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DefineCustomAttribute(QCallModule module, int tkAssociate, int tkConstructor, byte[] attr, int attrLength, bool toDisk, bool updateCompilerFlags);

		// Token: 0x06005257 RID: 21079 RVA: 0x00198428 File Offset: 0x00197628
		internal static void DefineCustomAttribute(ModuleBuilder module, int tkAssociate, int tkConstructor, byte[] attr, bool toDisk, bool updateCompilerFlags)
		{
			byte[] array = null;
			if (attr != null)
			{
				array = new byte[attr.Length];
				Buffer.BlockCopy(attr, 0, array, 0, attr.Length);
			}
			TypeBuilder.DefineCustomAttribute(new QCallModule(ref module), tkAssociate, tkConstructor, array, (array != null) ? array.Length : 0, toDisk, updateCompilerFlags);
		}

		// Token: 0x06005258 RID: 21080
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineProperty(QCallModule module, int tkParent, string name, PropertyAttributes attributes, byte[] signature, int sigLength);

		// Token: 0x06005259 RID: 21081
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineEvent(QCallModule module, int tkParent, string name, EventAttributes attributes, int tkEventType);

		// Token: 0x0600525A RID: 21082
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void DefineMethodSemantics(QCallModule module, int tkAssociation, MethodSemanticsAttributes semantics, int tkMethod);

		// Token: 0x0600525B RID: 21083
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void DefineMethodImpl(QCallModule module, int tkType, int tkBody, int tkDecl);

		// Token: 0x0600525C RID: 21084
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetMethodImpl(QCallModule module, int tkMethod, MethodImplAttributes MethodImplAttributes);

		// Token: 0x0600525D RID: 21085
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int SetParamInfo(QCallModule module, int tkMethod, int iSequence, ParameterAttributes iParamAttributes, string strParamName);

		// Token: 0x0600525E RID: 21086
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int GetTokenFromSig(QCallModule module, byte[] signature, int sigLength);

		// Token: 0x0600525F RID: 21087
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetFieldLayoutOffset(QCallModule module, int fdToken, int iOffset);

		// Token: 0x06005260 RID: 21088
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetClassLayout(QCallModule module, int tk, PackingSize iPackingSize, int iTypeSize);

		// Token: 0x06005261 RID: 21089
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void SetConstantValue(QCallModule module, int tk, int corType, void* pValue);

		// Token: 0x06005262 RID: 21090
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetPInvokeData(QCallModule module, string DllName, string name, int token, int linkFlags);

		// Token: 0x06005263 RID: 21091 RVA: 0x0019846C File Offset: 0x0019766C
		internal static bool IsTypeEqual(Type t1, Type t2)
		{
			if (t1 == t2)
			{
				return true;
			}
			TypeBuilder typeBuilder = null;
			TypeBuilder typeBuilder2 = null;
			Type left;
			if (t1 is TypeBuilder)
			{
				typeBuilder = (TypeBuilder)t1;
				left = typeBuilder.m_bakedRuntimeType;
			}
			else
			{
				left = t1;
			}
			Type type;
			if (t2 is TypeBuilder)
			{
				typeBuilder2 = (TypeBuilder)t2;
				type = typeBuilder2.m_bakedRuntimeType;
			}
			else
			{
				type = t2;
			}
			return (typeBuilder != null && typeBuilder2 != null && typeBuilder == typeBuilder2) || (left != null && type != null && left == type);
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x001984F4 File Offset: 0x001976F4
		internal unsafe static void SetConstantValue(ModuleBuilder module, int tk, Type destType, object value)
		{
			if (value == null)
			{
				TypeBuilder.SetConstantValue(new QCallModule(ref module), tk, 18, null);
				return;
			}
			Type type = value.GetType();
			if (destType.IsByRef)
			{
				destType = destType.GetElementType();
			}
			destType = (Nullable.GetUnderlyingType(destType) ?? destType);
			if (destType.IsEnum)
			{
				EnumBuilder enumBuilder = destType as EnumBuilder;
				Type type2;
				if (enumBuilder != null)
				{
					type2 = enumBuilder.GetEnumUnderlyingType();
					if (type != enumBuilder.m_typeBuilder.m_bakedRuntimeType && type != type2)
					{
						throw new ArgumentException(SR.Argument_ConstantDoesntMatch);
					}
				}
				else
				{
					TypeBuilder typeBuilder = destType as TypeBuilder;
					if (typeBuilder != null)
					{
						type2 = typeBuilder.m_enumUnderlyingType;
						if (type2 == null || (type != typeBuilder.UnderlyingSystemType && type != type2))
						{
							throw new ArgumentException(SR.Argument_ConstantDoesntMatch);
						}
					}
					else
					{
						type2 = Enum.GetUnderlyingType(destType);
						if (type != destType && type != type2)
						{
							throw new ArgumentException(SR.Argument_ConstantDoesntMatch);
						}
					}
				}
				type = type2;
			}
			else if (!destType.IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.Argument_ConstantDoesntMatch);
			}
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType)type);
			if (corElementType - CorElementType.ELEMENT_TYPE_BOOLEAN <= 11)
			{
				fixed (byte* rawData = value.GetRawData())
				{
					byte* pValue = rawData;
					TypeBuilder.SetConstantValue(new QCallModule(ref module), tk, (int)corElementType, (void*)pValue);
				}
				return;
			}
			if (type == typeof(string))
			{
				string text = (string)value;
				char* ptr;
				if (text == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = text.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* pValue2 = ptr;
				TypeBuilder.SetConstantValue(new QCallModule(ref module), tk, 14, (void*)pValue2);
				char* ptr2 = null;
				return;
			}
			if (type == typeof(DateTime))
			{
				long ticks = ((DateTime)value).Ticks;
				TypeBuilder.SetConstantValue(new QCallModule(ref module), tk, 10, (void*)(&ticks));
				return;
			}
			throw new ArgumentException(SR.Format(SR.Argument_ConstantNotSupported, type));
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x001986B9 File Offset: 0x001978B9
		internal TypeBuilder(ModuleBuilder module)
		{
			this.m_tdType = new TypeToken(33554432);
			this.m_isHiddenGlobalType = true;
			this.m_module = module;
			this.m_listMethods = new List<MethodBuilder>();
			this.m_lastTokenizedMethod = -1;
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x001986F4 File Offset: 0x001978F4
		internal TypeBuilder(string szName, int genParamPos, MethodBuilder declMeth)
		{
			this.m_strName = szName;
			this.m_genParamPos = genParamPos;
			this.m_bIsGenParam = true;
			this.m_typeInterfaces = new List<Type>();
			this.m_declMeth = declMeth;
			this.m_DeclaringType = this.m_declMeth.GetTypeBuilder();
			this.m_module = declMeth.GetModuleBuilder();
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0019874B File Offset: 0x0019794B
		private TypeBuilder(string szName, int genParamPos, TypeBuilder declType)
		{
			this.m_strName = szName;
			this.m_genParamPos = genParamPos;
			this.m_bIsGenParam = true;
			this.m_typeInterfaces = new List<Type>();
			this.m_DeclaringType = declType;
			this.m_module = declType.GetModuleBuilder();
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00198788 File Offset: 0x00197988
		internal TypeBuilder(string fullname, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (fullname.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "fullname");
			}
			if (fullname[0] == '\0')
			{
				throw new ArgumentException(SR.Argument_IllegalName, "fullname");
			}
			if (fullname.Length > 1023)
			{
				throw new ArgumentException(SR.Argument_TypeNameTooLong, "fullname");
			}
			this.m_module = module;
			this.m_DeclaringType = enclosingType;
			AssemblyBuilder containingAssemblyBuilder = this.m_module.ContainingAssemblyBuilder;
			containingAssemblyBuilder._assemblyData.CheckTypeNameConflict(fullname, enclosingType);
			if (enclosingType != null && ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic))
			{
				throw new ArgumentException(SR.Argument_BadNestedTypeFlags, "attr");
			}
			int[] array = null;
			if (interfaces != null)
			{
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (interfaces[i] == null)
					{
						throw new ArgumentNullException("interfaces");
					}
				}
				array = new int[interfaces.Length + 1];
				for (int i = 0; i < interfaces.Length; i++)
				{
					array[i] = this.m_module.GetTypeTokenInternal(interfaces[i]).Token;
				}
			}
			int num = fullname.LastIndexOf('.');
			if (num == -1 || num == 0)
			{
				this.m_strNameSpace = string.Empty;
				this.m_strName = fullname;
			}
			else
			{
				this.m_strNameSpace = fullname.Substring(0, num);
				this.m_strName = fullname.Substring(num + 1);
			}
			this.VerifyTypeAttributes(attr);
			this.m_iAttr = attr;
			this.SetParent(parent);
			this.m_listMethods = new List<MethodBuilder>();
			this.m_lastTokenizedMethod = -1;
			this.SetInterfaces(interfaces);
			int tkParent = 0;
			if (this.m_typeParent != null)
			{
				tkParent = this.m_module.GetTypeTokenInternal(this.m_typeParent).Token;
			}
			int tkEnclosingType = 0;
			if (enclosingType != null)
			{
				tkEnclosingType = enclosingType.m_tdType.Token;
			}
			this.m_tdType = new TypeToken(TypeBuilder.DefineType(new QCallModule(ref module), fullname, tkParent, this.m_iAttr, tkEnclosingType, array));
			this.m_iPackingSize = iPackingSize;
			this.m_iTypeSize = iTypeSize;
			if (this.m_iPackingSize != PackingSize.Unspecified || this.m_iTypeSize != 0)
			{
				TypeBuilder.SetClassLayout(new QCallModule(ref module), this.m_tdType.Token, this.m_iPackingSize, this.m_iTypeSize);
			}
			this.m_module.AddType(this.FullName, this);
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x001989D8 File Offset: 0x00197BD8
		private FieldBuilder DefineDataHelper(string name, byte[] data, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			if (size <= 0 || size >= 4128768)
			{
				throw new ArgumentException(SR.Argument_BadSizeForData);
			}
			this.ThrowIfCreated();
			string text = "$ArrayType$" + size.ToString();
			Type type = this.m_module.FindTypeBuilderWithName(text, false);
			TypeBuilder typeBuilder = type as TypeBuilder;
			if (typeBuilder == null)
			{
				TypeAttributes attr = TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed;
				typeBuilder = this.m_module.DefineType(text, attr, typeof(ValueType), PackingSize.Size1, size);
				typeBuilder.CreateType();
			}
			FieldBuilder fieldBuilder = this.DefineField(name, typeBuilder, attributes | FieldAttributes.Static);
			fieldBuilder.SetData(data, size);
			return fieldBuilder;
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x00198A98 File Offset: 0x00197C98
		private void VerifyTypeAttributes(TypeAttributes attr)
		{
			if (this.DeclaringType == null)
			{
				if ((attr & TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.VisibilityMask) != TypeAttributes.Public)
				{
					throw new ArgumentException(SR.Argument_BadTypeAttrNestedVisibilityOnNonNestedType);
				}
			}
			else if ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
			{
				throw new ArgumentException(SR.Argument_BadTypeAttrNonNestedVisibilityNestedType);
			}
			if ((attr & TypeAttributes.LayoutMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.LayoutMask) != TypeAttributes.SequentialLayout && (attr & TypeAttributes.LayoutMask) != TypeAttributes.ExplicitLayout)
			{
				throw new ArgumentException(SR.Argument_BadTypeAttrInvalidLayout);
			}
			if ((attr & TypeAttributes.ReservedMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(SR.Argument_BadTypeAttrReservedBitsSet);
			}
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x00198B13 File Offset: 0x00197D13
		public bool IsCreated()
		{
			return this.m_hasBeenCreated;
		}

		// Token: 0x0600526C RID: 21100
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int DefineType(QCallModule module, string fullname, int tkParent, TypeAttributes attributes, int tkEnclosingType, int[] interfaceTokens);

		// Token: 0x0600526D RID: 21101
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int DefineGenericParam(QCallModule module, string name, int tkParent, GenericParameterAttributes attributes, int position, int[] constraints);

		// Token: 0x0600526E RID: 21102
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void TermCreateClass(QCallModule module, int tk, ObjectHandleOnStack type);

		// Token: 0x0600526F RID: 21103 RVA: 0x00198B1B File Offset: 0x00197D1B
		internal void ThrowIfCreated()
		{
			if (this.IsCreated())
			{
				throw new InvalidOperationException(SR.InvalidOperation_TypeHasBeenCreated);
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x00198B30 File Offset: 0x00197D30
		internal object SyncRoot
		{
			get
			{
				return this.m_module.SyncRoot;
			}
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00198B3D File Offset: 0x00197D3D
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.m_module;
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06005272 RID: 21106 RVA: 0x00198B45 File Offset: 0x00197D45
		internal RuntimeType BakedRuntimeType
		{
			get
			{
				return this.m_bakedRuntimeType;
			}
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00198B4D File Offset: 0x00197D4D
		internal void SetGenParamAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_genParamAttributes = genericParameterAttributes;
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00198B58 File Offset: 0x00197D58
		internal void SetGenParamCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			TypeBuilder.CustAttr genParamCustomAttributeNoLock = new TypeBuilder.CustAttr(con, binaryAttribute);
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetGenParamCustomAttributeNoLock(genParamCustomAttributeNoLock);
			}
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00198BA4 File Offset: 0x00197DA4
		internal void SetGenParamCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			TypeBuilder.CustAttr genParamCustomAttributeNoLock = new TypeBuilder.CustAttr(customBuilder);
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetGenParamCustomAttributeNoLock(genParamCustomAttributeNoLock);
			}
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00198BEC File Offset: 0x00197DEC
		private void SetGenParamCustomAttributeNoLock(TypeBuilder.CustAttr ca)
		{
			if (this.m_ca == null)
			{
				this.m_ca = new List<TypeBuilder.CustAttr>();
			}
			this.m_ca.Add(ca);
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00198175 File Offset: 0x00197375
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x00198C0D File Offset: 0x00197E0D
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this.m_DeclaringType;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x00198C0D File Offset: 0x00197E0D
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.m_DeclaringType;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600527A RID: 21114 RVA: 0x00198C15 File Offset: 0x00197E15
		public override string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600527B RID: 21115 RVA: 0x00198C1D File Offset: 0x00197E1D
		public override Module Module
		{
			get
			{
				return this.GetModuleBuilder();
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600527D RID: 21117 RVA: 0x00198C25 File Offset: 0x00197E25
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_tdType.Token;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x00198C32 File Offset: 0x00197E32
		public override Guid GUID
		{
			get
			{
				if (!this.IsCreated())
				{
					throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
				}
				return this.m_bakedRuntimeType.GUID;
			}
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00198C54 File Offset: 0x00197E54
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		[NullableContext(2)]
		public override object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] string[] namedParameters)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x00198C8C File Offset: 0x00197E8C
		public override Assembly Assembly
		{
			get
			{
				return this.m_module.Assembly;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x00198C9C File Offset: 0x00197E9C
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_DynamicModule);
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x00198CB4 File Offset: 0x00197EB4
		[Nullable(2)]
		public override string FullName
		{
			[NullableContext(2)]
			get
			{
				string result;
				if ((result = this.m_strFullQualName) == null)
				{
					result = (this.m_strFullQualName = TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName));
				}
				return result;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x00198CDB File Offset: 0x00197EDB
		[Nullable(2)]
		public override string Namespace
		{
			[NullableContext(2)]
			get
			{
				return this.m_strNameSpace;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06005284 RID: 21124 RVA: 0x0019816C File Offset: 0x0019736C
		[Nullable(2)]
		public override string AssemblyQualifiedName
		{
			[NullableContext(2)]
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06005285 RID: 21125 RVA: 0x00198CE3 File Offset: 0x00197EE3
		[Nullable(2)]
		public override Type BaseType
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeParent;
			}
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00198CEB File Offset: 0x00197EEB
		[NullableContext(2)]
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00198D12 File Offset: 0x00197F12
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetConstructors(bindingAttr);
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00198D33 File Offset: 0x00197F33
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		[NullableContext(2)]
		protected override MethodInfo GetMethodImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			if (types == null)
			{
				return this.m_bakedRuntimeType.GetMethod(name, bindingAttr);
			}
			return this.m_bakedRuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00198D6E File Offset: 0x00197F6E
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetMethods(bindingAttr);
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00198D8F File Offset: 0x00197F8F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		[return: Nullable(2)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetField(name, bindingAttr);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00198DB1 File Offset: 0x00197FB1
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetFields(bindingAttr);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00198DD2 File Offset: 0x00197FD2
		[return: Nullable(2)]
		public override Type GetInterface(string name, bool ignoreCase)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetInterface(name, ignoreCase);
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x00198DF4 File Offset: 0x00197FF4
		public override Type[] GetInterfaces()
		{
			if (this.m_bakedRuntimeType != null)
			{
				return this.m_bakedRuntimeType.GetInterfaces();
			}
			if (this.m_typeInterfaces == null)
			{
				return Array.Empty<Type>();
			}
			return this.m_typeInterfaces.ToArray();
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x00198E29 File Offset: 0x00198029
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		[return: Nullable(2)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetEvent(name, bindingAttr);
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x00198E4B File Offset: 0x0019804B
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public override EventInfo[] GetEvents()
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetEvents();
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		[NullableContext(2)]
		[return: Nullable(1)]
		protected override PropertyInfo GetPropertyImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00198E6B File Offset: 0x0019806B
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetProperties(bindingAttr);
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00198E8C File Offset: 0x0019808C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00198EAD File Offset: 0x001980AD
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		[return: Nullable(2)]
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x00198ECF File Offset: 0x001980CF
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x00198EF2 File Offset: 0x001980F2
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x00198F13 File Offset: 0x00198113
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetEvents(bindingAttr);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x00198F34 File Offset: 0x00198134
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return this.m_bakedRuntimeType.GetMembers(bindingAttr);
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x00198F58 File Offset: 0x00198158
		[NullableContext(2)]
		public override bool IsAssignableFrom([NotNullWhen(true)] Type c)
		{
			if (TypeBuilder.IsTypeEqual(c, this))
			{
				return true;
			}
			TypeBuilder typeBuilder = c as TypeBuilder;
			Type type;
			if (typeBuilder != null)
			{
				type = typeBuilder.m_bakedRuntimeType;
			}
			else
			{
				type = c;
			}
			if (type != null && type is RuntimeType)
			{
				return !(this.m_bakedRuntimeType == null) && this.m_bakedRuntimeType.IsAssignableFrom(type);
			}
			if (typeBuilder == null)
			{
				return false;
			}
			if (typeBuilder.IsSubclassOf(this))
			{
				return true;
			}
			if (!base.IsInterface)
			{
				return false;
			}
			Type[] interfaces = typeBuilder.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (TypeBuilder.IsTypeEqual(interfaces[i], this))
				{
					return true;
				}
				if (interfaces[i].IsSubclassOf(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x00199009 File Offset: 0x00198209
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_iAttr;
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsTypeDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00199011 File Offset: 0x00198211
		protected override bool IsCOMObjectImpl()
		{
			return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) != TypeAttributes.NotPublic;
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override Type GetElementType()
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00199024 File Offset: 0x00198224
		public override bool IsSubclassOf(Type c)
		{
			if (TypeBuilder.IsTypeEqual(this, c))
			{
				return false;
			}
			Type baseType = this.BaseType;
			while (baseType != null)
			{
				if (TypeBuilder.IsTypeEqual(baseType, c))
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x00199063 File Offset: 0x00198263
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.m_bakedRuntimeType != null)
				{
					return this.m_bakedRuntimeType;
				}
				if (!this.IsEnum)
				{
					return this;
				}
				if (this.m_enumUnderlyingType == null)
				{
					throw new InvalidOperationException(SR.InvalidOperation_NoUnderlyingTypeOnEnum);
				}
				return this.m_enumUnderlyingType;
			}
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00190BFD File Offset: 0x0018FDFD
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*", this, 0);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x00190C0B File Offset: 0x0018FE0B
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&", this, 0);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x00190C19 File Offset: 0x0018FE19
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]", this, 0);
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x00190C28 File Offset: 0x0018FE28
		public override Type MakeArrayType(int rank)
		{
			string rankString = TypeInfo.GetRankString(rank);
			return SymbolType.FormCompoundType(rankString, this, 0);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x001990A3 File Offset: 0x001982A3
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, typeof(object) as RuntimeType, inherit);
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x001990D4 File Offset: 0x001982D4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, runtimeType, inherit);
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0019913C File Offset: 0x0019833C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(SR.NotSupported_TypeNotYetCreated);
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this.m_bakedRuntimeType, runtimeType, inherit);
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x001991A2 File Offset: 0x001983A2
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return this.m_genParamAttributes;
			}
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x001991AA File Offset: 0x001983AA
		internal void SetInterfaces(params Type[] interfaces)
		{
			this.ThrowIfCreated();
			this.m_typeInterfaces = new List<Type>();
			if (interfaces != null)
			{
				this.m_typeInterfaces.AddRange(interfaces);
			}
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x001991CC File Offset: 0x001983CC
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
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i] == null)
				{
					throw new ArgumentNullException("names");
				}
			}
			if (this.m_inst != null)
			{
				throw new InvalidOperationException();
			}
			this.m_inst = new GenericTypeParameterBuilder[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				this.m_inst[j] = new GenericTypeParameterBuilder(new TypeBuilder(names[j], j, this));
			}
			return this.m_inst;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00199260 File Offset: 0x00198460
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			this.CheckContext(typeArguments);
			return TypeBuilderInstantiation.MakeGenericType(this, typeArguments);
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00199270 File Offset: 0x00198470
		public override Type[] GetGenericArguments()
		{
			Type[] inst = this.m_inst;
			return inst ?? Array.Empty<Type>();
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x0019928E File Offset: 0x0019848E
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.IsGenericType;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060052B5 RID: 21173 RVA: 0x00199296 File Offset: 0x00198496
		public override bool IsGenericType
		{
			get
			{
				return this.m_inst != null;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x001992A1 File Offset: 0x001984A1
		public override bool IsGenericParameter
		{
			get
			{
				return this.m_bIsGenParam;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060052B7 RID: 21175 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x001992A9 File Offset: 0x001984A9
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_genParamPos;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x001992B1 File Offset: 0x001984B1
		[Nullable(2)]
		public override MethodBase DeclaringMethod
		{
			[NullableContext(2)]
			get
			{
				return this.m_declMeth;
			}
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x001992B9 File Offset: 0x001984B9
		public override Type GetGenericTypeDefinition()
		{
			if (this.IsGenericTypeDefinition)
			{
				return this;
			}
			if (this.m_genTypeDef == null)
			{
				throw new InvalidOperationException();
			}
			return this.m_genTypeDef;
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x001992E0 File Offset: 0x001984E0
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineMethodOverrideNoLock(methodInfoBody, methodInfoDeclaration);
			}
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x00199324 File Offset: 0x00198524
		private void DefineMethodOverrideNoLock(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			if (methodInfoBody == null)
			{
				throw new ArgumentNullException("methodInfoBody");
			}
			if (methodInfoDeclaration == null)
			{
				throw new ArgumentNullException("methodInfoDeclaration");
			}
			this.ThrowIfCreated();
			if (methodInfoBody.DeclaringType != this)
			{
				throw new ArgumentException(SR.ArgumentException_BadMethodImplBody);
			}
			MethodToken methodTokenInternal = this.m_module.GetMethodTokenInternal(methodInfoBody);
			MethodToken methodTokenInternal2 = this.m_module.GetMethodTokenInternal(methodInfoDeclaration);
			ModuleBuilder module = this.m_module;
			TypeBuilder.DefineMethodImpl(new QCallModule(ref module), this.m_tdType.Token, methodTokenInternal.Token, methodTokenInternal2.Token);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x001993B9 File Offset: 0x001985B9
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x001993C7 File Offset: 0x001985C7
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, null, null);
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x001993D4 File Offset: 0x001985D4
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			return this.DefineMethod(name, attributes, callingConvention, null, null);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x001993E4 File Offset: 0x001985E4
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x00199404 File Offset: 0x00198604
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
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
			object syncRoot = this.SyncRoot;
			MethodBuilder result;
			lock (syncRoot)
			{
				result = this.DefineMethodNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			}
			return result;
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00199458 File Offset: 0x00198658
		private MethodBuilder DefineMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
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
			if (parameterTypes != null)
			{
				if (parameterTypeOptionalCustomModifiers != null && parameterTypeOptionalCustomModifiers.Length != parameterTypes.Length)
				{
					throw new ArgumentException(SR.Format(SR.Argument_MismatchedArrays, "parameterTypeOptionalCustomModifiers", "parameterTypes"));
				}
				if (parameterTypeRequiredCustomModifiers != null && parameterTypeRequiredCustomModifiers.Length != parameterTypes.Length)
				{
					throw new ArgumentException(SR.Format(SR.Argument_MismatchedArrays, "parameterTypeRequiredCustomModifiers", "parameterTypes"));
				}
			}
			this.ThrowIfCreated();
			MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this);
			if (!this.m_isHiddenGlobalType && (methodBuilder.Attributes & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope && methodBuilder.Name.Equals(ConstructorInfo.ConstructorName))
			{
				this.m_constructorCount++;
			}
			this.m_listMethods.Add(methodBuilder);
			return methodBuilder;
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00199580 File Offset: 0x00198780
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethodHelper(name, dllName, name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x001995A8 File Offset: 0x001987A8
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x001995D0 File Offset: 0x001987D0
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, [Nullable(2)] Type returnType, [Nullable(new byte[]
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
		})] Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x001995FC File Offset: 0x001987FC
		private MethodBuilder DefinePInvokeMethodHelper(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
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
			object syncRoot = this.SyncRoot;
			MethodBuilder result;
			lock (syncRoot)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				if (name.Length == 0)
				{
					throw new ArgumentException(SR.Argument_EmptyName, "name");
				}
				if (dllName == null)
				{
					throw new ArgumentNullException("dllName");
				}
				if (dllName.Length == 0)
				{
					throw new ArgumentException(SR.Argument_EmptyName, "dllName");
				}
				if (importName == null)
				{
					throw new ArgumentNullException("importName");
				}
				if (importName.Length == 0)
				{
					throw new ArgumentException(SR.Argument_EmptyName, "importName");
				}
				if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
				{
					throw new ArgumentException(SR.Argument_BadPInvokeMethod);
				}
				if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
				{
					throw new ArgumentException(SR.Argument_BadPInvokeOnInterface);
				}
				this.ThrowIfCreated();
				attributes |= MethodAttributes.PinvokeImpl;
				MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this);
				int num;
				methodBuilder.GetMethodSignature().InternalGetSignature(out num);
				if (this.m_listMethods.Contains(methodBuilder))
				{
					throw new ArgumentException(SR.Argument_MethodRedefined);
				}
				this.m_listMethods.Add(methodBuilder);
				MethodToken token = methodBuilder.GetToken();
				int num2 = 0;
				switch (nativeCallConv)
				{
				case CallingConvention.Winapi:
					num2 = 256;
					break;
				case CallingConvention.Cdecl:
					num2 = 512;
					break;
				case CallingConvention.StdCall:
					num2 = 768;
					break;
				case CallingConvention.ThisCall:
					num2 = 1024;
					break;
				case CallingConvention.FastCall:
					num2 = 1280;
					break;
				}
				switch (nativeCharSet)
				{
				case CharSet.None:
					num2 |= 0;
					break;
				case CharSet.Ansi:
					num2 |= 2;
					break;
				case CharSet.Unicode:
					num2 |= 4;
					break;
				case CharSet.Auto:
					num2 |= 6;
					break;
				}
				ModuleBuilder module = this.m_module;
				TypeBuilder.SetPInvokeData(new QCallModule(ref module), dllName, importName, token.Token, num2);
				methodBuilder.SetToken(token);
				result = methodBuilder;
			}
			return result;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00199838 File Offset: 0x00198A38
		public ConstructorBuilder DefineTypeInitializer()
		{
			object syncRoot = this.SyncRoot;
			ConstructorBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeInitializerNoLock();
			}
			return result;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0019987C File Offset: 0x00198A7C
		private ConstructorBuilder DefineTypeInitializerNoLock()
		{
			this.ThrowIfCreated();
			return new ConstructorBuilder(ConstructorInfo.TypeConstructorName, MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName, CallingConventions.Standard, null, this.m_module, this);
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x001998AC File Offset: 0x00198AAC
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ConstructorNotAllowedOnInterface);
			}
			object syncRoot = this.SyncRoot;
			ConstructorBuilder result;
			lock (syncRoot)
			{
				result = this.DefineDefaultConstructorNoLock(attributes);
			}
			return result;
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00199908 File Offset: 0x00198B08
		private ConstructorBuilder DefineDefaultConstructorNoLock(MethodAttributes attributes)
		{
			ConstructorInfo constructorInfo = null;
			if (this.m_typeParent is TypeBuilderInstantiation)
			{
				Type type = this.m_typeParent.GetGenericTypeDefinition();
				if (type is TypeBuilder)
				{
					type = ((TypeBuilder)type).m_bakedRuntimeType;
				}
				if (type == null)
				{
					throw new NotSupportedException(SR.NotSupported_DynamicModule);
				}
				Type type2 = type.MakeGenericType(this.m_typeParent.GetGenericArguments());
				if (type2 is TypeBuilderInstantiation)
				{
					constructorInfo = TypeBuilder.GetConstructor(type2, type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null));
				}
				else
				{
					constructorInfo = type2.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
				}
			}
			if (constructorInfo == null)
			{
				constructorInfo = this.m_typeParent.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			}
			if (constructorInfo == null)
			{
				throw new NotSupportedException(SR.NotSupported_NoParentDefaultConstructor);
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, null);
			this.m_constructorCount++;
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructorInfo);
			ilgenerator.Emit(OpCodes.Ret);
			constructorBuilder.m_isDefaultConstructor = true;
			return constructorBuilder;
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00199A19 File Offset: 0x00198C19
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x00199A28 File Offset: 0x00198C28
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] requiredCustomModifiers, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] optionalCustomModifiers)
		{
			if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & MethodAttributes.Static) != MethodAttributes.Static)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ConstructorNotAllowedOnInterface);
			}
			object syncRoot = this.SyncRoot;
			ConstructorBuilder result;
			lock (syncRoot)
			{
				result = this.DefineConstructorNoLock(attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			}
			return result;
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00199A94 File Offset: 0x00198C94
		private ConstructorBuilder DefineConstructorNoLock(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.CheckContext(parameterTypes);
			this.CheckContext(requiredCustomModifiers);
			this.CheckContext(optionalCustomModifiers);
			this.ThrowIfCreated();
			string name;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				name = ConstructorInfo.ConstructorName;
			}
			else
			{
				name = ConstructorInfo.TypeConstructorName;
			}
			attributes |= MethodAttributes.SpecialName;
			ConstructorBuilder result = new ConstructorBuilder(name, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, this.m_module, this);
			this.m_constructorCount++;
			return result;
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00199B00 File Offset: 0x00198D00
		public TypeBuilder DefineNestedType(string name)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineNestedTypeNoLock(name, TypeAttributes.NestedPrivate, null, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00199B48 File Offset: 0x00198D48
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, [Nullable(2)] [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] interfaces)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				this.CheckContext(new Type[]
				{
					parent
				});
				this.CheckContext(interfaces);
				result = this.DefineNestedTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00199BAC File Offset: 0x00198DAC
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, [Nullable(2)] [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineNestedTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00199BF4 File Offset: 0x00198DF4
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineNestedTypeNoLock(name, attr, null, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x00199C3C File Offset: 0x00198E3C
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, [Nullable(2)] [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, int typeSize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineNestedTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, typeSize);
			}
			return result;
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x00199C88 File Offset: 0x00198E88
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] [Nullable(2)] Type parent, PackingSize packSize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineNestedTypeNoLock(name, attr, parent, null, packSize, 0);
			}
			return result;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00199CD4 File Offset: 0x00198ED4
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] [Nullable(2)] Type parent, PackingSize packSize, int typeSize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineNestedTypeNoLock(name, attr, parent, null, packSize, typeSize);
			}
			return result;
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x00199D20 File Offset: 0x00198F20
		private TypeBuilder DefineNestedTypeNoLock(string name, TypeAttributes attr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
		{
			return new TypeBuilder(name, attr, parent, interfaces, this.m_module, packSize, typeSize, this);
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00199D37 File Offset: 0x00198F37
		public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
		{
			return this.DefineField(fieldName, type, null, null, attributes);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00199D44 File Offset: 0x00198F44
		public FieldBuilder DefineField(string fieldName, Type type, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] requiredCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder result;
			lock (syncRoot)
			{
				result = this.DefineFieldNoLock(fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
			}
			return result;
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00199D90 File Offset: 0x00198F90
		private FieldBuilder DefineFieldNoLock(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			this.ThrowIfCreated();
			this.CheckContext(new Type[]
			{
				type
			});
			this.CheckContext(requiredCustomModifiers);
			if (this.m_enumUnderlyingType == null && this.IsEnum && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
			{
				this.m_enumUnderlyingType = type;
			}
			return new FieldBuilder(this, fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x00199DEC File Offset: 0x00198FEC
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

		// Token: 0x060052DA RID: 21210 RVA: 0x00199E34 File Offset: 0x00199034
		private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.DefineDataHelper(name, data, data.Length, attributes);
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00199E50 File Offset: 0x00199050
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

		// Token: 0x060052DC RID: 21212 RVA: 0x00199E98 File Offset: 0x00199098
		private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
		{
			return this.DefineDataHelper(name, null, size, attributes);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00199EA4 File Offset: 0x001990A4
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00199EC0 File Offset: 0x001990C0
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00199EE0 File Offset: 0x001990E0
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, [Nullable(new byte[]
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
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x00199F04 File Offset: 0x00199104
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, [Nullable(new byte[]
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
			object syncRoot = this.SyncRoot;
			PropertyBuilder result;
			lock (syncRoot)
			{
				result = this.DefinePropertyNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			}
			return result;
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00199F58 File Offset: 0x00199158
		private PropertyBuilder DefinePropertyNoLock(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
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
			this.ThrowIfCreated();
			SignatureHelper propertySigHelper = SignatureHelper.GetPropertySigHelper(this.m_module, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			int sigLength;
			byte[] signature = propertySigHelper.InternalGetSignature(out sigLength);
			ModuleBuilder module = this.m_module;
			PropertyToken prToken = new PropertyToken(TypeBuilder.DefineProperty(new QCallModule(ref module), this.m_tdType.Token, name, attributes, signature, sigLength));
			return new PropertyBuilder(this.m_module, name, propertySigHelper, attributes, returnType, prToken, this);
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0019A02C File Offset: 0x0019922C
		public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
		{
			object syncRoot = this.SyncRoot;
			EventBuilder result;
			lock (syncRoot)
			{
				result = this.DefineEventNoLock(name, attributes, eventtype);
			}
			return result;
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0019A074 File Offset: 0x00199274
		private EventBuilder DefineEventNoLock(string name, EventAttributes attributes, Type eventtype)
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
			this.CheckContext(new Type[]
			{
				eventtype
			});
			this.ThrowIfCreated();
			int token = this.m_module.GetTypeTokenInternal(eventtype).Token;
			ModuleBuilder module = this.m_module;
			EventToken evToken = new EventToken(TypeBuilder.DefineEvent(new QCallModule(ref module), this.m_tdType.Token, name, attributes, token));
			return new EventBuilder(this.m_module, name, attributes, this, evToken);
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0019A124 File Offset: 0x00199324
		[NullableContext(2)]
		public TypeInfo CreateTypeInfo()
		{
			object syncRoot = this.SyncRoot;
			TypeInfo result;
			lock (syncRoot)
			{
				result = this.CreateTypeNoLock();
			}
			return result;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0019A168 File Offset: 0x00199368
		[NullableContext(2)]
		public Type CreateType()
		{
			object syncRoot = this.SyncRoot;
			Type result;
			lock (syncRoot)
			{
				result = this.CreateTypeNoLock();
			}
			return result;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0019A1AC File Offset: 0x001993AC
		internal void CheckContext(params Type[][] typess)
		{
			this.m_module.CheckContext(typess);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0019A1BA File Offset: 0x001993BA
		internal void CheckContext(params Type[] types)
		{
			this.m_module.CheckContext(types);
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0019A1C8 File Offset: 0x001993C8
		private TypeInfo CreateTypeNoLock()
		{
			if (this.IsCreated())
			{
				return this.m_bakedRuntimeType;
			}
			if (this.m_typeInterfaces == null)
			{
				this.m_typeInterfaces = new List<Type>();
			}
			int[] array = new int[this.m_typeInterfaces.Count];
			for (int i = 0; i < this.m_typeInterfaces.Count; i++)
			{
				array[i] = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[i]).Token;
			}
			int num = 0;
			if (this.m_typeParent != null)
			{
				num = this.m_module.GetTypeTokenInternal(this.m_typeParent).Token;
			}
			ModuleBuilder module = this.m_module;
			if (this.IsGenericParameter)
			{
				int[] array2;
				if (this.m_typeParent != null)
				{
					array2 = new int[this.m_typeInterfaces.Count + 2];
					int[] array3 = array2;
					array3[array3.Length - 2] = num;
				}
				else
				{
					array2 = new int[this.m_typeInterfaces.Count + 1];
				}
				for (int j = 0; j < this.m_typeInterfaces.Count; j++)
				{
					array2[j] = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[j]).Token;
				}
				int tkParent = (this.m_declMeth == null) ? this.m_DeclaringType.m_tdType.Token : this.m_declMeth.GetToken().Token;
				this.m_tdType = new TypeToken(TypeBuilder.DefineGenericParam(new QCallModule(ref module), this.m_strName, tkParent, this.m_genParamAttributes, this.m_genParamPos, array2));
				if (this.m_ca != null)
				{
					foreach (TypeBuilder.CustAttr custAttr in this.m_ca)
					{
						custAttr.Bake(this.m_module, this.MetadataTokenInternal);
					}
				}
				this.m_hasBeenCreated = true;
				return this;
			}
			if ((this.m_tdType.Token & 16777215) != 0 && (num & 16777215) != 0)
			{
				TypeBuilder.SetParentType(new QCallModule(ref module), this.m_tdType.Token, num);
			}
			if (this.m_inst != null)
			{
				foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in this.m_inst)
				{
					genericTypeParameterBuilder.m_type.CreateType();
				}
			}
			if (!this.m_isHiddenGlobalType && this.m_constructorCount == 0 && (this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !base.IsValueType && (this.m_iAttr & (TypeAttributes.Abstract | TypeAttributes.Sealed)) != (TypeAttributes.Abstract | TypeAttributes.Sealed))
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			int count = this.m_listMethods.Count;
			for (int l = 0; l < count; l++)
			{
				MethodBuilder methodBuilder = this.m_listMethods[l];
				if (methodBuilder.IsGenericMethodDefinition)
				{
					methodBuilder.GetToken();
				}
				MethodAttributes attributes = methodBuilder.Attributes;
				if ((methodBuilder.GetMethodImplementationFlags() & (MethodImplAttributes)135) == MethodImplAttributes.IL && (attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
				{
					int sigLength;
					byte[] localSignature = methodBuilder.GetLocalSignature(out sigLength);
					if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope && (this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException(SR.InvalidOperation_BadTypeAttributesNotAbstract);
					}
					byte[] body = methodBuilder.GetBody();
					if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
					{
						if (body != null)
						{
							throw new InvalidOperationException(SR.Format(SR.InvalidOperation_BadMethodBody, methodBuilder.Name));
						}
					}
					else if (body == null || body.Length == 0)
					{
						if (methodBuilder.m_ilGenerator != null)
						{
							methodBuilder.CreateMethodBodyHelper(methodBuilder.GetILGenerator());
						}
						body = methodBuilder.GetBody();
						if ((body == null || body.Length == 0) && !methodBuilder.m_canBeRuntimeImpl)
						{
							throw new InvalidOperationException(SR.Format(SR.InvalidOperation_BadEmptyMethodBody, methodBuilder.Name));
						}
					}
					int maxStack = methodBuilder.GetMaxStack();
					ExceptionHandler[] exceptionHandlers = methodBuilder.GetExceptionHandlers();
					int[] tokenFixups = methodBuilder.GetTokenFixups();
					TypeBuilder.SetMethodIL(new QCallModule(ref module), methodBuilder.GetToken().Token, methodBuilder.InitLocals, body, (body != null) ? body.Length : 0, localSignature, sigLength, maxStack, exceptionHandlers, (exceptionHandlers != null) ? exceptionHandlers.Length : 0, tokenFixups, (tokenFixups != null) ? tokenFixups.Length : 0);
					if (this.m_module.ContainingAssemblyBuilder._assemblyData._access == AssemblyBuilderAccess.Run)
					{
						methodBuilder.ReleaseBakedStructures();
					}
				}
			}
			this.m_hasBeenCreated = true;
			RuntimeType runtimeType = null;
			TypeBuilder.TermCreateClass(new QCallModule(ref module), this.m_tdType.Token, ObjectHandleOnStack.Create<RuntimeType>(ref runtimeType));
			if (!this.m_isHiddenGlobalType)
			{
				this.m_bakedRuntimeType = runtimeType;
				if (this.m_DeclaringType != null && this.m_DeclaringType.m_bakedRuntimeType != null)
				{
					this.m_DeclaringType.m_bakedRuntimeType.InvalidateCachedNestedType();
				}
				return runtimeType;
			}
			return null;
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060052E9 RID: 21225 RVA: 0x0019A688 File Offset: 0x00199888
		public int Size
		{
			get
			{
				return this.m_iTypeSize;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060052EA RID: 21226 RVA: 0x0019A690 File Offset: 0x00199890
		public PackingSize PackingSize
		{
			get
			{
				return this.m_iPackingSize;
			}
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0019A698 File Offset: 0x00199898
		[NullableContext(2)]
		public void SetParent([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type parent)
		{
			this.ThrowIfCreated();
			if (parent != null)
			{
				this.CheckContext(new Type[]
				{
					parent
				});
				if (parent.IsInterface)
				{
					throw new ArgumentException(SR.Argument_CannotSetParentToInterface);
				}
				this.m_typeParent = parent;
				return;
			}
			else
			{
				if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.ClassSemanticsMask)
				{
					this.m_typeParent = typeof(object);
					return;
				}
				if ((this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
				{
					throw new InvalidOperationException(SR.InvalidOperation_BadInterfaceNotAbstract);
				}
				this.m_typeParent = null;
				return;
			}
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0019A720 File Offset: 0x00199920
		public void AddInterfaceImplementation([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			this.CheckContext(new Type[]
			{
				interfaceType
			});
			this.ThrowIfCreated();
			TypeToken typeTokenInternal = this.m_module.GetTypeTokenInternal(interfaceType);
			ModuleBuilder module = this.m_module;
			TypeBuilder.AddInterfaceImpl(new QCallModule(ref module), this.m_tdType.Token, typeTokenInternal.Token);
			this.m_typeInterfaces.Add(interfaceType);
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x0019A795 File Offset: 0x00199995
		public TypeToken TypeToken
		{
			get
			{
				if (this.IsGenericParameter)
				{
					this.ThrowIfCreated();
				}
				return this.m_tdType;
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0019A7AC File Offset: 0x001999AC
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
			TypeBuilder.DefineCustomAttribute(this.m_module, this.m_tdType.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0019A808 File Offset: 0x00199A08
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute(this.m_module, this.m_tdType.Token);
		}

		// Token: 0x04001517 RID: 5399
		public const int UnspecifiedTypeSize = 0;

		// Token: 0x04001518 RID: 5400
		private List<TypeBuilder.CustAttr> m_ca;

		// Token: 0x04001519 RID: 5401
		private TypeToken m_tdType;

		// Token: 0x0400151A RID: 5402
		private readonly ModuleBuilder m_module;

		// Token: 0x0400151B RID: 5403
		private readonly string m_strName;

		// Token: 0x0400151C RID: 5404
		private readonly string m_strNameSpace;

		// Token: 0x0400151D RID: 5405
		private string m_strFullQualName;

		// Token: 0x0400151E RID: 5406
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		private Type m_typeParent;

		// Token: 0x0400151F RID: 5407
		private List<Type> m_typeInterfaces;

		// Token: 0x04001520 RID: 5408
		private readonly TypeAttributes m_iAttr;

		// Token: 0x04001521 RID: 5409
		private GenericParameterAttributes m_genParamAttributes;

		// Token: 0x04001522 RID: 5410
		internal List<MethodBuilder> m_listMethods;

		// Token: 0x04001523 RID: 5411
		internal int m_lastTokenizedMethod;

		// Token: 0x04001524 RID: 5412
		private int m_constructorCount;

		// Token: 0x04001525 RID: 5413
		private readonly int m_iTypeSize;

		// Token: 0x04001526 RID: 5414
		private readonly PackingSize m_iPackingSize;

		// Token: 0x04001527 RID: 5415
		private readonly TypeBuilder m_DeclaringType;

		// Token: 0x04001528 RID: 5416
		private Type m_enumUnderlyingType;

		// Token: 0x04001529 RID: 5417
		internal bool m_isHiddenGlobalType;

		// Token: 0x0400152A RID: 5418
		private bool m_hasBeenCreated;

		// Token: 0x0400152B RID: 5419
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		private RuntimeType m_bakedRuntimeType;

		// Token: 0x0400152C RID: 5420
		private readonly int m_genParamPos;

		// Token: 0x0400152D RID: 5421
		private GenericTypeParameterBuilder[] m_inst;

		// Token: 0x0400152E RID: 5422
		private readonly bool m_bIsGenParam;

		// Token: 0x0400152F RID: 5423
		private readonly MethodBuilder m_declMeth;

		// Token: 0x04001530 RID: 5424
		private readonly TypeBuilder m_genTypeDef;

		// Token: 0x02000655 RID: 1621
		private class CustAttr
		{
			// Token: 0x060052F0 RID: 21232 RVA: 0x0019A82F File Offset: 0x00199A2F
			public CustAttr(ConstructorInfo con, byte[] binaryAttribute)
			{
				if (con == null)
				{
					throw new ArgumentNullException("con");
				}
				if (binaryAttribute == null)
				{
					throw new ArgumentNullException("binaryAttribute");
				}
				this.m_con = con;
				this.m_binaryAttribute = binaryAttribute;
			}

			// Token: 0x060052F1 RID: 21233 RVA: 0x0019A861 File Offset: 0x00199A61
			public CustAttr(CustomAttributeBuilder customBuilder)
			{
				if (customBuilder == null)
				{
					throw new ArgumentNullException("customBuilder");
				}
				this.m_customBuilder = customBuilder;
			}

			// Token: 0x060052F2 RID: 21234 RVA: 0x0019A880 File Offset: 0x00199A80
			public void Bake(ModuleBuilder module, int token)
			{
				if (this.m_customBuilder == null)
				{
					TypeBuilder.DefineCustomAttribute(module, token, module.GetConstructorToken(this.m_con).Token, this.m_binaryAttribute, false, false);
					return;
				}
				this.m_customBuilder.CreateCustomAttribute(module, token);
			}

			// Token: 0x04001531 RID: 5425
			private readonly ConstructorInfo m_con;

			// Token: 0x04001532 RID: 5426
			private readonly byte[] m_binaryAttribute;

			// Token: 0x04001533 RID: 5427
			private readonly CustomAttributeBuilder m_customBuilder;
		}
	}
}
