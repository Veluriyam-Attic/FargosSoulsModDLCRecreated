using System;

namespace System.Reflection
{
	// Token: 0x0200059F RID: 1439
	internal readonly struct ConstArray
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x0018640A File Offset: 0x0018560A
		public IntPtr Signature
		{
			get
			{
				return this.m_constArray;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x00186412 File Offset: 0x00185612
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000B36 RID: 2870
		public unsafe byte this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_length)
				{
					throw new IndexOutOfRangeException();
				}
				return ((byte*)this.m_constArray.ToPointer())[index];
			}
		}

		// Token: 0x04001265 RID: 4709
		internal readonly int m_length;

		// Token: 0x04001266 RID: 4710
		internal readonly IntPtr m_constArray;
	}
}
