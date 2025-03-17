using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000656 RID: 1622
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x060052F3 RID: 21235 RVA: 0x000BC768 File Offset: 0x000BB968
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x0019A8C8 File Offset: 0x00199AC8
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			if (!type.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException();
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			foreach (Type left in typeArguments)
			{
				if (left == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x0019A920 File Offset: 0x00199B20
		private TypeBuilderInstantiation(Type type, Type[] inst)
		{
			this.m_type = type;
			this.m_inst = inst;
			this.m_hashtable = new Hashtable();
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00198175 File Offset: 0x00197375
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060052F7 RID: 21239 RVA: 0x0019A941 File Offset: 0x00199B41
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060052F8 RID: 21240 RVA: 0x0019A94E File Offset: 0x00199B4E
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x0019A95B File Offset: 0x00199B5B
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060052FA RID: 21242 RVA: 0x0019A968 File Offset: 0x00199B68
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00190BFD File Offset: 0x0018FDFD
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*", this, 0);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00190C0B File Offset: 0x0018FE0B
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&", this, 0);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00190C19 File Offset: 0x0018FE19
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]", this, 0);
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0019A978 File Offset: 0x00199B78
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string format = (rank == 1) ? "[]" : ("[" + new string(',', rank - 1) + "]");
			return SymbolType.FormCompoundType(format, this, 0);
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x001911E0 File Offset: 0x001903E0
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x0019A9BC File Offset: 0x00199BBC
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x000C2C3C File Offset: 0x000C1E3C
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x0019A9CC File Offset: 0x00199BCC
		public override string FullName
		{
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

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0019A9F3 File Offset: 0x00199BF3
		public override string Namespace
		{
			get
			{
				return this.m_type.Namespace;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x0019816C File Offset: 0x0019736C
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0019AA00 File Offset: 0x00199C00
		private Type Substitute(Type[] substitutes)
		{
			Type[] genericArguments = this.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Type type = genericArguments[i];
				TypeBuilderInstantiation typeBuilderInstantiation = type as TypeBuilderInstantiation;
				if (typeBuilderInstantiation != null)
				{
					array[i] = typeBuilderInstantiation.Substitute(substitutes);
				}
				else if (type is GenericTypeParameterBuilder)
				{
					array[i] = substitutes[type.GenericParameterPosition];
				}
				else
				{
					array[i] = type;
				}
			}
			return this.GetGenericTypeDefinition().MakeGenericType(array);
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x0019AA70 File Offset: 0x00199C70
		public override Type BaseType
		{
			get
			{
				Type baseType = this.m_type.BaseType;
				if (baseType == null)
				{
					return null;
				}
				TypeBuilderInstantiation typeBuilderInstantiation = baseType as TypeBuilderInstantiation;
				if (typeBuilderInstantiation == null)
				{
					return baseType;
				}
				return typeBuilderInstantiation.Substitute(this.GetGenericArguments());
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x000C279F File Offset: 0x000C199F
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x000C279F File Offset: 0x000C199F
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x000C279F File Offset: 0x000C199F
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x000C279F File Offset: 0x000C199F
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0019AAB2 File Offset: 0x00199CB2
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_type.Attributes;
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600531B RID: 21275 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x000C279F File Offset: 0x000C199F
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x000AC098 File Offset: 0x000AB298
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x0019AABF File Offset: 0x00199CBF
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06005329 RID: 21289 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0019AAC7 File Offset: 0x00199CC7
		protected override bool IsValueTypeImpl()
		{
			return this.m_type.IsValueType;
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x0019AAD4 File Offset: 0x00199CD4
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x000C26FD File Offset: 0x000C18FD
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x0019AB06 File Offset: 0x00199D06
		public override Type GetGenericTypeDefinition()
		{
			return this.m_type;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00191240 File Offset: 0x00190440
		public override Type MakeGenericType(params Type[] inst)
		{
			throw new InvalidOperationException(SR.Format(SR.Arg_NotGenericTypeDefinition, this));
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x000C279F File Offset: 0x000C199F
		public override bool IsAssignableFrom([NotNullWhen(true)] Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x000C279F File Offset: 0x000C199F
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x000C279F File Offset: 0x000C199F
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x000C279F File Offset: 0x000C199F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x000C279F File Offset: 0x000C199F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001534 RID: 5428
		private Type m_type;

		// Token: 0x04001535 RID: 5429
		private Type[] m_inst;

		// Token: 0x04001536 RID: 5430
		private string m_strFullQualName;

		// Token: 0x04001537 RID: 5431
		internal Hashtable m_hashtable;
	}
}
