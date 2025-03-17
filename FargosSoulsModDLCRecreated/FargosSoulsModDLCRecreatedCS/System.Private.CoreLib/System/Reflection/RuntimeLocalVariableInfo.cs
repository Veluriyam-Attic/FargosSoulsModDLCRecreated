using System;

namespace System.Reflection
{
	// Token: 0x020005AE RID: 1454
	internal sealed class RuntimeLocalVariableInfo : LocalVariableInfo
	{
		// Token: 0x06004B0A RID: 19210 RVA: 0x001887B9 File Offset: 0x001879B9
		private RuntimeLocalVariableInfo()
		{
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004B0B RID: 19211 RVA: 0x001887C1 File Offset: 0x001879C1
		public override Type LocalType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x001887C9 File Offset: 0x001879C9
		public override int LocalIndex
		{
			get
			{
				return this._localIndex;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004B0D RID: 19213 RVA: 0x001887D1 File Offset: 0x001879D1
		public override bool IsPinned
		{
			get
			{
				return this._isPinned;
			}
		}

		// Token: 0x0400129C RID: 4764
		private RuntimeType _type;

		// Token: 0x0400129D RID: 4765
		private int _localIndex;

		// Token: 0x0400129E RID: 4766
		private bool _isPinned;
	}
}
