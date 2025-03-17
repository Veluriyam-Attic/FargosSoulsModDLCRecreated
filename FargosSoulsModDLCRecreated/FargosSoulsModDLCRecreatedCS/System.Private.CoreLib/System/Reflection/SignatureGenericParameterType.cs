using System;

namespace System.Reflection
{
	// Token: 0x02000602 RID: 1538
	internal abstract class SignatureGenericParameterType : SignatureType
	{
		// Token: 0x06004D61 RID: 19809 RVA: 0x0018C7C9 File Offset: 0x0018B9C9
		protected SignatureGenericParameterType(int position)
		{
			this._position = position;
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004D67 RID: 19815 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004D69 RID: 19817 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x000AC09B File Offset: 0x000AB29B
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x000AC09E File Offset: 0x000AB29E
		public sealed override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004D6D RID: 19821
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x000AC09E File Offset: 0x000AB29E
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x000C26FD File Offset: 0x000C18FD
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0018C62A File Offset: 0x0018B82A
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException(SR.Argument_HasToBeArrayClass);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0018C7D8 File Offset: 0x0018B9D8
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException(SR.InvalidOperation_NotGenericType);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x00186403 File Offset: 0x00185603
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x00186403 File Offset: 0x00185603
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x0018C7E4 File Offset: 0x0018B9E4
		public sealed override int GenericParameterPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004D75 RID: 19829
		public abstract override string Name { get; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x000C26FD File Offset: 0x000C18FD
		public sealed override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x0018C7EC File Offset: 0x0018B9EC
		public sealed override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040013DF RID: 5087
		private readonly int _position;
	}
}
