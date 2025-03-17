using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006FC RID: 1788
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	public sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x06005956 RID: 22870 RVA: 0x001B1C05 File Offset: 0x001B0E05
		[NullableContext(1)]
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06005957 RID: 22871 RVA: 0x001B1C1B File Offset: 0x001B0E1B
		[Nullable(1)]
		public string Category { [NullableContext(1)] get; }

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06005958 RID: 22872 RVA: 0x001B1C23 File Offset: 0x001B0E23
		[Nullable(1)]
		public string CheckId { [NullableContext(1)] get; }

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06005959 RID: 22873 RVA: 0x001B1C2B File Offset: 0x001B0E2B
		// (set) Token: 0x0600595A RID: 22874 RVA: 0x001B1C33 File Offset: 0x001B0E33
		public string Scope { get; set; }

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600595B RID: 22875 RVA: 0x001B1C3C File Offset: 0x001B0E3C
		// (set) Token: 0x0600595C RID: 22876 RVA: 0x001B1C44 File Offset: 0x001B0E44
		public string Target { get; set; }

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600595D RID: 22877 RVA: 0x001B1C4D File Offset: 0x001B0E4D
		// (set) Token: 0x0600595E RID: 22878 RVA: 0x001B1C55 File Offset: 0x001B0E55
		public string MessageId { get; set; }

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x0600595F RID: 22879 RVA: 0x001B1C5E File Offset: 0x001B0E5E
		// (set) Token: 0x06005960 RID: 22880 RVA: 0x001B1C66 File Offset: 0x001B0E66
		public string Justification { get; set; }
	}
}
