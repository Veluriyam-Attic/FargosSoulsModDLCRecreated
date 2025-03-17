using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000542 RID: 1346
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class IteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06004747 RID: 18247 RVA: 0x0017ADC2 File Offset: 0x00179FC2
		[NullableContext(1)]
		public IteratorStateMachineAttribute(Type stateMachineType) : base(stateMachineType)
		{
		}
	}
}
