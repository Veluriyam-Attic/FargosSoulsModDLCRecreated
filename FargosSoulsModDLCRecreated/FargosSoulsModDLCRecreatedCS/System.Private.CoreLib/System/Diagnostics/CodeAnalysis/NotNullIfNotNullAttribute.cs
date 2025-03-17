using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F7 RID: 1783
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	public sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x0600594A RID: 22858 RVA: 0x001B1B63 File Offset: 0x001B0D63
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600594B RID: 22859 RVA: 0x001B1B72 File Offset: 0x001B0D72
		public string ParameterName { get; }
	}
}
