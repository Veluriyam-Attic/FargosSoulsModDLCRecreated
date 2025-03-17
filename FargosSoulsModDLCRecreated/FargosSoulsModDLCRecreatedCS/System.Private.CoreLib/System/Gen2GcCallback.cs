using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200010F RID: 271
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06000E31 RID: 3633 RVA: 0x000D0265 File Offset: 0x000CF465
		private Gen2GcCallback(Func<object, bool> callback, object targetObj)
		{
			this._callback1 = callback;
			this._weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000D0281 File Offset: 0x000CF481
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			new Gen2GcCallback(callback, targetObj);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000D028C File Offset: 0x000CF48C
		protected override void Finalize()
		{
			try
			{
				if (this._weakTargetObj.IsAllocated)
				{
					object target = this._weakTargetObj.Target;
					if (target == null)
					{
						this._weakTargetObj.Free();
						return;
					}
					try
					{
						if (!this._callback1(target))
						{
							return;
						}
						goto IL_54;
					}
					catch
					{
						goto IL_54;
					}
				}
				try
				{
					if (!this._callback0())
					{
						return;
					}
				}
				catch
				{
				}
				IL_54:
				GC.ReRegisterForFinalize(this);
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0400030D RID: 781
		private readonly Func<bool> _callback0;

		// Token: 0x0400030E RID: 782
		private readonly Func<object, bool> _callback1;

		// Token: 0x0400030F RID: 783
		private GCHandle _weakTargetObj;
	}
}
