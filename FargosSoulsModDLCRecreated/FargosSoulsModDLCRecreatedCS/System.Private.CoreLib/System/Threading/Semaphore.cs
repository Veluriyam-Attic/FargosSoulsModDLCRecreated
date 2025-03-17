using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020002AF RID: 687
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Semaphore : WaitHandle
	{
		// Token: 0x06002820 RID: 10272 RVA: 0x001474AB File Offset: 0x001466AB
		public Semaphore(int initialCount, int maximumCount) : this(initialCount, maximumCount, null)
		{
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x001474B8 File Offset: 0x001466B8
		[NullableContext(2)]
		public Semaphore(int initialCount, int maximumCount, string name)
		{
			bool flag;
			this..ctor(initialCount, maximumCount, name, out flag);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x001474D0 File Offset: 0x001466D0
		[NullableContext(2)]
		public Semaphore(int initialCount, int maximumCount, string name, out bool createdNew)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.Argument_SemaphoreInitialMaximum);
			}
			this.CreateSemaphoreCore(initialCount, maximumCount, name, out createdNew);
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x00147528 File Offset: 0x00146728
		[SupportedOSPlatform("windows")]
		public static Semaphore OpenExisting(string name)
		{
			Semaphore result;
			switch (Semaphore.OpenExistingWorker(name, out result))
			{
			case WaitHandle.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case WaitHandle.OpenExistingResult.PathNotFound:
				throw new IOException(SR.Format(SR.IO_PathNotFound_Path, name));
			case WaitHandle.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(SR.Format(SR.Threading_WaitHandleCannotBeOpenedException_InvalidHandle, name));
			default:
				return result;
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0014757D File Offset: 0x0014677D
		[SupportedOSPlatform("windows")]
		public static bool TryOpenExisting(string name, [Nullable(2)] [NotNullWhen(true)] out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00147589 File Offset: 0x00146789
		public int Release()
		{
			return this.ReleaseCore(1);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x00147592 File Offset: 0x00146792
		public int Release(int releaseCount)
		{
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.ReleaseCore(releaseCount);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00144EE9 File Offset: 0x001440E9
		private Semaphore(SafeWaitHandle handle)
		{
			base.SafeWaitHandle = handle;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x001475B0 File Offset: 0x001467B0
		private void CreateSemaphoreCore(int initialCount, int maximumCount, string name, out bool createdNew)
		{
			SafeWaitHandle safeWaitHandle = Interop.Kernel32.CreateSemaphoreEx(IntPtr.Zero, initialCount, maximumCount, name, 0U, 34603010U);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!safeWaitHandle.IsInvalid)
			{
				createdNew = (lastWin32Error != 183);
				base.SafeWaitHandle = safeWaitHandle;
				return;
			}
			if (!string.IsNullOrEmpty(name) && lastWin32Error == 6)
			{
				throw new WaitHandleCannotBeOpenedException(SR.Format(SR.Threading_WaitHandleCannotBeOpenedException_InvalidHandle, name));
			}
			throw Win32Marshal.GetExceptionForLastWin32Error("");
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0014761C File Offset: 0x0014681C
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, out Semaphore result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			SafeWaitHandle safeWaitHandle = Interop.Kernel32.OpenSemaphore(34603010U, false, name);
			if (!safeWaitHandle.IsInvalid)
			{
				result = new Semaphore(safeWaitHandle);
				return WaitHandle.OpenExistingResult.Success;
			}
			result = null;
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 2 || lastWin32Error == 123)
			{
				return WaitHandle.OpenExistingResult.NameNotFound;
			}
			if (lastWin32Error == 3)
			{
				return WaitHandle.OpenExistingResult.PathNotFound;
			}
			if (!string.IsNullOrEmpty(name) && lastWin32Error == 6)
			{
				return WaitHandle.OpenExistingResult.NameInvalid;
			}
			throw Win32Marshal.GetExceptionForLastWin32Error("");
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x001476A0 File Offset: 0x001468A0
		private int ReleaseCore(int releaseCount)
		{
			int result;
			if (!Interop.Kernel32.ReleaseSemaphore(base.SafeWaitHandle, releaseCount, out result))
			{
				throw new SemaphoreFullException();
			}
			return result;
		}
	}
}
