using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000056 RID: 86
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000AF0B5 File Offset: 0x000AE2B5
		private BadImageFormatException(string fileName, int hResult) : base(null)
		{
			base.HResult = hResult;
			this._fileName = fileName;
			this.SetMessageField();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000AF0D2 File Offset: 0x000AE2D2
		public BadImageFormatException() : base(SR.Arg_BadImageFormatException)
		{
			base.HResult = -2147024885;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000AF0EA File Offset: 0x000AE2EA
		public BadImageFormatException(string message) : base(message)
		{
			base.HResult = -2147024885;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000AF0FE File Offset: 0x000AE2FE
		public BadImageFormatException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024885;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000AF113 File Offset: 0x000AE313
		public BadImageFormatException(string message, string fileName) : base(message)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000AF12E File Offset: 0x000AE32E
		public BadImageFormatException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000AF14A File Offset: 0x000AE34A
		[NullableContext(1)]
		protected BadImageFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._fileName = info.GetString("BadImageFormat_FileName");
			this._fusionLog = info.GetString("BadImageFormat_FusionLog");
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000AF176 File Offset: 0x000AE376
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this._fileName, typeof(string));
			info.AddValue("BadImageFormat_FusionLog", this._fusionLog, typeof(string));
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000AF1B6 File Offset: 0x000AE3B6
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000AF1C4 File Offset: 0x000AE3C4
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = SR.Arg_BadImageFormatException;
					return;
				}
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000AF211 File Offset: 0x000AE411
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000AF21C File Offset: 0x000AE41C
		[NullableContext(1)]
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (!string.IsNullOrEmpty(this._fileName))
			{
				text = text + "\r\n" + SR.Format(SR.IO_FileName_Name, this._fileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + "\r\n" + this.StackTrace;
			}
			if (this._fusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text = text + "\r\n\r\n" + this._fusionLog;
			}
			return text;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000AF2CB File Offset: 0x000AE4CB
		public string FusionLog
		{
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x040000D9 RID: 217
		private readonly string _fileName;

		// Token: 0x040000DA RID: 218
		private readonly string _fusionLog;
	}
}
