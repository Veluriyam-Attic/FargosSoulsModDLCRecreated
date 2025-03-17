using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006FD RID: 1789
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		// Token: 0x06005961 RID: 22881 RVA: 0x001B1C6F File Offset: 0x001B0E6F
		[NullableContext(1)]
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06005962 RID: 22882 RVA: 0x001B1C85 File Offset: 0x001B0E85
		[Nullable(1)]
		public string Category { [NullableContext(1)] get; }

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06005963 RID: 22883 RVA: 0x001B1C8D File Offset: 0x001B0E8D
		[Nullable(1)]
		public string CheckId { [NullableContext(1)] get; }

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06005964 RID: 22884 RVA: 0x001B1C95 File Offset: 0x001B0E95
		// (set) Token: 0x06005965 RID: 22885 RVA: 0x001B1C9D File Offset: 0x001B0E9D
		public string Scope { get; set; }

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06005966 RID: 22886 RVA: 0x001B1CA6 File Offset: 0x001B0EA6
		// (set) Token: 0x06005967 RID: 22887 RVA: 0x001B1CAE File Offset: 0x001B0EAE
		public string Target { get; set; }

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06005968 RID: 22888 RVA: 0x001B1CB7 File Offset: 0x001B0EB7
		// (set) Token: 0x06005969 RID: 22889 RVA: 0x001B1CBF File Offset: 0x001B0EBF
		public string MessageId { get; set; }

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x0600596A RID: 22890 RVA: 0x001B1CC8 File Offset: 0x001B0EC8
		// (set) Token: 0x0600596B RID: 22891 RVA: 0x001B1CD0 File Offset: 0x001B0ED0
		public string Justification { get; set; }
	}
}
