using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F9 RID: 1785
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x0600594D RID: 22861 RVA: 0x001B1B7A File Offset: 0x001B0D7A
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600594E RID: 22862 RVA: 0x001B1B89 File Offset: 0x001B0D89
		public bool ParameterValue { get; }
	}
}
