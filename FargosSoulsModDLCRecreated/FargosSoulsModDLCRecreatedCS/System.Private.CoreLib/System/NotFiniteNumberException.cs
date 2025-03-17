using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000155 RID: 341
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x000DEE09 File Offset: 0x000DE009
		public NotFiniteNumberException() : base(SR.Arg_NotFiniteNumberException)
		{
			this._offendingNumber = 0.0;
			base.HResult = -2146233048;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000DEE30 File Offset: 0x000DE030
		public NotFiniteNumberException(double offendingNumber)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000DEE4A File Offset: 0x000DE04A
		public NotFiniteNumberException(string message) : base(message)
		{
			this._offendingNumber = 0.0;
			base.HResult = -2146233048;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000DEE6D File Offset: 0x000DE06D
		public NotFiniteNumberException(string message, double offendingNumber) : base(message)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000DEE88 File Offset: 0x000DE088
		public NotFiniteNumberException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233048;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000DEE9D File Offset: 0x000DE09D
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException) : base(message, innerException)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000DEEB9 File Offset: 0x000DE0B9
		[NullableContext(1)]
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._offendingNumber = info.GetDouble("OffendingNumber");
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000DEED4 File Offset: 0x000DE0D4
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this._offendingNumber, typeof(double));
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000DEEFE File Offset: 0x000DE0FE
		public double OffendingNumber
		{
			get
			{
				return this._offendingNumber;
			}
		}

		// Token: 0x04000426 RID: 1062
		private readonly double _offendingNumber;
	}
}
