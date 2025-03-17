using System;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x020003A3 RID: 931
	internal sealed class HandleMarshaler
	{
		// Token: 0x060030AF RID: 12463 RVA: 0x00167FDE File Offset: 0x001671DE
		internal static IntPtr ConvertSafeHandleToNative(SafeHandle handle, ref CleanupWorkListElement cleanupWorkList)
		{
			if (Unsafe.IsNullRef<CleanupWorkListElement>(ref cleanupWorkList))
			{
				throw new InvalidOperationException(SR.Interop_Marshal_SafeHandle_InvalidOperation);
			}
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			return StubHelpers.AddToCleanupList(ref cleanupWorkList, handle);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x00168008 File Offset: 0x00167208
		internal static void ThrowSafeHandleFieldChanged()
		{
			throw new NotSupportedException(SR.Interop_Marshal_CannotCreateSafeHandleField);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x00168014 File Offset: 0x00167214
		internal static void ThrowCriticalHandleFieldChanged()
		{
			throw new NotSupportedException(SR.Interop_Marshal_CannotCreateCriticalHandleField);
		}
	}
}
