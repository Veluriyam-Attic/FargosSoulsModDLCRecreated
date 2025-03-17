using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C8 RID: 1224
	[NullableContext(1)]
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IStream
	{
		// Token: 0x06004584 RID: 17796
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x06004585 RID: 17797
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x06004586 RID: 17798
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x06004587 RID: 17799
		void SetSize(long libNewSize);

		// Token: 0x06004588 RID: 17800
		void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x06004589 RID: 17801
		void Commit(int grfCommitFlags);

		// Token: 0x0600458A RID: 17802
		void Revert();

		// Token: 0x0600458B RID: 17803
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x0600458C RID: 17804
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x0600458D RID: 17805
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x0600458E RID: 17806
		void Clone(out IStream ppstm);
	}
}
