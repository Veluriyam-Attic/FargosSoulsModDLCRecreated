using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000644 RID: 1604
	internal sealed class PunkSafeHandle : SafeHandle
	{
		// Token: 0x060050B7 RID: 20663 RVA: 0x00193694 File Offset: 0x00192894
		protected override bool ReleaseHandle()
		{
			PunkSafeHandle.m_Release(this.handle);
			return true;
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060050B8 RID: 20664 RVA: 0x001936A7 File Offset: 0x001928A7
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)0;
			}
		}

		// Token: 0x060050B9 RID: 20665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr nGetDReleaseTarget();

		// Token: 0x060050BA RID: 20666 RVA: 0x001936BA File Offset: 0x001928BA
		static PunkSafeHandle()
		{
			PunkSafeHandle.m_Release((IntPtr)0);
		}

		// Token: 0x040014B7 RID: 5303
		private static PunkSafeHandle.DRelease m_Release = (PunkSafeHandle.DRelease)Marshal.GetDelegateForFunctionPointer(PunkSafeHandle.nGetDReleaseTarget(), typeof(PunkSafeHandle.DRelease));

		// Token: 0x02000645 RID: 1605
		// (Invoke) Token: 0x060050BC RID: 20668
		private delegate void DRelease(IntPtr punk);
	}
}
