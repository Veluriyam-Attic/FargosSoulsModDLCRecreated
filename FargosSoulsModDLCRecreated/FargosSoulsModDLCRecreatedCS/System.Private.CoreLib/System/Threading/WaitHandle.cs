using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000279 RID: 633
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class WaitHandle : MarshalByRefObject, IDisposable
	{
		// Token: 0x060026B5 RID: 9909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WaitOneCore(IntPtr waitHandle, int millisecondsTimeout);

		// Token: 0x060026B6 RID: 9910 RVA: 0x00142CDC File Offset: 0x00141EDC
		internal unsafe static int WaitMultipleIgnoringSyncContext(Span<IntPtr> waitHandles, bool waitAll, int millisecondsTimeout)
		{
			fixed (IntPtr* reference = MemoryMarshal.GetReference<IntPtr>(waitHandles))
			{
				IntPtr* waitHandles2 = reference;
				return WaitHandle.WaitMultipleIgnoringSyncContext(waitHandles2, waitHandles.Length, waitAll, millisecondsTimeout);
			}
		}

		// Token: 0x060026B7 RID: 9911
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int WaitMultipleIgnoringSyncContext(IntPtr* waitHandles, int numHandles, bool waitAll, int millisecondsTimeout);

		// Token: 0x060026B8 RID: 9912 RVA: 0x00142D04 File Offset: 0x00141F04
		private static int SignalAndWaitCore(IntPtr waitHandleToSignal, IntPtr waitHandleToWaitOn, int millisecondsTimeout)
		{
			int num = WaitHandle.SignalAndWaitNative(waitHandleToSignal, waitHandleToWaitOn, millisecondsTimeout);
			if (num == 298)
			{
				throw new InvalidOperationException(SR.Threading_WaitHandleTooManyPosts);
			}
			return num;
		}

		// Token: 0x060026B9 RID: 9913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SignalAndWaitNative(IntPtr waitHandleToSignal, IntPtr waitHandleToWaitOn, int millisecondsTimeout);

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00142D2E File Offset: 0x00141F2E
		// (set) Token: 0x060026BC RID: 9916 RVA: 0x00142D49 File Offset: 0x00141F49
		[Obsolete("Use the SafeWaitHandle property instead.")]
		public virtual IntPtr Handle
		{
			get
			{
				if (this._waitHandle != null)
				{
					return this._waitHandle.DangerousGetHandle();
				}
				return WaitHandle.InvalidHandle;
			}
			set
			{
				if (value == WaitHandle.InvalidHandle)
				{
					if (this._waitHandle != null)
					{
						this._waitHandle.SetHandleAsInvalid();
						this._waitHandle = null;
						return;
					}
				}
				else
				{
					this._waitHandle = new SafeWaitHandle(value, true);
				}
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x00142D80 File Offset: 0x00141F80
		// (set) Token: 0x060026BE RID: 9918 RVA: 0x00142DAB File Offset: 0x00141FAB
		public SafeWaitHandle SafeWaitHandle
		{
			get
			{
				SafeWaitHandle result;
				if ((result = this._waitHandle) == null)
				{
					result = (this._waitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false));
				}
				return result;
			}
			[param: AllowNull]
			set
			{
				this._waitHandle = value;
			}
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x00142DB4 File Offset: 0x00141FB4
		internal static int ToTimeoutMilliseconds(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_LessEqualToIntegerMaxVal);
			}
			return (int)num;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00142DFA File Offset: 0x00141FFA
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00142E02 File Offset: 0x00142002
		protected virtual void Dispose(bool explicitDisposing)
		{
			SafeWaitHandle waitHandle = this._waitHandle;
			if (waitHandle == null)
			{
				return;
			}
			waitHandle.Close();
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00142E14 File Offset: 0x00142014
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00142E23 File Offset: 0x00142023
		public virtual bool WaitOne(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return this.WaitOneNoCheck(millisecondsTimeout);
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x00142E40 File Offset: 0x00142040
		private bool WaitOneNoCheck(int millisecondsTimeout)
		{
			SafeWaitHandle waitHandle = this._waitHandle;
			if (waitHandle == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_Generic);
			}
			SafeWaitHandle safeWaitHandle = waitHandle;
			bool flag = false;
			bool result;
			try
			{
				safeWaitHandle.DangerousAddRef(ref flag);
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				int num;
				if (synchronizationContext != null && synchronizationContext.IsWaitNotificationRequired())
				{
					num = synchronizationContext.Wait(new IntPtr[]
					{
						safeWaitHandle.DangerousGetHandle()
					}, false, millisecondsTimeout);
				}
				else
				{
					num = WaitHandle.WaitOneCore(safeWaitHandle.DangerousGetHandle(), millisecondsTimeout);
				}
				if (num == 128)
				{
					throw new AbandonedMutexException();
				}
				result = (num != 258);
			}
			finally
			{
				if (flag)
				{
					safeWaitHandle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x00142EE0 File Offset: 0x001420E0
		private static SafeWaitHandle[] RentSafeWaitHandleArray(int capacity)
		{
			SafeWaitHandle[] array = WaitHandle.t_safeWaitHandlesForRent;
			WaitHandle.t_safeWaitHandlesForRent = null;
			int num = (array != null) ? array.Length : 0;
			if (num < capacity)
			{
				array = new SafeWaitHandle[Math.Max(capacity, Math.Min(64, 2 * num))];
			}
			return array;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00142F1E File Offset: 0x0014211E
		private static void ReturnSafeWaitHandleArray(SafeWaitHandle[] safeWaitHandles)
		{
			WaitHandle.t_safeWaitHandlesForRent = safeWaitHandles;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00142F28 File Offset: 0x00142128
		private unsafe static void ObtainSafeWaitHandles(ReadOnlySpan<WaitHandle> waitHandles, Span<SafeWaitHandle> safeWaitHandles, Span<IntPtr> unsafeWaitHandles)
		{
			bool flag = true;
			SafeWaitHandle safeWaitHandle = null;
			try
			{
				for (int i = 0; i < waitHandles.Length; i++)
				{
					WaitHandle waitHandle = *waitHandles[i];
					if (waitHandle == null)
					{
						throw new ArgumentNullException("waitHandles[" + i.ToString() + "]", SR.ArgumentNull_ArrayElement);
					}
					SafeWaitHandle waitHandle2 = waitHandle._waitHandle;
					if (waitHandle2 == null)
					{
						throw new ObjectDisposedException(null, SR.ObjectDisposed_Generic);
					}
					SafeWaitHandle safeWaitHandle2 = waitHandle2;
					safeWaitHandle = safeWaitHandle2;
					flag = false;
					safeWaitHandle2.DangerousAddRef(ref flag);
					*safeWaitHandles[i] = safeWaitHandle2;
					*unsafeWaitHandles[i] = safeWaitHandle2.DangerousGetHandle();
				}
			}
			catch
			{
				for (int j = 0; j < waitHandles.Length; j++)
				{
					SafeWaitHandle safeWaitHandle3 = *safeWaitHandles[j];
					if (safeWaitHandle3 == null)
					{
						break;
					}
					safeWaitHandle3.DangerousRelease();
					*safeWaitHandles[j] = null;
					if (safeWaitHandle3 == safeWaitHandle)
					{
						safeWaitHandle = null;
						flag = true;
					}
				}
				if (!flag)
				{
					safeWaitHandle.DangerousRelease();
				}
				throw;
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0014301C File Offset: 0x0014221C
		private static int WaitMultiple(WaitHandle[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles", SR.ArgumentNull_Waithandles);
			}
			return WaitHandle.WaitMultiple(new ReadOnlySpan<WaitHandle>(waitHandles), waitAll, millisecondsTimeout);
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00143040 File Offset: 0x00142240
		private unsafe static int WaitMultiple(ReadOnlySpan<WaitHandle> waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyWaithandleArray, "waitHandles");
			}
			if (waitHandles.Length > 64)
			{
				throw new NotSupportedException(SR.NotSupported_MaxWaitHandles);
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			bool flag = synchronizationContext != null && synchronizationContext.IsWaitNotificationRequired();
			SafeWaitHandle[] array = WaitHandle.RentSafeWaitHandleArray(waitHandles.Length);
			int result;
			try
			{
				int num;
				if (flag)
				{
					IntPtr[] array2 = new IntPtr[waitHandles.Length];
					WaitHandle.ObtainSafeWaitHandles(waitHandles, array, array2);
					num = synchronizationContext.Wait(array2, waitAll, millisecondsTimeout);
				}
				else
				{
					int length = waitHandles.Length;
					Span<IntPtr> span = new Span<IntPtr>(stackalloc byte[checked(unchecked((UIntPtr)length) * (UIntPtr)sizeof(IntPtr))], length);
					Span<IntPtr> span2 = span;
					WaitHandle.ObtainSafeWaitHandles(waitHandles, array, span2);
					num = WaitHandle.WaitMultipleIgnoringSyncContext(span2, waitAll, millisecondsTimeout);
				}
				if (num >= 128 && num < 128 + waitHandles.Length)
				{
					if (waitAll)
					{
						throw new AbandonedMutexException();
					}
					num -= 128;
					throw new AbandonedMutexException(num, *waitHandles[num]);
				}
				else
				{
					result = num;
				}
			}
			finally
			{
				for (int i = 0; i < waitHandles.Length; i++)
				{
					if (array[i] != null)
					{
						array[i].DangerousRelease();
						array[i] = null;
					}
				}
				WaitHandle.ReturnSafeWaitHandleArray(array);
			}
			return result;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0014319C File Offset: 0x0014239C
		private static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout)
		{
			if (toSignal == null)
			{
				throw new ArgumentNullException("toSignal");
			}
			if (toWaitOn == null)
			{
				throw new ArgumentNullException("toWaitOn");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			SafeWaitHandle waitHandle = toSignal._waitHandle;
			SafeWaitHandle waitHandle2 = toWaitOn._waitHandle;
			if (waitHandle == null || waitHandle2 == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_Generic);
			}
			bool flag = false;
			bool flag2 = false;
			bool result;
			try
			{
				waitHandle.DangerousAddRef(ref flag);
				waitHandle2.DangerousAddRef(ref flag2);
				int num = WaitHandle.SignalAndWaitCore(waitHandle.DangerousGetHandle(), waitHandle2.DangerousGetHandle(), millisecondsTimeout);
				if (num == 128)
				{
					throw new AbandonedMutexException();
				}
				result = (num != 258);
			}
			finally
			{
				if (flag2)
				{
					waitHandle2.DangerousRelease();
				}
				if (flag)
				{
					waitHandle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00143268 File Offset: 0x00142468
		public virtual bool WaitOne(TimeSpan timeout)
		{
			return this.WaitOneNoCheck(WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00143276 File Offset: 0x00142476
		public virtual bool WaitOne()
		{
			return this.WaitOneNoCheck(-1);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0014327F File Offset: 0x0014247F
		public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			return this.WaitOne(millisecondsTimeout);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00143268 File Offset: 0x00142468
		public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			return this.WaitOneNoCheck(WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00143288 File Offset: 0x00142488
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitMultiple(waitHandles, true, millisecondsTimeout) != 258;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0014329C File Offset: 0x0014249C
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitMultiple(waitHandles, true, WaitHandle.ToTimeoutMilliseconds(timeout)) != 258;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x001432B5 File Offset: 0x001424B5
		public static bool WaitAll(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitMultiple(waitHandles, true, -1) != 258;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x00143288 File Offset: 0x00142488
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			return WaitHandle.WaitMultiple(waitHandles, true, millisecondsTimeout) != 258;
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0014329C File Offset: 0x0014249C
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			return WaitHandle.WaitMultiple(waitHandles, true, WaitHandle.ToTimeoutMilliseconds(timeout)) != 258;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x001432C9 File Offset: 0x001424C9
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitMultiple(waitHandles, false, millisecondsTimeout);
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x001432D3 File Offset: 0x001424D3
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitMultiple(waitHandles, false, WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x001432E2 File Offset: 0x001424E2
		public static int WaitAny(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitMultiple(waitHandles, false, -1);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x001432C9 File Offset: 0x001424C9
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			return WaitHandle.WaitMultiple(waitHandles, false, millisecondsTimeout);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x001432D3 File Offset: 0x001424D3
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			return WaitHandle.WaitMultiple(waitHandles, false, WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x001432EC File Offset: 0x001424EC
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1);
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x001432F6 File Offset: 0x001424F6
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x00143305 File Offset: 0x00142505
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, millisecondsTimeout);
		}

		// Token: 0x04000A16 RID: 2582
		internal const int MaxWaitHandles = 64;

		// Token: 0x04000A17 RID: 2583
		protected static readonly IntPtr InvalidHandle = new IntPtr(-1);

		// Token: 0x04000A18 RID: 2584
		private SafeWaitHandle _waitHandle;

		// Token: 0x04000A19 RID: 2585
		[ThreadStatic]
		private static SafeWaitHandle[] t_safeWaitHandlesForRent;

		// Token: 0x04000A1A RID: 2586
		internal const int WaitSuccess = 0;

		// Token: 0x04000A1B RID: 2587
		internal const int WaitAbandoned = 128;

		// Token: 0x04000A1C RID: 2588
		public const int WaitTimeout = 258;

		// Token: 0x0200027A RID: 634
		internal enum OpenExistingResult
		{
			// Token: 0x04000A1E RID: 2590
			Success,
			// Token: 0x04000A1F RID: 2591
			NameNotFound,
			// Token: 0x04000A20 RID: 2592
			PathNotFound,
			// Token: 0x04000A21 RID: 2593
			NameInvalid
		}
	}
}
