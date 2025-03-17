using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000659 RID: 1625
	internal sealed class FieldOnTypeBuilderInstantiation : FieldInfo
	{
		// Token: 0x06005368 RID: 21352 RVA: 0x0019AD4C File Offset: 0x00199F4C
		internal static FieldInfo GetField(FieldInfo Field, TypeBuilderInstantiation type)
		{
			FieldInfo fieldInfo;
			if (type.m_hashtable.Contains(Field))
			{
				fieldInfo = (type.m_hashtable[Field] as FieldInfo);
			}
			else
			{
				fieldInfo = new FieldOnTypeBuilderInstantiation(Field, type);
				type.m_hashtable[Field] = fieldInfo;
			}
			return fieldInfo;
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0019AD91 File Offset: 0x00199F91
		internal FieldOnTypeBuilderInstantiation(FieldInfo field, TypeBuilderInstantiation type)
		{
			this.m_field = field;
			this.m_type = type;
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x0019ADA7 File Offset: 0x00199FA7
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600536B RID: 21355 RVA: 0x000CA38E File Offset: 0x000C958E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x0019ADAF File Offset: 0x00199FAF
		public override string Name
		{
			get
			{
				return this.m_field.Name;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600536D RID: 21357 RVA: 0x0019ADBC File Offset: 0x00199FBC
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600536E RID: 21358 RVA: 0x0019ADBC File Offset: 0x00199FBC
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0019ADC4 File Offset: 0x00199FC4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0019ADD2 File Offset: 0x00199FD2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0019ADE1 File Offset: 0x00199FE1
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06005372 RID: 21362 RVA: 0x0019ADF0 File Offset: 0x00199FF0
		internal int MetadataTokenInternal
		{
			get
			{
				FieldBuilder fieldBuilder = this.m_field as FieldBuilder;
				if (fieldBuilder != null)
				{
					return fieldBuilder.MetadataTokenInternal;
				}
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x0019AE24 File Offset: 0x0019A024
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0019AE31 File Offset: 0x0019A031
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_field.GetRequiredCustomModifiers();
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0019AE3E File Offset: 0x0019A03E
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_field.GetOptionalCustomModifiers();
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x00177FCF File Offset: 0x001771CF
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x00177FCF File Offset: 0x001771CF
		public override object GetValueDirect(TypedReference obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06005378 RID: 21368 RVA: 0x0019AE4C File Offset: 0x0019A04C
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x00177FCF File Offset: 0x001771CF
		public override Type FieldType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override object GetValue(object obj)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600537C RID: 21372 RVA: 0x0019AE5E File Offset: 0x0019A05E
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x0400153C RID: 5436
		private FieldInfo m_field;

		// Token: 0x0400153D RID: 5437
		private TypeBuilderInstantiation m_type;
	}
}
