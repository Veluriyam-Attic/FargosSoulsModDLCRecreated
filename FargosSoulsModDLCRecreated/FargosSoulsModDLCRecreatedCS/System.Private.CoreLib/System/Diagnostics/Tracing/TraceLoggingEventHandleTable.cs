using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000793 RID: 1939
	internal sealed class TraceLoggingEventHandleTable
	{
		// Token: 0x06005D84 RID: 23940 RVA: 0x001C4446 File Offset: 0x001C3646
		internal TraceLoggingEventHandleTable()
		{
			this.m_innerTable = new IntPtr[10];
		}

		// Token: 0x17000F2D RID: 3885
		internal IntPtr this[int eventID]
		{
			get
			{
				IntPtr result = IntPtr.Zero;
				IntPtr[] array = Volatile.Read<IntPtr[]>(ref this.m_innerTable);
				if (eventID >= 0 && eventID < array.Length)
				{
					result = array[eventID];
				}
				return result;
			}
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x001C448C File Offset: 0x001C368C
		internal void SetEventHandle(int eventID, IntPtr eventHandle)
		{
			if (eventID >= this.m_innerTable.Length)
			{
				int num = this.m_innerTable.Length * 2;
				if (num <= eventID)
				{
					num = eventID + 1;
				}
				IntPtr[] array = new IntPtr[num];
				Array.Copy(this.m_innerTable, array, this.m_innerTable.Length);
				Volatile.Write<IntPtr[]>(ref this.m_innerTable, array);
			}
			this.m_innerTable[eventID] = eventHandle;
		}

		// Token: 0x04001C65 RID: 7269
		private IntPtr[] m_innerTable;
	}
}
