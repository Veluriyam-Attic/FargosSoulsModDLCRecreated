using System;

namespace System.Reflection
{
	// Token: 0x02000604 RID: 1540
	internal sealed class SignaturePointerType : SignatureHasElementType
	{
		// Token: 0x06004D91 RID: 19857 RVA: 0x0018C621 File Offset: 0x0018B821
		internal SignaturePointerType(SignatureType elementType) : base(elementType)
		{
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x000AC09E File Offset: 0x000AB29E
		protected sealed override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004D95 RID: 19861 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x0018C62A File Offset: 0x0018B82A
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException(SR.Argument_HasToBeArrayClass);
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x0018C855 File Offset: 0x0018BA55
		protected sealed override string Suffix
		{
			get
			{
				return "*";
			}
		}
	}
}
