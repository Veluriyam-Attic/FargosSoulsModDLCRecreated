using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000653 RID: 1619
	internal sealed class SymbolType : TypeInfo
	{
		// Token: 0x06005214 RID: 21012 RVA: 0x000BC768 File Offset: 0x000BB968
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00197C80 File Offset: 0x00196E80
		internal static Type FormCompoundType(string format, Type baseType, int curIndex)
		{
			if (format == null || curIndex == format.Length)
			{
				return baseType;
			}
			if (format[curIndex] == '&')
			{
				SymbolType symbolType = new SymbolType(TypeKind.IsByRef);
				symbolType.SetFormat(format, curIndex, 1);
				curIndex++;
				if (curIndex != format.Length)
				{
					throw new ArgumentException(SR.Argument_BadSigFormat);
				}
				symbolType.SetElementType(baseType);
				return symbolType;
			}
			else
			{
				if (format[curIndex] == '[')
				{
					SymbolType symbolType = new SymbolType(TypeKind.IsArray);
					int num = curIndex;
					curIndex++;
					int num2 = 0;
					int num3 = -1;
					while (format[curIndex] != ']')
					{
						if (format[curIndex] == '*')
						{
							symbolType.m_isSzArray = false;
							curIndex++;
						}
						if ((format[curIndex] >= '0' && format[curIndex] <= '9') || format[curIndex] == '-')
						{
							bool flag = false;
							if (format[curIndex] == '-')
							{
								flag = true;
								curIndex++;
							}
							while (format[curIndex] >= '0' && format[curIndex] <= '9')
							{
								num2 *= 10;
								num2 += (int)(format[curIndex] - '0');
								curIndex++;
							}
							if (flag)
							{
								num2 = 0 - num2;
							}
							num3 = num2 - 1;
						}
						if (format[curIndex] == '.')
						{
							curIndex++;
							if (format[curIndex] != '.')
							{
								throw new ArgumentException(SR.Argument_BadSigFormat);
							}
							curIndex++;
							if ((format[curIndex] >= '0' && format[curIndex] <= '9') || format[curIndex] == '-')
							{
								bool flag2 = false;
								num3 = 0;
								if (format[curIndex] == '-')
								{
									flag2 = true;
									curIndex++;
								}
								while (format[curIndex] >= '0' && format[curIndex] <= '9')
								{
									num3 *= 10;
									num3 += (int)(format[curIndex] - '0');
									curIndex++;
								}
								if (flag2)
								{
									num3 = 0 - num3;
								}
								if (num3 < num2)
								{
									throw new ArgumentException(SR.Argument_BadSigFormat);
								}
							}
						}
						if (format[curIndex] == ',')
						{
							curIndex++;
							symbolType.SetBounds(num2, num3);
							num2 = 0;
							num3 = -1;
						}
						else if (format[curIndex] != ']')
						{
							throw new ArgumentException(SR.Argument_BadSigFormat);
						}
					}
					symbolType.SetBounds(num2, num3);
					curIndex++;
					symbolType.SetFormat(format, num, curIndex - num);
					symbolType.SetElementType(baseType);
					return SymbolType.FormCompoundType(format, symbolType, curIndex);
				}
				if (format[curIndex] == '*')
				{
					SymbolType symbolType = new SymbolType(TypeKind.IsPointer);
					symbolType.SetFormat(format, curIndex, 1);
					curIndex++;
					symbolType.SetElementType(baseType);
					return SymbolType.FormCompoundType(format, symbolType, curIndex);
				}
				return null;
			}
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00197ECE File Offset: 0x001970CE
		internal SymbolType(TypeKind typeKind)
		{
			this.m_typeKind = typeKind;
			this.m_iaLowerBound = new int[4];
			this.m_iaUpperBound = new int[4];
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00197EFC File Offset: 0x001970FC
		internal void SetElementType(Type baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			this.m_baseType = baseType;
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00197F14 File Offset: 0x00197114
		private void SetBounds(int lower, int upper)
		{
			if (lower != 0 || upper != -1)
			{
				this.m_isSzArray = false;
			}
			if (this.m_iaLowerBound.Length <= this.m_cRank)
			{
				int[] array = new int[this.m_cRank * 2];
				Array.Copy(this.m_iaLowerBound, array, this.m_cRank);
				this.m_iaLowerBound = array;
				Array.Copy(this.m_iaUpperBound, array, this.m_cRank);
				this.m_iaUpperBound = array;
			}
			this.m_iaLowerBound[this.m_cRank] = lower;
			this.m_iaUpperBound[this.m_cRank] = upper;
			this.m_cRank++;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00197FA9 File Offset: 0x001971A9
		internal void SetFormat(string format, int curIndex, int length)
		{
			this.m_format = format.Substring(curIndex, length);
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600521A RID: 21018 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600521B RID: 21019 RVA: 0x00197FB9 File Offset: 0x001971B9
		public override bool IsSZArray
		{
			get
			{
				return this.m_cRank <= 1 && this.m_isSzArray;
			}
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00197FCC File Offset: 0x001971CC
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType(this.m_format + "*", this.m_baseType, 0);
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x00197FEA File Offset: 0x001971EA
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType(this.m_format + "&", this.m_baseType, 0);
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x00198008 File Offset: 0x00197208
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType(this.m_format + "[]", this.m_baseType, 0);
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x00198028 File Offset: 0x00197228
		public override Type MakeArrayType(int rank)
		{
			string rankString = TypeInfo.GetRankString(rank);
			return SymbolType.FormCompoundType(this.m_format + rankString, this.m_baseType, 0) as SymbolType;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0019805B File Offset: 0x0019725B
		public override int GetArrayRank()
		{
			if (!base.IsArray)
			{
				throw new NotSupportedException(SR.NotSupported_SubclassOverride);
			}
			return this.m_cRank;
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x00198078 File Offset: 0x00197278
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_NonReflectedType);
			}
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06005223 RID: 21027 RVA: 0x0019809C File Offset: 0x0019729C
		public override Module Module
		{
			get
			{
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Module;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x001980CC File Offset: 0x001972CC
		public override Assembly Assembly
		{
			get
			{
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Assembly;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x001980FC File Offset: 0x001972FC
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_NonReflectedType);
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x00198114 File Offset: 0x00197314
		public override string Name
		{
			get
			{
				string str = this.m_format;
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					str = ((SymbolType)baseType).m_format + str;
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Name + str;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x00198163 File Offset: 0x00197363
		public override string FullName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06005228 RID: 21032 RVA: 0x0019816C File Offset: 0x0019736C
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00198175 File Offset: 0x00197375
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600522A RID: 21034 RVA: 0x0019817E File Offset: 0x0019737E
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x0019818B File Offset: 0x0019738B
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0019808F File Offset: 0x0019728F
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x0019808F File Offset: 0x0019728F
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x0019808F File Offset: 0x0019728F
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0019808F File Offset: 0x0019728F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00198198 File Offset: 0x00197398
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type baseType = this.m_baseType;
			while (baseType is SymbolType)
			{
				baseType = ((SymbolType)baseType).m_baseType;
			}
			return baseType.Attributes;
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x001981C8 File Offset: 0x001973C8
		protected override bool IsArrayImpl()
		{
			return this.m_typeKind == TypeKind.IsArray;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x001981D3 File Offset: 0x001973D3
		protected override bool IsPointerImpl()
		{
			return this.m_typeKind == TypeKind.IsPointer;
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x001981DE File Offset: 0x001973DE
		protected override bool IsByRefImpl()
		{
			return this.m_typeKind == TypeKind.IsByRef;
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x001981E9 File Offset: 0x001973E9
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x001981F1 File Offset: 0x001973F1
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06005248 RID: 21064 RVA: 0x000AC098 File Offset: 0x000AB298
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0019808F File Offset: 0x0019728F
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0019808F File Offset: 0x0019728F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0019808F File Offset: 0x0019728F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_NonReflectedType);
		}

		// Token: 0x04001510 RID: 5392
		internal TypeKind m_typeKind;

		// Token: 0x04001511 RID: 5393
		internal Type m_baseType;

		// Token: 0x04001512 RID: 5394
		internal int m_cRank;

		// Token: 0x04001513 RID: 5395
		internal int[] m_iaLowerBound;

		// Token: 0x04001514 RID: 5396
		internal int[] m_iaUpperBound;

		// Token: 0x04001515 RID: 5397
		private string m_format;

		// Token: 0x04001516 RID: 5398
		private bool m_isSzArray = true;
	}
}
