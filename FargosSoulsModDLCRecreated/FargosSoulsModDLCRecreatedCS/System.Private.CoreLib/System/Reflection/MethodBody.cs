using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005E8 RID: 1512
	[NullableContext(1)]
	[Nullable(0)]
	public class MethodBody
	{
		// Token: 0x06004C7A RID: 19578 RVA: 0x000ABD27 File Offset: 0x000AAF27
		protected MethodBody()
		{
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x0018BC8C File Offset: 0x0018AE8C
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				throw new ArgumentNullException("array");
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int MaxStackSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool InitLocals
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x000C26FD File Offset: 0x000C18FD
		[NullableContext(2)]
		public virtual byte[] GetILAsByteArray()
		{
			return null;
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x0018BC8C File Offset: 0x0018AE8C
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				throw new ArgumentNullException("array");
			}
		}
	}
}
