using System;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004B4 RID: 1204
	internal class ExpandoToDispatchExMarshaler : ICustomMarshaler
	{
		// Token: 0x06004528 RID: 17704 RVA: 0x0017A255 File Offset: 0x00179455
		public static ICustomMarshaler GetInstance(string cookie)
		{
			return ExpandoToDispatchExMarshaler.s_ExpandoToDispatchExMarshaler;
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private ExpandoToDispatchExMarshaler()
		{
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void CleanUpManagedData(object ManagedObj)
		{
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void CleanUpNativeData(IntPtr pNativeData)
		{
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x0011DE1A File Offset: 0x0011D01A
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x0017A25C File Offset: 0x0017945C
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_IExpando);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x0017A25C File Offset: 0x0017945C
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_IExpando);
		}

		// Token: 0x04000FD6 RID: 4054
		private static readonly ExpandoToDispatchExMarshaler s_ExpandoToDispatchExMarshaler = new ExpandoToDispatchExMarshaler();
	}
}
