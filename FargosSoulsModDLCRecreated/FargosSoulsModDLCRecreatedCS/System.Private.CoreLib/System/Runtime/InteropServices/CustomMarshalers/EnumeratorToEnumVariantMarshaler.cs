using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004B1 RID: 1201
	internal class EnumeratorToEnumVariantMarshaler : ICustomMarshaler
	{
		// Token: 0x06004518 RID: 17688 RVA: 0x0017A0A8 File Offset: 0x001792A8
		public static ICustomMarshaler GetInstance(string cookie)
		{
			return EnumeratorToEnumVariantMarshaler.s_enumeratorToEnumVariantMarshaler;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private EnumeratorToEnumVariantMarshaler()
		{
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void CleanUpManagedData(object ManagedObj)
		{
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x00179F37 File Offset: 0x00179137
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			Marshal.Release(pNativeData);
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x0011DE1A File Offset: 0x0011D01A
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x0017A0B0 File Offset: 0x001792B0
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			if (ManagedObj == null)
			{
				throw new ArgumentNullException("ManagedObj");
			}
			EnumeratorViewOfEnumVariant enumeratorViewOfEnumVariant = ManagedObj as EnumeratorViewOfEnumVariant;
			if (enumeratorViewOfEnumVariant != null)
			{
				return Marshal.GetComInterfaceForObject<object, IEnumVARIANT>(enumeratorViewOfEnumVariant.GetUnderlyingObject());
			}
			EnumVariantViewOfEnumerator o = new EnumVariantViewOfEnumerator((System.Collections.IEnumerator)ManagedObj);
			return Marshal.GetComInterfaceForObject<EnumVariantViewOfEnumerator, IEnumVARIANT>(o);
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x0017A0F4 File Offset: 0x001792F4
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			if (pNativeData == IntPtr.Zero)
			{
				throw new ArgumentNullException("pNativeData");
			}
			object objectForIUnknown = Marshal.GetObjectForIUnknown(pNativeData);
			if (objectForIUnknown.GetType().IsCOMObject)
			{
				return ComDataHelpers.GetOrCreateManagedViewFromComData<IEnumVARIANT, EnumeratorViewOfEnumVariant>(objectForIUnknown, (IEnumVARIANT var) => new EnumeratorViewOfEnumVariant(var));
			}
			EnumVariantViewOfEnumerator enumVariantViewOfEnumerator = objectForIUnknown as EnumVariantViewOfEnumerator;
			if (enumVariantViewOfEnumerator != null)
			{
				return enumVariantViewOfEnumerator.Enumerator;
			}
			return objectForIUnknown as System.Collections.IEnumerator;
		}

		// Token: 0x04000FCF RID: 4047
		private static readonly EnumeratorToEnumVariantMarshaler s_enumeratorToEnumVariantMarshaler = new EnumeratorToEnumVariantMarshaler();
	}
}
