using System;
using System.Collections;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004AE RID: 1198
	internal class EnumerableToDispatchMarshaler : ICustomMarshaler
	{
		// Token: 0x06004509 RID: 17673 RVA: 0x00179F30 File Offset: 0x00179130
		public static ICustomMarshaler GetInstance(string cookie)
		{
			return EnumerableToDispatchMarshaler.s_enumerableToDispatchMarshaler;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private EnumerableToDispatchMarshaler()
		{
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void CleanUpManagedData(object ManagedObj)
		{
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x00179F37 File Offset: 0x00179137
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			Marshal.Release(pNativeData);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x0011DE1A File Offset: 0x0011D01A
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00179F40 File Offset: 0x00179140
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			if (ManagedObj == null)
			{
				throw new ArgumentNullException("ManagedObj");
			}
			return Marshal.GetComInterfaceForObject<object, IEnumerable>(ManagedObj);
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00179F58 File Offset: 0x00179158
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			if (pNativeData == IntPtr.Zero)
			{
				throw new ArgumentNullException("pNativeData");
			}
			object objectForIUnknown = Marshal.GetObjectForIUnknown(pNativeData);
			return ComDataHelpers.GetOrCreateManagedViewFromComData<object, EnumerableViewOfDispatch>(objectForIUnknown, (object obj) => new EnumerableViewOfDispatch(obj));
		}

		// Token: 0x04000FC9 RID: 4041
		private static readonly EnumerableToDispatchMarshaler s_enumerableToDispatchMarshaler = new EnumerableToDispatchMarshaler();
	}
}
