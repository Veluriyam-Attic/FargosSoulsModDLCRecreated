using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000622 RID: 1570
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EnumBuilder : TypeInfo
	{
		// Token: 0x06004F83 RID: 20355 RVA: 0x000BC768 File Offset: 0x000BB968
		[NullableContext(2)]
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0019095C File Offset: 0x0018FB5C
		public FieldBuilder DefineLiteral(string literalName, [Nullable(2)] object literalValue)
		{
			FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x00190981 File Offset: 0x0018FB81
		[NullableContext(2)]
		public TypeInfo CreateTypeInfo()
		{
			return this.m_typeBuilder.CreateTypeInfo();
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x0019098E File Offset: 0x0018FB8E
		[NullableContext(2)]
		public Type CreateType()
		{
			return this.m_typeBuilder.CreateType();
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x0019099B File Offset: 0x0018FB9B
		public TypeToken TypeToken
		{
			get
			{
				return this.m_typeBuilder.TypeToken;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x001909A8 File Offset: 0x0018FBA8
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this.m_underlyingField;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x001909B0 File Offset: 0x0018FBB0
		public override string Name
		{
			get
			{
				return this.m_typeBuilder.Name;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x001909BD File Offset: 0x0018FBBD
		public override Guid GUID
		{
			get
			{
				return this.m_typeBuilder.GUID;
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x001909CC File Offset: 0x0018FBCC
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] string[] namedParameters)
		{
			return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x001909F1 File Offset: 0x0018FBF1
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x001909FE File Offset: 0x0018FBFE
		public override Assembly Assembly
		{
			get
			{
				return this.m_typeBuilder.Assembly;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x00190A0B File Offset: 0x0018FC0B
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.m_typeBuilder.TypeHandle;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x00190A18 File Offset: 0x0018FC18
		[Nullable(2)]
		public override string FullName
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeBuilder.FullName;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x00190A25 File Offset: 0x0018FC25
		[Nullable(2)]
		public override string AssemblyQualifiedName
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeBuilder.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x00190A32 File Offset: 0x0018FC32
		[Nullable(2)]
		public override string Namespace
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeBuilder.Namespace;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x00190A3F File Offset: 0x0018FC3F
		[Nullable(2)]
		public override Type BaseType
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeBuilder.BaseType;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x00190A4C File Offset: 0x0018FC4C
		[NullableContext(2)]
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x00190A60 File Offset: 0x0018FC60
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetConstructors(bindingAttr);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00190A6E File Offset: 0x0018FC6E
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		[NullableContext(2)]
		protected override MethodInfo GetMethodImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.m_typeBuilder.GetMethod(name, bindingAttr);
			}
			return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00190A96 File Offset: 0x0018FC96
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMethods(bindingAttr);
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00190AA4 File Offset: 0x0018FCA4
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		[return: Nullable(2)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetField(name, bindingAttr);
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x00190AB3 File Offset: 0x0018FCB3
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetFields(bindingAttr);
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00190AC1 File Offset: 0x0018FCC1
		[return: Nullable(2)]
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.m_typeBuilder.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x00190AD0 File Offset: 0x0018FCD0
		public override Type[] GetInterfaces()
		{
			return this.m_typeBuilder.GetInterfaces();
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00190ADD File Offset: 0x0018FCDD
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		[return: Nullable(2)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00190AEC File Offset: 0x0018FCEC
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public override EventInfo[] GetEvents()
		{
			return this.m_typeBuilder.GetEvents();
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0018DD92 File Offset: 0x0018CF92
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

		// Token: 0x06004F9F RID: 20383 RVA: 0x00190AF9 File Offset: 0x0018FCF9
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetProperties(bindingAttr);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00190B07 File Offset: 0x0018FD07
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00190B15 File Offset: 0x0018FD15
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		[return: Nullable(2)]
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00190B24 File Offset: 0x0018FD24
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00190B34 File Offset: 0x0018FD34
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMembers(bindingAttr);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00190B42 File Offset: 0x0018FD42
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.m_typeBuilder.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00190B50 File Offset: 0x0018FD50
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvents(bindingAttr);
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x00190B5E File Offset: 0x0018FD5E
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_typeBuilder.Attributes;
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsTypeDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x000AC09E File Offset: 0x000AB29E
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004FAF RID: 20399 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x00190B6B File Offset: 0x0018FD6B
		[NullableContext(2)]
		public override Type GetElementType()
		{
			return this.m_typeBuilder.GetElementType();
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00190B78 File Offset: 0x0018FD78
		protected override bool HasElementTypeImpl()
		{
			return this.m_typeBuilder.HasElementType;
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00190B85 File Offset: 0x0018FD85
		public override Type GetEnumUnderlyingType()
		{
			return this.m_underlyingField.FieldType;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004FB3 RID: 20403 RVA: 0x00190B92 File Offset: 0x0018FD92
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.GetEnumUnderlyingType();
			}
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00190B9A File Offset: 0x0018FD9A
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00190BA8 File Offset: 0x0018FDA8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00190BB7 File Offset: 0x0018FDB7
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x00190BC6 File Offset: 0x0018FDC6
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_typeBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x00190BD4 File Offset: 0x0018FDD4
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeBuilder.DeclaringType;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x00190BE1 File Offset: 0x0018FDE1
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.m_typeBuilder.ReflectedType;
			}
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x00190BEE File Offset: 0x0018FDEE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x00190BFD File Offset: 0x0018FDFD
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*", this, 0);
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x00190C0B File Offset: 0x0018FE0B
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&", this, 0);
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x00190C19 File Offset: 0x0018FE19
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]", this, 0);
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x00190C28 File Offset: 0x0018FE28
		public override Type MakeArrayType(int rank)
		{
			string rankString = TypeInfo.GetRankString(rank);
			return SymbolType.FormCompoundType(rankString, this, 0);
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00190C44 File Offset: 0x0018FE44
		internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, ModuleBuilder module)
		{
			if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(SR.Argument_ShouldOnlySetVisibilityFlags, "name");
			}
			this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof(Enum), null, module, PackingSize.Unspecified, 0, null);
			this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
		}

		// Token: 0x04001451 RID: 5201
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		internal TypeBuilder m_typeBuilder;

		// Token: 0x04001452 RID: 5202
		private FieldBuilder m_underlyingField;
	}
}
