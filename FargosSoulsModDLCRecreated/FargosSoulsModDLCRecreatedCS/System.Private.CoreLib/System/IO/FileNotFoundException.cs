using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000671 RID: 1649
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		// Token: 0x060053F7 RID: 21495 RVA: 0x0019CC03 File Offset: 0x0019BE03
		private FileNotFoundException(string fileName, int hResult) : base(null)
		{
			base.HResult = hResult;
			this.FileName = fileName;
			this.SetMessageField();
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0019CC20 File Offset: 0x0019BE20
		public FileNotFoundException() : base(SR.IO_FileNotFound)
		{
			base.HResult = -2147024894;
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x0019CC38 File Offset: 0x0019BE38
		public FileNotFoundException(string message) : base(message)
		{
			base.HResult = -2147024894;
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x0019CC4C File Offset: 0x0019BE4C
		public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024894;
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0019CC61 File Offset: 0x0019BE61
		public FileNotFoundException(string message, string fileName) : base(message)
		{
			base.HResult = -2147024894;
			this.FileName = fileName;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0019CC7C File Offset: 0x0019BE7C
		public FileNotFoundException(string message, string fileName, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024894;
			this.FileName = fileName;
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060053FD RID: 21501 RVA: 0x0019CC98 File Offset: 0x0019BE98
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

		// Token: 0x060053FE RID: 21502 RVA: 0x0019CCA8 File Offset: 0x0019BEA8
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this.FileName == null && base.HResult == -2146233088)
				{
					this._message = SR.IO_FileNotFound;
					return;
				}
				if (this.FileName != null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult);
				}
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x0019CCFD File Offset: 0x0019BEFD
		public string FileName { get; }

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x0019CD05 File Offset: 0x0019BF05
		public string FusionLog { get; }

		// Token: 0x06005401 RID: 21505 RVA: 0x0019CD10 File Offset: 0x0019BF10
		[NullableContext(1)]
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (!string.IsNullOrEmpty(this.FileName))
			{
				text = text + "\r\n" + SR.Format(SR.IO_FileName_Name, this.FileName);
			}
			if (base.InnerException != null)
			{
				text = text + "\r\n ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + "\r\n" + this.StackTrace;
			}
			if (this.FusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text = text + "\r\n\r\n" + this.FusionLog;
			}
			return text;
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0019CDBF File Offset: 0x0019BFBF
		[NullableContext(1)]
		protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.FileName = info.GetString("FileNotFound_FileName");
			this.FusionLog = info.GetString("FileNotFound_FusionLog");
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0019CDEB File Offset: 0x0019BFEB
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this.FileName, typeof(string));
			info.AddValue("FileNotFound_FusionLog", this.FusionLog, typeof(string));
		}
	}
}
