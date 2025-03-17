using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F0 RID: 240
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x000CFFDF File Offset: 0x000CF1DF
		public EntryPointNotFoundException() : base(SR.Arg_EntryPointNotFoundException)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000CFFF7 File Offset: 0x000CF1F7
		public EntryPointNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000D000B File Offset: 0x000CF20B
		public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000CFAD4 File Offset: 0x000CECD4
		[NullableContext(1)]
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
