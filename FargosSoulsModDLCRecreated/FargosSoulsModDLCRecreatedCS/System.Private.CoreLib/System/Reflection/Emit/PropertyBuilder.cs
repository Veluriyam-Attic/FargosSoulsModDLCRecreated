using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200064F RID: 1615
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PropertyBuilder : PropertyInfo
	{
		// Token: 0x060051B1 RID: 20913 RVA: 0x00196A18 File Offset: 0x00195C18
		internal PropertyBuilder(ModuleBuilder mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
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
			this.m_name = name;
			this.m_moduleBuilder = mod;
			this.m_signature = sig;
			this.m_attributes = attr;
			this.m_returnType = returnType;
			this.m_prToken = prToken;
			this.m_tkProperty = prToken.Token;
			this.m_containingType = containingType;
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x00196AAC File Offset: 0x00195CAC
		[NullableContext(2)]
		public void SetConstant(object defaultValue)
		{
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_moduleBuilder, this.m_prToken.Token, this.m_returnType, defaultValue);
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x00196AD6 File Offset: 0x00195CD6
		public PropertyToken PropertyToken
		{
			get
			{
				return this.m_prToken;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x00196ADE File Offset: 0x00195CDE
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x00196AEC File Offset: 0x00195CEC
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			ModuleBuilder moduleBuilder = this.m_moduleBuilder;
			TypeBuilder.DefineMethodSemantics(new QCallModule(ref moduleBuilder), this.m_prToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00196B45 File Offset: 0x00195D45
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Getter);
			this.m_getMethod = mdBuilder;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00196B56 File Offset: 0x00195D56
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Setter);
			this.m_setMethod = mdBuilder;
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00196B67 File Offset: 0x00195D67
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00196B74 File Offset: 0x00195D74
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
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token, this.m_moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00196BDB File Offset: 0x00195DDB
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token);
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		[return: Nullable(1)]
		public override object GetValue(object obj, object[] index)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		[return: Nullable(1)]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		public override void SetValue(object obj, object value, object[] index)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x00196C0D File Offset: 0x00195E0D
		[NullableContext(2)]
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_getMethod == null)
			{
				return this.m_getMethod;
			}
			if ((this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_getMethod;
			}
			return null;
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00196C3F File Offset: 0x00195E3F
		[NullableContext(2)]
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_setMethod == null)
			{
				return this.m_setMethod;
			}
			if ((this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_setMethod;
			}
			return null;
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x00196C71 File Offset: 0x00195E71
		public override Type PropertyType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060051C4 RID: 20932 RVA: 0x00196C79 File Offset: 0x00195E79
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x00196C81 File Offset: 0x00195E81
		public override bool CanRead
		{
			get
			{
				return this.m_getMethod != null;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060051C6 RID: 20934 RVA: 0x00196C94 File Offset: 0x00195E94
		public override bool CanWrite
		{
			get
			{
				return this.m_setMethod != null;
			}
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060051CA RID: 20938 RVA: 0x00196CA7 File Offset: 0x00195EA7
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x00196CAF File Offset: 0x00195EAF
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x00196CAF File Offset: 0x00195EAF
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x040014F4 RID: 5364
		private string m_name;

		// Token: 0x040014F5 RID: 5365
		private PropertyToken m_prToken;

		// Token: 0x040014F6 RID: 5366
		private int m_tkProperty;

		// Token: 0x040014F7 RID: 5367
		private ModuleBuilder m_moduleBuilder;

		// Token: 0x040014F8 RID: 5368
		private SignatureHelper m_signature;

		// Token: 0x040014F9 RID: 5369
		private PropertyAttributes m_attributes;

		// Token: 0x040014FA RID: 5370
		private Type m_returnType;

		// Token: 0x040014FB RID: 5371
		private MethodInfo m_getMethod;

		// Token: 0x040014FC RID: 5372
		private MethodInfo m_setMethod;

		// Token: 0x040014FD RID: 5373
		private TypeBuilder m_containingType;
	}
}
