using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000528 RID: 1320
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public abstract class CustomConstantAttribute : Attribute
	{
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600471B RID: 18203
		public abstract object Value { get; }
	}
}
