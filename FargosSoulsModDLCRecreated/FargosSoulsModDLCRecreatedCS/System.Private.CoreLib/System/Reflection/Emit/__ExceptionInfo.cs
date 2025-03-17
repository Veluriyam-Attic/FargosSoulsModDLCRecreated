using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000628 RID: 1576
	internal sealed class __ExceptionInfo
	{
		// Token: 0x06005064 RID: 20580 RVA: 0x00192AA0 File Offset: 0x00191CA0
		internal __ExceptionInfo(int startAddr, Label endLabel)
		{
			this.m_startAddr = startAddr;
			this.m_endAddr = -1;
			this.m_filterAddr = new int[4];
			this.m_catchAddr = new int[4];
			this.m_catchEndAddr = new int[4];
			this.m_catchClass = new Type[4];
			this.m_currentCatch = 0;
			this.m_endLabel = endLabel;
			this.m_type = new int[4];
			this.m_endFinally = -1;
			this.m_currentState = 0;
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00192B1C File Offset: 0x00191D1C
		private void MarkHelper(int catchorfilterAddr, int catchEndAddr, Type catchClass, int type)
		{
			int currentCatch = this.m_currentCatch;
			if (currentCatch >= this.m_catchAddr.Length)
			{
				this.m_filterAddr = ILGenerator.EnlargeArray<int>(this.m_filterAddr);
				this.m_catchAddr = ILGenerator.EnlargeArray<int>(this.m_catchAddr);
				this.m_catchEndAddr = ILGenerator.EnlargeArray<int>(this.m_catchEndAddr);
				this.m_catchClass = ILGenerator.EnlargeArray<Type>(this.m_catchClass);
				this.m_type = ILGenerator.EnlargeArray<int>(this.m_type);
			}
			if (type == 1)
			{
				this.m_type[currentCatch] = type;
				this.m_filterAddr[currentCatch] = catchorfilterAddr;
				this.m_catchAddr[currentCatch] = -1;
				if (currentCatch > 0)
				{
					this.m_catchEndAddr[currentCatch - 1] = catchorfilterAddr;
				}
			}
			else
			{
				this.m_catchClass[currentCatch] = catchClass;
				if (this.m_type[currentCatch] != 1)
				{
					this.m_type[currentCatch] = type;
				}
				this.m_catchAddr[currentCatch] = catchorfilterAddr;
				if (currentCatch > 0 && this.m_type[currentCatch] != 1)
				{
					this.m_catchEndAddr[currentCatch - 1] = catchEndAddr;
				}
				this.m_catchEndAddr[currentCatch] = -1;
				this.m_currentCatch++;
			}
			if (this.m_endAddr == -1)
			{
				this.m_endAddr = catchorfilterAddr;
			}
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00192C2A File Offset: 0x00191E2A
		internal void MarkFilterAddr(int filterAddr)
		{
			this.m_currentState = 1;
			this.MarkHelper(filterAddr, filterAddr, null, 1);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x00192C3D File Offset: 0x00191E3D
		internal void MarkFaultAddr(int faultAddr)
		{
			this.m_currentState = 4;
			this.MarkHelper(faultAddr, faultAddr, null, 4);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00192C50 File Offset: 0x00191E50
		internal void MarkCatchAddr(int catchAddr, Type catchException)
		{
			this.m_currentState = 2;
			this.MarkHelper(catchAddr, catchAddr, catchException, 0);
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00192C63 File Offset: 0x00191E63
		internal void MarkFinallyAddr(int finallyAddr, int endCatchAddr)
		{
			if (this.m_endFinally != -1)
			{
				throw new ArgumentException(SR.Argument_TooManyFinallyClause);
			}
			this.m_currentState = 3;
			this.m_endFinally = finallyAddr;
			this.MarkHelper(finallyAddr, endCatchAddr, null, 2);
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x00192C91 File Offset: 0x00191E91
		internal void Done(int endAddr)
		{
			this.m_catchEndAddr[this.m_currentCatch - 1] = endAddr;
			this.m_currentState = 5;
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x00192CAA File Offset: 0x00191EAA
		internal int GetStartAddress()
		{
			return this.m_startAddr;
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x00192CB2 File Offset: 0x00191EB2
		internal int GetEndAddress()
		{
			return this.m_endAddr;
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x00192CBA File Offset: 0x00191EBA
		internal int GetFinallyEndAddress()
		{
			return this.m_endFinally;
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x00192CC2 File Offset: 0x00191EC2
		internal Label GetEndLabel()
		{
			return this.m_endLabel;
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00192CCA File Offset: 0x00191ECA
		internal int[] GetFilterAddresses()
		{
			return this.m_filterAddr;
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x00192CD2 File Offset: 0x00191ED2
		internal int[] GetCatchAddresses()
		{
			return this.m_catchAddr;
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x00192CDA File Offset: 0x00191EDA
		internal int[] GetCatchEndAddresses()
		{
			return this.m_catchEndAddr;
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x00192CE2 File Offset: 0x00191EE2
		internal Type[] GetCatchClass()
		{
			return this.m_catchClass;
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00192CEA File Offset: 0x00191EEA
		internal int GetNumberOfCatches()
		{
			return this.m_currentCatch;
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00192CF2 File Offset: 0x00191EF2
		internal int[] GetExceptionTypes()
		{
			return this.m_type;
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00192CFA File Offset: 0x00191EFA
		internal void SetFinallyEndLabel(Label lbl)
		{
			this.m_finallyEndLabel = lbl;
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x00192D04 File Offset: 0x00191F04
		internal bool IsInner(__ExceptionInfo exc)
		{
			int num = exc.m_currentCatch - 1;
			int num2 = this.m_currentCatch - 1;
			return exc.m_catchEndAddr[num] < this.m_catchEndAddr[num2] || (exc.m_catchEndAddr[num] == this.m_catchEndAddr[num2] && exc.GetEndAddress() > this.GetEndAddress());
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00192D59 File Offset: 0x00191F59
		internal int GetCurrentState()
		{
			return this.m_currentState;
		}

		// Token: 0x04001476 RID: 5238
		internal int m_startAddr;

		// Token: 0x04001477 RID: 5239
		internal int[] m_filterAddr;

		// Token: 0x04001478 RID: 5240
		internal int[] m_catchAddr;

		// Token: 0x04001479 RID: 5241
		internal int[] m_catchEndAddr;

		// Token: 0x0400147A RID: 5242
		internal int[] m_type;

		// Token: 0x0400147B RID: 5243
		internal Type[] m_catchClass;

		// Token: 0x0400147C RID: 5244
		internal Label m_endLabel;

		// Token: 0x0400147D RID: 5245
		internal Label m_finallyEndLabel;

		// Token: 0x0400147E RID: 5246
		internal int m_endAddr;

		// Token: 0x0400147F RID: 5247
		internal int m_endFinally;

		// Token: 0x04001480 RID: 5248
		internal int m_currentCatch;

		// Token: 0x04001481 RID: 5249
		private int m_currentState;
	}
}
