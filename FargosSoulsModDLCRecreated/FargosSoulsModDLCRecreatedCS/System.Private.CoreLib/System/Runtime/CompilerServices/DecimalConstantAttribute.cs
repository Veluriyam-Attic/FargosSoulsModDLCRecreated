using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200052A RID: 1322
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class DecimalConstantAttribute : Attribute
	{
		// Token: 0x0600471F RID: 18207 RVA: 0x0017D508 File Offset: 0x0017C708
		[CLSCompliant(false)]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this._dec = new decimal((int)low, (int)mid, (int)hi, sign > 0, scale);
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x0017D508 File Offset: 0x0017C708
		public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
		{
			this._dec = new decimal(low, mid, hi, sign > 0, scale);
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x0017D525 File Offset: 0x0017C725
		public decimal Value
		{
			get
			{
				return this._dec;
			}
		}

		// Token: 0x04001114 RID: 4372
		private readonly decimal _dec;
	}
}
