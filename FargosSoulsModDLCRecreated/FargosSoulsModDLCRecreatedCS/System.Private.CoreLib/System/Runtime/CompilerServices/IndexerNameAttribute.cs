using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000538 RID: 1336
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public sealed class IndexerNameAttribute : Attribute
	{
		// Token: 0x0600473D RID: 18237 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		[NullableContext(1)]
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
