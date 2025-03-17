using System;
using System.Reflection;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020006FF RID: 1791
	internal interface ISymbolWriter
	{
		// Token: 0x0600596F RID: 22895
		ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x06005970 RID: 22896
		void OpenMethod(SymbolToken method);

		// Token: 0x06005971 RID: 22897
		void CloseMethod();

		// Token: 0x06005972 RID: 22898
		void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x06005973 RID: 22899
		int OpenScope(int startOffset);

		// Token: 0x06005974 RID: 22900
		void CloseScope(int endOffset);

		// Token: 0x06005975 RID: 22901
		void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

		// Token: 0x06005976 RID: 22902
		void UsingNamespace(string fullName);
	}
}
