using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A8 RID: 1192
	public class StandardOleMarshalObject : MarshalByRefObject, IMarshal
	{
		// Token: 0x060044EE RID: 17646 RVA: 0x000C45E4 File Offset: 0x000C37E4
		protected StandardOleMarshalObject()
		{
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x00179BC8 File Offset: 0x00178DC8
		private IntPtr GetStdMarshaler(ref Guid riid, int dwDestContext, int mshlflags)
		{
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this);
			if (iunknownForObject != IntPtr.Zero)
			{
				try
				{
					IntPtr zero = IntPtr.Zero;
					if (Interop.Ole32.CoGetStandardMarshal(ref riid, iunknownForObject, dwDestContext, IntPtr.Zero, mshlflags, out zero) == 0)
					{
						return zero;
					}
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			throw new InvalidOperationException(SR.Format(SR.StandardOleMarshalObjectGetMarshalerFailed, riid));
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x00179C40 File Offset: 0x00178E40
		int IMarshal.GetUnmarshalClass(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out Guid pCid)
		{
			pCid = StandardOleMarshalObject.CLSID_StdMarshal;
			return 0;
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x00179C50 File Offset: 0x00178E50
		unsafe int IMarshal.GetMarshalSizeMax(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize)
		{
			IntPtr stdMarshaler = this.GetStdMarshaler(ref riid, dwDestContext, mshlflags);
			int result;
			try
			{
				IntPtr intPtr = *(IntPtr*)stdMarshaler.ToPointer();
				IntPtr ptr = *(IntPtr*)((byte*)intPtr.ToPointer() + (IntPtr)4 * (IntPtr)sizeof(IntPtr));
				StandardOleMarshalObject.GetMarshalSizeMaxDelegate getMarshalSizeMaxDelegate = (StandardOleMarshalObject.GetMarshalSizeMaxDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(StandardOleMarshalObject.GetMarshalSizeMaxDelegate));
				result = getMarshalSizeMaxDelegate(stdMarshaler, ref riid, pv, dwDestContext, pvDestContext, mshlflags, out pSize);
			}
			finally
			{
				Marshal.Release(stdMarshaler);
			}
			return result;
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x00179CC8 File Offset: 0x00178EC8
		unsafe int IMarshal.MarshalInterface(IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags)
		{
			IntPtr stdMarshaler = this.GetStdMarshaler(ref riid, dwDestContext, mshlflags);
			int result;
			try
			{
				IntPtr intPtr = *(IntPtr*)stdMarshaler.ToPointer();
				IntPtr ptr = *(IntPtr*)((byte*)intPtr.ToPointer() + (IntPtr)5 * (IntPtr)sizeof(IntPtr));
				StandardOleMarshalObject.MarshalInterfaceDelegate marshalInterfaceDelegate = (StandardOleMarshalObject.MarshalInterfaceDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(StandardOleMarshalObject.MarshalInterfaceDelegate));
				result = marshalInterfaceDelegate(stdMarshaler, pStm, ref riid, pv, dwDestContext, pvDestContext, mshlflags);
			}
			finally
			{
				Marshal.Release(stdMarshaler);
			}
			return result;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x00179D44 File Offset: 0x00178F44
		int IMarshal.UnmarshalInterface(IntPtr pStm, ref Guid riid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			return -2147467263;
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x00179D52 File Offset: 0x00178F52
		int IMarshal.ReleaseMarshalData(IntPtr pStm)
		{
			return -2147467263;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x00179D52 File Offset: 0x00178F52
		int IMarshal.DisconnectObject(int dwReserved)
		{
			return -2147467263;
		}

		// Token: 0x04000FC7 RID: 4039
		private static readonly Guid CLSID_StdMarshal = new Guid("00000017-0000-0000-c000-000000000046");

		// Token: 0x020004A9 RID: 1193
		// (Invoke) Token: 0x060044F8 RID: 17656
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int GetMarshalSizeMaxDelegate(IntPtr _this, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize);

		// Token: 0x020004AA RID: 1194
		// (Invoke) Token: 0x060044FA RID: 17658
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int MarshalInterfaceDelegate(IntPtr _this, IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags);
	}
}
