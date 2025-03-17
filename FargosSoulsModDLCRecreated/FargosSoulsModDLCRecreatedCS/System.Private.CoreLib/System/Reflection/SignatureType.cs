using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000605 RID: 1541
	internal abstract class SignatureType : Type
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x000AC09E File Offset: 0x000AB29E
		public sealed override bool IsSignatureType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004D9A RID: 19866
		public abstract override bool IsTypeDefinition { get; }

		// Token: 0x06004D9B RID: 19867
		protected abstract override bool HasElementTypeImpl();

		// Token: 0x06004D9C RID: 19868
		protected abstract override bool IsArrayImpl();

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004D9D RID: 19869
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004D9E RID: 19870
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x06004D9F RID: 19871
		protected abstract override bool IsByRefImpl();

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004DA0 RID: 19872
		public abstract override bool IsByRefLike { get; }

		// Token: 0x06004DA1 RID: 19873
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004DA2 RID: 19874 RVA: 0x0018C85C File Offset: 0x0018BA5C
		public sealed override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition || this.IsConstructedGenericType;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004DA3 RID: 19875
		public abstract override bool IsGenericTypeDefinition { get; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004DA4 RID: 19876
		public abstract override bool IsConstructedGenericType { get; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004DA5 RID: 19877
		public abstract override bool IsGenericParameter { get; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004DA6 RID: 19878
		public abstract override bool IsGenericTypeParameter { get; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004DA7 RID: 19879
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004DA8 RID: 19880
		public abstract override bool ContainsGenericParameters { get; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x000C26EB File Offset: 0x000C18EB
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x0018C86E File Offset: 0x0018BA6E
		public sealed override Type MakeArrayType()
		{
			return new SignatureArrayType(this, 1, false);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x0018C878 File Offset: 0x0018BA78
		public sealed override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			return new SignatureArrayType(this, rank, true);
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0018C88C File Offset: 0x0018BA8C
		public sealed override Type MakeByRefType()
		{
			return new SignatureByRefType(this);
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x0018C894 File Offset: 0x0018BA94
		public sealed override Type MakePointerType()
		{
			return new SignaturePointerType(this);
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x0018C8A8 File Offset: 0x0018BAA8
		public sealed override Type GetElementType()
		{
			return this.ElementType;
		}

		// Token: 0x06004DB0 RID: 19888
		public abstract override int GetArrayRank();

		// Token: 0x06004DB1 RID: 19889
		public abstract override Type GetGenericTypeDefinition();

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004DB2 RID: 19890
		public abstract override Type[] GenericTypeArguments { get; }

		// Token: 0x06004DB3 RID: 19891
		public abstract override Type[] GetGenericArguments();

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004DB4 RID: 19892
		public abstract override int GenericParameterPosition { get; }

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004DB5 RID: 19893
		internal abstract SignatureType ElementType { get; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x000AC098 File Offset: 0x000AB298
		public sealed override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004DB7 RID: 19895
		public abstract override string Name { get; }

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004DB8 RID: 19896
		public abstract override string Namespace { get; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x000C26FD File Offset: 0x000C18FD
		public sealed override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x000C26FD File Offset: 0x000C18FD
		public sealed override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004DBB RID: 19899
		public abstract override string ToString();

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Assembly Assembly
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Module Module
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type ReflectedType
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type BaseType
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type[] GetInterfaces()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsAssignableFrom([NotNullWhen(true)] Type c)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override int MetadataToken
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type DeclaringType
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override MethodBase DeclaringMethod
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type[] GetGenericParameterConstraints()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsEnumDefined(object value)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override string GetEnumName(object value)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override string[] GetEnumNames()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type GetEnumUnderlyingType()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Array GetEnumValues()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x0018C8B0 File Offset: 0x0018BAB0
		public sealed override Guid GUID
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override TypeCode GetTypeCodeImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override TypeAttributes GetAttributeFlagsImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public sealed override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public sealed override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public sealed override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public sealed override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public sealed override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public sealed override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public sealed override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public sealed override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public sealed override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public sealed override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public sealed override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected sealed override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected sealed override MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		protected sealed override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public sealed override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public sealed override MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public sealed override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
		public sealed override MemberInfo[] GetDefaultMembers()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public sealed override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0018C89C File Offset: 0x0018BA9C
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected sealed override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override bool IsCOMObjectImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override bool IsPrimitiveImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004DEB RID: 19947 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0018C8C8 File Offset: 0x0018BAC8
		public sealed override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override bool IsContextfulImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004DEF RID: 19951 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsEnum
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsEquivalentTo([NotNullWhen(true)] Type other)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsInstanceOfType([NotNullWhen(true)] object o)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override bool IsMarshalByRefImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsSecurityCritical
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsSecurityTransparent
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsSerializable
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0018C89C File Offset: 0x0018BA9C
		protected sealed override bool IsValueTypeImpl()
		{
			throw new NotSupportedException(SR.NotSupported_SignatureType);
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x0018C89C File Offset: 0x0018BA9C
		public sealed override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x0018C8E0 File Offset: 0x0018BAE0
		public sealed override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SignatureType);
			}
		}
	}
}
