using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000560 RID: 1376
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		// Token: 0x06004794 RID: 18324 RVA: 0x0017DC2D File Offset: 0x0017CE2D
		public TypeForwardedToAttribute(Type destination)
		{
			this.Destination = destination;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x0017DC3C File Offset: 0x0017CE3C
		public Type Destination { get; }
	}
}
