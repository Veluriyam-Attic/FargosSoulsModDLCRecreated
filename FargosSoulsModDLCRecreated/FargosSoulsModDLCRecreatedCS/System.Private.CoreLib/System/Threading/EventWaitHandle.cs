using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000299 RID: 665
	[Nullable(0)]
	[NullableContext(1)]
	public class EventWaitHandle : WaitHandle
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x00144E2C File Offset: 0x0014402C
		public EventWaitHandle(bool initialState, EventResetMode mode)
		{
			bool flag;
			this..ctor(initialState, mode, null, out flag);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00144E44 File Offset: 0x00144044
		[NullableContext(2)]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name)
		{
			bool flag;
			this..ctor(initialState, mode, name, out flag);
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00144E5C File Offset: 0x0014405C
		[NullableContext(2)]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew)
		{
			if (mode != EventResetMode.AutoReset && mode != EventResetMode.ManualReset)
			{
				throw new ArgumentException(SR.Argument_InvalidFlag, "mode");
			}
			this.CreateEventCore(initialState, mode, name, out createdNew);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00144E88 File Offset: 0x00144088
		[SupportedOSPlatform("windows")]
		public static EventWaitHandle OpenExisting(string name)
		{
			EventWaitHandle result;
			switch (EventWaitHandle.OpenExistingWorker(name, out result))
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

		// Token: 0x0600276E RID: 10094 RVA: 0x00144EDD File Offset: 0x001440DD
		[SupportedOSPlatform("windows")]
		public static bool TryOpenExisting(string name, [Nullable(2)] [NotNullWhen(true)] out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00144EE9 File Offset: 0x001440E9
		private EventWaitHandle(SafeWaitHandle handle)
		{
			base.SafeWaitHandle = handle;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00144EF8 File Offset: 0x001440F8
		private void CreateEventCore(bool initialState, EventResetMode mode, string name, out bool createdNew)
		{
			uint num = initialState ? 2U : 0U;
			if (mode == EventResetMode.ManualReset)
			{
				num |= 1U;
			}
			SafeWaitHandle safeWaitHandle = Interop.Kernel32.CreateEventEx(IntPtr.Zero, name, num, 34603010U);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!safeWaitHandle.IsInvalid)
			{
				createdNew = (lastWin32Error != 183);
				base.SafeWaitHandle = safeWaitHandle;
				return;
			}
			safeWaitHandle.SetHandleAsInvalid();
			if (!string.IsNullOrEmpty(name) && lastWin32Error == 6)
			{
				throw new WaitHandleCannotBeOpenedException(SR.Format(SR.Threading_WaitHandleCannotBeOpenedException_InvalidHandle, name));
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, name);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x00144F78 File Offset: 0x00144178
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, out EventWaitHandle result)
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
			SafeWaitHandle safeWaitHandle = Interop.Kernel32.OpenEvent(34603010U, false, name);
			if (!safeWaitHandle.IsInvalid)
			{
				result = new EventWaitHandle(safeWaitHandle);
				return WaitHandle.OpenExistingResult.Success;
			}
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
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, name);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00144FFC File Offset: 0x001441FC
		public bool Reset()
		{
			bool flag = Interop.Kernel32.ResetEvent(base.SafeWaitHandle);
			if (!flag)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			return flag;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00145024 File Offset: 0x00144224
		public bool Set()
		{
			bool flag = Interop.Kernel32.SetEvent(base.SafeWaitHandle);
			if (!flag)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			return flag;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0014504C File Offset: 0x0014424C
		internal static bool Set(SafeWaitHandle waitHandle)
		{
			return Interop.Kernel32.SetEvent(waitHandle);
		}
	}
}
