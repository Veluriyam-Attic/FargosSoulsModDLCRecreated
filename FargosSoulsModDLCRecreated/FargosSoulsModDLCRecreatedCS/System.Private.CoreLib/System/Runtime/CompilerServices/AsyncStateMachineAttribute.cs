using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000502 RID: 1282
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x0600465B RID: 18011 RVA: 0x0017ADC2 File Offset: 0x00179FC2
		[NullableContext(1)]
		public AsyncStateMachineAttribute(Type stateMachineType) : base(stateMachineType)
		{
		}
	}
}
