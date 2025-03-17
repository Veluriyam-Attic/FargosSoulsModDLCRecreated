using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005D8 RID: 1496
	[NullableContext(2)]
	[Nullable(0)]
	public class ExceptionHandlingClause
	{
		// Token: 0x06004C4F RID: 19535 RVA: 0x000ABD27 File Offset: 0x000AAF27
		protected ExceptionHandlingClause()
		{
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return ExceptionHandlingClauseOptions.Clause;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004C51 RID: 19537 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int TryOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004C52 RID: 19538 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int TryLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004C53 RID: 19539 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int HandlerOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004C54 RID: 19540 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int HandlerLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x0018BB27 File Offset: 0x0018AD27
		public virtual int FilterOffset
		{
			get
			{
				throw new InvalidOperationException(SR.Arg_EHClauseNotFilter);
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004C56 RID: 19542 RVA: 0x000C26FD File Offset: 0x000C18FD
		public virtual Type CatchType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0018BB34 File Offset: 0x0018AD34
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, CatchType={5}", new object[]
			{
				this.Flags,
				this.TryOffset,
				this.TryLength,
				this.HandlerOffset,
				this.HandlerLength,
				this.CatchType
			});
		}
	}
}
