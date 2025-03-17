using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004FE RID: 1278
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncIteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x0600464F RID: 17999 RVA: 0x0017ADC2 File Offset: 0x00179FC2
		[NullableContext(1)]
		public AsyncIteratorStateMachineAttribute(Type stateMachineType) : base(stateMachineType)
		{
		}
	}
}
