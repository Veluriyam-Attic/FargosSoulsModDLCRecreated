using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000482 RID: 1154
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ExternalException : SystemException
	{
		// Token: 0x06004479 RID: 17529 RVA: 0x00179204 File Offset: 0x00178404
		public ExternalException() : base(SR.Arg_ExternalException)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x0017921C File Offset: 0x0017841C
		public ExternalException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x00179230 File Offset: 0x00178430
		public ExternalException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x000DB07D File Offset: 0x000DA27D
		public ExternalException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected ExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x00179245 File Offset: 0x00178445
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x00179250 File Offset: 0x00178450
		[NullableContext(1)]
		public override string ToString()
		{
			string message = this.Message;
			string str = base.GetType().ToString();
			string text = str + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + "\r\n ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + "\r\n" + this.StackTrace;
			}
			return text;
		}
	}
}
