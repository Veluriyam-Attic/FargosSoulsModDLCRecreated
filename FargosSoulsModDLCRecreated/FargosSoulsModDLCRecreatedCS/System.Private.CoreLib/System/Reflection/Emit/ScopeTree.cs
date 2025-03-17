using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200062A RID: 1578
	internal sealed class ScopeTree
	{
		// Token: 0x06005078 RID: 20600 RVA: 0x00192D61 File Offset: 0x00191F61
		internal ScopeTree()
		{
			this.m_iOpenScopeCount = 0;
			this.m_iCount = 0;
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x00192D78 File Offset: 0x00191F78
		internal int GetCurrentActiveScopeIndex()
		{
			if (this.m_iCount == 0)
			{
				return -1;
			}
			int num = this.m_iCount - 1;
			int num2 = 0;
			while (num2 > 0 || this.m_ScopeActions[num] == ScopeAction.Close)
			{
				num2 = (int)((sbyte)num2 + this.m_ScopeActions[num]);
				num--;
			}
			return num;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x00192DBC File Offset: 0x00191FBC
		internal void AddLocalSymInfoToCurrentScope(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			LocalSymInfo[] localSymInfos = this.m_localSymInfos;
			int num = currentActiveScopeIndex;
			if (localSymInfos[num] == null)
			{
				localSymInfos[num] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddLocalSymInfo(strName, signature, slot, startOffset, endOffset);
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x00192DFC File Offset: 0x00191FFC
		internal void AddUsingNamespaceToCurrentScope(string strNamespace)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			LocalSymInfo[] localSymInfos = this.m_localSymInfos;
			int num = currentActiveScopeIndex;
			if (localSymInfos[num] == null)
			{
				localSymInfos[num] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddUsingNamespace(strNamespace);
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x00192E34 File Offset: 0x00192034
		internal void AddScopeInfo(ScopeAction sa, int iOffset)
		{
			if (sa == ScopeAction.Close && this.m_iOpenScopeCount <= 0)
			{
				throw new ArgumentException(SR.Argument_UnmatchingSymScope);
			}
			this.EnsureCapacity();
			this.m_ScopeActions[this.m_iCount] = sa;
			this.m_iOffsets[this.m_iCount] = iOffset;
			this.m_localSymInfos[this.m_iCount] = null;
			checked
			{
				this.m_iCount++;
			}
			this.m_iOpenScopeCount = (int)((sbyte)this.m_iOpenScopeCount + -(int)sa);
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00192EA8 File Offset: 0x001920A8
		internal void EnsureCapacity()
		{
			if (this.m_iCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_ScopeActions = new ScopeAction[16];
				this.m_localSymInfos = new LocalSymInfo[16];
				return;
			}
			if (this.m_iCount == this.m_iOffsets.Length)
			{
				int num = checked(this.m_iCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iOffsets, array, this.m_iCount);
				this.m_iOffsets = array;
				ScopeAction[] array2 = new ScopeAction[num];
				Array.Copy(this.m_ScopeActions, array2, this.m_iCount);
				this.m_ScopeActions = array2;
				LocalSymInfo[] array3 = new LocalSymInfo[num];
				Array.Copy(this.m_localSymInfos, array3, this.m_iCount);
				this.m_localSymInfos = array3;
			}
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x00192F60 File Offset: 0x00192160
		internal void EmitScopeTree(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iCount; i++)
			{
				if (this.m_ScopeActions[i] == ScopeAction.Open)
				{
					symWriter.OpenScope(this.m_iOffsets[i]);
				}
				else
				{
					symWriter.CloseScope(this.m_iOffsets[i]);
				}
				if (this.m_localSymInfos[i] != null)
				{
					this.m_localSymInfos[i].EmitLocalSymInfo(symWriter);
				}
			}
		}

		// Token: 0x04001485 RID: 5253
		internal int[] m_iOffsets;

		// Token: 0x04001486 RID: 5254
		internal ScopeAction[] m_ScopeActions;

		// Token: 0x04001487 RID: 5255
		internal int m_iCount;

		// Token: 0x04001488 RID: 5256
		internal int m_iOpenScopeCount;

		// Token: 0x04001489 RID: 5257
		internal LocalSymInfo[] m_localSymInfos;
	}
}
