using System;
using System.Runtime.Serialization;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000554 RID: 1364
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("System.Runtime.Extensions, Version=4.2.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public sealed class SwitchExpressionException : InvalidOperationException
	{
		// Token: 0x06004766 RID: 18278 RVA: 0x0017D723 File Offset: 0x0017C923
		public SwitchExpressionException() : base(SR.Arg_SwitchExpressionException)
		{
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x0017D730 File Offset: 0x0017C930
		public SwitchExpressionException(Exception innerException) : base(SR.Arg_SwitchExpressionException, innerException)
		{
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x0017D73E File Offset: 0x0017C93E
		public SwitchExpressionException(object unmatchedValue) : this()
		{
			this.UnmatchedValue = unmatchedValue;
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0017D74D File Offset: 0x0017C94D
		private SwitchExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.UnmatchedValue = info.GetValue("UnmatchedValue", typeof(object));
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0017D772 File Offset: 0x0017C972
		public SwitchExpressionException(string message) : base(message)
		{
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x0017D77B File Offset: 0x0017C97B
		public SwitchExpressionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x0017D785 File Offset: 0x0017C985
		public object UnmatchedValue { get; }

		// Token: 0x0600476D RID: 18285 RVA: 0x0017D78D File Offset: 0x0017C98D
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("UnmatchedValue", this.UnmatchedValue, typeof(object));
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x0017D7B4 File Offset: 0x0017C9B4
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				if (this.UnmatchedValue == null)
				{
					return base.Message;
				}
				string str = SR.Format(SR.SwitchExpressionException_UnmatchedValue, this.UnmatchedValue);
				return base.Message + Environment.NewLine + str;
			}
		}
	}
}
