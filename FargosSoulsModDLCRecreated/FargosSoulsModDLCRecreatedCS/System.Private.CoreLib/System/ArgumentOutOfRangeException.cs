using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C7 RID: 199
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ArgumentOutOfRangeException : ArgumentException
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x000C8923 File Offset: 0x000C7B23
		public ArgumentOutOfRangeException() : base(SR.Arg_ArgumentOutOfRangeException)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000C893B File Offset: 0x000C7B3B
		public ArgumentOutOfRangeException(string paramName) : base(SR.Arg_ArgumentOutOfRangeException, paramName)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000C8954 File Offset: 0x000C7B54
		public ArgumentOutOfRangeException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000C8969 File Offset: 0x000C7B69
		public ArgumentOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000C897E File Offset: 0x000C7B7E
		public ArgumentOutOfRangeException(string paramName, object actualValue, string message) : base(message, paramName)
		{
			this._actualValue = actualValue;
			base.HResult = -2146233086;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000C899A File Offset: 0x000C7B9A
		[NullableContext(1)]
		protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._actualValue = info.GetValue("ActualValue", typeof(object));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000C89BF File Offset: 0x000C7BBF
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ActualValue", this._actualValue, typeof(object));
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000C89E4 File Offset: 0x000C7BE4
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				string message = base.Message;
				if (this._actualValue == null)
				{
					return message;
				}
				string text = SR.Format(SR.ArgumentOutOfRange_ActualValue, this._actualValue);
				if (message == null)
				{
					return text;
				}
				return message + "\r\n" + text;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000C8A24 File Offset: 0x000C7C24
		public virtual object ActualValue
		{
			get
			{
				return this._actualValue;
			}
		}

		// Token: 0x04000274 RID: 628
		private readonly object _actualValue;
	}
}
