using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.StubHelpers
{
	// Token: 0x020003B2 RID: 946
	internal static class StubHelpers
	{
		// Token: 0x060030E5 RID: 12517
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitDeclaringType(IntPtr pMD);

		// Token: 0x060030E6 RID: 12518
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetNDirectTarget(IntPtr pMD);

		// Token: 0x060030E7 RID: 12519
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetDelegateTarget(Delegate pThis, ref IntPtr pStubArg);

		// Token: 0x060030E8 RID: 12520
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearLastError();

		// Token: 0x060030E9 RID: 12521
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastError();

		// Token: 0x060030EA RID: 12522
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ThrowInteropParamException(int resID, int paramIdx);

		// Token: 0x060030EB RID: 12523 RVA: 0x001685BC File Offset: 0x001677BC
		internal static IntPtr AddToCleanupList(ref CleanupWorkListElement pCleanupWorkList, SafeHandle handle)
		{
			SafeHandleCleanupWorkListElement safeHandleCleanupWorkListElement = new SafeHandleCleanupWorkListElement(handle);
			CleanupWorkListElement.AddToCleanupList(ref pCleanupWorkList, safeHandleCleanupWorkListElement);
			return safeHandleCleanupWorkListElement.AddRef();
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x001685E0 File Offset: 0x001677E0
		internal static void KeepAliveViaCleanupList(ref CleanupWorkListElement pCleanupWorkList, object obj)
		{
			KeepAliveCleanupWorkListElement newElement = new KeepAliveCleanupWorkListElement(obj);
			CleanupWorkListElement.AddToCleanupList(ref pCleanupWorkList, newElement);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x001685FB File Offset: 0x001677FB
		internal static void DestroyCleanupList(ref CleanupWorkListElement pCleanupWorkList)
		{
			if (pCleanupWorkList != null)
			{
				pCleanupWorkList.Destroy();
				pCleanupWorkList = null;
			}
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x0016860C File Offset: 0x0016780C
		internal static Exception GetHRExceptionObject(int hr)
		{
			Exception ex = StubHelpers.InternalGetHRExceptionObject(hr);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x060030EF RID: 12527
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception InternalGetHRExceptionObject(int hr);

		// Token: 0x060030F0 RID: 12528 RVA: 0x00168628 File Offset: 0x00167828
		internal static Exception GetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis)
		{
			Exception ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x060030F1 RID: 12529
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception InternalGetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis);

		// Token: 0x060030F2 RID: 12530
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr CreateCustomMarshalerHelper(IntPtr pMD, int paramToken, IntPtr hndManagedType);

		// Token: 0x060030F3 RID: 12531 RVA: 0x00168645 File Offset: 0x00167845
		internal static IntPtr SafeHandleAddRef(SafeHandle pHandle, ref bool success)
		{
			if (pHandle == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.pHandle, ExceptionResource.ArgumentNull_SafeHandle);
			}
			pHandle.DangerousAddRef(ref success);
			return pHandle.DangerousGetHandle();
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x00168660 File Offset: 0x00167860
		internal static void SafeHandleRelease(SafeHandle pHandle)
		{
			if (pHandle == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.pHandle, ExceptionResource.ArgumentNull_SafeHandle);
			}
			pHandle.DangerousRelease();
		}

		// Token: 0x060030F5 RID: 12533
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget, out bool pfNeedsRelease);

		// Token: 0x060030F6 RID: 12534
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ProfilerBeginTransitionCallback(IntPtr pSecretParam, IntPtr pThread, object pThis);

		// Token: 0x060030F7 RID: 12535
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ProfilerEndTransitionCallback(IntPtr pMD, IntPtr pThread);

		// Token: 0x060030F8 RID: 12536 RVA: 0x00168674 File Offset: 0x00167874
		internal static void CheckStringLength(int length)
		{
			StubHelpers.CheckStringLength((uint)length);
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x0016867C File Offset: 0x0016787C
		internal static void CheckStringLength(uint length)
		{
			if (length > 2147483632U)
			{
				throw new MarshalDirectiveException(SR.Marshaler_StringTooLong);
			}
		}

		// Token: 0x060030FA RID: 12538
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FmtClassUpdateNativeInternal(object obj, byte* pNative, ref CleanupWorkListElement pCleanupWorkList);

		// Token: 0x060030FB RID: 12539
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FmtClassUpdateCLRInternal(object obj, byte* pNative);

		// Token: 0x060030FC RID: 12540
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void LayoutDestroyNativeInternal(object obj, byte* pNative);

		// Token: 0x060030FD RID: 12541
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AllocateInternal(IntPtr typeHandle);

		// Token: 0x060030FE RID: 12542
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MarshalToUnmanagedVaListInternal(IntPtr va_list, uint vaListSize, IntPtr pArgIterator);

		// Token: 0x060030FF RID: 12543
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MarshalToManagedVaListInternal(IntPtr va_list, IntPtr pArgIterator);

		// Token: 0x06003100 RID: 12544
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint CalcVaListSize(IntPtr va_list);

		// Token: 0x06003101 RID: 12545
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ValidateObject(object obj, IntPtr pMD, object pThis);

		// Token: 0x06003102 RID: 12546
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogPinnedArgument(IntPtr localDesc, IntPtr nativeArg);

		// Token: 0x06003103 RID: 12547
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ValidateByref(IntPtr byref, IntPtr pMD, object pThis);

		// Token: 0x06003104 RID: 12548
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetStubContext();

		// Token: 0x06003105 RID: 12549
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetStubContextAddr();

		// Token: 0x06003106 RID: 12550
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ArrayTypeCheck(object o, object[] arr);

		// Token: 0x06003107 RID: 12551
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MulticastDebuggerTraceHelper(object o, int count);

		// Token: 0x06003108 RID: 12552
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr NextCallReturnAddress();
	}
}
