using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000152 RID: 338
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MissingMethodException : MissingMemberException
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x000DED24 File Offset: 0x000DDF24
		public MissingMethodException() : base(SR.Arg_MissingMethodException)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000DED3C File Offset: 0x000DDF3C
		[NullableContext(2)]
		public MissingMethodException(string message) : base(message)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000DED50 File Offset: 0x000DDF50
		[NullableContext(2)]
		public MissingMethodException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000DECA4 File Offset: 0x000DDEA4
		[NullableContext(2)]
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000DECBA File Offset: 0x000DDEBA
		protected MissingMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x000DED68 File Offset: 0x000DDF68
		public override string Message
		{
			get
			{
				if (this.ClassName != null)
				{
					return SR.Format(SR.MissingMethod_Name, this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
				}
				return base.Message;
			}
		}
	}
}
