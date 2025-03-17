using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200054F RID: 1359
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class StateMachineAttribute : Attribute
	{
		// Token: 0x0600475C RID: 18268 RVA: 0x0017D6E2 File Offset: 0x0017C8E2
		public StateMachineAttribute(Type stateMachineType)
		{
			this.StateMachineType = stateMachineType;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x0017D6F1 File Offset: 0x0017C8F1
		public Type StateMachineType { get; }
	}
}
