using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000624 RID: 1572
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FieldBuilder : FieldInfo
	{
		// Token: 0x06004FC9 RID: 20425 RVA: 0x00190E04 File Offset: 0x00190004
		internal FieldBuilder(TypeBuilder typeBuilder, string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			if (fieldName.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "fieldName");
			}
			if (fieldName[0] == '\0')
			{
				throw new ArgumentException(SR.Argument_IllegalName, "fieldName");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(void))
			{
				throw new ArgumentException(SR.Argument_BadFieldType);
			}
			this.m_fieldName = fieldName;
			this.m_typeBuilder = typeBuilder;
			this.m_fieldType = type;
			this.m_Attributes = (attributes & ~(FieldAttributes.RTSpecialName | FieldAttributes.HasFieldMarshal | FieldAttributes.HasDefault | FieldAttributes.HasFieldRVA));
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this.m_typeBuilder.Module);
			fieldSigHelper.AddArgument(type, requiredCustomModifiers, optionalCustomModifiers);
			int sigLength;
			byte[] signature = fieldSigHelper.InternalGetSignature(out sigLength);
			ModuleBuilder moduleBuilder = this.m_typeBuilder.GetModuleBuilder();
			this.m_fieldTok = TypeBuilder.DefineField(new QCallModule(ref moduleBuilder), typeBuilder.TypeToken.Token, fieldName, signature, sigLength, this.m_Attributes);
			this.m_tkField = new FieldToken(this.m_fieldTok, type);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x00190F18 File Offset: 0x00190118
		internal void SetData(byte[] data, int size)
		{
			ModuleBuilder moduleBuilder = this.m_typeBuilder.GetModuleBuilder();
			ModuleBuilder.SetFieldRVAContent(new QCallModule(ref moduleBuilder), this.m_tkField.Token, data, size);
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x00190F4A File Offset: 0x0019014A
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x00190F52 File Offset: 0x00190152
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x00190F5F File Offset: 0x0019015F
		public override string Name
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x00190F67 File Offset: 0x00190167
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x00190F67 File Offset: 0x00190167
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00190F7E File Offset: 0x0019017E
		public override Type FieldType
		{
			get
			{
				return this.m_fieldType;
			}
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		public override object GetValue(object obj)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x00190F88 File Offset: 0x00190188
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_DynamicModule);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x00190F9F File Offset: 0x0019019F
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_Attributes;
			}
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0018DD92 File Offset: 0x0018CF92
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00190FA7 File Offset: 0x001901A7
		public FieldToken GetToken()
		{
			return this.m_tkField;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x00190FB0 File Offset: 0x001901B0
		public void SetOffset(int iOffset)
		{
			this.m_typeBuilder.ThrowIfCreated();
			ModuleBuilder moduleBuilder = this.m_typeBuilder.GetModuleBuilder();
			TypeBuilder.SetFieldLayoutOffset(new QCallModule(ref moduleBuilder), this.GetToken().Token, iOffset);
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00190FF0 File Offset: 0x001901F0
		[NullableContext(2)]
		public void SetConstant(object defaultValue)
		{
			this.m_typeBuilder.ThrowIfCreated();
			if (defaultValue == null && this.m_fieldType.IsValueType && (!this.m_fieldType.IsGenericType || !(this.m_fieldType.GetGenericTypeDefinition() == typeof(Nullable<>))))
			{
				throw new ArgumentException(SR.Argument_ConstantNull);
			}
			TypeBuilder.SetConstantValue(this.m_typeBuilder.GetModuleBuilder(), this.GetToken().Token, this.m_fieldType, defaultValue);
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x00191074 File Offset: 0x00190274
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
			ModuleBuilder moduleBuilder = this.m_typeBuilder.Module as ModuleBuilder;
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(moduleBuilder, this.m_tkField.Token, moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x001910E4 File Offset: 0x001902E4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_typeBuilder.ThrowIfCreated();
			ModuleBuilder mod = this.m_typeBuilder.Module as ModuleBuilder;
			customBuilder.CreateCustomAttribute(mod, this.m_tkField.Token);
		}

		// Token: 0x04001458 RID: 5208
		private int m_fieldTok;

		// Token: 0x04001459 RID: 5209
		private FieldToken m_tkField;

		// Token: 0x0400145A RID: 5210
		private TypeBuilder m_typeBuilder;

		// Token: 0x0400145B RID: 5211
		private string m_fieldName;

		// Token: 0x0400145C RID: 5212
		private FieldAttributes m_Attributes;

		// Token: 0x0400145D RID: 5213
		private Type m_fieldType;
	}
}
