using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000702 RID: 1794
	[NullableContext(1)]
	public interface ISymbolDocumentWriter
	{
		// Token: 0x0600597C RID: 22908
		void SetCheckSum(Guid algorithmId, byte[] checkSum);

		// Token: 0x0600597D RID: 22909
		void SetSource(byte[] source);
	}
}
