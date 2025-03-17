using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace System.Threading
{
	// Token: 0x0200026D RID: 621
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class Thread : CriticalFinalizerObject
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x00141432 File Offset: 0x00140632
		private Thread()
		{
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0014143A File Offset: 0x0014063A
		private void Create(ThreadStart start)
		{
			this.SetStartHelper(start, 0);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00141444 File Offset: 0x00140644
		private void Create(ThreadStart start, int maxStackSize)
		{
			this.SetStartHelper(start, maxStackSize);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0014143A File Offset: 0x0014063A
		private void Create(ParameterizedThreadStart start)
		{
			this.SetStartHelper(start, 0);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00141444 File Offset: 0x00140644
		private void Create(ParameterizedThreadStart start, int maxStackSize)
		{
			this.SetStartHelper(start, maxStackSize);
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060025C2 RID: 9666
		public extern int ManagedThreadId { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060025C3 RID: 9667 RVA: 0x00141450 File Offset: 0x00140650
		internal ThreadHandle GetNativeHandle()
		{
			IntPtr dont_USE_InternalThread = this._DONT_USE_InternalThread;
			if (dont_USE_InternalThread == IntPtr.Zero)
			{
				throw new ArgumentException(null, SR.Argument_InvalidHandle);
			}
			return new ThreadHandle(dont_USE_InternalThread);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00141483 File Offset: 0x00140683
		[NullableContext(2)]
		public void Start(object parameter)
		{
			if (this._delegate is ThreadStart)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ThreadWrongThreadStart);
			}
			this._threadStartArg = parameter;
			this.Start();
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x001414AC File Offset: 0x001406AC
		public void Start()
		{
			this.StartupSetApartmentStateInternal();
			if (this._delegate != null)
			{
				ThreadHelper threadHelper = (ThreadHelper)this._delegate.Target;
				ExecutionContext executionContextHelper = ExecutionContext.Capture();
				threadHelper.SetExecutionContextHelper(executionContextHelper);
			}
			this.StartInternal();
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x001414EC File Offset: 0x001406EC
		private void SetCultureOnUnstartedThreadNoCheck(CultureInfo value, bool uiCulture)
		{
			ThreadHelper threadHelper = (ThreadHelper)this._delegate.Target;
			if (uiCulture)
			{
				threadHelper._startUICulture = value;
				return;
			}
			threadHelper._startCulture = value;
		}

		// Token: 0x060025C7 RID: 9671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartInternal();

		// Token: 0x060025C8 RID: 9672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetCurrentThread();

		// Token: 0x060025C9 RID: 9673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SleepInternal(int millisecondsTimeout);

		// Token: 0x060025CA RID: 9674 RVA: 0x0014151C File Offset: 0x0014071C
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.SleepInternal(millisecondsTimeout);
		}

		// Token: 0x060025CB RID: 9675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWaitInternal(int iterations);

		// Token: 0x060025CC RID: 9676 RVA: 0x00141524 File Offset: 0x00140724
		public static void SpinWait(int iterations)
		{
			Thread.SpinWaitInternal(iterations);
		}

		// Token: 0x060025CD RID: 9677
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern Interop.BOOL YieldInternal();

		// Token: 0x060025CE RID: 9678 RVA: 0x0014152C File Offset: 0x0014072C
		public static bool Yield()
		{
			return Thread.YieldInternal() > Interop.BOOL.FALSE;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x00141536 File Offset: 0x00140736
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Thread InitializeCurrentThread()
		{
			return Thread.t_currentThread = Thread.GetCurrentThreadNative();
		}

		// Token: 0x060025D0 RID: 9680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Thread GetCurrentThreadNative();

		// Token: 0x060025D1 RID: 9681 RVA: 0x00141544 File Offset: 0x00140744
		private void SetStartHelper(Delegate start, int maxStackSize)
		{
			ThreadHelper @object = new ThreadHelper(start);
			if (start is ThreadStart)
			{
				this.SetStart(new ThreadStart(@object.ThreadStart), maxStackSize);
				return;
			}
			this.SetStart(new ParameterizedThreadStart(@object.ThreadStart), maxStackSize);
		}

		// Token: 0x060025D2 RID: 9682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStart(Delegate start, int maxStackSize);

		// Token: 0x060025D3 RID: 9683 RVA: 0x00141588 File Offset: 0x00140788
		~Thread()
		{
			this.InternalFinalize();
		}

		// Token: 0x060025D4 RID: 9684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalFinalize();

		// Token: 0x060025D5 RID: 9685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartupSetApartmentStateInternal();

		// Token: 0x060025D6 RID: 9686
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InformThreadNameChange(ThreadHandle t, string name, int len);

		// Token: 0x060025D7 RID: 9687
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern DeserializationTracker GetThreadDeserializationTracker(ref StackCrawlMark stackMark);

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060025D8 RID: 9688
		public extern bool IsAlive { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x001415B4 File Offset: 0x001407B4
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x001415BC File Offset: 0x001407BC
		public bool IsBackground
		{
			get
			{
				return this.IsBackgroundNative();
			}
			set
			{
				this.SetBackgroundNative(value);
			}
		}

		// Token: 0x060025DB RID: 9691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsBackgroundNative();

		// Token: 0x060025DC RID: 9692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBackgroundNative(bool isBackground);

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060025DD RID: 9693
		public extern bool IsThreadPoolThread { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x001415C5 File Offset: 0x001407C5
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x001415CD File Offset: 0x001407CD
		public ThreadPriority Priority
		{
			get
			{
				return (ThreadPriority)this.GetPriorityNative();
			}
			set
			{
				this.SetPriorityNative((int)value);
			}
		}

		// Token: 0x060025E0 RID: 9696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPriorityNative();

		// Token: 0x060025E1 RID: 9697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPriorityNative(int priority);

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x001415D6 File Offset: 0x001407D6
		internal static ulong CurrentOSThreadId
		{
			get
			{
				return Thread.GetCurrentOSThreadId();
			}
		}

		// Token: 0x060025E3 RID: 9699
		[DllImport("QCall")]
		private static extern ulong GetCurrentOSThreadId();

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x001415DD File Offset: 0x001407DD
		public ThreadState ThreadState
		{
			get
			{
				return (ThreadState)this.GetThreadStateNative();
			}
		}

		// Token: 0x060025E5 RID: 9701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetThreadStateNative();

		// Token: 0x060025E6 RID: 9702 RVA: 0x001415E5 File Offset: 0x001407E5
		public ApartmentState GetApartmentState()
		{
			return (ApartmentState)this.GetApartmentStateNative();
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x001415F0 File Offset: 0x001407F0
		private bool TrySetApartmentStateUnchecked(ApartmentState state)
		{
			ApartmentState apartmentState = (ApartmentState)this.SetApartmentStateNative((int)state);
			return (state == ApartmentState.Unknown && apartmentState == ApartmentState.MTA) || apartmentState == state;
		}

		// Token: 0x060025E8 RID: 9704
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetApartmentStateNative();

		// Token: 0x060025E9 RID: 9705
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int SetApartmentStateNative(int state);

		// Token: 0x060025EA RID: 9706
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableComObjectEagerCleanup();

		// Token: 0x060025EB RID: 9707
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Interrupt();

		// Token: 0x060025EC RID: 9708
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Join(int millisecondsTimeout);

		// Token: 0x060025ED RID: 9709
		[DllImport("QCall")]
		private static extern int GetOptimalMaxSpinWaitsPerSpinIterationInternal();

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x00141618 File Offset: 0x00140818
		internal static int OptimalMaxSpinWaitsPerSpinIteration
		{
			get
			{
				int num = Thread.s_optimalMaxSpinWaitsPerSpinIteration;
				if (num == 0)
				{
					return Thread.CalculateOptimalMaxSpinWaitsPerSpinIteration();
				}
				return num;
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00141635 File Offset: 0x00140835
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static int CalculateOptimalMaxSpinWaitsPerSpinIteration()
		{
			Thread.s_optimalMaxSpinWaitsPerSpinIteration = Thread.GetOptimalMaxSpinWaitsPerSpinIterationInternal();
			return Thread.s_optimalMaxSpinWaitsPerSpinIteration;
		}

		// Token: 0x060025F0 RID: 9712
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCurrentProcessorNumber();

		// Token: 0x060025F1 RID: 9713 RVA: 0x00141646 File Offset: 0x00140846
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetCurrentProcessorId()
		{
			if (Thread.s_isProcessorNumberReallyFast)
			{
				return Thread.GetCurrentProcessorNumber();
			}
			return ProcessorIdCache.GetCurrentProcessorId();
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000AB30B File Offset: 0x000AA50B
		internal void ResetThreadPoolThread()
		{
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0014165A File Offset: 0x0014085A
		public Thread(ThreadStart start) : this()
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.Create(start);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00141677 File Offset: 0x00140877
		public Thread(ThreadStart start, int maxStackSize) : this()
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (maxStackSize < 0)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.Create(start, maxStackSize);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x001416A9 File Offset: 0x001408A9
		public Thread(ParameterizedThreadStart start) : this()
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.Create(start);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x001416C6 File Offset: 0x001408C6
		public Thread(ParameterizedThreadStart start, int maxStackSize) : this()
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (maxStackSize < 0)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.Create(start, maxStackSize);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x001416F8 File Offset: 0x001408F8
		private void RequireCurrentThread()
		{
			if (this != Thread.CurrentThread)
			{
				throw new InvalidOperationException(SR.Thread_Operation_RequiresCurrentThread);
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0014170D File Offset: 0x0014090D
		private void SetCultureOnUnstartedThread(CultureInfo value, bool uiCulture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((this.ThreadState & ThreadState.Unstarted) == ThreadState.Running)
			{
				throw new InvalidOperationException(SR.Thread_Operation_RequiresCurrentThread);
			}
			this.SetCultureOnUnstartedThreadNoCheck(value, uiCulture);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0014173A File Offset: 0x0014093A
		private void ThreadNameChanged(string value)
		{
			Thread.InformThreadNameChange(this.GetNativeHandle(), value, (value != null) ? value.Length : 0);
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00141754 File Offset: 0x00140954
		// (set) Token: 0x060025FB RID: 9723 RVA: 0x00141761 File Offset: 0x00140961
		public CultureInfo CurrentCulture
		{
			get
			{
				this.RequireCurrentThread();
				return CultureInfo.CurrentCulture;
			}
			set
			{
				if (this != Thread.CurrentThread)
				{
					this.SetCultureOnUnstartedThread(value, false);
					return;
				}
				CultureInfo.CurrentCulture = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x0014177A File Offset: 0x0014097A
		// (set) Token: 0x060025FD RID: 9725 RVA: 0x00141787 File Offset: 0x00140987
		public CultureInfo CurrentUICulture
		{
			get
			{
				this.RequireCurrentThread();
				return CultureInfo.CurrentUICulture;
			}
			set
			{
				if (this != Thread.CurrentThread)
				{
					this.SetCultureOnUnstartedThread(value, true);
					return;
				}
				CultureInfo.CurrentUICulture = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x001417A0 File Offset: 0x001409A0
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x001417D4 File Offset: 0x001409D4
		[Nullable(2)]
		public static IPrincipal CurrentPrincipal
		{
			[NullableContext(2)]
			get
			{
				AsyncLocal<IPrincipal> asyncLocal = Thread.s_asyncLocalPrincipal;
				IPrincipal principal = (asyncLocal != null) ? asyncLocal.Value : null;
				if (principal == null)
				{
					principal = (Thread.CurrentPrincipal = AppDomain.CurrentDomain.GetThreadPrincipal());
				}
				return principal;
			}
			[NullableContext(2)]
			set
			{
				if (Thread.s_asyncLocalPrincipal == null)
				{
					if (value == null)
					{
						return;
					}
					Interlocked.CompareExchange<AsyncLocal<IPrincipal>>(ref Thread.s_asyncLocalPrincipal, new AsyncLocal<IPrincipal>(), null);
				}
				Thread.s_asyncLocalPrincipal.Value = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x001417FD File Offset: 0x001409FD
		public static Thread CurrentThread
		{
			get
			{
				return Thread.t_currentThread ?? Thread.InitializeCurrentThread();
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x0014180D File Offset: 0x00140A0D
		[Nullable(2)]
		public ExecutionContext ExecutionContext
		{
			[NullableContext(2)]
			get
			{
				return ExecutionContext.Capture();
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00141814 File Offset: 0x00140A14
		// (set) Token: 0x06002603 RID: 9731 RVA: 0x0014181C File Offset: 0x00140A1C
		[Nullable(2)]
		public string Name
		{
			[NullableContext(2)]
			get
			{
				return this._name;
			}
			[NullableContext(2)]
			set
			{
				lock (this)
				{
					if (this._name != null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_WriteOnce);
					}
					this._name = value;
					this.ThreadNameChanged(value);
				}
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00141874 File Offset: 0x00140A74
		[Obsolete("Thread.Abort is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0006", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public void Abort()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ThreadAbort);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00141874 File Offset: 0x00140A74
		[NullableContext(2)]
		[Obsolete("Thread.Abort is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0006", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public void Abort(object stateInfo)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ThreadAbort);
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00141874 File Offset: 0x00140A74
		public static void ResetAbort()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ThreadAbort);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00141880 File Offset: 0x00140A80
		[Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
		public void Suspend()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ThreadSuspend);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00141880 File Offset: 0x00140A80
		[Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
		public void Resume()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ThreadSuspend);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000AB30B File Offset: 0x000AA50B
		public static void BeginCriticalRegion()
		{
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000AB30B File Offset: 0x000AA50B
		public static void EndCriticalRegion()
		{
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000AB30B File Offset: 0x000AA50B
		public static void BeginThreadAffinity()
		{
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000AB30B File Offset: 0x000AA50B
		public static void EndThreadAffinity()
		{
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0014188C File Offset: 0x00140A8C
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Thread.LocalDataStore.AllocateSlot();
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00141893 File Offset: 0x00140A93
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Thread.LocalDataStore.AllocateNamedSlot(name);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0014189B File Offset: 0x00140A9B
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Thread.LocalDataStore.GetNamedSlot(name);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x001418A3 File Offset: 0x00140AA3
		public static void FreeNamedDataSlot(string name)
		{
			Thread.LocalDataStore.FreeNamedSlot(name);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x001418AB File Offset: 0x00140AAB
		[return: Nullable(2)]
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.LocalDataStore.GetData(slot);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x001418B3 File Offset: 0x00140AB3
		public static void SetData(LocalDataStoreSlot slot, [Nullable(2)] object data)
		{
			Thread.LocalDataStore.SetData(slot, data);
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x001418BC File Offset: 0x00140ABC
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x001418C4 File Offset: 0x00140AC4
		[Obsolete("The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.", false)]
		public ApartmentState ApartmentState
		{
			get
			{
				return this.GetApartmentState();
			}
			set
			{
				this.TrySetApartmentState(value);
			}
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x001418CE File Offset: 0x00140ACE
		[SupportedOSPlatform("windows")]
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.TrySetApartmentState(state))
			{
				throw Thread.GetApartmentStateChangeFailedException();
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x001418DF File Offset: 0x00140ADF
		public bool TrySetApartmentState(ApartmentState state)
		{
			if (state > ApartmentState.Unknown)
			{
				throw new ArgumentOutOfRangeException("state", SR.ArgumentOutOfRange_Enum);
			}
			return this.TrySetApartmentStateUnchecked(state);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x001418FC File Offset: 0x00140AFC
		[Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			throw new InvalidOperationException(SR.Thread_GetSetCompressedStack_NotSupported);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x001418FC File Offset: 0x00140AFC
		[Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public void SetCompressedStack(CompressedStack stack)
		{
			throw new InvalidOperationException(SR.Thread_GetSetCompressedStack_NotSupported);
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00141908 File Offset: 0x00140B08
		public static AppDomain GetDomain()
		{
			return AppDomain.CurrentDomain;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000AC09E File Offset: 0x000AB29E
		public static int GetDomainID()
		{
			return 1;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0014190F File Offset: 0x00140B0F
		public override int GetHashCode()
		{
			return this.ManagedThreadId;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00141917 File Offset: 0x00140B17
		public void Join()
		{
			this.Join(-1);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00141921 File Offset: 0x00140B21
		public bool Join(TimeSpan timeout)
		{
			return this.Join(WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0014192F File Offset: 0x00140B2F
		public static void MemoryBarrier()
		{
			Interlocked.MemoryBarrier();
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00141936 File Offset: 0x00140B36
		public static void Sleep(TimeSpan timeout)
		{
			Thread.Sleep(WaitHandle.ToTimeoutMilliseconds(timeout));
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00141943 File Offset: 0x00140B43
		public static byte VolatileRead(ref byte address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0014194B File Offset: 0x00140B4B
		public static double VolatileRead(ref double address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00141953 File Offset: 0x00140B53
		public static short VolatileRead(ref short address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x0014195B File Offset: 0x00140B5B
		public static int VolatileRead(ref int address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00141963 File Offset: 0x00140B63
		public static long VolatileRead(ref long address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0014196B File Offset: 0x00140B6B
		public static IntPtr VolatileRead(ref IntPtr address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00141973 File Offset: 0x00140B73
		[NullableContext(2)]
		[return: NotNullIfNotNull("address")]
		public static object VolatileRead(ref object address)
		{
			return Volatile.Read<object>(ref address);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0014197B File Offset: 0x00140B7B
		[CLSCompliant(false)]
		public static sbyte VolatileRead(ref sbyte address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00141983 File Offset: 0x00140B83
		public static float VolatileRead(ref float address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0014198B File Offset: 0x00140B8B
		[CLSCompliant(false)]
		public static ushort VolatileRead(ref ushort address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00141993 File Offset: 0x00140B93
		[CLSCompliant(false)]
		public static uint VolatileRead(ref uint address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0014199B File Offset: 0x00140B9B
		[CLSCompliant(false)]
		public static ulong VolatileRead(ref ulong address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x001419A3 File Offset: 0x00140BA3
		[CLSCompliant(false)]
		public static UIntPtr VolatileRead(ref UIntPtr address)
		{
			return Volatile.Read(ref address);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x001419AB File Offset: 0x00140BAB
		public static void VolatileWrite(ref byte address, byte value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x001419B4 File Offset: 0x00140BB4
		public static void VolatileWrite(ref double address, double value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x001419BD File Offset: 0x00140BBD
		public static void VolatileWrite(ref short address, short value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x001419C6 File Offset: 0x00140BC6
		public static void VolatileWrite(ref int address, int value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x001419CF File Offset: 0x00140BCF
		public static void VolatileWrite(ref long address, long value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x001419D8 File Offset: 0x00140BD8
		public static void VolatileWrite(ref IntPtr address, IntPtr value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x001419E1 File Offset: 0x00140BE1
		[NullableContext(2)]
		public static void VolatileWrite([NotNullIfNotNull("value")] ref object address, object value)
		{
			Volatile.Write<object>(ref address, value);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x001419EA File Offset: 0x00140BEA
		[CLSCompliant(false)]
		public static void VolatileWrite(ref sbyte address, sbyte value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x001419F3 File Offset: 0x00140BF3
		public static void VolatileWrite(ref float address, float value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x001419FC File Offset: 0x00140BFC
		[CLSCompliant(false)]
		public static void VolatileWrite(ref ushort address, ushort value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00141A05 File Offset: 0x00140C05
		[CLSCompliant(false)]
		public static void VolatileWrite(ref uint address, uint value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00141A0E File Offset: 0x00140C0E
		[CLSCompliant(false)]
		public static void VolatileWrite(ref ulong address, ulong value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00141A17 File Offset: 0x00140C17
		[CLSCompliant(false)]
		public static void VolatileWrite(ref UIntPtr address, UIntPtr value)
		{
			Volatile.Write(ref address, value);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00141A20 File Offset: 0x00140C20
		private static Exception GetApartmentStateChangeFailedException()
		{
			return new InvalidOperationException(SR.Thread_ApartmentState_ChangeFailed);
		}

		// Token: 0x040009E6 RID: 2534
		internal ExecutionContext _executionContext;

		// Token: 0x040009E7 RID: 2535
		internal SynchronizationContext _synchronizationContext;

		// Token: 0x040009E8 RID: 2536
		private string _name;

		// Token: 0x040009E9 RID: 2537
		private Delegate _delegate;

		// Token: 0x040009EA RID: 2538
		private object _threadStartArg;

		// Token: 0x040009EB RID: 2539
		private IntPtr _DONT_USE_InternalThread;

		// Token: 0x040009EC RID: 2540
		private int _priority;

		// Token: 0x040009ED RID: 2541
		private int _managedThreadId;

		// Token: 0x040009EE RID: 2542
		internal const bool IsThreadStartSupported = true;

		// Token: 0x040009EF RID: 2543
		private static int s_optimalMaxSpinWaitsPerSpinIteration;

		// Token: 0x040009F0 RID: 2544
		private static readonly bool s_isProcessorNumberReallyFast = ProcessorIdCache.ProcessorNumberSpeedCheck();

		// Token: 0x040009F1 RID: 2545
		private static AsyncLocal<IPrincipal> s_asyncLocalPrincipal;

		// Token: 0x040009F2 RID: 2546
		[ThreadStatic]
		private static Thread t_currentThread;

		// Token: 0x0200026E RID: 622
		private static class LocalDataStore
		{
			// Token: 0x0600263C RID: 9788 RVA: 0x00141A38 File Offset: 0x00140C38
			public static LocalDataStoreSlot AllocateSlot()
			{
				return new LocalDataStoreSlot(new ThreadLocal<object>());
			}

			// Token: 0x0600263D RID: 9789 RVA: 0x00141A44 File Offset: 0x00140C44
			private static Dictionary<string, LocalDataStoreSlot> EnsureNameToSlotMap()
			{
				Dictionary<string, LocalDataStoreSlot> dictionary = Thread.LocalDataStore.s_nameToSlotMap;
				if (dictionary != null)
				{
					return dictionary;
				}
				dictionary = new Dictionary<string, LocalDataStoreSlot>();
				return Interlocked.CompareExchange<Dictionary<string, LocalDataStoreSlot>>(ref Thread.LocalDataStore.s_nameToSlotMap, dictionary, null) ?? dictionary;
			}

			// Token: 0x0600263E RID: 9790 RVA: 0x00141A74 File Offset: 0x00140C74
			public static LocalDataStoreSlot AllocateNamedSlot(string name)
			{
				LocalDataStoreSlot localDataStoreSlot = Thread.LocalDataStore.AllocateSlot();
				Dictionary<string, LocalDataStoreSlot> dictionary = Thread.LocalDataStore.EnsureNameToSlotMap();
				Dictionary<string, LocalDataStoreSlot> obj = dictionary;
				lock (obj)
				{
					dictionary.Add(name, localDataStoreSlot);
				}
				return localDataStoreSlot;
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x00141AC0 File Offset: 0x00140CC0
			public static LocalDataStoreSlot GetNamedSlot(string name)
			{
				Dictionary<string, LocalDataStoreSlot> dictionary = Thread.LocalDataStore.EnsureNameToSlotMap();
				Dictionary<string, LocalDataStoreSlot> obj = dictionary;
				LocalDataStoreSlot result;
				lock (obj)
				{
					LocalDataStoreSlot localDataStoreSlot;
					if (!dictionary.TryGetValue(name, out localDataStoreSlot))
					{
						localDataStoreSlot = Thread.LocalDataStore.AllocateSlot();
						dictionary[name] = localDataStoreSlot;
					}
					result = localDataStoreSlot;
				}
				return result;
			}

			// Token: 0x06002640 RID: 9792 RVA: 0x00141B1C File Offset: 0x00140D1C
			public static void FreeNamedSlot(string name)
			{
				Dictionary<string, LocalDataStoreSlot> dictionary = Thread.LocalDataStore.EnsureNameToSlotMap();
				Dictionary<string, LocalDataStoreSlot> obj = dictionary;
				lock (obj)
				{
					dictionary.Remove(name);
				}
			}

			// Token: 0x06002641 RID: 9793 RVA: 0x00141B60 File Offset: 0x00140D60
			private static ThreadLocal<object> GetThreadLocal(LocalDataStoreSlot slot)
			{
				if (slot == null)
				{
					throw new ArgumentNullException("slot");
				}
				return slot.Data;
			}

			// Token: 0x06002642 RID: 9794 RVA: 0x00141B76 File Offset: 0x00140D76
			public static object GetData(LocalDataStoreSlot slot)
			{
				return Thread.LocalDataStore.GetThreadLocal(slot).Value;
			}

			// Token: 0x06002643 RID: 9795 RVA: 0x00141B83 File Offset: 0x00140D83
			public static void SetData(LocalDataStoreSlot slot, object value)
			{
				Thread.LocalDataStore.GetThreadLocal(slot).Value = value;
			}

			// Token: 0x040009F3 RID: 2547
			private static Dictionary<string, LocalDataStoreSlot> s_nameToSlotMap;
		}
	}
}
