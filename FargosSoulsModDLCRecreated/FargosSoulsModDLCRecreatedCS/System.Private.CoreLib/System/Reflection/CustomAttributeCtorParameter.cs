using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000591 RID: 1425
	[StructLayout(LayoutKind.Auto)]
	internal readonly struct CustomAttributeCtorParameter
	{
		// Token: 0x06004948 RID: 18760 RVA: 0x00184682 File Offset: 0x00183882
		public CustomAttributeCtorParameter(CustomAttributeType type)
		{
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x00184697 File Offset: 0x00183897
		public CustomAttributeEncodedArgument CustomAttributeEncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x040011FD RID: 4605
		private readonly CustomAttributeType m_type;

		// Token: 0x040011FE RID: 4606
		private readonly CustomAttributeEncodedArgument m_encodedArgument;
	}
}
