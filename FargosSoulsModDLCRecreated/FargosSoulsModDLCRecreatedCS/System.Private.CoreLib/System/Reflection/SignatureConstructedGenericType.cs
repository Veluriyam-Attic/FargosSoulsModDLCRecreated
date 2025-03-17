using System;
using System.Text;

namespace System.Reflection
{
	// Token: 0x02000600 RID: 1536
	internal sealed class SignatureConstructedGenericType : SignatureType
	{
		// Token: 0x06004D45 RID: 19781 RVA: 0x0018C640 File Offset: 0x0018B840
		internal SignatureConstructedGenericType(Type genericTypeDefinition, Type[] typeArguments)
		{
			if (genericTypeDefinition == null)
			{
				throw new ArgumentNullException("genericTypeDefinition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			typeArguments = (Type[])typeArguments.Clone();
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			this._genericTypeDefinition = genericTypeDefinition;
			this._genericTypeArguments = typeArguments;
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004D46 RID: 19782 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x0018C6A8 File Offset: 0x0018B8A8
		public sealed override bool IsByRefLike
		{
			get
			{
				return this._genericTypeDefinition.IsByRefLike;
			}
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004D4D RID: 19789 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004D4E RID: 19790 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004D4F RID: 19791 RVA: 0x000AC09E File Offset: 0x000AB29E
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004D50 RID: 19792 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x0018C6B8 File Offset: 0x0018B8B8
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this._genericTypeArguments.Length; i++)
				{
					if (this._genericTypeArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x000C26FD File Offset: 0x000C18FD
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0018C62A File Offset: 0x0018B82A
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException(SR.Argument_HasToBeArrayClass);
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0018C6EA File Offset: 0x0018B8EA
		public sealed override Type GetGenericTypeDefinition()
		{
			return this._genericTypeDefinition;
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0018C6F2 File Offset: 0x0018B8F2
		public sealed override Type[] GetGenericArguments()
		{
			return this.GenericTypeArguments;
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004D58 RID: 19800 RVA: 0x0018C6FA File Offset: 0x0018B8FA
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return (Type[])this._genericTypeArguments.Clone();
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004D59 RID: 19801 RVA: 0x000C2793 File Offset: 0x000C1993
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException(SR.Arg_NotGenericParameter);
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x0018C70C File Offset: 0x0018B90C
		public sealed override string Name
		{
			get
			{
				return this._genericTypeDefinition.Name;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004D5B RID: 19803 RVA: 0x0018C719 File Offset: 0x0018B919
		public sealed override string Namespace
		{
			get
			{
				return this._genericTypeDefinition.Namespace;
			}
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0018C728 File Offset: 0x0018B928
		public sealed override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._genericTypeDefinition.ToString());
			stringBuilder.Append('[');
			for (int i = 0; i < this._genericTypeArguments.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this._genericTypeArguments[i].ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x040013DD RID: 5085
		private readonly Type _genericTypeDefinition;

		// Token: 0x040013DE RID: 5086
		private readonly Type[] _genericTypeArguments;
	}
}
