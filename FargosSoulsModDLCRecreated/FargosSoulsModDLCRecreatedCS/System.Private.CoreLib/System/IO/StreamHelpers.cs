using System;

namespace System.IO
{
	// Token: 0x020006BF RID: 1727
	internal static class StreamHelpers
	{
		// Token: 0x06005834 RID: 22580 RVA: 0x001AF650 File Offset: 0x001AE850
		public static void ValidateCopyToArgs(Stream source, Stream destination, int bufferSize)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", bufferSize, SR.ArgumentOutOfRange_NeedPosNum);
			}
			bool canRead = source.CanRead;
			if (!canRead && !source.CanWrite)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_StreamClosed);
			}
			bool canWrite = destination.CanWrite;
			if (!canWrite && !destination.CanRead)
			{
				throw new ObjectDisposedException("destination", SR.ObjectDisposed_StreamClosed);
			}
			if (!canRead)
			{
				throw new NotSupportedException(SR.NotSupported_UnreadableStream);
			}
			if (!canWrite)
			{
				throw new NotSupportedException(SR.NotSupported_UnwritableStream);
			}
		}
	}
}
