using System;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000700 RID: 1792
	internal enum SymAddressKind
	{
		// Token: 0x040019D3 RID: 6611
		ILOffset = 1,
		// Token: 0x040019D4 RID: 6612
		NativeRVA,
		// Token: 0x040019D5 RID: 6613
		NativeRegister,
		// Token: 0x040019D6 RID: 6614
		NativeRegisterRelative,
		// Token: 0x040019D7 RID: 6615
		NativeOffset,
		// Token: 0x040019D8 RID: 6616
		NativeRegisterRegister,
		// Token: 0x040019D9 RID: 6617
		NativeRegisterStack,
		// Token: 0x040019DA RID: 6618
		NativeStackRegister,
		// Token: 0x040019DB RID: 6619
		BitField,
		// Token: 0x040019DC RID: 6620
		NativeSectionOffset
	}
}
