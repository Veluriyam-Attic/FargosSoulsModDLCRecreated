using System;

namespace System
{
	// Token: 0x0200016B RID: 363
	internal readonly struct ParamsArray
	{
		// Token: 0x06001264 RID: 4708 RVA: 0x000E6AD4 File Offset: 0x000E5CD4
		public ParamsArray(object arg0)
		{
			this._arg0 = arg0;
			this._arg1 = null;
			this._arg2 = null;
			this._args = ParamsArray.s_oneArgArray;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000E6AF6 File Offset: 0x000E5CF6
		public ParamsArray(object arg0, object arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = null;
			this._args = ParamsArray.s_twoArgArray;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000E6B18 File Offset: 0x000E5D18
		public ParamsArray(object arg0, object arg1, object arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._args = ParamsArray.s_threeArgArray;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000E6B3C File Offset: 0x000E5D3C
		public ParamsArray(object[] args)
		{
			int num = args.Length;
			this._arg0 = ((num > 0) ? args[0] : null);
			this._arg1 = ((num > 1) ? args[1] : null);
			this._arg2 = ((num > 2) ? args[2] : null);
			this._args = args;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x000E6B84 File Offset: 0x000E5D84
		public int Length
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x1700019B RID: 411
		public object this[int index]
		{
			get
			{
				if (index != 0)
				{
					return this.GetAtSlow(index);
				}
				return this._arg0;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000E6BA1 File Offset: 0x000E5DA1
		private object GetAtSlow(int index)
		{
			if (index == 1)
			{
				return this._arg1;
			}
			if (index == 2)
			{
				return this._arg2;
			}
			return this._args[index];
		}

		// Token: 0x04000464 RID: 1124
		private static readonly object[] s_oneArgArray = new object[1];

		// Token: 0x04000465 RID: 1125
		private static readonly object[] s_twoArgArray = new object[2];

		// Token: 0x04000466 RID: 1126
		private static readonly object[] s_threeArgArray = new object[3];

		// Token: 0x04000467 RID: 1127
		private readonly object _arg0;

		// Token: 0x04000468 RID: 1128
		private readonly object _arg1;

		// Token: 0x04000469 RID: 1129
		private readonly object _arg2;

		// Token: 0x0400046A RID: 1130
		private readonly object[] _args;
	}
}
