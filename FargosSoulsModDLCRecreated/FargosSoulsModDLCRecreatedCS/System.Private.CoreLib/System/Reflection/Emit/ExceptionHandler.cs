using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000649 RID: 1609
	internal readonly struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		// Token: 0x06005111 RID: 20753 RVA: 0x001948E7 File Offset: 0x00193AE7
		internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
		{
			this.m_tryStartOffset = tryStartOffset;
			this.m_tryEndOffset = tryEndOffset;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerStartOffset;
			this.m_handlerEndOffset = handlerEndOffset;
			this.m_kind = (ExceptionHandlingClauseOptions)kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0019491E File Offset: 0x00193B1E
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00194950 File Offset: 0x00193B50
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x00194968 File Offset: 0x00193B68
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		// Token: 0x040014DE RID: 5342
		internal readonly int m_exceptionClass;

		// Token: 0x040014DF RID: 5343
		internal readonly int m_tryStartOffset;

		// Token: 0x040014E0 RID: 5344
		internal readonly int m_tryEndOffset;

		// Token: 0x040014E1 RID: 5345
		internal readonly int m_filterOffset;

		// Token: 0x040014E2 RID: 5346
		internal readonly int m_handlerStartOffset;

		// Token: 0x040014E3 RID: 5347
		internal readonly int m_handlerEndOffset;

		// Token: 0x040014E4 RID: 5348
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}
