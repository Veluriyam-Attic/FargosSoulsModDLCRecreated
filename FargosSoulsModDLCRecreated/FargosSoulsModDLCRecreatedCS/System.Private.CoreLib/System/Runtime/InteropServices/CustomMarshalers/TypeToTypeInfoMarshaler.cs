using System;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004B5 RID: 1205
	internal class TypeToTypeInfoMarshaler : ICustomMarshaler
	{
		// Token: 0x06004530 RID: 17712 RVA: 0x0017A274 File Offset: 0x00179474
		public static ICustomMarshaler GetInstance(string cookie)
		{
			return TypeToTypeInfoMarshaler.s_typeToTypeInfoMarshaler;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private TypeToTypeInfoMarshaler()
		{
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void CleanUpManagedData(object ManagedObj)
		{
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void CleanUpNativeData(IntPtr pNativeData)
		{
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0011DE1A File Offset: 0x0011D01A
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x0017A27B File Offset: 0x0017947B
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ITypeInfo);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x0017A27B File Offset: 0x0017947B
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ITypeInfo);
		}

		// Token: 0x04000FD7 RID: 4055
		private static readonly TypeToTypeInfoMarshaler s_typeToTypeInfoMarshaler = new TypeToTypeInfoMarshaler();
	}
}
