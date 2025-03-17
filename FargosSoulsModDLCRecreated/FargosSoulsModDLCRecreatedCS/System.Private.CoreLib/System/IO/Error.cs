using System;

namespace System.IO
{
	// Token: 0x0200068A RID: 1674
	internal static class Error
	{
		// Token: 0x06005536 RID: 21814 RVA: 0x001A1B74 File Offset: 0x001A0D74
		internal static Exception GetStreamIsClosed()
		{
			return new ObjectDisposedException(null, SR.ObjectDisposed_StreamClosed);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x001A1B81 File Offset: 0x001A0D81
		internal static Exception GetEndOfFile()
		{
			return new EndOfStreamException(SR.IO_EOF_ReadBeyondEOF);
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x001A1B8D File Offset: 0x001A0D8D
		internal static Exception GetFileNotOpen()
		{
			return new ObjectDisposedException(null, SR.ObjectDisposed_FileClosed);
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x001A1B9A File Offset: 0x001A0D9A
		internal static Exception GetReadNotSupported()
		{
			return new NotSupportedException(SR.NotSupported_UnreadableStream);
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x001A1BA6 File Offset: 0x001A0DA6
		internal static Exception GetSeekNotSupported()
		{
			return new NotSupportedException(SR.NotSupported_UnseekableStream);
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x001A1BB2 File Offset: 0x001A0DB2
		internal static Exception GetWriteNotSupported()
		{
			return new NotSupportedException(SR.NotSupported_UnwritableStream);
		}
	}
}
