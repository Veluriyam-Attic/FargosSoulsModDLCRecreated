using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x0200059A RID: 1434
	internal sealed class MdFieldInfo : RuntimeFieldInfo
	{
		// Token: 0x060049A7 RID: 18855 RVA: 0x00186212 File Offset: 0x00185412
		internal MdFieldInfo(int tkField, FieldAttributes fieldAttributes, RuntimeTypeHandle declaringTypeHandle, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags) : base(reflectedTypeCache, declaringTypeHandle.GetRuntimeType(), bindingFlags)
		{
			this.m_tkField = tkField;
			this.m_name = null;
			this.m_fieldAttributes = fieldAttributes;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0018623C File Offset: 0x0018543C
		internal override bool CacheEquals(object o)
		{
			MdFieldInfo mdFieldInfo = o as MdFieldInfo;
			return mdFieldInfo != null && mdFieldInfo.m_tkField == this.m_tkField && this.m_declaringType.GetTypeHandleInternal().GetModuleHandle().Equals(mdFieldInfo.m_declaringType.GetTypeHandleInternal().GetModuleHandle());
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x00186294 File Offset: 0x00185494
		public override string Name
		{
			get
			{
				string result;
				if ((result = this.m_name) == null)
				{
					result = (this.m_name = this.GetRuntimeModule().MetadataImport.GetName(this.m_tkField).ToString());
				}
				return result;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x001862DB File Offset: 0x001854DB
		public override int MetadataToken
		{
			get
			{
				return this.m_tkField;
			}
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x001862E3 File Offset: 0x001854E3
		internal override RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x001862F0 File Offset: 0x001854F0
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x00186302 File Offset: 0x00185502
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x0018630A File Offset: 0x0018550A
		public override bool IsSecurityCritical
		{
			get
			{
				return this.DeclaringType.IsSecurityCritical;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x00186317 File Offset: 0x00185517
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.DeclaringType.IsSecuritySafeCritical;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x00186324 File Offset: 0x00185524
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.DeclaringType.IsSecurityTransparent;
			}
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x00186331 File Offset: 0x00185531
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValueDirect(TypedReference obj)
		{
			return this.GetValue(null);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0018633A File Offset: 0x0018553A
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new FieldAccessException(SR.Acc_ReadOnly);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x00186346 File Offset: 0x00185546
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object GetValue(object obj)
		{
			return this.GetValue(false);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0018634F File Offset: 0x0018554F
		public override object GetRawConstantValue()
		{
			return this.GetValue(true);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x00186358 File Offset: 0x00185558
		private object GetValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_tkField, this.FieldType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new NotSupportedException(SR.Arg_EnumLitValueNotFound);
			}
			return value;
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0018633A File Offset: 0x0018553A
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new FieldAccessException(SR.Acc_ReadOnly);
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x0018639C File Offset: 0x0018559C
		public override Type FieldType
		{
			get
			{
				if (this.m_fieldType == null)
				{
					ConstArray sigOfFieldDef = this.GetRuntimeModule().MetadataImport.GetSigOfFieldDef(this.m_tkField);
					this.m_fieldType = new Signature(sigOfFieldDef.Signature.ToPointer(), sigOfFieldDef.Length, this.m_declaringType).FieldType;
				}
				return this.m_fieldType;
			}
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00186403 File Offset: 0x00185603
		public override Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x00186403 File Offset: 0x00185603
		public override Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x04001218 RID: 4632
		private int m_tkField;

		// Token: 0x04001219 RID: 4633
		private string m_name;

		// Token: 0x0400121A RID: 4634
		private RuntimeType m_fieldType;

		// Token: 0x0400121B RID: 4635
		private FieldAttributes m_fieldAttributes;
	}
}
