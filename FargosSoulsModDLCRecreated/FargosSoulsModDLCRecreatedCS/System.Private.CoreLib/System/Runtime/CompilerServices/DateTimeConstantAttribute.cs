using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000529 RID: 1321
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x0600471D RID: 18205 RVA: 0x0017D4E7 File Offset: 0x0017C6E7
		public DateTimeConstantAttribute(long ticks)
		{
			this._date = new DateTime(ticks);
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x0017D4FB File Offset: 0x0017C6FB
		public override object Value
		{
			get
			{
				return this._date;
			}
		}

		// Token: 0x04001113 RID: 4371
		private readonly DateTime _date;
	}
}
