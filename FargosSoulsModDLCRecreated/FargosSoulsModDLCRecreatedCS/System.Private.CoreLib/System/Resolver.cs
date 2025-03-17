using System;
using System.Reflection;

namespace System
{
	// Token: 0x02000086 RID: 134
	internal abstract class Resolver
	{
		// Token: 0x06000591 RID: 1425
		internal abstract RuntimeType GetJitContext(out int securityControlFlags);

		// Token: 0x06000592 RID: 1426
		internal abstract byte[] GetCodeInfo(out int stackSize, out int initLocals, out int EHCount);

		// Token: 0x06000593 RID: 1427
		internal abstract byte[] GetLocalsSignature();

		// Token: 0x06000594 RID: 1428
		internal unsafe abstract void GetEHInfo(int EHNumber, void* exception);

		// Token: 0x06000595 RID: 1429
		internal abstract byte[] GetRawEHInfo();

		// Token: 0x06000596 RID: 1430
		internal abstract string GetStringLiteral(int token);

		// Token: 0x06000597 RID: 1431
		internal abstract void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle);

		// Token: 0x06000598 RID: 1432
		internal abstract byte[] ResolveSignature(int token, int fromMethod);

		// Token: 0x06000599 RID: 1433
		internal abstract MethodInfo GetDynamicMethod();

		// Token: 0x02000087 RID: 135
		internal struct CORINFO_EH_CLAUSE
		{
			// Token: 0x040001B0 RID: 432
			internal int Flags;

			// Token: 0x040001B1 RID: 433
			internal int TryOffset;

			// Token: 0x040001B2 RID: 434
			internal int TryLength;

			// Token: 0x040001B3 RID: 435
			internal int HandlerOffset;

			// Token: 0x040001B4 RID: 436
			internal int HandlerLength;

			// Token: 0x040001B5 RID: 437
			internal int ClassTokenOrFilterOffset;
		}
	}
}
