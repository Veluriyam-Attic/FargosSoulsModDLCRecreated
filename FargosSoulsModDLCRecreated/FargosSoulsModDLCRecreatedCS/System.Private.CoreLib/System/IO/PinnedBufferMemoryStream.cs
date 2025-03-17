using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020006A0 RID: 1696
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x0600564D RID: 22093 RVA: 0x001A7DF8 File Offset: 0x001A6FF8
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			this._array = array;
			this._pinningHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			int num = array.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(array))
			{
				byte* pointer = reference;
				base.Initialize(pointer, (long)num, (long)num, FileAccess.Read);
			}
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x001A7E41 File Offset: 0x001A7041
		public override int Read(Span<byte> buffer)
		{
			return base.ReadCore(buffer);
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x001A7E4A File Offset: 0x001A704A
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			base.WriteCore(buffer);
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x001A28EC File Offset: 0x001A1AEC
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x001A7E53 File Offset: 0x001A7053
		protected override void Dispose(bool disposing)
		{
			if (this._pinningHandle.IsAllocated)
			{
				this._pinningHandle.Free();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001877 RID: 6263
		private readonly byte[] _array;

		// Token: 0x04001878 RID: 6264
		private GCHandle _pinningHandle;
	}
}
