using System;

namespace System.Reflection
{
	// Token: 0x020005FF RID: 1535
	internal sealed class SignatureByRefType : SignatureHasElementType
	{
		// Token: 0x06004D3D RID: 19773 RVA: 0x0018C621 File Offset: 0x0018B821
		internal SignatureByRefType(SignatureType elementType) : base(elementType)
		{
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x000AC09E File Offset: 0x000AB29E
		protected sealed override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x0018C62A File Offset: 0x0018B82A
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException(SR.Argument_HasToBeArrayClass);
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x0018C636 File Offset: 0x0018B836
		protected sealed override string Suffix
		{
			get
			{
				return "&";
			}
		}
	}
}
