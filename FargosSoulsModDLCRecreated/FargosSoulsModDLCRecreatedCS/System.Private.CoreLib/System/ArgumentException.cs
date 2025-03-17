using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C5 RID: 197
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class ArgumentException : SystemException
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x000C8798 File Offset: 0x000C7998
		public ArgumentException() : base(SR.Arg_ArgumentException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000C87B0 File Offset: 0x000C79B0
		public ArgumentException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000C87C4 File Offset: 0x000C79C4
		public ArgumentException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000C87D9 File Offset: 0x000C79D9
		public ArgumentException(string message, string paramName, Exception innerException) : base(message, innerException)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000C87F5 File Offset: 0x000C79F5
		public ArgumentException(string message, string paramName) : base(message)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000C8810 File Offset: 0x000C7A10
		[NullableContext(1)]
		protected ArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._paramName = info.GetString("ParamName");
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000C882B File Offset: 0x000C7A2B
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this._paramName, typeof(string));
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x000C8850 File Offset: 0x000C7A50
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				this.SetMessageField();
				string text = base.Message;
				if (!string.IsNullOrEmpty(this._paramName))
				{
					text = text + " " + SR.Format(SR.Arg_ParamName_Name, this._paramName);
				}
				return text;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000C8894 File Offset: 0x000C7A94
		private void SetMessageField()
		{
			if (this._message == null && base.HResult == -2147024809)
			{
				this._message = SR.Arg_ArgumentException;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000C88B6 File Offset: 0x000C7AB6
		public virtual string ParamName
		{
			get
			{
				return this._paramName;
			}
		}

		// Token: 0x04000273 RID: 627
		private readonly string _paramName;
	}
}
