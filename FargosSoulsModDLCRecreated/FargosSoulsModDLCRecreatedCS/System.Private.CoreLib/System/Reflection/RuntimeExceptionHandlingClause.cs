using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005AC RID: 1452
	internal sealed class RuntimeExceptionHandlingClause : ExceptionHandlingClause
	{
		// Token: 0x06004AF0 RID: 19184 RVA: 0x00188420 File Offset: 0x00187620
		private RuntimeExceptionHandlingClause()
		{
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x00188428 File Offset: 0x00187628
		public override ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x00188430 File Offset: 0x00187630
		public override int TryOffset
		{
			get
			{
				return this._tryOffset;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x00188438 File Offset: 0x00187638
		public override int TryLength
		{
			get
			{
				return this._tryLength;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x00188440 File Offset: 0x00187640
		public override int HandlerOffset
		{
			get
			{
				return this._handlerOffset;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x00188448 File Offset: 0x00187648
		public override int HandlerLength
		{
			get
			{
				return this._handlerLength;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x00188450 File Offset: 0x00187650
		public override int FilterOffset
		{
			get
			{
				if (this._flags != ExceptionHandlingClauseOptions.Filter)
				{
					throw new InvalidOperationException(SR.Arg_EHClauseNotFilter);
				}
				return this._filterOffset;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x0018846C File Offset: 0x0018766C
		public override Type CatchType
		{
			get
			{
				if (this._flags != ExceptionHandlingClauseOptions.Clause)
				{
					throw new InvalidOperationException(SR.Arg_EHClauseNotClause);
				}
				Type result = null;
				if (!MetadataToken.IsNullToken(this._catchMetadataToken))
				{
					Type declaringType = this._methodBody._methodBase.DeclaringType;
					Module module = (declaringType == null) ? this._methodBody._methodBase.Module : declaringType.Module;
					result = module.ResolveType(this._catchMetadataToken, (declaringType != null) ? declaringType.GetGenericArguments() : null, (this._methodBody._methodBase is MethodInfo) ? this._methodBody._methodBase.GetGenericArguments() : null);
				}
				return result;
			}
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00188510 File Offset: 0x00187710
		public override string ToString()
		{
			if (this.Flags == ExceptionHandlingClauseOptions.Clause)
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
			if (this.Flags == ExceptionHandlingClauseOptions.Filter)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, FilterOffset={5}", new object[]
				{
					this.Flags,
					this.TryOffset,
					this.TryLength,
					this.HandlerOffset,
					this.HandlerLength,
					this.FilterOffset
				});
			}
			return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[]
			{
				this.Flags,
				this.TryOffset,
				this.TryLength,
				this.HandlerOffset,
				this.HandlerLength
			});
		}

		// Token: 0x04001291 RID: 4753
		private RuntimeMethodBody _methodBody;

		// Token: 0x04001292 RID: 4754
		private ExceptionHandlingClauseOptions _flags;

		// Token: 0x04001293 RID: 4755
		private int _tryOffset;

		// Token: 0x04001294 RID: 4756
		private int _tryLength;

		// Token: 0x04001295 RID: 4757
		private int _handlerOffset;

		// Token: 0x04001296 RID: 4758
		private int _handlerLength;

		// Token: 0x04001297 RID: 4759
		private int _catchMetadataToken;

		// Token: 0x04001298 RID: 4760
		private int _filterOffset;
	}
}
