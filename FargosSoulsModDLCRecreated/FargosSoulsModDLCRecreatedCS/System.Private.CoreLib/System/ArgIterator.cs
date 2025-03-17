using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200004F RID: 79
	public ref struct ArgIterator
	{
		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ArgIterator(IntPtr arglist);

		// Token: 0x060000E0 RID: 224 RVA: 0x000AB272 File Offset: 0x000AA472
		public ArgIterator(RuntimeArgumentHandle arglist)
		{
			this = new ArgIterator(arglist.Value);
		}

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern ArgIterator(IntPtr arglist, void* ptr);

		// Token: 0x060000E2 RID: 226 RVA: 0x000AB281 File Offset: 0x000AA481
		[CLSCompliant(false)]
		public unsafe ArgIterator(RuntimeArgumentHandle arglist, void* ptr)
		{
			this = new ArgIterator(arglist.Value, ptr);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000AB294 File Offset: 0x000AA494
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg()
		{
			TypedReference result = default(TypedReference);
			this.FCallGetNextArg((void*)(&result));
			return result;
		}

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void FCallGetNextArg(void* result);

		// Token: 0x060000E5 RID: 229 RVA: 0x000AB2B4 File Offset: 0x000AA4B4
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg(RuntimeTypeHandle rth)
		{
			if (this.sigPtr != IntPtr.Zero)
			{
				return this.GetNextArg();
			}
			if (this.ArgPtr == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			TypedReference result = default(TypedReference);
			this.InternalGetNextArg((void*)(&result), rth.GetRuntimeType());
			return result;
		}

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void InternalGetNextArg(void* result, RuntimeType rt);

		// Token: 0x060000E7 RID: 231 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void End()
		{
		}

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetRemainingCount();

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* _GetNextArgType();

		// Token: 0x060000EA RID: 234 RVA: 0x000AB30D File Offset: 0x000AA50D
		public RuntimeTypeHandle GetNextArgType()
		{
			return new RuntimeTypeHandle(Type.GetTypeFromHandleUnsafe((IntPtr)this._GetNextArgType()));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000AB324 File Offset: 0x000AA524
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.ArgCookie);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000AB331 File Offset: 0x000AA531
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			throw new NotSupportedException(SR.NotSupported_NYI);
		}

		// Token: 0x040000CD RID: 205
		private IntPtr ArgCookie;

		// Token: 0x040000CE RID: 206
		private IntPtr sigPtr;

		// Token: 0x040000CF RID: 207
		private IntPtr sigPtrLen;

		// Token: 0x040000D0 RID: 208
		private IntPtr ArgPtr;

		// Token: 0x040000D1 RID: 209
		private int RemainingArgs;
	}
}
