using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200060C RID: 1548
	[NullableContext(1)]
	[Nullable(0)]
	public class TypeDelegator : TypeInfo
	{
		// Token: 0x06004E17 RID: 19991 RVA: 0x000BC768 File Offset: 0x000BB968
		[NullableContext(2)]
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x0018CD96 File Offset: 0x0018BF96
		protected TypeDelegator()
		{
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0018CD9E File Offset: 0x0018BF9E
		public TypeDelegator([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x0018CDBB File Offset: 0x0018BFBB
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x0018CDC8 File Offset: 0x0018BFC8
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0018CDD8 File Offset: 0x0018BFD8
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004E1D RID: 19997 RVA: 0x0018CDFD File Offset: 0x0018BFFD
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x0018CE0A File Offset: 0x0018C00A
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x0018CE17 File Offset: 0x0018C017
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004E20 RID: 20000 RVA: 0x0018CE24 File Offset: 0x0018C024
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004E21 RID: 20001 RVA: 0x0018CE31 File Offset: 0x0018C031
		[Nullable(2)]
		public override string FullName
		{
			[NullableContext(2)]
			get
			{
				return this.typeImpl.FullName;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x0018CE3E File Offset: 0x0018C03E
		[Nullable(2)]
		public override string Namespace
		{
			[NullableContext(2)]
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x0018CE4B File Offset: 0x0018C04B
		[Nullable(2)]
		public override string AssemblyQualifiedName
		{
			[NullableContext(2)]
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0018CE58 File Offset: 0x0018C058
		[Nullable(2)]
		public override Type BaseType
		{
			[NullableContext(2)]
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x0018CE65 File Offset: 0x0018C065
		[NullableContext(2)]
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x0018CE79 File Offset: 0x0018C079
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x0018CE87 File Offset: 0x0018C087
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected override MethodInfo GetMethodImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x0018CEAF File Offset: 0x0018C0AF
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x0018CEBD File Offset: 0x0018C0BD
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		[return: Nullable(2)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x0018CECC File Offset: 0x0018C0CC
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x0018CEDA File Offset: 0x0018C0DA
		[return: Nullable(2)]
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x0018CEE9 File Offset: 0x0018C0E9
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x0018CEF6 File Offset: 0x0018C0F6
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		[return: Nullable(2)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x0018CF05 File Offset: 0x0018C105
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public override EventInfo[] GetEvents()
		{
			return this.typeImpl.GetEvents();
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x0018CF12 File Offset: 0x0018C112
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		protected override PropertyInfo GetPropertyImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x0018CF44 File Offset: 0x0018C144
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x0018CF52 File Offset: 0x0018C152
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x0018CF60 File Offset: 0x0018C160
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x0018CF6E File Offset: 0x0018C16E
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		[return: Nullable(2)]
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x0018CF7D File Offset: 0x0018C17D
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x0018CF8D File Offset: 0x0018C18D
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0018CF9B File Offset: 0x0018C19B
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x0018CFA8 File Offset: 0x0018C1A8
		public override bool IsTypeDefinition
		{
			get
			{
				return this.typeImpl.IsTypeDefinition;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x0018CFB5 File Offset: 0x0018C1B5
		public override bool IsSZArray
		{
			get
			{
				return this.typeImpl.IsSZArray;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x0018CFC2 File Offset: 0x0018C1C2
		public override bool IsVariableBoundArray
		{
			get
			{
				return this.typeImpl.IsVariableBoundArray;
			}
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x0018CFCF File Offset: 0x0018C1CF
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x0018CFDC File Offset: 0x0018C1DC
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x0018CFE9 File Offset: 0x0018C1E9
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x0018CFF6 File Offset: 0x0018C1F6
		public override bool IsGenericTypeParameter
		{
			get
			{
				return this.typeImpl.IsGenericTypeParameter;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x0018D003 File Offset: 0x0018C203
		public override bool IsGenericMethodParameter
		{
			get
			{
				return this.typeImpl.IsGenericMethodParameter;
			}
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x0018D010 File Offset: 0x0018C210
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0018D01D File Offset: 0x0018C21D
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x0018D02A File Offset: 0x0018C22A
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x0018D037 File Offset: 0x0018C237
		public override bool IsByRefLike
		{
			get
			{
				return this.typeImpl.IsByRefLike;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004E43 RID: 20035 RVA: 0x0018D044 File Offset: 0x0018C244
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x0018D051 File Offset: 0x0018C251
		public override bool IsCollectible
		{
			get
			{
				return this.typeImpl.IsCollectible;
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0018D05E File Offset: 0x0018C25E
		[NullableContext(2)]
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x0018D06B File Offset: 0x0018C26B
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004E47 RID: 20039 RVA: 0x0018D078 File Offset: 0x0018C278
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x0018D085 File Offset: 0x0018C285
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x0018D093 File Offset: 0x0018C293
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x0018D0A2 File Offset: 0x0018C2A2
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x0018D0B1 File Offset: 0x0018C2B1
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		// Token: 0x04001402 RID: 5122
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		protected Type typeImpl;
	}
}
