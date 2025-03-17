using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004EB RID: 1259
	internal struct DependentHandle
	{
		// Token: 0x060045E0 RID: 17888 RVA: 0x0017A2D4 File Offset: 0x001794D4
		public DependentHandle(object primary, object secondary)
		{
			this._handle = DependentHandle.nInitialize(primary, secondary);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060045E1 RID: 17889 RVA: 0x0017A2E3 File Offset: 0x001794E3
		public bool IsAllocated
		{
			get
			{
				return this._handle != IntPtr.Zero;
			}
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x0017A2F5 File Offset: 0x001794F5
		public object GetPrimary()
		{
			return DependentHandle.nGetPrimary(this._handle);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x0017A302 File Offset: 0x00179502
		public object GetPrimaryAndSecondary(out object secondary)
		{
			return DependentHandle.nGetPrimaryAndSecondary(this._handle, out secondary);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x0017A310 File Offset: 0x00179510
		public void SetPrimary(object primary)
		{
			DependentHandle.nSetPrimary(this._handle, primary);
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x0017A31E File Offset: 0x0017951E
		public void SetSecondary(object secondary)
		{
			DependentHandle.nSetSecondary(this._handle, secondary);
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x0017A32C File Offset: 0x0017952C
		public void Free()
		{
			if (this._handle != IntPtr.Zero)
			{
				IntPtr handle = this._handle;
				this._handle = IntPtr.Zero;
				DependentHandle.nFree(handle);
			}
		}

		// Token: 0x060045E7 RID: 17895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr nInitialize(object primary, object secondary);

		// Token: 0x060045E8 RID: 17896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nGetPrimary(IntPtr dependentHandle);

		// Token: 0x060045E9 RID: 17897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nGetPrimaryAndSecondary(IntPtr dependentHandle, out object secondary);

		// Token: 0x060045EA RID: 17898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nSetPrimary(IntPtr dependentHandle, object primary);

		// Token: 0x060045EB RID: 17899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nSetSecondary(IntPtr dependentHandle, object secondary);

		// Token: 0x060045EC RID: 17900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nFree(IntPtr dependentHandle);

		// Token: 0x040010AC RID: 4268
		private IntPtr _handle;
	}
}
