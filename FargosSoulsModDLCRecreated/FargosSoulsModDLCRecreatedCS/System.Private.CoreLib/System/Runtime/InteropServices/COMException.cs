using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000470 RID: 1136
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class COMException : ExternalException
	{
		// Token: 0x0600444C RID: 17484 RVA: 0x00178DE9 File Offset: 0x00177FE9
		public COMException() : base(SR.Arg_COMException)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00178E01 File Offset: 0x00178001
		public COMException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x00178E15 File Offset: 0x00178015
		public COMException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x00178E2A File Offset: 0x0017802A
		public COMException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x00178E3A File Offset: 0x0017803A
		[NullableContext(1)]
		protected COMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00178E44 File Offset: 0x00178044
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = base.GetType().ToString();
			stringBuilder.Append(value).Append(" (0x").Append(base.HResult.ToString("X8", CultureInfo.InvariantCulture)).Append(')');
			string message = this.Message;
			if (!string.IsNullOrEmpty(message))
			{
				stringBuilder.Append(": ").Append(message);
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				stringBuilder.Append("\r\n ---> ").Append(innerException.ToString());
			}
			string stackTrace = this.StackTrace;
			if (stackTrace != null)
			{
				stringBuilder.AppendLine().Append(stackTrace);
			}
			return stringBuilder.ToString();
		}
	}
}
