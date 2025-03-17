using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000625 RID: 1573
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		// Token: 0x06004FDD RID: 20445 RVA: 0x000BC768 File Offset: 0x000BB968
		[NullableContext(2)]
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0019112D File Offset: 0x0019032D
		internal GenericTypeParameterBuilder(TypeBuilder type)
		{
			this.m_type = type;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0019113C File Offset: 0x0019033C
		public override string ToString()
		{
			return this.m_type.Name;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0019114C File Offset: 0x0019034C
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			GenericTypeParameterBuilder genericTypeParameterBuilder = o as GenericTypeParameterBuilder;
			return !(genericTypeParameterBuilder == null) && genericTypeParameterBuilder.m_type == this.m_type;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x00191179 File Offset: 0x00190379
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x00191186 File Offset: 0x00190386
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x00191193 File Offset: 0x00190393
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x0019113C File Offset: 0x0019033C
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004FE5 RID: 20453 RVA: 0x001911A0 File Offset: 0x001903A0
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x001911AD File Offset: 0x001903AD
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_type.MetadataTokenInternal;
			}
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x00190BFD File Offset: 0x0018FDFD
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*", this, 0);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x00190C0B File Offset: 0x0018FE0B
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&", this, 0);
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x00190C19 File Offset: 0x0018FE19
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]", this, 0);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x001911BC File Offset: 0x001903BC
		public override Type MakeArrayType(int rank)
		{
			string rankString = TypeInfo.GetRankString(rank);
			return SymbolType.FormCompoundType(rankString, this, 0) as SymbolType;
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x001911E0 File Offset: 0x001903E0
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		[NullableContext(2)]
		[return: Nullable(1)]
		public override object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x001911F2 File Offset: 0x001903F2
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x000C2C3C File Offset: 0x000C1E3C
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public override string FullName
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public override string Namespace
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public override string AssemblyQualifiedName
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004FF3 RID: 20467 RVA: 0x001911FF File Offset: 0x001903FF
		[Nullable(2)]
		public override Type BaseType
		{
			[NullableContext(2)]
			get
			{
				return this.m_type.BaseType;
			}
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, [Nullable(2)] Binder binder, CallingConventions callConvention, Type[] types, [Nullable(2)] ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, [Nullable(2)] Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, [Nullable(2)] ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x000C279F File Offset: 0x000C199F
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x000C279F File Offset: 0x000C199F
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x000C279F File Offset: 0x000C199F
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		[return: Nullable(1)]
		protected override PropertyInfo GetPropertyImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x000C279F File Offset: 0x000C199F
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x000AC09E File Offset: 0x000AB29E
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06005007 RID: 20487 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06005008 RID: 20488 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x000C279F File Offset: 0x000C199F
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06005010 RID: 20496 RVA: 0x000AC098 File Offset: 0x000AB298
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06005012 RID: 20498 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06005014 RID: 20500 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06005015 RID: 20501 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06005016 RID: 20502 RVA: 0x0019120C File Offset: 0x0019040C
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_type.GenericParameterPosition;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x00191219 File Offset: 0x00190419
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_type.ContainsGenericParameters;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x00191226 File Offset: 0x00190426
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return this.m_type.GenericParameterAttributes;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x00191233 File Offset: 0x00190433
		[Nullable(2)]
		public override MethodBase DeclaringMethod
		{
			[NullableContext(2)]
			get
			{
				return this.m_type.DeclaringMethod;
			}
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00191240 File Offset: 0x00190440
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(SR.Format(SR.Arg_NotGenericTypeDefinition, this));
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x000C279F File Offset: 0x000C199F
		[NullableContext(2)]
		public override bool IsAssignableFrom([NotNullWhen(true)] Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x000C279F File Offset: 0x000C199F
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x000C279F File Offset: 0x000C199F
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x000C279F File Offset: 0x000C199F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x000C279F File Offset: 0x000C199F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00191252 File Offset: 0x00190452
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_type.SetGenParamCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00191261 File Offset: 0x00190461
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_type.SetGenParamCustomAttribute(customBuilder);
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0019126F File Offset: 0x0019046F
		[NullableContext(2)]
		public void SetBaseTypeConstraint([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type baseTypeConstraint)
		{
			this.m_type.CheckContext(new Type[]
			{
				baseTypeConstraint
			});
			this.m_type.SetParent(baseTypeConstraint);
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x00191292 File Offset: 0x00190492
		public void SetInterfaceConstraints([Nullable(new byte[]
		{
			2,
			1
		})] params Type[] interfaceConstraints)
		{
			this.m_type.CheckContext(interfaceConstraints);
			this.m_type.SetInterfaces(interfaceConstraints);
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x001912AC File Offset: 0x001904AC
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_type.SetGenParamAttributes(genericParameterAttributes);
		}

		// Token: 0x0400145E RID: 5214
		internal TypeBuilder m_type;
	}
}
