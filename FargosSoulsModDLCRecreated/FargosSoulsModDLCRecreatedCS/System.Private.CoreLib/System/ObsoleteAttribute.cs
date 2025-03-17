using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000165 RID: 357
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	public sealed class ObsoleteAttribute : Attribute
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		public ObsoleteAttribute()
		{
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000E66D1 File Offset: 0x000E58D1
		public ObsoleteAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000E66E0 File Offset: 0x000E58E0
		public ObsoleteAttribute(string message, bool error)
		{
			this.Message = message;
			this.IsError = error;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000E66F6 File Offset: 0x000E58F6
		public string Message { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x000E66FE File Offset: 0x000E58FE
		public bool IsError { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x000E6706 File Offset: 0x000E5906
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x000E670E File Offset: 0x000E590E
		public string DiagnosticId { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x000E6717 File Offset: 0x000E5917
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x000E671F File Offset: 0x000E591F
		public string UrlFormat { get; set; }
	}
}
