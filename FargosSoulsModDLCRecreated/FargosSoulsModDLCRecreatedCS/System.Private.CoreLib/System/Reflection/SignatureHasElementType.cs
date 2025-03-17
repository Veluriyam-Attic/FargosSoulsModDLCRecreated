using System;

namespace System.Reflection
{
	// Token: 0x02000603 RID: 1539
	internal abstract class SignatureHasElementType : SignatureType
	{
		// Token: 0x06004D78 RID: 19832 RVA: 0x0018C7F4 File Offset: 0x0018B9F4
		protected SignatureHasElementType(SignatureType elementType)
		{
			this._elementType = elementType;
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x000AC09E File Offset: 0x000AB29E
		protected sealed override bool HasElementTypeImpl()
		{
			return true;
		}

		// Token: 0x06004D7C RID: 19836
		protected abstract override bool IsArrayImpl();

		// Token: 0x06004D7D RID: 19837
		protected abstract override bool IsByRefImpl();

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004D7E RID: 19838 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D7F RID: 19839
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004D80 RID: 19840
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004D81 RID: 19841
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x0018C803 File Offset: 0x0018BA03
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return this._elementType.ContainsGenericParameters;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x0018C810 File Offset: 0x0018BA10
		internal sealed override SignatureType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06004D88 RID: 19848
		public abstract override int GetArrayRank();

		// Token: 0x06004D89 RID: 19849 RVA: 0x0018C7D8 File Offset: 0x0018B9D8
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException(SR.InvalidOperation_NotGenericType);
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x00186403 File Offset: 0x00185603
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00186403 File Offset: 0x00185603
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x000C2793 File Offset: 0x000C1993
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException(SR.Arg_NotGenericParameter);
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x0018C818 File Offset: 0x0018BA18
		public sealed override string Name
		{
			get
			{
				return this._elementType.Name + this.Suffix;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x0018C830 File Offset: 0x0018BA30
		public sealed override string Namespace
		{
			get
			{
				return this._elementType.Namespace;
			}
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0018C83D File Offset: 0x0018BA3D
		public sealed override string ToString()
		{
			return this._elementType.ToString() + this.Suffix;
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004D90 RID: 19856
		protected abstract string Suffix { get; }

		// Token: 0x040013E0 RID: 5088
		private readonly SignatureType _elementType;
	}
}
