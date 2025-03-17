using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004B0 RID: 1200
	internal class EnumerableViewOfDispatch : ICustomAdapter, System.Collections.IEnumerable
	{
		// Token: 0x06004514 RID: 17684 RVA: 0x00179FC9 File Offset: 0x001791C9
		public EnumerableViewOfDispatch(object dispatch)
		{
			this._dispatch = dispatch;
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x00179FD8 File Offset: 0x001791D8
		private IDispatch Dispatch
		{
			get
			{
				return (IDispatch)this._dispatch;
			}
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x00179FE8 File Offset: 0x001791E8
		public unsafe System.Collections.IEnumerator GetEnumerator()
		{
			Variant variant;
			void* value = (void*)(&variant);
			DISPPARAMS dispparams = default(DISPPARAMS);
			Guid empty = Guid.Empty;
			this.Dispatch.Invoke(-4, ref empty, 1, InvokeFlags.DISPATCH_METHOD | InvokeFlags.DISPATCH_PROPERTYGET, ref dispparams, new IntPtr(value), IntPtr.Zero, IntPtr.Zero);
			IntPtr intPtr = IntPtr.Zero;
			System.Collections.IEnumerator result;
			try
			{
				object obj = variant.ToObject();
				IEnumVARIANT enumVARIANT = obj as IEnumVARIANT;
				if (enumVARIANT == null)
				{
					throw new InvalidOperationException(SR.InvalidOp_InvalidNewEnumVariant);
				}
				intPtr = Marshal.GetIUnknownForObject(enumVARIANT);
				result = (System.Collections.IEnumerator)EnumeratorToEnumVariantMarshaler.GetInstance(null).MarshalNativeToManaged(intPtr);
			}
			finally
			{
				variant.Clear();
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x0017A0A0 File Offset: 0x001792A0
		public object GetUnderlyingObject()
		{
			return this._dispatch;
		}

		// Token: 0x04000FCC RID: 4044
		private const int DISPID_NEWENUM = -4;

		// Token: 0x04000FCD RID: 4045
		private const int LCID_DEFAULT = 1;

		// Token: 0x04000FCE RID: 4046
		private readonly object _dispatch;
	}
}
