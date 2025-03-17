using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x020002C3 RID: 707
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x060028A0 RID: 10400 RVA: 0x00149418 File Offset: 0x00148618
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this._tlocal = tlocal;
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x00149427 File Offset: 0x00148627
		public bool IsValueCreated
		{
			get
			{
				return this._tlocal.IsValueCreated;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x00149434 File Offset: 0x00148634
		public T Value
		{
			get
			{
				return this._tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x00149441 File Offset: 0x00148641
		public List<T> Values
		{
			get
			{
				return this._tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x04000ADD RID: 2781
		private readonly ThreadLocal<T> _tlocal;
	}
}
