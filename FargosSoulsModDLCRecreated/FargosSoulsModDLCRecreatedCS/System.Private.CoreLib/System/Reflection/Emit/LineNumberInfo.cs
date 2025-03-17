using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200062B RID: 1579
	internal sealed class LineNumberInfo
	{
		// Token: 0x0600507F RID: 20607 RVA: 0x00192FC0 File Offset: 0x001921C0
		internal LineNumberInfo()
		{
			this.m_DocumentCount = 0;
			this.m_iLastFound = 0;
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00192FD8 File Offset: 0x001921D8
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			int num = this.FindDocument(document);
			this.m_Documents[num].AddLineNumberInfo(document, iOffset, iStartLine, iStartColumn, iEndLine, iEndColumn);
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x00193004 File Offset: 0x00192204
		private int FindDocument(ISymbolDocumentWriter document)
		{
			if (this.m_iLastFound < this.m_DocumentCount && this.m_Documents[this.m_iLastFound].m_document == document)
			{
				return this.m_iLastFound;
			}
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				if (this.m_Documents[i].m_document == document)
				{
					this.m_iLastFound = i;
					return this.m_iLastFound;
				}
			}
			this.EnsureCapacity();
			this.m_iLastFound = this.m_DocumentCount;
			this.m_Documents[this.m_iLastFound] = new REDocument(document);
			checked
			{
				this.m_DocumentCount++;
				return this.m_iLastFound;
			}
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x001930A4 File Offset: 0x001922A4
		private void EnsureCapacity()
		{
			if (this.m_DocumentCount == 0)
			{
				this.m_Documents = new REDocument[16];
				return;
			}
			if (this.m_DocumentCount == this.m_Documents.Length)
			{
				REDocument[] array = new REDocument[this.m_DocumentCount * 2];
				Array.Copy(this.m_Documents, array, this.m_DocumentCount);
				this.m_Documents = array;
			}
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x00193100 File Offset: 0x00192300
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				this.m_Documents[i].EmitLineNumberInfo(symWriter);
			}
		}

		// Token: 0x0400148A RID: 5258
		private int m_DocumentCount;

		// Token: 0x0400148B RID: 5259
		private REDocument[] m_Documents;

		// Token: 0x0400148C RID: 5260
		private int m_iLastFound;
	}
}
