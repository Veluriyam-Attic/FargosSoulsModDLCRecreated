using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020002A4 RID: 676
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class Mutex : WaitHandle
	{
		// Token: 0x060027CE RID: 10190 RVA: 0x00146019 File Offset: 0x00145219
		[NullableContext(2)]
		public Mutex(bool initiallyOwned, string name, out bool createdNew)
		{
			this.CreateMutexCore(initiallyOwned, name, out createdNew);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0014602C File Offset: 0x0014522C
		[NullableContext(2)]
		public Mutex(bool initiallyOwned, string name)
		{
			bool flag;
			this.CreateMutexCore(initiallyOwned, name, out flag);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0014604C File Offset: 0x0014524C
		public Mutex(bool initiallyOwned)
		{
			bool flag;
			this.CreateMutexCore(initiallyOwned, null, out flag);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0014606C File Offset: 0x0014526C
		public Mutex()
		{
			bool flag;
			this.CreateMutexCore(false, null, out flag);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x00144EE9 File Offset: 0x001440E9
		private Mutex(SafeWaitHandle handle)
		{
			base.SafeWaitHandle = handle;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0014608C File Offset: 0x0014528C
		public static Mutex OpenExisting(string name)
		{
			Mutex result;
			switch (Mutex.OpenExistingWorker(name, out result))
			{
			case WaitHandle.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case WaitHandle.OpenExistingResult.PathNotFound:
				throw new DirectoryNotFoundException(SR.Format(SR.IO_PathNotFound_Path, name));
			case WaitHandle.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(SR.Format(SR.Threading_WaitHandleCannotBeOpenedException_InvalidHandle, name));
			default:
				return result;
			}
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x001460E1 File Offset: 0x001452E1
		public static bool TryOpenExisting(string name, [NotNullWhen(true)] [Nullable(2)] out Mutex result)
		{
			return Mutex.OpenExistingWorker(name, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x001460F0 File Offset: 0x001452F0
		private void CreateMutexCore(bool initiallyOwned, string name, out bool createdNew)
		{
			uint flags = initiallyOwned ? 1U : 0U;
			SafeWaitHandle safeWaitHandle = Interop.Kernel32.CreateMutexEx(IntPtr.Zero, name, flags, 34603009U);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!safeWaitHandle.IsInvalid)
			{
				createdNew = (lastWin32Error != 183);
				base.SafeWaitHandle = safeWaitHandle;
				return;
			}
			safeWaitHandle.SetHandleAsInvalid();
			if (lastWin32Error == 6)
			{
				throw new WaitHandleCannotBeOpenedException(SR.Format(SR.Threading_WaitHandleCannotBeOpenedException_InvalidHandle, name));
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, name);
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0014615C File Offset: 0x0014535C
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, out Mutex result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			result = null;
			SafeWaitHandle safeWaitHandle = Interop.Kernel32.OpenMutex(34603009U, false, name);
			if (!safeWaitHandle.IsInvalid)
			{
				result = new Mutex(safeWaitHandle);
				return WaitHandle.OpenExistingResult.Success;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (2 == lastWin32Error || 123 == lastWin32Error)
			{
				return WaitHandle.OpenExistingResult.NameNotFound;
			}
			if (3 == lastWin32Error)
			{
				return WaitHandle.OpenExistingResult.PathNotFound;
			}
			if (6 == lastWin32Error)
			{
				return WaitHandle.OpenExistingResult.NameInvalid;
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, name);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x001461D5 File Offset: 0x001453D5
		public void ReleaseMutex()
		{
			if (!Interop.Kernel32.ReleaseMutex(base.SafeWaitHandle))
			{
				throw new ApplicationException(SR.Arg_SynchronizationLockException);
			}
		}
	}
}
