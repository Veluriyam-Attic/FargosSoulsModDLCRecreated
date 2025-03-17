using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000670 RID: 1648
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class FileLoadException : IOException
	{
		// Token: 0x060053E8 RID: 21480 RVA: 0x0019C9B8 File Offset: 0x0019BBB8
		private FileLoadException(string fileName, int hResult) : base(null)
		{
			base.HResult = hResult;
			this.FileName = fileName;
			this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult);
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x0019C9E8 File Offset: 0x0019BBE8
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			string format = null;
			FileLoadException.GetFileLoadExceptionMessage(hResult, new StringHandleOnStack(ref format));
			string arg = null;
			if (hResult == -2147024703)
			{
				arg = SR.Arg_BadImageFormatException;
			}
			else
			{
				FileLoadException.GetMessageForHR(hResult, new StringHandleOnStack(ref arg));
			}
			return string.Format(format, fileName, arg);
		}

		// Token: 0x060053EA RID: 21482
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFileLoadExceptionMessage(int hResult, StringHandleOnStack retString);

		// Token: 0x060053EB RID: 21483
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMessageForHR(int hresult, StringHandleOnStack retString);

		// Token: 0x060053EC RID: 21484 RVA: 0x0019CA2B File Offset: 0x0019BC2B
		public FileLoadException() : base(SR.IO_FileLoad)
		{
			base.HResult = -2146232799;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x0019CA43 File Offset: 0x0019BC43
		public FileLoadException(string message) : base(message)
		{
			base.HResult = -2146232799;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0019CA57 File Offset: 0x0019BC57
		public FileLoadException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232799;
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x0019CA6C File Offset: 0x0019BC6C
		public FileLoadException(string message, string fileName) : base(message)
		{
			base.HResult = -2146232799;
			this.FileName = fileName;
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0019CA87 File Offset: 0x0019BC87
		public FileLoadException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232799;
			this.FileName = fileName;
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x0019CAA4 File Offset: 0x0019BCA4
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				string result;
				if ((result = this._message) == null)
				{
					result = (this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult));
				}
				return result;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x0019CAD5 File Offset: 0x0019BCD5
		public string FileName { get; }

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060053F3 RID: 21491 RVA: 0x0019CADD File Offset: 0x0019BCDD
		public string FusionLog { get; }

		// Token: 0x060053F4 RID: 21492 RVA: 0x0019CAE8 File Offset: 0x0019BCE8
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

		// Token: 0x060053F5 RID: 21493 RVA: 0x0019CB97 File Offset: 0x0019BD97
		[NullableContext(1)]
		protected FileLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.FileName = info.GetString("FileLoad_FileName");
			this.FusionLog = info.GetString("FileLoad_FusionLog");
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0019CBC3 File Offset: 0x0019BDC3
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this.FileName, typeof(string));
			info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
		}
	}
}
