using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000151 RID: 337
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x000DEC63 File Offset: 0x000DDE63
		public MissingFieldException() : base(SR.Arg_MissingFieldException)
		{
			base.HResult = -2146233071;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000DEC7B File Offset: 0x000DDE7B
		[NullableContext(2)]
		public MissingFieldException(string message) : base(message)
		{
			base.HResult = -2146233071;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000DEC8F File Offset: 0x000DDE8F
		[NullableContext(2)]
		public MissingFieldException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233071;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000DECA4 File Offset: 0x000DDEA4
		[NullableContext(2)]
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000DECBA File Offset: 0x000DDEBA
		protected MissingFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000DECC4 File Offset: 0x000DDEC4
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format(SR.MissingField_Name, ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName);
			}
		}
	}
}
