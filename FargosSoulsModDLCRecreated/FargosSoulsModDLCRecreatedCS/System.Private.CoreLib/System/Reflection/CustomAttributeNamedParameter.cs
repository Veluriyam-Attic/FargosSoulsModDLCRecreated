using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000590 RID: 1424
	[StructLayout(LayoutKind.Auto)]
	internal readonly struct CustomAttributeNamedParameter
	{
		// Token: 0x06004946 RID: 18758 RVA: 0x00184642 File Offset: 0x00183842
		public CustomAttributeNamedParameter(string argumentName, CustomAttributeEncoding fieldOrProperty, CustomAttributeType type)
		{
			if (argumentName == null)
			{
				throw new ArgumentNullException("argumentName");
			}
			this.m_argumentName = argumentName;
			this.m_fieldOrProperty = fieldOrProperty;
			this.m_padding = fieldOrProperty;
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x0018467A File Offset: 0x0018387A
		public CustomAttributeEncodedArgument EncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x040011F8 RID: 4600
		private readonly string m_argumentName;

		// Token: 0x040011F9 RID: 4601
		private readonly CustomAttributeEncoding m_fieldOrProperty;

		// Token: 0x040011FA RID: 4602
		private readonly CustomAttributeEncoding m_padding;

		// Token: 0x040011FB RID: 4603
		private readonly CustomAttributeType m_type;

		// Token: 0x040011FC RID: 4604
		private readonly CustomAttributeEncodedArgument m_encodedArgument;
	}
}
