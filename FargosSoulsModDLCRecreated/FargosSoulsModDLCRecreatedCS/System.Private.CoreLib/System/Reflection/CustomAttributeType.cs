using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000592 RID: 1426
	[StructLayout(LayoutKind.Auto)]
	internal readonly struct CustomAttributeType
	{
		// Token: 0x0600494A RID: 18762 RVA: 0x0018469F File Offset: 0x0018389F
		public CustomAttributeType(CustomAttributeEncoding encodedType, CustomAttributeEncoding encodedArrayType, CustomAttributeEncoding encodedEnumType, string enumName)
		{
			this.m_encodedType = encodedType;
			this.m_encodedArrayType = encodedArrayType;
			this.m_encodedEnumType = encodedEnumType;
			this.m_enumName = enumName;
			this.m_padding = this.m_encodedType;
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x001846CA File Offset: 0x001838CA
		public CustomAttributeEncoding EncodedType
		{
			get
			{
				return this.m_encodedType;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x001846D2 File Offset: 0x001838D2
		public CustomAttributeEncoding EncodedEnumType
		{
			get
			{
				return this.m_encodedEnumType;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x001846DA File Offset: 0x001838DA
		public CustomAttributeEncoding EncodedArrayType
		{
			get
			{
				return this.m_encodedArrayType;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600494E RID: 18766 RVA: 0x001846E2 File Offset: 0x001838E2
		public string EnumName
		{
			get
			{
				return this.m_enumName;
			}
		}

		// Token: 0x040011FF RID: 4607
		private readonly string m_enumName;

		// Token: 0x04001200 RID: 4608
		private readonly CustomAttributeEncoding m_encodedType;

		// Token: 0x04001201 RID: 4609
		private readonly CustomAttributeEncoding m_encodedEnumType;

		// Token: 0x04001202 RID: 4610
		private readonly CustomAttributeEncoding m_encodedArrayType;

		// Token: 0x04001203 RID: 4611
		private readonly CustomAttributeEncoding m_padding;
	}
}
