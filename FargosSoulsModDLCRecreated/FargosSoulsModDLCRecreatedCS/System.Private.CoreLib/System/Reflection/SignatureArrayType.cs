using System;

namespace System.Reflection
{
	// Token: 0x020005FE RID: 1534
	internal sealed class SignatureArrayType : SignatureHasElementType
	{
		// Token: 0x06004D35 RID: 19765 RVA: 0x0018C5B2 File Offset: 0x0018B7B2
		internal SignatureArrayType(SignatureType elementType, int rank, bool isMultiDim) : base(elementType)
		{
			this._rank = rank;
			this._isMultiDim = isMultiDim;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x000AC09E File Offset: 0x000AB29E
		protected sealed override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x0018C5C9 File Offset: 0x0018B7C9
		public sealed override bool IsSZArray
		{
			get
			{
				return !this._isMultiDim;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x0018C5D4 File Offset: 0x0018B7D4
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return this._isMultiDim;
			}
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0018C5DC File Offset: 0x0018B7DC
		public sealed override int GetArrayRank()
		{
			return this._rank;
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x0018C5E4 File Offset: 0x0018B7E4
		protected sealed override string Suffix
		{
			get
			{
				if (!this._isMultiDim)
				{
					return "[]";
				}
				if (this._rank == 1)
				{
					return "[*]";
				}
				return "[" + new string(',', this._rank - 1) + "]";
			}
		}

		// Token: 0x040013DB RID: 5083
		private readonly int _rank;

		// Token: 0x040013DC RID: 5084
		private readonly bool _isMultiDim;
	}
}
