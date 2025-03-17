using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006C4 RID: 1732
	[NullableContext(2)]
	[Nullable(0)]
	public static class Debugger
	{
		// Token: 0x06005841 RID: 22593 RVA: 0x001AFD2F File Offset: 0x001AEF2F
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Break()
		{
			Debugger.BreakInternal();
		}

		// Token: 0x06005842 RID: 22594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BreakInternal();

		// Token: 0x06005843 RID: 22595 RVA: 0x001AFD36 File Offset: 0x001AEF36
		public static bool Launch()
		{
			return Debugger.IsAttached || Debugger.LaunchInternal();
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001AFD46 File Offset: 0x001AEF46
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void NotifyOfCrossThreadDependencySlow()
		{
			Debugger.CustomNotification(new Debugger.CrossThreadDependencyNotification());
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x001AFD52 File Offset: 0x001AEF52
		public static void NotifyOfCrossThreadDependency()
		{
			if (Debugger.IsAttached)
			{
				Debugger.NotifyOfCrossThreadDependencySlow();
			}
		}

		// Token: 0x06005846 RID: 22598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LaunchInternal();

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06005847 RID: 22599
		public static extern bool IsAttached { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06005848 RID: 22600
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Log(int level, string category, string message);

		// Token: 0x06005849 RID: 22601
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		// Token: 0x0600584A RID: 22602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CustomNotification(ICustomDebuggerNotification data);

		// Token: 0x0400193F RID: 6463
		public static readonly string DefaultCategory;

		// Token: 0x020006C5 RID: 1733
		private class CrossThreadDependencyNotification : ICustomDebuggerNotification
		{
		}
	}
}
