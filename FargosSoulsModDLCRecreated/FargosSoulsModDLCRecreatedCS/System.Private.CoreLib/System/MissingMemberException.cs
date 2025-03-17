using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000076 RID: 118
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MissingMemberException : MemberAccessException, ISerializable
	{
		// Token: 0x06000468 RID: 1128
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FormatSignature(byte[] signature);

		// Token: 0x06000469 RID: 1129 RVA: 0x000B7B63 File Offset: 0x000B6D63
		public MissingMemberException() : base(SR.Arg_MissingMemberException)
		{
			base.HResult = -2146233070;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000B7B7B File Offset: 0x000B6D7B
		public MissingMemberException(string message) : base(message)
		{
			base.HResult = -2146233070;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000B7B8F File Offset: 0x000B6D8F
		public MissingMemberException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233070;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000B7BA4 File Offset: 0x000B6DA4
		public MissingMemberException(string className, string memberName)
		{
			this.ClassName = className;
			this.MemberName = memberName;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000B7BBC File Offset: 0x000B6DBC
		[NullableContext(1)]
		protected MissingMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000B7C14 File Offset: 0x000B6E14
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName, typeof(string));
			info.AddValue("MMMemberName", this.MemberName, typeof(string));
			info.AddValue("MMSignature", this.Signature, typeof(byte[]));
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000B7C7C File Offset: 0x000B6E7C
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format(SR.MissingMember_Name, this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
			}
		}

		// Token: 0x04000189 RID: 393
		protected string ClassName;

		// Token: 0x0400018A RID: 394
		protected string MemberName;

		// Token: 0x0400018B RID: 395
		protected byte[] Signature;
	}
}
