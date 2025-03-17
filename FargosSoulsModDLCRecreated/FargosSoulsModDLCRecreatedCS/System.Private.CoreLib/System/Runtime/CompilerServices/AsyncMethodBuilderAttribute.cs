using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004FF RID: 1279
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		// Token: 0x06004650 RID: 18000 RVA: 0x0017ADCB File Offset: 0x00179FCB
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			this.BuilderType = builderType;
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x0017ADDA File Offset: 0x00179FDA
		public Type BuilderType { get; }
	}
}
