using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200050E RID: 1294
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public sealed class CallerArgumentExpressionAttribute : Attribute
	{
		// Token: 0x060046C3 RID: 18115 RVA: 0x0017C20E File Offset: 0x0017B40E
		public CallerArgumentExpressionAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x0017C21D File Offset: 0x0017B41D
		public string ParameterName { get; }
	}
}
