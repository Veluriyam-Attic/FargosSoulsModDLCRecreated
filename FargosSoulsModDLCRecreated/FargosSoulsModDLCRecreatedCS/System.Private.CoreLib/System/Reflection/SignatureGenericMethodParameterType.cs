using System;

namespace System.Reflection
{
	// Token: 0x02000601 RID: 1537
	internal sealed class SignatureGenericMethodParameterType : SignatureGenericParameterType
	{
		// Token: 0x06004D5D RID: 19805 RVA: 0x0018C798 File Offset: 0x0018B998
		internal SignatureGenericMethodParameterType(int position) : base(position)
		{
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004D5E RID: 19806 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004D5F RID: 19807 RVA: 0x000AC09E File Offset: 0x000AB29E
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x0018C7A4 File Offset: 0x0018B9A4
		public sealed override string Name
		{
			get
			{
				return "!!" + this.GenericParameterPosition.ToString();
			}
		}
	}
}
